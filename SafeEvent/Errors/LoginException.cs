using System;

namespace SafeEvent.Errors
{
    public class LoginException : Exception
    {
        public LoginException(string mensaje) : base(mensaje)
        {
        }
    }
}