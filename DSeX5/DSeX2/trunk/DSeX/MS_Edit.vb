Imports System.Text.RegularExpressions
Imports System
Imports DSeX.ConfigStructs
Imports System.IO
Imports Furcadia.IO
Imports No_Flicker
Imports AutocompleteMenuNS
Imports DSeX.IniFile
Imports System.Text

Public Class MS_Edit

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
#Region "Properties"
    Public WithEvents RTBWrapper As New cRTBWrapper()


    Public CanOpen As List(Of Boolean) = New List(Of Boolean)
    Public SettingsChanged As List(Of Boolean) = New List(Of Boolean)
    Public WorkFileName As List(Of String) = New List(Of String)
    Public WorkPath As List(Of String) = New List(Of String)
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

    Enum TSecType
        SecNormal
        SecEnd
        SecFixed
        SecDefault
    End Enum

    <Serializable()> _
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
    Dim TabSections As List(Of List(Of TDSSegment)) = New List(Of List(Of TDSSegment))

    Public TemplatePaths As List(Of String) = New List(Of String)

    Public Sub AutoMenu() Handles AutocompleteMenu1.Selected

        'MS_Editor.SelectedText2 = Chr(10)
        ' Dim pos As Integer = MS_Editor.SelectionStart + MS_Editor.SelectionLength
        RTBWrapper.ColorLine(MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart - 1))
        ' MS_Editor.SelectionStart = pos
        lblStatus.Text = "Status: Auto Complete Insert"
    End Sub

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

    Public Function MS_Editor() As RichTextBox2
        If TabControl2.SelectedIndex = -1 Then Return Nothing
        Return FindControl(TabControl2.TabPages.Item(TabControl2.SelectedIndex), "edit")
        ' Return CType(TabControl2.TabPages.Item(TabControl2.SelectedIndex).Controls.Find("edit" + TabControl2.SelectedIndex.ToString, True)(0), RichTextBox2)
    End Function '+ TabControl2.SelectedIndex.ToString

    Public Function MyPictureBox() As PictureBox
        Return FindControl(TabControl2.TabPages.Item(TabControl2.SelectedIndex), "Gutter")
        'Return CType(TabControl2.TabPages.Item(TabControl2.SelectedIndex).Controls.Find("Gutter", True)(0), PictureBox)
    End Function

#End Region

#Region "Event Handlers"
    Delegate Sub FileSave(ByRef path As String, ByRef filename As String)
