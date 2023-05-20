Imports DirectShowLib
Imports System.Runtime.InteropServices
Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices.ComTypes

Public Class Video
#Region "Deklaracja zmiennych"
    Dim hr As Integer : Dim hr2 As Integer 'Zmienne do obsługi DirectShow

    Dim pause_play As Byte = 0 '1 - PAUSE

    'Przenosze do modułu ADVERTISING!
    'Public toggle_window_position_flag As Boolean = False 'Zmienia projektor dla każdej kolejnej projekcji

    'Dla IMP
    Dim Duration_Double As Double 'Długość filmu w milisekundach
    Dim Current_Position_Double As Double = 0 'Aktualna pozycja filmu w milisekundach
    Dim Current_Frame_OLD As Double = 0 'Poprzednia pozycja filmu w milisekundach
    Dim End_Play_Delay_Timer As Integer = 0 'Jak nie zmienia się Current_Frame to robi STOP_PLAY

    'Zastąpione dla IMS
    'Dim Duration_Long As Long = 0
    'Dim Current_Position_Long As Long = 0

    Dim Open_Movie_File_Error_Flag As Boolean = False 'Flaga nieprawidłowego pliku

    'RS232
    Dim RS_232_Delay As Integer = 0 'Opóźnienie na porcie RS-232 x 10ms

    'Dla kursora myszki poprawka sposobu wygaszania
    Dim cursor_position_old As Point
    Dim cursor_position_delay As Integer
    Dim cursor_show_flag As Boolean = True

    Dim Tmr_Stop_Enable_Flag As Boolean

#End Region

