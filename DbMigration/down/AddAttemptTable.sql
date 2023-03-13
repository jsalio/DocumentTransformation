BEGIN TRANSACTION;
GO

DROP TABLE [Pdf].[AttemptDetail];
GO

DROP TABLE [Pdf].[Attempt];
GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20230312000721_AddAttemptTables';
GO

COMMIT;
GO

