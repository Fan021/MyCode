Imports System.Collections.Concurrent
Imports System.IO

Public Class clsSystemManager
    Protected cSettings As New Settings
    Private _Object As New Object
    Public Const Name As String = "SystemManager"


    Public ReadOnly Property Settings As Settings
        Get
            SyncLock _Object
                Return cSettings
            End SyncLock
        End Get
    End Property

    Public Function Init() As Boolean
        SyncLock _Object
            Try
                cSettings.Init()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function
End Class


Public Class MachineIdentifier
    Protected strTraceId As String = String.Empty
    Protected strProjectId As String = String.Empty
    Protected strLineId As String = String.Empty
    Private _Object As New Object
    Public Property TraceId() As String
        Get
            SyncLock _Object
                Return strTraceId
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strTraceId = value
            End SyncLock
        End Set
    End Property
    Public Property ProjectId() As String
        Get
            SyncLock _Object
                Return strProjectId
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strProjectId = value
            End SyncLock
        End Set
    End Property

    Public Property LineId() As String
        Get
            SyncLock _Object
                Return strLineId
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strLineId = value
            End SyncLock
        End Set
    End Property
End Class

Public Class Settings
    Protected Const strExtension_IniFile As String = ".ini"
    Protected Const strExtension_ConfigFile As String = ".ini"
    Protected Const strExtension_LoggingFile As String = ".log"
    Protected Const strExtension_VariantFile As String = ".ini"
    Protected Const strExtension_HelpFile As String = ".pdf"
    Protected Const strExtension_ApplicationFile As String = ".exe"
    Protected Const strSeparator As String = ";"
    Protected Const strCommentDelimiter As String = "'"

    Protected strConfigFile As String = String.Empty
    Protected strVariantConfig As String = String.Empty
    Protected strDevicesConfig As String = String.Empty
    Protected strExeFolder As String = String.Empty
    Protected strUserConfig As String = String.Empty
    Protected strTextListConfig As String = String.Empty
    Protected strPictureListConfig As String = String.Empty
    Protected strErrorCodeListConfig As String = String.Empty
    Protected strMessageListConfig As String = String.Empty
    Protected strIOListConfig As String = String.Empty
    Protected strCylinderListConfig As String = String.Empty
    Protected strGlobalProgramListConfig As String = String.Empty
    Protected strShortCutConfig As String = String.Empty
    Protected strProgramDebugConfig As String = String.Empty
    Protected strProgramCylinderDebugConfig As String = String.Empty
    Protected strApplicationFolder As String = String.Empty
    Protected strStatisticsFolder As String = String.Empty
    Protected strResourcesFolder As String = String.Empty
    Protected strPictureFolder As String = String.Empty
    Protected strPrinterFolder As String = String.Empty
    Protected strApplicationName As String = String.Empty
    Protected strApplicationFullName As String = String.Empty
    Protected strConfigFolder As String = String.Empty
    Protected strLogFolder As String = String.Empty
    Protected strLanguageFolder As String = String.Empty
    Protected strDeviceFolder As String = String.Empty
    Protected strActionFolder As String = String.Empty
    Protected strVariantFolder As String = String.Empty
    Protected strUserDefineFolder As String = String.Empty
    Protected strHelpFolder As String = String.Empty
    Protected strLineControlFolder As String = String.Empty
    Protected strHelpFiles As New Dictionary(Of Integer, String)
    Protected strHelpApplication As New Dictionary(Of Integer, String)
    Protected strLibFolder As String = String.Empty
    Protected strStationErrorConfig As String = String.Empty

    Public Const Name As String = "Settings"
    Property cFileHandler As New clsFileHandler
    Private _Object As New Object

