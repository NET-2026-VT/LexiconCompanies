namespace Domain.Models.Responses;

public sealed class ApiOkResponse<TResult> : ApiBaseResponse
{
    public TResult Result { get; }

    public ApiOkResponse(TResult result)
        : base(true)
    {
        Result = result;
    }
}
