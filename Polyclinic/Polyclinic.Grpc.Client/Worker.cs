using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polyclinic.Grpc.Protos;

namespace Polyclinic.Grpc.Client;

/// <summary>
/// Background service for generating and streaming appointment contracts to gRPC server.
/// Works in a loop: generates a batch of appointments, sends via bidirectional streaming,
/// receives status feedback from server and repeats after delay.
/// </summary>
/// <param name="logger">Logger for structured logging of Worker operations.</param>
/// <param name="options">Configuration parameters for Worker service.</param>
/// <param name="generator">Random appointment contracts generator.</param>
public class Worker(
    ILogger<Worker> logger,
    IOptions<WorkerOptions> options,
    AppointmentGenerator generator) : BackgroundService
{
    private readonly WorkerOptions _options = options.Value;

    /// <summary>
    /// Main execution loop of Worker: generates and sends appointment batches to server.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token for graceful shutdown.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            "Worker started. Server: {Server}, Batch size: {BatchSize}, Interval: {Interval}s",
            _options.ServerAddress, _options.BatchSize, _options.BatchIntervalSeconds);

        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SendAppointmentBatchAsync(stoppingToken);

                logger.LogInformation(
                    "Waiting {Interval} seconds before next batch...",
                    _options.BatchIntervalSeconds);
                await Task.Delay(TimeSpan.FromSeconds(_options.BatchIntervalSeconds), stoppingToken);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Unavailable)
            {
                logger.LogWarning("Server unavailable. Retrying in 10 seconds... Error: {Message}", ex.Message);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Critical error while sending appointments. Retrying in 15 seconds...");
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }

        logger.LogInformation("Worker stopped.");
    }

    /// <summary>
    /// Generates a batch of appointments and sends them to server via bidirectional streaming.
    /// Receives continuous status feedback during transmission.
    /// </summary>
    private async Task SendAppointmentBatchAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating {Count} appointments...", _options.BatchSize);
        var appointments = generator.GenerateBulk(
            _options.BatchSize,
            _options.MaxPatientId,
            _options.MaxDoctorId).ToList();

        logger.LogInformation("Creating gRPC channel to server: {Server}", _options.ServerAddress);

        using var channel = GrpcChannel.ForAddress(_options.ServerAddress);
        var client = new PolyclinicGenerator.PolyclinicGeneratorClient(channel);

        logger.LogInformation("Starting bidirectional stream for {Count} appointments...", appointments.Count);


        using var call = client.StreamAppointments(cancellationToken: cancellationToken);


        var sendTask = Task.Run(async () =>
        {
            var sentCount = 0;
            foreach (var appointment in appointments)
            {
                await call.RequestStream.WriteAsync(appointment, cancellationToken);
                sentCount++;

                if (sentCount % 10 == 0 || sentCount == appointments.Count)
                {
                    logger.LogDebug("Sent {Sent}/{Total} appointments", sentCount, appointments.Count);
                }
                await Task.Delay(100, cancellationToken);
            }

            logger.LogInformation("Completed sending all {Count} appointments", appointments.Count);
            await call.RequestStream.CompleteAsync();
        }, cancellationToken);

        var receiveTask = Task.Run(async () =>
        {
            var successCount = 0;
            var failedCount = 0;

            try
            {
                await foreach (var callback in call.ResponseStream.ReadAllAsync(cancellationToken))
                {
                    if (callback.Success)
                    {
                        successCount++;
                        if (successCount % 10 == 0)
                        {
                            logger.LogDebug("Successfully saved {Count} appointments so far", successCount);
                        }
                    }
                    else
                    {
                        failedCount++;
                        logger.LogWarning("Failed to save appointment: {Error}", callback.Error);
                    }
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                logger.LogInformation("Stream reading cancelled");
            }

            logger.LogInformation(
                "Final results: {Success} saved, {Failed} failed",
                successCount, failedCount);
        }, cancellationToken);

        await Task.WhenAll(sendTask, receiveTask);

        logger.LogInformation("Batch processing completed");
    }
}