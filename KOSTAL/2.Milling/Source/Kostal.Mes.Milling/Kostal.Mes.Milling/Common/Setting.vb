Option Explicit On
Imports System.Windows.Forms

Public Enum enumSettingsStatus
    OK = 0
    INCORRECT_APP_FOLDER = -1
    NO_ROOT_INI_AVAILABLE = -2
    FAIL_BY_FOLDER_CREATE = -3
End Enum

Public Class LogCfg
    Public LogEnable As Boolean = False
    Public LogPath As String = String.Empty
    Public LogLevel As enmLogType
End Class

Public Class PLCConfig
    Protected mName As String = String.Empty
    Protected dTwinCatEnable As Boolean = False
    Protected mTwinCatAmsNetId As String = String.Empty
    Protected mTwinCatPort As Integer = 0
    Protected mReadIndexGroup As Long = 0
    Protected mReadIndexOffset As Long = 0
    Protected mWriteIndexGroup As Long = 0
    Protected mWriteIndexOffset As Long = 0

    Public Property Name As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Public Property TwinCatEnable As Boolean
        Get
            Return dTwinCatEnable
        End Get
        Set(ByVal value As Boolean)
            dTwinCatEnable = value
        End Set
    End Property

    Public Property TwinCatAmsNetId As String
        Get
            Return mTwinCatAmsNetId
        End Get
        Set(ByVal value As String)
            mTwinCatAmsNetId = value
        End Set
    End Property

    Public Property TwinCatPort As Integer
        Get
            Return mTwinCatPort
        End Get
        Set(ByVal value As Integer)
            mTwinCatPort = value
        End Set
    End Property

    Public Property ReadIndexGroup As Long
        Get
            Return mReadIndexGroup
        End Get
        Set(ByVal value As Long)
            mReadIndexGroup = value
        End Set
    End Property

    Public Property ReadIndexOffset As Long
        Get
            Return mReadIndexOffset
        End Get
        Set(ByVal value As Long)
            mReadIndexOffset = value
        End Set
    End Property

    Public Property WriteIndexGroup As Long
        Get
            Return mWriteIndexGroup
        End Get
        Set(ByVal value As Long)
            mWriteIndexGroup = value
        End Set
    End Property

    Public Property WriteIndexOffset As Long
        Get
            Return mWriteIndexOffset
        End Get
        Set(ByVal value As Long)
            mWriteIndexOffset = value
        End Set
    End Property


End Class
Public Class Settings
    Protected Const mExtension_IniFile As String = ".ini"
    Protected Const mExtension_LanguageFile As String = ".lng"
    Protected Const mExtension_LoggingFile As String = ".log"
    Protected Const mExtension_VariantFile As String = ".var"
    Protected Const mExtension_HelpFile As String = ".pdf"
    Protected Const mSeparator As String = ";"
    Protected Const mCommentDelimiter As String = "'"
    Protected Const mConfigName As String = "Config.xml"

    Protected mApplicationFolder As String = String.Empty
    Protected mApplicationName As String = String.Empty
    Protected mConfigFolder As String = String.Empty
    Protected mResourceFolder As String = String.Empty
    Protected mLogFolder As String = String.Empty
    Protected mLineControlFolder As String = String.Empty
    Protected mVarFolder As String = String.Empty
    Protected _LibFolder As String = String.Empty
    Protected mLngFolder As String = String.Empty
    Protected mPrinterFolder As String = String.Empty
    Protected mPLCConfig As New PLCConfig
    Protected mLogCfg As New LogCfg
    Protected mLogBox As Messager
    Protected mLogName As String = String.Empty
    Public Const Name As String = "_mAppSettings"
    Protected mStaionCfg As New Dictionary(Of String, StationCfg)
    Protected mDataStoreCfg As New DataStoreCfg
    Protected mLineControlCfg As New LineControlCfg
    Protected mWebServiceCfg As New WebServiceCfg
    Protected mSqlDataCfg As New DataCfg
    Private mAlarmTime As String = String.Empty
    Private mShowTime As String = String.Empty
