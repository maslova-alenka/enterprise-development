using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Patient entities with CRUD operations.
/// Uses AsNoTracking for read operations to improve performance.
/// </summary>
public class PatientEfCoreRepository(PolyclinicDbContext db) : IRepository<Patient, int>
{
    /// <summary>
    /// Creates new patient in database.
    /// </summary>
    public void Create(Patient entity)
    {
        db.Patients.Add(entity);
        db.SaveChanges();
    }

    /// <summary>
    /// Deletes patient by ID if exists.
    /// </summary>
    public void Delete(int entityId)
    {
        var entity = db.Patients.Find(entityId);
        if (entity != null)
        {
            db.Patients.Remove(entity);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Gets patient by ID without entity tracking.
    /// </summary>
    public Patient? Read(int entityId)
    {
        return db.Patients
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == entityId);
    }

    /// <summary>
    /// Gets all patients without tracking, ordered by ID.
    /// </summary>
    public List<Patient> ReadAll()
    {
        return db.Patients
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToList();
    }

    /// <summary>
    /// Updates existing patient if found.
    /// </summary>
    public void Update(Patient entity)
    {
        if (db.Patients.Any(x => x.Id == entity.Id))
        {
            db.Patients.Update(entity);
            db.SaveChanges();
        }
    }
}