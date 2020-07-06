program Exemplo2;

uses
  Forms,
  Unit1 in 'Unit1.pas' {Form1},
  BRYSIGNERCOMLib_TLB in 'BRYSIGNERCOMLib_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
