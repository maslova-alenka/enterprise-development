namespace Polyclinic.Test

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
            List<int> expectedDoctors = [1, 2, 3, 5, 6, 7];

            var experiencedDoctors = seed.Doctors
                .Where(d => d.ExperienceYears >= 10)
                .Select(d => d.Id)
                .Order()
                .ToList();

            Assert.NotNull(experiencedDoctors);
            Assert.Equal(expectedDoctors, experiencedDoctors);
        }

        /// <summary>
        /// Test to get patients by specific doctor ordered by name
        /// </summary>
        [Fact]
        public void GetPatientsByDoctorOrderedByName()
        {
            const int doctorId = 7;

            var patientIds = seed.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => a.PatientId)
                .Distinct();

            var result = seed.Patients
                .Where(p => patientIds.Contains(p.Id))
                .OrderBy(p => p.FullName)
                .ToList();

            Assert.True(result.Count > 0);
            Assert.Equal(result, [.. result.OrderBy(p => p.FullName)]);

        }

        /// <summary>
        /// Test to count follow-up appointments from last month
        /// </summary>
        [Fact]
        public void GetFollowUpAppointmentsCountLastMonth()
        {
            const int reference = 2;
            var referenceDate = new DateTime(2025, 3, 1);
            var lastMonth = referenceDate.AddMonths(-1);

            var result = seed.Appointments.Count(a =>
                a.IsFollowUp &&
                a.AppointmentDateTime.Month == lastMonth.Month &&
                a.AppointmentDateTime.Year == lastMonth.Year);

            Assert.Equal(reference, result);

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

            var cutoffDate = new DateTime(1995, 10, 16);
            var result = seed.Patients
                .Where(p => patientsWithMultipleDoctors.Contains(p.Id) && p.Birthday <= cutoffDate)
                .OrderBy(p => p.Birthday)
                .ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(result, [.. result.OrderBy(p => p.Birthday)]);
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
