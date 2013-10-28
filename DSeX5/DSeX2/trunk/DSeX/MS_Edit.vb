Imports System.Text.RegularExpressions
Imports System
Imports DSeX.ConfigStructs
Imports System.IO
Imports Furcadia.IO
Imports DSeX.IniFile
Imports System.Text
Imports FastColoredTextBoxNS
Imports System.Runtime.InteropServices

Public Class MS_Edit
#Region "Sorters"
    Class CatSorter
        Implements IComparer(Of String)

        Public Function Compare(ByVal item1 As String, ByVal item2 As String) As Integer Implements IComparer(Of String).Compare

            Dim cat As New Regex("\((.[0-9]*?)\:(.[0-9]*?)\)")
            Dim num1 As Integer = Int(cat.Match(item1).Groups(1).ToString)
            Dim num2 As Integer = Int(cat.Match(item2).Groups(1).ToString)
            Dim num3 As Integer = Int(cat.Match(item1).Groups(2).ToString)
            Dim num4 As Integer = Int(cat.Match(item2).Groups(2).ToString)


            If num3 > num4 Then
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return 1
            ElseIf num3 < num4 Then
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return -1
            Else
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return 0
            End If
        End Function

    End Class

    Class MyCustomSorter

        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare

            Dim item1 As ListViewItem = CType(x, ListViewItem)
            Dim item2 As ListViewItem = CType(y, ListViewItem)

            Dim cat As New Regex("\((.[0-9]*?)\:(.[0-9]*?)\)")
            Dim num1 As Integer = Int(cat.Match(item1.SubItems(0).Text).Groups(1).ToString)
            Dim num2 As Integer = Int(cat.Match(item2.SubItems(0).Text).Groups(1).ToString)
            Dim num3 As Integer = Int(cat.Match(item1.SubItems(0).Text).Groups(2).ToString)
            Dim num4 As Integer = Int(cat.Match(item2.SubItems(0).Text).Groups(2).ToString)

            If num3 > num4 Then
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return 1
            ElseIf num3 < num4 Then
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return -1
            Else
                If num1 > num2 Then Return 1
                If num1 < num2 Then Return -1
                Return 0
            End If

            Return 0

        End Function

    End Class
