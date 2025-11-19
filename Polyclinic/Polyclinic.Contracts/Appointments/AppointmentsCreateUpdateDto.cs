namespace Polyclinic.Contracts.Appointments;

/// <summary>
/// Data transfer object for creating and updating appointments
/// </summary>
/// <param name="PatientId">Patient unique identifier</param>
/// <param name="DoctorId">Doctor unique identifier</param>
/// <param name="AppointmentDateTime">Date and time of the appointment</param>
/// <param name="RoomNumber">Room number where the appointment takes place</param>
/// <param name="IsFollowUp">Indicates if this is a follow-up appointment</param>
public record AppointmentCreateUpdateDto(
    int PatientId,
    int DoctorId,
    DateTime AppointmentDateTime,
    int RoomNumber,
    bool IsFollowUp);