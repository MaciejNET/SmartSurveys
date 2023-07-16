namespace SmartSurveys.Core.Results;

public class Result
{
    private Result()
    {
        IsSuccess = true;
    }

    private Result(string error)
    {
        IsSuccess = false;
        Errors = new List<string> {error};
    }
    
    private Result(List<string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> Errors { get; }
    
    public static Result Success() => new();
    public static Result Failure(string error) => new(error);
    public static Result Failure(List<string> errors) => new(errors);
}