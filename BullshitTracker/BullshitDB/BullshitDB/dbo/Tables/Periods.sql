CREATE TABLE [dbo].[Periods] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [StartDate] DATETIME     NULL,
    [EndDate]   DATETIME     NULL,
    [Name]      VARCHAR (50) NULL,
    [Month]     INT          NULL,
    [Year]      INT          NULL,
    CONSTRAINT [PK_Periods] PRIMARY KEY CLUSTERED ([Id] ASC)
);

