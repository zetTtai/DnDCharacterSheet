using DnDCharacterSheet.Application.Common.Interfaces;

public class Result : IResult
{
    public Result()
    {
        Succeeded = true;
        Errors = [];
    }

    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T> : Result, IResult<T>
{
    public T? Value { get; set; }

    public Result() : base() { }

    public Result(bool succeeded, IEnumerable<string> errors, T? value = default)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        return new Result<T>(true, Array.Empty<string>(), value);
    }

    public static new Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(false, errors, default);
    }
}
