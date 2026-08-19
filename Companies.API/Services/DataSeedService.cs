using Bogus;
using Companies.API.Data;
using Companies.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Services;

internal class DataSeedService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DataSeedService> logger;
    private List<Position> _positions = [];

    public DataSeedService(IServiceProvider serviceProvider, ILogger<DataSeedService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!env.IsDevelopment()) return;

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                            ?? throw new ArgumentNullException();

        if (await context.Companies.AnyAsync(cancellationToken)) return;

        try
        {
             _positions =
                [
                  new Position {Name = "Developer"},
                  new Position {Name = "Tester"},
                  new Position {Name = "Admin"},
                ];

            context.AddRange(_positions);
            IEnumerable<Company> companies = GenerateCompanies(10);
            context.Companies.AddRange(companies);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed complete");
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with message: {ex.Message}. Exceeption: {ex.InnerException}");
            throw;
        }
    }

    private IEnumerable<Company> GenerateCompanies(int numberOfCompanies)
    {
        var faker = new Faker<Company>("sv").Rules((f, c) =>
        {
            c.Name = f.Company.CompanyName();
            c.Address = new Address
            {
                City = f.Address.City(),
                StreetAddress = f.Address.StreetAddress(),
                Country = f.Address.Country()
            };
            c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
        });

        return faker.Generate(numberOfCompanies);
    }

    private ICollection<Employee> GenerateEmployees(int numberofEmployees)
    {
        var faker = new Faker<Employee>("sv").Rules((f, e) =>
        {
            e.Name = f.Person.FullName;
            e.Age = f.Random.Int(18, 70);
            e.Position = _positions[f.Random.Int(0, _positions.Count - 1)];
        });

        return faker.Generate(numberofEmployees);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}