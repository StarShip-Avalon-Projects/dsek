 RES_SEC_Marker = '**SECTION**  ';
 RES_DS_begin = 'DSPK V';
 RES_DS_end = '*Endtriggers* 8888 *Endtriggers*';
 RES_DSS_begin = 'DS-START';
 RES_DSS_End = 'DS-END';
 RES_Def_section = 'Default Section';
 RES_DSS_All = 'Entire Document';

type 
 TSecType = (SecNormal, SecEnd, SecFixed, SecDefault);
  TDSSegment = class(TObject)
   Title : string;
   Lines : TStringlist;
   Marks : TList;
   sectype : TSecType;
   //Undolist : ???  //Potentially store undo lists for each segment.
   constructor Create;
   destructor Destroy; override;
   end;

procedure TSysDsEd.UpdateSegments;
var
  I: Integer;
 tmpsec, sec2 : TDSSegment;
 bypass : boolean;
 t1 : string;
 blank : boolean;
begin
//Clear Old Segments
for i := 0 to DSSegments.Count - 1 do
  TDSSegment(DSSegments[i]).Free;
DSSegments.Clear;
//Build from the basics
tmpsec := TDSSegment.create;
tmpsec.Title := RES_Def_section;
DSSegments.Add(tmpsec);
bypass := false;
blank := true;
for i := 0 to fullfile.Count - 1 do
  begin
  //Header segment
  if (copy(fullfile[i],1,length(RES_DS_begin)) = RES_DS_begin) then
   begin
   if i = 0 then
    begin
    sec2 := TDSSegment.create;
    DSSegments.Insert(0,sec2);
    sec2.title := RES_DSS_Begin;
    sec2.Lines.add(fullfile[i]);
    sec2.sectype := SecFixed;
    bypass := true;
    end;
   end;
  //Ending segment
  if (fullfile[i] = RES_DS_End) then
   begin
    tmpsec := TDSSegment.create;
    DSSegments.Add(tmpsec);
    tmpsec.title := RES_DSS_End;
    tmpsec.sectype := SecEnd;
   end;
  if (fullfile[i] <> '') and (tmpsec.title = RES_Def_Section) and (blank) then
    begin
    blank := false;
    end;
  //Section marker
  if copy(fullfile[i],1,length(RES_SEC_Marker)) = RES_SEC_Marker then
   begin
   t1 := copy(fullfile[i],length(res_sec_marker)+1,length(fullfile[i]));
    if (not blank) then
     begin
     tmpsec := TDSSegment.Create;
     tmpsec.Title := t1;
     DSSegments.Add(tmpsec);
     end;
   bypass := true;
   end;
  if not bypass then
   tmpsec.Lines.Add(fullfile[i]);
  bypass := false;
  end;
end;

procedure TSysDsEd.UpdateSegmentList;
var
  i: Integer;
  tseg : TDSSegment;
begin
with Frame21 do
 begin
 CodeSects.Clear;
 CodeSects.items.Add(RES_DSS_All);
 for i := 0 to DSSegments.count - 1 do
  begin
  tseg := TDSSegment(DSSegments[i]);
  if (tseg.Title <> RES_DSS_begin) and (tseg.Title <> RES_DSS_end) then
   if ((tseg.Title = RES_Def_section) and ((i <= 1))) then
    begin
    CodeSects.items.add(TDSSegment(DSSegments[i]).Title);
    end
   else
    CodeSects.items.add(Sects_Indent+TDSSegment(DSSegments[i]).Title)
  else
   CodeSects.items.add(TDSSegment(DSSegments[i]).Title);
  end;
 end;
end;