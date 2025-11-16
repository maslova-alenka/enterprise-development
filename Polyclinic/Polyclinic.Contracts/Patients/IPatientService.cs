using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;

namespace Polyclinic.Contracts.Patients;

public interface IPatientService : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    public List<AppointmentDto> GetPatientAppointments(int patientId);
}