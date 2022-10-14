﻿// <auto-generated />
using System;
using BookLibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookLibraryApi.Migrations
{
    [DbContext(typeof(BookLibraryApiContext))]
    [Migration("20221013122525_BookKeeperTableUpdate")]
    partial class BookKeeperTableUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookLibraryApi.Models.BookKeeper", b =>
                {
                    b.Property<int>("BookKeeperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookKeeperId"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Expiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsertedBy")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("InsertedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("KeepType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BookKeeperId");

                    b.ToTable("BookKeeper");
                });

            modelBuilder.Entity("BookLibraryApi.Models.Books", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"), 1L, 1);

                    b.Property<string>("Authors")
                        .HasColumnType("nvarchar(1200)");

                    b.Property<float?>("AverageRating")
                        .HasColumnType("real");

                    b.Property<int?>("BookCount")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("ISBN10")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ISBN13")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("InsertedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("KeepType")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("PublishedYear")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("RatingsCount")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subtitle")
                        .HasColumnType("nvarchar(1200)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("BookID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookLibraryApi.Models.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("LastUpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
