Public Class Frm_Edit

    Dim T_DMX_Cnt As Integer = 0 'Wskaźnik Tablicy efktów DMX

    Private Sub Frm_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        T_DMX_Cnt = 0
        'Lst_DMX.HorizontalScrollbar = True
        'Lst_DMX.FormattingEnabled = True
        'Lst_DMX.MultiColumn = True
        'Lst_DMX.ColumnWidth = 20
        'Lst_DMX.SelectionMode = SelectionMode.MultiExtended


        'Lst_DMX.Items.Add("000" & vbTab & "|" & vbTab & "001")
        'Lst_DMX.Items.Add("000" & vbTab & "|" & vbTab & "256")

        ' Lst_DMX.Items.Add("1" & "2")
        ' Lst_DMX.Items.Add("2")
        Lbl_Opis.Text = "Timecode | BuA | SmA | BuB | SmB | BuC | SmC | BuD | SmD | BuE | SmE | BuF | SmF | W06 | W18 | W30 | Thd |"
    End Sub


    Private Sub But_Pause_Click(sender As Object, e As EventArgs) Handles But_Pause.Click
        Video.Pause()
    End Sub

    Private Sub But_Set_Position_Click(sender As Object, e As EventArgs) Handles But_Set_Position.Click
        'Set Position
        If Val(Txt_Edit_TC.Text) > 0 Then
            Dim Txt_Edit_TC_przecinek As String = "" 'Zamnienia przecinek na krpkę
            Dim x As Integer = 0
            For x = 1 To Len(Txt_Edit_TC.Text)
                If Mid(Txt_Edit_TC.Text, x, 1) <> "," Then
                    Txt_Edit_TC_przecinek = Txt_Edit_TC_przecinek & Mid(Txt_Edit_TC.Text, x, 1)
                Else
                    Txt_Edit_TC_przecinek = Txt_Edit_TC_przecinek & "."
                End If
            Next
            Step_Forward_Time = Math.Round((Val(Txt_Edit_TC_przecinek)), 3) ' Val(Txt_Edit_TC.Text)
            Call Video.Set_Position()
        End If
    End Sub




    Private Sub But_ADD_Click(sender As Object, e As EventArgs) Handles But_ADD.Click
        Call ADD_DMX_Effect
    End Sub


    'Add DMX Effect:
    Private Sub ADD_DMX_Effect()
        T_DMX_Cnt = T_DMX_Cnt + 1

        T_DMX(T_DMX_Cnt, 1) = Current_Frame
        'A:
        T_DMX(T_DMX_Cnt, 2) = Frm_360.Trk_DMX_A_Bubble.Value 'A Bubble
        T_DMX(T_DMX_Cnt, 3) = Frm_360.Trk_DMX_A_Smoke.Value 'A Smoke
        'B:
        T_DMX(T_DMX_Cnt, 4) = Frm_360.Trk_DMX_B_Bubble.Value 'B Bubble
        T_DMX(T_DMX_Cnt, 5) = Frm_360.Trk_DMX_B_Smoke.Value 'B Smoke
        'C:
        T_DMX(T_DMX_Cnt, 6) = Frm_360.Trk_DMX_C_Bubble.Value 'C Bubble
        T_DMX(T_DMX_Cnt, 7) = Frm_360.Trk_DMX_C_Smoke.Value 'C Smoke
        'D:
        T_DMX(T_DMX_Cnt, 8) = Frm_360.Trk_DMX_D_Bubble.Value 'D Bubble
        T_DMX(T_DMX_Cnt, 9) = Frm_360.Trk_DMX_D_Smoke.Value 'D Smoke
        'E
        T_DMX(T_DMX_Cnt, 10) = Frm_360.Trk_DMX_E_Bubble.Value 'E Bubble
        T_DMX(T_DMX_Cnt, 11) = Frm_360.Trk_DMX_E_Smoke.Value 'E Smoke
        'F
        T_DMX(T_DMX_Cnt, 12) = Frm_360.Trk_DMX_F_Bubble.Value 'F Bubble
        T_DMX(T_DMX_Cnt, 13) = Frm_360.Trk_DMX_F_Smoke.Value 'F Smoke
        'WIND:
        T_DMX(T_DMX_Cnt, 14) = Frm_360.Trk_DMX_Wind60.Value 'Trk_DMX_Wind60
        T_DMX(T_DMX_Cnt, 15) = Frm_360.Trk_DMX_Wind180.Value 'Trk_DMX_Wind180
        T_DMX(T_DMX_Cnt, 16) = Frm_360.Trk_DMX_Wind300.Value 'Trk_DMX_Wind300
        'THUNDER:
        T_DMX(T_DMX_Cnt, 17) = Frm_360.Trk_DMX_Thunder.Value 'Trk_DMX_Thunder


        'Aktualizacja Lst_Box:
        Lst_DMX.Items.Clear()
        Dim x As Integer = 0, y As Byte
        Dim Str As String = "", Str_dop As String = ""
        Dim Timecode As String = ""
        For x = 1 To 999
            If T_DMX(x, 1) > 0 Then

                Str = ""
                For y = 2 To 17
                    Select Case T_DMX(x, y)
                        Case < 10 : Str_dop = "00"
                        Case < 100 : Str_dop = "0"
                        Case < 1000 : Str_dop = ""
                    End Select
                    Str = Str & Str_dop & Val(T_DMX(x, y)) & " | "
                Next

                Select Case T_DMX(x, 1)
                    Case < 10 : Str_dop = "0000"
                    Case < 100 : Str_dop = "000"
                    Case < 1000 : Str_dop = "00"
                    Case < 10000 : Str_dop = "0"
                End Select
                Timecode = Str_dop & Val(T_DMX(x, 1))
                Lst_DMX.Items.Add(Timecode & vbTab & " | " & Str)
                'Lst_DMX.Items.Add(T_DMX(x, 1) & vbTab & " | " & Str)

            End If
        Next
        'Lst_DMX.Items.Insert(0, "Hi")



    End Sub


End Class