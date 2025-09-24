namespace Polyclinic.PolyclinicModels;
public class Appointment
{
    public string PatientPassport { get; set; }
    public string DoctorPassport { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int RoomNumber { get; set; }
    public bool IsFollowUp { get; set; }
}
