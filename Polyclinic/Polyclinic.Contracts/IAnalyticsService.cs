using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Contracts;

public interface IAnalyticsService
{
    public List<DoctorDto> GetDoctorsWithExperienceAtLeast(int minYears);
    public List<PatientDto> GetPatientsByDoctorOrderedByName(int doctorId);
    public int GetFollowUpAppointmentsCountLastMonth(DateTime referenceDate);
    public List<PatientDto> GetPatientsOver30WithMultipleDoctors();
    public List<AppointmentDto> GetAppointmentsByRoomForCurrentMonth(int roomNumber, int year, int month);
}