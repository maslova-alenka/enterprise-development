using Polyclinic.Domain.Data;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Repositories.InMemory;

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
    /// Creates a new appointment asynchronously
    /// </summary>
    /// <param name="entity">Appointment to create</param>
    public Task CreateAsync(Appointment entity)
    {
        _appointments.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes an appointment by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Appointment identifier</param>
    public Task DeleteAsync(int entityId)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == entityId);
        if (appointment != null)
            _appointments.Remove(appointment);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Retrieves an appointment by identifier asynchronously
    /// </summary>
    /// <param name="entityId">Appointment identifier</param>
    /// <returns>Appointment if found</returns>
    public Task<Appointment?> ReadAsync(int entityId)
    {
        return Task.FromResult(_appointments.FirstOrDefault(a => a.Id == entityId));
    }

    /// <summary>
    /// Retrieves all appointments asynchronously
    /// </summary>
    /// <returns>List of all appointments</returns>
    public Task<List<Appointment>> ReadAllAsync()
    {
        return Task.FromResult<List<Appointment>>([.. _appointments]);
    }

    /// <summary>
    /// Updates an existing appointment asynchronously
    /// </summary>
    /// <param name="entity">Appointment with updated data</param>
    public async Task UpdateAsync(Appointment entity)
    {
        await DeleteAsync(entity.Id);
        await CreateAsync(entity);
    }
}