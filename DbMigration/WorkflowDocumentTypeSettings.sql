BEGIN TRANSACTION;
GO

CREATE TABLE [Pdf].[DocumentConvertSettings] (
    [Id] bigint NOT NULL IDENTITY,
    [WorkflowId] int NOT NULL,
    [ConvertPdf] bit NULL,
    [SupportOcr] bit NULL,
    [DocumentTypeName] nvarchar(450) NULL,
    [DocumentTypeId] bigint NOT NULL,
    CONSTRAINT [PK_DocumentConvertSettings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DocumentConvertSettings_Workflows_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [Pdf].[Workflows] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_DocumentConvertSettings_DocumentTypeId] ON [Pdf].[DocumentConvertSettings] ([DocumentTypeId]);
GO

CREATE INDEX [IX_DocumentConvertSettings_DocumentTypeName] ON [Pdf].[DocumentConvertSettings] ([DocumentTypeName]);
GO

CREATE INDEX [IX_DocumentConvertSettings_WorkflowId] ON [Pdf].[DocumentConvertSettings] ([WorkflowId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230318235047_WorkflowDocumentTypeSettings', N'5.0.17');
GO

COMMIT;
GO

