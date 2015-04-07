Public Delegate Sub RemoteAppCoreWriter(arg As String, color As ConsoleColor)

Public Class HostCore
    Private Class ClientInfo
        Public Property Username As String = ""
        Public Property Group As String = ""
    End Class

    Private _writer As RemoteAppCoreWriter

    Sub New(writer As RemoteAppCoreWriter)
        _writer = writer
    End Sub

    Private WithEvents _netServer As New NetServer

    Private Sub Write(arg As String, Optional color As ConsoleColor = ConsoleColor.Gray)
        _writer.Invoke(arg, color)
    End Sub

    Public Sub Start(port As Integer)
        _netServer.StartServer(port)
        Write("Started RemoteApp Host on port: " + port.ToString, ConsoleColor.Cyan)
        Write("AppPath: """ + DataPath + """", ConsoleColor.Gray)
        Write("Waiting for connections...", ConsoleColor.Cyan)
    End Sub

    Private prc As Process
    Private Sub StopProcess()
        If prc IsNot Nothing Then
            Try
                prc.Kill()
                prc = Nothing
            Catch ex As Exception
            End Try
            Write("Stopped process", ConsoleColor.Green)
        Else
            Write("Process wasn't run", ConsoleColor.Gray)
        End If
    End Sub

    Private Sub RunProcess(cmd As String, args As String)
        StopProcess()
        Try
            prc = New Process()
            prc.StartInfo = New ProcessStartInfo
            With prc.StartInfo
                .WorkingDirectory = DataPath
                .FileName = cmd
                .Arguments = args
            End With
            prc.Start()
            Write("Started process: " + cmd + " " + args, ConsoleColor.Green)
        Catch ex As Exception
            Write(ex.Message)
        End Try
    End Sub

    Public ReadOnly Property DataPath As String
        Get
            Dim dp = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app")
            Return dp
        End Get
    End Property

    Private Sub _netServer_ReceivedMessage(message As NetMessage, client As ConnectedClient) Handles _netServer.ReceivedMessage
        SyncLock _netServer
            If client.tag(0) Is Nothing Then client.tag(0) = New ClientInfo
            Dim info As ClientInfo = client.tag(0)
            If info.Username > "" Then

            Else
                If message.Part(0) = "login" Then
                    Dim name = message.Part(1)
                    If name > "" Then
                        info.Username = name
                        Write(client.ID.ToString + "-> " + "Logged as " + name + " from " + client.IPAddress, ConsoleColor.Green)
                        client.SendMessage(New NetMessage("S", "login-ok"))
                    End If
                Else
                    '  _logger.AddWarning(client.ID.ToString + "-> " + "Trying to use " + message.Part(0) + " when not logged, from " + client.IPAddress)
                End If

                If message.Part(0) = "copy" Then
                    Try
                        Dim relpath = message.Part(1).Replace("\", IO.Path.DirectorySeparatorChar)
                        Dim filename = message.Part(2)
                        Dim fullpath = IO.Path.Combine(DataPath, relpath, filename)
                        Dim bytes = message.PartBytes(3)
                        IO.Directory.CreateDirectory(IO.Path.Combine(DataPath, relpath))
                        IO.File.WriteAllBytes(fullpath, bytes)
                        Write("Copied " + filename + " " + bytes.Length.ToString + " bytes", ConsoleColor.Gray)
                        client.SendMessage(New NetMessage("S", "copy-ok"))
                    Catch ex As Exception
                        Write(ex.Message)

                    End Try

                End If
                If message.Part(0) = "stop" Then
                    StopProcess()
                    client.SendMessage(New NetMessage("S", "stop-ok"))

                End If
                If message.Part(0) = "run" Then
                    RunProcess(message.Part(1), message.Part(2))
                    client.SendMessage(New NetMessage("S", "run-ok"))

                End If
            End If
        End SyncLock
    End Sub
End Class
