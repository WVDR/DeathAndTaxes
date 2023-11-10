USE [DeathAndTaxes]
GO

--[TaxCalculationTypes]
INSERT INTO [dbo].[TaxCalculationTypes]
           ([Name])
     VALUES
           ('Progressive');

INSERT INTO [dbo].[TaxCalculationTypes]
           ([Name])
     VALUES
           ('FlatValue');

INSERT INTO [dbo].[TaxCalculationTypes]
           ([Name])
     VALUES
           ('FlatRate');
GO

--[PostalCodes]
INSERT INTO [dbo].[PostalCodes]
           ([Code]
           ,[TaxCalculationTypeId])
     VALUES
           ('7441'
           ,1);

INSERT INTO [dbo].[PostalCodes]
           ([Code]
           ,[TaxCalculationTypeId])
     VALUES
           ('A100'
           ,2);

INSERT INTO [dbo].[PostalCodes]
           ([Code]
           ,[TaxCalculationTypeId])
     VALUES
           ('7000'
           ,3);

INSERT INTO [dbo].[PostalCodes]
           ([Code]
           ,[TaxCalculationTypeId])
     VALUES
           ('1000'
           ,1);
GO

--[TaxPercentageRates]
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (5)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (10)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (15)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (17.5)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (25)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (28)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (33)
INSERT INTO [dbo].[TaxPercentageRates]
           ([PercentageRate])
     VALUES
           (35)
GO

--[TaxIncomeBrackets]
INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (0
           ,8350)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (8351
           ,33950)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (33951
           ,82250)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (82251
           ,171550)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (171551
           ,372950)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (372951
           ,999999)

INSERT INTO [dbo].[TaxIncomeBrackets]
           ([FromIncomeBracket]
           ,[ToIncomeBracket])
     VALUES
           (0
           ,199999)
GO

--[ProgressiveTaxes]
INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('Base'
           ,2
           ,1)

INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('0 to 8350 at 10%'
           ,3
           ,2)

INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('8351 to 33950 - 15%'
           ,5
           ,3)

INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('33951 to 82250 25%'
           ,6
           ,4)

INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('82251 to 171550 28%'
           ,7
           ,5)

INSERT INTO [dbo].[ProgressiveTaxes]
           ([Description]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('171551 to 372950 33%'
           ,8
           ,6)
GO

--[FlatValueTaxes]
INSERT INTO [dbo].[FlatValueTaxes]
           ([Description]
           ,[Base]
           ,[Months]
           ,[TaxPercentageRateId]
           ,[TaxIncomeBracketId])
     VALUES
           ('10000 per year, else if the individual earns less than 200000 per year the tax will be at 5%'
           ,10000
           ,12
           ,1
           ,7)
GO

INSERT INTO [dbo].[FlatRateTaxes]
           ([Description]
           ,[TaxPercentageRateId])
     VALUES
           ('All users pay 17.5% tax on their income'
           ,4)
GO