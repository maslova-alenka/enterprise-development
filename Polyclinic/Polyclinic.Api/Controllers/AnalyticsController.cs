using Microsoft.AspNetCore.Mvc;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Api.Controllers;

/// <summary>
/// Controller for handling analytical data and reports
/// </summary>
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves doctors with experience at least specified number of years asynchronously
    /// </summary>
    /// <param name="minYears">Minimum years of experience required</param>
    /// <returns>List of doctors meeting the experience criteria</returns>
    [HttpGet("doctors/experienced/{minYears}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<DoctorDto>>> GetDoctorsWithExperienceAtLeastAsync(
        [Range(0, 70, ErrorMessage = "Experience years must be between 0 and 70")] int minYears)
    {
        try
        {
            var res = await service.GetDoctorsWithExperienceAtLeastAsync(minYears);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetDoctorsWithExperienceAtLeastAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetDoctorsWithExperienceAtLeastAsync");
            return StatusCode(500, "Failed to get doctors list");
        }
    }

    /// <summary>
    /// Retrieves patients for a specific doctor ordered by name asynchronously
    /// </summary>
    /// <param name="doctorId">Unique identifier of the doctor</param>
    /// <returns>List of patients for the specified doctor</returns>
    [HttpGet("doctors/{doctorId}/patients")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientDto>>> GetPatientsByDoctorOrderedByNameAsync(
        [Range(1, int.MaxValue, ErrorMessage = "Doctor ID must be positive")] int doctorId)
    {
        try
        {
            var res = await service.GetPatientsByDoctorOrderedByNameAsync(doctorId);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetPatientsByDoctorOrderedByNameAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetPatientsByDoctorOrderedByNameAsync");
            return StatusCode(500, "Failed to get patients list");
        }
    }

    /// <summary>
    /// Gets the count of follow-up appointments for the last month relative to reference date asynchronously
    /// </summary>
    /// <param name="referenceDate">Reference date for calculating last month</param>
    /// <returns>Count of follow-up appointments</returns>
    [HttpGet("follow-up-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<int>> GetFollowUpAppointmentsCountLastMonthAsync([FromQuery] DateTime referenceDate)
    {
        try
        {
            var res = await service.GetFollowUpAppointmentsCountLastMonthAsync(referenceDate);
            return Ok(res);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetFollowUpAppointmentsCountLastMonthAsync");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetFollowUpAppointmentsCountLastMonthAsync");
            return StatusCode(500, "Failed to get follow-up appointments count");
        }
    }

    /// <summary>
    /// Retrieves patients over 30 years old who have appointments with multiple doctors asynchronously
    /// </summary>
    /// <returns>List of patients meeting the criteria</returns>
    [HttpGet("patients/over-30-multiple-doctors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PatientDto>>> GetPatientsOver30WithMultipleDoctorsAsync()
    {
        try
        {
            var res = await service.GetPatientsOver30WithMultipleDoctorsAsync();
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetPatientsOver30WithMultipleDoctorsAsync");
            return StatusCode(500, "Failed to get patients list");
        }
    }

    /// <summary>
    /// Retrieves appointments for a specific room in the current month asynchronously
    /// </summary>
    /// <param name="roomNumber">Room number to filter appointments</param>
    /// <param name="year">Year for filtering appointments</param>
    /// <param name="month">Month for filtering appointments</param>
    /// <returns>List of appointments for the specified room and period</returns>
    [HttpGet("appointments/room/{roomNumber}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<AppointmentDto>>> GetAppointmentsByRoomForCurrentMonthAsync(
        [Range(1, 1000, ErrorMessage = "Room number must be between 1 and 1000")] int roomNumber,
        [FromQuery] int year,
        [FromQuery] int month)
    {
        try
        {
            var res = await service.GetAppointmentsByRoomForCurrentMonthAsync(roomNumber, year, month);
            return res.Count > 0 ? Ok(res) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error in GetAppointmentsByRoomForCurrentMonthAsync");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetAppointmentsByRoomForCurrentMonthAsync");
            return StatusCode(500, "Failed to get appointments list");
        }
    }
}