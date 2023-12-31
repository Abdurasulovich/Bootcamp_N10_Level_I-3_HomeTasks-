﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N76.Api.DbContexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace N76.Api.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20231206065505_string")]
    partial class @string
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("N76.Api.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedByUserid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeletedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ModifiedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
