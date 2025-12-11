using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for managing patients and their related data
/// </summary>
public class PatientsController(IPatientService crudService, ILogger<PatientsController> logger)
    : CrudControllerBase<PatientDto, PatientCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Retrieves all appointments for a specific patient asynchronously
    /// </summary>
    /// <param name="id">Patient identifier</param>
    /// <returns>List of patient's appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentDto>>> GetPatientAppointmentsAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be positive")] int id)
    {
        try
        {
            var res = await crudService.GetPatientAppointmentsAsync(id);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetPatientAppointmentsAsync");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetPatientAppointmentsAsync");
            return StatusCode(500, "Failed to get patient appointments");
        }
    }
}