{+-----------------------------------------------------------------------------+
 | Class:       TSynDSSyn
 | Created:     2010-09-08
 | Last change: 2010-10-25
 | Author:      Lothus
 | Description: Dragonspeak Syntax Parser/Highlighter
 | Version:     1.15
 |
 | Copyright (c) 2010 Lothus. All rights reserved.
 |
 | Generated with SynGen - and then modified in several important ways.
 +----------------------------------------------------------------------------+}

{$IFNDEF QSYNDSHIGHLIGHT}
unit SynDSHighlight;
{$ENDIF}

{$I SynEdit.inc}

interface

uses
{$IFDEF SYN_CLX}
  QGraphics,
  QSynEditTypes,
  QSynEditHighlighter,
  QSynUnicode,
{$ELSE}
  Graphics,
  SynEditTypes,
  SynEditHighlighter,
  SynUnicode,
{$ENDIF}
  SysUtils,
  Classes, Windows;

type
  TtkTokenKind = (
    tkComment,
    tkDigits,
    tkCommandDigits,
    tkFooter,
    tkHolder,
    tkIdentifier,
    tkKey,
    tkNull,
    tkSpace,
    tkString,
    tkStringVar,
    tkSymbol,
    tkUnknown,
    tkVariable);

  TRangeState = (rsUnknown, rsString, rsStringVar, rsStringStrVar);

  TProcTableProc = procedure of object;

  PIdentFuncTableFunc = ^TIdentFuncTableFunc;
  TIdentFuncTableFunc = function (Index: Integer): TtkTokenKind of object;

type
  TSynDSSyn = class(TSynCustomHighlighter)
  private
    fRange: TRangeState;
    fTokenID: TtkTokenKind;
    fIdentFuncTable: array[0..1] of TIdentFuncTableFunc;
    fCommentAttri: TSynHighlighterAttributes;
    fDigitsAttri: TSynHighlighterAttributes;
    fCommandDigitsAttri: TSynHighlighterAttributes;
    fFooterAttri: TSynHighlighterAttributes;
    fHolderAttri: TSynHighlighterAttributes;
    fIdentifierAttri: TSynHighlighterAttributes;
    fKeyAttri: TSynHighlighterAttributes;
    fSpaceAttri: TSynHighlighterAttributes;
    fStringAttri: TSynHighlighterAttributes;
    fStringVarAttri: TSynHighlighterAttributes;
    fSymbolAttri: TSynHighlighterAttributes;
    fVariableAttri: TSynHighlighterAttributes;
    fheader : string;
    fheadert : string;
    ffooter : string;
    ffootert : string;
    function HashKey(Str: PWideChar): Cardinal;
    function FuncSomeinsanekeyword(Index: Integer): TtkTokenKind;
    procedure CommentProc;
    procedure IdentProc;
    procedure NumberProc;
    procedure NumberHolderProc;
    procedure StringVariableProc;
    procedure VariableProc;
    procedure UnknownProc;
    function AltFunc(Index: Integer): TtkTokenKind;
    procedure InitIdent;
    function IdentKind(MayBe: PWideChar): TtkTokenKind;
    procedure NullProc;
    procedure SpaceProc;
    procedure CRProc;
    procedure LFProc;
    procedure StringOpenProc;
    procedure StringProc;
  protected
    function GetSampleSource: UnicodeString; override;
    function IsFilterStored: Boolean; override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure SetFooter(data: string);
    procedure SetHeader(data: string);
    class function GetFriendlyLanguageName: UnicodeString; override;
    class function GetLanguageName: string; override;
    function GetRange: Pointer; override;
    procedure ResetRange; override;
    procedure SetRange(Value: Pointer); override;
    function GetDefaultAttribute(Index: Integer): TSynHighlighterAttributes; override;
    function GetEol: Boolean; override;
    function GetKeyWords(TokenKind: Integer): UnicodeString; override;
    function GetTokenID: TtkTokenKind;
    function GetTokenAttribute: TSynHighlighterAttributes; override;
    function GetTokenKind: Integer; override;
    function IsIdentChar(AChar: WideChar): Boolean; override;
    procedure Next; override;
  published
    property CommentAttri: TSynHighlighterAttributes read fCommentAttri write fCommentAttri;
    property DigitsAttri: TSynHighlighterAttributes read fDigitsAttri write fDigitsAttri;
    property CommandDigitsAttri: TSynHighlighterAttributes read fCommandDigitsAttri write fCommandDigitsAttri;
    property FooterAttri: TSynHighlighterAttributes read fFooterAttri write fFooterAttri;
    property HolderAttri: TSynHighlighterAttributes read fHolderAttri write fHolderAttri;
    property IdentifierAttri: TSynHighlighterAttributes read fIdentifierAttri write fIdentifierAttri;
    property KeyAttri: TSynHighlighterAttributes read fKeyAttri write fKeyAttri;
    property SpaceAttri: TSynHighlighterAttributes read fSpaceAttri write fSpaceAttri;
    property StringAttri: TSynHighlighterAttributes read fStringAttri write fStringAttri;
    property StringVarAttri: TSynHighlighterAttributes read fStringVarAttri write fStringVarAttri;
    property SymbolAttri: TSynHighlighterAttributes read fSymbolAttri write fSymbolAttri;
    property VariableAttri: TSynHighlighterAttributes read fVariableAttri write fVariableAttri;
    property Header:string read fHeader write SetHeader;
    property Footer:string read fFooter write SetFooter;
  end;

