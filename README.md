# Product API
================

## Descripción
-----------
A continuación, se muestra un resumen de las principales características de la API RESTful: .NET 8 para la implementación de las operaciones básicas de calidad CRUD para un maestro de productos. La arquitectura de la solución es con base al patrón de arquitectura limpia y se utilizan varios patrones de diseño para asegurar que el código sea mantenible, extensible y testeado.

## Arquitectura y Patrones Utilizados
-----------------------------------

### Arquitectura

El proyecto está dividido en una arquitectura en capas, divididas de la siguientes manera:

* **ProductApi.Domain**: Contiene la lógica de dominio y los modelos de datos del negocio. Define las entidades y las interfaces de los servicios de dominio.
* **ProductApi.Application**: Maneja la lógica de aplicación, incluyendo servicios y DTOs. Implementa la lógica de negocio y orquesta la interacción entre el dominio y la capa de infraestructura.
* **ProductApi.Infrastructure**: Proporciona implementaciones concretas para las interfaces definidas en el dominio. Incluye el acceso a datos (repositorios), servicios externos, y manejo de caché.
* **ProductApi.Web**: Contiene los controladores y la configuración de la API. Expone los endpoints HTTP y maneja las solicitudes del cliente.

### Patrones de Diseño

* **Patrón de Repositorio**: Abstrae el acceso a datos y proporciona una interfaz para operaciones CRUD sin exponer los detalles de la implementación.
* **Patrón de Servicio**: Define la lógica de negocio en servicios que encapsulan la funcionalidad del dominio.
* **Patrón de Unidad de Trabajo**: Maneja transacciones y cambios en los datos de manera coherente.
* **Patrón de Validación**: Usa FluentValidation para validar los datos de entrada de manera declarativa y consistente.
* **Patrón de Caché**: Implementa caché en memoria para almacenar el estado de los productos y mejorar el rendimiento de las lecturas.
* **Patrón de Middleware**: Utiliza middleware para el manejo de excepciones y logging de solicitudes.

## Configuración del Proyecto
-------------------------

### Requisitos

* .NET 8 SDK
* SQL Server o SQL Server Express

### Pasos para Levantar el Proyecto Localmente

1. **Clonar el Repositorio**
```bash
git clone https://github.com/ELopezTeam/ProductsProject.git
```
2. **Ejecutar el siguiente script en su base de datos:**
```bash
CREATE TABLE dbo.Products (
	ProductId int NOT NULL,
	Name varchar(100),
	Status int NULL,
	Stock int NULL,
	Description varchar(100),
	Price decimal(38,0) NULL
);
```
3. **Configurar la Base de Datos.** Asegúrate de tener una instancia de SQL Server en ejecución. Configura la cadena de conexión en el archivo appsettings.json ubicado en la raíz del proyecto:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}
```

4. **Restaurar Dependencias.** Navega al directorio del proyecto y restaura las dependencias usando el siguiente comando
```bash
dotnet restore
```

5. **Ejecutar la Aplicación.** Ejecuta la aplicación localmente con el siguiente comando
```bash
dotnet run
```

2. **Configurar la Base de Datos**
Asegúrate de tener una instancia de SQL Server en ejecución. Configura la cadena de conexión en el archivo appsettings.json ubicado en la raíz del proyecto:
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}

3. **Restaurar Dependencias**
Navega al directorio del proyecto y restaura las dependencias usando el siguiente comando:
```bash
dotnet restore

4. **Aplicar Migraciones de Base de Datos**
Si el proyecto usa Entity Framework Core, aplica las migraciones para configurar la base de datos:
```bash
dotnet ef database update

5. **Ejecutar la Aplicación**
Ejecuta la aplicación localmente con el siguiente comando:
```bash
dotnet run
