unit BRYSIGNERCOMLib_TLB;

// ************************************************************************ //
// WARNING                                                                    
// -------                                                                    
// The types declared in this file were generated from data read from a       
// Type Library. If this type library is explicitly or indirectly (via        
// another type library referring to this type library) re-imported, or the   
// 'Refresh' command of the Type Library Editor activated while editing the   
// Type Library, the contents of this file will be regenerated and all        
// manual modifications will be lost.                                         
// ************************************************************************ //

// PASTLWTR : 1.2
// File generated on 8/30/2012 3:24:15 PM from Type Library described below.

// ************************************************************************  //
// Type Lib: C:\visualWorkspace\BRySignerCOM\BRySignerCOM\BRySignerCOM.tlb (1)
// LIBID: {6A8AD962-44DD-4F00-97EB-E82C8FEAC5E5}
// LCID: 0
// Helpfile: 
// HelpString: BRySignerCOM 2.1.0.2 Type Library
// DepndLst: 
//   (1) v2.0 stdole, (C:\WINDOWS\system32\stdole2.tlb)
// Errors:
//   Error creating palette bitmap of (TCRL) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
//   Error creating palette bitmap of (TCertificado) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
//   Error creating palette bitmap of (TCarimbo) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
//   Error creating palette bitmap of (TRepositorio) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
//   Error creating palette bitmap of (TAssinador) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
//   Error creating palette bitmap of (TVerificador) : Server C:\Users\user\Documents\SVN\BRySigner\Instaladores\BRySignerCOM.dll contains no icons
// ************************************************************************ //
// *************************************************************************//
// NOTE:                                                                      
// Items guarded by $IFDEF_LIVE_SERVER_AT_DESIGN_TIME are used by properties  
// which return objects that may need to be explicitly created via a function 
// call prior to any access via the property. These items have been disabled  
// in order to prevent accidental use from within the object inspector. You   
// may enable them by defining LIVE_SERVER_AT_DESIGN_TIME or by selectively   
// removing them from the $IFDEF blocks. However, such items must still be    
// programmatically created via a method of the appropriate CoClass before    
// they can be used.                                                          
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, OleServer, StdVCL, Variants;
  

// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  BRYSIGNERCOMLibMajorVersion = 1;
  BRYSIGNERCOMLibMinorVersion = 0;

  LIBID_BRYSIGNERCOMLib: TGUID = '{6A8AD962-44DD-4F00-97EB-E82C8FEAC5E5}';

  IID_ICRL: TGUID = '{92B1DD0B-6C77-4D47-B928-EE83F9B26B09}';
  CLASS_CRL: TGUID = '{2287F887-557A-4EE4-AB30-BFF658121BA0}';
  IID_ICertificado: TGUID = '{D00A8723-BFDF-4582-82FF-5B05F2FC2D79}';
  CLASS_Certificado: TGUID = '{86DE8D4F-D663-4395-82E9-051D79766FB9}';
  IID_ICarimbo: TGUID = '{3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}';
  CLASS_Carimbo: TGUID = '{83C5AF6B-0CEF-4A58-9ACC-009ABEF94260}';
  IID_IRepositorio: TGUID = '{7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}';
  CLASS_Repositorio: TGUID = '{5B12C0E6-F4E6-4A74-9314-89AFA21AA1C5}';
  IID_IAssinador: TGUID = '{FE9B621E-FDFD-4299-B7E0-E7E753693624}';
  CLASS_Assinador: TGUID = '{D74AA60A-D77B-4482-8850-4768E636AAC7}';
  IID_IVerificador: TGUID = '{F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}';
  CLASS_Verificador: TGUID = '{1690EE68-8D31-473D-9CEB-C51D56A76D7B}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  ICRL = interface;
  ICRLDisp = dispinterface;
  ICertificado = interface;
  ICertificadoDisp = dispinterface;
  ICarimbo = interface;
  ICarimboDisp = dispinterface;
  IRepositorio = interface;
  IRepositorioDisp = dispinterface;
  IAssinador = interface;
  IAssinadorDisp = dispinterface;
  IVerificador = interface;
  IVerificadorDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  CRL = ICRL;
  Certificado = ICertificado;
  Carimbo = ICarimbo;
  Repositorio = IRepositorio;
  Assinador = IAssinador;
  Verificador = IVerificador;


// *********************************************************************//
// Interface: ICRL
// Flags:     (4544) Dual NonExtensible OleAutomation Dispatchable
// GUID:      {92B1DD0B-6C77-4D47-B928-EE83F9B26B09}
// *********************************************************************//
  ICRL = interface(IDispatch)
    ['{92B1DD0B-6C77-4D47-B928-EE83F9B26B09}']
    function getDataInicio: WideString; safecall;
    function getDataTermino: WideString; safecall;
    function getAlgoritmoAssinatura: WideString; safecall;
    function getEmissorCN: WideString; safecall;
    function getEmissorC: WideString; safecall;
    function getEmissorO: WideString; safecall;
    function getEmissorOU: WideString; safecall;
    function getEmissorL: WideString; safecall;
    function getEmissorS: WideString; safecall;
    function getEmissorE: WideString; safecall;
    function getEmissor: WideString; safecall;
    function getNumeroLista: SYSINT; safecall;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString; safecall;
    function getVersao: SYSINT; safecall;
    function visualizarLCR: SYSINT; safecall;
    function inicializar(const __arquivo: WideString): SYSINT; safecall;
    procedure finalizar; safecall;
    function instalar: SYSINT; safecall;
  end;

// *********************************************************************//
// DispIntf:  ICRLDisp
// Flags:     (4544) Dual NonExtensible OleAutomation Dispatchable
// GUID:      {92B1DD0B-6C77-4D47-B928-EE83F9B26B09}
// *********************************************************************//
  ICRLDisp = dispinterface
    ['{92B1DD0B-6C77-4D47-B928-EE83F9B26B09}']
    function getDataInicio: WideString; dispid 1;
    function getDataTermino: WideString; dispid 2;
    function getAlgoritmoAssinatura: WideString; dispid 3;
    function getEmissorCN: WideString; dispid 4;
    function getEmissorC: WideString; dispid 5;
    function getEmissorO: WideString; dispid 6;
    function getEmissorOU: WideString; dispid 7;
    function getEmissorL: WideString; dispid 8;
    function getEmissorS: WideString; dispid 9;
    function getEmissorE: WideString; dispid 10;
    function getEmissor: WideString; dispid 11;
    function getNumeroLista: SYSINT; dispid 12;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString; dispid 13;
    function getVersao: SYSINT; dispid 14;
    function visualizarLCR: SYSINT; dispid 15;
    function inicializar(const __arquivo: WideString): SYSINT; dispid 16;
    procedure finalizar; dispid 17;
    function instalar: SYSINT; dispid 18;
  end;

// *********************************************************************//
// Interface: ICertificado
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {D00A8723-BFDF-4582-82FF-5B05F2FC2D79}
// *********************************************************************//
  ICertificado = interface(IDispatch)
    ['{D00A8723-BFDF-4582-82FF-5B05F2FC2D79}']
    procedure inicialize(const __strHash: WideString); safecall;
    procedure finalize; safecall;
    function getDataTermino: WideString; safecall;
    function mostrarCertificado: SYSINT; safecall;
    function getIdCertificado: WideString; safecall;
    function getAssunto: WideString; safecall;
    function getAssuntoCN: WideString; safecall;
    function getAssuntoO: WideString; safecall;
    function getAssuntoOU: WideString; safecall;
    function getAssuntoL: WideString; safecall;
    function getAssuntoS: WideString; safecall;
    function getAssuntoC: WideString; safecall;
    function getAssuntoE: WideString; safecall;
    function getEmissor: WideString; safecall;
    function getEmissorCN: WideString; safecall;
    function getEmissorO: WideString; safecall;
    function getEmissorOU: WideString; safecall;
    function getEmissorL: WideString; safecall;
    function getEmissorS: WideString; safecall;
    function getEmissorC: WideString; safecall;
    function getEmissorE: WideString; safecall;
    function getNumeroSerial: WideString; safecall;
    function getDataInicio: WideString; safecall;
    function existeExtensao(const __OID: WideString): Integer; safecall;
    function existeFinalidade(const __OID: WideString): Integer; safecall;
    function existeUsoChave(const __OID: WideString): Integer; safecall;
    function baixarCRL: SYSINT; safecall;
    function existeCRL: SYSINT; safecall;
    function verificarCRL: SYSINT; safecall;
    function verificarValidade: SYSINT; safecall;
    function status: SYSINT; safecall;
    function getCountCRL: SYSINT; safecall;
    function getCountUsoChaves: SYSINT; safecall;
    function getCountFinalidades: SYSINT; safecall;
    function getCountExtensoes: SYSINT; safecall;
    function getEndCRL(__posicao: SYSINT): WideString; safecall;
    function getUsoChaves(__posicao: SYSINT): WideString; safecall;
    function getFinalidades(__posicao: SYSINT): WideString; safecall;
    function getExtensaoValor(__posicao: SYSINT): WideString; safecall;
    function getExtensaoOID(__posicao: SYSINT): WideString; safecall;
    function getExtensaoCritica(__posicao: SYSINT): SYSINT; safecall;
    function getCountCertificadosCadeia: SYSINT; safecall;
    function getStatusCadeiaCertificacao(__posicao: SYSINT): SYSINT; safecall;
    function getErrorCadeiaCertificacao(__posicao: SYSINT): SYSINT; safecall;
    function getHashCadeiaCertificacao(__posicao: SYSINT): WideString; safecall;
    function verificarCadeiaCertificacao: SYSINT; safecall;
    function existeChavePrivada: Integer; safecall;
    function setSenhaProxy(const __senha: WideString): SYSINT; safecall;
    function setLoginProxy(const __login: WideString): SYSINT; safecall;
    function getCPF: WideString; safecall;
    function getRG: WideString; safecall;
    function getDataNascimentoTitular: WideString; safecall;
    function getNIS: WideString; safecall;
    function getOrgaoExpedidor: WideString; safecall;
    function getCEIPF: WideString; safecall;
    function getCEIPJ: WideString; safecall;
    function getCNPJ: WideString; safecall;
    function getNomeResponsavel: WideString; safecall;
    function getTituloEleitor: WideString; safecall;
    function getZonaEleitoral: WideString; safecall;
    function getSecaoEleitoral: WideString; safecall;
    function getMunicipioUF: WideString; safecall;
    function getCountPoliticas: SYSINT; safecall;
    function getPoliticaOID(__posicao: SYSINT): WideString; safecall;
    function getPoliticaCPS(__posicao: SYSINT): WideString; safecall;
    procedure InicializeCertBase64(const __strCert64: WideString); safecall;
    function getRFC822Name: WideString; safecall;
    function setDataVerifica(const __dataVerifica: WideString): SYSINT; safecall;
    function getUsoExtendidoChaves(__posicao: SYSINT): WideString; safecall;
    function getCountUsoExtendidoChaves: SYSINT; safecall;
    function isAC: SYSINT; safecall;
    function isRestricaoCaminho: SYSINT; safecall;
    function getTamanhoRestricaoCaminho: SYSINT; safecall;
    function getTipoICPBrasil: SYSINT; safecall;
    function getHashIdentificadorAutoridade: WideString; safecall;
    function getCountAssuntoOU: SYSINT; safecall;
    function getMultiplosAssuntoOU(__indice: SYSINT): WideString; safecall;
    function getCountEmissorOU: SYSINT; safecall;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString; safecall;
    function setCRL(const __crl: ICRL): SYSINT; safecall;
    function getLCR: ICRL; safecall;
    function getMotivoRevogacao: WideString; safecall;
    function getDataRevogacao: WideString; safecall;
    function isAutoAssinada: Integer; safecall;
    function getCountEndCRL: SYSINT; safecall;
    function getTamanhoChave: Integer; safecall;
    function getCountOIDSubjectAlternativeName: Integer; safecall;
    function getOIDSubjectAlternativeName(indice: Integer): WideString; safecall;
  end;

