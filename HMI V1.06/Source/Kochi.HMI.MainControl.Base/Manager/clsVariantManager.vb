Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.IO
Public Class clsVariantManager
    Private lVariantList As New List(Of clsVariantCfg)
    Private cVariantData As New clsVariantData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cHMIDataView As MachineListView
    Private cLanguageManager As clsLanguageManager
    Private cCurrentVariantCfg As New clsVariantCfg
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager

    Public Event VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
    Public Event LoadChanged()
    Public Const Name As String = "VariantManager"
    Private _Object As New Object

    Public ReadOnly Property CurrentVariantCfg As clsVariantCfg

        Get
            SyncLock _Object
                Return cCurrentVariantCfg
            End SyncLock
        End Get
    End Property

    Public Function RegisterManager(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As MachineListView) As Boolean

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
                cVariantData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetVariantListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lVariantList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetVariantCfgFromKey(ByVal iKey As Integer) As clsVariantCfg
        SyncLock _Object
            Try
                If iKey <= lVariantList.Count - 1 Then
                    Return lVariantList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function GetVariantCfgFromVariant(ByVal strVariant As String) As clsVariantCfg
        SyncLock _Object
            Try
                If Not HasVariant(strVariant) Then
                    Return Nothing
                End If
                Return lVariantList.Where(Function(e) e.Variant = strVariant).First
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function InSertData(ByVal strID As String, ByVal strVariant As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strPicturePath As String, ByVal strProgramPath As String, ByVal strGlobalProgramPath As String, ByVal lListElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strVariant + ".ini"
                If strGlobalProgramPath = "" Then strGlobalProgramPath = "GlobalAction"
                cVariantData.InSertData(strID, strVariant, strDescription, strDescription2, strPicturePath, strProgramPath, strGlobalProgramPath, lListElement)
                lVariantList.Add(New clsVariantCfg(strID, strVariant, strDescription, strDescription2, strPicturePath, strProgramPath, strGlobalProgramPath, lListElement))
                RaiseEvent LoadChanged()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strVariant As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strPicturePath As String, ByVal strProgramPath As String, ByVal strGlobalProgramPath As String, ByVal lListElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strVariant + ".ini"
                If strGlobalProgramPath = "" Then strGlobalProgramPath = "GlobalAction"
                cVariantData.ModifyData(strID, strVariant, strDescription, strDescription2, strPicturePath, strProgramPath, strGlobalProgramPath, lListElement)
                For Each element As clsVariantCfg In lVariantList
                    If element.ID = strID Then
                        element.Variant = strVariant
                        element.Description = strDescription
                        element.Description2 = strDescription2
                        element.PicturePath = strPicturePath
                        element.ProgramPath = strProgramPath
                        element.GlobalProgramPath = strGlobalProgramPath
                        element.ListElement.Clear()
                        For Each elementKey As String In lListElement.Keys
                            element.ListElement.Add(elementKey, lListElement(elementKey))
                        Next
                    End If
                Next
                ChangeID()
                RaiseEvent LoadChanged()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function HasVariant(ByVal strVariant As String) As Boolean

        SyncLock _Object
            Try
                If lVariantList.Any(Function(e) e.Variant = strVariant) Then
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

    Public Function HasVariant(ByVal strVariant As String, ByVal strID As String) As Boolean

        SyncLock _Object
            Try
                If lVariantList.Any(Function(e) e.Variant = strVariant And e.ID <> strID) Then
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


    Public Function GetProgramPath(ByVal strVariant As String) As String

        SyncLock _Object
            Try
                Dim strProgramPath As String = String.Empty
                If HasVariant(strVariant) Then
                    strProgramPath = GetVariantCfgFromVariant(strVariant).ProgramPath
                End If
                If strProgramPath = "" Then strProgramPath = cSystemManager.Settings.VariantFolder + "\" + strVariant + ".ini"
                Return strProgramPath
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GlobalProgramPath(ByVal strVariant As String) As String

        SyncLock _Object
            Try
                Dim strProgramPath As String = String.Empty
                If HasVariant(strVariant) Then
                    strProgramPath = GetVariantCfgFromVariant(strVariant).GlobalProgramPath
                End If
                If strProgramPath = "" Then strProgramPath = "GlobalAction"
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
                strProgramPath = lVariantList(strID - 1).ProgramPath
                Dim iCnt As Integer = 0
                For Each element As clsVariantCfg In lVariantList
                    If element.ProgramPath = strProgramPath Then
                        iCnt = iCnt + 1
                    End If
                Next
                If iCnt = 1 Then
                    If File.Exists(strProgramPath) Then
                        File.Delete(strProgramPath)
                    End If
                End If
                lVariantList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cVariantData.SaveVariant(lVariantList)
                RaiseEvent LoadChanged()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ChangeVariant(ByVal strVariant As String, Optional ByVal eSelectVariantType As enumSelectVariantType = enumSelectVariantType.Manual) As Boolean
        SyncLock _Object
            Try
                If Not HasVariant(strVariant) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine(clsVariantManager.Name, "1", strVariant), enumExceptionType.Alarm)
                    Return False
                End If
                Dim icnt As Int16 = Integer.Parse(eSelectVariantType)
                cCurrentVariantCfg = GetVariantCfgFromVariant(strVariant)
                cCurrentVariantCfg.Jump = False
                RaiseEvent VariantChanged(strVariant, cCurrentVariantCfg, icnt)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadVariantCfg() As Boolean

        SyncLock _Object
            Try
                cVariantData.LoadVariant(lVariantList)
                If Not IsNothing(cCurrentVariantCfg) AndAlso cCurrentVariantCfg.Variant <> "" Then
                    cCurrentVariantCfg = GetVariantCfgFromVariant(cCurrentVariantCfg.Variant)
                    cCurrentVariantCfg.Jump = False
                End If
                RaiseEvent LoadChanged()
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
                Dim dt As DataTable = New DataTable("VariantTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Variant"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                If cLanguageManager.SecondLanguageEnable Then
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Description", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage)))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Description2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage)))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                Else
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Description3"))
                    dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                End If
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "PicturePath"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ProgramPath"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "GlobalProgramPath"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, mKey))
                    If dt.Columns(dt.Columns.Count - 1).ReadOnly Then dt.Columns(dt.Columns.Count - 1).ReadOnly = False
                Next
                For Each element As clsVariantCfg In lVariantList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.ID.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cListSearchContion(1) <> "" Then
                            If element.Variant.IndexOf(cListSearchContion(1)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cLanguageManager.SecondLanguageEnable Then
                        Dim lListValue As New List(Of String)
                        lListValue.Add(element.ID)
                        lListValue.Add(element.Variant)
                        lListValue.Add(element.Description)
                        lListValue.Add(element.Description2)
                        lListValue.Add(element.PicturePath)
                        lListValue.Add(element.ProgramPath)
                        lListValue.Add(element.GlobalProgramPath)
                        For Each mValue As String In element.ListElement.Values
                            lListValue.Add(mValue)
                        Next
                        dt.Rows.Add(lListValue.ToArray)
                    Else
                        Dim lListValue As New List(Of String)
                        lListValue.Add(element.ID)
                        lListValue.Add(element.Variant)
                        lListValue.Add(element.Description)
                        lListValue.Add(element.PicturePath)
                        lListValue.Add(element.ProgramPath)
                        lListValue.Add(element.GlobalProgramPath)
                        For Each mValue As String In element.ListElement.Values
                            lListValue.Add(mValue)
                        Next
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
                For i = 0 To cHMIDataView.Columns.Count - 1
                    cHMIDataView.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next

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
                For i = 0 To lVariantList.Count - 1
                    lVariantList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

End Class

Public Class clsVariantCfg
    Private strID As String = String.Empty
    Private strVariant As String = String.Empty
    Private strSFC As String = String.Empty
    Private iCarrierID As Integer = 0
    Private strDescription As String = String.Empty
    Private strDescription2 As String = String.Empty
    Private strPicturePath As String = String.Empty
    Private strProgramPath As String = String.Empty
    Private strGlobalProgramPath As String = String.Empty
    Private lListElement As New Dictionary(Of String, Object)
    Private _Object As New Object
    Public Const Name As String = "VariantCfg"
    Private bJump As Boolean = False
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

    Public Property ListElement As Dictionary(Of String, Object)

        Set(ByVal value As Dictionary(Of String, Object))
            SyncLock _Object
                lListElement = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return lListElement
            End SyncLock
        End Get

    End Property

    Public Property [Variant] As String

        Set(ByVal value As String)
            SyncLock _Object
                strVariant = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strVariant
            End SyncLock
        End Get
    End Property


    Public Property SFC As String

        Set(ByVal value As String)
            SyncLock _Object
                strSFC = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strSFC
            End SyncLock
        End Get

    End Property


    Public Property Jump As Boolean

        Set(ByVal value As Boolean)
            SyncLock _Object
                bJump = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return bJump
            End SyncLock
        End Get

    End Property

    Public ReadOnly Property Program As String

        Get
            SyncLock _Object
                If strProgramPath = "" Then
                    Return ""
                End If
                Dim cProgram() As String = strProgramPath.Split("\")
                If cProgram.Length <= 0 Then Return ""
                Dim strTemp As String = cProgram(cProgram.Length - 1)
                Return strTemp.Replace(".ini", "").Replace(".INI", "")
            End SyncLock
        End Get

    End Property

    Public Property CarrierID As String

        Set(ByVal value As String)
            SyncLock _Object
                iCarrierID = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return iCarrierID
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

    Public Property PicturePath As String

        Set(ByVal value As String)
            SyncLock _Object
                strPicturePath = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strPicturePath
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

    Public Property GlobalProgramPath As String

        Set(ByVal value As String)
            SyncLock _Object
                strGlobalProgramPath = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strGlobalProgramPath
            End SyncLock
        End Get
    End Property

    Sub New()
        SyncLock _Object

        End SyncLock
    End Sub


    Sub New(ByVal strID As String, ByVal strVariant As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strPicturePath As String, ByVal strProgramPath As String, ByVal strGlobalProgramPath As String, ByVal lListElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.strID = strID
            Me.strVariant = strVariant
            Me.Description = strDescription
            Me.Description2 = strDescription2
            Me.strPicturePath = strPicturePath
            Me.strProgramPath = strProgramPath
            Me.strGlobalProgramPath = strGlobalProgramPath
            Me.lListElement = lListElement
        End SyncLock
    End Sub
End Class

Public Class clsVariantData
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

    Public Function InSertData(ByVal strID As String, ByVal strVariant As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strPicturePath As String, ByVal strProgramPath As String, ByVal strGlobalProgramPath As String, ByVal lListElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("VariantName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("PicturePath")
                lListKey.Add("ProgramPath")
                lListKey.Add("GlobalProgramPath")
                For Each mKey As String In lListElement.Keys
                    lListKey.Add(mKey)
                Next
                lListValue.Add(strID)
                lListValue.Add(strVariant)
                lListValue.Add(strDescription)
                lListValue.Add(strDescription2)
                lListValue.Add(clsSystemPath.ToIniPath(strPicturePath))
                lListValue.Add(clsSystemPath.ToIniPath(strProgramPath))
                lListValue.Add(strGlobalProgramPath)
                For Each mValue As String In lListElement.Values
                    lListValue.Add(mValue)
                Next
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.VariantConfig, "Variant" + strID, lListKey.ToArray,
                                      lListValue.ToArray)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strVariant As String, ByVal strDescription As String, ByVal strDescription2 As String, ByVal strPicturePath As String, ByVal strProgramPath As String, ByVal strGlobalProgramPath As String, ByVal lListElement As Dictionary(Of String, Object)) As Boolean

        SyncLock _Object
            Try
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("VariantName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("PicturePath")
                lListKey.Add("ProgramPath")
                lListKey.Add("GlobalProgramPath")
                For Each mKey As String In lListElement.Keys
                    lListKey.Add(mKey)
                Next
                lListValue.Add(strID)
                lListValue.Add(strVariant)
                lListValue.Add(strDescription)
                lListValue.Add(strDescription2)
                lListValue.Add(clsSystemPath.ToIniPath(strPicturePath))
                lListValue.Add(clsSystemPath.ToIniPath(strProgramPath))
                lListValue.Add(strGlobalProgramPath)
                For Each mValue As String In lListElement.Values
                    lListValue.Add(mValue)
                Next
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.VariantConfig, "Variant" + strID, lListKey.ToArray,
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
                Return cIniHandler.RemoveSection(cSystemManager.Settings.VariantConfig, "Variant" + strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadVariant(ByRef lVariantList As List(Of clsVariantCfg)) As Boolean

        SyncLock _Object
            Try
                lVariantList.Clear()
                Dim lListKey As New List(Of String)
                Dim lListValue As New List(Of String)
                lListKey.Add("ID")
                lListKey.Add("VariantName")
                lListKey.Add("Description")
                lListKey.Add("Description2")
                lListKey.Add("PicturePath")
                lListKey.Add("ProgramPath")
                lListKey.Add("GlobalProgramPath")
                For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                    lListKey.Add(mKey)
                Next

                Dim cVariantElement As clsVariantCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.VariantConfig, "Variant", lListKey.ToArray)
                    cVariantElement = New clsVariantCfg
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("VariantName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("VariantName"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.Variant = CType(element("VariantName"), String)
                    End If
                    If CType(element("Description"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Description"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.Description = CType(element("Description"), String)
                    End If
                    If CType(element("Description2"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Description2"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.Description2 = CType(element("Description2"), String)
                    End If
                    If CType(element("PicturePath"), String) <> clsXmlHandler.s_DEFAULT And CType(element("PicturePath"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.PicturePath = clsSystemPath.ToSystemPath(CType(element("PicturePath"), String))
                    End If

                    If CType(element("ProgramPath"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ProgramPath"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.ProgramPath = clsSystemPath.ToSystemPath(CType(element("ProgramPath"), String))
                    Else
                        cVariantElement.ProgramPath = cSystemManager.Settings.VariantFolder + "\" + cVariantElement.Variant + ".ini"
                    End If

                    If CType(element("GlobalProgramPath"), String) <> clsXmlHandler.s_DEFAULT And CType(element("GlobalProgramPath"), String) <> clsXmlHandler.s_Null Then
                        cVariantElement.GlobalProgramPath = CType(element("GlobalProgramPath"), String)
                    Else
                        cVariantElement.GlobalProgramPath = "GlobalAction"
                    End If

                    For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                        If CType(element(mKey), String) <> clsXmlHandler.s_DEFAULT And CType(element(mKey), String) <> clsXmlHandler.s_Null Then
                            cVariantElement.ListElement.Add(mKey, CType(element(mKey), String))
                        Else
                            If mKey = "Single Header" Then
                                cVariantElement.ListElement.Add(mKey, "TRUE")
                            ElseIf mKey = "Double Header" Then
                                cVariantElement.ListElement.Add(mKey, "FALSE")
                            Else
                                cVariantElement.ListElement.Add(mKey, "")
                            End If

                        End If
                    Next
                    lVariantList.Add(cVariantElement)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function SaveVariant(ByVal lVariantList As List(Of clsVariantCfg)) As Boolean

        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(cSystemManager.Settings.VariantConfig, "Variant")
                For Each element As clsVariantCfg In lVariantList
                    InSertData(element.ID, element.Variant, element.Description, element.Description2, clsSystemPath.ToIniPath(element.PicturePath), clsSystemPath.ToIniPath(element.ProgramPath), element.GlobalProgramPath, element.ListElement)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Enum enumSelectVariantType
    Manual = 0
    Auto
End Enum