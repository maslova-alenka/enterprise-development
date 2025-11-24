using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts.Doctors;

/// <summary>
/// Service interface for managing doctors
/// </summary>
public interface IDoctorService : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <summary>
    /// Retrieves all appointments for a specific doctor
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's appointments</returns>
    public List<AppointmentDto> GetDoctorAppointments(int doctorId);

    /// <summary>
    /// Retrieves all patients for a specific doctor
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's patients</returns>
    public List<PatientDto> GetDoctorPatients(int doctorId);
}