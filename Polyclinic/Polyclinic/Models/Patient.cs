using Polyclinic.Models.Enums;

namespace Polyclinic.Models;

/// <summary>
/// Patient personal and medical information
/// </summary>
public class Patient
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Passport number for identification
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the patient
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gender of the patient
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime Birthday { get; set; }

    /// <summary>
    /// Residential address
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Blood group type
    /// </summary>
    public BloodType BloodType { get; set; }

    /// <summary>
    /// Rh factor classification
    /// </summary>
    public RhFactor RhFactor { get; set; }

    /// <summary>
    /// Contact phone number
    /// </summary>
    public required string PhoneNumber { get; set; }
}

