using Polyclinic.Models;

namespace Polyclinic.Test;

/// <summary>
/// Test fixture for polyclinic unit tests
/// </summary>
public class TestFixture
{
    /// <summary>
    /// Collection of doctors for testing
    /// </summary>
    public List<Doctor> Doctors { get; }

    /// <summary>
    /// Collection of patients for testing
    /// </summary>
    public List<Patient> Patients { get; }

    /// <summary>
    /// Collection of medical appointments for testing
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Initializes a new instance of TestFixture with test data
    /// </summary>
    public TestFixture()
    {
        Doctors = DataSeed.Doctors;
        Patients = DataSeed.Patients;
        Appointments = DataSeed.Appointments;
    }
}
