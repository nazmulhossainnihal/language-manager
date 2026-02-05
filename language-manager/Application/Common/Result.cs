namespace language_manager.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? Error { get; private set; }
    public int StatusCode { get; private set; }

    private Result(bool isSuccess, T? data, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T data, int statusCode = 200) =>
        new(true, data, null, statusCode);

    public static Result<T> Failure(string error, int statusCode = 400) =>
        new(false, default, error, statusCode);

    public static Result<T> NotFound(string error = "Resource not found") =>
        new(false, default, error, 404);

    public static Result<T> Unauthorized(string error = "Unauthorized") =>
        new(false, default, error, 401);

    public static Result<T> Conflict(string error) =>
        new(false, default, error, 409);
}

public class Result
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public int StatusCode { get; private set; }

    private Result(bool isSuccess, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result Success(int statusCode = 200) =>
        new(true, null, statusCode);

    public static Result Failure(string error, int statusCode = 400) =>
        new(false, error, statusCode);

    public static Result NotFound(string error = "Resource not found") =>
        new(false, error, 404);

    public static Result Unauthorized(string error = "Unauthorized") =>
        new(false, error, 401);

    public static Result Conflict(string error) =>
        new(false, error, 409);
}
