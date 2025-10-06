using Polyclinic.PolyclinicModels;
using System.Runtime.CompilerServices;

namespace Polyclinic.Test;
public class DataSeed
{
    public List<Doctor> Doctors { get; set; } = 
    [
        new Doctor 
        {
            ID = 1,
            PassportNumber = "1854 654123",
            FullName = "Воробьев Григорий Павлович",
            BirthYear = 1984,
            Specialization = "Хирург",
            ExperienceYears = 10

        },

        new Doctor
        {
            ID = 2,
            PassportNumber = "5489 658745",
            FullName = "Соколова Галина Михайлова",
            BirthYear = 1967,
            Specialization = "Невролог",
            ExperienceYears = 26

        },

        new Doctor
        {
            ID = 3,
            PassportNumber = "7598 658234",
            FullName = "Попов Николай Игоревич",
            BirthYear = 1977,
            Specialization = "Дерматолог",
            ExperienceYears = 18

        },

        new Doctor
        {
            ID = 4,
            PassportNumber = "7539 951357",
            FullName = "Новиков Анатолий Юрьевич",
            BirthYear = 1989,
            Specialization = "Офтальмолог",
            ExperienceYears = 8

        },

        new Doctor
        {
            ID = 5,
            PassportNumber = "3984 109283",
            FullName = "Крылова Инна Александровна",
            BirthYear = 1980,
            Specialization = "Хирург",
            ExperienceYears = 16

        },

        new Doctor
        {
            ID = 6,
            PassportNumber = "5984 398623",
            FullName = "Григорьева Анна Михайловна",
            BirthYear = 1969,
            Specialization = "Терапевт",
            ExperienceYears = 31

        },

        new Doctor
        {
            ID = 7,
            PassportNumber = "5752 757855",
            FullName = "Васнецов Сергей Андреевич",
            BirthYear = 1985,
            Specialization = "Педиатр",
            ExperienceYears = 11

        },

        new Doctor
        {
            ID = 8,
            PassportNumber = "4147 775533",
            FullName = "Лебедев Дмитрий Евгеньевич",
            BirthYear = 1979,
            Specialization = "Эндокринолог",
            ExperienceYears = 17

        },

        new Doctor
        {
            ID = 9,
            PassportNumber = "3541 542145",
            FullName = "Кузнецова Дарья Владимировна",
            BirthYear = 1986,
            Specialization = "Терапевт",
            ExperienceYears = 12

        },

        new Doctor
        {
            ID = 10,
            PassportNumber = "7544 445511",
            FullName = "Иващенко Надежда Александровна",
            BirthYear = 1995,
            Specialization = "Офтальмолог",
            ExperienceYears = 3

        },
    ];
    public List<Patient> Patients { get; set; } =
    [
        new Patient
        {
            ID = 1,
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
            ID = 2,
            PassportNumber = "4321 654321",
            FullName = "Иванова Мария Романовна",
            Gender = Gender.Female,
            Birthday = new DateTime(2000, 6, 19),
            Address = "ул. Ленина, д.147, кв 15",
            BloodType = BloodType.AB,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "89945382732"

        },

        new Patient
        {
            ID = 3,
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
            ID = 4,
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
            ID = 5,
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
            ID = 6,
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
            ID = 7,
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
            ID = 8,
            PassportNumber = "4562 753698",
            FullName = "Чехов Александр Александрович",
            Gender = Gender.Male,
            Birthday = new DateTime(2002, 5, 5),
            Address = "ул. Студенческая, д. 18, кв. 76",
            BloodType = BloodType.AB,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "89274924685"

        },

        new Patient
        {
            ID = 9,
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
            ID = 10,
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

    public int ID { get; set; }
    public string PatientPassport { get; set; }
    public string DoctorPassport { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int RoomNumber { get; set; }
    public bool IsFollowUp { get; set; }
    public List<Appointment> Appointments { get; set; } =
        [
            new Appointment{
                ID = 1,
                PatientId = 1,
                DoctorId = 1,
                AppointmentDateTime = new DateTime(2025, 6, 14, 10, 0,0),
                RoomNumber = 254,
                IsFollowUp = true   
            },

            new Appointment{
                ID = 2,
                PatientId = 2,
                DoctorId = 2,
                AppointmentDateTime = new DateTime(2025, 10, 24, 11, 45,0),
                RoomNumber = 101,
                IsFollowUp = true
            },

            new Appointment{
                ID = 3,
                PatientId = 3,
                DoctorId = 3,
                AppointmentDateTime = new DateTime(2025, 9, 6, 10, 15,0),
                RoomNumber = 112,
                IsFollowUp = false
            },

            new Appointment{
                ID = 4,
                PatientId = 4,
                DoctorId = 4,
                AppointmentDateTime = new DateTime(2025, 5, 29, 15, 20,0),
                RoomNumber = 352,
                IsFollowUp = true
            },

            new Appointment{
                ID = 5,
                PatientId = 5,
                DoctorId = 5,
                AppointmentDateTime = new DateTime(2025, 7, 9, 13, 30,0),
                RoomNumber = 201,
                IsFollowUp = false
            },

            new Appointment{
                ID = 6,
                PatientId = 6,
                DoctorId = 6,
                AppointmentDateTime = new DateTime(2025, 11, 22, 13, 0,0),
                RoomNumber = 405,
                IsFollowUp = false
            },

            new Appointment{
                ID = 7,
                PatientId = 7,
                DoctorId = 7,
                AppointmentDateTime = new DateTime(2025, 2, 28, 10, 0,0),
                RoomNumber = 222,
                IsFollowUp = true
            },

            new Appointment{
                ID = 8,
                PatientId = 8,
                DoctorId = 8,
                AppointmentDateTime = new DateTime(2025, 5, 27, 16, 15,0),
                RoomNumber = 123,
                IsFollowUp = false
            },

            new Appointment{
                ID = 9,
                PatientId = 9,
                DoctorId = 9,
                AppointmentDateTime = new DateTime(2025, 6, 30, 12, 30,0),
                RoomNumber = 241,
                IsFollowUp = false
            },

            new Appointment{
                ID = 10,
                PatientId = 10,
                DoctorId = 10,
                AppointmentDateTime = new DateTime(2025, 8, 15, 14, 50,0),
                RoomNumber = 118,
                IsFollowUp = true
            },

        ];
}
