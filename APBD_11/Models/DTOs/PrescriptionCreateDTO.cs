namespace APBD_11.Models.DTOs;

public class PrescriptionCreateDTO
{
   
    public int? IdPatient { get; set; }
    public PatientMiniDTO? NewPatient { get; set; }   

    public int IdDoctor { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public List<PrescriptionMedicamentCreateDTO> Medicaments { get; set; }
}
