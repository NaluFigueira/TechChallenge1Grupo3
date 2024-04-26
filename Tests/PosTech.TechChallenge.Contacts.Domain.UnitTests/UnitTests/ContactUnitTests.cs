using FluentValidation.TestHelper;

using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;
using PosTech.TechChallenge.Contacts.Validation;

namespace PosTech.TechChallenge.Contacts.Domain.UnitTests;

public class ContactUnitTests
{
    private readonly ContactValidator _validator;

    public ContactUnitTests()
    {
        _validator = new ContactValidator();
    }

    [Fact]
    public void ContactEntity_ShouldHaveId_WhenNewInstanceIsCreated()
    {
        var contact = new Contact
        {
            Name = "Exemple Exemple",
            DDD = DDDBrazil.DDD_55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        Assert.IsType<Guid>(contact.Id);
        Assert.NotEqual(contact.Id, Guid.Empty);
    }

    [Fact]
    public void ContactValidation_ShouldPointValidationError_WhenNameIsEmpty()
    {
        var contact = new Contact
        {
            Name = "",
            DDD = DDDBrazil.DDD_55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Fact]
    public void ContactValidation_ShouldPointValidationError_WhenDDDIsEmpty()
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = (DDDBrazil)0,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.DDD)
            .WithErrorMessage("DDD is required.");
    }

    [Theory]
    [InlineData("", "Phone number is required.")]
    [InlineData("1234-5678", "Phone number must be 8 or 9 digits.")]
    [InlineData("55912345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("2345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("888888888", "Phone number with 9 digits must start with '9'.")]
    public void ContactValidation_ShouldPointValidationError_WhenPhoneNumberIsInvalidFormat(
        string phoneNumber,
        string expectedMessage
    )
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD_55,
            PhoneNumber = phoneNumber,
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber)
            .WithErrorMessage(expectedMessage);
    }

    [Theory]
    [InlineData("", "Email is required.")]
    [InlineData("invalidEmail.com", "A valid email is required.")]
    public void ContactValidation_ShouldPointValidationError_WhenEmailIsInvalidFormat(
        string email,
        string expectedMessage
    )
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD_55,
            PhoneNumber = "123456789",
            Email = email
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public void ContactValidation_ShouldPointTwoValidationErrors_WhenThereIsTwoInvalidFiels()
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD_55,
            PhoneNumber = "+55 (55) 98765-4321",
            Email = ""
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Theory]
    [InlineData("John Doe", DDDBrazil.DDD_11, "987654321", "john.doe@example.com")]
    [InlineData("Alice Johnson", DDDBrazil.DDD_21, "976543219", "alice.johnson@example.net")]
    [InlineData("Bob Brown", DDDBrazil.DDD_31, "965432198", "bob.brown@example.org")]
    [InlineData("Clara Sky", DDDBrazil.DDD_41, "954321987", "clara.sky@example.co.uk")]
    [InlineData("Daniel Moon", DDDBrazil.DDD_51, "943219876", "d.moon@moonlight.io")]
    [InlineData("Eva Storm", DDDBrazil.DDD_61, "932198765", "eva.storm@cloud.com")]
    [InlineData("Finn Gale", DDDBrazil.DDD_71, "921987654", "finn.gale@example.com")]
    [InlineData("Grace Field", DDDBrazil.DDD_81, "919876543", "grace.field@fields.net")]
    [InlineData("Hector Sage", DDDBrazil.DDD_91, "998765432", "hector.sage@sage.org")]
    [InlineData("Isla Frost", DDDBrazil.DDD_19, "923456789", "isla.frost@frost.co.uk")]
    public void ContactValidation_ShouldNotPointValidationError_WhenAllFieldsAreValid(
        string name,
        DDDBrazil ddd,
        string phoneNumber,
        string email
    )
    {
        var contact = new Contact
        {
            Name = name,
            DDD = ddd,
            PhoneNumber = phoneNumber,
            Email = email
        };

        var result = _validator.TestValidate(contact);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
