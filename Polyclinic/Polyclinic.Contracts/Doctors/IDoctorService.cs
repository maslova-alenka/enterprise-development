using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts.Doctors;

public interface IDoctorService : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{

    public List<AppointmentDto> GetDoctorAppointments(int doctorId);


    public List<PatientDto> GetDoctorPatients(int doctorId);
}