// *********************************************************************//
// DispIntf:  ICertificadoDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {D00A8723-BFDF-4582-82FF-5B05F2FC2D79}
// *********************************************************************//
  ICertificadoDisp = dispinterface
    ['{D00A8723-BFDF-4582-82FF-5B05F2FC2D79}']
    procedure inicialize(const __strHash: WideString); dispid 1;
    procedure finalize; dispid 2;
    function getDataTermino: WideString; dispid 3;
    function mostrarCertificado: SYSINT; dispid 4;
    function getIdCertificado: WideString; dispid 5;
    function getAssunto: WideString; dispid 6;
    function getAssuntoCN: WideString; dispid 7;
    function getAssuntoO: WideString; dispid 8;
    function getAssuntoOU: WideString; dispid 9;
    function getAssuntoL: WideString; dispid 10;
    function getAssuntoS: WideString; dispid 11;
    function getAssuntoC: WideString; dispid 12;
    function getAssuntoE: WideString; dispid 13;
    function getEmissor: WideString; dispid 14;
    function getEmissorCN: WideString; dispid 15;
    function getEmissorO: WideString; dispid 16;
    function getEmissorOU: WideString; dispid 17;
    function getEmissorL: WideString; dispid 18;
    function getEmissorS: WideString; dispid 19;
    function getEmissorC: WideString; dispid 20;
    function getEmissorE: WideString; dispid 21;
    function getNumeroSerial: WideString; dispid 22;
    function getDataInicio: WideString; dispid 23;
    function existeExtensao(const __OID: WideString): Integer; dispid 24;
    function existeFinalidade(const __OID: WideString): Integer; dispid 25;
    function existeUsoChave(const __OID: WideString): Integer; dispid 26;
    function baixarCRL: SYSINT; dispid 27;
    function existeCRL: SYSINT; dispid 28;
    function verificarCRL: SYSINT; dispid 29;
    function verificarValidade: SYSINT; dispid 30;
    function status: SYSINT; dispid 31;
    function getCountCRL: SYSINT; dispid 33;
    function getCountUsoChaves: SYSINT; dispid 34;
    function getCountFinalidades: SYSINT; dispid 35;
    function getCountExtensoes: SYSINT; dispid 36;
    function getEndCRL(__posicao: SYSINT): WideString; dispid 37;
    function getUsoChaves(__posicao: SYSINT): WideString; dispid 38;
    function getFinalidades(__posicao: SYSINT): WideString; dispid 39;
    function getExtensaoValor(__posicao: SYSINT): WideString; dispid 40;
    function getExtensaoOID(__posicao: SYSINT): WideString; dispid 41;
    function getExtensaoCritica(__posicao: SYSINT): SYSINT; dispid 42;
    function getCountCertificadosCadeia: SYSINT; dispid 43;
    function getStatusCadeiaCertificacao(__posicao: SYSINT): SYSINT; dispid 44;
    function getErrorCadeiaCertificacao(__posicao: SYSINT): SYSINT; dispid 45;
    function getHashCadeiaCertificacao(__posicao: SYSINT): WideString; dispid 46;
    function verificarCadeiaCertificacao: SYSINT; dispid 47;
    function existeChavePrivada: Integer; dispid 48;
    function setSenhaProxy(const __senha: WideString): SYSINT; dispid 49;
    function setLoginProxy(const __login: WideString): SYSINT; dispid 50;
    function getCPF: WideString; dispid 51;
    function getRG: WideString; dispid 52;
    function getDataNascimentoTitular: WideString; dispid 53;
    function getNIS: WideString; dispid 54;
    function getOrgaoExpedidor: WideString; dispid 55;
    function getCEIPF: WideString; dispid 56;
    function getCEIPJ: WideString; dispid 57;
    function getCNPJ: WideString; dispid 58;
    function getNomeResponsavel: WideString; dispid 59;
    function getTituloEleitor: WideString; dispid 60;
    function getZonaEleitoral: WideString; dispid 61;
    function getSecaoEleitoral: WideString; dispid 62;
    function getMunicipioUF: WideString; dispid 63;
    function getCountPoliticas: SYSINT; dispid 64;
    function getPoliticaOID(__posicao: SYSINT): WideString; dispid 65;
    function getPoliticaCPS(__posicao: SYSINT): WideString; dispid 66;
    procedure InicializeCertBase64(const __strCert64: WideString); dispid 67;
    function getRFC822Name: WideString; dispid 68;
    function setDataVerifica(const __dataVerifica: WideString): SYSINT; dispid 69;
    function getUsoExtendidoChaves(__posicao: SYSINT): WideString; dispid 70;
    function getCountUsoExtendidoChaves: SYSINT; dispid 71;
    function isAC: SYSINT; dispid 72;
    function isRestricaoCaminho: SYSINT; dispid 73;
    function getTamanhoRestricaoCaminho: SYSINT; dispid 74;
    function getTipoICPBrasil: SYSINT; dispid 75;
    function getHashIdentificadorAutoridade: WideString; dispid 76;
    function getCountAssuntoOU: SYSINT; dispid 77;
    function getMultiplosAssuntoOU(__indice: SYSINT): WideString; dispid 78;
    function getCountEmissorOU: SYSINT; dispid 79;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString; dispid 80;
    function setCRL(const __crl: ICRL): SYSINT; dispid 81;
    function getLCR: ICRL; dispid 82;
    function getMotivoRevogacao: WideString; dispid 83;
    function getDataRevogacao: WideString; dispid 84;
    function isAutoAssinada: Integer; dispid 85;
    function getCountEndCRL: SYSINT; dispid 86;
    function getTamanhoChave: Integer; dispid 87;
    function getCountOIDSubjectAlternativeName: Integer; dispid 88;
    function getOIDSubjectAlternativeName(indice: Integer): WideString; dispid 89;
  end;

// *********************************************************************//
// Interface: ICarimbo
// Flags:     (4544) Dual NonExtensible OleAutomation Dispatchable
// GUID:      {3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}
// *********************************************************************//
  ICarimbo = interface(IDispatch)
    ['{3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}']
    function getCertificadoCarimbadora: ICertificado; safecall;
    function getStatus: SYSINT; safecall;
    function getDataUTC: WideString; safecall;
    function getDataCalculada: WideString; safecall;
    function getVersao: SYSINT; safecall;
    function getTSAHash: WideString; safecall;
    function getNumeroSerial: WideString; safecall;
    function getPolitica: WideString; safecall;
    function isOrdering: SYSINT; safecall;
    function getNonce: SYSINT; safecall;
    function getAccuracyMicrosegundos: WideString; safecall;
    function getAccuracyMilisegundos: WideString; safecall;
    function getAccuracySegundos: WideString; safecall;
  end;

// *********************************************************************//
// DispIntf:  ICarimboDisp
// Flags:     (4544) Dual NonExtensible OleAutomation Dispatchable
// GUID:      {3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}
// *********************************************************************//
  ICarimboDisp = dispinterface
    ['{3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}']
    function getCertificadoCarimbadora: ICertificado; dispid 1;
    function getStatus: SYSINT; dispid 2;
    function getDataUTC: WideString; dispid 3;
    function getDataCalculada: WideString; dispid 4;
    function getVersao: SYSINT; dispid 5;
    function getTSAHash: WideString; dispid 6;
    function getNumeroSerial: WideString; dispid 7;
    function getPolitica: WideString; dispid 8;
    function isOrdering: SYSINT; dispid 9;
    function getNonce: SYSINT; dispid 10;
    function getAccuracyMicrosegundos: WideString; dispid 11;
    function getAccuracyMilisegundos: WideString; dispid 12;
    function getAccuracySegundos: WideString; dispid 13;
  end;

