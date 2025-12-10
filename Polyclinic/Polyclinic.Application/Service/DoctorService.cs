using AutoMapper;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Interfaces;

namespace Polyclinic.Application.Service;

/// <summary>
/// Service for managing doctors
/// </summary>
/// <param name="doctorRepository">Doctor repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="specializationRepository">Specialization repository</param>
/// <param name="mapper">Mapping profile</param>
public class DoctorService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Specialization, int> specializationRepository,
    IMapper mapper) : IDoctorService
{
    /// <summary>
    /// Creates a new doctor with specified specialization asynchronously
    /// </summary>
    /// <param name="dto">Data for creating the doctor including specialization ID</param>
    /// <returns>Created doctor with specialization details</returns>
    /// <exception cref="ArgumentException">Thrown when specialization with specified ID is not found</exception>
    public async Task<DoctorDto> CreateAsync(DoctorCreateUpdateDto dto)
    {
        var specialization = await specializationRepository.ReadAsync(dto.SpecializationId)
            ?? throw new ArgumentException($"Specialization with ID {dto.SpecializationId} not found");

        var newDoctor = mapper.Map<Doctor>(dto);
        newDoctor.Specialization = specialization;

        var allDoctors = await doctorRepository.ReadAllAsync();
        var lastDoctorId = allDoctors.Max(d => d.Id);
        newDoctor.Id = lastDoctorId + 1;

        await doctorRepository.CreateAsync(newDoctor);
        return mapper.Map<DoctorDto>(newDoctor);
    }

    /// <summary>
    /// Deletes a doctor by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>True if deletion was successful</returns>
    public async Task<bool> DeleteAsync(int dtoId)
    {
        await doctorRepository.DeleteAsync(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a doctor by identifier asynchronously
    /// </summary>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>Doctor if found</returns>
    public async Task<DoctorDto?> GetAsync(int dtoId)
    {
        var doctor = await doctorRepository.ReadAsync(dtoId);
        return mapper.Map<DoctorDto?>(doctor);
    }

    /// <summary>
    /// Retrieves all doctors asynchronously
    /// </summary>
    /// <returns>List of all doctors</returns>
    public async Task<List<DoctorDto>> GetAllAsync()
    {
        var doctors = await doctorRepository.ReadAllAsync();
        return mapper.Map<List<DoctorDto>>(doctors);
    }

    /// <summary>
    /// Updates an existing doctor with new data including specialization asynchronously
    /// </summary>
    /// <param name="dto">Data for updating the doctor including specialization ID</param>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>Updated doctor with specialization details</returns>
    /// <exception cref="ArgumentException">Thrown when doctor or specialization with specified ID is not found</exception>
    public async Task<DoctorDto> UpdateAsync(DoctorCreateUpdateDto dto, int dtoId)
    {
        var specialization = await specializationRepository.ReadAsync(dto.SpecializationId)
            ?? throw new ArgumentException($"Specialization with ID {dto.SpecializationId} not found");

        var existingDoctor = await doctorRepository.ReadAsync(dtoId)
            ?? throw new ArgumentException($"Doctor with ID {dtoId} not found");

        existingDoctor.PassportNumber = dto.PassportNumber;
        existingDoctor.FullName = dto.FullName;
        existingDoctor.BirthYear = dto.BirthYear;
        existingDoctor.Specialization = specialization;
        existingDoctor.ExperienceYears = dto.ExperienceYears;

        await doctorRepository.UpdateAsync(existingDoctor);
        return mapper.Map<DoctorDto>(existingDoctor);
    }

    /// <summary>
    /// Retrieves all appointments for a specific doctor asynchronously
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's appointments</returns>
    public async Task<List<AppointmentDto>> GetDoctorAppointmentsAsync(int doctorId)
    {
        var appointments = await appointmentRepository.ReadAllAsync();
        var doctorAppointments = appointments
            .Where(a => a.Doctor.Id == doctorId)
            .ToList();

        return mapper.Map<List<AppointmentDto>>(doctorAppointments);
    }

    /// <summary>
    /// Retrieves all patients for a specific doctor asynchronously
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's patients</returns>
    public async Task<List<PatientDto>> GetDoctorPatientsAsync(int doctorId)
    {
        var appointments = await appointmentRepository.ReadAllAsync();
        var patients = appointments
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();

        return mapper.Map<List<PatientDto>>(patients);
    }
}