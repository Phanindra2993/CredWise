public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public object? Errors { get; set; }

    public ApiResponse(T? data = default, string? message = null, bool success = true, object? errors = null)
    {
        Success = success;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public static ApiResponse<T> CreateSuccess(T data, string? message = null)
    {
        return new ApiResponse<T>(data, message, true);
    }

    public static ApiResponse<T> CreateError(string message, object? errors = null)
    {
        return new ApiResponse<T>(default, message, false, errors);
    }
} 