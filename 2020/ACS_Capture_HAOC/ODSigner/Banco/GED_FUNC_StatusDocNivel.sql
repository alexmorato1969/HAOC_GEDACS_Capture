-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
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
-- Author:		JWill
-- Create date: 2014-01-21
-- Description:	Status do Documento por nivel
-- =============================================
drop FUNCTION GED_FUNC_StatusDocNivel
go
CREATE FUNCTION GED_FUNC_StatusDocNivel
(
	-- Add the parameters for the function here
	@id_Usuario int,
	@DOC_idUsuarioACSCapture int,
	@DOC_idUsuarioAssinaNivel1 int,
	@DOC_idUsuarioAssinaNivel2 int,
	@DOC_idUsuarioAssinaNivel3 int
	
)
RETURNS int
AS
BEGIN
	DECLARE @RETURN INT
	SELECT  @RETURN = 
		CASE 
			WHEN (@DOC_idUsuarioAssinaNivel1 IS NULL AND (@DOC_idUsuarioACSCapture = @id_Usuario OR @DOC_idUsuarioACSCapture IS NULL)) THEN -1
			WHEN (@DOC_idUsuarioAssinaNivel1 = @id_Usuario) THEN 1
			WHEN (@DOC_idUsuarioAssinaNivel2 IS NULL AND NOT @DOC_idUsuarioAssinaNivel1 IS NULL ) THEN -2
			WHEN (@DOC_idUsuarioAssinaNivel2 = @id_Usuario) THEN 2
			WHEN (@DOC_idUsuarioAssinaNivel3 IS NULL AND NOT @DOC_idUsuarioAssinaNivel2 IS NULL ) THEN -3
			WHEN (@DOC_idUsuarioAssinaNivel3 = @id_Usuario) THEN 3
			ELSE -4
		END 
	RETURN @RETURN
END
GO

