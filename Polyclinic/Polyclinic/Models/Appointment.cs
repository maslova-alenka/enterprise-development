namespace Polyclinic.Models;

/// <summary>
/// Represents a medical appointment in the polyclinic system.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Unique identifier for the appointment record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key referencing the Patient who booked the appointment
    /// </summary>
    public required Patient Patient { get; set; }

    /// <summary>
    /// Foreign key referencing the Doctor who will conduct the appointment
    /// </summary>
    public required Doctor Doctor { get; set; }

    /// <summary>
    /// Date and time when the appointment is scheduled to take place
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Room number where the appointment will be conducted
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Indicates whether this is a follow-up appointment (true) or initial consultation (false)
    /// Follow-up appointments are for returning patients for continued treatment
    /// </summary>
    public bool IsFollowUp { get; set; }
}
