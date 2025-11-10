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
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender)))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => Enum.Parse<BloodType>(src.BloodType)))
            .ForMember(dest => dest.RhFactor, opt => opt.MapFrom(src => Enum.Parse<RhFactor>(src.RhFactor)));

        // Doctors
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name));
        CreateMap<DoctorCreateUpdateDto, Doctor>();

        // Appointments
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName));
        CreateMap<AppointmentCreateUpdateDto, Appointment>();

        // Specializations
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<SpecializationCreateUpdateDto, Specialization>();
    }
}