
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/27/2018 10:57:56
-- Generated from EDMX file: D:\OwnerTemp\WorkInfo\KeyshawnPo.CodeLib\KeyshawnPo.Helper\KeyshawnPo.Dao.EntityFramework\Dao.EntityFramework\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MInfoSet];
GO
IF OBJECT_ID(N'[dbo].[Tmplete]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tmplete];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tmplete'
CREATE TABLE [dbo].[Tmplete] (
    [ID] uniqueidentifier  NOT NULL,
    [ParamKey] varchar(100)  NOT NULL,
    [ParamValue] varchar(100)  NOT NULL,
    [ParamRemarks] nvarchar(200)  NOT NULL,
    [ParamVersion] smallint  NOT NULL,
    [ParentID] uniqueidentifier  NULL
);
GO

-- Creating table 'MInfoSet'
CREATE TABLE [dbo].[MInfoSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [SourceName] nvarchar(50)  NULL,
    [Source] nvarchar(50)  NOT NULL,
    [IndependentSource] nvarchar(100)  NULL,
    [SourceCreateDate] datetime  NULL,
    [SourceCollectionDate] datetime  NOT NULL,
    [SourceRemark] nvarchar(max)  NULL,
    [Area] nvarchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Tmplete'
ALTER TABLE [dbo].[Tmplete]
ADD CONSTRAINT [PK_Tmplete]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'MInfoSet'
ALTER TABLE [dbo].[MInfoSet]
ADD CONSTRAINT [PK_MInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------