CREATE TABLE [eligibility].[AuditTrails] (
    [Id]             INT              IDENTITY (1, 1) NOT NULL,
    [TableName]      NVARCHAR (75)    NOT NULL,
    [RecordId]       BIGINT           NOT NULL,
    [Field]          NVARCHAR (75)    NOT NULL,
    [OldValue]       NVARCHAR (MAX)   NOT NULL,
    [NewValue]       NVARCHAR (MAX)   NOT NULL,
    [ChangeDateTime] DATETIME         NOT NULL,
    [UserAction]     NVARCHAR (255)   NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_AuditTrails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

