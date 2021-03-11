Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsActionDataManager
    Private cActionData As New clsActionData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cMachineManager As clsMachineManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private cDs_Data As DataSet
    Private cDs_Analysis As DataSet
    Private _Object As New Object
    Public Const Name As String = "ActionDataManager"

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
                cActionData.Init(cSystemElement)
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
                Return cActionData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function InSertData(ByVal strStation As String, ByVal strSFC As String, ByVal strVariant As String, ByVal strStage As String, ByVal strActionID As String, ByVal strActionName As String, ByVal strResult As String, ByVal strErrorMessage As String, ByVal cStartTime As DateTime, ByVal cEndTime As DateTime) As Boolean
        SyncLock _Object
            Try
                If cActionData.InSertData(strStation, strSFC, strVariant, strStage, strActionID, strActionName, strResult, strErrorMessage, cStartTime, cEndTime) Then
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



    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Return cActionData.DeleteData(strID)
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
                cActionData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetTextLine("ActionDataManager", mDc.ColumnName)
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


Public Class clsActionData
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
                strCheckTable = " select * from hmidatabase.Actiontable"
                strCreateTable = "CREATE TABLE hmidatabase.Actiontable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Station` VARCHAR(45) NOT NULL," & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `Variant` VARCHAR(20) NOT NULL," & _
                                    " `Stage` VARCHAR(25) NOT NULL," & _
                                    " `ActionID` VARCHAR(5) NOT NULL," & _
                                    " `ActionName` VARCHAR(100) NOT NULL," & _
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
                strDelCmd = "DELETE FROM hmidatabase.Actiontable where StartTime < '" & Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strStation As String, ByVal strSFC As String, ByVal strVariant As String, ByVal strStage As String, ByVal strActionID As String, ByVal strActionName As String, ByVal strResult As String, ByVal strErrorMessage As String, ByVal cStartTime As DateTime, ByVal cEndTime As DateTime) As Boolean
        SyncLock _Object
            Try

                If strErrorMessage.Length >= 253 Then
                    strErrorMessage = strErrorMessage.Substring(0, 253)
                Else
                    strErrorMessage = strErrorMessage.Substring(0, strErrorMessage.Length)
                End If
                Dim dTime As Double = 0
                Dim ts As TimeSpan = cEndTime.Subtract(cStartTime).Duration()
                dTime = System.Math.Round(ts.TotalSeconds, 3)
                Dim strInserCmd As String = String.Empty
                strErrorMessage = strErrorMessage.Replace("'", "\'")
                strActionName = strActionName.Replace("'", "\'")
                strInserCmd = "insert into hmidatabase.Actiontable (`Station`,`SFC`,`Variant`,`Stage`,`ActionID`,`ActionName`, `Time`, `Result`, `ErrorMessage`, `StartTime`, `EndTime`) values ('" & strStation & "','" & strSFC & "', '" & strVariant & "','" & strStage & "','" & strActionID & "','" & strActionName & "','" & dTime & "', '" & strResult & "', '" & strErrorMessage & "', '" & cStartTime.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & cEndTime.ToString("yyyy-MM-dd HH:mm:ss") & "')"
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
                strDelCmd = "DELETE FROM hmidatabase.Actiontable where ID = '" & strID & "'"
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
                        strSearchCmd = "where Stage = '" & cListSearchContion(6) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Stage = '" & cListSearchContion(6) & "'"
                    End If
                End If

                If cListSearchContion(7) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where ActionId = '" & cListSearchContion(7) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and ActionId = '" & cListSearchContion(7) & "'"
                    End If
                End If

                If cListSearchContion(8) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where ActionName = '" & cListSearchContion(8) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and ActionName = '" & cListSearchContion(8) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID,Station,SFC,Variant,Stage,ActionID,ActionName, convert(Time,decimal(18,2)) as Time,Result,ErrorMessage,StartTime,EndTime from hmidatabase.Actiontable order by ID desc "
                Else
                    strInquiryCmd = "select ID,Station,SFC,Variant, Stage,ActionID,ActionName,convert(Time,decimal(18,2)) as Time,Result,ErrorMessage,StartTime,EndTime from hmidatabase.Actiontable " & strSearchCmd & " order by ID desc "
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


