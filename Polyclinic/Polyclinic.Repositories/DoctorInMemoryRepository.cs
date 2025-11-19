using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// In-memory repository implementation for doctors
/// </summary>
public class DoctorInMemoryRepository : IRepository<Doctor, int>
{
    private readonly List<Doctor> _doctors;

    /// <summary>
    /// Initializes a new instance of the DoctorInMemoryRepository class
    /// </summary>
    public DoctorInMemoryRepository()
    {
        _doctors = DataSeed.Doctors;
    }

    /// <summary>
    /// Creates a new doctor
    /// </summary>
    /// <param name="entity">Doctor to create</param>
    public void Create(Doctor entity)
    {
        _doctors.Add(entity);
    }

    /// <summary>
    /// Deletes a doctor by identifier
    /// </summary>
    /// <param name="entityId">Doctor identifier</param>
    public void Delete(int entityId)
    {
        var doctor = Read(entityId);
        if (doctor != null)
            _doctors.Remove(doctor);
    }

    /// <summary>
    /// Retrieves a doctor by identifier
    /// </summary>
    /// <param name="entityId">Doctor identifier</param>
    /// <returns>Doctor if found</returns>
    public Doctor Read(int entityId)
    {
        return _doctors.First(d => d.Id == entityId);
    }

    /// <summary>
    /// Retrieves all doctors
    /// </summary>
    /// <returns>List of all doctors</returns>
    public List<Doctor> ReadAll()
    {
        return [.. _doctors];
    }

    /// <summary>
    /// Updates an existing doctor
    /// </summary>
    /// <param name="entity">Doctor with updated data</param>
    public void Update(Doctor entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}