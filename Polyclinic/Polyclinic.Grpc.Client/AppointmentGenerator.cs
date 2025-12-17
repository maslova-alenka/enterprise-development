using Bogus;
using Polyclinic.Grpc.Protos;

namespace Polyclinic.Grpc.Client;

/// <summary>
/// Appointment contract generator using Bogus library.
/// </summary>
public class AppointmentGenerator
{
    private readonly Faker _faker = new();

    /// <summary>
    /// Generates one random appointment contract.
    /// </summary>
    /// <param name="maxPatientId">Maximum patient ID in database.</param>
    /// <param name="maxDoctorId">Maximum doctor ID in database.</param>
    public AppointmentResponse GenerateRandomContract(
        int maxPatientId = 10,
        int maxDoctorId = 10)
    {
        var hour = _faker.Random.Int(8, 18);
        var minute = _faker.PickRandom(new[] { 0, 15, 30, 45 });

        return new AppointmentResponse
        {
            PatientId = _faker.Random.Int(1, maxPatientId),
            DoctorId = _faker.Random.Int(1, maxDoctorId),
            AppointmentDate = _faker.Date.FutureDateOnly(1).ToString("yyyy-MM-dd") +
                             $"T{hour:00}:{minute:00}:00",
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