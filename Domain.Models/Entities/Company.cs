namespace Domain.Models.Entities;

public class Company
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    //Navigation property
    public Address Address { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = [];

}
