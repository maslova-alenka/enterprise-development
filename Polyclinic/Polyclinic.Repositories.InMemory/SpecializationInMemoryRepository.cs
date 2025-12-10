using Polyclinic.Domain.Data;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Repositories.InMemory;

/// <summary>
/// In-memory repository implementation for specializations
/// </summary>
public class SpecializationInMemoryRepository : IRepository<Specialization, int>
{
    private readonly List<Specialization> _specializations;

    /// <summary>
    /// Initializes a new instance of the SpecializationInMemoryRepository class
    /// </summary>
    public SpecializationInMemoryRepository()
    {
        _specializations = DataSeed.Specializations;
    }

    /// <summary>
    /// Creates a new specialization asynchronously
    /// </summary>
    /// <param name="entity">Specialization to create</param>
    public Task CreateAsync(Specialization entity)
    {
        _specializations.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes a specialization by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Specialization identifier</param>
    public Task DeleteAsync(int entityId)
    {
        var specialization = _specializations.FirstOrDefault(s => s.Id == entityId);
        if (specialization != null)
            _specializations.Remove(specialization);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Retrieves a specialization by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Specialization identifier</param>
    /// <returns>Specialization if found</returns>
    public Task<Specialization?> ReadAsync(int entityId)
    {
        return Task.FromResult(_specializations.FirstOrDefault(s => s.Id == entityId));
    }

    /// <summary>
    /// Retrieves all specializations asynchronously
    /// </summary>
    /// <returns>List of all specializations</returns>
    public Task<List<Specialization>> ReadAllAsync()
    {
        return Task.FromResult<List<Specialization>>([.. _specializations]);
    }

    /// <summary>
    /// Updates an existing specialization asynchronously
    /// </summary>
    /// <param name="entity">Specialization with updated data</param>
    public async Task UpdateAsync(Specialization entity)
    {
        await DeleteAsync(entity.Id);
        await CreateAsync(entity);
    }
}