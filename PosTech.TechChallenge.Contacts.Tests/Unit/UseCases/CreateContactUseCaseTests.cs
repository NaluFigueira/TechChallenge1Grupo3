﻿using Moq;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;
using PosTech.TechChallenge.Contacts.Application;
using Microsoft.Extensions.Logging;

namespace PosTech.TechChallenge.Contacts.Tests;

public class CreateContactUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<CreateContactUseCase>>();

        var request = new CreateContactDTOBuilder().Build();
        var expectedContact = new Contact
        {
            Name = request.Name,
            DDD = request.DDD,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        mockRepository
            .Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedContact);

        var useCase = new CreateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result.Value);
        Assert.True(result.IsSuccess);

        Assert.Equal(expectedContact.Id, result.Value.Id);
        Assert.Equal(expectedContact.Name, result.Value.Name);
        Assert.Equal(expectedContact.DDD, result.Value.DDD);
        Assert.Equal(expectedContact.PhoneNumber, result.Value.PhoneNumber);
        Assert.Equal(expectedContact.Email, result.Value.Email);

        mockRepository.Verify(repo => repo.CreateContactAsync(It.Is<Contact>(c =>
            c.Name == request.Name &&
            c.DDD == request.DDD &&
            c.PhoneNumber == request.PhoneNumber &&
            c.Email == request.Email
        )), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenContactHasInvalidFields_ShouldReturnResultFail()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<CreateContactUseCase>>();

        var request = new CreateContactDTOBuilder().WithPhoneNumber("").Build();
        var expectedContact = new Contact
        {
            Name = request.Name,
            DDD = request.DDD,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        mockRepository
            .Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>()))
            .ThrowsAsync(new Exception("Validation failed."));

        var useCase = new CreateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);

        mockRepository.Verify(repo => repo.CreateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
