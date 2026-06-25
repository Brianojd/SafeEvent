using SafeEvent.Models;

namespace SafeEvent.Services
{
    public interface ITicketService
    {
       
        Task<Ticket> RegistrarTicketAsync(int eventoId, int clienteId);
        Task<Ticket> ValidarAccesoAsync(Guid codigoGuid, int staffValidadorId);
        Task<object> ConsultarEstadoYAforoAsync(Guid codigoGuid);
        Task<bool> AnularAcreditaciónAsync(int ticketId);
    }
}