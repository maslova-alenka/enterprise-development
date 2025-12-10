using AutoMapper;
using Polyclinic.Contracts.Specializations;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Interfaces;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing specializations
/// </summary>
/// <param name="specializationRepository">Specialization repository</param>
/// <param name="mapper">Mapping profile</param>
public class SpecializationService(
    IRepository<Specialization, int> specializationRepository,
    IMapper mapper) : ISpecializationService
{
    /// <summary>
    /// Creates a new specialization asynchronously
    /// </summary>
    /// <param name="dto">Data for creating the specialization</param>
    /// <returns>Created specialization</returns>
    public async Task<SpecializationDto> CreateAsync(SpecializationCreateUpdateDto dto)
    {
        var newSpecialization = mapper.Map<Specialization>(dto);
        var allSpecializations = await specializationRepository.ReadAllAsync();
        var lastSpecializationId = allSpecializations.Max(s => s.Id);
        newSpecialization.Id = lastSpecializationId + 1;

        await specializationRepository.CreateAsync(newSpecialization);
        return mapper.Map<SpecializationDto>(newSpecialization);
    }

    /// <summary>
    /// Deletes a specialization by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>True if deletion was successful</returns>
    public async Task<bool> DeleteAsync(int dtoId)
    {
        await specializationRepository.DeleteAsync(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a specialization by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>Specialization if found</returns>
    public async Task<SpecializationDto?> GetAsync(int dtoId)
    {
        var specialization = await specializationRepository.ReadAsync(dtoId);
        return mapper.Map<SpecializationDto?>(specialization);
    }

    /// <summary>
    /// Retrieves all specializations asynchronously
    /// </summary>
    /// <returns>List of all specializations</returns>
    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var specializations = await specializationRepository.ReadAllAsync();
        return mapper.Map<List<SpecializationDto>>(specializations);
    }

    /// <summary>
    /// Updates an existing specialization asynchronously
    /// </summary>
    /// <param name="dto">Data for updating the specialization</param>
    /// <param name="dtoId">Specialization identifier</param>
    /// <returns>Updated specialization</returns>
    public async Task<SpecializationDto> UpdateAsync(SpecializationCreateUpdateDto dto, int dtoId)
    {
        var updateSpecialization = mapper.Map<Specialization>(dto);
        updateSpecialization.Id = dtoId;
        await specializationRepository.UpdateAsync(updateSpecialization);
        return mapper.Map<SpecializationDto>(updateSpecialization);
    }
}