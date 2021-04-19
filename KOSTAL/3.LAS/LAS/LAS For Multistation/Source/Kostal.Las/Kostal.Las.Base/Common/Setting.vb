Option Explicit On
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

Public Enum enumSettingsStatus
    OK = 0
    INCORRECT_APP_FOLDER = -1
    NO_ROOT_INI_AVAILABLE = -2
    FAIL_BY_FOLDER_CREATE = -3
End Enum

Public Class MachineIdentifier
    Protected _TraceId As String = String.Empty
    Protected _ProjectId As String = String.Empty
    Protected _LineId As String = String.Empty
    Public Property TraceId() As String
        Get
            Return _TraceId
        End Get
        Set(ByVal value As String)
            _TraceId = value
        End Set
    End Property
    Public Property ProjectId() As String
        Get
            Return _ProjectId
        End Get
        Set(ByVal value As String)
            _ProjectId = value
        End Set
    End Property

    Public Property LineId() As String
        Get
            Return _LineId
        End Get
        Set(ByVal value As String)
            _LineId = value
        End Set
    End Property
End Class

Public Class PLCConfig
    Protected mName As String = String.Empty
    Protected dTwinCatEnable As Boolean = False
    Protected mTwinCatAmsNetId As String = String.Empty
    Protected mTwinCatPort As Integer = 0
    Protected mUserDefine As Boolean
    Protected iPLCVersion As Double = 0
    Protected eLineType As enumLineType
    Public Property Name As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Public Property LineType As enumLineType
        Get
            Return eLineType
        End Get
        Set(ByVal value As enumLineType)
            eLineType = value
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

    Public Property UserDefine As Boolean
        Get
            Return mUserDefine
        End Get
        Set(ByVal value As Boolean)
            mUserDefine = value
        End Set
    End Property

    Public Property PLCVersion As Double
        Get
            Return iPLCVersion
        End Get
        Set(ByVal value As Double)
            iPLCVersion = value
        End Set
    End Property

End Class

Public Class ArticleAndMappingFile
    Protected _ArticleFile As String
    Protected _MappingFile As String
    Public Sub New(ByVal mArticleFile As String, ByVal mMappingFile As String)
        _ArticleFile = mArticleFile
        _MappingFile = mMappingFile
    End Sub

    Property ArticleFile As String
        Set(ByVal value As String)
            _ArticleFile = value
        End Set
        Get
            Return _ArticleFile
        End Get
    End Property

    Property MappingFile As String
        Set(ByVal value As String)
            _MappingFile = value
        End Set
        Get
            Return _MappingFile
        End Get
    End Property

End Class

Public Enum enumLineType
    ManualLine = 0
    BoschLine = 1
    MultiLine = 2
End Enum
Public Class Settings
    Protected Const mRootIniName As String = "root.xml"
    Protected Const mRootLogName As String = "root.log"
    Protected Const mExtension_IniFile As String = ".ini"
    Protected Const mExtension_LanguageFile As String = ".lng"
    Protected Const mExtension_LoggingFile As String = ".log"
    Protected Const mExtension_VariantFile As String = ".var"
    Protected Const mExtension_HelpFile As String = ".pdf"
    Protected Const mSeparator As String = ";"
    Protected Const mCommentDelimiter As String = "'"

    Protected mApplicationFolder As String = String.Empty
    Protected mApplicationName As String = String.Empty
    Protected mConfigFolder As String = String.Empty
    Protected mConfigName As String = "LAS.xml"
    Protected mLogFolder As String = String.Empty
    Protected mLngFolder As String = String.Empty
    Protected mPicFolder As String = String.Empty
    Protected mSkinFolder As String = String.Empty
    Protected mVarFolder As String = String.Empty
    Protected mPrinterFolder As String = String.Empty
    Protected mHelpFolder As String = String.Empty
    Protected mMesFolder As String = String.Empty
    Protected eLineType As enumLineType
    Protected mHelpFiles As New Dictionary(Of Integer, String)
    Protected mHelpApplication As New Dictionary(Of Integer, String)
    Protected mRoboterHelpFile As String = String.Empty
    Protected mPLCConfig As New Dictionary(Of String, PLCConfig)
    Public mForm As Form

    Protected mLogBox As Messager

    Protected _CounterFolder As String = String.Empty
    Protected _LineControlFolder As String = String.Empty
    Protected _LibFolder As String = String.Empty
    Protected _ScheduleFolder As String = String.Empty
    Protected _MechatronicFolder As String = String.Empty

    Protected mStatus As enumSettingsStatus
    Protected mArticleAndMappingFile As New List(Of ArticleAndMappingFile)
    Protected mCsvMapperFile As New List(Of String)
    Protected mScheduleFile As String
    Protected mMachineIdentifier As New MachineIdentifier
    Public Const Name As String = "_mAppSettings"

