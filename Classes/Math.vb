'This class handles math operations including the random number generator
Class Math_Class
    Dim EC As New Error_Class
    ReadOnly Name As String = "+Math"
    Dim Func As String = Nothing

    Public Function KeyWords(ByVal Word As String) As Boolean
        Select Case Word
            Case "Random"
                Return True
            Case "Calculate"
                Return True
            Case "Factorial"
                Return True
            Case "Count"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function Random(Optional ByVal Max As Integer = 1, Optional ByVal Min As Integer = 100) As Integer
        Try
            Dim Ran As New Random
            Return Ran.Next(Max, Min)
        Catch
            EC.Er(Err.Description)
        End Try
        Return 0
    End Function

    Public Function Calculate(ByVal Operation As String) As Long 'reserved
        'split by math operations
        Return 0
    End Function

    Public Function Factorial(ByVal Number As Integer) As Long
        Try
            Dim Fact_Number As Long = Number
            For i = 1 To Number
                Fact_Number = Fact_Number * (Fact_Number - i)
            Next
            Return Fact_Number
        Catch
            EC.Er(Err.Description)
        End Try
        Return 0
    End Function

    Public Sub Count(Optional ByVal Start As Integer = 1, Optional ByVal Finish As Integer = 10)
        If Start > Finish Then
            EC.Warn("The start of the counter is larger then the end point")
        End If
        For i As Integer = Start To Finish
            Console.WriteLine(i)
        Next

    End Sub

End Class
