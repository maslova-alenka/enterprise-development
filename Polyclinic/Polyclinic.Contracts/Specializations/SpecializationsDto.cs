namespace Polyclinic.Contracts.Specializations;

/// <summary>
/// Data transfer object for specialization information
/// </summary>
/// <param name="Id">Specialization unique identifier</param>
/// <param name="Name">Name of the specialization</param>
public record SpecializationDto(int Id, string? Name);