unit ClientCom;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, BRYSIGNERCOMLib_TLB;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label6: TLabel;
    GroupBox1: TGroupBox;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    GroupBox2: TGroupBox;
    Button11: TButton;
    Button12: TButton;
    cbSobreescrever: TCheckBox;
    cbVerificar: TCheckBox;
    GroupBox3: TGroupBox;
    Label5: TLabel;
    Button6: TButton;
    Button7: TButton;
    Button8: TButton;
    Button9: TButton;
    Button10: TButton;
    Button13: TButton;
    edIndex: TEdit;
    Button5: TButton;
    edArq: TEdit;
    edDestino: TEdit;
    edComentario: TEdit;
    GroupBox4: TGroupBox;
    rMy: TRadioButton;
    rOutras: TRadioButton;
    rCA: TRadioButton;
    GroupBox5: TGroupBox;
    rCurrent: TRadioButton;
    rLocal: TRadioButton;
    GroupBox6: TGroupBox;
    Button14: TButton;
    Button16: TButton;
    Button17: TButton;
    Button18: TButton;
    Button15: TButton;
    cbCerts: TComboBox;
    GroupBox7: TGroupBox;
    Button1: TButton;
    Button19: TButton;
    Button20: TButton;
    Button21: TButton;
    Button22: TButton;
    Button23: TButton;
    Button24: TButton;
    Button25: TButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button7Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
    procedure Button10Click(Sender: TObject);
    procedure Button11Click(Sender: TObject);
    procedure Button14Click(Sender: TObject);
    procedure Button16Click(Sender: TObject);
    procedure Button17Click(Sender: TObject);
    procedure Button18Click(Sender: TObject);
    procedure Button15Click(Sender: TObject);
    procedure Button21Click(Sender: TObject);
    procedure Button20Click(Sender: TObject);
    procedure Button19Click(Sender: TObject);
    procedure Button12Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button13Click(Sender: TObject);
    procedure Button22Click(Sender: TObject);
    procedure Button23Click(Sender: TObject);
    procedure Button24Click(Sender: TObject);
    procedure Button25Click(Sender: TObject);
  private
    { Private declarations }
  public
    signer : IAssinador;
    verifier  : IVerificador;
    certificado : ICertificado;
    repositorio : IRepositorio;
    assinatura : WideString;
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
    signer:= CoAssinador.Create;
    verifier := CoVerificador.Create;
    certificado := CoCertificado.Create;
    repositorio := CoRepositorio.Create;
end;

procedure TForm1.Button1Click(Sender: TObject);
var
    _status , _index:integer;
    _cert : ICertificado;
begin

    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _status := _cert.existeCRL();
        ShowMessage(IntToStr(_status));
    end;
end;

procedure TForm1.Button2Click(Sender: TObject);
var
    _status, _index : integer;
    _cert : ICertificado;
begin
     _status := -1;
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _status := signer.assineArquivo(WideString(edArq.Text),
                         WideString(edDestino.Text),
                         WideString(edComentario.Text),
                         _cert.getIdCertificado());
    end;

    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button4Click(Sender: TObject);
var
    _status : integer;
begin
    verifier.setBaixarLCR(1);
    verifier.setInstalarCertAssinador(1);
    {verifier.setInstalarCadeia(1);}
    _status := verifier.verifiqueAssinatura(WideString(edArq.Text));
    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button5Click(Sender: TObject);
var
    _num : integer;
begin
    _num := verifier.getCountAssinaturas;
    ShowMessage(IntToStr(_num));
end;

procedure TForm1.Button3Click(Sender: TObject);
var
    _status, _index : integer;
    _cert : ICertificado;
begin
     _status := -1;
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _status := signer.coAssineArquivo(WideString(edArq.Text),
                         WideString(edDestino.Text),
                         WideString(edComentario.Text),
                         _cert.getIdCertificado());
    end;

    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button6Click(Sender: TObject);
var
    _status : integer;
begin
    _status := verifier.getStatusAssinatura(StrToInt(edIndex.Text));
    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button7Click(Sender: TObject);
var
    _status : integer;
    _cert   : widestring;
    _iCert  : ICertificado;
begin
    //TCOMICertificado _cert;
    //ICertificadoPtr _certPtr;
    //ICertificado* _iCert;


    _iCert := verifier.getCertificado(StrToInt(edIndex.Text));

    if _iCert <> nil then
    begin
         _iCert.mostrarCertificado();
    end;
end;

procedure TForm1.Button9Click(Sender: TObject);
var
    _descricao  : widestring;
