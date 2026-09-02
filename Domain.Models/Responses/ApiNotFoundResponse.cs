namespace Domain.Models.Responses;

public abstract class ApiNotFoundResponse : ApiBaseResponse
{
    public string Message { get; }

    protected ApiNotFoundResponse(string message)
        : base(false)
    {
        Message = message;
    }
}
