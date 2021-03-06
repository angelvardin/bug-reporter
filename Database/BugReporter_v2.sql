USE [master]
GO
/****** Object:  Database [BugReporter_v2]    Script Date: 02/17/2013 17:56:17 ******/
CREATE DATABASE [BugReporter_v2] ON  PRIMARY 
( NAME = N'BugReporter_v2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\BugReporter_v2.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BugReporter_v2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\BugReporter_v2_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BugReporter_v2] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BugReporter_v2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BugReporter_v2] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [BugReporter_v2] SET ANSI_NULLS OFF
GO
ALTER DATABASE [BugReporter_v2] SET ANSI_PADDING OFF
GO
ALTER DATABASE [BugReporter_v2] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [BugReporter_v2] SET ARITHABORT OFF
GO
ALTER DATABASE [BugReporter_v2] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [BugReporter_v2] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [BugReporter_v2] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [BugReporter_v2] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [BugReporter_v2] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [BugReporter_v2] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [BugReporter_v2] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [BugReporter_v2] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [BugReporter_v2] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [BugReporter_v2] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [BugReporter_v2] SET  DISABLE_BROKER
GO
ALTER DATABASE [BugReporter_v2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [BugReporter_v2] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [BugReporter_v2] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [BugReporter_v2] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [BugReporter_v2] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [BugReporter_v2] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [BugReporter_v2] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [BugReporter_v2] SET  READ_WRITE
GO
ALTER DATABASE [BugReporter_v2] SET RECOVERY SIMPLE
GO
ALTER DATABASE [BugReporter_v2] SET  MULTI_USER
GO
ALTER DATABASE [BugReporter_v2] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [BugReporter_v2] SET DB_CHAINING OFF
GO
USE [BugReporter_v2]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
	[Email] [nvarchar](80) NOT NULL,
	[Telephone] [nvarchar](56) NULL,
	[FirstName] [nvarchar](80) NOT NULL,
	[LastName] [nvarchar](80) NOT NULL,
	[LastActivityTime] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__UserProf__1788CC4C7F60ED59] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__UserProf__C9F28456023D5A04] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT [dbo].[UserProfile] ([UserId], [UserName], [Email], [Telephone], [FirstName], [LastName], [LastActivityTime], [IsActive]) VALUES (1, N'admin', N'admin@abv.bg', NULL, N'Angel', N'Ivanov', CAST(0x0000A16801267F76 AS DateTime), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [Email], [Telephone], [FirstName], [LastName], [LastActivityTime], [IsActive]) VALUES (2, N'Jordan', N'admin@admin.bg', NULL, N'Angel', N'Vardin', CAST(0x0000A1630160423F AS DateTime), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [Email], [Telephone], [FirstName], [LastName], [LastActivityTime], [IsActive]) VALUES (4, N'ivan_kanchev', N'admin@admin.bg', N'+359035533180', N'Ivan', N'Kanchev', CAST(0x0000A16600D07650 AS DateTime), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [Email], [Telephone], [FirstName], [LastName], [LastActivityTime], [IsActive]) VALUES (6, N'Administrator', N'admin@admin.bg', NULL, N'Admin', N'Admin', CAST(0x0000A166013A15FD AS DateTime), 1)
