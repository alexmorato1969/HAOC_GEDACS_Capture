object Form1: TForm1
  Left = 353
  Top = 180
  Width = 850
  Height = 554
  Caption = 'Form1'
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
  object Label6: TLabel
    Left = 48
    Top = 196
    Width = 55
    Height = 13
    Caption = 'Certificados'
  end
  object Label1: TLabel
    Left = 16
    Top = 160
    Width = 459
    Height = 24
    Caption = 'Clique no certificado que voc'#234' deseja extrair os dados:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 87
    Top = 256
    Width = 199
    Height = 20
    Caption = 'Subject CN (Nome Comum):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 10
    Top = 288
    Width = 276
    Height = 20
    Caption = 'Subject OU (Unidade da Organiza'#231#227'o):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 118
    Top = 320
    Width = 168
    Height = 20
    Caption = 'Subject O Organiza'#231#227'o:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 124
    Top = 352
    Width = 162
    Height = 20
    Caption = 'Subject L (Localidade):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label7: TLabel
    Left = 148
    Top = 384
    Width = 138
    Height = 20
    Caption = 'Subject S (Estado):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label8: TLabel
    Left = 169
    Top = 416
    Width = 117
    Height = 20
    Caption = 'Subject C (Pais):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label9: TLabel
    Left = 155
    Top = 448
    Width = 131
    Height = 20
    Caption = 'Subject E (E-mail):'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelCN: TLabel
    Left = 304
    Top = 256
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelOU: TLabel
    Left = 304
    Top = 288
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelO: TLabel
    Left = 304
    Top = 320
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelL: TLabel
    Left = 304
    Top = 352
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelS: TLabel
    Left = 304
    Top = 384
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelC: TLabel
    Left = 304
    Top = 416
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelE: TLabel
    Left = 304
    Top = 448
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelCertificadoBRy: TLabel
    Left = 47
    Top = 480
    Width = 239
    Height = 20
    Caption = #201' um certificado Classe 2 da BRy:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clHotLight
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object LabelCertValido: TLabel
    Left = 299
    Top = 480
    Width = 4
    Height = 20
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Button1: TButton
    Left = 56
    Top = 16
    Width = 201
    Height = 25
    Caption = 'Carrega certificados do Store abaixo'
    TabOrder = 0
    OnClick = Button1Click
  end
  object GroupBox4: TGroupBox
    Left = 16
    Top = 49
    Width = 137
    Height = 73
    Caption = 'Local'
    TabOrder = 1
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
    Top = 50
    Width = 137
    Height = 71
    Caption = 'Armazenamento'
    TabOrder = 2
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
  object cbCerts: TComboBox
    Left = 111
    Top = 193
    Width = 161
    Height = 21
    ItemHeight = 13
    TabOrder = 3
    OnClick = cbCertsClick
  end
end
