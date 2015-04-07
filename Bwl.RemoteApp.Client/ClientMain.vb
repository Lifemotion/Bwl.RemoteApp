Module ClientMain

    Public Sub RemoteAppCoreWriter(arg As String, color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(arg)
    End Sub

    Dim core As ClientCore

    Public Sub Main(args() As String)
        Console.Title = "RemoteApp Client"
        core = New ClientCore(AddressOf RemoteAppCoreWriter)
        core.Process(args)
        For Each arg In args
            If arg IsNot Nothing AndAlso arg.ToLower = "-pause" Then Console.WriteLine("Press Enter...") : Console.ReadLine()
        Next
    End Sub
End Module
