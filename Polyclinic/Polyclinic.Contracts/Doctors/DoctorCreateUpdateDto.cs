namespace Polyclinic.Contracts.Doctors;

/// <summary>
/// Data transfer object for creating and updating doctors
/// </summary>
/// <param name="PassportNumber">Passport number for identification</param>
/// <param name="FullName">Full name of the doctor</param>
/// <param name="BirthYear">Year of birth</param>
/// <param name="SpecializationId">Specialization unique identifier</param>
/// <param name="ExperienceYears">Years of professional experience</param>
public record DoctorCreateUpdateDto(
    string PassportNumber,
    string FullName,
    int BirthYear,
    int SpecializationId,
    int ExperienceYears
    );