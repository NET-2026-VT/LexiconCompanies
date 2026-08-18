namespace Companies.API.Models.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public  int Age { get; set; }
    public required string Position { get; set; }

    //FK
    public Guid CompanyId { get; set; }

    //Navigation property
    public Company? Company { get; set; }
}
