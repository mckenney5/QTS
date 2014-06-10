'This class handles debugging of the interpreter
Imports System
Imports System.Console
Class Debug
    Const Name As String = "Debug"
    Public Function DWrite(ByVal Info As String) As Boolean
        Console.WriteLine(Info)
        Return False
    End Function
    Public Sub Test()
        Func = ".Test" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Called")
        End If
        'this is where you can test all the features of the compiler
        'Debugging Only
        Try
            Console.WriteLine(Name & "Setting Verbose_Mode On")
            Verbose_Mode = True
            Console.WriteLine(Name & "Loading all libraries")
            Dim i As UInteger = 0
            Console.Write(Name & "Error_Class: " & vbTab & vbTab)
            Dim EC As New Error_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "Console_Class: ")
            Dim CC As New Console_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "File_IO_Class: " & vbTab & vbTab)
            Dim FC As New File_IO_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "Help_Class: " & vbTab & vbTab)
            Dim HC As New Help_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "Math_Class: " & vbTab & vbTab)
            Dim MC As New Math_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "Text_Class: " & vbTab & vbTab)
            Dim TC As New Text_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & "Vars_Class: " & vbTab & vbTab)
            Dim VC As New Vars_Class
            Console.WriteLine("DONE")
            i += 1
            Console.Write(Name & " " & i & " classes loaded" & vbNewLine)
            Console.WriteLine("Welcome to QTS Debugger Shell!")
            Do While True
                Console.Write("> ")
                Dim Input As String = Console.ReadLine()
                If Input = "help" Then

                ElseIf Input.StartsWith("say ") = True Then
                    CC.Say(Input.Remove(0, 4))
                ElseIf Input = Nothing Then
                    'do nothing
                Else
                    Console.WriteLine("Unknown Command")
                End If
            Loop
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message & vbNewLine)
            Do While True
                Console.WriteLine("Press 1 to reload test, Press 2 to exit")
                Dim Temp As Char = ChrW(Console.Read())
                If Temp = "1" Then
                    Test()
                ElseIf Temp = "2" Then
                    Environment.Exit(1)
                Else
                    Console.WriteLine()
                End If
            Loop
        End Try
    End Sub
End Class