USE [master]
GO
/****** Object:  Database [Cash_register]    Script Date: 20.12.2021 19:42:16 ******/
CREATE DATABASE [Cash_register]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cash_register', FILENAME = N'C:\MSSQL server express\MSSQL15.SQLEXPRESS\MSSQL\DATA\Cash_register.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cash_register_log', FILENAME = N'C:\MSSQL server express\MSSQL15.SQLEXPRESS\MSSQL\DATA\Cash_register_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Cash_register] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cash_register].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cash_register] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cash_register] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cash_register] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cash_register] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cash_register] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cash_register] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Cash_register] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cash_register] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cash_register] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cash_register] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cash_register] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cash_register] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cash_register] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cash_register] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cash_register] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Cash_register] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cash_register] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cash_register] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cash_register] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cash_register] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cash_register] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cash_register] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cash_register] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cash_register] SET  MULTI_USER 
GO
ALTER DATABASE [Cash_register] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cash_register] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cash_register] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cash_register] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cash_register] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cash_register] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Cash_register] SET QUERY_STORE = OFF
GO
USE [Cash_register]
GO
/****** Object:  Table [dbo].[BalanceAfterCloseCashRegister]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BalanceAfterCloseCashRegister](
	[BalanceId] [int] IDENTITY(1,1) NOT NULL,
	[MoneyInTheCashRegister] [money] NOT NULL,
	[MoneyAtTheBeginningOfTheShift] [money] NOT NULL,
	[Withdrawals] [money] NOT NULL,
	[Deposits] [money] NOT NULL,
	[Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cheque]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cheque](
	[ChequeId] [int] IDENTITY(1,1) NOT NULL,
	[ChequeNumber] [int] NOT NULL,
	[FK_SaleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChequeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pament]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pament](
	[PamentId] [int] IDENTITY(1,1) NOT NULL,
	[TipOfPament] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductPrice] [money] NOT NULL,
	[ProductCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductsList]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsList](
	[ProductsListId] [int] IDENTITY(1,1) NOT NULL,
	[FK_ProductId] [int] NOT NULL,
	[FK_SaleId] [int] NOT NULL,
	[ProductCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductsListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Refunds]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Refunds](
	[RefundId] [int] IDENTITY(1,1) NOT NULL,
	[FK_ProductId] [int] NOT NULL,
	[ProductCount] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[RefundDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RefundId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[SaleId] [int] IDENTITY(1,1) NOT NULL,
	[FK_ShiftId] [int] NOT NULL,
	[FK_PamentId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[ShiftId] [int] IDENTITY(1,1) NOT NULL,
	[ShiftNumber] [int] NOT NULL,
	[FK_workerId] [int] NOT NULL,
	[FK_BalanceId] [int] NOT NULL,
	[CloseShiftDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 20.12.2021 19:42:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[WorkerId] [int] IDENTITY(1,1) NOT NULL,
	[LName] [nvarchar](50) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[MName] [nvarchar](50) NOT NULL,
	[RoleWorker] [nvarchar](15) NOT NULL,
	[PinCode] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BalanceAfterCloseCashRegister] ON 

INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (1, 1.9000, 0.0000, 1.1000, 3.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (2, 1.9000, 1.9000, 0.0000, 0.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (3, 1.9000, 1.9000, 0.0000, 0.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (4, 122.0000, 0.0000, 1.0000, 123.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (5, 122.0000, 122.0000, 0.0000, 0.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (6, 122.0000, 122.0000, 0.0000, 0.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (7, 122.0000, 122.0000, 0.0000, 0.0000, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (8, 568.2400, 122.0000, 0.0000, 0.0000, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (9, 522.0000, 568.2400, 0.0000, 0.0000, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (10, 1604.0400, 522.0000, 1.0000, 1.0000, CAST(N'2021-12-16' AS Date))
INSERT [dbo].[BalanceAfterCloseCashRegister] ([BalanceId], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Withdrawals], [Deposits], [Date]) VALUES (11, 1885.8400, 1604.0400, 0.0000, 0.0000, CAST(N'2021-12-16' AS Date))
SET IDENTITY_INSERT [dbo].[BalanceAfterCloseCashRegister] OFF
GO
SET IDENTITY_INSERT [dbo].[Cheque] ON 

INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (1, 1, 1)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (2, 2, 2)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (3, 3, 3)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (4, 4, 4)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (5, 5, 5)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (6, 6, 6)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (7, 7, 7)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (8, 8, 8)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (9, 9, 9)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (10, 10, 10)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (11, 11, 11)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (12, 12, 12)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (13, 13, 13)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (14, 14, 14)
INSERT [dbo].[Cheque] ([ChequeId], [ChequeNumber], [FK_SaleId]) VALUES (15, 15, 15)
SET IDENTITY_INSERT [dbo].[Cheque] OFF
GO
SET IDENTITY_INSERT [dbo].[Pament] ON 

INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (1, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (2, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (3, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (4, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (5, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (6, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (7, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (8, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (9, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (10, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (11, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (14, N'Банковская карта')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (15, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (16, N'Наличные')
INSERT [dbo].[Pament] ([PamentId], [TipOfPament]) VALUES (17, N'Банковская карта')
SET IDENTITY_INSERT [dbo].[Pament] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductName], [ProductPrice], [ProductCount]) VALUES (2, N'Конфеты за кг', 100.1200, 52)
INSERT [dbo].[Products] ([ProductId], [ProductName], [ProductPrice], [ProductCount]) VALUES (4, N'Энергетик', 76.4000, 38)
INSERT [dbo].[Products] ([ProductId], [ProductName], [ProductPrice], [ProductCount]) VALUES (5, N'Молоко', 76.0000, 32)
INSERT [dbo].[Products] ([ProductId], [ProductName], [ProductPrice], [ProductCount]) VALUES (6, N'Сыр "Российский"', 60.0000, 11)
INSERT [dbo].[Products] ([ProductId], [ProductName], [ProductPrice], [ProductCount]) VALUES (7, N'Хлеб', 25.5000, 12)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductsList] ON 

INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (2, 2, 2, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (3, 2, 3, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (10, 2, 8, 4)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (12, 4, 9, 2)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (13, 2, 9, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (14, 4, 10, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (15, 2, 10, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (16, 2, 11, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (17, 4, 11, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (18, 5, 12, 2)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (19, 4, 12, 1)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (20, 2, 13, 2)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (21, 2, 14, 3)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (22, 7, 14, 2)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (23, 4, 15, 2)
INSERT [dbo].[ProductsList] ([ProductsListId], [FK_ProductId], [FK_SaleId], [ProductCount]) VALUES (24, 6, 15, 3)
SET IDENTITY_INSERT [dbo].[ProductsList] OFF
GO
SET IDENTITY_INSERT [dbo].[Refunds] ON 

INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (1, 2, 1, 100.1200, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (2, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (4, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (5, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (29, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (31, 2, 4, 400.4800, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (32, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (33, 2, 1, 100.1200, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (38, 7, 2, 51.0000, CAST(N'2021-12-16' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (39, 2, 1, 100.1200, CAST(N'2021-12-16' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (40, 4, 2, 152.8000, CAST(N'2021-12-16' AS Date))
INSERT [dbo].[Refunds] ([RefundId], [FK_ProductId], [ProductCount], [Amount], [RefundDate]) VALUES (41, 7, 2, 51.0000, CAST(N'2021-12-18' AS Date))
SET IDENTITY_INSERT [dbo].[Refunds] OFF
GO
SET IDENTITY_INSERT [dbo].[Sale] ON 

INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (1, 8, 1, 123.0000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (2, 8, 2, 100.1200)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (3, 8, 3, 346.1200)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (4, 8, 4, 25.0000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (5, 9, 5, 25.0000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (6, 9, 6, 50.0000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (7, 9, 7, 25.0000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (8, 9, 8, 823.4800)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (9, 10, 9, 252.9200)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (10, 10, 10, 176.5200)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (11, 10, 11, 176.5200)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (12, 10, 14, 228.4000)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (13, 10, 15, 200.2400)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (14, 10, 16, 351.3600)
INSERT [dbo].[Sale] ([SaleId], [FK_ShiftId], [FK_PamentId], [Amount]) VALUES (15, 11, 17, 332.8000)
SET IDENTITY_INSERT [dbo].[Sale] OFF
GO
SET IDENTITY_INSERT [dbo].[Shift] ON 

INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (1, 1, 1, 1, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (2, 2, 1, 2, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (3, 3, 1, 3, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (4, 4, 2, 4, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (5, 5, 2, 5, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (6, 6, 2, 6, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (7, 7, 1, 7, CAST(N'2021-12-07' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (8, 8, 1, 8, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (9, 9, 2, 9, CAST(N'2021-12-08' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (10, 10, 1, 10, CAST(N'2021-12-16' AS Date))
INSERT [dbo].[Shift] ([ShiftId], [ShiftNumber], [FK_workerId], [FK_BalanceId], [CloseShiftDate]) VALUES (11, 11, 1, 11, CAST(N'2021-12-18' AS Date))
SET IDENTITY_INSERT [dbo].[Shift] OFF
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([WorkerId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (1, N'Борковский', N'Владимир', N'Владимирович', N'Администратор', N'e67d2d057b1f3cb4f72a1c9c789746cd')
INSERT [dbo].[Workers] ([WorkerId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (2, N'Кашлач', N'Никита', N'Сергеевич', N'Кассир', N'05f64c9285b7d8d76dd7538b04eb1c5d')
INSERT [dbo].[Workers] ([WorkerId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (3, N'Плотникова', N'Алена', N'Владимировна', N'Кассир', N'5fa285e1bebe0a6623e33afc04a1fbd5')
SET IDENTITY_INSERT [dbo].[Workers] OFF
GO
ALTER TABLE [dbo].[Cheque]  WITH CHECK ADD  CONSTRAINT [FK_SaleId_sales] FOREIGN KEY([FK_SaleId])
REFERENCES [dbo].[Sale] ([SaleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cheque] CHECK CONSTRAINT [FK_SaleId_sales]
GO
ALTER TABLE [dbo].[ProductsList]  WITH CHECK ADD  CONSTRAINT [FK_ProductId_Product] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsList] CHECK CONSTRAINT [FK_ProductId_Product]
GO
ALTER TABLE [dbo].[ProductsList]  WITH CHECK ADD  CONSTRAINT [FK_SaleId_Sale] FOREIGN KEY([FK_SaleId])
REFERENCES [dbo].[Sale] ([SaleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductsList] CHECK CONSTRAINT [FK_SaleId_Sale]
GO
ALTER TABLE [dbo].[Refunds]  WITH CHECK ADD  CONSTRAINT [FK_ProductId_Products] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Refunds] CHECK CONSTRAINT [FK_ProductId_Products]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_PamentId_Pament] FOREIGN KEY([FK_PamentId])
REFERENCES [dbo].[Pament] ([PamentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_PamentId_Pament]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_ShiftId_Shift] FOREIGN KEY([FK_ShiftId])
REFERENCES [dbo].[Shift] ([ShiftId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_ShiftId_Shift]
GO
ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_BalanceId_BalanceAfterCloseCashRegister] FOREIGN KEY([FK_BalanceId])
REFERENCES [dbo].[BalanceAfterCloseCashRegister] ([BalanceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_BalanceId_BalanceAfterCloseCashRegister]
GO
ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_workerId_Workers] FOREIGN KEY([FK_workerId])
REFERENCES [dbo].[Workers] ([WorkerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_workerId_Workers]
GO
USE [master]
GO
ALTER DATABASE [Cash_register] SET  READ_WRITE 
GO
