using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;

namespace Polyclinic.Contracts.Appointments;

public interface IAppointmentService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    PatientDto? GetAppointmentPatient(int appointmentId);  
    DoctorDto? GetAppointmentDoctor(int appointmentId);  
}