using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Passport number must be between 5 and 20 characters")]
    string PassportNumber,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    string FullName,

    [Required(ErrorMessage = "Birth year is required")]
    [Range(1900, 2025, ErrorMessage = "Birth year must be between 1900 and 2020")]
    int BirthYear,

    [Required(ErrorMessage = "Specialization ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Specialization ID must be positive")]
    int SpecializationId,

    [Required(ErrorMessage = "Experience years is required")]
    [Range(0, 70, ErrorMessage = "Experience years must be between 0 and 70")]
    int ExperienceYears
);