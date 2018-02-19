USE [AnimalHospital]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 2/19/2018 3:21:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[Owner](
	[Owner_First_Name] [varchar](30) NOT NULL,
	[Owner_Last_Name] [varchar](30) NOT NULL,
	[Owner_ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[Owner_ID] ASC
);




CREATE TABLE [dbo].[Address](
	[Address_ID] [int] NOT NULL,
	[Owner_ID] [int] NOT NULL,
	[Address_Type] [varchar](30) NULL,
	[AddressOtherDesc] [varchar](30) NULL,
	[AddressLine1] [varchar](30) NULL,
	[AddressLine2] [varchar](30) NULL,
	[City] [varchar](30) NULL,
	[State] [varchar](30) NULL,
	[Postal_Code] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Address_ID] ASC
),
CONSTRAINT fk_addressOwnerID Foreign Key (Owner_ID) references Owner(Owner_ID);



CREATE TABLE [dbo].[Invoice](
	[Invoice_ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner_ID] [int] NULL,
	[Invoice_Date] [date] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Invoice_ID] ASC
),
CONSTRAINT fk_invoiceOwnerID Foreign Key (Owner_ID) references Owner(Owner_ID);



CREATE TABLE [dbo].[Pet](
	[Pet_ID] [int] IDENTITY(1,1) NOT NULL,
	[Pet_Name] [varchar](30) NOT NULL,
	[Pet_Type] [varchar](30) NOT NULL,
	[Pet_Age] [int] NOT NULL,
	[Owner_ID] [int] NOT NULL,
	[Last_Vist_Date] [date] NOT NULL,
 CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED 
(
	[Pet_ID] ASC
),
CONSTRAINT fk_PetOwner_ID Foreign Key (Owner_ID) references Owner(Owner_ID),
CONSTRAINT fk_Petpet_type Foreign Key (pet_Type) references Pricing(pet_type);





CREATE TABLE [dbo].[Pricing](
	[Pet_Type] [varchar](30) NOT NULL,
	[Procedure_ID] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Tax] [float] NOT NULL,
 CONSTRAINT [PK_Pricing] PRIMARY KEY CLUSTERED 
(
	[Pet_Type] ASC,
	[procedure_ID] 
);




CREATE TABLE [dbo].[Procedure](
	[Procedure_ID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Procedure] PRIMARY KEY CLUSTERED 
(
	[Procedure_ID] ASC
);




