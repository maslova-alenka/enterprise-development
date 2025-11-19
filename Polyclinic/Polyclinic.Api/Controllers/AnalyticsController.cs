using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for handling analytical data and reports
/// </summary>
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : Controller
{
    /// <summary>
    /// Retrieves doctors with experience at least specified number of years
    /// </summary>
    /// <param name="minYears">Minimum years of experience required</param>
    /// <returns>List of doctors meeting the experience criteria</returns>
    [HttpGet("doctors/experienced/{minYears}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<DoctorDto>> GetDoctorsWithExperienceAtLeast(int minYears)
    {
        try
        {
            var res = service.GetDoctorsWithExperienceAtLeast(minYears);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetDoctorsWithExperienceAtLeast: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves patients for a specific doctor ordered by name
    /// </summary>
    /// <param name="doctorId">Unique identifier of the doctor</param>
    /// <returns>List of patients for the specified doctor</returns>
    [HttpGet("doctors/{doctorId}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsByDoctorOrderedByName(int doctorId)
    {
        try
        {
            var res = service.GetPatientsByDoctorOrderedByName(doctorId);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetPatientsByDoctorOrderedByName: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Gets the count of follow-up appointments for the last month relative to reference date
    /// </summary>
    /// <param name="referenceDate">Reference date for calculating last month</param>
    /// <returns>Count of follow-up appointments</returns>
    [HttpGet("follow-up-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<int> GetFollowUpAppointmentsCountLastMonth([FromQuery] DateTime referenceDate)
    {
        try
        {
            var res = service.GetFollowUpAppointmentsCountLastMonth(referenceDate);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetFollowUpAppointmentsCountLastMonth: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves patients over 30 years old who have appointments with multiple doctors
    /// </summary>
    /// <returns>List of patients meeting the criteria</returns>
    [HttpGet("patients/over-30-multiple-doctors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PatientDto>> GetPatientsOver30WithMultipleDoctors()
    {
        try
        {
            var res = service.GetPatientsOver30WithMultipleDoctors();
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetPatientsOver30WithMultipleDoctors: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Retrieves appointments for a specific room in the current month
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="year">Year for filtering appointments</param>
    /// <param name="month">Month for filtering appointments</param>
    /// <returns>List of appointments for the specified room and period</returns>
    [HttpGet("appointments/room/{roomNumber}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<AppointmentDto>> GetAppointmentsByRoomForCurrentMonth(
        int roomNumber, [FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            var res = service.GetAppointmentsByRoomForCurrentMonth(roomNumber, year, month);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("Error in GetAppointmentsByRoomForCurrentMonth: {Message}", ex.Message);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}