/*
   Wednesday, February 21, 20186:04:00 PM
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
ALTER TABLE dbo.[Procedure] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.[Completed Prodecures]
	DROP CONSTRAINT [FK_Completed Prodecures_Completed Prodecures]
GO
ALTER TABLE dbo.[Completed Prodecures] ADD CONSTRAINT
	[FK_Completed Prodecures_Completed Prodecures] FOREIGN KEY
	(
	Completed_Procedure_ID
	) REFERENCES dbo.[Procedure]
	(
	Procedure_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.[Completed Prodecures] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
