using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

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
    [ProducesResponseType(500)]
    public ActionResult<PatientDto> GetAppointmentPatient(int id)
    {
        try
        {
            var res = crudService.GetAppointmentPatient(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetAppointmentPatient: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
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
    [ProducesResponseType(500)]
    public ActionResult<DoctorDto> GetAppointmentDoctor(int id)
    {
        try
        {
            var res = crudService.GetAppointmentDoctor(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetAppointmentDoctor: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}