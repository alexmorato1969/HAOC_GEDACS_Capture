USE [GEDPES_OI_PRD]
GO

/****** Object:  UserDefinedFunction [dbo].[F_LOTES]    Script Date: 08/01/2014 18:31:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		JWIll
-- Create date: 24/03/2014
-- Description:	Busca Lotes
-- =============================================
ALTER FUNCTION [dbo].[F_LOTES]
(	
	-- Add the parameters for the function here
@Tipo int,
@Value varchar(Max),
@DtIni DateTime,
@DtFin DateTime

)
RETURNS TABLE 
AS
RETURN 
(
	 SELECT DISTINCT
			PAS_IDPASSAGEM,
			PAS_CODIGOPASSAGEM,
			PAS_REGISTRO,
			CPF_NOME AS NOME,
			PAS_DATAHORAPASSAGEM,
			CPF_CPF AS CPF_CNPJ,
			CPF_FLAGATIVO AS CPF_CNPJ_FLAGATIVO,
			ISNULL(MAX(cast(DOC_nomeArquivo as int)) over (partition by DOC_IdPassagem),0) as INCLUSAO,
			CASE 
				WHEN ISNULL(MAX(DOC_Ordem_Visualizacao) over (partition by DOC_IdPassagem),0) = 0 THEN 
					Count(DOC_IdPassagem) over (PARTITION by DOC_IdPassagem)
				ELSE
					ISNULL(MAX(DOC_Ordem_Visualizacao) over (partition by DOC_IdPassagem),0)
			END MAX_ORDER 	
	 FROM GEDPASSAGENS
	 INNER JOIN GEDCLIENTEPF ON (CPF_REGISTRO = PAS_REGISTRO)
	 LEFT JOIN GEDDocumentos ON (DOC_idPassagem = PAS_idPassagem)
	 WHERE (@Tipo = 2 and Pas_Registro  like '%' + @Value + '%') 
		OR (@Tipo = 3 and PAS_codigoPassagem  like '%' + @Value + '%')
		OR (@Tipo = 1 and  PAS_DATAHORAPASSAGEM BETWEEN @DtIni AND @DtFin)
)


GO


