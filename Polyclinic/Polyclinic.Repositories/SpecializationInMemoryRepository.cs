using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// Имплементация репозитория для специализаций
/// </summary>
public class SpecializationInMemoryRepository : IRepository<Specialization, int>
{
    private readonly List<Specialization> _specializations;

    /// <inheritdoc/>
    public SpecializationInMemoryRepository()
    {
        _specializations = DataSeed.Specializations;
    }

    /// <inheritdoc/>
    public void Create(Specialization entity)
    {
        _specializations.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var specialization = Read(entityId);
        if (specialization != null)
            _specializations.Remove(specialization);
    }

    /// <inheritdoc/>
    public Specialization Read(int entityId)
    {
        return _specializations.First(s => s.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Specialization> ReadAll()
    {
        return [.. _specializations];
    }

    /// <inheritdoc/>
    public void Update(Specialization entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}