#End Region

    Private Sub GetTemplates()
        Dim path As String = Application.StartupPath + "/Templates/"
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
        path = Furcadia.IO.Paths.GetFurcadiaDocPath + "/Templates/"
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
                Dim result = MessageBox.Show("Save your work before closing?", "caption", MessageBoxButtons.YesNoCancel)
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


                        '  MsgBox(e.CloseReason.ToString)
                    Catch eX As Exception
                        Dim logError As New ErrorLogging(eX, Me)
                    End Try
                ElseIf result = DialogResult.Yes Then
                    SaveMS_File(WorkPath(i), WorkFileName(i))
                    Try
                        TabControl2.TabPages.RemoveAt(i)
                        CanOpen.RemoveAt(i)
                        WorkFileName.RemoveAt(i)
                        WorkPath.RemoveAt(i)
                        frmTitle.RemoveAt(i)
                        SectionIdx.RemoveAt(i)
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

    Private Sub MS_Edit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.O AndAlso e.Modifiers = Keys.Control) Then
            OpenMS_File()
        ElseIf (e.KeyCode = Keys.W AndAlso e.Modifiers = Keys.Control) Then
            wMain.Show()
        ElseIf (e.KeyCode = Keys.S AndAlso e.Modifiers = Keys.Control) Then
            SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
        ElseIf (e.KeyCode = Keys.F1) Then

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
                'MS_Editor.Lines = File.ReadAllLines(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
                MS_Editor.BeginUpdate()
                MS_Editor.Text = ""
                FullFile(TabControl2.SelectedIndex).Clear()
                Do While reader.Peek <> -1
                    Dim line As String = reader.ReadLine
                    FullFile(TabControl2.SelectedIndex).Add(line)
                    MS_Editor.AppendLine(line)
                Loop
                MS_Editor.EndUpdate()
                UpdateSegments()
                UpdateSegmentList()
                RTBWrapper.colorDocument()
                CanOpen(TabControl2.SelectedIndex) = True
                TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex)
                TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)
            End If
        End With

    End Sub

    Public Sub OpenMS_File(ByRef filename As String)

        Dim slashPosition As Integer = filename.LastIndexOf("\")
        WorkFileName(TabControl2.SelectedIndex) = filename.Substring(slashPosition + 1)
        WorkPath(TabControl2.SelectedIndex) = filename.Replace(WorkFileName(TabControl2.SelectedIndex), "")

        frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        lblStatus.Text = "Status: opened " & WorkFileName(TabControl2.SelectedIndex)
        Dim reader As New StreamReader(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
        'MS_Editor.Lines = File.ReadAllLines(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
        MS_Editor.BeginUpdate()
        MS_Editor.Text = ""
        Do While reader.Peek <> -1
            Dim line As String = reader.ReadLine
            FullFile(TabControl2.SelectedIndex).Add(line)
            MS_Editor.AppendLine(line)
        Loop
        MS_Editor.EndUpdate()
        UpdateSegments()
        RTBWrapper.colorDocument()
        CanOpen(TabControl2.SelectedIndex) = True
        TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex)
        TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)

    End Sub

    Public Sub Reset()
        If IsNothing(MS_Editor) Then Exit Sub
        For i = 0 To SettingsChanged.Count - 1
            If i <> TabControl2.SelectedIndex Then
                SettingsChanged(i) = True
            End If
        Next
        Reset(MS_Editor)
        RTBWrapper.colorDocument()
    End Sub

    Public Sub Reset(ByRef rtf As RichTextBox2)
        With RTBWrapper
            .unbind()
            .bind(rtf)
            'tDict(Pattern, isRegex, isCase, value)
            'Variable
            .rtfSyntax.Clear()
            .rtfSyntax.add("%([A-Za-z0-9_]+)", True, True, EditSettings.VariableColor.ToArgb)
            .rtfSyntax.add("~([A-Za-z0-9_]+)", True, True, EditSettings.StringVariableColor.ToArgb)
            'string
            .rtfSyntax.add("\\{(.*?)\\}", True, True, EditSettings.StringColor.ToArgb)
            'Line ID
            .rtfSyntax.add("\(([0-9]*)\:([0-9]*)\)", True, True, EditSettings.IDColor.ToArgb)
            'Comment
            .rtfSyntax.add("^\*(.*?)$", True, True, EditSettings.CommentColor.ToArgb)
            'Number
            .rtfSyntax.add(" ([0-9#]+)", True, True, EditSettings.NumberColor.ToArgb)
            .rtfSyntax.add("\.([0-9#]+)", True, True, EditSettings.NumberColor.ToArgb)
        End With
    End Sub



    Private Sub MS_Edit_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.DoubleBuffered = True

        Dim splash As SplashScreen1 = CType(My.Application.SplashScreen, SplashScreen1)
        KeysIni = New IniFile()
        KeysIni.Load(My.Application.Info.DirectoryPath + "\Keys.ini")
        EditSettings = New EditSettings
        Me.Location = My.Settings.EditFormLocation
        Me.Visible = True

        Try

            AutocompleteMenu1.Enabled = EditSettings.AutoCompleteEnable
            Dim autoCompleteList As New List(Of String)
            Dim KeyCount As Integer = CInt(KeysIni.GetKeyValue("Init-Types", "Count"))
            For i As Integer = 1 To KeyCount
                Dim DSLines As New List(Of String)
                Dim DSLines2 As New ArrayList
                Dim key As String = KeysIni.GetKeyValue("Init-Types", i.ToString)
                splash.UpdateProgress("Loading " + key + "...", i / (KeyCount + 2) * 100)
                Dim DSSection As IniSection = KeysIni.GetSection(key)

                For Each K As IniSection.IniKey In DSSection.Keys
                    Dim fields As ArrayList = SplitCSV(K.Value)
                    DSLines.Add(fields(2))
                    DSLines2.Add(fields(2))
                Next

                AddNewTab(key, i.ToString, DSLines2)
                autoCompleteList.AddRange(DSLines)

            Next

            splash.UpdateProgress("Finishing up...", (KeyCount + 1) / (KeyCount + 2) * 100)

            AutocompleteMenu1.SetAutocompleteItems(autoCompleteList)


        Catch eX As Exception
            Dim logError As New ErrorLogging(eX, Me)
        End Try

        AddNewEditorTab("", mPath, 0)
        CanOpen(0) = True
        If (My.Application.CommandLineArgs.Count > 0) Then
            OpenMS_File(My.Application.CommandLineArgs(0))
        Else
            NewFile()
        End If

        GetTemplates()
        splash.UpdateProgress("Complete!", 100)
        'If Not IsNothing(splash) Then splash.Close()
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
        str.AppendLine(KeysIni.GetKeyValue("MS-General", "Footer"))
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
        str.AppendLine(KeysIni.GetKeyValue("DM-Script", "Footer"))
        Return str.ToString
    End Function

    Private Sub TextInsert(ByRef LB As ListView, Optional ByVal Spaces As Integer = 0)
        Dim ch As String = " "
        If ini.GetKeyValue("Init-Types", "Character") = "Tab" Then ch = vbTab
        Dim insertText = StrDup(Spaces, ch) & LB.SelectedItems(0).Text

        Dim insertPos As Integer = MS_Editor.SelectionStart
        'MS_Editor.

        MS_Editor.SelectedText2 = insertText + chr(10)

        RTBWrapper.ColorLine(MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart - 1))
        ' MS_Editor.SelectionStart = insertPos + insertText.Length

        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1

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
            .InitialDirectory = Environment.SpecialFolder.MyDocuments & "Furcadia\Dreams\"
            If .ShowDialog = DialogResult.OK Then

                Dim slashPosition As Integer = .FileName.LastIndexOf("\")
                WorkFileName(TabControl2.SelectedIndex) = .FileName.Substring(slashPosition + 1)
                WorkPath(TabControl2.SelectedIndex) = .FileName.Replace(WorkFileName(TabControl2.SelectedIndex), "")
                MS_Editor.SaveFile(WorkPath(TabControl2.SelectedIndex) & "/" & WorkFileName(TabControl2.SelectedIndex), RichTextBoxStreamType.PlainText)
                lblStatus.Text = "Status: Saved " & WorkFileName(TabControl2.SelectedIndex)
                frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
                Me.Text = frmTitle(TabControl2.SelectedIndex)
                CanOpen(TabControl2.SelectedIndex) = True
            End If
        End With
    End Sub

    Private Sub SaveMS_File(ByRef path As String, ByRef fName As String)
        If MS_Editor.InvokeRequired Then
            Dim d As New FileSave(AddressOf SaveMS_File)
            Me.Invoke(d, path, fName)
        Else
            Dim Restart As Boolean = False
            If Not CanOpen(TabControl2.SelectedIndex) Then

                If String.IsNullOrEmpty(fName) Then
                    SaveAs()
                    Exit Sub
                End If
                UpdateSegments()
                RebuildFullFile()
                Try
                    Dim Writer As New StreamWriter(path & "/" & fName)
                    For j = 0 To FullFile(TabControl2.SelectedIndex).Count - 1
                        Writer.WriteLine(FullFile(TabControl2.SelectedIndex)(j))
                    Next
                    Writer.Close()
                    lblStatus.Text = "Status: File Saved."

                    CanOpen(TabControl2.SelectedIndex) = True
                    Me.Text = frmTitle(TabControl2.SelectedIndex)
                Catch ex As Exception
                    MessageBox.Show("There was an error writing to " + fName)
                End Try


            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
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
        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1

    End Sub


    Private Sub MS_Editor_TextChanged(sender As Object, e As System.EventArgs)
        If SectionChange = False Then CanOpen(TabControl2.SelectedIndex) = False
        'RTBWrapper.colorDocument()
        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1
        sb.Panels.Item(2).Text = "Total Lines: " & MS_Editor.GetLineFromCharIndex(MS_Editor.Text.Length) + 1
        sb.Panels.Item(3).Text = "Total Characters: " & MS_Editor.Text.Length.ToString
        If CanOpen(TabControl2.SelectedIndex) = False Then Me.Text = frmTitle(TabControl2.SelectedIndex) & "*"
        If WorkFileName(TabControl2.SelectedIndex) = "" And CanOpen(TabControl2.SelectedIndex) = False Then
            TabControl2.SelectedTab.Text = "(New File)*"
            TabControl2.RePositionCloseButtons()
        ElseIf CanOpen(TabControl2.SelectedIndex) = False Then
            TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex) + "*"
            TabControl2.RePositionCloseButtons()
        End If

        If CanOpen(TabControl2.SelectedIndex) = False Then lblStatus.Text = "Status: A change has occured since you last saved the document."
    End Sub
