Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Xml.Serialization
Imports Kochi.HMI.MainControl.UI

Public Class clsCarrierDataManager
    Private cCarrierData As New clsCarrierData
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Public Const Name As String = "CarrierDataManager"
    Public strIP As String = ""
    Public strUserName As String = ""
    Public strPassWord As String = ""
    Private cMachineManager As clsMachineManager
    Public strEnable As String = ""
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDs_Data As DataSet

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
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                If strEnable <> "TRUE" Then
                    Return True
                End If
                cCarrierData.strIP = strIP
                cCarrierData.strUserName = strUserName
                cCarrierData.strPassWord = strPassWord
                cCarrierData.Init(cSystemElement)

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
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strCarrierID As String, ByVal strStation As String) As Boolean
        SyncLock _Object
            Try
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.InSertData(strCarrierID, strStation, Now.ToString("yyyy-MM-dd HH:mm:ss"))
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strCarrierID As String, ByVal strStation As String) As Boolean
        SyncLock _Object
            Try
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.UpdateData(strCarrierID, strStation, Now.ToString("yyyy-MM-dd HH:mm:ss"))
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
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.DeleteData(strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteCarrierID(ByVal strCarrierID As String) As Boolean
        SyncLock _Object
            Try
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.DeleteCarrierID(strCarrierID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasCarrierID(ByVal strCarrierID As String) As Boolean
        SyncLock _Object
            Try
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.HasCarrierID(strCarrierID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasCarrierID(ByVal strCarrierID As String, ByRef strStation As String) As Boolean
        SyncLock _Object
            Try
                If strEnable <> "TRUE" Then
                    Return True
                End If
                Return cCarrierData.HasCarrierID(strCarrierID, strStation)
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
                If strEnable <> "TRUE" Then
                    Return True
                End If
                cDs_Data = New DataSet
                cCarrierData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetUserTextLine("CarrierManager", mDc.ColumnName)
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
End Class


Public Class clsCarrierData
    Private cMySqlAdapter As New clsMySqlAdapter
    Private cMachineManager As clsMachineManager
    Private _Object As New Object
    Public strIP As String = ""
    Public strUserName As String = ""
    Public strPassWord As String = ""
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                Dim strCmd As String = String.Empty
                strCmd = GetInitCmd(strIP)
                If cMySqlAdapter.Init(strCmd) Then
                    If CreateDB() Then
                        '   DeleteData()
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
    Private Function GetInitCmd(ByVal strIP As String) As String
        Try
            Dim strCmd As String = ""
            Dim strChangedIP As String = ""

            If strIP.IndexOf(".") >= 0 Then
                strChangedIP = strIP
            Else
                Dim mTempHost As String = strIP
                If mTempHost.IndexOf(":") > 0 Then
                    mTempHost = mTempHost.Substring(0, mTempHost.IndexOf(":"))
                Else
                    mTempHost = mTempHost
                End If
                Dim ip() As IPAddress = Dns.GetHostAddresses(mTempHost)
                For i = 0 To ip.Length - 1
                    If ip(i).ToString.IndexOf(".") >= 0 Then
                        strChangedIP = ip(i).ToString
                    End If
                Next
            End If
            strCmd = "Persist Security Info=False;server=" + strChangedIP + ";user id=" + strUserName + "; pwd=" + strPassWord + ""

            Return strCmd
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function


    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.CarrierDatatable where StartTime < '" & Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") & "'"
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
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.CarrierDatatable"
                strCreateTable = "CREATE TABLE `HMIDatabase`.`CarrierDatatable`" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `SFC` VARCHAR(80) NOT NULL," & _
                                    " `Station` VARCHAR(255) NOT NULL," & _
                                    " `StartTime` VARCHAR(25) NOT NULL," & _
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

    Public Function InSertData(ByVal strCarrierID As String, ByVal strStation As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                '  DeleteData()
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.CarrierDatatable (`SFC`,`Station`, `StartTime`) values ('" & strCarrierID & "', '" & strStation & "', '" & strStartTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strCarrierID As String, ByVal strStation As String, ByVal strEndTime As String) As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                Dim dDuration As Double = 0
                strUpdateCmd = "UPDATE hmidatabase.CarrierDatatable set `Station`='" & strStation & "',`StartTime`='" & strEndTime & "' where SFC='" & strCarrierID & "'"
                Return cMySqlAdapter.UpdateData(strUpdateCmd)
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
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.CarrierDatatable where ID = '" & strID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteCarrierID(ByVal strCarrierID As String) As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.CarrierDatatable where SFC = '" & strCarrierID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasCarrierID(ByVal strCarrierID As String) As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(1) As Object
                strInquiryCmd = "select * from hmidatabase.CarrierDatatable where SFC='" + strCarrierID + "' order by ID desc"
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

    Public Function HasCarrierID(ByVal strCarrierID As String, ByRef strStation As String) As Boolean
        SyncLock _Object
            Try
                strStation = ""
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(2) As Object
                strInquiryCmd = "select * from hmidatabase.CarrierDatatable where SFC='" + strCarrierID + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {0, 2}, cTagValue) Then
                    strStation = cTagValue(1).ToString
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


    Public Function SelectToDataView(ByRef Ds As DataSet, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                Dim strInquiryCmd As String = String.Empty
                Dim strSearchCmd As String = String.Empty


                If cListSearchContion(0) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where SFC = '" & cListSearchContion(0) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and SFC = '" & cListSearchContion(0) & "'"
                    End If
                End If

                If cListSearchContion(1) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Station Like '%" & cListSearchContion(1) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and Station Like '%" & cListSearchContion(1) & "%'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "SELECT * FROM hmidatabase.CarrierDatatable order by ID desc"
                Else
                    strInquiryCmd = "SELECT * FROM hmidatabase.CarrierDatatable " & strSearchCmd & "  order by ID desc"
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
