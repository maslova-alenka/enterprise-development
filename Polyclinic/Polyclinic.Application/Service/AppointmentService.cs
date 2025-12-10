using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing appointments
/// </summary>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="patientRepository">Patient repository</param>
/// <param name="doctorRepository">Doctor repository</param>
/// <param name="mapper">Mapping profile</param>
public class AppointmentService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Patient, int> patientRepository,
    IRepository<Doctor, int> doctorRepository,
    IMapper mapper) : IAppointmentService
{
    /// <summary>
    /// Creates a new appointment asynchronously
    /// </summary>
    /// <param name="dto">Data for creating the appointment</param>
    /// <returns>Created appointment</returns>
    public async Task<AppointmentDto> CreateAsync(AppointmentCreateUpdateDto dto)
    {
        var patient = await patientRepository.ReadAsync(dto.PatientId);
        var doctor = await doctorRepository.ReadAsync(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        var allAppointments = await appointmentRepository.ReadAllAsync();
        var lastAppointmentId = allAppointments.Count > 0 ? allAppointments.Max(a => a.Id) : 0;
        var newId = lastAppointmentId + 1;

        var newAppointment = new Appointment
        {
            Id = newId,
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = dto.AppointmentDateTime,
            RoomNumber = dto.RoomNumber,
            IsFollowUp = dto.IsFollowUp
        };

        await appointmentRepository.CreateAsync(newAppointment);
        return mapper.Map<AppointmentDto>(newAppointment);
    }

    /// <summary>
    /// Deletes an appointment by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>True if deletion was successful</returns>
    public async Task<bool> DeleteAsync(int dtoId)
    {
        await appointmentRepository.DeleteAsync(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves an appointment by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>Appointment if found</returns>
    public async Task<AppointmentDto?> GetAsync(int dtoId)
    {
        var appointment = await appointmentRepository.ReadAsync(dtoId);
        return appointment == null ? null : mapper.Map<AppointmentDto>(appointment);
    }

    /// <summary>
    /// Retrieves all appointments asynchronously
    /// </summary>
    /// <returns>List of all appointments</returns>
    public async Task<List<AppointmentDto>> GetAllAsync()
    {
        var appointments = await appointmentRepository.ReadAllAsync();
        return mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <summary>
    /// Updates an existing appointment asynchronously
    /// </summary>
    /// <param name="dto">Data for updating the appointment</param>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>Updated appointment</returns>
    public async Task<AppointmentDto> UpdateAsync(AppointmentCreateUpdateDto dto, int dtoId)
    {
        var existing = await appointmentRepository.ReadAsync(dtoId)
            ?? throw new ArgumentException($"Appointment with ID {dtoId} not found");

        var patient = await patientRepository.ReadAsync(dto.PatientId);
        var doctor = await doctorRepository.ReadAsync(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        existing.Patient = patient;
        existing.Doctor = doctor;
        existing.AppointmentDateTime = dto.AppointmentDateTime;
        existing.RoomNumber = dto.RoomNumber;
        existing.IsFollowUp = dto.IsFollowUp;

        await appointmentRepository.UpdateAsync(existing);
        return mapper.Map<AppointmentDto>(existing);
    }

    /// <summary>
    /// Retrieves patient information for a specific appointment asynchronously
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Patient details or null if not found</returns>
    public async Task<PatientDto?> GetAppointmentPatientAsync(int appointmentId)
    {
        var appointment = await appointmentRepository.ReadAsync(appointmentId);
        return appointment == null ? null : mapper.Map<PatientDto>(appointment.Patient);
    }

    /// <summary>
    /// Retrieves doctor information for a specific appointment asynchronously
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Doctor details or null if not found</returns>
    public async Task<DoctorDto?> GetAppointmentDoctorAsync(int appointmentId)
    {
        var appointment = await appointmentRepository.ReadAsync(appointmentId);
        return appointment == null ? null : mapper.Map<DoctorDto>(appointment.Doctor);
    }
}