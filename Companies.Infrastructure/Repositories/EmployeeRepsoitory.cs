using Companies.Infrastructure.Data;

namespace Companies.Infrastructure.Repositories;

public class EmployeeRepsoitory
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepsoitory(ApplicationDbContext context)
    {
        _context = context;
    }
}
