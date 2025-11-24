using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Patient ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be positive")]
    int PatientId,

    [Required(ErrorMessage = "Doctor ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be positive")]
    int DoctorId,

    [Required(ErrorMessage = "Appointment date and time is required")]
    [DataType(DataType.DateTime)]
    DateTime AppointmentDateTime,

    [Required(ErrorMessage = "Room number is required")]
    [Range(1, 1000, ErrorMessage = "Room number must be between 1 and 1000")]
    int RoomNumber,

    bool IsFollowUp
);