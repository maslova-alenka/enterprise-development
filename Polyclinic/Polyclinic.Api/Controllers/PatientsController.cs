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
    /// Retrieves all appointments for a specific patient
    /// </summary>
    /// <param name="id">Patient identifier</param>
    /// <returns>List of patient's appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetPatientAppointments(
        [Range(1, int.MaxValue, ErrorMessage = "Patient ID must be positive")] int id)
    {
        try
        {
            var res = crudService.GetPatientAppointments(id);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetPatientAppointments");
            return StatusCode(500, "Failed to get patient appointments");
        }
    }
}