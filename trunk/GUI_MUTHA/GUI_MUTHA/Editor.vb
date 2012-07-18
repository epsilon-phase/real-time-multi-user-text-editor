Imports System
Imports System.IO
Imports System.Text

Public Class Editor
    Dim Directory, NamePerson, FileName, IP As String

    Private Sub Editor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Startup.Hide()
        FileName = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Directory = "C:\Users\user\AppData\Local\Temp\" + FileName + ".txt"
        rtbText.Text = My.Computer.FileSystem.ReadAllText(Directory)
        NamePerson = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Name.txt")
        lblNames.Text = NamePerson
        IP = Startup.IP

        'For Alex'
        'Me.e = New OperationalTransform.TextTransformCollection()
    End Sub

    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Startup.Close()
        Me.Close()
    End Sub

    'Private Sub StartClient()
    '    Dim f As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream)
    'End Sub

    Private Sub ToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem3.Click
        rtbText.ZoomFactor = 0.25
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem4.Click
        rtbText.ZoomFactor = 0.5
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem5.Click
        rtbText.ZoomFactor = 0.75
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem6.Click
        rtbText.ZoomFactor = 1.0
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem7.Click
        rtbText.ZoomFactor = 1.25
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem8.Click
        rtbText.ZoomFactor = 1.5
    End Sub

    Private Sub ToolStripMenuItem9_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem9.Click
        rtbText.ZoomFactor = 1.75
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem10.Click
        rtbText.ZoomFactor = 2.0
    End Sub

    Private Sub backButton_Click(sender As System.Object, e As System.EventArgs) Handles backButton.Click
        rtbText.Text = ""
        My.Computer.FileSystem.DeleteFile("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Startup.Show()
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        paste()
    End Sub

    Private Sub copy()
        My.Computer.Clipboard.SetText(rtbText.SelectedText)
    End Sub

    Private Sub paste()
        Dim text As String = My.Computer.Clipboard.GetText()
        rtbText.SelectedText = text
    End Sub

    Private Sub cut()
        My.Computer.Clipboard.SetText(rtbText.SelectedText)
        rtbText.SelectedText = ""
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CutToolStripMenuItem.Click
        cut()
    End Sub

    Private Sub selectall()
        rtbText.SelectAll()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        selectall()
    End Sub

    Private Sub bUP_Click(sender As System.Object, e As System.EventArgs)
        PanelUP.Visible = True
        PanelDown.Visible = False
    End Sub

    Private Sub BDown_Click(sender As System.Object, e As System.EventArgs) Handles BDown.Click
        PanelUP.Visible = False
        PanelDown.Visible = True
    End Sub

    Private Sub xBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles xBox.Click
        PanelUP.Visible = False
        PanelDown.Visible = False
    End Sub

    Private Sub xBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles xBox2.Click
        PanelUP.Visible = False
        PanelDown.Visible = False
    End Sub

    Private Sub ChatToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChatToolStripMenuItem.Click
        If PanelUP.Visible = False And PanelDown.Visible = False Then
            PanelUP.Visible = True
            PanelDown.Visible = False
        End If
    End Sub

    Private Sub bUP_Click_1(sender As System.Object, e As System.EventArgs) Handles bUP.Click
        PanelUP.Visible = True
        PanelDown.Visible = False
    End Sub

    Private Sub Send_Click(sender As System.Object, e As System.EventArgs) Handles Send.Click
        Dim SendMess As String = "[" + NamePerson + "]: " + ChatMessage.Text

        If Chatbox.Text = "" Then
            Chatbox.Text = SendMess
            ChatMessage.Text = ""
        Else
            Chatbox.Text = Chatbox.Text + vbCrLf + SendMess
            ChatMessage.Text = ""
        End If
    End Sub

    Private Sub Chatbox_TextChanged(sender As System.Object, e As System.EventArgs) Handles Chatbox.TextChanged
        Chatbox.SelectionStart = Chatbox.Text.Length
        Chatbox.ScrollToCaret()
    End Sub

    '====This Was Removed Because Alex Screws Around Alot===='


    'Dim e As OperationalTransform.TextTransformCollection

    'Private Sub rtbText_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rtbText.KeyUp

    'End Sub

    'Private Sub rtbText_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles rtbText.KeyPress
    '    Dim a As New OperationalTransform.TextTransformActor(rtbText.SelectionStart, e.KeyChar.ToString())
    '    a.AlterForClient()
    '    REM insert this single character here
    '    Me.e.Add(a)
    '    'The text should not be entered through into the text ending.
    '    e.Handled = True
    'End Sub

    'Private Sub ConsolidateShit()
    '    rtbText.Text = e.CalculateConsolidatedString()
    'End Sub

    'Private Sub rtbText_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles rtbText.PreviewKeyDown
    '    If e.KeyCode = Keys.Back Then
    '        Dim a As New OperationalTransform.TextTransformActor(rtbText.SelectionStart - 1, 1)
    '        a.AlterForClient()
    '        Me.e.Add(a)
    '    End If
    'End Sub
End Class