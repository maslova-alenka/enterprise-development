namespace Polyclinic.Contracts.Patients;

/// <summary>
/// Data transfer object for patient information
/// </summary>
/// <param name="Id">Patient unique identifier</param>
/// <param name="PassportNumber">Passport number for identification</param>
/// <param name="FullName">Full name of the patient</param>
/// <param name="Gender">Patient's gender</param>
/// <param name="Birthday">Date of birth</param>
/// <param name="Address">Residential address</param>
/// <param name="BloodType">Blood type</param>
/// <param name="RhFactor">Rh factor</param>
/// <param name="PhoneNumber">Contact phone number</param>
public record PatientDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateTime Birthday,
    string Address,
    string BloodType,
    string RhFactor,
    string PhoneNumber
    );