Module HostMain

    Public Sub RemoteAppCoreWriter(arg As String, color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(arg)
    End Sub

    Dim core As HostCore

    Public Sub Main()
        Console.Title = "RemoteApp Host"
        core = New HostCore(AddressOf RemoteAppCoreWriter)
        core.Start(3200)
        While True
            Threading.Thread.Sleep(10)
            If Console.KeyAvailable Then If Console.ReadKey.Key = ConsoleKey.Escape Then End
        End While
    End Sub
End Module
