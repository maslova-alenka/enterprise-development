using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for managing doctors and their related data
/// </summary>
public class DoctorsController(IDoctorService crudService, ILogger<DoctorsController> logger)
    : CrudControllerBase<DoctorDto, DoctorCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Retrieves all appointments for a specific doctor asynchronously
    /// </summary>
    /// <param name="id">Doctor identifier</param>
    /// <returns>List of doctor's appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentDto>>> GetDoctorAppointmentsAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be positive")] int id)
    {
        try
        {
            var res = await crudService.GetDoctorAppointmentsAsync(id);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetDoctorAppointmentsAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetDoctorAppointmentsAsync");
            return StatusCode(500, "Failed to get doctor appointments");
        }
    }

    /// <summary>
    /// Retrieves all patients for a specific doctor asynchronously
    /// </summary>
    /// <param name="id">Doctor identifier</param>
    /// <returns>List of doctor's patients</returns>
    [HttpGet("{id}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientDto>>> GetDoctorPatientsAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be positive")] int id)
    {
        try
        {
            var res = await crudService.GetDoctorPatientsAsync(id);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetDoctorPatientsAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetDoctorPatientsAsync");
            return StatusCode(500, "Failed to get doctor patients");
        }
    }
}