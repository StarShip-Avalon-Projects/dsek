Imports System.IO
Imports DSeX.ConfigStructs
Imports System.Drawing.Text
Imports System.Drawing
Imports Furcadia.IO


Public Class Config

    Dim MyConfig As New ConfigStructs

    Private Sub BTN_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancel.Click
        'Close with out Saving
        Me.Close()
    End Sub

    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click
        'Editor settings

        EditSettings.AutoCompleteEnable = ChkBxAutoComplete.Checked
        ' MS_Edit.AutocompleteMenu1.Enabled = ChkBxAutoComplete.Checked


        EditSettings.CommentColor = CommentPictureBox.BackColor
        EditSettings.StringColor = StringPictureBox.BackColor
        EditSettings.NumberColor = NumberPictureBox.BackColor
        EditSettings.VariableColor = VariablePictureBox.BackColor
        EditSettings.IDColor = IDPictureBox.BackColor
        EditSettings.StringVariableColor = StringVariableClrBx.BackColor
        For i As Integer = 0 To ListBox1.Items.Count - 1
            Dim KV() As String = ListBox1.Items(i).split("=")
            ini.SetKeyValue("C-Indents", KV(0), KV(1))
        Next
        If RadioSpace.Checked Then
            ini.SetKeyValue("Init-Types", "Character", "Space")
        ElseIf RadioTab.Checked Then
            ini.SetKeyValue("Init-Types", "Character", "Tab")
        Else
            ini.SetKeyValue("Init-Types", "Character", "Space")
        End If
        'Save the settings to the ini file
        EditSettings.SaveEditorSettings()

        'If MS_Edit.Visible Then
        '    MS_Edit.Reset()
        'End If



        Me.Dispose()

    End Sub

    Private Sub Config_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.ConfigFormLocation = Me.Location
        My.Settings.Save()
    End Sub

    Private Sub Config_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConfigTabs.SelectedIndex = My.Settings.ConfigSelectedTab
        Loadconfig()
    End Sub

    Public Sub Loadconfig()

        EditSettings = New EditSettings
        'Editor

        ChkBxAutoComplete.Checked = EditSettings.AutoCompleteEnable

        CommentPictureBox.BackColor = EditSettings.CommentColor
        StringPictureBox.BackColor = EditSettings.StringColor
        NumberPictureBox.BackColor = EditSettings.NumberColor
        VariablePictureBox.BackColor = EditSettings.VariableColor
        IDPictureBox.BackColor = EditSettings.IDColor
        StringVariableClrBx.BackColor = EditSettings.StringVariableColor
        Dim count As Integer = ini.GetKeyValue("Init-Types", "Count").ToInteger
        For i = 1 To count
            Dim key As String = ini.GetKeyValue("Init-Types", i.ToString)
            Dim s As String = ini.GetKeyValue("C-Indents", key)
            ListBox1.Items.Add(key + "=" + s)
        Next
        Dim indentType As String = ini.GetKeyValue("Init-Types", "Character")
        If indentType = "Space" Then
            RadioSpace.Checked = True
        ElseIf indentType = "Tab" Then
            RadioTab.Checked = True
        End If
        Me.Location = My.Settings.ConfigFormLocation
    End Sub

    Private Sub ConfigTabs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ConfigTabs.SelectedIndexChanged
        My.Settings.ConfigSelectedTab = sender.SelectedIndex
    End Sub


    Public Sub GetColor(ByRef ColorBX As System.Windows.Forms.PictureBox)
        Dim dlg As New ColorDialog
        dlg.Color = ColorBX.BackColor
        If dlg.ShowDialog() = DialogResult.OK Then
            ColorBX.BackColor = dlg.Color
        End If
    End Sub

    Private Sub CommentPictureBox_Click(sender As System.Object, e As System.EventArgs) Handles CommentPictureBox.Click, StringPictureBox.Click, NumberPictureBox.Click, VariablePictureBox.Click, IDPictureBox.Click, StringVariableClrBx.Click
        GetColor(sender)
    End Sub

    Private Sub ChkBxAutoComplete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkBxAutoComplete.CheckedChanged
        EditSettings.AutoCompleteEnable = ChkBxAutoComplete.Checked
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

    End Sub

    Private Sub NumericUpDown1_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles NumericUpDown1.KeyUp

        Dim i As Integer = ListBox1.SelectedIndex
        If i = -1 Then Exit Sub
        Dim Key As String = ""
        Key = ini.GetKeyValue("Init-Types", i.ToString)

        ListBox1.Items.RemoveAt(i)
        ListBox1.Items.Insert(i, Key + "=" + NumericUpDown1.Value.ToString)

        ListBox1.SelectedIndex = i
    End Sub

    Private Sub ListBox1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseUp
        Dim str As String = ListBox1.SelectedItem
        Dim Val As Integer = str.Split("=")(1).ToInteger
        NumericUpDown1.Value = Val
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged

        Dim i As Integer = ListBox1.SelectedIndex
        If i = -1 Then Exit Sub
        Dim Key As String = ""
        Key = ini.GetKeyValue("Init-Types", i.ToString)

        ListBox1.Items.RemoveAt(i)
        ListBox1.Items.Insert(i, Key + "=" + NumericUpDown1.Value.ToString)
        ListBox1.SelectedIndex = i
    End Sub
End Class
