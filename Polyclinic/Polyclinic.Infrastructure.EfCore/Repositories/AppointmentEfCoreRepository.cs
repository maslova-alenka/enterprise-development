using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Appointment entities with CRUD operations.
/// Handles Patient-Doctor relationships and entity tracking.
/// </summary>
public class AppointmentEfCoreRepository(PolyclinicDbContext db) : IRepository<Appointment, int>
{
    /// <summary>
    /// Creates new appointment with proper entity tracking.
    /// </summary>
    public void Create(Appointment entity)
    {
        if (entity.Patient == null || entity.Patient.Id <= 0)
            throw new ArgumentException("Patient must have valid Id");

        if (entity.Doctor == null || entity.Doctor.Id <= 0)
            throw new ArgumentException("Doctor must have valid Id");

        if (entity.Patient.Id > 0)
        {
            var existingPatient = db.Patients.Local.FirstOrDefault(p => p.Id == entity.Patient.Id)
                                ?? db.Patients.Find(entity.Patient.Id);
            if (existingPatient != null) entity.Patient = existingPatient;
        }

        if (entity.Doctor.Id > 0)
        {
            var existingDoctor = db.Doctors.Local.FirstOrDefault(d => d.Id == entity.Doctor.Id)
                               ?? db.Doctors.Find(entity.Doctor.Id);
            if (existingDoctor != null) entity.Doctor = existingDoctor;
        }

        db.Appointments.Add(entity);
        db.SaveChanges();
    }

    /// <summary>
    /// Deletes appointment by ID if exists.
    /// </summary>
    public void Delete(int entityId)
    {
        var entity = db.Appointments.Find(entityId);
        if (entity != null)
        {
            db.Appointments.Remove(entity);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Gets appointment by ID with Patient, Doctor and Specialization.
    /// </summary>
    public Appointment? Read(int entityId)
    {
        return db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor).ThenInclude(d => d.Specialization)
            .FirstOrDefault(a => a.Id == entityId);
    }

    /// <summary>
    /// Gets all appointments with related data, ordered by ID.
    /// </summary>
    public List<Appointment> ReadAll()
    {
        return db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor).ThenInclude(d => d.Specialization)
            .OrderBy(a => a.Id)
            .ToList();
    }

    /// <summary>
    /// Updates existing appointment if found.
    /// </summary>
    public void Update(Appointment entity)
    {
        if (db.Appointments.Any(x => x.Id == entity.Id))
        {
            db.Appointments.Update(entity);
            db.SaveChanges();
        }
    }
}