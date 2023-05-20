Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Data
'Imports System.Linq
Imports System.Text
'Imports System.Threading.Tasks

Imports System.IO
Imports System.Net.Sockets.Socket
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Imports System.Runtime.CompilerServices

Public Class Frm_360

#Region "Deklaracja zmiennych"
    Dim FT(49) As Integer 'Aktualna pozycja foteli


    Public RemoteIPEndPoint As New System.Net.IPEndPoint(System.Net.IPAddress.Any, 0)

    'Komunikacja z serwerem Gry:
    Public receivingUDPclient As UdpClient '
    Public ThreadReceive As System.Threading.Thread '
    Dim SocketNO As Integer '
    Dim UDP_Communication_Time_Delay As Byte = 0 'Sprawdzenie czy jest komunikacja UDP
    Dim Rx_Game_string As String = "" 'Pakiet odebrany od serwera Gier

    'Komunikacja z panelem dotykowym:
    Dim Panel_UDP_Subscriber As New Sockets.UdpClient
    Dim Panel_SocketNO As Integer '
    Public Panel_receivingUDPclient As UdpClient '
    Public Panel_ThreadReceive As System.Threading.Thread '
    Public Rx_Command_string As String

    Dim Ax1_crchigh As Byte, Ax1_crclow As Byte 'Obliczenie sumy kontrolnej
    Dim Ax1_RX As String 'Odebrana odpowiedź od falownika



#End Region

#Region "Inicjalizacja programu"
    Private Sub Frm_360_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponent()
        Load_Config()
        Video.Show()

        Me.Text = "DoDo 360" ' & " [" & Activ_Seat & "]"

        Dim x As Byte
        For x = 1 To 33
            Select Case x
                Case 1 : If Len(T_Show(x, 1)) > 0 Then But_01.Text = T_Show(x, 1) Else But_01.Text = "-"
                Case 2 : If Len(T_Show(x, 1)) > 0 Then But_02.Text = T_Show(x, 1) Else But_02.Text = "-"
                Case 3 : If Len(T_Show(x, 1)) > 0 Then But_03.Text = T_Show(x, 1) Else But_03.Text = "-"
                Case 4 : If Len(T_Show(x, 1)) > 0 Then But_04.Text = T_Show(x, 1) Else But_04.Text = "-"
                Case 5 : If Len(T_Show(x, 1)) > 0 Then But_05.Text = T_Show(x, 1) Else But_05.Text = "-"
                Case 6 : If Len(T_Show(x, 1)) > 0 Then But_06.Text = T_Show(x, 1) Else But_06.Text = "-"
                Case 7 : If Len(T_Show(x, 1)) > 0 Then But_07.Text = T_Show(x, 1) Else But_07.Text = "-"
                Case 8 : If Len(T_Show(x, 1)) > 0 Then But_08.Text = T_Show(x, 1) Else But_08.Text = "-"
                Case 9 : If Len(T_Show(x, 1)) > 0 Then But_09.Text = T_Show(x, 1) Else But_09.Text = "-"
                Case 10 : If Len(T_Show(x, 1)) > 0 Then But_10.Text = T_Show(x, 1) Else But_10.Text = "-"
                Case 11 : If Len(T_Show(x, 1)) > 0 Then But_11.Text = T_Show(x, 1) Else But_11.Text = "-"
                Case 12 : If Len(T_Show(x, 1)) > 0 Then But_12.Text = T_Show(x, 1) Else But_12.Text = "-"
                Case 13 : If Len(T_Show(x, 1)) > 0 Then But_13.Text = T_Show(x, 1) Else But_13.Text = "-"
                Case 14 : If Len(T_Show(x, 1)) > 0 Then But_14.Text = T_Show(x, 1) Else But_14.Text = "-"
                Case 15 : If Len(T_Show(x, 1)) > 0 Then But_15.Text = T_Show(x, 1) Else But_15.Text = "-"
                Case 16 : If Len(T_Show(x, 1)) > 0 Then But_16.Text = T_Show(x, 1) Else But_16.Text = "-"
                Case 17 : If Len(T_Show(x, 1)) > 0 Then But_17.Text = T_Show(x, 1) Else But_17.Text = "-"
                Case 18 : If Len(T_Show(x, 1)) > 0 Then But_18.Text = T_Show(x, 1) Else But_18.Text = "-"
                Case 19 : If Len(T_Show(x, 1)) > 0 Then But_19.Text = T_Show(x, 1) Else But_19.Text = "-"
                Case 20 : If Len(T_Show(x, 1)) > 0 Then But_20.Text = T_Show(x, 1) Else But_20.Text = "-"
                Case 21 : If Len(T_Show(x, 1)) > 0 Then But_21.Text = T_Show(x, 1) Else But_21.Text = "-"
                Case 22 : If Len(T_Show(x, 1)) > 0 Then But_22.Text = T_Show(x, 1) Else But_22.Text = "-"
                Case 23 : If Len(T_Show(x, 1)) > 0 Then But_23.Text = T_Show(x, 1) Else But_23.Text = "-"
                Case 24 : If Len(T_Show(x, 1)) > 0 Then But_24.Text = T_Show(x, 1) Else But_24.Text = "-"
                Case 25 : If Len(T_Show(x, 1)) > 0 Then But_25.Text = T_Show(x, 1) Else But_25.Text = "-"
                Case 26 : If Len(T_Show(x, 1)) > 0 Then But_26.Text = T_Show(x, 1) Else But_26.Text = "-"
                Case 27 : If Len(T_Show(x, 1)) > 0 Then But_27.Text = T_Show(x, 1) Else But_27.Text = "-"
                Case 28 : If Len(T_Show(x, 1)) > 0 Then But_28.Text = T_Show(x, 1) Else But_28.Text = "-"
                Case 29 : If Len(T_Show(x, 1)) > 0 Then But_29.Text = T_Show(x, 1) Else But_29.Text = "-"
                Case 30 : If Len(T_Show(x, 1)) > 0 Then But_30.Text = T_Show(x, 1) Else But_30.Text = "-"
                Case 31 : If Len(T_Show(x, 1)) > 0 Then But_31.Text = T_Show(x, 1) Else But_31.Text = "-"
                Case 32 : If Len(T_Show(x, 1)) > 0 Then But_32.Text = T_Show(x, 1) Else But_32.Text = "-"
                Case 33 : If Len(T_Show(x, 1)) > 0 Then But_33.Text = T_Show(x, 1) Else But_33.Text = "-"
            End Select
        Next

        Set_Seat_Position()
        '                   1    6    11   16   21   26   31   36   41   46
        Txt_Game_Rx.Text = "{Sta}{Ide}{Deg}{Rsv}{BuA}{SmA}{BuB}{SmB}{BuC}{SmC}" &
                           "{BuD}{SmD}{BuE}{SmE}{BuF}{SmF}{W06}{W18}{W30}{Thd}" & ' 51 -  96
                           "{000}{001}{002}{003}{004}{005}{006}{007}{008}{009}" & '101 - 146
                           "{010}{011}{012}{013}{014}{015}{016}{017}{018}{019}" & '151 - 196
                           "{020}{021}{022}{023}{024}{025}{026}{027}{028}{029}" & '201 - 246
                           "{030}{031}{032}{033}{034}{035}{036}{037}{038}{039}" & '251 - 296
                           "{040}{041}{042}{043}{044}{045}{046}{047}{048}{049}" '  301 - 346

        Try
            'UDP Gry
            Lbl_Socket.Text = "11003" : SocketNO = 11003 ' Lbl_Socket.Text
            receivingUDPclient = New System.Net.Sockets.UdpClient(SocketNO)
            Tim_UDP_Control.Enabled = True
            'UDP Panel
            'Lbl_Panel_Socket.Text = "11004" : Panel_SocketNO = 11004 ' Lbl_Panel_Socket.Text
            'Panel_receivingUDPclient = New System.Net.Sockets.UdpClient(Panel_SocketNO)

            NewInitialize()
            'NewPanelInitialize()

        Catch ex As Exception
        End Try

        Try
            'UDP Gry
            'Lbl_Socket.Text = "11003" : SocketNO = 11003 ' Lbl_Socket.Text
            'receivingUDPclient = New System.Net.Sockets.UdpClient(SocketNO)
            'Tim_UDP_Control.Enabled = True
            'UDP Panel
            Lbl_Panel_Socket.Text = "11004" : Panel_SocketNO = 11004 ' Lbl_Panel_Socket.Text
            Panel_receivingUDPclient = New System.Net.Sockets.UdpClient(Panel_SocketNO)

            'NewInitialize()
            NewPanelInitialize()

        Catch ex As Exception
        End Try








        'Ser_MODBUS.Open() : AddHandler Ser_MODBUS.DataReceived, AddressOf Ax1_Rx_Data 'Inicjacja portu
        If DMX_Enable = True Then DMX_init()
        If Com_PLC = "COM0" Then
            Lbl_RS_232.Text = "Com = none"
        Else
            Lbl_RS_232.Text = "Com = " & Com_PLC
            Ser_MODBUS.PortName = Com_PLC
            Ser_MODBUS.Open()
            Tim_MODBUS.Enabled = True
        End If

    End Sub

    Public Sub New()
        'InitalizeFTArray()
    End Sub

    'Private Sub InitalizeFTArray()

    'End Sub
#End Region

#Region "Pozycjonowanie foteli"
    Public Sub ReceiveMessages()
        'Stop
        Try
            Dim receiveBytes As Byte() = receivingUDPclient.Receive(RemoteIPEndPoint)
            Lbl_IP.Text = RemoteIPEndPoint.Address.ToString
            Dim BitDet As BitArray
            BitDet = New BitArray(receiveBytes)
            Dim strReturnData As String = System.Text.Encoding.Unicode.GetString(receiveBytes)
            Rx_Game_string = Encoding.ASCII.GetChars(receiveBytes)
            Txt_Game_Rx.Text = Rx_Game_string
            UDP_Communication_Time_Delay = 0
            Convert_Rx_Game_String()
            Set_Seat_Position()
            NewInitialize()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub PanelReceiveMessages()
        'Stop
        Try
            Dim receiveBytes As Byte() = Panel_receivingUDPclient.Receive(RemoteIPEndPoint)
            Lbl_Panel_IP.Text = RemoteIPEndPoint.Address.ToString
            Dim BitDet As BitArray
            BitDet = New BitArray(receiveBytes)
            Dim strReturnData As String = System.Text.Encoding.Unicode.GetString(receiveBytes)
            Rx_Command_string = Encoding.ASCII.GetChars(receiveBytes)
            'UDP_Communication_Time_Delay = 0
            Convert_Panel_Command()
            'Set_Seat_Position()
            NewPanelInitialize()
        Catch ex As Exception
        End Try

    End Sub



    Private Sub Tim_UDP_Control_Tick(sender As Object, e As EventArgs) Handles Tim_UDP_Control.Tick

        If UDP_Communication_Time_Delay < 10 Then UDP_Communication_Time_Delay = UDP_Communication_Time_Delay + 1 Else UDP_Communication_Time_Delay = 5
        If UDP_Communication_Time_Delay > 4 Then
            If (UDP_Communication_Time_Delay And 1) = 1 Then Txt_Game_Rx.Text = "Not connected !!!" Else Txt_Game_Rx.Text = ""

            If Chk_Service_Mode.Checked = False Then
                If Lbl_Duration.Text = "" Then
                    If Reset_XD_Delay < 255 Then Reset_XD_Delay = Reset_XD_Delay + 1 Else Reset_XD_Delay = 0
                    If Reset_XD_Delay = 200 Then XD_All_Reset()
                End If
            End If

            If Lbl_Duration.Text <> "" Then Reset_XD_Delay = 0

        End If
            If UDP_Communication_Time_Delay = 10 Then Lbl_IP.Text = "---.---.---.---.---"


        If Lbl_IP.Text <> "---.---.---.---.---" Then Reset_XD_Delay = 0
        Lbl_XD_Delay.Text = Reset_XD_Delay.ToString
    End Sub

    Dim Reset_XD_Delay As Byte = 0 'Do wizualizacji czasu
    'Private Sub Tim_XD_Reset_Tick(sender As Object, e As EventArgs) Handles Tim_XD_Reset.Tick 'Reset efektów gdy brak komunikacji z grą

    'End Sub



    Public Sub NewInitialize()
        ThreadReceive = New System.Threading.Thread(AddressOf ReceiveMessages) : ThreadReceive.Start()
    End Sub
    Public Sub NewPanelInitialize()
        Panel_ThreadReceive = New System.Threading.Thread(AddressOf PanelReceiveMessages) : Panel_ThreadReceive.Start()
    End Sub

    Private Sub Convert_Panel_Command()
        'Lbl_Panel_Command.Text = Rx_Command_string
        Select Case Rx_Command_string
            Case "Start00" : Lbl_Panel_Command.Text = "Stop"
            Case "Start01" : Start_Fun_Key = True : Start_Show_01()
            Case "Start02" : Start_Fun_Key = True : Start_Show_02()
            Case "Start03" : Start_Fun_Key = True : Start_Show_03()
            Case "Start04" : Start_Fun_Key = True : Start_Show_04()
            Case "Start05" : Start_Fun_Key = True : Start_Show_05()
            Case "Start06" : Start_Fun_Key = True : Start_Show_06()
            Case "Start07" : Start_Fun_Key = True : Start_Show_07()
            Case "Start08" : Start_Fun_Key = True : Start_Show_08()
            Case "Start09" : Start_Fun_Key = True : Start_Show_09()
            Case "Start10" : Start_Fun_Key = True : Start_Show_10()
            Case "Start11" : Start_Fun_Key = True : Start_Show_11()
            Case "Start12" : Start_Fun_Key = True : Start_Show_12()
            Case "Start13" : Start_Fun_Key = True : Start_Show_13()
            Case "Start14" : Start_Fun_Key = True : Start_Show_14()
            Case "Start15" : Start_Fun_Key = True : Start_Show_15()
            Case "Start16" : Start_Fun_Key = True : Start_Show_16()
            Case "Start17" : Start_Fun_Key = True : Start_Show_17()
            Case "Start18" : Start_Fun_Key = True : Start_Show_18()
            Case "Start19" : Start_Fun_Key = True : Start_Show_19()
            Case "Start20" : Start_Fun_Key = True : Start_Show_20()
            Case "Start21" : Start_Fun_Key = True : Start_Show_21()
            Case "Start22" : Start_Fun_Key = True : Start_Show_22()
            Case "Start23" : Start_Fun_Key = True : Start_Show_23()
            Case "Start24" : Start_Fun_Key = True : Start_Show_24()
            Case "Start25" : Start_Fun_Key = True : Start_Show_25()
            Case "Start26" : Start_Fun_Key = True : Start_Show_26()
            Case "Start27" : Start_Fun_Key = True : Start_Show_27()
            Case "Start28" : Start_Fun_Key = True : Start_Show_28()
            Case "Start29" : Start_Fun_Key = True : Start_Show_29()
            Case "Start30" : Start_Fun_Key = True : Start_Show_30()
            Case "Start31" : Start_Fun_Key = True : Start_Show_31()
            Case "Start32" : Start_Fun_Key = True : Start_Show_32()
            Case "Start33" : Start_Fun_Key = True : Start_Show_33()
        End Select

        'Rx_Command_string = ""
    End Sub

    Private Sub Convert_Rx_Game_String()
        Dim S_Rx As String = ""
        For i = 1 To 346 Step 5
            S_Rx = Mid(Rx_Game_string, i + 1, 3)
            Select Case i

                Case 21 : Trk_DMX_A_Bubble.Value = Val(S_Rx) : Chk_A_Bubble_Set()
                Case 26 : Trk_DMX_A_Smoke.Value = Val(S_Rx) : Chk_A_Smoke_Set()
                Case 31 : Trk_DMX_B_Bubble.Value = Val(S_Rx) : Chk_B_Bubble_Set()
                Case 36 : Trk_DMX_B_Smoke.Value = Val(S_Rx) : Chk_B_Smoke_Set()
                Case 41 : Trk_DMX_C_Bubble.Value = Val(S_Rx) : Chk_C_Bubble_Set()
                Case 46 : Trk_DMX_C_Smoke.Value = Val(S_Rx) : Chk_C_Smoke_Set()
                Case 51 : Trk_DMX_D_Bubble.Value = Val(S_Rx) : Chk_D_Bubble_Set()
                Case 56 : Trk_DMX_D_Smoke.Value = Val(S_Rx) : Chk_D_Smoke_Set()
                Case 61 : Trk_DMX_E_Bubble.Value = Val(S_Rx) : Chk_E_Bubble_Set()
                Case 66 : Trk_DMX_E_Smoke.Value = Val(S_Rx) : Chk_E_Smoke_Set()
                Case 71 : Trk_DMX_F_Bubble.Value = Val(S_Rx) : Chk_F_Bubble_Set()
                Case 76 : Trk_DMX_F_Smoke.Value = Val(S_Rx) : Chk_F_Smoke_Set()
                Case 81 : Trk_DMX_Wind60.Value = Val(S_Rx) : Chk_Wind60_Set()
                Case 86 : Trk_DMX_Wind180.Value = Val(S_Rx) : Chk_Wind180_Set()
                Case 91 : Trk_DMX_Wind300.Value = Val(S_Rx) : Chk_Wind300_Set()
                Case 96 : Trk_DMX_Thunder.Value = Val(S_Rx) : Chk_Thunder_Set()

