<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Video
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Tmr_Control = New System.Windows.Forms.Timer(Me.components)
        Me.Tmr_Play_Delay = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Tmr_Control
        '
        Me.Tmr_Control.Enabled = True
        Me.Tmr_Control.Interval = 10
        '
        'Tmr_Play_Delay
        '
        '
        'Video
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Video"
        Me.Text = "Video"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tmr_Control As Timer
    Friend WithEvents Tmr_Play_Delay As Timer
End Class
