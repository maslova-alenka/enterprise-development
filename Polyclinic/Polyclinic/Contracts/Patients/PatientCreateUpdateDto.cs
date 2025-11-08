namespace Polyclinic.Contracts.Patients;
public record PatientCreateUpdateDto(
    string PassportNumber,
    string FullName,
    string Gender,
    DateTime Birthday,
    string Address,
    string BloodType,
    string RhFactor,
    string PhoneNumber);
