create table dbo.Lookups(
	[Id]			INT NOT NULL,
	[Type]			NVARCHAR(50)  NOT NULL,
	[Value]			NVARCHAR(100) NOT NULL,
	[CreatedBy]		NVARCHAR(100)       NULL,
    [CreatedDate]   DATETIME			NULL,
	[UpdatedBy]		NVARCHAR(100)       NULL,
    [UpdateDate]    DATETIME			NULL,
	CONSTRAINT [Lookups_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO


create table dbo.Tasks (
	[Id]			INT IDENTITY(1,1) NOT NULL,
	[PriorityLookupId]	INT					NOT NULL,
	[Description]	NVARCHAR(200)		NOT NULL,
	[CreatedBy]		NVARCHAR(100)       NULL,
    [CreatedDate]   DATETIME			NULL,
	[UpdatedBy]		NVARCHAR(100)       NULL,
    [UpdateDate]    DATETIME			NULL,
	CONSTRAINT [Tasks_PK] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Tasks_LookupTypeId] FOREIGN KEY ([PriorityLookupId]) REFERENCES [dbo].[Lookups] ([Id]),
)
GO


INSERT INTO dbo.Lookups ([Id], [Type], [Value], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(100, 'TaskPriority', 'High', 'achan', GETUTCDATE(), NULL, NULL)
INSERT INTO dbo.Lookups ([Id], [Type], [Value], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(101, 'TaskPriority', 'Medium', 'achan', GETUTCDATE(), NULL, NULL)
INSERT INTO dbo.Lookups ([Id], [Type], [Value], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(102, 'TaskPriority', 'Low', 'achan', GETUTCDATE(), NULL, NULL)


INSERT INTO dbo.Tasks ([PriorityLookupId], [Description],[CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(100, 'High Priority Tasks to be done', 'achan', GETUTCDATE(), NULL, NULL)
INSERT INTO dbo.Tasks ([PriorityLookupId], [Description],[CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(101, 'Medium Priority Tasks to be done', 'achan', GETUTCDATE(), NULL, NULL)
INSERT INTO dbo.Tasks ([PriorityLookupId], [Description],[CreatedBy], [CreatedDate], [UpdatedBy], [UpdateDate])
VALUES(102, 'Low Priority Tasks to be done', 'achan', GETUTCDATE(), NULL, NULL)