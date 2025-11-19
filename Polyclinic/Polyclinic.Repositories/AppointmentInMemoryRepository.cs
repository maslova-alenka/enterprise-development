using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// In-memory repository implementation for appointments
/// </summary>
public class AppointmentInMemoryRepository : IRepository<Appointment, int>
{
    private readonly List<Appointment> _appointments;

    /// <summary>
    /// Initializes a new instance of the AppointmentInMemoryRepository class
    /// </summary>
    public AppointmentInMemoryRepository()
    {
        _appointments = DataSeed.Appointments;
    }

    /// <summary>
    /// Creates a new appointment
    /// </summary>
    /// <param name="entity">Appointment to create</param>
    public void Create(Appointment entity)
    {
        _appointments.Add(entity);
    }

    /// <summary>
    /// Deletes an appointment by identifier
    /// </summary>
    /// <param name="entityId">Appointment identifier</param>
    public void Delete(int entityId)
    {
        var appointment = Read(entityId);
        if (appointment != null)
            _appointments.Remove(appointment);
    }

    /// <summary>
    /// Retrieves an appointment by identifier
    /// </summary>
    /// <param name="entityId">Appointment identifier</param>
    /// <returns>Appointment if found</returns>
    public Appointment Read(int entityId)
    {
        return _appointments.First(a => a.Id == entityId);
    }

    /// <summary>
    /// Retrieves all appointments
    /// </summary>
    /// <returns>List of all appointments</returns>
    public List<Appointment> ReadAll()
    {
        return [.. _appointments];
    }

    /// <summary>
    /// Updates an existing appointment
    /// </summary>
    /// <param name="entity">Appointment with updated data</param>
    public void Update(Appointment entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}