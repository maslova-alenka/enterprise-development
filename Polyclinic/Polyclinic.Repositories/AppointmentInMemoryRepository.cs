using Polyclinic.Models;
using Polyclinic.Test;

namespace Polyclinic.Repositories;

/// <summary>
/// Имплементация репозитория для записей на прием
/// </summary>
public class AppointmentInMemoryRepository : IRepository<Appointment, int>
{
    private readonly List<Appointment> _appointments;

    /// <inheritdoc/>
    public AppointmentInMemoryRepository()
    {
        _appointments = DataSeed.Appointments;
    }

    /// <inheritdoc/>
    public void Create(Appointment entity)
    {
        _appointments.Add(entity);
    }

    /// <inheritdoc/>
    public void Delete(int entityId)
    {
        var appointment = Read(entityId);
        if (appointment != null)
            _appointments.Remove(appointment);
    }

    /// <inheritdoc/>
    public Appointment Read(int entityId)
    {
        return _appointments.First(a => a.Id == entityId);
    }

    /// <inheritdoc/>
    public List<Appointment> ReadAll()
    {
        return [.. _appointments];
    }

    /// <inheritdoc/>
    public void Update(Appointment entity)
    {
        Delete(entity.Id);
        Create(entity);
    }
}