#End Region
    Public autoCompleteList As New List(Of AutocompleteItem)
    Private Class DA_AUtoCompleteMenu
        Inherits AutocompleteItem

        Public Sub New(snippet As String)
            MyBase.New(snippet)
        End Sub

        Public Shared RegexSpecSymbolsPattern As String = "[ \.\(\)]"
        '[\ \^\$\\(\)\.\\\*\+\|\?]
        ''' <summary>
        ''' Compares fragment text with this item
        ''' </summary>
        Public Overrides Function Compare(fragmentText As String) As CompareResult
            fragmentText = fragmentText.Trim
            Dim pattern = Regex.Replace(fragmentText, RegexSpecSymbolsPattern, "$0").Trim
            If Regex.IsMatch(Text, pattern, RegexOptions.IgnoreCase) Then
                If Regex.IsMatch(Text.Trim, "\{" & fragmentText & "\}?", RegexOptions.IgnoreCase) Then
                    Return CompareResult.Hidden
                End If
                Return CompareResult.Visible
            End If
            Return CompareResult.Hidden
        End Function


    End Class

#Region "Properties"
    Public Enum EditStyles
        none = 0
        ini
        ds
        ms
    End Enum
    Public TabEditStyles As List(Of EditStyles) = New List(Of EditStyles)
    Public CanOpen As List(Of Boolean) = New List(Of Boolean)
    ' Public SettingsChanged As List(Of Boolean) = New List(Of Boolean)
    Public WorkFileName As List(Of String) = New List(Of String)
    Public WorkPath As List(Of String) = New List(Of String)

    Private objMutex As System.Threading.Mutex

    Private Shared DS_HEADER As String = ""
    Private Shared DS_FOOTER As String = ""
    'mPath()
    Dim frmTitle As List(Of String) = New List(Of String)
    'Dim lastTab As Integer = 0
    Dim Flag As Boolean = False
    Private Const RES_SEC_Marker As String = "**SECTION**  "
    Private Const RES_DS_begin As String = "DSPK V"
    Private Const RES_DS_end As String = "*Endtriggers* 8888 *Endtriggers*"
    Private Const RES_DSS_begin As String = "DS-START"
    Private Const RES_DSS_End As String = "DS-END"
    Private Const RES_Def_section As String = "Default Section"
    Private Const RES_DSS_All As String = "Entire Document"
    Private Const My_Docs As String = "/Furcadia"
    Dim FullFile As List(Of List(Of String)) = New List(Of List(Of String))
    Dim SectionIdx As List(Of Integer) = New List(Of Integer)
    Private Const New_File_Tag As String = "(New File)"
    Private charsToTrim() As Char = {vbCr, vbLf}
    Enum TSecType
        SecNormal
        SecEnd
        SecFixed
        SecDefault
    End Enum

    Public Class TDSSegment
        Public Property Title As String
        Public Property lines As List(Of String) = New List(Of String)
        Public Property SecType As TSecType
        Sub New()
            Title = ""
            lines.Clear()
            SecType = TSecType.SecDefault
        End Sub
    End Class
    Dim SectionChange As Boolean = False
    Private TabSections As List(Of List(Of TDSSegment)) = New List(Of List(Of TDSSegment))

    Public TemplatePaths As List(Of String) = New List(Of String)

    Private AutoCompleteMenu1 As AutocompleteMenu

    Public Function FindControl(parent As Control, ident As String) As Control
        Dim control As Control = New Control
        For Each child As Control In parent.Controls
            If child.Name.Contains(ident) Then
                control = child
                Exit For
            End If
        Next
        Return control
    End Function

    Public Function MS_Editor() As FastColoredTextBox
        If TabControl2.TabCount = 0 Then Return Nothing
        Return MS_Editor(TabControl2.SelectedIndex)
    End Function

    Public Function MS_Editor(i As Integer) As FastColoredTextBox
        If TabControl2.TabCount < i Then Return Nothing
        Return FindControl(TabControl2.TabPages.Item(i), "edit")
    End Function '+ TabControl2.SelectedIndex.ToString


#End Region

#Region "WmCpyDta"
#If DEBUG Then
    Const WmCpyDta As String = "WmCpyDta_d.dll"
#Else
    Const WmCpyDta As String = "WmCpyDta.dll"
#End If


    Const WM_COPYDATA As Integer = &H4A
    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_MaxNameLen")> _
    Private Shared Function WmCpyDta_MaxNameLen() As Integer
    End Function
    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_MaxTagLen")> _
    Private Shared Function WmCpyDta_MaxTagLen() As Integer
    End Function

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_MaxDataLen")> _
    Private Shared Function WmCpyDta_MaxDataLen() As Integer
    End Function

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_GetMessage_sTagData")> _
    Private Shared Function WmCpyDta_GetMessage_sTagData(hReceiver As Integer, hSender As Integer, lParam As Integer, lpName As StringBuilder, lpFID As UInteger, lpTag As StringBuilder, lpData As StringBuilder) As Boolean
    End Function

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_SetEncrypt")> _
    Private Shared Sub WmCpyDta_SetEncrypt(c As Char)
    End Sub

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_BaseDefaultMsgId")> _
    Private Shared Function WmCpyDta_BaseDefaultMsgId() As Integer
    End Function

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_SetMessageId")> _
    Private Shared Sub WmCpyDta_SetMessageId(iMsgId As Integer)
    End Sub

    <DllImport("user32.dll", EntryPoint:="FindWindow")> _
    Private Shared Function FindWindow(_ClassName As String, _WindowName As String) As Integer
    End Function
    Public Declare Function SetFocusAPI Lib "user32.dll" Alias "SetFocus" (ByVal hWnd As Long) As Long
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Long) As Long



    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_GetMessageId")> _
    Private Shared Function WmCpyDta_GetMessageId() As Integer
    End Function

    <DllImport(WmCpyDta, EntryPoint:="WmCpyDta_SendMessage_sTagData")> _
    Private Shared Function WmCpyDta_SendMessage_sTagData(hReceiver As Integer, hSender As Integer, _strName As String, _intFID As UInteger, _strTag As String, _strData As String) As Integer
    End Function

    Public Function FindProcessByName(strProcessName As String) As IntPtr
        Dim HandleOfToProcess As IntPtr = IntPtr.Zero
        Dim MyProcess As Process = Process.GetCurrentProcess()
        Dim p As Process() = Process.GetProcessesByName("DSeX")
        For Each p1 As Process In p
            Debug.WriteLine(p1.ProcessName.ToUpper())
            If p1.ProcessName.ToUpper() = strProcessName.ToUpper() And p1.Id <> MyProcess.Id Then
                HandleOfToProcess = p1.MainWindowHandle
                Exit For
            End If
        Next
        Return HandleOfToProcess
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_COPYDATA Then
            Dim bOurMessage As Boolean = False

            'Option 1 - Encryption
            'To make a message a little more difficult to hack we can make it look like a bad piece of memory.
            'The sender must also set the same value.
            WmCpyDta_SetEncrypt("d"c)
            ' 'd' is a bitwise seed value. I like to use d because it
            'makes the message look like bad memory
            'Option 2
            '				//If your receiver wants to conditional handle the received message
            '				//then define new ids based on the default.
            '				//This example does not need this so we will define a few but not use them.
            '				//Look for more comments on wCustomMsgId_1, and wCustomMsgId_2 below.
            '				WPARAM wCustomMsgId_1 = WmCpyDta_BaseDefaultMsgId() + 1;
            '				WPARAM wCustomMsgId_2 = WmCpyDta_BaseDefaultMsgId() + 2;
            '					If some condition then
            '					WmCpyDta_SetMessageId(wCustomMsgId_1);
            '					else some other conditin WmCpyDta_SetMessageId(wCustomMsgId_2);
            '				


            WmCpyDta_SetMessageId(WmCpyDta_BaseDefaultMsgId())
            'define for default behavior


            Dim sName As New StringBuilder(WmCpyDta_MaxNameLen())
            Dim sFID As UInteger = 0
            Dim sTag As New StringBuilder(WmCpyDta_MaxTagLen())
            Dim sData As New StringBuilder(WmCpyDta_MaxDataLen())

            'Sample Data

            'sName = ~DSEX~
            'sFID = 0 "n/a"
            'sTag = -b=BotName
            'sData = "path/Filename.ms"

            'sName = ~DSEX~
            'sFID = 0 "n/a"
            'sTag = 
            'sData = "path/Filename.ms"

            bOurMessage = WmCpyDta_GetMessage_sTagData(0, 0, m.LParam.ToInt32(), sName, sFID, sTag, sData)
            If bOurMessage Then

                Dim bName As String = "none"
                If sTag.ToString.StartsWith("-b=") Then
                    bName = sTag.ToString.Substring(3)
                    'store Botname to List at NewTab index
                End If


                OpenMS_File(sData.ToString, bName)

            End If

            If bOurMessage = True Then
                Return
            End If
        End If


        MyBase.WndProc(m)

    End Sub



#End Region

#Region "Event Handlers"
    Delegate Sub FileSave(ByRef Idx As Integer)
#End Region

    Private Sub GetTemplates()
        Dim path As String = Application.StartupPath + "/Templates"
        Directory.CreateDirectory(path)
        TemplatePaths.Clear()
        ListBox2.Items.Clear()
        Dim x As Integer = 0
        ListBox2.BeginUpdate()
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ds").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            ListBox2.Items.Add(s.Remove(s.Length - 3, 3))
            TemplatePaths.Add(path)
        Next
        path = Furcadia.IO.Paths.GetFurcadiaDocPath + "/Templates"
        'path = Enviroment.GetFolderPath(Enviroment.SpecialFolderMyDocuments) + My_Docs + "/templates"
        Directory.CreateDirectory(path)
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ds").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            ListBox2.Items.Add(s.Remove(s.Length - 3, 3))
            TemplatePaths.Add(path)
        Next
        ListBox2.EndUpdate()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MS_Edit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For i = TabControl2.TabPages.Count - 1 To 0 Step -1
            If Not CanOpen(i) Then
                Dim fname As String = WorkFileName(i)
                If fname = "" Then
                    fname = New_File_Tag
                End If
                Dim result = MessageBox.Show(fname + " was modified." + Environment.NewLine + "Save your work before closing?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If result = DialogResult.Cancel Then
                    e.Cancel = True
                    CanOpen(i) = False
                    Exit Sub
                ElseIf result = DialogResult.No Then
                    Try
                        TabControl2.TabPages.RemoveAt(i)
                        CanOpen.RemoveAt(i)
                        WorkFileName.RemoveAt(i)
                        WorkPath.RemoveAt(i)
                        frmTitle.RemoveAt(i)
                        SectionIdx.RemoveAt(i)
                        TabEditStyles.RemoveAt(i)

                        '  MsgBox(e.CloseReason.ToString)
                    Catch eX As Exception
                        Dim logError As New ErrorLogging(eX, Me)
                    End Try
                ElseIf result = DialogResult.Yes Then
                    SaveMS_File(i)
                    Try
                        TabControl2.TabPages.RemoveAt(i)
                        CanOpen.RemoveAt(i)
                        WorkFileName.RemoveAt(i)
                        WorkPath.RemoveAt(i)
                        frmTitle.RemoveAt(i)
                        SectionIdx.RemoveAt(i)
                        TabEditStyles.RemoveAt(i)
                    Catch eX As Exception
                        Dim logError As New ErrorLogging(eX, Me)
                    End Try
                End If
            Else
                Try
                    TabControl2.TabPages.RemoveAt(i)
                    CanOpen.RemoveAt(i)
                    WorkFileName.RemoveAt(i)
                    WorkPath.RemoveAt(i)
                    frmTitle.RemoveAt(i)
                    SectionIdx.RemoveAt(i)
                    TabEditStyles.RemoveAt(i)
                Catch eX As Exception
                    Dim logError As New ErrorLogging(eX, Me)
                End Try
            End If
        Next
        'Set my user setting MainFormLocation to
        'the current form's location
        EditSettings.SaveEditorSettings()
        My.Settings.EditFormLocation = Me.Location
        My.Settings.Save()
        Me.Dispose()
    End Sub

    Private Sub MS_Edit_HandleCreated(sender As Object, e As System.EventArgs) Handles Me.HandleCreated

    End Sub

    Private Sub MS_Edit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.O AndAlso e.Modifiers = Keys.Control) Then
            OpenMS_File()
        ElseIf (e.KeyCode = Keys.W AndAlso e.Modifiers = Keys.Control) Then
            wMain.Show()
        ElseIf (e.KeyCode = Keys.S AndAlso e.Modifiers = Keys.Control) Then
            'SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
        ElseIf (e.KeyCode = Keys.F1) Then

        ElseIf e.KeyCode = Keys.F5 Then
            GotoBookPrevMark()
        ElseIf e.KeyCode = Keys.F5 Then
            GotoBookMark()
        ElseIf (e.KeyCode = Keys.F AndAlso e.Modifiers = Keys.Control) Then
            FindReplace()
        End If

    End Sub
    Private Sub OpenMS_File()
        With MS_BrosweDialog
            ' Select Character ini file
            .InitialDirectory = Environment.SpecialFolder.MyDocuments & "\Furcadia\"
            If .ShowDialog = DialogResult.OK Then
                AddNewEditorTab("", "", "")
                TabSections(TabControl2.SelectedIndex).Clear()
                Dim slashPosition As Integer = .FileName.LastIndexOf("\")
                WorkFileName(TabControl2.SelectedIndex) = .FileName.Substring(slashPosition + 1)
                WorkPath(TabControl2.SelectedIndex) = .FileName.Replace(WorkFileName(TabControl2.SelectedIndex), "")

                frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
                Me.Text = frmTitle(TabControl2.SelectedIndex)
                lblStatus.Text = "Status: opened " & WorkFileName(TabControl2.SelectedIndex)

                Dim reader As New StreamReader(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
                MS_Editor.Text = ""
                FullFile(TabControl2.SelectedIndex).Clear()
                Do While reader.Peek <> -1
                    Dim line As String = reader.ReadLine
                    FullFile(TabControl2.SelectedIndex).Add(line)
                Loop
                MS_Editor.Text = String.Join(vbCrLf, FullFile(TabControl2.SelectedIndex).ToArray)
                MS_Editor.ClearUndo()
                reader.Close()

                UpdateSegments()
                UpdateSegmentList()
                CanOpen(TabControl2.SelectedIndex) = True
                TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex)
                TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)
            End If
        End With

    End Sub

    Public Function FileTab(ByRef File As String) As Integer
        Dim f As String = Path.GetFileName(File)
        Dim p As String = Path.GetDirectoryName(File)
        For I = 0 To TabControl2.TabPages.Count - 1
            If WorkFileName(I) = f And WorkPath(I) = p Then
                Return I
            End If
        Next
        Return -1
    End Function

    Public Function IsEditorOpen(ByRef File As String) As Boolean
        Dim f As String = Path.GetFileName(File)
        Dim p As String = Path.GetDirectoryName(File)
        For I = 0 To TabControl2.TabPages.Count - 1
            If WorkFileName(I) = f And WorkPath(I) = p Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function IsEditorOpen(ByRef File As String, ByRef path As String) As Boolean
        For I = 0 To TabControl2.TabPages.Count - 1
            If WorkFileName(I) = File And WorkPath(I) = path Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub OpenMS_File(ByRef filename As String, Optional ByRef bName As String = "none")

        If IsEditorOpen(filename) Then
            TabControl2.SelectedIndex = FileTab(filename)
            Exit Sub
        End If

        Dim f As String = Path.GetFileName(filename)
        Dim p As String = Path.GetDirectoryName(filename)
        AddNewEditorTab(f, p, TabControl2.TabPages.Count)
        WorkFileName(TabControl2.SelectedIndex) = f
        WorkPath(TabControl2.SelectedIndex) = p

        frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        lblStatus.Text = "Status: opened " & WorkFileName(TabControl2.SelectedIndex)

        Dim reader As New StreamReader(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
        MS_Editor.Text = ""
        Do While reader.Peek <> -1
            Dim line As String = reader.ReadLine
            FullFile(TabControl2.SelectedIndex).Add(line)
        Loop
        MS_Editor.Text = String.Join(vbLf, FullFile(TabControl2.SelectedIndex).ToArray)
        MS_Editor.ClearUndo()
        reader.Close()

        UpdateSegments()

        CanOpen(TabControl2.SelectedIndex) = True
        TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex)
        TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)

    End Sub




    Private Sub MS_Edit_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim splash As SplashScreen1 = CType(My.Application.SplashScreen, SplashScreen1)
        Dim filename As String = ""
        If My.Application.CommandLineArgs.Count > 0 Then
            filename = My.Application.CommandLineArgs.Last()
        End If

        Dim BotName As String = "none"
        If My.Application.CommandLineArgs.Count > 1 Then
            BotName = My.Application.CommandLineArgs(0)
        End If
        objMutex = New System.Threading.Mutex(False, "MyApplicationName")
        If objMutex.WaitOne(0, False) = False Then
            splash.UpdateProgress("Loading file: " & filename, 50 / 100)
            objMutex.Close()
            objMutex = Nothing
            'splash.Close()
            'Step 1.
            'The second way is to iter through all processes 
            'Since a windows title can change, I perfer the second method
            Dim cstrProcessName As String = "DSeX"
            Dim ProcessHandle As IntPtr = FindProcessByName(cstrProcessName)

            'Step 2.
            'Create some information to send.
            Dim strTag As String = BotName

            Dim strData As String = filename

            'Option 1 - Encryption
            'To make a message a little more difficult to hack we can make it look like a bad piece of memory.
            'The receiver must also set the same value.
            WmCpyDta_SetEncrypt("d"c)
            ' 'd' is a bitwise seed value. I like to use d because it 
            'makes the message look like bad memory

            Dim iResult As Integer = 0
            If ProcessHandle.ToInt32() <> 0 Then
                iResult = WmCpyDta_SendMessage_sTagData(ProcessHandle.ToInt32(), 0, "~DSEX~", 0, strTag, strData)
            End If
            splash.UpdateProgress("Complete ", 100)
            End
        End If

        Me.DoubleBuffered = True

        KeysIni = New IniFile()
        KeysIni.Load(My.Application.Info.DirectoryPath + "\Keys.ini")
        EditSettings = New EditSettings
        Me.Location = My.Settings.EditFormLocation
        Me.Visible = True

        Try


            Dim items As List(Of AutocompleteItem) = New List(Of AutocompleteItem)()
            Dim KeyCount As Integer = CInt(KeysIni.GetKeyValue("Init-Types", "Count"))
            For i As Integer = 1 To KeyCount
                items.Clear()
                Dim DSLines As New List(Of String)
                Dim DSLines2 As New ArrayList
                Dim key As String = KeysIni.GetKeyValue("Init-Types", i.ToString)
                splash.UpdateProgress("Loading " + key + "...", i / (KeyCount + 2) * 100)
                Dim DSSection As IniSection = KeysIni.GetSection(key)

                For Each K As IniSection.IniKey In DSSection.Keys
                    Dim fields As ArrayList = SplitCSV(K.Value)
                    DSLines.Add(fields(2))
                    DSLines2.Add(fields(2))
                    items.Add(New DA_AUtoCompleteMenu(fields(2)))
                Next

                AddNewTab(key, i.ToString, DSLines2)
                autoCompleteList.AddRange(items)

            Next

            splash.UpdateProgress("Finishing up...", (KeyCount + 1) / (KeyCount + 2) * 100)




        Catch eX As Exception
            Dim logError As New ErrorLogging(eX, Me)
        End Try

        AddNewEditorTab("", mPath, 0)
        SetDSHilighter()
        CanOpen(0) = True
        If (My.Application.CommandLineArgs.Count > 0) Then
            OpenMS_File(filename)
        Else
            NewFile()
        End If

        GetTemplates()
        splash.UpdateProgress("Complete!", 100)
    End Sub

    Private Sub RefreshTemplatesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RefreshTemplatesToolStripMenuItem.Click
        GetTemplates()
    End Sub

    Public Function SplitCSV(ByRef input As String) As ArrayList
        Dim csvSplit As New Regex("(?:^|,)(""(?:[^""]+|"""")*""|[^,]*)", RegexOptions.Compiled)
        Dim str As ArrayList = New ArrayList
        For Each match As Match In csvSplit.Matches(input)
            str.Add(match.Value.TrimStart(","c).Trim(""""c))
        Next

        Return str
    End Function

    Public Sub Reset()
        If TabControl2.TabPages.Count = 0 Then Exit Sub

        DS_String_Style.ForeBrush = New SolidBrush(EditSettings.StringVariableColor)
        DS_Str_Var_Style.ForeBrush = New SolidBrush(EditSettings.StringVariableColor)
        DS_Num_Var_Style.ForeBrush = New SolidBrush(EditSettings.VariableColor)
        DS_Comment_Style.ForeBrush = New SolidBrush(EditSettings.CommentColor)
        DS_Default_Style.ForeBrush = New SolidBrush(Color.Green)
        DS_Num_Style.ForeBrush = New SolidBrush(EditSettings.NumberColor)
        DS_Line_ID_Style.ForeBrush = New SolidBrush(EditSettings.IDColor)
        DS_HEADER = KeysIni.GetKeyValue("MS-General", "Header")
        DS_FOOTER = KeysIni.GetKeyValue("MS-General", "Footer")

        For i = 0 To TabControl2.TabPages.Count - 1
            MS_Editor(i).Invalidate()
        Next

    End Sub

    Private Sub SetDSHilighter()
        DS_String_Style.ForeBrush = New SolidBrush(EditSettings.StringVariableColor)
        DS_Str_Var_Style.ForeBrush = New SolidBrush(EditSettings.StringVariableColor)
        DS_Num_Var_Style.ForeBrush = New SolidBrush(EditSettings.VariableColor)
        DS_Comment_Style.ForeBrush = New SolidBrush(EditSettings.CommentColor)
        DS_Num_Style.ForeBrush = New SolidBrush(EditSettings.NumberColor)
        DS_Line_ID_Style.ForeBrush = New SolidBrush(EditSettings.IDColor)
        DS_HEADER = KeysIni.GetKeyValue("MS-General", "Header")
        DS_FOOTER = KeysIni.GetKeyValue("MS-General", "Footer")

    End Sub


    Public Function RegExEscapedSring(ByVal text As String) As String
        text = text.Replace("\", "\\")
        text = text.Replace(".", "\.")
        text = text.Replace("$", "\$")
        text = text.Replace("^", "\^")
        text = text.Replace("{", "\{")
        text = text.Replace("[", "\[")
        text = text.Replace("(", "\(")
        text = text.Replace("|", "\|")
        text = text.Replace("}", "\}")
        text = text.Replace(")", "\)")
        text = text.Replace("]", "\]")
        text = text.Replace("*", "\*")
        text = text.Replace("+", "\+")
        text = text.Replace("?", "\?")
        Return text

    End Function

    Private Sub AddNewTab(ByRef n As String, ByRef VL_Name As String, ByRef lst As ArrayList)
        Causes.TabPages.Add(n)
        'Adds a new tab to your tab control

        Dim intLastTabIndex As Integer = Causes.TabPages.Count - 1
        'Gets the index number of the last tab

        Causes.TabPages(intLastTabIndex).Name = "tbpageBrowser" & Causes.TabPages.Count
        'Causes.SelectedTab = Causes.TabPages(intLastTabIndex)

        'Creates the listview and displays it in the new tab
        Dim lstView As ListView_NoFlicker = New ListView_NoFlicker()


        lstView.Dock = DockStyle.Fill
        lstView.Sorting = SortOrder.Ascending
        lstView.Columns.Add(n)
        lstView.Location = New System.Drawing.Point(6, 3)
        lstView.Height = Causes.Height
        lstView.Width = Causes.Width
        lstView.BeginUpdate()
        For Each t In lst
            lstView.Items.Add(t)
        Next
        lstView.EndUpdate()
        lstView.Parent = Causes.TabPages(intLastTabIndex)
        lstView.ListViewItemSorter = New MyCustomSorter
        lstView.HeaderStyle = ColumnHeaderStyle.None
        lstView.Name = VL_Name
        lstView.FullRowSelect = True
        lstView.View = View.Details
        lstView.Columns(0).Width() = lstView.Width
        AddHandler lstView.DoubleClick, AddressOf ListCauses_DoubleClick
        AddHandler lstView.Resize, AddressOf ListView_resize
        'lstView.Show()

    End Sub

    Private Function NewMSFile() As String
        Dim str As New StringBuilder
        str.AppendLine(KeysIni.GetKeyValue("MS-General", "Header"))
        Dim t As String = " "
        Dim n As Integer = 0
        While t <> ""
            t = KeysIni.GetKeyValue("MS-General", "H" + n.ToString)
            If t <> "" Then str.AppendLine(t)
            n += 1
        End While
        For i = 0 To CInt(KeysIni.GetKeyValue("MS-General", "InitLineSpaces"))
            str.AppendLine("")
        Next
        str.Append(KeysIni.GetKeyValue("MS-General", "Footer"))
        Return str.ToString
    End Function

    Private Function NewDMScript() As String
        Dim str As New StringBuilder
        str.AppendLine(KeysIni.GetKeyValue("DM-Script", "Header"))
        Dim t As String = " "
        Dim n As Integer = 0
        While t <> ""
            t = KeysIni.GetKeyValue("DM-Script", "H" + n.ToString)
            If t <> "" Then str.AppendLine(t)
            n += 1
        End While
        For i = 0 To CInt(KeysIni.GetKeyValue("DM-Script", "InitLineSpaces"))
            str.AppendLine("")
        Next
        str.Append(KeysIni.GetKeyValue("DM-Script", "Footer"))
        Return str.ToString
    End Function

    Private Sub TextInsert(ByRef LB As ListView, Optional ByVal Spaces As Integer = 0)

        If IsNothing(MS_Editor) Then Exit Sub
        Dim ch As String = " "
        If ini.GetKeyValue("Init-Types", "Character") = "Tab" Then ch = vbTab
        Dim insertText = StrDup(Spaces, ch) & LB.SelectedItems(0).Text

        MS_Editor.InsertText(insertText + vbCrLf)
        UpdateStatusBar()
    End Sub


    Private Sub ListCauses_DoubleClick(sender As Object, e As System.EventArgs)
        Dim test2 As Object = ini.GetKeyValue("Init-Types", sender.name)

        Dim test As Integer = ini.GetKeyValue("C-Indents", test2).ToInteger
        TextInsert(sender, test)

    End Sub

    Private Sub ListView_resize(sender As Object, e As EventArgs)
        sender.Columns(0).Width() = sender.Width
    End Sub

    Private Sub SaveAs()

        With MSSaveDialog
            ' Select Character ini file
            .InitialDirectory = Paths.GetFurcadiaDocPath() + "Dreams\"
            If .ShowDialog = DialogResult.OK Then

                Dim slashPosition As Integer = .FileName.LastIndexOf("\")
                WorkFileName(TabControl2.SelectedIndex) = Path.GetFileName(.FileName)
                'WorkPath(TabControl2.SelectedIndex) = .FileName.Replace(WorkFileName(TabControl2.SelectedIndex), "")
                WorkPath(TabControl2.SelectedIndex) = Path.GetDirectoryName(.FileName)

                SaveMS_File(TabControl2.SelectedIndex)
                lblStatus.Text = "Status: Saved " & WorkFileName(TabControl2.SelectedIndex)
                frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
                Me.Text = frmTitle(TabControl2.SelectedIndex)
                CanOpen(TabControl2.SelectedIndex) = True
            End If
        End With
    End Sub

    Private Sub SaveMS_File(ByRef TabIdx As Integer)
        If MS_Editor.InvokeRequired Then
            Dim d As New FileSave(AddressOf SaveMS_File)
            Me.Invoke(d, TabIdx)
        Else
            'Dim Restart As Boolean = False
            If Not CanOpen(TabIdx) Then

                If String.IsNullOrEmpty(WorkFileName(TabIdx)) Then
                    SaveAs()
                    Exit Sub
                End If
                SaveSections(TabIdx)
                RebuildFullFile(TabIdx)
                'UpdateSegments()
                Try
                    Dim Writer As New StreamWriter(WorkPath(TabIdx) & "/" & WorkFileName(TabIdx))
                    For j = 0 To FullFile(TabIdx).Count - 1
                        Writer.WriteLine(FullFile(TabIdx)(j))
                    Next
                    Writer.Close()
                    lblStatus.Text = "Status: File Saved."

                    CanOpen(TabIdx) = True
                    If TabIdx = TabControl2.SelectedIndex Then Me.Text = frmTitle(TabIdx)
                Catch ex As Exception
                    MessageBox.Show("There was an error writing to " + WorkFileName(TabIdx))
                End Try


            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveMS_File(TabControl2.SelectedIndex)
    End Sub

    Private Sub MenuCopy_Click(sender As System.Object, e As System.EventArgs) Handles MenuCopy.Click, EditDropCopy.Click
        MS_Editor.Copy()
    End Sub

    Private Sub MenuCut_Click(sender As System.Object, e As System.EventArgs) Handles MenuCut.Click, EditDropCut.Click
        MS_Editor.Cut()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PasteToolStripMenuItem.Click, EditDropPaste.Click
        MS_Editor.Paste()
    End Sub

    Private Sub MS_Editor_CursorChanged(sender As Object, e As System.EventArgs)
        UpdateStatusBar()
    End Sub

    Private Sub MS_Editor_TextChangedDelayed(sender As Object, e As TextChangedEventArgs)
        If TabEditStyles(TabControl2.SelectedIndex) = EditStyles.ds Then
            sender.CommentPrefix = "*"
            'clear style of changed range
            e.ChangedRange.ClearStyle(StyleIndex.All)

            'Header
            e.ChangedRange.SetStyle(DS_Header_Style, "(" + RegExEscapedSring(DS_HEADER) + ")", RegexOptions.IgnoreCase)

            'Footer
            e.ChangedRange.SetStyle(DS_Footer_Style, "(" + RegExEscapedSring(DS_FOOTER) + ")", RegexOptions.IgnoreCase)
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


            'Section Folding Experimental.. Not sure how to do this -Gero

            'sender.Range.ClearFoldingMarkers()
            'Dim currentIndent = 0
            'Dim lastNonEmptyLine = 0

            'For i As Integer = 0 To sender.LinesCount - 1
            '    Dim line = sender(i)
            '    Dim spacesCount = line.StartSpacesCount
            '    If spacesCount = line.Count Then
            '        'empty line
            '        Continue For
            '    End If

            '    If line.Text.StartsWith(RES_SEC_Marker) Then
            '        'append start folding marker
            '        sender(lastNonEmptyLine + 1).FoldingStartMarker = line.Text
            '    ElseIf currentIndent > spacesCount Then
            '        'append end folding marker

            '    End If

            '    currentIndent = spacesCount
            '    lastNonEmptyLine = i
            'Next
        ElseIf TabEditStyles(TabControl2.SelectedIndex) = EditStyles.ms Then

        End If
    End Sub



    Private Sub MS_Editor_TextChanged(sender As Object, e As System.EventArgs)

        If SectionChange = False Then CanOpen(TabControl2.SelectedIndex) = False

        UpdateStatusBar()
        If CanOpen(TabControl2.SelectedIndex) = False Then Me.Text = frmTitle(TabControl2.SelectedIndex) & "*"
        If WorkFileName(TabControl2.SelectedIndex) = "" And CanOpen(TabControl2.SelectedIndex) = False Then
            TabControl2.SelectedTab.Text = New_File_Tag + "*"
            TabControl2.RePositionCloseButtons()
        ElseIf CanOpen(TabControl2.SelectedIndex) = False Then
            TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex) + "*"
            TabControl2.RePositionCloseButtons()
        End If

        If CanOpen(TabControl2.SelectedIndex) = False Then lblStatus.Text = "Status: A change has occured since you last saved the document."
    End Sub

    Private Sub UpdateStatusBar()
        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.Selection.Start.iChar.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.Selection.Start.iLine.ToString
        sb.Panels.Item(2).Text = "Total Lines: " & MS_Editor.Lines.Count.ToString
        sb.Panels.Item(3).Text = "Total Characters: " & MS_Editor.Text.Length.ToString
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click

        OpenMS_File()
    End Sub

    Private Sub ToolBoxSave_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxSave.Click
        SaveMS_File(TabControl2.SelectedIndex)
    End Sub

    Private Sub ToolBoxOpen_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxOpen.Click
        OpenMS_File()

    End Sub

    Private Sub ToolBoxFindReplace_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxFindReplace.Click
        Try

            Dim frm As Form = New frmSearch

            frm.Show() 'Dialog()

        Catch exc As Exception

            MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub FindReplaceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FindReplaceToolStripMenuItem.Click
        FindReplace()
    End Sub

    Private Sub FindReplace()
        If IsNothing(MS_Editor) Then Exit Sub
        Try

            Dim frm As Form = New frmSearch

            frm.Show() 'Dialog()

        Catch exc As Exception

            MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub ToolBoxCut_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxCut.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Cut()
    End Sub

    Private Sub ToolBoxyCopy_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxyCopy.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Copy()
    End Sub

    Private Sub ToolBoxPaste_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxPaste.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Paste()
    End Sub

    Private Sub ToolBoxUndo_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxUndo.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Undo()
    End Sub

    Private Sub ToolBoxRedo_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxRedo.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Redo()
    End Sub

    Private Sub GotoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GotoToolStripMenuItem.Click, ToolStripButton1.Click
        If IsNothing(MS_Editor) Then Exit Sub
        Dim i As String = _
InputBox("What line within the document do you want to send the cursor to?", _
" Location to send the Cursor?", "0")
        If String.IsNullOrEmpty(i) Then Exit Sub
        If IsInteger(i) And i.ToInteger > 0 Then
            If i > MS_Editor.Lines.Count - 1 Then i = MS_Editor.Lines.Count - 1
            MS_Editor.Selection.Start = New Place(0, i.ToInteger - 1)
            MS_Editor.Selection.Expand()
            MS_Editor.DoSelectionVisible()
            UpdateStatusBar()

        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Redo()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click, ToolBoxSaveAs.Click
        If IsNothing(MS_Editor) Then Exit Sub
        SaveAs()
    End Sub

    Private Sub NewFile()
        If Not CanOpen(TabControl2.SelectedIndex) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(TabControl2.SelectedIndex)
            End If
        End If
        TabSections(TabControl2.SelectedIndex).Clear()
        MS_Editor.Text = NewMSFile()
        MS_Editor.ClearUndo()
        WorkFileName(TabControl2.SelectedIndex) = ""

        lblStatus.Text = "Status: Opened New DragonSpeak  File "
        frmTitle(TabControl2.SelectedIndex) = "DSeX - New File"
        FullFile(TabControl2.SelectedIndex).Clear()
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        For i = 0 To MS_Editor.Lines.Count - 1
            FullFile(TabControl2.SelectedIndex).Add(MS_Editor.Lines.Item(i).Trim(charsToTrim))
        Next
        TabControl2.SelectedTab.Text = New_File_Tag
        CanOpen(TabControl2.SelectedIndex) = True
        TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)
        UpdateSegments()
        UpdateSegmentList()
        'MS_Editor.Lexing.Colorize()
    End Sub
    Public Sub NewScript()
        If Not CanOpen(TabControl2.SelectedIndex) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(TabControl2.SelectedIndex)
            End If
        End If
        TabSections(TabControl2.SelectedIndex).Clear()
        MS_Editor.Text = NewDMScript()

        WorkFileName(TabControl2.SelectedIndex) = ""

        lblStatus.Text = "Status: Opened New Draconian Magic Script "
        frmTitle(TabControl2.SelectedIndex) = "DSeX - New File"
        FullFile(TabControl2.SelectedIndex).Clear()
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        For i = 0 To MS_Editor.Lines.Count - 1
            FullFile(TabControl2.SelectedIndex).Add(MS_Editor.Lines.Item(i))
        Next
        TabControl2.SelectedTab.Text = New_File_Tag
        CanOpen(TabControl2.SelectedIndex) = True
        TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)
        UpdateSegments()
        UpdateSegmentList()
    End Sub

    Private Sub ToolBoxNew_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxNew.Click, NewMonkeySpeakToolStripMenuItem.Click
        AddNewEditorTab("", mPath, TabControl2.TabCount.ToString)
        NewFile()
    End Sub

    Private Sub FixIndentsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FixIndentsToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub
        Dim StrArray() As String = MS_Editor.Text.Replace(vbCr, "").Split(vbLf)
        Dim str As String
        Dim Count As Integer = ini.GetKeyValue("Init-Types", "Count").ToInteger
        Dim pattern(Count - 1) As String
        Dim pat(Count - 1) As Integer
        Dim chr As String = " "
        If ini.GetKeyValue("Init-Types", "Character") = "Tab" Then chr = vbTab
        Dim T As String = " "

        For I As Integer = 1 To Count
            T = KeysIni.GetKeyValue("Init-Types", I.ToString)
            Dim s As String = ini.GetKeyValue("Indent-Lookup", T)
            Dim u As String = ini.GetKeyValue("C-Indents", T)
            pattern(I - 1) = "(" + s
            pat(I - 1) = u.ToInteger
        Next
        Dim insertPos As Integer = MS_Editor.SelectionStart
        For I As Integer = 0 To StrArray.Length - 1
            str = StrArray(I).Trim

            For N As Integer = 0 To pattern.Length - 1
                If str.StartsWith(pattern(N)) Then
                    Dim c As Integer = pattern(N).Substring(1, 1)
                    StrArray(I) = StrDup(pat(N), chr) & str
                    Exit For
                End If
            Next
        Next

        MS_Editor.Text = String.Join(vbCrLf, StrArray)

        MS_Editor.SelectionStart = insertPos
    End Sub

    Private Sub BtnComment_Click(sender As System.Object, e As System.EventArgs) Handles BtnComment.Click, ApplyCommentToolStripMenuItem.Click, AutocommentOnToolStripMenuItem.Click
        ApplyComment()
    End Sub

    Private Sub ApplyCommentToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ApplyCommentToolStripMenuItem1.Click
        If IsNothing(MS_Editor) Then Exit Sub

        Dim str() As String = MS_Editor.Text.Replace(vbCr, "").Split(vbLf)
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                str(i) = "*" & str(i)
            Next
            MS_Editor.Text = String.Join(vbCrLf, str)

        End If
    End Sub

    Private Sub AutoCommentOffToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AutoCommentOffToolStripMenuItem.Click, AutocommentOffToolStripMenuItem1.Click
        If IsNothing(MS_Editor) Then Exit Sub

        Dim str() As String = MS_Editor.Text.Replace(vbCr, "").Split(vbLf)
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                If str(i).StartsWith("*") Then str(i) = str(i).Remove(0, 1)
            Next
            MS_Editor.Text = String.Join(vbCrLf, str)

        End If
    End Sub
    Private Sub ApplyComment()
        If IsNothing(MS_Editor) Then Exit Sub

        Dim this As String = MS_Editor.Selection.Text

        Dim str() As String = this.Replace(vbCr, "").Split(Chr(10))
        Dim str2 As String = ""
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                str(i) = "*" + str(i)
            Next
            MS_Editor.SelectedText = String.Join(vbCrLf, str)

        End If
    End Sub

    Private Sub RemoveComment()
        If IsNothing(MS_Editor) Then Exit Sub

        Dim this As String = MS_Editor.Selection.Text
        Dim str() As String = this.Replace(vbCr, "").Split(Chr(10))
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                If str(i).StartsWith("*") Then str(i) = str(i).Remove(0, 1)
            Next
            MS_Editor.SelectedText = String.Join(vbCrLf, str)

        End If
    End Sub


    Private Sub RemoveCommentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveCommentToolStripMenuItem.Click, BtnUncomment.Click
        RemoveComment()
    End Sub

    Private Sub ConfigToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ConfigToolStripMenuItem.Click
        Config.Show()
        Config.Activate()
    End Sub

    Dim LastSearchString As String = String.Empty
    Dim LastFoundIndex As Integer = 0
    Dim lisView As Integer = 1

    Private Sub BtnFind_Click(sender As System.Object, e As System.EventArgs) Handles BtnFind.Click
        'Reset the starting index to Zero as the Search has changed   

        Dim Test2 As Boolean = False
        Debug.WriteLine("TabControl1.TabPages.Count: " & Causes.TabPages.Count)
        Debug.WriteLine("Control count: " & Causes.Controls.Find("1", True).Count)
        Dim LV1 As ListView_NoFlicker = CType(Causes.TabPages.Item(lisView - 1).Controls.Find(lisView.ToString, True)(0), ListView_NoFlicker)

        LV1.Items(LastFoundIndex).BackColor = Color.White
        LV1.Items(LastFoundIndex).ForeColor = Color.Black
        If TxtBxFind.Text <> LastSearchString Then
            LastFoundIndex = 0
            lisView = 1
        Else
            LastFoundIndex += 1
        End If
        For lis As Integer = lisView To CInt(KeysIni.GetKeyValue("Init-Types", "Count"))
            LV1 = CType(Causes.TabPages.Item(lis - 1).Controls.Find(lis.ToString, True)(0), ListView_NoFlicker)

            With LV1
                For i As Integer = LastFoundIndex To .Items.Count - 1
                    If .Items(i).SubItems(0).Text.ToLower.Contains(TxtBxFind.Text.ToLower) Then
                        Causes.SelectedTab = Causes.TabPages.Item(lis - 1)
                        .Items(i).Selected = True
                        .Items(i).EnsureVisible()
                        .Items(i).BackColor = Color.Blue
                        .Items(i).ForeColor = Color.White
                        LastFoundIndex = i
                        LastSearchString = TxtBxFind.Text
                        lisView = lis
                        Test2 = True
                        Exit For
                    End If
                Next
            End With
            If Test2 Then
                lisView = lis
                Exit For
            End If
            LastFoundIndex = 0

        Next
        If Not Test2 Then
            MessageBox.Show("No items found.")
        End If
    End Sub



    Private Sub TxtBxFind_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtBxFind.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            BtnFind.PerformClick()
        End If
    End Sub


    Private Sub DSWizardToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DSWizardToolStripMenuItem.Click
        wMain.Show()
        wMain.Activate()
    End Sub



    Private Sub AbutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AbutToolStripMenuItem.Click
        AboutBox1.Show()
        AboutBox1.Activate()
    End Sub

    Private popupMenu As AutocompleteMenu
    Public Sub AddNewEditorTab(ByRef FileName As String, FilePath As String, ByRef n As String)

        Dim tp As New TabPage

        tp.Text = New_File_Tag + "     "
        TabControl2.TabPages.Add(tp)
        Dim intLastTabIndex As Integer = TabControl2.TabPages.Count - 1
        tp.Name = "tbpageBrowser" & intLastTabIndex.ToString
        'Adds a new tab to your tab control
        CanOpen.Add(True)
        WorkFileName.Add(FileName)
        WorkPath.Add(FilePath)

        frmTitle.Add("DSeX - New DragonSpeak File")
        SectionIdx.Add(0)
        TabEditStyles.Add(EditStyles.ds)
        Dim sec As List(Of String) = New List(Of String)
        sec.Clear()
        FullFile.Add(sec)
        'Gets the index number of the last tab
        Dim segs As List(Of TDSSegment) = New List(Of TDSSegment)
        TabSections.Add(segs)

        'Creates the listview and displays it in the new tab
        Dim lstView As FastColoredTextBox = New FastColoredTextBox()
        lstView.ContextMenuStrip = Me.EditMenu
        lstView.AcceptsTab = True
        lstView.Parent = tp
        lstView.AutoIndent = False
        lstView.Anchor = AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        lstView.Name = "edit" + n
        lstView.Dock = DockStyle.Fill
        lstView.CommentPrefix = "*"
        lstView.Language = Language.Custom
        lstView.Show()
        lstView.ContextMenuStrip = SectionMenu
        TabControl2.SelectedTab = TabControl2.TabPages(intLastTabIndex)
        popupMenu = New AutocompleteMenu(lstView)
        popupMenu.Enabled = True

        popupMenu.SearchPattern = "[ \w\.:=!<>\{\}]"
        popupMenu.Items.MaximumSize = New System.Drawing.Size(600, 300)
        popupMenu.Items.Width = 600
        popupMenu.Items.SetAutocompleteItems(autoCompleteList)
        AddHandler lstView.TextChangedDelayed, AddressOf MS_Editor_TextChangedDelayed
        AddHandler lstView.TextChanged, AddressOf MS_Editor_TextChanged
        AddHandler lstView.MouseUp, AddressOf MS_EditRightClick
        AddHandler lstView.CursorChanged, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.MouseClick, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.KeyUp, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.MouseClick, AddressOf MS_Editor_MouseDoubleClick
        'UpdateSegments()
    End Sub


    Private Sub MS_EditRightClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.EditMenu.Show(MS_Editor, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControl2.SelectedIndexChanged
        ListBox1.Items.Clear()
        If TabControl2.SelectedIndex = -1 Then Exit Sub

        If CanOpen(TabControl2.SelectedIndex) = False Then
            Me.Text = frmTitle(TabControl2.SelectedIndex) + "*"
        Else
            Me.Text = frmTitle(TabControl2.SelectedIndex)
        End If
        UpdateSegmentList()
        If SectionIdx(TabControl2.SelectedIndex) <> ListBox1.SelectedIndex Then ListBox1.SelectedIndex = SectionIdx(TabControl2.SelectedIndex)

        'If SettingsChanged(TabControl2.SelectedIndex) Then
        '    SetDSHilighter()
        '    SettingsChanged(TabControl2.SelectedIndex) = False
        'End If
    End Sub

    Private Sub TabControl2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim x As New ContextMenuStrip
            Dim s As ToolStripItem = x.Items.Add("New Tab", Nothing, AddressOf ToolBoxNew_Click)
            s.Tag = sender
            Dim t As ToolStripItem = x.Items.Add("Close All Other Tabs", Nothing, AddressOf FCloseAllTab_Click)
            Dim v As ToolStripItem = x.Items.Add("Close Tab", Nothing, AddressOf FCloseTab_Click)
            x.Items.Add(New ToolStripSeparator)
            Dim u As ToolStripItem = x.Items.Add("Save File", Nothing, AddressOf FSave_Click)

            x.Show(sender, e.Location)
            Dim tabPageIndex As Integer = 0
            For i As Integer = 0 To TabControl2.TabPages.Count - 1
                If TabControl2.GetTabRect(i).Contains(e.X, e.Y) Then
                    tabPageIndex = i
                    Exit For
                End If

            Next
            t.Tag = tabPageIndex
            u.Tag = tabPageIndex
        ElseIf e.Button = Windows.Forms.MouseButtons.Middle Then
            For i As Integer = 0 To TabControl2.TabPages.Count - 1
                If TabControl2.GetTabRect(i).Contains(e.X, e.Y) Then
                    CloseTab(i)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub CloseAllButThis(ByRef i As Integer)
        For j = TabControl2.TabPages.Count - 1 To 0 Step -1
            If i <> j Then CloseTab(j)
        Next
    End Sub

    Private Sub FCloseAllTab_Click(sender As System.Object, e As System.EventArgs)
        Dim i As Integer = sender.Tag
        CloseAllButThis(i)
    End Sub

    Private Sub FCloseTab_Click(sender As System.Object, e As System.EventArgs)
        Dim i As Integer = sender.Tag
        CloseTab(i)
    End Sub

    Private Sub TabControl2_CloseButtonClick(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TabControl2.CloseButtonClick
        e.Cancel = True
        Dim i As Integer = sender.TabIndex
        If sender.TabIndex = -1 Then Exit Sub
        If sender.TabIndex >= TabControl2.TabPages.Count Then Exit Sub

        CloseTab(i)
        TabControl2.RePositionCloseButtons()
    End Sub

    Private Sub CloseTab(ByVal i As Integer)
        If i = -1 Then Exit Sub
        If i >= TabControl2.TabPages.Count Then Exit Sub
        Dim fname As String = WorkFileName(i)
        If fname = "" Then
            fname = New_File_Tag
        End If

        If Not CanOpen(i) Then
            Dim reply As DialogResult = MessageBox.Show(fname + " has been modified." + Environment.NewLine + "Save the changes?", "Warning", _
      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(i)
            ElseIf reply = DialogResult.Cancel Then
                Exit Sub
            End If

        End If
        TabControl2.TabPages.RemoveAt(i)
        CanOpen.RemoveAt(i)
        WorkFileName.RemoveAt(i)
        WorkPath.RemoveAt(i)
        frmTitle.RemoveAt(i)
        SectionIdx.RemoveAt(i)
        FullFile.RemoveAt(i)
        TabSections.RemoveAt(i)
        TabEditStyles.RemoveAt(i)
        If TabControl2.TabPages.Count = 0 And Me.Disposing = False Then
            AddNewEditorTab("", "", "")
            NewFile()
        End If

    End Sub


    Private Sub FNewTab_Click(sender As System.Object, e As System.EventArgs)
        Dim c As Integer = TabControl2.TabCount
        AddNewEditorTab("", mPath, c.ToString)
        NewFile()
    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox2.DoubleClick, InsertToDSFileToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub
        Dim p As String = TemplatePaths.Item(ListBox2.SelectedIndex) + "\" + ListBox2.SelectedItem + ".ds"
        Dim reader As New StreamReader(p)
        Dim str As String = ""
        Do While reader.Peek <> -1
            str += reader.ReadLine + vbLf
        Loop
        reader.Close()
        Dim pos As Integer = MS_Editor.SelectionStart
        MS_Editor.InsertText(str)
        MS_Editor.SelectionStart = pos + str.Length

    End Sub



    Private Sub FSave_Click(sender As System.Object, e As System.EventArgs)

        SaveMS_File(sender.tag)
    End Sub

    Private Sub RenameToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles RenameToolStripMenuItem1.Click
        Dim s As String = Microsoft.VisualBasic.InputBox("New Name?")
        If String.IsNullOrEmpty(s) Then Exit Sub
        My.Computer.FileSystem.RenameFile(TemplatePaths.Item(ListBox2.SelectedIndex) + ListBox2.SelectedItem + ".ds", TemplatePaths(ListBox2.SelectedIndex) + s + ".ds")
        GetTemplates()
    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles EditToolStripMenuItem1.Click
        OpenMS_File(TemplatePaths.Item(ListBox2.SelectedIndex) + "/" + ListBox2.SelectedItem + ".ds")
    End Sub

    Private Sub BtnTemplateDelete_Click(sender As System.Object, e As System.EventArgs) Handles BtnTemplateDelete.Click, DeleteToolStripMenuItem.Click
        Dim reply As DialogResult = MessageBox.Show("Really delete this template?", "Caption", _
     MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = DialogResult.OK Then
            File.Delete(TemplatePaths(ListBox2.SelectedIndex) + ListBox2.SelectedItem + ".ds")
            TemplatePaths.RemoveAt(ListBox2.SelectedIndex)
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If

    End Sub

    Private Sub BtnTemplateAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnTemplateAdd.Click, AddToolStripMenuItem.Click
        Dim path As String = Furcadia.IO.Paths.GetFurcadiaDocPath + "/Templates/"
        Dim message, title As String
        Dim myValue As Object
        message = "Name of file?"
        title = "Template Name"
        myValue = InputBox(message, title, "")
        If myValue Is "" Then Exit Sub
        TemplatePaths.Add(path)
        ListBox2.Items.Add(myValue)
        File.WriteAllText(path + myValue.ToString + ".ds", MS_Editor.Selection.Text)
    End Sub

    Public Sub UpdateSegments()
        UpdateSegments(TabControl2.SelectedIndex)
    End Sub

    Public Sub UpdateSegments(ByRef idx As Integer)
        Debug.Print("UpdateSegments()")
        Dim tmpsec As TDSSegment = New TDSSegment
        Dim sec2 As TDSSegment = New TDSSegment
        Dim bypass As Boolean = False
        Dim t1 As String = ""
        Dim blank As Boolean = False
        'Dim TabSections As List(Of Dictionary(Of String, TDSSegment)) 
        If Not IsNothing(TabSections(idx)) Then TabSections(idx).Clear()

        'Build from the basics
        tmpsec.Title = RES_Def_section


        bypass = False

        For i = 0 To FullFile(idx).Count - 1
            'Debug.Print("UpdateSegments FullFile.Count" + FullFile(idx).Count.ToString)
            If FullFile(idx)(i).StartsWith(RES_DS_begin) Then
                sec2.Title = RES_DSS_begin
                sec2.lines.Add(FullFile(idx)(i))
                sec2.SecType = TSecType.SecFixed
                bypass = True
                blank = True
                TabSections(idx).Insert(0, sec2)
            End If
            'Ending segment

            If FullFile(idx)(i) = RES_DS_end Then
                tmpsec = New TDSSegment
                tmpsec.Title = RES_DSS_End
                tmpsec.SecType = TSecType.SecEnd
                'tmpsec.lines.Add(FullFile(idx)(i))
                TabSections(idx).Add(tmpsec)
                bypass = False
                blank = False
            End If

            If (tmpsec.Title = RES_Def_section) And i = 1 And Not FullFile(idx)(i).StartsWith(RES_SEC_Marker) Then
                blank = False
                TabSections(idx).Add(tmpsec)
                bypass = False
                ' End If
                ' Section marker

            End If

            If FullFile(idx)(i).StartsWith(RES_SEC_Marker) Then

                t1 = FullFile(idx)(i).Substring(RES_SEC_Marker.Length)

                tmpsec = New TDSSegment
                tmpsec.Title = t1
                TabSections(idx).Add(tmpsec)
                blank = False
                bypass = True
            End If

            If Not bypass Then
                tmpsec.lines.Add(FullFile(idx)(i))

            End If
            bypass = False
            'Debug.Print("UpdateSegments TabSections " + TabSections(idx).Count.ToString)
        Next

    End Sub
    Public Sub UpdateSegmentList()
        UpdateSegmentList(TabControl2.SelectedIndex)
    End Sub

    Public Sub UpdateSegmentList(ByRef idx As Integer)
        Dim tseg As TDSSegment = New TDSSegment
        Dim Sects_Indent As String = Space(4)
        'Debug.Print("UpdateSegmentList()")
        With ListBox1
            .Items.Clear()
            .Items.Add(RES_DSS_All)
            For i = 0 To TabSections(idx).Count - 1

                tseg = TabSections(idx)(i)
                If (tseg.Title <> RES_DSS_begin) And (tseg.Title <> RES_DSS_End) Then
                    If tseg.Title = RES_Def_section And i = 1 Then

                        .Items.Add(TabSections(idx)(i).Title)

                    Else
                        .Items.Add(Sects_Indent + TabSections(idx)(i).Title)
                    End If
                Else
                    .Items.Add(TabSections(idx)(i).Title)
                End If


            Next
        End With
    End Sub

    Dim SectionLstIdx As Integer = 0
    Dim SectionLstIdxOld As Integer = 0
    Private Sub ListBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDown
        If sender.Items.Count = 0 Then Exit Sub
        SectionLstIdx = sender.IndexFromPoint(New Point(e.X, e.Y))
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If SectionLstIdx = -1 Then SectionLstIdx = sender.SelectedIndex
            sender.SelectedIndex = SectionLstIdx
        End If
        Debug.Print("ListBox1_MouseDown()")
        'SaveSections(TabControl2.SelectedIndex)

        If sender.SelectedIndex <> SectionLstIdxOld Then SaveSections(TabControl2.SelectedIndex)
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged, ListBox2.SelectedIndexChanged
        If IsNothing(sender.selecteditem) Then Exit Sub
        ToolTip1.SetToolTip(sender, sender.SelectedItem.ToString)
    End Sub
    Private Sub ListBox1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseUp
        If ListBox1.Items.Count = 0 Or sender.SelectedIndex = -1 Then Exit Sub
        Debug.Print("ListBox1_MouseUp()")
        Dim test As Integer = sender.SelectedIndex
        If ListBox1.SelectedIndex = SectionLstIdxOld Then Exit Sub
        SectionChange = True
        If ListBox1.SelectedIndex = 0 Then
            'Rebuild FullFile list first
            RebuildFullFile()
            MS_Editor.Text = ""
            MS_Editor.Text = String.Join(vbCrLf, FullFile(TabControl2.SelectedIndex).ToArray)

            UpdateSegments(TabControl2.SelectedIndex)
        Else
            DisplaySection(ListBox1.SelectedIndex - 1)
        End If
        SectionChange = False
        UpdateSegmentList()
        ListBox1.SelectedIndex = SectionLstIdx
        MS_Editor.ClearUndo()

        Dim j As Integer = ListBox1.SelectedIndex
        SectionIdx(TabControl2.SelectedIndex) = sender.SelectedIndex

        SectionLstIdxOld = sender.SelectedIndex
    End Sub

    Private Sub SaveSections(ByVal tabidx As Integer)
        If ListBox1.SelectedIndex = -1 Then ListBox1.SelectedIndex = 0
        'Debug.Print("SaveSections()")

        If SectionIdx(tabidx) = 0 And MS_Editor(tabidx).Text <> "" Then
            'Debug.Print("SectionIdx(" + tabidx.ToString + ")")
            FullFile(tabidx).Clear()
            For i = 0 To MS_Editor.Lines.Count - 1
                FullFile(tabidx).Add(MS_Editor(tabidx).Lines.Item(i).TrimEnd(charsToTrim))
            Next
            UpdateSegments()

        ElseIf SectionIdx(tabidx) > 0 Then
            Dim section As TDSSegment = TabSections(tabidx)(SectionIdx(tabidx) - 1)
            section.lines.Clear()
            For i = 0 To MS_Editor(tabidx).Lines.Count - 1
                section.lines.Add(MS_Editor(tabidx).Lines.Item(i))
            Next
            'TabSections(tabidx)(SectionIdx(tabidx) - 1) = section
        End If
    End Sub

    Private Sub DisplaySection(ByRef j As Integer)
        MS_Editor.Text = ""
        MS_Editor.Text = String.Join(vbCrLf, TabSections(TabControl2.SelectedIndex)(j).lines.ToArray)
    End Sub

    Private Sub RebuildFullFile()
        RebuildFullFile(TabControl2.SelectedIndex)
    End Sub

    Private Sub RebuildFullFile(ByRef Tab As Integer)
        Debug.Print("RebuildFullFile()")
        FullFile(Tab).Clear()
        For i = 0 To TabSections(Tab).Count - 1

            'RES_SEC_Marker
            If TabSections(Tab)(i).Title <> RES_DSS_begin And _
                TabSections(Tab)(i).Title <> RES_DSS_End And _
                (TabSections(Tab)(i).Title <> RES_Def_section Or TabSections(Tab)(i).Title = RES_Def_section And i > 1) Then
                FullFile(Tab).Add(RES_SEC_Marker + TabSections(Tab)(i).Title)
                Debug.Print("RebuildFullFile SectionMarker FullFile.Count" + FullFile(Tab).Count.ToString)
            End If

            FullFile(Tab).AddRange(TabSections(Tab)(i).lines)
            Debug.Print("RebuildFullFile AddRange FullFile.Count" + FullFile(Tab).Count.ToString)
        Next
    End Sub

    Private Sub NewSection_Click(sender As System.Object, e As System.EventArgs) Handles NewSection.Click, BtnSectionAdd.Click
        If TabControl2.TabCount > 0 Then NewSec((TabSections(TabControl2.SelectedIndex).Count))
    End Sub

    Private Sub InsertSectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InsertSectionToolStripMenuItem.Click
        If ListBox1.SelectedIndex > 1 Then NewSec(ListBox1.SelectedIndex)
    End Sub

    Private Sub NewSec(ByRef i As Integer)
        If ListBox1.Items.Count = 0 Then Exit Sub
        If ListBox1.SelectedIndex < 1 Or i < 1 Then Exit Sub
        'If i >= 1 Then i = 2
        i -= 1
        Debug.Print("NewSection_Click()")
        Dim s As String = Microsoft.VisualBasic.InputBox("Add Section")
        If String.IsNullOrEmpty(s) Then Exit Sub

        SaveSections(TabControl2.SelectedIndex)
        Dim section As New TDSSegment
        'Dim sec As TDSSegment = TabSections(TabControl2.SelectedIndex)(i)
        section.Title = s
        section.lines.Add("")

        TabSections(TabControl2.SelectedIndex).Insert(i, section)
        'TabSections(TabControl2.SelectedIndex)(i + 1) = sec
        UpdateSegmentList()

        SectionChange = False

        DisplaySection(i) 'Loosing Selected Section here, Remove this Line Everything saves properly
        i += 1
        SectionChange = True
        ListBox1.SelectedIndex = i
        SectionLstIdxOld = i
    End Sub

    Private Sub RemoveSection(ByRef i As Integer)
        If ListBox1.SelectedIndex < 2 Then Exit Sub
        If ListBox1.Items.Count = 0 Then Exit Sub
        Dim reply As DialogResult = MessageBox.Show("Really delete this section?", "Caption", _
MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = DialogResult.Cancel Then Exit Sub

        TabSections(TabControl2.SelectedIndex).RemoveAt(i)
        'RebuildFullFile()
        UpdateSegmentList()
        'UpdateSegments(i) 'm Goin bald trying to figure this
        ListBox1.SelectedIndex = i + 1
        DisplaySection(i)

    End Sub

    Private Sub DeleteSection_Click(sender As System.Object, e As System.EventArgs) Handles DeleteSection.Click, BtnSectionDelete.Click
        RemoveSection(ListBox1.SelectedIndex - 1)
    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        If ListBox1.Items.Count = 0 Then Exit Sub
        Dim idx As Integer = ListBox1.SelectedIndex
        Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(idx - 1)
        Dim s As String = Microsoft.VisualBasic.InputBox("New Name?")
        If String.IsNullOrEmpty(s) Then Exit Sub
        section.Title = s
        UpdateSegmentList()
        ListBox1.SelectedIndex = idx
    End Sub

    Private Sub BtnSectionUp_Click(sender As System.Object, e As System.EventArgs) Handles BtnSectionUp.Click
        If ListBox1.Items.Count = 0 Then Exit Sub
        If ListBox1.SelectedIndex <= 2 Then Exit Sub
        Dim idx As Integer = ListBox1.SelectedIndex - 1

        Dim item As TDSSegment = New TDSSegment
        item = TabSections(TabControl2.SelectedIndex)(idx)

        If idx <> 0 Then
            TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
            idx -= 1
            TabSections(TabControl2.SelectedIndex).Insert(idx, item)
            UpdateSegmentList()
            ListBox1.SelectedIndex = idx + 1
            SectionLstIdxOld = idx + 1
        End If

        'Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(idx)
        'TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
        'TabSections(TabControl2.SelectedIndex).Insert(idx - 1, section)
        UpdateSegmentList()
        ListBox1.SelectedIndex = idx
    End Sub

    Private Sub BtnSectionDown_Click(sender As System.Object, e As System.EventArgs) Handles BtnSectionDown.Click
        If ListBox1.Items.Count = 0 Then Exit Sub
        If ListBox1.SelectedIndex < 2 Then Exit Sub
        If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then Exit Sub
        Dim idx As Integer = ListBox1.SelectedIndex - 1

        Dim item As TDSSegment = New TDSSegment
        item = TabSections(TabControl2.SelectedIndex)(idx)

        If idx < TabSections(TabControl2.SelectedIndex).Count - 1 Then
            TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
            idx += 1
            TabSections(TabControl2.SelectedIndex).Insert(idx, item)
            UpdateSegmentList()
            ListBox1.SelectedIndex = idx + 1
            SectionLstIdxOld = idx + 1
        End If

        'Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(idx)
        'TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
        'TabSections(TabControl2.SelectedIndex).Insert(idx + 1, section)


    End Sub



    Private Sub ListBox2_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ListBox2.SelectedIndex = ListBox2.IndexFromPoint(New Point(e.X, e.Y))
        End If
    End Sub



    Private Sub GotoNextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GotoNextToolStripMenuItem.Click
        GotoBookMark()
    End Sub
    Private Sub GotoBookMark()
        If IsNothing(MS_Editor) Then Exit Sub
        Dim l As Integer = MS_Editor.Selection.Start.iLine
        MS_Editor.GotoNextBookmark(l)
    End Sub

    Private Sub GotoPreviousToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GotoPreviousToolStripMenuItem.Click
        GotoBookPrevMark()
    End Sub

    Private Sub GotoBookPrevMark()
        If IsNothing(MS_Editor) Then Exit Sub
        Dim l As Integer = MS_Editor.Selection.Start.iLine
        MS_Editor.GotoPrevBookmark(l)

    End Sub

    Private Sub RemoveAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveAllToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub
        MS_Editor.Bookmarks.Clear()
    End Sub

    Private Sub MS_Editor_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        If e.X < MS_Editor.LeftIndent Then
            Dim place = MS_Editor.PointToPlace(e.Location)
            If MS_Editor.Bookmarks.Contains(place.iLine) Then
                MS_Editor.Bookmarks.Remove(place.iLine)
            Else
                MS_Editor.Bookmarks.Add(place.iLine)
            End If
        End If
    End Sub

End Class