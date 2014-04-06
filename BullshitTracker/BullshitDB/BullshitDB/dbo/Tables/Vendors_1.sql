CREATE TABLE [dbo].[Vendors] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [Category] INT          NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vendors_Categories] FOREIGN KEY ([Category]) REFERENCES [dbo].[Categories] ([Id])
);

