BEGIN TRANSACTION;
GO

DROP TABLE [Pdf].[DocumentConvertSettings];
GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20230318235047_WorkflowDocumentTypeSettings';
GO

COMMIT;
GO

