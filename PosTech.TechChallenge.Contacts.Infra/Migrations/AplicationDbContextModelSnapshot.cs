﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosTech.TechChallenge.Contacts.Infra.Context;

#nullable disable

namespace PosTech.TechChallenge.Contacts.Infra.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PosTech.TechChallenge.Contacts.Domain.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("DDD")
                        .HasMaxLength(2)
                        .HasColumnType("TINYINT")
                        .HasColumnName("DDD");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR(9)")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Contacts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a82b622c-4510-4097-accc-eb5e0de5b6f3"),
                            DDD = (byte)43,
                            Email = "pedro-ferreira85@yahoo.com.br",
                            Name = "Pedro Henrique Erick Ferreira",
                            PhoneNumber = "989340101"
                        },
                        new
                        {
                            Id = new Guid("5a88b2d1-c503-4970-ae5a-564d42d05e59"),
                            DDD = (byte)63,
                            Email = "thomas.pires@credendio.com.br",
                            Name = "Thomas Vinicius João Pires",
                            PhoneNumber = "989769978"
                        },
                        new
                        {
                            Id = new Guid("15c3b39a-088d-4a35-adc3-e18c0700251e"),
                            DDD = (byte)11,
                            Email = "julia92@casabellavidros.com.br",
                            Name = "Julia Milena Rita Almeida",
                            PhoneNumber = "998212236"
                        },
                        new
                        {
                            Id = new Guid("969b2536-d662-4eb0-bcc8-29922ebf913d"),
                            DDD = (byte)21,
                            Email = "bianca_assis@4now.com.br",
                            Name = "Bianca Liz Assis",
                            PhoneNumber = "992804701"
                        },
                        new
                        {
                            Id = new Guid("caecd404-a14f-4035-9b66-e63464de65bd"),
                            DDD = (byte)32,
                            Email = "alessandra75@jovempanfmtaubate.com.br",
                            Name = "Alessandra Gabrielly Esther Costa",
                            PhoneNumber = "985537746"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
