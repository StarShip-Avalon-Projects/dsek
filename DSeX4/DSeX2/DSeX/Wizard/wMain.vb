Imports System.IO
Imports DSeX.IniFile

'######### Please Read ##############
'
' This Project was originally not intended to be "open-source".
' However the source is released and if you use a lot of the source
' provided please, in your project, give credit where credit is due.
' 
' Sincerely,
' Squizzle (in-game)
'####################################


'Begin the main Form!
Public Class wMain

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadOptions()
        If OnToolStripMenuItem.Checked = True Then
            MyBase.Opacity = 0.0
            Timer1.Enabled = True
        End If
        'Gets the scripts.ini files in your "Scripts" folder
        GetScriptIni()
    End Sub

    Private Sub AboutToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AboutBox1.Show()
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        'ExitToolStrip
        If wUI.IsDisposed = False Then
            wUI.Dispose()
            If Me.OnToolStripMenuItem.Checked = True Then
                If OnToolStripMenuItem.Checked = True Then
                    Timer1.Enabled = False
                    Timer.Enabled = True
                End If
            Else
                Me.Dispose()
            End If

        Else
            If Me.OnToolStripMenuItem.Checked = True Then
                If OnToolStripMenuItem.Checked = True Then
                    Timer1.Enabled = False
                    Timer.Enabled = True
                End If
            Else
                Me.Dispose()
            End If
        End If

    End Sub

    Private Sub selecter_DoubleClick(sender As Object, e As System.EventArgs) Handles selecter.DoubleClick
        Dim sIni = selecter.GetItemText(selecter.SelectedItem)
        If wUI.IsDisposed = False Then
            wUI.Dispose()
        End If
        If IO.File.Exists(My.Application.Info.DirectoryPath() & "\help.txt") Then
            wUI.dsdesc2.Text = FileIO.FileSystem.ReadAllText(My.Application.Info.DirectoryPath() & "\help.txt")
        Else
            wUI.dsdesc2.Text = "Error: " & My.Application.Info.DirectoryPath() & "\help.txt" & " doesn't exist.  Help contents cannot be displayed."
        End If
        wUI.PathIndex = selecter.SelectedIndex
        GetParams(ScriptPaths(selecter.SelectedIndex) & sIni & ".ini")
        wUI.wVariables.Clear()
        wUI.NumericUpDown1.Value = 1
        wUI.wVariables.Clear()
        wUI.Show()
    End Sub

    Private Sub selecter_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles selecter.DragOver

    End Sub

    Private Sub selecter_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles selecter.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            sender.SelectedIndex = sender.IndexFromPoint(New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub selecter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selecter.SelectedIndexChanged

        If IO.File.Exists(ScriptPaths(selecter.SelectedIndex) & selecter.GetItemText(selecter.SelectedItem) & ".ini") Then
            Dim sIni = selecter.GetItemText(selecter.SelectedItem)
            GetInfo(ScriptPaths(selecter.SelectedIndex) & sIni & ".ini")

        End If
    End Sub



    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        'AboutToolStrip
        AboutBox1.Show()
    End Sub

    Public Sub GetParams(ByVal sIni As String)
        Try

            wUI.Code = ScriptIni.Code
            wUI.ListBox1.Items.Clear()
            wUI.selecter2.Items.Clear()
            Dim s As String = " "
            Dim t As String = ""
            Dim i As Integer = 1
            Do While s <> ""
                s = ScriptIni.GetKeyValue("main", "v" + i.ToString)
                If s <> "" Then wUI.selecter2.Items.Add(s)
                t = ScriptIni.GetKeyValue("main", "m" + i.ToString)
                If t = "" And s <> "" Then t = "text"
                If t <> "" Then wUI.ListBox1.Items.Add(t)
                i += 1
            Loop
            wUI.SetUI()

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.Exclamation, "Error!")
        End Try
        wUI.Text = FileIO.FileSystem.GetName(sIni)
    End Sub

    Public Sub GetInfo(ByVal sIni As String)
        Try
            ScriptIni.Load(sIni)
            dsdesc.Text = ""
            Dim s As String = " "
            Dim i As Integer = 1
            s = ScriptIni.GetKeyValue("main", "name")
            If s <> "" Then dsdesc.AppendText("Name: " + s + vbLf)
            s = ScriptIni.GetKeyValue("main", "Author")
            If s <> "" Then dsdesc.AppendText("Author: " + s + vbLf)
            Do While s <> ""
                s = ScriptIni.GetKeyValue("main", "d" + i.ToString)
                dsdesc.AppendText(s + vbLf)
                i += 1
            Loop
            s = ScriptIni.GetKeyValue("main", "DefaultRepeat")
            If IsInteger(s) Then
                wUI.NumericUpDown1.Value = s.ToInteger
            Else
                wUI.NumericUpDown1.Value = 0
                end if

        Catch ex As Exception
            Dim x As New ErrorLogging(ex, Me)
        End Try

    End Sub

    Private Sub GetScriptIni()
        selecter.Items.Clear()
        Dim path As String = Application.StartupPath + "/Scripts/"
        Directory.CreateDirectory(path)
        ScriptPaths.Clear()
        Dim x As Integer = 0
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ini").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            selecter.Items.Add(s.Remove(s.Length - 4, 4))
            ScriptPaths.Add(path)
        Next
        path = Furcadia.IO.Paths.GetFurcadiaDocPath + "\Scripts\"
        Directory.CreateDirectory(path)
        For x = 0 To FileIO.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly, "*.ini").Count - 1
            Dim s = FileIO.FileSystem.GetName(FileIO.FileSystem.GetFiles(path).Item(x))
            selecter.Items.Add(s.Remove(s.Length - 4, 4))
            ScriptPaths.Add(path)
        Next
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        'fade stuffs
        Me.Opacity -= 0.01
        If Me.Opacity = 0 Then Me.Dispose()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'fade stuffs
        Me.Opacity += 0.01
        If Me.Opacity = 100 Then
            Me.Show()
        End If
    End Sub



    Private Sub OnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem1.Click
        MsgBox(sender.ToString)
    End Sub

    Private Sub OnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnToolStripMenuItem.Click
        If OnToolStripMenuItem.Checked = True And OnToolStripMenuItem.CheckState = CheckState.Checked Then
            ' IniWrite(AppPath & "\Settings", "Main", "Fade", 1)
        Else
            ' IniWrite(AppPath & "\Settings", "Main", "Fade", 0)
        End If

    End Sub


    Private Sub LoadOptions()
        Dim a As Integer = 0
        'Dim a = IniRead(AppPath & "\Settings", "Main", "Fade", 1)
        If a = 0 Then
            OnToolStripMenuItem.Checked = False
            OnToolStripMenuItem.CheckState = CheckState.Unchecked
        Else
            OnToolStripMenuItem.Checked = True
            OnToolStripMenuItem.CheckState = CheckState.Checked
        End If

    End Sub


    Private Sub WizardRefresh_Click(sender As System.Object, e As System.EventArgs) Handles WizardRefresh.Click
        GetScriptIni()
    End Sub

    Private Sub NewScriptToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewScriptToolStripMenuItem.Click
        MS_Edit.AddNewEditorTab("", "", 0)
        MS_Edit.NewScript()
    End Sub

    Private Sub WizardEdit_Click(sender As System.Object, e As System.EventArgs) Handles WizardEdit.Click
        MS_Edit.AddNewEditorTab("", "", 0)
        MS_Edit.OpenMS_File(ScriptPaths.Item(selecter.SelectedIndex) + "/" + selecter.SelectedItem + ".ini")
    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        Dim s As String = Microsoft.VisualBasic.InputBox("New Name?")
        If String.IsNullOrEmpty(s) Then Exit Sub
        My.Computer.FileSystem.RenameFile(ScriptPaths.Item(selecter.SelectedIndex) + "/" + selecter.SelectedItem + ".ini", ScriptPaths(selecter.SelectedIndex) + s + ".ini")

    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        Dim reply As DialogResult = MessageBox.Show("Really delete this script?", "Caption", _
MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If reply = DialogResult.OK Then
            File.Delete(ScriptPaths.Item(selecter.SelectedIndex) + selecter.SelectedItem + ".ini")
            ScriptPaths.RemoveAt(selecter.SelectedIndex)
            selecter.Items.RemoveAt(selecter.SelectedIndex)
        End If
    End Sub
End Class
