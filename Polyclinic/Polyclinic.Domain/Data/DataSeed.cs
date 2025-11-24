using Polyclinic.Domain.Enums;
using Polyclinic.Domain.Models;

namespace Polyclinic.Domain.Data;

/// <summary>
/// Test data seed for polyclinic system
/// </summary>
public class DataSeed
{
    /// <summary>
    /// List of specializations
    /// </summary>
    public static List<Specialization> Specializations { get; } =
        [
        new Specialization{ Id = 1, Name = "Хирург"},
        new Specialization{ Id = 2, Name = "Невролог"},
        new Specialization{ Id = 3, Name = "Дерматолог"},
        new Specialization{ Id = 4, Name = "Офтальмолог"},
        new Specialization{ Id = 5, Name = "Терапевт"},
        new Specialization{ Id = 6, Name = "Педиатр"},
        new Specialization{ Id = 7, Name = "Стоматолог"},
        new Specialization{ Id = 8, Name = "Ортопед"},
        new Specialization{ Id = 9, Name = "Кардиолог"},
        new Specialization{ Id = 10, Name = "Эндокринолог"},
        ];

    /// <summary>
    /// List of doctors
    /// </summary>
    public static List<Doctor> Doctors { get; set; } =
    [
        new Doctor
        {
            Id = 1,
            PassportNumber = "1854 654123",
            FullName = "Воробьев Григорий Павлович",
            BirthYear = 1984,
            Specialization = Specializations[0],
            ExperienceYears = 10

        },

        new Doctor
        {
            Id = 2,
            PassportNumber = "5489 658745",
            FullName = "Соколова Галина Михайлова",
            BirthYear = 1967,
            Specialization = Specializations[1],
            ExperienceYears = 26

        },

        new Doctor
        {
            Id = 3,
            PassportNumber = "7598 658234",
            FullName = "Попов Николай Игоревич",
            BirthYear = 1977,
            Specialization = Specializations[3],
            ExperienceYears = 18

        },

        new Doctor
        {
            Id = 4,
            PassportNumber = "7539 951357",
            FullName = "Новиков Анатолий Юрьевич",
            BirthYear = 1989,
            Specialization = Specializations[4],
            ExperienceYears = 8

        },

        new Doctor
        {
            Id = 5,
            PassportNumber = "3984 109283",
            FullName = "Крылова Инна Александровна",
            BirthYear = 1980,
            Specialization = Specializations[6],
            ExperienceYears = 16

        },

        new Doctor
        {
            Id = 6,
            PassportNumber = "5984 398623",
            FullName = "Григорьева Анна Михайловна",
            BirthYear = 1969,
            Specialization = Specializations[6],
            ExperienceYears = 31

        },

        new Doctor
        {
            Id = 7,
            PassportNumber = "5752 757855",
            FullName = "Васнецов Сергей Андреевич",
            BirthYear = 1985,
            Specialization = Specializations[6],
            ExperienceYears = 11

        },

    ];

