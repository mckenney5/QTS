﻿'This handles multiple vars and system info
Class Vars_Class
    Dim EC As New Error_Class
    ReadOnly Name As String = "+Vars"
    Dim Func As String = Nothing
    Public UserName As String = My.Computer.Name
    Public OperatingSystem As String = My.Computer.Info.OSFullName
    Public OS As String = OperatingSystem
    Public Const ENTER As Char = vbCrLf
    Public Const TAB As Char = vbTab


    Public Function Info() As Boolean
        Try
            Console.WriteLine("Username:                   " & Environment.UserName)
            Console.WriteLine("Computer Name:              " & My.Computer.Name)
            Console.WriteLine("Operating System:           " & My.Computer.Info.OSFullName)
            Console.WriteLine("OS Version:                 " & My.Computer.Info.OSVersion)
            If OS = "Unix" Then
                Console.WriteLine("System Dir:                 /")
            Else
                Console.WriteLine("System Dir:                 " & Environment.SystemDirectory)
            End If
            Console.WriteLine("Number of CPU(s):           " & Environment.ProcessorCount)
            'Console.WriteLine("Local IPv4:                 " & System.Net.GetLocalIpAddress().ToString)
            If OS.Contains("Windows") = True Then
                Console.WriteLine("Total VMem:                 " & Math.Round(My.Computer.Info.TotalVirtualMemory / 1024 / 1024).ToString & " MB")
                Console.WriteLine("Total PMem:                 " & Math.Round(My.Computer.Info.TotalPhysicalMemory / 1024 / 1024).ToString & " MB")
            End If
            Return True
        Catch
            EC.Er(Err.Description)
            Return False
        End Try
    End Function
    
    Public Function KeyWords(ByVal Line As String) As Boolean 'Used for every class to determine usable functions
        If Line.StartsWith("Info") = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Class