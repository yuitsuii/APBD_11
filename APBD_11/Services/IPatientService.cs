using APBD_11.Models.DTOs;

namespace APBD_11.Services;

public interface IPatientService
{
    Task<PatientDTO> GetPatientInfoAsync(int idPatient);
    Task<string> GetPatientFullNameAsync(int idPatient);
}