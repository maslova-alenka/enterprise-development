using AutoMapper;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Models;


namespace Polyclinic.Application.Services;

public class DoctorService(
    IDoctorRepository doctorRepository,
    IAppointmentRepository appointmentRepository,
    IPatientRepository patientRepository,
    IMapper mapper) : IDoctorService
{
    /// <inheritdoc/>
    public async Task<DoctorDto> Create(DoctorCreateUpdateDto dto)
    {
        var newDoctor = mapper.Map<Doctor>(dto);
        var result = await doctorRepository.CreateAsync(newDoctor);
        return mapper.Map<DoctorDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await doctorRepository.DeleteAsync(dtoId);

    /// <inheritdoc/>
    public async Task<DoctorDto?> Get(int dtoId) =>
        mapper.Map<DoctorDto>(await doctorRepository.GetByIdAsync(dtoId));

    /// <inheritdoc/>
    public async Task<IList<DoctorDto>> GetAll() =>
        mapper.Map<List<DoctorDto>>(await doctorRepository.GetAllAsync());

    /// <inheritdoc/>
    public async Task<DoctorDto> Update(DoctorCreateUpdateDto dto, int dtoId)
    {
        var updateDoctor = mapper.Map<Doctor>(dto);
        updateDoctor.Id = dtoId;
        var result = await doctorRepository.UpdateAsync(updateDoctor);
        return mapper.Map<DoctorDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetDoctorAppointments(int doctorId) =>
        mapper.Map<IList<AppointmentDto>>(await appointmentRepository.GetByDoctorAsync(doctorId));

    /// <inheritdoc/>
    public async Task<IList<PatientDto>> GetDoctorPatients(int doctorId)
    {
        var appointments = await appointmentRepository.GetByDoctorAsync(doctorId);
        var patients = appointments.Select(a => a.Patient).Distinct().ToList();
        return mapper.Map<IList<PatientDto>>(patients);
    }
}