﻿CREATE TABLE [dbo].[Usuario]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nombre] VARCHAR(600) NOT NULL, 
    [Email] VARCHAR(600) NOT NULL, 
    [Procedencia] VARCHAR(50) NOT NULL
)
