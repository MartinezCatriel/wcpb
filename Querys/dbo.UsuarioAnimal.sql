CREATE TABLE [dbo].[UsuarioAnimal]
(
	[IdUsuario] INT NOT NULL , 
    [IdAnimal] INT NOT NULL, 
    PRIMARY KEY ([IdAnimal], [IdUsuario]), 
    CONSTRAINT [FK_UsuarioAnimal_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([Id]), 
    CONSTRAINT [FK_UsuarioAnimal_Animal] FOREIGN KEY ([IdAnimal]) REFERENCES [Animal]([Id])
)

GO

CREATE INDEX [IX_UsuarioAnimal_IdUsuario_IdAnimal] ON [dbo].[UsuarioAnimal] ([IdUsuario],[IdAnimal])
