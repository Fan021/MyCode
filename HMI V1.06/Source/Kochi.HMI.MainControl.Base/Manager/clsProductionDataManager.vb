Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsProductionDataManager
    Private cProductionData As New clsProductionData
    Private cProductionMESData As New clsProductionMESData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDataGridViewPage_Analysis As clsDataGridViewPage
    Private cHMIDataView_Analysis As HMIDataView
    Private cMachineManager As clsMachineManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private cDs_Data As DataSet
    Private cDs_Analysis As DataSet
    Private _Object As New Object
    Public Const Name As String = "ProductionDataManager"

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
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cProductionData.Init(cSystemElement)
                cProductionMESData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage, ByVal cHMIDataView_Data As HMIDataView, ByVal cDataGridViewPage_Analysis As clsDataGridViewPage, ByVal cHMIDataView_Analysis As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage_Data = cDataGridViewPage_Data
            Me.cHMIDataView_Data = cHMIDataView_Data
            Me.cDataGridViewPage_Analysis = cDataGridViewPage_Analysis
            Me.cHMIDataView_Analysis = cHMIDataView_Analysis
            Return True
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage, ByVal cHMIDataView_Data As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage_Data = cDataGridViewPage_Data
            Me.cHMIDataView_Data = cHMIDataView_Data
            Me.cDataGridViewPage_Analysis = cDataGridViewPage_Analysis
            Me.cHMIDataView_Analysis = cHMIDataView_Analysis
            Return True
        End SyncLock
    End Function

    Public Function CreateDB() As Boolean
        SyncLock _Object
            Try
                Return cProductionData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function InSertData(ByVal strStation As String, ByVal strCarrierID As String, ByVal strSFC As String, ByVal strVariant As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try
                If cProductionData.InSertData(strStation, strCarrierID, strSFC, strVariant, 0, "", "", strStartTime, "") Then
                    Return True
                Else
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function InSertData(ByVal strStation As String, ByVal strCarrierID As String, ByVal strSFC As String, ByVal strVariant As String, ByVal strResult As String, ByVal strErrorMessage As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try
                If cProductionData.InSertData(strStation, strCarrierID, strSFC, strVariant, 0, strResult, strErrorMessage, strStartTime, "") Then
                    Return True
                Else
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckStationResult(ByVal strStation As String, ByVal strSFC As String, ByVal strVariant As String, ByRef strResult As String) As Integer
        SyncLock _Object
            Try
                Return cProductionMESData.CheckStationResult(strStation, strSFC, strVariant, strResult)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strStation As String, ByVal strVariant As String, ByVal strSFC As String, ByVal strResult As String, ByVal strErrorMessage As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                If strResult <> "ESTOP" Then
                    cProductionMESData.InSertData(strVariant, strSFC, strStation, strResult)
                End If

                If strResult = "FAILNEXT" Then
                    strResult = "FAIL"
                End If
                If strResult = "ESTOP" Then
                    strResult = "FAIL"
                End If
                If cProductionData.UpdateData(strStation, strSFC, strResult, strErrorMessage, strEndTime) Then
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
                    Return cProductionData.DeleteData(strID)
                Catch ex As Exception
                    Throw New clsHMIException(ex, enumExceptionType.Crash)
                    Return False
                End Try
        End SyncLock
    End Function

    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cDs_Data = New DataSet
                cProductionData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("ProductionDataManager", mDc.ColumnName)
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

    Public Function SelectControlDataToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cDs_Data = New DataSet
                cProductionMESData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("ProductionDataManager", mDc.ColumnName)
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

    Public Function HasControlDataSFC(ByVal strSFC As String) As Boolean
        SyncLock _Object
            Try
                Return cProductionMESData.HasSFC(strSFC)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetControlDataSFC(ByVal strSFC As String, ByRef strResult As String) As Boolean
        SyncLock _Object
            Try
                Return cProductionMESData.GetData(strSFC, strResult)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertControlDataData(ByVal strVariant As String, ByVal strSFC As String, ByVal strStation As String, ByVal strResult As String) As Boolean
        SyncLock _Object
            Try
                If cProductionMESData.InSertData(strVariant, strSFC, strStation, strResult) Then
                    Return True
                Else
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateControlDataSFC(ByVal strSFC As String, ByVal strResult As String) As Boolean
        SyncLock _Object
            Try
                Return cProductionMESData.UpdateData(strSFC, strResult)
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
                    Dim strOrderByConditon As String = String.Empty
                    cDs_Analysis = New DataSet
                    If cMachineManager.MachineCellManager.MachineCellCfg.CellName <> "" Then
                        strOrderByConditon = "'" & cMachineManager.MachineCellManager.MachineCellCfg.CellName & "'"
                    End If

                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If strOrderByConditon = "" Then
                        strOrderByConditon = "'" & element.StationName & "'"
                    Else
                        strOrderByConditon = strOrderByConditon + ",'" & element.StationName & "'"
                    End If
                Next

                cProductionData.SelectToAnalysisView(cDs_Analysis, strOrderByConditon, cListSearchContion)
                For Each mDc As DataColumn In cDs_Analysis.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("ProductionDataManager", mDc.ColumnName)
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


Public Class clsProductionData
    Private cMySqlAdapter As New clsMySqlAdapter
    Private _Object As New Object
    Private cMachineManager As clsMachineManager
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
                If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.AutoCreateDB).ToString.ToUpper <> "TRUE" Then Return True
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.productiontable"
                strCreateTable = "CREATE TABLE hmidatabase.productiontable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Station` VARCHAR(45) NOT NULL," & _
                                    " `CarrierId` VARCHAR(5) NOT NULL," & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `Variant` VARCHAR(20) NOT NULL," & _
                                    " `Time` DOUBLE NOT NULL," & _
                                    " `Result` VARCHAR(10) NOT NULL," & _
                                    " `ErrorMessage` VARCHAR(255) NOT NULL," & _
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

    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.productiontable where StartTime < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strStation As String, ByVal strCarrierID As String, ByVal strSFC As String, ByVal strVariant As String, ByVal dTime As Double, ByVal strResult As String, ByVal strErrorMessage As String, ByVal strStartTime As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                If strErrorMessage.Length >= 253 Then
                    strErrorMessage = strErrorMessage.Substring(0, 253)
                Else
                    strErrorMessage = strErrorMessage.Substring(0, strErrorMessage.Length)
                End If
                strErrorMessage = strErrorMessage.Replace("'", "\'")

                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.productiontable (`Station`,`CarrierId`,`SFC`,`Variant`, `Time`, `Result`, `ErrorMessage`, `StartTime`, `EndTime`) values ('" & strStation & "','" & strCarrierID & "','" & strSFC & "', '" & strVariant & "','" & dTime & "', '" & strResult & "', '" & strErrorMessage & "', '" & strStartTime & "', '" & strEndTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strStation As String, ByVal strSFC As String, ByVal strResult As String, ByVal strErrorMessage As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                If strErrorMessage.Length >= 253 Then
                    strErrorMessage = strErrorMessage.Substring(0, 253)
                Else
                    strErrorMessage = strErrorMessage.Substring(0, strErrorMessage.Length)
                End If
                strErrorMessage = strErrorMessage.Replace("'", "\'")
                If strResult = "" Then strResult = "FAIL"
                strInquiryCmd = "select * from hmidatabase.productiontable where SFC='" + strSFC + "' and Station='" + strStation + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 8}, cTagValue) Then
                    Try
                        Dim ts1 As TimeSpan = New TimeSpan(DateTime.Parse(cTagValue(1).ToString).Ticks)
                        Dim ts2 As TimeSpan = New TimeSpan(DateTime.Parse(strEndTime).Ticks)
                        Dim ts As TimeSpan = ts2.Subtract(ts1).Duration()
                        dDuration = System.Math.Round(ts.TotalSeconds, 3)
                    Catch ex1 As Exception
                        dDuration = 0
                    End Try
                    strUpdateCmd = "UPDATE hmidatabase.productiontable set `Time`=" & dDuration.ToString & ",`Result`='" & strResult & "',`ErrorMessage`='" & strErrorMessage & "',`EndTime`='" & strEndTime & "' where SFC='" + strSFC + "' and Station='" + strStation + "' and ID=" & cTagValue(0).ToString & ""
                    Return cMySqlAdapter.UpdateData(strUpdateCmd)
                End If
                Return True
            Catch ex As Exception
                  Return True
            End Try
            Return True
        End SyncLock
    End Function

    Public Function CheckStationResult(ByVal strStation As String, ByVal strSFC As String, ByVal strVariant As String) As Integer
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.productiontable where SFC='" + strSFC + "' and Variant='" + strVariant + "' and Station='" + strStation + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 5}, cTagValue) Then
                    If cTagValue(1).ToString.ToUpper = "PASS" Then
                        Return 0
                    Else
                        Return -1
                    End If
                End If
                Return -2
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetSFCMessage(ByVal strStation As String, ByVal strSFC As String, ByRef strErrorMessage As String) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.productiontable where SFC='" + strSFC + "' and Station='" + strStation + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 7}, cTagValue) Then
                    If cTagValue(1).ToString = "" Then
                        Return False
                    Else
                        strErrorMessage = cTagValue(1).ToString
                        Return True
                    End If
                End If
                Return False
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
                strDelCmd = "DELETE FROM hmidatabase.productiontable where ID = '" & strID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToAnalysisView(ByRef Ds As DataSet, ByVal strOrderByConditon As String, ByVal ParamArray cListSearchContion() As String) As Boolean
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
                        strSearchCmd = "where Station = '" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Station = '" & cListSearchContion(2) & "'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Variant = '" & cListSearchContion(3) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Variant = '" & cListSearchContion(3) & "'"
                    End If
                End If

                If cListSearchContion(4) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result = '" & cListSearchContion(4) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Result = '" & cListSearchContion(4) & "'"
                    End If
                Else
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result in ('PASS','FAIL')"
                    Else
                        strSearchCmd = strSearchCmd + " and Result in ('PASS','FAIL')"
                    End If
                End If


                If cListSearchContion(5) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where SFC Like '%" & cListSearchContion(5) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and SFC Like '%" & cListSearchContion(5) & "%'"
                    End If
                End If
                If cListSearchContion(6) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where CarrierId = '" & cListSearchContion(6) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and CarrierId = '" & cListSearchContion(6) & "'"
                    End If
                End If
                If strSearchCmd = "" Then
                    strInquiryCmd = "SELECT Station ,count(Result='PASS'or Result='FAIL'or null) as TotalCount, count( Result='PASS'or null ) as PassCount, count(Result='FAIL' or null ) as FailCount,convert((count(Result='PASS' or null)*100.0/count(Result='PASS'or Result='FAIL'or null)),decimal(18,2)) as FailRate, convert(avg(Time),decimal(18,2)) as AverageTime,  convert(max(Time),decimal(18,2)) as MaxTime,  convert(Min(Time),decimal(18,2)) as MinTime FROM hmidatabase.productiontable group by Station ORDER BY FIELD(`Station`," & strOrderByConditon & ")"
                Else
                    strInquiryCmd = "SELECT Station ,count(Result='PASS'or Result='FAIL'or null) as TotalCount, count( Result='PASS'or null ) as PassCount, count(Result='FAIL' or null) as FailCount,convert((count(Result='PASS' or null)*100.0/count(Result='PASS'or Result='FAIL'or null)),decimal(18,2)) as FailRate,convert(avg(Time),decimal(18,2)) as AverageTime, convert(max(Time),decimal(18,2)) as MaxTime,  convert(Min(Time),decimal(18,2)) as MinTime FROM hmidatabase.productiontable " & strSearchCmd & " group by Station ORDER BY FIELD(`Station`," & strOrderByConditon & ")"
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
                        strSearchCmd = "where Station = '" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Station = '" & cListSearchContion(2) & "'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Variant = '" & cListSearchContion(3) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Variant = '" & cListSearchContion(3) & "'"
                    End If
                End If

                If cListSearchContion(4) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result = '" & cListSearchContion(4) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Result = '" & cListSearchContion(4) & "'"
                    End If
                Else
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result in ('PASS','FAIL')"
                    Else
                        strSearchCmd = strSearchCmd + " and Result in ('PASS','FAIL')"
                    End If
                End If


                If cListSearchContion(5) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where SFC Like '%" & cListSearchContion(5) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and SFC Like '%" & cListSearchContion(5) & "%'"
                    End If
                End If
                
                If cListSearchContion(6) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where CarrierId = '" & cListSearchContion(6) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and CarrierId = '" & cListSearchContion(6) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID,Station,CarrierId,SFC,Variant, convert(Time,decimal(18,2)) as Time,Result,ErrorMessage,StartTime,EndTime from hmidatabase.productiontable order by ID desc "
                Else
                    strInquiryCmd = "select ID,Station,CarrierId,SFC,Variant, convert(Time,decimal(18,2)) as Time,Result,ErrorMessage,StartTime,EndTime from hmidatabase.productiontable " & strSearchCmd & " order by ID desc "
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


