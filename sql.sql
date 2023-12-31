USE [master]
GO
/****** Object:  Database [EGift]    Script Date: 8/3/2023 12:53:30 PM ******/
CREATE DATABASE [EGift]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EGift', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\EGift.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EGift_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\EGift_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EGift] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EGift].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EGift] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EGift] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EGift] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EGift] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EGift] SET ARITHABORT OFF 
GO
ALTER DATABASE [EGift] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EGift] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EGift] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EGift] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EGift] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EGift] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EGift] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EGift] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EGift] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EGift] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EGift] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EGift] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EGift] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EGift] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EGift] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EGift] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EGift] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EGift] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EGift] SET  MULTI_USER 
GO
ALTER DATABASE [EGift] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EGift] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EGift] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EGift] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EGift] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EGift] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EGift] SET QUERY_STORE = OFF
GO
USE [EGift]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[AvatarUrl] [nvarchar](max) NULL,
	[CreateAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[Id] [uniqueidentifier] NOT NULL,
	[CartId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Phone] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
	[AvatarUrl] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[VerifyToken] [nvarchar](256) NOT NULL,
	[VerifyTime] [datetime] NULL,
	[CreateAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feverous]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feverous](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeverousItem]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeverousItem](
	[Id] [uniqueidentifier] NOT NULL,
	[FeverousId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](256) NOT NULL,
	[Amount] [int] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Phone] [nvarchar](256) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[Receiver] [nvarchar](256) NOT NULL,
	[VoucherId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__Order__3214EC070E0EF26E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK__OrderDet__3214EC07E9F53334] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Price] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](256) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImage]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImage](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](256) NULL,
	[Url] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [varchar](256) NOT NULL,
	[Discount] [float] NOT NULL,
	[Description] [varchar](256) NOT NULL,
	[Quantity] [int] NOT NULL,
	[FromPrice] [int] NOT NULL,
	[ToPrice] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CreateAt] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoucherMark]    Script Date: 8/3/2023 12:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherMark](
	[VoucherId] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_VoucherMark] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC,
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Cart] ([Id], [CustomerId]) VALUES (N'1b72b44e-cf98-4ed3-bd1c-1320ea4fe368', N'a2df9540-2858-47e3-aee7-13d376736b5c')
INSERT [dbo].[Cart] ([Id], [CustomerId]) VALUES (N'94997614-5519-4101-b6c2-98409cf92a9f', N'169023d7-84db-47d2-af0d-2c33d725e68e')
INSERT [dbo].[Cart] ([Id], [CustomerId]) VALUES (N'9dc48fe6-b5d1-4131-b098-fa7e942bd589', N'97c6000a-9f20-41d7-a514-4ea329c334b7')
INSERT [dbo].[Cart] ([Id], [CustomerId]) VALUES (N'a091edc2-fbd3-4975-ac0f-fef350b15a40', N'c65a5a62-3671-491e-a221-e82f5c720a07')
INSERT [dbo].[Cart] ([Id], [CustomerId]) VALUES (N'9b80a023-d7b9-41fa-8817-6250b5ecc4f9', N'564eb3ed-5747-40c6-872f-ee7f50d465c4')
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'9438499f-9234-4823-9727-066165ea8bcd', N'Accessory', N'Các phụ kiện blink blink', CAST(N'2023-06-06T14:59:30.187' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'4c7348eb-82d0-4d7e-b157-190b210297a2', N'Footwear', N'Giày dép nhập khẩu bền bỉ', CAST(N'2023-06-05T21:17:08.530' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'87f25a97-a1e2-4e3c-ad97-449f57b54263', N'Teddy', N'Gấu Bông quà tặng ý nghĩa', CAST(N'2023-06-04T19:52:41.780' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'f0aced50-b688-4640-9b92-5f29f0359fbb', N'Baby gift', N'Quà tặng dành cho các em bé nhỏ từ 4 đến 7 tuổi', CAST(N'2023-06-06T15:09:02.793' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0', N'Decorations', N'Các thể loại đồ trang trí trong nhà, tiệm trà, tiệm cafe', CAST(N'2023-06-06T14:55:24.420' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d', N'For men', N'Các quà tặng cho nam', CAST(N'2023-06-06T14:56:54.007' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'd73cd780-69d2-4308-bfd1-78b5535ab46c', N'Flower', N'Hoa dành cho các dịp lễ tết noel', CAST(N'2023-06-05T21:17:50.000' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'b4f9d436-8af9-4ab5-8591-7ef363d8670e', N'Water bottle', N'Bình giữ nhiệt', CAST(N'2023-07-13T10:03:56.100' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'a959ab8c-00a1-4cd1-99e3-8dd4dd711d7e', N'Cake', N'Bánh kem dành cho các dịp lễ', CAST(N'2023-06-06T14:53:51.197' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'a314db75-db24-4a09-91ec-a4431e840cbc', N'Clothes', N'Quần áo đẹp, chất lượng', CAST(N'2023-06-05T21:16:38.820' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreateAt]) VALUES (N'ac08f98b-0f2e-4209-8555-b42800d566db', N'For girl', N'Các quà tặng cho nữ', CAST(N'2023-06-06T14:57:24.000' AS DateTime))
GO
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'a2df9540-2858-47e3-aee7-13d376736b5c', N'employee1', N'123456', N'Employee.Egift', N'egift.employee@gmail.com', N'0000000000', N'EGFIT', NULL, 0, N'FC68D1271872D4D7F26D48E3D48371F08AF4AE73ACCAAFF310DFD438CFFF0F41FFEEC7D811C2918622334C2A04032C240FC73C050B0F5125047DC54382D4728F', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-06-22T13:39:10.010' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'169023d7-84db-47d2-af0d-2c33d725e68e', N'tantrung', N'123456', N'Nguyễn Tấn Trung', N'nguyentantrung1801@gmail.com', N'0000', N'Bình Phước', NULL, 0, N'F2B8D603E1BE3D1E817F9FF66F1AA5F610C3D4300C1664FA7E3599AC74FB84A5DE3780C704718D2F1A6EC529000E754584C4D146641224FC6FBC5B97E40D2D2A', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-06-22T11:11:53.740' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'f4545b0b-1808-4cd7-a8fb-49053b99cc06', N'thanhnam', N'123456', N'Nguyen Thanh Nam', N'nguyentantrung9179@gmail.com', N'', N'', NULL, 1, N'6654C55EC5E45C200CC27BE56026044B0E61F57B886CF5FDDD9AD252C556EAAFFA4F6B727BA05E949D2A9A8D3A9A5D4B4CF192DB0F3A0B9A5539A081B03C3679', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-07-13T15:42:16.463' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'97c6000a-9f20-41d7-a514-4ea329c334b7', N'congdoan', N'123456', N'Công Đoàn', N'congdoan@gmail.com', N'000000000', N'Hồ Chí Minh', NULL, 0, N'DB0C81CCCDE33BD2B8F9488D3B06310FFE29D582D9EB777250EC79D2F57DF653738ADAAEAB9C770B6A3A73A566DA9E94810BBB7AC4B942E55FC62D8F2338DACC', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-06-22T13:43:28.193' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'1a0e393f-a92b-4cc4-995d-b612283044c5', N'dat', N'123456', N'dat', N'datnqse62453@fpt.edu.vn', N'', N'', NULL, 0, N'4AEFED42C672F3EA6AC5768851D34DD1BA17B6459B7B9944BBCF7FD413396A06398B86B59C306DE313FC380A0C09B337D67C546947B818800BBD0C5B9F9B864C', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-07-13T15:40:25.993' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'aa4ad384-a46f-4d89-8524-e7c7f0d5f758', N'testne', N'123456', N'string', N'user111@example.com', N'', N'', NULL, 0, N'D4B12071E8146BD20F5A22BBBB541C0539F2090E5D5FE0CEB7E6C1643AE523F7E39992F9436B64418D8012053DFBDB5415434F2AD4D4DB7D910CFF457624F8EB', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-07-13T16:14:28.253' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'c65a5a62-3671-491e-a221-e82f5c720a07', N'minhvu', N'123456', N'Minh Vũ', N'minhvu@gmail.com', N'000000000', N'Hồ Chí Minh', NULL, 0, N'FBA4952D77DB7CD38E7C886DC247E392580C4EF14D5521FEAAC42C781AA1C951E37D7847E6E6A03C373DF867E34E43377EEB5B199878CF2BE870FA238B55DC52', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-06-22T13:43:49.280' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'c9d67c29-331a-4345-bd70-ea05c50b8657', N'datnq', N'123456', N'Quốc Đạt', N'user@example.com', N'00000', N'Tuy Hòa', NULL, 0, N'EAF109126633BC48F5ED15D3C535E9C812AB464343C589216D2AFC482C3ECD64D68B26AA67057998F454683E05E66EA4048D33A60CB5A0E1224279E9037E71D6', CAST(N'2023-07-13T15:42:28.640' AS DateTime), CAST(N'2023-06-23T11:32:34.337' AS DateTime))
INSERT [dbo].[Customer] ([Id], [Username], [Password], [Name], [Email], [Phone], [Address], [AvatarUrl], [IsActive], [VerifyToken], [VerifyTime], [CreateAt]) VALUES (N'564eb3ed-5747-40c6-872f-ee7f50d465c4', N'trung', N'123456', N'Nguyen Nam', N'nguyentantrung1801@gmail.com', N'0933247893', N'Thủ Dầu Một, Bình Dương', NULL, 1, N'536778E98DF4113E00AB5B3A46E18CBDA41E3F90ACAE6A2094F9A18A70E1291A8E703FF9E00D9926207B50BC8193F5E0DFF0A1C289CDE07EC13E7FE5B5D9589F', CAST(N'2023-06-22T15:02:58.327' AS DateTime), CAST(N'2023-06-22T15:02:38.240' AS DateTime))
GO
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'bd05626e-5c8d-4698-bd64-cbfef73b08c5', N'a2df9540-2858-47e3-aee7-13d376736b5c')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'c5449ada-b885-45eb-aed4-40d5189417f9', N'169023d7-84db-47d2-af0d-2c33d725e68e')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'1b5d8b65-d2d5-4458-bb21-ec93cd98840a', N'f4545b0b-1808-4cd7-a8fb-49053b99cc06')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'fb9c6e1b-312c-47d3-bf15-60573eda9e04', N'97c6000a-9f20-41d7-a514-4ea329c334b7')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'15c4d92c-b2dc-40c9-9924-b9c1d4e7779a', N'1a0e393f-a92b-4cc4-995d-b612283044c5')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'64cabe5d-661f-42db-b449-d6017fc13fbe', N'aa4ad384-a46f-4d89-8524-e7c7f0d5f758')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'a88ad7b2-28a2-43b6-b63f-fed3d20f1fb2', N'c65a5a62-3671-491e-a221-e82f5c720a07')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'9e69696e-7b42-4db2-bfc0-d580dd3f6186', N'c9d67c29-331a-4345-bd70-ea05c50b8657')
INSERT [dbo].[Feverous] ([Id], [CustomerId]) VALUES (N'6860eb27-93a4-47b6-8415-5144ab840922', N'564eb3ed-5747-40c6-872f-ee7f50d465c4')
GO
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'32c7d6a7-a6e3-425b-b87f-0ad739a8da30', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Canceled', 30000, N'string', N'string', N'string', 0, CAST(N'2023-08-03T10:36:38.917' AS DateTime), N'string', N'c4a02f0f-10b2-4e72-ad57-c06306ea0d87')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'dec2096e-9ad7-41e0-a5b8-46e955670b93', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Canceled', 295000, N'string', N'string', N'string', 0, CAST(N'2023-08-03T10:29:28.120' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'b8da7be6-d851-497c-9ce5-485cb101f3ae', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'nguyentantrung1801@gmail.com', N'0000', N'aaa', 0, CAST(N'2023-07-31T17:34:15.233' AS DateTime), N'Nguyễn Tấn Trung', NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'44adb492-0f28-41b7-b102-5638919dc58f', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'vuchatnguyen@gmail.com', N'0326598856', N'string', 0, CAST(N'2023-08-03T10:18:51.183' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'd3b9ceab-63e7-4007-a6c7-5f1b21aea52f', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 213000, N'string', N'string', N'string', 0, CAST(N'2023-08-03T12:10:26.873' AS DateTime), N'string', N'dcb4469a-f725-418e-866a-10d40144e282')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'e2077238-4568-4d2b-99d8-6812f1f99d65', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'vuchatnguyen@gmail.com', N'0326598856', N'string', 0, CAST(N'2023-08-03T10:17:09.720' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'c6a0ff9b-4401-4a1a-8e4e-68d82d1d067b', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Canceled', 177000, N'string', N'string', N'string', 0, CAST(N'2023-08-03T12:49:05.233' AS DateTime), N'string', N'dcb4469a-f725-418e-866a-10d40144e282')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'b5e9f814-99f0-4faa-9723-a2bdb43057c3', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 20000, N'nguyyenminhvu@gmail.com', N'string', N'string', 0, CAST(N'2023-08-03T10:22:32.000' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'ee041f7b-f5b1-4fa4-8b7a-a86fd3567b2d', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 1300000, N'nguyentantrung1801@gmail.com', N'0000', N'aaaa', 0, CAST(N'2023-07-31T17:36:36.033' AS DateTime), N'Nguyễn Tấn Trung', NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'a83039a8-525f-43a1-a951-b72776ac552d', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'nguyentantrung1801@gmail.com', N'0000', N'aaa', 0, CAST(N'2023-07-31T17:30:39.493' AS DateTime), N'Nguyễn Tấn Trung', NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'346015da-0ab5-410c-b96b-b80af01c7382', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'asdasdasd@gmail.com', N'0365849958', N'string', 0, CAST(N'2023-08-03T10:04:08.730' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'98641d1d-c6e4-4788-86f9-bffe9138abf3', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'asdasdasd@gmail.com', N'0365849958', N'string', 0, CAST(N'2023-08-03T10:05:40.150' AS DateTime), N'string', N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'bb442558-30cc-4f55-88df-c6e3ad50dc3d', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'nguyentantrung1801@gmail.com', N'0000', N'aaa', 0, CAST(N'2023-07-31T17:35:37.810' AS DateTime), N'Nguyễn Tấn Trung', NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'21493b00-2659-45db-8ec1-c82669c4e48c', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'string', N'string', N'string', 0, CAST(N'2023-08-03T10:11:13.720' AS DateTime), N'string', N'dcb4469a-f725-418e-866a-10d40144e282')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'e420ad20-eb9e-43ee-9342-cf345d6e6f4d', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 0, N'string', N'string', N'string', 0, CAST(N'2023-08-03T10:12:00.010' AS DateTime), N'string', N'c4a02f0f-10b2-4e72-ad57-c06306ea0d87')
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'9ec95605-540e-4a49-ba7e-dec213eeae40', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 350000, N'nguyentantrung1801@gmail.com', N'0000', N'aaa', 0, CAST(N'2023-07-31T17:19:04.080' AS DateTime), N'Nguyễn Tấn Trung', NULL)
INSERT [dbo].[Order] ([Id], [CustomerId], [Status], [Amount], [Email], [Phone], [Address], [IsPaid], [CreateAt], [Receiver], [VoucherId]) VALUES (N'3fbb29df-5c7f-409b-bf2d-e0ee49cf8bde', N'169023d7-84db-47d2-af0d-2c33d725e68e', N'Processing', 888000, N'nguyentantrung1801@gmail.com', N'0000', N'as', 0, CAST(N'2023-07-31T17:16:50.457' AS DateTime), N'Nguyễn Tấn Trung', NULL)
GO
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'a69c836d-f362-48a0-8c4e-0b7fe24e5b0a', N'c6a0ff9b-4401-4a1a-8e4e-68d82d1d067b', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 1, CAST(N'2023-08-03T12:49:05.233' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'fd73e0c1-1642-4ddc-b0e4-3c939fe65ecc', N'd3b9ceab-63e7-4007-a6c7-5f1b21aea52f', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 1, CAST(N'2023-08-03T12:10:26.913' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'9d142f47-f6f5-4af7-bf1a-4323c9ed9723', N'44adb492-0f28-41b7-b102-5638919dc58f', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:18:51.183' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'05123ebe-1fb9-475b-bcc0-5dcbe6db5158', N'd3b9ceab-63e7-4007-a6c7-5f1b21aea52f', N'411b30ac-82b7-45b0-969a-249fa310b486', 1, CAST(N'2023-08-03T12:10:26.873' AS DateTime), 60000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'9c68d2c9-e5b3-4085-bbc0-680b70be0d7e', N'346015da-0ab5-410c-b96b-b80af01c7382', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:04:08.730' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'2b3c8b83-96b1-4961-b3a2-69e0020c590a', N'3fbb29df-5c7f-409b-bf2d-e0ee49cf8bde', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-07-31T17:16:50.457' AS DateTime), 0)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'9adcae89-23dc-4a75-8e0a-6b6c0f23739e', N'21493b00-2659-45db-8ec1-c82669c4e48c', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:11:13.720' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'3492a929-134c-49a9-91b1-8a323f1330dd', N'e2077238-4568-4d2b-99d8-6812f1f99d65', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:17:09.720' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'1845c2ad-b1ce-4194-b8e5-954b28341e86', N'dec2096e-9ad7-41e0-a5b8-46e955670b93', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:29:28.120' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'b9a1940b-640d-48b6-95e5-9897734dee8b', N'ee041f7b-f5b1-4fa4-8b7a-a86fd3567b2d', N'411b30ac-82b7-45b0-969a-249fa310b486', 2, CAST(N'2023-07-31T17:36:36.033' AS DateTime), 60000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'32d96d55-ab19-49b6-b92c-9f5569607eb3', N'bb442558-30cc-4f55-88df-c6e3ad50dc3d', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 3, CAST(N'2023-07-31T17:35:37.810' AS DateTime), 0)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'c4a4114d-157e-4920-bf9c-a2a6e5179d8c', N'98641d1d-c6e4-4788-86f9-bffe9138abf3', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:05:40.150' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'753fe41e-ca45-457c-b796-bd634b6bd829', N'9ec95605-540e-4a49-ba7e-dec213eeae40', N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', 1, CAST(N'2023-07-31T17:19:04.080' AS DateTime), 0)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'abc61d05-df3c-4cda-97e9-cf26d7783c09', N'a83039a8-525f-43a1-a951-b72776ac552d', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', 2, CAST(N'2023-07-31T17:30:39.493' AS DateTime), 0)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'8d8d35d0-2db0-48b7-a967-d176ee303356', N'ee041f7b-f5b1-4fa4-8b7a-a86fd3567b2d', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 4, CAST(N'2023-07-31T17:36:36.050' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'951c8dfd-5bd1-45d9-9d47-d5e02e71599d', N'32c7d6a7-a6e3-425b-b87f-0ad739a8da30', N'411b30ac-82b7-45b0-969a-249fa310b486', 1, CAST(N'2023-08-03T10:36:38.917' AS DateTime), 60000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'9b81a261-2f3f-4477-8764-dfab09ff6722', N'b8da7be6-d851-497c-9ce5-485cb101f3ae', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', 2, CAST(N'2023-07-31T17:34:15.233' AS DateTime), 0)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'ec098030-f1a5-4f51-9ab7-e2513cb3f175', N'b5e9f814-99f0-4faa-9723-a2bdb43057c3', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:22:32.000' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'ef1fc3bd-409f-4ccb-bf07-ee92871b48c0', N'e420ad20-eb9e-43ee-9342-cf345d6e6f4d', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', 2, CAST(N'2023-08-03T10:12:00.010' AS DateTime), 295000)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity], [CreateAt], [Price]) VALUES (N'6568d6a7-055a-4b2f-8047-f752d20165d6', N'3fbb29df-5c7f-409b-bf2d-e0ee49cf8bde', N'feb3949f-8766-4080-bb69-3b8bb6a44a45', 2, CAST(N'2023-07-31T17:16:50.547' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', N'Fragrant Wax Bouquet with Teddy Bear', 295000, 425, N'Material: Made from soap and scented wax thinly dispersed by the skillful hands of artisans to become flowers with lifelike vitality up to 90% Model: Various hand-held flowers, flower baskets, boxes flowers, decorative flowers with diverse quantities and unique designs.', N'Active', CAST(N'2023-06-19T11:43:27.727' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'411b30ac-82b7-45b0-969a-249fa310b486', N'Model 04" crystal watch', 60000, 433, N'Model 04 crystal watch is a high-end watch product made from high-quality monolithic Crystal, size 15-20 cm. Crystal products are carefully selected, through many stages of processing, cutting, grinding, shaping and polishing. Model 04 crystal clock has modern and sophisticated beauty, good usability, easy to arrange in spaces. Crystal watches are an ideal choice for gifts.', N'Active', CAST(N'2023-06-19T11:19:26.953' AS DateTime), CAST(N'2023-06-19T11:21:16.017' AS DateTime))
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'e41899ea-dec7-4c97-b967-2a47b65311a8', N'Flower Gift Box', 2000000, 474, N'A new masterpiece released this year, is being chosen by many male friends as a gift to show their affection. That is a gift box of 12 soap roses with a cute bear. Suitable for holidays such as 8/3, valentine''s, 20/10... In addition to the aesthetic effect, these flowers also have a disinfecting effect because they are made from soap, very convenient for every home. I''m sure you will love this gift.', N'Active', CAST(N'2023-06-19T11:30:58.757' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'feb3949f-8766-4080-bb69-3b8bb6a44a45', N'Bình giữ nhiệt', 149000, 3, N'Updating...', N'Active', CAST(N'2023-07-13T10:07:39.873' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', N'Basket of roses', 499000, 478, N'Product information is being updated', N'Active', CAST(N'2023-06-19T11:32:49.267' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'0b090f12-e73f-4626-b81b-5a35df835cb6', N'Sleepy Pig Teddy Bear', 99000, 482, N'Sleepy Pig Teddy Bear full size - Lovely pink color - Both hug and pillow as a cute gift for female friends.', N'Active', CAST(N'2023-06-19T11:56:11.047' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', N'Wax Rose 1 Twig', 60000, 480, N'Material: Made from soap and scented wax thinly dispersed by the skillful hands of artisans to become flowers with lifelike vitality up to 90% Model: Various hand-held flowers, flower baskets, boxes flowers, decorative flowers with diverse quantities and unique designs.', N'Active', CAST(N'2023-06-19T11:45:30.277' AS DateTime), CAST(N'2023-06-19T11:46:44.950' AS DateTime))
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', N'Combo set of gifts for office people', 350000, 493, N'Desktop office gift sets: often mixed with many products, any material, to serve work in the office. For example: crystal desk gifts with pens, note books with wooden tabletop gifts, pens with keychains.', N'Active', CAST(N'2023-06-19T11:27:47.333' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', N'THE UNIFRIEND BOX', 60000, 487, N'The Unifriend Box – Gift set of Unifriend clothes with lovely teddy bears – Unique gift box PRINTING PERSONAL NAME, BIRTHDAY, WISHES exclusively for babies.', N'Active', CAST(N'2023-06-19T11:50:03.817' AS DateTime), CAST(N'2023-06-19T11:51:02.327' AS DateTime))
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'66ea193f-a39b-4937-834a-8270764eabf1', N'Bouquet of Sunflowers with Teddy Bear', 249000, 493, N'Uses: Wax flowers are used to make anniversary gifts, wedding gifts or gifts for girlfriends, mothers, colleagues on Valentine''s Day 14/2, 8/3, 20/10, birthday gifts or gifts for her. on the holiday of November 20.', N'Active', CAST(N'2023-06-19T11:38:12.750' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'd9b58772-7465-4ac8-8c39-dd40e16eced4', N'Versatile soft pillow', 99000, 497, N'This will be a very meaningful gift for your lover if your other half is a sleeper! Although it may seem strange, multi-purpose pillows are a familiar item of girls in the office.', N'Active', CAST(N'2023-06-19T11:48:40.587' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'7cda7480-509b-426a-98f5-df1ac54aaf3b', N'Avocado teddy bear', 399000, 488, N'Avocado teddy bear 2 BLUE WHITE HOT FULL SIZE CHOOSE, avocado teddy bear 2 sizes, cute teddy bear like animal-shaped pillow, fruit-shaped teddy bear, small teddy bear', N'Active', CAST(N'2023-06-19T11:55:05.007' AS DateTime), NULL)
INSERT [dbo].[Product] ([Id], [Name], [Price], [Quantity], [Description], [Status], [CreateAt], [UpdateAt]) VALUES (N'89233ac3-919f-484b-9310-dfaab4a19e05', N'Special gift set', 1500000, 496, N'The gift box has a luxurious, eye-catching form, inside are 12 brightly colored wax roses and a lovely teddy bear. The surface of the flowers is covered with delicate wax, creating a gentle, romantic fragrance, especially long-lasting. Flowers do not wither or wither, expressing sincere love. Suitable as a gift for loved ones.', N'Active', CAST(N'2023-06-19T11:34:33.337' AS DateTime), NULL)
GO
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'411b30ac-82b7-45b0-969a-249fa310b486', N'9438499f-9234-4823-9727-066165ea8bcd')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'411b30ac-82b7-45b0-969a-249fa310b486', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'411b30ac-82b7-45b0-969a-249fa310b486', N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'411b30ac-82b7-45b0-969a-249fa310b486', N'ac08f98b-0f2e-4209-8555-b42800d566db')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'e41899ea-dec7-4c97-b967-2a47b65311a8', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'e41899ea-dec7-4c97-b967-2a47b65311a8', N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'e41899ea-dec7-4c97-b967-2a47b65311a8', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'e41899ea-dec7-4c97-b967-2a47b65311a8', N'a959ab8c-00a1-4cd1-99e3-8dd4dd711d7e')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'feb3949f-8766-4080-bb69-3b8bb6a44a45', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'feb3949f-8766-4080-bb69-3b8bb6a44a45', N'b4f9d436-8af9-4ab5-8591-7ef363d8670e')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'0b090f12-e73f-4626-b81b-5a35df835cb6', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'0b090f12-e73f-4626-b81b-5a35df835cb6', N'f0aced50-b688-4640-9b92-5f29f0359fbb')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'0b090f12-e73f-4626-b81b-5a35df835cb6', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'0b090f12-e73f-4626-b81b-5a35df835cb6', N'ac08f98b-0f2e-4209-8555-b42800d566db')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', N'a314db75-db24-4a09-91ec-a4431e840cbc')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', N'ac08f98b-0f2e-4209-8555-b42800d566db')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', N'f0aced50-b688-4640-9b92-5f29f0359fbb')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', N'a314db75-db24-4a09-91ec-a4431e840cbc')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'66ea193f-a39b-4937-834a-8270764eabf1', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'66ea193f-a39b-4937-834a-8270764eabf1', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'66ea193f-a39b-4937-834a-8270764eabf1', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'd9b58772-7465-4ac8-8c39-dd40e16eced4', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'd9b58772-7465-4ac8-8c39-dd40e16eced4', N'f0aced50-b688-4640-9b92-5f29f0359fbb')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'd9b58772-7465-4ac8-8c39-dd40e16eced4', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'7cda7480-509b-426a-98f5-df1ac54aaf3b', N'87f25a97-a1e2-4e3c-ad97-449f57b54263')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'7cda7480-509b-426a-98f5-df1ac54aaf3b', N'f0aced50-b688-4640-9b92-5f29f0359fbb')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'7cda7480-509b-426a-98f5-df1ac54aaf3b', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'89233ac3-919f-484b-9310-dfaab4a19e05', N'507008d4-f3ec-4b9a-8c76-709d8d3da3f0')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'89233ac3-919f-484b-9310-dfaab4a19e05', N'ea75d1e1-838c-46dd-9f6f-75eef6e7947d')
INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId]) VALUES (N'89233ac3-919f-484b-9310-dfaab4a19e05', N'd73cd780-69d2-4308-bfd1-78b5535ab46c')
GO
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'95d21a8a-df2d-4c3d-8a97-01350aabf9a6', N'd9b58772-7465-4ac8-8c39-dd40e16eced4', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fa5b0c035-1482-4724-85b0-eaea67e0f25b.png?alt=media&token=f4d998d6-a416-4649-801a-58f382af7772')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'fc6f873d-fac9-4d6a-8511-052fc30739bb', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F4b832086-84a7-4c1e-8afd-c4ae7a97d605.png?alt=media&token=182fca57-4b12-4ceb-b617-3897e87d5afe')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'f178d6ae-a32c-4d1a-bae5-05ac4114ec61', N'e41899ea-dec7-4c97-b967-2a47b65311a8', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F7cbb716c-a7f6-4dae-8f4c-1948c0d3354d.png?alt=media&token=3cd0f3d5-1f25-4b89-a3c2-68cda7c7ef1c')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'19088554-1b6e-42f1-89cf-076ca0cfad19', N'411b30ac-82b7-45b0-969a-249fa310b486', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F485c6bd4-66f9-402b-a7fb-8836a4c44eae.png?alt=media&token=3319bb49-9aac-4688-989c-f481f462db85')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'2a49cb47-b616-4b49-b803-117d85b6adf8', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F0939d300-8d08-4603-a018-06c55adbbccb.png?alt=media&token=b5c11f98-bdaf-4637-a205-981b26a8cea0')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'02ba2257-eeee-4858-a66b-11ae7d8a0e24', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F1415f280-2120-4df7-8aeb-ec4d0f89ddda.png?alt=media&token=42afea8d-f4ac-4024-a5b9-2e49155e4ac2')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'9eb8eda8-5cc4-49fa-b2da-1d06e57ddb5b', N'feb3949f-8766-4080-bb69-3b8bb6a44a45', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F7309d206-ba5f-4846-8e08-e0d3ba81c784.png?alt=media&token=a6ad7b8b-ef58-4b3e-be6e-054c9d6cb9bb')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'8847bf57-71b9-4583-8de2-1f1736a4fcd7', N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F77131ed0-cdb2-4c32-8ea2-440c85fe64c4.png?alt=media&token=c15dd8d0-58e4-4244-9b26-263f8cecb678')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'aef0de66-1b69-44aa-99db-2563e429f0fe', N'89233ac3-919f-484b-9310-dfaab4a19e05', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F5eff48d6-0c7c-457a-bc63-afa0ed9d4065.png?alt=media&token=adcb90d3-9429-4909-aad0-ec0d443e8bd5')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'dab27114-b978-4148-9edf-257e4f9e6444', N'd9b58772-7465-4ac8-8c39-dd40e16eced4', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fdc2667b2-9b29-42e1-a384-dddd71acd088.png?alt=media&token=9084145a-e7cc-4966-a747-1986efc1b0d0')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'3947d55f-e85d-49c6-9977-2becb3709277', N'411b30ac-82b7-45b0-969a-249fa310b486', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fe4984c77-0aba-4ca5-981e-8cf37a2167dd.png?alt=media&token=f9bfba80-0d47-4df8-ba32-19d9c0ed63d6')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'1d2ccf5b-4d6e-4cf4-971c-3326c09e259a', N'411b30ac-82b7-45b0-969a-249fa310b486', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F262eaed9-e766-4bc6-b23e-a4aa287d6277.png?alt=media&token=b4bfc509-dbd5-4215-835b-5117c5cea997')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'0e11116a-6565-45b0-8064-37b87cbcc6b8', N'89233ac3-919f-484b-9310-dfaab4a19e05', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fc488c813-9ebc-499c-8ad5-a9fc8baa7482.png?alt=media&token=287585eb-5104-4a81-b2b7-9c44f6cde268')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'90b2bafb-d618-42a9-bafc-3953e5e2cb82', N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F00c18f8d-99ea-4a92-add6-e74778cab818.png?alt=media&token=c61ce06f-2ea6-4ab1-b7b3-54949428252d')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'7ad749aa-450a-4a46-9b24-3ce3485c2b55', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fcc6c5d8d-42e5-4956-9162-bcdc0f2033c4.png?alt=media&token=36204bac-bff0-4b37-b541-f39acc941df8')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'77746223-e98b-499f-9468-3ef2732ce84a', N'66ea193f-a39b-4937-834a-8270764eabf1', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F8e96ccc1-1af5-49e1-acd6-81126b749747.png?alt=media&token=a5b938ec-1a36-4409-8734-260ffc79eded')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'7ea9d020-a50c-454e-ae81-3f02099f7d87', N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F3bc3b18b-a4ee-4bc6-aace-1d3e0d8fe3e4.png?alt=media&token=d9fa1eb2-228c-4c1c-bd17-a424bf47ffd8')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'426f38e0-27be-413d-9bbc-3fcc0d8655ed', N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F836fb882-b389-4069-8b7b-1bee01b7b7fa.png?alt=media&token=3a7f7c7b-8c9f-454a-b40f-bf4698e41c3e')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'cef3663d-e22b-4495-95e8-3fdc27b64302', N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fab38de0b-4f1f-4027-a4b4-e84d0fda674b.png?alt=media&token=c4f25984-9532-4064-a892-60e94f933c5d')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'6d64d6a5-d058-4792-a512-4105a4b8e5df', N'feb3949f-8766-4080-bb69-3b8bb6a44a45', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fd13936e0-bfb9-489e-a37e-8cb6a3bee0f9.png?alt=media&token=4fdc4c02-6bda-499c-a4ce-275c9434819a')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'849018e6-e8b7-4514-a6cc-4399971af1fc', N'89233ac3-919f-484b-9310-dfaab4a19e05', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Ffb993546-d437-4602-9592-68d2716db5a6.png?alt=media&token=d6d734f6-eb58-40d0-84b2-9f6092e344ca')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'b2919b99-35cd-429a-a781-4ee71fb10e1b', N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fbab295de-63e9-4e41-a33a-a8d7e01505fc.png?alt=media&token=737748e1-1d5e-457a-9005-0d2caa8d5b94')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'3d088303-b7a5-4024-9706-5298fa678af3', N'd9b58772-7465-4ac8-8c39-dd40e16eced4', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F6ae0ff69-22cd-4689-8f8c-9a8da97b3c1a.png?alt=media&token=578d400c-bde2-4296-b5d5-c5e7118f2307')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'5029ad0a-bc96-42d3-817a-5415c13843ea', N'7cda7480-509b-426a-98f5-df1ac54aaf3b', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fb0a7e1b0-2da5-4249-a516-d8493533a2cb.png?alt=media&token=d79c0b1b-1b67-4b07-9836-d96141224c35')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'f8967df6-8d3f-4258-95ae-5c4508aed919', N'7cda7480-509b-426a-98f5-df1ac54aaf3b', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F2c07376e-8de6-4190-9084-32c9260199c7.png?alt=media&token=a37723f8-180c-4300-a560-894f14c57da3')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'500661ae-346e-42a7-b1ab-5fe7afff0385', N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fa0aa6868-aaa7-4971-8f50-c2643ec20f4e.png?alt=media&token=c83574f9-a9a2-4141-9b0e-0b60923d39e2')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'0e2b99e7-d4a2-4559-aee2-630a817ec02d', N'7cda7480-509b-426a-98f5-df1ac54aaf3b', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fde98a3b3-4193-4a57-b9ec-0e0a8aaa895d.png?alt=media&token=26f083c0-af04-4e5c-96eb-aa20db66d059')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'df4b7e0a-34c3-4707-ba5c-662ac1aa7359', N'7cda7480-509b-426a-98f5-df1ac54aaf3b', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F20df04df-462d-404b-b41b-2de20ce12356.png?alt=media&token=56d0e85a-75ee-44e2-b144-6a05a341ee6e')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'f0586f32-4bea-453f-a858-6ef074d46273', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F989e2534-2a85-4b45-b4ba-41952e5bf1c6.png?alt=media&token=3e806865-da69-471b-90f1-d85538c01673')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'fde33e10-6263-4ac9-a840-7966f7370641', N'feb3949f-8766-4080-bb69-3b8bb6a44a45', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F45b48fde-6910-49b3-aeea-e5ae7fd8abdd.png?alt=media&token=d81c3566-3a44-4d07-b582-811165eab13a')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'42d9ba1d-b92c-4780-ad8b-7adeac0a6552', N'd9b58772-7465-4ac8-8c39-dd40e16eced4', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fc80271d5-da25-4ab9-95fc-8015148f7ed1.png?alt=media&token=8f1b8c1a-0876-487e-a798-5f6df991b6d1')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'55a7115f-70dc-4499-aa33-8d44919c4449', N'e41899ea-dec7-4c97-b967-2a47b65311a8', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F24daae01-1be3-48df-b502-89b17219376d.png?alt=media&token=85942c91-5059-4667-af21-1bdd45feb9ef')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'2a986614-944b-4e4b-8fdf-8ece58e498de', N'0b090f12-e73f-4626-b81b-5a35df835cb6', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F7577ffd1-f95c-42d5-9fb6-92bad3d63d61.png?alt=media&token=f32e56dd-12c5-4633-bda1-03ed308ebc0a')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'45e75a38-f16a-4895-8cac-8ff78b211176', N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F93ecb5ab-e01a-4040-8f64-a6b96b4bc8ca.png?alt=media&token=b09f3526-c940-4ba6-953f-6cb490e8b2e7')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'18aea799-fdd4-46ad-a9c9-95489434c26b', N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F74e33ae2-3d0a-4fdb-8568-46e916d3f90f.png?alt=media&token=9a9b4c1b-6852-4aff-8974-e08f51530c4a')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'599d4420-76dc-427f-8743-9bd707a3a8ce', N'e41899ea-dec7-4c97-b967-2a47b65311a8', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F00a5b097-d77e-4ebc-9289-5652ab42ac33.png?alt=media&token=e122f1c7-b0b0-4066-bedd-a75428f0a038')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'74e97fcf-4232-4e38-87f3-9c7922c5b482', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fb97adba6-b837-4719-925f-0d5e9ee86b31.png?alt=media&token=1b3747b3-34c3-4dd4-b2ad-1c07a6139079')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'a9bbfd8b-1f7f-433b-bf91-a5da3811c6b6', N'89233ac3-919f-484b-9310-dfaab4a19e05', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F063776bd-6760-4fa6-8d74-eeeeace9623d.png?alt=media&token=61bd96c7-11c2-44a7-94c5-fe9ad5d5fe16')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'077f00f2-490d-4937-9255-a740cd69154f', N'66ea193f-a39b-4937-834a-8270764eabf1', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Ff106f621-adad-4a69-a2c9-9fec1beb520b.png?alt=media&token=266b01c6-d12c-49c8-ac7e-d77f2304f525')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'84ca077a-df61-4dc0-bcaa-b53ba75210a0', N'4a201f6e-462e-4f5f-ae18-6fa162a4a402', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fb2ca3074-4f30-4e64-a61c-7cab3375ce06.png?alt=media&token=f906676c-483b-43fa-8c2c-8ba983ccec20')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'f415cc25-25cc-49e6-a117-b614d75c1fb0', N'31a8ce84-c27d-4139-b99f-6fec0486ab5a', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fa012333b-1177-4abf-9fdc-e737eca5549d.png?alt=media&token=174117d3-ea47-4a57-aa57-0ec4084bb598')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'aa5e0a79-b466-45c0-9988-b953f4408a35', N'2ba68254-b401-4c99-9fcc-5aac3d7e4d30', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F2551a75a-9d3b-4702-a37f-05b3a5404054.png?alt=media&token=3ab7936f-04ec-4f1e-a51e-635b147655ad')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'8f90d50f-44f4-47d0-b587-bf67fb343931', N'411b30ac-82b7-45b0-969a-249fa310b486', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fae489c82-2b6e-40e2-af37-e6fdb84eb91f.png?alt=media&token=d226770c-9627-412a-8a7b-b6055d6b1f85')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'4475b35e-cba9-4008-ab6f-c42390adbe05', N'dfcb9f26-2ad6-47dd-9cbb-51f899693ea5', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Ff171f86b-0eb4-45a2-83d5-dc40f08f5eb8.png?alt=media&token=603e87f0-e7a0-4c0c-bbc5-1e80a4701ed9')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'507042f8-26e9-4626-b5f2-c43e7e71570f', N'66ea193f-a39b-4937-834a-8270764eabf1', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Ff75e62a6-b461-4bb2-a6c2-c195986d0ce7.png?alt=media&token=47113054-d250-4568-b7e5-8045a1bcd44e')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'f71b5ed9-0d01-4412-89a0-c47aae722724', N'feb3949f-8766-4080-bb69-3b8bb6a44a45', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F8374625d-d208-476f-871c-32303cd56430.png?alt=media&token=66fc4696-b52a-472c-a39b-52bbc36e3962')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'ad9b0175-b20a-4504-abe1-cb00a41f48af', N'e41899ea-dec7-4c97-b967-2a47b65311a8', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Ff457d2cd-d3a4-42ac-8ade-096408841b6e.png?alt=media&token=75892419-f3f2-4e6e-b5ed-ca044a0b415e')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'a0a1af0a-0023-40e5-8d1b-cea850559fda', N'f0908c0a-6f2e-4b38-b2c4-152e6a539dba', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F7ab4d24f-9ba0-460d-81cb-d0d26ac6a0ea.png?alt=media&token=f43a0c56-9287-458a-9dad-cf7cbf2f48f3')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'c1df77f2-483b-4e84-9e8e-d11ed7679d4d', N'0b090f12-e73f-4626-b81b-5a35df835cb6', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F47cfafa2-e8d4-4651-8dbd-561aeb29487f.png?alt=media&token=2b3a32a1-d532-4c8d-8d66-28613ca0c667')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'440539cc-dc6c-4711-938e-e23503f3ce06', N'0b090f12-e73f-4626-b81b-5a35df835cb6', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2Fd920358b-7167-497a-b3b5-611f132732f5.png?alt=media&token=c4df6cdc-0788-41f8-a5fa-9204eef60995')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'44c2a74c-396c-4f42-a209-fcbbf85b42c5', N'66ea193f-a39b-4937-834a-8270764eabf1', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F1263a7a8-80da-4401-a7ef-973b4805c305.png?alt=media&token=4e03449c-7ae1-4858-8726-2b59cd4672f3')
INSERT [dbo].[ProductImage] ([Id], [ProductId], [Type], [Url]) VALUES (N'2ca49b77-351a-40fe-b316-fd357aa69cb9', N'0b090f12-e73f-4626-b81b-5a35df835cb6', NULL, N'https://firebasestorage.googleapis.com/v0/b/e-gift-6276a.appspot.com/o/images%2F6297a82b-d3cf-4289-a1da-56b7054a7092.png?alt=media&token=a6526cdb-a583-466e-bd39-6b74574ae9f3')
GO
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'dcb4469a-f725-418e-866a-10d40144e282', N'U3K2S8DJL1', 60, N'string', 10, 20000, 60000, CAST(N'2023-08-03T00:56:30.020' AS DateTime), CAST(N'2023-08-06T00:56:30.020' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'2113bd2d-53ed-4bda-9c5e-1cc65255df4d', N'D6O538E8O3', 10, N'string', 10, 10000, 50000, CAST(N'2023-08-03T00:52:27.523' AS DateTime), CAST(N'2023-08-03T00:52:27.523' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'b3c0ee0d-d2eb-4603-a5fe-1d19bf049648', N'27S582D6ET', 20, N'string', 10, 50000, 100000, CAST(N'2023-08-03T01:49:08.607' AS DateTime), CAST(N'2023-08-09T01:49:08.607' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'5826551b-2a22-4dc5-99e9-63892002de2b', N'EIY2SS1SAQ', 10, N'string', 10, 10000, 50000, CAST(N'2023-08-03T00:49:29.193' AS DateTime), CAST(N'2023-08-03T00:49:29.193' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'fca13cb0-2c61-4b27-a0cd-6726cbabc6c1', N'422N33OIDX', 20, N'string', 10, 60000, 120000, CAST(N'2023-08-03T04:16:03.370' AS DateTime), CAST(N'2023-08-05T04:16:03.370' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'ed024d72-58ca-463f-80ed-8315a9addd04', N'74JK9A7H42', 20, N'string', 10, 10000, 50000, CAST(N'2023-08-03T00:00:00.000' AS DateTime), CAST(N'2023-08-03T00:00:00.000' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'24b56ee3-6fce-4851-8203-8d1411e387f2', N'T7AFDPS321', 20, N'string', 10, 10000, 50000, CAST(N'2023-08-01T00:56:30.020' AS DateTime), CAST(N'2023-08-02T00:56:30.020' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'00f087b4-2691-4e28-9bce-ae12f913e184', N'QH53WFDCL3', 50, N'string', 10, 100000, 200000, CAST(N'2023-08-05T00:54:53.057' AS DateTime), CAST(N'2023-08-09T00:54:53.057' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'c4a02f0f-10b2-4e72-ad57-c06306ea0d87', N'OKHD63DJS2', 20, N'string', 10, 10000, 50000, CAST(N'2023-08-03T01:49:08.607' AS DateTime), CAST(N'2023-08-09T01:49:08.607' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'bd5bce78-364a-4380-8e6d-e6fd59191b7f', N'NW3F42J8SS', 30, N'string', 10, 5000, 10000, CAST(N'2023-08-03T00:37:54.400' AS DateTime), CAST(N'2023-08-03T00:37:54.400' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'58bf0de6-c7e9-42d3-b6e7-eaf09065cdf1', N'TD43333S5D', 20, N'string', 10, 10000, 50000, CAST(N'2023-08-03T00:51:06.747' AS DateTime), CAST(N'2023-08-03T00:51:06.747' AS DateTime), CAST(N'2023-08-03' AS Date), 0)
INSERT [dbo].[Voucher] ([Id], [Code], [Discount], [Description], [Quantity], [FromPrice], [ToPrice], [StartDate], [EndDate], [CreateAt], [IsActive]) VALUES (N'594dd216-c671-40dc-9279-f513976d70e9', N'FVXA300CAD', 40, N'string', 10, 10000, 50000, CAST(N'2023-08-03T01:49:08.607' AS DateTime), CAST(N'2023-08-09T01:49:08.607' AS DateTime), CAST(N'2023-08-03' AS Date), 1)
GO
/****** Object:  Index [UQ__Cart__A4AE64D9A1A45FA4]    Script Date: 8/3/2023 12:53:31 PM ******/
ALTER TABLE [dbo].[Cart] ADD UNIQUE NONCLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Feverous__A4AE64D95A31A018]    Script Date: 8/3/2023 12:53:31 PM ******/
ALTER TABLE [dbo].[Feverous] ADD UNIQUE NONCLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CartItem] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[FeverousItem] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF__Order__CreateAt__534D60F1]  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF__OrderDeta__Creat__5441852A]  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD FOREIGN KEY([CartId])
REFERENCES [dbo].[Cart] ([Id])
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Feverous]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[FeverousItem]  WITH CHECK ADD FOREIGN KEY([FeverousId])
REFERENCES [dbo].[Feverous] ([Id])
GO
ALTER TABLE [dbo].[FeverousItem]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__CustomerI__5BE2A6F2] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__CustomerI__5BE2A6F2]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Voucher]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__5CD6CB2B] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK__OrderDeta__Order__5CD6CB2B]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Produ__5DCAEF64] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK__OrderDeta__Produ__5DCAEF64]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[VoucherMark]  WITH CHECK ADD  CONSTRAINT [FK_VoucherMark_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[VoucherMark] CHECK CONSTRAINT [FK_VoucherMark_Customer]
GO
ALTER TABLE [dbo].[VoucherMark]  WITH CHECK ADD  CONSTRAINT [FK_VoucherMark_Voucher] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher] ([Id])
GO
ALTER TABLE [dbo].[VoucherMark] CHECK CONSTRAINT [FK_VoucherMark_Voucher]
GO
USE [master]
GO
ALTER DATABASE [EGift] SET  READ_WRITE 
GO
