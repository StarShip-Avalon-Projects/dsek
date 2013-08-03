Imports System.Text
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports ScintillaNET
Imports DSeX.ConfigStructs
Class PwrLexer
    Private Const ST_DEFAULT As Integer = 32
    Private Const ST_STRING_VAR As Integer = 11
    Private Const ST_NUM_VAR As Integer = 12
    Private Const ST_STRING As Integer = 13
    Private Const ST_ID As Integer = 14
    Private Const ST_NUMBER As Integer = 15
    Private Const ST_COMMENT As Integer = 16
    Private Const ST_HEADER As Integer = 17

    Private Shared RegWhiteSpace As New Regex("^\s+")
    Private Shared RegStrVar As New Regex("^~([A-Za-z0-9_]+)")
    Private Shared RegNumVar As New Regex("^%([A-Za-z0-9_]+)")
    Private Shared RegString As New Regex("^\{(.*?)\}")
    Private Shared RegLineID As New Regex("^\(([0-9]*)\:([0-9]*)\)")
    Private Shared RegNumber As New Regex("^([0-9#]+)")
    Private Shared RegHeader As New Regex("^(" + KeysIni.GetKeyValue("MS-General", "Header") + ")", RegexOptions.IgnoreCase)

    Public Shared Sub Init(scintilla As Scintilla)
        scintilla.Indentation.SmartIndentType = SmartIndent.None
        scintilla.ConfigurationManager.Language = [String].Empty
        scintilla.Lexing.LexerName = "container"
        scintilla.Lexing.Lexer = Lexer.Container

        scintilla.Styles(ST_DEFAULT).ForeColor = Color.Black
        scintilla.Styles(ST_STRING_VAR).ForeColor = EditSettings.StringVariableColor
        scintilla.Styles(ST_NUM_VAR).ForeColor = EditSettings.VariableColor
        scintilla.Styles(ST_STRING).ForeColor = EditSettings.StringColor
        scintilla.Styles(ST_COMMENT).ForeColor = EditSettings.CommentColor
        scintilla.Styles(ST_ID).ForeColor = EditSettings.IDColor
        scintilla.Styles(ST_NUMBER).ForeColor = EditSettings.NumberColor
        scintilla.Styles(ST_HEADER).ForeColor = Color.Green
        scintilla.Styles(ST_HEADER).Bold = True
    End Sub

    Public Shared Sub StyleNeeded(scintilla As Scintilla, range As Range)
        Dim start As Integer = range.StartingLine.StartPosition
        Dim [end] As Integer = start
        Dim max As Integer = scintilla.Lines(scintilla.Lines.Count - 1).StartPosition + scintilla.Lines(scintilla.Lines.Count - 1).Length

        'we'll get the whole update range at once
        'we also get the maximum editor range, incase a new comment goes to the end of the file
        Dim curr As Line = range.StartingLine
        While curr.Number <= range.EndingLine.Number
            [end] += curr.Length
            curr = curr.[Next]
        End While

        StyleSection(scintilla, start, [end], max)
    End Sub

    Public Shared Sub StyleSection(scintilla As Scintilla, start As Integer, [end] As Integer, max As Integer)
        Dim pos As Integer = start
        While pos < [end]
            Dim curr As String = scintilla.GetRange(pos, [end]).Text.ToUpper()

            'make a couple of direct checks for special hanlding (comments/string literals) pass the rest to a RegEx handler
            If (curr.Length > 1) AndAlso (curr(0) = "*"c) Then
                pos += StyleCommentSingle(scintilla, pos, [end], max)
            ElseIf RegHeader.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegHeader, ST_HEADER, pos, [end], max)
            ElseIf (curr(0) = "{"c) Then
                pos += StyleString(scintilla, curr, pos, [end], max)
            ElseIf RegStrVar.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegStrVar, ST_STRING_VAR, pos, [end], max)
            ElseIf RegNumVar.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegNumVar, ST_NUM_VAR, pos, [end], max)
            ElseIf RegLineID.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegLineID, ST_ID, pos, [end], max)
            ElseIf RegNumber.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegNumber, ST_NUMBER, pos, [end], max)
            ElseIf RegWhiteSpace.IsMatch(curr) Then
                pos += StyleRegExWhole(scintilla, curr, RegWhiteSpace, ST_DEFAULT, pos, [end], max)

            Else
                pos += 1
            End If
        End While
    End Sub

    Public Shared Function StyleHeader(scintilla As Scintilla, start As Integer, [end] As Integer, max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 0
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (start + offset < max)
            offset += 1
        End While
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, ST_HEADER)
        Return offset
    End Function
    Public Shared Function StyleCommentSingle(scintilla As Scintilla, start As Integer, [end] As Integer, max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 0
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (start + offset < max)
            offset += 1
        End While
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, ST_COMMENT)
        Return offset
    End Function

    Public Shared Function StyleString(scintilla As Scintilla, text As String, start As Integer, [end] As Integer, max As Integer) As Integer
        Dim full As String = scintilla.GetRange(start, max).Text.ToUpper()
        Dim offset As Integer = 1
        While (full(offset) <> ControlChars.Cr) AndAlso (full(offset) <> ControlChars.Lf) AndAlso (full(offset) <> "}"c) AndAlso (start + offset < max)
            offset += 1
        End While
        offset += 1
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(offset, ST_STRING)
        Return offset
    End Function

    Public Shared Function StyleRegExWhole(scintilla As Scintilla, text As String, reg As Regex, style As Integer, start As Integer, [end] As Integer, _
     max As Integer) As Integer
        'match & style an entire regex
        Dim match As String = reg.Match(text).Value
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(match.Length, style)
        Return match.Length
    End Function

    Public Shared Function StyleRegExSection(scintilla As Scintilla, text As String, reg As Regex, style As Integer, start As Integer, [end] As Integer, _
     max As Integer) As Integer
        'match & style first subgroup of regex
        Dim match As String = reg.Match(text).Groups(1).Value
        DirectCast(scintilla, INativeScintilla).StartStyling(start, &H1F)
        DirectCast(scintilla, INativeScintilla).SetStyling(match.Length, style)
        Return match.Length
    End Function
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
End Class
