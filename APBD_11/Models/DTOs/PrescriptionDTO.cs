namespace APBD_11.Models.DTOs;

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }
}