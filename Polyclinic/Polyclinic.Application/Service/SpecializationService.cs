using AutoMapper;
using Polyclinic.Contracts.Specializations;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

public class SpecializationService(IRepository<Specialization, int> specializationRepository, IMapper mapper)
    : ISpecializationService
{
    /// <inheritdoc/>
    public SpecializationDto Create(SpecializationCreateUpdateDto dto)
    {
        var newSpecialization = mapper.Map<Specialization>(dto);
        var lastSpecialization = specializationRepository.ReadAll().OrderByDescending(s => s.Id).FirstOrDefault();
        newSpecialization.Id = (lastSpecialization?.Id ?? 0) + 1;
        specializationRepository.Create(newSpecialization);
        return mapper.Map<SpecializationDto>(newSpecialization);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId)
    {
        specializationRepository.Delete(dtoId);
        return true;
    }

    /// <inheritdoc/>
    public SpecializationDto? Get(int dtoId) =>
        mapper.Map<SpecializationDto?>(specializationRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<SpecializationDto> GetAll() =>
        mapper.Map<List<SpecializationDto>>(specializationRepository.ReadAll());

    /// <inheritdoc/>
    public SpecializationDto Update(SpecializationCreateUpdateDto dto, int dtoId)
    {
        var updateSpecialization = mapper.Map<Specialization>(dto);
        updateSpecialization.Id = dtoId;
        specializationRepository.Update(updateSpecialization);
        return mapper.Map<SpecializationDto>(updateSpecialization);
    }
}