'Fotel 00 do 09
                Case 101 : FT(0) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 1, 1) = Mid(S_Rx, 1, 1)
                Case 106 : FT(1) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 2, 1) = Mid(S_Rx, 1, 1)
                Case 111 : FT(2) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 3, 1) = Mid(S_Rx, 1, 1)
                Case 116 : FT(3) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 4, 1) = Mid(S_Rx, 1, 1)
                Case 121 : FT(4) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 5, 1) = Mid(S_Rx, 1, 1)
                Case 126 : FT(5) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 6, 1) = Mid(S_Rx, 1, 1)
                Case 131 : FT(6) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 7, 1) = Mid(S_Rx, 1, 1)
                Case 136 : FT(7) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 8, 1) = Mid(S_Rx, 1, 1)
                Case 141 : FT(8) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 9, 1) = Mid(S_Rx, 1, 1)
                Case 146 : FT(9) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 10, 1) = Mid(S_Rx, 1, 1)
'Fotel 10 do 19
                Case 151 : FT(10) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 11, 1) = Mid(S_Rx, 1, 1)
                Case 156 : FT(11) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 12, 1) = Mid(S_Rx, 1, 1)
                Case 161 : FT(12) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 13, 1) = Mid(S_Rx, 1, 1)
                Case 166 : FT(13) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 14, 1) = Mid(S_Rx, 1, 1)
                Case 171 : FT(14) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 15, 1) = Mid(S_Rx, 1, 1)
                Case 176 : FT(15) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 16, 1) = Mid(S_Rx, 1, 1)
                Case 181 : FT(16) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 17, 1) = Mid(S_Rx, 1, 1)
                Case 186 : FT(17) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 18, 1) = Mid(S_Rx, 1, 1)
                Case 191 : FT(18) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 19, 1) = Mid(S_Rx, 1, 1)
                Case 196 : FT(19) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 20, 1) = Mid(S_Rx, 1, 1)
'Fotel 20 do 29
                Case 201 : FT(20) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 21, 1) = Mid(S_Rx, 1, 1)
                Case 206 : FT(21) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 22, 1) = Mid(S_Rx, 1, 1)
                Case 211 : FT(22) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 23, 1) = Mid(S_Rx, 1, 1)
                Case 216 : FT(23) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 24, 1) = Mid(S_Rx, 1, 1)
                Case 221 : FT(24) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 25, 1) = Mid(S_Rx, 1, 1)
                Case 226 : FT(25) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 26, 1) = Mid(S_Rx, 1, 1)
                Case 231 : FT(26) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 27, 1) = Mid(S_Rx, 1, 1)
                Case 236 : FT(27) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 28, 1) = Mid(S_Rx, 1, 1)
                Case 241 : FT(28) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 29, 1) = Mid(S_Rx, 1, 1)
                Case 246 : FT(29) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 30, 1) = Mid(S_Rx, 1, 1)
'Fotel 30 do 39
                Case 251 : FT(30) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 31, 1) = Mid(S_Rx, 1, 1)
                Case 256 : FT(31) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 32, 1) = Mid(S_Rx, 1, 1)
                Case 261 : FT(32) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 33, 1) = Mid(S_Rx, 1, 1)
                Case 266 : FT(33) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 34, 1) = Mid(S_Rx, 1, 1)
                Case 271 : FT(34) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 35, 1) = Mid(S_Rx, 1, 1)
                Case 276 : FT(35) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 36, 1) = Mid(S_Rx, 1, 1)
                Case 281 : FT(36) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 37, 1) = Mid(S_Rx, 1, 1)
                Case 286 : FT(37) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 38, 1) = Mid(S_Rx, 1, 1)
                Case 291 : FT(38) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 39, 1) = Mid(S_Rx, 1, 1)
                Case 296 : FT(39) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 40, 1) = Mid(S_Rx, 1, 1)
'Fotel 40 do 49
                Case 301 : FT(40) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 41, 1) = Mid(S_Rx, 1, 1)
                Case 306 : FT(41) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 42, 1) = Mid(S_Rx, 1, 1)
                Case 311 : FT(42) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 43, 1) = Mid(S_Rx, 1, 1)
                Case 316 : FT(43) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 44, 1) = Mid(S_Rx, 1, 1)
                Case 321 : FT(44) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 45, 1) = Mid(S_Rx, 1, 1)
                Case 326 : FT(45) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 46, 1) = Mid(S_Rx, 1, 1)
                Case 331 : FT(46) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 47, 1) = Mid(S_Rx, 1, 1)
                Case 336 : FT(47) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 48, 1) = Mid(S_Rx, 1, 1)
                Case 341 : FT(48) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 49, 1) = Mid(S_Rx, 1, 1)
                Case 346 : FT(49) = Val(Mid(S_Rx, 2, 2)) : Mid(TxD, 50, 1) = Mid(S_Rx, 1, 1)
            End Select
        Next
    End Sub

    Private Sub Set_Seat_Position()
        'GoTo Bez_Pozycjonowania
        Dim Cnt As Byte = 0
        For Cnt = 0 To (Activ_Seat - 1)
            'Load up an image from disk
            'Dim imgOrig As Image = Image.FromFile("C:\Users\Hydra\OneDrive\Projects\DoDo360\52x52x90.bmp")
            Dim imgOrig As Image = My.Resources._52x52x90
            'Make a copy
            Dim imgClone As Image = imgOrig.Clone

            'Angle of rotation in degrees
            Dim angle As Single
            angle = 270 ' angle = 360 - FT(Cnt)

            'Create the graphics object to draw onto
            Dim g As Graphics = Graphics.FromImage(imgOrig)
            'Clear the picture and give us a black background
            g.Clear(Color.White)

            'Translate to the center of the image
            g.TranslateTransform(imgClone.Width / 2, imgClone.Height / 2)
            'Rotate about center of image
            g.RotateTransform(angle)
            'Translate back
            g.TranslateTransform(-imgClone.Width / 2, -imgClone.Height / 2)

            'Draw the image
            g.DrawImage(imgClone, New Point(0, 0))
            'Display the results in a PictureBox
            'Pic_Ft_00.Image = imgOrig.Clone

            Select Case Cnt
                Case 0 : Pic_Ft_00.Image = imgOrig.Clone ': FT00.Text = "00:" & FT(Cnt).ToString & "°"
                Case 1 : Pic_Ft_01.Image = imgOrig.Clone ': FT01.Text = "01:" & FT(Cnt).ToString & "°"
                Case 2 : Pic_Ft_02.Image = imgOrig.Clone ': FT02.Text = "02:" & FT(Cnt).ToString & "°"
                Case 3 : Pic_Ft_03.Image = imgOrig.Clone ': FT03.Text = "03:" & FT(Cnt).ToString & "°"
                Case 4 : Pic_Ft_04.Image = imgOrig.Clone ': FT04.Text = "04:" & FT(Cnt).ToString & "°"
                Case 5 : Pic_Ft_05.Image = imgOrig.Clone ': FT05.Text = "05:" & FT(Cnt).ToString & "°"
                Case 6 : Pic_Ft_06.Image = imgOrig.Clone ': FT06.Text = "06:" & FT(Cnt).ToString & "°"
                Case 7 : Pic_Ft_07.Image = imgOrig.Clone ': FT07.Text = "07:" & FT(Cnt).ToString & "°"
                Case 8 : Pic_Ft_08.Image = imgOrig.Clone ': FT08.Text = "08:" & FT(Cnt).ToString & "°"
                Case 9 : Pic_Ft_09.Image = imgOrig.Clone ': FT09.Text = "09:" & FT(Cnt).ToString & "°"
                Case 10 : Pic_Ft_10.Image = imgOrig.Clone ': FT10.Text = "10:" & FT(Cnt).ToString & "°"
                Case 11 : Pic_Ft_11.Image = imgOrig.Clone ': FT11.Text = "11:" & FT(Cnt).ToString & "°"
                Case 12 : Pic_Ft_12.Image = imgOrig.Clone ': FT12.Text = "12:" & FT(Cnt).ToString & "°"
                Case 13 : Pic_Ft_13.Image = imgOrig.Clone ': FT13.Text = "13:" & FT(Cnt).ToString & "°"
                Case 14 : Pic_Ft_14.Image = imgOrig.Clone ': FT14.Text = "14:" & FT(Cnt).ToString & "°"
                Case 15 : Pic_Ft_15.Image = imgOrig.Clone ': FT15.Text = "15:" & FT(Cnt).ToString & "°"
                Case 16 : Pic_Ft_16.Image = imgOrig.Clone ': FT16.Text = "16:" & FT(Cnt).ToString & "°"
                Case 17 : Pic_Ft_17.Image = imgOrig.Clone ': FT17.Text = "17:" & FT(Cnt).ToString & "°"
                Case 18 : Pic_Ft_18.Image = imgOrig.Clone ': FT18.Text = "18:" & FT(Cnt).ToString & "°"
                Case 19 : Pic_Ft_19.Image = imgOrig.Clone ': FT19.Text = "19:" & FT(Cnt).ToString & "°"
                Case 20 : Pic_Ft_20.Image = imgOrig.Clone ': FT20.Text = "20:" & FT(Cnt).ToString & "°"
                Case 21 : Pic_Ft_21.Image = imgOrig.Clone ': FT21.Text = "21:" & FT(Cnt).ToString & "°"
                Case 22 : Pic_Ft_22.Image = imgOrig.Clone ': FT22.Text = "22:" & FT(Cnt).ToString & "°"
                Case 23 : Pic_Ft_23.Image = imgOrig.Clone ': FT23.Text = "23:" & FT(Cnt).ToString & "°"
                Case 24 : Pic_Ft_24.Image = imgOrig.Clone ': FT24.Text = "24:" & FT(Cnt).ToString & "°"
                Case 25 : Pic_Ft_25.Image = imgOrig.Clone ': FT25.Text = "25:" & FT(Cnt).ToString & "°"
                Case 26 : Pic_Ft_26.Image = imgOrig.Clone ': FT26.Text = "26:" & FT(Cnt).ToString & "°"
                Case 27 : Pic_Ft_27.Image = imgOrig.Clone ': FT27.Text = "27:" & FT(Cnt).ToString & "°"
                Case 28 : Pic_Ft_28.Image = imgOrig.Clone ': FT28.Text = "28:" & FT(Cnt).ToString & "°"
                Case 29 : Pic_Ft_29.Image = imgOrig.Clone ': FT29.Text = "29:" & FT(Cnt).ToString & "°"
                Case 30 : Pic_Ft_30.Image = imgOrig.Clone ': FT30.Text = "30:" & FT(Cnt).ToString & "°"
                Case 31 : Pic_Ft_31.Image = imgOrig.Clone ': FT31.Text = "31:" & FT(Cnt).ToString & "°"
                Case 32 : Pic_Ft_32.Image = imgOrig.Clone ': FT32.Text = "32:" & FT(Cnt).ToString & "°"
                Case 33 : Pic_Ft_33.Image = imgOrig.Clone ': FT33.Text = "33:" & FT(Cnt).ToString & "°"
                Case 34 : Pic_Ft_34.Image = imgOrig.Clone ': FT34.Text = "34:" & FT(Cnt).ToString & "°"
                Case 35 : Pic_Ft_35.Image = imgOrig.Clone ': FT35.Text = "35:" & FT(Cnt).ToString & "°"
                Case 36 : Pic_Ft_36.Image = imgOrig.Clone ': FT36.Text = "36:" & FT(Cnt).ToString & "°"
                Case 37 : Pic_Ft_37.Image = imgOrig.Clone ': FT37.Text = "37:" & FT(Cnt).ToString & "°"
                Case 38 : Pic_Ft_38.Image = imgOrig.Clone ': FT38.Text = "38:" & FT(Cnt).ToString & "°"
                Case 39 : Pic_Ft_39.Image = imgOrig.Clone ': FT39.Text = "39:" & FT(Cnt).ToString & "°"
                Case 40 : Pic_Ft_40.Image = imgOrig.Clone ': FT40.Text = "40:" & FT(Cnt).ToString & "°"
                Case 41 : Pic_Ft_41.Image = imgOrig.Clone ': FT41.Text = "41:" & FT(Cnt).ToString & "°"
                Case 42 : Pic_Ft_42.Image = imgOrig.Clone ': FT42.Text = "42:" & FT(Cnt).ToString & "°"
                Case 43 : Pic_Ft_43.Image = imgOrig.Clone ': FT43.Text = "43:" & FT(Cnt).ToString & "°"
                Case 44 : Pic_Ft_44.Image = imgOrig.Clone ': FT44.Text = "44:" & FT(Cnt).ToString & "°"
                Case 45 : Pic_Ft_45.Image = imgOrig.Clone ': FT45.Text = "45:" & FT(Cnt).ToString & "°"
                Case 46 : Pic_Ft_46.Image = imgOrig.Clone ': FT46.Text = "46:" & FT(Cnt).ToString & "°"
                Case 47 : Pic_Ft_47.Image = imgOrig.Clone ': FT47.Text = "47:" & FT(Cnt).ToString & "°"
                Case 48 : Pic_Ft_48.Image = imgOrig.Clone ': FT48.Text = "48:" & FT(Cnt).ToString & "°"
                Case 49 : Pic_Ft_49.Image = imgOrig.Clone ': FT49.Text = "49:" & FT(Cnt).ToString & "°"
            End Select

            'clean up
            imgClone.Dispose()
            imgOrig.Dispose()
            g.Dispose()
        Next

