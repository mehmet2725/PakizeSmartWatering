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
CREATE TABLE [Devices] (
    [Id] uniqueidentifier NOT NULL,
    [MacAddress] nvarchar(50) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsOnline] bit NOT NULL,
    [LastSeenAt] datetime2 NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Devices] PRIMARY KEY ([Id])
);

CREATE TABLE [Plants] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Species] nvarchar(max) NOT NULL,
    [MoistureThreshold] float NOT NULL,
    [DeviceId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Plants] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Plants_Devices_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Devices] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [MoistureLogs] (
    [Id] uniqueidentifier NOT NULL,
    [PlantId] uniqueidentifier NOT NULL,
    [MoisturePercentage] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_MoistureLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MoistureLogs_Plants_PlantId] FOREIGN KEY ([PlantId]) REFERENCES [Plants] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [WateringHistories] (
    [Id] uniqueidentifier NOT NULL,
    [PlantId] uniqueidentifier NOT NULL,
    [IsManualTrigger] bit NOT NULL,
    [PumpRunDurationSeconds] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_WateringHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WateringHistories_Plants_PlantId] FOREIGN KEY ([PlantId]) REFERENCES [Plants] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_MoistureLogs_PlantId] ON [MoistureLogs] ([PlantId]);

CREATE UNIQUE INDEX [IX_Plants_DeviceId] ON [Plants] ([DeviceId]);

CREATE INDEX [IX_WateringHistories_PlantId] ON [WateringHistories] ([PlantId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260227213230_InitialCreate', N'10.0.3');

COMMIT;
GO

