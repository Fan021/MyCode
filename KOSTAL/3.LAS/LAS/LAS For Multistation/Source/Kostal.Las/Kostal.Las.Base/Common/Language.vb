Option Explicit On
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports Kostal.Las.ArticleProvider
Imports System.Drawing
Public Enum enumLanguageStatus
    OK = 0
    LANGUAGEFILE_NOT_FOUND = -1
    NO_MAXTEXT_DEFINED = -2
    FAIL_IN_SECTION_OR_KEYWORD = -3
End Enum

Public Class LanguageElement
    Inherits Texts

    Protected mStatus As enumLanguageStatus
    Protected mMaxText As Long
    Protected mIsInit As Boolean
    Protected Log As Logger

    Protected mSection_LanguageFileNames As String
    Protected mKeyWord_SelectedLanguage As String
    Protected mLanguageFileNames As New Dictionary(Of String, String)
    Protected mSelectedLanguageFileName As String
    Protected mTextSectionName As String
    Protected mKeyWordMaxText As String
    Public Const Con_English As String = "English"
    Protected mSettings As Settings
    Public Sub New(ByVal mSettings As Settings)
        Me.mSettings = mSettings
    End Sub

    Public Property Section_LanguageFileNames() As String
        Get
            Return mSection_LanguageFileNames
        End Get
        Set(ByVal value As String)
            mSection_LanguageFileNames = value
        End Set
    End Property

    Public Property KeyWord_SelectedLanguage() As String
        Get
            Return mKeyWord_SelectedLanguage
        End Get
        Set(ByVal value As String)
            mKeyWord_SelectedLanguage = value
        End Set
    End Property

    Public Function LanguageFileName_Add(ByVal FileName As String) As String
        Try
            mLanguageFileNames.Add(FileName, FileName)
            Return FileName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function LanguageFileName_Remove(ByVal FileName As String) As String
        Try
            mLanguageFileNames.Remove(FileName)
            Return FileName
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function LanguageFileName_Count() As Integer
        Return mLanguageFileNames.Count
    End Function

    Public Sub LanguageFileName_Clear()
        mLanguageFileNames.Clear()
    End Sub

    Public Overloads ReadOnly Property LanguageFileName(ByVal Index As Integer) As String
        Get

            Try
                Dim a(mLanguageFileNames.Count - 1) As String
                mLanguageFileNames.Keys.CopyTo(a, 0)
                Return mLanguageFileNames(a(Index - 1))
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Public Overloads ReadOnly Property LanguageFileName(ByVal Key As String) As String
        Get
            Try
                Return mLanguageFileNames(Key)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Public Property SelectedLanguageFileName() As String
        Get
            Return mSelectedLanguageFileName
        End Get
        Set(ByVal value As String)
            mSelectedLanguageFileName = value
        End Set
    End Property

    Public Property IsInit() As Boolean
        Get
            Return mIsInit
        End Get
        Set(ByVal value As Boolean)
            mIsInit = value
        End Set
    End Property

    Public Property MaxText() As Long
        Get
            Return mMaxText
        End Get
        Set(ByVal value As Long)
            mMaxText = value
        End Set
    End Property

    Public Property TextSectionName() As String
        Get
            Return mTextSectionName
        End Get
        Set(ByVal value As String)
            mTextSectionName = value
        End Set
    End Property

    Public Property KeyWordMaxText() As String
        Get
            Return mKeyWordMaxText
        End Get
        Set(ByVal value As String)
            mKeyWordMaxText = value
        End Set
    End Property

    Public Property Status() As enumLanguageStatus
        Get
            Return mStatus
        End Get
        Set(ByVal value As enumLanguageStatus)
            mStatus = value
        End Set
    End Property
    Public Function GetText(ByVal Section As String, ByVal Key As Integer, ByVal ParamArray strAppend() As String) As String
        Dim i As New Station
        Dim lX As Integer, sResult As String
        i.Parameters.Clear()
        For Each strMsg In strAppend
            i.Parameters.Add(strMsg)
        Next
        Try
            sResult = mFileHandler.ReadLanguageFile(mSettings.LngFolder, Me.SelectedLanguageFileName & mSettings.Extension_LanguageFile, Section, Key.ToString)
            If (i.Parameters.Count > 0) Then
                For lX = 1 To i.Parameters.Count
                    sResult = Replace(sResult, mSubstitutionVariable & Trim(CStr(lX)), i.Parameters(lX).ToString)
                Next lX
            End If
            GetText = Replace(sResult, mSubstitutionCrLf, vbCrLf)
        Catch ex As Exception
            GetText = ""
        End Try
        i.Parameters.Clear()
    End Function
End Class


