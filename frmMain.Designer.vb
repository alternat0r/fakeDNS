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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTipMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.txtOnly = New System.Windows.Forms.TextBox()
        Me.optOnly = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.List1 = New System.Windows.Forms.ListBox()
        Me.txtIp = New System.Windows.Forms.TextBox()
        Me.Option2 = New System.Windows.Forms.RadioButton()
        Me.Option1 = New System.Windows.Forms.RadioButton()
        Me._ws_0 = New AxMSWinsockLib.AxWinsock()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.cmdListen = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me._ws_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        Me.listBoxHelper1 = New UpgradeHelpers.Gui.ListBoxHelper(Me.components)
        ' 
        'Frame1
        ' 
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Frame1.Controls.Add(Me.txtOnly)
        Me.Frame1.Controls.Add(Me.optOnly)
        Me.Frame1.Controls.Add(Me.optAll)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(27, 318)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(247, 25)
        Me.Frame1.TabIndex = 9
        Me.Frame1.Visible = True
        ' 
        'txtOnly
        ' 
        Me.txtOnly.AcceptsReturn = True
        Me.txtOnly.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnly.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtOnly.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnly.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnly.Location = New System.Drawing.Point(111, 3)
        Me.txtOnly.MaxLength = 0
        Me.txtOnly.Name = "txtOnly"
        Me.txtOnly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnly.Size = New System.Drawing.Size(109, 22)
        Me.txtOnly.TabIndex = 12
        ' 
        'optOnly
        ' 
        Me.optOnly.Appearance = System.Windows.Forms.Appearance.Normal
        Me.optOnly.BackColor = System.Drawing.SystemColors.Control
        Me.optOnly.CausesValidation = True
        Me.optOnly.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optOnly.Checked = False
        Me.optOnly.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOnly.Enabled = True
        Me.optOnly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOnly.Location = New System.Drawing.Point(57, 9)
        Me.optOnly.Name = "optOnly"
        Me.optOnly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOnly.Size = New System.Drawing.Size(58, 16)
        Me.optOnly.TabIndex = 11
        Me.optOnly.TabStop = True
        Me.optOnly.Text = "only"
        Me.optOnly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optOnly.Visible = True
        ' 
        'optAll
        ' 
        Me.optAll.Appearance = System.Windows.Forms.Appearance.Normal
        Me.optAll.BackColor = System.Drawing.SystemColors.Control
        Me.optAll.CausesValidation = True
        Me.optAll.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optAll.Checked = True
        Me.optAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAll.Enabled = True
        Me.optAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.Location = New System.Drawing.Point(6, 6)
        Me.optAll.Name = "optAll"
        Me.optAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAll.Size = New System.Drawing.Size(52, 19)
        Me.optAll.TabIndex = 10
        Me.optAll.TabStop = True
        Me.optAll.Text = "all"
        Me.optAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optAll.Visible = True
        ' 
        'cmdClear
        ' 
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(512, 296)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(59, 21)
        Me.cmdClear.TabIndex = 8
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdClear.UseVisualStyleBackColor = False
        ' 
        'cmdCopy
        ' 
        Me.cmdCopy.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCopy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCopy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCopy.Location = New System.Drawing.Point(598, 296)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCopy.Size = New System.Drawing.Size(57, 19)
        Me.cmdCopy.TabIndex = 7
        Me.cmdCopy.Text = "Copy"
        Me.cmdCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCopy.UseVisualStyleBackColor = False
        ' 
        'List1
        ' 
        Me.List1.BackColor = System.Drawing.SystemColors.Window
        Me.List1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.List1.CausesValidation = True
        Me.List1.Cursor = System.Windows.Forms.Cursors.Default
        Me.List1.Enabled = True
        Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.List1.IntegralHeight = True
        Me.List1.Location = New System.Drawing.Point(478, 2)
        Me.List1.MultiColumn = False
        Me.List1.Name = "List1"
        Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.List1.Size = New System.Drawing.Size(177, 293)
        Me.List1.Sorted = False
        Me.List1.TabIndex = 6
        Me.List1.TabStop = True
        Me.List1.Visible = True
        ' 
        'txtIp
        ' 
        Me.txtIp.AcceptsReturn = True
        Me.txtIp.BackColor = System.Drawing.SystemColors.Window
        Me.txtIp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtIp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIp.Enabled = False
        Me.txtIp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIp.Location = New System.Drawing.Point(312, 296)
        Me.txtIp.MaxLength = 0
        Me.txtIp.Name = "txtIp"
        Me.txtIp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIp.Size = New System.Drawing.Size(101, 21)
        Me.txtIp.TabIndex = 1
        Me.txtIp.Text = "10.10.10.7"
        ' 
        'Option2
        ' 
        Me.Option2.Appearance = System.Windows.Forms.Appearance.Normal
        Me.Option2.BackColor = System.Drawing.SystemColors.Control
        Me.Option2.CausesValidation = True
        Me.Option2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Option2.Checked = False
        Me.Option2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option2.Enabled = True
        Me.Option2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option2.Location = New System.Drawing.Point(228, 300)
        Me.Option2.Name = "Option2"
        Me.Option2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option2.Size = New System.Drawing.Size(89, 17)
        Me.Option2.TabIndex = 5
        Me.Option2.TabStop = True
        Me.Option2.Text = "User defined"
        Me.Option2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Option2.Visible = True
        ' 
        'Option1
        ' 
        Me.Option1.Appearance = System.Windows.Forms.Appearance.Normal
        Me.Option1.BackColor = System.Drawing.SystemColors.Control
        Me.Option1.CausesValidation = True
        Me.Option1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Option1.Checked = True
        Me.Option1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Option1.Enabled = True
        Me.Option1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Option1.Location = New System.Drawing.Point(152, 300)
        Me.Option1.Name = "Option1"
        Me.Option1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Option1.Size = New System.Drawing.Size(69, 17)
        Me.Option1.TabIndex = 4
        Me.Option1.TabStop = True
        Me.Option1.Text = "127.0.0.1"
        Me.Option1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Option1.Visible = True
        ' 
        '_ws_0
        ' 
        Me._ws_0.Location = New System.Drawing.Point(380, 4)
        Me._ws_0.Name = "_ws_0"
        Me._ws_0.OcxState = CType(resources.GetObject("_ws_0.OcxState"), System.Windows.Forms.AxHost.State)
        ' 
        'txtLog
        ' 
        Me.txtLog.AcceptsReturn = True
        Me.txtLog.BackColor = System.Drawing.SystemColors.Window
        Me.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtLog.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLog.Font = New System.Drawing.Font("Arial", 6.0!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 255)
        Me.txtLog.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLog.Location = New System.Drawing.Point(0, 0)
        Me.txtLog.MaxLength = 0
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(473, 293)
        Me.txtLog.TabIndex = 3
        ' 
        'cmdListen
        ' 
        Me.cmdListen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdListen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdListen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdListen.Location = New System.Drawing.Point(416, 296)
        Me.cmdListen.Name = "cmdListen"
        Me.cmdListen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdListen.Size = New System.Drawing.Size(57, 21)
        Me.cmdListen.TabIndex = 2
        Me.cmdListen.Tag = "0"
        Me.cmdListen.Text = "Listen"
        Me.cmdListen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdListen.UseVisualStyleBackColor = False
        ' 
        'Label2
        ' 
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(0, 300)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(149, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Redirect DNS Queries to IP:"
        ' 
        'Form1
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6, 13)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(658, 346)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdCopy)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.txtIp)
        Me.Controls.Add(Me.Option2)
        Me.Controls.Add(Me.Option1)
        Me.Controls.Add(Me._ws_0)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.cmdListen)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Fake DNS"
        listBoxHelper1.SetSelectionMode(Me.List1, System.Windows.Forms.SelectionMode.One)
        CType(Me._ws_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)
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
#End Region
End Class