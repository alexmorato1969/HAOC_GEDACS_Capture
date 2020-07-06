-- ================================================
-- Template generated from Template Explorer using:
-- Create Inline Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:JWill
-- Create date: 2014-01-21
-- Description:	Retorna Lotes P/ Usuário e Nivel Assinatura
-- =============================================
drop FUNCTION GED_FUNC_LotesUsuarioNivelAssinatura
go
CREATE FUNCTION GED_FUNC_LotesUsuarioNivelAssinatura
(	
	-- Add the parameters for the function here
	@id_Usuario int, 
	@Nivel int,
	@Data_Ini datetime,
	@Data_Fin datetime,
	@Status varchar(1) -- P - Pendêntes, A - Assinados, T - Todos
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here	
	SELECT	PAS_codigoPassagem,
			DIV_codigoReduzido, 
			PAS_registro ,
			DOC_idDocumento,
			DOC_nomeArquivo, 
			DOC_dataEmissao,
			DOC_dataHoraCadastro,
			DOC_idUsuarioAssinaNivel1,
			DOC_idUsuarioAssinaNivel2,
			DOC_idUsuarioAssinaNivel3,
			DOC_dataAssinaNivel1,
			DOC_dataAssinaNivel2,
			DOC_dataAssinaNivel3,
			DOC_p7s,
			DOC_idUsuarioACSCapture,
			DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) DOC_statusCod,
			CASE 
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = -1  THEN 'Pendênte Assiantura Nivel 1'
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = 1 THEN 'Assinado Nivel 1' 
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = -2 THEN 'Pendênte Assinatura Nivel 2'
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = 2 THEN 'Assinado Nivel 2' 
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = -3 THEN 'Pendênte Assiantura Nivel 3' 
				WHEN DBO.GED_FUNC_StatusDocNivel(@id_Usuario,DOC_idUsuarioACSCapture, DOC_idUsuarioAssinaNivel1,DOC_idUsuarioAssinaNivel2,DOC_idUsuarioAssinaNivel3) = 3 THEN  'Assinado Nivel 3'
				ELSE 'Nenhuma assinatura encontrada'
			END DOC_status			
				
		FROM GEDDocumentos
	INNER JOIN GEDPassagens ON DOC_idPassagem = PAS_idPassagem
	INNER JOIN GEDTiposDocumentos ON DOC_idTipoDocumento = TPD_idTipoDocumento
	INNER JOIN GEDDivisoes ON DIV_idDivisao = TPD_idDivisao
	WHERE DOC_dataHoraCadastro BETWEEN @Data_Ini AND @Data_Fin
	  AND (
			((@Nivel = 1 AND @Status = 'P' AND DOC_idUsuarioAssinaNivel1 IS NULL AND DOC_idUsuarioACSCapture = @id_Usuario  ) OR 
			 (@Nivel = 1 AND @Status = 'A' AND DOC_idUsuarioAssinaNivel1 = @id_Usuario) OR 
			 (@Nivel = 1 AND @Status = 'T' AND DOC_idUsuarioACSCapture = @id_Usuario)) OR  
			
			((@Nivel = 2 AND @Status = 'P' AND DOC_idUsuarioAssinaNivel2 IS NULL AND NOT DOC_idUsuarioAssinaNivel1 IS NULL ) OR 
			 (@Nivel = 2 AND @Status = 'A' AND DOC_idUsuarioAssinaNivel2 = @id_Usuario) OR 
			 (@Nivel = 2 AND @Status = 'T' /*AND NOT DOC_idUsuarioAssinaNivel1 IS NULL*/)) OR  
			
			((@Nivel = 3 AND @Status = 'P' AND DOC_idUsuarioAssinaNivel3 IS NULL AND NOT DOC_idUsuarioAssinaNivel2 IS NULL ) OR 
			 (@Nivel = 3 AND @Status = 'A' AND DOC_idUsuarioAssinaNivel3 = @id_Usuario) OR 
			 (@Nivel = 3 AND @Status = 'T' /*AND NOT DOC_idUsuarioAssinaNivel2 IS NULL*/))   
		   )
)
GO
