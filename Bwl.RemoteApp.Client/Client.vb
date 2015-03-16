
Module Client

    Private Function ConvPath(path As String) As String
        If Right(path, 1) = IO.Path.DirectorySeparatorChar Then path = path.Substring(0, path.Length - 2)
        Dim pathi = New IO.DirectoryInfo(path)
        path = pathi.FullName
        Return path
    End Function

    Sub Main(args() As String)
        Console.Title = "RemoteApp Client"
        ReDim Preserve args(10)
        If args(0) Is Nothing Then args(0) = ""
        Select Case args(0).ToLower
            Case "deploy-run"
                Console.WriteLine("RemoteAppClient: Deploy-Run")
                Dim target = args(1)
                Dim path = ConvPath(args(2))
                Dim command = args(3)
                Dim argsu = args(4)
                Console.WriteLine("Target: " + target)
                Console.WriteLine("Path: " + path)
                Console.WriteLine("Command: " + command)
                Console.WriteLine("Args: " + argsu)
                Dim client = Connect(target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    DeployFolder(client, path)
                    RunProcess(client, command, argsu)
                    client.Disconnect()
                End If
            Case "deploy"
                Console.WriteLine("RemoteAppClient: Deploy")
                Dim target = args(1)
                Dim path = ConvPath(args(2))
                Console.WriteLine("Target: " + target)
                Console.WriteLine("Path: " + path)
                Dim client = Connect(target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    DeployFolder(client, path)
                    client.Disconnect()
                End If
            Case "stop"
                Console.WriteLine("RemoteAppClient: Stop")
                Dim target = args(1)
                Console.WriteLine("Target: " + target)
                Dim client = Connect(target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    client.Disconnect()
                End If
            Case "run"
                Console.WriteLine("RemoteAppClient: Run")
                Dim target = args(1)
                Dim command = args(2)
                Dim argsu = args(3)
                Console.WriteLine("Target: " + target)
                Console.WriteLine("Command: " + command)
                Console.WriteLine("Args: " + argsu)
                Dim client = Connect(target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    RunProcess(client, command, argsu)
                    client.Disconnect()
                End If
            Case Else
                Console.WriteLine("RemoteAppClient: Help")
                Console.WriteLine("")
                Console.WriteLine("rapp-client deploy       <target-host:port> <source-path>")
                Console.WriteLine("rapp-client run          <target-host:port> <run-command> <run-args>")
                Console.WriteLine("rapp-client deploy-run   <target-host:port> <source-path> <run-cmd> <run-args>")
                Console.WriteLine("rapp-client stop         <target-host:port>")
                Console.WriteLine("")

        End Select
        For Each arg In args
            If arg IsNot Nothing AndAlso arg.ToLower = "-pause" Then Console.WriteLine("Press Enter...") : Console.ReadLine()
        Next
    End Sub

    Private Function Connect(target As String) As NetClient
        Try
            Dim client As New NetClient
            Dim targetparts = target.Split(":")
            client.Connect(targetparts(0), Val(targetparts(1)))
            Console.WriteLine("Connected: " + target)
            Return client
        Catch ex As Exception
            Console.WriteLine("Error: " + ex.Message)
            Return Nothing
        End Try

    End Function

    Private Sub StopProcess(client As NetClient)
        Dim msg As New NetMessage("S", "stop")
        client.SendMessageWaitAnswer(msg, "stop-ok", 60)
        Console.WriteLine("Process Stopped ")

    End Sub

    Private Sub RunProcess(client As NetClient, cmd As String, args As String)
        If args Is Nothing Then args = ""
        Dim msg As New NetMessage("S", "run", cmd, args)
        client.SendMessageWaitAnswer(msg, "run-ok", 60)
        Console.WriteLine("Process Started ")
    End Sub

    Private Sub DeployFolder(client As NetClient, path As String)
        Dim filelist As String()
        Try
            filelist = IO.Directory.GetFiles(path, "*.*", IO.SearchOption.AllDirectories)
        Catch ex As Exception
            Console.WriteLine("Error: " + ex.Message)
            Return
        End Try

        Console.WriteLine("files to copy: " + filelist.Length.ToString)
        For Each file In filelist
            Dim finf = New IO.FileInfo(file)
            Dim relpath = finf.DirectoryName
            If Right(relpath, 1) = IO.Path.DirectorySeparatorChar Then relpath = relpath.Substring(0, relpath.Length - 2)
            relpath = relpath.Replace(path + IO.Path.DirectorySeparatorChar, "")
            relpath = relpath.Replace(path, "")
            Dim name = finf.Name
            Console.WriteLine("--> " + relpath + IO.Path.DirectorySeparatorChar + name)
            Dim msg As New NetMessage("S", "copy", relpath, finf.Name)
            msg.PartBytes(3) = IO.File.ReadAllBytes(file)
            client.SendMessageWaitAnswer(msg, "copy-ok", 60)
        Next
    End Sub

End Module
