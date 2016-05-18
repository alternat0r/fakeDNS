Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Drawing
Imports System.Windows.Forms
'Imports UpgradeHelpers.Gui
'Imports UpgradeHelpers.Helpers
Partial Friend Class frmMain
    Inherits System.Windows.Forms.Form
    '
    'Author: david@idefense.com
    '
    'Purpose: a very quick-n-dirty dns server that allows you to
    '         have all dns queries resolve to a predefined ip.
    '         Currently only supports A-records, should support
    '         MX queries at a latter date. Useful to force bots
    '         and other malicious code to use your own services
    '         for analysis. (ie. mail, web, irc servers etc)
    '
    'License: Copyright (C) 2005 David Zimmer <david@idefense.com, dzzie@yahoo.com>
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

    'Private Type dns_pkt
    '    transaction_id As Integer
    '    flags As Integer 'bit fields
    '    ques As Integer
    '    ans_rrs As Integer
    '    authority_rrs As Integer
    '    aditional_rrs As Integer
    '    strSize As Byte
    '    queries(500) As Byte 'variable should be enough size
    '    'format strSize = size of string
    '    'ascii string of len strsize
    '    'query type as integer
    '    'query class as integer
    'End Type

    'for dns reponse
    'mirror transaction_id
    'flags 0x8180 = standard query response no error
    'questions 1
    'answers 1
    'mirror question query
    'add answer format (A record)
    '   name: C0 0C (host name)
    '   Type: 00 01 = host address  | 00 0f = MX
    '   Class: 00 01
    '   ttl:   00 00 34 ef  (some time interval)
    '   datalen: 00 04
    '   address: 40 eb ea 1e  (example = 64.235.234.30)

    '               |name|type| class|   ttl     |len  |   ip      |
    'Const reply = "C0 0C 00 01 00 01 00 00 51 81 00 04 7F 00 00 01 00"
    '
    '
    'Updates:
    '        8-11-05  added load/unload ws(1) while busy code: some dns
    '                 clients would only work on first request and fail
    '                 on all subsequent..this seems to fix it (dirty fix)
    '
    '
    '                                                 127. 0. 0. 1
    '                                                 \           /
    Const reply As String = "C0 0C 00 01 00 01 00 00 51 81 00 04 7F 00 00 01"
    Dim answer() As Byte
    Dim no_reply() As Byte

    Private busy As Boolean
    'UPGRADE_NOTE: (2041) The following line was commented. More Information: http://www.vbtonet.com/ewis/ewi2041.aspx
    'Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
    'UPGRADE_NOTE: (2041) The following line was commented. More Information: http://www.vbtonet.com/ewis/ewi2041.aspx
    'Private Declare Sub CopyMemory Lib "kernel32"  Alias "RtlMoveMemory"(ByVal Destination As Integer, ByVal Source As Integer, ByVal Length As Integer)

    Private _strings As CStrings = Nothing
    Public Sub New()
        MyBase.New()
        If m_vb6FormDefInstance Is Nothing Then
            If m_InitializingDefInstance Then
                m_vb6FormDefInstance = Me
            Else
                Try
                    'For the start-up form, the first instance created is the default instance.
                    If Not (System.Reflection.Assembly.GetExecutingAssembly().EntryPoint Is Nothing) AndAlso System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType Is Me.GetType() Then
                        m_vb6FormDefInstance = Me
                    End If

                Catch
                End Try
            End If
        End If
        'This call is required by the Windows Form Designer.
        isInitializingComponent = True
        InitializeComponent()
        isInitializingComponent = False
        ReLoadForm(False)
    End Sub


    Property strings() As CStrings
        Get
            If _strings Is Nothing Then
                _strings = New CStrings()
            End If
            Return _strings
        End Get
        Set(ByVal Value As CStrings)
            _strings = Value
        End Set
    End Property

    Function BuildAnswer() As Boolean
        Dim result As Boolean = False
        Try

            Dim tmp() As String
            Dim ip() As Byte

            tmp = reply.Split(" "c)

            If Option2.Checked Then 'custom IP not 127.0.0.1
                ip = ArraysHelper.DeepCopy(GetBytes())
                For i As Integer = 12 To 15
                    tmp(i) = ip(i - 12).ToString("X")
                Next
            End If

            ReDim answer(16)

            For i As Integer = 0 To tmp.GetUpperBound(0)
                answer(i) = CInt("&h" & tmp(i))
            Next

            result = True

            'no reply is 0.0.0.0
            no_reply = ArraysHelper.DeepCopy(answer)
            For i As Integer = 12 To 15
                no_reply(i) = 0
            Next

        Catch
        End Try



        Return result
    End Function


    Private Sub cmdClear_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdClear.Click
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Try
            List1.Items.Clear()
            txtLog.Text = ""

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try
    End Sub

    Private Sub cmdCopy_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdCopy.Click
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Try
            Dim tmp As String = ""
            For i As Integer = 0 To List1.Items.Count
                tmp = tmp & List1.GetListItem(i) & Environment.NewLine
            Next
            My.Computer.Clipboard.Clear()
            'UPGRADE_WARNING: (2081) Clipboard.SetText has a new behavior. More Information: http://www.vbtonet.com/ewis/ewi2081.aspx
            My.Computer.Clipboard.SetText(tmp)

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try
    End Sub

    Private Sub cmdListen_Click(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles cmdListen.Click

        Try

            If StringsHelper.ToDoubleSafe(Convert.ToString(cmdListen.Tag)) = 0 Then

                If Not isIpValid() Then
                    MessageBox.Show("Ip is not Valid it must be x.x.x.x decimal format", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If Not BuildAnswer() Then
                    MessageBox.Show("humm could not build answer?", Application.ProductName)
                    Exit Sub
                End If

                cmdListen.Text = "Close"
                cmdListen.Tag = CStr(1)
                txtIp.Enabled = False
                Option1.Enabled = False
                Option2.Enabled = False

                ControlArrayHelper.LoadControl(Me, "ws", 1)
                ws(1).Bind(53)

            Else

                cmdListen.Tag = CStr(0)
                cmdListen.Text = "Listen"
                Option1.Enabled = True
                Option2.Enabled = True
                txtIp.Enabled = Option2.Checked
                ws(1).Close()
                ControlArrayHelper.UnloadControl(Me, "ws", 1)

            End If

        Catch excep As System.Exception
            MessageBox.Show("Error: " & excep.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    'UPGRADE_WARNING: (2080) Form_Load event was upgraded to Form_Load method and has a new behavior. More Information: http://www.vbtonet.com/ewis/ewi2080.aspx
    Private Sub Form_Load()

        Try
            txtIp.Text = Interaction.GetSetting("iDefense", "fakeDNS", "ip", ws(0).LocalIP)

        Catch
        End Try

    End Sub

    Private Sub Form1_Closed(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Closed
        Interaction.SaveSetting("iDefense", "fakeDNS", "ip", txtIp.Text)
    End Sub

    Private isInitializingComponent As Boolean
    Private Sub Form1_Resize(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles MyBase.Resize
        If isInitializingComponent Then
            Exit Sub
        End If
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        'Me.Width = 7360
        'Me.Height = 5190
    End Sub

    Private Sub Option1_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Option1.CheckedChanged
        If eventSender.Checked Then
            If isInitializingComponent Then
                Exit Sub
            End If
            With txtIp
                txtIp.BackColor = ColorTranslator.FromOle(IIf(Option2.Checked, CInt(ColorTranslator.ToOle(Color.White)), CInt(ColorTranslator.ToOle(Me.BackColor))))
                txtIp.Enabled = Option2.Checked
            End With
        End If
    End Sub

    Private Sub Option2_CheckedChanged(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Option2.CheckedChanged
        If eventSender.Checked Then
            If isInitializingComponent Then
                Exit Sub
            End If
            With txtIp
                txtIp.BackColor = ColorTranslator.FromOle(IIf(Option2.Checked, CInt(ColorTranslator.ToOle(Color.White)), CInt(ColorTranslator.ToOle(Me.BackColor))))
                txtIp.Enabled = Option2.Checked
            End With
        End If
    End Sub

    Private Sub ws_DataArrival(ByVal eventSender As Object, ByVal eventArgs As AxMSWinsockLib.DMSWinsockControlEvents_DataArrivalEvent) Handles _ws_0.DataArrival
        Try

            Dim i As Integer
            Dim b() As Byte
            Dim tmp As String = ""
            Dim dmp As Object
            Dim orgLen As Integer
            Dim rep() As Byte

            While busy
                Threading.Thread.Sleep(1)
                Application.DoEvents()
            End While

            busy = True
            ws(1).GetData(b)
            Log(b, "Request:  ( " & DateTimeHelper.ToString(DateTime.Now) & " )")

            rep = ArraysHelper.DeepCopy(answer)

            If optOnly.Checked Then
                'UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
                tmp = StringsHelper.StrConv(StringsHelper.ByteArrayToString(b), StringsHelper.VbStrConvEnum.VbUnicode)
                If (tmp.IndexOf(txtOnly.Text, StringComparison.CurrentCultureIgnoreCase) + 1) < 1 Then
                    'really we should get the real response here and proxy it back...
                    'or work by sniffing dns and injecting fake replies for intercepts only..
                    txtLog.SelectedText = Environment.NewLine & Environment.NewLine & "Does not match filter exiting.."
                    txtLog.SelectionStart = Microsoft.VisualBasic.Strings.Len(txtLog.Text)
                    Exit Sub
                    'rep = no_reply
                End If
            End If

            'todo: check what kind of query it is support mx and a
            'but for now just blindly reply as if it was a an A record request
            orgLen = b.GetUpperBound(0) + 1
            ReDim Preserve b(b.GetUpperBound(0) + 16) 'space to add our answer field
            UpgradeSolution1Support.SafeNative.kernel32.CopyMemory(b(orgLen), rep(0), 16) 'copy our answer to buffer

            b(2) = &H81S '\_flags 0x8180 standard query response no error
            b(3) = &H80S '/
            b(6) = 0 '\_1 answer
            b(7) = 1 '/

            Log(b, "Response:")
            ws(1).SendData(b)
            ws(1).Close()

            ControlArrayHelper.UnloadControl(Me, "ws", 1)
            ControlArrayHelper.LoadControl(Me, "ws", 1)
            ws(1).Bind(53)

            Threading.Thread.Sleep(1)
            Application.DoEvents()
            busy = False

        Catch


            busy = False
            'MsgBox Err.Description
            Application.DoEvents()
        End Try
    End Sub

    Sub Extract(ByRef b() As Byte)
        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Try
            Dim found As Boolean

            'UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
            Dim y As String = StringsHelper.StrConv(StringsHelper.ByteArrayToString(b), StringsHelper.VbStrConvEnum.VbUnicode)
            For i As Integer = 2 To 13 'cheat yet functional..
                y = y.Replace(Microsoft.VisualBasic.Strings.Chr(i).ToString(), ".")
            Next

            Dim tmp As Object = strings.FromString(y)
            ReflectionHelper.SetPrimitiveValue(tmp, ReflectionHelper.GetPrimitiveValue(Of String)(tmp).Split(CChar(Environment.NewLine)))
            For Each x As String In tmp
                found = False
                While x.StartsWith(".")
                    x = x.Substring(1)
                End While
                For i As Integer = 0 To List1.Items.Count
                    If List1.GetListItem(i).ToUpper() = x.Trim().ToUpper() Then
                        found = True
                        Exit For
                    End If
                Next
                If Not found Then List1.Items.Add(x.Trim())
            Next x

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try

    End Sub


    Sub Log(ByRef it() As Byte, Optional ByRef header As String = "")

        'UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: http://www.vbtonet.com/ewis/ewi1069.aspx
        Try

            Extract(it)
            If Microsoft.VisualBasic.Strings.Len(header) > 0 Then txtLog.SelectedText = IIf(Microsoft.VisualBasic.Strings.Len(txtLog.Text) > 0, Environment.NewLine & Environment.NewLine, "") & header

            'UPGRADE_WARNING: (1059) Code was upgraded to use UpgradeHelpers.Helpers.StringsHelper.ByteArrayToString() which may not have the same behavior. More Information: http://www.vbtonet.com/ewis/ewi1059.aspx
            txtLog.SelectedText = IIf(Microsoft.VisualBasic.Strings.Len(txtLog.Text) > 0, Environment.NewLine & Environment.NewLine, "") & hexdump(StringsHelper.StrConv(StringsHelper.ByteArrayToString(it), StringsHelper.VbStrConvEnum.VbUnicode))
            txtLog.SelectionStart = Microsoft.VisualBasic.Strings.Len(txtLog.Text)

        Catch exc As Exception
            Console.Write("Resume in On-Error-Resume-Next Block")
        End Try

    End Sub

    Function isIpValid() As Boolean
        Try
            GetBytes()
            Return True

        Catch
            Return Nothing
        End Try
    End Function

    Function GetBytes() As Byte()
        txtIp.Text = txtIp.Text.Trim()
        Dim ret(3) As Byte

        If Microsoft.VisualBasic.Strings.Len(txtIp.Text) = 0 Then Throw New System.Exception("1")

        Dim tmp() As String = txtIp.Text.Split("."c)

        If tmp.GetUpperBound(0) <> 3 Then Throw New System.Exception("2")

        For i As Integer = 0 To ret.GetUpperBound(0)
            ret(i) = CByte(tmp(i))
        Next

        Return ret
    End Function



    Function hexdump(ByRef it As String) As String
        Dim a As Integer
        Dim b As String = ""
        Dim c As String
        Dim lines() As String = ""

        Dim my As String = ""
        For i As Integer = 1 To Microsoft.VisualBasic.Strings.Len(it)
            a = Microsoft.VisualBasic.Strings.Asc(it.Substring(i - 1, Math.Min(1, it.Length - (i - 1)))(0))
            c = a.ToString("X")
            c = IIf(Microsoft.VisualBasic.Strings.Len(c) = 1, "0" & c, c)
            b = b & IIf(a > 33 And a < 123, Microsoft.VisualBasic.Strings.Chr(a).ToString(), ".")
            my = my & c & " "
            If i Mod 16 = 0 Then
                push(lines, my & "  [" & b & "]")
                my = ""
                b = ""
            End If
        Next

        If Microsoft.VisualBasic.Strings.Len(b) > 0 Then
            If Microsoft.VisualBasic.Strings.Len(my) < 48 Then
                my = my & New String(" ", 48 - Microsoft.VisualBasic.Strings.Len(my))
            End If
            If Microsoft.VisualBasic.Strings.Len(b) < 16 Then
                b = b & New String(" ", 16 - Microsoft.VisualBasic.Strings.Len(b))
            End If
            push(lines, my & "  [" & b & "]")
        End If

        If Microsoft.VisualBasic.Strings.Len(it) < 16 Then
            Return my & "  [" & b & "]" & Environment.NewLine
        Else
            Return String.Join(Environment.NewLine, lines)
        End If


    End Function

    Sub push(ByRef ary() As String, ByRef value As String) 'this modifies parent ary object
        Try
            Dim x As Integer
            x = ary.GetUpperBound(0) '<-throws Error If Not initalized
            ReDim Preserve ary(ary.GetUpperBound(0) + 1)
            ary(ary.GetUpperBound(0)) = value

        Catch
            ReDim ary(0) : ary(0) = value
        End Try
    End Sub



    Function KeyExistsInCollection(ByRef c As OrderedDictionary, ByRef val As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim t As Object
            ReflectionHelper.SetPrimitiveValue(t, ReflectionHelper.GetPrimitiveValue(c(val)))
            Return True

        Catch
            result = False
        End Try
        Return result
    End Function







    'A RECORD REQUEST
    '______________________________________________________________________
    '00 01 01 00 00 01 00 00 00 00 00 00 06 67 6F 6F   [.............goo]
    '67 6C 65 03 63 6F 6D 00 00 01 00 01 00 00 00 00   [gle.com.........]
    '00 00 00 00 00 00 00 00 00 00 00 00 00            [.............   ]
    '
    'A RECORD ANSWER
    '----------------------------------------------------------------------
    '00 01 85 80 00 01 00 01 00 00 00 00 06 67 6F 6F   [.............goo]
    '67 6C 65 03 63 6F 6D 00 00 01 00 01 C0 0C 00 01   [gle.com.........]
    '00 01 00 00 51 81 00 04 7F 00 00 01 00            [....Q........   ]
    '
    '
    'MX QUERY NOT IMPLEMENTED YET
    '
    'MX QUERY
    '-----------------------------------------------------------------------------
    '00 40 05 28 | 0B 24 00 A0 | C9 3D FC B2 | 08 00 45 00 [.@.(.$...=....E.]
    '00 3C 35 13 | 00 00 80 11 | A6 19 0A 0A | 0A 07 A6 66 [.<5............f]
    'A5 0D 07 7A | 00 35 00 28 | 88 F3 11 DF | 01 00 00 01 [...z.5.(........]
    '00 00 00 00 | 00 00 0A 6C | 75 6E 61 72 | 70 61 67 65 [.......lunarpage]
    '73 03 63 6F | 6D 00 00 0F | 00 01       |             [s.com.....]
    '
    '
    'MX Response
    '-----------------------------------------------------------------------------
    '00 A0 C9 3D | FC B2 00 40 | 05 28 0B 24 | 08 00 45 00 [...=...@.(.$..E.]
    '00 78 E0 8B | 40 00 F0 11 | 4A 64 A6 66 | A5 0D 0A 0A [.x..@...Jd.f....]
    '0A 07 00 35 | 07 7A 00 64 | 2D E7 11 DF | 81 80 00 01 [...5.z.d-.......]
    '00 01 00 02 | 00 00 0A 6C | 75 6E 61 72 | 70 61 67 65 [.......lunarpage]
    '73 03 63 6F | 6D 00 00 0F | 00 01 C0 0C | 00 0F 00 01 [s.com...........]
    '00 00 07 08 | 00 0C 00 00 | 07 6D 78 6C | 6F 67 69 63 [.........mxlogic]
    'C0 0C C0 0C | 00 02 00 01 | 00 00 06 55 | 00 06 03 6E [...........U...n]
    '73 32 C0 0C | C0 0C 00 02 | 00 01 00 00 | 06 55 00 06 [s2...........U..]
    '03 6E 73 31 | C0 0C       |             |             [.ns1..]

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class