// *********************************************************************//
// Interface: IRepositorio
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}
// *********************************************************************//
  IRepositorio = interface(IDispatch)
    ['{7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}']
    procedure finalize; safecall;
    procedure inicialize(const __local: WideString; __armazenamento: SYSINT); safecall;
    function adicionar(const __certificado: WideString; __FLAG: SYSINT): SYSINT; safecall;
    function adicionarDoArquivo(const __arquivo: WideString): SYSINT; safecall;
    function getCertificado(__i: SYSINT): ICertificado; safecall;
    function getCountCertificados: SYSINT; safecall;
    function getCertificadoBase64(__i: SYSINT): WideString; safecall;
    function existeCartaoLeitora: Integer; safecall;
  end;

// *********************************************************************//
// DispIntf:  IRepositorioDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}
// *********************************************************************//
  IRepositorioDisp = dispinterface
    ['{7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}']
    procedure finalize; dispid 1;
    procedure inicialize(const __local: WideString; __armazenamento: SYSINT); dispid 2;
    function adicionar(const __certificado: WideString; __FLAG: SYSINT): SYSINT; dispid 3;
    function adicionarDoArquivo(const __arquivo: WideString): SYSINT; dispid 4;
    function getCertificado(__i: SYSINT): ICertificado; dispid 5;
    function getCountCertificados: SYSINT; dispid 6;
    function getCertificadoBase64(__i: SYSINT): WideString; dispid 7;
    function existeCartaoLeitora: Integer; dispid 8;
  end;

// *********************************************************************//
// Interface: IAssinador
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {FE9B621E-FDFD-4299-B7E0-E7E753693624}
// *********************************************************************//
  IAssinador = interface(IDispatch)
    ['{FE9B621E-FDFD-4299-B7E0-E7E753693624}']
    function assineArquivo(const __arq: WideString; const __caminho: WideString; 
                           const __descricao: WideString; const __hashCert: WideString): SYSINT; safecall;
    function coAssineArquivo(const __arq: WideString; const __caminho: WideString; 
                             const __descricao: WideString; const __hash: WideString): SYSINT; safecall;
    function assineMem(const __mem: WideString; const __descricao: WideString; 
                       const __hashCert: WideString; out __assinatura: WideString): SYSINT; safecall;
    function retorneAssinaturaMem: WideString; safecall;
    function coAssineMem(const __mem: WideString; const __descricao: WideString; 
                         const __hashCert: WideString; out __assinatura: WideString): SYSINT; safecall;
    function setDataHora(__dia: SYSINT; __mes: SYSINT; __ano: SYSINT; __hora: SYSINT; 
                         __minutos: SYSINT; __segundos: SYSINT; __milisegundos: SYSINT): SYSINT; safecall;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT; safecall;
    function hashDoArquivo(const __arquivo: WideString): WideString; safecall;
    function hashDados(const __dados: WideString; __tamanho: SYSINT): WideString; safecall;
    function assineArquivoDetached(const __arq: WideString; const __caminho: WideString; 
                                   const __descricao: WideString; const __hashCert: WideString): SYSINT; safecall;
    function assineMemDetached(const __mem: WideString; const __descricao: WideString; 
                               const __hashCert: WideString; var __assinatura: WideString): SYSINT; safecall;
    function setAddCrlCaminhoCert(__intAdicionar: SYSINT): SYSINT; safecall;
    function coAssineArquivoDetached(const __arq: WideString; const __original: WideString; 
                                     const __caminho: WideString; const __descricao: WideString; 
                                     const __hashCert: WideString): SYSINT; safecall;
    function coAssineMemDetached(const __mem: WideString; const __original: WideString; 
                                 const __descricao: WideString; const __hashCert: WideString; 
                                 var __assinatura: WideString): SYSINT; safecall;
  end;

// *********************************************************************//
// DispIntf:  IAssinadorDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {FE9B621E-FDFD-4299-B7E0-E7E753693624}
// *********************************************************************//
  IAssinadorDisp = dispinterface
    ['{FE9B621E-FDFD-4299-B7E0-E7E753693624}']
    function assineArquivo(const __arq: WideString; const __caminho: WideString; 
                           const __descricao: WideString; const __hashCert: WideString): SYSINT; dispid 1;
    function coAssineArquivo(const __arq: WideString; const __caminho: WideString; 
                             const __descricao: WideString; const __hash: WideString): SYSINT; dispid 2;
    function assineMem(const __mem: WideString; const __descricao: WideString; 
                       const __hashCert: WideString; out __assinatura: WideString): SYSINT; dispid 3;
    function retorneAssinaturaMem: WideString; dispid 4;
    function coAssineMem(const __mem: WideString; const __descricao: WideString; 
                         const __hashCert: WideString; out __assinatura: WideString): SYSINT; dispid 5;
    function setDataHora(__dia: SYSINT; __mes: SYSINT; __ano: SYSINT; __hora: SYSINT; 
                         __minutos: SYSINT; __segundos: SYSINT; __milisegundos: SYSINT): SYSINT; dispid 6;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT; dispid 7;
    function hashDoArquivo(const __arquivo: WideString): WideString; dispid 8;
    function hashDados(const __dados: WideString; __tamanho: SYSINT): WideString; dispid 9;
    function assineArquivoDetached(const __arq: WideString; const __caminho: WideString; 
                                   const __descricao: WideString; const __hashCert: WideString): SYSINT; dispid 10;
    function assineMemDetached(const __mem: WideString; const __descricao: WideString; 
                               const __hashCert: WideString; var __assinatura: WideString): SYSINT; dispid 11;
    function setAddCrlCaminhoCert(__intAdicionar: SYSINT): SYSINT; dispid 12;
    function coAssineArquivoDetached(const __arq: WideString; const __original: WideString; 
                                     const __caminho: WideString; const __descricao: WideString; 
                                     const __hashCert: WideString): SYSINT; dispid 13;
    function coAssineMemDetached(const __mem: WideString; const __original: WideString; 
                                 const __descricao: WideString; const __hashCert: WideString; 
                                 var __assinatura: WideString): SYSINT; dispid 14;
  end;

// *********************************************************************//
// Interface: IVerificador
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}
// *********************************************************************//
  IVerificador = interface(IDispatch)
    ['{F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}']
    function verifiqueAssinatura(const __arq: WideString): SYSINT; safecall;
    function getCountAssinaturas: SYSINT; safecall;
    function getStatusCertificado(__index: SYSINT): SYSINT; safecall;
    function getStatusAssinatura(__index: SYSINT): SYSINT; safecall;
    function getHash(__index: SYSINT): WideString; safecall;
    function getArquivo(__index: SYSINT): WideString; safecall;
    function getNomeArquivo(__index: SYSINT): WideString; safecall;
    function getDescricao(__index: SYSINT): WideString; safecall;
    function extrairDocumento(const __arq: WideString; const __caminho: WideString; __FLAG: SYSINT; 
                              __toVerify: Integer; __sobrescrever: Integer): SYSINT; safecall;
    function getCertificado(__index: SYSINT): ICertificado; safecall;
    function getStatusCadeia(__index: SYSINT): SYSINT; safecall;
    procedure inicialize; safecall;
    procedure finalize; safecall;
    function verifiqueAssinaturaMem(const __mem: WideString): SYSINT; safecall;
    function getDataUTC(__index: SYSINT): WideString; safecall;
    function getDataCalculada(__index: SYSINT): WideString; safecall;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT; safecall;
    function getHashDocumentoOriginal(__index: SYSINT): WideString; safecall;
    function verifiqueAssinaturaDetached(const __arqAssinado: WideString; 
                                         const __arqOriginal: WideString): SYSINT; safecall;
    function verifiqueAssinaturaMemDetached(const __memAssinada: WideString; 
                                            const __memOriginal: WideString): SYSINT; safecall;
    function getDataCarimboTempo(__index: SYSINT): WideString; safecall;
    function getStatusCarimboTempo(__index: SYSINT): SYSINT; safecall;
    function getCarimboAssinatura(indice: SYSINT): ICarimbo; safecall;
    function getLCR(__indice: SYSINT): ICRL; safecall;
    function getCountCRLs: SYSINT; safecall;
    function isArquivoAssinadoDetached(const __arq: WideString): SYSINT; safecall;
    function getCountCertificadosCadeia: SYSINT; safecall;
    function getCertificadoCadeia(__indice: SYSINT): ICertificado; safecall;
    procedure setInstalarCadeia(__instalarCadeia: Integer); safecall;
    procedure setBaixarLCR(__baixarLCR: Integer); safecall;
    procedure setInstalarCertAssinador(__instalarCertAssinador: Integer); safecall;
    procedure setLoginProxy(const login: WideString); safecall;
    procedure setSenhaProxy(const senha: WideString); safecall;
    procedure setURLProxy(const url: WideString); safecall;
  end;

