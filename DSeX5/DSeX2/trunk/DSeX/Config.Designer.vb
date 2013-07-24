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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StringVariableClrBx = New System.Windows.Forms.PictureBox()
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
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioSpace = New System.Windows.Forms.RadioButton()
        Me.RadioTab = New System.Windows.Forms.RadioButton()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.StringVariableClrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IDPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumberPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VariablePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CommentPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StringPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ConfigTabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox8.Controls.Add(Me.Label1)
        Me.GroupBox8.Controls.Add(Me.StringVariableClrBx)
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
        Me.GroupBox8.Location = New System.Drawing.Point(117, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(166, 160)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Syntax Highlighting"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "~String Variable Color"
        '
        'StringVariableClrBx
        '
        Me.StringVariableClrBx.BackColor = System.Drawing.Color.Lime
        Me.StringVariableClrBx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StringVariableClrBx.Location = New System.Drawing.Point(132, 129)
        Me.StringVariableClrBx.Name = "StringVariableClrBx"
        Me.StringVariableClrBx.Size = New System.Drawing.Size(15, 14)
        Me.StringVariableClrBx.TabIndex = 43
        Me.StringVariableClrBx.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(58, 110)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(68, 13)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Line ID Color"
        '
        'IDPictureBox
        '
        Me.IDPictureBox.BackColor = System.Drawing.Color.Lime
        Me.IDPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.IDPictureBox.Location = New System.Drawing.Point(132, 110)
        Me.IDPictureBox.Name = "IDPictureBox"
        Me.IDPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.IDPictureBox.TabIndex = 41
        Me.IDPictureBox.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(53, 70)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 13)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "Number Color"
        '
        'NumberPictureBox
        '
        Me.NumberPictureBox.BackColor = System.Drawing.Color.Lime
        Me.NumberPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.NumberPictureBox.Location = New System.Drawing.Point(132, 70)
        Me.NumberPictureBox.Name = "NumberPictureBox"
        Me.NumberPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.NumberPictureBox.TabIndex = 39
        Me.NumberPictureBox.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(43, 90)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "% Variable Color"
        '
        'VariablePictureBox
        '
        Me.VariablePictureBox.BackColor = System.Drawing.Color.Lime
        Me.VariablePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.VariablePictureBox.Location = New System.Drawing.Point(132, 90)
        Me.VariablePictureBox.Name = "VariablePictureBox"
        Me.VariablePictureBox.Size = New System.Drawing.Size(15, 14)
        Me.VariablePictureBox.TabIndex = 37
        Me.VariablePictureBox.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Comment Color"
        '
        'CommentPictureBox
        '
        Me.CommentPictureBox.BackColor = System.Drawing.Color.Lime
        Me.CommentPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CommentPictureBox.Location = New System.Drawing.Point(132, 30)
        Me.CommentPictureBox.Name = "CommentPictureBox"
        Me.CommentPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.CommentPictureBox.TabIndex = 35
        Me.CommentPictureBox.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(64, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "String Color"
        '
        'StringPictureBox
        '
        Me.StringPictureBox.BackColor = System.Drawing.Color.Lime
        Me.StringPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StringPictureBox.Location = New System.Drawing.Point(132, 50)
        Me.StringPictureBox.Name = "StringPictureBox"
        Me.StringPictureBox.Size = New System.Drawing.Size(15, 14)
        Me.StringPictureBox.TabIndex = 33
        Me.StringPictureBox.TabStop = False
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPage5)
        Me.ConfigTabs.Controls.Add(Me.TabPage1)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 12)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(379, 256)
        Me.ConfigTabs.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ListBox1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.NumericUpDown1)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(371, 230)
        Me.TabPage1.TabIndex = 5
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioTab)
        Me.GroupBox2.Controls.Add(Me.RadioSpace)
        Me.GroupBox2.Location = New System.Drawing.Point(185, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(87, 71)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Indent Type"
        '
        'RadioSpace
        '
        Me.RadioSpace.AutoSize = True
        Me.RadioSpace.Checked = True
        Me.RadioSpace.Location = New System.Drawing.Point(6, 19)
        Me.RadioSpace.Name = "RadioSpace"
        Me.RadioSpace.Size = New System.Drawing.Size(61, 17)
        Me.RadioSpace.TabIndex = 0
        Me.RadioSpace.TabStop = True
        Me.RadioSpace.Text = "Spaces"
        Me.RadioSpace.UseVisualStyleBackColor = True
        '
        'RadioTab
        '
        Me.RadioTab.AutoSize = True
        Me.RadioTab.Location = New System.Drawing.Point(6, 42)
        Me.RadioTab.Name = "RadioTab"
        Me.RadioTab.Size = New System.Drawing.Size(49, 17)
        Me.RadioTab.TabIndex = 1
        Me.RadioTab.Text = "Tabs"
        Me.RadioTab.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(185, 156)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDown1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(182, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Spaces of Indentation"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(240, 199)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(106, 25)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Reset to Defaults"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(20, 16)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(156, 160)
        Me.ListBox1.TabIndex = 5
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
        CType(Me.StringVariableClrBx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IDPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumberPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VariablePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CommentPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StringPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ConfigTabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents BTN_Cancel As System.Windows.Forms.Button
    Friend WithEvents IniBrowseDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StringVariableClrBx As System.Windows.Forms.PictureBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioTab As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSpace As System.Windows.Forms.RadioButton
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
End Class

