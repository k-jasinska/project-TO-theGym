
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2018 16:20:43
-- Generated from EDMX file: C:\Users\jasesobie\Desktop\GymSystem\GymSystem\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GymSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EntrancePerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntranceSet] DROP CONSTRAINT [FK_EntrancePerson];
GO
IF OBJECT_ID(N'[dbo].[FK_EntranceTypeEntrance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntranceSet] DROP CONSTRAINT [FK_EntranceTypeEntrance];
GO
IF OBJECT_ID(N'[dbo].[FK_EntranceLogEntrance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntranceLogSet] DROP CONSTRAINT [FK_EntranceLogEntrance];
GO
IF OBJECT_ID(N'[dbo].[FK_AdressPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_AdressPerson];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[EntranceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntranceSet];
GO
IF OBJECT_ID(N'[dbo].[EntranceTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntranceTypeSet];
GO
IF OBJECT_ID(N'[dbo].[EntranceLogSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntranceLogSet];
GO
IF OBJECT_ID(N'[dbo].[AddressSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Pesel] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Adress_Id] int  NULL
);
GO

-- Creating table 'EntranceSet'
CREATE TABLE [dbo].[EntranceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BeginDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Person_Id] int  NOT NULL,
    [EntranceType_Id] int  NOT NULL
);
GO

-- Creating table 'EntranceTypeSet'
CREATE TABLE [dbo].[EntranceTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Duration] int  NOT NULL
);
GO

-- Creating table 'EntranceLogSet'
CREATE TABLE [dbo].[EntranceLogSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Entrance_Id] int  NOT NULL
);
GO

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [street] nvarchar(max)  NOT NULL,
    [code] nvarchar(max)  NOT NULL,
    [city] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntranceSet'
ALTER TABLE [dbo].[EntranceSet]
ADD CONSTRAINT [PK_EntranceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntranceTypeSet'
ALTER TABLE [dbo].[EntranceTypeSet]
ADD CONSTRAINT [PK_EntranceTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntranceLogSet'
ALTER TABLE [dbo].[EntranceLogSet]
ADD CONSTRAINT [PK_EntranceLogSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Person_Id] in table 'EntranceSet'
ALTER TABLE [dbo].[EntranceSet]
ADD CONSTRAINT [FK_EntrancePerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntrancePerson'
CREATE INDEX [IX_FK_EntrancePerson]
ON [dbo].[EntranceSet]
    ([Person_Id]);
GO

-- Creating foreign key on [EntranceType_Id] in table 'EntranceSet'
ALTER TABLE [dbo].[EntranceSet]
ADD CONSTRAINT [FK_EntranceTypeEntrance]
    FOREIGN KEY ([EntranceType_Id])
    REFERENCES [dbo].[EntranceTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntranceTypeEntrance'
CREATE INDEX [IX_FK_EntranceTypeEntrance]
ON [dbo].[EntranceSet]
    ([EntranceType_Id]);
GO

-- Creating foreign key on [Entrance_Id] in table 'EntranceLogSet'
ALTER TABLE [dbo].[EntranceLogSet]
ADD CONSTRAINT [FK_EntranceLogEntrance]
    FOREIGN KEY ([Entrance_Id])
    REFERENCES [dbo].[EntranceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntranceLogEntrance'
CREATE INDEX [IX_FK_EntranceLogEntrance]
ON [dbo].[EntranceLogSet]
    ([Entrance_Id]);
GO

-- Creating foreign key on [Adress_Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [FK_AdressPerson]
    FOREIGN KEY ([Adress_Id])
    REFERENCES [dbo].[AddressSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdressPerson'
CREATE INDEX [IX_FK_AdressPerson]
ON [dbo].[PersonSet]
    ([Adress_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------