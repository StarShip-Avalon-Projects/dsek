Imports No_Flicker
Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MS_Edit
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MS_Edit))
        Me.MSSaveDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MS_BrosweDialog = New System.Windows.Forms.OpenFileDialog()
        Me.EditMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewMonkeySpeakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditDropCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditDropCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditDropPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.FindReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FixIndentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GotoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApplyCommentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveCommentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DSWizardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.BtnSectionDelete = New System.Windows.Forms.Button()
        Me.BtnSectionAdd = New System.Windows.Forms.Button()
        Me.BtnSectionDown = New System.Windows.Forms.Button()
        Me.BtnSectionUp = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.BtnTemplateAdd = New System.Windows.Forms.Button()
        Me.BtnTemplateDelete = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.TabControl2 = New DSeX.TabControlEx()
        Me.ToolBox = New System.Windows.Forms.ToolStrip()
        Me.ToolBoxNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxOpen = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxSaveAs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolBoxCut = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxyCopy = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxPaste = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolBoxUndo = New System.Windows.Forms.ToolStripButton()
        Me.ToolBoxRedo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolBoxFindReplace = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.seperateor = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnComment = New System.Windows.Forms.ToolStripButton()
        Me.BtnUncomment = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblStatus = New System.Windows.Forms.ToolStripLabel()
        Me.TxtBxFind = New System.Windows.Forms.TextBox()
        Me.BtnFind = New System.Windows.Forms.Button()
        Me.sb = New System.Windows.Forms.StatusBar()
        Me.panelCurrentPosition = New System.Windows.Forms.StatusBarPanel()
        Me.panelCurrentLine = New System.Windows.Forms.StatusBarPanel()
        Me.panelTotalLines = New System.Windows.Forms.StatusBarPanel()
        Me.panelTotalCharacters = New System.Windows.Forms.StatusBarPanel()
        Me.Causes = New System.Windows.Forms.TabControl()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SectionMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NewSection = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteSection = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopySection = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutSection = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutocompleteMenu1 = New AutocompleteMenuNS.AutocompleteMenu()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.PasteSection = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditMenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ToolBox.SuspendLayout()
        CType(Me.panelCurrentPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelCurrentLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelTotalLines, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelTotalCharacters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SectionMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'MSSaveDialog
        '
        Me.MSSaveDialog.DefaultExt = "ds"
        Me.MSSaveDialog.Filter = "DragonSpeak Files)|*.ds"
        Me.MSSaveDialog.RestoreDirectory = True
        Me.MSSaveDialog.Title = "SaveAs"
        '
        'MS_BrosweDialog
        '
        Me.MS_BrosweDialog.DefaultExt = "ds"
        Me.MS_BrosweDialog.Filter = "DragonSpeak Files)|*.ds"
        '
        'EditMenu
        '
        Me.EditMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuCopy, Me.MenuCut, Me.PasteToolStripMenuItem, Me.ToolStripSeparator3})
        Me.EditMenu.Name = "EditMenu"
        Me.EditMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.EditMenu.ShowImageMargin = False
        Me.EditMenu.Size = New System.Drawing.Size(115, 76)
        '
        'MenuCopy
        '
        Me.MenuCopy.Name = "MenuCopy"
        Me.MenuCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.MenuCopy.Size = New System.Drawing.Size(114, 22)
        Me.MenuCopy.Text = "Copy"
        '
        'MenuCut
        '
        Me.MenuCut.Name = "MenuCut"
        Me.MenuCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K), System.Windows.Forms.Keys)
        Me.MenuCut.Size = New System.Drawing.Size(114, 22)
        Me.MenuCut.Text = "Cut"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(111, 6)
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "")
        Me.imgList.Images.SetKeyName(1, "")
        Me.imgList.Images.SetKeyName(2, "")
        Me.imgList.Images.SetKeyName(3, "")
        Me.imgList.Images.SetKeyName(4, "")
        Me.imgList.Images.SetKeyName(5, "")
        Me.imgList.Images.SetKeyName(6, "")
        Me.imgList.Images.SetKeyName(7, "")
        Me.imgList.Images.SetKeyName(8, "")
        Me.imgList.Images.SetKeyName(9, "")
        Me.imgList.Images.SetKeyName(10, "")
        Me.imgList.Images.SetKeyName(11, "")
        Me.imgList.Images.SetKeyName(12, "")
        Me.imgList.Images.SetKeyName(13, "")
        Me.imgList.Images.SetKeyName(14, "")
        Me.imgList.Images.SetKeyName(15, "")
        Me.imgList.Images.SetKeyName(16, "")
        Me.imgList.Images.SetKeyName(17, "")
        Me.imgList.Images.SetKeyName(18, "")
        Me.imgList.Images.SetKeyName(19, "")
        Me.imgList.Images.SetKeyName(20, "")
        Me.imgList.Images.SetKeyName(21, "")
        Me.imgList.Images.SetKeyName(22, "")
        Me.imgList.Images.SetKeyName(23, "")
        Me.imgList.Images.SetKeyName(24, "")
        Me.imgList.Images.SetKeyName(25, "")
        Me.imgList.Images.SetKeyName(26, "")
        Me.imgList.Images.SetKeyName(27, "")
        Me.imgList.Images.SetKeyName(28, "")
        Me.imgList.Images.SetKeyName(29, "")
        Me.imgList.Images.SetKeyName(30, "")
        Me.imgList.Images.SetKeyName(31, "")
        Me.imgList.Images.SetKeyName(32, "")
        Me.imgList.Images.SetKeyName(33, "")
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewMonkeySpeakToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem, Me.CloseToolStripMenuItem, Me.CloseAllToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewMonkeySpeakToolStripMenuItem
        '
        Me.NewMonkeySpeakToolStripMenuItem.Name = "NewMonkeySpeakToolStripMenuItem"
        Me.NewMonkeySpeakToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewMonkeySpeakToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.NewMonkeySpeakToolStripMenuItem.Text = "New File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.SaveAsToolStripMenuItem.Text = "SaveAs"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(150, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'CloseAllToolStripMenuItem
        '
        Me.CloseAllToolStripMenuItem.Name = "CloseAllToolStripMenuItem"
        Me.CloseAllToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.CloseAllToolStripMenuItem.Text = "Close All"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditDropCopy, Me.EditDropCut, Me.EditDropPaste, Me.ToolStripSeparator8, Me.FindReplaceToolStripMenuItem, Me.ToolStripSeparator6, Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'EditDropCopy
        '
        Me.EditDropCopy.Name = "EditDropCopy"
        Me.EditDropCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.EditDropCopy.Size = New System.Drawing.Size(194, 22)
        Me.EditDropCopy.Text = "Copy"
        '
        'EditDropCut
        '
        Me.EditDropCut.Name = "EditDropCut"
        Me.EditDropCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.EditDropCut.Size = New System.Drawing.Size(194, 22)
        Me.EditDropCut.Text = "Cut"
        '
        'EditDropPaste
        '
        Me.EditDropPaste.Name = "EditDropPaste"
        Me.EditDropPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.EditDropPaste.Size = New System.Drawing.Size(194, 22)
        Me.EditDropPaste.Text = "Paste"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(191, 6)
        '
        'FindReplaceToolStripMenuItem
        '
        Me.FindReplaceToolStripMenuItem.Name = "FindReplaceToolStripMenuItem"
        Me.FindReplaceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindReplaceToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FindReplaceToolStripMenuItem.Text = "&Find and Replace"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(191, 6)
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.UndoToolStripMenuItem.Text = "Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.RedoToolStripMenuItem.Text = "Redo"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.WindowsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(747, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FixIndentsToolStripMenuItem, Me.GotoToolStripMenuItem, Me.ApplyCommentToolStripMenuItem, Me.RemoveCommentToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'FixIndentsToolStripMenuItem
        '
        Me.FixIndentsToolStripMenuItem.Name = "FixIndentsToolStripMenuItem"
        Me.FixIndentsToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.FixIndentsToolStripMenuItem.Text = "Fix Indents"
        '
        'GotoToolStripMenuItem
        '
        Me.GotoToolStripMenuItem.Name = "GotoToolStripMenuItem"
        Me.GotoToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.GotoToolStripMenuItem.Text = "Goto Line"
        '
        'ApplyCommentToolStripMenuItem
        '
        Me.ApplyCommentToolStripMenuItem.Name = "ApplyCommentToolStripMenuItem"
        Me.ApplyCommentToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ApplyCommentToolStripMenuItem.Text = "Apply Comment"
        '
        'RemoveCommentToolStripMenuItem
        '
        Me.RemoveCommentToolStripMenuItem.Name = "RemoveCommentToolStripMenuItem"
        Me.RemoveCommentToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.RemoveCommentToolStripMenuItem.Text = "Remove Comment"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ConfigToolStripMenuItem
        '
        Me.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem"
        Me.ConfigToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.ConfigToolStripMenuItem.Text = "Config"
        '
        'WindowsToolStripMenuItem
        '
        Me.WindowsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DSWizardToolStripMenuItem})
        Me.WindowsToolStripMenuItem.Name = "WindowsToolStripMenuItem"
        Me.WindowsToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.WindowsToolStripMenuItem.Text = "Windows"
        '
        'DSWizardToolStripMenuItem
        '
        Me.DSWizardToolStripMenuItem.Name = "DSWizardToolStripMenuItem"
        Me.DSWizardToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.DSWizardToolStripMenuItem.Text = "DS Wizard"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AbutToolStripMenuItem
        '
        Me.AbutToolStripMenuItem.Name = "AbutToolStripMenuItem"
        Me.AbutToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.AbutToolStripMenuItem.Text = "About"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TxtBxFind)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnFind)
        Me.SplitContainer1.Panel2.Controls.Add(Me.sb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Causes)
        Me.SplitContainer1.Size = New System.Drawing.Size(747, 432)
        Me.SplitContainer1.SplitterDistance = 255
        Me.SplitContainer1.TabIndex = 5
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.TabControl2)
        Me.SplitContainer2.Size = New System.Drawing.Size(747, 230)
        Me.SplitContainer2.SplitterDistance = 121
        Me.SplitContainer2.TabIndex = 6
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(121, 230)
        Me.TabControl1.TabIndex = 9
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.BtnSectionDelete)
        Me.TabPage1.Controls.Add(Me.BtnSectionAdd)
        Me.TabPage1.Controls.Add(Me.BtnSectionDown)
        Me.TabPage1.Controls.Add(Me.BtnSectionUp)
        Me.TabPage1.Controls.Add(Me.ListBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(113, 204)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Sections"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'BtnSectionDelete
        '
        Me.BtnSectionDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSectionDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSectionDelete.Location = New System.Drawing.Point(73, 178)
        Me.BtnSectionDelete.Name = "BtnSectionDelete"
        Me.BtnSectionDelete.Size = New System.Drawing.Size(22, 23)
        Me.BtnSectionDelete.TabIndex = 9
        Me.BtnSectionDelete.Text = "-"
        Me.BtnSectionDelete.UseVisualStyleBackColor = True
        '
        'BtnSectionAdd
        '
        Me.BtnSectionAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSectionAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSectionAdd.Location = New System.Drawing.Point(50, 178)
        Me.BtnSectionAdd.Name = "BtnSectionAdd"
        Me.BtnSectionAdd.Size = New System.Drawing.Size(17, 23)
        Me.BtnSectionAdd.TabIndex = 9
        Me.BtnSectionAdd.Text = "+"
        Me.BtnSectionAdd.UseVisualStyleBackColor = True
        '
        'BtnSectionDown
        '
        Me.BtnSectionDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSectionDown.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnSectionDown.Location = New System.Drawing.Point(25, 178)
        Me.BtnSectionDown.Name = "BtnSectionDown"
        Me.BtnSectionDown.Size = New System.Drawing.Size(19, 23)
        Me.BtnSectionDown.TabIndex = 9
        Me.BtnSectionDown.Text = "â"
        Me.BtnSectionDown.UseVisualStyleBackColor = True
        '
        'BtnSectionUp
        '
        Me.BtnSectionUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSectionUp.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnSectionUp.Location = New System.Drawing.Point(-1, 178)
        Me.BtnSectionUp.Name = "BtnSectionUp"
        Me.BtnSectionUp.Size = New System.Drawing.Size(20, 23)
        Me.BtnSectionUp.TabIndex = 9
        Me.BtnSectionUp.Text = "á"
        Me.BtnSectionUp.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.ContextMenuStrip = Me.SectionMenu
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"Entire Document", "DS-Start", "   Default Section", "DS-End"})
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(107, 173)
        Me.ListBox1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BtnTemplateAdd)
        Me.TabPage2.Controls.Add(Me.BtnTemplateDelete)
        Me.TabPage2.Controls.Add(Me.ListBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(113, 204)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Templates"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'BtnTemplateAdd
        '
        Me.BtnTemplateAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnTemplateAdd.Location = New System.Drawing.Point(3, 178)
        Me.BtnTemplateAdd.Name = "BtnTemplateAdd"
        Me.BtnTemplateAdd.Size = New System.Drawing.Size(52, 23)
        Me.BtnTemplateAdd.TabIndex = 2
        Me.BtnTemplateAdd.Text = "Add"
        Me.BtnTemplateAdd.UseVisualStyleBackColor = True
        '
        'BtnTemplateDelete
        '
        Me.BtnTemplateDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnTemplateDelete.Location = New System.Drawing.Point(61, 178)
        Me.BtnTemplateDelete.Name = "BtnTemplateDelete"
        Me.BtnTemplateDelete.Size = New System.Drawing.Size(46, 23)
        Me.BtnTemplateDelete.TabIndex = 1
        Me.BtnTemplateDelete.Text = "Delete"
        Me.BtnTemplateDelete.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(3, 3)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(107, 173)
        Me.ListBox2.TabIndex = 0
        '
        'TabControl2
        '
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(622, 230)
        Me.TabControl2.TabIndex = 0
        '
        'ToolBox
        '
        Me.ToolBox.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBoxNew, Me.ToolBoxOpen, Me.ToolBoxSave, Me.ToolBoxSaveAs, Me.ToolStripSeparator2, Me.ToolBoxCut, Me.ToolBoxyCopy, Me.ToolBoxPaste, Me.ToolStripSeparator4, Me.ToolBoxUndo, Me.ToolBoxRedo, Me.ToolStripSeparator5, Me.ToolBoxFindReplace, Me.ToolStripButton1, Me.seperateor, Me.BtnComment, Me.BtnUncomment, Me.ToolStripSeparator7, Me.lblStatus})
        Me.ToolBox.Location = New System.Drawing.Point(0, 0)
        Me.ToolBox.Name = "ToolBox"
        Me.ToolBox.Size = New System.Drawing.Size(747, 25)
        Me.ToolBox.TabIndex = 5
        '
        'ToolBoxNew
        '
        Me.ToolBoxNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxNew.Image = Global.DSeX.My.Resources.Resources.NewDocumentHS
        Me.ToolBoxNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxNew.Name = "ToolBoxNew"
        Me.ToolBoxNew.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxNew.Text = "ToolStripButton2"
        Me.ToolBoxNew.ToolTipText = "New MS File"
        '
        'ToolBoxOpen
        '
        Me.ToolBoxOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxOpen.Image = Global.DSeX.My.Resources.Resources.OpenFile
        Me.ToolBoxOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxOpen.Name = "ToolBoxOpen"
        Me.ToolBoxOpen.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxOpen.Text = "ToolStripButton3"
        Me.ToolBoxOpen.ToolTipText = "Open MS file"
        '
        'ToolBoxSave
        '
        Me.ToolBoxSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxSave.Image = Global.DSeX.My.Resources.Resources.saveHS
        Me.ToolBoxSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxSave.Name = "ToolBoxSave"
        Me.ToolBoxSave.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxSave.Text = "ToolStripButton4"
        Me.ToolBoxSave.ToolTipText = "Save"
        '
        'ToolBoxSaveAs
        '
        Me.ToolBoxSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxSaveAs.Image = Global.DSeX.My.Resources.Resources.SaveAsWebPageHS
        Me.ToolBoxSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxSaveAs.Name = "ToolBoxSaveAs"
        Me.ToolBoxSaveAs.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxSaveAs.Text = "SaveAs"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBoxCut
        '
        Me.ToolBoxCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxCut.Image = Global.DSeX.My.Resources.Resources.CutHS
        Me.ToolBoxCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxCut.Name = "ToolBoxCut"
        Me.ToolBoxCut.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxCut.Text = "Cut"
        '
        'ToolBoxyCopy
        '
        Me.ToolBoxyCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxyCopy.Image = Global.DSeX.My.Resources.Resources.CopyHS
        Me.ToolBoxyCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxyCopy.Name = "ToolBoxyCopy"
        Me.ToolBoxyCopy.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxyCopy.Text = "Copy"
        '
        'ToolBoxPaste
        '
        Me.ToolBoxPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxPaste.Image = Global.DSeX.My.Resources.Resources.PasteHS
        Me.ToolBoxPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxPaste.Name = "ToolBoxPaste"
        Me.ToolBoxPaste.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxPaste.Text = "Paste"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBoxUndo
        '
        Me.ToolBoxUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxUndo.Image = Global.DSeX.My.Resources.Resources.Edit_UndoHS
        Me.ToolBoxUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxUndo.Name = "ToolBoxUndo"
        Me.ToolBoxUndo.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxUndo.Text = "ToolStripButton2"
        Me.ToolBoxUndo.ToolTipText = "Undo"
        '
        'ToolBoxRedo
        '
        Me.ToolBoxRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxRedo.Image = Global.DSeX.My.Resources.Resources.Edit_RedoHS
        Me.ToolBoxRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxRedo.Name = "ToolBoxRedo"
        Me.ToolBoxRedo.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxRedo.Text = "Redo"
        Me.ToolBoxRedo.ToolTipText = "Redo"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBoxFindReplace
        '
        Me.ToolBoxFindReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBoxFindReplace.Image = Global.DSeX.My.Resources.Resources.Find_VS
        Me.ToolBoxFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBoxFindReplace.Name = "ToolBoxFindReplace"
        Me.ToolBoxFindReplace.Size = New System.Drawing.Size(23, 22)
        Me.ToolBoxFindReplace.Text = "Find and Replace"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.DSeX.My.Resources.Resources.PageNumber
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolBoxGoto"
        Me.ToolStripButton1.ToolTipText = "Goto Line #"
        '
        'seperateor
        '
        Me.seperateor.Name = "seperateor"
        Me.seperateor.Size = New System.Drawing.Size(6, 25)
        '
        'BtnComment
        '
        Me.BtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnComment.Image = CType(resources.GetObject("BtnComment.Image"), System.Drawing.Image)
        Me.BtnComment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnComment.Name = "BtnComment"
        Me.BtnComment.Size = New System.Drawing.Size(23, 22)
        Me.BtnComment.Text = "ToolStripButton2"
        Me.BtnComment.ToolTipText = "Apply Comment"
        '
        'BtnUncomment
        '
        Me.BtnUncomment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnUncomment.Image = CType(resources.GetObject("BtnUncomment.Image"), System.Drawing.Image)
        Me.BtnUncomment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnUncomment.Name = "BtnUncomment"
        Me.BtnUncomment.Size = New System.Drawing.Size(23, 22)
        Me.BtnUncomment.Text = "ToolStripButton3"
        Me.BtnUncomment.ToolTipText = "Remove Comment"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(366, 22)
        Me.lblStatus.Text = "Status:"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBxFind
        '
        Me.AutocompleteMenu1.SetAutocompleteMenu(Me.TxtBxFind, Nothing)
        Me.TxtBxFind.Location = New System.Drawing.Point(12, 3)
        Me.TxtBxFind.Name = "TxtBxFind"
        Me.TxtBxFind.Size = New System.Drawing.Size(150, 20)
        Me.TxtBxFind.TabIndex = 8
        '
        'BtnFind
        '
        Me.BtnFind.Location = New System.Drawing.Point(168, 1)
        Me.BtnFind.Name = "BtnFind"
        Me.BtnFind.Size = New System.Drawing.Size(75, 23)
        Me.BtnFind.TabIndex = 7
        Me.BtnFind.Text = "Find"
        Me.BtnFind.UseVisualStyleBackColor = True
        '
        'sb
        '
        Me.sb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sb.Location = New System.Drawing.Point(0, 149)
        Me.sb.Name = "sb"
        Me.sb.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.panelCurrentPosition, Me.panelCurrentLine, Me.panelTotalLines, Me.panelTotalCharacters})
        Me.sb.ShowPanels = True
        Me.sb.Size = New System.Drawing.Size(747, 24)
        Me.sb.SizingGrip = False
        Me.sb.TabIndex = 6
        '
        'panelCurrentPosition
        '
        Me.panelCurrentPosition.Name = "panelCurrentPosition"
        Me.panelCurrentPosition.Text = "Cursor Position: 0"
        Me.panelCurrentPosition.ToolTipText = "Shows the current position within the document"
        Me.panelCurrentPosition.Width = 150
        '
        'panelCurrentLine
        '
        Me.panelCurrentLine.Name = "panelCurrentLine"
        Me.panelCurrentLine.Text = "Current Line:"
        Me.panelCurrentLine.ToolTipText = "Displays the current line number the cursor is on"
        '
        'panelTotalLines
        '
        Me.panelTotalLines.Name = "panelTotalLines"
        Me.panelTotalLines.Text = "Total Lines:"
        Me.panelTotalLines.ToolTipText = "Displays the total lines in the document"
        '
        'panelTotalCharacters
        '
        Me.panelTotalCharacters.Name = "panelTotalCharacters"
        Me.panelTotalCharacters.Text = "Total Characters: "
        Me.panelTotalCharacters.ToolTipText = "Displays the total length of the document in characters."
        Me.panelTotalCharacters.Width = 140
        '
        'Causes
        '
        Me.Causes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Causes.Location = New System.Drawing.Point(0, 30)
        Me.Causes.Name = "Causes"
        Me.Causes.SelectedIndex = 0
        Me.Causes.Size = New System.Drawing.Size(744, 113)
        Me.Causes.TabIndex = 5
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.DisplayIndex = 0
        Me.ColumnHeader3.Text = ""
        Me.ColumnHeader3.Width = 640
        '
        'SectionMenu
        '
        Me.SectionMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewSection, Me.DeleteSection, Me.ToolStripSeparator9, Me.CopySection, Me.CutSection, Me.PasteSection})
        Me.SectionMenu.Name = "ContextMenuStrip1"
        Me.SectionMenu.Size = New System.Drawing.Size(153, 142)
        '
        'NewSection
        '
        Me.NewSection.Name = "NewSection"
        Me.NewSection.Size = New System.Drawing.Size(152, 22)
        Me.NewSection.Text = "New Section"
        '
        'DeleteSection
        '
        Me.DeleteSection.Name = "DeleteSection"
        Me.DeleteSection.Size = New System.Drawing.Size(152, 22)
        Me.DeleteSection.Text = "Delete Section"
        '
        'CopySection
        '
        Me.CopySection.Name = "CopySection"
        Me.CopySection.Size = New System.Drawing.Size(152, 22)
        Me.CopySection.Text = "Copy Section"
        '
        'CutSection
        '
        Me.CutSection.Name = "CutSection"
        Me.CutSection.Size = New System.Drawing.Size(152, 22)
        Me.CutSection.Text = "Cut Section"
        '
        'AutocompleteMenu1
        '
        Me.AutocompleteMenu1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.AutocompleteMenu1.ImageList = Nothing
        Me.AutocompleteMenu1.Items = New String() {"abc", "abcd", "abcde"}
        Me.AutocompleteMenu1.MaximumSize = New System.Drawing.Size(500, 200)
        Me.AutocompleteMenu1.SearchPattern = "[ \w\.:=!<>\{\}]"
        Me.AutocompleteMenu1.TargetControlWrapper = Nothing
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(149, 6)
        '
        'PasteSection
        '
        Me.PasteSection.Name = "PasteSection"
        Me.PasteSection.Size = New System.Drawing.Size(152, 22)
        Me.PasteSection.Text = "Paste Section"
        '
        'MS_Edit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 456)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MS_Edit"
        Me.Text = "Monkey Speak Editor"
        Me.EditMenu.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ToolBox.ResumeLayout(False)
        Me.ToolBox.PerformLayout()
        CType(Me.panelCurrentPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelCurrentLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelTotalLines, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelTotalCharacters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SectionMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MSSaveDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MS_BrosweDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents EditMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents sb As System.Windows.Forms.StatusBar
    Friend WithEvents panelCurrentPosition As System.Windows.Forms.StatusBarPanel
    Friend WithEvents panelCurrentLine As System.Windows.Forms.StatusBarPanel
    Friend WithEvents panelTotalLines As System.Windows.Forms.StatusBarPanel
    Friend WithEvents panelTotalCharacters As System.Windows.Forms.StatusBarPanel
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditDropCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditDropCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditDropPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolBox As System.Windows.Forms.ToolStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolBoxNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBoxOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBoxSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolBoxCut As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBoxyCopy As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBoxPaste As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolBoxUndo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolBoxRedo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolBoxFindReplace As System.Windows.Forms.ToolStripButton
    Friend WithEvents seperateor As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AbutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GotoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolBoxSaveAs As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewMonkeySpeakToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnComment As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnUncomment As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FixIndentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApplyCommentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveCommentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtBxFind As System.Windows.Forms.TextBox
    Friend WithEvents BtnFind As System.Windows.Forms.Button
    Friend WithEvents AutocompleteMenu1 As AutocompleteMenuNS.AutocompleteMenu
    Friend WithEvents DSWizardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabCauses As System.Windows.Forms.TabPage
    Friend WithEvents ListCauses As DSeX.ListView_NoFlicker
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents BtnSectionDelete As System.Windows.Forms.Button
    Friend WithEvents BtnSectionAdd As System.Windows.Forms.Button
    Friend WithEvents BtnSectionDown As System.Windows.Forms.Button
    Friend WithEvents BtnSectionUp As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents SectionMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents BtnTemplateAdd As System.Windows.Forms.Button
    Friend WithEvents BtnTemplateDelete As System.Windows.Forms.Button
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Causes As System.Windows.Forms.TabControl
    Friend WithEvents TabControl2 As DSeX.TabControlEx
    Friend WithEvents NewSection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteSection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopySection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutSection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PasteSection As System.Windows.Forms.ToolStripMenuItem
End Class
