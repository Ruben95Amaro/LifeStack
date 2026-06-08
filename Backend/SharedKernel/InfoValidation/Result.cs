using System.Diagnostics.CodeAnalysis;

namespace SharedKernel.InfoValidation;

public class Result
{
    public bool IsSuccess { get; init; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; init; }

    public static Result Success() =>
        new()
        {
            IsSuccess = true,
            Error = Error.None
        };

    public static Result Failure(Error error) =>
        new()
        {
            IsSuccess = false,
            Error = error
        };
}

public class Result<T>
{
    public bool IsSuccess { get; init; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; init; }

    public T? Value { get; init; }

    public static Result<T> Success(T value) =>
        new()
        {
            IsSuccess = true,
            Value = value,
            Error = Error.None
        };

    public static Result<T> Failure(Error error) =>
        new()
        {
            IsSuccess = false,
            Error = error
        };

    public static implicit operator Result<T>(Result result)
    {
        return Failure(result.Error);
    }
}