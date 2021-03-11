
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Public Class clsFailureActionManager
    Private cFailureAction As New clsFailureAction
    Private cMachineManager As clsMachineManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Public Const Name As String = "FailureActionManager"

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cFailureAction.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function
    Public Function InSertData(ByVal strSFC As String, ByVal strStationID As String, ByVal cActionResultCfg As clsActionResultCfg) As Boolean
        SyncLock _Object
            Return cFailureAction.InSertData(strSFC, strStationID, cActionResultCfg)
        End SyncLock
    End Function

    Public Function DeleteSFC(ByVal strSFC As String, ByVal strStationID As String) As Boolean
        SyncLock _Object
            Return cFailureAction.DeleteSFC(strSFC, strStationID)
        End SyncLock
    End Function
    Public Function DeleteSFC(ByVal strSFC As String) As Boolean
        SyncLock _Object
            Return cFailureAction.DeleteSFC(strSFC)
        End SyncLock
    End Function

    Public Function GetData(ByVal strSFC As String, ByRef lListError As List(Of clsActionResultCfg)) As Boolean
        SyncLock _Object
            Return cFailureAction.GetData(strSFC, lListError)
        End SyncLock
    End Function
    Public Function GetData(ByVal strSFC As String, ByVal strStationID As String, ByRef lListError As List(Of clsActionResultCfg)) As Boolean
        SyncLock _Object
            Return cFailureAction.GetData(strSFC, strStationID, lListError)
        End SyncLock
    End Function
End Class


Public Class clsFailureAction
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
                Dim strCreateDatabase As String = String.Empty
                Dim strCheckTable As String = String.Empty
                Dim strCreateTable As String = String.Empty
                strCreateDatabase = "create schema if not exists HMIDatabase"
                strCheckTable = " select * from hmidatabase.FailureAction"
                strCreateTable = "CREATE TABLE hmidatabase.FailureAction" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `StationID` VARCHAR(10) NOT NULL," & _
                                    " `DataParameter` VARCHAR(255) NOT NULL," & _
                                    " `DataMessage` VARCHAR(255) NOT NULL," & _
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
                strDelCmd = "DELETE FROM hmidatabase.FailureAction where EndTime < '" & Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteSFC(ByVal strSFC As String, ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.FailureAction where SFC = '" & strSFC & "' and StationID='" & strStationID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function DeleteSFC(ByVal strSFC As String) As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM hmidatabase.FailureAction where SFC = '" & strSFC & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strSFC As String, ByVal strStationID As String, ByVal cActionResultCfg As clsActionResultCfg) As Boolean
        SyncLock _Object
            Try
                DeleteData()
                Dim strParameter As String = ""
                Dim strMessage As String = cActionResultCfg.ErrorMessage
                strParameter = cActionResultCfg.ErrorCode.ToString & _
                              "$" & cActionResultCfg.ErrorType.ToString & _
                              "$" & cActionResultCfg.Location.ToString & _
                              "$" & cActionResultCfg.MainErrorType.ToString & _
                              "$" & cActionResultCfg.RepeatNum.ToString & _
                              "$" & cActionResultCfg.Result.ToString & _
                              "$" & cActionResultCfg.ReWorkNum.ToString & _
                              "$" & cActionResultCfg.UpLimit.ToString & _
                              "$" & cActionResultCfg.LowLimit.ToString & _
                              "$" & cActionResultCfg.MESPosition.ToString & _
                              "$" & cActionResultCfg.Value.ToString
                If strParameter.Length >= 255 Then strParameter = strParameter.Substring(0, 254)
                If strMessage.Length >= 255 Then strParameter = strMessage.Substring(0, 254)
                strMessage = strMessage.Replace("'", "\'")
                strParameter = strParameter.Replace("'", "\'")
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.FailureAction (`SFC`,`StationID`,`DataParameter`,`DataMessage`, `EndTime`) values ('" & strSFC & "','" & strStationID & "','" & strParameter & "','" & strMessage & "', '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetData(ByVal strSFC As String, ByRef lListError As List(Of clsActionResultCfg)) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim lValue As New List(Of Object())
                Dim dDuration As Double = 0
                lListError.Clear()
                strInquiryCmd = "select * from hmidatabase.FailureAction where SFC='" + strSFC + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {3, 4}, lValue) Then
                    For i = 0 To lValue.Count - 1
                        Dim cActionResultCfg As New clsActionResultCfg
                        Dim strParameter As String = lValue(i)(0)
                        Dim strMessage As String = lValue(i)(1)
                        Dim cParameter() As String = strParameter.Split("$")
                        cActionResultCfg.ErrorCode = cParameter(0)
                        cActionResultCfg.ErrorType = cParameter(1)
                        cActionResultCfg.Location = cParameter(2)
                        cActionResultCfg.MainErrorType = cParameter(3)
                        cActionResultCfg.RepeatNum = cParameter(4)
                        cActionResultCfg.Result = IIf(cParameter(5).ToUpper = "TRUE", True, False)
                        cActionResultCfg.ReWorkNum = cParameter(6)
                        cActionResultCfg.UpLimit = cParameter(7)
                        cActionResultCfg.LowLimit = cParameter(8)
                        cActionResultCfg.MESPosition = cParameter(9)
                        cActionResultCfg.Value = cParameter(10)
                        cActionResultCfg.ErrorMessage = lValue(i)(1)
                        lListError.Add(cActionResultCfg)
                    Next
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
    Public Function GetData(ByVal strSFC As String, ByVal strStationID As String, ByRef lListError As List(Of clsActionResultCfg)) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim lValue As New List(Of Object())
                Dim dDuration As Double = 0
                lListError.Clear()
                strInquiryCmd = "select * from hmidatabase.FailureAction where SFC='" + strSFC + "' and StationID='" + strStationID + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {3, 4}, lValue) Then
                    For i = 0 To lValue.Count - 1
                        Dim cActionResultCfg As New clsActionResultCfg
                        Dim strParameter As String = lValue(i)(0)
                        Dim strMessage As String = lValue(i)(1)
                        Dim cParameter() As String = strParameter.Split("$")
                        cActionResultCfg.ErrorCode = cParameter(0)
                        cActionResultCfg.ErrorType = cParameter(1)
                        cActionResultCfg.Location = cParameter(2)
                        cActionResultCfg.MainErrorType = cParameter(3)
                        cActionResultCfg.RepeatNum = cParameter(4)
                        cActionResultCfg.Result = IIf(cParameter(5).ToUpper = "TRUE", True, False)
                        cActionResultCfg.ReWorkNum = cParameter(6)
                        cActionResultCfg.UpLimit = cParameter(7)
                        cActionResultCfg.LowLimit = cParameter(8)
                        cActionResultCfg.MESPosition = cParameter(9)
                        cActionResultCfg.Value = cParameter(10)
                        cActionResultCfg.ErrorMessage = lValue(i)(1)
                        lListError.Add(cActionResultCfg)
                    Next
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
End Class
