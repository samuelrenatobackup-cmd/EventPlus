using EventPlusWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("testar")]
        public async Task<IActionResult> Testar()
        {
            await _emailService.VerificarEventos();

            return Ok("Verificação executada.");
        }
    }
}