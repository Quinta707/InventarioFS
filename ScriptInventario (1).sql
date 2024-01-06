CREATE DATABASE InventarioSEQP
GO
USE InventarioSEQP
GO

CREATE TABLE Usuarios(
	UsuarioID				INT IDENTITY(1,1),
	Nombre					VARCHAR(100),
	Clave					VARCHAR(MAX),
	EmpleadoID				INT,
	RolID					INT,

	Estado					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacionID		DATETIME,
	CONSTRAINT PK_Usuarios_UsuariosID	PRIMARY KEY(UsuarioID),
);
GO

DECLARE @pass varchar(max) = '123'
INSERT INTO Usuarios(Nombre, Clave, EmpleadoID, RolID, UsuarioCreacionID, FechaCreacion)
VALUES	('Admin', HASHBYTES('Sha_512', @pass), 1, 1, 1, GETDATE())
GO
ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID)
GO
ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
GO
CREATE TABLE Roles(
	RolID					INT IDENTITY(1,1),
	Nombre					VARCHAR(50),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_Roles_RolID														PRIMARY KEY(RolID),
	CONSTRAINT FK_Roles_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Roles_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

INSERT INTO Roles(Nombre, UsuarioCreacionID, FechaCreacion)
VALUES	('Jefe de bodega', 1, GETDATE())
GO

CREATE TABLE Permisos(
	PermisoID				INT IDENTITY(1,1),
	Nombre					VARCHAR(50),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_Permisos_PermisoID													PRIMARY KEY(PermisoID),
	CONSTRAINT FK_Permisos_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Permisos_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE RolesPorPermiso(
	RolPorPermisoID			INT IDENTITY(1,1),
	RolID					INT,
	PermisoID				INT,

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_RolesPorPermiso_RolPorPermisoID											PRIMARY KEY(RolPorPermisoID),
	CONSTRAINT FK_RolesPorPermiso_RolID					FOREIGN KEY(RolID)					REFERENCES Roles(RolID),
	CONSTRAINT FK_RolesPorPermiso_PermisoID				FOREIGN KEY(PermisoID)				REFERENCES Permisos(PermisoID),
	CONSTRAINT FK_RolesPorPermiso_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_RolesPorPermiso_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE EstadosCiviles(
	EstadoCivilID			INT IDENTITY(1,1),
	Descripcion				VARCHAR(40),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_EstadosCiviles_EstadoCivilID												PRIMARY KEY(EstadoCivilID),
	CONSTRAINT FK_EstadosCiviles_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_EstadosCiviles_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE Empleados(
	EmpleadoID				INT IDENTITY(1,1),
	Nombre					VARCHAR(100),
	Apellido				VARCHAR(100),
	Identidad				VARCHAR(13) UNIQUE,
	Telefono				VARCHAR(20),
	Genero					CHAR(1),
	Direccion				VARCHAR(100),
	EstadoCivilID			INT,

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PKEmpleados_EmpleadoID													PRIMARY KEY(EmpleadoID),
	CONSTRAINT CK_Empleados_EmpleadoID													CHECK (Genero IN ('F', 'M')),
	CONSTRAINT FK_Empleados_EstadoCivilID			FOREIGN KEY(EstadoCivilID)			REFERENCES EstadosCiviles(EstadoCivilID),
	CONSTRAINT FK_Empleados_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Empleados_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE EstadoSalidas(
	EstadoID				INT IDENTITY(1,1),
	Descripcion				VARCHAR(50),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_EstadoSalidas_EstadoID													PRIMARY KEY(EstadoID),
	CONSTRAINT FK_EstadoSalidas_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_EstadoSalidas_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE Sucursales(
	SucursalID				INT IDENTITY(1,1),
	Nombre					VARCHAR(50),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_Sucursales_SucursalID													PRIMARY KEY(SucursalID),
	CONSTRAINT FK_Sucursales_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Sucursales_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE Productos(
	ProductoID				INT IDENTITY(1,1),
	Nombre					VARCHAR(100),

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_Productos_ProductoID													PRIMARY KEY(ProductoID),
	CONSTRAINT FK_Productos_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Productos_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO


CREATE TABLE Lotes(
	LoteID					INT IDENTITY(1,1),
	ProductoID				INT,
	CantidadInicial			INT,
	CostoUnitario			DECIMAL(18,2),
	CantidadActual			INT,
	FechaVencimiento		DATETIME,

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_Lotes_LoteID														PRIMARY KEY(LoteID),
	CONSTRAINT FK_Lotes_ProductoID					FOREIGN KEY(ProductoID)				REFERENCES Productos(ProductoID),
	CONSTRAINT FK_Lotes_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_Lotes_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE SalidasInventario(
	SalidaInventarioID		INT IDENTITY(1,1),
	SucursalID				INT,
	FechaSalida				DATETIME,
	Total					INT,
	FechaRecibido			DATETIME,
	UsuarioRecibeID			INT,

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_SalidasInventario_SalidaInventarioID											PRIMARY KEY(SalidaInventarioID),
	CONSTRAINT FK_SalidasInventario_UsuarioCreacionID		FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_SalidasInventario_UsuarioModificacionID	FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_SalidasInventario_UsuarioRecibeID			FOREIGN KEY(UsuarioRecibeID)		REFERENCES Usuarios(UsuarioID)
);
GO

CREATE TABLE SalidasInventarioDetalles(
	SalidaInventarioDetalleID		INT IDENTITY(1,1),
	SalidaInventarioID				INT,
	LoteID							INT,
	Cantidad						INT,

	Activo					BIT DEFAULT(1),
	UsuarioCreacionID		INT,
	FechaCreacion			DATETIME,
	UsuarioModificacionID	INT,
	FechaModificacion		DATETIME,
	CONSTRAINT PK_SalidasInventarioDetalles_SalidaInventarioDetalleID										PRIMARY KEY(SalidaInventarioDetalleID),
	CONSTRAINT FK_SalidasInventarioDetalles_SalidaInventarioID			FOREIGN KEY(SalidaInventarioID)		REFERENCES SalidasInventario(SalidaInventarioID),
	CONSTRAINT FK_SalidasInventarioDetalles_LoteID						FOREIGN KEY(LoteID)					REFERENCES Lotes(LoteID),
	CONSTRAINT FK_SalidasInventarioDetalles_UsuarioCreacionID			FOREIGN KEY(UsuarioCreacionID)		REFERENCES Usuarios(UsuarioID),
	CONSTRAINT FK_SalidasInventarioDetalles_UsuarioModificacionID		FOREIGN KEY(UsuarioModificacionID)	REFERENCES Usuarios(UsuarioID),
);
GO

----------------------------------------------------------------------------------
---------------------------------INSERTS------------------------------------------
----------------------------------------------------------------------------------


------------********** PERMISOS **********---------------------

INSERT INTO Permisos(Nombre, UsuarioCreacionID, FechaCreacion)
VALUES	('Creoqueaquiibanpantallas', 1, GETDATE())
GO


------------********** ROLES POR PERMISOS **********---------------------

INSERT INTO RolesPorPermiso(RolID, PermisoID, UsuarioCreacionID, FechaCreacion)
VALUES	(1, 1, 1, GETDATE())
GO

------------********** ESTADOS CIVILES **********---------------------

INSERT INTO EstadosCiviles(Descripcion, UsuarioCreacionID, FechaCreacion)
VALUES	('Casado(a)', 1, GETDATE()),
		('Divorciado(a)', 1, GETDATE()),
		('Soltero(a)', 1, GETDATE()),
		('Viudo(a)', 1, GETDATE()),
		('Union Libre(a)', 1, GETDATE());
GO

------------********** ESTADOS CIVILES **********---------------------

INSERT INTO Empleados(Nombre, Apellido, Identidad, Telefono, Genero, Direccion, EstadoCivilID, UsuarioCreacionID, FechaCreacion)
VALUES	('Mauricio', 'Mateo', '0501200500928', '238218394', 'M', 'Santa Martha', 2, 1, GETDATE())

ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_EmpleadoID FOREIGN KEY(EmpleadoID) REFERENCES Empleados(EmpleadoID)
GO

ALTER TABLE SalidasInventario
ADD CONSTRAINT DF_SalidasInventario_EstadoID DEFAULT 1 FOR EstadoID
GO

ALTER TABLE SalidasInventario
ADD CONSTRAINT FK_SalidasInventario_EstadoID FOREIGN KEY (EstadoID) REFERENCES [dbo].[EstadoSalidas]([EstadoID])