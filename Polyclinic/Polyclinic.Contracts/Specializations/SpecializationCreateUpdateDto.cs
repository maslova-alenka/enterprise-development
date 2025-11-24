using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Contracts.Specializations;

/// <summary>
/// Data transfer object for creating and updating specializations
/// </summary>
/// <param name="Name">Name of the specialization</param>
public record SpecializationCreateUpdateDto(
    [Required(ErrorMessage = "Specialization name is required")]
    string? Name
);