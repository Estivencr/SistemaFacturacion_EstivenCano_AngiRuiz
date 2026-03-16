# SistemaFacturacion (WinForms)

Proyecto de escritorio en C# (WinForms) con arquitectura por capas:
- `CapaDatos`: acceso a datos (SQL Server)
- `CapaNegocio`: reglas de negocio
- `Pantallas_Sistema_facturacion_EstivenCano_AngiRuiz`: interfaz (WinForms)
- `Query`: script SQL para crear la base de datos

## Requisitos
- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- SQL Server / SQL Server Express (ej: `SQLEXPRESS`)

## Configuracion inicial
1. Ejecuta el script: `Query/SQLQuery1.sql` (crea `SistemaVentasDB` y sus tablas).
2. Verifica el connection string en `Pantallas_Sistema_facturacion_EstivenCano_AngiRuiz/App.config`.
3. Compila y ejecuta la solucion desde Visual Studio.

## Credenciales por defecto
El script `Query/SQLQuery1.sql` crea un usuario inicial si no existe:
- Usuario: `Estiven`
- Contrasena: `123456`

## Notas
- El formulario de Ayuda usa el control `WebBrowser`. Dependiendo del equipo, algunas paginas modernas pueden no renderizar perfecto.
