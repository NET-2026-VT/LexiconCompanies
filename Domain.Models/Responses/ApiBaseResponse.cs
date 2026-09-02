namespace Domain.Models.Responses;

public abstract class ApiBaseResponse
{
    public bool Success { get; }

    protected ApiBaseResponse(bool success)
    {
        Success = success;
    }
}
