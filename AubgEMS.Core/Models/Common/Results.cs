namespace AubgEMS.Core.Models.Common;

public class Result
{
    public bool Succeeded { get; }
    public string? Error { get; }

    // must be protected so Result<T> can call it
    protected Result(bool succeeded, string? error)
    {
        Succeeded = succeeded;
        Error = error;
    }

    public static Result Ok()               => new Result(true, null);
    public static Result Fail(string error) => new Result(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }
    
    protected Result(bool succeeded, string? error, T? value)
        : base(succeeded, error)
        => Value = value;

    public static Result<T> Ok(T value)         => new Result<T>(true, null, value);
    public static new Result<T> Fail(string error) => new Result<T>(false, error, default);
}