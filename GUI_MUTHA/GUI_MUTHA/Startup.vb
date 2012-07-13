Imports System
Imports System.IO
Imports System.Text

Public Class Startup

    Dim FileName As String

    Private Sub Startup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Editor.Hide()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        FileName = ComboBox.Text
        Dim fs As FileStream = File.Create("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(FileName)
        fs.Write(info, 0, info.Length)
        fs.Close()

        Editor.Show()
        Me.Hide()
    End Sub

    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Editor.Close()
        Me.Close()
    End Sub
End Class