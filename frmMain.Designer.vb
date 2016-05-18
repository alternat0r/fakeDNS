<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As frmMain
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As frmMain
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = CreateInstance()
                m_InitializingDefInstance = False
            End If
            Return m_vb6FormDefInstance
        End Get
        Set(ByVal Value As frmMain)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region
#Region "Windows Form Designer generated code "
    Public Shared Function CreateInstance() As frmMain
        Dim theInstance As frmMain = New frmMain()
        theInstance.Form_Load()
        Return theInstance
    End Function
    Private visualControls() As String = New String() {"components", "ToolTipMain", "txtOnly", "optOnly", "optAll", "Frame1", "cmdClear", "cmdCopy", "List1", "txtIp", "Option2", "Option1", "_ws_0", "txtLog", "cmdListen", "Label2", "ws", "listBoxHelper1"}
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTipMain As System.Windows.Forms.ToolTip
    Public WithEvents txtOnly As System.Windows.Forms.TextBox
    Public WithEvents optOnly As System.Windows.Forms.RadioButton
    Public WithEvents optAll As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Public WithEvents cmdCopy As System.Windows.Forms.Button
    Public WithEvents List1 As System.Windows.Forms.ListBox
    Public WithEvents txtIp As System.Windows.Forms.TextBox
    Public WithEvents Option2 As System.Windows.Forms.RadioButton
    Public WithEvents Option1 As System.Windows.Forms.RadioButton
    Private WithEvents _ws_0 As AxMSWinsockLib.AxWinsock
    Public WithEvents txtLog As System.Windows.Forms.TextBox
    Public WithEvents cmdListen As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public ws(0) As AxMSWinsockLib.AxWinsock
    Private listBoxHelper1 As UpgradeHelpers.Gui.ListBoxHelper
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.List1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout
        '
        'List1
        '
        Me.List1.FormattingEnabled = true
        Me.List1.Location = New System.Drawing.Point(320, 3)
        Me.List1.Name = "List1"
        Me.List1.Size = New System.Drawing.Size(176, 251)
        Me.List1.TabIndex = 0
        '
        'frmMain
        '
        Me.ClientSize = New System.Drawing.Size(499, 314)
        Me.Controls.Add(Me.List1)
        Me.Name = "frmMain"
        Me.ResumeLayout(false)

End Sub
    Sub ReLoadForm(ByVal addEvents As Boolean)
        Initializews()
        Form_Load()
        If addEvents Then
            AddHandler MyBase.Closed, AddressOf Me.Form1_Closed
            AddHandler MyBase.Resize, AddressOf Me.Form1_Resize
        End If
    End Sub
    Sub Initializews()
        ReDim ws(0)
        Me.ws(0) = _ws_0
    End Sub
    Friend WithEvents List1 As System.Windows.Forms.ListBox
#End Region
End Class