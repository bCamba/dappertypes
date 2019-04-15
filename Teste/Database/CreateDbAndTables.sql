USE master
IF EXISTS(select * from sys.databases where name='dapper_types')
DROP DATABASE dapper_types

CREATE DATABASE dapper_types

USE [dapper_types]

IF OBJECT_ID ('dbo.country') IS NOT NULL
	DROP TABLE [dbo.city]

CREATE TABLE [country] (
	[id] INT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(256) NOT NULL,
	CONSTRAINT [pk_country] PRIMARY KEY ([id])
)

IF OBJECT_ID ('dbo.state') IS NOT NULL
	DROP TABLE [dbo.state]

CREATE TABLE [state] (
	[id] INT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(256) NOT NULL,
	[id_country] INT NOT NULL,
	CONSTRAINT [pk_state] PRIMARY KEY ([id]),
	CONSTRAINT [fk_state_country] FOREIGN KEY ([id_country]) REFERENCES country([id])
)

IF OBJECT_ID ('dbo.address') IS NOT NULL
	DROP TABLE [dbo.address]

CREATE TABLE [address] (
	[id] INT IDENTITY(1,1) NOT NULL,
	[street] VARCHAR(256) NOT NULL,
	[number] int NOT NULL,
	[city] VARCHAR(256) NOT NULL,
	[zip] INT NOT NULL,
	[id_state] INT NOT NULL
	CONSTRAINT [pk_address] PRIMARY KEY ([id]),
	CONSTRAINT [fk_address] FOREIGN KEY ([id_state]) REFERENCES state([id])
)


IF OBJECT_ID ('dbo.account') IS NOT NULL
	DROP TABLE [dbo.account]

CREATE TABLE [account]
	([id] INT IDENTITY(1,1) NOT NULL,
	 [amount] DECIMAL(10,2) NULL,
	 CONSTRAINT [pk_account] PRIMARY KEY ([id]))

IF OBJECT_ID ('dbo.customer') IS NOT NULL
	DROP TABLE [dbo.customer]

CREATE TABLE [customer]
	([id] UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID() NOT NULL,
	 [number] INT NOT NULL,
	 [name] VARCHAR(256) NOT NULL,
	 [id_account] INT NOT NULL,
	 [id_address] INT NOT NULL
	 CONSTRAINT [pk_customer] PRIMARY KEY ([id]),
	 CONSTRAINT [fk_customer_account] FOREIGN KEY ([id_account]) REFERENCES [account]([id]),
	 CONSTRAINT [fk_customer_address] FOREIGN KEY ([id_address]) REFERENCES [address]([id]))