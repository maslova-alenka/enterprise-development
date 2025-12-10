using AutoMapper;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Interfaces;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing patients
/// </summary>
/// <param name="patientRepository">Patient repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="mapper">Mapping profile</param>
public class PatientService(
    IRepository<Patient, int> patientRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) : IPatientService
{
    /// <summary>
    /// Creates a new patient asynchronously
    /// </summary>
    /// <param name="dto">Data for creating the patient</param>
    /// <returns>Created patient</returns>
    public async Task<PatientDto> CreateAsync(PatientCreateUpdateDto dto)
    {
        var newPatient = mapper.Map<Patient>(dto);
        var allPatients = await patientRepository.ReadAllAsync();
        var lastPatientId = allPatients.Max(p => p.Id);
        newPatient.Id = lastPatientId + 1;

        await patientRepository.CreateAsync(newPatient);
        return mapper.Map<PatientDto>(newPatient);
    }

    /// <summary>
    /// Deletes a patient by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>True if deletion was successful</returns>
    public async Task<bool> DeleteAsync(int dtoId)
    {
        await patientRepository.DeleteAsync(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a patient by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>Patient if found</returns>
    public async Task<PatientDto?> GetAsync(int dtoId)
    {
        var patient = await patientRepository.ReadAsync(dtoId);
        return mapper.Map<PatientDto?>(patient);
    }

    /// <summary>
    /// Retrieves all patients asynchronously
    /// </summary>
    /// <returns>List of all patients</returns>
    public async Task<List<PatientDto>> GetAllAsync()
    {
        var patients = await patientRepository.ReadAllAsync();
        return mapper.Map<List<PatientDto>>(patients);
    }

    /// <summary>
    /// Updates an existing patient asynchronously
    /// </summary>
    /// <param name="dto">Data for updating the patient</param>
    /// <param name="dtoId">Patient identifier</param>
    /// <returns>Updated patient</returns>
    public async Task<PatientDto> UpdateAsync(PatientCreateUpdateDto dto, int dtoId)
    {
        var existingPatient = await patientRepository.ReadAsync(dtoId)
            ?? throw new ArgumentException($"Patient with ID {dtoId} not found");

        var updatePatient = mapper.Map<Patient>(dto);
        updatePatient.Id = dtoId;
        await patientRepository.UpdateAsync(updatePatient);
        return mapper.Map<PatientDto>(updatePatient);
    }

    /// <summary>
    /// Retrieves all appointments for a specific patient asynchronously
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <returns>List of patient's appointments</returns>
    public async Task<List<AppointmentDto>> GetPatientAppointmentsAsync(int patientId)
    {
        var appointments = await appointmentRepository.ReadAllAsync();
        var patientAppointments = appointments
            .Where(a => a.Patient.Id == patientId)
            .ToList();

        return mapper.Map<List<AppointmentDto>>(patientAppointments);
    }
}