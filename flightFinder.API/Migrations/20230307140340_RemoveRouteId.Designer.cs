﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using flightFinder.API.Data;

#nullable disable

namespace flightFinder.API.Migrations
{
    [DbContext(typeof(FlightDbContext))]
    [Migration("20230307140340_RemoveRouteId")]
    partial class RemoveRouteId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("flightFinder.API.Models.Flight", b =>
                {
                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ArrivalAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightRouteRouteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PricesId")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.HasIndex("FlightRouteRouteId");

                    b.HasIndex("PricesId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("flightFinder.API.Models.FlightRoute", b =>
                {
                    b.Property<string>("RouteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArrivalDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RouteId");

                    b.ToTable("FlightRoutes");
                });

            modelBuilder.Entity("flightFinder.API.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Adult")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Child")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("flightFinder.API.Models.Flight", b =>
                {
                    b.HasOne("flightFinder.API.Models.FlightRoute", "FlightRoute")
                        .WithMany("Itineraries")
                        .HasForeignKey("FlightRouteRouteId");

                    b.HasOne("flightFinder.API.Models.Price", "Prices")
                        .WithMany()
                        .HasForeignKey("PricesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightRoute");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("flightFinder.API.Models.FlightRoute", b =>
                {
                    b.Navigation("Itineraries");
                });
#pragma warning restore 612, 618
        }
    }
}
