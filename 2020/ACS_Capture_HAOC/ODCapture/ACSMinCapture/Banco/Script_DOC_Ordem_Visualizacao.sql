
--alter table GEDDocumentos drop column DOC_Ordem_Visualizacao
--go 

alter table GEDDocumentos add DOC_Ordem_Visualizacao int

go

update A
	set A.DOC_Ordem_Visualizacao = 
	(Select C.OrderDoc 
		from (select ROW_NUMBER() over(order by B.DOC_NomeArquivo ) OrderDoc, B.DOC_idDocumento
					from GEDDocumentos B
						where B.DOC_idPassagem = A.DOC_idPassagem)C 
			where C.DOC_idDocumento = A.DOC_idDocumento)  
from GEDDocumentos A		
