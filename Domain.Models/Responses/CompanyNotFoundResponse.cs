namespace Domain.Models.Responses;

public sealed class CompanyNotFoundResponse : ApiNotFoundResponse
{
    public CompanyNotFoundResponse(Guid id)
        : base($"The company with id: {id} was not found")
    {
    }
}
