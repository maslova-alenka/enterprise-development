namespace Polyclinic.Contracts.Patients;

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
