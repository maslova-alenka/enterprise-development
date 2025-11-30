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
    /// Retrieves patient information for a specific appointment
    /// </summary>
    /// <param name="id">Appointment identifier</param>
    /// <returns>Patient details for the specified appointment</returns>
    [HttpGet("{id}/patient")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<PatientDto> GetAppointmentPatient(
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be positive")] int id)
    {
        try
        {
            var res = crudService.GetAppointmentPatient(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppointmentPatient");
            return StatusCode(500, "Failed to get appointment patient");
        }
    }

    /// <summary>
    /// Retrieves doctor information for a specific appointment
    /// </summary>
    /// <param name="id">Appointment identifier</param>
    /// <returns>Doctor details for the specified appointment</returns>
    [HttpGet("{id}/doctor")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<DoctorDto> GetAppointmentDoctor(
        [Range(1, int.MaxValue, ErrorMessage = "Appointment ID must be positive")] int id)
    {
        try
        {
            var res = crudService.GetAppointmentDoctor(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppointmentDoctor");
            return StatusCode(500, "Failed to get appointment doctor");
        }
    }
}