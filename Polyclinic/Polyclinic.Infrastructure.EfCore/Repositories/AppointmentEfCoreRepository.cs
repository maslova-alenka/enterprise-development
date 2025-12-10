using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

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
    public async Task CreateAsync(Appointment entity)
    {
        if (entity.Patient == null || entity.Patient.Id <= 0)
            throw new ArgumentException("Patient must have valid Id");

        if (entity.Doctor == null || entity.Doctor.Id <= 0)
            throw new ArgumentException("Doctor must have valid Id");

        var existingPatient = db.Patients.Local.FirstOrDefault(p => p.Id == entity.Patient.Id)
                            ?? await db.Patients.FindAsync(entity.Patient.Id);
        if (existingPatient != null) entity.Patient = existingPatient;

        var existingDoctor = db.Doctors.Local.FirstOrDefault(d => d.Id == entity.Doctor.Id)
                           ?? await db.Doctors.FindAsync(entity.Doctor.Id);
        if (existingDoctor != null) entity.Doctor = existingDoctor;

        await db.Appointments.AddAsync(entity);
        await db.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes appointment by ID if exists.
    /// </summary>
    public async Task DeleteAsync(int entityId)
    {
        var entity = await db.Appointments.FindAsync(entityId);
        if (entity != null)
        {
            db.Appointments.Remove(entity);
            await db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets appointment by ID with Patient, Doctor and Specialization.
    /// </summary>
    public async Task<Appointment?> ReadAsync(int entityId)
    {
        return await db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor.Specialization)
            .FirstOrDefaultAsync(a => a.Id == entityId);
    }

    /// <summary>
    /// Gets all appointments with related data, ordered by ID.
    /// </summary>
    public async Task<List<Appointment>> ReadAllAsync()
    {
        return await db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor.Specialization)
            .OrderBy(a => a.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Updates existing appointment if found.
    /// </summary>
    public async Task UpdateAsync(Appointment entity)
    {
        if (await db.Appointments.AnyAsync(x => x.Id == entity.Id))
        {
            db.Appointments.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}