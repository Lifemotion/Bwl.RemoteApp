<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClientForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.targetTextbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.operationComboBox = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.sourcePathTextbox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.runCommandTextbox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.runArgsTextbox = New System.Windows.Forms.TextBox()
        Me.runButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(0, 140)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(449, 213)
        Me.TextBox1.TabIndex = 1
        '
        'targetTextbox
        '
        Me.targetTextbox.Location = New System.Drawing.Point(103, 6)
        Me.targetTextbox.Name = "targetTextbox"
        Me.targetTextbox.Size = New System.Drawing.Size(108, 20)
        Me.targetTextbox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Target Host:Port"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(230, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Operation:"
        '
        'operationComboBox
        '
        Me.operationComboBox.FormattingEnabled = True
        Me.operationComboBox.Items.AddRange(New Object() {"deploy", "deploy-run", "run", "stop", "autorun"})
        Me.operationComboBox.Location = New System.Drawing.Point(319, 6)
        Me.operationComboBox.Name = "operationComboBox"
        Me.operationComboBox.Size = New System.Drawing.Size(121, 21)
        Me.operationComboBox.TabIndex = 6
        Me.operationComboBox.Text = "deploy-run"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Source Path:"
        '
        'sourcePathTextbox
        '
        Me.sourcePathTextbox.Location = New System.Drawing.Point(103, 32)
        Me.sourcePathTextbox.Name = "sourcePathTextbox"
        Me.sourcePathTextbox.Size = New System.Drawing.Size(337, 20)
        Me.sourcePathTextbox.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Run command:"
        '
        'runCommandTextbox
        '
        Me.runCommandTextbox.Location = New System.Drawing.Point(103, 58)
        Me.runCommandTextbox.Name = "runCommandTextbox"
        Me.runCommandTextbox.Size = New System.Drawing.Size(337, 20)
        Me.runCommandTextbox.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Run args:"
        '
        'argumentsTextbox
        '
        Me.runArgsTextbox.Location = New System.Drawing.Point(103, 85)
        Me.runArgsTextbox.Name = "argumentsTextbox"
        Me.runArgsTextbox.Size = New System.Drawing.Size(337, 20)
        Me.runArgsTextbox.TabIndex = 11
        '
        'runButton
        '
        Me.runButton.Location = New System.Drawing.Point(364, 114)
        Me.runButton.Name = "runButton"
        Me.runButton.Size = New System.Drawing.Size(76, 20)
        Me.runButton.TabIndex = 13
        Me.runButton.Text = "Run"
        Me.runButton.UseVisualStyleBackColor = True
        '
        'ClientForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 354)
        Me.Controls.Add(Me.runButton)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.runArgsTextbox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.runCommandTextbox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.sourcePathTextbox)
        Me.Controls.Add(Me.operationComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.targetTextbox)
        Me.Controls.Add(Me.TextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "ClientForm"
        Me.Text = "RemoteApp Client"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents targetTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents operationComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents sourcePathTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents runCommandTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents runArgsTextbox As System.Windows.Forms.TextBox
    Friend WithEvents runButton As System.Windows.Forms.Button

End Class
