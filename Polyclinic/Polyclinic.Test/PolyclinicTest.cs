namespace Polyclinic.Test;

/// <summary>
/// Unit tests for Polyclinic
/// </summary>
public class PolyclinicTest(TestFixture fixture) : IClassFixture<TestFixture>
{
    /// <summary>
    /// Test to fetch doctors with at least 10 years of experience
    /// </summary>
    [Fact]
    public void GetDoctorsWithExperienceAtLeast10Years()
    {
        List<int> expectedDoctors = [1, 2, 3, 5, 6, 7];

        var experiencedDoctors = fixture.Doctors
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
        var expectedNames = new List<string> 
        {
            "Иванов Иван Иванович",
            "Петрова Оксана Владимировна",
            "Романова Виктория Сергеевна",
            "Чехов Александр Александрович"
        };

        var result = fixture.Appointments
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .Select(p => p.FullName)
            .ToList();

        Assert.Equal(expectedNames, result);
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

        var result = fixture.Appointments.Count(a =>
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
        var expectedNames = new List<string>
        {
            "Сидорова Валентина Ивановна",
            "Иванова Мария Романовна"        
        };

        var cutoffDate = new DateTime(1995, 10, 16);

        var patients = fixture.Appointments
            .GroupBy(a => a.Patient.Id)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.First().Patient)
            .Where(p => p.Birthday <= cutoffDate)
            .OrderBy(p => p.Birthday)
            .Select(p => p.FullName)
            .ToList();

        Assert.Equal(expectedNames, patients);
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

        var appointments = fixture.Appointments
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime >= startDate &&
                       a.AppointmentDateTime <= endDate)
            .ToList();

        Assert.Equal(expectedCount, appointments.Count);
    }
}
