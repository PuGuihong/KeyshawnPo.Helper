
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/12/2018 21:58:13
-- Generated from EDMX file: D:\MyCodePro\MyLib\keyshawnpo.helper\KeyshawnPo.Helper\KeyshawnPo.Helper\KeyshawnPo.Dao.EntityFramework\Dao.EntityFramework\DBModel.edmx
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

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Tmplete'
ALTER TABLE [dbo].[Tmplete]
ADD CONSTRAINT [PK_Tmplete]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------