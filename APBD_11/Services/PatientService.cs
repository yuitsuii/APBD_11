using APBD_11.Data;
using APBD_11.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Services;

public class PatientService : IPatientService
    {
        private readonly MDbContext _context;

        public PatientService(MDbContext context)
        {
            _context = context;
        }

        public async Task<PatientDTO> GetPatientInfoAsync(int idPatient)
        {
            var patient = await _context.Patients
                .Where(p => p.IdPatient == idPatient)
                .Select(p => new PatientDTO
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    Prescriptions = p.Prescriptions
                        .OrderBy(pr => pr.DueDate)
                        .Select(pr => new PrescriptionDTO
                        {
                            IdPrescription = pr.IdPrescription,
                            Date = pr.Date,
                            DueDate = pr.DueDate,
                            Doctor = new DoctorDTO
                            {
                                IdDoctor = pr.Doctor.IdDoctor,
                                FirstName = pr.Doctor.FirstName,
                                LastName = pr.Doctor.LastName
                            },
                            Medicaments = pr.PrescriptionMedicaments
                                .Select(pm => new MedicamentDTO
                                {
                                    IdMedicament = pm.Medicament.IdMedicament,
                                    Name = pm.Medicament.Name,
                                    Description = pm.Medicament.Description,
                                    Dose = pm.Dose
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();
            return patient;
        }
        
        //this method here was for a test, everything works fine now but I do not want to remove it
        public async Task<string> GetPatientFullNameAsync(int idPatient)
        {
            var patient = await _context.Patients
                .Where(p => p.IdPatient == idPatient)
                .Select(p => new { p.FirstName, p.LastName })
                .FirstOrDefaultAsync();

            return patient == null ? null : $"{patient.FirstName} {patient.LastName}";
        }
        
        
    }