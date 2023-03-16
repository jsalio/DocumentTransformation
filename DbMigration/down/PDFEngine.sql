BEGIN TRANSACTION;
GO

DROP TABLE [Pdf].[EngineLicenses];
GO

DROP TABLE [Pdf].[PdfEngines];
GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20230313165522_AddEngineTable';
GO

COMMIT;
GO

