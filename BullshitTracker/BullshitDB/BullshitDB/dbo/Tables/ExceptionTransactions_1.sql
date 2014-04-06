CREATE TABLE [dbo].[ExceptionTransactions] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [TransactionId] INT           NULL,
    [Description]   VARCHAR (MAX) NULL,
    [AmountPreTax]  MONEY         NULL,
    [HST]           BIT           CONSTRAINT [DF_ExceptionTransactions_HST] DEFAULT ((1)) NULL,
    [Category]      INT           NULL,
    [Shame]         BIT           CONSTRAINT [DF_ExceptionTransactions_Shame] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ExceptionTransactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExceptionTransactions_Categories] FOREIGN KEY ([Category]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_ExceptionTransactions_Transactions] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[Transactions] ([Id])
);

