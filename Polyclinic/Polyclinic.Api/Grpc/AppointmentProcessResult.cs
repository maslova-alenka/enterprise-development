namespace Polyclinic.Api.Grpc;

/// <summary>
/// Result of processing one appointment contract.
/// Used instead of tuples for better readability and extensibility.
/// </summary>
public class AppointmentProcessResult
{
    /// <summary>
    /// Indicates successful processing of the appointment.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Error description if processing failed.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Creates a successful appointment processing result.
    /// </summary>
    public static AppointmentProcessResult Ok => new() { Success = true, Error = null };

    /// <summary>
    /// Creates a result with error.
    /// </summary>
    /// <param name="error">Error description.</param>
    public static AppointmentProcessResult Fail(string error) => new() { Success = false, Error = error };
}