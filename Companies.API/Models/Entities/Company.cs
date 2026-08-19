namespace Companies.API.Models.Entities;

public class Company
{
    public Guid Id { get; set; }

    public required string Name { get; set; } 

    //Navigation property
    public Address Address { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = [];

}

public class Address
{
    public Guid Id { get; set; }
    public required string StreetAddress { get; set; }
    public required string City { get; set; }
    public string? Country { get; set; }

    //FK
    public Guid CompanyId { get; set; }

    //Navigation property
    public Company Company { get; set; } = null!;
}
