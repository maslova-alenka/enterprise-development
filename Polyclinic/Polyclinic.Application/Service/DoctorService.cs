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
/// /// <param name="specializationRepository">Appointment repository</param>
/// <param name="mapper">Mapping profile</param>
public class DoctorService(IRepository<Doctor, int> doctorRepository, IRepository<Appointment, int> appointmentRepository, IMapper mapper) : IDoctorService
{
    /// <summary>
    /// Creates a new doctor
    /// </summary>
    /// <param name="dto">Data for creating the doctor</param>
    /// <returns>Created doctor</returns>
    public DoctorDto Create(DoctorCreateUpdateDto dto)
    {
        var newDoctor = mapper.Map<Doctor>(dto);
        var lastDoctor = doctorRepository.ReadAll().OrderByDescending(d => d.Id).FirstOrDefault();
        newDoctor.Id = (lastDoctor?.Id ?? 0) + 1;
        doctorRepository.Create(newDoctor);
        return mapper.Map<DoctorDto>(newDoctor);
    }

    /// <summary>
    /// Deletes a doctor by identifier
    /// </summary>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>True if deletion was successful</returns>
    public bool Delete(int dtoId)
    {
        doctorRepository.Delete(dtoId);
        return true;
    }

    /// <summary>
    /// Retrieves a doctor by identifier
    /// </summary>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>Doctor if found</returns>
    public DoctorDto? Get(int dtoId) =>
        mapper.Map<DoctorDto?>(doctorRepository.Read(dtoId));

    /// <summary>
    /// Retrieves all doctors
    /// </summary>
    /// <returns>List of all doctors</returns>
    public List<DoctorDto> GetAll() =>
        mapper.Map<List<DoctorDto>>(doctorRepository.ReadAll());

    /// <summary>
    /// Updates an existing doctor
    /// </summary>
    /// <param name="dto">Data for updating the doctor</param>
    /// <param name="dtoId">Doctor identifier</param>
    /// <returns>Updated doctor</returns>
    public DoctorDto Update(DoctorCreateUpdateDto dto, int dtoId)
    {
        var updateDoctor = mapper.Map<Doctor>(dto);
        updateDoctor.Id = dtoId;
        doctorRepository.Update(updateDoctor);
        return mapper.Map<DoctorDto>(updateDoctor);
    }

    /// <summary>
    /// Retrieves all appointments for a specific doctor
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's appointments</returns>
    public List<AppointmentDto> GetDoctorAppointments(int doctorId) =>
        mapper.Map<List<AppointmentDto>>(appointmentRepository.ReadAll().Where(a => a.Doctor.Id == doctorId).ToList());

    /// <summary>
    /// Retrieves all patients for a specific doctor
    /// </summary>
    /// <param name="doctorId">Doctor identifier</param>
    /// <returns>List of doctor's patients</returns>
    public List<PatientDto> GetDoctorPatients(int doctorId)
    {
        var patients = appointmentRepository.ReadAll()
            .Where(a => a.Doctor.Id == doctorId)
            .Select(a => a.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();
        return mapper.Map<List<PatientDto>>(patients);
    }
}