#Region "Init"
    '**************************************************************************
    ' Inicjalizacja DirectShow
    '**************************************************************************
    '//EvenCode
    Dim ev As EventCode
    '/The IMediaEvent interface contains methods for retrieving event notifications and for overriding
    '/the Filter Graph Manager's default handling of events.
    'CancelDefaultHandling      - Cancels the Filter Graph Manager's default handling for a specified event. 
    'FreeEventParams            - Frees resources associated with the parameters of an event. 
    'GetEvent Retrieves         - the next event notification from the event queue. 
    'GetEventHandle             - Retrieves a handle to a manual-reset event that remains signaled while the queue contains event notifications. 
    'RestoreDefaultHandling     - Restores the Filter Graph Manager's default handling for a specified event. 
    'WaitForCompletion          - Waits for the filter graph to render all available data. 

    '//Filter Graph
    Dim fg As FilterGraph
    '/The Filter Graph Manager builds and controls filter graphs. This object is the central component in DirectShow.
    '/Applications use it to build and control filter graphs. The Filter Graph Manager also handles synchronization,
    '/event notification, and other aspects of the controlling the filter graph. Create this object by calling CoCreateInstance.
    'CLSID_FilterGraph          - Creates the Filter Graph Manager on a shared worker thread.
    'CLSID_FilterGraphNoThread  - Creates the Filter Graph Manager on the application thread.

    '//IFilterGraph2
    Dim ifg2 As IFilterGraph2
    '/The IFilterGraph2 interface extends the IFilterGraph and IGraphBuilder interfaces,
    '/which contain methods for building filter graphs.
    'AddSourceFilterForMoniker  - Creates a source filter from a moniker and adds it to the graph.  
    'ReconnectEx                - Breaks an existing pin connection and reconnects the same pins, using a specified media type.  
    'RenderEx                   - Renders an output pin, with an option to use existing renderers only. 

    '//IMediaControl
    Dim imc As IMediaControl
    '/The IMediaControl interface provides methods for controlling the flow of data through the filter graph.
    'Run                        - Runs all the filters in the filter graph. 
    'Pause                      - Pauses all filters in the filter graph. 
    'Stop                       - Stops all the filters in the filter graph. 
    'StopWhenReady              - Pauses the filter graph, allowing filters to queue data, and then stops the filter graph. 
    'GetState                   - Retrieves the state of the filter graph. 
    'RenderFile                 - Intended for Visual Basic 6.0; not documented here. 
    'AddSourceFilter            - Intended for Visual Basic 6.0; not documented here. 
    'get_FilterCollection       - Intended for Visual Basic 6.0; not documented here. 
    'get_RegFilterCollection    - Intended for Visual Basic 6.0; not documented here. 

    ''//IMediaEvent
    Dim ime As IMediaEvent
    '/The IMediaEvent interface contains methods for retrieving event notifications and for overriding
    '/the Filter Graph Manager's default handling of events.
    'CancelDefaultHandling      - Cancels the Filter Graph Manager's default handling for a specified event. 
    'FreeEventParams            - Frees resources associated with the parameters of an event. 
    'GetEvent                   - Retrieves the next event notification from the event queue. 
    'GetEventHandle             - Retrieves a handle to a manual-reset event that remains signaled while the queue contains event notifications. 
    'RestoreDefaultHandling     - Restores the Filter Graph Manager's default handling for a specified event. 
    'WaitForCompletion          - Waits for the filter graph to render all available data. 

    '------------------------------------------------------------------------------

    '//IMediaPosition
    Dim imp As IMediaPosition
    '/The IMediaPosition interface contains methods for seeking to a position within a stream. The IMediaSeeking interface improves on this interface.
    '/Applications written in C/C++ should use IMediaSeeking instead of IMediaPosition. 
    '/However, IMediaSeeking is not compatible with Automation, so applications written in Microsoft® Visual Basic® 6.0 must use IMediaPosition instead
    'get_Duration               - Retrieves the duration of the stream.
    'put_CurrentPosition        - Sets the current position, relative to the total duration of the stream.
    'get_CurrentPosition        - Retrieves the current position, relative to the total duration of the stream.
    'get_StopTime               - Retrieves the time at which the playback will stop, relative to the duration of the stream.
    'put_StopTime               - Sets the time at which the playback will stop, relative to the duration of the stream.
    'get_PrerollTime            - Retrieves the amount of data that will be queued before the start position.
    'put_PrerollTime            - Sets the amount of data that will be queued before the start position.
    'put_Rate                   - Sets the playback rate.
    'get_Rate                   - Retrieves the playback rate.
    'CanSeekForward             - Determines whether the filter graph can seek forward in the stream.
    'CanSeekBackward            - Determines whether the filter graph can seek backward in the stream.

    '//IMediaSeeking
    Dim ims As IMediaSeeking
    '/The IMediaSeeking interface contains methods for seeking to a position within a stream, and for setting the playback rate.
    '/The Filter Graph Manager exposes this interface, and so do individual filters or pins. Applications should query the Filter Graph Manager for the interface.
    'GetCapabilities            - Retrieves all the seeking capabilities of the stream.
    'CheckCapabilities          - Queries whether a stream has specified seeking capabilities.
    'IsFormatSupported          - Determines whether a specified time format is supported for seek operations.
    'QueryPreferredFormat       - Retrieves the preferred time format for seeking.
    'GetTimeFormat              - Retrieves the time format that is currently being used for seek operations.
    'IsUsingTimeFormat          - Determines whether seek operations are currently using a specified time format.
    'SetTimeFormat              - Sets the time format for subsequent seek operations.
    'GetDuration                - Retrieves the duration of the stream.
    'GetStopPosition            - Retrieves the time at which the playback will stop, relative to the duration of the stream.
    'GetCurrentPosition         - Retrieves the current position, relative to the total duration of the stream.
    'ConvertTimeFormat          - Converts from one time format to another.
    'SetPositions               - Sets the current position and the stop position.
    'GetPositions               - Retrieves the current position and the stop position, relative to the total duration of the stream.
    'GetAvailable               - Retrieves the range of times in which seeking is efficient.
    'SetRate                    - Sets the playback rate.
    'GetRate                    - Retrieves the playback rate.
    'GetPreroll                 - Retrieves the amount of data that will be queued before the start position.

    '//IFilterGraph
    Dim ifg As IFilterGraph
    '/The IFilterGraph interface provides methods for building a filter graph. An application can use it to add filters to the graph,
    '/connect or disconnect filters, remove filters, and perform other basic operations.
    'AddFilter                  - Adds a filter to the graph.  
    'RemoveFilter               - Removes a filter from the graph.  
    'EnumFilters                - Provides an enumerator for all filters in the graph.  
    'FindFilterByName           - Finds a filter that was added with a specified name.  
    'ConnectDirect              - Connects two pins directly (without intervening filters).  
    'Reconnect                  - Breaks the existing pin connection and reconnects it to the same pin.  
    'Disconnect                 - Disconnects a specified pin.  
    'SetDefaultSyncSource       - Sets the reference clock to the default clock. 

    '//IFilterGraph3
    Dim ifg3 As IFilterGraph3
    '/The IFilterGraph3 interface extends the IFilterGraph2 interface,
    '/which contains methods for building filter graphs.
    'SetSyncSourceEx            - Sets a primary and secondary reference clock for the filter graph.

    '//IBasicVideo
    Dim ibv As IBasicVideo
    '/The IBasicVideo interface sets video properties such as the destination and source rectangles. The Video Renderer filter and Video Mixing Renderer filters
    '/implement this interface, but the interface is exposed to applications through the Filter Graph Manager. Applications should always retrieve this 
    '/interface from the Filter Graph Manager. 
    'get_AvgTimePerFrame        - Retrieves the average time between successive frames. 
    'get_BitErrorRate           - Retrieves the approximate bit error rate of the video stream. 
    'get_BitRate                - Retrieves the approximate bit rate of the video stream. 
    'get_DestinationHeight      - Retrieves the height of the destination rectangle. 
    'get_DestinationLeft        - Retrieves the x-coordinate of the destination rectangle. 
    'get_DestinationTop         - Retrieves the y-coordinate of the destination rectangle. 
    'get_DestinationWidth       - Retrieves the width of the destination rectangle. 
    'get_SourceHeight           - Retrieves the height of the source rectangle. 
    'get_SourceLeft             - Retrieves the x-coordinate of the source rectangle. 
    'get_SourceTop              - Retrieves the y-coordinate of the source rectangle. 
    'get_SourceWidth            - Retrieves the width of the source rectangle. 
    'get_VideoHeight            - Retrieves the video height. 
    'get_VideoWidth             - Retrieves the video width. 
    'GetCurrentImage            - Returns a copy of the current image that is waiting at the renderer. 
    'GetDestinationPosition     - Retrieves the destination rectangle. 
    'GetSourcePosition          - Retrieves the source rectangle. 
    'GetVideoPaletteEntries     - Retrieves the color palette entries required by the video. 
    'GetVideoSize               - Retrieves the native video dimensions. 
    'IsUsingDefaultDestination  - Queries whether the renderer is using the default destination rectangle. 
    'IsUsingDefaultSource       - Queries whether the renderer is using the default source rectangle. 
    'put_DestinationHeight      - Sets the height of the destination rectangle. 
    'put_DestinationLeft        - Sets the x-coordinate of the destination rectangle. 
    'put_DestinationTop         - Sets the y-coordinate of the destination rectangle. 
    'put_DestinationWidth       - Sets the width of the destination rectangle. 
    'put_SourceHeight           - Sets the height of the video rectangle. 
    'put_SourceLeft             - Sets the x-coordinate of the source rectangle. 
    'put_SourceTop              - Sets the y-coordinate of the source rectangle. 
    'put_SourceWidth            - Sets the width of the source rectangle. 
    'SetDefaultDestinationPosition  - Reverts to the default destination rectangle. 
    'SetDefaultSourcePosition   - Reverts to the default source rectangle. 
    'SetDestinationPosition     - Sets the destination rectangle. 
    'SetSourcePosition          - Sets the source rectangle.

    '//IBasicVideo2
    Dim ibv2 As IBasicVideo2
    '/The IBasicVideo2 interface extends the IBasicVideo interface. The Video Renderer filter
    '/and Video Mixing Renderer filters implement this interface, but the interface is exposed
    '/to applications through the Filter Graph Manager. Applications should always retrieve
    '/this interface from the Filter Graph Manager.
    'GetPreferredAspectRatio    - Retrieves the preferred video aspect ratio.

    '//IVideoWindow
    Dim ivw As IVideoWindow
    '/The IVideoWindow interface sets properties on the video window.
    '/Applications can use it to set the window owner, the position and dimensions of the window,
    '/and other properties. 
    '/Note
    '/The IVMRWindowlessControl or IVMRWindowlessControl9 interface is now preferred over
    '/IVideoWindow. For more information, see Using Windowless Mode.
    'get_AutoShow               - Queries whether the video renderer automatically shows the video window when it receives video data.
    'get_BackgroundPalette      - Queries whether the video window realizes its palette in the background. 
    'get_BorderColor            - Retrieves the color that appears around the edges of the destination rectangle.
    'get_Caption                - Retrieves the video window caption. 
    'get_FullScreenMode         - Queries whether the video renderer is in full-screen mode. 
    'get_Height                 - Retrieves the height of the video window. 
    'get_Left                   - Retrieves the video window's x-coordinate. 
    'get_MessageDrain           - Retrieves the window that receives mouse and keyboard messages from the video window, if any. 
    'get_Owner                  - Retrieves the video window's parent window, if any. 
    'get_Top                    - Retrieves the video window's y-coordinate. 
    'get_Visible                - Queries whether the video window is visible. 
    'get_Width                  - Retrieves the width of the video window. 
    'get_WindowState            - Queries whether the video window is visible, hidden, minimized, or maximized. 
    'get_WindowStyle            - Retrieves the window style on the video window. 
    'get_WindowStyleEx          - Retrieves the extended window style on the video window. 
    'GetMaxIdealImageSize       - Retrieves the maximum ideal size for the video image. 
    'GetMinIdealImageSize       - Retrieves the minimum ideal size for the video image. 
    'GetRestorePosition         - Retrieves the restored window position. 
    'GetWindowPosition          - Retrieves the position of the video window. 
    'HideCursor                 - Hides the cursor. 
    'IsCursorHidden             - Queries whether the cursor is hidden. 
    'NotifyOwnerMessage         - Forwards a message to the video window. 
    'put_AutoShow               - Specifies whether the video renderer automatically shows the video window when it receives video data.
    'put_BackgroundPalette      - Specifies whether the video window realizes its palette in the background. 
    'put_BorderColor            - Sets the color that appears around the edges of the destination rectangle.
    'put_Caption                - Sets the video window caption. 
    'put_FullScreenMode         - Enables or disables full-screen mode. 
    'put_Height                 - Sets the height of the video window. 
    'put_Left                   - Sets the video window's x-coordinate. 
    'put_MessageDrain           - Specifies a window to receive mouse and keyboard messages from the video window. 
    'put_Owner                  - Specifies a parent window for the video window. 
    'put_Top                    - Sets the video window's y-coordinate. 
    'put_Visible                - Shows or hides the video window. 
    'put_Width                  - Sets the width of the video window. 
    'put_WindowState            - Shows, hides, minimizes, or maximizes the video window. 
    'put_WindowStyle            - Sets the window style on the video window. 
    'put_WindowStyleEx          - Sets the extended window style on the video window. 
    'SetWindowForeground        - Places the video window at the top of the Z order. 
    'SetWindowPosition          - Sets the position of the video window.

    '//IVideoFrameStep
    Dim ivfs As IVideoFrameStep
    '/The IVideoFrameStep interface steps through a video stream.
    '/This interface enables Microsoft® DirectShow® applications, including DVD players,
    '/to step through a video stream as slowly as one frame at a time.
    '/Obtain the interface through the filter graph manager, which controls the frame stepping
    '/process in conjunction with the Overlay Mixer Filter or the Video Renderer Filter.
    '/Backward frame stepping is not supported.
    'Step                       - Causes the filter graph to step forward by the specified number of frames. 
    'CanStep                    - Determines the stepping capabilities of the specified filter. 
    'CancelStep                 - Cancels the previous step operation.

    '//IBasicAudio
    Dim iba As IBasicAudio
    '/The IBasicAudio interface controls the volume and balance of the audio stream.
    'get_Balance                - Retrieves the balance for the audio signal. 
    'get_Volume                 - Retrieves the volume (amplitude) of the audio signal. 
    'put_Balance                - Sets the balance for the audio signal. 
    'put_Volume                 - Sets the volume (amplitude) of the audio signal.

    '------------------------------------------------------------------------------

    '//IAMGraphStreams Interface
    Dim iamgs As IAMGraphStreams
    '/The IAMGraphStreams interface controls a filter graph that renders a live source.
    '/A live source is one that streams data in real time, such as a capture device or a network
    '/broadcast. The Filter Graph Manager implements this interface.
    'FindUpstreamInterface      - Searches the filter graph for a specified interface, upstream from a specified pin.
    'SyncUsingStreamOffset      - Enables or disables synchronization using time-stamp offsets.
    'SetMaxGraphLatency         - Sets the maximum latency for the graph.

    '//IAMStats Interface
    Dim iams As IAMStats
    '/The IAMStats interface retrieves performance data from the Filter Graph Manager. Filters can use this interface to record performance data.
    'AddValue                   - Records a new value.
    'get_Count                  - Retrieves the number of statistics.
    'GetIndex                   - Retrieves the index for a named statistic, or creates a new statistic.
    'GetValueByIndex            - Retrieves a statistic by index.
    'GetValueByName             - Retrieves a statistic by name.
    'Reset                      - Resets all statistics to zero.

    '//IFilterChain
    Dim ifc As IFilterChain
    '/The IFilterChain interface provides methods for starting, stopping, or removing chains of filters in a filter graph.
    '/The filter graph manager exposes this interface.
    '/A filter chain is a sequence of filters, each with at most one connected input pin and one connected output pin,
    '/that forms an unbroken line of filters. A filter chain is defined by the filter at the start of the chain and the filter at the end of the chain.
    '/(These can be the same filter, making a chain of one filter.) By definition, there is a single stream path going from the start of the chain downstream to the end of the chain.
    'StartChain                 - Switches all the filters in a filter chain into a running state.
    'StopChain                  - Switches all the filters in a filter chain into a stopped state.
    'RemoveChain                - Removes every filter in a filter chain from the filter graph.
    'PauseChain                 - Switches all the filters in a filter chain into a paused state.

    '//IFilterMapper2 Interface
    Dim ifm2 As IFilterMapper2
    '/Registers and unregisters filters, and locates filters in the registry.
    '/The Filter Mapper helper object implements this interface.
    'CreateCategory             - Adds a new filter category to the registry.
    'UnregisterFilter           - Removes filter information from the registry.
    'RegisterFilter             - Adds filter information to the registry.
    'EnumMatchingFilters        - Enumerates registered filters that meet specified requirements.

    '//IGraphBuilder
    Dim igb As IGraphBuilder
    '/This interface provides methods that enable an application to build a filter graph. The Filter Graph Manager implements this interface.
    'Connect                    - Connects two pins. If they will not connect directly, this method connects them with intervening transforms. 
    'Render                     - Adds a chain of filters to a specified output pin to render it.
    'RenderFile                 - Builds a filter graph that renders the specified file.
    'AddSourceFilter            - Adds a source filter to the filter graph for a specific file.
    'SetLogFile                 - Sets the file for logging actions taken when attempting to perform an operation.
    'Abort                      - Requests that the graph builder return as soon as possible from its current task.
    'ShouldOperationContinue    - Queries whether the current operation should continue.

    '//IGraphConfig
    Dim igc As IGraphConfig
    '/The Filter Graph Manager exposes IGraphConfig to support dynamic graph building. This interface enables applications and filters to reconfigure the filter graph while
    '/the graph is in a running state, and without losing data from the stream.
    'Reconnect                  - Performs a dynamic reconnection between two pins.
    'Reconfigure                - Locks the filter graph and calls a callback function in the application or filter to perform a dynamic reconfiguration.
    'AddFilterToCache           - Adds a filter to the filter cache.
    'RemoveFilterFromCache      - Removes a filter from the filter cache.
    'EnumCacheFilter            - Enumerates the filters in the filter cache.
    'GetStartTime               - Retrieves the reference time used when the filter graph was last put into a running state.
    'PushThroughData            - Pushes data through the filter graph to the specified pin.
    'SetFilterFlags             - Sets a filter's configuration information.
    'GetFilterFlags             - Retrieves a filter's configuration information.
    'RemoveFilterEx             - Removes a filter from the filter graph.

    Dim ibf As IBaseFilter

    Private Sub Video_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = 0 : Me.Left = Video_X
        Me.Height = Video_Height : Me.Width = Video_Width
        Me.BackgroundImage = System.Drawing.Image.FromFile(".\Picture.jpg") : Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
