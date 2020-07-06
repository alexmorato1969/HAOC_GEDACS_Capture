program DelphiCom;

uses
  Forms,
  ClientCom in 'ClientCom.pas' {Form1},
  BRYSIGNERCOMLib_TLB in 'BRYSIGNERCOMLib_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
