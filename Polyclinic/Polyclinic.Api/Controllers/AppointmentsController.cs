using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for managing medical appointments
/// </summary>
public class AppointmentsController(IAppointmentService crudService, ILogger<AppointmentsController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Retrieves patient information for a specific appointment asynchronously
    /// </summary>
    /// <param name="id">Appointment identifier</param>
    /// <returns>Patient details for the specified appointment</returns>
    [HttpGet("{id}/patient")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PatientDto>> GetAppointmentPatientAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be positive")] int id)
    {
        try
        {
            var res = await crudService.GetAppointmentPatientAsync(id);
            return res != null ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetAppointmentPatientAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppointmentPatientAsync");
            return StatusCode(500, "Failed to get appointment patient");
        }
    }

    /// <summary>
    /// Retrieves doctor information for a specific appointment asynchronously
    /// </summary>
    /// <param name="id">Appointment identifier</param>
    /// <returns>Doctor details for the specified appointment</returns>
    [HttpGet("{id}/doctor")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DoctorDto>> GetAppointmentDoctorAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be positive")] int id)
    {
        try
        {
            var res = await crudService.GetAppointmentDoctorAsync(id);
            return res != null ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetAppointmentDoctorAsync");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppointmentDoctorAsync");
            return StatusCode(500, "Failed to get appointment doctor");
        }
    }
}