// *********************************************************************//
// DispIntf:  IVerificadorDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}
// *********************************************************************//
  IVerificadorDisp = dispinterface
    ['{F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}']
    function verifiqueAssinatura(const __arq: WideString): SYSINT; dispid 1;
    function getCountAssinaturas: SYSINT; dispid 2;
    function getStatusCertificado(__index: SYSINT): SYSINT; dispid 3;
    function getStatusAssinatura(__index: SYSINT): SYSINT; dispid 4;
    function getHash(__index: SYSINT): WideString; dispid 5;
    function getArquivo(__index: SYSINT): WideString; dispid 6;
    function getNomeArquivo(__index: SYSINT): WideString; dispid 7;
    function getDescricao(__index: SYSINT): WideString; dispid 8;
    function extrairDocumento(const __arq: WideString; const __caminho: WideString; __FLAG: SYSINT; 
                              __toVerify: Integer; __sobrescrever: Integer): SYSINT; dispid 9;
    function getCertificado(__index: SYSINT): ICertificado; dispid 10;
    function getStatusCadeia(__index: SYSINT): SYSINT; dispid 11;
    procedure inicialize; dispid 12;
    procedure finalize; dispid 13;
    function verifiqueAssinaturaMem(const __mem: WideString): SYSINT; dispid 14;
    function getDataUTC(__index: SYSINT): WideString; dispid 15;
    function getDataCalculada(__index: SYSINT): WideString; dispid 16;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT; dispid 17;
    function getHashDocumentoOriginal(__index: SYSINT): WideString; dispid 18;
    function verifiqueAssinaturaDetached(const __arqAssinado: WideString; 
                                         const __arqOriginal: WideString): SYSINT; dispid 19;
    function verifiqueAssinaturaMemDetached(const __memAssinada: WideString; 
                                            const __memOriginal: WideString): SYSINT; dispid 20;
    function getDataCarimboTempo(__index: SYSINT): WideString; dispid 21;
    function getStatusCarimboTempo(__index: SYSINT): SYSINT; dispid 22;
    function getCarimboAssinatura(indice: SYSINT): ICarimbo; dispid 23;
    function getLCR(__indice: SYSINT): ICRL; dispid 24;
    function getCountCRLs: SYSINT; dispid 25;
    function isArquivoAssinadoDetached(const __arq: WideString): SYSINT; dispid 26;
    function getCountCertificadosCadeia: SYSINT; dispid 27;
    function getCertificadoCadeia(__indice: SYSINT): ICertificado; dispid 28;
    procedure setInstalarCadeia(__instalarCadeia: Integer); dispid 29;
    procedure setBaixarLCR(__baixarLCR: Integer); dispid 30;
    procedure setInstalarCertAssinador(__instalarCertAssinador: Integer); dispid 31;
    procedure setLoginProxy(const login: WideString); dispid 32;
    procedure setSenhaProxy(const senha: WideString); dispid 33;
    procedure setURLProxy(const url: WideString); dispid 34;
  end;

// *********************************************************************//
// The Class CoCRL provides a Create and CreateRemote method to          
// create instances of the default interface ICRL exposed by              
// the CoClass CRL. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCRL = class
    class function Create: ICRL;
    class function CreateRemote(const MachineName: string): ICRL;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCRL
// Help String      : CRL Class
// Default Interface: ICRL
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCRLProperties= class;
{$ENDIF}
  TCRL = class(TOleServer)
  private
    FIntf:        ICRL;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCRLProperties;
    function      GetServerProperties: TCRLProperties;
{$ENDIF}
    function      GetDefaultInterface: ICRL;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: ICRL);
    procedure Disconnect; override;
    function getDataInicio: WideString;
    function getDataTermino: WideString;
    function getAlgoritmoAssinatura: WideString;
    function getEmissorCN: WideString;
    function getEmissorC: WideString;
    function getEmissorO: WideString;
    function getEmissorOU: WideString;
    function getEmissorL: WideString;
    function getEmissorS: WideString;
    function getEmissorE: WideString;
    function getEmissor: WideString;
    function getNumeroLista: SYSINT;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString;
    function getVersao: SYSINT;
    function visualizarLCR: SYSINT;
    function inicializar(const __arquivo: WideString): SYSINT;
    procedure finalizar;
    function instalar: SYSINT;
    property DefaultInterface: ICRL read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCRLProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCRL
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCRLProperties = class(TPersistent)
  private
    FServer:    TCRL;
    function    GetDefaultInterface: ICRL;
    constructor Create(AServer: TCRL);
  protected
  public
    property DefaultInterface: ICRL read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoCertificado provides a Create and CreateRemote method to          
// create instances of the default interface ICertificado exposed by              
// the CoClass Certificado. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCertificado = class
    class function Create: ICertificado;
    class function CreateRemote(const MachineName: string): ICertificado;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCertificado
// Help String      : Certificado Class
// Default Interface: ICertificado
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCertificadoProperties= class;
{$ENDIF}
  TCertificado = class(TOleServer)
  private
    FIntf:        ICertificado;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCertificadoProperties;
    function      GetServerProperties: TCertificadoProperties;
{$ENDIF}
    function      GetDefaultInterface: ICertificado;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: ICertificado);
    procedure Disconnect; override;
    procedure inicialize(const __strHash: WideString);
    procedure finalize;
    function getDataTermino: WideString;
    function mostrarCertificado: SYSINT;
    function getIdCertificado: WideString;
    function getAssunto: WideString;
    function getAssuntoCN: WideString;
    function getAssuntoO: WideString;
    function getAssuntoOU: WideString;
    function getAssuntoL: WideString;
    function getAssuntoS: WideString;
    function getAssuntoC: WideString;
    function getAssuntoE: WideString;
    function getEmissor: WideString;
    function getEmissorCN: WideString;
    function getEmissorO: WideString;
    function getEmissorOU: WideString;
    function getEmissorL: WideString;
    function getEmissorS: WideString;
    function getEmissorC: WideString;
    function getEmissorE: WideString;
    function getNumeroSerial: WideString;
    function getDataInicio: WideString;
    function existeExtensao(const __OID: WideString): Integer;
    function existeFinalidade(const __OID: WideString): Integer;
    function existeUsoChave(const __OID: WideString): Integer;
    function baixarCRL: SYSINT;
    function existeCRL: SYSINT;
    function verificarCRL: SYSINT;
    function verificarValidade: SYSINT;
    function status: SYSINT;
    function getCountCRL: SYSINT;
    function getCountUsoChaves: SYSINT;
    function getCountFinalidades: SYSINT;
    function getCountExtensoes: SYSINT;
    function getEndCRL(__posicao: SYSINT): WideString;
    function getUsoChaves(__posicao: SYSINT): WideString;
    function getFinalidades(__posicao: SYSINT): WideString;
    function getExtensaoValor(__posicao: SYSINT): WideString;
    function getExtensaoOID(__posicao: SYSINT): WideString;
    function getExtensaoCritica(__posicao: SYSINT): SYSINT;
    function getCountCertificadosCadeia: SYSINT;
    function getStatusCadeiaCertificacao(__posicao: SYSINT): SYSINT;
    function getErrorCadeiaCertificacao(__posicao: SYSINT): SYSINT;
    function getHashCadeiaCertificacao(__posicao: SYSINT): WideString;
    function verificarCadeiaCertificacao: SYSINT;
    function existeChavePrivada: Integer;
    function setSenhaProxy(const __senha: WideString): SYSINT;
    function setLoginProxy(const __login: WideString): SYSINT;
    function getCPF: WideString;
    function getRG: WideString;
    function getDataNascimentoTitular: WideString;
    function getNIS: WideString;
    function getOrgaoExpedidor: WideString;
    function getCEIPF: WideString;
    function getCEIPJ: WideString;
    function getCNPJ: WideString;
    function getNomeResponsavel: WideString;
    function getTituloEleitor: WideString;
    function getZonaEleitoral: WideString;
    function getSecaoEleitoral: WideString;
    function getMunicipioUF: WideString;
    function getCountPoliticas: SYSINT;
    function getPoliticaOID(__posicao: SYSINT): WideString;
    function getPoliticaCPS(__posicao: SYSINT): WideString;
    procedure InicializeCertBase64(const __strCert64: WideString);
    function getRFC822Name: WideString;
    function setDataVerifica(const __dataVerifica: WideString): SYSINT;
    function getUsoExtendidoChaves(__posicao: SYSINT): WideString;
    function getCountUsoExtendidoChaves: SYSINT;
    function isAC: SYSINT;
    function isRestricaoCaminho: SYSINT;
    function getTamanhoRestricaoCaminho: SYSINT;
    function getTipoICPBrasil: SYSINT;
    function getHashIdentificadorAutoridade: WideString;
    function getCountAssuntoOU: SYSINT;
    function getMultiplosAssuntoOU(__indice: SYSINT): WideString;
    function getCountEmissorOU: SYSINT;
    function getMultiplosEmissorOU(__indice: SYSINT): WideString;
    function setCRL(const __crl: ICRL): SYSINT;
    function getLCR: ICRL;
    function getMotivoRevogacao: WideString;
    function getDataRevogacao: WideString;
    function isAutoAssinada: Integer;
    function getCountEndCRL: SYSINT;
    function getTamanhoChave: Integer;
    function getCountOIDSubjectAlternativeName: Integer;
    function getOIDSubjectAlternativeName(indice: Integer): WideString;
    property DefaultInterface: ICertificado read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCertificadoProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCertificado
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCertificadoProperties = class(TPersistent)
  private
    FServer:    TCertificado;
    function    GetDefaultInterface: ICertificado;
    constructor Create(AServer: TCertificado);
  protected
  public
    property DefaultInterface: ICertificado read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoCarimbo provides a Create and CreateRemote method to          
// create instances of the default interface ICarimbo exposed by              
// the CoClass Carimbo. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCarimbo = class
    class function Create: ICarimbo;
    class function CreateRemote(const MachineName: string): ICarimbo;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCarimbo
