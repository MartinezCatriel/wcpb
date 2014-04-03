
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Partido'))
BEGIN
	delete from Partido
	drop table Partido
END

CREATE TABLE [dbo].[Partido]
(
	[Id] INT NOT NULL IDENTITY, 
    [Fecha] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Geolocalizacion] VARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([Id]) 
)
