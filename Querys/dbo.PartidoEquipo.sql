CREATE TABLE [dbo].[PartidoEquipo] (
    [IdPartido] INT NOT NULL,
    [IdEquipo]  INT NOT NULL,
    [Goles]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEquipo] ASC, [IdPartido] ASC),
    CONSTRAINT [FK_PartidoEquipo_ToPartido] FOREIGN KEY ([IdPartido]) REFERENCES [dbo].[Partido] ([Id]),
    CONSTRAINT [FK_PartidoEquipo_ToEquipo] FOREIGN KEY ([IdEquipo]) REFERENCES [dbo].[Equipo] ([Id])
);


GO

CREATE INDEX [IX_PartidoEquipo_IdPartido_IdEquipo] ON [dbo].[PartidoEquipo] ([IdPartido],[IdEquipo])
