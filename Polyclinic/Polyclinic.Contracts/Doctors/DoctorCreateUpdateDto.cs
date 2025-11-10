namespace Polyclinic.Contracts.Doctors;

public record DoctorCreateUpdateDto(
    string PassportNumber,
    string FullName,
    int BirthYear,
    int SpecializationId,
    int ExperienceYears
    );
