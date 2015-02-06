'This allows more advanced text manipulations
Imports System.Text 'again, lol
Imports System.IO
Class Text_Class
    Dim EC As New Error_Class
    ReadOnly Name As String = "+Text"
    Dim Func As String = Nothing
    Public Function Replace_(ByVal Input As String, ByVal Target As String, ByVal Replacement As String) As String
        Try
            Input = Input.Replace(Target, Replacement)
            Return Input
        Catch
            EC.Er(Err.Description)
        End Try
        Return "Error"
    End Function

    Public Function Find(ByVal Input() As String, ByVal FindWhat As String) As ULong
        Try
            Dim i As ULong = 0
            Dim Found As ULong = 0
            While i <= Input.Length
                If Input(i).Contains(FindWhat) = True Then
                    Found += 1
                End If
                i += 1
            End While
            Return Found
        Catch
            EC.Er(Err.Description)
        End Try
        Return 0
    End Function
End Class