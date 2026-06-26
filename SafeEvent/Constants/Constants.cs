namespace SafeEvent.Constants
{
    public static class ErrorMessages
    {
        public const string TicketInexistente = "El ticket escaneado no existe en el sistema.";
        public const string TicketYaUtilizado = "Acceso Denegado: Este ticket ya fue utilizado.";
        public const string TicketAnulado = "Acceso Denegado: Este ticket se encuentra anulado.";
        public const string EventoAforoLleno = "Acceso Denegado: El evento ha alcanzado su capacidad máxima.";
        public const string TicketNoEncontradoConsulta = "Ticket no encontrado.";
        public const string AnulacionTicketIngresado = "No se puede anular un ticket que ya fue utilizado.";

        // --- Mensajes de Error: Registro / Emisión ---
        public const string EventoInexistenteRegistro = "El evento especificado no existe.";
        public const string ClienteInexistenteRegistro = "El cliente especificado no existe o el ID es inválido.";

        // --- Títulos de Problem Details ---
        public const string TitleValidacion = "Error en la Validación del Ticket";
        public const string TitleRegistro = "Error en el Registro / Emisión";
        public const string TitleErrorInterno = "Error Interno del Servidor";

      
        public const string TypeValidacion = "ERR_VALIDACION_ACCESO";
        public const string TypeRegistro = "ERR_REGISTRO_FALLIDO";
        public const string TypeErrorInterno = "ERR_INTERNO_SERVIDOR";

        // --- Mensajes de Éxito ---
        public const string AccesoPermitido = "Acceso Permitido";
        public const string AnulacionExitosa = "Ticket anulado correctamente.";
       

      
        public const string CredencialesInvalidas = "Acceso Denegado: Email o contraseña incorrectos.";
        public const string UsuarioNoActivo = "El usuario se encuentra inhabilitado para ingresar.";

      
 
        public const string EmailDuplicado = "El correo electrónico ya se encuentra registrado.";
        public const string DniDuplicado = "El DNI ingresado ya se encuentra registrado en el sistema.";
        public const string RolInvalido = "El rol especificado no es válido para el sistema.";

      
    }
}