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
    Public EditSettings As EditSettings = New EditSettings
    Dim KeysIni As IniFile
    Public CanOpen As List(Of Boolean) = New List(Of Boolean)
    Public WorkFileName As List(Of String) = New List(Of String)
    Public WorkPath As List(Of String) = New List(Of String)
    'mPath()
    Dim frmTitle As List(Of String) = New List(Of String)
    Dim SectionIdx As List(Of Integer) = New List(Of Integer)
    Public TemplatePaths As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
    Dim CMD_Args As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
    Public Sub AutoMenu() Handles AutocompleteMenu1.Selected


        Dim pos As Integer = MS_Editor.SelectionStart + MS_Editor.SelectionLength
        RTBWrapper.colorDocument()
        MS_Editor.SelectionStart = pos
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
        TemplatePaths.Clear()
        Dim x As Integer = 0
        ListBox2.BeginUpdate()
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ds").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            ListBox2.Items.Add(s.Remove(s.Length - 3, 3))
            TemplatePaths.Add(ListBox2.Items.Count - 1, path)
        Next
        path = Furcadia.IO.Paths.GetFurcadiaDocPath + "/Templates/"
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ds").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            ListBox2.Items.Add(s.Remove(s.Length - 3, 3))
            TemplatePaths.Add(ListBox2.Items.Count - 1, path)
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
        My.Settings.EditFormLocation = Me.Location
        My.Settings.Save()
        Me.Dispose()
    End Sub

    Private Sub MS_Edit_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked

    End Sub

    Private Sub MS_Edit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.O AndAlso e.Modifiers = Keys.Control) Then
            OpenMS_File()
        ElseIf (e.KeyCode = Keys.W AndAlso e.Modifiers = Keys.Control) Then
            wMain.Show()
            wMain.Activate()
        ElseIf (e.KeyCode = Keys.S AndAlso e.Modifiers = Keys.Control) Then
            SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
        ElseIf (e.KeyCode = Keys.F1) Then
            Process.Start(Application.StartupPath & "/Silver Monkey.chm")
        ElseIf (e.KeyCode = Keys.F AndAlso e.Modifiers = Keys.Control) Then
            FindReplace()
        End If

    End Sub
    Private Sub OpenMS_File()
        If Not CanOpen(TabControl2.SelectedIndex) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
            Else
                '    'Process No

            End If
        End If
        With MS_BrosweDialog
            ' Select Character ini file
            .InitialDirectory = Environment.SpecialFolder.MyDocuments & "\Furcadia\"
            If .ShowDialog = DialogResult.OK Then
                Dim slashPosition As Integer = .FileName.LastIndexOf("\")
                WorkFileName(TabControl2.SelectedIndex) = .FileName.Substring(slashPosition + 1)
                WorkPath(TabControl2.SelectedIndex) = .FileName.Replace(WorkFileName(TabControl2.SelectedIndex), "")

                frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(TabControl2.SelectedIndex)
                Me.Text = frmTitle(TabControl2.SelectedIndex)
                lblStatus.Text = "Status: opened " & WorkFileName(TabControl2.SelectedIndex)
                MS_Editor.Lines = File.ReadAllLines(WorkPath(TabControl2.SelectedIndex) + "/" + WorkFileName(TabControl2.SelectedIndex))
                RTBWrapper.colorDocument()
                CanOpen(TabControl2.SelectedIndex) = True
                TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex)
            End If
        End With

    End Sub



    Public Sub Reset()
        With RTBWrapper
            '.bind(MS_Editor)
            'tDict(Pattern, isRegex, isCase, value)
            'Variable
            EditSettings.LoadEditorSettings()
            .rtfSyntax.Clear()
            .rtfSyntax.add("%([A-Za-z0-9]+)", True, True, EditSettings.VariableColor.ToArgb)
            .rtfSyntax.add("~([A-Za-z0-9]+)", True, True, EditSettings.StringVariableColor.ToArgb)
            '.rtfSyntax.add("([0-9]*)\.?([0-9]*)", True, True, Color.Violet.ToArgb)
            'string
            .rtfSyntax.add("\\{(.*?)\\}", True, True, EditSettings.StringColor.ToArgb)
            'Line ID
            .rtfSyntax.add("\(([0-9]*)\:([0-9]*)\)", True, True, EditSettings.IDColor.ToArgb)
            'Comment
            .rtfSyntax.add("^\*(.*?)$", True, True, EditSettings.CommentColor.ToArgb)
            '.rtfSyntax.add("([0-9]+)\.([0-9]*)", True, True, Color.Violet.ToArgb)
            'Number
            .rtfSyntax.add(" ([0-9]+)", True, True, EditSettings.NumberColor.ToArgb)
            .rtfSyntax.add("\.([0-9]+)", True, True, EditSettings.NumberColor.ToArgb)
            '.rtfSyntax.add("<tr.*?>", True, True, Color.Brown.ToArgb)
            '.rtfSyntax.add("<td.*?>", True, True, Color.Brown.ToArgb)
            '.rtfSyntax.add("<img.*?>", True, True, Color.Red.ToArgb)
        End With
    End Sub


    Private Sub MS_Edit_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim splash As SplashScreen1 = CType(My.Application.SplashScreen, SplashScreen1)
        KeysIni = New IniFile()
        KeysIni.Load("Keys.ini")

        EditSettings.LoadEditorSettings()
        Me.Location = My.Settings.EditFormLocation
        Me.Visible = True

        Try

            AutocompleteMenu1.Enabled = EditSettings.AutoCompleteEnable
            Dim autoCompleteList As New List(Of String)

            For i As Integer = 1 To CInt(KeysIni.GetKeyValue("Init-Types", "Count"))
                Dim DSLines As New List(Of String)
                Dim DSLines2 As New ArrayList
                Dim key As String = KeysIni.GetKeyValue("Init-Types", i.ToString)
                splash.UpdateProgress("Loading " + key + "...", (i / (CInt(KeysIni.GetKeyValue("Init-Types", "Count")) + 1) * 100))
                Dim DSSection As IniSection = KeysIni.GetSection(key)

                For Each K As IniSection.IniKey In DSSection.Keys
                    Dim fields As ArrayList = SplitCSV(K.Value)
                    DSLines.Add(fields(2))
                    DSLines2.Add(fields(2))
                Next

                AddNewTab(key, i.ToString, DSLines2)
                autoCompleteList.AddRange(DSLines)

            Next

            splash.UpdateProgress("Finishing up...", 100)

            AutocompleteMenu1.SetAutocompleteItems(autoCompleteList)


        Catch eX As Exception
            Dim logError As New ErrorLogging(eX, Me)
        End Try


        AddNewEditorTab("New.ds", mPath, 0)

        CanOpen(0) = True
        NewFile()

        WorkFileName(0) = "New DragonSpeak File"
        'MS_Editor.SaveFile(WorkPath & "/" & cBot.MS_File, RichTextBoxStreamType.PlainText)
        MS_Editor.Text = NewMSFile()
        RTBWrapper.colorDocument()
        lblStatus.Text = "Status: Saved " & WorkFileName(0)
        frmTitle(TabControl2.SelectedIndex) = "DSeX - " & WorkFileName(0)
        Me.Text = frmTitle(0)
        CanOpen(0) = True
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
        Causes.SelectedTab = Causes.TabPages(intLastTabIndex)

        'Creates the listview and displays it in the new tab
        Dim lstView As ListView_NoFlicker = New ListView_NoFlicker()

        lstView.Parent = Causes.TabPages(intLastTabIndex)
        lstView.Dock = DockStyle.Fill
        lstView.Sorting = SortOrder.Ascending
        lstView.Columns.Add(n)

        lstView.BeginUpdate()
        For Each t In lst
            lstView.Items.Add(t)
        Next
        lstView.EndUpdate()

        lstView.ListViewItemSorter = New MyCustomSorter
        lstView.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lstView.Name = VL_Name
        lstView.FullRowSelect = True
        lstView.View = View.Details
        lstView.Columns(0).Width() = lstView.Width
        AddHandler lstView.DoubleClick, AddressOf ListCauses_DoubleClick
        AddHandler lstView.Resize, AddressOf ListView_resize
        lstView.Show()

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

    Private Sub TextInsert(ByRef LB As ListView, Optional ByVal Spaces As Integer = 0)

        Dim insertText = StrDup(Spaces, " ") & LB.SelectedItems(0).Text
        If MS_Editor.SelectedText = "" Then
            insertText = insertText & Environment.NewLine
        Else
            MS_Editor.SelectedText = ""
        End If

        Dim insertPos As Integer = MS_Editor.SelectionStart
        'MS_Editor.

        MS_Editor.SelectedText2 = insertText
        RTBWrapper.colorDocument()
        MS_Editor.SelectionStart = insertPos + insertText.Length - 1

        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1

    End Sub


    Private Sub ListCauses_DoubleClick(sender As Object, e As System.EventArgs)
        Dim test2 As Object = KeysIni.GetKeyValue("Init-Types", sender.name)

        Dim test As Object = KeysIni.GetKeyValue("C-Indents", test2)
        TextInsert(sender, Convert.ToInt32(test))

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

                If fName = Nothing Or fName = "" Then
                    SaveAs()
                    Exit Sub
                End If
                MS_Editor.SaveFile(path & "/" & fName, RichTextBoxStreamType.PlainText)

                lblStatus.Text = "Status: File Saved."

                CanOpen(TabControl2.SelectedIndex) = True
                Me.Text = frmTitle(TabControl2.SelectedIndex)
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
        CanOpen(TabControl2.SelectedIndex) = False
        'RTBWrapper.colorDocument()
        sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
        sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1
        sb.Panels.Item(2).Text = "Total Lines: " & MS_Editor.GetLineFromCharIndex(MS_Editor.Text.Length) + 1
        sb.Panels.Item(3).Text = "Total Characters: " & MS_Editor.Text.Length.ToString
        Me.Text = frmTitle(TabControl2.SelectedIndex) & "*"
        TabControl2.SelectedTab.Text = WorkFileName(TabControl2.SelectedIndex) + "*"
        lblStatus.Text = "Status: A change has occured since you last saved the document."
    End Sub