    /// <summary>
    /// List of patients
    /// </summary>
    public static List<Patient> Patients { get; set; } =
    [
        new Patient
        {
            Id = 1,
            PassportNumber = "1234 123456",
            FullName = "Петров Петр Петрович",
            Gender = Gender.Male,
            Birthday = new DateTime(2003, 3, 3),
            Address = "ул. Московская, д.5",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89271234567"

        },

        new Patient
        {
            Id = 2,
            PassportNumber = "4321 654321",
            FullName = "Иванова Мария Романовна",
            Gender = Gender.Female,
            Birthday = new DateTime(1980, 6, 19),
            Address = "ул. Ленина, д.147, кв 15",
            BloodType = BloodType.Ab,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89945382732"

        },

        new Patient
        {
            Id = 3,
            PassportNumber = "1234 582736",
            FullName = "Сидорова Валентина Ивановна",
            Gender = Gender.Female,
            Birthday = new DateTime(1978, 12, 4),
            Address = "пр. Мира, д. 52, кв 247",
            BloodType = BloodType.O,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89275789245"

        },

        new Patient
        {
            Id = 4,
            PassportNumber = "1324 459985",
            FullName = "Васильев Василий Васильевич",
            Gender = Gender.Male,
            Birthday = new DateTime(1999, 4, 29),
            Address = "ул. Победы, д. 23",
            BloodType = BloodType.B,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89278345655"

        },

        new Patient
        {
            Id = 5,
            PassportNumber = "1747 452796",
            FullName = "Смирнова Ольга Олеговна",
            Gender = Gender.Female,
            Birthday = new DateTime(2004, 7, 3),
            Address = "ул. Таежная, д. 12, кв. 526",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89271212121"

        },

        new Patient
        {
            Id = 6,
            PassportNumber = "6487 123456",
            FullName = "Романов Антон Александрович",
            Gender = Gender.Male,
            Birthday = new DateTime(2000, 3, 5),
            Address = "ул. Московская, д.58, кв. 1",
            BloodType = BloodType.B,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89228195847"

        },

        new Patient
        {
            Id = 7,
            PassportNumber = "5873 561728",
            FullName = "Романова Виктория Сергеевна",
            Gender = Gender.Female,
            Birthday = new DateTime(1998, 10, 4),
            Address = "пр. Гагарина, д.45, кв.3",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89279414522"

        },

        new Patient
        {
            Id = 8,
            PassportNumber = "4562 753698",
            FullName = "Чехов Александр Александрович",
            Gender = Gender.Male,
            Birthday = new DateTime(2002, 5, 5),
            Address = "ул. Студенческая, д. 18, кв. 76",
            BloodType = BloodType.Ab,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89274924685"

        },

        new Patient
        {
            Id = 9,
            PassportNumber = "5555 555555",
            FullName = "Иванов Иван Иванович",
            Gender = Gender.Male,
            Birthday = new DateTime(1985, 11, 11),
            Address = "ул. Толстова, д.55, кв. 55",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89275555555"

        },

        new Patient
        {
            Id = 10,
            PassportNumber = "4275 724586",
            FullName = "Петрова Оксана Владимировна",
            Gender = Gender.Female,
            Birthday = new DateTime(2006, 6, 6),
            Address = "ул. Авроры, д.372, кв. 185",
            BloodType = BloodType.B,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89274523776"

        },

    ];

    /// <summary>
    /// List of appointments
    /// </summary>
    public static List<Appointment> Appointments { get; set; } =
    [
        new Appointment{
            Id = 1,
            Patient = Patients[0],
            Doctor = Doctors[0],
            AppointmentDateTime = new DateTime(2025, 6, 14, 10, 0,0),
            RoomNumber = 254,
            IsFollowUp = true
        },

        new Appointment{
            Id = 2,
            Patient = Patients[1],
            Doctor = Doctors[1],
            AppointmentDateTime = new DateTime(2025, 10, 24, 11, 45,0),
            RoomNumber = 101,
            IsFollowUp = true
        },

        new Appointment{
            Id = 3,
            Patient = Patients[2],
            Doctor = Doctors[2],
            AppointmentDateTime = new DateTime(2025, 9, 6, 10, 15,0),
            RoomNumber = 112,
            IsFollowUp = false
        },

        new Appointment{
            Id = 4,
            Patient = Patients[2],
            Doctor = Doctors[3],
            AppointmentDateTime = new DateTime(2025, 2, 26, 15, 20,0),
            RoomNumber = 352,
            IsFollowUp = true
        },

        new Appointment{
            Id = 5,
            Patient = Patients[4],
            Doctor = Doctors[4],
            AppointmentDateTime = new DateTime(2025, 2, 9, 13, 30,0),
            RoomNumber = 201,
            IsFollowUp = false
        },

        new Appointment{
            Id = 6,
            Patient = Patients[1],
            Doctor = Doctors[5],
            AppointmentDateTime = new DateTime(2025, 11, 22, 13, 0,0),
            RoomNumber = 405,
            IsFollowUp = false
        },

        new Appointment{
            Id = 7,
            Patient = Patients[6],
            Doctor = Doctors[6],
            AppointmentDateTime = new DateTime(2025, 2, 27, 10, 0,0),
            RoomNumber = 201,
            IsFollowUp = true
        },

        new Appointment{
            Id = 8,
            Patient = Patients[7],
            Doctor = Doctors[6],
            AppointmentDateTime = new DateTime(2025, 5, 27, 16, 15,0),
            RoomNumber = 123,
            IsFollowUp = false
        },

        new Appointment{
            Id = 9,
            Patient = Patients[8],
            Doctor = Doctors[6],
            AppointmentDateTime = new DateTime(2025, 6, 30, 12, 30,0),
            RoomNumber = 241,
            IsFollowUp = false
        },

        new Appointment{
            Id = 10,
            Patient = Patients[9],
            Doctor = Doctors[6],
            AppointmentDateTime = new DateTime(2025, 8, 15, 14, 50,0),
            RoomNumber = 118,
            IsFollowUp = true
        },

    ];
}

