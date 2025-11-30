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
    /// Creates a new appointment
    /// </summary>
    /// <param name="dto">Data for creating the appointment</param>
    /// <returns>Created appointment</returns>
    public AppointmentDto Create(AppointmentCreateUpdateDto dto)
    {
        var patient = patientRepository.Read(dto.PatientId);
        var doctor = doctorRepository.Read(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        var lastAppointmentId = appointmentRepository.ReadAll().Max(a => a.Id);
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

        appointmentRepository.Create(newAppointment);
        return mapper.Map<AppointmentDto>(newAppointment);
    }

    /// <summary>
    /// Deletes an appointment by identifier
    /// </summary>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>True if deletion was successful</returns>
    public bool Delete(int dtoId)
    {
        appointmentRepository.Delete(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves an appointment by identifier
    /// </summary>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>Appointment if found</returns>
    public AppointmentDto? Get(int dtoId)
    {
        var appointment = appointmentRepository.Read(dtoId);
        return appointment == null ? null : mapper.Map<AppointmentDto>(appointment);
    }

    /// <summary>
    /// Retrieves all appointments
    /// </summary>
    /// <returns>List of all appointments</returns>
    public List<AppointmentDto> GetAll()
    {
        var appointments = appointmentRepository.ReadAll();
        return mapper.Map<List<AppointmentDto>>(appointments);
    }

    /// <summary>
    /// Updates an existing appointment
    /// </summary>
    /// <param name="dto">Data for updating the appointment</param>
    /// <param name="dtoId">Appointment identifier</param>
    /// <returns>Updated appointment</returns>
    public AppointmentDto Update(AppointmentCreateUpdateDto dto, int dtoId)
    {
        var existing = appointmentRepository.Read(dtoId) ?? throw new ArgumentException($"Appointment with ID {dtoId} not found");
        var patient = patientRepository.Read(dto.PatientId);
        var doctor = doctorRepository.Read(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        existing.Patient = patient;
        existing.Doctor = doctor;
        existing.AppointmentDateTime = dto.AppointmentDateTime;
        existing.RoomNumber = dto.RoomNumber;
        existing.IsFollowUp = dto.IsFollowUp;

        appointmentRepository.Update(existing);
        return mapper.Map<AppointmentDto>(existing);
    }

    /// <summary>
    /// Retrieves patient information for a specific appointment
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Patient details or null if not found</returns>
    public PatientDto? GetAppointmentPatient(int appointmentId)
    {
        var appointment = appointmentRepository.Read(appointmentId);
        return appointment == null ? null : mapper.Map<PatientDto>(appointment.Patient);
    }

    /// <summary>
    /// Retrieves doctor information for a specific appointment
    /// </summary>
    /// <param name="appointmentId">Appointment identifier</param>
    /// <returns>Doctor details or null if not found</returns>
    public DoctorDto? GetAppointmentDoctor(int appointmentId)
    {
        var appointment = appointmentRepository.Read(appointmentId);
        return appointment == null ? null : mapper.Map<DoctorDto>(appointment.Doctor);
    }
}