Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsMachineDataManager
    Private cMachineData As New clsMachineData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDs_Data As DataSet
    Private cDs_Analysis As DataSet
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private strTempValue As String
    Private cTempManchineActionCfg As clsManchineActionCfg
    Private lListManchineActionCfg As New Dictionary(Of String, clsManchineActionCfg)
    Private _Object As New Object
    Public Const Name As String = "MachineDataManager"

    Public ReadOnly Property Ds_Data
        Get
            SyncLock _Object
                Return cDs_Data
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property Ds_Analysis
        Get
            SyncLock _Object
                Return cDs_Analysis
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                lListManchineActionCfg.Clear()
                For Each element In [Enum].GetValues(GetType(enumManchineActionType))
                    cTempManchineActionCfg = New clsManchineActionCfg
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", element.ToString, "InsertValue")
                    cTempManchineActionCfg.InsertValue = IIf(strTempValue = "True", True, False)
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", element.ToString, "UpdateValue")
                    cTempManchineActionCfg.UpdateValue = IIf(strTempValue = "True", True, False)
                    lListManchineActionCfg.Add(element.ToString, cTempManchineActionCfg)
                Next
                cMachineData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage, ByVal cHMIDataView_Data As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage_Data = cDataGridViewPage_Data
            Me.cHMIDataView_Data = cHMIDataView_Data
            Return True
        End SyncLock
    End Function


    Public Function CreateDB() As Boolean
        SyncLock _Object
            Try
                Return cMachineData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetManchineActionListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lListManchineActionCfg.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetManchineActionCfgFromKey(ByVal iKey As Integer) As clsManchineActionCfg
        SyncLock _Object
            Try
                If iKey <= lListManchineActionCfg.Count - 1 Then
                    Return lListManchineActionCfg(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetManchineActionCfgFromName(ByVal strName As String) As clsManchineActionCfg
        SyncLock _Object
            Try
                If lListManchineActionCfg.ContainsKey(strName) Then
                    Return lListManchineActionCfg(strName)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function InSertData(ByVal strAction As String, ByVal strDescription As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try
                If cMachineData.InSertData(strAction, strDescription, 0, strStartTime, "") Then
                    lListManchineActionCfg(strAction).InsertValue = True
                    lListManchineActionCfg(strAction).UpdateValue = False
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", strAction, "InsertValue", lListManchineActionCfg(strAction).InsertValue.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", strAction, "UpdateValue", lListManchineActionCfg(strAction).UpdateValue.ToString)
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strAction As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                If cMachineData.UpdateData(strAction, strEndTime) Then
                    lListManchineActionCfg(strAction).UpdateValue = True
                    lListManchineActionCfg(strAction).InsertValue = False
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", strAction, "InsertValue", lListManchineActionCfg(strAction).InsertValue.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineData.ini", strAction, "UpdateValue", lListManchineActionCfg(strAction).UpdateValue.ToString)
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try

        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Return cMachineData.DeleteData(strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cDs_Data = New DataSet
                cMachineData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("MachineDataManager", mDc.ColumnName)
                Next

                If Not IsNothing(cDataGridViewPage_Data) Then
                    cDataGridViewPage_Data.SetDataView = cDs_Data.Tables(0).DefaultView
                    cDataGridViewPage_Data.Paging(cViewPageType)
                Else
                    cHMIDataView_Data.DataSource = cDs_Data.Tables(0)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SelectToAnayliseView(ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cDs_Analysis = New DataSet
                cMachineData.SelectToAnalysisView(cDs_Analysis, cListSearchContion)
                For Each mDc As DataColumn In cDs_Analysis.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("MachineDataManager", mDc.ColumnName)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class


Public Class clsMachineData
    Private cMySqlAdapter As New clsMySqlAdapter
    Private cMachineManager As clsMachineManager
    Private _Object As New Object
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                Dim strCmd As String = String.Empty
                strCmd = "Persist Security Info=False;server=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseIP) + ";user id=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseName) + "; pwd=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBasePassword) + ""
                If cMySqlAdapter.Init(strCmd) Then
                    If CreateDB() Then
                        DeleteData()
                    End If
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function CreateDB() As Boolean
        SyncLock _Object
            Try
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.machinetable"
                strCreateTable = "CREATE TABLE `hmidatabase`.`machinetable`" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Action` VARCHAR(20) NOT NULL," & _
                                    " `Description` VARCHAR(45) NOT NULL," & _
                                    " `Duration` DOUBLE NOT NULL," & _
                                    " `StartTime` VARCHAR(25) NOT NULL," & _
                                    " `EndTime` VARCHAR(25) NOT NULL," & _
                                    " PRIMARY KEY (`ID`)" & _
                                    ")"
                Return cMySqlAdapter.CreateDB(strCreateDatabase, strCheckTable, strCreateTable)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.machinetable where StartTime < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strAction As String, ByVal strDescription As String, ByVal dDuration As Double, ByVal strStartTime As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.machinetable (`Action`,`Description`, `Duration`, `StartTime`, `EndTime`) values ('" & strAction & "', '" & strDescription & "'," & dDuration & ", '" & strStartTime & "', '" & strEndTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strAction As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.machinetable where Action='" + strAction + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 4}, cTagValue) Then
                    Dim ts1 As TimeSpan = New TimeSpan(DateTime.Parse(cTagValue(1).ToString).Ticks)
                    Dim ts2 As TimeSpan = New TimeSpan(DateTime.Parse(strEndTime).Ticks)
                    Dim ts As TimeSpan = ts2.Subtract(ts1).Duration()
                    dDuration = System.Math.Round(ts.TotalMinutes, 3)
                    strUpdateCmd = "UPDATE hmidatabase.machinetable set `Duration`=" & dDuration.ToString & ",`EndTime`='" & strEndTime & "' where Action='" & strAction & "' and ID=" & cTagValue(0).ToString & ""
                    Return cMySqlAdapter.UpdateData(strUpdateCmd)
                End If
                Return True
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
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.machinetable where ID = '" & strID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToAnalysisView(ByRef Ds As DataSet, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim strInquiryCmd As String = String.Empty
                Dim strSearchCmd As String = String.Empty

                If cListSearchContion(0) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where StartTime>='" & cListSearchContion(0) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and StartTime>='" & cListSearchContion(0) & "'"
                    End If
                End If

                If cListSearchContion(1) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where StartTime<='" & cListSearchContion(1) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and StartTime<='" & cListSearchContion(1) & "'"
                    End If
                End If

                If cListSearchContion(2) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Action ='" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Action='" & cListSearchContion(2) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "SELECT Action , count(*) AS TotalCount ,convert(sum(Duration),decimal(18,3)) as  TotalTime FROM hmidatabase.machinetable group by Action order by TotalCount DESC "
                Else
                    strInquiryCmd = "SELECT Action , count(*) AS TotalCount ,convert(sum(Duration),decimal(18,3)) as  TotalTime FROM hmidatabase.machinetable " & strSearchCmd & " group by Action order by TotalCount DESC "
                End If

                Return cMySqlAdapter.SelectToDataView(strInquiryCmd, Ds)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToDataView(ByRef Ds As DataSet, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim strInquiryCmd As String = String.Empty
                Dim strSearchCmd As String = String.Empty

                If cListSearchContion(0) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where StartTime>='" & cListSearchContion(0) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and StartTime>='" & cListSearchContion(0) & "'"
                    End If
                End If

                If cListSearchContion(1) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where StartTime<='" & cListSearchContion(1) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and StartTime<='" & cListSearchContion(1) & "'"
                    End If
                End If

                If cListSearchContion(2) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Action ='" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Action='" & cListSearchContion(2) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID,Action,Description, convert(Duration,decimal(18,3)) as Duration,StartTime,EndTime  from hmidatabase.machinetable order by ID desc "
                Else
                    strInquiryCmd = "select ID,Action,Description, convert(Duration,decimal(18,3)) as Duration,StartTime,EndTime  from hmidatabase.machinetable " & strSearchCmd & " order by ID desc "
                End If
                Return cMySqlAdapter.SelectToDataView(strInquiryCmd, Ds)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Enum enumManchineActionType
    PowerOn = 1
    PowerOff
    Auto
    Manual
    Work
    Waiting
    Alarm
End Enum

Public Class clsManchineActionCfg
    Protected bClickValue As Boolean = False
    Protected bInsertValue As Boolean = False
    Protected bUpdateValue As Boolean = False
    Protected bLastStatusValue As Boolean = False
    Private _Object As New Object

    Public Property InsertValue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bInsertValue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bInsertValue
            End SyncLock
        End Get
    End Property
    Public Property UpdateValue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bUpdateValue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bUpdateValue
            End SyncLock
        End Get
    End Property
    Public Property ClickValue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bClickValue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bClickValue
            End SyncLock
        End Get
    End Property
    Public Property LastStatusValue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bLastStatusValue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bLastStatusValue
            End SyncLock
        End Get
    End Property
End Class


