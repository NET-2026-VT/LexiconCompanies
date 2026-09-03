using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Employee>(options)
{
    public DbSet<Company> Companies { get; set; } = default!;
    //public DbSet<Employee> Users { get; set; } = default!;
    public DbSet<Position> Positions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>().ToTable("Company");
        modelBuilder.Entity<Employee>().ToTable("AspNetUsers");
        modelBuilder.Entity<Position>().ToTable("Position");
    }
}