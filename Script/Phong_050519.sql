USE [RestaurantManagement]
GO
/****** Object:  Table [dbo].[GroupUser]    Script Date: 2019-05-05 1:31:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2019-05-05 1:31:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[FullName] [nvarchar](200) NULL,
	[Password] [varchar](200) NULL,
	[Address] [nvarchar](500) NOT NULL,
	[GroupID] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GroupUser] ON 

INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [IsActive]) VALUES (1, N'Admin', NULL, 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [IsActive]) VALUES (2, N'Quan ly', N'Quản lý', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [IsActive]) VALUES (3, N'Dau bep', N'Đầu bếp', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [IsActive]) VALUES (4, N'Thu ngan', N'Thu ngân', 1)
INSERT [dbo].[GroupUser] ([ID], [GroupName], [Description], [IsActive]) VALUES (5, N'Phuc vu', N'Phục vụ', 1)
SET IDENTITY_INSERT [dbo].[GroupUser] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [UserName], [FullName], [Password], [Address], [GroupID], [IsActive]) VALUES (1, N'phucvu1', N'Kiều Phong', N'dc35caeb3345f7d58fb2f38c9f8e7573', N'ở xa lắm', 5, 1)
INSERT [dbo].[User] ([ID], [UserName], [FullName], [Password], [Address], [GroupID], [IsActive]) VALUES (2, N'daubep1', N'Trương Vô Kỵ', N'07679cbd4d5e8ecf3d1c07a4592066e8', N'như trên', 3, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_GroupUser] FOREIGN KEY([GroupID])
REFERENCES [dbo].[GroupUser] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_GroupUser]
GO
