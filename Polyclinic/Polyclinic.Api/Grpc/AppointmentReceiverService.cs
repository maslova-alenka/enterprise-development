using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Grpc.Protos;
using Grpc.Core;

namespace Polyclinic.Api.Grpc;

/// <summary>
/// gRPC service for receiving appointment contracts from generator.
/// Validates appointments and saves them to the database.
/// </summary>
public class AppointmentReceiverService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Patient, int> patientRepository,
    IRepository<Doctor, int> doctorRepository,
    ILogger<AppointmentReceiverService> logger)
    : PolyclinicGenerator.PolyclinicGeneratorBase
{
    /// <summary>
    /// Processes appointment contracts stream from client (bidirectional streaming).
    /// Sends status feedback for each appointment during processing.
    /// </summary>
    public override async Task StreamAppointments(
        IAsyncStreamReader<AppointmentResponse> requestStream,
        IServerStreamWriter<GenerationCallback> responseStream,
        ServerCallContext context)
    {
        var received = 0;
        var saved = 0;
        var failed = 0;

        logger.LogInformation("Starting to receive appointment contracts stream...");

        try
        {
            await foreach (var appointment in requestStream.ReadAllAsync(context.CancellationToken))
            {
                received++;

                logger.LogDebug("Received appointment #{Number}: PatientId={PId}, DoctorId={DId}, Date={Date}",
                    received, appointment.PatientId, appointment.DoctorId, appointment.AppointmentDate);

                var result = await ProcessAppointmentAsync(appointment);

                await responseStream.WriteAsync(new GenerationCallback
                {
                    Success = result.Success,
                    Error = result.Success ? string.Empty : result.Error
                });

                if (result.Success)
                {
                    saved++;
                    logger.LogDebug("Successfully saved appointment #{Number}", received);
                }
                else
                {
                    failed++;
                    logger.LogWarning("Failed to save appointment #{Number}: {Error}", received, result.Error);
                }
            }

            logger.LogInformation("Stream completed. Received: {Received}, Saved: {Saved}, Failed: {Failed}",
                received, saved, failed);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Critical error during stream processing");
            throw;
        }
    }

    /// <summary>
    /// Validates and processes a single appointment contract.
    /// </summary>
    private async Task<AppointmentProcessResult> ProcessAppointmentAsync(AppointmentResponse appointment)
    {
        try
        {

            if (appointment.PatientId <= 0 || appointment.DoctorId <= 0)
            {
                return AppointmentProcessResult.Fail("PatientId and DoctorId must be positive");
            }

            if (appointment.RoomNumber <= 0)
            {
                return AppointmentProcessResult.Fail("RoomNumber must be positive");
            }

            var patient = await patientRepository.ReadAsync(appointment.PatientId);
            if (patient == null)
            {
                return AppointmentProcessResult.Fail($"Patient with ID {appointment.PatientId} not found");
            }

    
            var doctor = await doctorRepository.ReadAsync(appointment.DoctorId);
            if (doctor == null)
            {
                return AppointmentProcessResult.Fail($"Doctor with ID {appointment.DoctorId} not found");
            }

            if (!DateTime.TryParse(appointment.AppointmentDate, out var appointmentDateTime))
            {
                return AppointmentProcessResult.Fail($"Invalid AppointmentDate: {appointment.AppointmentDate}. Expected ISO 8601 format");
            }


            if (appointmentDateTime <= DateTime.Now)
            {
                return AppointmentProcessResult.Fail($"Appointment date must be in future: {appointment.AppointmentDate}");
            }
            var newAppointment = new Appointment
            {
                Patient = patient,
                Doctor = doctor,
                AppointmentDateTime = appointmentDateTime,
                RoomNumber = appointment.RoomNumber,
                IsFollowUp = appointment.IsFollowUp
            };

            await appointmentRepository.CreateAsync(newAppointment);

            return AppointmentProcessResult.Ok;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing appointment");
            return AppointmentProcessResult.Fail($"Internal error: {ex.Message}");
        }
    }

    /// <summary>
    /// Result of appointment processing operation.
    /// </summary>
    private class AppointmentProcessResult
    {
        public bool Success { get; init; }
        public string Error { get; init; } = string.Empty;

        public static AppointmentProcessResult Ok => new() { Success = true };

        public static AppointmentProcessResult Fail(string error) => new()
        {
            Success = false,
            Error = error
        };
    }
}