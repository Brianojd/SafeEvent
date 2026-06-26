using System;

namespace SafeEvent.Errors
{
    
    public class TicketValidacionException : Exception
    {
        public TicketValidacionException(string mensaje) : base(mensaje)
        {
        }
    }
}