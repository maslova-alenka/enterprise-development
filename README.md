# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1JD6aiOG6r7GrA79oJncjgUHWtfeW4g_YZ9ayNgxb_w0/edit?usp=sharing)

## Задание - Поликлинника(Вариант 71)

В базе данных поликлиники содержится информация о записях пациентов на прием к врачам. 

Пациент характеризуется номером паспорта, ФИО, полом, датой рождения, адресом, группой крови, резус фактором и контактным телефоном.
Пол пациента является перечислением.
Группа крови пациента является перечислением.
Резус фактор пациента является перечислением.

Информация о враче включает номер паспорта, ФИО, год рождения, специализацию, стаж работы.
Специализация врача является справочником.

При записи на прием пациента в базе данных фиксируется дата и время приема, номер кабинета, а также индикатор того, является ли прием повторным.
Используется в качестве контракта.


### Структура проекта
```
Polyclinic/
├── Polyclinic.Models/
| ├── Enums/
│ | ├── BloodType.cs
│ | ├── Gender.cs
│ | └── RhFactor.cs
│ ├── Doctor.cs
│ ├── Patient.cs
│ ├── Appointment.cs
│ └── Specialization.cs
├── Polyclinic.Contracts/
│ ├── Doctors/
│ │ ├── DoctorDto.cs
│ │ ├── DoctorCreateUpdateDto.cs
│ │ └── IDoctorService.cs
│ ├── Patients/
│ │ ├── PatientDto.cs
│ │ ├── PatientCreateUpdateDto.cs
│ │ └── IPatientService.cs
│ ├── Appointments/
│ │ ├── AppointmentDto.cs
│ │ ├── AppointmentCreateUpdateDto.cs
│ │ └── IAppointmentService.cs
│ ├── Specializations/
│ │ ├── SpecializationDto.cs
│ │ ├── SpecializationCreateUpdateDto.cs
│ │ └── ISpecializationService.cs
│ ├── IAnalyticsService.cs
│ └── IApplicationService.cs
├── Polyclinic.Application.Service/
│ ├── DoctorService.cs
│ ├── PatientService.cs
│ ├── AppointmentService.cs
│ ├── SpecializationService.cs
│ └── AnalyticsService.cs
├── Polyclinic.Repositories/
│ ├── IRepository.cs
│ ├── DoctorInMemoryRepository.cs
│ ├── PatientInMemoryRepository.cs
│ ├── AppointmentInMemoryRepository.cs
│ └── SpecializationInMemoryRepository.cs
├── Polyclinic.Api.Controllers/
│ ├── DoctorsController.cs
│ ├── PatientsController.cs
│ ├── AppointmentsController.cs
│ ├── SpecializationsController.cs
│ ├── AnalyticsController.cs
│ └── CrudControllerBase.cs
└── Polyclinic.Test/
    └── DataSeed.cs
    └── PolyclinicTest.cs
```

### Тесты
1) Вывести информацию о всех врачах, стаж работы которых не менее 10 лет.
2) Вывести информацию о всех пациентах, записанных на прием к указанному врачу, упорядочить по ФИО.
3) Вывести информацию о количестве повторных приемов пациентов за последний месяц.
4) Вывести информацию о пациентах старше 30 лет, которые записаны на прием к нескольким врачам, упорядочить по дате рождения. 
5) Вывести информацию о приемах за текущий месяц, проходящих в выбранном кабинете.

# Лабораторная работа 2 - "Сервер"
## Описание проекта
Разработано серверное REST API приложение для автоматизации работы медицинской поликлиники с полным набором операций для управления сущностями системы и выполнения аналитических запросов.