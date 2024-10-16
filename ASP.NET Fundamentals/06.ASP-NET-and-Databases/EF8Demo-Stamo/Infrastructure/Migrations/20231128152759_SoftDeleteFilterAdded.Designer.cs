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
    [Migration("20231128152759_SoftDeleteFilterAdded")]
    partial class SoftDeleteFilterAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Data.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("Book ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("author")
                        .HasComment("Book author");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title")
                        .HasComment("Book title");

                    b.Property<int?>("YearPublished")
                        .HasColumnType("integer")
                        .HasColumnName("year_published")
                        .HasComment("Book year of publishing");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books", null, t =>
                        {
                            t.HasComment("Books table");
                        });
                });

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
                        .HasName("pk_employees");

                    b.ToTable("employees", null, t =>
                        {
                            t.HasComment("An employee.");
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("The unique identifier for the film.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration")
                        .HasComment("The duration of the film.");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title")
                        .HasComment("The title of the film.");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year")
                        .HasComment("The year of the film.");

                    b.HasKey("Id")
                        .HasName("pk_films");

                    b.ToTable("films", null, t =>
                        {
                            t.HasComment("A film.");
                        });
                });

            modelBuilder.Entity("Infrastructure.Data.Models.MedicalPractitioner", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("medical_practitioner_name")
                        .HasComment("Medical practitioner name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("medical_practitioner_telephone")
                        .HasComment("Medical practitioner telephone");

                    b.Property<string>("Uin")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("uin")
                        .HasComment("Medical practitioner UIN");

                    b.HasKey("Id")
                        .HasName("pk_prescriptions");

                    b.ToTable("Prescriptions", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer")
                        .HasColumnName("patient_age")
                        .HasComment("Patient age");

                    b.Property<string>("Ekatte")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("patient_ekatte")
                        .HasComment("Patient EKATTE");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("patient_gender")
                        .HasComment("Patient gender");

                    b.Property<Guid>("Mpi")
                        .HasColumnType("uuid")
                        .HasColumnName("mpi")
                        .HasComment("Patient identifier");

                    b.HasKey("Id")
                        .HasName("pk_prescriptions");

                    b.ToTable("Prescriptions", (string)null);
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

            modelBuilder.Entity("Infrastructure.Data.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("Prescription ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Medication")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("medication")
                        .HasComment("Prescribed medication");

                    b.Property<string>("Nrn")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("nrn")
                        .HasComment("National referent number");

                    b.Property<int>("PatientAge")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer")
                        .HasColumnName("patient_age")
                        .HasComment("Patient age");

                    b.HasKey("Id")
                        .HasName("pk_prescriptions");

                    b.ToTable("Prescriptions", null, t =>
                        {
                            t.HasComment("Prescriptions table");
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
                                .HasColumnType("integer");

                            b1.Property<string>("Address")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasComment("The address of the workplace.");

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasComment("The city of the workplace.");

                            b1.Property<int?>("Floor")
                                .HasColumnType("integer")
                                .HasComment("The floor of the workplace.");

                            b1.Property<string>("Office")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasComment("The name of the workplace.");

                            b1.Property<string[]>("Phones")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasComment("The phone numbers of the workplace.");

                            b1.Property<int>("RoomNumber")
                                .HasColumnType("integer")
                                .HasComment("The room number of the workplace.");

                            b1.HasKey("EmployeeId")
                                .HasName("pk_employees");

                            b1.ToTable("employees", t =>
                                {
                                    t.HasComment("A workplace.");
                                });

                            b1.ToJson("workplace");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employees_employees_employee_id");
                        });

                    b.Navigation("Workplace")
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Data.Models.MedicalPractitioner", b =>
                {
                    b.HasOne("Infrastructure.Data.Models.Prescription", null)
                        .WithOne("MedicalPractitioner")
                        .HasForeignKey("Infrastructure.Data.Models.MedicalPractitioner", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_prescriptions_prescriptions_id");
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Patient", b =>
                {
                    b.HasOne("Infrastructure.Data.Models.Prescription", null)
                        .WithOne("Patient")
                        .HasForeignKey("Infrastructure.Data.Models.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_prescriptions_prescriptions_id");
                });

            modelBuilder.Entity("Infrastructure.Data.Models.Prescription", b =>
                {
                    b.Navigation("MedicalPractitioner")
                        .IsRequired();

                    b.Navigation("Patient")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