INSERT [dbo].[UserProfile] ([UserId], [UserName], [Email], [Telephone], [FirstName], [LastName], [LastActivityTime], [IsActive]) VALUES (7, N'User ', N'angelivanov@abv.bg', NULL, N'Angel', N'Ivanon', CAST(0x0000A167012BF729 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
/****** Object:  Table [dbo].[Projects]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](90) NOT NULL,
	[ProjectDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [ProjectDescription]) VALUES (1, N'OnlineGameShop', N'Online Game Shop for games')
SET IDENTITY_INSERT [dbo].[Projects] OFF
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A15D00A1D5EB AS DateTime), NULL, 1, CAST(0x0000A168010508A4 AS DateTime), 0, N'AEd8fnsdSvNwH00YGaIKwp4m6kaA3Oe7jo5IgJElzzIDHJvkj4fdLadh0nXjVYgcfA==', CAST(0x0000A15D00A1D5EB AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0x0000A15E0135BD96 AS DateTime), NULL, 1, CAST(0x0000A16301153769 AS DateTime), 0, N'AFstJebH/uC3YjcUOXvOUzd2kju6kEdRU38T9M0axFJ+nDhgfoXRVtH0SiyD8ESG2g==', CAST(0x0000A15E0135BD96 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (4, CAST(0x0000A1640104A2B1 AS DateTime), NULL, 1, CAST(0x0000A167016A31D1 AS DateTime), 0, N'AJWtJ/0/OKHCvrO8FdVtR+YAjMkIt7qEn+UQiKPBZtjhbIFVXBJ7d1XOJhy+0HrLiA==', CAST(0x0000A167016E062A AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (6, CAST(0x0000A166011788E2 AS DateTime), NULL, 1, NULL, 0, N'AIAV2ubACZGkJQHZjP94Uh4YZC3WPiFmVZ5EArnxTwsP+7SeGqOX9amnMwUdN2gp1w==', CAST(0x0000A166011788E2 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (7, CAST(0x0000A16601181840 AS DateTime), NULL, 1, NULL, 0, N'AKVdxSAlksViA3QORKE7VE9pHUg70Lt+Qd82HxTvqYNhY8yzGKLwVcFXhnqH9MAXlA==', CAST(0x0000A16601181840 AS DateTime), N'', NULL, NULL)
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (4, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (6, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (7, 2)
/****** Object:  Table [dbo].[Bugs]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[BugId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[BugDescription] [nvarchar](max) NULL,
	[Priority] [nvarchar](20) NOT NULL,
	[ProjectId] [int] NULL,
	[DateOfFirstSubmit] [datetime] NOT NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[BugId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bugs] ON
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (3, 1, N'In Progress', N'Linq query “Object reference not set to an instance of an object”', N'Normal', 1, CAST(0x0000A15F00AA22D2 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (5, 1, N'In Progress', N'Linq query “Object reference not set to an instance of an object”', N'Hight', 1, CAST(0x0000A15F00B19BE4 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (6, 1, N'In Progress', N'NullPointerException Thrown when an application attempts to use null in a case where an object is required. These include: Calling the instance method of a null object.Accessing 
', N'Normal', 1, CAST(0x0000A15F00B4C83F AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (7, 1, N'In Progress', N'NullPointerException Thrown when an application attempts to use null in a case where an object is required. These include: Calling the instance method of a null object.Accessing  
', N'Normal', 1, CAST(0x0000A15F00B5BFBA AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (8, 1, N'Fixed', N'The argument types ''Edm.Int32'' and ''Edm.String'' are incompatible for this operation. Near WHERE predicate', N'Normal', 1, CAST(0x0000A15F00B761B6 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (10, 4, N'New', N'Linq query “Object reference not set to an instance of an object”', N'Low', 1, CAST(0x0000A1630115EA40 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (11, 1, N'In Progress', N'The argument types ''Edm.Int32'' and ''Edm.String'' are incompatible for this operation. ', N'Normal', 1, CAST(0x0000A1630121CF22 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (12, 1, N'New', N'NullPointerException Thrown when an application attempts to use null in a case where an object is required. These include: Calling the instance method of a null object.Accessing', N'Low', 1, CAST(0x0000A16400C2522A AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (13, 1, N'New', N'Linq query “Object reference not set to an instance of an object”', N'Normal', 1, CAST(0x0000A16401107277 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (14, 7, N'New', N'Linq query “Object reference not set to an instance of an object”', N'Normal', 1, CAST(0x0000A165002990A9 AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (16, 7, N'In Progress', N'New Bug', N'Low', 1, CAST(0x0000A1660186887A AS DateTime))
INSERT [dbo].[Bugs] ([BugId], [UserId], [Status], [BugDescription], [Priority], [ProjectId], [DateOfFirstSubmit]) VALUES (17, 4, N'New', N'The argument types ''Edm.Int32'' and ''Edm.String'' are incompatible for this operation.', N'Low', 1, CAST(0x0000A1660186D518 AS DateTime))
SET IDENTITY_INSERT [dbo].[Bugs] OFF
/****** Object:  Table [dbo].[UsersInProjects]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInProjects](
	[UserId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_UsersInProjects] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UsersInProjects] ([UserId], [ProjectId]) VALUES (1, 1)
INSERT [dbo].[UsersInProjects] ([UserId], [ProjectId]) VALUES (2, 1)
INSERT [dbo].[UsersInProjects] ([UserId], [ProjectId]) VALUES (7, 1)
/****** Object:  Table [dbo].[Logs]    Script Date: 02/17/2013 17:56:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[BugId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Activity] [nvarchar](16) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Logs] ON
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (2, 8, 1, CAST(0x0000A15F00B761D9 AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (3, 5, 1, CAST(0x0000A15F0166E613 AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (4, 6, 1, CAST(0x0000A15F0169D46C AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (5, 7, 1, CAST(0x0000A15F016A359D AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (8, 10, 2, CAST(0x0000A1630115EAD7 AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (9, 11, 2, CAST(0x0000A1630121CF8D AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (10, 12, 1, CAST(0x0000A16400C2528C AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (11, 13, 1, CAST(0x0000A164011072ED AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (14, 11, 7, CAST(0x0000A166015A12E1 AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (15, 14, 7, CAST(0x0000A166016CF7EF AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (16, 16, 7, CAST(0x0000A166018688DB AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (17, 16, 7, CAST(0x0000A1660186C512 AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (18, 17, 7, CAST(0x0000A1660186D51E AS DateTime), N'Add')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (19, 12, 7, CAST(0x0000A167012B1215 AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (20, 13, 7, CAST(0x0000A167012B31CE AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (21, 10, 7, CAST(0x0000A167012BE7FA AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (22, 10, 1, CAST(0x0000A16801265F0E AS DateTime), N'Edit')
INSERT [dbo].[Logs] ([LogId], [BugId], [UserId], [Time], [Activity]) VALUES (23, 17, 1, CAST(0x0000A1680126778D AS DateTime), N'Edit')
SET IDENTITY_INSERT [dbo].[Logs] OFF
/****** Object:  Default [DF__webpages___IsCon__0BC6C43E]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
/****** Object:  Default [DF__webpages___Passw__0CBAE877]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
/****** Object:  ForeignKey [fk_RoleId]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
/****** Object:  ForeignKey [fk_UserId]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO
/****** Object:  ForeignKey [FK_Bugs_Projects]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ProjectId])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Projects]
GO
/****** Object:  ForeignKey [FK_Bugs_UserProfile]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_UserProfile]
GO
/****** Object:  ForeignKey [FK_UsersInProjects_Projects]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[UsersInProjects]  WITH CHECK ADD  CONSTRAINT [FK_UsersInProjects_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ProjectId])
GO
ALTER TABLE [dbo].[UsersInProjects] CHECK CONSTRAINT [FK_UsersInProjects_Projects]
GO
/****** Object:  ForeignKey [FK_UsersInProjects_UserProfile]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[UsersInProjects]  WITH CHECK ADD  CONSTRAINT [FK_UsersInProjects_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[UsersInProjects] CHECK CONSTRAINT [FK_UsersInProjects_UserProfile]
GO
/****** Object:  ForeignKey [FK_Logs_Bugs]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Bugs] FOREIGN KEY([BugId])
REFERENCES [dbo].[Bugs] ([BugId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Bugs]
GO
/****** Object:  ForeignKey [FK_Logs_UserProfile]    Script Date: 02/17/2013 17:56:19 ******/
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_UserProfile]
GO
