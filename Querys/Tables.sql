IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[Equipo]'))
BEGIN
	delete from [Equipo]
	drop table [Equipo]
END

go

CREATE TABLE [dbo].[Equipo] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (300) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

go

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[Partido]'))
BEGIN
	delete from [Partido]
	drop table [Partido]
END

go

CREATE TABLE [dbo].[Partido] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Fecha]           DATETIME      DEFAULT (getdate()) NOT NULL,
    [Geolocalizacion] VARCHAR (500) NOT NULL,
    [Ponderado]       INT           NOT NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Id] ASC)
);

go

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[PartidoEquipo]'))
BEGIN
	delete from PartidoEquipo
	drop table PartidoEquipo
END

go
CREATE TABLE [dbo].[PartidoEquipo] (
    [IdPartido] INT NOT NULL,
    [IdEquipo]  INT NOT NULL,
    [Goles]     INT NOT NULL,
    CONSTRAINT [PK_PartidoEquipo] PRIMARY KEY CLUSTERED ([IdPartido] ASC, [IdEquipo] ASC),
    CONSTRAINT [FK_PartidoEquipo_ToPartido] FOREIGN KEY ([IdPartido]) REFERENCES [dbo].[Partido] ([Id]),
    CONSTRAINT [FK_PartidoEquipo_ToEquipo] FOREIGN KEY ([IdEquipo]) REFERENCES [dbo].[Equipo] ([Id])
);


GO


CREATE NONCLUSTERED INDEX [IX_PartidoEquipo_IdPartido_IdEquipo]
    ON [dbo].[PartidoEquipo]([IdPartido] ASC, [IdEquipo] ASC);


go


IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[Usuario]'))
BEGIN
	delete from [Usuario]
	drop table [Usuario]
END

go

CREATE TABLE [dbo].[Usuario] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Email]       VARCHAR (600) NOT NULL,
    [Procedencia] VARCHAR (50)  NOT NULL,
    [Token]       VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



go

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[Animal]'))
BEGIN
	delete from [Animal]
	drop table [Animal]
END

go

CREATE TABLE [dbo].[Animal] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (50) NOT NULL,
    [Tipo]   INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

go
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[UsuarioAnimal]'))
BEGIN
	delete from UsuarioAnimal
	drop table UsuarioAnimal
END

go

CREATE TABLE [dbo].[UsuarioAnimal] (
    [IdUsuario] INT NOT NULL,
    [IdAnimal]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAnimal] ASC, [IdUsuario] ASC),
    CONSTRAINT [FK_UsuarioAnimal_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id]),
    CONSTRAINT [FK_UsuarioAnimal_Animal] FOREIGN KEY ([IdAnimal]) REFERENCES [dbo].[Animal] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UsuarioAnimal_IdUsuario_IdAnimal]
    ON [dbo].[UsuarioAnimal]([IdUsuario] ASC, [IdAnimal] ASC);


go

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '[UsuarioPrediccion]'))
BEGIN
	delete from [UsuarioPrediccion]
	drop table [UsuarioPrediccion]
END

go

CREATE TABLE [dbo].[UsuarioPrediccion] (
    [IdPartido] INT NOT NULL,
    [IdUsuario] INT NOT NULL,
    [IdEquipo]  INT NOT NULL,
    [Goles]     INT DEFAULT ((0)) NOT NULL,
    [Predecido] INT DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEquipo] ASC, [IdPartido] ASC, [IdUsuario] ASC),
    CONSTRAINT [FK_UsuarioPrediccion_ToEquipo] FOREIGN KEY ([IdEquipo]) REFERENCES [dbo].[Equipo] ([Id]),
    CONSTRAINT [FK_UsuarioPrediccion_ToPartido] FOREIGN KEY ([IdPartido]) REFERENCES [dbo].[Partido] ([Id]),
    CONSTRAINT [FK_UsuarioPrediccion_ToUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id])
);

go
