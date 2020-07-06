unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,BRYSIGNERCOMLib_TLB;

type
  TForm1 = class(TForm)
    Button1: TButton;
    GroupBox4: TGroupBox;
    rMy: TRadioButton;
    rOutras: TRadioButton;
    rCA: TRadioButton;
    GroupBox5: TGroupBox;
    rCurrent: TRadioButton;
    rLocal: TRadioButton;
    cbCerts: TComboBox;
    Label6: TLabel;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    LabelCN: TLabel;
    LabelOU: TLabel;
    LabelO: TLabel;
    LabelL: TLabel;
    LabelS: TLabel;
    LabelC: TLabel;
    LabelE: TLabel;
    LabelCertificadoBRy: TLabel;
    LabelCertValido: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure cbCertsClick(Sender: TObject);
  private
    { Private declarations }
    repositorio : IRepositorio;
    _local : WideString;
    _armazenamento: integer;

  public
    { Public declarations }
    function CertificadoClasse2Bry(num:integer):string;
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
    _certCN : WideString;
    _i : integer;
    _cert : ICertificado;

begin
    cbCerts.Items.Clear();
    cbCerts.Clear;

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

    repositorio.finalize;
end;
procedure TForm1.FormCreate(Sender: TObject);
begin
    repositorio := CoRepositorio.Create;
end;



function TForm1.CertificadoClasse2Bry(num:integer):string;
var
    _cert : ICertificado;
begin
    _cert := repositorio.getCertificado(cbCerts.ItemIndex);
    _cert.verificarCadeiaCertificacao;
    if (_cert.getHashCadeiaCertificacao(1) <> 'E5AA7193897509ED7A9ADC8AF68BAE19324F5080') then
        result:= 'falso'
    else
        if (_cert.getHashCadeiaCertificacao(2) <> 'D5468747571469A6EA13C4E3FC26C11175D1F1D8')then
            result:= 'falso'
        else
            result:= 'verdadeiro';
end;

procedure TForm1.cbCertsClick(Sender: TObject);
var
    _certCN : WideString;
    _cert : ICertificado;
    repositorio2:IRepositorio;
    auxStr : WideString;
begin
    repositorio.inicialize(_local, _armazenamento);
    _cert := repositorio.getCertificado(cbCerts.ItemIndex);
    LabelCN.Caption := _cert.getAssuntoCN();
    LabelOU.Caption := _cert.getAssuntoOU();
    LabelO.Caption := _cert.getAssuntoO();
    LabelL.Caption := _cert.getAssuntoL();
    LabelS.Caption := _cert.getAssuntoS();
    LabelC.Caption := _cert.getAssuntoC();
    auxStr := _cert.getAssuntoE();
    if auxStr = '' then
    begin
         auxStr := _cert.getRFC822Name();
    end;
    LabelE.Caption := auxStr;
    LabelCertValido.Caption := CertificadoClasse2Bry(cbCerts.ItemIndex);
    repositorio.finalize;
end;

end.
