CREATE TABLE [eligibility].[MdRequestValidation] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [ValidationCode]   NVARCHAR (15)    NOT NULL,
    [Description]      NVARCHAR (300)   NULL,
    [IsSelfPay]        BIT              NOT NULL,
    [IsInsurance]      BIT              NOT NULL,
    [CreatedDateTime]  DATETIME         NOT NULL,
    [ModifiedDateTime] DATETIME         NOT NULL,
    [ModifiedBy]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_MdRequestValidation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