#Region "Properties"

    Public Property LineType As enumLineType
        Get
            Return eLineType
        End Get
        Set(ByVal value As enumLineType)
            eLineType = value
        End Set
    End Property

    Public Property MachineIdentifier() As MachineIdentifier
        Get
            Return mMachineIdentifier

        End Get
        Set(ByVal value As MachineIdentifier)
            mMachineIdentifier = value
        End Set
    End Property

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


    Public ReadOnly Property RootIniName() As String
        Get
            Return mRootIniName
        End Get
    End Property

    Public ReadOnly Property RootLogName() As String
        Get
            Return mRootLogName
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

    Public Property Status() As enumSettingsStatus
        Get
            Return mStatus
        End Get
        Set(ByVal value As enumSettingsStatus)
            mStatus = value
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

    Public Property ConfigFolder() As String
        Get
            Return mConfigFolder
        End Get
        Set(ByVal value As String)
            mConfigFolder = value
        End Set
    End Property

    Public Property ConfigName() As String
        Get
            Return mConfigName
        End Get
        Set(ByVal value As String)
            mConfigName = value
        End Set
    End Property


    Public Property LogFolder() As String
        Get
            Return mLogFolder
        End Get
        Set(ByVal value As String)
            mLogFolder = value
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

    Public Property PicFolder() As String
        Get
            Return mPicFolder
        End Get
        Set(ByVal value As String)
            mPicFolder = value
        End Set
    End Property

    Public Property SkinFolder() As String
        Get
            Return mSkinFolder
        End Get
        Set(ByVal value As String)
            mSkinFolder = value
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


    Public Property ScheduleFolder() As String
        Get
            Return _ScheduleFolder
        End Get
        Set(ByVal value As String)
            _ScheduleFolder = value
        End Set
    End Property


    Public Property MechatronicFolder() As String
        Get
            Return _MechatronicFolder
        End Get
        Set(ByVal value As String)
            _MechatronicFolder = value
        End Set
    End Property


    Public Property PrinterFolder() As String
        Get
            Return mPrinterFolder
        End Get
        Set(ByVal value As String)
            mPrinterFolder = value
        End Set
    End Property

    Public Property HelpFolder() As String
        Get
            Return mHelpFolder
        End Get
        Set(ByVal value As String)
            mHelpFolder = value
        End Set
    End Property


    Public Property CounterFolder() As String
        Get
            Return _CounterFolder
        End Get
        Set(ByVal value As String)
            _CounterFolder = value
        End Set
    End Property


    Public Property LineControlFolder() As String
        Get
            Return _LineControlFolder
        End Get
        Set(ByVal value As String)
            _LineControlFolder = value
        End Set
    End Property


    Public Property MesFolder As String
        Get
            Return _LibFolder
        End Get
        Set(ByVal value As String)
            _LibFolder = value
        End Set
    End Property

    Public Property LibFolder() As String
        Get
            Return mMesFolder
        End Get
        Set(ByVal value As String)
            mMesFolder = value
        End Set
    End Property

    Public ReadOnly Property HelpFiles() As Dictionary(Of Integer, String)
        Get
            Try
                Return mHelpFiles
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property


    Public Property HelpApplication() As Dictionary(Of Integer, String)
        Get
            Return mHelpApplication
        End Get
        Set(ByVal value As Dictionary(Of Integer, String))
            mHelpApplication = value
        End Set
    End Property


    Public Property RoboterHelpFile() As String
        Get
            Return mRoboterHelpFile
        End Get
        Set(ByVal value As String)
            mRoboterHelpFile = value
        End Set
    End Property

    Public Property ArticleAndMappingFile As List(Of ArticleAndMappingFile)
        Get
            Return mArticleAndMappingFile
        End Get
        Set(ByVal value As List(Of ArticleAndMappingFile))
            mArticleAndMappingFile = value
        End Set
    End Property

    Public Property ScheduleFile() As String
        Get
            Return mScheduleFile
        End Get
        Set(ByVal value As String)
            mScheduleFile = value
        End Set
    End Property

    Public Property PLCConfig As Dictionary(Of String, PLCConfig)
        Get
            Return mPLCConfig
        End Get
        Set(ByVal value As Dictionary(Of String, PLCConfig))
            mPLCConfig = value
        End Set
    End Property
