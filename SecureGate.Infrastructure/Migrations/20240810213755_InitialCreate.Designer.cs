﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureGate.Infrastructure.Data;

#nullable disable

namespace SecureGate.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240810213755_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("SecureGate.Domain.Aggregates.AccessRuleAggregate.AccessRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoorId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AccessRule");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EmployeeAggregate.BioData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BioData");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a36c05fd-5174-4c16-ad4c-8bb1b230e2a3"),
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2591),
                            DateOfBirth = new DateTime(1990, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Adam",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("BioDataId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<bool>("RegistrationApproved")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BioDataId")
                        .IsUnique();

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1088469d-5fb6-401e-9dac-f31a68097489"),
                            BioDataId = new Guid("c5b37ff9-9d69-4b2f-980a-f146ea9cc4c1"),
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2630),
                            PasswordHash = "ba8ffafd98cd9d08d78c753d8c0ed0bbe0aab3fc3640a8c3ffe5459151578ac2101d5382424e1a6e584da2f116d84590b31a1e4ded82c453f370958d695ccbb8",
                            RegistrationApproved = true,
                            RoleId = new Guid("ffc8fc55-c46e-447a-856e-7b9231c0aba7"),
                            Username = "admin@gmail.com"
                        });
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EmployeeAggregate.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffc8fc55-c46e-447a-856e-7b9231c0aba7"),
                            AccessLevel = 3,
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2339),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("8d9ba907-ab15-4a47-ba59-7964656b885e"),
                            AccessLevel = 2,
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2365),
                            Name = "Director"
                        },
                        new
                        {
                            Id = new Guid("800e5e11-4937-4c23-ad8f-39e67120a86b"),
                            AccessLevel = 2,
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2380),
                            Name = "Office Manager"
                        },
                        new
                        {
                            Id = new Guid("be3189ee-8bf0-432d-bfaf-a0db0cc8e61d"),
                            AccessLevel = 1,
                            CreatedAt = new DateTime(2024, 8, 10, 22, 37, 54, 798, DateTimeKind.Utc).AddTicks(2383),
                            Name = "Regular Employee"
                        });
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EventLogAggregate.EventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("AccessGranted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DoorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoorId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EventLogs");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.OfficeAggregate.Door", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OfficeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Doors");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.OfficeAggregate.Office", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.AccessRuleAggregate.AccessRule", b =>
                {
                    b.HasOne("SecureGate.Domain.Aggregates.OfficeAggregate.Door", "Door")
                        .WithMany()
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Door");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", b =>
                {
                    b.HasOne("SecureGate.Domain.Aggregates.EmployeeAggregate.BioData", "BioData")
                        .WithOne()
                        .HasForeignKey("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", "BioDataId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SecureGate.Domain.Aggregates.EmployeeAggregate.Role", "Role")
                        .WithOne()
                        .HasForeignKey("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", "RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BioData");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.EventLogAggregate.EventLog", b =>
                {
                    b.HasOne("SecureGate.Domain.Aggregates.OfficeAggregate.Door", "Door")
                        .WithMany()
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecureGate.Domain.Aggregates.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Door");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.OfficeAggregate.Door", b =>
                {
                    b.HasOne("SecureGate.Domain.Aggregates.OfficeAggregate.Office", "Office")
                        .WithMany("Doors")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("SecureGate.Domain.Aggregates.OfficeAggregate.Office", b =>
                {
                    b.Navigation("Doors");
                });
#pragma warning restore 612, 618
        }
    }
}
