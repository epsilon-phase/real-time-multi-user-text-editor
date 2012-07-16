Imports System
Imports System.IO
Imports System.Text

Public Class Startup

    Dim FileName, FileName2 As String
    Dim Dal As Boolean = False

    Private Sub Startup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Editor.Hide()
    End Sub

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

        Editor.Show()
        Me.Hide()
    End Sub

    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Editor.Close()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextName.Click
        TextName.ForeColor = Color.Black

        If Dal = False Then
            TextName.Text = ""
            Dal = True
        End If

    End Sub
End Class