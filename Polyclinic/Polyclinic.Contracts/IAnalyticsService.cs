using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts;

/// <summary>
/// Service interface for analytics operations
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Retrieves doctors with experience at least specified number of years
    /// </summary>
    /// <param name="minYears">Minimum years of experience required</param>
    /// <returns>List of doctors meeting the experience criteria</returns>
    Task<List<DoctorDto>> GetDoctorsWithExperienceAtLeastAsync(int minYears);

    /// <summary>
    /// Retrieves patients for a specific doctor ordered by name
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of patients for the specified doctor</returns>
    Task<List<PatientDto>> GetPatientsByDoctorOrderedByNameAsync(int doctorId);

    /// <summary>
    /// Gets the count of follow-up appointments for the last month relative to reference date
    /// </summary>
    /// <param name="referenceDate">Reference date for calculating last month</param>
    /// <returns>Count of follow-up appointments</returns>
    Task<int> GetFollowUpAppointmentsCountLastMonthAsync(DateTime referenceDate);

    /// <summary>
    /// Retrieves patients over 30 years old who have appointments with multiple doctors
    /// </summary>
    /// <returns>List of patients meeting the criteria</returns>
    Task<List<PatientDto>> GetPatientsOver30WithMultipleDoctorsAsync();

    /// <summary>
    /// Retrieves appointments for a specific room in the current month
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="year">Year for filtering appointments</param>
    /// <param name="month">Month for filtering appointments</param>
    /// <returns>List of appointments for the specified room and period</returns>
    Task<List<AppointmentDto>> GetAppointmentsByRoomForCurrentMonthAsync(int roomNumber, int year, int month);
}