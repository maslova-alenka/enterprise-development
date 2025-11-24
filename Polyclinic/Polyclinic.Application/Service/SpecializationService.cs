using AutoMapper;
using Polyclinic.Contracts.Specializations;
using Polyclinic.Domain.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing specializations
/// </summary>
/// <param name="specializationRepository">Specialization repository</param>
/// <param name="mapper">Mapping profile</param>
public class SpecializationService(IRepository<Specialization, int> specializationRepository, IMapper mapper)
    : ISpecializationService
{
    /// <summary>
    /// Creates a new specialization
    /// </summary>
    /// <param name="dto">Data for creating the specialization</param>
    /// <returns>Created specialization</returns>
    public SpecializationDto Create(SpecializationCreateUpdateDto dto)
    {
        var newSpecialization = mapper.Map<Specialization>(dto);
        var lastSpecialization = specializationRepository.ReadAll().OrderByDescending(s => s.Id).FirstOrDefault();
        newSpecialization.Id = (lastSpecialization?.Id ?? 0) + 1;
        specializationRepository.Create(newSpecialization);
        return mapper.Map<SpecializationDto>(newSpecialization);
    }

    /// <summary>
    /// Deletes a specialization by identifier
    /// </summary>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>True if deletion was successful</returns>
    public bool Delete(int dtoId)
    {
        specializationRepository.Delete(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a specialization by identifier
    /// </summary>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>Specialization if found</returns>
    public SpecializationDto? Get(int dtoId) =>
        mapper.Map<SpecializationDto?>(specializationRepository.Read(dtoId));

    /// <summary>
    /// Retrieves all specializations
    /// </summary>
    /// <returns>List of all specializations</returns>
    public List<SpecializationDto> GetAll() =>
        mapper.Map<List<SpecializationDto>>(specializationRepository.ReadAll());

    /// <summary>
    /// Updates an existing specialization
    /// </summary>
    /// <param name="dto">Data for updating the specialization</param>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>Updated specialization</returns>
    public SpecializationDto Update(SpecializationCreateUpdateDto dto, int dtoId)
    {
        var updateSpecialization = mapper.Map<Specialization>(dto);
        updateSpecialization.Id = dtoId;
        specializationRepository.Update(updateSpecialization);
        return mapper.Map<SpecializationDto>(updateSpecialization);
    }
}