using Domain.Models.Responses;

namespace Companies.Presentation.Extensions;

public static class ApiBaseResponseExtensions
{
    public static TResult GetResult<TResult>(this ApiBaseResponse response)
    {
        if (response is ApiOkResponse<TResult> okResponse)
        {
            return okResponse.Result;
        }

        throw new InvalidOperationException(
            $"Response is {response.GetType().Name} and does not contain " +
            $"a result of type {typeof(TResult).Name}.");
    }
}