#End Region

#Region "NEW FilterGraph"
    'Tworzy nowy obiekt DirectShow
    Private Sub new_fg()
        fg = New FilterGraph

        ifg2 = DirectCast(fg, IFilterGraph2)

        imc = DirectCast(fg, IMediaControl)
        ime = DirectCast(fg, IMediaEvent)
        ivw = DirectCast(fg, IVideoWindow)
        ibv2 = DirectCast(fg, IBasicVideo2)

        ifc = DirectCast(fg, IFilterChain)
        iba = DirectCast(fg, IBasicAudio)

        ivfs = DirectCast(fg, IVideoFrameStep)
        imp = DirectCast(fg, IMediaPosition)
        'ims = DirectCast(fg, IMediaSeeking)

        ifg = DirectCast(fg, IFilterGraph)

        hr = ifg.SetDefaultSyncSource

    End Sub
#End Region

#Region "DirestShow STATE"
    Sub Stan_Pracy_DirectShow()
        GoTo Wygas_Cursor

        'UWAGA:
        'Instrukcja imc. wyłączona bo przy odtwarzaniu plików HDTV zawiesza program
        'Również nie używać instrukcji 'PAUSE'

        'Wywoływany z zegara systemowego z Frm_Main
        'On Error GoTo Stan_Pracy_DirectShow
        ''ODCZYTANIE STANU PRACY DirectShow
        ''GetState 0-otwarty FilterGraph, 1-Pause, 2-Play
        'Dim msTimeout_Integer As Integer
        'hr = imc.GetState(msTimeout_Integer, FilterGraph_State) : DsError.ThrowExceptionForHR(hr)
        'Select Case FilterGraph_State
        '    Case 0 : Frm_Main.Lbl_Control.Text = "[" & FilterGraph_State & "] Init"
        '    Case 1 : Frm_Main.Lbl_Control.Text = "[" & FilterGraph_State & "] Pause"
        '    Case 2 : Frm_Main.Lbl_Control.Text = "[" & FilterGraph_State & "] Play"
        '    Case Is > 2 : Frm_Main.Lbl_Control.Text = "[" & FilterGraph_State & "[ Unnown state]"
        'End Select

        'Exit Sub
        'Stan_Pracy_DirectShow:
        '        Frm_Main.Lbl_Control.Text = "[-] Stop"
        '        Frm_Main.Lbl_Position.Text = ""
        '        Frm_Main.Lbl_Duration.Text = ""
        '        Frm_Main.Lbl_5D_Data.Text = ""
        '        Frm_Main.Lbl_5D_Frame.Text = ""


        'Wygaszanie kursora na formie Frm_Video
