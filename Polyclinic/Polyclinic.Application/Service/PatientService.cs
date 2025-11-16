using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

public class PatientService(IRepository<Patient, int> patientRepository, IRepository<Appointment, int> appointmentRepository, IMapper mapper) : IPatientService
{
    /// <inheritdoc/>
    public PatientDto Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var lastPatient = patientRepository.ReadAll().OrderByDescending(p => p.Id).FirstOrDefault();
        newPatient.Id = (lastPatient?.Id ?? 0) + 1;

        patientRepository.Create(newPatient);
        return mapper.Map<PatientDto>(newPatient);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId)
    {
        patientRepository.Delete(dtoId);
        return true;
    }

    /// <inheritdoc/>
    public PatientDto? Get(int dtoId) =>
        mapper.Map<PatientDto?>(patientRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<PatientDto> GetAll() =>
        mapper.Map<List<PatientDto>>(patientRepository.ReadAll());

    /// <inheritdoc/>
    public PatientDto Update(PatientCreateUpdateDto dto, int dtoId)
    {
        var updatePatient = mapper.Map<Patient>(dto);
        updatePatient.Id = dtoId;
        patientRepository.Update(updatePatient);
        return mapper.Map<PatientDto>(updatePatient);
    }

    /// <inheritdoc/>
    public List<AppointmentDto> GetPatientAppointments(int patientId) =>
        mapper.Map<List<AppointmentDto>>(appointmentRepository.ReadAll().Where(a => a.Patient.Id == patientId).ToList());
}