CREATE DATABASE SistemaVentasDB;
GO

USE SistemaVentasDB;
GO

CREATE TABLE Categorias(
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    NombreCategoria NVARCHAR(150) NOT NULL UNIQUE,
    Estado BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Productos(
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    CodigoReferencia NVARCHAR(50) NOT NULL,
    PrecioCompra DECIMAL(18,2) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    IdCategoria INT NOT NULL,
    RutaImagen NVARCHAR(250),
    Detalles NVARCHAR(500),
    Estado BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Productos_Categorias
    FOREIGN KEY (IdCategoria)
    REFERENCES Categorias(IdCategoria)
);

CREATE TABLE Clientes(
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto NVARCHAR(150) NOT NULL,
    Documento NVARCHAR(20),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    Direccion NVARCHAR(200),
    Estado BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Empleados(
    IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto NVARCHAR(150) NOT NULL,
    Documento NVARCHAR(20),
    Telefono NVARCHAR(20),
    Cargo NVARCHAR(100),
    Estado BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Usuarios(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    IdEmpleado INT NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    ClaveHash NVARCHAR(256) NOT NULL,
    Rol NVARCHAR(50) DEFAULT 'Vendedor',
    Estado BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Usuarios_Empleados
    FOREIGN KEY (IdEmpleado)
    REFERENCES Empleados(IdEmpleado)
);

CREATE TABLE Ventas(
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdEmpleado INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL,
    Estado BIT DEFAULT 1,

    CONSTRAINT FK_Ventas_Clientes
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),

    CONSTRAINT FK_Ventas_Empleados
    FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado)
);


CREATE TABLE DetalleVenta(
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleVenta_Ventas
    FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta),

    CONSTRAINT FK_DetalleVenta_Productos
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);


INSERT INTO Categorias (NombreCategoria) VALUES
('Electrónica'),
('Ropa'),
('Alimentos');

-- Empleado admin por defecto
DECLARE @IdEmpleadoAdmin INT;

IF NOT EXISTS (SELECT 1 FROM Empleados WHERE NombreCompleto = 'Administrador General')
BEGIN
    INSERT INTO Empleados (NombreCompleto, Cargo, Estado)
    VALUES ('Administrador General', 'Administrador', 1);

    SET @IdEmpleadoAdmin = SCOPE_IDENTITY();
END
ELSE
BEGIN
    SELECT TOP 1 @IdEmpleadoAdmin = IdEmpleado
    FROM Empleados
    WHERE NombreCompleto = 'Administrador General'
    ORDER BY IdEmpleado;
END

-- Usuario por defecto
-- Usuario: Estiven
-- Clave : 123456
IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'Estiven')
BEGIN
    INSERT INTO Usuarios (IdEmpleado, Usuario, ClaveHash, Rol, Estado)
    VALUES (
        @IdEmpleadoAdmin,
        'Estiven',
        LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
        'Administrador',
        1
    );
END

-- Consultas de verificacion (opcional)
-- SELECT * FROM Clientes;
-- SELECT * FROM Categorias;
-- SELECT * FROM DetalleVenta;
-- SELECT * FROM Empleados;
-- SELECT * FROM Productos;
-- SELECT * FROM Usuarios;
-- SELECT IdCategoria, NombreCategoria FROM Categorias WHERE Estado = 1;
INSERT INTO Clientes (NombreCompleto, Documento, Telefono, Email, Direccion, Estado)
VALUES 
('Carlos Andrés Gómez', '1023456789', '3001234567', 'carlos.gomez@email.com', 'Calle 45 #12-34, Bogotá', 1),

('María Fernanda López', '1098765432', '3109876543', 'maria.lopez@email.com', 'Cra 10 #20-15, Medellín', 1),

('Juan Sebastián Torres', '1011122233', '3154567890', 'juan.torres@email.com', 'Av 68 #30-50, Cali', 1),

('Laura Camila Rojas', '1033344455', '3206549871', 'laura.rojas@email.com', 'Calle 8 #14-22, Barranquilla', 1),

('Miguel Ángel Herrera', '1044455566', '3117894561', 'miguel.herrera@email.com', 'Cra 50 #40-60, Bucaramanga', 1),

('Sofía Valentina Castro', '1055566677', '3183216547', 'sofia.castro@email.com', 'Calle 25 #18-75, Cartagena', 0);
