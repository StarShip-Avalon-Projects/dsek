Imports System.IO
Imports DSeX.ConfigStructs
Imports System.Drawing.Text
Imports System.Drawing
Imports Furcadia.IO


Public Class Config

    Dim MyConfig As New ConfigStructs



    Public EditSettings As EditSettings

    Private Sub BTN_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancel.Click
        'Close with out Saving
        Me.Close()
    End Sub

    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click
        'Editor settings

        EditSettings.AutoCompleteEnable = ChkBxAutoComplete.Checked
        MS_Edit.AutocompleteMenu1.Enabled = ChkBxAutoComplete.Checked

        EditSettings.CommentColor = CommentPictureBox.BackColor
        EditSettings.StringColor = StringPictureBox.BackColor
        EditSettings.NumberColor = NumberPictureBox.BackColor
        EditSettings.VariableColor = VariablePictureBox.BackColor
        EditSettings.IDColor = IDPictureBox.BackColor
        If MS_Edit.Visible Then
            MS_Edit.Reset()
        End If
        'Save the settings to the ini file

        EditSettings.SaveEditorSettings()


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

        EditSettings.LoadEditorSettings()



        'Editor


        ChkBxAutoComplete.Checked = EditSettings.AutoCompleteEnable

        CommentPictureBox.BackColor = EditSettings.CommentColor
        StringPictureBox.BackColor = EditSettings.StringColor
        NumberPictureBox.BackColor = EditSettings.NumberColor
        VariablePictureBox.BackColor = EditSettings.VariableColor
        IDPictureBox.BackColor = EditSettings.IDColor

        Me.Location = My.Settings.ConfigFormLocation
    End Sub

    Private Sub ConfigTabs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ConfigTabs.SelectedIndexChanged
        My.Settings.ConfigSelectedTab = sender.SelectedIndex
    End Sub


    Private Sub ChkBxAutoComplete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkBxAutoComplete.CheckedChanged
        EditSettings.AutoCompleteEnable = ChkBxAutoComplete.Checked
    End Sub
End Class
