Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsAlarmDataManager
    Private cAlarmData As New clsAlarmData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDataGridViewPage_Analysis As clsDataGridViewPage
    Private cHMIDataView_Analysis As HMIDataView
    Private cDs_Data As DataSet
    Private cDs_Analysis As DataSet
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Public Const Name As String = "AlarmDataManager"

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

    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage, ByVal cHMIDataView_Data As HMIDataView, ByVal cDataGridViewPage_Analysis As clsDataGridViewPage, ByVal cHMIDataView_Analysis As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage_Data = cDataGridViewPage_Data
            Me.cHMIDataView_Data = cHMIDataView_Data
            Me.cDataGridViewPage_Analysis = cDataGridViewPage_Analysis
            Me.cHMIDataView_Analysis = cHMIDataView_Analysis
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cAlarmData.Init(cSystemElement)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
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
                Return cAlarmData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strErrorCode As String, ByVal strDescription As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try
                Return cAlarmData.InSertData(strErrorCode, strDescription, 0, strStartTime, "")
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strErrorCode As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                Return cAlarmData.UpdateData(strErrorCode, strEndTime)
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
                Return cAlarmData.DeleteData(strID)
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
                cAlarmData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("AlarmDataManager", mDc.ColumnName)
                Next

                If Not IsNothing(cDataGridViewPage_Data) Then
                    cDataGridViewPage_Data.SetDataView = cDs_Data.Tables(0).DefaultView
                    cDataGridViewPage_Data.Paging(cViewPageType)
                Else
                    cHMIDataView_Data.DataSource = cDs_Data.Tables(0)
                End If

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToAnayliseView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cDs_Analysis = New DataSet
                cAlarmData.SelectToAnalysisView(cDs_Analysis, cListSearchContion)
                For Each mDc As DataColumn In cDs_Analysis.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("AlarmDataManager", mDc.ColumnName)
                Next

                If Not IsNothing(cDataGridViewPage_Analysis) Then
                    cDataGridViewPage_Analysis.SetDataView = cDs_Analysis.Tables(0).DefaultView
                    cDataGridViewPage_Analysis.Paging(cViewPageType)
                Else
                    cHMIDataView_Analysis.DataSource = cDs_Analysis.Tables(0)
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


End Class


Public Class clsAlarmData
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

    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.alarmtable where StartTime < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
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
                If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.AutoCreateDB).ToString.ToUpper <> "TRUE" Then Return True
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.alarmtable"
                strCreateTable = "CREATE TABLE `HMIDatabase`.`AlarmTable`" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `ErrorCode` VARCHAR(20) NOT NULL," & _
                                    " `Description` VARCHAR(255) NOT NULL," & _
                                    " `Duration` DOUBLE NOT NULL," & _
                                    " `StartTime` VARCHAR(25) NOT NULL," & _
                                    " `EndTime` VARCHAR(25) NOT NULL," & _
                                    " PRIMARY KEY ( `ID`  )" & _
                                    " )"
                Return cMySqlAdapter.CreateDB(strCreateDatabase, strCheckTable, strCreateTable)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strErrorCode As String, ByVal strDescription As String, ByVal dDuration As Double, ByVal strStartTime As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                If strDescription.Length >= 253 Then
                    strDescription = strDescription.Substring(0, 253)
                Else
                    strDescription = strDescription.Substring(0, strDescription.Length)
                End If
                strDescription = strDescription.Replace("'", "\'")
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.alarmtable (`ErrorCode`,`Description`, `Duration`, `StartTime`, `EndTime`) values ('" & strErrorCode & "', '" & strDescription & "'," & dDuration & ", '" & strStartTime & "', '" & strEndTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strErrorCode As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.alarmtable where ErrorCode='" + strErrorCode + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 4}, cTagValue) Then
                    Dim ts1 As TimeSpan = New TimeSpan(DateTime.Parse(cTagValue(1).ToString).Ticks)
                    Dim ts2 As TimeSpan = New TimeSpan(DateTime.Parse(strEndTime).Ticks)
                    Dim ts As TimeSpan = ts2.Subtract(ts1).Duration()
                    dDuration = System.Math.Round(ts.TotalMinutes, 3)
                    strUpdateCmd = "UPDATE hmidatabase.alarmtable set `Duration`=" & dDuration.ToString & ",`EndTime`='" & strEndTime & "' where ErrorCode='" & strErrorCode & "' and ID=" & cTagValue(0).ToString & ""
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
                strDelCmd = "DELETE FROM hmidatabase.alarmtable where ID = '" & strID & "'"
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
                        strSearchCmd = "where ErrorCode Like '%" & cListSearchContion(2) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and ErrorCode Like '%" & cListSearchContion(2) & "%'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Description Like '%" & cListSearchContion(3) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and Description Like '%" & cListSearchContion(3) & "%'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "SELECT ErrorCode , count(*) AS TotalCount ,convert(sum(Duration),decimal(18,3)) as TotalTime FROM hmidatabase.alarmtable group by ErrorCode order by TotalCount DESC "
                Else
                    strInquiryCmd = "SELECT ErrorCode , count(*) AS TotalCount ,convert(sum(Duration),decimal(18,3)) as TotalTime FROM hmidatabase.alarmtable " & strSearchCmd & " group by ErrorCode order by TotalCount DESC "
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
                        strSearchCmd = "where ErrorCode Like '%" & cListSearchContion(2) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and ErrorCode Like '%" & cListSearchContion(2) & "%'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Description Like '%" & cListSearchContion(3) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and Description Like '%" & cListSearchContion(3) & "%'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID,ErrorCode,Description, convert(Duration,decimal(18,3)) as Duration,StartTime,EndTime from hmidatabase.alarmtable order by ID desc "
                Else
                    strInquiryCmd = "select ID,ErrorCode,Description, convert(Duration,decimal(18,3)) as Duration,StartTime,EndTime from hmidatabase.alarmtable " & strSearchCmd & " order by ID desc "
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
