using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Base controller for CRUD operations with async support
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
    /// Creates a new entity asynchronously
    /// </summary>
    /// <param name="newDto">Data for creating the entity</param>
    /// <returns>Created entity</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> CreateAsync(TCreateUpdateDto newDto)
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

            var res = await appService.CreateAsync(newDto);

            return StatusCode(201, res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CreateAsync");
            return StatusCode(500, "Failed to create entity");
        }
    }

    /// <summary>
    /// Updates an existing entity asynchronously
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="newDto">Data for updating the entity</param>
    /// <returns>Updated entity</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> EditAsync(TKey id, TCreateUpdateDto newDto)
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

            var res = await appService.UpdateAsync(newDto, id);
            return Ok(res);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("not found"))
        {
            logger.LogWarning(ex, "Entity not found in EditAsync");
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in EditAsync");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in EditAsync");
            return StatusCode(500, "Failed to update entity");
        }
    }

    /// <summary>
    /// Deletes an entity by identifier asynchronously
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>Action result indicating success or failure</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult> DeleteAsync(TKey id)
    {
        try
        {
            var result = await appService.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in DeleteAsync");
            return StatusCode(500, "Failed to delete entity");
        }
    }

    /// <summary>
    /// Retrieves all entities asynchronously
    /// </summary>
    /// <returns>List of all entities</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<List<TDto>>> GetAllAsync()
    {
        try
        {
            var res = await appService.GetAllAsync();
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAllAsync");
            return StatusCode(500, "Failed to get entities list");
        }
    }

    /// <summary>
    /// Retrieves an entity by identifier asynchronously
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>Entity if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public virtual async Task<ActionResult<TDto>> GetAsync(TKey id)
    {
        try
        {
            var res = await appService.GetAsync(id);
            return res != null ? Ok(res) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAsync");
            return StatusCode(500, "Failed to get entity");
        }
    }
}