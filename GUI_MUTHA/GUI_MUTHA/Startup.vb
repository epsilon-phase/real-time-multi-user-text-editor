Imports System
Imports System.IO
Imports System.Text


Public Class Startup

    #Region "Fields"

    Public FileName, FileName2, IP As String

    Dim Dal As Boolean = False
    Dim Dal2 As Boolean = False

    #End Region 'Fields

    #Region "Methods"

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        FileName = ComboBox.Text
        Dim fs As FileStream = File.Create("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(FileName)
        fs.Write(info, 0, info.Length)
        fs.Close()

        FileName2 = TextName.Text
        Dim fs2 As FileStream = File.Create("C:\Users\user\AppData\Local\Temp\Name.txt")
        Dim info2 As Byte() = New UTF8Encoding(True).GetBytes(FileName2)
        fs2.Write(info2, 0, info2.Length)
        fs2.Close()

        IP = ipText.Text
        'check if the IPaddress is valid, or at least formatted correctly
        If OperationalTransform.ClientForSam.ValidateIPAddress(IP) Then
            Editor.Show()
            Me.Hide()
        Else
            'Otherwise tell the user that they did it wrong.
            MessageBox.Show("Your IP address is not formatted correctly. The correct form is xxx.xxx.xxx.xxx")
        End If
    End Sub

    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Editor.Close()
        Me.Close()
    End Sub

    Private Sub ipText_TextChanged(sender As System.Object, e As System.EventArgs) Handles ipText.TextChanged
        If ipText.Text <> "What is your IP?" Then
            ComboBox.Enabled = True
            TextName.Enabled = True
            Button2.Enabled = True
        End If
    End Sub

    Private Sub ipText_TextClicked(sender As System.Object, e As System.EventArgs) Handles ipText.Click
        ipText.ForeColor = Color.Black

        If Dal2 = False Then
            ipText.Text = ""
            Dal2 = True
        End If
    End Sub

    Private Sub Startup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Editor.Hide()
        Button2.Enabled = False
        TextName.Enabled = False
        ComboBox.Enabled = False
    End Sub

    Private Sub TextName_TextClicked(sender As System.Object, e As System.EventArgs) Handles TextName.Click
        TextName.ForeColor = Color.Black

        If Dal = False Then
            TextName.Text = ""
            Dal = True
        End If
    End Sub

    #End Region 'Methods

End Class