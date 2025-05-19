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
    
    
}