implementation

uses
{$IFDEF SYN_CLX}
  QSynEditStrConst;
{$ELSE}
  SynEditStrConst;
{$ENDIF}

resourcestring
  SYNS_FilterFurcadiaDragonspeak = 'Furcadia Dragonspeak (*.ds)|*.ds';
  SYNS_LangFurcadiaDragonspeak = 'Furcadia Dragonspeak';
  SYNS_FriendlyLangFurcadiaDragonspeak = 'Furcadia Dragonspeak';
  SYNS_AttrDigits = 'Digits';
  SYNS_FriendlyAttrDigits = 'Digits';
  SYNS_AttrCommandDigits = 'CDigits';
  SYNS_FriendlyAttrCommandDigits = 'Trigger Codes';
  SYNS_AttrFooter = 'Footer';
  SYNS_FriendlyAttrFooter = 'Footer';
  SYNS_AttrHolder = 'Holder';
  SYNS_FriendlyAttrHolder = 'Holder';
  SYNS_AttrStringVar = 'StringVar';
  SYNS_FriendlyAttrStringVar = 'StringVar';
  SYNS_Foot = '*Endtriggers* 8888 *Endtriggers*';
  SYNS_Head = 'DSPK V04.00 Furcadia';

const
  // as this language is case-insensitive keywords *must* be in lowercase
  KeyWords: array[0..0] of UnicodeString = (
    'someinsanekeyword' 
  );

  KeyIndices: array[0..1] of Integer = (
    -1, 0 
  );

procedure TSynDSSyn.SetFooter(data: string);
begin
fFooter := data;
fFooterT := lowercase(data);
end;

procedure TSynDSSyn.SetHeader(data: string);
begin
fHeader := data;
fHeaderT := lowercase(data);
end;

procedure TSynDSSyn.InitIdent;
var
  i: Integer;
begin
  for i := Low(fIdentFuncTable) to High(fIdentFuncTable) do
    if KeyIndices[i] = -1 then
      fIdentFuncTable[i] := AltFunc;

  fIdentFuncTable[1] := FuncSomeinsanekeyword;
end;

{$Q-}
function TSynDSSyn.HashKey(Str: PWideChar): Cardinal;
begin
  Result := 0;
  while IsIdentChar(Str^) do
  begin
    Result := Result + Ord(Str^);
    inc(Str);
  end;
  Result := Result mod 2;
  fStringLen := Str - fToIdent;
end;
{$Q+}

function TSynDSSyn.FuncSomeinsanekeyword(Index: Integer): TtkTokenKind;
begin
  if IsCurrentToken(KeyWords[Index]) then
    Result := tkKey
  else
    Result := tkIdentifier;
end;

