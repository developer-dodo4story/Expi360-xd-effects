<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Edit
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
        Me.But_Pause = New System.Windows.Forms.Button()
        Me.But_Set_Position = New System.Windows.Forms.Button()
        Me.Lst_DMX = New System.Windows.Forms.ListBox()
        Me.Txt_Edit_TC = New System.Windows.Forms.TextBox()
        Me.Lbl_Curr_Frame_Edit = New System.Windows.Forms.Label()
        Me.But_ADD = New System.Windows.Forms.Button()
        Me.Lbl_Opis = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'But_Pause
        '
        Me.But_Pause.Location = New System.Drawing.Point(12, 12)
        Me.But_Pause.Name = "But_Pause"
        Me.But_Pause.Size = New System.Drawing.Size(121, 46)
        Me.But_Pause.TabIndex = 0
        Me.But_Pause.Text = "Pause"
        Me.But_Pause.UseVisualStyleBackColor = True
        '
        'But_Set_Position
        '
        Me.But_Set_Position.Location = New System.Drawing.Point(327, 12)
        Me.But_Set_Position.Name = "But_Set_Position"
        Me.But_Set_Position.Size = New System.Drawing.Size(121, 46)
        Me.But_Set_Position.TabIndex = 1
        Me.But_Set_Position.Text = "Set_Position"
        Me.But_Set_Position.UseVisualStyleBackColor = True
        '
        'Lst_DMX
        '
        Me.Lst_DMX.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Lst_DMX.FormattingEnabled = True
        Me.Lst_DMX.Location = New System.Drawing.Point(12, 142)
        Me.Lst_DMX.Name = "Lst_DMX"
        Me.Lst_DMX.Size = New System.Drawing.Size(644, 420)
        Me.Lst_DMX.TabIndex = 2
        '
        'Txt_Edit_TC
        '
        Me.Txt_Edit_TC.Location = New System.Drawing.Point(245, 38)
        Me.Txt_Edit_TC.Name = "Txt_Edit_TC"
        Me.Txt_Edit_TC.Size = New System.Drawing.Size(76, 20)
        Me.Txt_Edit_TC.TabIndex = 3
        '
        'Lbl_Curr_Frame_Edit
        '
        Me.Lbl_Curr_Frame_Edit.BackColor = System.Drawing.SystemColors.Window
        Me.Lbl_Curr_Frame_Edit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_Curr_Frame_Edit.Location = New System.Drawing.Point(139, 38)
        Me.Lbl_Curr_Frame_Edit.Name = "Lbl_Curr_Frame_Edit"
        Me.Lbl_Curr_Frame_Edit.Size = New System.Drawing.Size(78, 20)
        Me.Lbl_Curr_Frame_Edit.TabIndex = 4
        '
        'But_ADD
        '
        Me.But_ADD.Location = New System.Drawing.Point(12, 64)
        Me.But_ADD.Name = "But_ADD"
        Me.But_ADD.Size = New System.Drawing.Size(121, 59)
        Me.But_ADD.TabIndex = 5
        Me.But_ADD.Text = "ADD >>"
        Me.But_ADD.UseVisualStyleBackColor = True
        '
        'Lbl_Opis
        '
        Me.Lbl_Opis.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Lbl_Opis.Location = New System.Drawing.Point(11, 126)
        Me.Lbl_Opis.Name = "Lbl_Opis"
        Me.Lbl_Opis.Size = New System.Drawing.Size(644, 13)
        Me.Lbl_Opis.TabIndex = 6
        Me.Lbl_Opis.Text = "-"
        '
        'Frm_Edit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 575)
        Me.Controls.Add(Me.Lbl_Opis)
        Me.Controls.Add(Me.But_ADD)
        Me.Controls.Add(Me.Lbl_Curr_Frame_Edit)
        Me.Controls.Add(Me.Txt_Edit_TC)
        Me.Controls.Add(Me.Lst_DMX)
        Me.Controls.Add(Me.But_Set_Position)
        Me.Controls.Add(Me.But_Pause)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_Edit"
        Me.Text = "Frm_Edit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents But_Pause As Button
    Friend WithEvents But_Set_Position As Button
    Friend WithEvents Lst_DMX As ListBox
    Friend WithEvents Txt_Edit_TC As TextBox
    Friend WithEvents Lbl_Curr_Frame_Edit As Label
    Friend WithEvents But_ADD As Button
    Friend WithEvents Lbl_Opis As Label
End Class
