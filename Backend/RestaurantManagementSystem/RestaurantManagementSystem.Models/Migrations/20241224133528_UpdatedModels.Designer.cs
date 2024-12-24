﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantManagementSystem.Models;

#nullable disable

namespace RestaurantManagementSystem.Models.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241224133528_UpdatedModels")]
    partial class UpdatedModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantManagementSystem.Models.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<TimeSpan>("FromTime")
                        .HasColumnType("time");

                    b.Property<int>("GuestCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("ToTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1111111-1111-1111-1111-111111111111"),
                            ContactNumber = "+1234567890",
                            CreatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8172),
                            CustomerName = "John Doe",
                            FromTime = new TimeSpan(0, 18, 0, 0, 0),
                            GuestCount = 4,
                            ReservationDate = new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            TableId = new Guid("11111111-1111-1111-1111-111111111111"),
                            ToTime = new TimeSpan(0, 20, 0, 0, 0),
                            UpdatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8315)
                        },
                        new
                        {
                            Id = new Guid("a2222222-2222-2222-2222-222222222222"),
                            ContactNumber = "+1234567891",
                            CreatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8480),
                            CustomerName = "Jane Smith",
                            FromTime = new TimeSpan(0, 19, 0, 0, 0),
                            GuestCount = 6,
                            ReservationDate = new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            TableId = new Guid("22222222-2222-2222-2222-222222222222"),
                            ToTime = new TimeSpan(0, 21, 0, 0, 0),
                            UpdatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 667, DateTimeKind.Utc).AddTicks(8481)
                        });
                });

            modelBuilder.Entity("RestaurantManagementSystem.Models.Models.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SeatingCapacity")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TableNumber")
                        .IsUnique();

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(2883),
                            IsActive = true,
                            Location = "Main Hall",
                            SeatingCapacity = 4,
                            TableNumber = 1,
                            UpdatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3025)
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3161),
                            IsActive = true,
                            Location = "Window Side",
                            SeatingCapacity = 6,
                            TableNumber = 2,
                            UpdatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3161)
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            CreatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3163),
                            IsActive = true,
                            Location = "Outdoor Patio",
                            SeatingCapacity = 8,
                            TableNumber = 3,
                            UpdatedAt = new DateTime(2024, 12, 24, 13, 35, 27, 666, DateTimeKind.Utc).AddTicks(3163)
                        });
                });

            modelBuilder.Entity("RestaurantManagementSystem.Models.Models.Reservation", b =>
                {
                    b.HasOne("RestaurantManagementSystem.Models.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("RestaurantManagementSystem.Models.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}