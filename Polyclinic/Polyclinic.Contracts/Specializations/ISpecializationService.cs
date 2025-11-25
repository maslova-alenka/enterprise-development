namespace Polyclinic.Contracts.Specializations;

/// <summary>
/// Service interface for managing specializations
/// </summary>
public interface ISpecializationService : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>;