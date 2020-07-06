alter table GEDUsuarios add USR_flagAssina bit default(0)
alter table GEDUsuarios add USR_NivelAssina int default(0)
alter table GEDUsuarios add USR_SerialNumberCert Varchar(max)


alter table GEDDocumentos add DOC_idUsuarioACSCapture int
alter table GEDDocumentos add DOC_idUsuarioAssinaNivel1 int
alter table GEDDocumentos add DOC_idUsuarioAssinaNivel2 int
alter table GEDDocumentos add DOC_idUsuarioAssinaNivel3 int

alter table GEDDocumentos add DOC_dataAssinaNivel1 datetime
alter table GEDDocumentos add DOC_dataAssinaNivel2 datetime
alter table GEDDocumentos add DOC_dataAssinaNivel3 datetime

alter table GEDDocumentos add DOC_p7s varchar(max)

alter table GEDDocumentos add DOC_detail_cert varchar(max)

select * from GEDDocumentos

inner join GEDPassagens on DOC_idPassagem = PAS_idPassagem

sp_help GEDDocumentos

select * from GEDUsuarios

--update GEDDocumentos set DOC_idUsuarioACSCapture = 5
--update GEDUsuarios set USR_flagAssina = 1

update GEDDocumentos set DOC_idUsuarioAssinaNivel1 = null,
DOC_idUsuarioAssinaNivel2 = null,
DOC_idUsuarioAssinaNivel3 = null,

DOC_dataAssinaNivel1 = null,
DOC_dataAssinaNivel2 = null,
DOC_dataAssinaNivel3 = null,
DOC_p7s = null


select * from GED_FUNC_LotesUsuarioNivelAssinatura(90,2,0,'2014-12-31','T')
order by DOC_nomeArquivo