Module HostMainGui
    Dim form As HostForm
    Dim core As HostCore

    Public Sub RemoteAppCoreWriter(arg As String, color As ConsoleColor)
        If form IsNot Nothing Then
            form.Write(arg)
        End If
    End Sub

    Public Sub RemoteAppCoreWriteDebug(arg As String, color As ConsoleColor)
        Debug.WriteLine(arg)
    End Sub

    Public Sub Main()
        If Command.ToLower.Contains("-hidden") = True Then
            If form IsNot Nothing Then form.Show()
            core = New HostCore(AddressOf RemoteAppCoreWriteDebug)
            core.Start(3200)
            Application.Run()
        Else
            form = New HostForm
            If form IsNot Nothing Then form.Show()

            core = New HostCore(AddressOf RemoteAppCoreWriter)
            core.Start(3200)
            Application.Run(form)
        End If
    End Sub
End Module