// Help String      : Carimbo Class
// Default Interface: ICarimbo
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCarimboProperties= class;
{$ENDIF}
  TCarimbo = class(TOleServer)
  private
    FIntf:        ICarimbo;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCarimboProperties;
    function      GetServerProperties: TCarimboProperties;
{$ENDIF}
    function      GetDefaultInterface: ICarimbo;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: ICarimbo);
    procedure Disconnect; override;
    function getCertificadoCarimbadora: ICertificado;
    function getStatus: SYSINT;
    function getDataUTC: WideString;
    function getDataCalculada: WideString;
    function getVersao: SYSINT;
    function getTSAHash: WideString;
    function getNumeroSerial: WideString;
    function getPolitica: WideString;
    function isOrdering: SYSINT;
    function getNonce: SYSINT;
    function getAccuracyMicrosegundos: WideString;
    function getAccuracyMilisegundos: WideString;
    function getAccuracySegundos: WideString;
    property DefaultInterface: ICarimbo read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCarimboProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCarimbo
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCarimboProperties = class(TPersistent)
  private
    FServer:    TCarimbo;
    function    GetDefaultInterface: ICarimbo;
    constructor Create(AServer: TCarimbo);
  protected
  public
    property DefaultInterface: ICarimbo read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoRepositorio provides a Create and CreateRemote method to          
// create instances of the default interface IRepositorio exposed by              
// the CoClass Repositorio. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoRepositorio = class
    class function Create: IRepositorio;
    class function CreateRemote(const MachineName: string): IRepositorio;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TRepositorio
// Help String      : Repositorio Class
// Default Interface: IRepositorio
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TRepositorioProperties= class;
{$ENDIF}
  TRepositorio = class(TOleServer)
  private
    FIntf:        IRepositorio;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TRepositorioProperties;
    function      GetServerProperties: TRepositorioProperties;
{$ENDIF}
    function      GetDefaultInterface: IRepositorio;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: IRepositorio);
    procedure Disconnect; override;
    procedure finalize;
    procedure inicialize(const __local: WideString; __armazenamento: SYSINT);
    function adicionar(const __certificado: WideString; __FLAG: SYSINT): SYSINT;
    function adicionarDoArquivo(const __arquivo: WideString): SYSINT;
    function getCertificado(__i: SYSINT): ICertificado;
    function getCountCertificados: SYSINT;
    function getCertificadoBase64(__i: SYSINT): WideString;
    function existeCartaoLeitora: Integer;
    property DefaultInterface: IRepositorio read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TRepositorioProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TRepositorio
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TRepositorioProperties = class(TPersistent)
  private
    FServer:    TRepositorio;
    function    GetDefaultInterface: IRepositorio;
    constructor Create(AServer: TRepositorio);
  protected
  public
    property DefaultInterface: IRepositorio read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAssinador provides a Create and CreateRemote method to          
// create instances of the default interface IAssinador exposed by              
// the CoClass Assinador. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAssinador = class
    class function Create: IAssinador;
    class function CreateRemote(const MachineName: string): IAssinador;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TAssinador
// Help String      : Assinador Class
// Default Interface: IAssinador
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TAssinadorProperties= class;
{$ENDIF}
  TAssinador = class(TOleServer)
  private
    FIntf:        IAssinador;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TAssinadorProperties;
    function      GetServerProperties: TAssinadorProperties;
{$ENDIF}
    function      GetDefaultInterface: IAssinador;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: IAssinador);
    procedure Disconnect; override;
    function assineArquivo(const __arq: WideString; const __caminho: WideString; 
                           const __descricao: WideString; const __hashCert: WideString): SYSINT;
    function coAssineArquivo(const __arq: WideString; const __caminho: WideString; 
                             const __descricao: WideString; const __hash: WideString): SYSINT;
    function assineMem(const __mem: WideString; const __descricao: WideString; 
                       const __hashCert: WideString; out __assinatura: WideString): SYSINT;
    function retorneAssinaturaMem: WideString;
    function coAssineMem(const __mem: WideString; const __descricao: WideString; 
                         const __hashCert: WideString; out __assinatura: WideString): SYSINT;
    function setDataHora(__dia: SYSINT; __mes: SYSINT; __ano: SYSINT; __hora: SYSINT; 
                         __minutos: SYSINT; __segundos: SYSINT; __milisegundos: SYSINT): SYSINT;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT;
    function hashDoArquivo(const __arquivo: WideString): WideString;
    function hashDados(const __dados: WideString; __tamanho: SYSINT): WideString;
    function assineArquivoDetached(const __arq: WideString; const __caminho: WideString; 
                                   const __descricao: WideString; const __hashCert: WideString): SYSINT;
    function assineMemDetached(const __mem: WideString; const __descricao: WideString; 
                               const __hashCert: WideString; var __assinatura: WideString): SYSINT;
    function setAddCrlCaminhoCert(__intAdicionar: SYSINT): SYSINT;
    function coAssineArquivoDetached(const __arq: WideString; const __original: WideString; 
                                     const __caminho: WideString; const __descricao: WideString; 
                                     const __hashCert: WideString): SYSINT;
    function coAssineMemDetached(const __mem: WideString; const __original: WideString; 
                                 const __descricao: WideString; const __hashCert: WideString; 
                                 var __assinatura: WideString): SYSINT;
    property DefaultInterface: IAssinador read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TAssinadorProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TAssinador
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TAssinadorProperties = class(TPersistent)
  private
    FServer:    TAssinador;
    function    GetDefaultInterface: IAssinador;
    constructor Create(AServer: TAssinador);
  protected
  public
    property DefaultInterface: IAssinador read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoVerificador provides a Create and CreateRemote method to          
// create instances of the default interface IVerificador exposed by              
// the CoClass Verificador. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoVerificador = class
    class function Create: IVerificador;
    class function CreateRemote(const MachineName: string): IVerificador;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TVerificador
// Help String      : Verificador Class
// Default Interface: IVerificador
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TVerificadorProperties= class;
{$ENDIF}
  TVerificador = class(TOleServer)
  private
    FIntf:        IVerificador;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TVerificadorProperties;
    function      GetServerProperties: TVerificadorProperties;
{$ENDIF}
    function      GetDefaultInterface: IVerificador;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: IVerificador);
    procedure Disconnect; override;
    function verifiqueAssinatura(const __arq: WideString): SYSINT;
    function getCountAssinaturas: SYSINT;
    function getStatusCertificado(__index: SYSINT): SYSINT;
    function getStatusAssinatura(__index: SYSINT): SYSINT;
    function getHash(__index: SYSINT): WideString;
    function getArquivo(__index: SYSINT): WideString;
    function getNomeArquivo(__index: SYSINT): WideString;
    function getDescricao(__index: SYSINT): WideString;
    function extrairDocumento(const __arq: WideString; const __caminho: WideString; __FLAG: SYSINT; 
                              __toVerify: Integer; __sobrescrever: Integer): SYSINT;
    function getCertificado(__index: SYSINT): ICertificado;
    function getStatusCadeia(__index: SYSINT): SYSINT;
    procedure inicialize;
    procedure finalize;
    function verifiqueAssinaturaMem(const __mem: WideString): SYSINT;
    function getDataUTC(__index: SYSINT): WideString;
    function getDataCalculada(__index: SYSINT): WideString;
    function setFormatoDadosMemoria(__formato: SYSINT): SYSINT;
    function getHashDocumentoOriginal(__index: SYSINT): WideString;
    function verifiqueAssinaturaDetached(const __arqAssinado: WideString; 
                                         const __arqOriginal: WideString): SYSINT;
    function verifiqueAssinaturaMemDetached(const __memAssinada: WideString; 
                                            const __memOriginal: WideString): SYSINT;
    function getDataCarimboTempo(__index: SYSINT): WideString;
    function getStatusCarimboTempo(__index: SYSINT): SYSINT;
    function getCarimboAssinatura(indice: SYSINT): ICarimbo;
    function getLCR(__indice: SYSINT): ICRL;
    function getCountCRLs: SYSINT;
    function isArquivoAssinadoDetached(const __arq: WideString): SYSINT;
    function getCountCertificadosCadeia: SYSINT;
    function getCertificadoCadeia(__indice: SYSINT): ICertificado;
    procedure setInstalarCadeia(__instalarCadeia: Integer);
    procedure setBaixarLCR(__baixarLCR: Integer);
    procedure setInstalarCertAssinador(__instalarCertAssinador: Integer);
    procedure setLoginProxy(const login: WideString);
    procedure setSenhaProxy(const senha: WideString);
    procedure setURLProxy(const url: WideString);
    property DefaultInterface: IVerificador read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TVerificadorProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TVerificador
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TVerificadorProperties = class(TPersistent)
  private
    FServer:    TVerificador;
    function    GetDefaultInterface: IVerificador;
    constructor Create(AServer: TVerificador);
  protected
  public
    property DefaultInterface: IVerificador read GetDefaultInterface;
  published
  end;
{$ENDIF}


procedure Register;

resourcestring
  dtlServerPage = 'ActiveX';

  dtlOcxPage = 'ActiveX';

implementation

uses ComObj;

class function CoCRL.Create: ICRL;
begin
  Result := CreateComObject(CLASS_CRL) as ICRL;
end;

class function CoCRL.CreateRemote(const MachineName: string): ICRL;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_CRL) as ICRL;
end;

procedure TCRL.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{2287F887-557A-4EE4-AB30-BFF658121BA0}';
    IntfIID:   '{92B1DD0B-6C77-4D47-B928-EE83F9B26B09}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCRL.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as ICRL;
  end;
end;

procedure TCRL.ConnectTo(svrIntf: ICRL);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCRL.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCRL.GetDefaultInterface: ICRL;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCRL.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCRLProperties.Create(Self);
{$ENDIF}
end;

destructor TCRL.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCRL.GetServerProperties: TCRLProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TCRL.getDataInicio: WideString;
begin
  Result := DefaultInterface.getDataInicio;
end;

function TCRL.getDataTermino: WideString;
begin
  Result := DefaultInterface.getDataTermino;
end;

function TCRL.getAlgoritmoAssinatura: WideString;
begin
  Result := DefaultInterface.getAlgoritmoAssinatura;
end;

function TCRL.getEmissorCN: WideString;
begin
  Result := DefaultInterface.getEmissorCN;