#Region "Gutter"
    Private Sub DrawRichTextBoxLineNumbers(ByRef g As Graphics, ByRef RTF As RichTextBox2)
        Try
            'calculate font heigth as the difference in Y coordinate between line 2 and line 1
            'note that the RichTextBox text must have at least two lines. So the initial Text property
            'of the RichTextBox should not be an empty string. It could be something like vbcrlf & vbcrlf & vbcrlf 
            If RTF.Lines.Count < 3 Then RTF.Text = vbCrLf & vbCrLf & vbCrLf
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
        Try

            Dim frm As Form = New frmSearch

            frm.Show() 'Dialog()

        Catch exc As Exception

            MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub ToolBoxCut_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxCut.Click
        MS_Editor.Cut()
    End Sub

    Private Sub ToolBoxyCopy_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxyCopy.Click
        MS_Editor.Copy()
    End Sub

    Private Sub ToolBoxPaste_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxPaste.Click
        MS_Editor.Paste()
    End Sub

    Private Sub ToolBoxUndo_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxUndo.Click
        MS_Editor.Undo()
    End Sub

    Private Sub ToolBoxRedo_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxRedo.Click
        MS_Editor.Redo()
    End Sub

    Private Sub GotoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GotoToolStripMenuItem.Click, ToolStripButton1.Click
        Dim i As String = _
