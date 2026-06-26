using System.Collections.Generic;

namespace SafeEvent.Models
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Lugar { get; set; } = string.Empty;

        // Propiedades clave para el control de acceso y aforo
        public int CapacidadMaxima { get; set; }
        public int AforoActual { get; set; }

        // Relación inversa: Un evento tiene muchos tickets vendidos
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}