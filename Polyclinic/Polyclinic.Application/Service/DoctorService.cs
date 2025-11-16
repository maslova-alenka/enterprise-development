using AutoMapper;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Patients;
using Polyclinic.Models;
using Polyclinic.Repositories;

namespace Polyclinic.Application.Service;

public class DoctorService(IRepository<Doctor, int> doctorRepository, IRepository<Appointment, int> appointmentRepository, IMapper mapper) : IDoctorService
{
    /// <inheritdoc/>
    public DoctorDto Create(DoctorCreateUpdateDto dto)
    {
        var newDoctor = mapper.Map<Doctor>(dto);
        var lastDoctor = doctorRepository.ReadAll().OrderByDescending(d => d.Id).FirstOrDefault();
        newDoctor.Id = (lastDoctor?.Id ?? 0) + 1;
        doctorRepository.Create(newDoctor);
        return mapper.Map<DoctorDto>(newDoctor);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId)
    {
        doctorRepository.Delete(dtoId);
        return true;
    }

    /// <inheritdoc/>
    public DoctorDto? Get(int dtoId) =>
        mapper.Map<DoctorDto?>(doctorRepository.Read(dtoId));

    /// <inheritdoc/>
    public List<DoctorDto> GetAll() =>
        mapper.Map<List<DoctorDto>>(doctorRepository.ReadAll());

    /// <inheritdoc/>
    public DoctorDto Update(DoctorCreateUpdateDto dto, int dtoId)
    {
        var updateDoctor = mapper.Map<Doctor>(dto);
        updateDoctor.Id = dtoId;
        doctorRepository.Update(updateDoctor);
        return mapper.Map<DoctorDto>(updateDoctor);
    }

    /// <inheritdoc/>
    public List<AppointmentDto> GetDoctorAppointments(int doctorId) =>
        mapper.Map<List<AppointmentDto>>(appointmentRepository.ReadAll().Where(a => a.Doctor.Id == doctorId).ToList());

    /// <inheritdoc/>
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