#Region "Gutter"
    Private Sub DrawRichTextBoxLineNumbers(ByRef g As Graphics, ByRef RTF As RichTextBox2)
        Try
            'calculate font heigth as the difference in Y coordinate between line 2 and line 1
            'note that the RichTextBox text must have at least two lines. So the initial Text property
            'of the RichTextBox should not be an empty string. It could be something like vbcrlf & vbcrlf & vbcrlf 
            If RTF.Lines.Count < 3 Then Exit Sub 'RTF.Text = vbCrLf & vbCrLf & vbCrLf
            Dim font_height As Single = RTF.GetPositionFromCharIndex(RTF.GetFirstCharIndexFromLine(2)).Y - RTF.GetPositionFromCharIndex(RTF.GetFirstCharIndexFromLine(1)).Y
            If font_height = 0 Then Exit Sub

            'Get the first line index and location
            Dim firstIndex As Integer = RTF.GetCharIndexFromPosition(New Point(0, g.VisibleClipBounds.Y + font_height / 3))
            Dim firstLine As Integer = RTF.GetLineFromCharIndex(firstIndex)
            Dim firstLineY As Integer = RTF.GetPositionFromCharIndex(firstIndex).Y

            'Print on the PictureBox the visible line numbers of the RichTextBox
            g.Clear(Control.DefaultBackColor)
            Dim i As Integer = firstLine
            Dim y As Single
            Do While y < g.VisibleClipBounds.Y + g.VisibleClipBounds.Height
                y = firstLineY + 2 + font_height * (i - firstLine - 1)
                g.DrawString((i).ToString, RTF.Font, Brushes.DarkBlue, MyPictureBox.Width - g.MeasureString((i).ToString, RTF.Font).Width, y)
                i += 1
            Loop
        Catch ex As Exception
            Dim logError As New ErrorLogging(ex, Me)
        End Try
        'Debug.WriteLine("Finished: " & firstLine + 1 & " " & i - 1)
    End Sub

    Private Sub r_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
        MyPictureBox.Invalidate()
    End Sub

    Private Sub r_VScroll(ByVal sender As Object, ByVal e As System.EventArgs)
        MyPictureBox.Invalidate()
    End Sub

    Private Sub p_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        DrawRichTextBoxLineNumbers(e.Graphics, MS_Editor)
    End Sub

