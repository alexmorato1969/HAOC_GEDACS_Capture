USE [GEDPES_OI_PRD]
GO

/****** Object:  Table [dbo].[GEDLotesXUsuarios]    Script Date: 08/11/2014 17:19:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GEDLotesXUsuarios](
	[LTU_Id] [int] IDENTITY(1,1) NOT NULL,
	[LTU_IdUsuario] [int] NOT NULL,
	[LTU_CodigoPassagem] [varchar](30) NOT NULL,
	[LTU_IdStatusLote] [int] NOT NULL,
	[LTU_Data] [datetime] NOT NULL,
	[LTU_Observacao] [nvarchar](140) NULL,
	[LTU_PathImagens] [nvarchar](max) NULL,
 CONSTRAINT [PK_GEDLoteXUsuario] PRIMARY KEY CLUSTERED 
(
	[LTU_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[GEDLotesXUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_GEDLoteXUsuario_GEDStatusLote] FOREIGN KEY([LTU_IdStatusLote])
REFERENCES [dbo].[GEDStatusLote] ([STL_Id])
GO

ALTER TABLE [dbo].[GEDLotesXUsuarios] CHECK CONSTRAINT [FK_GEDLoteXUsuario_GEDStatusLote]
GO

ALTER TABLE [dbo].[GEDLotesXUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_GEDLoteXUsuario_GEDUsuarios] FOREIGN KEY([LTU_IdUsuario])
REFERENCES [dbo].[GEDUsuarios] ([USR_idUsuario])
GO

ALTER TABLE [dbo].[GEDLotesXUsuarios] CHECK CONSTRAINT [FK_GEDLoteXUsuario_GEDUsuarios]
GO

