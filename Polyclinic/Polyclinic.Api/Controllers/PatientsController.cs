using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;


public class PatientsController(IPatientService crudService, ILogger<PatientsController> logger)
    : CrudControllerBase<PatientDto, PatientCreateUpdateDto, int>(crudService, logger)
{
  
    [HttpGet("{id}/appointments")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<AppointmentDto>>> GetPatientAppointments(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetPatientAppointments), GetType().Name, id);
        try
        {
            var res = await crudService.GetPatientAppointments(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientAppointments), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientAppointments), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}