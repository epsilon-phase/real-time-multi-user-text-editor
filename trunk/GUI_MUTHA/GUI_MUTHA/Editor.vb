﻿Imports System
Imports System.IO
Imports System.Text

Public Class Editor
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Long) As Integer
    Dim Directory As String
    Dim FileName As String

    Private Sub Editor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Startup.Hide()

        FileName = My.Computer.FileSystem.ReadAllText("C:\Users\user\AppData\Local\Temp\Doc.txt")
        Directory = "C:\Users\user\AppData\Local\Temp\" + FileName + ".txt"
        rtbText.Text = My.Computer.FileSystem.ReadAllText(Directory)
    End Sub

    Private Sub closeButton_Click(sender As System.Object, e As System.EventArgs) Handles closeButton.Click
        Startup.Close()
        Me.Close()
    End Sub

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

End Class