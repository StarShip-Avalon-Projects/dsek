Imports DSeX.IniFile
Imports FastColoredTextBoxNS

Module MainModule
    Public KeysIni As IniFile = New IniFile
    Public ini As IniFile = New IniFile
    Public EditSettings As ConfigStructs.EditSettings = New ConfigStructs.EditSettings


    Public DS_String_Style As TextStyle = New TextStyle(Brushes.Brown, Nothing, FontStyle.Italic)
    Public DS_Str_Var_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Num_Var_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Comment_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Default_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Num_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Line_ID_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Regular)
    Public DS_Header_Style As TextStyle = New TextStyle(New SolidBrush(Color.Green), Nothing, FontStyle.Bold)

End Module
