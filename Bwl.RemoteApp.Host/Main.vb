Module Main

    Dim core As RemoteAppCore

    Public Sub Main()
        Console.Title = "RemoteApp Host"
        core = New RemoteAppCore()
        core.Start(3200)
        While True
            Threading.Thread.Sleep(10)
            If Console.KeyAvailable Then If Console.ReadKey.Key = ConsoleKey.Escape Then End
        End While
    End Sub
End Module
