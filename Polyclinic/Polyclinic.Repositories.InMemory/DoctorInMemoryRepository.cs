using Polyclinic.Domain.Data;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Repositories.InMemory;

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
    /// Creates a new doctor asynchronously
    /// </summary>
    /// <param name="entity">Doctor to create</param>
    public Task CreateAsync(Doctor entity)
    {
        _doctors.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes a doctor by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Doctor identifier</param>
    public Task DeleteAsync(int entityId)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == entityId);
        if (doctor != null)
            _doctors.Remove(doctor);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Retrieves a doctor by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Doctor identifier</param>
    /// <returns>Doctor if found</returns>
    public Task<Doctor?> ReadAsync(int entityId)
    {
        return Task.FromResult(_doctors.FirstOrDefault(d => d.Id == entityId));
    }

    /// <summary>
    /// Retrieves all doctors asynchronously
    /// </summary>
    /// <returns>List of all doctors</returns>
    public Task<List<Doctor>> ReadAllAsync()
    {
        return Task.FromResult<List<Doctor>>([.. _doctors]);
    }

    /// <summary>
    /// Updates an existing doctor asynchronously
    /// </summary>
    /// <param name="entity">Doctor with updated data</param>
    public async Task UpdateAsync(Doctor entity)
    {
        await DeleteAsync(entity.Id);
        await CreateAsync(entity);
    }
}