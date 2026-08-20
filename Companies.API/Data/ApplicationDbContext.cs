using Companies.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<Employee> Emplpoyees { get; set; } = default!;
    public DbSet<Position> Positions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>().ToTable("Company");
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Position>().ToTable("Position");
    }
}