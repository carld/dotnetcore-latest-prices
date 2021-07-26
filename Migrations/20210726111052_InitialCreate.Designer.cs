﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using latest_prices.Models;

namespace latest_prices.Migrations
{
    [DbContext(typeof(MarketContext))]
    [Migration("20210726111052_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("latest_prices.Models.Instrument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ISIN")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("latest_prices.Models.Price", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("Cents")
                        .HasColumnType("INTEGER")
                        .HasColumnName("price_in_cents");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("published_at");

                    b.Property<string>("Ticker")
                        .HasColumnType("TEXT")
                        .HasColumnName("ticker");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
