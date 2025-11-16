using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// Имплементация репозитория для врачей
/// </summary>
public class DoctorInMemoryRepository : IRepository<Doctor, int>
{
    private readonly List<Doctor> _doctors;

    /// <inheritdoc/>
    public DoctorInMemoryRepository()
    {
        _doctors = DataSeed.Doctors;
    }

    /// <inheritdoc/>
    public void Create(Doctor entity)
    {
        _doctors.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var doctor = Read(entityId);
        if (doctor != null)
            _doctors.Remove(doctor);
    }

    /// <inheritdoc/>
    public Doctor Read(int entityId)
    {
        return _doctors.First(d => d.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Doctor> ReadAll()
    {
        return [.. _doctors];
    }

    /// <inheritdoc/>
    public void Update(Doctor entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}