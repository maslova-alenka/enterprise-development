namespace Polyclinic.Contracts.Doctors;

public record DoctorDto(
    int Id,
    string PassportNumber,
    string FullName,
    int BirthYear,
    int SpecializationId,
    string SpecializationName,
    int ExperienceYears);