begin
    _descricao := verifier.getDescricao(StrToInt(edIndex.Text));
    showmessage(_descricao);
end;

procedure TForm1.Button10Click(Sender: TObject);
var
    _nome  : widestring;
begin
    _nome := verifier.getNomeArquivo(StrToInt(edIndex.Text));
    showmessage(_nome);

end;

procedure TForm1.Button11Click(Sender: TObject);
var
    _verificar, _sobreescrever, _status : integer;
begin
    if cbVerificar.Checked then
        _verificar := 1
    else
        _verificar := 0;

    if cbSobreescrever.Checked then
        _sobreescrever := 1
    else
        _sobreescrever := 0;

    _status := verifier.extrairDocumento(edArq.Text,edDestino.Text,1,_verificar,_sobreescrever);
    showmessage(inttostr(_status));
end;

procedure TForm1.Button14Click(Sender: TObject);
var
    _local : WideString;
    _certCN : WideString;
    _armazenamento, _i : integer;
    _cert : ICertificado;
begin

    if rMy.Checked then
        _local := 'MY'
    else if rOutras.Checked then
         _local := 'AddressBook'
    else if rCA.Checked then
         _local := 'CA';

    if rCurrent.Checked then
        _armazenamento := 0
    else if rLocal.Checked then
        _armazenamento := 1;
    repositorio.inicialize(_local, _armazenamento);


    cbCerts.Items.Clear();
    for _i:= 0 to repositorio.getCountCertificados()-1 do
    begin
        _cert := repositorio.getCertificado(_i);
        _certCN := _cert.getAssuntoCN();
        cbCerts.Items.Add(_certCN);
    end;
end;

procedure TForm1.Button16Click(Sender: TObject);
var
    _num : integer;
begin
    _num := repositorio.getCountCertificados();
     ShowMessage(IntToStr(_num));
end;

procedure TForm1.Button17Click(Sender: TObject);
var
    _cert : ICertificado;
    _index : integer;
begin
    _index := cbCerts.ItemIndex;
    if _index >=0  then
    begin
        _cert := repositorio.getCertificado(_index);
        _cert.mostrarCertificado();
    end;
end;

procedure TForm1.Button18Click(Sender: TObject);
var
    retorno:integer;
    cert : WideString;