Wygas_Cursor:

        'If Editor_5D.Tmr_Editor.Enabled = True Then GoTo Nie_wygaszaj_bo_aktywny_EDITOR

        'Wstawka wygaszająca kursor w stanie nieaktywności
        'zmienia sposób wygaszenia kursora z wygaszania na Frm_Video w zależności od pozycji
        'Powołuje nową zmienną w ramach modułu: Dim cursor_position_old as point
        'Dim cursor_position As Point

        '-------------------------------------------------------------------------------
        'Wygas_cursor_nieaktywny:
        'cursor_position = Windows.Forms.Cursor.Position

        'If cursor_position_old <> cursor_position Then GoTo Przykrywajaca_forma_2

        'If cursor_position_old = cursor_position Then

        'If cursor_show_flag = True Then
        'cursor_position_delay = cursor_position_delay + 1
        'If cursor_position_delay > 10 Then
        'Windows.Forms.Cursor.Hide() : cursor_show_flag = False
        'cursor_position_delay = 0
        'End If
        'End If

        'Frm_Main.Lbl_Control.Text = "=="
        'GoTo ukryj_kursor_2
        'End If

        'Nie_wygaszaj_bo_aktywny_EDITOR:

        'Przykrywajaca_forma_2:
        'cursor_position_old = cursor_position
        'If cursor_show_flag = False Then
        'Windows.Forms.Cursor.Show() : cursor_show_flag = True
        'End If
        'Frm_Main.Lbl_Control.Text = "<>"

        'ukryj_kursor_2:

    End Sub
