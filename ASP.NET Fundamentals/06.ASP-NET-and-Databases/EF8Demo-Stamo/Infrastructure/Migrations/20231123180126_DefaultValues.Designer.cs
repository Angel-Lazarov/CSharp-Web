﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231123180126_DefaultValues")]
    partial class DefaultValues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Data.Models.DefaultValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("The unique identifier for the table.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreditsWithSentinel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10)
                        .HasColumnName("credits_with_sentinel")
                        .HasComment("The default value for the integer with custom sentinel");

                    b.Property<int>("CreditsWithoutSentinel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10)
                        .HasColumnName("credits_without_sentinel")
                        .HasComment("The default value for the integer.");

                    b.Property<bool>("IsActiveDefaultFalse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_active_default_false")
                        .HasComment("The default value for the bool false.");

                    b.Property<bool>("IsActiveDefaultTrue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active_default_true")
                        .HasComment("The default value for the bool true.");

                    b.HasKey("Id")
                        .HasName("pk_default_values");

                    b.ToTable("default_values", null, t =>
                        {
                            t.HasComment("Default value.");
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("The unique identifier for the employee.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasComment("Current email address of the employee.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name")
                        .HasComment("The name of the employee.");

                    b.HasKey("Id")
                        .HasName("pk_employee");

                    b.ToTable("employee", null, t =>
                        {
                            t.HasComment("An employee.");
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("The unique identifier for the person.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name")
                        .HasComment("The name of the person.");

                    b.ComplexProperty<Dictionary<string, object>>("CurrentAddress", "Infrastructure.Data.Models.Person.CurrentAddress#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("current_address_city")
                                .HasComment("The city of the address.");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("current_address_country")
                                .HasComment("The country of the address.");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("current_address_line1")
                                .HasComment("The first line of the address.");

                            b1.Property<string>("Line2")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("current_address_line2")
                                .HasComment("The second line of the address.");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("current_address_post_code")
                                .HasComment("The post code of the address.");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PermanentAddress", "Infrastructure.Data.Models.Person.PermanentAddress#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("permanent_address_city")
                                .HasComment("The city of the address.");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("permanent_address_country")
                                .HasComment("The country of the address.");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("permanent_address_line1")
                                .HasComment("The first line of the address.");

                            b1.Property<string>("Line2")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("permanent_address_line2")
                                .HasComment("The second line of the address.");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("permanent_address_post_code")
                                .HasComment("The post code of the address.");
                        });

                    b.HasKey("Id")
                        .HasName("pk_people");

                    b.ToTable("people", null, t =>
                        {
                            t.HasComment("A person.");
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.PrimitiveCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("The unique identifier for the table.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime[]>("DateTimes")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone[]")
                        .HasColumnName("date_times")
                        .HasComment("Collection of timestamps.");

                    b.Property<DateOnly[]>("Dates")
                        .IsRequired()
                        .HasColumnType("date[]")
                        .HasColumnName("dates")
                        .HasComment("Collection of dates.");

                    b.Property<int[]>("Ints")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("ints")
                        .HasComment("Collection of integers.");

                    b.Property<string[]>("Strings")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("strings")
                        .HasComment("Collection of strings.");

                    b.HasKey("Id")
                        .HasName("pk_primitive_collections");

                    b.ToTable("primitive_collections", null, t =>
                        {
                            t.HasComment("Collections of primitive types.");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTimes = new[] { new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Utc), new DateTime(2021, 2, 1, 2, 2, 2, 0, DateTimeKind.Utc), new DateTime(2021, 3, 1, 3, 3, 3, 0, DateTimeKind.Utc) },
                            Dates = new[] { new DateOnly(2021, 1, 1), new DateOnly(2021, 2, 1), new DateOnly(2021, 3, 1) },
                            Ints = new[] { 1, 2, 3 },
                            Strings = new[] { "one", "two", "three" }
                        },
                        new
                        {
                            Id = 2,
                            DateTimes = new[] { new DateTime(2021, 4, 1, 4, 4, 4, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 5, 5, 5, 0, DateTimeKind.Utc), new DateTime(2021, 6, 1, 6, 6, 6, 0, DateTimeKind.Utc) },
                            Dates = new[] { new DateOnly(2021, 4, 1), new DateOnly(2021, 5, 1), new DateOnly(2021, 6, 1) },
                            Ints = new[] { 4, 5, 6 },
                            Strings = new[] { "four", "five", "six" }
                        },
                        new
                        {
                            Id = 3,
                            DateTimes = new[] { new DateTime(2021, 7, 1, 7, 7, 7, 0, DateTimeKind.Utc), new DateTime(2021, 8, 1, 8, 8, 8, 0, DateTimeKind.Utc), new DateTime(2021, 9, 1, 9, 9, 9, 0, DateTimeKind.Utc) },
                            Dates = new[] { new DateOnly(2021, 7, 1), new DateOnly(2021, 8, 1), new DateOnly(2021, 9, 1) },
                            Ints = new[] { 7, 8, 9 },
                            Strings = new[] { "seven", "eight", "nine" }
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Employee", b =>
                {
                    b.OwnsOne("Infrastructure.Data.Models.Workplace", "Workplace", b1 =>
                        {
                            b1.Property<int>("EmployeeId")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.Property<string>("Address")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("workplace_address")
                                .HasComment("The address of the workplace.");

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("workplace_city")
                                .HasComment("The city of the workplace.");

                            b1.Property<int?>("Floor")
                                .HasColumnType("integer")
                                .HasColumnName("workplace_floor")
                                .HasComment("The floor of the workplace.");

                            b1.Property<string>("Office")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("workplace_office")
                                .HasComment("The name of the workplace.");

                            b1.Property<string[]>("Phones")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("workplace_phones")
                                .HasComment("The phone numbers of the workplace.");

                            b1.Property<int>("RoomNumber")
                                .HasColumnType("integer")
                                .HasColumnName("workplace_room_number")
                                .HasComment("The room number of the workplace.");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("employee", t =>
                                {
                                    t.HasComment("A workplace.");
                                });

                            b1.ToJson("workplace");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employee_employee_id");
                        });

                    b.Navigation("Workplace")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}