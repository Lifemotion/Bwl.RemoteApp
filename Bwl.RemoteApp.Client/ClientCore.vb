Public Delegate Sub RemoteAppCoreWriter(arg As String, color As ConsoleColor)

Class ClientCore
    Private _writer As RemoteAppCoreWriter

    Sub New(writer As RemoteAppCoreWriter)
        _writer = writer
    End Sub

    Private Sub Write(arg As String, Optional color As ConsoleColor = ConsoleColor.Gray)
        _writer.Invoke(arg, color)
    End Sub

    Private Function ConvPath(path As String) As String
        If Right(path, 1) = IO.Path.DirectorySeparatorChar Then path = path.Substring(0, path.Length - 2)
        Dim pathi = New IO.DirectoryInfo(path)
        path = pathi.FullName
        Return path
    End Function

    Public Property Target As String = ""
    Public Property Operation As String = ""
    Public Property SourcePath As String = ""
    Public Property RunCommand As String = ""
    Public Property RunArgs As String = ""

    Public Sub ParseArgs(args() As String)
        ReDim Preserve args(10)
        If args(0) Is Nothing Then args(0) = ""
        Select Case args(0).ToLower
            Case "deploy-run"
                _Operation = "deploy-run"
                _Target = args(1)
                _SourcePath = ConvPath(args(2))
                _RunCommand = args(3)
                _RunArgs = args(4)
            Case "deploy"
                _Operation = "deploy"
                _Target = args(1)
                _SourcePath = ConvPath(args(2))
                _RunCommand = ""
                _RunArgs = args(3)
            Case "stop"
                _Operation = "stop"
                _Target = args(1)
                _SourcePath = ""
                _RunCommand = ConvPath(args(2))
                _RunArgs = args(3)
            Case "run"
                _Operation = "deploy-run"
                _Target = args(1)
                _RunCommand = args(2)
                _RunArgs = args(3)
            Case Else
                _Operation = ""
                _Target = ""
                _SourcePath = ""
                _RunCommand = ""
                _RunArgs = ""
        End Select

        Write("Target: " + Target)
        Write("Path: " + SourcePath)
        Write("Command: " + RunCommand)
        Write("Args: " + RunArgs)

    End Sub

    Public Sub Process(args() As String)
        ParseArgs(args)
        Execute()
    End Sub

    Public Sub Execute()
        Select Case _Operation.ToLower
            Case "deploy-run"
                Write("RemoteAppClient: Deploy-Run")
                Write("Target: " + Target)
                Write("Path: " + SourcePath)
                Write("Command: " + RunCommand)
                Write("Args: " + RunArgs)
                Dim client = Connect(Target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    DeployFolder(client, SourcePath)
                    RunProcess(client, RunCommand, RunArgs)
                    client.Disconnect()
                End If
            Case "deploy"
                Write("RemoteAppClient: Deploy")
                Write("Target: " + Target)
                Write("Path: " + SourcePath)
                Dim client = Connect(Target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    DeployFolder(client, SourcePath)
                    client.Disconnect()
                End If
            Case "stop"
                Write("RemoteAppClient: Stop")
                Write("Target: " + Target)
                Write("Command: " + RunCommand)
                Dim client = Connect(Target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    client.Disconnect()
                End If
            Case "run"
                Write("RemoteAppClient: Run")
                Write("Target: " + Target)
                Write("Command: " + RunCommand)
                Write("Args: " + RunArgs)
                Dim client = Connect(Target)
                If client IsNot Nothing Then
                    StopProcess(client)
                    RunProcess(client, RunCommand, RunArgs)
                    client.Disconnect()
                End If
            Case Else
                Write("RemoteAppClient: Help")
                Write("")
                Write("rapp-client deploy       <target-host:port> <source-path>")
                Write("rapp-client run          <target-host:port> <run-command> <run-args>")
                Write("rapp-client deploy-run   <target-host:port> <source-path> <run-cmd> <run-args>")
                Write("rapp-client stop         <target-host:port>")
                Write("")
                Write("-pause       -wait for key to exit")
                Write("-hidden      -show no gui forms")
                Write("")
                Write("rapp-client.exe and rapp-client-gui-exe has the same interface")
        End Select
    End Sub

    Private Function Connect(target As String) As NetClient
        Try
            Dim client As New NetClient
            Dim targetparts = target.Split(":")
            If targetparts.Length > 1 Then
                client.Connect(targetparts(0), Val(targetparts(1)))
            Else
                client.Connect(targetparts(0), 3200)

            End If
            Write("Connected: " + target)
            Return client
        Catch ex As Exception
            Write("Error: " + ex.Message)
            Return Nothing
        End Try

    End Function

    Private Sub StopProcess(client As NetClient)
        Dim msg As New NetMessage("S", "stop")
        client.SendMessageWaitAnswer(msg, "stop-ok", 60)
        Write("Process Stopped ")

    End Sub

    Private Sub RunProcess(client As NetClient, cmd As String, args As String)
        If args Is Nothing Then args = ""
        Dim msg As New NetMessage("S", "run", cmd, args)
        client.SendMessageWaitAnswer(msg, "run-ok", 60)
        Write("Process Started ")
    End Sub

    Private Sub DeployFolder(client As NetClient, path As String)
        Dim filelist As String()
        Try
            filelist = IO.Directory.GetFiles(path, "*.*", IO.SearchOption.AllDirectories)
        Catch ex As Exception
            Write("Error: " + ex.Message)
            Return
        End Try

        Write("files to copy: " + filelist.Length.ToString)
        For Each file In filelist
            Dim finf = New IO.FileInfo(file)
            Dim relpath = finf.DirectoryName
            If Right(relpath, 1) = IO.Path.DirectorySeparatorChar Then relpath = relpath.Substring(0, relpath.Length - 2)
            relpath = relpath.Replace(path + IO.Path.DirectorySeparatorChar, "")
            relpath = relpath.Replace(path, "")
            Dim name = finf.Name
            Write("--> " + relpath + IO.Path.DirectorySeparatorChar + name)
            Dim msg As New NetMessage("S", "copy", relpath, finf.Name)
            msg.PartBytes(3) = IO.File.ReadAllBytes(file)
            client.SendMessageWaitAnswer(msg, "copy-ok", 60)
        Next
    End Sub

End Class
