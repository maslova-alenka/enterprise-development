using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Specialization { Id = 1, Name = "Хирург" },
                new Specialization { Id = 2, Name = "Невролог" },
                new Specialization { Id = 3, Name = "Дерматолог" },
                new Specialization { Id = 4, Name = "Офтальмолог" },
                new Specialization { Id = 5, Name = "Терапевт" },
                new Specialization { Id = 6, Name = "Педиатр" },
                new Specialization { Id = 7, Name = "Стоматолог" },
                new Specialization { Id = 8, Name = "Ортопед" },
                new Specialization { Id = 9, Name = "Кардиолог" },
                new Specialization { Id = 10, Name = "Эндокринолог" }
            );
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
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);
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

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasConversion<string>(); 

            builder.Property(p => p.Birthday)
                .IsRequired();

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.BloodType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.RhFactor)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
        });


        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.AppointmentDateTime)
                .IsRequired();

            builder.Property(a => a.RoomNumber)
                .IsRequired();

            builder.Property(a => a.IsFollowUp)
                .IsRequired()
                .HasDefaultValue(false);
        });
    }
}