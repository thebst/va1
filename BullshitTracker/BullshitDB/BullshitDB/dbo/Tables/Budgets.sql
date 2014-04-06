CREATE TABLE [dbo].[Budgets] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [BudgetName] VARCHAR (50) NULL,
    CONSTRAINT [PK_Budgets] PRIMARY KEY CLUSTERED ([Id] ASC)
);

