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
    /// Creates a new specialization
    /// </summary>
    /// <param name="entity">Specialization to create</param>
    public void Create(Specialization entity)
    {
        _specializations.Add(entity);
    }

    /// <summary>
    /// Deletes a specialization by identifier
    /// </summary>
    /// <param name="entityId">Specialization identifier</param>
    public void Delete(int entityId)
    {
        var specialization = Read(entityId);
        if (specialization != null)
            _specializations.Remove(specialization);
    }

    /// <summary>
    /// Retrieves a specialization by identifier
    /// </summary>
    /// <param name="entityId">Specialization identifier</param>
    /// <returns>Specialization if found</returns>
    public Specialization? Read(int entityId)
    {
        return _specializations.FirstOrDefault(s => s.Id == entityId);
    }

    /// <summary>
    /// Retrieves all specializations
    /// </summary>
    /// <returns>List of all specializations</returns>
    public List<Specialization> ReadAll()
    {
        return [.. _specializations];
    }

    /// <summary>
    /// Updates an existing specialization
    /// </summary>
    /// <param name="entity">Specialization with updated data</param>
    public void Update(Specialization entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}