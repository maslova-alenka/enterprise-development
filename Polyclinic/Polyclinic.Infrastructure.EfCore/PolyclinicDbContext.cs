using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Data;
using Polyclinic.Domain.Enums;
using Polyclinic.Domain.Models;

namespace Polyclinic.Infrastructure.EfCore;

public class PolyclinicDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .Property(p => p.Gender)
            .HasConversion<string>();

        modelBuilder.Entity<Patient>()
            .Property(p => p.BloodType)
            .HasConversion<string>();

        modelBuilder.Entity<Patient>()
            .Property(p => p.RhFactor)
            .HasConversion<string>();

        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(DataSeed.Specializations);
        });

        modelBuilder.Entity<Doctor>(builder =>
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.BirthYear)
                .IsRequired();

            builder.Property(d => d.ExperienceYears)
                .IsRequired();

            builder.HasOne(d => d.Specialization)
                .WithMany()
                .IsRequired();

            builder.HasIndex(d => d.PassportNumber)
                .IsUnique();

            builder.HasData(DataSeed.Doctors.Select(d => new
            {
                d.Id,
                d.PassportNumber,
                d.FullName,
                d.BirthYear,
                d.ExperienceYears,
                SpecializationId = d.Specialization?.Id ?? 1
            }));
        });

        modelBuilder.Entity<Patient>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Birthday)
                .IsRequired();

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasIndex(p => p.PassportNumber)
                .IsUnique();

            builder.HasData(DataSeed.Patients.Select(p => new
            {
                p.Id,
                p.PassportNumber,
                p.FullName,
                p.Gender,           
                p.Birthday,
                p.Address,
                p.BloodType,       
                p.RhFactor,        
                p.PhoneNumber
            }));
        });

        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Patient)
                .WithMany()
                .IsRequired();

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .IsRequired();

            builder.Property(a => a.AppointmentDateTime)
                .IsRequired();

            builder.Property(a => a.RoomNumber)
                .IsRequired();

            builder.Property(a => a.IsFollowUp)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasIndex(a => a.AppointmentDateTime);
            builder.HasIndex(a => new { a.Id, a.AppointmentDateTime });

        builder.HasData(DataSeed.Appointments.Select(a => new
            {
                a.Id,
                PatientId = a.Patient?.Id ?? 1,
                DoctorId = a.Doctor?.Id ?? 1,
                a.AppointmentDateTime,
                a.RoomNumber,
                a.IsFollowUp
            }));
        });
    }
}