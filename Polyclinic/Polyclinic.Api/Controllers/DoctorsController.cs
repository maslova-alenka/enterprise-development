using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for managing doctors and their related data
/// </summary>
public class DoctorsController(IDoctorService crudService, ILogger<DoctorsController> logger)
    : CrudControllerBase<DoctorDto, DoctorCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Retrieves all appointments for a specific doctor
    /// </summary>
    /// <param name="id">Doctor identifier</param>
    /// <returns>List of doctor's appointments</returns>
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetDoctorAppointments(int id)
    {
        try
        {
            var res = crudService.GetDoctorAppointments(id);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetDoctorAppointments: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves all patients for a specific doctor
    /// </summary>
    /// <param name="id">Doctor identifier</param>
    /// <returns>List of doctor's patients</returns>
    [HttpGet("{id}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetDoctorPatients(int id)
    {
        try
        {
            var res = crudService.GetDoctorPatients(id);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetDoctorPatients: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}