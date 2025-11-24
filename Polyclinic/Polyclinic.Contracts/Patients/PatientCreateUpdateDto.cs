using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Contracts.Patients;

/// <summary>
/// Data transfer object for creating and updating patients
/// </summary>
/// <param name="PassportNumber">Passport number for identification</param>
/// <param name="FullName">Full name of the patient</param>
/// <param name="Gender">Patient's gender</param>
/// <param name="Birthday">Date of birth</param>
/// <param name="Address">Residential address</param>
/// <param name="BloodType">Blood type</param>
/// <param name="RhFactor">Rh factor</param>
/// <param name="PhoneNumber">Contact phone number</param>
public record PatientCreateUpdateDto(
    [Required(ErrorMessage = "Passport number is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Passport number must be between 5 and 20 characters")]
    string PassportNumber,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    string FullName,

    [Required(ErrorMessage = "Gender is required")]
    [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be either Male or Female")]
    string Gender,

    [Required(ErrorMessage = "Birthday is required")]
    [DataType(DataType.Date)]
    DateTime Birthday,

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 200 characters")]
    string Address,

    [Required(ErrorMessage = "Blood type is required")]
    [RegularExpression("^(O|A|B|Ab)$", ErrorMessage = "Blood type must be O, A, B, or Ab")]
    string BloodType,

    [Required(ErrorMessage = "Rh factor is required")]
    [RegularExpression("^(Positive|Negative)$", ErrorMessage = "Rh factor must be either Positive or Negative")]
    string RhFactor,

    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Phone number must be between 5 and 15 characters")]
    string PhoneNumber
);