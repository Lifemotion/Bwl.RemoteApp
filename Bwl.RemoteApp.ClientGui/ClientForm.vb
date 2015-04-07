Public Class ClientForm
    Private _args As String()

    Public Sub New(args() As String)
        Me.InitializeComponent()
        _args = args
    End Sub

    Public Sub Write(text As String, color As ConsoleColor)
        Try
            Me.Invoke(Sub()
                          TextBox1.Text += text + vbCrLf
                          If TextBox1.Text.Length > 2000 Then TextBox1.Text = ""
                      End Sub)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Clear()
        Try
            Me.Invoke(Sub()
                          TextBox1.Text = ""
                      End Sub)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClientForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim file = IO.File.ReadAllLines(".remoteappclient.config")
            For Each line In file
                Dim parts = line.Split("=")
                If parts.Length = 2 Then
                    If parts(0) = "operation" Then operationComboBox.Text = parts(1)
                    If parts(0) = "target" Then targetTextbox.Text = parts(1)
                    If parts(0) = "sourcepath" Then sourcePathTextbox.Text = parts(1)
                    If parts(0) = "runcommand" Then runCommandTextbox.Text = parts(1)
                    If parts(0) = "runargs" Then runArgsTextbox.Text = parts(1)
                End If
            Next

        Catch ex As Exception

        End Try
  
        Dim core = New ClientCore(AddressOf Write)
        core.ParseArgs(_args)

        If core.Operation > "" Then operationComboBox.Text = core.Operation
        If core.Target > "" Then targetTextbox.Text = core.Target
        If core.SourcePath > "" Then sourcePathTextbox.Text = core.SourcePath
        If core.RunCommand > "" Then runCommandTextbox.Text = core.RunCommand
        If core.RunArgs > "" Then runArgsTextbox.Text = core.RunArgs

        If _args.Length > 2 Then
            core.Execute()
        End If
    End Sub

    Private Sub runButton_Click(sender As Object, e As EventArgs) Handles runButton.Click
        Try
            Dim lines As New List(Of String)
            lines.Add("operation=" + operationComboBox.Text)
            lines.Add("target=" + targetTextbox.Text)
            lines.Add("sourcepath=" + sourcePathTextbox.Text)
            lines.Add("runcommand=" + runCommandTextbox.Text)
            lines.Add("runargs=" + runArgsTextbox.Text)
            IO.File.WriteAllLines(".remoteappclient.config", lines.ToArray)
        Catch ex As Exception

        End Try
        Clear()
        Dim core = New ClientCore(AddressOf Write)
        core.Operation = operationComboBox.Text
        core.SourcePath = sourcePathTextbox.Text
        core.RunCommand = runCommandTextbox.Text
        core.RunArgs = runArgsTextbox.Text
        core.Target = targetTextbox.Text
        core.Execute()
    End Sub
End Class
