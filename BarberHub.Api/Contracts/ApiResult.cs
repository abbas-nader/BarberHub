using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Contracts;

public class ApiResult(bool success, int status, object? error = null, string? location = null) : IActionResult
{
    public bool Success { get; } = success;
    public int Status { get; } = status;
    public object? Error { get; } = error;

    public static ApiResult Succeeded(int status = StatusCodes.Status200OK)
        => new(true, status);

    public static ApiResult Failed(object? error, int status)
        => new(false, status, error);

    public static ApiResult NoContent(int status = StatusCodes.Status204NoContent)
        => new(true, status);

    public Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Items[typeof(ApiResult)] = true;

        if (location is not null)
            context.HttpContext.Response.Headers.Location = location;

        return new ObjectResult(this)
        {
            StatusCode = Status
        }.ExecuteResultAsync(context);
    }
}

public class ApiResult<T>(T? data,bool success, int status, object? error= null, string? location = null)
    : ApiResult(success, status, error, location)
{
    public T? Data { get; } = data;

    public static ApiResult<T> Succeeded(T? data, int status = StatusCodes.Status200OK)
        => new(data, true, status);

    public new static ApiResult<T> Failed(object? error, int status)
        => new(default, false, status, error);

    public static ApiResult<T> Created(T? data, string? location)
        => new(data, true, StatusCodes.Status201Created, location: location);

    public static ApiResult<T> NoContent()
        => new(default, true, StatusCodes.Status204NoContent);

    public static implicit operator ApiResult<T>(T? data) => Succeeded(data);
}