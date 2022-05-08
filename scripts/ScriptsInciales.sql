CREATE TABLE TBL_MERCADERIAS
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	NOMBRE VARCHAR (200) NOT NULL,
	DESCRIPCION VARCHAR (200) NOT NULL,
	ESTADO BIT NOT NULL,
	PRECIO_VENTA DECIMAL (18,2),
	PRECIO_COMPRA DECIMAL (18,2),
	FECHA_MODIFICACION DATETIME NOT NULL,
	CANTIDAD_STOCK INT NOT NULL
)

CREATE TABLE TBL_MERCADERIAS_RETIROS
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_MERCADERIA INT NOT NULL,
	ID_PRODUCTO_CLIENTE INT NULL,
	ES_VENDIBLE BIT NOT NULL,
	OBSERVACIONES VARCHAR (255)
)

CREATE TABLE TBL_CARTERAS
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	NOMBRE_CARTERA VARCHAR (255) NOT NULL,
	ID_COBRADOR VARCHAR (255) NULL,
	ID_VENDEDOR VARCHAR (255) NULL,
	ID_SUPERVISOR VARCHAR (255) NULL,
	FECHA_CREACION DATETIME NOT NULL,
)

CREATE TABLE TBL_CLIENTES
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_CARTERA INT NOT NULL,
	NOMBRE VARCHAR (255),
	TELEFONO VARCHAR (30) NULL,
	DIRECCION VARCHAR(150) NOT NULL,
	DESCRIPCION VARCHAR (200),
	URL_IMAGEN VARCHAR (400) NULL,
	CALIFICACION VARCHAR (20) NULL,
	PERIDICIDAD_COBRO VARCHAR (30) NOT NULL,
	FECHA_REGISTRO DATETIME,
	ESTADO_PAGOS VARCHAR (30) NULL
)

CREATE TABLE TBL_PRODUCTOS_CLIENTE 
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_CLIENTE INT NOT NULL,
	ID_MERCADERIA INT NOT NULL,
	FECHA_COMPRA DATETIME NOT NULL,
	VALOR_COMPRA DECIMAL (18,2),
	ANOTACIONES VARCHAR (200) NULL,
	ESTADO_PAGO VARCHAR (50) NOT NULL,
	RETIRADO VARCHAR (50)
)

CREATE TABLE TBL_PRODUCTO_CLIENTE_ABONOS
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_PRODUCTO_CLIENTE INT NOT NULL,
	VALOR DECIMAL (18,2) NOT NULL,
	FECHA_ABONO DATETIME NOT NULL,
	ID_COBRADOR VARCHAR (255),
	ES_INICIAL BIT NOT NULL
)

CREATE TABLE TBL_VENTAS
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_PRODUCTO_CLIENTE INT NOT NULL,
	ID_USUARIO_VENDEDOR VARCHAR (255),
	ESTADO_LIQUIDACION BIT NOT NULL,
	VALOR_LIQUIDADO DECIMAL (18,2) NULL,
	FECHA_LIQUIDACION DATETIME NULL,
	VENTA_RETIRADA VARCHAR (50)
)

CREATE TABLE TBL_CATEGORIA_VENDEDORES
(
	ID INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	ID_USUARIO VARCHAR (255),
	PORCENTAJE_COMISION INT NOT NULL,
	CATEGORIA_VENDEDOR VARCHAR (20) NULL,
)