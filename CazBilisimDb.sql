USE [CazBilisimDB]
GO
/****** Object:  Table [dbo].[ClassL]    Script Date: 12.07.2019 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassL](
	[ClassLId] [smallint] NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[LessonID] [smallint] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 12.07.2019 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[LessonId] [smallint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 12.07.2019 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StudentNumber] [nvarchar](10) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student_ClassL]    Script Date: 12.07.2019 16:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_ClassL](
	[StudentId] [smallint] NOT NULL,
	[ClassLId] [smallint] NOT NULL,
 CONSTRAINT [PK_Student_Class] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[ClassLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentId], [Name], [StudentNumber]) VALUES (1, N'Kubra', N'12312312')
INSERT [dbo].[Student] ([StudentId], [Name], [StudentNumber]) VALUES (2, N'ahmet', N'1213132')
INSERT [dbo].[Student] ([StudentId], [Name], [StudentNumber]) VALUES (3, N'zeynep', N'345245')
INSERT [dbo].[Student] ([StudentId], [Name], [StudentNumber]) VALUES (4, N'rıeojf', N'12345524')
SET IDENTITY_INSERT [dbo].[Student] OFF
ALTER TABLE [dbo].[ClassL]  WITH CHECK ADD  CONSTRAINT [FK_Class_Lesson] FOREIGN KEY([LessonID])
REFERENCES [dbo].[Lesson] ([LessonId])
GO
ALTER TABLE [dbo].[ClassL] CHECK CONSTRAINT [FK_Class_Lesson]
GO
ALTER TABLE [dbo].[Student_ClassL]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class_Class] FOREIGN KEY([ClassLId])
REFERENCES [dbo].[ClassL] ([ClassLId])
GO
ALTER TABLE [dbo].[Student_ClassL] CHECK CONSTRAINT [FK_Student_Class_Class]
GO
ALTER TABLE [dbo].[Student_ClassL]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[Student_ClassL] CHECK CONSTRAINT [FK_Student_Class_Student]
GO
