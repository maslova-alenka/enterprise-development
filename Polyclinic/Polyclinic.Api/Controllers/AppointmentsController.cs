using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;


public class AppointmentsController(IAppointmentService crudService, ILogger<AppointmentsController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentCreateUpdateDto, int>(crudService, logger)
{

    [HttpGet("{id}/patient")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PatientDto>> GetAppointmentPatient(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAppointmentPatient), GetType().Name, id);
        try
        {
            var res = await crudService.GetAppointmentPatient(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentPatient), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentPatient), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{id}/doctor")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DoctorDto>> GetAppointmentDoctor(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAppointmentDoctor), GetType().Name, id);
        try
        {
            var res = await crudService.GetAppointmentDoctor(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentDoctor), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentDoctor), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}