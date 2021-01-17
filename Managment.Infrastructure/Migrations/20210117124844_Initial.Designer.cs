﻿// <auto-generated />
using System;
using Managment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Managment.Infrastructure.Migrations
{
    [DbContext(typeof(AnimalDbContext))]
    [Migration("20210117124844_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Managment.Core.Domain.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Adoptable")
                        .HasColumnType("bit");

                    b.Property<string>("AdoptedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfAdoption")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfArrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfPassing")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EstimatedAge")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("KidFriendly")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReasonAdoptable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResidenceId")
                        .HasColumnType("int");

                    b.Property<bool>("Sterilised")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResidenceId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Managment.Core.Domain.AnimalForAdoption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReasonAdoptable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sterilised")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("AnimalForAdoptions");
                });

            modelBuilder.Entity("Managment.Core.Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Managment.Core.Domain.Interest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("Managment.Core.Domain.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MadeBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VolunteerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WrittenOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Managment.Core.Domain.Residence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Residences");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 4,
                            Gender = 0,
                            Name = "Cage1HondMan4",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 4,
                            Gender = 1,
                            Name = "Cage2KatVrouw4",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 4,
                            Gender = 0,
                            Name = "Cage3KatMan4",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 4,
                            Gender = 1,
                            Name = "Cage4HondVrouw4",
                            Type = 1
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 1,
                            Name = "Cage5"
                        });
                });

            modelBuilder.Entity("Managment.Core.Domain.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AllowedAgeInMonths")
                        .HasColumnType("int");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<double?>("Charge")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PerformedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PerformedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Managment.Core.Domain.Volunteer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Managment.Core.Domain.Animal", b =>
                {
                    b.HasOne("Managment.Core.Domain.Residence", "Residence")
                        .WithMany()
                        .HasForeignKey("ResidenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Residence");
                });

            modelBuilder.Entity("Managment.Core.Domain.AnimalForAdoption", b =>
                {
                    b.HasOne("Managment.Core.Domain.Customer", "Customer")
                        .WithMany("AnimalForAdoptions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Managment.Core.Domain.Interest", b =>
                {
                    b.HasOne("Managment.Core.Domain.Animal", "Animal")
                        .WithMany("Interests")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Managment.Core.Domain.Customer", "Customer")
                        .WithMany("Interests")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Managment.Core.Domain.Note", b =>
                {
                    b.HasOne("Managment.Core.Domain.Animal", "Animal")
                        .WithMany("Notations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Managment.Core.Domain.Volunteer", "Volunteer")
                        .WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Managment.Core.Domain.Treatment", b =>
                {
                    b.HasOne("Managment.Core.Domain.Animal", "Animal")
                        .WithMany("Treatments")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("Managment.Core.Domain.Animal", b =>
                {
                    b.Navigation("Interests");

                    b.Navigation("Notations");

                    b.Navigation("Treatments");
                });

            modelBuilder.Entity("Managment.Core.Domain.Customer", b =>
                {
                    b.Navigation("AnimalForAdoptions");

                    b.Navigation("Interests");
                });
#pragma warning restore 612, 618
        }
    }
}
