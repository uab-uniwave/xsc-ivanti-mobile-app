namespace Application.Exceptions;

/// <summary>
/// Base exception for all application layer errors.
/// Provides a consistent exception hierarchy for the Application layer.
/// </summary>
public class ApplicationException : System.Exception
{
    public ApplicationException(string message) 
        : base(message)
    {
    }

    public ApplicationException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}
