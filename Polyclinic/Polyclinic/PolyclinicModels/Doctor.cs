namespace Polyclinic.PolyclinicModels;

/// <summary>
/// Medical doctor information
/// </summary>
public class Doctor
{

    /// <summary>
    /// Unique identifier
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Passport number for identification
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Full name of the doctor
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Year of birth
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Medical specialization
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Years of professional experience
    /// </summary>
    public int ExperienceYears { get; set; }
}
