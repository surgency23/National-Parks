/*
   Wednesday, February 21, 20188:38:18 AM
   User: 
   Server: localhost\sqlexpress
   Database: AnimalHospital
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.[Procedure].Procedure_ID', N'Tmp_Prodcedure_ID_3', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.[Procedure].Tmp_Prodcedure_ID_3', N'Prodcedure_ID', 'COLUMN' 
GO
ALTER TABLE dbo.[Procedure] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Completed_Prodecudure
	(
	Completed_Prodecure_ID int NOT NULL,
	Procedure_ID int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Completed_Prodecudure ADD CONSTRAINT
	PK_Completed_Prodecudure PRIMARY KEY CLUSTERED 
	(
	Completed_Prodecure_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Completed_Prodecudure ON dbo.Completed_Prodecudure
	(
	Completed_Prodecure_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Completed_Prodecudure ADD CONSTRAINT
	FK_Completed_Prodecudure_Procedure_ID FOREIGN KEY
	(
	Procedure_ID
	) REFERENCES dbo.[Procedure]
	(
	Prodcedure_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Completed_Prodecudure SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Pet
	DROP CONSTRAINT FK_Pet_Owner_ID
GO
ALTER TABLE dbo.Owner SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Pet
	(
	Pet_ID int NOT NULL IDENTITY (1, 1),
	Pet_Name varchar(30) NOT NULL,
	Completed_Procedure int NOT NULL,
	Pet_Age int NOT NULL,
	Owner_ID int NOT NULL,
	Last_Vist_Date date NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Pet SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Pet ON
GO
IF EXISTS(SELECT * FROM dbo.Pet)
	 EXEC('INSERT INTO dbo.Tmp_Pet (Pet_ID, Pet_Name, Completed_Procedure, Pet_Age, Owner_ID, Last_Vist_Date)
		SELECT Pet_ID, Pet_Name, CONVERT(int, Pet_Type), Pet_Age, Owner_ID, Last_Vist_Date FROM dbo.Pet WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Pet OFF
GO
DROP TABLE dbo.Pet
GO
EXECUTE sp_rename N'dbo.Tmp_Pet', N'Pet', 'OBJECT' 
GO
ALTER TABLE dbo.Pet ADD CONSTRAINT
	PK_Pet PRIMARY KEY CLUSTERED 
	(
	Pet_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Pet ADD CONSTRAINT
	FK_Pet_Owner_ID FOREIGN KEY
	(
	Owner_ID
	) REFERENCES dbo.Owner
	(
	Owner_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Pet ADD CONSTRAINT
	FK_Pet_Completed_Procedure FOREIGN KEY
	(
	Completed_Procedure
	) REFERENCES dbo.Completed_Prodecudure
	(
	Completed_Prodecure_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Pricing
	(
	Pet_ID int NULL,
	Price decimal(18, 0) NOT NULL,
	Tax float(53) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Pricing SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Pricing)
	 EXEC('INSERT INTO dbo.Tmp_Pricing (Price, Tax)
		SELECT Price, Tax FROM dbo.Pricing WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Pricing
GO
EXECUTE sp_rename N'dbo.Tmp_Pricing', N'Pricing', 'OBJECT' 
GO
ALTER TABLE dbo.Pricing ADD CONSTRAINT
	PK_Pricing_1 PRIMARY KEY CLUSTERED 
	(
	Price
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Pricing ADD CONSTRAINT
	FK_Pricing_Pet_ID FOREIGN KEY
	(
	Pet_ID
	) REFERENCES dbo.Pet
	(
	Pet_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Invoice
	(
	Invoice_ID int NOT NULL IDENTITY (1, 1),
	Owner_ID int NULL,
	Price decimal(18, 0) NULL,
	Invoice_Date date NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Invoice SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Invoice ON
GO
IF EXISTS(SELECT * FROM dbo.Invoice)
	 EXEC('INSERT INTO dbo.Tmp_Invoice (Invoice_ID, Owner_ID, Invoice_Date)
		SELECT Invoice_ID, Owner_ID, Invoice_Date FROM dbo.Invoice WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Invoice OFF
GO
DROP TABLE dbo.Invoice
GO
EXECUTE sp_rename N'dbo.Tmp_Invoice', N'Invoice', 'OBJECT' 
GO
ALTER TABLE dbo.Invoice ADD CONSTRAINT
	PK_Invoice PRIMARY KEY CLUSTERED 
	(
	Invoice_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Invoice ADD CONSTRAINT
	FK_Invoice_Price FOREIGN KEY
	(
	Price
	) REFERENCES dbo.Pricing
	(
	Price
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Invoice ADD CONSTRAINT
	FK_Invoice_Owner FOREIGN KEY
	(
	Owner_ID
	) REFERENCES dbo.Owner
	(
	Owner_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Address ADD CONSTRAINT
	FK_Address_Owner_ID FOREIGN KEY
	(
	Owner_ID
	) REFERENCES dbo.Owner
	(
	Owner_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Address SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
