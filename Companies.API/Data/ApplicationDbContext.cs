using Companies.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Company> Company { get; set; } = default!;
}