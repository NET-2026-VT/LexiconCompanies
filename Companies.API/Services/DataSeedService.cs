using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Services;

internal class DataSeedService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IConfiguration configuration;
    private readonly ILogger<DataSeedService> logger;
    private List<Position> _positions = [];
    private UserManager<Employee> userManager = null!;
    private RoleManager<IdentityRole> roleManager = null!;
    private const string EmployeeRole = "Employee";
    private const string AdminRole = "Admin";
    private const string TeamLead = "TeamLead";

    public DataSeedService(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<DataSeedService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.configuration = configuration;
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

        userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>()
                            ?? throw new ArgumentNullException();

        roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()
                            ?? throw new ArgumentNullException();

        try
        {

            await CreateRolesAsync([AdminRole, EmployeeRole]);

            _positions =
               [
                  new Position {Name = "Developer"},
                  new Position {Name = "Tester"},
                  new Position {Name = TeamLead},
                ];

            context.AddRange(_positions);
            IEnumerable<Company> companies = GenerateCompanies(10);
            context.Companies.AddRange(companies);
            await GenerateEmployeesAsync(50, companies);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed complete");
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with message: {ex.Message}. Exceeption: {ex.InnerException}");
            throw;
        }
    }

    private async Task CreateRolesAsync(string[] rolenames)
    {
        foreach (string rolename in rolenames)
        {
            if (await roleManager.RoleExistsAsync(rolename)) continue;
            var role = new IdentityRole { Name = rolename };
            var res = await roleManager.CreateAsync(role);

            if (!res.Succeeded) throw new Exception
                    (string.Join("\n", res.Errors.Select(e => $"{e.Code}: {e.Description}")));
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
          //  c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
        });

        return faker.Generate(numberOfCompanies);
    }

    private async Task GenerateEmployeesAsync(int numberofEmployees, IEnumerable<Company> companies)
    {
        var faker = new Faker<Employee>("sv").Rules((f, e) =>
        {
            e.Name = f.Person.FullName;
            e.Age = f.Random.Int(18, 70);
            e.Position = _positions[f.Random.Int(0, _positions.Count - 1)];
            e.Email = f.Person.Email;
            e.UserName = f.Person.UserName;
            e.Company = f.PickRandom(companies);
        });

        var users = faker.Generate(numberofEmployees);

        var teamlead = _positions.First(p => p.Name == TeamLead);

        var password = configuration["password"];
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded) 
                throw new Exception(string.Join("\n", 
                result.Errors.Select(e => $"{e.Code}: {e.Description}")));

            var role = user.Position.Name == TeamLead
                ? AdminRole
                : EmployeeRole;

            await userManager.AddToRoleAsync(user, role);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}