end;

function TCRL.getEmissorC: WideString;
begin
  Result := DefaultInterface.getEmissorC;
end;

function TCRL.getEmissorO: WideString;
begin
  Result := DefaultInterface.getEmissorO;
end;

function TCRL.getEmissorOU: WideString;
begin
  Result := DefaultInterface.getEmissorOU;
end;

function TCRL.getEmissorL: WideString;
begin
  Result := DefaultInterface.getEmissorL;
end;

function TCRL.getEmissorS: WideString;
begin
  Result := DefaultInterface.getEmissorS;
end;

function TCRL.getEmissorE: WideString;
begin
  Result := DefaultInterface.getEmissorE;
end;

function TCRL.getEmissor: WideString;
begin
  Result := DefaultInterface.getEmissor;
end;

function TCRL.getNumeroLista: SYSINT;
begin
  Result := DefaultInterface.getNumeroLista;
end;

function TCRL.getMultiplosEmissorOU(__indice: SYSINT): WideString;
begin
  Result := DefaultInterface.getMultiplosEmissorOU(__indice);
end;

function TCRL.getVersao: SYSINT;
begin
  Result := DefaultInterface.getVersao;
end;

function TCRL.visualizarLCR: SYSINT;
begin
  Result := DefaultInterface.visualizarLCR;
end;

function TCRL.inicializar(const __arquivo: WideString): SYSINT;
begin
  Result := DefaultInterface.inicializar(__arquivo);
end;

procedure TCRL.finalizar;
begin
  DefaultInterface.finalizar;
end;

function TCRL.instalar: SYSINT;
begin
  Result := DefaultInterface.instalar;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCRLProperties.Create(AServer: TCRL);
begin
  inherited Create;
  FServer := AServer;
end;

function TCRLProperties.GetDefaultInterface: ICRL;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoCertificado.Create: ICertificado;
begin
  Result := CreateComObject(CLASS_Certificado) as ICertificado;
end;

class function CoCertificado.CreateRemote(const MachineName: string): ICertificado;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Certificado) as ICertificado;
end;

procedure TCertificado.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{86DE8D4F-D663-4395-82E9-051D79766FB9}';
    IntfIID:   '{D00A8723-BFDF-4582-82FF-5B05F2FC2D79}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCertificado.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as ICertificado;
  end;
end;

procedure TCertificado.ConnectTo(svrIntf: ICertificado);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCertificado.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCertificado.GetDefaultInterface: ICertificado;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCertificado.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCertificadoProperties.Create(Self);
{$ENDIF}
end;

destructor TCertificado.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCertificado.GetServerProperties: TCertificadoProperties;
begin
  Result := FProps;
end;
{$ENDIF}

procedure TCertificado.inicialize(const __strHash: WideString);
begin
  DefaultInterface.inicialize(__strHash);
end;

procedure TCertificado.finalize;
begin
  DefaultInterface.finalize;
end;

function TCertificado.getDataTermino: WideString;
begin
  Result := DefaultInterface.getDataTermino;
end;

function TCertificado.mostrarCertificado: SYSINT;
begin
  Result := DefaultInterface.mostrarCertificado;
end;

function TCertificado.getIdCertificado: WideString;
begin
  Result := DefaultInterface.getIdCertificado;
end;

function TCertificado.getAssunto: WideString;
begin
  Result := DefaultInterface.getAssunto;
end;

function TCertificado.getAssuntoCN: WideString;
begin
  Result := DefaultInterface.getAssuntoCN;
end;

function TCertificado.getAssuntoO: WideString;
begin
  Result := DefaultInterface.getAssuntoO;
end;

function TCertificado.getAssuntoOU: WideString;
begin
  Result := DefaultInterface.getAssuntoOU;
end;

function TCertificado.getAssuntoL: WideString;
begin
  Result := DefaultInterface.getAssuntoL;
end;

function TCertificado.getAssuntoS: WideString;
begin
  Result := DefaultInterface.getAssuntoS;
end;

function TCertificado.getAssuntoC: WideString;
begin
  Result := DefaultInterface.getAssuntoC;
end;

function TCertificado.getAssuntoE: WideString;
begin
  Result := DefaultInterface.getAssuntoE;
end;

function TCertificado.getEmissor: WideString;
begin
  Result := DefaultInterface.getEmissor;
end;

function TCertificado.getEmissorCN: WideString;
begin
  Result := DefaultInterface.getEmissorCN;
end;

function TCertificado.getEmissorO: WideString;
begin
  Result := DefaultInterface.getEmissorO;
end;

function TCertificado.getEmissorOU: WideString;
begin
  Result := DefaultInterface.getEmissorOU;
end;

function TCertificado.getEmissorL: WideString;
begin
  Result := DefaultInterface.getEmissorL;
end;

function TCertificado.getEmissorS: WideString;
begin
  Result := DefaultInterface.getEmissorS;
end;

function TCertificado.getEmissorC: WideString;
begin
  Result := DefaultInterface.getEmissorC;
end;

function TCertificado.getEmissorE: WideString;
begin
  Result := DefaultInterface.getEmissorE;
end;

function TCertificado.getNumeroSerial: WideString;
begin
  Result := DefaultInterface.getNumeroSerial;
end;

function TCertificado.getDataInicio: WideString;
begin
  Result := DefaultInterface.getDataInicio;
end;

function TCertificado.existeExtensao(const __OID: WideString): Integer;
begin
  Result := DefaultInterface.existeExtensao(__OID);
end;

function TCertificado.existeFinalidade(const __OID: WideString): Integer;
begin
  Result := DefaultInterface.existeFinalidade(__OID);
end;

function TCertificado.existeUsoChave(const __OID: WideString): Integer;
begin
  Result := DefaultInterface.existeUsoChave(__OID);
end;

function TCertificado.baixarCRL: SYSINT;
begin
  Result := DefaultInterface.baixarCRL;
end;

function TCertificado.existeCRL: SYSINT;
begin
  Result := DefaultInterface.existeCRL;
end;

function TCertificado.verificarCRL: SYSINT;
begin
  Result := DefaultInterface.verificarCRL;
end;

function TCertificado.verificarValidade: SYSINT;
begin
  Result := DefaultInterface.verificarValidade;
end;

function TCertificado.status: SYSINT;
begin
  Result := DefaultInterface.status;
end;

function TCertificado.getCountCRL: SYSINT;
begin
  Result := DefaultInterface.getCountCRL;
end;

function TCertificado.getCountUsoChaves: SYSINT;
begin
  Result := DefaultInterface.getCountUsoChaves;
end;

function TCertificado.getCountFinalidades: SYSINT;
begin
  Result := DefaultInterface.getCountFinalidades;
end;

function TCertificado.getCountExtensoes: SYSINT;
begin
  Result := DefaultInterface.getCountExtensoes;
end;

