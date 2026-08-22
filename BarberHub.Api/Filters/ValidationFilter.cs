using BarberHub.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberHub.Api.Filters;

public class ValidationFilter : IAsyncResultFilter
{
     public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ApiResult or ApiResult<object>)
        {
            await next();
            return;
        }
        int statusCode;

        switch (context.Result)
        {
            case OkObjectResult okResult:
                statusCode = StatusCodes.Status200OK;
                WrapObjectResult(okResult, statusCode);
                break;
            case CreatedResult createdResult:
                statusCode = StatusCodes.Status201Created;
                WrapObjectResult(createdResult, statusCode);
                break;
            case AcceptedResult acceptedResult:
                statusCode = StatusCodes.Status202Accepted;
                WrapObjectResult(acceptedResult, statusCode);
                break;
            case NotFoundResult:
                statusCode = StatusCodes.Status404NotFound;
                context.Result = new ObjectResult(ApiResult.Failed(null, statusCode))
                { StatusCode = statusCode };
                break;
            case NoContentResult:
                statusCode = StatusCodes.Status204NoContent;
                context.Result = new ObjectResult(ApiResult.NoContent())
                { StatusCode = statusCode };
                break;
            case ObjectResult { StatusCode: not null } objectResult:
                statusCode = objectResult.StatusCode.Value;
                WrapObjectResult(objectResult, statusCode);
                break;
        }

        await next();
    }

    private static void WrapObjectResult(ObjectResult objectResult, int statusCode)
    {
        var apiResult = statusCode is >= 200 and < 300
            ? ApiResult<object?>.Succeeded(objectResult.Value, statusCode)
            : ApiResult.Failed(objectResult.Value, statusCode);

        objectResult.Value = apiResult;
        objectResult.StatusCode = statusCode;
    }
}