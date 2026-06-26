using Microsoft.AspNetCore.Mvc;
using SafeEvent.Constants; // <--- Importamos las constantes
using SafeEvent.Services;

namespace SafeEvent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

       
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromQuery] int eventoId, [FromQuery] int clienteId)
        {
            var ticket = await _ticketService.RegistrarTicketAsync(eventoId, clienteId);
            return Ok(ticket);
        }

       
        [HttpPut("validar")]
        public async Task<IActionResult> Validar([FromQuery] Guid codigoGuid, [FromQuery] int staffId)
        {
            var ticketValidado = await _ticketService.ValidarAccesoAsync(codigoGuid, staffId);

           
            return Ok(new { mensaje = ErrorMessages.AccesoPermitido, ticket = ticketValidado });
        }

        
        [HttpGet("consultar/{codigoGuid}")]
        public async Task<IActionResult> Consultar(Guid codigoGuid)
        {
            var resultado = await _ticketService.ConsultarEstadoYAforoAsync(codigoGuid);
            return Ok(resultado);
        }

      
        [HttpPut("anular/{id}")]
        public async Task<IActionResult> Anular(int id)
        {
            var anulado = await _ticketService.AnularAcreditaciónAsync(id);
            if (!anulado) return NotFound();

            
            return Ok(new { mensaje = ErrorMessages.AnulacionExitosa });
        }
    }
}