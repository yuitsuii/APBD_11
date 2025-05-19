using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        
        //this method here was for a test, do not want to remove it
        [HttpGet("{idPatient}/name")]
        public async Task<IActionResult> GetPatientName(int idPatient)
        {
            var name = await _patientService.GetPatientFullNameAsync(idPatient);
    
            if (name == null)
                return NotFound($"Patient with ID {idPatient} not found.");

            return Ok(new { FullName = name });
        }

        [HttpGet("{idPatient}")]
        public async Task<IActionResult> GetPatientInfo(int idPatient)
        {
            var patient = await _patientService.GetPatientInfoAsync(idPatient);

            if (patient == null)
                return NotFound($"Patient with ID {idPatient} not found.");

            return Ok(patient);
        }
    }
}

