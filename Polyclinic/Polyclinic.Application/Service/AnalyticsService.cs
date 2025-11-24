using AutoMapper;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Interfaces;

namespace Polyclinic.Application.Service;

/// <summary>
/// Analytics service
/// </summary>
/// <param name="doctorRepository">Doctor repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="mapper">Mapping profile</param>
public class AnalyticsService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Retrieves doctors with experience at least specified number of years
    /// </summary>
    /// <param name="minYears">Minimum years of experience required</param>
    /// <returns>List of doctors meeting the experience criteria</returns>
    public List<DoctorDto> GetDoctorsWithExperienceAtLeast(int minYears) =>
        mapper.Map<List<DoctorDto>>(doctorRepository.ReadAll()
            .Where(d => d.ExperienceYears >= minYears)
            .OrderBy(d => d.Id)
            .ToList());

    /// <summary>
    /// Retrieves patients for a specific doctor ordered by name
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of patients for the specified doctor</returns>
    public List<PatientDto> GetPatientsByDoctorOrderedByName(int doctorId) =>
        mapper.Map<List<PatientDto>>(appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList());

    /// <summary>
    /// Gets the count of follow-up appointments for the last month relative to reference date
    /// </summary>
    /// <param name="referenceDate">Reference date for calculating last month</param>
    /// <returns>Count of follow-up appointments</returns>
    public int GetFollowUpAppointmentsCountLastMonth(DateTime referenceDate)
    {
        var lastMonth = referenceDate.AddMonths(-1);
        return appointmentRepository.ReadAll()
            .Count(a => a.IsFollowUp &&
                       a.AppointmentDateTime.Month == lastMonth.Month &&
                       a.AppointmentDateTime.Year == lastMonth.Year);
    }

    /// <summary>
    /// Retrieves patients over 30 years old who have appointments with multiple doctors
    /// </summary>
    /// <returns>List of patients meeting the criteria</returns>
    public List<PatientDto> GetPatientsOver30WithMultipleDoctors()
    {
        var cutoffDate = DateTime.Today.AddYears(-30);
        var appointments = appointmentRepository.ReadAll();

        var patients = appointments
            .GroupBy(a => a.Patient.Id)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.First().Patient)
            .Where(p => p.Birthday <= cutoffDate)
            .OrderBy(p => p.Birthday)
            .ToList();

        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <summary>
    /// Retrieves appointments for a specific room in the current month
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="year">Year for filtering appointments</param>
    /// <param name="month">Month for filtering appointments</param>
    /// <returns>List of appointments for the specified room and period</returns>
    public List<AppointmentDto> GetAppointmentsByRoomForCurrentMonth(int roomNumber, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var appointments = appointmentRepository.ReadAll()
            .Where(a => a.RoomNumber == roomNumber);

        var filteredAppointments = appointments
            .Where(a => a.AppointmentDateTime >= startDate && a.AppointmentDateTime <= endDate)
            .ToList();

        return mapper.Map<List<AppointmentDto>>(filteredAppointments);
    }
}