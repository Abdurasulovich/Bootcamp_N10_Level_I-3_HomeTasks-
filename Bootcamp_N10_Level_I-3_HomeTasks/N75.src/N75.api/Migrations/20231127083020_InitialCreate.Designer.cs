﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N75.api.DataContexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace N75.api.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20231127083020_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "hstore");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("N75.api.Models.Common.NotificationEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("ResentAttemps")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Dictionary<string, string>>("Variables")
                        .IsRequired()
                        .HasColumnType("hstore");

                    b.HasKey("Id");

                    b.ToTable("NotificationEvents", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("N75.api.Models.Entites.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("N75.api.Models.Entites.EmailNotificationEvent", b =>
                {
                    b.HasBaseType("N75.api.Models.Common.NotificationEvent");

                    b.Property<string>("ReceiverEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(0);
                });
#pragma warning restore 612, 618
        }
    }
}
