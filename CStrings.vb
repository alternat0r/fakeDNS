Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports UpgradeHelpers.Helpers
Friend Class CStrings
	'License:   GPL
	'Copyright: 2005 iDefense a Verisign Company
	'Site:      http://labs.idefense.com
	'
	'Author:    David Zimmer <david@idefense.com, dzzie@yahoo.com>
	'
	'         This program is free software; you can redistribute it and/or modify it
	'         under the terms of the GNU General Public License as published by the Free
	'         Software Foundation; either version 2 of the License, or (at your option)
	'         any later version.
	'
	'         This program is distributed in the hope that it will be useful, but WITHOUT
	'         ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
	'         FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
	'         more details.
	'
	'         You should have received a copy of the GNU General Public License along with
	'         this program; if not, write to the Free Software Foundation, Inc., 59 Temple
	'         Place, Suite 330, Boston, MA 02111-1307 USA

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
			NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block")
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
			NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block")
		End Try

		Return result
	End Function


	Function FromFile(ByRef fpath As String) As String
		Dim result As String = ""
		Dim fso, hash As Object
		'UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: http://www.vbtonet.com/ewis/ewi1065.aspx
		UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (hell)")

		Dim f, pointer As Integer
		Dim buf() As Byte
		Dim x As Integer

		f = FileSystem.FreeFile()

		If Not ReflectionHelper.Invoke(Of Boolean)(fso, "FileExists", New Object() {fpath}) Then
			MessageBox.Show("File not found: " & fpath, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return result
		End If

		Const minStrLen As Integer = 4

		d.Pattern = "[\w0-9 /?.\-_=+$\\@!*\(\)#%~`\^&\|\{\}\[\]:;'""<>\,]{" & minStrLen & ",}"
		d.Global = True

		push(ret, "File: " & ReflectionHelper.Invoke(fso, "FileNameFromPath", New Object() {fpath}))
		push(ret, "MD5:  " & ReflectionHelper.Invoke(Of String)(hash, "HashFile", New Object() {fpath}).ToLower())
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
			NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block")
		End Try


		Return result
	End Function

	Private Sub Search(ByRef buf() As Byte)

		'UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
		Dim b As String = StringsHelper.StrConv(StringsHelper.ByteArrayToString(buf), StringsHelper.VbStrConvEnum.VbUnicode)
		mc = d.Execute(b)

		For	Each m2 As VBScript_RegExp_55.Match In mc
			m = m2
			push(ret, m.Value.Replace(Strings.Chr(0).ToString(), ""))
			m = Nothing
		Next m2


	End Sub


	Function LineGrep(ByRef sBuf() As String, ByRef sMatch As String) As String

		Dim ret() As String

		'how i really really hate regexp...
		'd.Pattern = "\n(\w)*" & Trim(sMatch) & "(\w)*\n"
		'd.Global = True
		'd.IgnoreCase = True
		'Set mc = d.Execute(sBuf)
		'
		'For Each m In mc
		'    push ret(), Mid(m.value, 2)
		'Next

		For	Each sBuf_item As String In sBuf
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
End Class