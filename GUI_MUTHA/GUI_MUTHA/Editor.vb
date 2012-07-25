Imports System
Imports System.IO
Imports System.Text
Imports CodeLangLib

Public Class Editor

    #Region "Fields"

    Dim clienthandlingthingies As OperationalTransform.ClientForSam
    Dim Directory, NamePerson, FileName, IP As String
    Dim go As System.Threading.Thread
    Dim d As ChatServerLib.ChatClient
    Dim lang As CodeLangLib.Language
    Dim langset As Boolean = False
#End Region 'Fields
    Private Sub Editor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Startup.Hide()
        'FileName = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Doc.txt")
        'Directory = "C:\Users\user\AppData\Local\Temp\" + FileName + ".txt"
        'rtbText.Text = My.Computer.FileSystem.ReadAllText(Directory)
        NamePerson = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Name.txt")
        lblNames.Text = NamePerson
        IP = Startup.IP
        'Parse the IPaddress
        Try
            Me.clienthandlingthingies = New OperationalTransform.ClientForSam(System.Net.IPAddress.Parse(IP))
            Dim ThreadHandle As New System.Threading.ThreadStart(AddressOf clienthandlingthingies.Start)
            'if it turns out to be valid, make the thread instance
            go = New System.Threading.Thread(ThreadHandle)
            'start the thread
            go.Start()
            d = New ChatServerLib.ChatClient(IP, 3410)
            d.start(New ChatServerLib.MessageRecievedListener(AddressOf messagerecieved), Startup.TextName.Text)

            MessageBox.Show("Connected Successfully")
            'start the timer
            consolidatetimer.Enabled = True
        Catch except As System.Net.Sockets.SocketException
            MessageBox.Show(except.Message)
            Startup.Show()
            Me.Close()
        End Try
        'For Alex'
        'Me.e = New OperationalTransform.TextTransformCollection()
        'Setup langauges
        cbxLang.Items.Add(Language.LangC)
        cbxLang.Items.Add(Language.LangCPlusPlus)
        cbxLang.Items.Add(Language.LangCSharp)
        cbxLang.Items.Add(Language.LangJava)
        cbxLang.Items.Add(Language.LangPython)
        cbxLang.Items.Add(Language.LangVBdotNET)
        'Get Language
        If Startup.ComboBox.Text.EndsWith(Language.LangC.fileExtension) Then
            lang = Language.LangC
        ElseIf Startup.ComboBox.Text.EndsWith(Language.LangCPlusPlus.fileExtension) Then
            lang = Language.LangCPlusPlus
        ElseIf Startup.ComboBox.Text.EndsWith(Language.LangCSharp.fileExtension) Then
            lang = Language.LangCSharp
        ElseIf Startup.ComboBox.Text.EndsWith(Language.LangJava.fileExtension) Then
            lang = Language.LangJava
        ElseIf Startup.ComboBox.Text.EndsWith(Language.LangPython.fileExtension) Then
            lang = Language.LangPython
        ElseIf Startup.ComboBox.Text.EndsWith(Language.LangVBdotNET.fileExtension) Then
            lang = Language.LangVBdotNET
        End If
        cbxLang.SelectedItem = lang
        langset = True
        updateColoring()
    End Sub
    Sub messagerecieved(msg As String)
        Me.AppendText(msg)
    End Sub
    Private Delegate Sub settextdelegate(a As String)
    Private Sub AppendText(a As String)
        If Chatbox.InvokeRequired Then
            Dim c As New settextdelegate(AddressOf AppendText)
            Me.Invoke(c, New Object() {[a]})
        Else
            Me.Chatbox.AppendText(vbCrLf + a)
        End If
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
        'rtbText.SelectedText = text
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
    Private Sub rtbText_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles rtbText.PreviewKeyDown
        Me.clienthandlingthingies.KeyPressDelete(e, rtbText.SelectionStart,rtbText.SelectionLength)
        Select Case e.KeyCode
            Case Keys.Delete
            Case Keys.Back
            Case Keys.Left
            Case Keys.Right
            Case Keys.Up
            Case Keys.Down
            Case Keys.Enter
                e.IsInputKey = False

            Case Else
                e.IsInputKey = True
        End Select
        Me.lastkey = e.KeyCode
    End Sub
    Private Sub selectall()
        rtbText.SelectAll()
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        selectall()
    End Sub

    Public Sub find(ByVal s As String, Optional ByVal start As Integer = 0)
        For i As Integer = start To rtbText.Text.Length - 1
            If s = rtbText.Text.Substring(i, s.Length) Then
                rtbText.Select(i, s.Length)
            End If
        Next
    End Sub
