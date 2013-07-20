  //Regexes for calculation parsing
  RGEX_Movement = '(\d+)(nw|ne|sw|se)(\d+)(.*)';
  RGEX_MathCalc = '(\d+)(\+|-|\*|/)(\d+)(.*)';
  RGEX_MathStep = '(\+|-|\*|/)(\d+)';
  RGEX_Coordinate = '(\d+)\s*,\s*(\d+)';
  RGEX_Number = '^(\d+)';
  RGEX_Mov_Steps = '(nw|ne|sw|se)(\d+)';
  RGEX_Range = '^(\d+)(-\s*)(\d+)?';
  
  //Regular Expression match indexes
 RPOS_Movement_Var = 1;
 RPOS_Movement_Dir = 2;
 RPOS_Movement_Dist = 3;
 RPOS_Movement_More = 4;
 RPOS_Mov_Steps_Dir = 1;
 RPOS_Mov_Steps_Dist = 2;
 RPOS_MathCalc_Var = 1;
 RPOS_MathCalc_Op = 2;
 RPOS_MathCalc_Val = 3;
 RPOS_MathCalc_More = 4;
 RPOS_MathStep_Op = 1;
 RPOS_MathStep_Num = 2;
 RPOS_Number_Num = 1;
 RPOS_Coord_X = 1;
 RPOS_Coord_Y = 2;
 RPOS_Range_Start = 1;
 RPOS_Range_Marker = 2;
 RPOS_Range_End = 3;

function TDSWiz.MoveCoord(variable, directions: string): string;
var
 x, y, i: integer;
 direction : string;
 distance : integer;
 coordParser : TRegexpr;
 stepParser : TRegExpr;
begin
//Move coordinates as intended
//OutputDebugString(pchar('MoveCoord Called: "'+variable+'","'+directions+'",'+inttostr(distance)));

coordParser := TRegexpr.Create;
coordParser.Expression := RGEX_Coordinate;
stepParser := TRegexpr.Create;
stepParser.ModifierI := true;
stepParser.Expression := RGEX_Mov_Steps;
result := '';

if not coordParser.Exec(variable) then
 begin
 //Bomb out quickly. :(
 OutputDebugString('MoveCoord: Invalid coordinate variable supplied.');
 exit;
 end;

//Set up our initial coordinates
x := strtoint(coordParser.Match[RPOS_Coord_X]);
y := strtoint(coordParser.Match[RPOS_Coord_Y]);

try
if (stepParser.Exec(directions)) then
repeat
 begin
  //Prepare calculations
  direction := stepParser.Match[RPOS_Mov_Steps_Dir];
  distance := strToInt(stepParser.Match[RPOS_Mov_Steps_Dist]);
 //Northwest
 if lowercase(direction) = 'nw' then
  begin
  for I := 0 to distance - 1 do
    if odd(y) then
  begin
   y := y - 1;
   x := x - 2;
  end
    else
  begin
   y := y - 1;
  end;
   result := inttostr(x)+','+inttostr(y);
   //OutputDebugString(pchar('MoveCoord: NorthWest, ('+variable+'), '+result));
  end
 else
 //Northeast
 if lowercase(direction) = 'ne' then
  begin
  for I := 0 to distance - 1 do
    if odd(y) then
  begin
   y := y - 1;
  end
    else
  begin
   x := x + 2;
   y := y - 1;
  end;
   result := inttostr(x)+','+inttostr(y);
   //OutputDebugString(pchar('MoveCoord: NorthEast, ('+variable+'), '+result));
  end
 else
 //Southwest
 if lowercase(direction) = 'sw' then
  begin
  for I := 0 to distance - 1 do
    if odd(y) then
  begin
   x := x - 2;
   y := y + 1;
  end
    else
  begin
   y := y + 1;
  end;
   result := inttostr(x)+','+inttostr(y);
   //OutputDebugString(pchar('MoveCoord: SouthWest, ('+variable+'), '+result));
  end
 else
 //Southeast
 if lowercase(direction) = 'se' then
  begin
  for I := 0 to distance - 1 do
    if odd(y) then
  begin
   y := y + 1;
  end
    else
  begin
   x := x + 2;
   y := y + 1;
  end;
   result := inttostr(x)+','+inttostr(y);
   //OutputDebugString(pchar('MoveCoord: SouthEast, ('+variable+'), '+result));
  end;
  end;
until not stepParser.ExecNext;
finally
 coordParser.Free;
 stepParser.Free;
end;
//If
//OutputDebugString(pchar('MoveCoord Result: '+result));

end;