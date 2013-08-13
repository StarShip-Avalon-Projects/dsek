Imports System.Text
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports ScintillaNET
Imports DSeX.ConfigStructs
Public Class PwrLexer

    Private Shared Lock As Boolean = False
    ' Origional Thread for this class
    'http://scintillanet.codeplex.com/discussions/42949
    Private Const STYLE_DEFAULT As Integer = 0
    Private Const STYLE_STRING As Integer = 11
    Private Const STYLE_NUMBER As Integer = 12
    Private Const STYLE_COMMENT As Integer = 14
    Private Const STYLE_NUM_VAR As Integer = 15
    Private Const STYLE_STR_VAR As Integer = 16
    Private Const STYLE_HEADER As Integer = 17
    Private Const STYLE_ID As Integer = 18

    Private Shared HEADER As String = KeysIni.GetKeyValue("MS-General", "Header")

    Private Shared RegHeader As New Regex("^(" + HEADER + ")", RegexOptions.IgnoreCase)

    Public Shared Sub Init(scintilla As Scintilla)
        scintilla.Indentation.SmartIndentType = SmartIndent.None
        'scintilla.ConfigurationManager.Language = "dragonspeak"
        scintilla.Lexing.LexerName = "dragonspeak"
        scintilla.Styles(STYLE_DEFAULT).ForeColor = Color.Black
        scintilla.Styles(STYLE_STR_VAR).ForeColor = EditSettings.StringVariableColor
        scintilla.Styles(STYLE_NUM_VAR).ForeColor = EditSettings.VariableColor
        scintilla.Styles(STYLE_STRING).ForeColor = EditSettings.StringColor
        scintilla.Styles(STYLE_COMMENT).ForeColor = EditSettings.CommentColor
        scintilla.Styles(STYLE_ID).ForeColor = EditSettings.IDColor
        scintilla.Styles(STYLE_NUMBER).ForeColor = EditSettings.NumberColor
        scintilla.Styles(STYLE_HEADER).ForeColor = Color.Green
        scintilla.Styles(STYLE_HEADER).Bold = True
    End Sub

    Public Shared Sub StyleNeeded(ByRef scintilla As Scintilla, ByRef range As Range)

        Dim start As Integer = range.StartingLine.StartPosition
        Dim [end] As Integer = start
        Dim max As Integer = scintilla.Lines(scintilla.Lines.Count - 1).StartPosition + scintilla.Lines(scintilla.Lines.Count - 1).Length
        Debug.Print("StyleNeededEventArgs()")
        'we'll get the whole update range at once
        'we also get the maximum editor range, incase a new comment goes to the end of the file
        'Dim curr As Line = range.StartingLine
        'While curr.Number <= range.EndingLine.Number
        '    [end] += curr.Length
        '    curr = curr.[Next]
        'End While
        Dim test As String = range.Text
        [end] += test.Length
        StyleSection(scintilla, start, [end], max, test)

    End Sub

    Public Shared Sub StyleSection(ByRef scintilla As Scintilla, ByRef start As Integer, ByRef [end] As Integer, ByRef max As Integer, ByRef Txt As String)
        Dim pos As Integer = start
        Dim i As Integer = 0
        While pos < [end]

            Dim curr As String = scintilla.GetRange(pos, [end]).Text

            'make a couple of direct checks for special handling (comments/string literals) pass the rest to a RegEx handler
            If RegHeader.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegHeader, STYLE_HEADER, pos, [end], max)
                Exit While
            End If
        End While
    End Sub

    Public Shared Function StyleHeader(ByRef scintilla As Scintilla, ByRef start As Integer, ByRef [end] As Integer, ByRef max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 0
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (start + offset < max)
            offset += 1
        End While
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, STYLE_HEADER)
        Return offset
    End Function
    Public Shared Function StyleCommentSingle(ByRef scintilla As Scintilla, ByRef start As Integer, ByRef [end] As Integer, ByRef max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 0
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (start + offset < max - 1)
            offset += 1
        End While
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, STYLE_COMMENT)
        Return offset
    End Function

    Public Shared Function StyleString(ByRef scintilla As Scintilla, ByRef text As String, ByRef start As Integer, ByRef [end] As Integer, ByRef max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 1
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (full(offset) <> "}"c) AndAlso (start + offset < max - 1)
            offset += 1
        End While
        offset += 1
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, STYLE_STRING)
        Return offset
    End Function

    Public Shared Function StyleRegExWhole(ByRef scintilla As Scintilla, ByRef text As String, ByRef reg As Regex, ByRef style As Integer, ByRef start As Integer, ByRef [end] As Integer, _
     ByRef max As Integer) As Integer
        'match & style an entire regex
        Dim match As String = reg.Match(text).Value
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(match.Length, style)
        Return match.Length
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
End Class
