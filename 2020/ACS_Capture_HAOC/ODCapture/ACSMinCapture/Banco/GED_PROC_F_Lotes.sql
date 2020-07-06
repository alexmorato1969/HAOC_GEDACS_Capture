USE [GEDPES_OI_PRD]
GO

/****** Object:  StoredProcedure [dbo].[GED_PROC_F_Lotes]    Script Date: 08/11/2014 16:56:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:	 JWILL
-- Create date: 15/04/2014
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[GED_PROC_F_Lotes]
@TipoPessoa int,
@Tipo int,
@Value varchar(Max),
@DtIni DateTime,
@DtFin DateTime,
@IdStatusLote int 
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

   -- Insert statements for procedure here
   DECLARE @SQL VARCHAR(max); 
	IF @TipoPessoa = 0 --FISICA
	BEGIN
		--SELECT *, '''' as DIRLOTEINBOX FROM F_LOTES (@Tipo,@Value,@DtIni,@DtFin)
		SET @SQL = 'SELECT *, '''' as DIRLOTEINBOX FROM F_LOTES ('''+CAST(@Tipo AS VARCHAR(10))+''','''+CAST(@Value AS VARCHAR(MAX))+''', '''+CAST(@DtIni AS VARCHAR(MAX))+''', '''+CAST(@DtFin AS VARCHAR(MAX))+''')'
	END
	ELSE IF @TipoPessoa = 1 -- JURIDICA
	BEGIN
		--SELECT *, '''' as DIRLOTEINBOX FROM F_LOTES_PJ (@Tipo,@Value,@DtIni,@DtFin)
		SET @SQL = 'SELECT *, '''' as DIRLOTEINBOX FROM F_LOTES_PJ ('''+CAST(@Tipo AS VARCHAR(10))+''','''+CAST(@Value AS VARCHAR(MAX))+''', '''+CAST(@DtIni AS VARCHAR(MAX))+''', '''+CAST(@DtFin AS VARCHAR(MAX))+''')'
	END
	
	IF @IdStatusLote > 0 
	BEGIN
		SET @SQL = @SQL + ' INNER JOIN F_LOTES_PSTATUS('+CAST(@IdStatusLote AS VARCHAR(10))+') ON LTU_CodigoPassagem = PAS_CODIGOPASSAGEM'
	END
	
	EXEC(@SQL);
	
END


GO