function TCertificado.getEndCRL(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getEndCRL(__posicao);
end;

function TCertificado.getUsoChaves(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getUsoChaves(__posicao);
end;

function TCertificado.getFinalidades(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getFinalidades(__posicao);
end;

function TCertificado.getExtensaoValor(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getExtensaoValor(__posicao);
end;

function TCertificado.getExtensaoOID(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getExtensaoOID(__posicao);
end;

function TCertificado.getExtensaoCritica(__posicao: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getExtensaoCritica(__posicao);
end;

function TCertificado.getCountCertificadosCadeia: SYSINT;
begin
  Result := DefaultInterface.getCountCertificadosCadeia;
end;

function TCertificado.getStatusCadeiaCertificacao(__posicao: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getStatusCadeiaCertificacao(__posicao);
end;

function TCertificado.getErrorCadeiaCertificacao(__posicao: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getErrorCadeiaCertificacao(__posicao);
end;

function TCertificado.getHashCadeiaCertificacao(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getHashCadeiaCertificacao(__posicao);
end;

function TCertificado.verificarCadeiaCertificacao: SYSINT;
begin
  Result := DefaultInterface.verificarCadeiaCertificacao;
end;

function TCertificado.existeChavePrivada: Integer;
begin
  Result := DefaultInterface.existeChavePrivada;
end;

function TCertificado.setSenhaProxy(const __senha: WideString): SYSINT;
begin
  Result := DefaultInterface.setSenhaProxy(__senha);
end;

function TCertificado.setLoginProxy(const __login: WideString): SYSINT;
begin
  Result := DefaultInterface.setLoginProxy(__login);
end;

function TCertificado.getCPF: WideString;
begin
  Result := DefaultInterface.getCPF;
end;

function TCertificado.getRG: WideString;
begin
  Result := DefaultInterface.getRG;
end;

function TCertificado.getDataNascimentoTitular: WideString;
begin
  Result := DefaultInterface.getDataNascimentoTitular;
end;

function TCertificado.getNIS: WideString;
begin
  Result := DefaultInterface.getNIS;
end;

function TCertificado.getOrgaoExpedidor: WideString;
begin
  Result := DefaultInterface.getOrgaoExpedidor;
end;

function TCertificado.getCEIPF: WideString;
begin
  Result := DefaultInterface.getCEIPF;
end;

function TCertificado.getCEIPJ: WideString;
begin
  Result := DefaultInterface.getCEIPJ;
end;

function TCertificado.getCNPJ: WideString;
begin
  Result := DefaultInterface.getCNPJ;
end;

function TCertificado.getNomeResponsavel: WideString;
begin
  Result := DefaultInterface.getNomeResponsavel;
end;

function TCertificado.getTituloEleitor: WideString;
begin
  Result := DefaultInterface.getTituloEleitor;
end;

function TCertificado.getZonaEleitoral: WideString;
begin
  Result := DefaultInterface.getZonaEleitoral;
end;

function TCertificado.getSecaoEleitoral: WideString;
begin
  Result := DefaultInterface.getSecaoEleitoral;
end;

function TCertificado.getMunicipioUF: WideString;
begin
  Result := DefaultInterface.getMunicipioUF;
end;

function TCertificado.getCountPoliticas: SYSINT;
begin
  Result := DefaultInterface.getCountPoliticas;
end;

function TCertificado.getPoliticaOID(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getPoliticaOID(__posicao);
end;

function TCertificado.getPoliticaCPS(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getPoliticaCPS(__posicao);
end;

procedure TCertificado.InicializeCertBase64(const __strCert64: WideString);
begin
  DefaultInterface.InicializeCertBase64(__strCert64);
end;

function TCertificado.getRFC822Name: WideString;
begin
  Result := DefaultInterface.getRFC822Name;
end;

function TCertificado.setDataVerifica(const __dataVerifica: WideString): SYSINT;
begin
  Result := DefaultInterface.setDataVerifica(__dataVerifica);
end;

function TCertificado.getUsoExtendidoChaves(__posicao: SYSINT): WideString;
begin
  Result := DefaultInterface.getUsoExtendidoChaves(__posicao);
end;

function TCertificado.getCountUsoExtendidoChaves: SYSINT;
begin
  Result := DefaultInterface.getCountUsoExtendidoChaves;
end;

function TCertificado.isAC: SYSINT;
begin
  Result := DefaultInterface.isAC;
end;

function TCertificado.isRestricaoCaminho: SYSINT;
begin
  Result := DefaultInterface.isRestricaoCaminho;
end;

function TCertificado.getTamanhoRestricaoCaminho: SYSINT;
begin
  Result := DefaultInterface.getTamanhoRestricaoCaminho;
end;

function TCertificado.getTipoICPBrasil: SYSINT;
begin
  Result := DefaultInterface.getTipoICPBrasil;
end;

function TCertificado.getHashIdentificadorAutoridade: WideString;
begin
  Result := DefaultInterface.getHashIdentificadorAutoridade;
end;

function TCertificado.getCountAssuntoOU: SYSINT;
begin
  Result := DefaultInterface.getCountAssuntoOU;
end;

function TCertificado.getMultiplosAssuntoOU(__indice: SYSINT): WideString;
begin
  Result := DefaultInterface.getMultiplosAssuntoOU(__indice);
end;

function TCertificado.getCountEmissorOU: SYSINT;
begin
  Result := DefaultInterface.getCountEmissorOU;
end;

function TCertificado.getMultiplosEmissorOU(__indice: SYSINT): WideString;
begin
  Result := DefaultInterface.getMultiplosEmissorOU(__indice);
end;

function TCertificado.setCRL(const __crl: ICRL): SYSINT;
begin
  Result := DefaultInterface.setCRL(__crl);
end;

function TCertificado.getLCR: ICRL;
begin
  Result := DefaultInterface.getLCR;
end;

function TCertificado.getMotivoRevogacao: WideString;
begin
  Result := DefaultInterface.getMotivoRevogacao;
end;

function TCertificado.getDataRevogacao: WideString;
begin
  Result := DefaultInterface.getDataRevogacao;
end;

function TCertificado.isAutoAssinada: Integer;
begin
  Result := DefaultInterface.isAutoAssinada;
end;

function TCertificado.getCountEndCRL: SYSINT;
begin
  Result := DefaultInterface.getCountEndCRL;
end;

function TCertificado.getTamanhoChave: Integer;
begin
  Result := DefaultInterface.getTamanhoChave;
end;

function TCertificado.getCountOIDSubjectAlternativeName: Integer;
begin
  Result := DefaultInterface.getCountOIDSubjectAlternativeName;
end;

function TCertificado.getOIDSubjectAlternativeName(indice: Integer): WideString;
begin
  Result := DefaultInterface.getOIDSubjectAlternativeName(indice);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCertificadoProperties.Create(AServer: TCertificado);
begin
  inherited Create;
  FServer := AServer;
end;

function TCertificadoProperties.GetDefaultInterface: ICertificado;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoCarimbo.Create: ICarimbo;
begin
  Result := CreateComObject(CLASS_Carimbo) as ICarimbo;
end;

class function CoCarimbo.CreateRemote(const MachineName: string): ICarimbo;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Carimbo) as ICarimbo;
end;

procedure TCarimbo.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{83C5AF6B-0CEF-4A58-9ACC-009ABEF94260}';
    IntfIID:   '{3E36BB2E-65E5-4E0A-BBFE-912532FBA8C7}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCarimbo.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as ICarimbo;
  end;
end;

procedure TCarimbo.ConnectTo(svrIntf: ICarimbo);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCarimbo.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCarimbo.GetDefaultInterface: ICarimbo;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCarimbo.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCarimboProperties.Create(Self);
{$ENDIF}
end;

destructor TCarimbo.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCarimbo.GetServerProperties: TCarimboProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TCarimbo.getCertificadoCarimbadora: ICertificado;
begin
  Result := DefaultInterface.getCertificadoCarimbadora;
end;

function TCarimbo.getStatus: SYSINT;
begin
  Result := DefaultInterface.getStatus;
end;

function TCarimbo.getDataUTC: WideString;
begin
  Result := DefaultInterface.getDataUTC;
end;

function TCarimbo.getDataCalculada: WideString;
begin
  Result := DefaultInterface.getDataCalculada;
end;

function TCarimbo.getVersao: SYSINT;
begin
  Result := DefaultInterface.getVersao;
end;

function TCarimbo.getTSAHash: WideString;
begin
  Result := DefaultInterface.getTSAHash;
end;

function TCarimbo.getNumeroSerial: WideString;
begin
  Result := DefaultInterface.getNumeroSerial;
end;

function TCarimbo.getPolitica: WideString;
begin
  Result := DefaultInterface.getPolitica;
end;

function TCarimbo.isOrdering: SYSINT;
begin
  Result := DefaultInterface.isOrdering;
end;

function TCarimbo.getNonce: SYSINT;
begin
  Result := DefaultInterface.getNonce;
end;

function TCarimbo.getAccuracyMicrosegundos: WideString;
begin
  Result := DefaultInterface.getAccuracyMicrosegundos;
end;

function TCarimbo.getAccuracyMilisegundos: WideString;
begin
  Result := DefaultInterface.getAccuracyMilisegundos;
end;

function TCarimbo.getAccuracySegundos: WideString;
begin
  Result := DefaultInterface.getAccuracySegundos;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCarimboProperties.Create(AServer: TCarimbo);
begin
  inherited Create;
  FServer := AServer;
end;

function TCarimboProperties.GetDefaultInterface: ICarimbo;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoRepositorio.Create: IRepositorio;
begin
  Result := CreateComObject(CLASS_Repositorio) as IRepositorio;
end;

class function CoRepositorio.CreateRemote(const MachineName: string): IRepositorio;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Repositorio) as IRepositorio;
end;

procedure TRepositorio.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{5B12C0E6-F4E6-4A74-9314-89AFA21AA1C5}';
    IntfIID:   '{7D1101D2-2B29-4EFD-8E70-1EB90084ADBA}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TRepositorio.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as IRepositorio;
  end;
end;

procedure TRepositorio.ConnectTo(svrIntf: IRepositorio);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TRepositorio.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TRepositorio.GetDefaultInterface: IRepositorio;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TRepositorio.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TRepositorioProperties.Create(Self);
{$ENDIF}
end;

destructor TRepositorio.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TRepositorio.GetServerProperties: TRepositorioProperties;
begin
  Result := FProps;
end;
{$ENDIF}

procedure TRepositorio.finalize;
begin
  DefaultInterface.finalize;
end;

procedure TRepositorio.inicialize(const __local: WideString; __armazenamento: SYSINT);
begin
  DefaultInterface.inicialize(__local, __armazenamento);
end;

function TRepositorio.adicionar(const __certificado: WideString; __FLAG: SYSINT): SYSINT;
begin
  Result := DefaultInterface.adicionar(__certificado, __FLAG);
end;

function TRepositorio.adicionarDoArquivo(const __arquivo: WideString): SYSINT;
begin
  Result := DefaultInterface.adicionarDoArquivo(__arquivo);
end;

function TRepositorio.getCertificado(__i: SYSINT): ICertificado;
begin
  Result := DefaultInterface.getCertificado(__i);
end;

function TRepositorio.getCountCertificados: SYSINT;
begin
  Result := DefaultInterface.getCountCertificados;
end;

function TRepositorio.getCertificadoBase64(__i: SYSINT): WideString;
begin
  Result := DefaultInterface.getCertificadoBase64(__i);
end;

function TRepositorio.existeCartaoLeitora: Integer;
begin
  Result := DefaultInterface.existeCartaoLeitora;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TRepositorioProperties.Create(AServer: TRepositorio);
begin
  inherited Create;
  FServer := AServer;
end;

function TRepositorioProperties.GetDefaultInterface: IRepositorio;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoAssinador.Create: IAssinador;
begin
  Result := CreateComObject(CLASS_Assinador) as IAssinador;
end;

class function CoAssinador.CreateRemote(const MachineName: string): IAssinador;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Assinador) as IAssinador;
end;

procedure TAssinador.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{D74AA60A-D77B-4482-8850-4768E636AAC7}';
    IntfIID:   '{FE9B621E-FDFD-4299-B7E0-E7E753693624}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TAssinador.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as IAssinador;
  end;
end;

procedure TAssinador.ConnectTo(svrIntf: IAssinador);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TAssinador.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TAssinador.GetDefaultInterface: IAssinador;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TAssinador.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TAssinadorProperties.Create(Self);
{$ENDIF}
end;

destructor TAssinador.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TAssinador.GetServerProperties: TAssinadorProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TAssinador.assineArquivo(const __arq: WideString; const __caminho: WideString; 
                                  const __descricao: WideString; const __hashCert: WideString): SYSINT;
begin
  Result := DefaultInterface.assineArquivo(__arq, __caminho, __descricao, __hashCert);
end;

function TAssinador.coAssineArquivo(const __arq: WideString; const __caminho: WideString; 
                                    const __descricao: WideString; const __hash: WideString): SYSINT;
begin
  Result := DefaultInterface.coAssineArquivo(__arq, __caminho, __descricao, __hash);
end;

function TAssinador.assineMem(const __mem: WideString; const __descricao: WideString; 
                              const __hashCert: WideString; out __assinatura: WideString): SYSINT;
begin
  Result := DefaultInterface.assineMem(__mem, __descricao, __hashCert, __assinatura);
end;

function TAssinador.retorneAssinaturaMem: WideString;
begin
  Result := DefaultInterface.retorneAssinaturaMem;
end;

function TAssinador.coAssineMem(const __mem: WideString; const __descricao: WideString; 
                                const __hashCert: WideString; out __assinatura: WideString): SYSINT;
begin
  Result := DefaultInterface.coAssineMem(__mem, __descricao, __hashCert, __assinatura);
end;

function TAssinador.setDataHora(__dia: SYSINT; __mes: SYSINT; __ano: SYSINT; __hora: SYSINT; 
                                __minutos: SYSINT; __segundos: SYSINT; __milisegundos: SYSINT): SYSINT;
begin
  Result := DefaultInterface.setDataHora(__dia, __mes, __ano, __hora, __minutos, __segundos, 
                                         __milisegundos);
end;

function TAssinador.setFormatoDadosMemoria(__formato: SYSINT): SYSINT;
begin
  Result := DefaultInterface.setFormatoDadosMemoria(__formato);
end;

function TAssinador.hashDoArquivo(const __arquivo: WideString): WideString;
begin
  Result := DefaultInterface.hashDoArquivo(__arquivo);
end;

function TAssinador.hashDados(const __dados: WideString; __tamanho: SYSINT): WideString;
begin
  Result := DefaultInterface.hashDados(__dados, __tamanho);
end;

function TAssinador.assineArquivoDetached(const __arq: WideString; const __caminho: WideString; 
                                          const __descricao: WideString; 
                                          const __hashCert: WideString): SYSINT;
begin
  Result := DefaultInterface.assineArquivoDetached(__arq, __caminho, __descricao, __hashCert);
end;

function TAssinador.assineMemDetached(const __mem: WideString; const __descricao: WideString; 
                                      const __hashCert: WideString; var __assinatura: WideString): SYSINT;
begin
  Result := DefaultInterface.assineMemDetached(__mem, __descricao, __hashCert, __assinatura);
end;

function TAssinador.setAddCrlCaminhoCert(__intAdicionar: SYSINT): SYSINT;
begin
  Result := DefaultInterface.setAddCrlCaminhoCert(__intAdicionar);
end;

function TAssinador.coAssineArquivoDetached(const __arq: WideString; const __original: WideString; 
                                            const __caminho: WideString; 
                                            const __descricao: WideString; 
                                            const __hashCert: WideString): SYSINT;
begin
  Result := DefaultInterface.coAssineArquivoDetached(__arq, __original, __caminho, __descricao, 
                                                     __hashCert);
end;

function TAssinador.coAssineMemDetached(const __mem: WideString; const __original: WideString; 
                                        const __descricao: WideString; 
                                        const __hashCert: WideString; var __assinatura: WideString): SYSINT;
begin
  Result := DefaultInterface.coAssineMemDetached(__mem, __original, __descricao, __hashCert, 
                                                 __assinatura);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TAssinadorProperties.Create(AServer: TAssinador);
begin
  inherited Create;
  FServer := AServer;
end;

function TAssinadorProperties.GetDefaultInterface: IAssinador;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoVerificador.Create: IVerificador;
begin
  Result := CreateComObject(CLASS_Verificador) as IVerificador;
end;

class function CoVerificador.CreateRemote(const MachineName: string): IVerificador;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Verificador) as IVerificador;
end;

procedure TVerificador.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{1690EE68-8D31-473D-9CEB-C51D56A76D7B}';
    IntfIID:   '{F3DD3DFF-32AF-4AC9-A743-6FD53A3F92FE}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TVerificador.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as IVerificador;
  end;
end;

procedure TVerificador.ConnectTo(svrIntf: IVerificador);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TVerificador.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TVerificador.GetDefaultInterface: IVerificador;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TVerificador.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TVerificadorProperties.Create(Self);
{$ENDIF}
end;

destructor TVerificador.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TVerificador.GetServerProperties: TVerificadorProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TVerificador.verifiqueAssinatura(const __arq: WideString): SYSINT;
begin
  Result := DefaultInterface.verifiqueAssinatura(__arq);
end;

function TVerificador.getCountAssinaturas: SYSINT;
begin
  Result := DefaultInterface.getCountAssinaturas;
end;

function TVerificador.getStatusCertificado(__index: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getStatusCertificado(__index);
end;

function TVerificador.getStatusAssinatura(__index: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getStatusAssinatura(__index);
end;

function TVerificador.getHash(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getHash(__index);
end;

function TVerificador.getArquivo(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getArquivo(__index);
end;

function TVerificador.getNomeArquivo(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getNomeArquivo(__index);
end;

function TVerificador.getDescricao(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getDescricao(__index);
end;

function TVerificador.extrairDocumento(const __arq: WideString; const __caminho: WideString; 
                                       __FLAG: SYSINT; __toVerify: Integer; __sobrescrever: Integer): SYSINT;
begin
  Result := DefaultInterface.extrairDocumento(__arq, __caminho, __FLAG, __toVerify, __sobrescrever);
end;

function TVerificador.getCertificado(__index: SYSINT): ICertificado;
begin
  Result := DefaultInterface.getCertificado(__index);
end;

function TVerificador.getStatusCadeia(__index: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getStatusCadeia(__index);
end;

procedure TVerificador.inicialize;
begin
  DefaultInterface.inicialize;
end;

procedure TVerificador.finalize;
begin
  DefaultInterface.finalize;
end;

function TVerificador.verifiqueAssinaturaMem(const __mem: WideString): SYSINT;
begin
  Result := DefaultInterface.verifiqueAssinaturaMem(__mem);
end;

function TVerificador.getDataUTC(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getDataUTC(__index);
end;

function TVerificador.getDataCalculada(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getDataCalculada(__index);
end;

function TVerificador.setFormatoDadosMemoria(__formato: SYSINT): SYSINT;
begin
  Result := DefaultInterface.setFormatoDadosMemoria(__formato);
end;

function TVerificador.getHashDocumentoOriginal(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getHashDocumentoOriginal(__index);
end;

function TVerificador.verifiqueAssinaturaDetached(const __arqAssinado: WideString; 
                                                  const __arqOriginal: WideString): SYSINT;
begin
  Result := DefaultInterface.verifiqueAssinaturaDetached(__arqAssinado, __arqOriginal);
end;

function TVerificador.verifiqueAssinaturaMemDetached(const __memAssinada: WideString; 
                                                     const __memOriginal: WideString): SYSINT;
begin
  Result := DefaultInterface.verifiqueAssinaturaMemDetached(__memAssinada, __memOriginal);
end;

function TVerificador.getDataCarimboTempo(__index: SYSINT): WideString;
begin
  Result := DefaultInterface.getDataCarimboTempo(__index);
end;

function TVerificador.getStatusCarimboTempo(__index: SYSINT): SYSINT;
begin
  Result := DefaultInterface.getStatusCarimboTempo(__index);
end;

function TVerificador.getCarimboAssinatura(indice: SYSINT): ICarimbo;
begin
  Result := DefaultInterface.getCarimboAssinatura(indice);
end;

function TVerificador.getLCR(__indice: SYSINT): ICRL;
begin
  Result := DefaultInterface.getLCR(__indice);
end;

function TVerificador.getCountCRLs: SYSINT;
begin
  Result := DefaultInterface.getCountCRLs;
end;

function TVerificador.isArquivoAssinadoDetached(const __arq: WideString): SYSINT;
begin
  Result := DefaultInterface.isArquivoAssinadoDetached(__arq);
end;

function TVerificador.getCountCertificadosCadeia: SYSINT;
begin
  Result := DefaultInterface.getCountCertificadosCadeia;
end;

function TVerificador.getCertificadoCadeia(__indice: SYSINT): ICertificado;
begin
  Result := DefaultInterface.getCertificadoCadeia(__indice);
end;

procedure TVerificador.setInstalarCadeia(__instalarCadeia: Integer);
begin
  DefaultInterface.setInstalarCadeia(__instalarCadeia);
end;

procedure TVerificador.setBaixarLCR(__baixarLCR: Integer);
begin
  DefaultInterface.setBaixarLCR(__baixarLCR);
end;

procedure TVerificador.setInstalarCertAssinador(__instalarCertAssinador: Integer);
begin
  DefaultInterface.setInstalarCertAssinador(__instalarCertAssinador);
end;

procedure TVerificador.setLoginProxy(const login: WideString);
begin
  DefaultInterface.setLoginProxy(login);
end;

procedure TVerificador.setSenhaProxy(const senha: WideString);
begin
  DefaultInterface.setSenhaProxy(senha);
end;

procedure TVerificador.setURLProxy(const url: WideString);
begin
  DefaultInterface.setURLProxy(url);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TVerificadorProperties.Create(AServer: TVerificador);
begin
  inherited Create;
  FServer := AServer;
end;

function TVerificadorProperties.GetDefaultInterface: IVerificador;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

procedure Register;
begin
  RegisterComponents(dtlServerPage, [TCRL, TCertificado, TCarimbo, TRepositorio, 
    TAssinador, TVerificador]);
end;

end.
