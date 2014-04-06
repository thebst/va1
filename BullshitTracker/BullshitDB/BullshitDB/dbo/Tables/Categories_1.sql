CREATE TABLE [dbo].[Categories] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (50) NULL,
    [Budget] INT          NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Categories_Budgets] FOREIGN KEY ([Budget]) REFERENCES [dbo].[Budgets] ([Id])
);