#End Region

#Region "TIMER CONTROL"
    '**************************************************************************
    ' KONTROLA PRACY DIRECT SHOW
    '**************************************************************************
    'Dim PLAY_CTRL As Boolean = False  'Flaga wskazująca na stan PLAY (1) albo STOP(2)
    Dim PLAY_CTRL_DELAY As Byte = 0 'Opóżnienie zerwania komunikacji

    Private Sub Tmr_Control_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tmr_Control.Tick

        '--------------------------------------------------
        ' Tu wywoływana jest obsługa 5D dla RS-232
        '--------------------------------------------------
        'If GS300_1_ENABLE = True Then Call _5D_Execute()
        'If FTK_UDP_01 = True Then Call FATEK_plc()

        ''RS-232
        'If GS300_1_ENABLE = True Then
        ' If _5D_Enable = True Then
        ' Call _5D_Execute()
        ' Q0 = (Q0 Or 128) 'RUN na PLC
        ' Q2 = (Q2 And 223) 'Q2.5 Light_OFF
        ' Call GS_300_1_Execute()
        ' Else
        ' _5d_Tx = "U0000"
        ' Q2 = (Q2 Or 32) 'Q2.5 Light_ON
        ' Q0 = (Q0 And 121) 'STOP na PLC
        ' Call GS_300_1_Execute()
        ' End If
        ' End If

        On Error GoTo Error_Tmr_Control

        'POBIERA DURATION I CURRENT_POSITION
        'If Current_Position_Double > 10 Then GoTo Bez_Odczytu_Duration

        'imp
        If Current_Position_Double > 100 Then GoTo Bez_Odczytu_Duration

        hr = imp.get_Duration(Duration_Double) : DsError.ThrowExceptionForHR(hr)
        Frm_360.Lbl_Duration.Text = "Duration: " & Int(Duration_Double * 24)

        'ims
        'hr = ims.GetDuration(Duration_Long) : DsError.ThrowExceptionForHR(hr)
        'Frm_Main.Lbl_Duration.Text = "Duration: " & Int(Duration_Long * 24 / 10000000)
Bez_Odczytu_Duration:

        'imp
        hr = imp.get_CurrentPosition(Current_Position_Double) : DsError.ThrowExceptionForHR(hr)
        'Frm_Main.Lbl_Position.Text = "Frame: " & Int(Current_Position_Double * 24)

        'ims
        'hr = ims.GetPositions(Current_Position_Long, Nothing)
        'Frm_Main.Lbl_Position.Text = "Frame: " & Int(Current_Position_Long * 24 / 10000000)

        PLAY_CTRL = True

        'Zatrzymanie przez zakończenie filmu:
        'imp
        If Current_Position_Double >= Duration_Double Then Call Stop_PLAY()
        'Zatrzymanie przez nie zmieniające się Frame'y:
        If pause_play = 1 Then GoTo Nie_Sprawdzaj_Film_Zatrzymany
        If Current_Position_Double = Current_Frame_OLD Then
            End_Play_Delay_Timer = End_Play_Delay_Timer + 1
            If End_Play_Delay_Timer = 1000 Then
                End_Play_Delay_Timer = 0 : Current_Frame_OLD = 0
                'Lst_Text = "MSG> Frame Timer Delay STOP!" : Call dopisz_do_lst() : Call Stop_PLAY()
            End If
        Else
            Current_Frame_OLD = Current_Position_Double
            End_Play_Delay_Timer = 0
        End If