begin

    cert := '308207A130820589A003020102020A6102DADB000200000015300D06092A864886F70D0101050500';
	cert := cert + '3081A9311C301A06092A864886F70D010901160D6163406272792E636F6D2E6272310B3009060355';
	cert := cert + '040613024252310B3009060355040813025343311630140603550407130D466C6F7269616E6F706F';
	cert := cert + '6C6973311C301A060355040A1313425279205465636E6F6C6F67696120532E412E3121301F060355';
	cert := cert + '040B13184175746F72696461646520436572746966696361646F7261311630140603550403130D42';
	cert := cert + '5279204143202D205261697A301E170D3032303230383136333735365A170D303430323038313634';
	cert := cert + '3735365A3081AC311C301A06092A864886F70D010901160D6163406272792E636F6D2E6272310B30';
	cert := cert + '09060355040613024252310B3009060355040813025343311630140603550407130D466C6F726961';
	cert := cert + '6E6F706F6C6973311C301A060355040A1313425279205465636E6F6C6F67696120532E412E312130';
	cert := cert + '1F060355040B13184175746F72696461646520436572746966696361646F72613119301706035504';
	cert := cert + '0313104252792041432D20436C61737365203330820222300D06092A864886F70D01010105000382';
	cert := cert + '020F003082020A0282020100E8DCE0BA90324E76BA0A65F9334E03FF9CCABF9022FBC5249CEBF56A';
	cert := cert + '2C6AAB2E1192B5D5B04383120F21283EF0D79D5D0269078BA268367A5EBC0E957203EA33BEAA3BE9';
	cert := cert + 'BC4365A1BF931D014377E636D5EBE580DF7CF23D609526085199FBE8E1B3E68A720B5ED5AF1A0433';
	cert := cert + 'CEFFC0BE4C77E91E9B43B63E68E34964A032F0EB0DAD594B90C281B1338A5C8E4CCB598CD4CA15F6';
	cert := cert + '2D22C1542CB80D9304D3352468FF9DEFA14CFDEC4CDA229D200448C279083FED26BD08119248C562';
	cert := cert + 'F344DEB10EF8E877CD1C6F8033B5713CB50D9BF0BEADB38F938FB98A1572023229B1727E126B11EE';
	cert := cert + 'EBB9F672E6BECB4A6DB736F29D9C3BE97B5F06DEF319C53E11E1C56AC6E151E4FCDC40A01E2C2FAD';
	cert := cert + 'B2DC6D35226C07E567FC3A321B6B6C97416EAE0B8E2388C3850A9E3DA660118DC494FB858948DF96';
	cert := cert + '0FF11023F8287FDBF7346CF09CA7990415A74B1B8A6FA2D44FBE114A37A2D875BBF9D9E95CF10CB9';
	cert := cert + '651D80B8493EBF3D228A2C8192E76FD09A0F5D6E261C8B04FC57DB924515AE005770EBD44E91E927';
	cert := cert + 'E744B816D31938D388D3EA97EC2E4636371A670D290916EE4BAEE306C5735E4ED1CB7C99DB7CD1B9';
	cert := cert + 'E742528D41B485D766DA393621D69016B60C186D0EAEE42A089BA051A64CCB2762D35DA17AF7C6B1';
	cert := cert + 'A1D83E9A6494360DDBE5DF517B9BF3A3BCBE242475D15A2B69F2F3CC9371F3B4360E8B4896B30CA6';
	cert := cert + 'B091D7090203010001A38201C4308201C0301006092B06010401823715010403020100301D060355';
	cert := cert + '1D0E04160414C48242581BEED5AA6AE36DBD7365898406EDC917300B0603551D0F0404030201C630';
	cert := cert + '0F0603551D130101FF040530030101FF3081E50603551D230481DD3081DA8014D2F665D6A917ABB0';
	cert := cert + '0545960BDEBC792B68F9E23BA181AFA481AC3081A9311C301A06092A864886F70D010901160D6163';
	cert := cert + '406272792E636F6D2E6272310B3009060355040613024252310B3009060355040813025343311630';
	cert := cert + '140603550407130D466C6F7269616E6F706F6C6973311C301A060355040A1313425279205465636E';
	cert := cert + '6F6C6F67696120532E412E3121301F060355040B13184175746F7269646164652043657274696669';
	cert := cert + '6361646F7261311630140603550403130D425279204143202D205261697A82105B1DE7AFBF703086';
	cert := cert + '4DED965CD9172D6B303D0603551D1F043630343032A030A02E862C687474703A2F2F7777772E6272';
	cert := cert + '792E636F6D2E62722F61632F63726C2F4252795F41435F5261697A2E63726C304806082B06010505';
	cert := cert + '070101043C303A303806082B06010505073002862C687474703A2F2F7777772E6272792E636F6D2E';
	cert := cert + '62722F61632F6372742F4252795F41435F5261697A2E637274300D06092A864886F70D0101050500';
	cert := cert + '0382020100015CBA62F6716A4531FF1DE1000333328492E357F05FAD0F4484698483D508615DBE42';
	cert := cert + 'B492CE52FBA1C46FB672E87482D5F96C7FC43069B5A715E4469FBFAAA2362C48C97D92B4C9783004';
	cert := cert + '066A8EF124C28B6CB46FE12EA466EF673F2B92B63D827FBD8FBCF5D09B5DCF554955A4731C0BAC3B';
	cert := cert + 'F1A4F448D147C443830F27DEDE8DD75A5470F2FE5CF71A83AE065F39BE8F39CDE43D20EAFDFCC916';
	cert := cert + '54D2534097311CDF9B4A84C3AC6982140950BD7BEF7BC67E41F2C6378D7ED8220BDBECCB36CD7D3B';
	cert := cert + 'AB3DD63D00CB2F04C631857E92E2B4811596EF621E39BED02FF14820BC069EEAEC9E5315A26E2437';
	cert := cert + 'FD04230A4F48A8793EBE0C2DC83E61F16FC386A2B11A3EAE7F73457E20A1336E77CD01A1938B87EF';
	cert := cert + '68FBC4C72ABCDE2F43EC305416B0D3D6764072D34D31736F5B3C5BD140CFDC1577B2AF125B0CDBAD';
	cert := cert + '566B0D7A964930F2BD0889161EE66576A6C17DBC95897D65A08AB2A517066BB84B4B7AE0F7E5ACFC';
	cert := cert + 'C66BA56CADFAD4E6CD6F0C7242407B1BB45B1DC3E7F1D850CB26B8D0670A3A26FCB7FFCD9EC62CCB';
	cert := cert + '636BB6621F52D6C98E952E7FCEAC28344491AE58D376E818BBA7AD44381F2766AA015D1A7B169776';
	cert := cert + 'C1BD9A253A3CDD7F03D6721A4C2AA84D4BB1F386BFB9ABE5964D1A8B6F86B6537BB5D3D38DB826D8';
	cert := cert + '2E32D6FC84F6F1A239E29E6CFAD209F93B175D1EDBCCD744D23E60EA4D3BBDDC1E15CE8D6A';

    retorno := repositorio.adicionar(cert,1);
    showmessage(inttostr(retorno));
