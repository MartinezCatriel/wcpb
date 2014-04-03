
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Equipo'))
BEGIN
	delete from Equipo
	drop table Equipo
END

CREATE TABLE [dbo].[Equipo] (
    [Id]     INT           NOT NULL IDENTITY,
    [Nombre] VARCHAR (300) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

