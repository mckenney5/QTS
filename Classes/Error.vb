'This handles errors, warnings, and info (tips)
Imports System.IO 'until we have an IO class
Class Error_Class
    Dim CC As New Console_Class
    Public Errors() As Array
    Public LogFile As String = Nothing
    Public Handle As UInteger = 0
    Public Ignore_Warnings As Boolean = False
    Public Ignore_Info As Boolean = False
    Public Ignore_Errors As Boolean = False
    ReadOnly Name As String = "+ERR"
    Dim Func As String = Nothing

    Public Function Er(Optional ByVal Desc As String = Nothing, Optional ByVal LineNumber As ULong = Nothing, Optional ByVal UseBeep As Boolean = False) As Boolean
        Func = "Er" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Er Called with: (" & Desc & " || " & LineNumber & " || " & UseBeep & ")")
        End If
        Err.Clear()
        If Ignore_Errors = True Then
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Er Ignoring error call." & vbNewLine & Name & ".Er Ended")
            End If
            Return True
        End If
        If UseBeep = True Then
            Console.Beep(100, 100) 'Change this
        End If
        If Desc = Nothing AndAlso LineNumber = Nothing AndAlso Handle = Nothing Then
            Console.Error.WriteLine(Name & ".Er Error: An error was thrown without a cause!")
        ElseIf Handle = 0 AndAlso LineNumber <> 0 Then
            CC.Say("Line (" & LineNumber & ") Error: " & Desc, ConsoleColor.Red)
        ElseIf Handle = 0 Then
            CC.Say("Error: " & Desc, ConsoleColor.Red)
        ElseIf Handle = 1 AndAlso LineNumber <> 0 Then
            MsgBox("Line (" & LineNumber & ") Error: " & Desc, 16, )
        ElseIf Handle = 1 Then
            MsgBox("Error: " & Desc, 16, )
        ElseIf Handle = 2 AndAlso LineNumber <> 0 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Line (" & LineNumber & ") Error: " & Desc & vbNewLine)
            Else
                Console.Error.WriteLine(Name & ".Er Error: The log file can not be found! (" & LogFile & ")")
            End If
        ElseIf Handle = 2 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Error: " & Desc & vbNewLine)
            Else
                Console.Error.WriteLine(Name & ".Er Error: The log file can not be found! (" & LogFile & ")")
            End If
        End If
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Er Ended")
        End If
        Return True
    End Function

    Public Function Warn(Optional ByVal Desc As String = Nothing, Optional ByVal LineNumber As ULong = Nothing, Optional ByVal UseBeep As Boolean = False) As Boolean
        Func = "Warn" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Warn Called with: (" & Desc & " || " & LineNumber & " || " & UseBeep & ")")
        End If
        If Ignore_Warnings = True Then
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Warn Ignoring warning call." & vbNewLine & Name & ".Warn Ended")
            End If
            Return True
        End If
        If Treat_Warns_As_Errors = True Then
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Warn Treating warning as error")
            End If
            Er(Desc, LineNumber, UseBeep)
        End If
        If UseBeep = True Then
            Console.Beep(100, 100) 'Change this
        End If
        If Desc = Nothing AndAlso LineNumber = Nothing AndAlso Handle = Nothing Then
            Console.WriteLine(Name & ".Warn Error: A warning was thrown without a cause!")
        ElseIf Handle = 0 AndAlso LineNumber <> 0 Then
            CC.Say("Line (" & LineNumber & ") Warning: " & Desc, ConsoleColor.Yellow)
        ElseIf Handle = 0 Then
            CC.Say("Warning: " & Desc, ConsoleColor.Yellow)
        ElseIf Handle = 1 AndAlso LineNumber <> 0 Then
            MsgBox("Line (" & LineNumber & ") Warning: " & Desc, 48, )
        ElseIf Handle = 1 Then
            MsgBox("Error: " & Desc, 48, )
        ElseIf Handle = 2 AndAlso LineNumber <> 0 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Line (" & LineNumber & ") Warning: " & Desc & vbNewLine)
            Else
                Console.WriteLine(Name & ".Warn Error: The log file can not be found! (" & LogFile & ")")
            End If
        ElseIf Handle = 2 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Warning: " & Desc & vbNewLine)
            Else
                Console.WriteLine(Name & ".Warn Error: The log file can not be found! (" & LogFile & ")")
            End If
        End If
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Warn Ended")
        End If
        Return True
    End Function

    Public Function Info(Optional ByVal Desc As String = Nothing, Optional ByVal LineNumber As ULong = Nothing, Optional ByVal UseBeep As Boolean = False) As Boolean
        Func = "Info" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Info Called with: (" & Desc & " || " & LineNumber & " || " & UseBeep & ")")
        End If
        If Ignore_Info = True Then
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Info Ignoring info call." & vbNewLine & Name & ".Info Ended")
            End If
            Return True
        End If
        If UseBeep = True Then
            Console.Beep(100, 100) 'Change this
        End If
        If Desc = Nothing AndAlso LineNumber = Nothing AndAlso Handle = Nothing Then
            Console.WriteLine(Name & ".Info Error: An info message was thrown without a cause!")
        ElseIf Handle = 0 AndAlso LineNumber <> 0 Then
            CC.Say("Line (" & LineNumber & ") Info: " & Desc, ConsoleColor.Cyan)
        ElseIf Handle = 0 Then
            CC.Say("Info: " & Desc, ConsoleColor.Cyan)
        ElseIf Handle = 1 AndAlso LineNumber <> 0 Then
            MsgBox("Line (" & LineNumber & ") Info: " & Desc, 16, )
        ElseIf Handle = 1 Then
            MsgBox("Info: " & Desc, 16, )
        ElseIf Handle = 2 AndAlso LineNumber <> 0 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Line (" & LineNumber & ") Info: " & Desc & vbNewLine)
            Else
                Console.WriteLine(Name & ".Info Error: The log file can not be found! (" & LogFile & ")")
            End If
        ElseIf Handle = 2 Then
            If File.Exists(LogFile) = True Then
                File.AppendAllText(LogFile, "Info: " & Desc & vbNewLine)
            Else
                Console.WriteLine(Name & ".Info Error: The log file can not be found! (" & LogFile & ")")
            End If
        End If
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Info Ended")
        End If
        Return True
    End Function
End Class