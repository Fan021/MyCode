Imports System.Threading
Imports System.Windows.Forms

Public Interface IMainForm
    Property MainForm_Timer As System.Windows.Forms.Timer
    Property MainForm_CycleCounter As Long
    Property MainForm_StationOverviewInfo As Dictionary(Of Integer, structStationOverviewInfo)
    Property MainForm_ErrorMessageSet As structErrorMessageSet
    Property MainForm_PLCdisconnect As Boolean
    Property MainForm_TabControlStations As TabControl
    Property MainForm_ScheduleSelectView As Base.IScheduleUI
    Property MainForm_lblCurrentSchedule As Label
    Property MainForm_lblActualSerialNumber As Label
    Property MainForm_btnArticle As Button
    Property MainForm_lblRefPart As Label
    Property MainForm_lblMessage As Label
    Property MainForm_lblPass As Label
    Property MainForm_lblfail As Label
    Property PlcIsPoweredOn As Boolean
    Property PlcAutoManual As enumPLC_AUTO_MANUAL
    Property MainForm_lbltotal As Label
    Property MainForm_stationView As Object
    Property MainForm_btnReset As Button

    Property MainForm_btnClear As Button
    Property MainForm_btnResetFail As Button
    Property MainForm_CaqIndicator As enumINDICATOR_STATRUS
    Property MainForm_cFormFontResize As clsFormFontResize
    Property MainForm_MyTwinCat As Dictionary(Of String, TwinCatAds)
    Property MainForm_Text As String
    Property MainForm_MainLogger As ListBox
    Property MainForm_CBArticle As ComboBox
    Property MainForm_NewPartStartion As NewPartStation
    Property MainForm_LinecotrolIndicator As enumINDICATOR_STATRUS

    Event MainForm_IamClosing()
    Property cFormFontResize As clsFormFontResize

    Function MainForm_Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
    Sub MainForm_Run()
    Sub MainForm_ReadLanguage()
    Sub MainForm_Show()
    Sub MainForm_Quit()
    Sub MainForm_Dispose()
    Sub MainForm_ResetClear()
    Sub MainForm_AddClear()
    Function MainForm_InitCounterView(ByVal CounterControl As CounterController) As Boolean
    Function InvokeAction(ByVal method As System.Delegate, ByVal ParamArray args() As Object) As Object
End Interface
