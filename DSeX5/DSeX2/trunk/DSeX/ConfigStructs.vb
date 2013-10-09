Imports System.IO
Imports Furcadia.IO

Public Class ConfigStructs


    Public Shared Function pPath() As String
        Dim str As String = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                      "Furcadia Framework/DSeX")
        Directory.CreateDirectory(str)
        Return str
    End Function
    Public Shared SetFile As String = pPath() & "/Settings.Ini"
    Public Shared Function mPath() As String
        mPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Furcadia")
        Directory.CreateDirectory(mPath)
        Return mPath
    End Function

    Public Class EditSettings

        Private _IDcolor As Color
        Private _CommentColor As Color
        Private _StringColor As Color
        Private _NumberColor As Color
        Private _VariableColor As Color
        Private _StringVariableColor As Color
        Private _AutoCompleteEnable As Boolean
        Public Property AutoCompleteEnable
            Get
                Return _AutoCompleteEnable
            End Get
            Set(value)
                _AutoCompleteEnable = value
            End Set
        End Property

        Public Property IDColor() As Color
            Get
                Return _IDcolor
            End Get
            Set(ByVal value As Color)
                _IDcolor = value
            End Set
        End Property
        Public Property StringColor() As Color
            Get
                Return _StringColor
            End Get
            Set(ByVal value As Color)
                _StringColor = value
            End Set
        End Property
        Public Property VariableColor() As Color
            Get
                Return _VariableColor
            End Get
            Set(ByVal value As Color)
                _VariableColor = value
            End Set
        End Property
        Public Property StringVariableColor() As Color
            Get
                Return _StringVariableColor
            End Get
            Set(ByVal value As Color)
                _StringVariableColor = value
            End Set
        End Property
        Public Property NumberColor() As Color
            Get
                Return _NumberColor
            End Get
            Set(ByVal value As Color)
                _NumberColor = value
            End Set
        End Property
        Public Property CommentColor() As Color
            Get
                Return _CommentColor
            End Get
            Set(ByVal value As Color)
                _CommentColor = value
            End Set
        End Property


        Public Sub New()
            ini.AddSection("Editor").AddKey("IDColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("CommentColor").Value = Color.Green.ToArgb
            ini.AddSection("Editor").AddKey("StringColor").Value = Color.Red.ToArgb
            ini.AddSection("Editor").AddKey("VariableColor").Value = Color.DarkGray.ToArgb
            ini.AddSection("Editor").AddKey("StringVariableColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("NumberColor").Value = Color.Brown.ToArgb


            ini.AddSection("Editor").AddKey("AutoComplete").Value = True.ToString

            Dim Count As Integer = KeysIni.GetKeyValue("Init-Types", "Count").ToInteger
            ini.AddSection("Init-Types").AddKey("Count").Value = Count
            ini.AddSection("Init-Types").AddKey("Character").Value = Keysini.GetKeyValue("Init-Types", "Character")
            For i As Integer = 1 To Count
                Dim key As String = KeysIni.GetKeyValue("Init-Types", i.ToString)
                Dim val As String = KeysIni.GetKeyValue("Indent-Lookup", key)
                ini.AddSection("Init-Types").AddKey(i.ToString).Value = key
                ini.AddSection("Indent-Lookup").AddKey(key).Value = val

                Dim dvalue As Integer = KeysIni.GetKeyValue("C-Indents", key)
                ini.AddSection("C-Indents").AddKey(key).Value = dvalue

            Next

            If File.Exists(SetFile) Then
                ini.Load(SetFile, True)
            End If


            _IDcolor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "IDColor"))
            _CommentColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "CommentColor"))
            _StringColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "StringColor"))
            _VariableColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "VariableColor"))
            _StringVariableColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "StringVariableColor"))
            _NumberColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "NumberColor"))

            _AutoCompleteEnable = Convert.ToBoolean(ini.GetKeyValue("Editor", "AutoComplete"))

        End Sub

        Public Sub SaveEditorSettings()


            ini.SetKeyValue("Editor", "IDColor", ColorTranslator.ToHtml(_IDcolor).ToString)
            ini.SetKeyValue("Editor", "NumberColor", ColorTranslator.ToHtml(_NumberColor).ToString)
            ini.SetKeyValue("Editor", "StringColor", ColorTranslator.ToHtml(_StringColor).ToString)
            ini.SetKeyValue("Editor", "VariableColor", ColorTranslator.ToHtml(_VariableColor).ToString)
            ini.SetKeyValue("Editor", "StringVariableColor", ColorTranslator.ToHtml(_StringVariableColor).ToString)
            ini.SetKeyValue("Editor", "CommentColor", ColorTranslator.ToHtml(_CommentColor).ToString)

            ini.SetKeyValue("Editor", "AutoComplete", _AutoCompleteEnable.ToString)

            ini.Save(SetFile)
        End Sub

    End Class


End Class
