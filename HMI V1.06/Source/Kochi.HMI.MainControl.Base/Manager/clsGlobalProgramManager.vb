Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.IO
Public Class clsGlobalProgramManager
    Private lGlobalProgramList As New List(Of clsGlobalProgramCfg)
    Private cGlobalProgramData As New clsGlobalProgramData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cHMIDataView As HMIDataView
    Private cLanguageManager As clsLanguageManager
    Private cCurrentGlobalProgramCfg As New clsGlobalProgramCfg
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Public Const Name As String = "GlobalProgramManager"
    Private _Object As New Object

    Public ReadOnly Property CurrentGlobalProgramCfg As clsGlobalProgramCfg

        Get
            SyncLock _Object
                Return cCurrentGlobalProgramCfg
            End SyncLock
        End Get
    End Property

    Public Function RegisterManager(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean

        SyncLock _Object
            Me.cDataGridViewPage = cDataGridViewPage
            Me.cHMIDataView = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cGlobalProgramData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetGlobalProgramListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lGlobalProgramList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetGlobalProgramCfgFromKey(ByVal iKey As Integer) As clsGlobalProgramCfg
        SyncLock _Object
            Try
                If iKey <= lGlobalProgramList.Count - 1 Then
                    Return lGlobalProgramList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function GetGlobalProgramCfgFromGlobalProgram(ByVal strGlobalProgram As String) As clsGlobalProgramCfg
        SyncLock _Object
            Try
                If Not HasGlobalProgram(strGlobalProgram) Then
                    Return Nothing
                End If
                Return lGlobalProgramList.Where(Function(e) e.GlobalProgram = strGlobalProgram).First
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function InSertData(ByVal strID As String, ByVal strGlobalProgram As String, ByVal strDescription As String, ByVal strDescription2 As String) As Boolean

        SyncLock _Object
            Try
                Dim strProgramPath As String = ""
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strGlobalProgram + ".ini"
                cGlobalProgramData.InSertData(strID, strGlobalProgram, strDescription, strDescription2, strProgramPath)
                lGlobalProgramList.Add(New clsGlobalProgramCfg(strID, strGlobalProgram, strDescription, strDescription2, strProgramPath))
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strGlobalProgram As String, ByVal strDescription As String, ByVal strDescription2 As String) As Boolean

        SyncLock _Object
            Try
                Dim strProgramPath As String = ""
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strGlobalProgram + ".ini"
                cGlobalProgramData.ModifyData(strID, strGlobalProgram, strDescription, strDescription2, strProgramPath)
                For Each element As clsGlobalProgramCfg In lGlobalProgramList
                    If element.ID = strID Then
                        element.GlobalProgram = strGlobalProgram
                        element.Description = strDescription
                        element.Description2 = strDescription2
                        element.ProgramPath = strProgramPath
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function HasGlobalProgram(ByVal strGlobalProgram As String) As Boolean

        SyncLock _Object
            Try
                If lGlobalProgramList.Any(Function(e) e.GlobalProgram = strGlobalProgram) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasGlobalProgram(ByVal strGlobalProgram As String, ByVal strID As String) As Boolean

        SyncLock _Object
            Try
                If lGlobalProgramList.Any(Function(e) e.GlobalProgram = strGlobalProgram And e.ID <> strID) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetProgramPath(ByVal strGlobalProgram As String) As String

        SyncLock _Object
            Try
                Dim strProgramPath As String = String.Empty
                If HasGlobalProgram(strGlobalProgram) Then
                    strProgramPath = GetGlobalProgramCfgFromGlobalProgram(strGlobalProgram).ProgramPath
                End If
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strGlobalProgram + ".ini"
                Return strProgramPath
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean

        SyncLock _Object
            Try
                Dim strProgramPath As String = String.Empty
                strProgramPath = lGlobalProgramList(strID - 1).ProgramPath
                Dim iCnt As Integer = 0
                For Each element As clsGlobalProgramCfg In lGlobalProgramList
                    If element.ProgramPath = strProgramPath Then
                        iCnt = iCnt + 1
                    End If
                Next
                If iCnt = 1 Then
                    If File.Exists(strProgramPath) Then
                        File.Delete(strProgramPath)
                    End If
                End If
                lGlobalProgramList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cGlobalProgramData.SaveGlobalProgram(lGlobalProgramList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function LoadGlobalProgramCfg() As Boolean

        SyncLock _Object
            Try
                cGlobalProgramData.LoadGlobalProgram(lGlobalProgramList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean

        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("GlobalProgramTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "ID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "GlobalProgram"))
                If cLanguageManager.SecondLanguageEnable Then
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "Description", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage)))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "Description2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage)))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                Else
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "Description3"))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                End If
                dt.Columns.Add(cLanguageManager.GetTextLine(clsGlobalProgramManager.Name, "ProgramPath"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                For Each element As clsGlobalProgramCfg In lGlobalProgramList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.ID.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cListSearchContion(1) <> "" Then
                            If element.GlobalProgram.IndexOf(cListSearchContion(1)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cLanguageManager.SecondLanguageEnable Then
                        Dim lListValue As New List(Of String)
                        lListValue.Add(element.ID)
                        lListValue.Add(element.GlobalProgram)
                        lListValue.Add(element.Description)
                        lListValue.Add(element.Description2)
                        lListValue.Add(element.ProgramPath)
                        dt.Rows.Add(lListValue.ToArray)
                    Else
                        Dim lListValue As New List(Of String)
                        lListValue.Add(element.ID)
                        lListValue.Add(element.GlobalProgram)
                        lListValue.Add(element.Description)
                        lListValue.Add(element.ProgramPath)
                        dt.Rows.Add(lListValue.ToArray)
                    End If
                Next
                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage) Then
                    cDataGridViewPage.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage.Paging(cViewPageType)
                Else
                    cHMIDataView.DataSource = Ds.Tables(0)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean

        SyncLock _Object
            Try
                Dim j As Integer = 1
                For i = 0 To lGlobalProgramList.Count - 1
                    lGlobalProgramList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

End Class

Public Class clsGlobalProgramCfg
    Private strID As String = String.Empty
    Private strGlobalProgram As String = String.Empty
    Private strDescription As String = String.Empty
    Private strDescription2 As String = String.Empty
    Private strProgramPath As String = String.Empty
    Private _Object As New Object

    Public Property GlobalProgram As String

        Set(ByVal value As String)
            SyncLock _Object
                strGlobalProgram = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strGlobalProgram
            End SyncLock
        End Get

    End Property


    Public Property ID As String

        Set(ByVal value As String)
            SyncLock _Object
                strID = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strID
            End SyncLock
        End Get

    End Property

    Public Property Description As String

        Set(ByVal value As String)
            SyncLock _Object
                strDescription = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strDescription
            End SyncLock
        End Get
    End Property

    Public Property Description2 As String

        Set(ByVal value As String)
            SyncLock _Object
                strDescription2 = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strDescription2
            End SyncLock
        End Get
    End Property

  

    Public Property ProgramPath As String

        Set(ByVal value As String)
            SyncLock _Object
                strProgramPath = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strProgramPath
            End SyncLock
        End Get
    End Property

    Sub New()
        SyncLock _Object

        End SyncLock
    End Sub


    Sub New(ByVal strID As String, ByVal strGlobalProgram As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strProgramPath As String)
        SyncLock _Object
            Me.strID = strID
            Me.strGlobalProgram = strGlobalProgram
            Me.Description = strDescription
            Me.Description2 = strDescription2
            Me.strProgramPath = strProgramPath
        End SyncLock
    End Sub
End Class

Public Class clsGlobalProgramData
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private _Object As New Object
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strGlobalProgram As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strProgramPath As String) As Boolean

        SyncLock _Object
            Try
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("GlobalProgramName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("ProgramPath")
                lListValue.Add(strID)
                lListValue.Add(strGlobalProgram)
                lListValue.Add(strDescription)
                lListValue.Add(strDescription2)
                lListValue.Add(clsSystemPath.ToIniPath(strProgramPath))
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.GlobalProgramList, "GlobalProgram" + strID, lListKey.ToArray,
                                      lListValue.ToArray)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strGlobalProgram As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strProgramPath As String) As Boolean

        SyncLock _Object
            Try
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("GlobalProgramName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("ProgramPath")
                lListValue.Add(strID)
                lListValue.Add(strGlobalProgram)
                lListValue.Add(strDescription)
                lListValue.Add(strDescription2)
                lListValue.Add(clsSystemPath.ToIniPath(strProgramPath))
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.GlobalProgramList, "GlobalProgram" + strID, lListKey.ToArray,
                                      lListValue.ToArray)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean

        SyncLock _Object
            Try
                Return cIniHandler.RemoveSection(cSystemManager.Settings.GlobalProgramList, "GlobalProgram" + strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadGlobalProgram(ByRef lGlobalProgramList As List(Of clsGlobalProgramCfg)) As Boolean

        SyncLock _Object
            Try
                lGlobalProgramList.Clear()
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("GlobalProgramName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("ProgramPath")

                Dim cGlobalProgramElement As clsGlobalProgramCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.GlobalProgramList, "GlobalProgram", lListKey.ToArray)
                    cGlobalProgramElement = New clsGlobalProgramCfg
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cGlobalProgramElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("GlobalProgramName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("GlobalProgramName"), String) <> clsXmlHandler.s_Null Then
                        cGlobalProgramElement.GlobalProgram = CType(element("GlobalProgramName"), String)
                    End If
                    If CType(element("Description"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Description"), String) <> clsXmlHandler.s_Null Then
                        cGlobalProgramElement.Description = CType(element("Description"), String)
                    End If
                    If CType(element("Description2"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Description2"), String) <> clsXmlHandler.s_Null Then
                        cGlobalProgramElement.Description2 = CType(element("Description2"), String)
                    End If


                    If CType(element("ProgramPath"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ProgramPath"), String) <> clsXmlHandler.s_Null Then
                        cGlobalProgramElement.ProgramPath = clsSystemPath.ToSystemPath(CType(element("ProgramPath"), String))
                    Else
                        cGlobalProgramElement.ProgramPath = cSystemManager.Settings.VariantFolder + "\" + cGlobalProgramElement.GlobalProgram + ".ini"
                    End If

                    lGlobalProgramList.Add(cGlobalProgramElement)
                    
                    
                Next
                If lGlobalProgramList.Count = 0 Then
                    cGlobalProgramElement = New clsGlobalProgramCfg
                    cGlobalProgramElement.ID = 1
                    cGlobalProgramElement.GlobalProgram = "GlobalAction"
                    cGlobalProgramElement.ProgramPath = cSystemManager.Settings.VariantFolder + "\GlobalAction.ini"
                    lGlobalProgramList.Add(cGlobalProgramElement)
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function SaveGlobalProgram(ByVal lGlobalProgramList As List(Of clsGlobalProgramCfg)) As Boolean

        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(cSystemManager.Settings.GlobalProgramList, "GlobalProgram")
                For Each element As clsGlobalProgramCfg In lGlobalProgramList
                    InSertData(element.ID, element.GlobalProgram, element.Description, element.Description2, clsSystemPath.ToIniPath(element.ProgramPath))
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

