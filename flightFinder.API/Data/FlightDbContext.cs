using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using flightFinder.API.Models;

namespace flightFinder.API.Data;

public class FlightDbContext : DbContext
{
    
    public FlightDbContext(DbContextOptions<FlightDbContext> options)
        : base(options)
    {
    }
    public DbSet<FlightRoute> FlightRoutes { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Price> Prices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>()
            .Property(p => p.Adult)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Price>()
            .Property(p => p.Child)
            .HasColumnType("decimal(18,2)");
    }
}



