Public Class Form2

    Private selectedIndex As Integer = -1
    Public Function getSelectedIndex() As Integer
        Return selectedIndex
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        selectedIndex = ListBox1.SelectedIndex
        Me.Close()

    End Sub

    'cancel button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        selectedIndex = -1
        Me.Close()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        selectedIndex = -1

        'listboxに過去logを表示
        ListBox1.Items.Clear()
        For i As Integer = 0 To g_logs - 1
            ListBox1.Items.Add(g_log(i).targetFile)
        Next

        If g_logs = 0 Then
            MessageBox.Show("No logs found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            ListBox1.SelectedIndex = 0 ' Select the first item by default
        End If

    End Sub




End Class