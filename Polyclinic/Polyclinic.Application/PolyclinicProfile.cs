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
        ////CreateMap<DoctorCreateUpdateDto, Doctor>()
        ////    .ForMember(destinationMember => destinationMember.Specialization, opt => opt.MapFrom(src => src.Specialization.Id));
        //CreateMap<Doctor, DoctorDto>()
        //    .ConstructUsing(src => new DoctorDto(
        //        src.Id,
        //        src.PassportNumber,
        //        src.FullName,
        //        src.BirthYear,
        //        src.Specialization.Id,          
        //        src.Specialization.Name,        
        //        src.ExperienceYears
        //    ));

        // Appointments
        //CreateMap<Appointment, AppointmentDto>()
        //    .ForMember(destinationMember => destinationMember.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
        //    .ForMember(destinationMember => destinationMember.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName));
        //CreateMap<AppointmentCreateUpdateDto, Appointment>();

        CreateMap<Appointment, AppointmentDto>()
            .ConstructUsing(src => new AppointmentDto(
                src.Id,
                src.Patient.Id,
                src.Patient.FullName,
                src.Doctor.Id,
                src.Doctor.FullName,
                src.AppointmentDateTime,
                src.RoomNumber,
                src.IsFollowUp
                ));

        // Specializations
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<SpecializationCreateUpdateDto, Specialization>();
    }
}