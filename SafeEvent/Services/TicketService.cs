using Microsoft.EntityFrameworkCore;
using SafeEvent.Constants;
using SafeEvent.Data;
using SafeEvent.Enum;
using SafeEvent.Errors;
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
            var eventoExiste = _context.Eventos.Any(e => e.EventoId == eventoId);
            if (!eventoExiste)
                throw new RegisterException(ErrorMessages.EventoInexistenteRegistro); // <-- Usando constante

            var clienteExiste = _context.Usuarios.Any(u => u.UsuarioId == clienteId && u.Rol == RolUsuarioEnum.Cliente);
            if (!clienteExiste)
                throw new RegisterException(ErrorMessages.ClienteInexistenteRegistro); // <-- Usando constante

            var nuevoTicket = new Ticket
            {
                EventoId = eventoId,
                ClienteId = clienteId,
                Estado = EstadoTicketEnum.Pendiente,
                CodigoGuid = Guid.NewGuid(),
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
                throw new TicketValidacionException(ErrorMessages.TicketInexistente);

            if (ticket.Estado == EstadoTicketEnum.Ingresado)
                throw new TicketValidacionException(ErrorMessages.TicketYaUtilizado);

            if (ticket.Estado == EstadoTicketEnum.Anulado)
                throw new TicketValidacionException(ErrorMessages.TicketAnulado);

            if (ticket.Evento != null && ticket.Evento.AforoActual >= ticket.Evento.CapacidadMaxima)
                throw new TicketValidacionException(ErrorMessages.EventoAforoLleno);

            ticket.Estado = EstadoTicketEnum.Ingresado;
            ticket.StaffValidadorId = staffValidadorId;
            ticket.FechaIngreso = DateTime.Now;

            if (ticket.Evento != null)
                ticket.Evento.AforoActual++;

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
                throw new TicketValidacionException(ErrorMessages.TicketNoEncontradoConsulta);

            var respuesta = new
            {
                TicketId = ticket.TicketId,
                Codigo = ticket.CodigoGuid,
                EstadoActual = ticket.Estado.ToString(),
                NombreCliente = ticket.Cliente?.Nombre,
                Evento = ticket.Evento?.Nombre,
                AforoActualDelEvento = ticket.Evento?.AforoActual,
                CapacidadMaximaDelEvento = ticket.Evento?.CapacidadMaxima
            };

            return Task.FromResult<object>(respuesta);
        }

        public Task<bool> AnularAcreditaciónAsync(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
                return Task.FromResult(false);

            if (ticket.Estado == EstadoTicketEnum.Ingresado)
                throw new TicketValidacionException(ErrorMessages.AnulacionTicketIngresado);

            ticket.Estado = EstadoTicketEnum.Anulado;
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}