using Polyclinic.Contracts.Appointments;

namespace Polyclinic.Contracts.Patients;

/// <summary>
/// Service interface for managing patients
/// </summary>
public interface IPatientService : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Retrieves all appointments for a specific patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <returns>List of patient's appointments</returns>
    public List<AppointmentDto> GetPatientAppointments(int patientId);
}