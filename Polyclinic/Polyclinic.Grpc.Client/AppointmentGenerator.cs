using Bogus;
using Polyclinic.Grpc.Protos;

namespace Polyclinic.Grpc.Client;

/// <summary>
/// Appointment contract generator using Bogus library.
/// </summary>
public class AppointmentGenerator
{
    private readonly Faker _faker = new();

    private static readonly int[] _minutes = { 0, 15, 30, 45 };
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ss";

    /// <summary>
    /// Generates one random appointment contract.
    /// </summary>
    /// <param name="maxPatientId">Maximum patient ID in database.</param>
    /// <param name="maxDoctorId">Maximum doctor ID in database.</param>
    public AppointmentResponse GenerateRandomContract(
        int maxPatientId = 10,
        int maxDoctorId = 10)
    {
        var date = _faker.Date.Future(1);
        var appointmentDateTime = new DateTime(
            date.Year,
            date.Month,
            date.Day,
            _faker.Random.Int(8, 18),
            _faker.PickRandom(_minutes),
            0);

        return new AppointmentResponse
        {
            PatientId = _faker.Random.Int(1, maxPatientId),
            DoctorId = _faker.Random.Int(1, maxDoctorId),
            AppointmentDate = appointmentDateTime.ToString(DateFormat),
            RoomNumber = _faker.Random.Int(100, 500),
            IsFollowUp = _faker.Random.Bool(0.3f)
        };
    }

    /// <summary>
    /// Generates a batch of appointment contracts.
    /// </summary>
    public IEnumerable<AppointmentResponse> GenerateBulk(
        int count,
        int maxPatientId = 10,
        int maxDoctorId = 10)
    {
        for (var i = 0; i < count; i++)
            yield return GenerateRandomContract(maxPatientId, maxDoctorId);
    }
}