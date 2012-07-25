<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Startup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Startup))
        Me.Button2 = New System.Windows.Forms.Button()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ComboBox = New System.Windows.Forms.ComboBox()
        Me.closeButton = New System.Windows.Forms.Button()
        Me.TextName = New System.Windows.Forms.TextBox()
        Me.ipText = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 91)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(200, 22)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Done"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBox
        '
        Me.ComboBox.Enabled = False
        Me.ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox.FormattingEnabled = True
        Me.ComboBox.Items.AddRange(New Object() {"Doc1", "Doc2", "Doc3", "TROLOLO"})
        Me.ComboBox.Location = New System.Drawing.Point(12, 38)
        Me.ComboBox.Name = "ComboBox"
        Me.ComboBox.Size = New System.Drawing.Size(200, 21)
        Me.ComboBox.TabIndex = 3
        '
        'closeButton
        '
        Me.closeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.closeButton.Location = New System.Drawing.Point(12, 119)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(200, 23)
        Me.closeButton.TabIndex = 4
        Me.closeButton.Text = "Close"
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'TextName
        '
        Me.TextName.Enabled = False
        Me.TextName.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextName.Location = New System.Drawing.Point(12, 65)
        Me.TextName.Name = "TextName"
        Me.TextName.Size = New System.Drawing.Size(200, 20)
        Me.TextName.TabIndex = 5
        Me.TextName.Text = "What is your name?"
        '
        'ipText
        '
        Me.ipText.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress
        Me.ipText.ForeColor = System.Drawing.SystemColors.GrayText
        Me.ipText.Location = New System.Drawing.Point(12, 12)
        Me.ipText.Name = "ipText"
        Me.ipText.Size = New System.Drawing.Size(200, 20)
        Me.ipText.TabIndex = 6
        Me.ipText.Text = "What is the IP?"
        '
        'Startup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(224, 154)
        Me.ControlBox = False
        Me.Controls.Add(Me.ipText)
        Me.Controls.Add(Me.TextName)
        Me.Controls.Add(Me.closeButton)
        Me.Controls.Add(Me.ComboBox)
        Me.Controls.Add(Me.Button2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Startup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Startup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents closeButton As System.Windows.Forms.Button
    Friend WithEvents TextName As System.Windows.Forms.TextBox
    Friend WithEvents ipText As System.Windows.Forms.TextBox
End Class
