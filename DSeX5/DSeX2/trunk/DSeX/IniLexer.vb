#Region "Using Directives"

Imports System.Drawing
Imports ScintillaNET

#End Region


' A helper class to use the Scintilla container as an INI lexer.
' We'll ignore the fact that SciLexer.DLL already has an INI capable lexer. ;)
Friend NotInheritable Class IniLexer
#Region "Constants"

    Private Const EOL As Integer = -1

    ' SciLexer's weird choice for a default style _index
    Private Const DEFAULT_STYLE As Integer = 32

    ' Our custom styles (indexes chosen not to conflict with anything else)
    Private Const KEY_STYLE As Integer = 11
    Private Const VALUE_STYLE As Integer = 12
    Private Const ASSIGNMENT_STYLE As Integer = 13
    Private Const SECTION_STYLE As Integer = 14
    Private Const COMMENT_STYLE As Integer = 15
    Private Const QUOTED_STYLE As Integer = 16

#End Region


#Region "Fields"

    Private _scintilla As Scintilla
    Private _startPos As Integer

    Private _index As Integer
    Private _text As String

#End Region


#Region "Methods"

    Public Shared Sub Init(scintilla As Scintilla)
        If IsNothing(scintilla) Then Exit Sub
        ' Reset any current language and enable the StyleNeeded
        ' event by setting the lexer to container.
        scintilla.Indentation.SmartIndentType = SmartIndent.None
        'scintilla.ConfigurationManager.Language = ""
        scintilla.Lexing.LexerName = "container"
        scintilla.Lexing.Lexer = Lexer.Container

        ' Add our custom styles to the collection
        scintilla.Styles(QUOTED_STYLE).ForeColor = Color.FromArgb(153, 51, 51)
        scintilla.Styles(KEY_STYLE).ForeColor = Color.FromArgb(0, 0, 153)
        scintilla.Styles(ASSIGNMENT_STYLE).ForeColor = Color.OrangeRed
        scintilla.Styles(VALUE_STYLE).ForeColor = Color.FromArgb(102, 0, 102)
        scintilla.Styles(COMMENT_STYLE).ForeColor = Color.FromArgb(102, 102, 102)
        scintilla.Styles(SECTION_STYLE).ForeColor = Color.FromArgb(0, 0, 102)
        scintilla.Styles(SECTION_STYLE).Bold = True
    End Sub


    Private Function Read() As Integer
        If _index < _text.Length Then
            Return AscW(_text(_index))
        End If

        Return EOL
    End Function


    Private Sub SetStyle(style As Integer, length As Integer)
        If length > 0 Then
            ' TODO Still using old API
            ' This will style the _length of chars and advance the style pointer.
            DirectCast(_scintilla, INativeScintilla).SetStyling(length, style)
        End If
    End Sub


    Public Sub Style()
        ' TODO Still using the old API
        ' Signals that we're going to begin styling from this point.
        DirectCast(_scintilla, INativeScintilla).StartStyling(_startPos, &H1F)

        ' Run our humble lexer...
        StyleWhitespace()
        Select Case Read()
            Case AscW("["c)

                ' Section, default, comment
                StyleUntilMatch(SECTION_STYLE, New Char() {"]"c})
                StyleCh(SECTION_STYLE)
                StyleUntilMatch(DEFAULT_STYLE, New Char() {";"c})
                'goto Case ";"C

            Case AscW(";"c)

                ' Comment
                SetStyle(COMMENT_STYLE, _text.Length - _index)

            Case Else


                ' Key, assignment, quote, value, comment
                StyleUntilMatch(KEY_STYLE, New Char() {"="c, ";"c})
                Select Case Read()
                    Case AscW("="c)

                        ' Assignment, quote, value, comment
                        StyleCh(ASSIGNMENT_STYLE)
                        Select Case Read()
                            Case AscW(""""c)

                                ' Quote
                                StyleCh(QUOTED_STYLE)
                                ' '"'
                                StyleUntilMatch(QUOTED_STYLE, New Char() {""""c})

                                ' Make sure it wasn't an escaped quote
                                '                      If _index > 0 AndAlso _index < _text.Length AndAlso _text(_index - 1) = "\"c Then
                                'goto case """"C
                                '                      End If

                                StyleCh(QUOTED_STYLE)
                                ' '"'
                                '		goto case default
                            Case Else


                                ' Value, comment
                                StyleUntilMatch(VALUE_STYLE, New Char() {";"c})
                                SetStyle(COMMENT_STYLE, _text.Length - _index)
                                Exit Select
                        End Select
                        Exit Select
                    Case Else

                        ' ';', EOL
                        ' Comment
                        SetStyle(COMMENT_STYLE, _text.Length - _index)
                        Exit Select
                End Select
                Exit Select
        End Select
    End Sub


    Private Sub StyleCh(style As Integer)
        ' Style just one char and advance
        SetStyle(style, 1)
        _index += 1
    End Sub


    Public Shared Sub StyleNeeded(scintilla As Scintilla, range As Range)
        ' Create an instance of our lexer and bada-bing the line!
        Dim lexer As New IniLexer(scintilla, range.Start, range.StartingLine.Length)
        lexer.Style()
    End Sub


    Private Sub StyleUntilMatch(style As Integer, chars As Char())
        ' Advance until we match a char in the array
        Dim startIndex As Integer = _index
        While _index < _text.Length AndAlso Array.IndexOf(Of Char)(chars, _text(_index)) < 0
            _index += 1
        End While

        If startIndex <> _index Then
            SetStyle(style, _index - startIndex)
        End If
    End Sub


    Private Sub StyleWhitespace()
        ' Advance the _index until non-whitespace character
        Dim startIndex As Integer = _index
        While _index < _text.Length AndAlso [Char].IsWhiteSpace(_text(_index))
            _index += 1
        End While

        SetStyle(DEFAULT_STYLE, _index - startIndex)
    End Sub

#End Region


#Region "Constructors"

    Private Sub New(scintilla As Scintilla, startPos As Integer, length As Integer)
        Me._scintilla = scintilla
        Me._startPos = startPos

        ' One line of _text
        Me._text = scintilla.GetRange(startPos, startPos + length).Text
    End Sub

#End Region
End Class
