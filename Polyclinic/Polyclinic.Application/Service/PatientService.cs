using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing patients
/// </summary>
/// <param name="patientRepository">Patient repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="mapper">Mapping profile</param>
public class PatientService(IRepository<Patient, int> patientRepository, IRepository<Appointment, int> appointmentRepository, IMapper mapper) : IPatientService
{
    /// <summary>
    /// Creates a new patient
    /// </summary>
    /// <param name="dto">Data for creating the patient</param>
    /// <returns>Created patient</returns>
    public PatientDto Create(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var lastPatient = patientRepository.ReadAll().OrderByDescending(p => p.Id).FirstOrDefault();
        newPatient.Id = (lastPatient?.Id ?? 0) + 1;

        patientRepository.Create(newPatient);
        return mapper.Map<PatientDto>(newPatient);
    }

    /// <summary>
    /// Deletes a patient by identifier
    /// </summary>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>True if deletion was successful</returns>
    public bool Delete(int dtoId)
    {
        patientRepository.Delete(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a patient by identifier
    /// </summary>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>Patient if found</returns>
    public PatientDto? Get(int dtoId) =>
        mapper.Map<PatientDto?>(patientRepository.Read(dtoId));

    /// <summary>
    /// Retrieves all patients
    /// </summary>
    /// <returns>List of all patients</returns>
    public List<PatientDto> GetAll() =>
        mapper.Map<List<PatientDto>>(patientRepository.ReadAll());

    /// <summary>
    /// Updates an existing patient
    /// </summary>
    /// <param name="dto">Data for updating the patient</param>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>Updated patient</returns>
    public PatientDto Update(PatientCreateUpdateDto dto, int dtoId)
    {
        var updatePatient = mapper.Map<Patient>(dto);
        updatePatient.Id = dtoId;
        patientRepository.Update(updatePatient);
        return mapper.Map<PatientDto>(updatePatient);
    }

    /// <summary>
    /// Retrieves all appointments for a specific patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <returns>List of patient's appointments</returns>
    public List<AppointmentDto> GetPatientAppointments(int patientId) =>
        mapper.Map<List<AppointmentDto>>(appointmentRepository.ReadAll().Where(a => a.Patient.Id == patientId).ToList());
}