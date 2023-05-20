Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO

Module Mod_Init
#Region "Deklaracja zmiennych"
    Public Ft_Value(49) As Integer 'Kąt obrotu fotela
    Public Activ_Seat As Byte = 0 'Ilość aktywnych foteli
    Public T_Show(33, 3) 'Tablica programów:
    '-film: ścieżka do pliku z filmem, ścieżka do pliku z dźwiękiem, ścieżka do pliku z efektami,
    '-gra:  ścieżka do pliku wykonywalnego gry.
    Public Curr_Show As Byte = 0 'Aktualny "Show"
    Public Curr_Proc(33) As Process  'Aktualny proces
    Public Start_Fun_Key As Boolean = False 'Uruchamianie procesu z klawiszy funkcyjnych



    'VS
    Public PLAY_CTRL As Boolean = False  'Flaga wskazująca na stan PLAY (1) albo STOP(2)
    Public Current_Frame As Integer 'Aktualna pozycja odtwarzanego filmu w klatkach
    Public Time_to_Left As Double 'Oblicza czas do konca
    Public Mono_File As String = "" 'Film monoskopowy
    Public FilterGraph_State As Byte 'Stan pracy DirectShow: 0-otwarty FilterGraph, 1-Pause, 2-Play
    Public Play_XX As Byte 'Aktualny numer playlisty

    Public Video_Width As Integer = 0 'Szerokość formy do projekcji filmu
    Public Video_Height As Integer = 0 'Wysokość formy do projekcji filmu
    Public Video_X As Integer = 0 'Przesunięcie X formy do projekcji filmu


    'Sterowanie:
    Public PLT_Enable As Boolean = False 'Sterowanie fotelami z poziomu DoDo360OPlayer
    Public DMX_Enable As Boolean = False 'Sterowanie efektami DMX512
    Public Com_PLC As String = "COM0" 'Sterowniki platform
    Public TxD As String = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS" 'Dana ModBus do obracania foteli

    'Skrypty DMX:
    Public T_DMX(999, 17) As Integer 'Efekty DMX
    Public Step_Forward_Time As Double 'Przesuw poklatkowy
    Public File_5D As String 'Plik z efektami 5D

#End Region

