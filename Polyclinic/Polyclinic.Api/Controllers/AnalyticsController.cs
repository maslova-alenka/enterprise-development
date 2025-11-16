using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;

public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : Controller
{
    [HttpGet("doctors/experienced/{minYears}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<DoctorDto>> GetDoctorsWithExperienceAtLeast(int minYears)
    {
        logger.LogInformation("{method} method of {controller} is called with {minYears} parameter", nameof(GetDoctorsWithExperienceAtLeast), GetType().Name, minYears);
        try
        {
            var res = service.GetDoctorsWithExperienceAtLeast(minYears);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetDoctorsWithExperienceAtLeast), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetDoctorsWithExperienceAtLeast), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("doctors/{doctorId}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsByDoctorOrderedByName(int doctorId)
    {
        logger.LogInformation("{method} method of {controller} is called with {doctorId} parameter", nameof(GetPatientsByDoctorOrderedByName), GetType().Name, doctorId);
        try
        {
            var res = service.GetPatientsByDoctorOrderedByName(doctorId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientsByDoctorOrderedByName), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientsByDoctorOrderedByName), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("follow-up-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<int> GetFollowUpAppointmentsCountLastMonth([FromQuery] DateTime referenceDate)
    {
        logger.LogInformation("{method} method of {controller} is called with {referenceDate} parameter", nameof(GetFollowUpAppointmentsCountLastMonth), GetType().Name, referenceDate);
        try
        {
            var res = service.GetFollowUpAppointmentsCountLastMonth(referenceDate);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetFollowUpAppointmentsCountLastMonth), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetFollowUpAppointmentsCountLastMonth), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("patients/over-30-multiple-doctors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsOver30WithMultipleDoctors()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetPatientsOver30WithMultipleDoctors), GetType().Name);
        try
        {
            var res = service.GetPatientsOver30WithMultipleDoctors();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPatientsOver30WithMultipleDoctors), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPatientsOver30WithMultipleDoctors), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("appointments/room/{roomNumber}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetAppointmentsByRoomForCurrentMonth(
        int roomNumber, [FromQuery] int year, [FromQuery] int month)
    {
        logger.LogInformation("{method} method of {controller} is called with roomNumber={roomNumber}, year={year}, month={month}",
            nameof(GetAppointmentsByRoomForCurrentMonth), GetType().Name, roomNumber, year, month);
        try
        {
            var res = service.GetAppointmentsByRoomForCurrentMonth(roomNumber, year, month);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAppointmentsByRoomForCurrentMonth), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAppointmentsByRoomForCurrentMonth), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}