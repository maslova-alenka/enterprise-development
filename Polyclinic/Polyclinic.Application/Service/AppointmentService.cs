using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Models;


namespace Polyclinic.Application.Services;

public class AppointmentService(
    IAppointmentRepository appointmentRepository,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IMapper mapper) : IAppointmentService
{
    /// <inheritdoc/>
    public async Task<AppointmentDto> Create(AppointmentCreateUpdateDto dto)
    {
        var patient = await patientRepository.GetByIdAsync(dto.PatientId);
        var doctor = await doctorRepository.GetByIdAsync(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        var newAppointment = new Appointment
        {
            Patient = patient,
            Doctor = doctor,
            AppointmentDateTime = dto.AppointmentDateTime,
            RoomNumber = dto.RoomNumber,
            IsFollowUp = dto.IsFollowUp
        };

        var result = await appointmentRepository.CreateAsync(newAppointment);
        return mapper.Map<AppointmentDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await appointmentRepository.DeleteAsync(dtoId);

    /// <inheritdoc/>
    public async Task<AppointmentDto?> Get(int dtoId) =>
        mapper.Map<AppointmentDto>(await appointmentRepository.GetByIdAsync(dtoId));

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetAll() =>
        mapper.Map<List<AppointmentDto>>(await appointmentRepository.GetAllAsync());

    /// <inheritdoc/>
    public async Task<AppointmentDto> Update(AppointmentCreateUpdateDto dto, int dtoId)
    {
        var existing = await appointmentRepository.GetByIdAsync(dtoId);
        if (existing == null) return null;

        var patient = await patientRepository.GetByIdAsync(dto.PatientId);
        var doctor = await doctorRepository.GetByIdAsync(dto.DoctorId);

        if (patient == null || doctor == null)
            throw new ArgumentException("Patient or Doctor not found");

        existing.Patient = patient;
        existing.Doctor = doctor;
        existing.AppointmentDateTime = dto.AppointmentDateTime;
        existing.RoomNumber = dto.RoomNumber;
        existing.IsFollowUp = dto.IsFollowUp;

        var result = await appointmentRepository.UpdateAsync(existing);
        return mapper.Map<AppointmentDto>(result);
    }

    /// <inheritdoc/>
    public async Task<PatientDto> GetAppointmentPatient(int appointmentId)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId);
        return appointment == null ? null : mapper.Map<PatientDto>(appointment.Patient);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto> GetAppointmentDoctor(int appointmentId)
    {
        var appointment = await appointmentRepository.GetByIdAsync(appointmentId);
        return appointment == null ? null : mapper.Map<DoctorDto>(appointment.Doctor);
    }
}