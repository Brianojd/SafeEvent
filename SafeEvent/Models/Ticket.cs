using SafeEvent.Enum;


namespace SafeEvent.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public Guid CodigoGuid { get; set; }
        public int EventoId { get; set; }
        public int ClienteId { get; set; }
        public int? StaffValidadorId { get; set; }

        public EstadoTicketEnum Estado { get; set; } = EstadoTicketEnum.Pendiente;

        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public DateTime? FechaIngreso { get; set; }

        public Evento? Evento { get; set; }
        public Usuario? Cliente { get; set; }
        public Usuario? StaffValidador { get; set; }
    }
}