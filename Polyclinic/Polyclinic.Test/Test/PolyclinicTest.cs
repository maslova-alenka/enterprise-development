namespace Polyclinic.Test.Test

/// <summary>
/// Unit tests for Polyclinic
/// </summary>
{
    public class PolyclinicTest(DataSeed seed) : IClassFixture<DataSeed>
    {

        /// <summary>
        /// Test to fetch doctors with at least 10 years of experience
        /// </summary>
        [Fact]
        public void GetDoctorsWithExperienceAtLeast10Years()
        {
            var expectedCount = 6;

            var experiencedDoctors = seed.Doctors.Where(d => d.ExperienceYears >= 10).ToList();

            Assert.NotNull(experiencedDoctors);
            Assert.All(experiencedDoctors, d => Assert.True(d.ExperienceYears >= 10));
            Assert.Equal(expectedCount, experiencedDoctors.Count);
        }

        /// <summary>
        /// Test to get patients by specific doctor ordered by name
        /// </summary>
        [Fact]
        public void GetPatientsByDoctorOrderedByName()
        {
            var doctorId = 7;

            var patientIds = seed.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.PatientId)
                .Distinct();

            var result = seed.Patients
                .Where(p => patientIds.Contains(p.ID))
                .OrderBy(p => p.FullName)
                .ToList();

            Assert.True(result.Count > 0);
            Assert.Equal(result, result.OrderBy(p => p.FullName).ToList());

        }

        /// <summary>
        /// Test to count follow-up appointments from last month
        /// </summary>
        [Fact]
        public void GetFollowUpAppointmentsCountLastMonth()
        {

            var lastMonth = DateTime.Now.AddMonths(-1);

            var result = seed.Appointments.Count(a =>
                a.IsFollowUp &&
                a.AppointmentDateTime.Month == lastMonth.Month &&
                a.AppointmentDateTime.Year == lastMonth.Year);

            Assert.True(result >= 0);
        }

        /// <summary>
        /// Test to find patients over 30 years old with multiple doctors
        /// </summary>
        [Fact]
        public void GetPatientsOver30WithMultipleDoctors()
        {
            var patientsWithMultipleDoctors = seed.Appointments
                .GroupBy(a => a.PatientId)
                .Where(g => g.Select(a => a.DoctorId).Distinct().Count() > 1)
                .Select(g => g.Key)
                .ToList();

            var cutoffDate = DateTime.Now.AddYears(-30);
            var result = seed.Patients
                .Where(p => patientsWithMultipleDoctors.Contains(p.ID) && p.Birthday <= cutoffDate)
                .OrderBy(p => p.Birthday)
                .ToList();

            Assert.All(result, patient => Assert.True(DateTime.Now.Year - patient.Birthday.Year >= 30));
            Assert.Equal(result, result.OrderBy(p => p.Birthday).ToList());
        }

        /// <summary>
        /// Test to fetch appointments by room number for current month
        /// </summary>
        [Fact]
        public void GetAppointmentsByRoomCurrentMonth()
        {
            const int expectedCount = 2;
            const int roomNumber = 201;
            var startDate = new DateTime(2025, 2, 1);
            var endDate = new DateTime(2025, 2, 28);

            var appointments = seed.Appointments
                .Where(a => a.RoomNumber == roomNumber &&
                           a.AppointmentDateTime >= startDate &&
                           a.AppointmentDateTime <= endDate)
                .ToList();

            Assert.Equal(expectedCount, appointments.Count);
        }
    }
}