#End Region

End Class

Public Class SetSettings

    Protected mSettings As New Settings
    Protected xmlHandler As New XmlHandler
    Property mFileHandler As New FileHandler
    Protected MapNetworkDrive As New NetConnect
    Protected Message As New Messager

    Public ReadOnly Property Settings() As Settings
        Get
            Return mSettings
        End Get
    End Property


    Public Function Init() As Settings
        Dim mFolder As String

        mSettings.ApplicationFolder = ""
        mSettings.ApplicationFolder = My.Application.Info.DirectoryPath & "\"

        If Not mFileHandler.FileExist(mSettings.ApplicationFolder & mSettings.RootIniName) Then
            mSettings.Status = enumSettingsStatus.NO_ROOT_INI_AVAILABLE
            mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, mSettings.Status.ToString)
            Throw New Exception("File Not Exist " + mSettings.ApplicationFolder & mSettings.RootIniName)
            Return Nothing
        End If

        mSettings.ApplicationName = xmlHandler.GetSectionInformation(mSettings.ApplicationFolder, mSettings.RootIniName, "Project", "Name")

        If (mSettings.ApplicationName = String.Empty) Or (mSettings.ApplicationName = mFileHandler.ErrorString) Then
            mSettings.ApplicationName = My.Application.Info.AssemblyName
        End If



        ' If Not MapNeworkDrives() Then Return Nothing

        mFolder = CheckFolder("Config")
        If mFolder = "" Then Return Nothing
        mSettings.ConfigFolder = mFolder

        mFolder = CheckFolder("log")
        If mFolder = "" Then Return Nothing
        mSettings.LogFolder = mFolder

        mFolder = CheckFolder("lng")
        If mFolder = "" Then Return Nothing
        mSettings.LngFolder = mFolder

        mFolder = CheckFolder("Picture")
        If mFolder = "" Then Return Nothing
        mSettings.PicFolder = mFolder

        mFolder = CheckFolder("var")
        If mFolder = "" Then Return Nothing
        mSettings.VarFolder = mFolder

        mFolder = CheckFolder("Schedule")
        If mFolder = "" Then Return Nothing
        mSettings.ScheduleFolder = mFolder

        mFolder = CheckFolder("Lib")
        If mFolder = "" Then Return Nothing
        mSettings.LibFolder = mFolder


        mFolder = CheckFolder("help")
        If mFolder = "" Then Return Nothing
        mSettings.HelpFolder = mFolder


        mFolder = CheckFolder("Mesxml")
        If mFolder = "" Then Return Nothing
        mSettings.MesFolder = mFolder

        mFolder = CheckFolder("printer")
        If mFolder = "" Then Return Nothing
        mSettings.PrinterFolder = mFolder

        mFolder = CheckFolder("Counter")
        If mFolder = "" Then Return Nothing
        mSettings.CounterFolder = mFolder

        mFolder = CheckFolder("LineControl")
        If mFolder = "" Then Return Nothing
        mSettings.LineControlFolder = mFolder

        'mFolder = CheckFolder("PositionMask")
        'If mFolder = "" Then Return Nothing
        'mSettings.PositionMaskFolder = mFolder

        mFolder = CheckFolder("Skin")
        If mFolder = "" Then Return Nothing
        mSettings.SkinFolder = mFolder


        ReadPLCVersion()
        ReadHelpApplication()
        ReadMachineIdentifier()
        ReadCsvMapperRelatedSettings()
        If mSettings.LineType >= 1 Then ReadScheduleCsvsSettings()
        Return mSettings

    End Function


    Public Function InitLogger(ByVal LoggerBox As ListBox, ByVal MainForm As IMainForm) As Boolean
        If Not IsNothing(LoggerBox) Then
            mSettings.LogBox = New Messager
            mSettings.LogBox.Construct(LoggerBox)
            mSettings.LogBox.Construct(MainForm)
        Else
            mSettings.LogBox = Nothing
        End If
        Return True
    End Function

    Protected Function ReadPLCVersion() As Boolean
        Dim tempPLCConfig As New PLCConfig
        mSettings.PLCConfig.Clear()
        mSettings.LineType = enumLineType.ManualLine
        For Each element As Dictionary(Of String, Object) In xmlHandler.GetAnyListFromXml(mSettings.ConfigFolder, mSettings.ConfigName, "PLCs", "PLC", New String() {"Name", "TwinCatEnable", "TwinCatAmsNetId", "TwinCatPort", "UserDefine", "PLCVersion", "LineType"})
            tempPLCConfig = New PLCConfig
            If CType(element("Name"), String) = XmlHandler.s_DEFAULT Then
                Throw New Exception("PLCs Name undefined" + " value:" + CType(element("Name"), String))
                Return False
            End If
            tempPLCConfig.Name = CType(element("Name"), String)

            If CType(element("TwinCatEnable"), String) = XmlHandler.s_DEFAULT Then
                Throw New Exception("PLCs TwinCatEnable undefined" + " value:" + CType(element("TwinCatEnable"), String))
                Return False
            End If
            tempPLCConfig.TwinCatEnable = CBool(IIf(element("TwinCatEnable").ToString.ToUpper = "TRUE", True, False))

            If CType(element("TwinCatAmsNetId"), String) = XmlHandler.s_DEFAULT Then
                Throw New Exception("PLCs TwinCatAmsNetId undefined" + " value:" + CType(element("TwinCatAmsNetId"), String))
                Return False
            End If
            tempPLCConfig.TwinCatAmsNetId = CType(element("TwinCatAmsNetId"), String)
            tempPLCConfig.LineType = CType([Enum].Parse(GetType(enumLineType), CType(element("LineType"), String)), enumLineType)
            If tempPLCConfig.LineType > 0 Then
                mSettings.LineType = tempPLCConfig.LineType
            End If

            Try

                If CType(element("TwinCatPort"), String) = XmlHandler.s_DEFAULT Then
                    Throw New Exception("PLCs TwinCatPort undefined" + " value:" + CType(element("TwinCatPort"), String))
                    Return False
                End If
                tempPLCConfig.TwinCatPort = CInt(element("TwinCatPort"))
            Catch
                Throw New Exception("PLCs TwinCatPort undefined" + " value:" + CType(element("TwinCatPort"), String))
            End Try

            If CType(element("UserDefine"), String) = XmlHandler.s_DEFAULT Then
                Throw New Exception("PLCs UserDefine undefined" + " value:" + CType(element("UserDefine"), String))
                Return False
            End If
            tempPLCConfig.UserDefine = CBool(IIf(element("UserDefine").ToString.ToUpper = "TRUE", True, False))

            Try

                If CType(element("PLCVersion"), String) = XmlHandler.s_DEFAULT Then
                    Throw New Exception("PLCs PLCVersion undefined" + " value:" + CType(element("PLCVersion"), String))
                    Return False
                End If
                tempPLCConfig.PLCVersion = CDbl(element("PLCVersion").ToString.Substring(1))
            Catch
                Throw New Exception("PLCs PLCVersion undefined" + " value:" + CType(element("PLCVersion"), String))
            End Try


            mSettings.PLCConfig.Add(tempPLCConfig.Name, tempPLCConfig)
        Next


        Return True
    End Function

    Protected Function MapNeworkDrives() As Boolean
        Dim Result As String, Letter As String, MapResult As String
        Dim DriveLetters As New Collection

        DriveLetters.Add("A")
        DriveLetters.Add("B")
        DriveLetters.Add("C")
        DriveLetters.Add("D")
        DriveLetters.Add("E")
        DriveLetters.Add("F")
        DriveLetters.Add("G")
        DriveLetters.Add("H")
        DriveLetters.Add("I")
        DriveLetters.Add("J")
        DriveLetters.Add("K")
        DriveLetters.Add("L")
        DriveLetters.Add("M")
        DriveLetters.Add("N")
        DriveLetters.Add("O")
        DriveLetters.Add("P")
        DriveLetters.Add("Q")
        DriveLetters.Add("R")
        DriveLetters.Add("S")
        DriveLetters.Add("T")
        DriveLetters.Add("U")
        DriveLetters.Add("V")
        DriveLetters.Add("W")
        DriveLetters.Add("X")
        DriveLetters.Add("Y")
        DriveLetters.Add("Z")

        Try

            For Each Letter In DriveLetters

                Result = mFileHandler.ReadIniFile(mSettings.ApplicationFolder & mSettings.RootIniName, "MAP", Letter)

                If Result <> mFileHandler.ErrorString Then

                    Letter = Letter + ":"
                    MapNetworkDrive.Disconnect(Letter)

                    MapResult = MapNetworkDrive.Connect(Result, "DETM", "L.K1o2s3t4a5l6", Letter)

                    If MapResult <> String.Empty Then
                        Throw New Exception("Map Not Exist" + Letter)
                        mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, " Folder >> " + Letter + " << unable to create;" + MapResult)
                        Return False

                    End If

                End If

            Next

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Protected Function CheckFolder(ByVal FolderName As String) As String
        Dim Result As String

        Result = xmlHandler.GetSectionInformation(mSettings.ApplicationFolder, mSettings.RootIniName, "Folder", FolderName)
        If Result = "#ERROR#" Then Result = "\" & FolderName
        If Left(Result, 1) = "\" Then
            Result = Right(Result, Len(Result) - 1)
            If Not My.Computer.FileSystem.DirectoryExists(mSettings.ApplicationFolder & Result) Then
                Try
                    My.Computer.FileSystem.CreateDirectory(mSettings.ApplicationFolder & Result)
                    mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, " Folder >> " & mSettings.ApplicationFolder & Result & " << created.")
                    Result = mSettings.ApplicationFolder & Result & "\"
                    Return Result
                Catch ex As Exception
                    mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, "Fail by " & FolderName & " Folder create : " & Result & mFileHandler.LogDelimiter & ex.ToString)
                    mSettings.Status = enumSettingsStatus.FAIL_BY_FOLDER_CREATE
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
                    mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, "Folder >> " & Result & " << created.")
                    Result = Result & "\"
                    Return Result
                Catch ex As Exception
                    mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.RootLogName, "Fail by " & FolderName & " Folder create : " & Result & mFileHandler.LogDelimiter & ex.ToString)
                    mSettings.Status = enumSettingsStatus.FAIL_BY_FOLDER_CREATE
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

    Protected Function ReadMachineIdentifier() As Boolean
        Dim sResult As String
        sResult = xmlHandler.GetSectionInformation(mSettings.ConfigFolder, mSettings.ConfigName, "GeneralInformation", "ProjectId")
        If sResult = xmlHandler.s_DEFAULT Or sResult = xmlHandler.s_Null Then
            Throw New Exception("Invalid Project ID. Please Add " + mSettings.ConfigName + "--ProjectId")
            Return False
        End If
        mSettings.MachineIdentifier.ProjectId = sResult

        sResult = xmlHandler.GetSectionInformation(mSettings.ConfigFolder, mSettings.ConfigName, "GeneralInformation", "TraceId")
        If sResult = xmlHandler.s_DEFAULT Or sResult = xmlHandler.s_Null Then
            Throw New Exception("Invalid TraceId. Please Add " + mSettings.ConfigName + "--TraceId")
            Return False
        End If
        mSettings.MachineIdentifier.TraceId = sResult

        sResult = xmlHandler.GetSectionInformation(mSettings.ConfigFolder, mSettings.ConfigName, "GeneralInformation", "LineId")
        If sResult = xmlHandler.s_DEFAULT Or sResult = xmlHandler.s_Null Then
            Throw New Exception("Invalid LineId. Please Add " + mSettings.ConfigName + "--LineId")
            Return False
        End If
        mSettings.MachineIdentifier.LineId = sResult
        Return True
    End Function

    Protected Function ReadHelpApplication() As Boolean
        mSettings.HelpFiles.Clear()
        mSettings.HelpApplication.Clear()

        For Each element As Dictionary(Of String, Object) In xmlHandler.GetAnyListFromXml(mSettings.ConfigFolder, mSettings.ConfigName, "Helps", "Help", New String() {"FileName", "Application"})

            If CType(element("Application"), String) = XmlHandler.s_DEFAULT Then
                mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ConfigName, "Help application undefined")
                Return False
            End If
            If Not mFileHandler.FileExist(CType(element("Application"), String)) Then
                mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ConfigName, "Help application does not exist")
                Return False
            End If

            If CType(element("FileName"), String) = XmlHandler.s_DEFAULT Then
                mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ConfigName, "Help FileName undefined")
                Return False
            End If
            If Not mFileHandler.FileExist(mSettings.HelpFolder + CType(element("FileName"), String)) Then
                mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ConfigName, CType(element("FileName"), String) + " - Help File does not exist")
                Continue For
            End If
            mSettings.HelpApplication.Add(mSettings.HelpApplication.Count + 1, CType(element("Application"), String))
            mSettings.HelpFiles.Add(mSettings.HelpFiles.Count + 1, CType(element("FileName"), String))
        Next
        Return True
    End Function



    Protected Function ReadCsvMapperRelatedSettings() As Boolean
        For Each element As Dictionary(Of String, Object) In xmlHandler.GetAnyListFromXml(mSettings.ConfigFolder, mSettings.ConfigName, "Article_Csvs", "Article_Csv", New String() {"CSV_Mapper", "CSV_FILE"})
            If CType(element("CSV_FILE"), String) = XmlHandler.s_DEFAULT Or CType(element("CSV_FILE"), String) = XmlHandler.s_Null Then
                Throw New Exception("Read failed: " & enumLK_CSV_STATUS.LK_CSV_FILE_NOT_AVAILABLE.ToString)
                Return False
            End If

            If CType(element("CSV_Mapper"), String) = XmlHandler.s_DEFAULT Or CType(element("CSV_Mapper"), String) = XmlHandler.s_Null Then
                Throw New Exception("Read failed: " & enumLK_CSV_STATUS.LK_CSV_MAPPER_NOT_AVAILABLE.ToString)
                Return False
            End If

            If Not mFileHandler.FileExist(mSettings.VarFolder + CType(element("CSV_FILE"), String)) Then
                Throw New Exception(CType(element("CSV_FILE"), String) + " -File does not exist")
                Continue For
            End If

            If Not mFileHandler.FileExist(mSettings.VarFolder + CType(element("CSV_Mapper"), String)) Then
                Throw New Exception(CType(element("CSV_Mapper"), String) + " -File does not exist")
                Continue For
            End If
            mSettings.ArticleAndMappingFile.Add(New ArticleAndMappingFile(CType(element("CSV_FILE"), String), CType(element("CSV_Mapper"), String)))

        Next

        If mSettings.ArticleAndMappingFile.Count = 0 Then
            Throw New Exception("ArticleAndMappingFile is Null. Please add at " + mSettings.ConfigName)
            Return False
        End If

        Return True
    End Function

    Protected Function ReadScheduleCsvsSettings() As Boolean

        For Each element As Dictionary(Of String, Object) In xmlHandler.GetAnyListFromXml(mSettings.ConfigFolder, mSettings.ConfigName, "Schedule_Csvs", "Schedule_Csv", New String() {"CSV_FILE"})
            If CType(element("CSV_FILE"), String) = XmlHandler.s_DEFAULT Or CType(element("CSV_FILE"), String) = XmlHandler.s_Null Then
                Throw New Exception("Read failed: " & enumLK_CSV_STATUS.LK_CSV_FILE_NOT_AVAILABLE.ToString)
                Return False
            End If
            If Not mFileHandler.FileExist(mSettings.ScheduleFolder + CType(element("CSV_FILE"), String)) Then
                Throw New Exception(CType(element("CSV_FILE"), String) + " -File does not exist")
                Continue For
            End If
            mSettings.ScheduleFile = CType(element("CSV_FILE"), String)
        Next

        If mSettings.ScheduleFile = "" Then
            Throw New Exception("Schedule-File is Null. Please add at " + mSettings.ConfigName)
        End If
        Return True
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

