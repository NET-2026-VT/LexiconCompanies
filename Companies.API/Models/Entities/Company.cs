namespace Companies.API.Models.Entities;

public class Company
{
    public Guid Id { get; set; }

    public required string Name { get; set; } 
    public required string Address { get; set; }
    public string? Country { get; set; }

    //Navigation property
    public IEnumerable<Employee> Employees { get; set; } = [];

}
