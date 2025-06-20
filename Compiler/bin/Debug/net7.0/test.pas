program demo;
var
  a: integer;
  b: array [1..5] of integer;
begin
  a := 3;
  b[2]:= a * 5 + 1;
  if b[2]  10 then
    a:= a + 1
  else
    a:= a - 1;
  for a:= 1 to 5 do
    b[a] := a * a;
  while a < 10 do 
  begin
    a:= a + a; 
  end;
  repeat
    a:= a - 1
  until a = 0;
end.