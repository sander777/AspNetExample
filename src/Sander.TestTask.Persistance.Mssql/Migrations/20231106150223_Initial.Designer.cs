﻿// <auto-generated />
using System;
using Sander.TestTask.Persistance.Mssql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Sander.TestTask.Persistance.Mssql.Migrations
{
    [DbContext(typeof(MarketDbContext))]
    [Migration("20231106150223_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sander.TestTask.Persistance.Mssql.Entities.AuctionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Buyer")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("buyer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("finished_at");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<string>("Seller")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("seller");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int>("item_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("Price");

                    b.HasIndex("Seller");

                    b.HasIndex("Status");

                    b.HasIndex("item_id");

                    b.ToTable("auction");
                });

            modelBuilder.Entity("Sander.TestTask.Persistance.Mssql.Entities.MarketItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("MetaData")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("meta_data");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("market_items");
                });

            modelBuilder.Entity("Sander.TestTask.Persistance.Mssql.Entities.AuctionEntity", b =>
                {
                    b.HasOne("Sander.TestTask.Persistance.Mssql.Entities.MarketItemEntity", "MarketItemEntity")
                        .WithMany()
                        .HasForeignKey("item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketItemEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
