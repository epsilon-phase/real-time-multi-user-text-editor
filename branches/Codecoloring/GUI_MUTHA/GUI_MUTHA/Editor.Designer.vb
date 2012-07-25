<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Editor))
        Me.rtbText = New System.Windows.Forms.RichTextBox()
        Me.closeButton = New System.Windows.Forms.Button()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindAndReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoAwayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WeCantHelpYouToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GetALifeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WHYAREYOUSTILLREADINGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.backButton = New System.Windows.Forms.Button()
        Me.PanelDown = New System.Windows.Forms.TableLayoutPanel()
        Me.Send = New System.Windows.Forms.Button()
        Me.bUP = New System.Windows.Forms.Button()
        Me.ChatMessage = New System.Windows.Forms.TextBox()
        Me.lblNames = New System.Windows.Forms.Label()
        Me.xBox2 = New System.Windows.Forms.TextBox()
        Me.Chatbox = New System.Windows.Forms.RichTextBox()
        Me.PanelUP = New System.Windows.Forms.Panel()
        Me.xBox = New System.Windows.Forms.TextBox()
        Me.BDown = New System.Windows.Forms.Button()
        Me.Consolidator = New System.ComponentModel.BackgroundWorker()
        Me.consolidatetimer = New System.Windows.Forms.Timer(Me.components)
        Me.cbxLang = New System.Windows.Forms.ComboBox()
        Me.MenuStrip.SuspendLayout()
        Me.PanelDown.SuspendLayout()
        Me.PanelUP.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtbText
        '
        Me.rtbText.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbText.Location = New System.Drawing.Point(12, 27)
        Me.rtbText.Name = "rtbText"
        Me.rtbText.Size = New System.Drawing.Size(909, 571)
        Me.rtbText.TabIndex = 0
        Me.rtbText.Text = ""
        '
        'closeButton
        '
        Me.closeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.closeButton.Location = New System.Drawing.Point(616, 604)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(305, 23)
        Me.closeButton.TabIndex = 1
        Me.closeButton.Text = "Close"
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(933, 24)
        Me.MenuStrip.TabIndex = 2
        Me.MenuStrip.Text = "MenuStrip"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadFileToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'DownloadFileToolStripMenuItem
        '
        Me.DownloadFileToolStripMenuItem.Name = "DownloadFileToolStripMenuItem"
        Me.DownloadFileToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.DownloadFileToolStripMenuItem.Text = "Download File"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToolStripMenuItem, Me.CutToolStripMenuItem, Me.PasteToolStripMenuItem, Me.SelectAllToolStripMenuItem, Me.FindAndReplaceToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Enabled = False
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        '
        'FindAndReplaceToolStripMenuItem
        '
        Me.FindAndReplaceToolStripMenuItem.Name = "FindAndReplaceToolStripMenuItem"
        Me.FindAndReplaceToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.FindAndReplaceToolStripMenuItem.Text = "Find and Replace"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ChatToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripMenuItem8, Me.ToolStripMenuItem9, Me.ToolStripMenuItem10})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(106, 22)
        Me.ToolStripMenuItem2.Text = "Zoom"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem3.Text = "25%"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem4.Text = "50%"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem5.Text = "75%"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem6.Text = "100%"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem7.Text = "125%"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem8.Text = "150%"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem9.Text = "175%"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(102, 22)
        Me.ToolStripMenuItem10.Text = "200%"
        '
        'ChatToolStripMenuItem
        '
        Me.ChatToolStripMenuItem.Name = "ChatToolStripMenuItem"
        Me.ChatToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.ChatToolStripMenuItem.Text = "Chat"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoAwayToolStripMenuItem, Me.WeCantHelpYouToolStripMenuItem, Me.GetALifeToolStripMenuItem, Me.WHYAREYOUSTILLREADINGToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'GoAwayToolStripMenuItem
        '
        Me.GoAwayToolStripMenuItem.Enabled = False
        Me.GoAwayToolStripMenuItem.Name = "GoAwayToolStripMenuItem"
        Me.GoAwayToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.GoAwayToolStripMenuItem.Text = "Go Away!"
        '
        'WeCantHelpYouToolStripMenuItem
        '
        Me.WeCantHelpYouToolStripMenuItem.Enabled = False
        Me.WeCantHelpYouToolStripMenuItem.Name = "WeCantHelpYouToolStripMenuItem"
        Me.WeCantHelpYouToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.WeCantHelpYouToolStripMenuItem.Text = "We Can't Help You!"
        '
        'GetALifeToolStripMenuItem
        '
        Me.GetALifeToolStripMenuItem.Enabled = False
        Me.GetALifeToolStripMenuItem.Name = "GetALifeToolStripMenuItem"
        Me.GetALifeToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.GetALifeToolStripMenuItem.Text = "Get A Life!"
        '
        'WHYAREYOUSTILLREADINGToolStripMenuItem
        '
        Me.WHYAREYOUSTILLREADINGToolStripMenuItem.Enabled = False
        Me.WHYAREYOUSTILLREADINGToolStripMenuItem.Name = "WHYAREYOUSTILLREADINGToolStripMenuItem"
        Me.WHYAREYOUSTILLREADINGToolStripMenuItem.Size = New System.Drawing.Size(243, 22)
        Me.WHYAREYOUSTILLREADINGToolStripMenuItem.Text = "WHY ARE YOU STILL READING!?"
        '
        'backButton
        '
        Me.backButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backButton.Location = New System.Drawing.Point(12, 604)
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(305, 23)
        Me.backButton.TabIndex = 3
        Me.backButton.Text = "Back"
        Me.backButton.UseVisualStyleBackColor = True
        '
        'PanelDown
        '
        Me.PanelDown.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelDown.ColumnCount = 2
        Me.PanelDown.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.64576!))
        Me.PanelDown.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.35424!))
        Me.PanelDown.Controls.Add(Me.Send, 1, 2)
        Me.PanelDown.Controls.Add(Me.bUP, 1, 0)
        Me.PanelDown.Controls.Add(Me.ChatMessage, 0, 2)
        Me.PanelDown.Controls.Add(Me.lblNames, 1, 1)
        Me.PanelDown.Controls.Add(Me.xBox2, 0, 0)
        Me.PanelDown.Controls.Add(Me.Chatbox, 0, 1)
        Me.PanelDown.Location = New System.Drawing.Point(634, 42)
        Me.PanelDown.Name = "PanelDown"
        Me.PanelDown.RowCount = 3
        Me.PanelDown.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.21739!))
        Me.PanelDown.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.78261!))
        Me.PanelDown.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.PanelDown.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.PanelDown.Size = New System.Drawing.Size(271, 209)
        Me.PanelDown.TabIndex = 5
        Me.PanelDown.Visible = False
        '
        'Send
        '
        Me.Send.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Send.Location = New System.Drawing.Point(208, 185)
        Me.Send.Name = "Send"
        Me.Send.Size = New System.Drawing.Size(60, 21)
        Me.Send.TabIndex = 11
        Me.Send.Text = "Send"
        Me.Send.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Send.UseVisualStyleBackColor = True
        '
        'bUP
        '
        Me.bUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bUP.Location = New System.Drawing.Point(249, 4)
        Me.bUP.Name = "bUP"
        Me.bUP.Size = New System.Drawing.Size(19, 20)
        Me.bUP.TabIndex = 12
        Me.bUP.Text = "^"
        Me.bUP.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.bUP.UseVisualStyleBackColor = True
        '
        'ChatMessage
        '
        Me.ChatMessage.Location = New System.Drawing.Point(3, 185)
        Me.ChatMessage.Name = "ChatMessage"
        Me.ChatMessage.Size = New System.Drawing.Size(199, 20)
        Me.ChatMessage.TabIndex = 0
        '
        'lblNames
        '
        Me.lblNames.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNames.BackColor = System.Drawing.SystemColors.Control
        Me.lblNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNames.Location = New System.Drawing.Point(208, 27)
        Me.lblNames.Name = "lblNames"
        Me.lblNames.Size = New System.Drawing.Size(60, 155)
        Me.lblNames.TabIndex = 7
        Me.lblNames.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'xBox2
        '
        Me.xBox2.BackColor = System.Drawing.SystemColors.Control
        Me.xBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.xBox2.Location = New System.Drawing.Point(3, 3)
        Me.xBox2.Name = "xBox2"
        Me.xBox2.ReadOnly = True
        Me.xBox2.Size = New System.Drawing.Size(19, 20)
        Me.xBox2.TabIndex = 11
        Me.xBox2.Text = "x"
        Me.xBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Chatbox
        '
        Me.Chatbox.Location = New System.Drawing.Point(3, 30)
        Me.Chatbox.Name = "Chatbox"
        Me.Chatbox.ReadOnly = True
        Me.Chatbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.Chatbox.Size = New System.Drawing.Size(199, 149)
        Me.Chatbox.TabIndex = 13
        Me.Chatbox.Text = ""
        '
        'PanelUP
        '
        Me.PanelUP.Controls.Add(Me.xBox)
        Me.PanelUP.Controls.Add(Me.BDown)
        Me.PanelUP.Location = New System.Drawing.Point(634, 42)
        Me.PanelUP.Name = "PanelUP"
        Me.PanelUP.Size = New System.Drawing.Size(271, 28)
        Me.PanelUP.TabIndex = 6
        '
        'xBox
        '
        Me.xBox.BackColor = System.Drawing.SystemColors.Control
        Me.xBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.xBox.Location = New System.Drawing.Point(3, 3)
        Me.xBox.Name = "xBox"
        Me.xBox.ReadOnly = True
        Me.xBox.Size = New System.Drawing.Size(19, 20)
        Me.xBox.TabIndex = 10
        Me.xBox.Text = "x"
        Me.xBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BDown
        '
        Me.BDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BDown.Location = New System.Drawing.Point(249, 5)
        Me.BDown.Name = "BDown"
        Me.BDown.Size = New System.Drawing.Size(19, 20)
        Me.BDown.TabIndex = 9
        Me.BDown.Text = "^"
        Me.BDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BDown.UseVisualStyleBackColor = True
        '
        'Consolidator
        '
        '
        'consolidatetimer
        '
        Me.consolidatetimer.Interval = 10
        '
        'cbxLang
        '
        Me.cbxLang.FormattingEnabled = True
        Me.cbxLang.Location = New System.Drawing.Point(323, 606)
        Me.cbxLang.Name = "cbxLang"
        Me.cbxLang.Size = New System.Drawing.Size(287, 21)
        Me.cbxLang.TabIndex = 7
        Me.cbxLang.Text = "Language..."
        '
        'Editor
        '
        Me.AcceptButton = Me.Send
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 639)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbxLang)
        Me.Controls.Add(Me.PanelUP)
        Me.Controls.Add(Me.backButton)
        Me.Controls.Add(Me.closeButton)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.PanelDown)
        Me.Controls.Add(Me.rtbText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Editor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Text Editor"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.PanelDown.ResumeLayout(False)
        Me.PanelDown.PerformLayout()
        Me.PanelUP.ResumeLayout(False)
        Me.PanelUP.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbText As System.Windows.Forms.RichTextBox
    Friend WithEvents closeButton As System.Windows.Forms.Button
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoAwayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WeCantHelpYouToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetALifeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WHYAREYOUSTILLREADINGToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents backButton As System.Windows.Forms.Button
    Friend WithEvents PanelDown As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ChatMessage As System.Windows.Forms.TextBox
    Friend WithEvents lblNames As System.Windows.Forms.Label
    Friend WithEvents PanelUP As System.Windows.Forms.Panel
    Friend WithEvents BDown As System.Windows.Forms.Button
    Friend WithEvents ChatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents xBox As System.Windows.Forms.TextBox
    Friend WithEvents bUP As System.Windows.Forms.Button
    Friend WithEvents xBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Send As System.Windows.Forms.Button
    Friend WithEvents Chatbox As System.Windows.Forms.RichTextBox
    Friend WithEvents Consolidator As System.ComponentModel.BackgroundWorker
    Friend WithEvents consolidatetimer As System.Windows.Forms.Timer
    Friend WithEvents FindAndReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbxLang As System.Windows.Forms.ComboBox

End Class