#Region "Properties"


    Public ReadOnly Property StationErrorConfig As String
        Get
            SyncLock _Object
                Return strStationErrorConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GlobalProgramList As String
        Get
            SyncLock _Object
                Return strGlobalProgramListConfig
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property Extension_IniFile As String
        Get
            SyncLock _Object
                Return strExtension_IniFile
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Extension_ConfigFile As String
        Get
            SyncLock _Object
                Return strExtension_ConfigFile
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property ConfigFile As String
        Get
            SyncLock _Object
                Return strConfigFile
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property VariantConfig As String
        Get
            SyncLock _Object
                Return strVariantConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property DevicesConfig As String
        Get
            SyncLock _Object
                Return strDevicesConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ExeFolder As String
        Get
            SyncLock _Object
                Return strExeFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property UserConfig As String
        Get
            SyncLock _Object
                Return strUserConfig
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property TextListConfig As String
        Get
            SyncLock _Object
                Return strTextListConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property PictureListConfig As String
        Get
            SyncLock _Object
                Return strPictureListConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ShortCutConfig As String
        Get
            SyncLock _Object
                Return strShortCutConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ProgramDebugConfig As String
        Get
            SyncLock _Object
                Return strProgramDebugConfig
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property ProgramCylinderDebugConfig As String
        Get
            SyncLock _Object
                Return strProgramCylinderDebugConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ErrorCodeListConfig As String
        Get
            SyncLock _Object
                Return strErrorCodeListConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property MessageListConfig As String
        Get
            SyncLock _Object
                Return strMessageListConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IOListConfig As String
        Get
            SyncLock _Object
                Return strIOListConfig
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property CylinderListConfig As String
        Get
            SyncLock _Object
                Return strCylinderListConfig
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Extension_LoggingFile As String
        Get
            SyncLock _Object
                Return strExtension_LoggingFile
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Extension_VariantFile As String
        Get
            SyncLock _Object
                Return strExtension_VariantFile
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Extension_HelpFile As String
        Get
            SyncLock _Object
                Return strExtension_HelpFile
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property Separator As String
        Get
            SyncLock _Object
                Return strSeparator
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property CommentDelimiter As String
        Get
            SyncLock _Object
                Return strCommentDelimiter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ResourcesFolder As String
        Get
            SyncLock _Object
                Return strResourcesFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property PictureFolder As String
        Get
            SyncLock _Object
                Return strPictureFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property PrinterFolder As String
        Get
            SyncLock _Object
                Return strPrinterFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property LineControlFolder As String
        Get
            SyncLock _Object
                Return strLineControlFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property StatisticsFolder As String
        Get
            SyncLock _Object
                Return strStatisticsFolder
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property UserDefineFolder As String
        Get
            SyncLock _Object
                Return strUserDefineFolder
            End SyncLock
        End Get
    End Property

    Public Property ApplicationFolder As String
        Get
            SyncLock _Object
                Return strApplicationFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strApplicationFolder = value
            End SyncLock
        End Set
    End Property

    Public Property ApplicationName As String
        Get
            SyncLock _Object
                Return strApplicationName
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strApplicationName = value
            End SyncLock
        End Set
    End Property

    Public Property ApplicationFullName As String
        Get
            SyncLock _Object
                Return strApplicationFullName
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strApplicationFullName = value
            End SyncLock
        End Set
    End Property

    Public ReadOnly Property ApplicationActive As String
        Get
            SyncLock _Object
                Return strApplicationName & "_Active"
            End SyncLock
        End Get
    End Property

    Public Property ConfigFolder As String
        Get
            SyncLock _Object
                Return strConfigFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strConfigFolder = value
            End SyncLock
        End Set
    End Property


    Public Property LanguageFolder As String
        Get
            SyncLock _Object
                Return strLanguageFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strLanguageFolder = value
            End SyncLock
        End Set
    End Property

    Public Property LogFolder As String
        Get
            SyncLock _Object
                Return strLogFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strLogFolder = value
                cFileHandler.CheckFolder(strLogFolder)
                If Directory.Exists(value) Then
                    Dim directroyInfo As DirectoryInfo = New DirectoryInfo(value)
                    Dim t1 As DateTime
                    Dim t2 As DateTime
                    t2 = DateTime.Parse(Date.Now.AddDays(-60).ToString)
                    For Each tempFile As FileInfo In directroyInfo.GetFiles
                        t1 = DateTime.Parse(tempFile.CreationTime.ToString)
                        If DateTime.Compare(t1, t2) <= 0 Then
                            File.Delete(tempFile.FullName)
                        End If
                    Next
                End If
            End SyncLock
        End Set
    End Property

    Public Property VariantFolder As String
        Get
            SyncLock _Object
                Return strVariantFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strVariantFolder = value
            End SyncLock
        End Set
    End Property

    Public Property HelpFolder As String
        Get
            SyncLock _Object
                Return strHelpFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strHelpFolder = value
            End SyncLock
        End Set
    End Property

    Public Property LibFolder As String
        Get
            SyncLock _Object
                Return strLibFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strLibFolder = value
            End SyncLock
        End Set
    End Property



    Public Property DeviceFolder As String
        Get
            SyncLock _Object
                Return strDeviceFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strDeviceFolder = value
            End SyncLock
        End Set
    End Property


    Public Property ActionFolder As String
        Get
            SyncLock _Object
                Return strActionFolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strActionFolder = value
            End SyncLock
        End Set
    End Property

    Public ReadOnly Property HelpFiles As Dictionary(Of Integer, String)
        Get
            SyncLock _Object
                Try
                    Return strHelpFiles
                Catch ex As Exception
                    Return Nothing
                End Try
            End SyncLock
        End Get
    End Property


    Public Property HelpApplication As Dictionary(Of Integer, String)
        Get
            SyncLock _Object
                Return strHelpApplication
            End SyncLock
        End Get
        Set(ByVal value As Dictionary(Of Integer, String))
            SyncLock _Object
                strHelpApplication = value
            End SyncLock
        End Set
    End Property

