using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Specializations;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for managing medical specializations
/// </summary>
public class SpecializationsController(ISpecializationService crudService, ILogger<SpecializationsController> logger)
    : CrudControllerBase<SpecializationDto, SpecializationCreateUpdateDto, int>(crudService, logger);