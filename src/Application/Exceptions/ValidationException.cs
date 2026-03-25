namespace Application.Exceptions;

/// <summary>
/// Exception thrown when validation of request or data fails.
/// Use for business logic validation errors.
/// </summary>
public class ValidationException : ApplicationException
{
    public Dictionary<string, string[]>? Errors { get; }

    public ValidationException(string message) 
        : base(message)
    {
    }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }
}