function TSynDSSyn.AltFunc(Index: Integer): TtkTokenKind;
begin
  Result := tkIdentifier;
end;

function TSynDSSyn.IdentKind(MayBe: PWideChar): TtkTokenKind;
var
  Key: Cardinal;
begin
  fToIdent := MayBe;
  Key := HashKey(MayBe);
  if Key <= High(fIdentFuncTable) then
    Result := fIdentFuncTable[Key](KeyIndices[Key])
  else
    Result := tkIdentifier;
end;

procedure TSynDSSyn.SpaceProc;
begin
  inc(Run);
  fTokenID := tkSpace;
  while (FLine[Run] <= #32) and not IsLineEnd(Run) do inc(Run);
end;

procedure TSynDSSyn.NullProc;
begin
  fTokenID := tkNull;
  inc(Run);
end;

procedure TSynDSSyn.CRProc;
begin
  fTokenID := tkSpace;
  inc(Run);
  if fLine[Run] = #10 then
    inc(Run);
end;

procedure TSynDSSyn.LFProc;
begin
  fTokenID := tkSpace;
  inc(Run);
end;

procedure TSynDSSyn.StringOpenProc;
begin
  fRange := rsString;
  fTokenID := tkString;
  //Inc(Run);
  StringProc;
end;

procedure TSynDSSyn.StringProc;
begin
  fTokenID := tkString;
  repeat
    if (fLine[Run] = '}') then
     begin
     run := run + 1;
     //Inc(Run);//, 1);
     fRange := rsUnknown;
     Break;
     end;
    case fLine[Run] of
     #0: begin
         NullProc;
         Exit;
         end;
     #10:begin
         LFProc;
         Exit;
         end;
     #13:begin
         CRProc;
         Exit;
         end;
     '%': begin
          fRange := rsStringVar;
          exit;
          end;
     '~': begin
          fRange := rsStringStrVar;
          exit;
          end;
     end;
  if not (isLineEnd(run)) then
     //Inc(Run);
     run := run + 1;
  until (IsLineEnd(Run));
end;

constructor TSynDSSyn.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  fCaseSensitive := False;

  fCommentAttri := TSynHighLighterAttributes.Create(SYNS_AttrComment, SYNS_FriendlyAttrComment);
  fCommentAttri.Style := [fsItalic];
  fCommentAttri.Foreground := clNavy;
  AddAttribute(fCommentAttri);

  fDigitsAttri := TSynHighLighterAttributes.Create(SYNS_AttrDigits, SYNS_FriendlyAttrDigits);
  fDigitsAttri.Foreground := clBlue;
  AddAttribute(fDigitsAttri);

  fCommandDigitsAttri := TSynHighLighterAttributes.Create(SYNS_AttrCommandDigits, SYNS_FriendlyAttrCommandDigits);
  fCommandDigitsAttri.Foreground := clFuchsia;
  AddAttribute(fCommandDigitsAttri);

  fFooterAttri := TSynHighLighterAttributes.Create(SYNS_AttrFooter, SYNS_FriendlyAttrFooter);
  fFooterAttri.Foreground := clGreen;
  AddAttribute(fFooterAttri);

  fHolderAttri := TSynHighLighterAttributes.Create(SYNS_AttrHolder, SYNS_FriendlyAttrHolder);
  fHolderAttri.Foreground := clMaroon;
  AddAttribute(fHolderAttri);

  fIdentifierAttri := TSynHighLighterAttributes.Create(SYNS_AttrIdentifier, SYNS_FriendlyAttrIdentifier);
  fIdentifierAttri.Foreground := clBlack;
  AddAttribute(fIdentifierAttri);

  fKeyAttri := TSynHighLighterAttributes.Create(SYNS_AttrReservedWord, SYNS_FriendlyAttrReservedWord);
  fKeyAttri.Style := [fsBold];
  AddAttribute(fKeyAttri);

  fSpaceAttri := TSynHighLighterAttributes.Create(SYNS_AttrSpace, SYNS_FriendlyAttrSpace);
  fSpaceAttri.Background := clWhite;;
  AddAttribute(fSpaceAttri);

  fStringAttri := TSynHighLighterAttributes.Create(SYNS_AttrString, SYNS_FriendlyAttrString);
  fStringAttri.Foreground := clRed;
  AddAttribute(fStringAttri);

  fStringVarAttri := TSynHighLighterAttributes.Create(SYNS_AttrStringVar, SYNS_FriendlyAttrStringVar);
  fStringVarAttri.Foreground := clBlue;
  AddAttribute(fStringVarAttri);

  fSymbolAttri := TSynHighLighterAttributes.Create(SYNS_AttrSymbol, SYNS_FriendlyAttrSymbol);
  fSymbolAttri.Foreground := clBlack;
  AddAttribute(fSymbolAttri);

  fVariableAttri := TSynHighLighterAttributes.Create(SYNS_AttrVariable, SYNS_FriendlyAttrVariable);
  fVariableAttri.Foreground := clPurple;
  AddAttribute(fVariableAttri);

  SetAttributesOnChange(DefHighlightChange);
  InitIdent;
  fDefaultFilter := SYNS_FilterFurcadiaDragonspeak;
  fRange := rsUnknown;
  Footer := SYNS_Foot;
  Header := SYNS_Head;
end;

procedure TSynDSSyn.CommentProc;
begin
//OutputDebugString(pchar('Comment: '+fLine));
//OutputDebugString(pchar('Missed Footer: '+ffooter));
  if (FLine <> {'*Endtriggers* 8888 *Endtriggers*'} fFootert) then
   begin
   if run = 0 then
    begin
     fTokenID := tkComment;
     Inc(Run, 1);
     while (FLine[Run] <> #0) do begin
       case FLine[Run] of
         #10, #13: break;
       end; { case }
      Inc(Run);
     end; { while }
    end
    else
     begin
     fTokenID := tkIdentifier;
     inc(run);
     end;
   end
 else
  begin
  fTokenID := tkFooter;
   while (FLine[Run] <> #0) do begin
    {case FLine[Run] of
      #10, #13: break;
     end; { case }
   if IsLineEnd(run) then break;
    Inc(Run);
    end;
  end;
end;

procedure TSynDSSyn.IdentProc;
begin
  if fline <> {'DSPK V03.00 Furcadia'} fHeadert then
 begin
 fTokenID := IdentKind((fLine + Run));
 inc(Run, fStringLen);
 //while Identifiers[fLine[Run]] do Inc(Run);
 end
else
 begin
 fTokenID := tkFooter;
   while (FLine[Run] <> #0) do begin
    case FLine[Run] of
      #10, #13: break;
     end; { case }
    Inc(Run);
    end;
 end;
end;

procedure TSynDSSyn.NumberProc;
begin
  fTokenID := tkDigits;
  repeat
    Inc(Run);
  //until not (fLine[Run] in ['0'..'9']);
  if (fline[run] = ':') and (charinset(fline[run+1],['0'..'9'])) then
   begin
   fTokenID := tkCommandDigits;
   inc(Run);
   end;
  until (not CharInSet(fLine[Run],['0'..'9']));
end;

procedure TSynDSSyn.NumberHolderProc;
begin
  fTokenID := tkHolder;
  repeat
    Inc(Run);
  until not (fLine[Run] = '#');
end;

procedure TSynDSSyn.StringVariableProc;
begin
  fTokenID := tkStringVar;
  repeat
    Inc(Run);
//until (not (fLine[Run] in ['a'..'z', 'A'..'Z', '0'..'9']));
until (not CharInSet(fLine[Run],['a'..'z', 'A'..'Z', '0'..'9', '_']));
if fRange = rsStringStrVar then
 fRange := rsString
else
 fRange := rsUnknown;
end;

procedure TSynDSSyn.VariableProc;
begin
  fTokenID := tkVariable;
  repeat
    Inc(Run);
//until (not (fLine[Run] in ['a'..'z', 'A'..'Z', '0'..'9']));
until (not CharInSet(fLine[Run],['a'..'z', 'A'..'Z', '0'..'9', '_']));
if fRange = rsStringVar then
 fRange := rsString
else
 fRange := rsUnknown;
end;

procedure TSynDSSyn.UnknownProc;
begin
  inc(Run);
  fTokenID := tkUnknown;
end;

procedure TSynDSSyn.Next;
begin
  fTokenPos := Run;
  //if fRange = rsString then OutputDebugString('We''re still in string mode');
  case fRange of
   rsString: StringProc;
   rsStringVar: VariableProc;
   rsStringStrVar: StringVariableProc
  else
    case fLine[Run] of
      #0: NullProc;
      #10: LFProc;
      #13: CRProc;
      '{': StringOpenProc;
      #1..#9, #11, #12, #14..#32: SpaceProc;
      '*': CommentProc;
      'A'..'Z', 'a'..'z', '_': IdentProc;
      '0'..'9': NumberProc;
      '#': NumberHolderProc;
      '~': StringVariableProc;
      '%': VariableProc;
    else
      UnknownProc;
    end;
  end;
  //end;
  inherited;
end;

function TSynDSSyn.GetDefaultAttribute(Index: Integer): TSynHighLighterAttributes;
begin
  case Index of
    SYN_ATTR_COMMENT: Result := fCommentAttri;
    SYN_ATTR_IDENTIFIER: Result := fIdentifierAttri;
    SYN_ATTR_KEYWORD: Result := fKeyAttri;
    SYN_ATTR_STRING: Result := fStringAttri;
    SYN_ATTR_WHITESPACE: Result := fSpaceAttri;
    SYN_ATTR_SYMBOL: Result := fSymbolAttri;
  else
    Result := nil;
  end;
end;

function TSynDSSyn.GetEol: Boolean;
begin
  Result := Run = fLineLen + 1;
end;

function TSynDSSyn.GetKeyWords(TokenKind: Integer): UnicodeString;
begin
  Result := 
    'someinsanekeyword';
end;

function TSynDSSyn.GetTokenID: TtkTokenKind;
begin
  Result := fTokenId;
end;

function TSynDSSyn.GetTokenAttribute: TSynHighLighterAttributes;
begin
  case GetTokenID of
    tkComment: Result := fCommentAttri;
    tkDigits: Result := fDigitsAttri;
    tkCommandDigits: Result := fCommandDigitsAttri;
    tkFooter: Result := fFooterAttri;
    tkHolder: Result := fHolderAttri;
    tkIdentifier: Result := fIdentifierAttri;
    tkKey: Result := fKeyAttri;
    tkSpace: Result := fSpaceAttri;
    tkString: Result := fStringAttri;
    tkStringVar: Result := fStringVarAttri;
    tkSymbol: Result := fSymbolAttri;
    tkVariable: Result := fVariableAttri;
    tkUnknown: Result := fIdentifierAttri;
  else
    Result := nil;
  end;
end;

function TSynDSSyn.GetTokenKind: Integer;
begin
  Result := Ord(fTokenId);
end;

function TSynDSSyn.IsIdentChar(AChar: WideChar): Boolean;
begin
  case AChar of
    '_', '#', '%', '0'..'9', 'a'..'z', 'A'..'Z':
      Result := True;
    else
      Result := False;
  end;
end;

function TSynDSSyn.GetSampleSource: UnicodeString;
begin
  Result := 
'*Test';
end;

function TSynDSSyn.IsFilterStored: Boolean;
begin
  Result := fDefaultFilter <> SYNS_FilterFurcadiaDragonspeak;
end;

class function TSynDSSyn.GetFriendlyLanguageName: UnicodeString;
begin
  Result := SYNS_FriendlyLangFurcadiaDragonspeak;
end;

class function TSynDSSyn.GetLanguageName: string;
begin
  Result := SYNS_LangFurcadiaDragonspeak;
end;

procedure TSynDSSyn.ResetRange;
begin
  fRange := rsUnknown;
end;

procedure TSynDSSyn.SetRange(Value: Pointer);
begin
  fRange := TRangeState(Value);
end;

function TSynDSSyn.GetRange: Pointer;
begin
  Result := Pointer(fRange);
end;

initialization
{$IFNDEF SYN_CPPB_1}
  RegisterPlaceableHighlighter(TSynDSSyn);
{$ENDIF}
end.
