using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Registrar([FromName] int eventoId, [FromName] int clienteId)
        {
            try
            {
                var ticket = await _ticketService.RegistrarTicketAsync(eventoId, clienteId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al emitir el ticket", detalle = ex.Message });
            }
        }

     
        [HttpPut("validar")]
        public async Task<IActionResult> Validar([FromQuery] Guid codigoGuid, [FromQuery] int staffId)
        {
            try
            {
                var ticketValidado = await _ticketService.ValidarAccesoAsync(codigoGuid, staffId);
                return Ok(new { mensaje = "Acceso Permitido", ticket = ticketValidado });
            }
            catch (KeyNotFoundException ex)
            {
                // Error controlado: El ticket no existe (Flujo alternativo)
                return NotFound(new { mensaje = "Acceso Denegado", detalle = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Error controlado: Ya usado, anulado o aforo lleno (Flujo alternativo)
                return BadRequest(new { mensaje = "Acceso Denegado", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno en el servidor", detalle = ex.Message });
            }
        }

       
        [HttpGet("consultar/{codigoGuid}")]
        public async Task<IActionResult> Consultar(Guid codigoGuid)
        {
            try
            {
                var resultado = await _ticketService.ConsultarEstadoYAforoAsync(codigoGuid);
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al consultar", detalle = ex.Message });
            }
        }

        
        [HttpPut("anular/{id}")]
        public async Task<IActionResult> Anular(int id)
        {
            try
            {
                var anulado = await _ticketService.AnularAcreditaciónAsync(id);
                if (!anulado) return NotFound(new { mensaje = "No se encontró el ticket para anular." });

                return Ok(new { mensaje = "Ticket anulado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = "No se pudo anular", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error de servidor", detalle = ex.Message });
            }
        }
    }
}