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

        If Not Directory.Exists(mPath) Then

            Directory.CreateDirectory(mPath)
        End If
        Return mPath
    End Function
    Public Shared ini As IniFile = New IniFile
    Public Structure EditSettings


        Private Shared _ConditionIndent As Integer
        Private Shared _EffectsIndent As Integer
        Private Shared _CauseIndent As Integer

        Private Shared _IDcolor As Color = Color.Blue
        Private Shared _CommentColor As Color = Color.Green
        Private Shared _StringColor As Color = Color.DarkCyan
        Private Shared _NumberColor As Color = Color.Violet
        Private Shared _VariableColor As Color = Color.Tan
        Private Shared _AutoCompleteEnable As Boolean = True
        Public Property AutoCompleteEnable
            Get
                Return _AutoCompleteEnable
            End Get
            Set(value)
                _AutoCompleteEnable = value
            End Set
        End Property

        Public Property ConditionIndent As Integer
            Get
                Return _ConditionIndent
            End Get
            Set(value As Integer)
                _ConditionIndent = value
            End Set
        End Property

        Public Property EffectsIndent As Integer
            Get
                Return _EffectsIndent
            End Get
            Set(value As Integer)
                _EffectsIndent = value
            End Set
        End Property

        Public Property CauseIndent As Integer
            Get
                Return _CauseIndent
            End Get
            Set(value As Integer)
                _CauseIndent = value
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

        Public Sub LoadEditorSettings()
            ini.AddSection("Editor").AddKey("IDColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("CommentColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("StringColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("VariableColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("NumberColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("AutoComplete").Value = True.ToString
            If File.Exists(SetFile) Then
                ini.Load(SetFile, True)
            Else
                ini.Save(SetFile)
            End If
            _IDcolor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "IDColor"))
            _CommentColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "CommentColor"))
            _StringColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "StringColor"))
            _VariableColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "VariableColor"))
            _NumberColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "NumberColor"))
            _AutoCompleteEnable = Convert.ToBoolean(ini.GetKeyValue("Editor", "AutoComplete"))

        End Sub

        Public Sub SaveEditorSettings()
            ini.SetKeyValue("Editor", "ConIndent", _ConditionIndent.ToString)
            ini.SetKeyValue("Editor", "EffectIndent", _EffectsIndent.ToString)
            ini.SetKeyValue("Editor", "CauseIndent", _CauseIndent.ToString)
            ini.SetKeyValue("Editor", "IDColor", ColorTranslator.ToHtml(_IDcolor).ToString)
            ini.SetKeyValue("Editor", "NumberColor", ColorTranslator.ToHtml(_NumberColor).ToString)
            ini.SetKeyValue("Editor", "StringColor", ColorTranslator.ToHtml(_StringColor).ToString)
            ini.SetKeyValue("Editor", "VariableColor", ColorTranslator.ToHtml(_VariableColor).ToString)
            ini.SetKeyValue("Editor", "CommentColor", ColorTranslator.ToHtml(_CommentColor).ToString)
            ini.SetKeyValue("Editor", "AutoComplete", _AutoCompleteEnable.ToString)
            ini.Save(SetFile)
        End Sub

    End Structure


End Class
