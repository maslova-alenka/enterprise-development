namespace Polyclinic.Grpc.Client;

/// <summary>
/// Configuration options for Worker service.
/// </summary>
public class WorkerOptions
{
    public const string SectionName = "Worker";

    /// <summary>
    /// gRPC server address.
    /// </summary>
    public required string ServerAddress { get; init; }

    /// <summary>
    /// Batch size for appointment generation.
    /// </summary>
    public int BatchSize { get; init; } = 50;

    /// <summary>
    /// Interval between batch generations (in seconds).
    /// </summary>
    public int BatchIntervalSeconds { get; init; } = 30;

    /// <summary>
    /// Maximum patient ID in database.
    /// </summary>
    public int MaxPatientId { get; init; } = 10;

    /// <summary>
    /// Maximum doctor ID in database.
    /// </summary>
    public int MaxDoctorId { get; init; } = 10;

    /// <summary>
    /// Maximum number of batches to generate.
    /// </summary>
    public int MaxBatches { get; init; } = 10;
}