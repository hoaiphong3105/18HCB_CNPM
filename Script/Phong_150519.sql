USE [master]
GO
/****** Object:  Database [RestaurantManagement]    Script Date: 2019-05-15 8:57:51 PM ******/
CREATE DATABASE [RestaurantManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestaurantManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\RestaurantManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RestaurantManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\RestaurantManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RestaurantManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestaurantManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestaurantManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestaurantManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestaurantManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestaurantManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestaurantManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RestaurantManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestaurantManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestaurantManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestaurantManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestaurantManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestaurantManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestaurantManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestaurantManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RestaurantManagement] SET  MULTI_USER 
GO
ALTER DATABASE [RestaurantManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestaurantManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestaurantManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestaurantManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RestaurantManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestaurantManagement] SET QUERY_STORE = OFF
GO
USE [RestaurantManagement]
GO
/****** Object:  Table [dbo].[FoodResource]    Script Date: 2019-05-15 8:57:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodResource](
	[FoodId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Measure] [nvarchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_FoodResource] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupUser]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [int] NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[FoodId] [int] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Material] [text] NOT NULL,
	[Price] [money] NOT NULL,
	[IsCombo] [bit] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuToday]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuToday](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_MenuToday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[TableId] [int] NOT NULL,
	[OrderTime] [datetime] NOT NULL,
	[Surcharge] [money] NOT NULL,
	[TotalPrice] [money] NOT NULL,
	[ServantId] [int] NOT NULL,
	[PaymentStatus] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[MeniItemId] [int] NOT NULL,
	[Note] [text] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_OrderItem_1] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpendingMoney]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpendingMoney](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[TotalPrice] [money] NOT NULL,
	[Reason] [nvarchar](500) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_SpendingMoney] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableList]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableList](
	[TableId] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TableList] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2019-05-15 8:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[DateOfBirth] [varchar](20) NULL,
	[Phone] [varchar](20) NULL,
	[GroupID] [int] NULL,
	[Salary] [money] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GroupUser] ON 

INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [Status]) VALUES (1, N'Admin', NULL, 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [Status]) VALUES (2, N'Quan ly', N'Quản lý', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [Status]) VALUES (3, N'Dau bep', N'Đầu bếp', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [Status]) VALUES (4, N'Thu ngan', N'Thu ngân', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [Status]) VALUES (5, N'Phuc vu', N'Phục vụ', 1)
SET IDENTITY_INSERT [dbo].[GroupUser] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [UserName], [FullName], [Password], [Address], [DateOfBirth], [Phone], [GroupID], [Salary], [Status]) VALUES (1, N'phucvu1', N'Kiều Phong', N'dc35caeb3345f7d58fb2f38c9f8e7573', N'ở xa lắm', NULL, NULL, 5, NULL, 1)
INSERT [dbo].[User] ([ID], [UserName], [FullName], [Password], [Address], [DateOfBirth], [Phone], [GroupID], [Salary], [Status]) VALUES (2, N'daubep1', N'Trương Vô Kỵ', N'07679cbd4d5e8ecf3d1c07a4592066e8', N'như trên', NULL, NULL, 3, NULL, 1)
INSERT [dbo].[User] ([ID], [UserName], [FullName], [Password], [Address], [DateOfBirth], [Phone], [GroupID], [Salary], [Status]) VALUES (3, N'thungan1', N'Triệu Mẫn', N'f9f21000a5042f3420841780bbbcc1ea', NULL, NULL, NULL, 4, NULL, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_FoodResource] FOREIGN KEY([FoodId])
REFERENCES [dbo].[FoodResource] ([FoodId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_FoodResource]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Menu] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[Menu] ([MenuItemId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Menu]
GO
ALTER TABLE [dbo].[MenuToday]  WITH CHECK ADD  CONSTRAINT [FK_MenuToday_Menu] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[Menu] ([MenuItemId])
GO
ALTER TABLE [dbo].[MenuToday] CHECK CONSTRAINT [FK_MenuToday_Menu]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_TableList] FOREIGN KEY([TableId])
REFERENCES [dbo].[TableList] ([TableId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_TableList]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([ServantId])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Menu] FOREIGN KEY([MeniItemId])
REFERENCES [dbo].[Menu] ([MenuItemId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Menu]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_GroupUser] FOREIGN KEY([GroupID])
REFERENCES [dbo].[GroupUser] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_GroupUser]
GO
USE [master]
GO
ALTER DATABASE [RestaurantManagement] SET  READ_WRITE 
GO
