using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts;


public interface IAnalyticsService
{

    public Task<IList<DoctorDto>> GetDoctorsWithExperienceAtLeast(int minYears);


    public Task<IList<PatientDto>> GetPatientsByDoctorOrderedByName(int doctorId);


    public Task<int> GetFollowUpAppointmentsCountLastMonth(DateTime referenceDate);


    public Task<IList<PatientDto>> GetPatientsOver30WithMultipleDoctors();


    public Task<IList<AppointmentDto>> GetAppointmentsByRoomForCurrentMonth(int roomNumber, int year, int month);
}