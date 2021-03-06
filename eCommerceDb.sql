USE [master]
GO
/****** Object:  Database [eCommerceDb]    Script Date: 13.08.2021 21:49:02 ******/
CREATE DATABASE [eCommerceDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eCommerceDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\eCommerceDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'eCommerceDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\eCommerceDb.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [eCommerceDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eCommerceDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eCommerceDb] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [eCommerceDb] SET ANSI_NULLS ON 
GO
ALTER DATABASE [eCommerceDb] SET ANSI_PADDING ON 
GO
ALTER DATABASE [eCommerceDb] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [eCommerceDb] SET ARITHABORT ON 
GO
ALTER DATABASE [eCommerceDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [eCommerceDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eCommerceDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eCommerceDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eCommerceDb] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [eCommerceDb] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [eCommerceDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eCommerceDb] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [eCommerceDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eCommerceDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [eCommerceDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eCommerceDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eCommerceDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eCommerceDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eCommerceDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eCommerceDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eCommerceDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eCommerceDb] SET RECOVERY FULL 
GO
ALTER DATABASE [eCommerceDb] SET  MULTI_USER 
GO
ALTER DATABASE [eCommerceDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eCommerceDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eCommerceDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eCommerceDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [eCommerceDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [eCommerceDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [eCommerceDb] SET QUERY_STORE = OFF
GO
USE [eCommerceDb]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryTranslations]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryTranslations](
	[CategoryId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[CategoryTranslation] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTranslations]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTranslations](
	[ProductId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13.08.2021 21:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Category]) VALUES (1, N'Snack')
INSERT [dbo].[Categories] ([Id], [Category]) VALUES (2, N'Electronics')
INSERT [dbo].[Categories] ([Id], [Category]) VALUES (3, N'Outfit')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[CategoryTranslations] ([CategoryId], [LanguageId], [CategoryTranslation]) VALUES (1, 2, N'Atıştırmalık')
INSERT [dbo].[CategoryTranslations] ([CategoryId], [LanguageId], [CategoryTranslation]) VALUES (2, 2, N'Elektronik Eşya')
INSERT [dbo].[CategoryTranslations] ([CategoryId], [LanguageId], [CategoryTranslation]) VALUES (3, 2, N'Kıyafet')
GO
SET IDENTITY_INSERT [dbo].[Languages] ON 

INSERT [dbo].[Languages] ([Id], [Language]) VALUES (1, N'english')
INSERT [dbo].[Languages] ([Id], [Language]) VALUES (2, N'türkçe')
INSERT [dbo].[Languages] ([Id], [Language]) VALUES (3, N'deutsch')
INSERT [dbo].[Languages] ([Id], [Language]) VALUES (4, N'français')
SET IDENTITY_INSERT [dbo].[Languages] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (1, N'Chocolate', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (2, N'Potato Chips', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (3, N'Candy', 1)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (4, N'Cell Phone', 2)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (5, N'Hair Dryer', 2)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId]) VALUES (6, N'Sweatshirt', 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (1, 2, N'Çikolata')
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (2, 2, N'Patates Cipsi')
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (3, 2, N'Şekerleme')
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (4, 2, N'Cep Telefonu')
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (5, 2, N'Saç Kurutma Makinesi')
INSERT [dbo].[ProductTranslations] ([ProductId], [LanguageId], [Name]) VALUES (6, 2, N'Kazak')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password]) VALUES (1, N'kaan', N'123')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[CategoryTranslations]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[CategoryTranslations]  WITH CHECK ADD FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[ProductTranslations]  WITH CHECK ADD FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProductTranslations]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
USE [master]
GO
ALTER DATABASE [eCommerceDb] SET  READ_WRITE 
GO
