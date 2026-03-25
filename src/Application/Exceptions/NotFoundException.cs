namespace Application.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found.
/// Use for cases where data lookup fails or entity doesn't exist.
/// </summary>
public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) 
        : base(message)
    {
    }

    public NotFoundException(string resourceName, string identifier)
        : base($"{resourceName} with identifier '{identifier}' was not found.")
    {
    }
}
