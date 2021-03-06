Public Class frmSearch
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnReplace As System.Windows.Forms.Button
    Friend WithEvents btnReplaceAll As System.Windows.Forms.Button
    Friend WithEvents chkMatchCase As System.Windows.Forms.CheckBox
    Friend WithEvents optWhole As System.Windows.Forms.RadioButton
    Friend WithEvents cmbReplace As System.Windows.Forms.ComboBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblReplace As System.Windows.Forms.Label
    Friend WithEvents btnFindNext As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents optPre As System.Windows.Forms.RadioButton
    Friend WithEvents optSub As System.Windows.Forms.RadioButton
    Friend WithEvents optNone As System.Windows.Forms.RadioButton
    Friend WithEvents chkReverse As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnTop As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbSearch = New System.Windows.Forms.ComboBox()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.btnReplaceAll = New System.Windows.Forms.Button()
        Me.chkMatchCase = New System.Windows.Forms.CheckBox()
        Me.optWhole = New System.Windows.Forms.RadioButton()
        Me.optPre = New System.Windows.Forms.RadioButton()
        Me.optSub = New System.Windows.Forms.RadioButton()
        Me.cmbReplace = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.lblReplace = New System.Windows.Forms.Label()
        Me.btnFindNext = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.optNone = New System.Windows.Forms.RadioButton()
        Me.chkReverse = New System.Windows.Forms.CheckBox()
        Me.chkOnTop = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cmbSearch
        '
        Me.cmbSearch.Location = New System.Drawing.Point(8, 24)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(256, 21)
        Me.cmbSearch.TabIndex = 0
        '
        'btnFind
        '
        Me.btnFind.Enabled = False
        Me.btnFind.Location = New System.Drawing.Point(272, 8)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 23)
        Me.btnFind.TabIndex = 1
        Me.btnFind.Text = "&Find"
        '
        'btnReplace
        '
        Me.btnReplace.Enabled = False
        Me.btnReplace.Location = New System.Drawing.Point(272, 64)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(75, 23)
        Me.btnReplace.TabIndex = 2
        Me.btnReplace.Text = "Repla&ce"
        '
        'btnReplaceAll
        '
        Me.btnReplaceAll.Enabled = False
        Me.btnReplaceAll.Location = New System.Drawing.Point(272, 88)
        Me.btnReplaceAll.Name = "btnReplaceAll"
        Me.btnReplaceAll.Size = New System.Drawing.Size(75, 23)
        Me.btnReplaceAll.TabIndex = 3
        Me.btnReplaceAll.Text = "Replace &All"
        '
        'chkMatchCase
        '
        Me.chkMatchCase.Location = New System.Drawing.Point(160, 104)
        Me.chkMatchCase.Name = "chkMatchCase"
        Me.chkMatchCase.Size = New System.Drawing.Size(104, 24)
        Me.chkMatchCase.TabIndex = 4
        Me.chkMatchCase.Text = "Match &Case"
        '
        'optWhole
        '
        Me.optWhole.Checked = True
        Me.optWhole.Location = New System.Drawing.Point(8, 104)
        Me.optWhole.Name = "optWhole"
        Me.optWhole.Size = New System.Drawing.Size(136, 16)
        Me.optWhole.TabIndex = 5
        Me.optWhole.TabStop = True
        Me.optWhole.Text = "Match - W&hole Word"
        '
        'optPre
        '
        Me.optPre.Location = New System.Drawing.Point(16, 208)
        Me.optPre.Name = "optPre"
        Me.optPre.Size = New System.Drawing.Size(128, 24)
        Me.optPre.TabIndex = 6
        Me.optPre.Text = "Match - Prefi&x"
        Me.optPre.Visible = False
        '
        'optSub
        '
        Me.optSub.Location = New System.Drawing.Point(16, 232)
        Me.optSub.Name = "optSub"
        Me.optSub.Size = New System.Drawing.Size(128, 24)
        Me.optSub.TabIndex = 7
        Me.optSub.Text = "Match - Sub&String"
        Me.optSub.Visible = False
        '
        'cmbReplace
        '
        Me.cmbReplace.Location = New System.Drawing.Point(8, 72)
        Me.cmbReplace.Name = "cmbReplace"
        Me.cmbReplace.Size = New System.Drawing.Size(256, 21)
        Me.cmbReplace.TabIndex = 8
        '
        'lblSearch
        '
        Me.lblSearch.Location = New System.Drawing.Point(8, 8)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(144, 16)
        Me.lblSearch.TabIndex = 9
        Me.lblSearch.Text = "S&earch For:"
        '
        'lblReplace
        '
        Me.lblReplace.Location = New System.Drawing.Point(8, 56)
        Me.lblReplace.Name = "lblReplace"
        Me.lblReplace.Size = New System.Drawing.Size(100, 16)
        Me.lblReplace.TabIndex = 10
        Me.lblReplace.Text = "Re&place With:"
        '
        'btnFindNext
        '
        Me.btnFindNext.Enabled = False
        Me.btnFindNext.Location = New System.Drawing.Point(272, 32)
        Me.btnFindNext.Name = "btnFindNext"
        Me.btnFindNext.Size = New System.Drawing.Size(75, 23)
        Me.btnFindNext.TabIndex = 11
        Me.btnFindNext.Text = "Find &Next"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(272, 120)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "&Close"
        '
        'optNone
        '
        Me.optNone.Location = New System.Drawing.Point(8, 128)
        Me.optNone.Name = "optNone"
        Me.optNone.Size = New System.Drawing.Size(136, 16)
        Me.optNone.TabIndex = 13
        Me.optNone.Text = "Match - All Matches"
        '
        'chkReverse
        '
        Me.chkReverse.Location = New System.Drawing.Point(144, 200)
        Me.chkReverse.Name = "chkReverse"
        Me.chkReverse.Size = New System.Drawing.Size(104, 24)
        Me.chkReverse.TabIndex = 14
        Me.chkReverse.Text = "Bottom to Top"
        Me.chkReverse.Visible = False
        '
        'chkOnTop
        '
        Me.chkOnTop.Location = New System.Drawing.Point(160, 128)
        Me.chkOnTop.Name = "chkOnTop"
        Me.chkOnTop.Size = New System.Drawing.Size(104, 16)
        Me.chkOnTop.TabIndex = 15
        Me.chkOnTop.Text = "Keep On-Top"
        '
        'frmSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(354, 152)
        Me.Controls.Add(Me.chkOnTop)
        Me.Controls.Add(Me.chkReverse)
        Me.Controls.Add(Me.optNone)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnFindNext)
        Me.Controls.Add(Me.lblReplace)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.cmbReplace)
        Me.Controls.Add(Me.optSub)
        Me.Controls.Add(Me.optPre)
        Me.Controls.Add(Me.optWhole)
        Me.Controls.Add(Me.chkMatchCase)
        Me.Controls.Add(Me.btnReplaceAll)
        Me.Controls.Add(Me.btnReplace)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.cmbSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frmSearch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find and Replace"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        '
        '
        If MS_Edit.MS_Editor.TextLength <= 1 Then Exit Sub
        '
        'MS_Editor
        If optWhole.Checked Then

            If chkMatchCase.Checked Then

                If MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                     RichTextBoxFinds.WholeWord Or _
                         RichTextBoxFinds.MatchCase) = -1 Then

                    MessageBox.Show("Finished searching the document", " Finished Searching", _
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

                MS_Edit.MS_Editor.Focus()

            Else

                If MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                     RichTextBoxFinds.WholeWord) = -1 Then

                    MessageBox.Show("Finished searching the document", " Finished Searching", _
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

                MS_Edit.MS_Editor.Focus()
            End If

        ElseIf optPre.Checked Then

            If MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                    RichTextBoxFinds.WholeWord Or _
                        RichTextBoxFinds.MatchCase) = -1 Then

                MessageBox.Show("Finished searching the document", " Finished Searching", _
                    MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            MS_Edit.MS_Editor.Focus()

        ElseIf optSub.Checked Then

            MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                RichTextBoxFinds.WholeWord Or _
                    RichTextBoxFinds.MatchCase)

            MS_Edit.MS_Editor.Focus()

        ElseIf optNone.Checked Then


            If chkMatchCase.Checked Then

                If MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                     RichTextBoxFinds.None Or _
                         RichTextBoxFinds.MatchCase) = -1 Then

                    MessageBox.Show("Finished searching the document", " Finished Searching", _
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

                MS_Edit.MS_Editor.Focus()

            Else

                If MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                RichTextBoxFinds.None) = -1 Then

                    MessageBox.Show("Finished searching the document", " Finished Searching", _
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

                MS_Edit.MS_Editor.Focus()

            End If

        End If

        btnFindNext.Enabled = True

        MS_Edit.MS_Editor.Focus()

    End Sub

    Private Sub cmbSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSearch.TextChanged

        If cmbSearch.Text.Length > 0 Then

            btnFind.Enabled = True

        Else

            btnFind.Enabled = False
            btnFindNext.Enabled = False

        End If

    End Sub

    Private Sub cmbReplace_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReplace.TextChanged

        If cmbReplace.Text.Length > 0 Then

            btnReplace.Enabled = True
            btnReplaceAll.Enabled = True

        Else

            btnReplace.Enabled = False
            btnReplaceAll.Enabled = False

        End If

    End Sub

    Private Sub btnFindNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindNext.Click
        '
        '
        If MS_Edit.MS_Editor.TextLength <= 1 Then Exit Sub
        '
        '
        If optWhole.Checked Then

            If chkMatchCase.Checked Then
                '
                If MS_Edit.MS_Editor.SelectionStart = MS_Edit.MS_Editor.TextLength Then

                    MessageBox.Show("Search has reached the end of the document.", " Finished Search", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub

                End If
                '
                'A search string was already selected and the user wants to find
                'more occurrences of the same string. So start searching at the
                'postition of the cursor.
                MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                    MS_Edit.MS_Editor.SelectionStart + 1, _
                    RichTextBoxFinds.WholeWord Or _
                        RichTextBoxFinds.MatchCase)

            Else
                '
                If MS_Edit.MS_Editor.SelectionStart = MS_Edit.MS_Editor.TextLength Then

                    MessageBox.Show("Search has reached the end of the document.", " Finished Search", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub

                End If
                '
                'A search string was already selected and the user wants to find
                'more occurrences of the same string. So start searching at the
                'postition of the cursor.
                MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                    MS_Edit.MS_Editor.SelectionStart + 1, _
                        RichTextBoxFinds.WholeWord)

            End If

        ElseIf optPre.Checked Then

        ElseIf optSub.Checked Then

        ElseIf optNone.Checked Then

            If chkMatchCase.Checked Then
                '
                If MS_Edit.MS_Editor.SelectionStart = MS_Edit.MS_Editor.TextLength Then

                    MessageBox.Show("Search has reached the end of the document.", " Finished Search", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub

                End If
                '
                'A search string was already selected and the user wants to find
                'more occurrences of the same string. So start searching at the
                'postition of the cursor.
                MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                    MS_Edit.MS_Editor.SelectionStart + 1, _
                    RichTextBoxFinds.None Or _
                        RichTextBoxFinds.MatchCase)

            Else
                '
                If MS_Edit.MS_Editor.SelectionStart = MS_Edit.MS_Editor.TextLength Then

                    MessageBox.Show("Search has reached the end of the document.", " Finished Search", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Exit Sub

                End If
                '
                'A search string was already selected and the user wants to find
                'more occurrences of the same string. So start searching at the
                'postition of the cursor.
                MS_Edit.MS_Editor.Find(cmbSearch.Text, _
                    MS_Edit.MS_Editor.SelectionStart + 1, _
                        RichTextBoxFinds.None)

            End If

        End If

        MS_Edit.MS_Editor.Focus()

    End Sub

    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplace.Click

        If (MS_Edit.MS_Editor.SelectedText.ToLower) = (cmbSearch.Text.ToLower) Then

            MS_Edit.MS_Editor.SelectedText = cmbReplace.Text

        End If

    End Sub

    Private Sub btnReplaceAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplaceAll.Click

        Dim i As Integer
        Dim t As Integer = 0

        Do

            Application.DoEvents()

            btnFind.PerformClick()

            If (MS_Edit.MS_Editor.SelectedText.ToLower) = (cmbSearch.Text.ToLower) Then

                i = 1
                MS_Edit.MS_Editor.SelectedText = cmbReplace.Text

                t += 1

            Else

                i = 0

            End If

        Loop Until i < 1

        MessageBox.Show("A total of: " & t.ToString & " was found and replaced.", "Search/Replace All Finished", MessageBoxButtons.OK, MessageBoxIcon.Information)

        MS_Edit.lblStatus.Text = "Status: A total of: " & t.ToString & " was found and replaced."

    End Sub

    Private Sub chkOnTop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnTop.CheckedChanged

        If chkOnTop.Checked Then

            Me.TopMost = True

        Else

            Me.TopMost = False

        End If

    End Sub

End Class