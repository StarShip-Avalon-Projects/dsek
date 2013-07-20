<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> 
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wMain))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.INIFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.dsdesc = New System.Windows.Forms.RichTextBox()
        Me.selecter = New System.Windows.Forms.ListBox()
        Me.WizMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WizardRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.WizardEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.WizMenu.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.HelpToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(554, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem7})
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(35, 20)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripMenuItem7.Text = "Exit"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FadeToolStripMenuItem, Me.INIFormatToolStripMenuItem})
        Me.ToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(58, 20)
        Me.ToolStripMenuItem2.Text = "Settings"
        '
        'FadeToolStripMenuItem
        '
        Me.FadeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem})
        Me.FadeToolStripMenuItem.Name = "FadeToolStripMenuItem"
        Me.FadeToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.FadeToolStripMenuItem.Text = "Fade"
        '
        'OnToolStripMenuItem
        '
        Me.OnToolStripMenuItem.CheckOnClick = True
        Me.OnToolStripMenuItem.Name = "OnToolStripMenuItem"
        Me.OnToolStripMenuItem.Size = New System.Drawing.Size(88, 22)
        Me.OnToolStripMenuItem.Text = "On"
        Me.OnToolStripMenuItem.ToolTipText = "Fade in effect? Yes/No"
        '
        'INIFormatToolStripMenuItem
        '
        Me.INIFormatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem1})
        Me.INIFormatToolStripMenuItem.Name = "INIFormatToolStripMenuItem"
        Me.INIFormatToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.INIFormatToolStripMenuItem.Text = "INI Format"
        '
        'OnToolStripMenuItem1
        '
        Me.OnToolStripMenuItem1.Checked = True
        Me.OnToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.OnToolStripMenuItem1.Name = "OnToolStripMenuItem1"
        Me.OnToolStripMenuItem1.Size = New System.Drawing.Size(88, 22)
        Me.OnToolStripMenuItem1.Text = "On"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem8})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(103, 22)
        Me.ToolStripMenuItem8.Text = "About"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.dsdesc, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.selecter, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 27)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.42211!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(552, 203)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'dsdesc
        '
        Me.dsdesc.BackColor = System.Drawing.SystemColors.Window
        Me.dsdesc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dsdesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dsdesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dsdesc.Location = New System.Drawing.Point(279, 3)
        Me.dsdesc.Name = "dsdesc"
        Me.dsdesc.Size = New System.Drawing.Size(270, 197)
        Me.dsdesc.TabIndex = 0
        Me.dsdesc.Text = ""
        '
        'selecter
        '
        Me.selecter.BackColor = System.Drawing.SystemColors.Window
        Me.selecter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.selecter.ContextMenuStrip = Me.WizMenu
        Me.selecter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.selecter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.selecter.FormattingEnabled = True
        Me.selecter.Location = New System.Drawing.Point(3, 3)
        Me.selecter.Name = "selecter"
        Me.selecter.Size = New System.Drawing.Size(270, 197)
        Me.selecter.TabIndex = 2
        '
        'WizMenu
        '
        Me.WizMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WizardRefresh, Me.ToolStripSeparator1, Me.WizardEdit, Me.NewScriptToolStripMenuItem, Me.RemoveToolStripMenuItem, Me.RenameToolStripMenuItem})
        Me.WizMenu.Name = "WizMenu"
        Me.WizMenu.Size = New System.Drawing.Size(153, 142)
        Me.WizMenu.Text = "Refresh"
        '
        'WizardRefresh
        '
        Me.WizardRefresh.Name = "WizardRefresh"
        Me.WizardRefresh.Size = New System.Drawing.Size(152, 22)
        Me.WizardRefresh.Text = "Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'WizardEdit
        '
        Me.WizardEdit.Name = "WizardEdit"
        Me.WizardEdit.Size = New System.Drawing.Size(152, 22)
        Me.WizardEdit.Text = "Edit"
        '
        'NewScriptToolStripMenuItem
        '
        Me.NewScriptToolStripMenuItem.Name = "NewScriptToolStripMenuItem"
        Me.NewScriptToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.NewScriptToolStripMenuItem.Text = "New Script"
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RemoveToolStripMenuItem.Text = "Delete"
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'Timer
        '
        Me.Timer.Interval = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'wMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(554, 231)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.MenuStrip)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MaximizeBox = False
        Me.Name = "wMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Draconian Magic"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.WizMenu.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetScriptsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShareScriptsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents selecter As System.Windows.Forms.ListBox
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FadeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents INIFormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dsdesc As System.Windows.Forms.RichTextBox
    Friend WithEvents WizMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents WizardRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents WizardEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewScriptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
