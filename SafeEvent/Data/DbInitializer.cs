using System.Linq;
using SafeEvent.Models;
using SafeEvent.Enum;

namespace SafeEvent.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
           
            context.Database.EnsureCreated();

          
            if (context.Eventos.Count() == 0)
            {
          

                // Evento 1: Con capacidad normal disponible
                var lollapalooza = new Evento
                {
                    Nombre = "Lollapalooza 2027",
                    Lugar = "Hipódromo de San Isidro",
                    CapacidadMaxima = 5,
                    AforoActual = 0
                };

                // Evento 2: Al borde del colapso (para probar error de aforo lleno)
                var recitalRiver = new Evento
                {
                    Nombre = "Recital River Plate",
                    Lugar = "Estadio Mâs Monumental",
                    CapacidadMaxima = 1, // Capacidad mínima para llenarlo fácil
                    AforoActual = 1      
                };

                context.Eventos.AddRange(lollapalooza, recitalRiver);

  
                var cliente1 = new Usuario
                {
                    Nombre = "Brian",
                    Email = "brian@safeevent.com",
                    DNI = "40123456",
                    Rol = RolUsuarioEnum.Cliente
                };

                var cliente2 = new Usuario
                {
                    Nombre = "Carlos Tévez",
                    Email = "apache@gmail.com",
                    DNI = "32111222",
                    Rol = RolUsuarioEnum.Cliente
                };

              
                var staff1 = new Usuario
                {
                    Nombre = "Validador Puerta Norte",
                    Email = "norte@safeevent.com",
                    DNI = "35987654",
                    Rol = RolUsuarioEnum.Staff
                };

             
                var intruso = new Usuario
                {
                    Nombre = "Juan Pérez",
                    Email = "juan@perez.com",
                    DNI = "28444555",
                    Rol = RolUsuarioEnum.Cliente 
                };

                context.Usuarios.AddRange(cliente1, cliente2, staff1, intruso);

              
                context.SaveChanges();
            }
        }
    }
}