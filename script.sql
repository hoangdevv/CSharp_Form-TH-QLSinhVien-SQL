USE QLSINHVIEN
GO

CREATE DATABASE QLSINHVIEN
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 9/26/2023 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[ID] [int] NOT NULL,
	[facultyName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 9/26/2023 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[studentID] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](50) NULL,
	[averageScore] [float] NULL,
	[facultyID] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[studentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Faculty] ([ID], [facultyName]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[Faculty] ([ID], [facultyName]) VALUES (2, N'Ngôn ngữ anh')
INSERT [dbo].[Faculty] ([ID], [facultyName]) VALUES (3, N'Quản trị kinh doanh')
GO
INSERT [dbo].[Student] ([studentID], [fullName], [averageScore], [facultyID]) VALUES (N'1611061916', N'Nguyễn Trần Hoàng Lan', 4.5, 1)
INSERT [dbo].[Student] ([studentID], [fullName], [averageScore], [facultyID]) VALUES (N'1711006104', N'Nguyễn Quốc An', 10, 2)
INSERT [dbo].[Student] ([studentID], [fullName], [averageScore], [facultyID]) VALUES (N'1711061548', N'Đàm Minh Đức', 2.5, 1)
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Faculty] FOREIGN KEY([facultyID])
REFERENCES [dbo].[Faculty] ([ID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Faculty]
GO
