BEGIN TRANSACTION;
GO

CREATE TABLE [Pdf].[PdfEngines] (
    [Id] int NOT NULL IDENTITY,
    [EngineTypeName] int NOT NULL,
    [EngineName] nvarchar(max) NULL,
    [EngineVersion] nvarchar(max) NULL,
    [EngineType] int NOT NULL,
    [EngineStatus] nvarchar(max) NULL,
    [EngineDescription] nvarchar(max) NULL,
    [LicenseType] int NOT NULL,
    [IsDefault] bit NOT NULL,
    [SupportOcr] bit NOT NULL,
    CONSTRAINT [PK_PdfEngines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pdf].[EngineLicenses] (
    [Id] int NOT NULL IDENTITY,
    [EngineId] int NOT NULL,
    [LicenseString] Text NULL,
    CONSTRAINT [PK_EngineLicenses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EngineLicenses_PdfEngines_EngineId] FOREIGN KEY ([EngineId]) REFERENCES [Pdf].[PdfEngines] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_EngineLicenses_EngineId] ON [Pdf].[EngineLicenses] ([EngineId]);
GO

CREATE INDEX [IX_PdfEngines_EngineTypeName] ON [Pdf].[PdfEngines] ([EngineTypeName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313165522_AddEngineTable', N'5.0.17');
GO

COMMIT;
GO

