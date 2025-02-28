namespace JAT.Core.Domain.Commons.Results;

/// <summary>
/// A global error used for flow control.
/// </summary>
public readonly record struct Error
{
    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    /// <summary>
    /// Creates a validation error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message</param>
    /// <returns>A <see cref="ErrorType.Validation"/> type <see cref="Error"/>.</returns>
    public static Error Validation(
        string code = "General.Validation",
        string message = "A validation error has occurred") => new(code, message, ErrorType.Validation);

    /// <summary>
    /// Creates a not-found error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message</param>
    /// <returns>A <see cref="ErrorType.NotFound"/> type <see cref="Error"/>.</returns>
    public static Error NotFound(
        string code = "General.NotFound",
        string message = "A 'Not Found' error has occurred") => new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Creates an unauthorized error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message</param>
    /// <returns>A <see cref="ErrorType.Unauthorized"/> type <see cref="Error"/>.</returns>
    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string message = "An 'Unauthorized' error has occurred") => new(code, message, ErrorType.Unauthorized);

    /// <summary>
    /// Creates a conflict error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>A <see cref="ErrorType.Conflict"/> type <see cref="Error"/>.</returns>
    public static Error Conflict(
        string code = "General.Conflict",
        string message = "A conflict error has occurred") => new(code, message, ErrorType.Conflict);

    /// <summary>
    /// Creates a forbidden error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>A <see cref="ErrorType.Forbidden"/> type <see cref="Error"/>.</returns>
    public static Error Forbidden(
        string code = "General.Forbidden",
        string message = "A forbidden error has occurred") => new(code, message, ErrorType.Forbidden);

    /// <summary>
    /// Creates a customer <see cref="Error"/>.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message</param>
    /// <param name="statusCode">The status code for the error.</param>
    /// <returns>An <see cref="Error"/>.</returns>
    public static Error Custom(string code, string message, int statusCode) =>
        new(code, message, (ErrorType)statusCode);
}