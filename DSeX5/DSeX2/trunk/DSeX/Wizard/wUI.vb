﻿
Imports System.Text.RegularExpressions
Imports DSeX.ConfigStructs
Imports DSeX.IniFile
Imports System.IO
Imports FastColoredTextBoxNS

Public Module MyExtensions
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsInteger(ByVal value As String) As Boolean
        If String.IsNullOrEmpty(value) Then
            Return False
        Else
            Return Integer.TryParse(value, Nothing)
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToInteger(ByVal value As String) As Integer
        If value.IsInteger() Then
            Return Integer.Parse(value)
        Else
            Return 0
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsDouble(ByVal value As String) As Boolean
        If String.IsNullOrEmpty(value) Then
            Return False
        Else
            Return Double.TryParse(value, Nothing)
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToDouble(ByVal value As String) As Integer
        If value.IsDouble() Then
            Return Double.Parse(value)
        Else
            Return 0
        End If
    End Function
End Module

Public Class wUI
#Region "Properties"
    'Dim ScriptPath = My.Application.Info.DirectoryPath() & "\Scripts\"
    Public Code As String
    Dim WorkFileName As String = ""
    Dim WorkPath As String = ""
    Public PathIndex As Integer = 0



    Public Structure StructMapSearch
        Public DreamPath As String
        Public Item As String
        Private _MSType As String
        Private _msValue As String
        Public Property MSType As String
            Get
                Return _MSType
            End Get

            Set(value As String)
                Try
                    Select Case value.ToLower
                        Case "object"
                            _msValue = "0"
                        Case "floor"
                            _msValue = "f"
                        Case "wall"
                            _msValue = "w"
                        Case "effect"
                            _msValue = "e"
                        Case "region"
                            _msValue = "r"
                        Case Else
                            Throw New NotImplementedException
                    End Select
                Catch ex As NotImplementedException
                    MsgBox(MsgBoxStyle.OkOnly, "Not Implemented Exception", value + " Is invalid, please try again")
                Catch ex As Exception

                End Try

                _MSType = value
            End Set
        End Property
        Public ReadOnly Property MSValue
            Get
                Return _msValue
            End Get
        End Property
        Private _list As List(Of String)
        Public ReadOnly Property Getlist
            Get
                Return _list
            End Get
        End Property
        Public WriteOnly Property Setlist
            Set(value)
                _list = value
            End Set
        End Property
    End Structure

    Public wVariables As Dictionary(Of Integer, Object) = New Dictionary(Of Integer, Object)

#End Region

#Region "Position Functions"

    'Regexes for calculation parsing
    Private Const RGEX_Movement As String = "(\d+)(nw|ne|sw|se)(\d+)(.*)"""
    Private Const RGEX_MathCalc As String = "(\d+)(\+|-|\*|/)(\d+)(.*)"""
    Private Const RGEX_MathStep As String = "(\+|-|\*|/)(\d+)"
    Private Const RGEX_Coordinate As String = "(\d+)\s*,\s*(\d+)"
    Private Const RGEX_Number As String = "^(\d+)"
    Private Const RGEX_Mov_Steps As String = "(nw|ne|sw|se)(\d+)"
    Private Const RGEX_Range As String = "^(\d+)(-\s*)(\d+)?"
    Private Const RPOS_Range_Start As Integer = 1
    Private Const RPOS_Range_Marker As Integer = 2
    Private Const RPOS_Range_End As Integer = 3

    'Regular Expression match indexes
    'RPOS_Movement_Var = 1;
    'RPOS_Movement_Dir = 2;
    'RPOS_Movement_Dist = 3;
    'RPOS_Movement_More = 4;
    'RPOS_Mov_Steps_Dir = 1;
    'RPOS_Mov_Steps_Dist = 2;
    'RPOS_MathCalc_Var = 1;
    'RPOS_MathCalc_Op = 2;
    'RPOS_MathCalc_Val = 3;
    'RPOS_MathCalc_More = 4;
    'RPOS_MathStep_Op = 1;
    'RPOS_MathStep_Num = 2;
    'RPOS_Number_Num = 1;
    'RPOS_Coord_X = 1;
    'RPOS_Coord_Y = 2;

    Private Function MoveCoord(ByRef variable As String, ByRef directions As String) As String
        'Match Cords
        Dim x As Integer = Regex.Match(variable, RGEX_Coordinate).Groups(0).Value
        Dim y As Integer = Regex.Match(variable, RGEX_Coordinate).Groups(1).Value

        Dim m As MatchCollection = Regex.Matches(directions, RGEX_Mov_Steps, RegexOptions.IgnoreCase)


        For Each s In m
            Dim spaces As Integer = s.groups(2)

            Select Case s.groups(1).ToLower
                Case "nw"
                    For i As Integer = 0 To spaces - 1
                        If IsOdd(y) Then
                            x -= 2
                            y -= 1
                        Else
                            y -= 1
                        End If
                    Next

                Case "ne"
                    For i As Integer = 0 To spaces - 1
                        If IsOdd(y) Then
                            y = -1
                        Else
                            x += 2
                            y -= 1
                        End If
                    Next

                Case "se"
                    For i As Integer = 0 To spaces - 1
                        If IsOdd(y) Then
                            y += 1
                        Else
                            x += 2
                            y += 1
                        End If
                    Next

                Case "sw"
                    For i As Integer = 0 To spaces - 1
                        If IsOdd(y) Then
                            x -= 2
                            y += 1
                        Else
                            y += 1
                        End If
                    Next

                Case Else
            End Select
        Next
        Return x.ToString = "," + y.ToString
    End Function

    Private Function CalcMath(ByRef variable As String, ByRef directions As String) As String
        'Match Cords
        Dim x As Integer = variable


        Dim m As MatchCollection = Regex.Matches(directions, RGEX_MathStep, RegexOptions.IgnoreCase)


        For Each s In m
            Dim spaces As Integer = s.groups(2)

            Select Case s.groups(1).ToLower
                Case "+"
                    x += spaces

                Case "-"
                    x -= spaces

                Case "/"
                    x /= spaces

                Case "*"
                    x *= spaces

                Case Else
            End Select
        Next
        Return x.ToString
    End Function