Bez_Pozycjonowania:
        Lbl_Txd.Text = "TxD: " & TxD
    End Sub


#End Region

#Region "Sterowanie ręczne foteli"
    Dim Deg As Byte = 10
    '00:
    Private Sub But_FT00_R_Click(sender As Object, e As EventArgs) Handles But_FT00_R.Click
        FT(0) += Deg : If FT(0) >= 360 Then FT(0) = 0
        Mid(TxD, 1, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT00_L_Click(sender As Object, e As EventArgs) Handles But_FT00_L.Click
        FT(0) -= Deg : If FT(0) < 0 Then FT(0) = 360 + FT(0)
        Mid(TxD, 1, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT00_S_Click(sender As Object, e As EventArgs) Handles But_FT00_S.Click
        Mid(TxD, 1, 1) = "S"
        Set_Seat_Position()
    End Sub
    '01:
    Private Sub But_FT01_R_Click(sender As Object, e As EventArgs) Handles But_FT01_R.Click
        FT(1) += Deg : If FT(1) >= 360 Then FT(1) = 0
        Mid(TxD, 2, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT01_L_Click(sender As Object, e As EventArgs) Handles But_FT01_L.Click
        FT(1) -= Deg : If FT(1) < 0 Then FT(1) = 360 + FT(1)
        Mid(TxD, 2, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT01_S_Click(sender As Object, e As EventArgs) Handles But_FT01_S.Click
        Mid(TxD, 2, 1) = "S"
        Set_Seat_Position()
    End Sub
    '02:
    Private Sub But_FT02_R_Click(sender As Object, e As EventArgs) Handles But_FT02_R.Click
        FT(2) += Deg : If FT(2) >= 360 Then FT(2) = 0
        Mid(TxD, 3, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT02_L_Click(sender As Object, e As EventArgs) Handles But_FT02_L.Click
        FT(2) -= Deg : If FT(2) < 0 Then FT(2) = 360 + FT(2)
        Mid(TxD, 3, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT02_S_Click(sender As Object, e As EventArgs) Handles But_FT02_S.Click
        Mid(TxD, 3, 1) = "S"
        Set_Seat_Position()
    End Sub
    '03:
    Private Sub But_FT03_R_Click(sender As Object, e As EventArgs) Handles But_FT03_R.Click
        FT(3) += Deg : If FT(3) >= 360 Then FT(3) = 0
        Mid(TxD, 4, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT03_L_Click(sender As Object, e As EventArgs) Handles But_FT03_L.Click
        FT(3) -= Deg : If FT(3) < 0 Then FT(3) = 360 + FT(3)
        Mid(TxD, 4, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT03_S_Click(sender As Object, e As EventArgs) Handles But_FT03_S.Click
        Mid(TxD, 4, 1) = "S"
        Set_Seat_Position()
    End Sub
    '04:
    Private Sub But_FT04_R_Click(sender As Object, e As EventArgs) Handles But_FT04_R.Click
        FT(4) += Deg : If FT(4) >= 360 Then FT(4) = 0
        Mid(TxD, 5, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT04_L_Click(sender As Object, e As EventArgs) Handles But_FT04_L.Click
        FT(4) -= Deg : If FT(4) < 0 Then FT(4) = 360 + FT(4)
        Mid(TxD, 5, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT04_S_Click(sender As Object, e As EventArgs) Handles But_FT04_S.Click
        Mid(TxD, 5, 1) = "S"
        Set_Seat_Position()
    End Sub
    '05:
    Private Sub But_FT05_R_Click(sender As Object, e As EventArgs) Handles But_FT05_R.Click
        FT(5) += Deg : If FT(5) >= 360 Then FT(5) = 0
        Mid(TxD, 6, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT05_L_Click(sender As Object, e As EventArgs) Handles But_FT05_L.Click
        FT(5) -= Deg : If FT(5) < 0 Then FT(5) = 360 + FT(5)
        Mid(TxD, 6, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT05_S_Click(sender As Object, e As EventArgs) Handles But_FT05_S.Click
        Mid(TxD, 6, 1) = "S"
        Set_Seat_Position()
    End Sub
    '06:
    Private Sub But_FT06_R_Click(sender As Object, e As EventArgs) Handles But_FT06_R.Click
        FT(6) += Deg : If FT(6) >= 360 Then FT(6) = 0
        Mid(TxD, 7, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT06_L_Click(sender As Object, e As EventArgs) Handles But_FT06_L.Click
        FT(6) -= Deg : If FT(6) < 0 Then FT(6) = 360 + FT(6)
        Mid(TxD, 7, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT06_S_Click(sender As Object, e As EventArgs) Handles But_FT06_S.Click
        Mid(TxD, 7, 1) = "S"
        Set_Seat_Position()
    End Sub
    '07:
    Private Sub But_FT07_R_Click(sender As Object, e As EventArgs) Handles But_FT07_R.Click
        FT(7) += Deg : If FT(7) >= 360 Then FT(7) = 0
        Mid(TxD, 8, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT07_L_Click(sender As Object, e As EventArgs) Handles But_FT07_L.Click
        FT(7) -= Deg : If FT(7) < 0 Then FT(7) = 360 + FT(7)
        Mid(TxD, 8, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT07_S_Click(sender As Object, e As EventArgs) Handles But_FT07_S.Click
        Mid(TxD, 8, 1) = "S"
        Set_Seat_Position()
    End Sub
    '08:
    Private Sub But_FT08_R_Click(sender As Object, e As EventArgs) Handles But_FT08_R.Click
        FT(8) += Deg : If FT(8) >= 360 Then FT(8) = 0
        Mid(TxD, 9, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT08_L_Click(sender As Object, e As EventArgs) Handles But_FT08_L.Click
        FT(8) -= Deg : If FT(8) < 0 Then FT(8) = 360 + FT(8)
        Mid(TxD, 9, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT08_S_Click(sender As Object, e As EventArgs) Handles But_FT08_S.Click
        Mid(TxD, 9, 1) = "S"
        Set_Seat_Position()
    End Sub
    '09:
    Private Sub But_FT09_R_Click(sender As Object, e As EventArgs) Handles But_FT09_R.Click
        FT(9) += Deg : If FT(9) >= 360 Then FT(9) = 0
        Mid(TxD, 10, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT09_L_Click(sender As Object, e As EventArgs) Handles But_FT09_L.Click
        FT(9) -= Deg : If FT(9) < 0 Then FT(9) = 360 + FT(9)
        Mid(TxD, 10, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT09_S_Click(sender As Object, e As EventArgs) Handles But_FT09_S.Click
        Mid(TxD, 10, 1) = "S"
        Set_Seat_Position()
    End Sub
    '10:
    Private Sub But_FT10_R_Click(sender As Object, e As EventArgs) Handles But_FT10_R.Click
        FT(10) += Deg : If FT(10) >= 360 Then FT(10) = 0
        Mid(TxD, 11, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT10_L_Click(sender As Object, e As EventArgs) Handles But_FT10_L.Click
        FT(10) -= Deg : If FT(10) < 0 Then FT(10) = 360 + FT(10)
        Mid(TxD, 11, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT10_S_Click(sender As Object, e As EventArgs) Handles But_FT10_S.Click
        Mid(TxD, 11, 1) = "S"
        Set_Seat_Position()
    End Sub
    '11:
    Private Sub But_FT11_R_Click(sender As Object, e As EventArgs) Handles But_FT11_R.Click
        FT(11) += Deg : If FT(11) >= 360 Then FT(11) = 0
        Mid(TxD, 12, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT11_L_Click(sender As Object, e As EventArgs) Handles But_FT11_L.Click
        FT(11) -= Deg : If FT(11) < 0 Then FT(11) = 360 + FT(11)
        Mid(TxD, 12, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT11_S_Click(sender As Object, e As EventArgs) Handles But_FT11_S.Click
        Mid(TxD, 12, 1) = "S"
        Set_Seat_Position()
    End Sub
    '12:
    Private Sub But_FT12_R_Click(sender As Object, e As EventArgs) Handles But_FT12_R.Click
        FT(12) += Deg : If FT(12) >= 360 Then FT(12) = 0
        Mid(TxD, 13, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT12_L_Click(sender As Object, e As EventArgs) Handles But_FT12_L.Click
        FT(12) -= Deg : If FT(12) < 0 Then FT(12) = 360 + FT(12)
        Mid(TxD, 13, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT12_S_Click(sender As Object, e As EventArgs) Handles But_FT12_S.Click
        Mid(TxD, 13, 1) = "S"
        Set_Seat_Position()
    End Sub
    '13:
    Private Sub But_FT13_R_Click(sender As Object, e As EventArgs) Handles But_FT13_R.Click
        FT(13) += Deg : If FT(13) >= 360 Then FT(13) = 0
        Mid(TxD, 14, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT13_L_Click(sender As Object, e As EventArgs) Handles But_FT13_L.Click
        FT(13) -= Deg : If FT(13) < 0 Then FT(13) = 360 + FT(13)
        Mid(TxD, 14, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT13_S_Click(sender As Object, e As EventArgs) Handles But_FT13_S.Click
        Mid(TxD, 14, 1) = "S"
        Set_Seat_Position()
    End Sub
    '14:
    Private Sub But_FT14_R_Click(sender As Object, e As EventArgs) Handles But_FT14_R.Click
        FT(14) += Deg : If FT(14) >= 360 Then FT(14) = 0
        Mid(TxD, 15, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT14_L_Click(sender As Object, e As EventArgs) Handles But_FT14_L.Click
        FT(14) -= Deg : If FT(14) < 0 Then FT(14) = 360 + FT(14)
        Mid(TxD, 15, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT14_S_Click(sender As Object, e As EventArgs) Handles But_FT14_S.Click
        Mid(TxD, 15, 1) = "S"
        Set_Seat_Position()
    End Sub
    '15:
    Private Sub But_FT15_R_Click(sender As Object, e As EventArgs) Handles But_FT15_R.Click
        FT(15) += Deg : If FT(15) >= 360 Then FT(15) = 0
        Mid(TxD, 16, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT15_L_Click(sender As Object, e As EventArgs) Handles But_FT15_L.Click
        FT(15) -= Deg : If FT(15) < 0 Then FT(15) = 360 + FT(15)
        Mid(TxD, 16, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT15_S_Click(sender As Object, e As EventArgs) Handles But_FT15_S.Click
        Mid(TxD, 16, 1) = "S"
        Set_Seat_Position()
    End Sub
    '16:
    Private Sub But_FT16_R_Click(sender As Object, e As EventArgs) Handles But_FT16_R.Click
        FT(16) += Deg : If FT(16) >= 360 Then FT(16) = 0
        Mid(TxD, 17, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT16_L_Click(sender As Object, e As EventArgs) Handles But_FT16_L.Click
        FT(16) -= Deg : If FT(16) < 0 Then FT(16) = 360 + FT(16)
        Mid(TxD, 17, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT16_S_Click(sender As Object, e As EventArgs) Handles But_FT16_S.Click
        Mid(TxD, 17, 1) = "S"
        Set_Seat_Position()
    End Sub
    '17:
    Private Sub But_FT17_R_Click(sender As Object, e As EventArgs) Handles But_FT17_R.Click
        FT(17) += Deg : If FT(17) >= 360 Then FT(17) = 0
        Mid(TxD, 18, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT17_L_Click(sender As Object, e As EventArgs) Handles But_FT17_L.Click
        FT(17) -= Deg : If FT(17) < 0 Then FT(17) = 360 + FT(17)
        Mid(TxD, 18, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT17_S_Click(sender As Object, e As EventArgs) Handles But_FT17_S.Click
        Mid(TxD, 18, 1) = "S"
        Set_Seat_Position()
    End Sub
    '18:
    Private Sub But_FT18_R_Click(sender As Object, e As EventArgs) Handles But_FT18_R.Click
        FT(18) += Deg : If FT(18) >= 360 Then FT(18) = 0
        Mid(TxD, 19, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT18_L_Click(sender As Object, e As EventArgs) Handles But_FT18_L.Click
        FT(18) -= Deg : If FT(18) < 0 Then FT(18) = 360 + FT(18)
        Mid(TxD, 19, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT18_S_Click(sender As Object, e As EventArgs) Handles But_FT18_S.Click
        Mid(TxD, 19, 1) = "S"
        Set_Seat_Position()
    End Sub
    '19:
    Private Sub But_FT19_R_Click(sender As Object, e As EventArgs) Handles But_FT19_R.Click
        FT(19) += Deg : If FT(19) >= 360 Then FT(19) = 0
        Mid(TxD, 20, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT19_L_Click(sender As Object, e As EventArgs) Handles But_FT19_L.Click
        FT(19) -= Deg : If FT(19) < 0 Then FT(19) = 360 + FT(19)
        Mid(TxD, 20, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT19_S_Click(sender As Object, e As EventArgs) Handles But_FT19_S.Click
        Mid(TxD, 20, 1) = "S"
        Set_Seat_Position()
    End Sub
    '20:
    Private Sub But_FT20_R_Click(sender As Object, e As EventArgs) Handles But_FT20_R.Click
        FT(20) += Deg : If FT(20) >= 360 Then FT(20) = 0
        Mid(TxD, 21, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT20_L_Click(sender As Object, e As EventArgs) Handles But_FT20_L.Click
        FT(20) -= Deg : If FT(20) < 0 Then FT(20) = 360 + FT(20)
        Mid(TxD, 21, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT20_S_Click(sender As Object, e As EventArgs) Handles But_FT20_S.Click
        Mid(TxD, 21, 1) = "S"
        Set_Seat_Position()
    End Sub
    '21:
    Private Sub But_FT21_R_Click(sender As Object, e As EventArgs) Handles But_FT21_R.Click
        FT(21) += Deg : If FT(21) >= 360 Then FT(21) = 0
        Mid(TxD, 22, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT21_L_Click(sender As Object, e As EventArgs) Handles But_FT21_L.Click
        FT(21) -= Deg : If FT(21) < 0 Then FT(21) = 360 + FT(21)
        Mid(TxD, 22, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT21_S_Click(sender As Object, e As EventArgs) Handles But_FT21_S.Click
        Mid(TxD, 22, 1) = "S"
        Set_Seat_Position()
    End Sub
    '22:
    Private Sub But_FT22_R_Click(sender As Object, e As EventArgs) Handles But_FT22_R.Click
        FT(22) += Deg : If FT(22) >= 360 Then FT(22) = 0
        Mid(TxD, 23, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT22_L_Click(sender As Object, e As EventArgs) Handles But_FT22_L.Click
        FT(22) -= Deg : If FT(22) < 0 Then FT(22) = 360 + FT(22)
        Mid(TxD, 23, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT22_S_Click(sender As Object, e As EventArgs) Handles But_FT22_S.Click
        Mid(TxD, 23, 1) = "S"
        Set_Seat_Position()
    End Sub
    '23:
    Private Sub But_FT23_R_Click(sender As Object, e As EventArgs) Handles But_FT23_R.Click
        FT(23) += Deg : If FT(23) >= 360 Then FT(23) = 0
        Mid(TxD, 24, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT23_L_Click(sender As Object, e As EventArgs) Handles But_FT23_L.Click
        FT(23) -= Deg : If FT(23) < 0 Then FT(23) = 360 + FT(23)
        Mid(TxD, 24, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT23_S_Click(sender As Object, e As EventArgs) Handles But_FT23_S.Click
        Mid(TxD, 24, 1) = "S"
        Set_Seat_Position()
    End Sub
    '24:
    Private Sub But_FT24_R_Click(sender As Object, e As EventArgs) Handles But_FT24_R.Click
        FT(24) += Deg : If FT(24) >= 360 Then FT(24) = 0
        Mid(TxD, 25, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT24_L_Click(sender As Object, e As EventArgs) Handles But_FT24_L.Click
        FT(24) -= Deg : If FT(24) < 0 Then FT(24) = 360 + FT(24)
        Mid(TxD, 25, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT24_S_Click(sender As Object, e As EventArgs) Handles But_FT24_S.Click
        Mid(TxD, 25, 1) = "S"
        Set_Seat_Position()
    End Sub
    '25:
    Private Sub But_FT25_R_Click(sender As Object, e As EventArgs) Handles But_FT25_R.Click
        FT(25) += Deg : If FT(25) >= 360 Then FT(25) = 0
        Mid(TxD, 26, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT25_L_Click(sender As Object, e As EventArgs) Handles But_FT25_L.Click
        FT(25) -= Deg : If FT(25) < 0 Then FT(25) = 360 + FT(25)
        Mid(TxD, 26, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT25_S_Click(sender As Object, e As EventArgs) Handles But_FT25_S.Click
        Mid(TxD, 26, 1) = "S"
        Set_Seat_Position()
    End Sub
    '26:
    Private Sub But_FT26_R_Click(sender As Object, e As EventArgs) Handles But_FT26_R.Click
        FT(26) += Deg : If FT(26) >= 360 Then FT(26) = 0
        Mid(TxD, 27, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT26_L_Click(sender As Object, e As EventArgs) Handles But_FT26_L.Click
        FT(26) -= Deg : If FT(26) < 0 Then FT(26) = 360 + FT(26)
        Mid(TxD, 27, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT26_S_Click(sender As Object, e As EventArgs) Handles But_FT26_S.Click
        Mid(TxD, 27, 1) = "S"
        Set_Seat_Position()
    End Sub
    '27:
    Private Sub But_FT27_R_Click(sender As Object, e As EventArgs) Handles But_FT27_R.Click
        FT(27) += Deg : If FT(27) >= 360 Then FT(27) = 0
        Mid(TxD, 28, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT27_L_Click(sender As Object, e As EventArgs) Handles But_FT27_L.Click
        FT(27) -= Deg : If FT(27) < 0 Then FT(27) = 360 + FT(27)
        Mid(TxD, 28, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT27_S_Click(sender As Object, e As EventArgs) Handles But_FT27_S.Click
        Mid(TxD, 28, 1) = "S"
        Set_Seat_Position()
    End Sub
    '28:
    Private Sub But_FT28_R_Click(sender As Object, e As EventArgs) Handles But_FT28_R.Click
        FT(28) += Deg : If FT(28) >= 360 Then FT(28) = 0
        Mid(TxD, 29, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT28_L_Click(sender As Object, e As EventArgs) Handles But_FT28_L.Click
        FT(28) -= Deg : If FT(28) < 0 Then FT(28) = 360 + FT(28)
        Mid(TxD, 29, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT28_S_Click(sender As Object, e As EventArgs) Handles But_FT28_S.Click
        Mid(TxD, 29, 1) = "S"
        Set_Seat_Position()
    End Sub
    '29:
    Private Sub But_FT29_R_Click(sender As Object, e As EventArgs) Handles But_FT29_R.Click
        FT(29) += Deg : If FT(29) >= 360 Then FT(29) = 0
        Mid(TxD, 30, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT29_L_Click(sender As Object, e As EventArgs) Handles But_FT29_L.Click
        FT(29) -= Deg : If FT(29) < 0 Then FT(29) = 360 + FT(29)
        Mid(TxD, 30, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT29_S_Click(sender As Object, e As EventArgs) Handles But_FT29_S.Click
        Mid(TxD, 30, 1) = "S"
        Set_Seat_Position()
    End Sub
    '30:
    Private Sub But_FT30_R_Click(sender As Object, e As EventArgs) Handles But_FT30_R.Click
        FT(30) += Deg : If FT(30) >= 360 Then FT(30) = 0
        Mid(TxD, 31, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT30_L_Click(sender As Object, e As EventArgs) Handles But_FT30_L.Click
        FT(30) -= Deg : If FT(30) < 0 Then FT(30) = 360 + FT(30)
        Mid(TxD, 31, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT30_S_Click(sender As Object, e As EventArgs) Handles But_FT30_S.Click
        Mid(TxD, 31, 1) = "S"
        Set_Seat_Position()
    End Sub
    '31:
    Private Sub But_FT31_R_Click(sender As Object, e As EventArgs) Handles But_FT31_R.Click
        FT(31) += Deg : If FT(31) >= 360 Then FT(31) = 0
        Mid(TxD, 32, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT31_L_Click(sender As Object, e As EventArgs) Handles But_FT31_L.Click
        FT(31) -= Deg : If FT(31) < 0 Then FT(31) = 360 + FT(31)
        Mid(TxD, 32, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT31_S_Click(sender As Object, e As EventArgs) Handles But_FT31_S.Click
        Mid(TxD, 32, 1) = "S"
        Set_Seat_Position()
    End Sub
    '32:
    Private Sub But_FT32_R_Click(sender As Object, e As EventArgs) Handles But_FT32_R.Click
        FT(32) += Deg : If FT(32) >= 360 Then FT(32) = 0
        Mid(TxD, 33, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT32_L_Click(sender As Object, e As EventArgs) Handles But_FT32_L.Click
        FT(32) -= Deg : If FT(32) < 0 Then FT(32) = 360 + FT(32)
        Mid(TxD, 33, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT32_S_Click(sender As Object, e As EventArgs) Handles But_FT32_S.Click
        Mid(TxD, 33, 1) = "S"
        Set_Seat_Position()
    End Sub
    '33:
    Private Sub But_FT33_R_Click(sender As Object, e As EventArgs) Handles But_FT33_R.Click
        FT(33) += Deg : If FT(33) >= 360 Then FT(33) = 0
        Mid(TxD, 34, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT33_L_Click(sender As Object, e As EventArgs) Handles But_FT33_L.Click
        FT(33) -= Deg : If FT(33) < 0 Then FT(33) = 360 + FT(33)
        Mid(TxD, 34, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT33_S_Click(sender As Object, e As EventArgs) Handles But_FT33_S.Click
        Mid(TxD, 34, 1) = "S"
        Set_Seat_Position()
    End Sub
    '34:
    Private Sub But_FT34_R_Click(sender As Object, e As EventArgs) Handles But_FT34_R.Click
        FT(34) += Deg : If FT(34) >= 360 Then FT(34) = 0
        Mid(TxD, 35, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT34_L_Click(sender As Object, e As EventArgs) Handles But_FT34_L.Click
        FT(34) -= Deg : If FT(34) < 0 Then FT(34) = 360 + FT(34)
        Mid(TxD, 35, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT34_S_Click(sender As Object, e As EventArgs) Handles But_FT34_S.Click
        Mid(TxD, 35, 1) = "S"
        Set_Seat_Position()
    End Sub
    '35:
    Private Sub But_FT35_R_Click(sender As Object, e As EventArgs) Handles But_FT35_R.Click
        FT(35) += Deg : If FT(35) >= 360 Then FT(35) = 0
        Mid(TxD, 36, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT35_L_Click(sender As Object, e As EventArgs) Handles But_FT35_L.Click
        FT(35) -= Deg : If FT(35) < 0 Then FT(35) = 360 + FT(35)
        Mid(TxD, 36, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT35_S_Click(sender As Object, e As EventArgs) Handles But_FT35_S.Click
        Mid(TxD, 36, 1) = "S"
        Set_Seat_Position()
    End Sub
    '36:
    Private Sub But_FT36_R_Click(sender As Object, e As EventArgs) Handles But_FT36_R.Click
        FT(36) += Deg : If FT(36) >= 360 Then FT(36) = 0
        Mid(TxD, 37, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT36_L_Click(sender As Object, e As EventArgs) Handles But_FT36_L.Click
        FT(36) -= Deg : If FT(36) < 0 Then FT(36) = 360 + FT(36)
        Mid(TxD, 37, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT36_S_Click(sender As Object, e As EventArgs) Handles But_FT36_S.Click
        Mid(TxD, 37, 1) = "S"
        Set_Seat_Position()
    End Sub
    '37:
    Private Sub But_FT37_R_Click(sender As Object, e As EventArgs) Handles But_FT37_R.Click
        FT(37) += Deg : If FT(37) >= 360 Then FT(37) = 0
        Mid(TxD, 38, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT37_L_Click(sender As Object, e As EventArgs) Handles But_FT37_L.Click
        FT(37) -= Deg : If FT(37) < 0 Then FT(37) = 360 + FT(37)
        Mid(TxD, 38, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT37_S_Click(sender As Object, e As EventArgs) Handles But_FT37_S.Click
        Mid(TxD, 38, 1) = "S"
        Set_Seat_Position()
    End Sub
    '38:
    Private Sub But_FT38_R_Click(sender As Object, e As EventArgs) Handles But_FT38_R.Click
        FT(38) += Deg : If FT(38) >= 360 Then FT(38) = 0
        Mid(TxD, 39, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT38_L_Click(sender As Object, e As EventArgs) Handles But_FT38_L.Click
        FT(38) -= Deg : If FT(38) < 0 Then FT(38) = 360 + FT(38)
        Mid(TxD, 39, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT38_S_Click(sender As Object, e As EventArgs) Handles But_FT38_S.Click
        Mid(TxD, 39, 1) = "S"
        Set_Seat_Position()
    End Sub
    '39:
    Private Sub But_FT39_R_Click(sender As Object, e As EventArgs) Handles But_FT39_R.Click
        FT(39) += Deg : If FT(39) >= 360 Then FT(39) = 0
        Mid(TxD, 40, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT39_L_Click(sender As Object, e As EventArgs) Handles But_FT39_L.Click
        FT(39) -= Deg : If FT(39) < 0 Then FT(39) = 360 + FT(39)
        Mid(TxD, 40, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT39_S_Click(sender As Object, e As EventArgs) Handles But_FT39_S.Click
        Mid(TxD, 40, 1) = "S"
        Set_Seat_Position()
    End Sub
    '40:
    Private Sub But_FT40_R_Click(sender As Object, e As EventArgs) Handles But_FT40_R.Click
        FT(40) += Deg : If FT(40) >= 360 Then FT(40) = 0
        Mid(TxD, 41, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT40_L_Click(sender As Object, e As EventArgs) Handles But_FT40_L.Click
        FT(40) -= Deg : If FT(40) < 0 Then FT(40) = 360 + FT(40)
        Mid(TxD, 41, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT40_S_Click(sender As Object, e As EventArgs) Handles But_FT40_S.Click
        Mid(TxD, 41, 1) = "S"
        Set_Seat_Position()
    End Sub
    '41:
    Private Sub But_FT41_R_Click(sender As Object, e As EventArgs) Handles But_FT41_R.Click
        FT(41) += Deg : If FT(41) >= 360 Then FT(41) = 0
        Mid(TxD, 42, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT41_L_Click(sender As Object, e As EventArgs) Handles But_FT41_L.Click
        FT(41) -= Deg : If FT(41) < 0 Then FT(41) = 360 + FT(41)
        Mid(TxD, 42, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT41_S_Click(sender As Object, e As EventArgs) Handles But_FT41_S.Click
        Mid(TxD, 42, 1) = "S"
        Set_Seat_Position()
    End Sub
    '42:
    Private Sub But_FT42_R_Click(sender As Object, e As EventArgs) Handles But_FT42_R.Click
        FT(42) += Deg : If FT(42) >= 360 Then FT(42) = 0
        Mid(TxD, 43, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT42_L_Click(sender As Object, e As EventArgs) Handles But_FT42_L.Click
        FT(42) -= Deg : If FT(42) < 0 Then FT(42) = 360 + FT(42)
        Mid(TxD, 43, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT42_S_Click(sender As Object, e As EventArgs) Handles But_FT42_S.Click
        Mid(TxD, 43, 1) = "S"
        Set_Seat_Position()
    End Sub
    '43:
    Private Sub But_FT43_R_Click(sender As Object, e As EventArgs) Handles But_FT43_R.Click
        FT(43) += Deg : If FT(43) >= 360 Then FT(43) = 0
        Mid(TxD, 44, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT43_L_Click(sender As Object, e As EventArgs) Handles But_FT43_L.Click
        FT(43) -= Deg : If FT(43) < 0 Then FT(43) = 360 + FT(43)
        Mid(TxD, 44, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT43_S_Click(sender As Object, e As EventArgs) Handles But_FT43_S.Click
        Mid(TxD, 44, 1) = "S"
        Set_Seat_Position()
    End Sub
    '44:
    Private Sub But_FT44_R_Click(sender As Object, e As EventArgs) Handles But_FT44_R.Click
        FT(44) += Deg : If FT(44) >= 360 Then FT(44) = 0
        Mid(TxD, 45, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT44_L_Click(sender As Object, e As EventArgs) Handles But_FT44_L.Click
        FT(44) -= Deg : If FT(44) < 0 Then FT(44) = 360 + FT(44)
        Mid(TxD, 45, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT44_S_Click(sender As Object, e As EventArgs) Handles But_FT44_S.Click
        Mid(TxD, 45, 1) = "S"
        Set_Seat_Position()
    End Sub
    '45:
    Private Sub But_FT45_R_Click(sender As Object, e As EventArgs) Handles But_FT45_R.Click
        FT(45) += Deg : If FT(45) >= 360 Then FT(45) = 0
        Mid(TxD, 46, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT45_L_Click(sender As Object, e As EventArgs) Handles But_FT45_L.Click
        FT(45) -= Deg : If FT(45) < 0 Then FT(45) = 360 + FT(45)
        Mid(TxD, 46, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT45_S_Click(sender As Object, e As EventArgs) Handles But_FT45_S.Click
        Mid(TxD, 46, 1) = "S"
        Set_Seat_Position()
    End Sub
    '46:
    Private Sub But_FT46_R_Click(sender As Object, e As EventArgs) Handles But_FT46_R.Click
        FT(46) += Deg : If FT(46) >= 360 Then FT(46) = 0
        Mid(TxD, 47, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT46_L_Click(sender As Object, e As EventArgs) Handles But_FT46_L.Click
        FT(46) -= Deg : If FT(46) < 0 Then FT(46) = 360 + FT(46)
        Mid(TxD, 47, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT46_S_Click(sender As Object, e As EventArgs) Handles But_FT46_S.Click
        Mid(TxD, 47, 1) = "S"
        Set_Seat_Position()
    End Sub
    '47:
    Private Sub But_FT47_R_Click(sender As Object, e As EventArgs) Handles But_FT47_R.Click
        FT(47) += Deg : If FT(47) >= 360 Then FT(47) = 0
        Mid(TxD, 48, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT47_L_Click(sender As Object, e As EventArgs) Handles But_FT47_L.Click
        FT(47) -= Deg : If FT(47) < 0 Then FT(47) = 360 + FT(47)
        Mid(TxD, 48, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT47_S_Click(sender As Object, e As EventArgs) Handles But_FT47_S.Click
        Mid(TxD, 48, 1) = "S"
        Set_Seat_Position()
    End Sub
    '48:
    Private Sub But_FT48_R_Click(sender As Object, e As EventArgs) Handles But_FT48_R.Click
        FT(48) += Deg : If FT(48) >= 360 Then FT(48) = 0
        Mid(TxD, 49, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT48_L_Click(sender As Object, e As EventArgs) Handles But_FT48_L.Click
        FT(48) -= Deg : If FT(48) < 0 Then FT(48) = 360 + FT(48)
        Mid(TxD, 49, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT48_S_Click(sender As Object, e As EventArgs) Handles But_FT48_S.Click
        Mid(TxD, 49, 1) = "S"
        Set_Seat_Position()
    End Sub
    '49:
    Private Sub But_FT49_R_Click(sender As Object, e As EventArgs) Handles But_FT49_R.Click
        FT(49) += Deg : If FT(49) >= 360 Then FT(49) = 0
        Mid(TxD, 50, 1) = "R"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT49_L_Click(sender As Object, e As EventArgs) Handles But_FT49_L.Click
        FT(49) -= Deg : If FT(49) < 0 Then FT(49) = 360 + FT(49)
        Mid(TxD, 50, 1) = "L"
        Set_Seat_Position()
    End Sub
    Private Sub But_FT49_S_Click(sender As Object, e As EventArgs) Handles But_FT49_S.Click
        Mid(TxD, 50, 1) = "S"
        Set_Seat_Position()
    End Sub

#End Region

#Region "DMX"

    '----------------------------------------------------------------------------------------------
    '------                                       DMX                                        ------
    '----------------------------------------------------------------------------------------------
    Private Declare Sub StartDevice Lib "k8062d.dll" ()
    Private Declare Sub SetData Lib "k8062d.dll" (ByVal Channel As Integer, ByVal Data As Integer)
    Private Declare Sub SetChannelCount Lib "k8062d.dll" (ByVal Count As Integer)
    Private Declare Sub StopDevice Lib "k8062d.dll" ()

    Private Sub DMX_init()
        Lbl_DMX_Info.Text = "DMX ENABLE"
        StartDevice()
    End Sub

    'A:
    Private Sub Trk_DMX_A_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_A_Bubble.Scroll
        Chk_A_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_A_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_A_Bubble.CheckedChanged
        Chk_A_Bubble_Set()
    End Sub
    Private Sub Chk_A_Bubble_Set()
        If Chk_DMX_A_Bubble.Checked = False Then Trk_DMX_A_Bubble.Value = 0
        Lbl_DMX_Val_A_Bubble.Text = Trk_DMX_A_Bubble.Value.ToString ': SetData(1, Trk_DMX_A_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_A_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_A_Smoke.Scroll
        Chk_A_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_A_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_A_Smoke.CheckedChanged
        Chk_A_Smoke_Set()
    End Sub
    Private Sub Chk_A_Smoke_Set()
        If Chk_DMX_A_Smoke.Checked = False Then Trk_DMX_A_Smoke.Value = 0
        Lbl_DMX_Val_A_Smoke.Text = Trk_DMX_A_Smoke.Value.ToString ': SetData(1, Trk_DMX_A_Smoke.Value)
    End Sub
    'B:
    Private Sub Trk_DMX_B_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_B_Bubble.Scroll
        Chk_B_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_B_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_B_Bubble.CheckedChanged
        Chk_B_Bubble_Set()
    End Sub
    Private Sub Chk_B_Bubble_Set()
        If Chk_DMX_B_Bubble.Checked = False Then Trk_DMX_B_Bubble.Value = 0
        Lbl_DMX_Val_B_Bubble.Text = Trk_DMX_B_Bubble.Value.ToString ': SetData(1, Trk_DMX_B_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_B_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_B_Smoke.Scroll
        Chk_B_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_B_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_B_Smoke.CheckedChanged
        Chk_B_Smoke_Set()
    End Sub
    Private Sub Chk_B_Smoke_Set()
        If Chk_DMX_B_Smoke.Checked = False Then Trk_DMX_B_Smoke.Value = 0
        Lbl_DMX_Val_B_Smoke.Text = Trk_DMX_B_Smoke.Value.ToString ': SetData(1, Trk_DMX_B_Smoke.Value)
    End Sub
    'C:
    Private Sub Trk_DMX_C_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_C_Bubble.Scroll
        Chk_C_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_C_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_C_Bubble.CheckedChanged
        Chk_C_Bubble_Set()
    End Sub
    Private Sub Chk_C_Bubble_Set()
        If Chk_DMX_C_Bubble.Checked = False Then Trk_DMX_C_Bubble.Value = 0
        Lbl_DMX_Val_C_Bubble.Text = Trk_DMX_C_Bubble.Value.ToString ': SetData(1, Trk_DMX_C_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_C_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_C_Smoke.Scroll
        Chk_C_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_C_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_C_Smoke.CheckedChanged
        Chk_C_Smoke_Set()
    End Sub
    Private Sub Chk_C_Smoke_Set()
        If Chk_DMX_C_Smoke.Checked = False Then Trk_DMX_C_Smoke.Value = 0
        Lbl_DMX_Val_C_Smoke.Text = Trk_DMX_C_Smoke.Value.ToString ': SetData(1, Trk_DMX_C_Smoke.Value)
    End Sub
    'D:
    Private Sub Trk_DMX_D_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_D_Bubble.Scroll
        Chk_D_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_D_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_D_Bubble.CheckedChanged
        Chk_D_Bubble_Set()
    End Sub
    Private Sub Chk_D_Bubble_Set()
        If Chk_DMX_D_Bubble.Checked = False Then Trk_DMX_D_Bubble.Value = 0
        Lbl_DMX_Val_D_Bubble.Text = Trk_DMX_D_Bubble.Value.ToString ': SetData(1, Trk_DMX_D_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_D_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_D_Smoke.Scroll
        Chk_D_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_D_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_D_Smoke.CheckedChanged
        Chk_D_Smoke_Set()
    End Sub
    Private Sub Chk_D_Smoke_Set()
        If Chk_DMX_D_Smoke.Checked = False Then Trk_DMX_D_Smoke.Value = 0
        Lbl_DMX_Val_D_Smoke.Text = Trk_DMX_D_Smoke.Value.ToString ': SetData(1, Trk_DMX_D_Smoke.Value)
    End Sub
    'E:
    Private Sub Trk_DMX_E_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_E_Bubble.Scroll
        Chk_E_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_E_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_E_Bubble.CheckedChanged
        Chk_E_Bubble_Set()
    End Sub
    Private Sub Chk_E_Bubble_Set()
        If Chk_DMX_E_Bubble.Checked = False Then Trk_DMX_E_Bubble.Value = 0
        Lbl_DMX_Val_E_Bubble.Text = Trk_DMX_E_Bubble.Value.ToString ': SetData(1, Trk_DMX_E_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_E_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_E_Smoke.Scroll
        Chk_E_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_E_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_E_Smoke.CheckedChanged
        Chk_E_Smoke_Set()
    End Sub
    Private Sub Chk_E_Smoke_Set()
        If Chk_DMX_E_Smoke.Checked = False Then Trk_DMX_E_Smoke.Value = 0
        Lbl_DMX_Val_E_Smoke.Text = Trk_DMX_E_Smoke.Value.ToString ': SetData(1, Trk_DMX_E_Smoke.Value)
    End Sub
    'F:
    Private Sub Trk_DMX_F_Bubble_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_F_Bubble.Scroll
        Chk_F_Bubble_Set()
    End Sub
    Private Sub Chk_DMX_F_Bubble_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_F_Bubble.CheckedChanged
        Chk_F_Bubble_Set()
    End Sub
    Private Sub Chk_F_Bubble_Set()
        If Chk_DMX_F_Bubble.Checked = False Then Trk_DMX_F_Bubble.Value = 0
        Lbl_DMX_Val_F_Bubble.Text = Trk_DMX_F_Bubble.Value.ToString ': SetData(1, Trk_DMX_F_Bubble.Value)
    End Sub
    Private Sub Trk_DMX_F_Smoke_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_F_Smoke.Scroll
        Chk_F_Smoke_Set()
    End Sub
    Private Sub Chk_DMX_F_Smoke_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_F_Smoke.CheckedChanged
        Chk_F_Smoke_Set()
    End Sub
    Private Sub Chk_F_Smoke_Set()
        If Chk_DMX_F_Smoke.Checked = False Then Trk_DMX_F_Smoke.Value = 0
        Lbl_DMX_Val_F_Smoke.Text = Trk_DMX_F_Smoke.Value.ToString ': SetData(1, Trk_DMX_F_Smoke.Value)
    End Sub
    'Wind:
    Private Sub Trk_DMX_Wind60_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_Wind60.Scroll
        Chk_Wind60_Set()
    End Sub
    Private Sub Chk_DMX_Wind60_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_Wind60.CheckedChanged
        Chk_Wind60_Set()
    End Sub
    Private Sub Chk_Wind60_Set()
        If Chk_DMX_Wind60.Checked = False Then Trk_DMX_Wind60.Value = 0
        Lbl_DMX_Val_Wind60.Text = Trk_DMX_Wind60.Value.ToString : SetData(1, Trk_DMX_Wind60.Value)
    End Sub
    Private Sub Trk_DMX_Wind180_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_Wind180.Scroll
        Chk_Wind180_Set()
    End Sub
    Private Sub Chk_DMX_Wind180_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_Wind180.CheckedChanged
        Chk_Wind180_Set()
    End Sub
    Private Sub Chk_Wind180_Set()
        If Chk_DMX_Wind180.Checked = False Then Trk_DMX_Wind180.Value = 0
        Lbl_DMX_Val_Wind180.Text = Trk_DMX_Wind180.Value.ToString : SetData(2, Trk_DMX_Wind180.Value)
    End Sub
    Private Sub Trk_DMX_Wind300_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_Wind300.Scroll
        Chk_Wind300_Set()
    End Sub
    Private Sub Chk_DMX_Wind300_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_Wind300.CheckedChanged
        Chk_Wind300_Set()
    End Sub
    Private Sub Chk_Wind300_Set()
        If Chk_DMX_Wind300.Checked = False Then Trk_DMX_Wind300.Value = 0
        Lbl_DMX_Val_Wind300.Text = Trk_DMX_Wind300.Value.ToString : SetData(3, Trk_DMX_Wind300.Value)
    End Sub
    'Thunder:
    Private Sub Trk_DMX_Thunder_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Trk_DMX_Thunder.Scroll
        Chk_Thunder_Set()
    End Sub
    Private Sub Chk_DMX_Thunder_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_DMX_Thunder.CheckedChanged
        Chk_Thunder_Set()
    End Sub
    Private Sub Chk_Thunder_Set()
        If Chk_DMX_Thunder.Checked = False Then Trk_DMX_Thunder.Value = 0
        Lbl_DMX_Val_Thunder.Text = Trk_DMX_Thunder.Value.ToString : SetData(5, Trk_DMX_Thunder.Value)
        'SetData(4, 255)
        'SetData(5, 255)
        'SetData(6, 255)
        'SetData(7, 255)
        'SetData(8, 255)
        Lbl_DMX_Val_Thunder.Text = Trk_DMX_Thunder.Value.ToString : SetData(6, Trk_DMX_Thunder.Value)
        'SetData(10, 0)
        'SetData(11, 0)
        'SetData(12, 0)
        'SetData(13, 0)
        'SetData(14, 0)
        'SetData(15, 0)
        'SetData(16, 0)
        'SetData(17, 0)
        'SetData(18, 0)
        'SetData(19, 0)
        'SetData(20, 0)
        'SetData(21, 0)
        'SetData(22, 0)
        'SetData(23, 0)
        'SetData(24, 0)
        'SetData(25, 0)
        'SetData(26, 0)
        'SetData(27, 0)
        'SetData(28, 0)
        'SetData(29, 0)
        'SetData(30, 0)
        'SetData(31, 0)
        'SetData(32, 0)
    End Sub


#End Region

#Region "Obsługa Show"

    'Obsługa klawiatury:
    '01
    Private Sub But_01_Click(sender As Object, e As EventArgs) Handles But_01.Click
        Start_Show_01()
    End Sub
    Private Sub Start_Show_01()
        Curr_Show = 1 : Start_Show()
    End Sub
    '02
    Private Sub But_02_Click(sender As Object, e As EventArgs) Handles But_02.Click
        Start_Show_02()
    End Sub
    Private Sub Start_Show_02()
        Curr_Show = 2 : Start_Show()
    End Sub
    '03
    Private Sub But_03_Click(sender As Object, e As EventArgs) Handles But_03.Click
        Start_Show_03()
    End Sub
    Private Sub Start_Show_03()
        Curr_Show = 3 : Start_Show()
    End Sub
    '04
    Private Sub But_04_Click(sender As Object, e As EventArgs) Handles But_04.Click
        Start_Show_04()
    End Sub
    Private Sub Start_Show_04()
        Curr_Show = 4 : Start_Show()
    End Sub
    '05
    Private Sub But_05_Click(sender As Object, e As EventArgs) Handles But_05.Click
        Start_Show_05()
    End Sub
    Private Sub Start_Show_05()
        Curr_Show = 5 : Start_Show()
    End Sub
    '06
    Private Sub But_06_Click(sender As Object, e As EventArgs) Handles But_06.Click
        Start_Show_06()
    End Sub
    Private Sub Start_Show_06()
        Curr_Show = 6 : Start_Show()
    End Sub
    '07
    Private Sub But_07_Click(sender As Object, e As EventArgs) Handles But_07.Click
        Start_Show_07()
    End Sub
    Private Sub Start_Show_07()
        Curr_Show = 7 : Start_Show()
    End Sub
    '08
    Private Sub But_08_Click(sender As Object, e As EventArgs) Handles But_08.Click
        Start_Show_08()
    End Sub
    Private Sub Start_Show_08()
        Curr_Show = 8 : Start_Show()
    End Sub
    '09
    Private Sub But_09_Click(sender As Object, e As EventArgs) Handles But_09.Click
        Start_Show_09()
    End Sub
    Private Sub Start_Show_09()
        Curr_Show = 9 : Start_Show()
    End Sub
    '10
    Private Sub But_10_Click(sender As Object, e As EventArgs) Handles But_10.Click
        Start_Show_10()
    End Sub
    Private Sub Start_Show_10()
        Curr_Show = 10 : Start_Show()
    End Sub
    '11
    Private Sub But_11_Click(sender As Object, e As EventArgs) Handles But_11.Click
        Start_Show_11()
    End Sub
    Private Sub Start_Show_11()
        Curr_Show = 11 : Start_Show()
    End Sub
    '12
    Private Sub But_12_Click(sender As Object, e As EventArgs) Handles But_12.Click
        Start_Show_12()
    End Sub
    Private Sub Start_Show_12()
        Curr_Show = 12 : Start_Show()
    End Sub
    '13
    Private Sub But_13_Click(sender As Object, e As EventArgs) Handles But_13.Click
        Start_Show_13()
    End Sub
    Private Sub Start_Show_13()
        Curr_Show = 13 : Start_Show()
    End Sub
    '14
    Private Sub But_14_Click(sender As Object, e As EventArgs) Handles But_14.Click
        Start_Show_14()
    End Sub
    Private Sub Start_Show_14()
        Curr_Show = 14 : Start_Show()
    End Sub
    '15
    Private Sub But_15_Click(sender As Object, e As EventArgs) Handles But_15.Click
        Start_Show_15()
    End Sub
    Private Sub Start_Show_15()
        Curr_Show = 15 : Start_Show()
    End Sub
    '16
    Private Sub But_16_Click(sender As Object, e As EventArgs) Handles But_16.Click
        Start_Show_16()
    End Sub
    Private Sub Start_Show_16()
        Curr_Show = 16 : Start_Show()
    End Sub
    '17
    Private Sub But_17_Click(sender As Object, e As EventArgs) Handles But_17.Click
        Start_Show_17()
    End Sub
    Private Sub Start_Show_17()
        Curr_Show = 17 : Start_Show()
    End Sub
    '18
    Private Sub But_18_Click(sender As Object, e As EventArgs) Handles But_18.Click
        Start_Show_18()
    End Sub
    Private Sub Start_Show_18()
        Curr_Show = 18 : Start_Show()
    End Sub
    '19
    Private Sub But_19_Click(sender As Object, e As EventArgs) Handles But_19.Click
        Start_Show_19()
    End Sub
    Private Sub Start_Show_19()
        Curr_Show = 19 : Start_Show()
    End Sub
    '20
    Private Sub But_20_Click(sender As Object, e As EventArgs) Handles But_20.Click
        Start_Show_20()
    End Sub
    Private Sub Start_Show_20()
        Curr_Show = 20 : Start_Show()
    End Sub
    '21
    Private Sub But_21_Click(sender As Object, e As EventArgs) Handles But_21.Click
        Start_Show_21()
    End Sub
    Private Sub Start_Show_21()
        Curr_Show = 21 : Start_Show()
    End Sub
    '22
    Private Sub But_22_Click(sender As Object, e As EventArgs) Handles But_22.Click
        Start_Show_22()
    End Sub
    Private Sub Start_Show_22()
        Curr_Show = 22 : Start_Show()
    End Sub
    '23
    Private Sub But_23_Click(sender As Object, e As EventArgs) Handles But_23.Click
        Start_Show_23()
    End Sub
    Private Sub Start_Show_23()
        Curr_Show = 23 : Start_Show()
    End Sub
    '24
    Private Sub But_24_Click(sender As Object, e As EventArgs) Handles But_24.Click
        Start_Show_24()
    End Sub
    Private Sub Start_Show_24()
        Curr_Show = 24 : Start_Show()
    End Sub
    '25
    Private Sub But_25_Click(sender As Object, e As EventArgs) Handles But_25.Click
        Start_Show_25()
    End Sub
    Private Sub Start_Show_25()
        Curr_Show = 25 : Start_Show()
    End Sub
    '26
    Private Sub But_26_Click(sender As Object, e As EventArgs) Handles But_26.Click
        Start_Show_26()
    End Sub
    Private Sub Start_Show_26()
        Curr_Show = 26 : Start_Show()
    End Sub
    '27
    Private Sub But_27_Click(sender As Object, e As EventArgs) Handles But_27.Click
        Start_Show_27()
    End Sub
    Private Sub Start_Show_27()
        Curr_Show = 27 : Start_Show()
    End Sub
    '28
    Private Sub But_28_Click(sender As Object, e As EventArgs) Handles But_28.Click
        Start_Show_28()
    End Sub
    Private Sub Start_Show_28()
        Curr_Show = 28 : Start_Show()
    End Sub
    '29
    Private Sub But_29_Click(sender As Object, e As EventArgs) Handles But_29.Click
        Start_Show_29()
    End Sub
    Private Sub Start_Show_29()
        Curr_Show = 29 : Start_Show()
    End Sub
    '30
    Private Sub But_30_Click(sender As Object, e As EventArgs) Handles But_30.Click
        Start_Show_30()
    End Sub
    Private Sub Start_Show_30()
        Curr_Show = 30 : Start_Show()
    End Sub
    '31
    Private Sub But_31_Click(sender As Object, e As EventArgs) Handles But_31.Click
        Start_Show_31()
    End Sub
    Private Sub Start_Show_31()
        Curr_Show = 31 : Start_Show()
    End Sub
    '32
    Private Sub But_32_Click(sender As Object, e As EventArgs) Handles But_32.Click
        Start_Show_32()
    End Sub
    Private Sub Start_Show_32()
        Curr_Show = 32 : Start_Show()
    End Sub
    '33
    Private Sub But_33_Click(sender As Object, e As EventArgs) Handles But_33.Click
        Start_Show_33()
    End Sub
    Private Sub Start_Show_33()
        Curr_Show = 33 : Start_Show()
    End Sub

    Private Sub But_Stop_Click(sender As Object, e As EventArgs) Handles But_Stop.Click
        End_All_Process()
    End Sub
    Private Sub End_All_Process()
        Stop_Process()
    End Sub

    'Czytanie klawiszy funkcyjnych:
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Select Case keyData
            Case Keys.F1
                Start_Fun_Key = True : Start_Show_01()
            Case Keys.F2
                Start_Fun_Key = True : Start_Show_02()
            Case Keys.F3
                Start_Fun_Key = True : Start_Show_03()
            Case Keys.F4
                Start_Fun_Key = True : Start_Show_04()
            Case Keys.F5
                Start_Fun_Key = True : Start_Show_05()
            Case Keys.F6
                Start_Fun_Key = True : Start_Show_06()
            Case Keys.F7
                Start_Fun_Key = True : Start_Show_07()
            Case Keys.F8
                Start_Fun_Key = True : Start_Show_08()
            Case Keys.F9
                Start_Fun_Key = True : Start_Show_09()
            Case Keys.F10
                Start_Fun_Key = True : Start_Show_10()
            Case Keys.F11
                Start_Fun_Key = True : Start_Show_11()
            Case Keys.F12
                Start_Fun_Key = True : Start_Show_12()

            Case Keys.Escape
                End_All_Process()

            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)

        End Select

        Return True
    End Function
#End Region

#Region "Menu"
    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        Frm_Edit.Show()
    End Sub
    Private Sub HideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideToolStripMenuItem.Click
        Frm_Edit.Hide()
    End Sub
    'SAVE
    Private Sub SaveDMXScriptAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveDMXScriptAsToolStripMenuItem.Click
        SaveFileDialog1.Filter = "Text files (*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            FileOpen(1, SaveFileDialog1.FileName, OpenMode.Output)
            Dim x_save As Integer
            For x_save = 0 To (Frm_Edit.Lst_DMX.Items.Count - 1)
                Frm_Edit.Lst_DMX.SetSelected(x_save, True)
                PrintLine(1, Frm_Edit.Lst_DMX.SelectedItem)
            Next x_save
        End If
        FileClose(1)
    End Sub

#End Region


#Region "Play"
    Private Sub Start_Show()
        If Start_Fun_Key = True Then
            Start_Fun_Key = False
            'Lbl_Panel_Command.Text = "ok04"
            Play_Process()
        Else
            Dialog_Play.Show()
        End If
    End Sub
#End Region

#Region "Koniec Programu"
    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End_Program()
    End Sub

    Private Sub End_Program()
        If DMX_Enable = True Then StopDevice
        SetData(1, 0)
        SetData(2, 0)
        SetData(3, 0)
        SetData(4, 0)
        SetData(5, 0)
        SetData(6, 0)

        End_All_Process()
        Ser_MODBUS.Close()
        Try
            receivingUDPclient.Close()
        Catch ex As Exception
        End Try
        Try
            Panel_receivingUDPclient.Close()
        Catch ex As Exception
        End Try

    End Sub
#End Region

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End_Program()
        Me.Close()
    End Sub









    '-- Test ---
    'https://support.industry.siemens.com/tf/WW/de/posts/sinamics-v20-modbus-c-oder-c-arduino/133390?page=0&pageSize=10
    'Folgende Parameter mußt Du im V20 einstellen..
    'P0003 Anwender-Zugriffsstufe = 3 damit Dir alle angezeigt werden.
    'P0700 Auswahl der Befehlsquelle   = 5: USS/MODBUS an RS485 
    'P1000 Auswahl des Frequenzsollwertes = 5: USS/MODBUS an RS485
    'P2010[0] USS/MODBUS-Baudrate = 6: 9600 bit/s (Werkseinstellung) 
    'P2014[0] USS/MODBUS-Telegramm-Auszeit = 1000 ms
    'P2021 Modbus-Adresse = 1 oder was Du willst
    'P2022 Zeitüberschreitung für Modbus-Antwort = 1000 ms

    'Zum Umrichter schreiben 
    '40100  Steuerwort
    '40101  Sollwert  hier sind 16384 (Hex 4000) = 100%


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ModBus_TxD()
        Exit Sub

        'Ax1_Command(0) = &H1 'Adres napędu OK !!!!!!!!!!!!!!!!!!!!
        'Ax1_Command(1) = &H3 'Komenda 06H - zapis jednego bajtu
        'Ax1_Command(2) = &H0 'Adres danych
        'Ax1_Command(3) = &H1 'Adres danych
        'Ax1_Command(4) = &H0 'Zawartość danych
        'Ax1_Command(5) = &H1 'Zawartość danych

        'Ax1_Command(0) = &H1 ' 'Adres napędu
        'Ax1_Command(1) = &H6 'Komenda 
        'Ax1_Command(2) = &H0 'Startowy adres danych
        'Ax1_Command(3) = &H3 'Freq Ref
        'Ax1_Command(4) = &H0 'Ilość
        'Ax1_Command(5) = &H1 'Ilość

        'Ax1_Command(0) = &H1 ' 'Adres napędu
        'Ax1_Command(1) = &H6 'Komenda 06H
        'Ax1_Command(2) = &H1 'Startowy adres danych 40100
        'Ax1_Command(3) = &H0 'Adres danych
        'Ax1_Command(4) = &H0 'Zawartość danych
        'Ax1_Command(5) = &H1 'Zawartość danych

        Dim Ax1_Command(12) As Byte 'Sterowanie prędkością
        Ax1_Command(0) = &H1 'Adres napędu
        Ax1_Command(1) = &H10 'Komenda 10H - zapis wielu bajtów
        Ax1_Command(2) = &H1 'Startowy adres danych 0100 STW-słowo kontrolne, 0101 HSW-prędkość 
        Ax1_Command(3) = &H0 'Startowy adres danych
        Ax1_Command(4) = &H0 'Ilość
        Ax1_Command(5) = &H2 'Ilość
        Ax1_Command(6) = &H4 'Ilość danych
        Ax1_Command(7) = &H40 'Słowo kontrolne
        Ax1_Command(8) = &H0 'Słowo kontrolne
        Ax1_Command(9) = &H0 'Prędkość
        Ax1_Command(10) = &H7F 'Prędkość

        Dim crcfull As UInt16 = &HFFFF
        Dim crclsb As Byte
        For i As Integer = 0 To 10 ' Ax1_Command.Length - 3
            crcfull = crcfull Xor Ax1_Command(i)
            For j As Integer = 0 To 7
                crclsb = crcfull And &H1
                crcfull = crcfull >> 1
                If (crclsb <> 0) Then
                    crcfull = crcfull Xor &HA001
                End If
            Next
        Next
        Ax1_crchigh = (crcfull >> 8) And &HFF
        Ax1_crclow = crcfull And &HFF

        'MooBus_CRC()
        Ax1_Command(11) = Ax1_crclow
        Ax1_Command(12) = Ax1_crchigh

        Ser_MODBUS.Write(Ax1_Command, 0, Ax1_Command.Length)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label2.Text = Ax1_RX
    End Sub

    'MODBUS TIMER
    Private Sub Tim_MODBUS_Tick(sender As Object, e As EventArgs) Handles Tim_MODBUS.Tick
        ModBus_TxD()
    End Sub

    Sub ModBus_TxD()
        On Error GoTo Blad_Transmisji
        Dim R0 As Byte : R0 = 0
        If Mid(TxD, 1, 1) = "L" Then R0 = (R0 Or 1)
        If Mid(TxD, 1, 1) = "R" Then R0 = (R0 Or 2)
        If Mid(TxD, 2, 1) = "L" Then R0 = (R0 Or 4)
        If Mid(TxD, 2, 1) = "R" Then R0 = (R0 Or 8)

        'If Mid(TxD, 3, 1) = "L" Then R0 = (R0 Or 16)
        'If Mid(TxD, 3, 1) = "R" Then R0 = (R0 Or 32)
        'If Mid(TxD, 4, 1) = "L" Then R0 = (R0 Or 64)
        'If Mid(TxD, 4, 1) = "R" Then R0 = (R0 Or 128)

        If Mid(TxD, 3, 1) = "R" Then R0 = (R0 Or 16)
        If Mid(TxD, 3, 1) = "L" Then R0 = (R0 Or 32)
        If Mid(TxD, 4, 1) = "R" Then R0 = (R0 Or 64)
        If Mid(TxD, 4, 1) = "L" Then R0 = (R0 Or 128)

        If Mid(TxD, 5, 1) = "L" Then R0 = (R0 Or 256)
        If Mid(TxD, 5, 1) = "R" Then R0 = (R0 Or 512)
        If Mid(TxD, 6, 1) = "L" Then R0 = (R0 Or 1024)
        If Mid(TxD, 6, 1) = "R" Then R0 = (R0 Or 2048)

        If Mid(TxD, 7, 1) = "L" Then R0 = (R0 Or 4096)
        If Mid(TxD, 7, 1) = "R" Then R0 = (R0 Or 8192)
        If Mid(TxD, 8, 1) = "L" Then R0 = (R0 Or 16384)
        If Mid(TxD, 8, 1) = "R" Then R0 = (R0 Or 32768)

        Dim R1 As Byte : R1 = 0
        If Mid(TxD, 9, 1) = "L" Then R1 = (R0 Or 1)
        If Mid(TxD, 9, 1) = "R" Then R1 = (R0 Or 2)
        If Mid(TxD, 10, 1) = "L" Then R1 = (R0 Or 4)
        If Mid(TxD, 10, 1) = "R" Then R1 = (R0 Or 8)

        If Mid(TxD, 11, 1) = "L" Then R1 = (R0 Or 16)
        If Mid(TxD, 11, 1) = "R" Then R1 = (R0 Or 32)
        If Mid(TxD, 12, 1) = "L" Then R1 = (R0 Or 64)
        If Mid(TxD, 12, 1) = "R" Then R1 = (R0 Or 128)

        If Mid(TxD, 13, 1) = "L" Then R1 = (R0 Or 256)
        If Mid(TxD, 13, 1) = "R" Then R1 = (R0 Or 512)
        If Mid(TxD, 14, 1) = "L" Then R1 = (R0 Or 1024)
        If Mid(TxD, 14, 1) = "R" Then R1 = (R0 Or 2048)

        If Mid(TxD, 15, 1) = "L" Then R1 = (R0 Or 4096)
        If Mid(TxD, 15, 1) = "R" Then R1 = (R0 Or 8192)
        If Mid(TxD, 16, 1) = "L" Then R1 = (R0 Or 16384)
        If Mid(TxD, 16, 1) = "R" Then R1 = (R0 Or 32768)

        Dim R2 As Byte : R2 = 0
        If Mid(TxD, 17, 1) = "L" Then R2 = (R0 Or 1)
        If Mid(TxD, 17, 1) = "R" Then R2 = (R0 Or 2)
        If Mid(TxD, 18, 1) = "L" Then R2 = (R0 Or 4)
        If Mid(TxD, 18, 1) = "R" Then R2 = (R0 Or 8)

        If Mid(TxD, 19, 1) = "L" Then R2 = (R0 Or 16)
        If Mid(TxD, 19, 1) = "R" Then R2 = (R0 Or 32)
        If Mid(TxD, 20, 1) = "L" Then R2 = (R0 Or 64)
        If Mid(TxD, 20, 1) = "R" Then R2 = (R0 Or 128)

        If Mid(TxD, 21, 1) = "L" Then R2 = (R0 Or 256)
        If Mid(TxD, 21, 1) = "R" Then R2 = (R0 Or 512)
        If Mid(TxD, 22, 1) = "L" Then R2 = (R0 Or 1024)
        If Mid(TxD, 22, 1) = "R" Then R2 = (R0 Or 2048)

        If Mid(TxD, 23, 1) = "L" Then R2 = (R0 Or 4096)
        If Mid(TxD, 23, 1) = "R" Then R2 = (R0 Or 8192)
        If Mid(TxD, 24, 1) = "L" Then R2 = (R0 Or 16384)
        If Mid(TxD, 24, 1) = "R" Then R2 = (R0 Or 32768)


        Dim R3 As Byte : R3 = 0
        If Mid(TxD, 25, 1) = "L" Then R3 = (R0 Or 1)
        If Mid(TxD, 25, 1) = "R" Then R3 = (R0 Or 2)
        If Mid(TxD, 26, 1) = "L" Then R3 = (R0 Or 4)
        If Mid(TxD, 26, 1) = "R" Then R3 = (R0 Or 8)

        If Mid(TxD, 27, 1) = "L" Then R3 = (R0 Or 16)
        If Mid(TxD, 27, 1) = "R" Then R3 = (R0 Or 32)
        If Mid(TxD, 28, 1) = "L" Then R3 = (R0 Or 64)
        If Mid(TxD, 28, 1) = "R" Then R3 = (R0 Or 128)

        If Mid(TxD, 29, 1) = "L" Then R3 = (R0 Or 256)
        If Mid(TxD, 29, 1) = "R" Then R3 = (R0 Or 512)
        If Mid(TxD, 30, 1) = "L" Then R3 = (R0 Or 1024)
        If Mid(TxD, 30, 1) = "R" Then R3 = (R0 Or 2048)

        If Mid(TxD, 31, 1) = "L" Then R3 = (R0 Or 4096)
        If Mid(TxD, 31, 1) = "R" Then R3 = (R0 Or 8192)
        If Mid(TxD, 32, 1) = "L" Then R3 = (R0 Or 16384)
        If Mid(TxD, 32, 1) = "R" Then R3 = (R0 Or 32768)

        Dim R4 As Byte : R4 = 0
        If Mid(TxD, 33, 1) = "L" Then R4 = (R0 Or 1)
        If Mid(TxD, 33, 1) = "R" Then R4 = (R0 Or 2)
        If Mid(TxD, 34, 1) = "L" Then R4 = (R0 Or 4)
        If Mid(TxD, 34, 1) = "R" Then R4 = (R0 Or 8)

        If Mid(TxD, 35, 1) = "L" Then R4 = (R0 Or 16)
        If Mid(TxD, 35, 1) = "R" Then R4 = (R0 Or 32)
        If Mid(TxD, 36, 1) = "L" Then R4 = (R0 Or 64)
        If Mid(TxD, 36, 1) = "R" Then R4 = (R0 Or 128)

        If Mid(TxD, 37, 1) = "L" Then R4 = (R0 Or 256)
        If Mid(TxD, 37, 1) = "R" Then R4 = (R0 Or 512)
        If Mid(TxD, 38, 1) = "L" Then R4 = (R0 Or 1024)
        If Mid(TxD, 38, 1) = "R" Then R4 = (R0 Or 2048)

        If Mid(TxD, 39, 1) = "L" Then R4 = (R0 Or 4096)
        If Mid(TxD, 39, 1) = "R" Then R4 = (R0 Or 8192)
        If Mid(TxD, 40, 1) = "L" Then R4 = (R0 Or 16384)
        If Mid(TxD, 40, 1) = "R" Then R4 = (R0 Or 32768)

        Dim R5 As Byte : R5 = 0
        If Mid(TxD, 41, 1) = "L" Then R5 = (R0 Or 1)
        If Mid(TxD, 41, 1) = "R" Then R5 = (R0 Or 2)
        If Mid(TxD, 42, 1) = "L" Then R5 = (R0 Or 4)
        If Mid(TxD, 42, 1) = "R" Then R5 = (R0 Or 8)

        If Mid(TxD, 43, 1) = "L" Then R5 = (R0 Or 16)
        If Mid(TxD, 43, 1) = "R" Then R5 = (R0 Or 32)
        If Mid(TxD, 44, 1) = "L" Then R5 = (R0 Or 64)
        If Mid(TxD, 44, 1) = "R" Then R5 = (R0 Or 128)

        If Mid(TxD, 45, 1) = "L" Then R5 = (R0 Or 256)
        If Mid(TxD, 45, 1) = "R" Then R5 = (R0 Or 512)
        If Mid(TxD, 46, 1) = "L" Then R5 = (R0 Or 1024)
        If Mid(TxD, 46, 1) = "R" Then R5 = (R0 Or 2048)

        If Mid(TxD, 47, 1) = "L" Then R5 = (R0 Or 4096)
        If Mid(TxD, 47, 1) = "R" Then R5 = (R0 Or 8192)
        If Mid(TxD, 48, 1) = "L" Then R5 = (R0 Or 16384)
        If Mid(TxD, 48, 1) = "R" Then R5 = (R0 Or 32768)

        Dim R6 As Byte : R6 = 0
        If Mid(TxD, 49, 1) = "L" Then R6 = (R0 Or 1)
        If Mid(TxD, 49, 1) = "R" Then R6 = (R0 Or 2)
        If Mid(TxD, 50, 1) = "L" Then R6 = (R0 Or 4)
        If Mid(TxD, 50, 1) = "R" Then R6 = (R0 Or 8)



        Dim Ax1_Command(22) As Byte 'Sterowanie prędkością
        Ax1_Command(0) = &H1 '1 Adres PLC
        Ax1_Command(1) = &H10 '2 Komenda 10H - zapis wielu bajtów
        Ax1_Command(2) = &H0 '3 Startowy adres danych 0100 STW-słowo kontrolne, 0101 HSW-prędkość 
        Ax1_Command(3) = &H1 '4 Startowy adres danych
        Ax1_Command(4) = &H0 '5 Ilość
        Ax1_Command(5) = &H7 '6 Ilość
        Ax1_Command(6) = &HE '7 Ilość danych
        Ax1_Command(7) = &H0 '8

        Ax1_Command(8) = R0 '9 R0
        Ax1_Command(9) = &H0 '10 R1
        Ax1_Command(10) = &H0 '11 R2
        Ax1_Command(11) = &H0 '12 R3
        Ax1_Command(12) = &H0 '13 R4
        Ax1_Command(13) = &H0 '14 R5
        Ax1_Command(14) = &H0 '15 R6

        Ax1_Command(15) = &H0 '16
        Ax1_Command(16) = &H0 '17
        Ax1_Command(17) = &H0 '18
        Ax1_Command(18) = &H0 '19
        Ax1_Command(19) = &H0 '20
        Ax1_Command(20) = &H0 '21

        Dim crcfull As UInt16 = &HFFFF
        Dim crclsb As Byte
        For i As Integer = 0 To 20
            crcfull = crcfull Xor Ax1_Command(i)
            For j As Integer = 0 To 7
                crclsb = crcfull And &H1
                crcfull = crcfull >> 1
                If (crclsb <> 0) Then
                    crcfull = crcfull Xor &HA001
                End If
            Next
        Next
        Ax1_crchigh = (crcfull >> 8) And &HFF
        Ax1_crclow = crcfull And &HFF

        'MooBus_CRC()
        Ax1_Command(21) = Ax1_crclow '22
        Ax1_Command(22) = Ax1_crchigh '23

        'Ax1_Command(11) = Ax1_crclow
        'Ax1_Command(12) = Ax1_crchigh

        Ser_MODBUS.Write(Ax1_Command, 0, Ax1_Command.Length)
        Exit Sub

Blad_Transmisji:
        Lbl_Txd.Text = "Txd: Error!!!"

    End Sub






    Sub Ax1_Rx_Data()

        Dim bytesNber As Integer = Ser_MODBUS.BytesToRead
        Dim hex2 As String = ""
        Do
            Dim ReceiveBuf() As Byte = New Byte() {}
            Array.Resize(ReceiveBuf, bytesNber)
            Ser_MODBUS.Read(ReceiveBuf, 0, bytesNber)
            hex2 &= bytetohex1(ReceiveBuf)
        Loop While Ser_MODBUS.BytesToRead <> 0
    End Sub



    Public Function bytetohex1(buffer() As Byte) As String
        Dim comp As String = ""
        bytetohex1 = vbNullString
        For i = 0 To UBound(buffer)
            If Len(Hex(buffer(i))) = 1 Then comp = " 0" Else comp = " "
            bytetohex1 = bytetohex1 & comp & Hex(buffer(i))
        Next i

        Ax1_RX = bytetohex1
    End Function








#Region "Programy testowe"
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Mono_File = "D:\Filmy5D\nWave Pictures\JetPack Adventure\Jetpack_Adventure_Polish_L.mpg"
        'Video.Play()
        Video.Stop_PLAY()
        End_All_Process()
    End Sub
#End Region



    Sub Stop_Process()
        Dim x As Byte = 0
        XD_All_Reset()

        For x = 0 To 33
            Try
                Curr_Proc(x).CloseMainWindow()
                Curr_Proc(x).Kill()
                Curr_Proc(x).Close()
            Catch
            End Try
        Next
        Video.Stop_PLAY()
        'Lbl_Panel_Command.Text = ""

    End Sub


    Private Sub XD_All_Reset()

        Trk_DMX_A_Smoke.Value = 0 : Chk_A_Smoke_Set()
        Trk_DMX_B_Smoke.Value = 0 : Chk_B_Smoke_Set()
        Trk_DMX_C_Smoke.Value = 0 : Chk_C_Smoke_Set()
        Trk_DMX_D_Smoke.Value = 0 : Chk_D_Smoke_Set()
        Trk_DMX_E_Smoke.Value = 0 : Chk_E_Smoke_Set()
        Trk_DMX_F_Smoke.Value = 0 : Chk_F_Smoke_Set()

        Lbl_DMX_Val_A_Smoke.Text = 0.ToString : Lbl_DMX_Val_B_Smoke.Text = 0.ToString : Lbl_DMX_Val_C_Smoke.Text = 0.ToString
        Lbl_DMX_Val_D_Smoke.Text = 0.ToString : Lbl_DMX_Val_E_Smoke.Text = 0.ToString : Lbl_DMX_Val_F_Smoke.Text = 0.ToString

        Trk_DMX_A_Bubble.Value = 0 : Chk_A_Bubble_Set()
        Trk_DMX_B_Bubble.Value = 0 : Chk_B_Bubble_Set()
        Trk_DMX_C_Bubble.Value = 0 : Chk_C_Bubble_Set()
        Trk_DMX_D_Bubble.Value = 0 : Chk_D_Bubble_Set()
        Trk_DMX_E_Bubble.Value = 0 : Chk_E_Bubble_Set()
        Trk_DMX_F_Bubble.Value = 0 : Chk_F_Bubble_Set()

        Lbl_DMX_Val_A_Bubble.Text = 0.ToString : Lbl_DMX_Val_B_Bubble.Text = 0.ToString : Lbl_DMX_Val_C_Bubble.Text = 0.ToString
        Lbl_DMX_Val_D_Bubble.Text = 0.ToString : Lbl_DMX_Val_E_Bubble.Text = 0.ToString : Lbl_DMX_Val_F_Bubble.Text = 0.ToString

        Trk_DMX_Wind60.Value = 0 : Chk_Wind60_Set()
        Trk_DMX_Wind180.Value = 0 : Chk_Wind180_Set()
        Trk_DMX_Wind300.Value = 0 : Chk_Wind300_Set()

        Lbl_DMX_Val_Wind60.Text = 0.ToString : Lbl_DMX_Val_Wind180.Text = 0.ToString : Lbl_DMX_Val_Wind300.Text = 0.ToString

        Trk_DMX_Thunder.Value = 0 : Chk_Thunder_Set()

        Lbl_DMX_Val_Thunder.Text = 0.ToString
    End Sub


    'Procedury ogólne:
    Dim Working_String As String = "", Working_Directory As String, Bat_File As String 'Dane do uruchomienia procesu



    Sub Play_Process()

        Stop_Process()
        Dim Run_Process As Byte = 0 ', Curr_Button As Byte = 1
        Run_Process = Curr_Show




        Working_String = T_Show(Curr_Show, 2) ' Tbl_Script(Curr_Button, Run_Process)
        Dim batDir As String = T_Show(Curr_Show, 2) ' = String.Format(Working_Directory)

        Dim Filename As String = Working_String
        Dim FileInfo As New FileInfo(Filename)

        Working_Directory = FileInfo.Directory.ToString
        Bat_File = FileInfo.Name

        Dim Video_File_flag As Boolean = False
        If Mid(T_Show(Curr_Show, 2), Len(T_Show(Curr_Show, 2)) - 3, 4) = ".mpg" Then Video_File_flag = True
        'If Mid(T_Show(Curr_Show, 2), Len(T_Show(Curr_Show, 2)) - 3, 4) = ".mp4" Then Video_File_flag = True
        If Mid(T_Show(Curr_Show, 2), Len(T_Show(Curr_Show, 2)) - 3, 4) = ".avi" Then Video_File_flag = True

        If Video_File_flag = True Then
            Mono_File = T_Show(Curr_Show, 2)
            File_5D = T_Show(Curr_Show, 3)
            'Call Video.Play()
            Lbl_Panel_Command.Text = "Start" ' Process666: " & Curr_Show.ToString & "."
            GoTo Projekcja_Video
        End If

        Try
            Curr_Proc(Run_Process) = New Process()
            Curr_Proc(Run_Process).StartInfo.WorkingDirectory = Working_Directory
            Curr_Proc(Run_Process).StartInfo.FileName = T_Show(Curr_Show, 2)
            Curr_Proc(Run_Process).StartInfo.CreateNoWindow = False
            Curr_Proc(Run_Process).Start()

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace.ToString())
            MessageBox.Show("File not executed !!")
        End Try
Projekcja_Video:

        'Timer_Delay.Enabled = True

    End Sub


    'Ustawianie Frm_Main
    Private Sub Timer_Delay_Tick(sender As Object, e As EventArgs) Handles Timer_Delay.Tick
        'Timer_Delay.Enabled = False
        If Lbl_Panel_Command.Text = "Start" Then
            Video.Stop_PLAY()
            Lbl_Panel_Command.Text = ""
            Call Video.Play()
        End If
        If Lbl_Panel_Command.Text = "Stop" Then
            Lbl_Panel_Command.Text = ""
            Video.Stop_PLAY()
            Stop_Process()
        End If
        'Me.Activate()
    End Sub


End Class