Public Class SetLanguage

    Protected IsDisposed As Boolean

    Protected LanguageElement As LanguageElement
    Protected mSettings As New Settings
    Protected Log As Logger
    Protected _i As Station
    Protected mFileHandler As New FileHandler
    Protected xmlHandler As New XmlHandler

    Public ReadOnly Property Language() As LanguageElement
        Get
            Return LanguageElement
        End Get
    End Property

    Public Sub New(ByVal i As Station,
                        ByVal _AppSettings As Settings,
                        Optional ByVal Section_LanguageFileNames As String = "LanguageFiles",
                        Optional ByVal KeyWord_SelectedLanguage As String = "SelLanguage",
                        Optional ByVal Section_Text As String = "Text",
                        Optional ByVal Keyword_MaxText As String = "MaxText")
        _i = i
        mSettings = _AppSettings
        Log = New Logger(mSettings)
        LanguageElement = Nothing
        LanguageElement = New LanguageElement(_AppSettings)

        LanguageElement.Section_LanguageFileNames = Section_LanguageFileNames
        LanguageElement.KeyWord_SelectedLanguage = KeyWord_SelectedLanguage
        LanguageElement.TextSectionName = Section_Text
        LanguageElement.KeyWordMaxText = Keyword_MaxText
    End Sub


    Public Function Init() As LanguageElement
        LanguageElement.IsInit = False

        If Not ReadLanguageFiles() Then
            Return Nothing
        ElseIf Not GetSelectedLanguage() Then
            Return Nothing
        ElseIf Not ReadTexts() Then
            Return Nothing
        Else
            LanguageElement.IsInit = True
            LanguageElement.Status = enumLanguageStatus.OK
            Return LanguageElement
        End If
    End Function

    Public Function ReloadLanguage() As Boolean
        If Not GetSelectedLanguage() Then
            Return False
        ElseIf Not ReadTexts() Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Function GetSelectedLanguage() As Boolean
        Dim Result As String

        Result = xmlHandler.GetSectionInformation(mSettings.LogFolder, mSettings.ApplicationActive, LanguageElement.Section_LanguageFileNames, LanguageElement.KeyWord_SelectedLanguage)
        _i.StepTextLine = "GetSelectedLanguage"
        If Result = XmlHandler.s_DEFAULT Or Result = XmlHandler.s_Null Then
            Log.Thrower(_i, "KeyWord_SelectedLanguage Or Section_LanguageFileNames", "SetLanguage.ReadLanguageFiles")
            LanguageElement.Status = enumLanguageStatus.FAIL_IN_SECTION_OR_KEYWORD
            LanguageElement.SelectedLanguageFileName = LanguageElement.LanguageFileName(1)
        Else
            LanguageElement.SelectedLanguageFileName = LanguageElement.LanguageFileName(Result)
            If LanguageElement.SelectedLanguageFileName = "" Then
                Log.Thrower(_i, "KeyWord_SelectedLanguage Or Section_LanguageFileNames", "SetLanguage.ReadLanguageFiles")
                LanguageElement.SelectedLanguageFileName = LanguageElement.LanguageFileName(1)
            End If
        End If

        If LanguageElement.SelectedLanguageFileName = "" Then
            Log.Thrower(_i, "No valid entry In LanguageFiles", "SetLanguage.ReadLanguageFiles")
            Return False
        End If
        xmlHandler.SetGeneralInformation(mSettings.LogFolder, mSettings.ApplicationActive, LanguageElement.Section_LanguageFileNames, LanguageElement.KeyWord_SelectedLanguage, LanguageElement.SelectedLanguageFileName)
        Log.Logger(_i, "GetSelectedLanguage Successful", "SetLanguage.GetSelectedLanguage")
        Return True
    End Function

    Protected Function ReadLanguageFiles() As Boolean

        For Each element As Dictionary(Of String, Object) In xmlHandler.GetAnyListFromXml(mSettings.ConfigFolder, mSettings.ConfigName, "LanguageFiles", "Language", New String() {"Name"})
            If Not mFileHandler.FileExist(mSettings.LngFolder + CType(element("Name"), String) + ".lng") Then
                Log.Thrower(_i, "Language File " + CType(element("Name"), String) + "  Not Exist.", "SetLanguage.ReadLanguageFiles")
                Return False
            End If
            LanguageElement.LanguageFileName_Add(CType(element("Name"), String))
        Next

        If Not mFileHandler.FileExist(mSettings.LogFolder + mSettings.ApplicationActive + ".xml") Then
            xmlHandler.SetGeneralInformation(mSettings.LogFolder, mSettings.ApplicationActive, LanguageElement.Section_LanguageFileNames, LanguageElement.KeyWord_SelectedLanguage, LanguageElement.LanguageFileName(1))
        End If

        Log.Logger(_i, "ReadLanguageFiles Successful", "SetLanguage.ReadLanguageFiles")
        Return True

    End Function

    Protected Function ReadTexts(Optional ByVal Section_Text As String = "", Optional ByVal Keyword_MaxText As String = "") As Boolean
        Dim Result As String, l As Long, LocalTextSectionToRead As String, LocalMaxKeyword As String
        If Section_Text = "" Then
            LocalTextSectionToRead = LanguageElement.TextSectionName
        Else
            LocalTextSectionToRead = Section_Text
        End If

        If Keyword_MaxText = "" Then
            LocalMaxKeyword = LanguageElement.KeyWordMaxText
        Else
            LocalMaxKeyword = Keyword_MaxText
        End If

        Result = mFileHandler.ReadLanguageFile(mSettings.LngFolder, LanguageElement.LanguageFileName(LanguageElement.SelectedLanguageFileName), LocalTextSectionToRead, LocalMaxKeyword)
        If Result = "" Or Result = mFileHandler.ErrorString Or Not IsNumeric(Result) Then
            Log.Thrower(_i, "Fail In Section " & LocalTextSectionToRead & " Or KeyWord " & LocalMaxKeyword, "SetLanguage.ReadLanguageFiles")
            LanguageElement.Status = enumLanguageStatus.NO_MAXTEXT_DEFINED
            Return False
        End If

        LanguageElement.ClearTexts()
        LanguageElement.MaxText = CLng(Result)

        For l = 0 To LanguageElement.MaxText
            Result = mFileHandler.ReadLanguageFile(mSettings.LngFolder, LanguageElement.LanguageFileName(LanguageElement.SelectedLanguageFileName), LocalTextSectionToRead, Trim(l.ToString))
            If Result <> "" And Result <> mFileHandler.ErrorString Then
                LanguageElement.AddNewTextLine(Result, l)
            Else
                LanguageElement.AddNewTextLine(l.ToString, l)
            End If
        Next
        Log.Logger(_i, "ReadTexts Successful", "SetLanguage.ReadTexts")
        Return True

    End Function

