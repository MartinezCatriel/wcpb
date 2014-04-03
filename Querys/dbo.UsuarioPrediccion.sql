CREATE TABLE [dbo].[UsuarioPrediccion]
(
	[IdPartido] INT NOT NULL , 
    [IdUsuario] INT NOT NULL, 
    [IdEquipo] INT NOT NULL, 
    [Goles] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY ([IdEquipo], [IdPartido], [IdUsuario]), 
    CONSTRAINT [FK_UsuarioPrediccion_ToPartido] FOREIGN KEY ([IdPartido]) REFERENCES [Partido]([Id]), 
    CONSTRAINT [FK_UsuarioPrediccion_ToUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([Id]), 
    CONSTRAINT [FK_UsuarioPrediccion_ToEquipo] FOREIGN KEY ([IdEquipo]) REFERENCES [Equipo]([Id])
)

GO

CREATE INDEX [IX_UsuarioPrediccion_IdPartido_IdUsuario_IdEquipo] ON [dbo].[UsuarioPrediccion] ([IdPartido],[IdUsuario],[IdEquipo])
