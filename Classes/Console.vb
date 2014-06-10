'This handles standard Input Output
Imports System.Console 'lol
Class Console_Class
    ReadOnly Name As String = "+Con"
    Dim Func As String = Nothing
    Public Function Say(ByVal Text As String, Optional ByVal Color As ConsoleColor = Nothing) As Integer
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Say Called with: (" & Text & " || " & Color & ")")
        End If
        If Color <> Nothing Then
            Dim TempVar As ConsoleColor = Console.ForegroundColor
            Console.ForegroundColor = Color
            Console.WriteLine(Text)
            Console.ForegroundColor = TempVar
        End If
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Say Ended")
        End If
        Return 0
    End Function

    Public Function Read(ByVal Input As String) As String
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Read Called with: (" & Input & ")")
            Console.WriteLine(Name & ".Read Ended")
        End If
        Return Console.ReadLine(Input)
    End Function

    Public Function TColor(ByVal Color As ConsoleColor) As Integer
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".TColor Called with: (" & Color & ")")
        End If
        Try
            Console.ForegroundColor = Color
        Catch
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".TColor " & Err.Description & vbNewLine & Name & ".TColor Ended")
            End If
            Return 1
        End Try
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".TColor Ended")
        End If
        Return 0
    End Function

    Public Function BColor(ByVal Color As ConsoleColor) As Integer
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".BColor Called with: (" & Color & ")")
        End If
        Try
            Console.BackgroundColor = Color
        Catch
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".BColor " & Err.Description & vbNewLine & Name & ".BColor Ended")
            End If
            Return 1
        End Try
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".BColor Ended")
        End If
        Return 0
    End Function
    
    Public Function KeyWords(ByVal Line As String) As Boolean 'Used for every class to determine usable functions
        If Line.StartsWith("Say") = True Then
            Return True
        ElseIf Line.StartsWith("Read") = True Then
            Return True
        ElseIf Line.StartsWith("TColor") = True Then
            Return True
        ElseIf Line.StartsWith("BColor") = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