#End Region

    'Main (or second main) form loads!
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If wMain.OnToolStripMenuItem.Checked = True And wMain.OnToolStripMenuItem.CheckState = CheckState.Checked Then
            MyBase.Opacity = 0.0
            Timer1.Enabled = True
        End If

        selecter2.SelectedIndex = 0
        Dim n As Integer = selecter2.SelectedIndex + 1
        Dim s As String = ScriptIni.GetKeyValue("main", "b" + n.ToString)
        If s <> "" Then TextBox1.Text = s

    End Sub


    Private Sub Form2_OnExit(ByVal sender As System.Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If wMain.OnToolStripMenuItem.Checked = True Then
            Timer1.Enabled = False
            Timer.Enabled = False
        End If

    End Sub

    Private Sub selecter2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selecter2.SelectedIndexChanged, selecter2.Click
        Dim s = selecter2.GetItemText(selecter2.SelectedItem)
        Dim Space As Integer = selecter2.SelectedIndex + 1
        Dim t As String = ScriptIni.GetKeyValue("main", "t" + Space.ToString)
        If t <> "" Then ToolTip.SetToolTip(selecter2, t)

        SetUI()
    End Sub

    'Fade effect Timer (fade-in)
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.01
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then Me.Dispose()
    End Sub

    'Fade effect Timer1 (fade-out)
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity += 0.01
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 100 Then
            Me.Show()
        End If
    End Sub

    Private Sub generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles generate.Click

        NextItem()

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            NextItem()
            e.Handled = True
        End If
    End Sub

    Private Sub NextItem()

        Dim n As Integer = selecter2.SelectedIndex + 1
        Dim t As String = "text"
        Dim s As String = ScriptIni.GetKeyValue("main", "m" + n.ToString).ToLower
        If s <> Nothing And s <> "" Then t = s
        Select Case t

            Case "mapsearch"
                Dim m As New StructMapSearch
                m.DreamPath = TextBox3.Text
                m.Item = TextBox1.Text
                m.MSType = ComboBox1.SelectedItem
                If wVariables.ContainsKey(n) Then
                    wVariables.Item(n) = m
                Else
                    wVariables.Add(n, m)
                End If
                Dim file As String = ""
                If WorkFileName <> "" Then file = WorkFileName.Remove(WorkFileName - 3, 3) + ".map"
                TextBox3.Text = file
                TextBox1.Text = ""
                ComboBox1.SelectedItem = "Object"
            Case Else
                If wVariables.ContainsKey(n) Then
                    wVariables.Item(n) = TextBox1.Text
                Else
                    wVariables.Add(n, TextBox1.Text)
                End If
                TextBox1.Text = ""
        End Select
        If selecter2.SelectedIndex + 1 <> selecter2.Items.Count() Then
            selecter2.SelectedIndex = selecter2.SelectedIndex + 1
            SetUI()
        Else
            generate.Enabled = False
        End If
    End Sub

    Public Sub SetUI()
        Dim n As Integer = selecter2.SelectedIndex + 1
        If selecter2.SelectedIndex <> selecter2.Items.Count() Then
            generate.Enabled = True
            Dim t As String = "text"
            Dim s As String = ScriptIni.GetKeyValue("main", "m" + n.ToString)
            Dim DefaultStr As String = ScriptIni.GetKeyValue("main", "b" + n.ToString)
            If s <> "" Then t = s
            Select Case t

                Case "mapsearch"
                    TextBox3.Visible = True
                    TextBox1.Visible = True
                    ComboBox1.Visible = True
                    Label1.Visible = True
                    Label5.Visible = True

                    If DefaultStr <> "" Then
                        'TextBox1.Text = DefaultStr
                        'TextBox3.Text = ""
                    ElseIf wVariables.ContainsKey(n) Then

                        'TextBox1.Text = wVariables.Item(n)
                    End If
                Case Else
                    'everything else process as text
                    TextBox1.Visible = True
                    TextBox3.Visible = False
                    ComboBox1.Visible = False
                    Label1.Visible = False
                    Label5.Visible = False
                    If DefaultStr <> "" Then
                        TextBox1.Text = DefaultStr
                    ElseIf wVariables.ContainsKey(n) Then
                        TextBox1.Text = wVariables.Item(n)
                    Else
                        TextBox1.Text = ""
                    End If
            End Select
        Else
            generate.Enabled = False
        End If
    End Sub

    Private Sub ProcessVariableList()

        Dim VariableList As List(Of List(Of String)) = New List(Of List(Of String))
        Dim Counters As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)
        Dim it1, mini, maxi As New Integer
        Dim tname As String = ""
        'parser : TRegexpr;
        Dim bounded As Boolean = False
        mini = 0
        maxi = 0
        For I = 0 To NumericUpDown1.Value - 1
            Dim VarList As List(Of String) = New List(Of String)
            For t = 1 To wVariables.Count

                Dim Str As String = ""
                Dim type = wVariables(t).GetType()
                If type Is GetType(System.String) Then


                    Dim regex As Regex = New Regex(RGEX_Range)
                    Dim match As Match = regex.Match(wVariables.Item(t))
                    If match.Success Then
                        If Counters.ContainsKey(t) Then
                            it1 = Counters.Item(t)
                            bounded = False

                            If match.Groups(RPOS_Range_End).Value <> "" Then
                                mini = match.Groups(RPOS_Range_Start).Value
                                maxi = match.Groups(RPOS_Range_End).Value
                                bounded = True
                            End If

                            If bounded Then
                                If mini > maxi Then
                                    it1 -= 1
                                    If it1 < maxi Then
                                        it1 = mini
                                    End If
                                Else
                                    it1 += 1
                                    If it1 > maxi Then
                                        it1 = mini
                                    End If
                                End If
                            Else
                                it1 += 1 'Unbounded, assume +
                            End If
                            Counters.Item(t) = it1
                        Else
                            it1 = match.Groups(RPOS_Range_Start).Value
                            Counters.Add(t, it1)
                        End If
                        Str = it1.ToString
                    Else
                        Str = wVariables.Item(t)
                    End If
                ElseIf type Is GetType(StructMapSearch) Then
                    Dim mlist As List(Of String) = New List(Of String)
                    Do While wVariables.Item(t).Getlist.count <= NumericUpDown1.Value - 1
                        Dim start_info As New ProcessStartInfo("mapsearch.exe")
                        start_info.UseShellExecute = False
                        start_info.CreateNoWindow = True
                        start_info.RedirectStandardOutput = True
                        start_info.RedirectStandardError = True
                        start_info.WorkingDirectory = WorkPath
                        'Parameters: [/ns] [/n #] [/f,/o,/w,/r,/e ##] <filename>
                        start_info.Arguments = "/ns /n " + NumericUpDown1.Value.ToString + " / " + _
                            wVariables.Item(t).MSValue + " " + wVariables.Item(t).Item + " " + _
                            wVariables.Item(t).DreamPath
                        ' Make the process and set its start information.
                        Dim proc As New Process()
                        proc.StartInfo = start_info

                        ' Start the process.
                        proc.Start()

                        ' Attach to stdout and stderr.
                        Dim std_out As StreamReader = proc.StandardOutput()
                        'Dim std_err As StreamReader = proc.StandardError()
                        Dim test As List(Of String) = New List(Of String)
                        Do While std_out.Peek <> -1
                            ' Display the results.
                            test.Add(std_out.ReadLine)
                        Loop

                        'txtStderr.Text = std_err.ReadToEnd()

                        ' Clean up.
                        std_out.Close()
                        'std_err.Close()
                        proc.Close()
                        If mlist.Count <= NumericUpDown1.Value Then
                            mlist.AddRange(test)
                        End If
                        If wVariables.Item(t).Getlist.count >= NumericUpDown1.Value Then
                            wVariables.Item(t).setlist.AddRange(mlist)
                        End If
                    Loop
                    Str = wVariables.Item(t).getlist(t)

                End If
                VarList.Add(Str)
            Next
            VariableList.Add(VarList)
        Next
        ProcessIterations(VariableList)
    End Sub


    Private Sub ProcessIterations(ByRef Values As List(Of List(Of String)))
        Solution.Text = ""
        For i As Integer = 0 To NumericUpDown1.Value - 1
            Dim template As String = Code
            For t = 1 To Values(i).Count
                Dim str As String = Values(i)(t - 1)
                template = Regex.Replace(template, "\^" & t.ToString & "\^", str)
                Dim m As MatchCollection = Regex.Matches(template, "\^" + RGEX_Movement + "\^", RegexOptions.IgnoreCase)
                For Each s In m
                    Dim List As String = s.groups(2).value + s.groups(3).value + s.groups(4).value
                    template = Regex.Replace(template, "\^" & s.groups(0) & "\^", MoveCoord(str, List), RegexOptions.IgnoreCase)
                Next
                m = Regex.Matches(template, "\^" + RGEX_MathCalc + "\^", RegexOptions.IgnoreCase)
                For Each s In m
                    Dim List As String = s.groups(2).value + s.groups(3).value + s.groups(4).value
                    template = Regex.Replace(template, "\^" & s.groups(0) & "\^", CalcMath(str, List), RegexOptions.IgnoreCase)
                Next
            Next
            Solution.AppendText(template + vbLf)
        Next

    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        If wMain.OnToolStripMenuItem.Checked = True Then
            Timer1.Enabled = False
            Timer.Enabled = True
        Else
            Me.Dispose()
        End If
    End Sub

    Private Sub ViewFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFileToolStripMenuItem.Click
        If IO.File.Exists(ScriptPaths(PathIndex) & Me.Text()) Then
            Try
                System.Diagnostics.Process.Start(ScriptPaths(PathIndex) & Me.Text())

            Catch
                MsgBox("Error while opening file.  File might not exist.")
            End Try
        End If

    End Sub

    Private Sub ReloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadToolStripMenuItem.Click
        Solution.Text = ""
        selecter2.Items.Clear()
        wMain.GetParams(ScriptPaths(PathIndex) & Me.Text())
        selecter2.SelectedIndex = 0
        SetUI()
        Dim n As Integer = selecter2.SelectedIndex + 1
        Dim s As String = ScriptIni.GetKeyValue("main", "b" + n.ToString)
        If s <> "" Then TextBox1.Text = s
        wVariables.Clear()
        s = ScriptIni.GetKeyValue("main", "DefaultRepeat")
        If s <> "" Then NumericUpDown1.Value = s.ToInteger Else NumericUpDown1.Value = 1
    End Sub

    Private Sub MS_Editor_TextChangedDelayed(sender As Object, e As TextChangedEventArgs) Handles Solution.TextChanged
        sender.CommentPrefix = "*"
        'clear style of changed range
        e.ChangedRange.ClearStyle(StyleIndex.All)

        'comment highlighting
        'e.ChangedRange.SetStyle(DS_Comment_Style, "^\*([^\n]*)")
        e.ChangedRange.SetStyle(DS_Comment_Style, "^\*(.*)$", RegexOptions.Multiline)

        'Line ID highlighting
        e.ChangedRange.SetStyle(DS_Line_ID_Style, "(\([0-9#]+):[0-9]+\)")
        'number Variable highlighting
        e.ChangedRange.SetStyle(DS_Num_Var_Style, "%([A-Za-z0-9_]+)")
        'number Variable highlighting
        e.ChangedRange.SetStyle(DS_Str_Var_Style, "~([A-Za-z0-9_]+)")

        'string highlighting
        e.ChangedRange.SetStyle(DS_String_Style, "\{.*?\}")
        'number highlighting
        e.ChangedRange.SetStyle(DS_Num_Style, "([0-9#]+)")
        'clear folding markers
        ' sender.Range.ClearFoldingMarkers()


    End Sub




    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ProcessVariableList()
    End Sub

    Private Sub BtnImport_Click(sender As System.Object, e As System.EventArgs) Handles BtnImport.Click
        If IsNothing(MS_Edit.MS_Editor) Then Exit Sub
        MS_Edit.MS_Editor.InsertText(Solution.Text)
    End Sub


End Class