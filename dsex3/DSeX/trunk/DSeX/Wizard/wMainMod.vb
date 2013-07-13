Imports DSeX.IniFile

Module wMainMod
    Public ScriptIni As IniFile = New IniFile

    Public ScriptPaths As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

    Public Function IsOdd(ByRef num As UInt32) As Boolean
        Return num Mod 2 <> 0
    End Function

    Public Function IsEven(ByRef num As UInt32) As Boolean
        Return num Mod 2 = 0
    End Function
End Module
