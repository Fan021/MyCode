Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base

Public Class clsGapFillerDataManager
    Private cGapFillerData As New clsGapFillerData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private cHMIDataView_Data As HMIDataView
    Private cDs_Data As DataSet
    Private _Object As New Object
    Public Const Name As String = "GapFillerDataManager"

    Public ReadOnly Property Ds_Data As DataSet
        Get
            SyncLock _Object
                Return cDs_Data
            End SyncLock
        End Get
    End Property

    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage, ByVal cHMIDataView_Data As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage_Data = cDataGridViewPage_Data
            Me.cHMIDataView_Data = cHMIDataView_Data
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cGapFillerData.Init(cSystemElement)
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
                Return cGapFillerData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(
                               ByVal strComponent As String,
                               ByVal strShot As String,
                               ByVal dLowValue As Double,
                               ByVal dMeasuredValue As Double,
                               ByVal dUpValue As Double,
                               ByVal strResult As String,
                               ByVal strTime As String
                               ) As Boolean
        SyncLock _Object
            Try
                Return cGapFillerData.InSertData(strComponent,
                                             strShot,
                                             dLowValue,
                                             dMeasuredValue,
                                             dUpValue,
                                             strResult,
                                             strTime)
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
                Return cGapFillerData.DeleteData(strID)
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
                cGapFillerData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetUserTextLine("GapFillerDataManager", mDc.ColumnName)
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

End Class


Public Class clsGapFillerData
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
                Return False
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
                strCheckTable = " select * from hmidatabase.Gapfillertable"
                strCreateTable = "CREATE TABLE hmidatabase.Gapfillertable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Component` VARCHAR(45) NOT NULL," & _
                                    " `Shot` VARCHAR(20) NOT NULL," & _
                                    " `LowValue` DOUBLE NOT NULL," & _
                                    " `MeasuredValue` DOUBLE NOT NULL," & _
                                    " `UpValue` DOUBLE NOT NULL," & _
                                    " `Result` VARCHAR(5) NOT NULL," & _
                                    " `Time` VARCHAR(25) NOT NULL," & _
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
                strDelCmd = "DELETE FROM hmidatabase.Gapfillertable where Time < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function
    Public Function InSertData(ByVal strComponent As String,
                               ByVal strShot As String,
                               ByVal dLowValue As Double,
                               ByVal dMeasuredValue As Double,
                               ByVal dUpValue As Double,
                               ByVal strResult As String,
                               ByVal strTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.Gapfillertable (`Component`," & _
                                                                  "`Shot`," & _
                                                                  "`LowValue`, " & _
                                                                  "`MeasuredValue`, " & _
                                                                  "`UpValue`, " & _
                                                                  "`Result`, " & _
                                                                  "`Time`) " & _
                                                                  "values ('" & strComponent & _
                                                                  "', '" & strShot & _
                                                                  "', " & dLowValue & _
                                                                  ", " & dMeasuredValue & _
                                                                  ", " & dUpValue & _
                                                                  ", '" & strResult & _
                                                                  "', '" & strTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
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
                strDelCmd = "DELETE FROM hmidatabase.Gapfillertable where ID = '" & strID & "'"
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
                        strSearchCmd = "where Time>='" & cListSearchContion(0) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Time>='" & cListSearchContion(0) & "'"
                    End If
                End If

                If cListSearchContion(1) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Time<='" & cListSearchContion(1) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Time<='" & cListSearchContion(1) & "'"
                    End If
                End If

                If cListSearchContion(2) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Component = '" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Component = '" & cListSearchContion(2) & "'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Shot = '" & cListSearchContion(3) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Shot = '" & cListSearchContion(3) & "'"
                    End If
                End If

                If cListSearchContion(4) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result = '" & cListSearchContion(4) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Result = '" & cListSearchContion(4) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID," & _
                                    "Component," & _
                                    "Shot," & _
                                    "convert(LowValue,decimal(18,2)) as LowValue," & _
                                    "convert(MeasuredValue,decimal(18,2)) as MeasuredValue," & _
                                    "convert(UpValue,decimal(18,2)) as UpValue," & _
                                    "Result," & _
                                    "Time " & _
                                    "from hmidatabase.Gapfillertable order by ID desc "
                Else
                    strInquiryCmd = "select ID," & _
                                   "Component," & _
                                   "Shot," & _
                                   "convert(LowValue,decimal(18,2)) as LowValue," & _
                                   "convert(MeasuredValue,decimal(18,2)) as MeasuredValue," & _
                                   "convert(UpValue,decimal(18,2)) as UpValue," & _
                                   "Result," & _
                                   "Time " & _
                                   "from hmidatabase.Gapfillertable " & strSearchCmd & " order by ID desc "
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
