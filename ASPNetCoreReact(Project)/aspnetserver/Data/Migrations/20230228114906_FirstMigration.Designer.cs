﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspnetserver.Data;

#nullable disable

namespace aspnetserver.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230228114906_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("aspnetserver.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("whom")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Action")
                       .IsRequired()
                       .HasColumnType("TEXT");

                    b.HasKey("PostId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7236),
                            From = "From AI",
                            Text = "This is task 1 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        },
                        new
                        {
                            PostId = 2,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7266),
                            From = "From AI",
                            Text = "This is task 2 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        },
                        new
                        {
                            PostId = 3,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7268),
                            From = "From AI",
                            Text = "This is task 3 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        },
                        new
                        {
                            PostId = 4,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7270),
                            From = "From AI",
                            Text = "This is task 4 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        },
                        new
                        {
                            PostId = 5,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7272),
                            From = "From AI",
                            Text = "This is task 5 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        },
                        new
                        {
                            PostId = 6,
                            Date = new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7273),
                            From = "From AI",
                            Text = "This is task 6 and do it, please.",
                            whom = "Manager",
                            Action = "Action"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
