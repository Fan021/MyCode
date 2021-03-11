Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base

Public Class clsVisionDataManager
    Private cVisionData As New clsVisionData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDs_Data As DataSet
    Private cDs_Analysis As DataSet
    Private cLanguageManager As clsLanguageManager
    Public Const Name As String = "VisionDataManager"
    Private _Object As New Object

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
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cVisionData.Init(cSystemElement)
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
                Return cVisionData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strStation As String, ByVal strVariant As String, ByVal strSFC As String, ByVal strDevice As String, ByVal strProgram As String, ByVal strFilePath As String, ByVal strResult As String, ByVal strTime As String) As Boolean

        SyncLock _Object
            Try
                Return cVisionData.InSertData(strStation, strVariant, strSFC, strDevice, strProgram, strFilePath, strResult, strTime)
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
                Return cVisionData.DeleteData(strID)
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
                cVisionData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetUserTextLine("VisionDataManager", mDc.ColumnName)
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


Public Class clsVisionData
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
                strCheckTable = " select * from hmidatabase.visiontable"
                strCreateTable = "CREATE TABLE hmidatabase.visiontable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Station` VARCHAR(45) NOT NULL," & _
                                    " `Variant` VARCHAR(20) NOT NULL," & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `Device` VARCHAR(20) NOT NULL," & _
                                    " `Program` VARCHAR(20) NOT NULL," & _
                                    " `FilePath` VARCHAR(100) NOT NULL," & _
                                    " `Result` VARCHAR(10) NOT NULL," & _
                                    " `Time` VARCHAR(25) NOT NULL," & _
                                    " PRIMARY KEY ( `ID`  )" & _
                                    " ) ENGINE=MyISAM"
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
                strDelCmd = "DELETE FROM hmidatabase.visiontable where Time < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strStation As String, ByVal strVariant As String, ByVal strSFC As String, ByVal strDevice As String, ByVal strProgram As String, ByVal strFilePath As String, ByVal strResult As String, ByVal strTime As String) As Boolean
        SyncLock _Object
            Try
                Dim strInserCmd As String = String.Empty
                strFilePath = strFilePath.Replace("\", "\\")
                strInserCmd = "insert into hmidatabase.visiontable (`Station`,`Variant`,`SFC`, `Device`,`Program`, `FilePath`,`Result`, `Time`) values ('" & strStation & "','" & strVariant & "', '" & strSFC & "','" & strDevice & "','" & strProgram & "', '" & strFilePath & "', '" & strResult & "', '" & strTime & "')"
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
                strDelCmd = "DELETE FROM hmidatabase.visiontable where ID = '" & strID & "'"
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
                        strSearchCmd = "where Device = '" & cListSearchContion(6) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Device = '" & cListSearchContion(6) & "'"
                    End If
                End If

                If cListSearchContion(7) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Program = '" & cListSearchContion(7) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Program = '" & cListSearchContion(7) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select * from hmidatabase.visiontable order by ID desc "
                Else
                    strInquiryCmd = "select * from hmidatabase.visiontable " & strSearchCmd & " order by ID desc "
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


