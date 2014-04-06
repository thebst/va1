CREATE TABLE [dbo].[MonthlyBudget] (
    [Id]     INT   IDENTITY (1, 1) NOT NULL,
    [Period] INT   NULL,
    [Budget] INT   NULL,
    [Amount] MONEY NULL,
    CONSTRAINT [PK_MonthlyBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MonthlyBudget_Budgets] FOREIGN KEY ([Budget]) REFERENCES [dbo].[Budgets] ([Id]),
    CONSTRAINT [FK_MonthlyBudget_Periods] FOREIGN KEY ([Period]) REFERENCES [dbo].[Periods] ([Id])
);

