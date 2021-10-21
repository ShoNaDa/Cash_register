USE [master]
GO
/****** Object:  Database [Cash_register]    Script Date: 21.10.2021 14:33:58 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 21.10.2021 14:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[SalePrice] [money] NOT NULL,
	[ProductCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statements]    Script Date: 21.10.2021 14:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statements](
	[StatementsId] [int] IDENTITY(1,1) NOT NULL,
	[ShiftNumber] [int] NOT NULL,
	[MoneyInTheCashRegister] [money] NOT NULL,
	[MoneyAtTheBeginningOfTheShift] [money] NOT NULL,
	[Sales] [money] NOT NULL,
	[Refund] [money] NOT NULL,
	[Withdrawals] [money] NOT NULL,
	[Deposits] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatementsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 21.10.2021 14:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[WorkersId] [int] IDENTITY(1,1) NOT NULL,
	[LName] [nvarchar](50) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[MName] [nvarchar](50) NOT NULL,
	[RoleWorker] [nvarchar](15) NOT NULL,
	[PinCode] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductName], [SalePrice], [ProductCount]) VALUES (1, N'Молоко', 35.0000, 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Statements] ON 

INSERT [dbo].[Statements] ([StatementsId], [ShiftNumber], [MoneyInTheCashRegister], [MoneyAtTheBeginningOfTheShift], [Sales], [Refund], [Withdrawals], [Deposits]) VALUES (1, 1, 1000.0000, 900.0000, 100.0000, 0.0000, 0.0000, 0.0000)
SET IDENTITY_INSERT [dbo].[Statements] OFF
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([WorkersId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (1, N'Борковский', N'Владимир', N'Владимирович', N'Администратор', N'e67d2d057b1f3cb4f72a1c9c789746cd')
INSERT [dbo].[Workers] ([WorkersId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (5, N'Кашлач', N'Никита', N'Сергеевич', N'Кассир', N'05f64c9285b7d8d76dd7538b04eb1c5d')
INSERT [dbo].[Workers] ([WorkersId], [LName], [FName], [MName], [RoleWorker], [PinCode]) VALUES (7, N'Маразматик', N'Вячеслав', N'Магомедович', N'Кассир', N'5fa285e1bebe0a6623e33afc04a1fbd5')
SET IDENTITY_INSERT [dbo].[Workers] OFF
GO
USE [master]
GO
ALTER DATABASE [Cash_register] SET  READ_WRITE 
GO