Nie_Sprawdzaj_Film_Zatrzymany:

        'ims
        'If Current_Position_Long = Duration_Long Then Call Stop_PLAY()

        Current_Frame = Int(Current_Position_Double * 24)
        Frm_360.Lbl_Position.Text = "Frame: " & Current_Frame
        Frm_Edit.Lbl_Curr_Frame_Edit.Text = Current_Frame




















        'PRZESZUKANIE LISTY EFEKTÓW:

        Dim T_Cnt As Integer = 0
        For T_Cnt = 1 To 999
            If Current_Frame > 0 Then
                If Val(T_DMX(T_Cnt, 1)) = Current_Frame Then
                    Dim T_Read_Cnt As Byte = 0, Rx_Str As String = ""

                    For T_Read_Cnt = 1 To 17
                        Rx_Str = Rx_Str & Val(T_DMX(T_Cnt, T_Read_Cnt)) & " / "
                    Next
                    Frm_360.Lbl_5D_Data.Text = "RxD: " & Rx_Str

                    'A:
                    Frm_360.Trk_DMX_A_Bubble.Value = T_DMX(T_Cnt, 2) =  'A Bubble
                    Frm_360.Trk_DMX_A_Smoke.Value = T_DMX(T_Cnt, 3)  'A Smoke
                    'B:
                    Frm_360.Trk_DMX_B_Bubble.Value = T_DMX(T_Cnt, 4)  'B Bubble
                    Frm_360.Trk_DMX_B_Smoke.Value = T_DMX(T_Cnt, 5)  'B Smoke
                    'C:
                    Frm_360.Trk_DMX_C_Bubble.Value = T_DMX(T_Cnt, 6)  'C Bubble
                    Frm_360.Trk_DMX_C_Smoke.Value = T_DMX(T_Cnt, 7) 'C Smoke
                    'D:
                    Frm_360.Trk_DMX_D_Bubble.Value = T_DMX(T_Cnt, 8) 'D Bubble
                    Frm_360.Trk_DMX_D_Smoke.Value = T_DMX(T_Cnt, 9) 'D Smoke
                    'E
                    Frm_360.Trk_DMX_E_Bubble.Value = T_DMX(T_Cnt, 10) 'E Bubble
                    Frm_360.Trk_DMX_E_Smoke.Value = T_DMX(T_Cnt, 11) 'E Smoke
                    'F
                    Frm_360.Trk_DMX_F_Bubble.Value = T_DMX(T_Cnt, 12) 'F Bubble
                    Frm_360.Trk_DMX_F_Smoke.Value = T_DMX(T_Cnt, 13)  'F Smoke
                    'WIND:
                    Frm_360.Trk_DMX_Wind60.Value = T_DMX(T_Cnt, 14)  'Trk_DMX_Wind60
                    Frm_360.Trk_DMX_Wind180.Value = T_DMX(T_Cnt, 15) 'Trk_DMX_Wind180
                    Frm_360.Trk_DMX_Wind300.Value = T_DMX(T_Cnt, 16) 'Trk_DMX_Wind300
                    'THUNDER:
                    Frm_360.Trk_DMX_Thunder.Value = T_DMX(T_Cnt, 17)  'Trk_DMX_Thunder



                End If
            End If
        Next




















        Time_to_Left = Val((Int(Duration_Double * 24)) - Current_Frame)
        '--------------------------------------------------
        Exit Sub
Error_Tmr_Control:
        'Frm_Main.Lbl_Duration.Text = "ERR> [502]: DS NI"

        'Przeniesione z DirectSHOW STATE:
        Frm_360.Lbl_Position.Text = ""
        Frm_360.Lbl_Duration.Text = ""
        Frm_360.Lbl_5D_Data.Text = ""
        Frm_360.Lbl_5D_Frame.Text = ""

        If PLAY_CTRL_DELAY < 20 Then
            PLAY_CTRL_DELAY = PLAY_CTRL_DELAY + 1
        End If
        If PLAY_CTRL_DELAY >= 20 Then PLAY_CTRL = False
    End Sub
#End Region

#Region "Play"
    '**************************************************************************
    ' PLAY
    '**************************************************************************
    Sub Play()
        '
        If pause_play = 1 Then GoTo Error_Play
        'If Tmr_Play_Delay.Enabled = True Then GoTo Error_Play

        'Generuje ERROR jeśli nie jest zainicjalizowany hr
        On Error GoTo Error_Sub_Play
        hr = imc.Stop() : DsError.ThrowExceptionForHR(hr)
        Marshal.ReleaseComObject(fg)

Error_Sub_Play:
        Call Stan_Pracy_DirectShow() 'Odświeża opis stanu pracy



        Call new_fg() : GoTo monoscopic_player
        Exit Sub
        '//ODTWARZACZ MONOSCOPOWY
monoscopic_player:
        'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()
        'Frm_Main.StepForwardToolStripMenuItem.Enabled = True
        'Frm_Main.StepBackToolStripMenuItem.Enabled = True
        'Frm_Main.ToolStripMenuItem13.Enabled = True
        Call resize_me()
        Call Load_5D()
        Call PLAY_MONO()
        Call wait_for_finish()
        GoTo End_Play