#End Region

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click

        OpenMS_File()
    End Sub

    Private Sub ToolBoxSave_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxSave.Click
        SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
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
" Location to send the Cursor?", "1")

        If IsInteger(i) And i > 0 Then
            If i > MS_Editor.Lines.Count Then i = MS_Editor.Lines.Count
            MS_Editor.SelectionStart = MS_Editor.GetFirstCharIndexFromLine(i - 1)

            sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
            sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1

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
                SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
            End If
        End If
        TabSections(TabControl2.SelectedIndex).Clear()
        MS_Editor.Text = NewMSFile()
        RTBWrapper.colorDocument()
        WorkFileName(TabControl2.SelectedIndex) = ""

        lblStatus.Text = "Status: Opened New DragonSpeak  File "
        frmTitle(TabControl2.SelectedIndex) = "DSeX - New File"
        FullFile(TabControl2.SelectedIndex).Clear()
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        For i = 0 To MS_Editor.Lines.Count - 1
            FullFile(TabControl2.SelectedIndex).Add(MS_Editor.Lines(i))
        Next
        TabControl2.SelectedTab.Text = "(New File)"
        CanOpen(TabControl2.SelectedIndex) = True
        TabControl2.RePositionCloseButtons(TabControl2.SelectedTab)
        UpdateSegments()
        UpdateSegmentList()
    End Sub
    Public Sub NewScript()
        If Not CanOpen(TabControl2.SelectedIndex) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
            End If
        End If
        TabSections(TabControl2.SelectedIndex).Clear()
        MS_Editor.Text = NewDMScript()
        RTBWrapper.colorDocument()
        WorkFileName(TabControl2.SelectedIndex) = ""

        lblStatus.Text = "Status: Opened New Draconian Magic Script "
        frmTitle(TabControl2.SelectedIndex) = "DSeX - New File"
        FullFile(TabControl2.SelectedIndex).Clear()
        Me.Text = frmTitle(TabControl2.SelectedIndex)
        For i = 0 To MS_Editor.Lines.Count - 1
            FullFile(TabControl2.SelectedIndex).Add(MS_Editor.Lines(i))
        Next
        TabControl2.SelectedTab.Text = "(New File)"
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
        Dim StrArray() As String = MS_Editor.Lines
        'MS_Editor.BeginUpdate()

        Dim str As String
        Dim Count As Integer = ini.GetKeyValue("Init-Types", "Count").ToInteger
        Dim pattern(Count - 1) As String
        Dim pat(Count - 1) As Integer
        Dim chr As String = " "
        If ini.GetKeyValue("Init-Types", "Character") = "Tab" Then Chr = vbTab
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

        MS_Editor.Lines = StrArray

        'MS_Editor.EndUpdate()

        RTBWrapper.colorDocument()
        MS_Editor.SelectionStart = insertPos
    End Sub

    Private Sub BtnComment_Click(sender As System.Object, e As System.EventArgs) Handles BtnComment.Click, ApplyCommentToolStripMenuItem.Click
        ApplyComment()
    End Sub

    Private Sub ApplyCommentToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ApplyCommentToolStripMenuItem1.Click
        If IsNothing(MS_Editor) Then Exit Sub

        Dim str() As String = MS_Editor.Lines
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                str(i) = "*" & str(i)
            Next
            MS_Editor.Lines = str
            RTBWrapper.colorDocument()
        End If
    End Sub

    Private Sub AutoCommentOffToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AutoCommentOffToolStripMenuItem.Click
        If IsNothing(MS_Editor) Then Exit Sub

        Dim str() As String = MS_Editor.Lines
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                If str(i).StartsWith("*") Then str(i) = str(i).Remove(0, 1)
            Next
            MS_Editor.Lines = str
            RTBWrapper.colorDocument()
        End If
    End Sub
    Private Sub ApplyComment()
        If IsNothing(MS_Editor) Then Exit Sub

        Dim this As String = MS_Editor.SelectedText
        Dim str() As String = this.Split(Chr(10))
        Dim str2 As String = ""
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                str2 &= Chr(10) & "*" & str(i)
            Next
            MS_Editor.SelectedText2 = str2.Substring(1)
            RTBWrapper.colorDocument()
        End If
    End Sub

    Private Sub RemoveComment()
        If IsNothing(MS_Editor) Then Exit Sub

        Dim this As String = MS_Editor.SelectedText
        Dim str() As String = this.Split(Chr(10))
        Dim str2 As String = ""
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                If str(i).StartsWith("*") Then str(i) = str(i).Remove(0, 1)
                str2 &= Chr(10) & str(i)
            Next
            MS_Editor.SelectedText2 = str2.Substring(1)
            RTBWrapper.colorDocument()
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

    Public Sub AddNewEditorTab(ByRef FileName As String, FilePath As String, ByRef n As String)

        Dim tp As New TabPage

        tp.Text = "(New File)     "
        TabControl2.TabPages.Add(tp)
        Dim intLastTabIndex As Integer = TabControl2.TabPages.Count - 1
        tp.Name = "tbpageBrowser" & intLastTabIndex.ToString
        'Adds a new tab to your tab control
        CanOpen.Add(True)
        SettingsChanged.Add(False)
        WorkFileName.Add(FileName)
        WorkPath.Add(FilePath)
        frmTitle.Add("DSeX - New DragonSpeak File")
        SectionIdx.Add(0)
        Dim sec As List(Of String) = New List(Of String)
        sec.Clear()
        FullFile.Add(sec)
        'Gets the index number of the last tab
        Dim segs As List(Of TDSSegment) = New List(Of TDSSegment)
        TabSections.Add(segs)
        Dim Gutter As PictureBox = New PictureBox
        Gutter.Parent = TabControl2.TabPages(intLastTabIndex)
        Gutter.Location = New System.Drawing.Point(0, 3)
        Gutter.Anchor = AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Bottom
        Gutter.Name = "Gutter" + n
        Gutter.Width = 30
        Gutter.Height = TabControl2.TabPages(intLastTabIndex).Height
        'Creates the listview and displays it in the new tab
        Dim lstView As RichTextBox2 = New RichTextBox2
        lstView.ContextMenuStrip = Me.EditMenu
        lstView.AcceptsTab = True
        lstView.Parent = tp
        lstView.Anchor = AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        lstView.Name = "edit" + n
        AutocompleteMenu1.SetAutocompleteMenu(lstView, Me.AutocompleteMenu1)
        lstView.Location = New System.Drawing.Point(32, 3)
        'lstView.Size = New System.Drawing.Size(573, 187)
        lstView.Height = TabControl2.TabPages(intLastTabIndex).Height
        lstView.Width = TabControl2.TabPages(intLastTabIndex).Width - 32
        lstView.Show()
        lstView.ContextMenuStrip = SectionMenu
        Reset(lstView)
        TabControl2.SelectedTab = TabControl2.TabPages(intLastTabIndex)

        lstView.WordWrap = False
        AddHandler lstView.UndoEvent, AddressOf undo
        AddHandler lstView.RedoEvent, AddressOf redo
        AddHandler lstView.CursorChanged, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.MouseClick, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.KeyUp, AddressOf MS_Editor_CursorChanged
        AddHandler Gutter.Paint, AddressOf p_Paint
        AddHandler lstView.VScroll, AddressOf r_VScroll
        AddHandler lstView.Resize, AddressOf r_Resize
        AddHandler lstView.TextChanged, AddressOf MS_Editor_TextChanged
        AddHandler lstView.MouseUp, AddressOf MS_EditRightClick
        UpdateSegments()
    End Sub

    Public Sub undo(sender As System.Object, e As No_Flicker.UndoRedoEventArgs)
        lblStatus.Text = "Status: Undo"
    End Sub
    Public Sub redo(sender As System.Object, e As No_Flicker.UndoRedoEventArgs)
        lblStatus.Text = "Status: Redo"
    End Sub

    Private Sub MS_EditRightClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.EditMenu.Show(MS_Editor, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControl2.SelectedIndexChanged
        ListBox1.Items.Clear()
        If TabControl2.SelectedIndex = -1 Then Exit Sub
        RTBWrapper.unbind()
        RTBWrapper.bind(MS_Editor)
        If CanOpen(TabControl2.SelectedIndex) = False Then
            Me.Text = frmTitle(TabControl2.SelectedIndex) + "*"
        Else
            Me.Text = frmTitle(TabControl2.SelectedIndex)
        End If
        UpdateSegmentList()
        If SectionIdx(TabControl2.SelectedIndex) <> ListBox1.SelectedIndex Then ListBox1.SelectedIndex = SectionIdx(TabControl2.SelectedIndex)
        'lastTab = TabControl2.SelectedIndex
        If SettingsChanged(TabControl2.SelectedIndex) Then
            RTBWrapper.colorDocument()
            SettingsChanged(TabControl2.SelectedIndex) = False
        End If
    End Sub

    Private Sub TabControl2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl2.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim x As New ContextMenuStrip
            Dim s As ToolStripItem = x.Items.Add("New Tab", Nothing, AddressOf ToolBoxNew_Click)
            s.Tag = sender
            Dim t As ToolStripItem = x.Items.Add("Close All Tabs But this one", Nothing, AddressOf FCloseAllTab_Click)
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
        CloseTab(sender.TabIndex)
        TabControl2.RePositionCloseButtons()
    End Sub

    Private Sub CloseTab(ByVal i As Integer)
        If i < 0 Or i > TabControl2.TabCount - 1 Then Exit Sub

        If Not CanOpen(i) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(WorkPath(i), WorkFileName(i))
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
    End Sub


    Private Sub FNewTab_Click(sender As System.Object, e As System.EventArgs)
        Dim c As Integer = TabControl2.TabCount
        AddNewEditorTab("", mPath, c.ToString)
        NewFile()
    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox2.DoubleClick, InsertToDSFileToolStripMenuItem.Click
        Dim p As String = TemplatePaths.Item(ListBox2.SelectedIndex) + ListBox2.SelectedItem + ".ds"
        Dim reader As New StreamReader(p)
        Dim str As String = ""
        Do While reader.Peek <> -1
            str += reader.ReadLine + vbLf
        Loop
        reader.Close()
        MS_Editor.SelectedText = str
        RTBWrapper.colorDocument()
    End Sub



    Private Sub FSave_Click(sender As System.Object, e As System.EventArgs)

        SaveMS_File(WorkPath(sender.tag), WorkFileName(sender.tag))
    End Sub

    Private Sub RenameToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles RenameToolStripMenuItem1.Click
        Dim s As String = Microsoft.VisualBasic.InputBox("New Name?")
        If String.IsNullOrEmpty(s) Then Exit Sub
        My.Computer.FileSystem.RenameFile(TemplatePaths.Item(ListBox2.SelectedIndex) + ListBox2.SelectedItem + ".ds", TemplatePaths(ListBox2.SelectedIndex) + s + ".ds")
        GetTemplates()
    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles EditToolStripMenuItem1.Click
        'TemplatePaths(ListBox2.SelectedIndex) + 
        AddNewEditorTab(ListBox2.SelectedItem + ".ds", TemplatePaths.Item(ListBox2.SelectedIndex), TabControl2.TabCount.ToString)
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
        File.WriteAllText(path + myValue.ToString + ".ds", MS_Editor.SelectedText)
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
        If Not IsNothing(TabSections) Then TabSections(idx).Clear()

        'Build from the basics

        tmpsec.Title = RES_Def_section

        TabSections(idx).Add(tmpsec)
        bypass = False

        For i = 0 To FullFile(idx).Count - 1

            If FullFile(idx)(i).StartsWith(RES_DS_begin) Then
                sec2.Title = RES_DSS_begin
                sec2.lines.Add(FullFile(idx)(i))
                sec2.SecType = TSecType.SecFixed
                bypass = True
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
            End If
            If (FullFile(idx)(i) <> "") And (tmpsec.Title = RES_Def_section) And i = 1 Then
                blank = False
                bypass = False
            End If
            ' Section marker
            If FullFile(idx)(i).StartsWith(RES_SEC_Marker) Then

                t1 = FullFile(idx)(i).Substring(RES_SEC_Marker.Length)

                tmpsec = New TDSSegment
                tmpsec.Title = t1
                TabSections(idx).Add(tmpsec)

                bypass = False
            ElseIf Not bypass Then
                tmpsec.lines.Add(FullFile(idx)(i))
                bypass = False
            End If
        Next

    End Sub
    Public Sub UpdateSegmentList()
        UpdateSegmentList(TabControl2.SelectedIndex)
    End Sub

    Public Sub UpdateSegmentList(ByRef idx As Integer)
        Dim tseg As TDSSegment
        Dim Sects_Indent As String = Space(4)
        With ListBox1
            .Items.Clear()
            .Items.Add(RES_DSS_All)
            For i = 0 To TabSections(idx).Count - 1

                tseg = TabSections(idx)(i)
                If (tseg.Title <> RES_DSS_begin) And (tseg.Title <> RES_DSS_End) Then
                    If ((tseg.Title = RES_Def_section) And ((i <= 1))) Then

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

    Private Sub ListBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDown
        If ListBox1.Items.Count = 0 Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ListBox1.SelectedIndex = ListBox1.IndexFromPoint(New Point(e.X, e.Y))
        End If
        Debug.Print("ListBox1_MouseDown()")
        SaveSections()

    End Sub

    Private Sub SaveSections()
        If ListBox1.SelectedIndex = -1 Then ListBox1.SelectedIndex = 0
        Debug.Print("SaveSections()")
        If SectionIdx(TabControl2.SelectedIndex) = 0 And MS_Editor.Text <> "" Then
            Debug.Print("SectionIdx(" + TabControl2.SelectedIndex.ToString + ")")
            FullFile(TabControl2.SelectedIndex).Clear()
            For i = 0 To MS_Editor.Lines.Count - 1
                FullFile(TabControl2.SelectedIndex).Add(MS_Editor.Lines(i))
            Next
            UpdateSegments()
        End If
        If SectionIdx(TabControl2.SelectedIndex) > 0 Then
            Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(SectionIdx(TabControl2.SelectedIndex) - 1)
            section.lines.Clear()
            For i = 0 To MS_Editor.Lines.Count - 1
                section.lines.Add(MS_Editor.Lines(i))
            Next
            TabSections(TabControl2.SelectedIndex)(SectionIdx(TabControl2.SelectedIndex) - 1) = section
        End If
    End Sub

    Private Sub ListBox1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseUp
        If ListBox1.Items.Count = 0 Then Exit Sub
        Debug.Print("ListBox1_MouseUp()")
        If ListBox1.SelectedIndex = -1 Then ListBox1.SelectedIndex = 0
        SectionChange = True
        MS_Editor.BeginUpdate()
        If ListBox1.SelectedIndex = 0 Then
            'Rebuild FullFile list first
            RebuildFullFile()
            MS_Editor.Text = ""
            MS_Editor.ClearUndo()
            For i = 0 To FullFile(TabControl2.SelectedIndex).Count - 1
                If i = FullFile(TabControl2.SelectedIndex).Count - 1 Then
                    MS_Editor.AppendText(FullFile(TabControl2.SelectedIndex)(i))
                Else
                    MS_Editor.AppendLine(FullFile(TabControl2.SelectedIndex)(i))
                End If

            Next
            UpdateSegments()
        Else
            DisplaySection(ListBox1.SelectedIndex - 1)
        End If

        RTBWrapper.colorDocument()
        MS_Editor.EndUpdate()
        Dim j As Integer = ListBox1.SelectedIndex
        SectionIdx(TabControl2.SelectedIndex) = ListBox1.SelectedIndex
        SectionChange = False
    End Sub
    Private Sub DisplaySection(ByRef j As Integer)
        MS_Editor.Text = ""

        For i = 0 To TabSections(TabControl2.SelectedIndex)(j).lines.Count - 1
            If i = TabSections(TabControl2.SelectedIndex)(j).lines.Count - 1 Then
                MS_Editor.AppendText(TabSections(TabControl2.SelectedIndex)(j).lines(i))
            Else
                MS_Editor.AppendLine(TabSections(TabControl2.SelectedIndex)(j).lines(i))
            End If

        Next
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
            End If
            FullFile(Tab).AddRange(TabSections(Tab)(i).lines)
        Next
    End Sub

    Private Sub NewSection_Click(sender As System.Object, e As System.EventArgs) Handles NewSection.Click, BtnSectionAdd.Click
        If TabControl2.TabCount > 0 Then NewSec((TabSections(TabControl2.SelectedIndex).Count - 1))
    End Sub

    Private Sub InsertSectionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InsertSectionToolStripMenuItem.Click
        NewSec(ListBox1.SelectedIndex - 1)
    End Sub

    Private Sub NewSec(ByRef i As Integer)
        If ListBox1.Items.Count = 0 Then Exit Sub
        If i > 1 Then i = 1
        Debug.Print("NewSection_Click()")
        SaveSections()
        Dim section As TDSSegment = New TDSSegment
        Dim s As String = Microsoft.VisualBasic.InputBox("Add Section")
        If String.IsNullOrEmpty(s) Then Exit Sub
        section.Title = s
        section.lines.Add("")

        TabSections(TabControl2.SelectedIndex).Insert(i, section)
        RebuildFullFile()
        UpdateSegmentList()
        ListBox1.SelectedIndex = i + 1
        DisplaySection(i)

    End Sub

    Private Sub RemoveSection(ByRef i As Integer)

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
        RTBWrapper.colorDocument()
    End Sub

    Private Sub DeleteSection_Click(sender As System.Object, e As System.EventArgs) Handles DeleteSection.Click
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
        Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(idx)
        TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
        TabSections(TabControl2.SelectedIndex).Insert(idx - 1, section)
        UpdateSegmentList()
        ListBox1.SelectedIndex = idx
    End Sub

    Private Sub BtnSectionDown_Click(sender As System.Object, e As System.EventArgs) Handles BtnSectionDown.Click
        If ListBox1.Items.Count = 0 Then Exit Sub
        If ListBox1.SelectedIndex < 2 Then Exit Sub
        If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then Exit Sub
        Dim idx As Integer = ListBox1.SelectedIndex - 1
        Dim section As TDSSegment = TabSections(TabControl2.SelectedIndex)(idx)
        TabSections(TabControl2.SelectedIndex).RemoveAt(idx)
        TabSections(TabControl2.SelectedIndex).Insert(idx + 1, section)
        UpdateSegmentList()
        ListBox1.SelectedIndex = idx + 2
    End Sub


    Private Sub ListBox2_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ListBox2.SelectedIndex = ListBox2.IndexFromPoint(New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged, ListBox2.SelectedIndexChanged
        ToolTip1.SetToolTip(sender, sender.SelectedItem.ToString)
    End Sub


End Class