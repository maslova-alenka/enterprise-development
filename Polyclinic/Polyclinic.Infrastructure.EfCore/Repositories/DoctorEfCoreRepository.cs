using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Doctor entities with CRUD operations.
/// Includes Specialization data in read operations.
/// </summary>
public class DoctorEfCoreRepository(PolyclinicDbContext db) : IRepository<Doctor, int>
{
    /// <summary>
    /// Creates new doctor in database asynchronously.
    /// </summary>
    public async Task CreateAsync(Doctor entity)
    {
        await db.Doctors.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes doctor by ID if exists asynchronously.
    /// </summary>
    public async Task DeleteAsync(int entityId)
    {
        var entity = await db.Doctors.FindAsync(entityId);
        if (entity != null)
        {
            db.Doctors.Remove(entity);
            await db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets doctor by ID with Specialization included asynchronously.
    /// </summary>
    public async Task<Doctor?> ReadAsync(int entityId)
    {
        return await db.Doctors
            .Include(d => d.Specialization)
            .FirstOrDefaultAsync(d => d.Id == entityId);
    }

    /// <summary>
    /// Gets all doctors with Specializations, ordered by ID asynchronously.
    /// </summary>
    public async Task<List<Doctor>> ReadAllAsync()
    {
        return await db.Doctors
            .Include(d => d.Specialization)
            .OrderBy(d => d.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Updates existing doctor if found asynchronously.
    /// </summary>
    public async Task UpdateAsync(Doctor entity)
    {
        if (await db.Doctors.AnyAsync(x => x.Id == entity.Id))
        {
            db.Doctors.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}