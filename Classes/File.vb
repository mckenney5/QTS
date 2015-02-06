'Handles file io
Imports System.IO 'again, again ... lol
Class File_IO_Class
    Dim EC As New Error_Class
    ReadOnly Name As String = "+File"
    Dim Func As String = Nothing

    Public Function KeyWords(ByVal Word As String) As Boolean
        Select Case Word
            Case "Found"
                Return True
            Case "Write"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function Found(ByVal File_Name As String) As Boolean
        Return File.Exists(File_Name)
    End Function

    Public Function Write(ByVal File_Name As String, ByVal Text_To_Write As String) As Boolean
        Try
            If Found(File_Name) = True Then
                File.AppendAllText(File_Name, Text_To_Write)
            Else
                Return False
            End If
        Catch
            EC.Er(Err.Description)
            Return False
        End Try
        Return True
    End Function
End Class
