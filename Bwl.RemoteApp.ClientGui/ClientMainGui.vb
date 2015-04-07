Module ClientMainGui
    Dim form As ClientForm
 
    Public Sub Main(args As String())
        Application.EnableVisualStyles()
        If Command.ToLower.Contains("-hidden") = True Then
            Dim core = New ClientCore(Sub(arg As String, color As ConsoleColor)
                                          Debug.WriteLine(arg)
                                      End Sub)
            Application.Run()
        Else
            Dim form = New ClientForm(args)
            form.Show()
            Application.Run(form)
        End If
    End Sub
End Module
