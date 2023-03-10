USE [master]
GO

CREATE DATABASE [EnterpriseApp]
GO

USE [EnterpriseApp]
GO

/****** Object:  Sequence [dbo].[OrdersSequence]    Script Date: 04/05/2022 22:42:19 ******/
CREATE SEQUENCE [dbo].[OrdersSequence] 
 AS [int]
 START WITH 1000
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[Street] [varchar](200) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Complement] [varchar](250) NULL,
	[Neighbourhood] [varchar](100) NOT NULL,
	[Cep] [varchar](20) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartCustomer]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartCustomer](
	[Id] [uniqueidentifier] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[HasUsedVoucher] [bit] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[Percent] [decimal](18, 2) NULL,
	[DiscountValue] [decimal](18, 2) NULL,
	[VoucherCode] [varchar](50) NULL,
	[DiscountType] [int] NULL,
 CONSTRAINT [PK_CartCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[ShoppingCartId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Email] [varchar](254) NULL,
	[Cpf] [varchar](11) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductName] [varchar](250) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnityPrice] [decimal](18, 2) NOT NULL,
	[ProductImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [int] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[VoucherId] [uniqueidentifier] NULL,
	[HasUsedVoucher] [bit] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[Street] [nvarchar](max) NULL,
	[Number] [nvarchar](max) NULL,
	[Complement] [nvarchar](max) NULL,
	[Neighbourhood] [nvarchar](max) NULL,
	[Cep] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[PaymentType] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Active] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[InsertDate] [datetime2](7) NOT NULL,
	[Image] [varchar](250) NOT NULL,
	[StockQuantity] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityKeys]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityKeys](
	[Id] [uniqueidentifier] NOT NULL,
	[Parameters] [nvarchar](max) NULL,
	[KeyId] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[JwsAlgorithm] [nvarchar](max) NULL,
	[JweAlgorithm] [nvarchar](max) NULL,
	[JweEncryption] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[JwkType] [int] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
 CONSTRAINT [PK_SecurityKeys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [uniqueidentifier] NOT NULL,
	[AuthorizationCode] [nvarchar](max) NULL,
	[CreditCardBrand] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[Tax] [decimal](18, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[TID] [nvarchar](max) NULL,
	[NSU] [nvarchar](max) NULL,
	[PaymentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 04/05/2022 22:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Percent] [decimal](18, 2) NULL,
	[DiscountValue] [decimal](18, 2) NULL,
	[Quantity] [int] NOT NULL,
	[DiscountType] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UsedDate] [datetime2](7) NULL,
	[MaximumValidationDate] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[Used] [bit] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211112172824_Initial', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211130142957_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211130144556_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211208112419_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220128172106_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220224011925_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309110527_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220311104533_Two', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220311104805_Three', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220331232007_One', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419110832_one', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220421161325_two', N'5.0.12')
GO
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State], [CustomerId]) VALUES (N'9bc610ca-1eb8-49cc-f514-08da1173c5c9', N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP', N'57243197-bca9-424f-b629-6ca26dc45673')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'57243197-bca9-424f-b629-6ca26dc45673', N'Catalog', N'View')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5407aeff-de0f-4e3a-b3eb-95c860bcd959', N'teste12@teste.com', N'TESTE12@TESTE.COM', N'teste12@teste.com', N'TESTE12@TESTE.COM', 1, N'AQAAAAEAACcQAAAAELHk/w88GnfJZjPAr3/fSCx76iXBYyobrWn/7Sp3hbchC5q6Edxq8oLaR1TWU4ItKA==', N'D3DTRTL346PWGRQB7AH44ISAL72WZLFD', N'737b2c62-f303-433c-8612-988712d65efe', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'57243197-bca9-424f-b629-6ca26dc45673', N'teste@teste.com', N'TESTE@TESTE.COM', N'teste@teste.com', N'TESTE@TESTE.COM', 1, N'AQAAAAEAACcQAAAAEBfqZQ4fS8zXXS51ypNAcdPCdziw3RlosJFZV5YSVfoQKu5ijwXZ4GvaY33sVD266Q==', N'7MZZM3BZKKAPXHC2FD6H26XDPKP72B4Y', N'38e69543-993b-4573-82cd-e6b0e7628ef9', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Cpf]) VALUES (N'57243197-bca9-424f-b629-6ca26dc45673', N'Tiago', N'teste@teste.com', N'48499434894')
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Cpf]) VALUES (N'5407aeff-de0f-4e3a-b3eb-95c860bcd959', N'Tiagodfgdfggala Baraúna g', N'teste12@teste.com', N'85804010034')
GO
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [UnityPrice], [ProductImage]) VALUES (N'aec29e5b-ba18-46af-83d9-69daaf974e05', N'759e3fdf-8579-49ba-9285-14233e3cbfd6', N'7d67df76-2d4e-4a47-a19c-08eb80a9060b', N'Camiseta Code Life Preta', 4, CAST(90.00 AS Decimal(18, 2)), N'camiseta2.jpg')
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [UnityPrice], [ProductImage]) VALUES (N'c95cf0eb-9149-489d-8b81-8ce4d6220a15', N'fd410304-41de-4b0c-a5f1-c91ded0d65a3', N'6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb', N'Camiseta Debugar Preta', 1, CAST(75.00 AS Decimal(18, 2)), N'camiseta4.jpg')
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [UnityPrice], [ProductImage]) VALUES (N'e848bc11-f7e2-4ad4-a9eb-c132250d3455', N'82c21d39-db1d-4a16-a657-78b915fbc4ce', N'6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb', N'Camiseta Code Life Cinza', 4, CAST(80.00 AS Decimal(18, 2)), N'camiseta3.jpg')
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [UnityPrice], [ProductImage]) VALUES (N'702dbe5e-57f8-4205-8af3-e5078290d8d4', N'c1917c6d-d6fe-42c6-a9c2-9efdef971802', N'6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb', N'Camiseta Code Life Cinza', 3, CAST(80.00 AS Decimal(18, 2)), N'camiseta3.jpg')
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [UnityPrice], [ProductImage]) VALUES (N'faa150f9-8390-4a0a-963f-e7a21bfd5f4a', N'28792145-f199-4382-ae55-922dddfc3194', N'7d67df76-2d4e-4a47-a19c-08eb80a9060b', N'Camiseta Code Life Preta', 3, CAST(90.00 AS Decimal(18, 2)), N'camiseta2.jpg')
GO
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasUsedVoucher], [Discount], [TotalPrice], [CreationDate], [OrderStatus], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State]) VALUES (N'759e3fdf-8579-49ba-9285-14233e3cbfd6', 1067, N'57243197-bca9-424f-b629-6ca26dc45673', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(360.00 AS Decimal(18, 2)), CAST(N'2022-05-04T22:38:17.7956663' AS DateTime2), 2, N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP')
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasUsedVoucher], [Discount], [TotalPrice], [CreationDate], [OrderStatus], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State]) VALUES (N'82c21d39-db1d-4a16-a657-78b915fbc4ce', 1017, N'57243197-bca9-424f-b629-6ca26dc45673', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(320.00 AS Decimal(18, 2)), CAST(N'2022-04-16T18:44:13.3725494' AS DateTime2), 5, N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP')
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasUsedVoucher], [Discount], [TotalPrice], [CreationDate], [OrderStatus], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State]) VALUES (N'28792145-f199-4382-ae55-922dddfc3194', 1016, N'57243197-bca9-424f-b629-6ca26dc45673', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(270.00 AS Decimal(18, 2)), CAST(N'2022-04-16T18:33:25.9244948' AS DateTime2), 2, N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP')
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasUsedVoucher], [Discount], [TotalPrice], [CreationDate], [OrderStatus], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State]) VALUES (N'c1917c6d-d6fe-42c6-a9c2-9efdef971802', 1018, N'57243197-bca9-424f-b629-6ca26dc45673', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), CAST(N'2022-04-16T18:48:57.9713182' AS DateTime2), 2, N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP')
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasUsedVoucher], [Discount], [TotalPrice], [CreationDate], [OrderStatus], [Street], [Number], [Complement], [Neighbourhood], [Cep], [City], [State]) VALUES (N'fd410304-41de-4b0c-a5f1-c91ded0d65a3', 1066, N'57243197-bca9-424f-b629-6ca26dc45673', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(75.00 AS Decimal(18, 2)), CAST(N'2022-05-03T23:05:01.9974762' AS DateTime2), 2, N'Rua Alfredo Melo de Aurélio Barros', N'1003', NULL, N'Jardim Antonio Maria', N'013456-120', N'São Paulo', N'SP')
GO
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Price]) VALUES (N'cda24185-0f71-4acb-b266-1471886329ca', N'28792145-f199-4382-ae55-922dddfc3194', 1, CAST(270.00 AS Decimal(18, 2)))
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Price]) VALUES (N'3a6a843f-7fd3-4e06-a0c5-1d6e58d99a7c', N'fd410304-41de-4b0c-a5f1-c91ded0d65a3', 1, CAST(75.00 AS Decimal(18, 2)))
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Price]) VALUES (N'978dcb5b-3652-4d95-8471-24ed918bda95', N'759e3fdf-8579-49ba-9285-14233e3cbfd6', 1, CAST(360.00 AS Decimal(18, 2)))
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Price]) VALUES (N'4108d15b-2a65-431d-bb34-3ba094b1c2a0', N'82c21d39-db1d-4a16-a657-78b915fbc4ce', 1, CAST(320.00 AS Decimal(18, 2)))
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Price]) VALUES (N'b57bb84a-5433-48b1-88a9-b54dc35b130a', N'c1917c6d-d6fe-42c6-a9c2-9efdef971802', 1, CAST(240.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'7d67df76-2d4e-4a47-a19c-08eb80a9060b', N'Camiseta Code Life Preta', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(90.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta2.jpg', 87)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'78162be3-61c4-4959-89d7-5ebfb476427e', N'Caneca No Coffee No Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(50.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca4.jpg', 100)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb', N'Camiseta Debugar Preta', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(75.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta4.jpg', 149)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb', N'Camiseta Code Life Cinza', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(80.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta3.jpg', 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'52dd696b-0882-4a73-9525-6af55dd416a4', N'Caneca Star Bugs Coffee', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca1.jpg', 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'191ddd3e-acd4-4c3b-ae74-8e473993c5da', N'Caneca Programmer Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(15.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca2.jpg', 1)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'fc184e11-014c-4978-aa10-9eb5e1af369b', N'Camiseta Software Developer', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(100.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta1.jpg', 9)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Active], [Price], [InsertDate], [Image], [StockQuantity]) VALUES (N'20e08cd4-2402-4e76-a3c9-a026185b193d', N'Caneca Turn Coffee in Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca3.jpg', 0)
GO
INSERT [dbo].[RefreshTokens] ([Id], [Username], [Token], [ExpirationDate]) VALUES (N'ecb546a6-f756-476e-9a14-0295a9223fbd', N'teste@teste.com', N'49137a1e-c7e6-476d-aff7-f7e6f79493fc', CAST(N'2022-05-05T09:06:15.9059852' AS DateTime2))
GO
INSERT [dbo].[SecurityKeys] ([Id], [Parameters], [KeyId], [Type], [JwsAlgorithm], [JweAlgorithm], [JweEncryption], [CreationDate], [JwkType], [IsRevoked]) VALUES (N'cf02ee71-d65a-476f-960b-97b51e39a1eb', N'{"AdditionalData":{},"Crv":"P-256","D":"LvsXGvuspNIIC5CwFSqyIA0WrbT0e75NS3eUrXYYrwU","KeyId":"gCH4VqZ3pHIfC9v2UAmyrg","KeyOps":[],"Kid":"gCH4VqZ3pHIfC9v2UAmyrg","Kty":"EC","X":"_VT8rxcK8m-NxFQjKJfPwHDbSlwoWi__p0sTJf98WmU","X5c":[],"Y":"j4fhiGESnXZzNe_VLi9JcilkghNSIa2eStDwKpssYhE","KeySize":256,"HasPrivateKey":true,"CryptoProviderFactory":{"CryptoProviderCache":{},"CacheSignatureProviders":true,"SignatureProviderObjectPoolCacheSize":32}}', N'gCH4VqZ3pHIfC9v2UAmyrg', N'EC', N'ES256', NULL, NULL, CAST(N'2022-04-19T08:25:22.5069381' AS DateTime2), 0, 0)
GO
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'1cf05052-96a8-42e9-bfa0-02faafa4cd69', N'R3SFLB6PE8', N'MasterCard', CAST(N'2022-05-03T23:05:01.9176943' AS DateTime2), CAST(75.00 AS Decimal(18, 2)), CAST(2.25 AS Decimal(18, 2)), 1, N'PAXIK3ZC0J', N'OMAV9PTXMH', N'3a6a843f-7fd3-4e06-a0c5-1d6e58d99a7c')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'26380179-b320-44dc-8400-25d4816ac5bc', N'WW0QBV2TRC', N'MasterCard', CAST(N'2022-04-16T18:40:49.5142437' AS DateTime2), CAST(270.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'KW4WI7328D', N'9CZL1ULN1S', N'cda24185-0f71-4acb-b266-1471886329ca')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'e38c2e2f-8f09-4f1f-93b1-2f2706576456', N'S01BGTWELZ', N'MasterCard', CAST(N'2022-05-04T22:38:14.6702625' AS DateTime2), CAST(360.00 AS Decimal(18, 2)), CAST(10.80 AS Decimal(18, 2)), 1, N'NTE5NYHIXA', N'OVTJUOEMX3', N'978dcb5b-3652-4d95-8471-24ed918bda95')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'338c4080-4e75-4e27-8015-2f30d72e19aa', N'ANVN0XU7SR', N'MasterCard', CAST(N'2022-05-03T23:05:03.2189174' AS DateTime2), CAST(75.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'PAXIK3ZC0J', N'OMAV9PTXMH', N'3a6a843f-7fd3-4e06-a0c5-1d6e58d99a7c')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'3cea2e8b-1401-4ce4-9a40-3141f6f54148', N'08PSAE5J4A', N'MasterCard', CAST(N'2022-04-16T18:44:13.2222324' AS DateTime2), CAST(320.00 AS Decimal(18, 2)), CAST(9.60 AS Decimal(18, 2)), 1, N'OE7RWZ9F9T', N'OS3S9WGZ4Y', N'4108d15b-2a65-431d-bb34-3ba094b1c2a0')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'a05f533a-8189-48c1-bf70-5e4d27effe95', N'Y2RZ3GICOE', N'MasterCard', CAST(N'2022-05-04T22:38:25.3988871' AS DateTime2), CAST(360.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'NTE5NYHIXA', N'OVTJUOEMX3', N'978dcb5b-3652-4d95-8471-24ed918bda95')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'1d26a731-20c2-4275-ae41-793325464215', N'DLSFMYTQ8A', N'MasterCard', CAST(N'2022-04-16T18:48:57.9289143' AS DateTime2), CAST(240.00 AS Decimal(18, 2)), CAST(7.20 AS Decimal(18, 2)), 1, N'FDOP1OR0AW', N'T9N249MYO3', N'b57bb84a-5433-48b1-88a9-b54dc35b130a')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'69f85a5b-732c-4c20-9729-8cd57c63a373', N'OZULNL0LJ4', N'MasterCard', CAST(N'2022-04-16T18:49:12.4526662' AS DateTime2), CAST(240.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'FDOP1OR0AW', N'T9N249MYO3', N'b57bb84a-5433-48b1-88a9-b54dc35b130a')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'4ff1f86e-a863-4a2e-9e84-bd3440f405ed', N'1PUMZ568PK', N'MasterCard', CAST(N'2022-04-16T18:45:08.0558106' AS DateTime2), CAST(320.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'OE7RWZ9F9T', N'OS3S9WGZ4Y', N'4108d15b-2a65-431d-bb34-3ba094b1c2a0')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'f2af8238-0049-406d-b1f8-e400d31abe8d', N'', N'MasterCard', CAST(N'2022-05-03T23:04:19.7030383' AS DateTime2), CAST(320.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 5, N'OE7RWZ9F9T', N'OS3S9WGZ4Y', N'4108d15b-2a65-431d-bb34-3ba094b1c2a0')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'c1a4766f-dc09-4b9a-9dc2-e97dc364adea', N'9EAAXOEX2S', N'MasterCard', CAST(N'2022-04-16T18:33:06.0508082' AS DateTime2), CAST(270.00 AS Decimal(18, 2)), CAST(8.10 AS Decimal(18, 2)), 1, N'KW4WI7328D', N'9CZL1ULN1S', N'cda24185-0f71-4acb-b266-1471886329ca')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardBrand], [Date], [TotalPrice], [Tax], [Status], [TID], [NSU], [PaymentId]) VALUES (N'38eefc23-75c1-462b-84c1-f937cbb026ef', N'', N'MasterCard', CAST(N'2022-04-16T19:05:40.7799286' AS DateTime2), CAST(320.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 5, N'OE7RWZ9F9T', N'OS3S9WGZ4Y', N'4108d15b-2a65-431d-bb34-3ba094b1c2a0')
GO
INSERT [dbo].[Vouchers] ([Id], [Code], [Percent], [DiscountValue], [Quantity], [DiscountType], [CreationDate], [UsedDate], [MaximumValidationDate], [Active], [Used]) VALUES (N'9d6d4769-ddbc-4965-aba5-56463c308f6c', N'10-OFF-GERAL', CAST(10.00 AS Decimal(18, 2)), NULL, 50, 0, CAST(N'2022-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Vouchers] ([Id], [Code], [Percent], [DiscountValue], [Quantity], [DiscountType], [CreationDate], [UsedDate], [MaximumValidationDate], [Active], [Used]) VALUES (N'8659301f-3649-425f-a126-692009584c3c', N'50-OFF-GERAL', CAST(50.00 AS Decimal(18, 2)), NULL, 50, 0, CAST(N'2022-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Vouchers] ([Id], [Code], [Percent], [DiscountValue], [Quantity], [DiscountType], [CreationDate], [UsedDate], [MaximumValidationDate], [Active], [Used]) VALUES (N'3e0faec3-61b2-4eae-9858-efb89cc8a416', N'150-OFF-GERAL', NULL, CAST(150.00 AS Decimal(18, 2)), 36, 1, CAST(N'2022-01-01T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), 1, 0)
GO
/****** Object:  Index [IX_Addresses_CustomerId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Addresses_CustomerId] ON [dbo].[Addresses]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 04/05/2022 22:42:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 04/05/2022 22:42:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Customer]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IDX_Customer] ON [dbo].[CartCustomer]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItems_ShoppingCartId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_CartItems_ShoppingCartId] ON [dbo].[CartItems]
(
	[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_VoucherId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_VoucherId] ON [dbo].[Orders]
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_PaymentId]    Script Date: 04/05/2022 22:42:19 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_PaymentId] ON [dbo].[Transactions]
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (NEXT VALUE FOR [OrdersSequence]) FOR [Code]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Customers_CustomerId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_CartCustomer_ShoppingCartId] FOREIGN KEY([ShoppingCartId])
REFERENCES [dbo].[CartCustomer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_CartCustomer_ShoppingCartId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Vouchers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Vouchers_VoucherId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Payments_PaymentId] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Payments_PaymentId]
GO
USE [master]
GO
ALTER DATABASE [EnterpriseApp] SET  READ_WRITE 
GO
