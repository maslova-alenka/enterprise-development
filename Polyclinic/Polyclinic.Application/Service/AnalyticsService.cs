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
    /// Retrieves doctors with experience at least specified number of years asynchronously
    /// </summary>
    /// <param name="minYears">Minimum years of experience required</param>
    /// <returns>List of doctors meeting the experience criteria</returns>
    public async Task<List<DoctorDto>> GetDoctorsWithExperienceAtLeastAsync(int minYears)
    {
        var doctors = await doctorRepository.ReadAllAsync();
        var filteredDoctors = doctors
            .Where(d => d.ExperienceYears >= minYears)
            .OrderBy(d => d.Id)
            .ToList();

        return mapper.Map<List<DoctorDto>>(filteredDoctors);
    }

    /// <summary>
    /// Retrieves patients for a specific doctor ordered by name asynchronously
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of patients for the specified doctor</returns>
    public async Task<List<PatientDto>> GetPatientsByDoctorOrderedByNameAsync(int doctorId)
    {
        var appointments = await appointmentRepository.ReadAllAsync();
        var patients = appointments
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();

        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <summary>
    /// Gets the count of follow-up appointments for the last month relative to reference date asynchronously
    /// </summary>
    /// <param name="referenceDate">Reference date for calculating last month</param>
    /// <returns>Count of follow-up appointments</returns>
    public async Task<int> GetFollowUpAppointmentsCountLastMonthAsync(DateTime referenceDate)
    {
        var lastMonth = referenceDate.AddMonths(-1);
        var appointments = await appointmentRepository.ReadAllAsync();

        return appointments
            .Count(a => a.IsFollowUp &&
                       a.AppointmentDateTime.Month == lastMonth.Month &&
                       a.AppointmentDateTime.Year == lastMonth.Year);
    }

    /// <summary>
    /// Retrieves patients over 30 years old who have appointments with multiple doctors asynchronously
    /// </summary>
    /// <returns>List of patients meeting the criteria</returns>
    public async Task<List<PatientDto>> GetPatientsOver30WithMultipleDoctorsAsync()
    {
        var cutoffDate = DateTime.Today.AddYears(-30);
        var appointments = await appointmentRepository.ReadAllAsync();

        var patientGroups = appointments
            .GroupBy(a => a.Patient.Id)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.First().Patient)
            .Where(p => p.Birthday <= cutoffDate)
            .OrderBy(p => p.Birthday)
            .ToList();

        return mapper.Map<List<PatientDto>>(patientGroups);
    }

    /// <summary>
    /// Retrieves appointments for a specific room in the current month asynchronously
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="year">Year for filtering appointments</param>
    /// <param name="month">Month for filtering appointments</param>
    /// <returns>List of appointments for the specified room and period</returns>
    public async Task<List<AppointmentDto>> GetAppointmentsByRoomForCurrentMonthAsync(
        int roomNumber, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var appointments = await appointmentRepository.ReadAllAsync();
        var filteredAppointments = appointments
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime >= startDate &&
                       a.AppointmentDateTime <= endDate)
            .ToList();

        return mapper.Map<List<AppointmentDto>>(filteredAppointments);
    }
}