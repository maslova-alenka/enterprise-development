using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;

public class DoctorsController(IDoctorService crudService, ILogger<DoctorsController> logger)
    : CrudControllerBase<DoctorDto, DoctorCreateUpdateDto, int>(crudService, logger)
{
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetDoctorAppointments(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetDoctorAppointments), GetType().Name, id);
        try
        {
            var res = crudService.GetDoctorAppointments(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetDoctorAppointments), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetDoctorAppointments), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{id}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetDoctorPatients(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetDoctorPatients), GetType().Name, id);
        try
        {
            var res = crudService.GetDoctorPatients(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetDoctorPatients), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetDoctorPatients), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}