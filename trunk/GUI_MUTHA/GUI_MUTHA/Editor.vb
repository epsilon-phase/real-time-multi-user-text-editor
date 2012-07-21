Imports System
Imports System.IO
Imports System.Text
Imports ChatServerLib


Public Class Editor

    #Region "Fields"

    Dim clienthandlingthingies As OperationalTransform.ClientForSam
    Dim Directory, NamePerson, FileName, IP As String
    Dim go As System.Threading.Thread
    Dim chatClient As ChatClient = New ChatClient(Startup.ipText.Text, 3341)

#End Region 'Fields

    Private Sub Editor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Startup.Hide()
        FileName = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Directory = "C:\Users\user\AppData\Local\Temp\" + FileName + ".txt"
        rtbText.Text = My.Computer.FileSystem.ReadAllText(Directory)
        NamePerson = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Name.txt")
        lblNames.Text = NamePerson
        IP = Startup.IP
        'Parse the IPaddress
        Try
            Me.clienthandlingthingies = New OperationalTransform.ClientForSam(System.Net.IPAddress.Parse(IP))
            Dim s As New System.Threading.ThreadStart(AddressOf clienthandlingthingies.Start)
            'if it turns out to be valid, make the thread instance
            go = New System.Threading.Thread(s)
            'start the thread
            go.Start()
        Catch except As System.Net.Sockets.SocketException
            MessageBox.Show(except.Message)
            Startup.Show()
            Me.Hide()
        End Try
        'For Alex'
        'Me.e = New OperationalTransform.TextTransformCollection()
        Dim mrl As MessageRecievedListener = New MessageRecievedListener(AddressOf Me.messageRecieved)
        chatClient.start(mrl)
    End Sub

    #Region "Methods"
    Private Sub backButton_Click(sender As System.Object, e As System.EventArgs) Handles backButton.Click
        rtbText.Text = ""
        My.Computer.FileSystem.DeleteFile("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Startup.Show()
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Startup.Close()
        Me.Close()
    End Sub
    Private Sub copy()
        My.Computer.Clipboard.SetText(rtbText.SelectedText)
    End Sub
    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        copy()
    End Sub
    Private Sub cut()
        Me.clienthandlingthingies.CutAdd(rtbText.SelectionStart, rtbText.SelectionLength + rtbText.SelectionStart)
        My.Computer.Clipboard.SetText(rtbText.SelectedText)
        rtbText.SelectedText = ""
    End Sub
    Private Sub CutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CutToolStripMenuItem.Click
        cut()
    End Sub
    Private Sub paste()
        'Add clipboard contents to the client buffery thingy
        Me.clienthandlingthingies.PasteAdd(rtbText.SelectionStart, My.Computer.Clipboard.GetText())
        Dim text As String = My.Computer.Clipboard.GetText()
        rtbText.SelectedText = text
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        paste()
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

    Private Sub rtbText_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles rtbText.KeyPress
        e.Handled = True
        Me.clienthandlingthingies.KeyPressadd(e, rtbText.SelectionStart)
        'Me.rtbText.Text = Me.clienthandlingthingies.getconsolidatedstring()
    End Sub

    Private Sub rtbText_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles rtbText.PreviewKeyDown
        Me.clienthandlingthingies.KeyPressDelete(e, rtbText.SelectionStart)
    End Sub
    Private Sub selectall()
        rtbText.SelectAll()
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        selectall()
    End Sub

#Region "Chat"

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

    Public Sub messageRecieved(ByVal s As String)
        Chatbox.AppendText(s)
    End Sub
#End Region


#Region "Zoom"

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
#End Region

#Region "ChatBox"

    Private Sub BDown_Click(sender As System.Object, e As System.EventArgs) Handles BDown.Click
        PanelUP.Visible = False
        PanelDown.Visible = True
    End Sub

    Private Sub ChatToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChatToolStripMenuItem.Click
        If PanelUP.Visible = False And PanelDown.Visible = False Then
            PanelUP.Visible = True
            PanelDown.Visible = False
        End If
    End Sub

    Private Sub bUP_Click(sender As System.Object, e As System.EventArgs)
        PanelUP.Visible = True
        PanelDown.Visible = False
    End Sub

    Private Sub bUP_Click_1(sender As System.Object, e As System.EventArgs) Handles bUP.Click
        PanelUP.Visible = True
        PanelDown.Visible = False
    End Sub

    Private Sub xBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles xBox2.Click
        PanelUP.Visible = False
        PanelDown.Visible = False
    End Sub

    Private Sub xBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles xBox.Click
        PanelUP.Visible = False
        PanelDown.Visible = False
    End Sub

#End Region

#End Region 'Methods

    Dim somenolescence As String

    'Private Sub Consolidator_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles Consolidator.DoWork
    '    Try
    '        somenolescence = clienthandlingthingies.getconsolidatedstring()
    '    Catch except As ArgumentException

    '    End Try
    'End Sub

    Private Sub consolidatetimer_Tick(sender As System.Object, e As System.EventArgs) Handles consolidatetimer.Tick
        Consolidator.RunWorkerAsync()
    End Sub

    Private Sub Consolidator_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Consolidator.RunWorkerCompleted
        rtbText.Text = somenolescence
    End Sub

    Private Sub DownloadFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DownloadFileToolStripMenuItem.Click
        Dim filePath As String
        Try
            filePath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, Startup.ComboBox.Text + ".txt")
            My.Computer.FileSystem.WriteAllText(filePath, rtbText.Text, True)
        Catch fileException As Exception
            Throw fileException
        End Try
    End Sub
End Class