Public Class clsProductionMESData
    Private cMySqlAdapter As New clsMySqlAdapter
    Private _Object As New Object
    Private cMachineManager As clsMachineManager
    Private cProductionData As New clsProductionData

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cProductionData.Init(cSystemElement)
                Dim strCmd As String = String.Empty
                strCmd = "Persist Security Info=False;server=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseIP) + ";user id=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseName) + "; pwd=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBasePassword) + ""
                If cMySqlAdapter.Init(strCmd) Then
                    Return CreateDB()
                End If
                DeleteData()
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
                strCheckTable = " select * from hmidatabase.productionmestable"
                strCreateTable = "CREATE TABLE hmidatabase.productionmestable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Variant` VARCHAR(45) NOT NULL," & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `Station` VARCHAR(45) NOT NULL," & _
                                    " `Result` VARCHAR(10) NOT NULL," & _
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

    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.productionmestable where StartTime < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Private Function DeleteData(ByVal strSFC As String, ByVal strStation As String) As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.productionmestable where SFC ='" & strSFC & "' and Station='" & strStation & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function
    Public Function InSertData(ByVal strVariant As String, ByVal strSFC As String, ByVal strStation As String, ByVal strResult As String) As Boolean
        SyncLock _Object
            Try
                DeleteData(strSFC, strStation)
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.productionmestable (`Variant`,`SFC`,`Station`,`Result`,`StartTime`,`EndTime`) values ('" & strVariant & "','" & strSFC & "','" & strStation & "','" & strResult & "', '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strSFC As String, ByVal strResult As String) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strUpdateCmd = "UPDATE hmidatabase.productionmestable set `Result`='" & strResult & "',`StartTime`='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',`EndTime`='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where SFC='" & strSFC & "'"
                Return cMySqlAdapter.UpdateData(strUpdateCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasSFC(ByVal strSFC As String) As Boolean
        SyncLock _Object
            Try
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(1) As Object
                strInquiryCmd = "select * from hmidatabase.productionmestable where SFC='" + strSFC + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0}, cTagValue) Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetData(ByVal strSFC As String, ByRef strResult As String) As Boolean
        SyncLock _Object
            Try
                strResult = ""
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                strInquiryCmd = "select * from hmidatabase.productionmestable where SFC='" + strSFC + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 4}, cTagValue) Then
                    strResult = cTagValue(1)
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function CheckStationResult(ByVal strStation As String, ByVal strSFC As String, ByVal strVariant As String, ByRef strResult As String) As Integer
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.productionmestable where SFC='" + strSFC + "' and Variant='" + strVariant + "' and Station='" + strStation + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 4}, cTagValue) Then
                    If cTagValue(1).ToString.ToUpper = "PASS" Then
                        Return 0
                    ElseIf cTagValue(1).ToString.ToUpper = "FAILNEXT" Then
                        Return 0
                        ' Not Inqueqe
                    ElseIf cTagValue(1).ToString = "NotInqueue" Then
                        If cProductionData.GetSFCMessage(strStation, strSFC, strResult) Then
                            Return -1
                        Else
                            Return -2
                        End If
                        ' Not Inqueqe
                    ElseIf cTagValue(1).ToString = "NotInque2" Then
                        If cProductionData.GetSFCMessage(strStation, strSFC, strResult) Then
                            Return -6
                        Else
                            Return -2
                        End If
                    ElseIf cTagValue(1).ToString = "ESTOP" Then 'HAN MES
                        Return -3
                    ElseIf cTagValue(1).ToString = "Abort" Then 'HAN MES
                        Return -7
                    Else
                        Return -4 'shibai
                    End If
                Else
                    Return -5 ' meiyou ceshi
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return -5
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.productionmestable where ID = '" & strID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
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
                        strSearchCmd = "where SFC Like '%" & cListSearchContion(2) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and SFC Like '%" & cListSearchContion(3) & "%'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Station = '" & cListSearchContion(3) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Station = '" & cListSearchContion(3) & "'"
                    End If
                End If

                
                If strSearchCmd = "" Then
                    strInquiryCmd = "select * from hmidatabase.productionmestable order by ID desc "
                Else
                    strInquiryCmd = "select * from hmidatabase.productionmestable " & strSearchCmd & " order by ID desc "
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

Public Class clsProductionCfg
    Protected bInsertValue As Boolean = False
    Protected bUpdateValue As Boolean = False
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
End Class
