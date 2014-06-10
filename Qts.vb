'This is the heart of the program with 3 major functions: The user interface, a scanner, and an interpreter to run the program

#Region "License"
'*  GNU License Agreement
'*  ---------------------
'*  This program is free software; you can redistribute it and/or modify
'*  it under the terms of the GNU General Public License version 3 as
'*  published by the Free Software Foundation.
'*
'*  This program is distributed in the hope that it will be useful,
'*  but WITHOUT ANY WARRANTY; without even the implied warranty of
'*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'*  GNU General Public License for more details.
'*
'*  You should have received a copy of the GNU General Public License
'*  along with this program; if not, write to the Free Software
'*  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA
'*
'*  http://www.gnu.org/licenses/gpl-3.0.txt
#End Region

Imports System.Console
Imports System.IO
Imports System.String
Imports System.Text
Module Interpreter

#Region "Variables"
    Dim EC As New Error_Class
    Const Ver As String = "0.0.0.1"
    Public Treat_Warns_As_Errors As Boolean = False
    Public Verbose_Mode As Boolean = True 'False 'Classes done so far: Error, Console, 
    Dim Check_Only As Boolean = False 'exit after checking for errors/warnings
    Dim OS As String = My.Computer.Info.OSFullName 'determines Operating System Type
    Dim Classes() As String 'hold a list of classes imported or to be imported
    Const Name As String = "+QTS"
    Dim Func As String = Nothing
    Public Variables() As String 'Holds vars in RAM (better than my old method of making a text file)
    Dim a As ULong = 0 'counter for Variables()
