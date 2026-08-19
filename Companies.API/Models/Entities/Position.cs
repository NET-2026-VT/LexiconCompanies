namespace Companies.API.Models.Entities;

public class Position
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];

}
