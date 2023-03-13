BEGIN TRANSACTION;
GO

CREATE TABLE [Pdf].[Attempt] (
    [Id] bigint NOT NULL IDENTITY,
    [BatchName] nvarchar(max) NULL,
    [BatchId] bigint NOT NULL,
    [DocumentHandler] bigint NOT NULL,
    [DocumentType] bigint NOT NULL,
    [RegistryDate] datetimeoffset NOT NULL,
    [LastUpDate] datetimeoffset NOT NULL,
    [CaseCaseStatus] int NOT NULL,
    CONSTRAINT [PK_Attempt] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pdf].[AttemptDetail] (
    [Id] int NOT NULL IDENTITY,
    [AttemptId] bigint NOT NULL,
    [RegistryDate] datetimeoffset NOT NULL,
    [ErrorDetails] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_AttemptDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttemptDetail_Attempt_AttemptId] FOREIGN KEY ([AttemptId]) REFERENCES [Pdf].[Attempt] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Attempt_DocumentHandler] ON [Pdf].[Attempt] ([DocumentHandler]);
GO

CREATE INDEX [IX_AttemptDetail_AttemptId] ON [Pdf].[AttemptDetail] ([AttemptId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313145814_AddAttemptTables', N'5.0.17');
GO

COMMIT;
GO

