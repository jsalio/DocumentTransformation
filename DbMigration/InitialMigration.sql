﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Pdf') IS NULL EXEC(N'CREATE SCHEMA [Pdf];');
GO

CREATE TABLE [Pdf].[Rules] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_Rules] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pdf].[ServiceSettings] (
    [Id] int NOT NULL IDENTITY,
    [WorkMode] int NOT NULL,
    [EnableSecondQueue] bit NOT NULL,
    [TimerWorkMode] int NOT NULL,
    [TimeUnit] int NOT NULL,
    [Interval] int NOT NULL,
    [startDate] datetime2 NOT NULL,
    [TimeInit] nvarchar(max) NULL,
    [TimeEnd] nvarchar(max) NULL,
    CONSTRAINT [PK_ServiceSettings] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pdf].[Workflows] (
    [Id] int NOT NULL IDENTITY,
    [Handle] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsActiveQaIndex] bit NOT NULL,
    [IsActiveQaScan] bit NOT NULL,
    [IsMultipleIndexingActive] bit NOT NULL,
    [ConvertToPdf] bit NOT NULL,
    CONSTRAINT [PK_Workflows] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230114040607_InitialMigration', N'5.0.17');
GO

COMMIT;
GO