#Region "Properties"



    Public Property LogBox() As Messager
        Get
            Try
                Return mLogBox
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(ByVal value As Messager)
            mLogBox = value
        End Set
    End Property

    Public ReadOnly Property ConfigName() As String
        Get
            Return mConfigName
        End Get
    End Property

    Public Property AlarmTime() As String
        Get
            Return mAlarmTime
        End Get
        Set(ByVal value As String)
            mAlarmTime = value
        End Set
    End Property

    Public Property ShowTime() As String
        Get
            Return mShowTime
        End Get
        Set(ByVal value As String)
            mShowTime = value
        End Set
    End Property

    Public ReadOnly Property Extension_IniFile() As String
        Get
            Return mExtension_IniFile
        End Get
    End Property

    Public ReadOnly Property Extension_LanguageFile() As String
        Get
            Return mExtension_LanguageFile
        End Get
    End Property

    Public ReadOnly Property Extension_LoggingFile() As String
        Get
            Return mExtension_LoggingFile
        End Get
    End Property

    Public ReadOnly Property Extension_VariantFile() As String
        Get
            Return mExtension_VariantFile
        End Get
    End Property

    Public ReadOnly Property Extension_HelpFile() As String
        Get
            Return mExtension_HelpFile
        End Get
    End Property

    Public ReadOnly Property Separator() As String
        Get
            Return mSeparator
        End Get
    End Property

    Public ReadOnly Property CommentDelimiter() As String
        Get
            Return mCommentDelimiter
        End Get
    End Property

    Public Property LogName() As String
        Get
            Return mLogName
        End Get
        Set(ByVal value As String)
            mLogName = value
        End Set
    End Property

    Public Property ApplicationFolder() As String
        Get
            Return mApplicationFolder
        End Get
        Set(ByVal value As String)
            mApplicationFolder = value
        End Set
    End Property

    Public Property ApplicationName() As String
        Get
            Return mApplicationName
        End Get
        Set(ByVal value As String)
            mApplicationName = value
        End Set
    End Property

    Public ReadOnly Property ApplicationActive As String
        Get
            Return mApplicationName & "_Active"
        End Get
    End Property

    Public Property LogFolder() As String
        Get
            Return mLogFolder
        End Get
        Set(ByVal value As String)
            mLogFolder = value
        End Set
    End Property

    Public Property LineControlFolder() As String
        Get
            Return mLineControlFolder
        End Get
        Set(ByVal value As String)
            mLineControlFolder = value
        End Set
    End Property



    Public Property LngFolder() As String
        Get
            Return mLngFolder
        End Get
        Set(ByVal value As String)
            mLngFolder = value
        End Set
    End Property

    Public Property VarFolder() As String
        Get
            Return mVarFolder
        End Get
        Set(ByVal value As String)
            mVarFolder = value
        End Set
    End Property


    Public Property ConfigFolder() As String
        Get
            Return mConfigFolder
        End Get
        Set(ByVal value As String)
            mConfigFolder = value
        End Set
    End Property



    Public Property ResourceFolder() As String
        Get
            Return mResourceFolder
        End Get
        Set(ByVal value As String)
            mResourceFolder = value
        End Set
    End Property

    Public Property LibFolder() As String
        Get
            Return _LibFolder
        End Get
        Set(ByVal value As String)
            _LibFolder = value
        End Set
    End Property

    Public Property LogCfg As LogCfg
        Get
            Return mLogCfg
        End Get
        Set(ByVal value As LogCfg)
            mLogCfg = value
        End Set
    End Property

    Public Property PLCConfig As PLCConfig
        Get
            Return mPLCConfig
        End Get
        Set(ByVal value As PLCConfig)
            mPLCConfig = value
        End Set
    End Property

    Public Property StaionCfg As Dictionary(Of String, StationCfg)
        Get
            Return mStaionCfg
        End Get
        Set(ByVal value As Dictionary(Of String, StationCfg))
            mStaionCfg = value
        End Set
    End Property


    Public Property DataStoreCfg As DataStoreCfg
        Get
            Return mDataStoreCfg
        End Get
        Set(ByVal value As DataStoreCfg)
            mDataStoreCfg = value
        End Set
    End Property

    Public Property LineControlCfg As LineControlCfg
        Get
            Return mLineControlCfg
        End Get
        Set(ByVal value As LineControlCfg)
            mLineControlCfg = value
        End Set
    End Property

    Public Property WebServiceCfg As WebServiceCfg
        Get
            Return mWebServiceCfg
        End Get
        Set(ByVal value As WebServiceCfg)
            mWebServiceCfg = value
        End Set
    End Property

    Public Property SqlDataCfg As DataCfg
        Get
            Return mSqlDataCfg
        End Get
        Set(ByVal value As DataCfg)
            mSqlDataCfg = value
        End Set
    End Property

