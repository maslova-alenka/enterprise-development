namespace Polyclinic.Contracts.Appointments;

    public record AppointmentCreateUpdateDto(
        int PatientId,
        int DoctorId,
        DateTime AppointmentDateTime,
        int RoomNumber,
        bool IsFollowUp);

