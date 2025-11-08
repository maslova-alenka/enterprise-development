using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;

namespace Polyclinic.Contracts.Patients;

public interface IPatientService : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    public Task<IList<AppointmentDto>> GetPatientAppointments(int patientId);
}