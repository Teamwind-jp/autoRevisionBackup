<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.file1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.min1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bkfolder1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.maxrev1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.msg1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BackgroundWorker1
        '
        '
        'file1
        '
        Me.file1.AllowDrop = True
        Me.file1.Location = New System.Drawing.Point(22, 29)
        Me.file1.Name = "file1"
        Me.file1.Size = New System.Drawing.Size(384, 19)
        Me.file1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Drop backup file"
        '
        'min1
        '
        Me.min1.Location = New System.Drawing.Point(22, 179)
        Me.min1.Name = "min1"
        Me.min1.Size = New System.Drawing.Size(57, 19)
        Me.min1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Interval mins."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Drop Backup Folder"
        '
        'bkfolder1
        '
        Me.bkfolder1.AllowDrop = True
        Me.bkfolder1.Location = New System.Drawing.Point(22, 81)
        Me.bkfolder1.Name = "bkfolder1"
        Me.bkfolder1.Size = New System.Drawing.Size(384, 19)
        Me.bkfolder1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Max Revisions"
        '
        'maxrev1
        '
        Me.maxrev1.Location = New System.Drawing.Point(22, 130)
        Me.maxrev1.Name = "maxrev1"
        Me.maxrev1.Size = New System.Drawing.Size(57, 19)
        Me.maxrev1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(412, 175)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'msg1
        '
        Me.msg1.AutoSize = True
        Me.msg1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.msg1.Location = New System.Drawing.Point(366, 180)
        Me.msg1.Name = "msg1"
        Me.msg1.Size = New System.Drawing.Size(37, 12)
        Me.msg1.TabIndex = 6
        Me.msg1.Text = "0 revs"
        Me.msg1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(412, 79)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "View"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(412, 27)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "Log"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 214)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.msg1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.maxrev1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.bkfolder1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.min1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.file1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "auto Revision Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As Timer
    Friend WithEvents file1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents min1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents maxrev1 As TextBox
    Friend WithEvents bkfolder1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents msg1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