InputBox("What location within the document do you want to send the cursor to?", _
" Location to send the Cursor?", "0")

        If Char.IsNumber(i) Then

            MS_Editor.SelectionStart = i

            sb.Panels.Item(0).Text = "Cursor Position: " & MS_Editor.SelectionStart.ToString
            sb.Panels.Item(1).Text = "Current Line: " & MS_Editor.GetLineFromCharIndex(MS_Editor.SelectionStart) + 1

        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        MS_Editor.Undo()
    End Sub

    Private Sub RedoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        MS_Editor.Redo()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveAs()
    End Sub

    Private Sub ToolBoxSaveAs_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxSaveAs.Click
        SaveAs()
    End Sub

    Private Sub NewFile()
        If Not CanOpen(TabControl2.SelectedIndex) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(WorkPath(TabControl2.SelectedIndex), WorkFileName(TabControl2.SelectedIndex))
                'Else
                '    'Process No

            End If
        End If
        MS_Editor.Text = NewMSFile()
        If MS_Editor.Text <> "" Then RTBWrapper.colorDocument()
        WorkFileName(TabControl2.SelectedIndex) = ""
        frmTitle(TabControl2.SelectedIndex) = "DSeX - New Dragon Speak File"
        lblStatus.Text = "Status: New Monkey Speak Script"

        Me.Text = frmTitle(TabControl2.SelectedIndex)

        CanOpen(TabControl2.SelectedIndex) = True

    End Sub

    Private Sub ToolBoxNew_Click(sender As System.Object, e As System.EventArgs) Handles ToolBoxNew.Click, NewMonkeySpeakToolStripMenuItem.Click
        AddNewEditorTab("New File", mPath, TabControl2.TabCount.ToString)
        NewFile()
    End Sub

    Private Sub FixIndentsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FixIndentsToolStripMenuItem.Click
        Dim StrArray() As String

        StrArray = MS_Editor.Lines
        Dim str As String
        Dim pattern(CInt(KeysIni.GetKeyValue("Init-Types", "Count"))) As String
        Dim pat(CInt(KeysIni.GetKeyValue("Init-Types", "Count"))) As Integer
        Dim T As String = " "

        For I As Integer = 1 To pattern.Length - 1
            T = KeysIni.GetKeyValue("Init-Types", I.ToString)
            Dim s As String = KeysIni.GetKeyValue("Indent-Lookup", T)
            Dim u As String = KeysIni.GetKeyValue("C-Indents", T)
            pattern(I) = "(" + s
            pat(I) = CInt(u)
        Next
        Dim insertPos As Integer = MS_Editor.SelectionStart
        For I As Integer = 0 To StrArray.Length - 1
            str = LTrim(StrArray(I) & vbCrLf)
            StrArray(I) = str
        Next
        MS_Editor.Text = ""
        For I As Integer = 0 To StrArray.Length - 1
            For N As Integer = 1 To pattern.Length - 1
                If StrArray(I).StartsWith(pattern(N)) Then
                    Dim count As Integer = pattern(N).Substring(1, 1)
                    StrArray(I) = StrDup(pat(N), " ") & StrArray(I)
                End If
            Next
            MS_Editor.Text += StrArray(I)
        Next
        RTBWrapper.colorDocument()
        MS_Editor.SelectionStart = insertPos
    End Sub


    Private Sub BtnComment_Click(sender As System.Object, e As System.EventArgs) Handles BtnComment.Click
        ApplyComment()
    End Sub

    Private Sub ApplyComment()


        Dim this As String = MS_Editor.SelectedText
        Dim str() As String = this.Split(Chr(10))
        Dim str2 As String = ""
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                str2 &= Chr(10) & "*" & str(i)
            Next
            MS_Editor.SelectedText = str2.Substring(1)
            RTBWrapper.colorDocument()
        End If
    End Sub

    Private Sub RemoveComment()


        Dim this As String = MS_Editor.SelectedText
        Dim str() As String = this.Split(Chr(10))
        Dim str2 As String = ""
        If str.Length > 1 Then
            For i As Integer = 0 To str.Length - 1
                If str(i).StartsWith("*") Then str(i) = str(i).Remove(0, 1)
                str2 &= Chr(10) & str(i)
            Next
            MS_Editor.SelectedText = str2.Substring(1)
            RTBWrapper.colorDocument()
        End If
    End Sub

    Private Sub ApplyCommentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ApplyCommentToolStripMenuItem.Click
        ApplyComment()
    End Sub

    Private Sub RemoveCommentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveCommentToolStripMenuItem.Click
        RemoveComment()
    End Sub

    Private Sub BtnUncomment_Click(sender As System.Object, e As System.EventArgs) Handles BtnUncomment.Click
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








    Private Sub AddNewEditorTab(ByRef FileName As String, FilePath As String, ByRef n As String)
        TabControl2.TabPages.Add(n)

        'Adds a new tab to your tab control
        CanOpen.Add(True)
        WorkFileName.Add(FileName)
        WorkPath.Add(FilePath)
        frmTitle.Add("DSeX - New DragonSpeak File")
        SectionIdx.Add(0)
        Dim intLastTabIndex As Integer = TabControl2.TabPages.Count - 1
        'Gets the index number of the last tab

        TabControl2.TabPages(intLastTabIndex).Name = "tbpageBrowser" & intLastTabIndex.ToString

        Dim Gutter As PictureBox = New PictureBox
        Gutter.Parent = TabControl2.TabPages(intLastTabIndex)
        Gutter.Location = New System.Drawing.Point(3, 3)


        Gutter.Anchor = AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Bottom
        Gutter.Name = "Gutter" + n
        Gutter.Width = 41
        Gutter.Height = TabControl2.TabPages(intLastTabIndex).Height
        'Creates the listview and displays it in the new tab
        Dim lstView As RichTextBox2 = New RichTextBox2


        lstView.Parent = TabControl2.TabPages(intLastTabIndex)
        lstView.Anchor = AnchorStyles.Left + AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        lstView.Name = "edit" + n
        AutocompleteMenu1.SetAutocompleteMenu(lstView, Me.AutocompleteMenu1)
        lstView.Location = New System.Drawing.Point(50, 3)
        'lstView.Size = New System.Drawing.Size(573, 187)
        lstView.Height = TabControl2.TabPages(intLastTabIndex).Height
        lstView.Width = TabControl2.TabPages(intLastTabIndex).Width - 41
        lstView.Show()
        lstView.ContextMenuStrip = ContextMenuStrip1
        With RTBWrapper
            .unbind()
            .bind(lstView)
            'tDict(Pattern, isRegex, isCase, value)
            'Variable
            .rtfSyntax.Clear()
            .rtfSyntax.add("%([A-Za-z0-9]+)", True, True, EditSettings.VariableColor.ToArgb)
            .rtfSyntax.add("~([A-Za-z0-9]+)", True, True, EditSettings.StringVariableColor.ToArgb)
            '.rtfSyntax.add("([0-9]*)\.?([0-9]*)", True, True, Color.Violet.ToArgb)
            'string
            .rtfSyntax.add("\\{(.*?)\\}", True, True, EditSettings.StringColor.ToArgb)
            'Line ID
            .rtfSyntax.add("\(([0-9]*)\:([0-9]*)\)", True, True, EditSettings.IDColor.ToArgb)
            'Comment
            .rtfSyntax.add("^\*(.*?)$", True, True, EditSettings.CommentColor.ToArgb)
            '.rtfSyntax.add("([0-9]+)\.([0-9]*)", True, True, Color.Violet.ToArgb)
            'Number
            .rtfSyntax.add(" ([0-9#]+)", True, True, EditSettings.NumberColor.ToArgb)
            .rtfSyntax.add("\.([0-9#]+)", True, True, EditSettings.NumberColor.ToArgb)
            '.rtfSyntax.add("<tr.*?>", True, True, Color.Brown.ToArgb)
            '.rtfSyntax.add("<td.*?>", True, True, Color.Brown.ToArgb)
            '.rtfSyntax.add("<img.*?>", True, True, Color.Red.ToArgb)
            .colorDocument()
        End With
        TabControl2.SelectedTab = TabControl2.TabPages(intLastTabIndex)
        TabControl2.SelectedTab.Text = FileName

        AddHandler lstView.UndoEvent, AddressOf undo
        AddHandler lstView.RedoEvent, AddressOf redo
        AddHandler lstView.CursorChanged, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.MouseClick, AddressOf MS_Editor_CursorChanged
        AddHandler lstView.KeyUp, AddressOf MS_Editor_CursorChanged
        AddHandler Gutter.Paint, AddressOf p_Paint
        AddHandler lstView.VScroll, AddressOf r_VScroll
        AddHandler lstView.Resize, AddressOf r_Resize
        AddHandler lstView.TextChanged, AddressOf MS_Editor_TextChanged
    End Sub

    Public Sub undo(sender As System.Object, e As No_Flicker.UndoRedoEventArgs)
        lblStatus.Text = "Status: Undo"
    End Sub
    Public Sub redo(sender As System.Object, e As No_Flicker.UndoRedoEventArgs)
        lblStatus.Text = "Status: Redo"
    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControl2.SelectedIndexChanged
        If TabControl2.SelectedIndex = -1 Then Exit Sub
        RTBWrapper.unbind()
        RTBWrapper.bind(MS_Editor)
        If CanOpen(TabControl2.SelectedIndex) = False Then
            Me.Text = frmTitle(TabControl2.SelectedIndex) + "*"
        Else
            Me.Text = frmTitle(TabControl2.SelectedIndex)
        End If

        ListBox1.SelectedIndex = SectionIdx(TabControl2.SelectedIndex)
    End Sub


    Private Sub TabControl2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim x As New ContextMenuStrip
            Dim s As ToolStripItem = x.Items.Add("New Tab", Nothing, AddressOf FNewTab_Click)
            s.Tag = sender
            Dim t As ToolStripItem = x.Items.Add("Close Tab", Nothing, AddressOf FCloseTab_Click)
            Dim u As ToolStripItem = x.Items.Add("Close All Tabs But this one", Nothing, AddressOf FCloseAllTab_Click)

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
        End If
    End Sub

    Private Sub CloseAllButThis(ByRef i As Integer)

        For j = TabControl2.TabPages.Count - 1 To 0 Step -1
            If i <> j Then CloseTab(j)
        Next
    End Sub
    Private Sub FCloseAllTab_Click(sender As System.Object, e As System.EventArgs)

        CloseAllButThis(sender.Tag)
    End Sub
    Private Sub FCloseTab_Click(sender As System.Object, e As System.EventArgs)

        CloseTab(sender.Tag)
    End Sub

    Private Sub CloseTab(ByVal i As Integer)
        If Not CanOpen(i) Then
            Dim reply As DialogResult = MessageBox.Show("Save changes? Yes or No", "Caption", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                SaveMS_File(WorkPath(i), WorkFileName(i))
            End If
        End If
        TabControl2.TabPages.RemoveAt(i)
        CanOpen.RemoveAt(i)
        WorkFileName.RemoveAt(i)
        WorkPath.RemoveAt(i)
        frmTitle.RemoveAt(i)
        SectionIdx.RemoveAt(i)
    End Sub


    Private Sub FNewTab_Click(sender As System.Object, e As System.EventArgs)
        Dim c As Integer = TabControl2.TabCount
        AddNewEditorTab("New.ds", mPath, c.ToString)
        NewFile()
    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox2.DoubleClick
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

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        SectionIdx(TabControl2.SelectedIndex) = ListBox1.SelectedIndex
    End Sub
End Class