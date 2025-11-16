using AutoMapper;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

public class AnalyticsService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public List<DoctorDto> GetDoctorsWithExperienceAtLeast(int minYears) =>
        mapper.Map<List<DoctorDto>>(doctorRepository.ReadAll()
            .Where(d => d.ExperienceYears >= minYears)
            .OrderBy(d => d.Id)
            .ToList());

    /// <inheritdoc/>
    public List<PatientDto> GetPatientsByDoctorOrderedByName(int doctorId) =>
        mapper.Map<List<PatientDto>>(appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList());

    /// <inheritdoc/>
    public int GetFollowUpAppointmentsCountLastMonth(DateTime referenceDate)
    {
        var lastMonth = referenceDate.AddMonths(-1);
        return appointmentRepository.ReadAll()
            .Count(a => a.IsFollowUp &&
                       a.AppointmentDateTime.Month == lastMonth.Month &&
                       a.AppointmentDateTime.Year == lastMonth.Year);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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