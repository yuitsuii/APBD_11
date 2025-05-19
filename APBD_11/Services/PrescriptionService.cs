using APBD_11.Data;
using APBD_11.Models;
using APBD_11.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly MDbContext _context;

    public PrescriptionService(MDbContext context) => _context = context;

    public async Task<PrescriptionDTO> AddPrescriptionAsync(PrescriptionCreateDTO dto)
    {
        if (dto.Medicaments == null || !dto.Medicaments.Any())
            throw new ArgumentException("At least one medicament is required.");
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("A prescription can contain max 10 medicaments.");
        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate must be greater or equal to Date.");
        
        await using var tx = await _context.Database.BeginTransactionAsync();

        var doctor = await _context.Doctors.FindAsync(dto.IdDoctor);
        if (doctor == null) throw new ArgumentException($"Doctor {dto.IdDoctor} does not exist.");

        Patient patient;
        if (dto.IdPatient.HasValue)
        {
            patient = await _context.Patients.FindAsync(dto.IdPatient.Value)
                      ?? throw new ArgumentException($"Patient {dto.IdPatient} does not exist.");
        }
        else
        {
            patient = new Patient
            {
                FirstName = dto.NewPatient!.FirstName,
                LastName  = dto.NewPatient.LastName,
                Birthdate = dto.NewPatient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var medicamentIds = dto.Medicaments.Select(m => m.IdMedicament).Distinct().ToList();

        var existingMedIds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        var missing = medicamentIds.Except(existingMedIds).ToList();
        if (missing.Any())
            throw new ArgumentException($"Medicament IDs not found: {string.Join(", ", missing)}");

        var prescription = new Prescription
        {
            Date      = dto.Date,
            DueDate   = dto.DueDate,
            Doctor    = doctor,
            Patient   = patient,
            PrescriptionMedicaments = dto.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose         = m.Dose,
                Details      = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        await tx.CommitAsync();

        return new PrescriptionDTO
        {
            IdPrescription = prescription.IdPrescription,
            Date           = prescription.Date,
            DueDate        = prescription.DueDate
        };
    }
}