using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Base controller for CRUD operations
/// </summary>
/// <typeparam name="TDto">Data transfer object type</typeparam>
/// <typeparam name="TCreateUpdateDto">Create and update data transfer object type</typeparam>
/// <typeparam name="TKey">Entity identifier type</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="newDto">Data for creating the entity</param>
    /// <returns>Created entity</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TCreateUpdateDto newDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var res = appService.Create(newDto);
            return CreatedAtAction(nameof(Get), new { id = res.GetType().GetProperty("Id")?.GetValue(res) }, res);
        }
        catch (Exception ex)
        {
            logger.LogError("Error in Create: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="newDto">Data for updating the entity</param>
    /// <returns>Updated entity</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey id, TCreateUpdateDto newDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var res = appService.Update(newDto, id);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("Error in Edit: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>Action result indicating success or failure</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult Delete(TKey id)
    {
        try
        {
            var res = appService.Delete(id);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in Delete: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<TDto>> GetAll()
    {
        try
        {
            var res = appService.GetAll();
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetAll: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves an entity by identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>Entity if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        try
        {
            var res = appService.Get(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in Get: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}