End Class

Public Class Language

    Protected mFileHandler As New FileHandler
    Protected mSettings As Settings
    Protected _SetAppLanguage As SetLanguage
    Protected _LanguageElement As LanguageElement
    Protected _i As Station
    Protected Log As Logger
    Protected _LockObject As Object = ""
    Public Const Name As String = "_mLanguage"

    Public ReadOnly Property SetAppLanguage() As SetLanguage
        Get
            Return _SetAppLanguage
        End Get
    End Property

    Public ReadOnly Property LanguageElement() As LanguageElement
        Get
            Return _LanguageElement
        End Get
    End Property


    Public Sub New(ByVal i As Station, ByVal _AppSettings As Settings)
        mSettings = _AppSettings
        _i = i
        _SetAppLanguage = New SetLanguage(_i, mSettings)
        Log = New Logger(mSettings)
    End Sub

    Public Function Init() As Boolean
        _LanguageElement = SetAppLanguage.Init()
        Log.Logger(_i, LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LANGUAGE_INIT, "Successful"), "Language.Init")
        Return True
    End Function

    Public Function ReadControlText(ByVal MyControl As Control) As Boolean
        SyncLock _LockObject
            Dim Result As String
            Dim mControl As Control

            If Not _LanguageElement.IsInit Then Return False

            Result = mFileHandler.ReadLanguageFile(mSettings.LngFolder, _LanguageElement.SelectedLanguageFileName & mSettings.Extension_LanguageFile, "Controls", MyControl.Name)
            If Result <> mFileHandler.ErrorString Then
                MyControl.Text = Result
            End If

            Try
                For Each mControl In MyControl.Controls
                    If mControl.Controls.Count > 0 Then
                        'mCallSelf = New LanguageReader(LanguageElement, mSettings)
                        ReadControlText(mControl)
                    ElseIf mControl.GetType.ToString = "System.Windows.Forms.MenuStrip" Then
                        ReadMenuStrip(CType(mControl, MenuStrip))
                    Else
                        ReadControl(mControl)
                    End If
                Next
            Catch ex As Exception
                '
            End Try
            Return True
        End SyncLock
    End Function

    Protected Function ReadControl(ByVal MyControl As Control) As Boolean

        Dim Result As String

        If Not _LanguageElement.IsInit Then Return False

        Try
            Result = mFileHandler.ReadLanguageFile(mSettings.LngFolder, _LanguageElement.SelectedLanguageFileName & mSettings.Extension_LanguageFile, "Controls", MyControl.Name)
            If Result <> mFileHandler.ErrorString Then
                MyControl.Text = Result
                '   MyControl.Font = New Font("Calibri", MyControl.Font.Size, MyControl.Font.Style)
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Public Function ReadMenuStrip(ByVal MyMenuStrip As MenuStrip) As Boolean
        Dim mItem As ToolStripMenuItem, NextItem As New ToolStripMenuItemReader

        If Not _LanguageElement.IsInit Then Return False

        For Each mItem In MyMenuStrip.Items
            NextItem.ReadToolStripMenuItem(mSettings.LngFolder, _LanguageElement.SelectedLanguageFileName, mItem)
        Next
        Return True
    End Function

    Public Function ReadContextMenuStrip(ByVal MyMenuStrip As ContextMenuStrip) As Boolean
        Dim mItem As ToolStripMenuItem, NextItem As New ToolStripMenuItemReader

        If Not _LanguageElement.IsInit Then Return False

        For Each mItem In MyMenuStrip.Items
            NextItem.ReadToolStripMenuItem(mSettings.LngFolder, _LanguageElement.SelectedLanguageFileName, mItem)
        Next
        Return True
    End Function

    Public Function Read(ByVal Section As String, ByVal KeyWord As String) As String
        SyncLock _LockObject
            Dim mTemp As String = mFileHandler.ReadLanguageFile(mSettings.LngFolder, _LanguageElement.SelectedLanguageFileName & mSettings.Extension_LanguageFile, Section, KeyWord)
            If mTemp = FileHandler.s_DEFAULT Then
                Return KeyWord
            End If
            Return mTemp
        End SyncLock
    End Function
