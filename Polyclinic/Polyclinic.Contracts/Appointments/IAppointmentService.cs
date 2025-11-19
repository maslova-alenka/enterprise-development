using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;

namespace Polyclinic.Contracts.Appointments;

/// <summary>
/// Service interface for managing appointments
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Retrieves patient information for a specific appointment
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Patient details or null if not found</returns>
    PatientDto? GetAppointmentPatient(int appointmentId);

    /// <summary>
    /// Retrieves doctor information for a specific appointment
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Doctor details or null if not found</returns>
    DoctorDto? GetAppointmentDoctor(int appointmentId);
}