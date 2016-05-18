Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Security.Cryptography

Friend Class CStrings
    Private _d As VBScript_RegExp_55.RegExp = Nothing
    Property d() As VBScript_RegExp_55.RegExp
        Get
            If _d Is Nothing Then
                _d = New VBScript_RegExp_55.RegExp()
            End If
            Return _d
        End Get
        Set(ByVal Value As VBScript_RegExp_55.RegExp)
            _d = Value
        End Set
    End Property
    Dim mc As VBScript_RegExp_55.MatchCollection
    Dim m As VBScript_RegExp_55.Match
    Dim ret() As String

    Function FromBytes(ByRef buf() As Byte, Optional ByRef unicodeAlso As Boolean = False) As String
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Dim result As String = ""
        Try
            Const minStrLen As Integer = 4

            d.Pattern = "[\w0-9 /?.\-_=+$\\@!*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,]{" & minStrLen & ",}"
            d.Global = True

            Search(buf)

            If unicodeAlso Then
                d.Pattern = "([\w0-9 /?.\-=+$\\@!\*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,][\x00]){" & minStrLen & ",}"
                Search(buf)
            End If

            result = String.Join(Environment.NewLine, ret)
            Erase ret

        Catch exc As Exception
            'NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block")
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try

        Return result
    End Function

    Function FromString(ByRef buffer As String, Optional ByRef unicodeAlso As Boolean = False) As String
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Dim result As String = ""
        Try

            Const minStrLen As Integer = 4

            d.Pattern = "[\w0-9 /?.\-_=+$\\@!*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,]{" & minStrLen & ",}"
            d.Global = True

            'UPGRADE_WARNING: (1059) Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
            Dim buf() As Byte = UnicodeEncoding.Unicode.GetBytes(StringsHelper.StrConv(buffer, StringsHelper.VbStrConvEnum.VbFromUnicode))
            Search(buf)

            If unicodeAlso Then
                d.Pattern = "([\w0-9 /?.\-=+$\\@!\*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,][\x00]){" & minStrLen & ",}"
                Search(buf)
            End If

            result = String.Join(Environment.NewLine, ret)
            Erase ret

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try

        Return result
    End Function


    Function FromFile(ByRef fpath As String) As String
        Dim result As String = ""
        'UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: http://www.vbtonet.com/ewis/ewi1065.aspx
        Console.Write("On Error Goto Label (hell)")

        Dim f, pointer As Integer
        Dim buf() As Byte
        Dim x As Integer

        f = FileSystem.FreeFile()
        If File.Exists(fpath) = False Then
            MessageBox.Show("File not found: " & fpath, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return result
        End If

        Const minStrLen As Integer = 4

        d.Pattern = "[\w0-9 /?.\-_=+$\\@!*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,]{" & minStrLen & ",}"
        d.Global = True

        push(ret, "File: " & Path.GetFileName(fpath))
        'push(ret, "MD5:  " & ReflectionHelper.Invoke(Of String)(hash, "HashFile", New Object() {fpath}).ToLower())
        push(ret, "MD5:  " & md5_hash(fpath))
        push(ret, "Size: " & CInt((New FileInfo(fpath)).Length) & Environment.NewLine)
        push(ret, "Ascii Strings:" & Environment.NewLine & New String("-", 75))

        ReDim buf(9000)
        FileSystem.FileOpen(f, fpath, OpenMode.Binary, OpenAccess.Read)

        Do While pointer < FileSystem.LOF(f)
            pointer = FileSystem.Seek(f)
            x = FileSystem.LOF(f) - pointer
            If x < 1 Then Exit Do
            If x < 9000 Then ReDim buf(x)
            'UPGRADE_WARNING: (2080) Get was upgraded to FileGet and has a new behavior. More Information: http://www.vbtonet.com/ewis/ewi2080.aspx
            FileSystem.FileGet(f, buf, -1)
            Search(buf)
        Loop

        push(ret, "")
        push(ret, "Unicode Strings:" & Environment.NewLine & New String("-", 75))

        d.Pattern = "([\w0-9 /?.\-=+$\\@!\*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,][\x00]){" & minStrLen & ",}"

        ReDim buf(9000)
        pointer = 1
        FileSystem.Seek(f, 1)

        Do While pointer < FileSystem.LOF(f)
            pointer = FileSystem.Seek(f)
            x = FileSystem.LOF(f) - pointer
            If x < 1 Then Exit Do
            If x < 9000 Then ReDim buf(x)
            'UPGRADE_WARNING: (2080) Get was upgraded to FileGet and has a new behavior. More Information: http://www.vbtonet.com/ewis/ewi2080.aspx
            FileSystem.FileGet(f, buf, -1)
            Search(buf)
        Loop

        FileSystem.FileClose(f)

        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Try
            result = String.Join(Environment.NewLine, ret)
            Erase ret


            Return result
hell:
            MessageBox.Show("Error getting strings: " & Information.Err().Description, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            FileSystem.FileClose(f)

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try


        Return result
    End Function

    Private Sub Search(ByRef buf() As Byte)

        'UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
        Dim b As String = StringsHelper.StrConv(StringsHelper.ByteArrayToString(buf), StringsHelper.VbStrConvEnum.VbUnicode)
        mc = d.Execute(b)

        For Each m2 As VBScript_RegExp_55.Match In mc
            m = m2
            push(ret, m.Value.Replace(Strings.Chr(0).ToString(), ""))
            m = Nothing
        Next m2


    End Sub


    Function LineGrep(ByRef sBuf() As String, ByRef sMatch As String) As String

        Dim ret() As String = ""

        'how i really really hate regexp...
        'd.Pattern = "\n(\w)*" & Trim(sMatch) & "(\w)*\n"
        'd.Global = True
        'd.IgnoreCase = True
        'Set mc = d.Execute(sBuf)
        '
        'For Each m In mc
        '    push ret(), Mid(m.value, 2)
        'Next

        For Each sBuf_item As String In sBuf
            If sBuf_item.IndexOf(sMatch, StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                push(ret, sBuf_item)
            End If
        Next sBuf_item


        Return String.Join(Environment.NewLine, ret)

    End Function

    Private Sub push(ByRef ary() As String, ByRef value As String) 'this modifies parent ary object
        Try
            Dim x As Integer
            x = ary.GetUpperBound(0) '<-throws Error If Not initalized
            ReDim Preserve ary(ary.GetUpperBound(0) + 1)
            ary(ary.GetUpperBound(0)) = value

        Catch
            ReDim ary(0) : ary(0) = value
        End Try
    End Sub

    ' Function to obtain the desired hash of a file
    Function hash_generator(ByVal hash_type As String, ByVal file_name As String) As String

        ' We declare the variable : hash
        Dim hash As String
        If hash_type.ToLower = "md5" Then
            ' Initializes a md5 hash object
            hash = MD5.Create
        ElseIf hash_type.ToLower = "sha1" Then
            ' Initializes a SHA-1 hash object
            hash = SHA1.Create()
        ElseIf hash_type.ToLower = "sha256" Then
            ' Initializes a SHA-256 hash object
            hash = SHA256.Create()
        Else
            MsgBox("Unknown type of hash : " & hash_type, MsgBoxStyle.Critical)
            Return False
        End If

        ' We declare a variable to be an array of bytes
        Dim hashValue() As Byte

        ' We create a FileStream for the file passed as a parameter
        Dim fileStream As FileStream = File.OpenRead(file_name)
        ' We position the cursor at the beginning of stream
        fileStream.Position = 0
        ' We calculate the hash of the file
        hashValue = hash.ComputeHash(fileStream)
        ' The array of bytes is converted into hexadecimal before it can be read easily
        Dim hash_hex As String = PrintByteArray(hashValue)

        ' We close the open file
        fileStream.Close()

        ' The hash is returned
        Return hash_hex

    End Function

    ' We traverse the array of bytes and converting each byte in hexadecimal
    Public Function PrintByteArray(ByVal array() As Byte) As String

        Dim hex_value As String = ""

        ' We traverse the array of bytes
        Dim i As Integer
        For i = 0 To array.Length - 1

            ' We convert each byte in hexadecimal
            hex_value += array(i).ToString("X2")

        Next i

        ' We return the string in lowercase
        Return hex_value.ToLower

    End Function

    ' md5 is a reserved name, so we named the function : md5_hash
    Function md5_hash(ByVal file_name As String) As String
        Return hash_generator("md5", file_name)
    End Function

    Function sha_1(ByVal file_name As String) As String
        Return hash_generator("sha1", file_name)
    End Function

    Function sha_256(ByVal file_name As String) As String
        Return hash_generator("sha256", file_name)
    End Function
End Class