#End Region

    Sub Main(ByVal Args() As String)
        'If debugging, uncomment the next line
        'Test()
        Console.Title = "QTS"
        Func = ".Main" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "called with " & Args.Length & " arg(s)")
        End If
        If Args.Length <> 0 Then 'Command Line Variables
            For i As UInteger = 0 To Args.Length
                If Args(i) = "-h" Or Args(i) = "--help" Then
                    Dim HC As New Help_Class
                    HC.Help()
                ElseIf Args(i) = "-man" Or Args(i) = "--manual" Then
                    Dim HC As New Help_Class
                    HC.Man()
                ElseIf Args(i) = "-man" Or Args(i) = "--manual" Then
                    Dim HC As New Help_Class
                    HC.Doc()
                ElseIf Args(i) = "-vb" Or Args(i) = "--verbose" Then
                    Verbose_Mode = True
                    Console.WriteLine("Verbose mode on!")
                ElseIf Args(i) = "-v" Or Args(i) = "--version" Then
                    Console.WriteLine("Version " & Ver)
                ElseIf Args(i) = "-n" Or Args(i) = "--nowarning" Then
                    Treat_Warns_As_Errors = True
                    Console.WriteLine("Treating warnings as errors!")
                ElseIf Args(i) = "-c" Or Args(i) = "--checkonly" Then
                    Check_Only = True
                    Console.WriteLine("Checking file only, not running it!")
                ElseIf Args(i) = "-iw" Or Args(i) = "--ignorewarnings" Then
                    EC.Ignore_Warnings = True
                    WriteLine("Supressing all warnings!")
                ElseIf Args(i) = "-ii" Or Args(i) = "--ignoreinfo" Then
                    EC.Ignore_Info = True
                    WriteLine("Supressing all recommendations!")
                ElseIf Args(i) = "-ie" Or Args(i) = "--ignoreerrors" Then
                    EC.Ignore_Errors = True
                    WriteLine("Skipping lines with errors! (NOT RECOMMENDED)")
                Else
                    If File.Exists(Args(i)) = True Then
                        If Verbose_Mode = True Then
                            Console.WriteLine(Name & Func & "calling Scan with the arg " & Args(i) & vbNewLine & _
                                              Name & Func & "Ended")
                        End If
                        Scan(Args(i))
                    Else
                        Console.WriteLine("Invalid argument or file not found")
                    End If
                End If
            Next
        Else
            If Verbose_Mode = True Then
                Console.WriteLine(Name & Func & "calling UI" & vbNewLine & _
                                  Name & Func & "Ended")
            End If
            UI()
        End If
    End Sub

    Sub UI() 'User interface
        Func = ".UI" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Called")
        End If
        Console.Write("==> ")
        Dim Input As String = Console.ReadLine()
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "was given the command " & Input)
        End If
        Dim L_Input As String = Input '.ToLower()
        If L_Input = "help" Then
            Dim HC As New Help_Class
            HC.Help()
        ElseIf L_Input = "exit" Or L_Input = "quit" Then
            QTS_Exit(0)
        ElseIf L_Input = "ver" Or L_Input = "version" Then
            Console.WriteLine(Ver)
        ElseIf L_Input = Nothing Then
            'do nothing
        Else
            If File.Exists(Input) = True Then
                If Verbose_Mode = True Then
                    Console.WriteLine(Name & Func & "Calling Scan with the arg " & Input & vbNewLine & _
                                              Name & Func & "Ended")
                End If
                Scan(Input)
            Else
                Console.WriteLine("Invalid command or file not found")
            End If
        End If
        UI()
    End Sub

    Private Sub Scan(ByVal FileName As String) 'Checks for errors/warnings
        Func = ".Scan" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Called with " & FileName)
        End If
        Dim ErrorOccured As Boolean = False
        If File.Exists(FileName) = False Then
            EC.Ignore_Errors = False
            If Verbose_Mode = True Then
                Console.WriteLine(Name & Func & "Threw an error")
            End If
            EC.Er("Source code can not be found!", , True)
            QTS_Exit(1)
        Else
            Dim Source() As String = Clean_Up_File(File.ReadAllLines(FileName))
            Dim b As UInteger = 0 '2nd counter
            For i As ULong = 0 To Source.Length 'loops through every line of the source file
                Try
                    If Verbose_Mode = True Then
                        Console.WriteLine(Name & Func & "determining what classes to call")
                    End If
                    Do Until Source(i).StartsWith("include ") = False 'determines what libraries should be called
                        If Source(i).Remove(0, 8) = "console" Then
                            Classes(b) = "console"
                            b += 1
                        ElseIf Source(i).Remove(0, 8) = "vars" Then
                            Classes(b) = "vars"
                            b += 1
                        ElseIf Source(i).Remove(0, 8) = "text" Then
                            Classes(b) = "text"
                            b += 1
                        ElseIf Source(i).Remove(0, 8) = "fileio" Then
                            Classes(b) = "fileio"
                            b += 1
                        ElseIf Source(i).Remove(0, 8) = "math" Then
                            Classes(b) = "math"
                            b += 1
                        ElseIf Source(i).Remove(0, 8) = "crypto" Then
                            Classes(b) = "crypto"
                            b += 1
                        Else
                            EC.Warn("library """ & Source(i).Remove(0, 8) & """ not found", i)
                            If Verbose_Mode = True Then
                                Console.WriteLine(Name & Func & "removing missing library from list")
                            End If
                            Source(i) = Nothing
                        End If
                        i += 1
                    Loop
                    'End Of Include Files
                    b = 0
                    'Checks for vars between Include and Main
                    Do Until Source(i).StartsWith("Main")
                        If i = Source.Length Then
                            EC.Er("Main was not found in the program")
                            ErrorOccured = True
                        Else
                            If Source(i).StartsWith("Set") = True Then
                                Check_Var(Source(i), i)
                            End If
                        End If
                    Loop
                    
                    If EC.Errors.Count <> 0 Then
                        For i As UInteger = 0 to EC.Errors.Count
                            'Do something?
                        Next
                        QTS_Exit(0)
                    End If
                    Run(Source)
                Catch
                    If Verbose_Mode = True Then
                        Console.WriteLine(Name & Func & "Threw an error")
                    End If
                    EC.Er(Err.Description, i)
                    If EC.Ignore_Errors = False Then
                        QTS_Exit(1)
                    End If
                End Try
            Next
        End If
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Ended")
        End If
    End Sub

    Private Sub Run(ByVal Source() As String) 'runs the program
        Func = ".Run" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Called with an array with " & Source.Length & " items")
        End If
        If Classes.Length <> 0 Then
            For i = 0 To Classes.Length
                If Classes(i) = "console" Then
                    Dim CC As New Console_Class
                ElseIf Classes(i) = "vars" Then
                    Dim VC As New Vars_Class
                ElseIf Classes(i) = "text" Then
                    Dim TC As New Text_Class
                ElseIf Classes(i) = "fileio" Then
                    Dim FC As New File_IO_Class
                ElseIf Classes(i) = "math" Then
                    Dim MC As New Math_Class
                ElseIf Classes(i) = "crypto" Then
                    Dim CyC As New Cryptography_Class
                Else
                    If IsDeprecated(Classes(i)) = True Then
                        EC.Warn(Classes(i) & " is deprecated!")
                        'Some how load it?
                    End If
                End If
            Next
            If Verbose_Mode = True Then
                Console.WriteLine(Name & Func & "Finished loading classes")
            End If
        End If
        Dim i As ULong
        Do Until Source(i).StartsWith("Main")
                        If i = Source.Length Then
                            
                        Else
                            
                        End If
         Loop
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Ended")
        End If
    End Sub

    Private Function IsDeprecated(ByVal Class_Name As String) As Boolean 'Reserved
        Return False
    End Function

    Private Sub QTS_Exit(ByVal Exit_Number As Integer) 'Handles exits
        Func = ".QTS_Exit" & vbTab & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & " is exiting with the exit code " & Exit_Number)
        End If
        If OS <> "Unix" Then 'Wont auto-close the window if it's not Unix
            Console.WriteLine("Press any key to close QTS...")
            Console.Read()
        End If
        Environment.Exit(Exit_Number)
    End Sub

    Private Function Clean_Up_File(ByVal File_Content() As String) As String() 'Removes indenting
        Func = ".Clean_Up_File" & vbTab
        If Verbose_Mode = True Then
            Console.WriteLine(Name & Func & "Called with an array with " & File_Content.Length & " items")
        End If
        Dim i As ULong = 0
        Try
            For i = 0 To File_Content.Length - 1
                Do Until File_Content(i).StartsWith(" ") = False AndAlso File_Content(i).StartsWith(vbTab) = False
                    File_Content(i) = File_Content(i).Remove(0, 1)
                Loop
            Next
        Catch
            EC.Er(Err.Description, i)
            QTS_Exit(1)
        End Try
        Return File_Content
    End Function
    
    Private Function Check_Var(ByVal Line As String, ByVal Line_Number As ULong, Optional ByVal PublicVar As Boolean = False) As Boolean
        If Line.Remove(0, 3) = Nothing Then
            goto Error_Handle
        ElseIf Line.Remove(0, 4) = " " Then
            goto Error_Handle
        Else
            'Dim Temp As Char = AscW(Line.Remove(0, 4))
            'If Temp >= 128 AndAlso Temp <= 255 Then
            '    EC.Er("Varible name can not be a special charactor", Line_Number)
            '    Return False
            'End If
            If Line.Contains(" = ") = True Then 'This <i>should</i> work
                Variables(a) = Line.Remove(0, 3) & PublicVar
                a += 1
            End If
        End If
        Return True
Error_Handle:
        EC.Er("Missing variable name", Line_Number)
        Return False
    End Function 
End Module