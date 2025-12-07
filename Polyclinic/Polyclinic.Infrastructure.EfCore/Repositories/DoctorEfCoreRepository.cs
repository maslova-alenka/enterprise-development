using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class DoctorEfCoreRepository(PolyclinicDbContext db) : IRepository<Doctor, int>
{
    public void Create(Doctor entity)
    {
        db.Doctors.Add(entity);
        db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var entity = db.Doctors.Find(entityId);
        if (entity != null)
        {
            db.Doctors.Remove(entity);
            db.SaveChanges();
        }
    }

    public Doctor? Read(int entityId)
    {
        return db.Doctors
            .Include(d => d.Specialization) 
            .FirstOrDefault(d => d.Id == entityId);
    }

    public List<Doctor> ReadAll()
    {
        return db.Doctors
            .Include(d => d.Specialization)
            .OrderBy(d => d.Id)
            .ToList();
    }

    public void Update(Doctor entity)
    {
        if (db.Doctors.Any(x => x.Id == entity.Id))
        {
            db.Doctors.Update(entity);
            db.SaveChanges();
        }
    }
}