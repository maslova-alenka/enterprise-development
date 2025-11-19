namespace Polyclinic.Contracts.Appointments;

/// <summary>
/// Data transfer object for appointment information
/// </summary>
/// <param name="Id">Appointment unique identifier</param>
/// <param name="PatientId">Patient unique identifier</param>
/// <param name="PatientName">Full name of the patient</param>
/// <param name="DoctorId">Doctor unique identifier</param>
/// <param name="DoctorName">Full name of the doctor</param>
/// <param name="AppointmentDateTime">Date and time of the appointment</param>
/// <param name="RoomNumber">Room number where the appointment takes place</param>
/// <param name="IsFollowUp">Indicates if this is a follow-up appointment</param>
public record AppointmentDto(
    int Id,
    int PatientId,
    string PatientName,
    int DoctorId,
    string DoctorName,
    DateTime AppointmentDateTime,
    int RoomNumber,
    bool IsFollowUp);