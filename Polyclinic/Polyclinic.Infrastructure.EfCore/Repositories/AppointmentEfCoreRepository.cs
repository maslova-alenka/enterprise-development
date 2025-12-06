using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class AppointmentEfCoreRepository(PolyclinicDbContext db) : IRepository<Appointment, int>
{
    public void Create(Appointment entity)
    {
        db.Appointments.Add(entity);
        db.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var entity = db.Appointments.Find(entityId);
        if (entity != null)
        {
            db.Appointments.Remove(entity);
            db.SaveChanges();
        }
    }

    public Appointment? Read(int entityId)
    {
        return db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialization)
            .FirstOrDefault(a => a.Id == entityId);
    }

    public List<Appointment> ReadAll()
    {
        return db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialization)
            .OrderBy(a => a.Id)
            .ToList();
    }

    public void Update(Appointment entity)
    {
        if (db.Appointments.Any(x => x.Id == entity.Id))
        {
            db.Appointments.Update(entity);
            db.SaveChanges();
        }
    }
}