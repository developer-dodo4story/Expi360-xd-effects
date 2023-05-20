<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog_Play
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
        Me.Lbl_Tittle = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Lbl_Tittle
        '
        Me.Lbl_Tittle.BackColor = System.Drawing.Color.White
        Me.Lbl_Tittle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Lbl_Tittle.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Lbl_Tittle.ForeColor = System.Drawing.Color.Maroon
        Me.Lbl_Tittle.Location = New System.Drawing.Point(87, 125)
        Me.Lbl_Tittle.Name = "Lbl_Tittle"
        Me.Lbl_Tittle.Size = New System.Drawing.Size(885, 202)
        Me.Lbl_Tittle.TabIndex = 5
        Me.Lbl_Tittle.Text = "Tittle"
        Me.Lbl_Tittle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(45, 342)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(970, 361)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.AutoSize = True
        Me.OK_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(42, 80)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(400, 200)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(527, 80)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(400, 200)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Dialog_Play
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1060, 828)
        Me.Controls.Add(Me.Lbl_Tittle)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Dialog_Play"
        Me.Text = "Play Show:"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Lbl_Tittle As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents OK_Button As Button
    Friend WithEvents Cancel_Button As Button
End Class
