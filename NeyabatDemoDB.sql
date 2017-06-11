USE [NeyabatDemo]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/11/2017 12:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Pass] [nvarchar](1000) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AudioFile]    Script Date: 06/11/2017 12:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AudioFile](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Status] [int] NULL,
	[FileSize] [bigint] NULL,
	[CreatedAt] [datetime] NULL,
	[FileType] [int] NULL,
	[UserID] [bigint] NULL,
 CONSTRAINT [PK_AudioFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_AudioFile_User]    Script Date: 06/11/2017 12:27:53 ******/
ALTER TABLE [dbo].[AudioFile]  WITH CHECK ADD  CONSTRAINT [FK_AudioFile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[AudioFile] CHECK CONSTRAINT [FK_AudioFile_User]
GO
