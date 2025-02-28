namespace JAT.Core.Domain.Commons.Results;

/// <summary>
/// Static Result class for returning common successful results without response objects
/// </summary>
public static class Result
{
    public static Success Success => default;
    public static Created Created => default;
}

/// <summary>A Success result usually reported as a 200.</summary>
public readonly record struct Success;
/// <summary>A Created result usually reported as a 201.</summary>
public readonly record struct Created;

/// <summary>
/// A discriminated union of a result or an <see cref="Models.Error"/>
/// </summary>
/// <typeparam name="TValue">The result.</typeparam>
public readonly struct Result<TValue>
{
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors;

    /// <summary>
    /// Custom non-error with an <see cref="ErrorType"/> of None.
    /// </summary>
    private static readonly Error NoErrors = Error.Custom(
        string.Empty, "Error cannot be retrieved from a successful Result", (int)ErrorType.None);

    /// <summary>
    /// Gets a value indicating whether the result is an error.
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public TValue Value => _value!;

    /// <summary>
    /// Gets the errors. If the result is not an error, it will return <see cref="NoErrors"/>.
    /// </summary>
    public List<Error> Errors => IsError ? _errors! : [NoErrors];

    private Result(TValue value)
    {
        _value = value;
        _errors = null;
        IsError = false;
    }

    private Result(Error error)
    {
        _value = default;
        _errors = [error];
        IsError = true;
    }

    private Result(List<Error> errors)
    {
        _value = default;
        _errors = errors;
        IsError = true;
    }

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a value.
    /// </summary>
    /// <param name="value">The successful value for the result.</param>
    /// <returns>A successful <see cref="Result{TValue}"/>.</returns>
    public static implicit operator Result<TValue>(TValue value) => new(value);

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from an <see cref="Models.Error"/>.
    /// </summary>
    /// <param name="error">The <see cref="Models.Error"/> for the result.</param>
    /// <returns>A unsuccessful <see cref="Result{TValue}"/>.</returns>
    public static implicit operator Result<TValue>(Error error) => new(error);

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from a list of <see cref="Models.Error"/>.
    /// </summary>
    /// <param name="errors">The list of <see cref="Models.Error"/> for the result.</param>
    /// <returns>A unsuccessful <see cref="Result{TValue}"/>.</returns>
    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);

    /// <summary>
    /// Creates an <see cref="Result{TValue}"/> from an array of <see cref="Models.Error"/>.
    /// </summary>
    /// <param name="errors">The array of <see cref="Models.Error"/> for the result.</param>
    /// <returns>A unsuccessful <see cref="Result{TValue}"/>.</returns>
    public static implicit operator Result<TValue>(Error[] errors) => new([.. errors]);

    /// <summary>
    /// Executes the appropriate function based on the state of the <see cref="Result{TValue}"/>.
    /// If the state is a value, the provided function <paramref name="onSuccess"/> is executed and its result is returned.
    /// If the state is an error, the provided function <paramref name="onFailure"/> is executed and its result is returned.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The function to execute if the state is a value.</param>
    /// <param name="onFailure">The function to execute if the state is an error.</param>
    /// <returns>The result of the executed function.</returns>
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<List<Error>, TResult> onFailure) =>
        IsError
            ? onFailure(Errors)
            : onSuccess(Value);

    /// <summary>
    /// Asynchronously executes the appropriate function based on the state of the <see cref="Result{TValue}"/>.
    /// If the state is a value, the provided function <paramref name="onSuccess"/> is executed asynchronously and its result is returned.
    /// If the state is an error, the provided function <paramref name="onFailure"/> is executed asynchronously and its result is returned.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="onSuccess">The asynchronous function to execute if the state is a value.</param>
    /// <param name="onFailure">The asynchronous function to execute if the state is an error.</param>
    /// <returns>The result of the executed function.</returns>
    public async Task<TResult> MatchAsync<TResult>(Func<TValue, Task<TResult>> onSuccess, Func<List<Error>, Task<TResult>> onFailure) =>
        IsError
            ? await onFailure(Errors).ConfigureAwait(false)
            : await onSuccess(Value).ConfigureAwait(false);
}