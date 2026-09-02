namespace Domain.Models.Responses;

public sealed class ApiNoContentResponse : ApiBaseResponse
{
    public ApiNoContentResponse()
        : base(true)
    {
    }
}