#End Region

End Class

Public Class SetSettings

    Protected mSettings As New Settings
    Property mFileHandler As New FileHandler
    Protected mConfigData As New ConfigData
    Protected Message As New Messager


    Public ReadOnly Property Settings() As Settings
        Get
            Return mSettings
        End Get
    End Property


    Public Function Init(Optional ByVal LoggerBox As ListBox = Nothing, Optional ByVal LogName As String = "") As Settings
        Dim mFolder As String

        mSettings.ApplicationFolder = ""
        mSettings.ApplicationFolder = My.Application.Info.DirectoryPath & "\"

        mSettings.SqlDataCfg.DBServer = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "server")
        mSettings.SqlDataCfg.DBPort = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "Port")
        mSettings.SqlDataCfg.DBUserName = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "UserName")
        mSettings.SqlDataCfg.DBPassWord = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "PassWord")
        mSettings.SqlDataCfg.DBName = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "Name")
        mSettings.SqlDataCfg.DBUserTable = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "UserTable")
        mSettings.SqlDataCfg.DBConfigTable = mFileHandler.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "ConfigTable")
        'mConfigData.Init(mSettings.SqlDataCfg)


        mFolder = CheckFolder("log")
        If mFolder = "" Then Return Nothing
        mSettings.LogFolder = mFolder


        mFolder = CheckFolder("Lib")
        If mFolder = "" Then Return Nothing
        mSettings.LibFolder = mFolder

        mFolder = CheckFolder("Language")
        If mFolder = "" Then Return Nothing
        mSettings.LngFolder = mFolder

        mFolder = CheckFolder("Resource")
        If mFolder = "" Then Return Nothing
        mSettings.ResourceFolder = mFolder

        mFolder = CheckFolder("Config")
        If mFolder = "" Then Return Nothing
        mSettings.ConfigFolder = mFolder

        mFolder = CheckFolder("LineControl")
        If mFolder = "" Then Return Nothing
        mSettings.LineControlFolder = mFolder

        'If Not mFileHandler.FileExist(mSettings.ConfigFolder & mSettings.ConfigName) Then
        '    Throw New Exception("File Not Exist " + mSettings.ApplicationFolder & mSettings.ConfigName)
        '    Return Nothing
        'End If
        If (mSettings.ApplicationName = String.Empty) Or (mSettings.ApplicationName = mFileHandler.ErrorString) Then
            mSettings.ApplicationName = My.Application.Info.AssemblyName
        End If


        If Not IsNothing(LoggerBox) Then
            mSettings.LogBox = New Messager
            mSettings.LogBox.Construct(LoggerBox)
        Else
            mSettings.LogBox = Nothing
        End If

        If LogName <> "" Then
            mSettings.LogName = LogName
        Else
            mSettings.LogName = mSettings.ApplicationName
        End If
        ' If Not MapNeworkDrives() Then Return Nothing


        'ReadPLCConfig()
        'ReadStationConfig()
        'ReadDataStoreConfig()
        'ReadWebServiceConfig()
        'ReadLogConfig()
        'ReadLineControlConfig()
        Return mSettings

    End Function

    Protected Function ReadPLCConfig() As Boolean
        Dim sResult As String

        sResult = mConfigData.GetItemValue("Twincat.AmsNetId")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid TwinCatAmsNetId. Please Add Kostal.Mes.Milling.Config.exe--AmsNetId")
            Return False
        End If
        mSettings.PLCConfig.TwinCatAmsNetId = sResult

        sResult = mConfigData.GetItemValue("Twincat.TwinCatPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid TwinCatPort. Please Add Kostal.Mes.Milling.Config.exe--TwinCatPort")
            Return False
        End If
        mSettings.PLCConfig.TwinCatPort = CInt(sResult)

        sResult = mConfigData.GetItemValue("Twincat.ReadIndexGroup")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid ReadIndexGroup. Please Add Kostal.Mes.Milling.Config.exe--ReadIndexGroup")
            Return False
        End If
        mSettings.PLCConfig.ReadIndexGroup = CLng(sResult)

        sResult = mConfigData.GetItemValue("Twincat.ReadIndexOffset")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid ReadIndexOffset. Please Add Kostal.Mes.Milling.Config.exe--ReadIndexOffset")
            Return False
        End If
        mSettings.PLCConfig.ReadIndexOffset = CLng(sResult)

        sResult = mConfigData.GetItemValue("Twincat.WriteIndexGroup")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid WriteIndexGroup. Please Add Kostal.Mes.Milling.Config.exe--WriteIndexGroup")
            Return False
        End If
        mSettings.PLCConfig.WriteIndexGroup = CLng(sResult)

        sResult = mConfigData.GetItemValue("Twincat.WriteIndexOffset")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid WriteIndexOffset Please Add Kostal.Mes.Milling.Config.exe--WriteIndexOffset")
            Return False
        End If
        mSettings.PLCConfig.WriteIndexOffset = CLng(sResult)

        Return True
    End Function

    Protected Function ReadWebServiceConfig() As Boolean
        Dim sResult As String

        sResult = mConfigData.GetItemValue("MES.WebserviceUrl")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid WebserviceUrl. Please Add Kostal.Mes.Milling.Config.exe--WebserviceUrl")
            Return False
        End If
        mSettings.WebServiceCfg.Url = sResult
        sResult = mConfigData.GetItemValue("MES.WebserviceUrl2")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid WebserviceUrl. Please Add Kostal.Mes.Milling.Config.exe--WebserviceUrl2")
            Return False
        End If
        mSettings.WebServiceCfg.Url2 = sResult

        sResult = mConfigData.GetItemValue("MES.LoginName")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid LoginName. Please Add Kostal.Mes.Milling.Config.exe--LoginName")
            Return False
        End If
        mSettings.WebServiceCfg.UserName = sResult

        sResult = mConfigData.GetItemValue("MES.LoginPassword")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid LoginPassword. Please Add Kostal.Mes.Milling.Config.exe--LoginPassword")
            Return False
        End If
        mSettings.WebServiceCfg.PassWord = sResult

        sResult = mConfigData.GetItemValue("MES.Resource")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Resource. Please Add Kostal.Mes.Milling.Config.exe--Resource")
            Return False
        End If
        mSettings.WebServiceCfg.ResourceId = sResult

        sResult = mConfigData.GetItemValue("MES.Operation")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Operation. Please Add Kostal.Mes.Milling.Config.exe--Operation")
            Return False
        End If
        mSettings.WebServiceCfg.OperationId = sResult

        sResult = mConfigData.GetItemValue("MES.Timeout")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Timeout. Please Add Kostal.Mes.Milling.Config.exe--Timeout")
            Return False
        End If
        mSettings.WebServiceCfg.Timeout = CInt(sResult)

        sResult = mConfigData.GetItemValue("MES.PassiveModeEnable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid PassiveMode. Please Add Kostal.Mes.Milling.Config.exe--PassiveMode")
            Return False
        End If
        mSettings.WebServiceCfg.PassiveMode = IIf(sResult.ToUpper = "TRUE", True, False)

        sResult = mConfigData.GetItemValue("MES.MESEnable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Enable. Please Add Kostal.Mes.Milling.Config.exe--Enable")
            Return False
        End If
        mSettings.WebServiceCfg.Enable = IIf(sResult.ToUpper = "TRUE", True, False)

        Return True
    End Function

    Protected Function ReadLogConfig() As Boolean
        Dim sResult As String

        sResult = "TRUE"
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid LogEnable. Please Add Kostal.Mes.Milling.Config.exe--LogEnable")
            Return False
        End If
        mSettings.LogCfg.LogEnable = IIf(sResult.ToUpper = "TRUE", True, False)

        sResult = ""
        mSettings.LogCfg.LogPath = sResult
        sResult = ChangeStringToLogType(mConfigData.GetItemValue("MES.LogLevel"))
        mSettings.LogCfg.LogLevel = sResult
        Return True
    End Function

    Public Function ChangeStringToLogType(ByVal Type As String) As enmLogType
        For Each TypeElemet As enmLogType In [Enum].GetValues(GetType(enmLogType))
            If [Enum].GetName(GetType(enmLogType), TypeElemet) = Type Then
                Return TypeElemet
            End If
        Next
        Return enmLogType.AllLog
    End Function

    Protected Function ReadDataStoreConfig() As Boolean
        Dim sResult As String

        sResult = mConfigData.GetItemValue("SQLBox.DBServer")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBServer. Please Add Kostal.Mes.Milling.Config.exe--DBServer")
            Return False
        End If
        mSettings.DataStoreCfg.DBServer = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBPort. Please Add Kostal.Mes.Milling.Config.exe--DBPort")
            Return False
        End If
        mSettings.DataStoreCfg.DBPort = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBUserName")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBUserName. Please Add Kostal.Mes.Milling.Config.exe--DBUserName")
            Return False
        End If
        mSettings.DataStoreCfg.DBUserName = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBPassWord")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBPassWord. Please Add Kostal.Mes.Milling.Config.exe--DBPassWord")
            Return False
        End If
        mSettings.DataStoreCfg.DBPassWord = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBName")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBName. Please Add Kostal.Mes.Milling.Config.exe--DBName")
            Return False
        End If
        mSettings.DataStoreCfg.DBName = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBTable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBTable. Please Add Kostal.Mes.Milling.Config.exe--DBTable")
            Return False
        End If
        mSettings.DataStoreCfg.DBTable = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBArticleTable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBArticleTable. Please Add Kostal.Mes.Milling.Config.exe--DBArticleTable")
            Return False
        End If
        mSettings.DataStoreCfg.DBArticleTable = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBLinecontrolTable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBLinecontrolTable. Please Add Kostal.Mes.Milling.Config.exe--DBLinecontrolTable")
            Return False
        End If
        mSettings.DataStoreCfg.DBLinecontrolTable = sResult

        sResult = mConfigData.GetItemValue("SQLBox.DBSMTTable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBSMTTable. Please Add Kostal.Mes.Milling.Config.exe--DBSMTTable")
            Return False
        End If
        mSettings.DataStoreCfg.DBSMTTable = sResult

        sResult = mConfigData.GetItemValue("SQLBox.WaitingTime")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBSMTTable. Please Add Kostal.Mes.Milling.Config.exe--WaitingTime")
            Return False
        End If
        mSettings.AlarmTime = sResult


        mSettings.DataStoreCfg.DBBITTable = "BIT"
        mSettings.DataStoreCfg.DBPLCTable = "PLCAddress"


        sResult = mConfigData.GetItemValue("SQLBox.ShowTime")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DBSMTTable. Please Add Kostal.Mes.Milling.Config.exe--ShowTime")
            Return False
        End If
        mSettings.ShowTime = sResult

        Return True
    End Function

    Protected Function ReadLineControlConfig() As Boolean
        Dim sResult As String

        sResult = mConfigData.GetItemValue("LineControl.DefaultLR")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DefaultLR. Please Add Kostal.Mes.Milling.Config.exe--DefaultLR")
            Return False
        End If
        mSettings.LineControlCfg.strDefaultLR = sResult

        sResult = mConfigData.GetItemValue("LineControl.TraceId")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid TraceId. Please Add Kostal.Mes.Milling.Config.exe--TraceId")
            Return False
        End If
        mSettings.LineControlCfg.strTraceId = sResult

        sResult = mConfigData.GetItemValue("LineControl.PreviousTest")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid PreviousTest. Please Add Kostal.Mes.Milling.Config.exe--PreviousTest")
            Return False
        End If
        mSettings.LineControlCfg.strPreviousTest = sResult

        sResult = mConfigData.GetItemValue("LineControl.CurrentTest")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid CurrentTest. Please Add Kostal.Mes.Milling.Config.exe--CurrentTest")
            Return False
        End If
        mSettings.LineControlCfg.strCurrentTest = sResult

        sResult = mConfigData.GetItemValue("LineControl.DefalutSection")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid DefalutSection. Please Add Kostal.Mes.Milling.Config.exe--DefalutSection")
            Return False
        End If
        mSettings.LineControlCfg.strDefalutSection = sResult

        sResult = mConfigData.GetItemValue("LineControl.LineControlEnable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Enable. Please Add Kostal.Mes.Milling.Config.exe--Enable")
            Return False
        End If
        mSettings.LineControlCfg.bEnable = IIf(sResult.ToUpper = "TRUE", True, False)

        Return True
    End Function

    Protected Function ReadStationConfig() As Boolean

        Dim sResult As String
        Dim StaionCfg As New StationCfg

        StaionCfg.Name = "Left"

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftType")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Type. Please Add Kostal.Mes.Milling.Config.exe--Type")
            Return False
        End If
        If sResult = "LAN" Then StaionCfg.DeviceConfig.Type = InterfaceType.LAN
        If sResult = "RS232" Then StaionCfg.DeviceConfig.Type = InterfaceType.RS232
        If sResult <> "LAN" And sResult <> "RS232" Then
            Throw New Exception("Invalid Type. Please Add Kostal.Mes.Milling.Config.exe--Type")
            Return False
        End If

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftIP")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid IP. Please Add Kostal.Mes.Milling.Config.exe--IP")
            Return False
        End If
        StaionCfg.DeviceConfig.IP = sResult


        sResult = mConfigData.GetItemValue("ScannerLeft.LeftIPPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid LeftStationIPPort. Please Add Kostal.Mes.Milling.Config.exe--IPPort")
            Return False
        End If
        StaionCfg.DeviceConfig.Port = sResult


        sResult = mConfigData.GetItemValue("ScannerLeft.LeftPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Port. Please Add Kostal.Mes.Milling.Config.exe--Port")
            Return False
        End If
        StaionCfg.DeviceConfig.RS232Port = sResult


        sResult = mConfigData.GetItemValue("ScannerLeft.LeftBandrate")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Bandrate. Please Add Kostal.Mes.Milling.Config.exe--Bandrate")
            Return False
        End If
        StaionCfg.DeviceConfig.BaudRate = sResult

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftParity")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Parity. Please Add Kostal.Mes.Milling.Config.exe--Parity")
            Return False
        End If

        Select Case sResult.ToUpper
            Case "N"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.None
            Case "E"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.Even
            Case "O"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.Odd
            Case Else
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.None
        End Select

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftDatabits")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Databits. Please Add Kostal.Mes.Milling.Config.exe--Databits")
            Return False
        End If
        StaionCfg.DeviceConfig.DataBits = sResult

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftStopbit")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Stopbit. Please Add Kostal.Mes.Milling.Config.exe--Stopbit")
            Return False
        End If
        StaionCfg.DeviceConfig.StopBits = sResult

        sResult = mConfigData.GetItemValue("ScannerLeft.LeftEnable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Enable. Please Add Kostal.Mes.Milling.Config.exe--Enable")
            Return False
        End If
        StaionCfg.DeviceConfig.Enable = IIf(sResult.ToUpper = "TRUE", True, False)
        mSettings.StaionCfg.Add(StaionCfg.Name, StaionCfg)


        StaionCfg = New StationCfg

        StaionCfg.Name = "Right"

        sResult = mConfigData.GetItemValue("ScannerRight.RightType")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Type. Please Add Kostal.Mes.Milling.Config.exe--Type")
            Return False
        End If
        If sResult = "LAN" Then StaionCfg.DeviceConfig.Type = InterfaceType.LAN
        If sResult = "RS232" Then StaionCfg.DeviceConfig.Type = InterfaceType.RS232
        If sResult <> "LAN" And sResult <> "RS232" Then
            Throw New Exception("Invalid Type. Please Add Kostal.Mes.Milling.Config.exe--Type")
            Return False
        End If

        sResult = mConfigData.GetItemValue("ScannerRight.RightIP")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid IP. Please Add Kostal.Mes.Milling.Config.exe--IP")
            Return False
        End If
        StaionCfg.DeviceConfig.IP = sResult


        sResult = mConfigData.GetItemValue("ScannerRight.RightIPPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid LeftStationIPPort. Please Add Kostal.Mes.Milling.Config.exe--IPPort")
            Return False
        End If
        StaionCfg.DeviceConfig.Port = sResult


        sResult = mConfigData.GetItemValue("ScannerRight.RightPort")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Port. Please Add Kostal.Mes.Milling.Config.exe--Port")
            Return False
        End If
        StaionCfg.DeviceConfig.RS232Port = sResult


        sResult = mConfigData.GetItemValue("ScannerRight.RightBandrate")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Bandrate. Please Add Kostal.Mes.Milling.Config.exe--Bandrate")
            Return False
        End If
        StaionCfg.DeviceConfig.BaudRate = sResult

        sResult = mConfigData.GetItemValue("ScannerRight.RightParity")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Parity. Please Add Kostal.Mes.Milling.Config.exe--Parity")
            Return False
        End If

        Select Case sResult.ToUpper
            Case "N"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.None
            Case "E"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.Even
            Case "O"
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.Odd
            Case Else
                StaionCfg.DeviceConfig.Parity = IO.Ports.Parity.None
        End Select

        sResult = mConfigData.GetItemValue("ScannerRight.RightDatabits")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Databits. Please Add Kostal.Mes.Milling.Config.exe--Databits")
            Return False
        End If
        StaionCfg.DeviceConfig.DataBits = sResult

        sResult = mConfigData.GetItemValue("ScannerRight.RightStopbit")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Stopbit. Please Add Kostal.Mes.Milling.Config.exe--Stopbit")
            Return False
        End If
        StaionCfg.DeviceConfig.StopBits = sResult

        sResult = mConfigData.GetItemValue("ScannerRight.RightEnable")
        If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
            Throw New Exception("Invalid Enable. Please Add Kostal.Mes.Milling.Config.exe--Enable")
            Return False
        End If
        StaionCfg.DeviceConfig.Enable = IIf(sResult.ToUpper = "TRUE", True, False)
        mSettings.StaionCfg.Add(StaionCfg.Name, StaionCfg)

        For Each element As StationCfg In mSettings.StaionCfg.Values
            sResult = mConfigData.GetItemValue("Twincat." + element.Name + "ReadIO")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid " + element.Name + "ReadIO. Please Add Kostal.Mes.Milling.Config.exe--" + element.Name + "ReadIO")
                Return False
            End If
            element.ReadIO = sResult

            sResult = mConfigData.GetItemValue("Twincat." + element.Name + "WriteIO")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid " + element.Name + "WriteIO. Please Add Kostal.Mes.Milling.Config.exe--" + element.Name + "WriteIO")
                Return False
            End If
            element.WriteIO = sResult

            element.DelayTime = mConfigData.GetItemValue("SQLBox.DelayTime")

            sResult = mConfigData.GetItemValue("PCBBarcode.StartChar")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid StartChar. Please Add Kostal.Mes.Milling.Config.exe--StartChar")
                Return False
            End If
            element.DeviceConfig.DataFrameSTX = Chr(Convert.ToInt16(sResult, 16))

            sResult = mConfigData.GetItemValue("PCBBarcode.StopChar")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid StopChar. Please Add Kostal.Mes.Milling.Config.exe--StopChar")
                Return False
            End If
            element.DeviceConfig.DataFrameEXT = Chr(Convert.ToInt16(sResult, 16))

            sResult = mConfigData.GetItemValue("PCBBarcode.DataLength")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid DataLength. Please Add Kostal.Mes.Milling.Config.exe--DataLength")
                Return False
            End If
            element.BarLength = CInt(sResult)

            sResult = mConfigData.GetItemValue("PCBBarcode.SMTDataLength")
            If sResult = ConfigData.s_DEFAULT Or sResult = ConfigData.s_Null Then
                Throw New Exception("Invalid SMTDataLength. Please Add Kostal.Mes.Milling.Config.exe--SMTDataLength")
                Return False
            End If
            element.SMTBarLength = CInt(sResult)

        Next
        Return True
    End Function

    Protected Function CheckFolder(ByVal FolderName As String) As String
        Dim Result As String

        Result = "\" & FolderName
        If Left(Result, 1) = "\" Then
            Result = Right(Result, Len(Result) - 1)
            If Not My.Computer.FileSystem.DirectoryExists(mSettings.ApplicationFolder & Result) Then
                Try
                    My.Computer.FileSystem.CreateDirectory(mSettings.ApplicationFolder & Result)
                    Result = mSettings.ApplicationFolder & Result & "\"
                    Return Result
                Catch ex As Exception
                    Throw New Exception("Fail by " & FolderName & " Folder create : " & Result & mFileHandler.LogDelimiter & ex.ToString)
                    Return ""
                End Try
            Else
                Result = mSettings.ApplicationFolder & Result & "\"
                Return Result
            End If
        Else
            If Not My.Computer.FileSystem.DirectoryExists(Result) Then
                Try
                    My.Computer.FileSystem.CreateDirectory(Result)
                    Result = Result & "\"
                    Return Result
                Catch ex As Exception
                    Throw New Exception("Fail by " & FolderName & " Folder create : " & Result & mFileHandler.LogDelimiter & ex.ToString)
                    Return ""
                End Try
            Else
                Result = Result & "\"
                Return Result
            End If
        End If

        Return ""

    End Function



    Protected Function ChangePath(ByVal mPath As String) As String
        Dim dPath() = mPath.Split(CChar("\"))
        Dim mNewPath As String = ""
        For i = 0 To dPath.Length - 6
            mNewPath = mNewPath + dPath(i) + "\"
        Next
        Return mNewPath
    End Function

End Class