end;

procedure TForm1.Button15Click(Sender: TObject);
begin
    cbCerts.Items.Clear();
    repositorio.finalize();
end;

procedure TForm1.Button21Click(Sender: TObject);
var
    _texto : WideString;
    _status, _index, _numCrl : integer;
    _cert : ICertificado;
begin
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _numCrl := _cert.getCountCRL();
        ShowMessage(IntToStr(_numCrl));
    end;
end;

procedure TForm1.Button20Click(Sender: TObject);
var
    _cert : ICertificado;
    _texto : WideString;
    _status, _index : integer;
begin
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _texto := _cert.getEndCRL(0);
         ShowMessage(_texto);
    end;
end;

procedure TForm1.Button19Click(Sender: TObject);
var
    _index : integer;
    _cert  : ICertificado;

begin
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
        _cert.baixarCRL();
    end;
end;

procedure TForm1.Button12Click(Sender: TObject);
var
    _verificar, _sobreescrever, _status : integer;
begin
    if cbVerificar.Checked then
        _verificar := 1
    else
        _verificar := 0;

    if cbSobreescrever.Checked then
        _sobreescrever := 1
    else
        _sobreescrever := 0;

    _status := verifier.extrairDocumento(edArq.Text,edDestino.Text,2,_verificar,_sobreescrever);
    showmessage(inttostr(_status));
end;

procedure TForm1.Button8Click(Sender: TObject);
var
    _arquivo : WideString;
begin
    _arquivo := verifier.getArquivo(StrToInt(edIndex.Text));
    ShowMessage(_arquivo);
end;

procedure TForm1.Button13Click(Sender: TObject);
var
    _status : integer;
begin
    _status := verifier.getStatusCertificado(StrToInt(edIndex.Text));
    ShowMessage(IntToStr(_status));



       //_status := signer.assineMem('VOU ASSINAR ISSU AKI',WideString(lb_comentario.Text),_cert,_caminho);
       //         _tamHex :=  Length(_caminho);


         //       Form3.statusVerificacao := Form3.verifier.verifiqueAssinaturaMem(_caminho);
           //     _ret :=Form3.verifier.getArquivo(0);
             //   _tamHex := Form3.verifier.getStatusCadeia(0);
               //    Form3.ShowModal();
               // ShowMessage(IntToStr(Length(_caminho)));
               // GetMem(pbin,Trunc(_tamHex/2));
                //GetMem(phex,_tamHex);
                //CopyMemory(phex,PChar(LowerCase(_caminho)),_tamHex);
                //HexToBin(phex,pbin,_tamHex);
                //fil := TFileStream.Create('C:\\yyyyy.p7s',fmCreate);

                //fil.Write(pbin^,Trunc(_tamHex/2));
end;

procedure TForm1.Button22Click(Sender: TObject);
var
    _status, _index : integer;
    _cert : ICertificado;
    _assinatura : WideString;
    fil : TFileStream;
begin
     _status := -1;
    _index := cbCerts.ItemIndex;
    if _index >=0 then
    begin
        _cert := repositorio.getCertificado(_index);
       _status := signer.assineMem(WideString(edArq.Text),WideString(edComentario.Text),_cert.getIdCertificado(),assinatura);
    end;
    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button23Click(Sender: TObject);
var
    _status : integer;
begin
    verifier.setBaixarLCR(1);
    verifier.setInstalarCertAssinador(1);
    {verifier.setInstalarCadeia(1);}
    _status := verifier.verifiqueAssinaturaMem(assinatura);
    ShowMessage(IntToStr(_status));
end;

procedure TForm1.Button24Click(Sender: TObject);
var
    _data  : widestring;
begin
    _data := verifier.getDataUTC(StrToInt(edIndex.Text));
    showmessage(_data);
end;

procedure TForm1.Button25Click(Sender: TObject);
var
    _data  : widestring;
begin
    _data := verifier.getDataCalculada(StrToInt(edIndex.Text));
    showmessage(_data);
end;

end.
