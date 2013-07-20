procedure TDSWiz.AdvanceCounters;
var
  I, it1, mini, maxi: Integer;
  tname : string;
  parser : TRegexpr;
  bounded : boolean;
begin
//Advance all range variables
mini := 0;
maxi := 0;
parser := TRegexpr.Create;
 try
 parser.Expression := RGEX_Range;
 for I := 0 to svalues.Count - 1 do
   begin
   bounded := false;
   if parser.Exec(svalues.ValueFromIndex[I]) then //Found a range
    begin
    tname := svalues.Names[I];
    if counters.Values[tname] <> '' then
     begin
     it1 := strtoint(counters.Values[tname]);

     if (parser.Match[RPOS_Range_End] <> '') then //Could be infinite!
      begin
      mini := strtoint(parser.Match[RPOS_Range_Start]);
      maxi := strtoint(parser.Match[RPOS_Range_End]);
      bounded := true;
      end;
     //Check for boundaries
     if bounded then
      begin
      if mini > maxi then
        begin
        it1 := it1 - 1;
        if it1 < maxi then
         begin
         //No need for any fancy loop-testing since we're always incrementing by one.
         it1 := mini;
         end;
        end
      else
       begin
        it1 := it1 + 1;
        if it1 > maxi then
         begin
         //No need for any fancy loop-testing since we're always incrementing by one.
         it1 := mini;
         end;
       end;
      end
     else
      it1 := it1 + 1; //Unbounded, assume +

     counters.Values[svalues.Names[I]] := inttostr(it1); //Set final variable
     end
    else
     counters.Values[svalues.Names[I]] := parser.Match[RPOS_Range_Start]; //Nothing? Set it default!
    end;
   end;
 finally
 parser.Free;
 end;
end;
RGEX_Range = '^(\d+)(-\s*)(\d+)?';
RPOS_Range_Start = 1;
 RPOS_Range_Marker = 2;
 RPOS_Range_End = 3;