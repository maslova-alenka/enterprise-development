using AutoMapper;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Specializations;
using Polyclinic.Models;
using Polyclinic.Models.Enums;

namespace Polyclinic.Application;

public class PolyclinicProfile : Profile
{
    public PolyclinicProfile()
    {
        // Patients
        CreateMap<Patient, PatientDto>();
        CreateMap<PatientCreateUpdateDto, Patient>()
            .ForMember(destinationMember => destinationMember.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))
            .ForMember(destinationMember => destinationMember.BloodType, opt => opt.MapFrom(src => Enum.Parse<BloodType>(src.BloodType)))
            .ForMember(destinationMember => destinationMember.RhFactor, opt => opt.MapFrom(src => Enum.Parse<RhFactor>(src.RhFactor)));

        // Doctors
        CreateMap<Doctor, DoctorDto>()
            .ForMember(destinationMember => destinationMember.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name));
        CreateMap<DoctorCreateUpdateDto, Doctor>();

        // Appointments
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(destinationMember => destinationMember.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(destinationMember => destinationMember.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName));
        CreateMap<AppointmentCreateUpdateDto, Appointment>();

        // Specializations
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<SpecializationCreateUpdateDto, Specialization>();
    }
}