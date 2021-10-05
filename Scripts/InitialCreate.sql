IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

CREATE TABLE [AnswerAttachments] (
    [Id] uniqueidentifier NOT NULL,
    [AnswerId] uniqueidentifier NOT NULL,
    [Created] datetime2 NOT NULL,
    [FileName] nvarchar(max) NULL,
    [MimeType] nvarchar(max) NULL,
    [Size] bigint NOT NULL,
    CONSTRAINT [PK_AnswerAttachments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AnswerEvents] (
    [Id] uniqueidentifier NOT NULL,
    [AnswerId] uniqueidentifier NOT NULL,
    [Value] nvarchar(max) NULL,
    [Type] int NOT NULL,
    [ClientTime] datetime2 NOT NULL,
    CONSTRAINT [PK_AnswerEvents] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_AnswerAttachments_AnswerId] ON [AnswerAttachments] ([AnswerId]);
GO

CREATE INDEX [IX_AnswerEvents_AnswerId] ON [AnswerEvents] ([AnswerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211005172437_InitialCreate', N'5.0.10');
GO

COMMIT;
GO

