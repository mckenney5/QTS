'This displays help and documentation
Imports System.Console
Class Help_Class
    ReadOnly Name As String = "+Help"
    Dim Func As String = Nothing
    Public Sub Help()
        Console.WriteLine("Foo")
        Console.WriteLine("Bar")
    End Sub

    Public Sub Doc()
        Shell("http://www.example.com/")
    End Sub

    Public Sub Man()
        Console.WriteLine("Manual Page")
    End Sub
End Class