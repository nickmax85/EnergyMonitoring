USE [master]
GO
/****** Object:  Database [EnergyMonitoring]    Script Date: 28.02.2020 14:23:06 ******/
CREATE DATABASE [EnergyMonitoring]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'energymonitoring', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\energymonitoring.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'energymonitoring_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\energymonitoring_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EnergyMonitoring] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EnergyMonitoring].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EnergyMonitoring] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET ARITHABORT OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EnergyMonitoring] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EnergyMonitoring] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EnergyMonitoring] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EnergyMonitoring] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET RECOVERY FULL 
GO
ALTER DATABASE [EnergyMonitoring] SET  MULTI_USER 
GO
ALTER DATABASE [EnergyMonitoring] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EnergyMonitoring] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EnergyMonitoring] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EnergyMonitoring] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EnergyMonitoring] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EnergyMonitoring', N'ON'
GO
ALTER DATABASE [EnergyMonitoring] SET QUERY_STORE = OFF
GO
USE [EnergyMonitoring]
GO
/****** Object:  User [root]    Script Date: 28.02.2020 14:23:06 ******/
CREATE USER [root] FOR LOGIN [root] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 28.02.2020 14:23:06 ******/
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
/****** Object:  Table [dbo].[Config]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[AuditDay] [int] NULL,
	[AuditTime] [datetime] NULL,
	[CostPerUnit] [decimal](6, 1) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Device]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Device](
	[DeviceID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IP] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[EquipmentID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_device] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[EquipmentID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](10) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[GroupID] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_machine] PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailGroup]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailGroup](
	[MailDistributionID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_MailDistribution] PRIMARY KEY CLUSTERED 
(
	[MailDistributionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Record]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Record](
	[RecordID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [decimal](6, 1) NOT NULL,
	[SensorID] [int] NOT NULL,
	[EquipmentID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_record] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sensor]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor](
	[SensorID] [int] NOT NULL,
	[LowerLimit] [decimal](18, 1) NULL,
	[UpperLimit] [decimal](18, 1) NULL,
	[UnitID] [int] NOT NULL,
	[DeviceID] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_sensor] PRIMARY KEY CLUSTERED 
(
	[SensorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sign] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_unit] PRIMARY KEY CLUSTERED 
(
	[UnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 28.02.2020 14:23:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Mail] [nchar](10) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Device] ON 

INSERT [dbo].[Device] ([DeviceID], [Name], [IP], [Active], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (1, N'WEBIO-087F68', N'10.187.136.29', 1, 1, NULL, CAST(N'2019-11-22T12:26:21.613' AS DateTime))
INSERT [dbo].[Device] ([DeviceID], [Name], [IP], [Active], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (2, N'WEBIO-087F6B', N'10.187.136.28', 0, 2, NULL, CAST(N'2019-11-22T12:26:21.613' AS DateTime))
INSERT [dbo].[Device] ([DeviceID], [Name], [IP], [Active], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (3, N'WUT0276', N'10.187.136.15', 0, 3, NULL, NULL)
INSERT [dbo].[Device] ([DeviceID], [Name], [IP], [Active], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (4, N'WUT0291', N'10.187.136.24', 0, 4, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Device] OFF
SET IDENTITY_INSERT [dbo].[Equipment] ON 

INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (1, N'512867', N'Montagelinie', 1, NULL, CAST(N'2019-11-22T12:26:42.040' AS DateTime))
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (2, N'512868', N'EOL', 1, NULL, CAST(N'2019-11-22T12:26:42.040' AS DateTime))
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (3, N'510239', N'CNC Aussenrundschleifmaschine GST 9', 2, NULL, CAST(N'2019-11-22T12:26:42.040' AS DateTime))
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (4, N'510428', N'CNC Walzmaschine Profiroll Rollex HP', 2, NULL, CAST(N'2019-11-22T12:26:42.040' AS DateTime))
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (5, N'510679', N'Vertikaldrehmaschine Scherer', 2, NULL, NULL)
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (7, N'510681', N'CNC Drehmaschine EMCO', 2, NULL, NULL)
INSERT [dbo].[Equipment] ([EquipmentID], [Number], [Name], [GroupID], [CreateDate], [UpdateDate]) VALUES (8, N'511058', N'Reinigungsanlage BUPI Toploader', 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Equipment] OFF
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([GroupID], [Name], [CreateDate], [UpdateDate]) VALUES (1, N'HMC Beveloid', NULL, CAST(N'2019-11-22T12:53:02.647' AS DateTime))
INSERT [dbo].[Group] ([GroupID], [Name], [CreateDate], [UpdateDate]) VALUES (2, N'FIAT GIORGIO Wellengruppe', NULL, CAST(N'2019-11-22T12:53:02.647' AS DateTime))
SET IDENTITY_INSERT [dbo].[Group] OFF
SET IDENTITY_INSERT [dbo].[Record] ON 

INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (1, CAST(6.7 AS Decimal(6, 1)), 2, 1, CAST(N'2020-02-25T12:08:42.037' AS DateTime), NULL)
INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (2, CAST(373.4 AS Decimal(6, 1)), 3, 1, CAST(N'2020-02-25T12:08:42.043' AS DateTime), NULL)
INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (3, CAST(6.7 AS Decimal(6, 1)), 2, 1, CAST(N'2020-02-25T12:08:47.050' AS DateTime), NULL)
INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (4, CAST(450.0 AS Decimal(6, 1)), 3, 1, CAST(N'2020-02-25T12:08:47.053' AS DateTime), NULL)
INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (11, CAST(6.7 AS Decimal(6, 1)), 2, 1, CAST(N'2020-02-25T13:11:31.730' AS DateTime), NULL)
INSERT [dbo].[Record] ([RecordID], [Value], [SensorID], [EquipmentID], [CreateDate], [UpdateDate]) VALUES (12, CAST(786.5 AS Decimal(6, 1)), 3, 1, CAST(N'2020-02-25T13:11:31.980' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Record] OFF
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (2, CAST(5.0 AS Decimal(18, 1)), CAST(7.0 AS Decimal(18, 1)), 1, 1, NULL, CAST(N'2019-11-22T11:28:05.063' AS DateTime))
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (3, CAST(0.0 AS Decimal(18, 1)), CAST(500.0 AS Decimal(18, 1)), 2, 1, NULL, CAST(N'2019-11-22T11:28:05.063' AS DateTime))
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (4, CAST(5.0 AS Decimal(18, 1)), CAST(7.0 AS Decimal(18, 1)), 1, 2, NULL, CAST(N'2019-11-22T11:28:05.063' AS DateTime))
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (5, CAST(0.0 AS Decimal(18, 1)), CAST(800.0 AS Decimal(18, 1)), 2, 2, NULL, CAST(N'2019-11-22T11:28:05.063' AS DateTime))
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (6, CAST(5.0 AS Decimal(18, 1)), CAST(7.0 AS Decimal(18, 1)), 1, 3, NULL, NULL)
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (7, CAST(0.0 AS Decimal(18, 1)), CAST(500.0 AS Decimal(18, 1)), 2, 3, NULL, NULL)
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (8, CAST(5.0 AS Decimal(18, 1)), CAST(7.0 AS Decimal(18, 1)), 1, 4, NULL, NULL)
INSERT [dbo].[Sensor] ([SensorID], [LowerLimit], [UpperLimit], [UnitID], [DeviceID], [CreateDate], [UpdateDate]) VALUES (9, CAST(0.0 AS Decimal(18, 1)), CAST(500.0 AS Decimal(18, 1)), 2, 4, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Unit] ON 

INSERT [dbo].[Unit] ([UnitID], [Name], [Sign], [CreateDate], [UpdateDate]) VALUES (1, N'Pressure', N'bar', NULL, CAST(N'2019-11-22T12:52:05.203' AS DateTime))
INSERT [dbo].[Unit] ([UnitID], [Name], [Sign], [CreateDate], [UpdateDate]) VALUES (2, N'Flow', N'l/min', NULL, CAST(N'2019-11-22T12:52:05.203' AS DateTime))
SET IDENTITY_INSERT [dbo].[Unit] OFF
ALTER TABLE [dbo].[Device] ADD  CONSTRAINT [DF_Device_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [FK_Device_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
GO
ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [FK_Device_Equipment]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Area] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([GroupID])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FK_Equipment_Area]
GO
ALTER TABLE [dbo].[MailGroup]  WITH CHECK ADD  CONSTRAINT [FK_Group_MailDistribution] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([GroupID])
GO
ALTER TABLE [dbo].[MailGroup] CHECK CONSTRAINT [FK_Group_MailDistribution]
GO
ALTER TABLE [dbo].[MailGroup]  WITH CHECK ADD  CONSTRAINT [FK_User_MailDistribution] FOREIGN KEY([MailDistributionID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[MailGroup] CHECK CONSTRAINT [FK_User_MailDistribution]
GO
ALTER TABLE [dbo].[Record]  WITH CHECK ADD  CONSTRAINT [FK_Record_Equipment] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
GO
ALTER TABLE [dbo].[Record] CHECK CONSTRAINT [FK_Record_Equipment]
GO
ALTER TABLE [dbo].[Record]  WITH CHECK ADD  CONSTRAINT [FK_Record_Sensor] FOREIGN KEY([SensorID])
REFERENCES [dbo].[Sensor] ([SensorID])
GO
ALTER TABLE [dbo].[Record] CHECK CONSTRAINT [FK_Record_Sensor]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_Device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[Device] ([DeviceID])
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_Sensor_Device]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_Unit] FOREIGN KEY([UnitID])
REFERENCES [dbo].[Unit] ([UnitID])
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_Sensor_Unit]
GO
USE [master]
GO
ALTER DATABASE [EnergyMonitoring] SET  READ_WRITE 
GO
