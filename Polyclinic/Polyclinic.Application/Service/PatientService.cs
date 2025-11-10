using AutoMapper;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Models;

namespace Polyclinic.Application.Services;

public class PatientService(
    IPatientRepository patientRepository,
    IAppointmentRepository appointmentRepository,
    IMapper mapper) : IPatientService
{
    /// <inheritdoc/>
    public async Task<PatientDto> Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var result = await patientRepository.CreateAsync(newPatient);
        return mapper.Map<PatientDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) =>
        await patientRepository.DeleteAsync(dtoId);

    /// <inheritdoc/>
    public async Task<PatientDto?> Get(int dtoId) =>
        mapper.Map<PatientDto>(await patientRepository.GetByIdAsync(dtoId));

    /// <inheritdoc/>
    public async Task<IList<PatientDto>> GetAll() =>
        mapper.Map<List<PatientDto>>(await patientRepository.GetAllAsync());

    /// <inheritdoc/>
    public async Task<PatientDto> Update(PatientCreateUpdateDto dto, int dtoId)
    {
        var updatePatient = mapper.Map<Patient>(dto);
        updatePatient.Id = dtoId;
        var result = await patientRepository.UpdateAsync(updatePatient);
        return mapper.Map<PatientDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetPatientAppointments(int patientId) =>
        mapper.Map<IList<AppointmentDto>>(await appointmentRepository.GetByPatientAsync(patientId));
}