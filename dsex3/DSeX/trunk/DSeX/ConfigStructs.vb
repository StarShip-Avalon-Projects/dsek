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
    Public Shared ini As IniFile = New IniFile
    Public Class EditSettings


        Private _ConditionIndent As Integer
        Private _EffectsIndent As Integer
        Private _CauseIndent As Integer
        Private _AreasIndent As Integer
        Private _FiltersIndent As Integer

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
        Public Property AreasIndent As Integer
            Get
                Return _AreasIndent
            End Get
            Set(value As Integer)
                _AreasIndent = value
            End Set
        End Property
        Public Property FiltersIndent As Integer
            Get
                Return _FiltersIndent
            End Get
            Set(value As Integer)
                _FiltersIndent = value
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
            ini.AddSection("Editor").AddKey("VariableColor").Value = Color.Cyan.ToArgb
            ini.AddSection("Editor").AddKey("StringVariableColor").Value = Color.Blue.ToArgb
            ini.AddSection("Editor").AddKey("NumberColor").Value = Color.Tan.ToArgb
            ini.AddSection("Editor").AddKey("AutoComplete").Value = True.ToString

            ini.AddSection("Editor").AddKey("Condition").Value = 4
            ini.AddSection("Editor").AddKey("Effect").Value = 12
            ini.AddSection("Editor").AddKey("Cause").Value = 0
            ini.AddSection("Editor").AddKey("Area").Value = 8
            ini.AddSection("Editor").AddKey("Filter").Value = 6


            If File.Exists(SetFile) Then
                ini.Load(SetFile, True)
            Else
                ini.Save(SetFile)
            End If
            _ConditionIndent = ini.GetKeyValue("Editor", "Condition").ToInteger
            _EffectsIndent = ini.GetKeyValue("Editor", "Effect").ToInteger
            _CauseIndent = ini.GetKeyValue("Editor", "Cause").ToInteger
            _FiltersIndent = ini.GetKeyValue("Editor", "Filter").ToInteger
            _AreasIndent = ini.GetKeyValue("Editor", "Area").ToInteger

            _IDcolor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "IDColor"))
            _CommentColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "CommentColor"))
            _StringColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "StringColor"))
            _VariableColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "VariableColor"))
            _StringVariableColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "StringVariableColor"))
            _NumberColor = ColorTranslator.FromHtml(ini.GetKeyValue("Editor", "NumberColor"))

            _AutoCompleteEnable = Convert.ToBoolean(ini.GetKeyValue("Editor", "AutoComplete"))

        End Sub

        Public Sub SaveEditorSettings()
            ini.SetKeyValue("Editor", "Condition", _ConditionIndent.ToString)
            ini.SetKeyValue("Editor", "Effect", _EffectsIndent.ToString)
            ini.SetKeyValue("Editor", "Cause", _CauseIndent.ToString)
            ini.SetKeyValue("Editor", "Filter", _FiltersIndent.ToString)
            ini.SetKeyValue("Editor", "Area", _AreasIndent.ToString)

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