End Class



Public Class ToolStripMenuItemReader
    Protected mFileModul As New FileHandler

    Public Overloads Function ReadToolStripMenuItem(ByVal CompleteLanguageFileName As String, ByVal MyItem As ToolStripMenuItem) As Boolean
        Dim Result As String, mItem As ToolStripMenuItem

        Result = mFileModul.ReadLanguageFile(CompleteLanguageFileName, "Controls", MyItem.Name)
        If Result <> mFileModul.ErrorString Then
            MyItem.Text = Result
        End If

        For Each mItem In MyItem.DropDownItems
            Dim NextItem As New ToolStripMenuItemReader
            NextItem.ReadToolStripMenuItem(CompleteLanguageFileName, mItem)
        Next
        Return True
    End Function

    Public Overloads Function ReadToolStripMenuItem(ByVal LanguagePath As String, ByVal LanguageFileName As String, ByVal MyItem As ToolStripMenuItem) As Boolean
        Dim Result As String, mItem As ToolStripMenuItem

        Result = mFileModul.ReadLanguageFile(LanguagePath, LanguageFileName, "Controls", MyItem.Name)
        If Result <> mFileModul.ErrorString Then
            MyItem.Text = Result
        End If

        For Each mItem In MyItem.DropDownItems
            Dim NextItem As New ToolStripMenuItemReader
            NextItem.ReadToolStripMenuItem(LanguagePath, LanguageFileName, mItem)
        Next
        Return True
    End Function
End Class

Public Class Texts

    Protected mTexts As New Dictionary(Of String, Text)
    Protected Const mSubstitutionVariable As String = "$"
    Protected Const mSubstitutionCrLf As String = "#"
    Protected mFileHandler As New FileHandler

    Public Sub ClearTexts()
        mTexts.Clear()
    End Sub

    Public Function AddNewTextLine(ByVal Text As String, ByVal Number As Long, Optional ByVal Key As String = Nothing) As Text

        Dim NewText As New Text

        NewText.Text = Text
        NewText.Number = Number

        If Key Is Nothing Then
            NewText.Key = NewText.Number.ToString
        End If

        mTexts.Add(NewText.Key, NewText)
        Return NewText

    End Function


    Public Function GetTextLine(ByVal i As Station, ByVal Key As enumLK_TEXT, ByVal ParamArray strAppend() As String) As String
        Dim lX As Integer, sResult As String
        i.Parameters.Clear()
        For Each strMsg In strAppend
            i.Parameters.Add(strMsg)
        Next
        Try
            sResult = mTexts(CStr(Key)).Text
            If (i.Parameters.Count > 0) Then
                For lX = 1 To i.Parameters.Count
                    sResult = Replace(sResult, mSubstitutionVariable & Trim(CStr(lX)), i.Parameters(lX).ToString)
                Next lX
            End If
            GetTextLine = Replace(sResult, mSubstitutionCrLf, vbCrLf)
        Catch ex As Exception
            GetTextLine = ""
        End Try
        i.Parameters.Clear()

    End Function




End Class

Public Class Text

    Protected mKey As String
    Protected mText As String
    Protected mNumber As Long

    Public Property Key() As String
        Get
            Return mKey
        End Get
        Set(ByVal value As String)
            mKey = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return mText
        End Get
        Set(ByVal value As String)
            mText = value
        End Set
    End Property

    Public Property Number() As Long
        Get
            Return mNumber
        End Get
        Set(ByVal value As Long)
            mNumber = value
        End Set
    End Property

End Class


