using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class PatientEfCoreRepository(PolyclinicDbContext db) : IRepository<Patient, int>
{
    public void Create(Patient entity)
    {
        db.Patients.Add(entity);
        db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var entity = db.Patients.Find(entityId);
        if (entity != null)
        {
            db.Patients.Remove(entity);
            db.SaveChanges();
        }
    }

    public Patient? Read(int entityId)
    {
        return db.Patients
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == entityId);
    }

    public List<Patient> ReadAll()
    {
        return db.Patients
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToList();
    }

    public void Update(Patient entity)
    {
        if (db.Patients.Any(x => x.Id == entity.Id))
        {
            db.Patients.Update(entity);
            db.SaveChanges();
        }
    }
}