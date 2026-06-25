using Microsoft.EntityFrameworkCore;
using SafeEvent.Data;
using SafeEvent.Models;

namespace SafeEvent.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public Task<Ticket> RegistrarTicketAsync(int eventoId, int clienteId)
        {
            var nuevoTicket = new Ticket
            {
                CodigoGuid = Guid.NewGuid(), 
                EventoId = eventoId,
                ClienteId = clienteId,
                Estado = "Pendiente",
                FechaEmision = DateTime.Now
            };

            _context.Tickets.Add(nuevoTicket);
            _context.SaveChanges();
            return Task.FromResult(nuevoTicket);
        }

       
        public Task<Ticket> ValidarAccesoAsync(Guid codigoGuid, int staffValidadorId)
        {
            
            var ticket = _context.Tickets
                .Include(t => t.Evento)
                .FirstOrDefault(t => t.CodigoGuid == codigoGuid);

         
            if (ticket == null)
                throw new KeyNotFoundException("El ticket escaneado no existe en el sistema.");

            if (ticket.Estado == "Ingresado")
                throw new InvalidOperationException("Acceso Denegado: Este ticket ya fue utilizado.");

            if (ticket.Estado == "Anulado")
                throw new InvalidOperationException("Acceso Denegado: Este ticket se encuentra anulado.");

            if (ticket.Evento != null && ticket.Evento.AforoActual >= ticket.Evento.CapacidadMaxima)
                throw new InvalidOperationException("Acceso Denegado: El evento ha alcanzado su capacidad máxima.");

            
            ticket.Estado = "Ingresado";
            ticket.StaffValidadorId = staffValidadorId; 
            ticket.FechaIngreso = DateTime.Now;

          
            if (ticket.Evento != null)
            {
                ticket.Evento.AforoActual++;
            }

            _context.SaveChanges();
            return Task.FromResult(ticket);
        }

        
        public Task<object> ConsultarEstadoYAforoAsync(Guid codigoGuid)
        {
            var ticket = _context.Tickets
                .Include(t => t.Evento)
                .Include(t => t.Cliente)
                .FirstOrDefault(t => t.CodigoGuid == codigoGuid);

            if (ticket == null)
                throw new KeyNotFoundException("Ticket no encontrado.");

            return Task.FromResult<object>(new
            {
                TicketId = ticket.TicketId,
                Asistente = ticket.Cliente?.Nombre,
                EstadoTicket = ticket.Estado,
                Evento = ticket.Evento?.Nombre,
                AforoActualDelEvento = ticket.Evento?.AforoActual,
                CapacidadMaxima = ticket.Evento?.CapacidadMaxima
            });
        }

       
        public Task<bool> AnularAcreditaciónAsync(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null) return Task.FromResult(false);

            
            if (ticket.Estado == "Ingresado")
                throw new InvalidOperationException("No se puede anular un ticket que ya fue utilizado.");

            ticket.Estado = "Anulado";
            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}