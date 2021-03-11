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
Public Class clsProcessStationManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cIniHandler As New clsIniHandler
    Public lListProcessCfg As New Dictionary(Of String, clsProcessStationCfg)
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Public strIP As String = ""
    Public strUserName As String = ""
    Public strPassWord As String = ""
    Private cProcessStationData As clsProcessStationData
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        Try

            cProcessStationData = New clsProcessStationData
            cProcessStationData.strIP = strIP
            cProcessStationData.strUserName = strUserName
            cProcessStationData.strPassWord = strPassWord
            cProcessStationData.Init(cSystemElement)
            Return cProcessStationData.SelectToDataView(lListProcessCfg)
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function


    Public Function SaveData() As Boolean
        Try

            cProcessStationData.DeleteAllData()
            For Each element As clsProcessStationCfg In lListProcessCfg.Values
                cProcessStationData.InSertData(element.StationName, element.PassStationName, element.FailureStationName)
            Next
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Function GetNextStation(ByVal strStation As String, ByVal bResult As Boolean) As String
        Try
            For Each element As clsProcessStationCfg In lListProcessCfg.Values
                If element.StationName = strStation Then
                    If bResult Then
                        Return element.PassStationName
                    Else
                        Return element.FailureStationName
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

End Class

Public Class clsProcessStationData
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
                    CreateDB()
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


    Public Function CreateDB() As Boolean
        SyncLock _Object
            Try
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.ProcessStationtable"
                strCreateTable = "CREATE TABLE `hmidatabase`.`ProcessStationtable`" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `StationName` VARCHAR(50)," & _
                                    " `PassStationName` VARCHAR(50)," & _
                                    " `FailStationName` VARCHAR(50)NULL," & _
                                    " `StartTime` VARCHAR(25) NOT NULL," & _
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



    Public Function InSertData(ByVal strStationName As String, ByVal strPassStationName As String, ByVal strFailStationStationName As String) As Boolean
        SyncLock _Object
            Try
                Dim strInserCmd As String = String.Empty
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                strInserCmd = "insert into hmidatabase.ProcessStationtable (`StationName`,`PassStationName`, `FailStationName`, `StartTime`) values ('" & strStationName & "', '" & strPassStationName & "','" & strFailStationStationName & "', '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function



    Public Function DeleteAllData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                strDelCmd = "DELETE FROM hmidatabase.ProcessStationtable"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SelectToDataView(ByRef lListProcessCfg As Dictionary(Of String, clsProcessStationCfg)) As Boolean
        SyncLock _Object
            Try
                Dim strInquiryCmd As String = String.Empty
                lListProcessCfg.Clear()
                cMySqlAdapter.connstr = GetInitCmd(strIP)
                strInquiryCmd = "select * from hmidatabase.ProcessStationtable order by ID asc"
                Dim Ds As New DataSet
                cMySqlAdapter.SelectToDataView(strInquiryCmd, Ds)
                Dim iCnt As Integer = 1
                For Each mDc As DataRow In Ds.Tables(0).Rows
                    Dim cProcessCfg As New clsProcessStationCfg
                    cProcessCfg.StationName = mDc(1).ToString
                    cProcessCfg.PassStationName = mDc(2).ToString
                    cProcessCfg.FailureStationName = mDc(3).ToString
                    lListProcessCfg.Add(iCnt, cProcessCfg)
                    iCnt = iCnt + 1
                Next

                Return True
            Catch ex As Exception
                lListProcessCfg.Clear()
                Return True
            End Try

        End SyncLock
    End Function
End Class
