Imports System.Windows.Forms

Public Class Dialog_Play
    Private Sub Dialog_Play_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim Form_Width As Integer = Me.Width
        Lbl_Tittle.Width = 885 '; 202 Me.Width - 100
        Lbl_Tittle.Text = T_Show(Curr_Show, 1)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Frm_360.Play_Process()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

End Class