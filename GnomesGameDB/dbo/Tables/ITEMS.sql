CREATE TABLE [dbo].[ITEMS]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Image] NVARCHAR(MAX) NULL, 
    [Action] NVARCHAR(MAX) NULL, 
    [Name] NVARCHAR(50) NULL
)
