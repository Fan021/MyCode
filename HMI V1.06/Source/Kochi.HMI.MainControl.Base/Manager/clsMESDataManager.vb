Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Public Class clsMESDataManager
    Private cMESData As New clsMESData
    Private cMachineManager As clsMachineManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Public Const Name As String = "MESDataManager"

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMESData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function
    Public Function InSertData(ByVal strSFC As String, ByVal strMaterialNumber As String, ByVal strMaterialVersion As String, ByVal strMaterialSFC As String) As Boolean
        SyncLock _Object
            Return cMESData.InSertData(strSFC, strMaterialNumber, strMaterialVersion, strMaterialSFC)
        End SyncLock
    End Function

    Public Function GetData(ByVal strSFC As String, ByRef strResult As clsMESDataCfg) As Boolean
        SyncLock _Object
            Return cMESData.GetData(strSFC, strResult)
        End SyncLock
    End Function

End Class


Public Class clsMESDataCfg
    Public MaterialNumber As String = String.Empty
    Public MaterialVersion As String = String.Empty
    Public MaterialSFC As String = String.Empty
End Class
Public Class clsMESData
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
                strCheckTable = " select * from hmidatabase.mestable"
                strCreateTable = "CREATE TABLE hmidatabase.mestable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `MaterialNumber` VARCHAR(45) NOT NULL," & _
                                    " `MaterialVersion` VARCHAR(45) NOT NULL," & _
                                    " `MaterialSFC` VARCHAR(20) NOT NULL," & _
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
                strDelCmd = "DELETE FROM hmidatabase.mestable where EndTime < '" & Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strSFC As String, ByVal strMaterialNumber As String, ByVal strMaterialVersion As String, ByVal strMaterialSFC As String) As Boolean
        SyncLock _Object
            Try
                DeleteData()
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.mestable (`SFC`,`MaterialNumber`,`MaterialVersion`,`MaterialSFC`, `EndTime`) values ('" & strSFC & "','" & strMaterialNumber & "','" & strMaterialVersion & "', '" & strMaterialSFC & "', '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetData(ByVal strSFC As String, ByRef strResult As clsMESDataCfg) As Boolean
        SyncLock _Object
            Try
                Dim strUpdateCmd As String = String.Empty
                Dim strInquiryCmd As String = String.Empty
                Dim cTagValue(3) As Object
                Dim dDuration As Double = 0
                strInquiryCmd = "select * from hmidatabase.mestable where SFC='" + strSFC + "' order by ID desc"
                If cMySqlAdapter.GetData(strInquiryCmd, New Integer() {2, 3, 4}, cTagValue) Then
                    strResult.MaterialNumber = cTagValue(0)
                    strResult.MaterialVersion = cTagValue(1)
                    strResult.MaterialSFC = cTagValue(2)
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
