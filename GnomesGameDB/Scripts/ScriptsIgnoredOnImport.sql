﻿
/*
   Monday, February 1, 20168:52:05 PM
   User: 
   Server: DESKTOP-ALS0798\SQLEXPRESS
   Database: GnomesGameDB
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
GO

SET QUOTED_IDENTIFIER ON
GO

SET ARITHABORT ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

SET CONCAT_NULL_YIELDS_NULL ON
GO

SET ANSI_NULLS ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_WARNINGS ON
GO

COMMIT
GO

BEGIN TRANSACTION
GO

ALTER TABLE dbo.ROLES SET (LOCK_ESCALATION = TABLE)
GO

COMMIT
GO

BEGIN TRANSACTION
GO

ALTER TABLE dbo.USERS SET (LOCK_ESCALATION = TABLE)
GO

COMMIT
GO

BEGIN TRANSACTION
GO

ALTER TABLE dbo.USER_ROLES SET (LOCK_ESCALATION = TABLE)
GO

COMMIT
GO
