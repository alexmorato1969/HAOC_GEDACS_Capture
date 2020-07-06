object Form1: TForm1
  Left = 278
  Top = 276
  Width = 782
  Height = 456
  Caption = 'Signer SDK Teste'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 187
    Top = 61
    Width = 36
    Height = 13
    Caption = 'Arquivo'
  end
  object Label2: TLabel
    Left = 187
    Top = 84
    Width = 36
    Height = 13
    Caption = 'Destino'
  end
  object Label3: TLabel
    Left = 184
    Top = 109
    Width = 53
    Height = 13
    Caption = 'Coment'#225'rio'
  end
  object Label6: TLabel
    Left = 184
    Top = 36
    Width = 55
    Height = 13
    Caption = 'Certificados'
  end
  object GroupBox1: TGroupBox
    Left = 432
    Top = 16
    Width = 153
    Height = 169
    Caption = 'Assinatura e Verificacao'
    TabOrder = 0
    object Button2: TButton
      Left = 10
      Top = 24
      Width = 137
      Height = 25
      Caption = 'Assinar'
      TabOrder = 0
      OnClick = Button2Click
    end
    object Button3: TButton
      Left = 10
      Top = 50
      Width = 137
      Height = 25
      Caption = 'CoAssinar'
      TabOrder = 1
      OnClick = Button3Click
    end
    object Button4: TButton
      Left = 10
      Top = 100
      Width = 137
      Height = 25
      Caption = 'Verificar'
      TabOrder = 2
      OnClick = Button4Click
    end
    object Button22: TButton
      Left = 10
      Top = 75
      Width = 137
      Height = 25
      Caption = 'Assinar Mem'#243'ria'
      TabOrder = 3
      OnClick = Button22Click
    end
    object Button23: TButton
      Left = 10
      Top = 125
      Width = 137
      Height = 25
      Caption = 'Verificar em Mem'#243'ria'
      TabOrder = 4
      OnClick = Button23Click
    end
  end
  object GroupBox2: TGroupBox
    Left = 432
    Top = 288
    Width = 153
    Height = 121
    Caption = 'Extra'#231#227'o do Arquivo'
    TabOrder = 1
    object Button11: TButton
      Left = 8
      Top = 26
      Width = 137
      Height = 25
      Caption = 'Abrir Arquivo'
      TabOrder = 0
      OnClick = Button11Click
    end
    object Button12: TButton
      Left = 8
      Top = 51
      Width = 137
      Height = 25
      Caption = 'Salvar Arquivo'
      TabOrder = 1
      OnClick = Button12Click
    end
    object cbSobreescrever: TCheckBox
      Left = 9
      Top = 80
      Width = 97
      Height = 17
      Caption = 'Sobreescrever'
      Checked = True
      State = cbChecked
      TabOrder = 2
    end
    object cbVerificar: TCheckBox
      Left = 8
      Top = 96
      Width = 97
      Height = 17
      Caption = 'Verificar'
      TabOrder = 3
    end
  end
  object GroupBox3: TGroupBox
    Left = 600
    Top = 16
    Width = 161
    Height = 393
    Caption = 'Status Verificacao'
    TabOrder = 2
    object Label5: TLabel
      Left = 63
      Top = 284
      Width = 26
      Height = 13
      Caption = 'Index'
    end
    object Button6: TButton
      Left = 8
      Top = 46
      Width = 137
      Height = 25
      Caption = 'Status de Verifica'#231#227'o'
      TabOrder = 0
      OnClick = Button6Click
    end
    object Button7: TButton
      Left = 8
      Top = 71
      Width = 137
      Height = 25
      Caption = 'Mostre Certificado'
      TabOrder = 1
      OnClick = Button7Click
    end
    object Button8: TButton
      Left = 8
      Top = 96
      Width = 137
      Height = 25
      Caption = 'Retorne Arquivo'
      TabOrder = 2
      OnClick = Button8Click
    end
    object Button9: TButton
      Left = 8
      Top = 121
      Width = 137
      Height = 25
      Caption = 'Retorne Descricao'
      TabOrder = 3
      OnClick = Button9Click
    end
    object Button10: TButton
      Left = 8
      Top = 146
      Width = 137
      Height = 25
      Caption = 'Retorne Nome Arquivo'
      TabOrder = 4
      OnClick = Button10Click
    end
    object Button13: TButton
      Left = 8
      Top = 171
      Width = 137
      Height = 25
      Caption = 'Status do Certificado'
      TabOrder = 5
      OnClick = Button13Click
    end
    object edIndex: TEdit
      Left = 99
      Top = 277
      Width = 43
      Height = 21
      TabOrder = 6
      Text = '0'
    end
    object Button5: TButton
      Left = 8
      Top = 21
      Width = 137
      Height = 25
      Caption = 'Retorne Num Assinaturas'
      TabOrder = 7
      OnClick = Button5Click
    end
    object Button24: TButton
      Left = 8
      Top = 196
      Width = 137
      Height = 25
      Caption = 'Data da Assinatura (UTC)'
      TabOrder = 8
      OnClick = Button24Click
    end
    object Button25: TButton
      Left = 8
      Top = 221
      Width = 137
      Height = 25
      Caption = 'Data da Assinatura (Calc)'
      TabOrder = 9
      OnClick = Button25Click
    end
  end
  object edArq: TEdit
    Left = 246
    Top = 56
    Width = 161
    Height = 21
    TabOrder = 3
    Text = 'C:\teste.txt'
  end
  object edDestino: TEdit
    Left = 246
    Top = 80
    Width = 161
    Height = 21
    TabOrder = 4
    Text = 'C:\teste'
  end
  object edComentario: TEdit
    Left = 246
    Top = 104
    Width = 161
    Height = 21
    TabOrder = 5
    Text = 'Este '#233' o coment'#225'rio'
  end
  object GroupBox4: TGroupBox
    Left = 16
    Top = 337
    Width = 137
    Height = 73
    Caption = 'Local'
    TabOrder = 6
    object rMy: TRadioButton
      Left = 8
      Top = 16
      Width = 57
      Height = 17
      Caption = 'MY'
      Checked = True
      TabOrder = 0
      TabStop = True
    end
    object rOutras: TRadioButton
      Left = 8
      Top = 32
      Width = 89
      Height = 17
      Caption = 'AddressBook'
      TabOrder = 1
    end
    object rCA: TRadioButton
      Left = 8
      Top = 48
      Width = 57
      Height = 17
      Caption = 'CA'
      TabOrder = 2
    end
  end
  object GroupBox5: TGroupBox
    Left = 154
    Top = 338
    Width = 137
    Height = 71
    Caption = 'Armazenamento'
    TabOrder = 7
    object rCurrent: TRadioButton
      Left = 16
      Top = 16
      Width = 113
      Height = 17
      Caption = 'CURRENT_USER'
      Checked = True
      TabOrder = 0
      TabStop = True
    end
    object rLocal: TRadioButton
      Left = 16
      Top = 32
      Width = 113
      Height = 17
      Caption = 'LOCAL_MACHINE'
      TabOrder = 1
    end
  end
  object GroupBox6: TGroupBox
    Left = 16
    Top = 8
    Width = 161
    Height = 169
    Caption = 'Repositorio'
    TabOrder = 8
    object Button14: TButton
      Left = 8
      Top = 24
      Width = 137
      Height = 25
      Caption = 'Inicialize Reposit'#243'rio'
      TabOrder = 0
      OnClick = Button14Click
    end
    object Button16: TButton
      Left = 8
      Top = 49
      Width = 137
      Height = 25
      Caption = 'Get Num Cert Repositorio'
      TabOrder = 1
      OnClick = Button16Click
    end
    object Button17: TButton
      Left = 8
      Top = 74
      Width = 137
      Height = 25
      Caption = 'Mostre Certificado'
      TabOrder = 2
      OnClick = Button17Click
    end
    object Button18: TButton
      Left = 8
      Top = 99
      Width = 137
      Height = 25
      Caption = 'Adicionar Certificado'
      TabOrder = 3
      OnClick = Button18Click
    end
    object Button15: TButton
      Left = 8
      Top = 124
      Width = 137
      Height = 25
      Caption = 'Finalize Reposit'#243'rio'
      TabOrder = 4
      OnClick = Button15Click
    end
  end
  object cbCerts: TComboBox
    Left = 247
    Top = 33
    Width = 161
    Height = 21
    ItemHeight = 13
    TabOrder = 9
  end
  object GroupBox7: TGroupBox
    Left = 296
    Top = 288
    Width = 121
    Height = 121
    Caption = 'CRL'
    TabOrder = 10
    object Button1: TButton
      Left = 16
      Top = 40
      Width = 75
      Height = 25
      Caption = 'Existe CRL'
      TabOrder = 0
      OnClick = Button1Click
    end
    object Button19: TButton
      Left = 16
      Top = 90
      Width = 75
      Height = 25
      Caption = 'Instalar CRL'
      TabOrder = 1
      OnClick = Button19Click
    end
    object Button20: TButton
      Left = 16
      Top = 65
      Width = 75
      Height = 25
      Caption = 'Get CRL'
      TabOrder = 2
      OnClick = Button20Click
    end
    object Button21: TButton
      Left = 16
      Top = 16
      Width = 75
      Height = 25
      Caption = 'Get Num CRL'
      TabOrder = 3
      OnClick = Button21Click
    end
  end
end