#Region "Wczytanie ustawien"
    Sub Load_Config()

        'Wczytanie pliku "config.ini" - plik do wczytania nastaw programu:
        Try
            FileOpen(1, "config.ini", OpenMode.Input)
        Catch
            MsgBox("Could not find file: config.ini") : Exit Sub
        End Try

        Do Until EOF(1)
            Dim LoT As String : LoT = LineInput(1)
            Select Case Mid(LoT, 1, 7)
                'Wizualizacja / lokalizacja fotela:
                Case "[FT00]:" : Frm_360.FT00.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT00.Visible = False Else Activ_Seat += 1
                Case "[FT01]:" : Frm_360.FT01.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT01.Visible = False Else Activ_Seat += 1
                Case "[FT02]:" : Frm_360.FT02.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT02.Visible = False Else Activ_Seat += 1
                Case "[FT03]:" : Frm_360.FT03.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT03.Visible = False Else Activ_Seat += 1
                Case "[FT04]:" : Frm_360.FT04.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT04.Visible = False Else Activ_Seat += 1
                Case "[FT05]:" : Frm_360.FT05.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT05.Visible = False Else Activ_Seat += 1
                Case "[FT06]:" : Frm_360.FT06.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT06.Visible = False Else Activ_Seat += 1
                Case "[FT07]:" : Frm_360.FT07.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT07.Visible = False Else Activ_Seat += 1
                Case "[FT08]:" : Frm_360.FT08.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT08.Visible = False Else Activ_Seat += 1
                Case "[FT09]:" : Frm_360.FT09.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT09.Visible = False Else Activ_Seat += 1
                Case "[FT10]:" : Frm_360.FT10.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT10.Visible = False Else Activ_Seat += 1
                Case "[FT11]:" : Frm_360.FT11.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT11.Visible = False Else Activ_Seat += 1
                Case "[FT12]:" : Frm_360.FT12.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT12.Visible = False Else Activ_Seat += 1
                Case "[FT13]:" : Frm_360.FT13.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT13.Visible = False Else Activ_Seat += 1
                Case "[FT14]:" : Frm_360.FT14.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT14.Visible = False Else Activ_Seat += 1
                Case "[FT15]:" : Frm_360.FT15.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT15.Visible = False Else Activ_Seat += 1
                Case "[FT16]:" : Frm_360.FT16.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT16.Visible = False
                Case "[FT17]:" : Frm_360.FT17.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT17.Visible = False
                Case "[FT18]:" : Frm_360.FT18.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT18.Visible = False
                Case "[FT19]:" : Frm_360.FT19.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT19.Visible = False
                Case "[FT20]:" : Frm_360.FT20.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT20.Visible = False
                Case "[FT21]:" : Frm_360.FT21.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT21.Visible = False
                Case "[FT22]:" : Frm_360.FT22.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT22.Visible = False
                Case "[FT23]:" : Frm_360.FT23.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT23.Visible = False
                Case "[FT24]:" : Frm_360.FT24.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT24.Visible = False
                Case "[FT25]:" : Frm_360.FT25.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT25.Visible = False
                Case "[FT26]:" : Frm_360.FT26.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT26.Visible = False
                Case "[FT27]:" : Frm_360.FT27.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT27.Visible = False
                Case "[FT28]:" : Frm_360.FT28.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT28.Visible = False
                Case "[FT29]:" : Frm_360.FT29.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT29.Visible = False
                Case "[FT30]:" : Frm_360.FT30.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT30.Visible = False
                Case "[FT31]:" : Frm_360.FT31.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT31.Visible = False
                Case "[FT32]:" : Frm_360.FT32.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT32.Visible = False
                Case "[FT33]:" : Frm_360.FT33.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT33.Visible = False
                Case "[FT34]:" : Frm_360.FT34.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT34.Visible = False
                Case "[FT35]:" : Frm_360.FT35.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT35.Visible = False
                Case "[FT36]:" : Frm_360.FT36.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT36.Visible = False
                Case "[FT37]:" : Frm_360.FT37.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT37.Visible = False
                Case "[FT38]:" : Frm_360.FT38.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT38.Visible = False
                Case "[FT39]:" : Frm_360.FT39.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT39.Visible = False
                Case "[FT40]:" : Frm_360.FT40.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT40.Visible = False
                Case "[FT41]:" : Frm_360.FT41.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT41.Visible = False
                Case "[FT42]:" : Frm_360.FT42.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT42.Visible = False
                Case "[FT43]:" : Frm_360.FT43.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT43.Visible = False
                Case "[FT44]:" : Frm_360.FT44.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT44.Visible = False
                Case "[FT45]:" : Frm_360.FT45.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT45.Visible = False
                Case "[FT46]:" : Frm_360.FT46.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT46.Visible = False
                Case "[FT47]:" : Frm_360.FT47.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT47.Visible = False
                Case "[FT48]:" : Frm_360.FT48.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT48.Visible = False
                Case "[FT49]:" : Frm_360.FT49.Location = New Point(Val(Mid(LoT, 8, 3)), Val(Mid(LoT, 12, 3))) : If Mid(LoT, 8, 7) = "---,---" Then Frm_360.FT49.Visible = False

                Case "PLT = Y" : PLT_Enable = True'Sterowanie fotelami
                Case "DMX = Y" : DMX_Enable = True 'Sterowanie wytworniczmi DMX
                Case "Com = 1" : Com_PLC = "COM1" 'Sterowniki platform - komunikacja asynchroniczna
                Case "Com = 2" : Com_PLC = "COM2"
                Case "Com = 3" : Com_PLC = "COM3"
                Case "Com = 4" : Com_PLC = "COM4"
                Case "Com = 5" : Com_PLC = "COM5"
                Case "Com = 6" : Com_PLC = "COM6"
                Case "Com = 7" : Com_PLC = "COM7"
                Case "Com = 8" : Com_PLC = "COM8"
                Case "Com = 9" : Com_PLC = "COM9"
            End Select
        Loop
        FileClose(1)

        'Wczytanie pliku "show.txt" - plik do obsługi przycisków panela sterującego:
        Try
            FileOpen(2, "show.txt", OpenMode.Input)
        Catch
            MsgBox("Could not find file: show.txt") : Exit Sub
        End Try

        Do Until EOF(2)
            Dim LoT As String : LoT = LineInput(2)
            Select Case Mid(LoT, 1, 5)
                'Wczytanie programów do uruchamiania:
                Case "[01T]" : T_Show(1, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[01F]" : T_Show(1, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[01D]" : T_Show(1, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[02T]" : T_Show(2, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[02F]" : T_Show(2, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[02D]" : T_Show(2, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[03T]" : T_Show(3, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[03F]" : T_Show(3, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[03D]" : T_Show(3, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[04T]" : T_Show(4, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[04F]" : T_Show(4, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[04D]" : T_Show(4, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[05T]" : T_Show(5, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[05F]" : T_Show(5, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[05D]" : T_Show(5, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[06T]" : T_Show(6, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[06F]" : T_Show(6, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[06D]" : T_Show(6, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[07T]" : T_Show(7, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[07F]" : T_Show(7, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[07D]" : T_Show(7, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[08T]" : T_Show(8, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[08F]" : T_Show(8, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[08D]" : T_Show(8, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[09T]" : T_Show(9, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[09F]" : T_Show(9, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[09D]" : T_Show(9, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[10T]" : T_Show(10, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[10F]" : T_Show(10, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[10D]" : T_Show(10, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[11T]" : T_Show(11, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[11F]" : T_Show(11, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[11D]" : T_Show(11, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[12T]" : T_Show(12, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[12F]" : T_Show(12, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[12D]" : T_Show(12, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[13T]" : T_Show(13, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[13F]" : T_Show(13, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[13D]" : T_Show(13, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[14T]" : T_Show(14, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[14F]" : T_Show(14, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[14D]" : T_Show(14, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[15T]" : T_Show(15, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[15F]" : T_Show(15, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[15D]" : T_Show(15, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[16T]" : T_Show(16, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[16F]" : T_Show(16, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[16D]" : T_Show(16, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[17T]" : T_Show(17, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[17F]" : T_Show(17, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[17D]" : T_Show(17, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[18T]" : T_Show(18, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[18F]" : T_Show(18, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[18D]" : T_Show(18, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[19T]" : T_Show(19, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[19F]" : T_Show(19, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[19D]" : T_Show(19, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[20T]" : T_Show(20, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[20F]" : T_Show(20, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[20D]" : T_Show(20, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[21T]" : T_Show(21, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[21F]" : T_Show(21, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[21D]" : T_Show(21, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[22T]" : T_Show(22, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[22F]" : T_Show(22, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[22D]" : T_Show(22, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[23T]" : T_Show(23, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[23F]" : T_Show(23, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[23D]" : T_Show(23, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[24T]" : T_Show(24, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[24F]" : T_Show(24, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[24D]" : T_Show(24, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[25T]" : T_Show(25, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[25F]" : T_Show(25, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[25D]" : T_Show(25, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[26T]" : T_Show(26, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[26F]" : T_Show(26, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[26D]" : T_Show(26, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[27T]" : T_Show(27, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[27F]" : T_Show(27, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[27D]" : T_Show(27, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[28T]" : T_Show(28, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[28F]" : T_Show(28, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[28D]" : T_Show(28, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[29T]" : T_Show(29, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[29F]" : T_Show(29, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[29D]" : T_Show(29, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[30T]" : T_Show(30, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[30F]" : T_Show(30, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[30D]" : T_Show(30, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[31T]" : T_Show(31, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[31F]" : T_Show(31, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[31D]" : T_Show(31, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[32T]" : T_Show(32, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[32F]" : T_Show(32, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[32D]" : T_Show(32, 3) = Mid(LoT, 6, Len(LoT) - 5)

                Case "[33T]" : T_Show(33, 1) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[33F]" : T_Show(33, 2) = Mid(LoT, 6, Len(LoT) - 5)
                Case "[33D]" : T_Show(33, 3) = Mid(LoT, 6, Len(LoT) - 5)
            End Select

            Select Case Mid(LoT, 1, 4)
                Case "W = " : Video_Width = Mid(LoT, 5, Len(LoT) - 4)
                Case "H = " : Video_Height = Mid(LoT, 5, Len(LoT) - 4)
                Case "X = " : Video_X = Mid(LoT, 5, Len(LoT) - 4)
            End Select
        Loop
        FileClose(2)



    End Sub
#End Region

#Region "Load 5D"
    Sub Load_5D()

        T_DMX_Clear()

        Try
            FileOpen(5, File_5D, OpenMode.Input)
        Catch
            MsgBox("Could not find file: " & File_5D) : Exit Sub
        End Try

        T_DMX_Clear()
        Dim x As Integer = 0
        Do Until EOF(5)
            Dim LoT As String : LoT = LineInput(5)

            If Val(Mid(LoT, 1, 5)) > 0 Then
                x = x + 1
                T_DMX(x, 1) = Val(Mid(LoT, 1, 5))
                T_DMX(x, 2) = Val(Mid(LoT, 10, 3))
                T_DMX(x, 3) = Val(Mid(LoT, 16, 3))
                T_DMX(x, 4) = Val(Mid(LoT, 22, 3))
                T_DMX(x, 5) = Val(Mid(LoT, 28, 3))
                T_DMX(x, 6) = Val(Mid(LoT, 34, 3))
                T_DMX(x, 7) = Val(Mid(LoT, 40, 3))

                T_DMX(x, 8) = Val(Mid(LoT, 46, 3))
                T_DMX(x, 9) = Val(Mid(LoT, 52, 3))
                T_DMX(x, 10) = Val(Mid(LoT, 58, 3))
                T_DMX(x, 11) = Val(Mid(LoT, 64, 3))
                T_DMX(x, 12) = Val(Mid(LoT, 70, 3))
                T_DMX(x, 13) = Val(Mid(LoT, 76, 3))
                T_DMX(x, 14) = Val(Mid(LoT, 82, 3))
                T_DMX(x, 15) = Val(Mid(LoT, 88, 3))
                T_DMX(x, 16) = Val(Mid(LoT, 94, 3))
                T_DMX(x, 17) = Val(Mid(LoT, 100, 3))


                'Frm_Edit.Lst_DMX.Items.Add(Val(T_DMX(x, 1)) & " " & T_DMX(x, 2))




            End If




            'Select Case Mid(LoT, 1, 7)





            'End Select

        Loop
        FileClose(5)


    End Sub


    Sub T_DMX_Clear()
        Dim x As Integer, y As Byte
        For x = 1 To 999
            For y = 1 To 17
                T_DMX(x, y) = 0
            Next
        Next
    End Sub
#End Region



End Module
