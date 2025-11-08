using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts.Doctors;

public interface IDoctorService : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{

    public Task<IList<AppointmentDto>> GetDoctorAppointments(int doctorId);


    public Task<IList<PatientDto>> GetDoctorPatients(int doctorId);
}
