using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

public class AppointmentService(IRepository<Appointment, int> appointmentRepository, IRepository<Patient, int> patientRepository, IRepository<Doctor, int> doctorRepository, IMapper mapper) : IAppointmentService
{
    /// <inheritdoc/>
    public AppointmentDto Create(AppointmentCreateUpdateDto dto)
    {
        var patient = patientRepository.Read(dto.PatientId);
        var doctor = doctorRepository.Read(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        var lastAppointment = appointmentRepository.ReadAll().OrderByDescending(a => a.Id).FirstOrDefault();
        var newAppointment = new Appointment
        {
            Id = (lastAppointment?.Id ?? 0) + 1,
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = dto.AppointmentDateTime,
            RoomNumber = dto.RoomNumber,
            IsFollowUp = dto.IsFollowUp
        };

        appointmentRepository.Create(newAppointment);
        return mapper.Map<AppointmentDto>(newAppointment);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId)
    {
        appointmentRepository.Delete(dtoId);
        return true;
    }

    /// <inheritdoc/>
    public AppointmentDto? Get(int dtoId) =>
        mapper.Map<AppointmentDto?>(appointmentRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<AppointmentDto> GetAll() =>
        mapper.Map<List<AppointmentDto>>(appointmentRepository.ReadAll());

    /// <inheritdoc/>
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public PatientDto? GetAppointmentPatient(int appointmentId)
    {
        var appointment = appointmentRepository.Read(appointmentId);
        return appointment == null ? null : mapper.Map<PatientDto>(appointment.Patient);
    }

    /// <inheritdoc/>
    public DoctorDto? GetAppointmentDoctor(int appointmentId)
    {
        var appointment = appointmentRepository.Read(appointmentId);
        return appointment == null ? null : mapper.Map<DoctorDto>(appointment.Doctor);
    }
}