End_Play:
        Exit Sub
        'ERROR_PLAY
Error_Play:
        'Lst_Text = "RSP> Nack" : Call dopisz_do_lst()
        Stop
    End Sub

    Private Sub resize_me() 'Procedura wyrzucona z Sub_PLAY
        'If video_size = "C" Then 'Custom Size
        Me.WindowState = FormWindowState.Normal
        'Me.Top = video_Top : Me.Left = video_Left : Me.Height = video_Height : Me.Width = video_Width

        'End If
        'If video_size = "F" Then 'Full Screen
        'Me.WindowState = FormWindowState.Maximized
        'End If

        'Call Background_image_ReSize()
    End Sub

    Sub PLAY_MONO() 'Otwórz kanał lewy:
        On Error GoTo Error_Open_Play_Mono
        ''BUFFER
        'hr = igb.Connect(fg, "SBESource") : DsError.ThrowExceptionForHR(hr)
        'hr = ifsf.GetCurFile(Mono_File, pSourcePinOut)
        'DsError.ThrowExceptionForHR(hr)

        hr = ifg2.RenderFile(Mono_File, Nothing) : DsError.ThrowExceptionForHR(hr)
        hr = Me.ivw.put_Owner(Me.Handle) : DsError.ThrowExceptionForHR(hr)
        hr = Me.ivw.put_WindowStyle(WindowStyle.Child Or WindowStyle.ClipChildren) : DsError.ThrowExceptionForHR(hr)
        If Not (Me.ivw Is Nothing) Then 'if the videopreview is not nothing

            'Select Case monoscopic_size
            Me.ivw.SetWindowPosition(0, 0, Me.Width, Me.ClientSize.Height)
            'Case 0 : Me.ivw.SetWindowPosition(0, 0, Me.Width, Me.ClientSize.Height)
            'Case 1 : Me.ivw.SetWindowPosition(0, 0, (Me.Width / 2), Me.ClientSize.Height)
            'Case 2 : Me.ivw.SetWindowPosition((Me.Width / 2), 0, (Me.Width / 2), Me.ClientSize.Height)
            'Case 3 : GoTo toggle_Window_Position
            'End Select

            GoTo monoscopic_size02
toggle_Window_Position:
            'If toggle_window_position_flag = True Then toggle_window_position_flag = False Else toggle_window_position_flag = True
            'If toggle_window_position_flag = True Then
            'Me.ivw.SetWindowPosition(0, 0, (Me.Width / 2), Me.ClientSize.Height)
            'Else
            'Me.ivw.SetWindowPosition((Me.Width / 2), 0, (Me.Width / 2), Me.ClientSize.Height)
            'End If
monoscopic_size02:
            'hr = ivw.HideCursor(OABool.True) : DsError.ThrowExceptionForHR(hr)
        End If

        'If Actual_Audio <> "" Then
        ''hr = iba.put_Volume(-10000) : DsError.ThrowExceptionForHR(hr)
        'hr = ifg2.RenderFile(Actual_Audio, Nothing) : DsError.ThrowExceptionForHR(hr)
        'Lst_Text = "INF> Audio file: " & Actual_Audio : Call dopisz_do_lst()
        'End If

        Exit Sub

Error_Open_Play_Mono:
        'Lst_Text = "ERR> [303]: 'Monoscopic file open'" : Call dopisz_do_lst()
        'Open_Movie_File_Error_Flag = True
        Stop
    End Sub

    Sub wait_for_finish() 'START OPÓŹNIENIE PLAY
        If Open_Movie_File_Error_Flag = True Then
            Call Nieprawidlowe_pliki_Filmow()
            Exit Sub
        End If


        'If Play_XX <> 99 Then 'ADVERTISING
        'Lst_Text = "CMD> Start Delay = " + Str(start_delay) + " ms" : Call dopisz_do_lst()
        'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()
        'Lst_Text = "CMD> Volume = " & Actual_Volume : Call dopisz_do_lst()

        'Tmr_Play_Delay.Interval = start_delay
        'If Play_XX = 9 Then Tmr_Play_Delay.Interval = 10 'CONTINUS PLAY
        'Tmr_Play_Delay.Enabled = True 'START

        'Else
        Tmr_Play_Delay.Interval = 100 : Tmr_Play_Delay.Enabled = True 'START
        'End If


        'VOLUME CONTROL
        'Call MUTE_ON()

        Me.BringToFront()
    End Sub
    Private Sub Nieprawidlowe_pliki_Filmow()
        'On Error GoTo Nie_Zainicjalizowany_Filter
        hr = imc.Stop() : DsError.ThrowExceptionForHR(hr)
Nie_Zainicjalizowany_Filter:
        Marshal.ReleaseComObject(fg)
    End Sub

#End Region

