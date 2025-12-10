using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Patient entities with CRUD operations.
/// Uses AsNoTracking for read operations to improve performance.
/// </summary>
public class PatientEfCoreRepository(PolyclinicDbContext db) : IRepository<Patient, int>
{
    /// <summary>
    /// Creates new patient in database asynchronously.
    /// </summary>
    public async Task CreateAsync(Patient entity)
    {
        await db.Patients.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes patient by ID if exists asynchronously.
    /// </summary>
    public async Task DeleteAsync(int entityId)
    {
        var entity = await db.Patients.FindAsync(entityId);
        if (entity != null)
        {
            db.Patients.Remove(entity);
            await db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets patient by ID without entity tracking asynchronously.
    /// </summary>
    public async Task<Patient?> ReadAsync(int entityId)
    {
        return await db.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == entityId);
    }

    /// <summary>
    /// Gets all patients without tracking, ordered by ID asynchronously.
    /// </summary>
    public async Task<List<Patient>> ReadAllAsync()
    {
        return await db.Patients
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Updates existing patient if found asynchronously.
    /// </summary>
    public async Task UpdateAsync(Patient entity)
    {
        if (await db.Patients.AnyAsync(x => x.Id == entity.Id))
        {
            db.Patients.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}