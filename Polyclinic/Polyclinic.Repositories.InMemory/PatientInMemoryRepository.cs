using Polyclinic.Domain.Data;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Repositories.InMemory;

/// <summary>
/// In-memory repository implementation for patients with async operations
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
    /// Creates a new patient asynchronously
    /// </summary>
    /// <param name="entity">Patient to create</param>
    public Task CreateAsync(Patient entity)
    {
        _patients.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes a patient by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Patient identifier</param>
    public Task DeleteAsync(int entityId)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == entityId);
        if (patient != null)
            _patients.Remove(patient);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Retrieves a patient by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Patient identifier</param>
    /// <returns>Patient if found</returns>
    public Task<Patient?> ReadAsync(int entityId)
    {
        return Task.FromResult(_patients.FirstOrDefault(p => p.Id == entityId));
    }

    /// <summary>
    /// Retrieves all patients asynchronously
    /// </summary>
    /// <returns>List of all patients</returns>
    public Task<List<Patient>> ReadAllAsync()
    {
        return Task.FromResult<List<Patient>>([.. _patients]);
    }

    /// <summary>
    /// Updates an existing patient asynchronously
    /// </summary>
    /// <param name="entity">Patient with updated data</param>
    public async Task UpdateAsync(Patient entity)
    {
        await DeleteAsync(entity.Id);
        await CreateAsync(entity);
    }
}