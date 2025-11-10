using Polyclinic.Contracts;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;

namespace Polyclinic.Contracts.Appointments;

public interface IAppointmentService : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{

    public Task<PatientDto> GetAppointmentPatient(int appointmentId);


    public Task<DoctorDto> GetAppointmentDoctor(int appointmentId);
}