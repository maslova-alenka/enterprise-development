namespace Polyclinic.PolyclinicModels;
public class Appointment
{
    public int ID { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int RoomNumber { get; set; }
    public bool IsFollowUp { get; set; }
}