#Region "Chat"

    Private Sub Send_Click(sender As System.Object, e As System.EventArgs) Handles Send.Click
        Dim SendMess As String = "[" + NamePerson + "]: " + ChatMessage.Text
        d.send(SendMess)
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
    Private Sub Consolidator_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles Consolidator.DoWork
        Try
            somenolescence = clienthandlingthingies.getconsolidatedstring()
        Catch except As NullReferenceException

        End Try
    End Sub
    ''' <summary>
    ''' Selection store for the update process 
    ''' Updated whenever the world sees fit to do so
    ''' </summary>
    ''' <remarks></remarks>
    Dim selectionstore As Integer
    Dim lastkey As Keys
    Private Sub consolidatetimer_Tick(sender As System.Object, e As System.EventArgs) Handles consolidatetimer.Tick
        If Not Consolidator.IsBusy And clienthandlingthingies.changed Then
            selectionstore = rtbText.SelectionStart
            Consolidator.RunWorkerAsync()
        End If

    End Sub

    Private Sub Consolidator_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Consolidator.RunWorkerCompleted
        Try
            rtbText.Text = somenolescence
            Select Case lastkey
                Case Keys.Back
                    rtbText.SelectionStart = selectionstore - 1
                    'Don't allow the arrow keys to be misinterpreted as another character.
                Case Keys.Right
                Case Keys.Left
                Case Keys.Up
                Case Keys.Down
                Case Keys.Delete


                Case Keys.Enter
                    rtbText.SelectionStart = selectionstore + 2
                Case Else
                    'seems to work just fine.
                    rtbText.SelectionStart = selectionstore + 1

            End Select
        Catch ex As NullReferenceException

        End Try

    End Sub

    Private Sub Editor_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Closes socket connection
        clienthandlingthingies.CloseConnection()
        Startup.Close()
        Me.Close()
    End Sub

    Private Sub rtbText_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles rtbText.KeyPress
        clienthandlingthingies.KeyPressadd(e, rtbText.SelectionStart)
    End Sub

    Private Sub rtbText_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rtbText.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
            Case Keys.Back
                e.SuppressKeyPress = True

            Case Else
                e.SuppressKeyPress = False
        End Select
    End Sub

    Private Sub DownloadFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DownloadFileToolStripMenuItem.Click
        Dim re As FileStream
        re = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\" + FileName)
        Dim q As Byte()
        q = Encoding.UTF8.GetBytes(clienthandlingthingies.getconsolidatedstring())
        re.Write(q, 0, q.Length)
        re.Close()
    End Sub

    Private Sub Editor_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If My.Computer.Keyboard.CtrlKeyDown Then
            If e.KeyCode = Keys.F Then
                FindAndReplace.Show()
            ElseIf e.KeyCode = Keys.P Then
                addParentheses()
            End If
            'TODO add more hotkeys here
        End If
    End Sub
    Public Sub ReplaceSelection(ByVal replacer As String)
        clienthandlingthingies.Generatereplace(rtbText.SelectionStart, rtbText.SelectionLength, replacer)
    End Sub
    Private Sub addParentheses()
        'Dim text As String = rtbText.SelectedText
        Dim startex As Integer = rtbText.SelectionStart
        Dim endex As Integer = rtbText.SelectionLength + startex
        clienthandlingthingies.AddParenthesis(startex, endex)
    End Sub

    Private Sub FindAndReplaceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FindAndReplaceToolStripMenuItem.Click
        FindAndReplace.Show()
    End Sub

    Private Sub cbxLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxLang.SelectedIndexChanged
        lang = cbxLang.SelectedItem
        updateColoring()
    End Sub

    Private Sub updateColoring()

    End Sub

    Private Sub rtbText_TextChanged(sender As Object, e As EventArgs) Handles rtbText.TextChanged
        updateColoring()
    End Sub
End Class