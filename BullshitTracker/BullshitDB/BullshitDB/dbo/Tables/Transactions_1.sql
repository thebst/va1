CREATE TABLE [dbo].[Transactions] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (MAX) NULL,
    [TransactionDate] DATE          NULL,
    [Vendor]          INT           NULL,
    [Total]           MONEY         NULL,
    [Account]         INT           NULL,
    [Shame]           BIT           CONSTRAINT [DF_Transactions_Shame] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY ([Account]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Transactions_Vendors] FOREIGN KEY ([Vendor]) REFERENCES [dbo].[Vendors] ([Id])
);

