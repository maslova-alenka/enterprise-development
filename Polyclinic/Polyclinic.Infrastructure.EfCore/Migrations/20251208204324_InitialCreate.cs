using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Polyclinic.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Patients",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                PassportNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Gender = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BloodType = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                RhFactor = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Patients", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specializations", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Doctors",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                PassportNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BirthYear = table.Column<int>(type: "int", nullable: false),
                SpecializationId = table.Column<int>(type: "int", nullable: false),
                ExperienceYears = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctors", x => x.Id);
                table.ForeignKey(
                    name: "FK_Doctors_Specializations_SpecializationId",
                    column: x => x.SpecializationId,
                    principalTable: "Specializations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Appointments",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                PatientId = table.Column<int>(type: "int", nullable: false),
                DoctorId = table.Column<int>(type: "int", nullable: false),
                AppointmentDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                RoomNumber = table.Column<int>(type: "int", nullable: false),
                IsFollowUp = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointments_Doctors_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Appointments_Patients_PatientId",
                    column: x => x.PatientId,
                    principalTable: "Patients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "Patients",
            columns: new[] { "Id", "Address", "Birthday", "BloodType", "FullName", "Gender", "PassportNumber", "PhoneNumber", "RhFactor" },
            values: new object[,]
            {
                { 1, "ул. Московская, д.5", new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "Петров Петр Петрович", "Male", "1234 123456", "89271234567", "Positive" },
                { 2, "ул. Ленина, д.147, кв 15", new DateTime(1980, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ab", "Иванова Мария Романовна", "Female", "4321 654321", "89945382732", "Positive" },
                { 3, "пр. Мира, д. 52, кв 247", new DateTime(1978, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "O", "Сидорова Валентина Ивановна", "Female", "1234 582736", "89275789245", "Negative" },
                { 4, "ул. Победы, д. 23", new DateTime(1999, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "Васильев Василий Васильевич", "Male", "1324 459985", "89278345655", "Positive" },
                { 5, "ул. Таежная, д. 12, кв. 526", new DateTime(2004, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "Смирнова Ольга Олеговна", "Female", "1747 452796", "89271212121", "Negative" },
                { 6, "ул. Московская, д.58, кв. 1", new DateTime(2000, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "Романов Антон Александрович", "Male", "6487 123456", "89228195847", "Negative" },
                { 7, "пр. Гагарина, д.45, кв.3", new DateTime(1998, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "Романова Виктория Сергеевна", "Female", "5873 561728", "89279414522", "Positive" },
                { 8, "ул. Студенческая, д. 18, кв. 76", new DateTime(2002, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ab", "Чехов Александр Александрович", "Male", "4562 753698", "89274924685", "Negative" },
                { 9, "ул. Толстова, д.55, кв. 55", new DateTime(1985, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "Иванов Иван Иванович", "Male", "5555 555555", "89275555555", "Positive" },
                { 10, "ул. Авроры, д.372, кв. 185", new DateTime(2006, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "Петрова Оксана Владимировна", "Female", "4275 724586", "89274523776", "Negative" }
            });

        migrationBuilder.InsertData(
            table: "Specializations",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Хирург" },
                { 2, "Невролог" },
                { 3, "Дерматолог" },
                { 4, "Офтальмолог" },
                { 5, "Терапевт" },
                { 6, "Педиатр" },
                { 7, "Стоматолог" },
                { 8, "Ортопед" },
                { 9, "Кардиолог" },
                { 10, "Эндокринолог" }
            });

        migrationBuilder.InsertData(
            table: "Doctors",
            columns: new[] { "Id", "BirthYear", "ExperienceYears", "FullName", "PassportNumber", "SpecializationId" },
            values: new object[,]
            {
                { 1, 1984, 10, "Воробьев Григорий Павлович", "1854 654123", 1 },
                { 2, 1967, 26, "Соколова Галина Михайлова", "5489 658745", 2 },
                { 3, 1977, 18, "Попов Николай Игоревич", "7598 658234", 4 },
                { 4, 1989, 8, "Новиков Анатолий Юрьевич", "7539 951357", 5 },
                { 5, 1980, 16, "Крылова Инна Александровна", "3984 109283", 7 },
                { 6, 1969, 31, "Григорьева Анна Михайловна", "5984 398623", 7 },
                { 7, 1985, 11, "Васнецов Сергей Андреевич", "5752 757855", 7 },
                { 8, 1987, 8, "Орехова Антонина Григорьева", "7777 777777", 10 },
                { 9, 1969, 18, "Малинин Роберт Иванович", "9999 999999", 5 },
                { 10, 1980, 15, "Пашкова Мария Романовна", "1010 101010", 4 }
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "IsFollowUp", "PatientId", "RoomNumber" },
            values: new object[,]
            {
                { 1, new DateTime(2025, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1, 254 },
                { 2, new DateTime(2025, 10, 24, 11, 45, 0, 0, DateTimeKind.Unspecified), 2, true, 2, 101 }
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "PatientId", "RoomNumber" },
            values: new object[] { 3, new DateTime(2025, 9, 6, 10, 15, 0, 0, DateTimeKind.Unspecified), 3, 3, 112 });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "IsFollowUp", "PatientId", "RoomNumber" },
            values: new object[] { 4, new DateTime(2025, 2, 26, 15, 20, 0, 0, DateTimeKind.Unspecified), 4, true, 3, 352 });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "PatientId", "RoomNumber" },
            values: new object[,]
            {
                { 5, new DateTime(2025, 2, 9, 13, 30, 0, 0, DateTimeKind.Unspecified), 5, 5, 201 },
                { 6, new DateTime(2025, 11, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 405 }
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "IsFollowUp", "PatientId", "RoomNumber" },
            values: new object[] { 7, new DateTime(2025, 2, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), 7, true, 7, 201 });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "PatientId", "RoomNumber" },
            values: new object[,]
            {
                { 8, new DateTime(2025, 5, 27, 16, 15, 0, 0, DateTimeKind.Unspecified), 7, 8, 123 },
                { 9, new DateTime(2025, 6, 30, 12, 30, 0, 0, DateTimeKind.Unspecified), 7, 9, 241 }
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime", "DoctorId", "IsFollowUp", "PatientId", "RoomNumber" },
            values: new object[] { 10, new DateTime(2025, 8, 15, 14, 50, 0, 0, DateTimeKind.Unspecified), 7, true, 10, 118 });

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_AppointmentDateTime",
            table: "Appointments",
            column: "AppointmentDateTime");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_DoctorId",
            table: "Appointments",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_Id_AppointmentDateTime",
            table: "Appointments",
            columns: new[] { "Id", "AppointmentDateTime" });

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_PatientId",
            table: "Appointments",
            column: "PatientId");

        migrationBuilder.CreateIndex(
            name: "IX_Doctors_PassportNumber",
            table: "Doctors",
            column: "PassportNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Doctors_SpecializationId",
            table: "Doctors",
            column: "SpecializationId");

        migrationBuilder.CreateIndex(
            name: "IX_Patients_PassportNumber",
            table: "Patients",
            column: "PassportNumber",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Appointments");

        migrationBuilder.DropTable(
            name: "Doctors");

        migrationBuilder.DropTable(
            name: "Patients");

        migrationBuilder.DropTable(
            name: "Specializations");
    }
}
