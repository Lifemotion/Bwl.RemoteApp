Module TestApp

    Sub Main()
        Console.ForegroundColor = ConsoleColor.Yellow
        Do
            Console.WriteLine("TestApp Running: " + DateTime.Now.ToLongTimeString)
            If Console.KeyAvailable Then If Console.ReadKey.Key = ConsoleKey.Escape Then End
            Threading.Thread.Sleep(1000)
        Loop
    End Sub

End Module
