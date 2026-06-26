# SafeEvent API - Módulo de Gestión y Validación de Tickets

Este componente backend pertenece a la plataforma **SafeEvent**, un sistema diseñado para la administración, emisión y control de accesos biunívocos a eventos masivos en tiempo real. 

El módulo implementa un modelo de persistencia relacional y lógica de negocio desacoplada para garantizar:
-la seguridad en la puerta de los eventos
-mitigando fraudes por duplicación de entradas 
-y controlando de manera estricta las capacidades de aforo y los permisos jerárquicos de los usuarios.



##  Requisitos del Sistema (Prerrequisitos)

Para compilar, inicializar la base de datos y ejecutar esta API correctamente, el entorno de desarrollo debe contar con:

1. **SDK de .NET 10.0** 
2. **Visual Studio 2022** (edición Community, Professional o Enterprise) con la carga de trabajo instalada: **"Desarrollo de ASP.NET y web"**.
3. **SQL Server Express LocalDB** (Instancia local liviana que viene por defecto en el instalador de Visual Studio).
   * *La cadena de conexión por defecto apunta a: `(localdb)\mssqllocaldb`.*

>  *Nota para entornos alternativos:* Si se utiliza **Visual Studio Code**, es obligatorio contar con el SDK de .NET 8.0, la extensión oficial **C# Dev Kit** instalada y un motor de SQL Server local activo configurado en el archivo `appsettings.json`.

---

##  Instrucciones de Instalación y Ejecución

La solución incorpora un enfoque de desarrollo **Code-First** con Entity Framework Core. La base de datos, su esquema de tablas y los datos de prueba se configuran de manera 100% automatizada al presionar el botón de inicio.

### Paso 1: Clonar el Repositorio y listo

