using System;

namespace SafeEvent.Errors
{
    public class RegisterException : Exception
    {
        public RegisterException(string mensaje) : base(mensaje)
        {
        }
    }
}