Imports System.Console
Imports System.Security
Public Class Cryptography_Class
    Dim EC As New Error_Class
    ReadOnly Name As String = "+Crypto"
    Dim Func As String = Nothing
    Public Function Encrypt(ByVal Input As String, ByVal Key As System.Security.SecureString) As String
        Key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(Key)
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Encrypt Called with: (" & Input & " || " & Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr))
        End If
        'Return Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr) = the key in string
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = Nothing
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr)))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Key.Dispose()
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Encrypt Ended")
            End If
            Return encrypted
        Catch
            Key.Dispose()
            EC.Er(Err.Description)
            Err.Clear()
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Encrypt Ended")
            End If
            Return "Error"
        End Try
    End Function

    Private Function Decrypt(ByVal Input As String, ByVal Key As System.Security.SecureString) As String
        Key.MakeReadOnly()
        Dim ptr As IntPtr
        ptr = Runtime.InteropServices.Marshal.SecureStringToBSTR(Key)
        If Verbose_Mode = True Then
            Console.WriteLine(Name & ".Decrypt Called with: (" & Input & " || " & Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr))
        End If
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = Nothing
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr)))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(Input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Key.Dispose()
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Decrypt Ended")
            End If
            Return decrypted
        Catch
            Key.Dispose()
            EC.Er(Err.Description)
            Err.Clear()
            If Verbose_Mode = True Then
                Console.WriteLine(Name & ".Decrypt Ended")
            End If
            Return "Error"
        End Try
    End Function

    Public Function KeyWords(ByVal Line As String) As Boolean 'Used for every class to determine usable functions
        If Line.StartsWith("Encrypt") = True Then
            Return True
        ElseIf Line.StartsWith("Decrypt") = True Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
