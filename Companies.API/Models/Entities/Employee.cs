using System.ComponentModel.DataAnnotations;

namespace Companies.API.Models.Entities;

public class Employee
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public  int Age { get; set; }

    //FK
    public Guid CompanyId { get; set; }
    public Guid PositionId { get; set; }

    //Navigation property
    public Company Company { get; set; } = null!;
    public Position Position { get; set; } = null!;
}

public class Position
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];

}
