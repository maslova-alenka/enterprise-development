using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// Имплементация репозитория для пациентов
/// </summary>
public class PatientInMemoryRepository : IRepository<Patient, int>
{
    private readonly List<Patient> _patients;

    /// <inheritdoc/>
    public PatientInMemoryRepository()
    {
        _patients = DataSeed.Patients;
    }

    /// <inheritdoc/>
    public void Create(Patient entity)
    {
        _patients.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var patient = Read(entityId);
        if (patient != null)
            _patients.Remove(patient);
    }

    /// <inheritdoc/>
    public Patient Read(int entityId)
    {
        return _patients.First(p => p.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Patient> ReadAll()
    {
        return [.. _patients];
    }

    /// <inheritdoc/>
    public void Update(Patient entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}