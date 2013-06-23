<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Config
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config))
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.IniBrowseDialog = New System.Windows.Forms.OpenFileDialog()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ChkBxAutoComplete = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.IDPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.NumberPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.VariablePictureBox = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CommentPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.StringPictureBox = New System.Windows.Forms.PictureBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.IDPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumberPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VariablePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CommentPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StringPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ConfigTabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'BTN_Ok
        '
        Me.BTN_Ok.Location = New System.Drawing.Point(201, 274)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Ok.TabIndex = 10
        Me.BTN_Ok.Text = "Ok"
        Me.BTN_Ok.UseVisualStyleBackColor = True
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.Location = New System.Drawing.Point(287, 274)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Cancel.TabIndex = 9
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = True
        '
        'IniBrowseDialog
        '
        Me.IniBrowseDialog.DefaultExt = "ini"
        Me.IniBrowseDialog.Filter = "Furcadia Character Files)|*.ini"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ChkBxAutoComplete)
        Me.TabPage5.Controls.Add(Me.GroupBox8)
        Me.TabPage5.Controls.Add(Me.GroupBox7)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(371, 230)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Editor Settings"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ChkBxAutoComplete
        '
        Me.ChkBxAutoComplete.AutoSize = True
        Me.ChkBxAutoComplete.Checked = True
        Me.ChkBxAutoComplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkBxAutoComplete.Location = New System.Drawing.Point(141, 172)
        Me.ChkBxAutoComplete.Name = "ChkBxAutoComplete"
        Me.ChkBxAutoComplete.Size = New System.Drawing.Size(131, 17)
        Me.ChkBxAutoComplete.TabIndex = 2
        Me.ChkBxAutoComplete.Text = "Enable Auto Complete"
        Me.ChkBxAutoComplete.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label20)
        Me.GroupBox8.Controls.Add(Me.IDPictureBox)
        Me.GroupBox8.Controls.Add(Me.Label15)
        Me.GroupBox8.Controls.Add(Me.NumberPictureBox)
        Me.GroupBox8.Controls.Add(Me.Label19)
        Me.GroupBox8.Controls.Add(Me.VariablePictureBox)
        Me.GroupBox8.Controls.Add(Me.Label3)
        Me.GroupBox8.Controls.Add(Me.CommentPictureBox)
        Me.GroupBox8.Controls.Add(Me.Label4)
        Me.GroupBox8.Controls.Add(Me.StringPictureBox)
        Me.GroupBox8.Location = New System.Drawing.Point(199, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(166, 140)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Syntax Highlighting"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(34, 109)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(68, 13)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Line ID Color"
        '
        'IDPictureBox
        '
        Me.IDPictureBox.BackColor = System.Drawing.Color.Lime
        Me.IDPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.IDPictureBox.Location = New System.Drawing.Point(108, 109)
        Me.IDPictureBox.Name = "IDPictureBox"
        Me.IDPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.IDPictureBox.TabIndex = 41
        Me.IDPictureBox.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(29, 69)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 13)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "Number Color"
        '
        'NumberPictureBox
        '
        Me.NumberPictureBox.BackColor = System.Drawing.Color.Lime
        Me.NumberPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.NumberPictureBox.Location = New System.Drawing.Point(108, 69)
        Me.NumberPictureBox.Name = "NumberPictureBox"
        Me.NumberPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.NumberPictureBox.TabIndex = 39
        Me.NumberPictureBox.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(30, 89)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 13)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "Variable Color"
        '
        'VariablePictureBox
        '
        Me.VariablePictureBox.BackColor = System.Drawing.Color.Lime
        Me.VariablePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.VariablePictureBox.Location = New System.Drawing.Point(108, 89)
        Me.VariablePictureBox.Name = "VariablePictureBox"
        Me.VariablePictureBox.Size = New System.Drawing.Size(15, 14)
        Me.VariablePictureBox.TabIndex = 37
        Me.VariablePictureBox.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Comment Color"
        '
        'CommentPictureBox
        '
        Me.CommentPictureBox.BackColor = System.Drawing.Color.Lime
        Me.CommentPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CommentPictureBox.Location = New System.Drawing.Point(108, 29)
        Me.CommentPictureBox.Name = "CommentPictureBox"
        Me.CommentPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.CommentPictureBox.TabIndex = 35
        Me.CommentPictureBox.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(40, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "String Color"
        '
        'StringPictureBox
        '
        Me.StringPictureBox.BackColor = System.Drawing.Color.Lime
        Me.StringPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StringPictureBox.Location = New System.Drawing.Point(108, 49)
        Me.StringPictureBox.Name = "StringPictureBox"
        Me.StringPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.StringPictureBox.TabIndex = 33
        Me.StringPictureBox.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.NumericUpDown3)
        Me.GroupBox7.Controls.Add(Me.NumericUpDown2)
        Me.GroupBox7.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox7.Controls.Add(Me.Label18)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Controls.Add(Me.Label16)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(187, 140)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "GroupBox7"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(114, 36)
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(33, 20)
        Me.NumericUpDown3.TabIndex = 22
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Location = New System.Drawing.Point(114, 88)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(33, 20)
        Me.NumericUpDown2.TabIndex = 21
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(114, 62)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(33, 20)
        Me.NumericUpDown1.TabIndex = 20
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(24, 38)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 13)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Cause Indent"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(24, 90)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(73, 13)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Effects Indent"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(24, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 13)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "Condition Indent"
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPage5)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 12)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(379, 256)
        Me.ConfigTabs.TabIndex = 11
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(405, 309)
        Me.Controls.Add(Me.ConfigTabs)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.DSeX.My.MySettings.Default, "ConfigFormLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = Global.DSeX.My.MySettings.Default.ConfigFormLocation
        Me.Name = "Config"
        Me.Text = "Configuration"
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.IDPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumberPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VariablePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CommentPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StringPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ConfigTabs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents BTN_Cancel As System.Windows.Forms.Button
    Friend WithEvents IniBrowseDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents IDPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents NumberPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents VariablePictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CommentPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents StringPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ChkBxAutoComplete As System.Windows.Forms.CheckBox
End Class

