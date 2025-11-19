namespace Polyclinic.Contracts.Doctors;

/// <summary>
/// Data transfer object for doctor information
/// </summary>
/// <param name="Id">Doctor unique identifier</param>
/// <param name="PassportNumber">Passport number for identification</param>
/// <param name="FullName">Full name of the doctor</param>
/// <param name="BirthYear">Year of birth</param>
/// <param name="SpecializationId">Specialization unique identifier</param>
/// <param name="SpecializationName">Name of the specialization</param>
/// <param name="ExperienceYears">Years of professional experience</param>
public record DoctorDto(
    int Id,
    string PassportNumber,
    string FullName,
    int BirthYear,
    int SpecializationId,
    string SpecializationName,
    int ExperienceYears);