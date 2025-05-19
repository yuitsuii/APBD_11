using APBD_11.Models.DTOs;

namespace APBD_11.Services;

public interface IPrescriptionService
{
    Task<PrescriptionDTO> AddPrescriptionAsync(PrescriptionCreateDTO dto);
}