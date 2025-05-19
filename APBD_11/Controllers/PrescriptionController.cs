using APBD_11.Models.DTOs;
using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDTO dto)
        {
            try
            {
                var created = await _service.AddPrescriptionAsync(dto);
                return CreatedAtAction(
                    nameof(PatientController.GetPatientInfo), "Patient",
                    new { idPatient = created.IdPrescription }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