#End Region

    Public Function Init() As Boolean
        SyncLock _Object
            Dim mFolder As String

            strApplicationFolder = ""
            strApplicationFolder = My.Application.Info.DirectoryPath

            If (strApplicationName = String.Empty) Or (strApplicationName = cFileHandler.ErrorString) Then
                strApplicationName = My.Application.Info.AssemblyName + strExtension_ApplicationFile
            End If
            strApplicationFullName = strApplicationFolder + "\" + strApplicationName

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Config")
            If mFolder = "" Then Return False
            strConfigFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\exe")
            If mFolder = "" Then Return False
            strExeFolder = mFolder

            strConfigFile = strConfigFolder + "\System" + strExtension_ConfigFile
            strVariantConfig = strConfigFolder + "\VariantList" + strExtension_ConfigFile
            strDevicesConfig = strConfigFolder + "\DevicesList" + strExtension_ConfigFile
            strUserConfig = strConfigFolder + "\UserList" + strExtension_ConfigFile
            strTextListConfig = strConfigFolder + "\TextList" + strExtension_ConfigFile
            strPictureListConfig = strConfigFolder + "\PictureList" + strExtension_ConfigFile
            strErrorCodeListConfig = strConfigFolder + "\ErrorCodeList" + strExtension_ConfigFile
            strMessageListConfig = strConfigFolder + "\MessageList" + strExtension_ConfigFile
            strIOListConfig = strConfigFolder + "\IOList" + strExtension_ConfigFile
            strCylinderListConfig = strConfigFolder + "\CylinderList" + strExtension_ConfigFile
            strShortCutConfig = strConfigFolder + "\ShortCutConfig" + strExtension_ConfigFile
            strStationErrorConfig = strConfigFolder + "\StationErrorConfig" + strExtension_ConfigFile
            strProgramDebugConfig = strConfigFolder + "\ProgramDebugConfig" + strExtension_ConfigFile
            strProgramCylinderDebugConfig = strConfigFolder + "\ProgramCylinderDebugConfig" + strExtension_ConfigFile
            strGlobalProgramListConfig = strConfigFolder + "\GlobalProgramList" + strExtension_ConfigFile
            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\log")
            If mFolder = "" Then Return False
            strLogFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Variant")
            If mFolder = "" Then Return False
            strVariantFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Lib")
            If mFolder = "" Then Return False
            strLibFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Device")
            If mFolder = "" Then Return False
            strDeviceFolder = mFolder


            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Action")
            If mFolder = "" Then Return False
            strActionFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\help")
            If mFolder = "" Then Return False
            strHelpFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Resources")
            If mFolder = "" Then Return False
            strResourcesFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Picture")
            If mFolder = "" Then Return False
            strPictureFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Statistics")
            If mFolder = "" Then Return False
            strStatisticsFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\UserDefine")
            If mFolder = "" Then Return False
            strUserDefineFolder = mFolder


            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Language")
            If mFolder = "" Then Return False
            strLanguageFolder = mFolder


            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\Printer")
            If mFolder = "" Then Return False
            strPrinterFolder = mFolder

            mFolder = cFileHandler.CheckFolder(strApplicationFolder + "\LineControl")
            If mFolder = "" Then Return False
            strLineControlFolder = mFolder

            Return True
        End SyncLock
    End Function

End Class

