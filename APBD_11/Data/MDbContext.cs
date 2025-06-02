using APBD_11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Data;

public class MDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected MDbContext()
    {
    }

    public MDbContext(DbContextOptions<MDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Doctor>().HasData(
        new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "j.doe@example.com" },
        new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Smith", Email = "anna.smith@example.com" }
    );

    modelBuilder.Entity<Patient>().HasData(
        new Patient { IdPatient = 1, FirstName = "Michael", LastName = "Brown", Birthdate = new DateTime(1980, 5, 15) },
        new Patient { IdPatient = 2, FirstName = "Emily", LastName = "Clark", Birthdate = new DateTime(1992, 11, 2) }
    );

    modelBuilder.Entity<Medicament>().HasData(
        new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Pain reliever and fever reducer", Type = "Tablet" },
        new Medicament { IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic for infections", Type = "Capsule" },
        new Medicament { IdMedicament = 3, Name = "Ibuprofen", Description = "Anti-inflammatory and pain relief", Type = "Tablet" }
    );

    modelBuilder.Entity<Prescription>().HasData(
        new Prescription { IdPrescription = 1, Date = new DateTime(2025, 6, 1), DueDate = new DateTime(2025, 6, 15), IdPatient = 1, IdDoctor = 1 },
        new Prescription { IdPrescription = 2, Date = new DateTime(2025, 6, 2), DueDate = new DateTime(2025, 6, 16), IdPatient = 2, IdDoctor = 2 }
    );

    modelBuilder.Entity<PrescriptionMedicament>().ToTable("Prescription_Medicament");
    modelBuilder.Entity<PrescriptionMedicament>().HasData(
        new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 500, Details = "Take one tablet every 6 hours" },
        new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 2, Dose = 250, Details = "Take one capsule twice a day" },
        new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 3, Dose = 400, Details = "Take after meals" }
    );
    }
    
    
}