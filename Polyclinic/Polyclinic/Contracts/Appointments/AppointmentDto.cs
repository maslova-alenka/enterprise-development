namespace Polyclinic.Contracts.Appointments;

    public record AppointmentDto(
        int Id,
        int PatientId,
        string PatientName,
        int DoctorId,
        string DoctorName,
        DateTime AppointmentDateTime,
        int RoomNumber,
        bool IsFollowUp);

