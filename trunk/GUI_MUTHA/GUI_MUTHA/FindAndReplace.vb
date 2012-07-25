Public Class FindAndReplace

    Private Sub txtFind_TextChanged(sender As Object, e As EventArgs) Handles txtFind.TextChanged
        Dim count As Integer = Editor.rtbText.Find(txtFind.Text)
        lblCount.Text = "Found " + count + " occurences."
    End Sub

    Private Sub btnReplaceAll_Click(sender As Object, e As EventArgs) Handles btnReplaceAll.Click
        Editor.rtbText.Text.Replace(txtFind.Text, txtReplace.Text)
    End Sub

    Private Sub btnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        If Not Editor.rtbText.SelectedText = txtFind.Text Then
            Editor.find(txtFind.Text)
        End If
        Editor.rtbText.SelectedText = txtReplace.Text

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If (Not Editor.rtbText.SelectedText = txtFind.Text) And (Not Editor.rtbText.SelectionStart = Editor.rtbText.TextLength - 1) Then
            Editor.find(txtFind.Text)
        Else
            Editor.find(txtFind.Text, Editor.rtbText.SelectionStart + 1)
        End If
    End Sub

    Private Sub Close_Click(sender As System.Object, e As System.EventArgs) Handles Close.Click
        Me.Hide()
    End Sub
End Class