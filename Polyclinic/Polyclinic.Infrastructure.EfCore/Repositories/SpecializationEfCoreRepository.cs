using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Specialization entities with CRUD operations.
/// Uses explicit DbSet for better separation of concerns.
/// </summary>
public class SpecializationEfCoreRepository(PolyclinicDbContext context) : IRepository<Specialization, int>
{
    private readonly DbSet<Specialization> _specializations = context.Specializations;

    /// <summary>
    /// Creates new specialization in database asynchronously.
    /// </summary>
    public async Task CreateAsync(Specialization entity)
    {
        await _specializations.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes specialization by ID if exists asynchronously.
    /// </summary>
    public async Task DeleteAsync(int entityId)
    {
        var entity = await ReadAsync(entityId);
        if (entity != null)
        {
            _specializations.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets specialization by ID asynchronously.
    /// </summary>
    public async Task<Specialization?> ReadAsync(int entityId)
    {
        return await _specializations.FirstOrDefaultAsync(s => s.Id == entityId);
    }

    /// <summary>
    /// Gets all specializations ordered by ID asynchronously.
    /// </summary>
    public async Task<List<Specialization>> ReadAllAsync()
    {
        return await _specializations.OrderBy(s => s.Id).ToListAsync();
    }

    /// <summary>
    /// Updates existing specialization asynchronously.
    /// </summary>
    public async Task UpdateAsync(Specialization entity)
    {
        _specializations.Update(entity);
        await context.SaveChangesAsync();
    }
}