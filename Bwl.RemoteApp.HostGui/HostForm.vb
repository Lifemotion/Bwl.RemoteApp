Public Class HostForm
    Public Sub Write(text As String)
        Try
            Me.Invoke(Sub()
                          TextBox1.Text += text + vbCrLf
                          If TextBox1.Text.Length > 2000 Then TextBox1.Text = ""
                      End Sub)
        Catch ex As Exception
        End Try
    End Sub
End Class
