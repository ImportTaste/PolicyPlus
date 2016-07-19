﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim ChSettingEnabled As System.Windows.Forms.ColumnHeader
        Dim ChSettingCommented As System.Windows.Forms.ColumnHeader
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenADMXFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenADMXFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseADMXWorkspaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.CategoriesTree = New System.Windows.Forms.TreeView()
        Me.PolicyIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.PoliciesList = New System.Windows.Forms.ListView()
        Me.ChSettingName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SettingInfoPanel = New System.Windows.Forms.Panel()
        Me.PolicyInfoTable = New System.Windows.Forms.TableLayoutPanel()
        Me.PolicyTitleLabel = New System.Windows.Forms.Label()
        Me.PolicySupportedLabel = New System.Windows.Forms.Label()
        Me.PolicyDescLabel = New System.Windows.Forms.Label()
        ChSettingEnabled = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ChSettingCommented = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MainMenu.SuspendLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SettingInfoPanel.SuspendLayout()
        Me.PolicyInfoTable.SuspendLayout()
        Me.SuspendLayout()
        '
        'ChSettingEnabled
        '
        ChSettingEnabled.Text = "State"
        ChSettingEnabled.Width = 79
        '
        'ChSettingCommented
        '
        ChSettingCommented.Text = "Commented"
        ChSettingCommented.Width = 90
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(667, 24)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenADMXFolderToolStripMenuItem, Me.OpenADMXFileToolStripMenuItem, Me.CloseADMXWorkspaceToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenADMXFolderToolStripMenuItem
        '
        Me.OpenADMXFolderToolStripMenuItem.Name = "OpenADMXFolderToolStripMenuItem"
        Me.OpenADMXFolderToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.OpenADMXFolderToolStripMenuItem.Text = "Open ADMX Folder"
        '
        'OpenADMXFileToolStripMenuItem
        '
        Me.OpenADMXFileToolStripMenuItem.Name = "OpenADMXFileToolStripMenuItem"
        Me.OpenADMXFileToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.OpenADMXFileToolStripMenuItem.Text = "Open ADMX File"
        '
        'CloseADMXWorkspaceToolStripMenuItem
        '
        Me.CloseADMXWorkspaceToolStripMenuItem.Name = "CloseADMXWorkspaceToolStripMenuItem"
        Me.CloseADMXWorkspaceToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.CloseADMXWorkspaceToolStripMenuItem.Text = "Close ADMX Workspace"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.CategoriesTree)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer.Panel2.Controls.Add(Me.PoliciesList)
        Me.SplitContainer.Panel2.Controls.Add(Me.SettingInfoPanel)
        Me.SplitContainer.Size = New System.Drawing.Size(667, 350)
        Me.SplitContainer.SplitterDistance = 180
        Me.SplitContainer.TabIndex = 1
        '
        'CategoriesTree
        '
        Me.CategoriesTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CategoriesTree.ImageIndex = 0
        Me.CategoriesTree.ImageList = Me.PolicyIcons
        Me.CategoriesTree.Location = New System.Drawing.Point(0, 0)
        Me.CategoriesTree.Name = "CategoriesTree"
        Me.CategoriesTree.SelectedImageIndex = 0
        Me.CategoriesTree.Size = New System.Drawing.Size(180, 350)
        Me.CategoriesTree.TabIndex = 0
        '
        'PolicyIcons
        '
        Me.PolicyIcons.ImageStream = CType(resources.GetObject("PolicyIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.PolicyIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.PolicyIcons.Images.SetKeyName(0, "folder.png")
        Me.PolicyIcons.Images.SetKeyName(1, "folder_error.png")
        Me.PolicyIcons.Images.SetKeyName(2, "folder_delete.png")
        Me.PolicyIcons.Images.SetKeyName(3, "folder_go.png")
        Me.PolicyIcons.Images.SetKeyName(4, "page_white.png")
        Me.PolicyIcons.Images.SetKeyName(5, "page_white_gear.png")
        '
        'PoliciesList
        '
        Me.PoliciesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PoliciesList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PoliciesList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ChSettingName, ChSettingEnabled, ChSettingCommented})
        Me.PoliciesList.Location = New System.Drawing.Point(190, 0)
        Me.PoliciesList.MultiSelect = False
        Me.PoliciesList.Name = "PoliciesList"
        Me.PoliciesList.Size = New System.Drawing.Size(293, 350)
        Me.PoliciesList.SmallImageList = Me.PolicyIcons
        Me.PoliciesList.TabIndex = 1
        Me.PoliciesList.UseCompatibleStateImageBehavior = False
        Me.PoliciesList.View = System.Windows.Forms.View.Details
        '
        'ChSettingName
        '
        Me.ChSettingName.Text = "Name"
        Me.ChSettingName.Width = 116
        '
        'SettingInfoPanel
        '
        Me.SettingInfoPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SettingInfoPanel.Controls.Add(Me.PolicyInfoTable)
        Me.SettingInfoPanel.Location = New System.Drawing.Point(0, 0)
        Me.SettingInfoPanel.Name = "SettingInfoPanel"
        Me.SettingInfoPanel.Size = New System.Drawing.Size(184, 350)
        Me.SettingInfoPanel.TabIndex = 0
        '
        'PolicyInfoTable
        '
        Me.PolicyInfoTable.ColumnCount = 1
        Me.PolicyInfoTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PolicyInfoTable.Controls.Add(Me.PolicyTitleLabel, 0, 0)
        Me.PolicyInfoTable.Controls.Add(Me.PolicySupportedLabel, 0, 1)
        Me.PolicyInfoTable.Controls.Add(Me.PolicyDescLabel, 0, 2)
        Me.PolicyInfoTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PolicyInfoTable.Location = New System.Drawing.Point(0, 0)
        Me.PolicyInfoTable.Name = "PolicyInfoTable"
        Me.PolicyInfoTable.RowCount = 3
        Me.PolicyInfoTable.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PolicyInfoTable.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PolicyInfoTable.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.PolicyInfoTable.Size = New System.Drawing.Size(184, 350)
        Me.PolicyInfoTable.TabIndex = 0
        '
        'PolicyTitleLabel
        '
        Me.PolicyTitleLabel.AutoSize = True
        Me.PolicyTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PolicyTitleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PolicyTitleLabel.Location = New System.Drawing.Point(3, 0)
        Me.PolicyTitleLabel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 24)
        Me.PolicyTitleLabel.Name = "PolicyTitleLabel"
        Me.PolicyTitleLabel.Size = New System.Drawing.Size(178, 13)
        Me.PolicyTitleLabel.TabIndex = 0
        Me.PolicyTitleLabel.Text = "Policy title"
        '
        'PolicySupportedLabel
        '
        Me.PolicySupportedLabel.AutoSize = True
        Me.PolicySupportedLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PolicySupportedLabel.Location = New System.Drawing.Point(3, 37)
        Me.PolicySupportedLabel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 24)
        Me.PolicySupportedLabel.Name = "PolicySupportedLabel"
        Me.PolicySupportedLabel.Size = New System.Drawing.Size(178, 13)
        Me.PolicySupportedLabel.TabIndex = 1
        Me.PolicySupportedLabel.Text = "Requirements"
        '
        'PolicyDescLabel
        '
        Me.PolicyDescLabel.AutoSize = True
        Me.PolicyDescLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PolicyDescLabel.Location = New System.Drawing.Point(3, 74)
        Me.PolicyDescLabel.Name = "PolicyDescLabel"
        Me.PolicyDescLabel.Size = New System.Drawing.Size(178, 276)
        Me.PolicyDescLabel.TabIndex = 2
        Me.PolicyDescLabel.Text = "Policy description"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 374)
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.MainMenu)
        Me.MainMenuStrip = Me.MainMenu
        Me.Name = "Main"
        Me.ShowIcon = False
        Me.Text = "Policy Plus"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.SettingInfoPanel.ResumeLayout(False)
        Me.PolicyInfoTable.ResumeLayout(False)
        Me.PolicyInfoTable.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MainMenu As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenADMXFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenADMXFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseADMXWorkspaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer As SplitContainer
    Friend WithEvents CategoriesTree As TreeView
    Friend WithEvents PoliciesList As ListView
    Friend WithEvents SettingInfoPanel As Panel
    Friend WithEvents PolicyIcons As ImageList
    Friend WithEvents PolicyInfoTable As TableLayoutPanel
    Friend WithEvents PolicyTitleLabel As Label
    Friend WithEvents PolicySupportedLabel As Label
    Friend WithEvents PolicyDescLabel As Label
    Friend WithEvents ChSettingName As ColumnHeader
End Class
