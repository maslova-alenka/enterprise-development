using AutoMapper;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Appointments;

namespace Polyclinic.Application.Service;


public class AnalyticsService(
    IDoctorRepository doctorRepository,
    IPatientRepository patientRepository,
    IAppointmentRepository appointmentRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IList<DoctorDto>> GetDoctorsWithExperienceAtLeast(int minYears) =>
        mapper.Map<List<DoctorDto>>((await doctorRepository.GetAllAsync())
            .Where(d => d.ExperienceYears >= minYears)
            .OrderBy(d => d.Id)
            .ToList());

    /// <inheritdoc/>
    public async Task<IList<PatientDto>> GetPatientsByDoctorOrderedByName(int doctorId) =>
        mapper.Map<List<PatientDto>>((await appointmentRepository.GetByDoctorAsync(doctorId))
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList());

    /// <inheritdoc/>
    public async Task<int> GetFollowUpAppointmentsCountLastMonth(DateTime referenceDate) =>
        await appointmentRepository.GetFollowUpCountAsync(referenceDate);

    /// <inheritdoc/>
    public async Task<IList<PatientDto>> GetPatientsOver30WithMultipleDoctors()
    {
        var cutoffDate = DateTime.Today.AddYears(-30);
        var appointments = await appointmentRepository.GetAllAsync();

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
    public async Task<IList<AppointmentDto>> GetAppointmentsByRoomForCurrentMonth(int roomNumber, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var appointments = await appointmentRepository.GetByRoomAsync(roomNumber);
        var filteredAppointments = appointments
            .Where(a => a.AppointmentDateTime >= startDate && a.AppointmentDateTime <= endDate)
            .ToList();

        return mapper.Map<List<AppointmentDto>>(filteredAppointments);
    }
}