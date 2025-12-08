using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Doctor entities with CRUD operations.
/// Includes Specialization data in read operations.
/// </summary>
public class DoctorEfCoreRepository(PolyclinicDbContext db) : IRepository<Doctor, int>
{
    /// <summary>
    /// Creates new doctor in database.
    /// </summary>
    public void Create(Doctor entity)
    {
        db.Doctors.Add(entity);
        db.SaveChanges();
    }

    /// <summary>
    /// Deletes doctor by ID if exists.
    /// </summary>
    public void Delete(int entityId)
    {
        var entity = db.Doctors.Find(entityId);
        if (entity != null)
        {
            db.Doctors.Remove(entity);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Gets doctor by ID with Specialization included.
    /// </summary>
    public Doctor? Read(int entityId)
    {
        return db.Doctors
            .Include(d => d.Specialization)
            .FirstOrDefault(d => d.Id == entityId);
    }

    /// <summary>
    /// Gets all doctors with Specializations, ordered by ID.
    /// </summary>
    public List<Doctor> ReadAll()
    {
        return db.Doctors
            .Include(d => d.Specialization)
            .OrderBy(d => d.Id)
            .ToList();
    }

    /// <summary>
    /// Updates existing doctor if found.
    /// </summary>
    public void Update(Doctor entity)
    {
        if (db.Doctors.Any(x => x.Id == entity.Id))
        {
            db.Doctors.Update(entity);
            db.SaveChanges();
        }
    }
}