#Region "RUN"
    'OPÓŹNIENIE PO INSTRUKCJI PLAY
    Private Sub Tmr_Play_Delay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tmr_Play_Delay.Tick
        'Pic_Background.Visible = False 'Ukryj Background_Image na czas projekcji

        Tmr_Stop_Enable_Flag = False

        'Call Test_Image_Hide() 'Ukryj Test_Image po uruchomieniu projekcji
        'Frm_Main.PlayToolStripMenuItem.Checked = True 'Zaznacz PLAY
        'Frm_Main.StopToolStripMenuItem.Checked = False 'Odznacz STOP

        hr = imc.Run() : DsError.ThrowExceptionForHR(hr)

        'Probuje renderowac na forme child
        'Call Frm_Video_Child.Child_run()

        Tmr_Play_Delay.Enabled = False
        'Lst_Text = "CMD> START" : Call dopisz_do_lst()
        'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()

        ''Tu uruchamiane są efekty 5D:
        'If _5D_File <> "" Then
        ''Lst_Text = "5D Go!!!" : Call dopisz_do_lst()
        'Call _5D_Load_From_File() 'Ładowanie pliku ze scenariuszem 5D
        'End If

        Tmr_Play_Delay.Enabled = False

        'If Play_XX <> 99 Then
        'Call Advertising_STOP() 'ADVERTISING
        'Frm_Main.AdvertisingSTARTToolStripMenuItem.Checked = False
        'Frm_Main.AdvertisingSTOPToolStripMenuItem.Checked = False
        'Frm_Main.AdvertisingSTARTToolStripMenuItem.Enabled = False
        'Frm_Main.AdvertisingSTOPToolStripMenuItem.Enabled = False
        'End If
        'Pic_Background.Visible = False

    End Sub
#End Region

#Region "STOP"
    '**************************************************************************
    ' STOP
    '**************************************************************************
    Sub Stop_PLAY()
        'If Tmr_Stop_Enable_Flag = True Then Exit Sub 'Zapobiega kilkukrotnym komunikatom STOP
        On Error GoTo error_stop
        'If Play_XX = 0 Then GoTo STOP_Z_TP

        ''ODCZYTANIE STANU PRACY DirectShow
        ''GetState 0-otwarty FilterGraph, 1-Pause, 2-Play
        Dim msTimeout_Integer As Integer
        hr = imc.GetState(msTimeout_Integer, FilterGraph_State) : DsError.ThrowExceptionForHR(hr)

        'GoTo wykonaj_stop
        Select Case FilterGraph_State
            Case 0 : Exit Sub
            Case 1 : GoTo wykonaj_stop
            Case 2 : GoTo wykonaj_stop
            Case Is > 2 : GoTo wykonaj_stop
        End Select
        Exit Sub
wykonaj_stop:
STOP_Z_TP:


        Tmr_Play_Delay.Enabled = False

        pause_play = 0 ': Frm_Main.PlayToolStripMenuItem.Enabled = True : Frm_Main.PauseToolStripMenuItem.Checked = False


        'On Error GoTo error_alredy_stopped

        hr = imc.Stop()
        DsError.ThrowExceptionForHR(hr)

error_alredy_stopped:
        Marshal.ReleaseComObject(fg) 'przeniesione do zegara opozniajacego

        ' On Error GoTo error_stop


        If Play_XX <> 99 Then

            'Tmr_Background_Image_Show.Enabled = True
        End If


        If Play_XX = 99 Then 'ADVERTISING
            'Pic_Advertising.Visible = True : Frm_Main.Tmr_Advertising.Enabled = True
            Play_XX = 100
        End If




        Tmr_Stop_Enable_Flag = True



        Exit Sub
error_stop:
        'Lst_Text = "RSP> Nack" : Call dopisz_do_lst()
    End Sub

#End Region

#Region "PAUSE"
    '**************************************************************************
    ' PAUSE
    '**************************************************************************
    Sub Pause()
        If Tmr_Play_Delay.Enabled = True Then GoTo Error_Pause

        On Error GoTo Error_Pause

        If pause_play = 0 Then
            'Lst_Text = "CMD> START PAUSE" : Call dopisz_do_lst()
            hr = imc.Pause() : DsError.ThrowExceptionForHR(hr)
            'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()
            pause_play = 1 ': Frm_Main.PauseToolStripMenuItem.Checked = True
        Else
            'Lst_Text = "CMD> END PAUSE" : Call dopisz_do_lst()
            hr = imc.Run() : DsError.ThrowExceptionForHR(hr)
            'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()
            pause_play = 0 ': Frm_Main.PauseToolStripMenuItem.Checked = False
            'Frm_Main.StepForwardToolStripMenuItem.Checked = False
            'Frm_Main.StepBackToolStripMenuItem.Checked = False
            'Frm_Main.ToolStripMenuItem13.Checked = False
        End If
        Exit Sub
        'ERROR PAUSE --------------------------------------
Error_Pause:
        pause_play = 0 ': Lst_Text = "RSP> Nack" : Call dopisz_do_lst()
    End Sub
#End Region

    Sub Set_Position()
        'On Error GoTo Error_Set_Position
        If Tmr_Play_Delay.Enabled = True Then Exit Sub

        Dim new_position As Double
        'If Step_Forward_Time <= 0 Then GoTo Error_Set_Position

        new_position = Step_Forward_Time / 24
        If new_position > Duration_Double Then GoTo Error_Set_Position

        hr = imc.Pause() : DsError.ThrowExceptionForHR(hr)
        hr = imp.put_CurrentPosition(new_position) : DsError.ThrowExceptionForHR(hr)

        pause_play = 1 ': Frm_Main.PauseToolStripMenuItem.Checked = True
        'Frm_Main.ToolStripMenuItem13.Checked = True
        'Frm_Main.StepForwardToolStripMenuItem.Checked = False
        'Frm_Main.StepBackToolStripMenuItem.Checked = False

        'Lst_Text = "SET POSITION= " & Step_Forward_Time : Call dopisz_do_lst()
        'Lst_Text = "RSP> Ack" : Call dopisz_do_lst()
        Exit Sub

Error_Set_Position:
        'Lst_Text = "ERR> [306]: 'SET POSITION'" : Call dopisz_do_lst()
    End Sub




End Class