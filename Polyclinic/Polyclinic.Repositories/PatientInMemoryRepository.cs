using Polyclinic.Domain.Data;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Repositories;

/// <summary>
/// In-memory repository implementation for patients
/// </summary>
public class PatientInMemoryRepository : IRepository<Patient, int>
{
    private readonly List<Patient> _patients;

    /// <summary>
    /// Initializes a new instance of the PatientInMemoryRepository class
    /// </summary>
    public PatientInMemoryRepository()
    {
        _patients = DataSeed.Patients;
    }

    /// <summary>
    /// Creates a new patient
    /// </summary>
    /// <param name="entity">Patient to create</param>
    public void Create(Patient entity)
    {
        _patients.Add(entity);
    }

    /// <summary>
    /// Deletes a patient by identifier
    /// </summary>
    /// <param name="entityId">Patient identifier</param>
    public void Delete(int entityId)
    {
        var patient = Read(entityId);
        if (patient != null)
            _patients.Remove(patient);
    }

    /// <summary>
    /// Retrieves a patient by identifier
    /// </summary>
    /// <param name="entityId">Patient identifier</param>
    /// <returns>Patient if found</returns>
    public Patient Read(int entityId)
    {
        return _patients.First(p => p.Id == entityId);
    }

    /// <summary>
    /// Retrieves all patients
    /// </summary>
    /// <returns>List of all patients</returns>
    public List<Patient> ReadAll()
    {
        return [.. _patients];
    }

    /// <summary>
    /// Updates an existing patient
    /// </summary>
    /// <param name="entity">Patient with updated data</param>
    public void Update(Patient entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}