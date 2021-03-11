Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base

Public Class clsScrewDataManager
    Private cScrewData As New clsScrewData
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cHMIDataView_Data As HMIDataView
    Private cDs_Data As DataSet
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager
    Public Const Name As String = "ScrewDataManager"

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
                cScrewData.Init(cSystemElement)
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
                Return cScrewData.CreateDB()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strCell As String,
                               ByVal strStation As String,
                               ByVal strVariant As String,
                               ByVal strSFC As String,
                               ByVal strPartNumber As String,
                               ByVal strPKP As String,
                               ByVal strAST11 As String,
                               ByVal strActionNumber As String,
                               ByVal strProgram As String,
                               ByVal strStatus As String,
                               ByVal strTime As String,
                               ByVal strStepNumber As String,
                               ByVal dCycleTime As Double,
                               ByVal strStep1Value As String,
                               ByVal dStep1TorqueLowValue As Double,
                               ByVal dStep1TorqueTarget As Double,
                               ByVal dStep1TorqueValue As Double,
                               ByVal dStep1TorqueUpValue As Double,
                               ByVal dStep1AngleLowValue As Double,
                               ByVal dStep1AngleTarget As Double,
                               ByVal dStep1AngleValue As Double,
                               ByVal dStep1AngleUpValue As Double,
                               ByVal strStep2Value As String,
                               ByVal dStep2TorqueLowValue As Double,
                               ByVal dStep2TorqueTarget As Double,
                               ByVal dStep2TorqueValue As Double,
                               ByVal dStep2TorqueUpValue As Double,
                               ByVal dStep2AngleLowValue As Double,
                               ByVal dStep2AngleTarget As Double,
                               ByVal dStep2AngleValue As Double,
                               ByVal dStep2AngleUpValue As Double,
                               ByVal strStep3Value As String,
                               ByVal dStep3TorqueLowValue As Double,
                               ByVal dStep3TorqueTarget As Double,
                               ByVal dStep3TorqueValue As Double,
                               ByVal dStep3TorqueUpValue As Double,
                               ByVal dStep3AngleLowValue As Double,
                               ByVal dStep3AngleTarget As Double,
                               ByVal dStep3AngleValue As Double,
                               ByVal dStep3AngleUpValue As Double,
                               ByVal strComment As String) As Boolean
        SyncLock _Object
            Try
                Return cScrewData.InSertData(strCell,
                                             strStation,
                                             strVariant,
                                             strSFC,
                                             strPartNumber,
                                             strPKP,
                                             strAST11,
                                             strActionNumber,
                                             strProgram,
                                             strStatus,
                                             strTime,
                                             strStepNumber,
                                             dCycleTime,
                                             strStep1Value,
                                             dStep1TorqueLowValue,
                                             dStep1TorqueTarget,
                                             dStep1TorqueValue,
                                             dStep1TorqueUpValue,
                                             dStep1AngleLowValue,
                                             dStep1AngleTarget,
                                             dStep1AngleValue,
                                             dStep1AngleUpValue,
                                             strStep2Value,
                                             dStep2TorqueLowValue,
                                             dStep2TorqueTarget,
                                             dStep2TorqueValue,
                                             dStep2TorqueUpValue,
                                             dStep2AngleLowValue,
                                             dStep2AngleTarget,
                                             dStep2AngleValue,
                                             dStep2AngleUpValue,
                                             strStep3Value,
                                             dStep3TorqueLowValue,
                                             dStep3TorqueTarget,
                                             dStep3TorqueValue,
                                             dStep3TorqueUpValue,
                                             dStep3AngleLowValue,
                                             dStep3AngleTarget,
                                             dStep3AngleValue,
                                             dStep3AngleUpValue,
                                             strComment)
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
                Return cScrewData.DeleteData(strID)
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
                cScrewData.SelectToDataView(cDs_Data, cListSearchContion)
                For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                    mDc.ColumnName = cLanguageManager.GetUserTextLine("ScrewDataManager", mDc.ColumnName)
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


Public Class clsScrewData
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
                strCheckTable = " select * from hmidatabase.screwtable"
                strCreateTable = "CREATE TABLE hmidatabase.screwtable" & _
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " & _
                                    " `Cell` VARCHAR(45) NOT NULL," & _
                                    " `Station` VARCHAR(45) NOT NULL," & _
                                    " `Variant` VARCHAR(45) NOT NULL," & _
                                    " `SFC` VARCHAR(45) NOT NULL," & _
                                    " `PartNumber` VARCHAR(20) NOT NULL," & _
                                    " `PKP` VARCHAR(10) NOT NULL," & _
                                    " `AST11` VARCHAR(10) NOT NULL," & _
                                    " `ActionNumber` VARCHAR(10) NOT NULL," & _
                                    " `Program` VARCHAR(5) NOT NULL," & _
                                    " `Status` VARCHAR(5) NOT NULL," & _
                                    " `Time` VARCHAR(25) NOT NULL," & _
                                    " `StepNumber` VARCHAR(5) NOT NULL," & _
                                     " `CycleTime` DOUBLE NOT NULL," & _
                                    " `Step1Value` VARCHAR(5) NOT NULL," & _
                                    " `Step1TorqueLowValue` DOUBLE NOT NULL," & _
                                    " `Step1TorqueTarget` DOUBLE NOT NULL," & _
                                    " `Step1TorqueValue` DOUBLE NOT NULL," & _
                                    " `Step1TorqueUpValue` DOUBLE NOT NULL," & _
                                    " `Step1AngleLowValue` DOUBLE NOT NULL," & _
                                    " `Step1AngleTarget` DOUBLE NOT NULL," & _
                                    " `Step1AngleValue` DOUBLE NOT NULL," & _
                                    " `Step1AngleUpValue` DOUBLE NOT NULL," & _
                                    " `Step2Value` VARCHAR(5) NOT NULL," & _
                                    " `Step2TorqueLowValue` DOUBLE NOT NULL," & _
                                    " `Step2TorqueTarget` DOUBLE NOT NULL," & _
                                    " `Step2TorqueValue` DOUBLE NOT NULL," & _
                                    " `Step2TorqueUpValue` DOUBLE NOT NULL," & _
                                    " `Step2AngleLowValue` DOUBLE NOT NULL," & _
                                    " `Step2AngleTarget` DOUBLE NOT NULL," & _
                                    " `Step2AngleValue` DOUBLE NOT NULL," & _
                                    " `Step2AngleUpValue` DOUBLE NOT NULL," & _
                                    " `Step3Value` VARCHAR(5) NOT NULL," & _
                                    " `Step3TorqueLowValue` DOUBLE NOT NULL," & _
                                    " `Step3TorqueTarget` DOUBLE NOT NULL," & _
                                    " `Step3TorqueValue` DOUBLE NOT NULL," & _
                                    " `Step3TorqueUpValue` DOUBLE NOT NULL," & _
                                    " `Step3AngleLowValue` DOUBLE NOT NULL," & _
                                    " `Step3AngleTarget` DOUBLE NOT NULL," & _
                                    " `Step3AngleValue` DOUBLE NOT NULL," & _
                                    " `Step3AngleUpValue` DOUBLE NOT NULL," & _
                                    " `Comment` VARCHAR(100) NOT NULL," & _
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
                strDelCmd = "DELETE FROM hmidatabase.screwtable where Time < '" & Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strCell As String,
                               ByVal strStation As String,
                               ByVal strVariant As String,
                               ByVal strSFC As String,
                               ByVal strPartNumber As String,
                               ByVal strPKP As String,
                               ByVal strAST11 As String,
                               ByVal strActionNumber As String,
                               ByVal strProgram As String,
                               ByVal strStatus As String,
                               ByVal strTime As String,
                               ByVal strStepNumber As String,
                               ByVal dCycleTime As Double,
                               ByVal strStep1Value As String,
                               ByVal dStep1TorqueLowValue As Double,
                               ByVal dStep1TorqueTarget As Double,
                               ByVal dStep1TorqueValue As Double,
                               ByVal dStep1TorqueUpValue As Double,
                               ByVal dStep1AngleLowValue As Double,
                               ByVal dStep1AngleTarget As Double,
                               ByVal dStep1AngleValue As Double,
                               ByVal dStep1AngleUpValue As Double,
                               ByVal strStep2Value As String,
                               ByVal dStep2TorqueLowValue As Double,
                               ByVal dStep2TorqueTarget As Double,
                               ByVal dStep2TorqueValue As Double,
                               ByVal dStep2TorqueUpValue As Double,
                               ByVal dStep2AngleLowValue As Double,
                               ByVal dStep2AngleTarget As Double,
                               ByVal dStep2AngleValue As Double,
                               ByVal dStep2AngleUpValue As Double,
                               ByVal strStep3Value As String,
                               ByVal dStep3TorqueLowValue As Double,
                               ByVal dStep3TorqueTarget As Double,
                               ByVal dStep3TorqueValue As Double,
                               ByVal dStep3TorqueUpValue As Double,
                               ByVal dStep3AngleLowValue As Double,
                               ByVal dStep3AngleTarget As Double,
                               ByVal dStep3AngleValue As Double,
                               ByVal dStep3AngleUpValue As Double,
                               ByVal strComment As String
                              ) As Boolean
        SyncLock _Object
            Try
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into hmidatabase.screwtable (`Cell`," & _
                                                                  "`Station`," & _
                                                                  "`Variant`," & _
                                                                  "`SFC`," & _
                                                                  "`PartNumber`," & _
                                                                  "`PKP`," & _
                                                                  "`AST11`," & _
                                                                  "`ActionNumber`," & _
                                                                  "`Program`, " & _
                                                                  "`Status`, " & _
                                                                  "`Time`, " & _
                                                                  "`StepNumber`, " & _
                                                                   "`CycleTime`, " & _
                                                                  "`Step1Value`, " & _
                                                                  "`Step1TorqueLowValue`, " & _
                                                                  "`Step1TorqueTarget`, " & _
                                                                  "`Step1TorqueValue`, " & _
                                                                  "`Step1TorqueUpValue`, " & _
                                                                  "`Step1AngleLowValue`, " & _
                                                                  "`Step1AngleTarget`, " & _
                                                                  "`Step1AngleValue`, " & _
                                                                  "`Step1AngleUpValue`, " & _
                                                                  "`Step2Value`, " & _
                                                                  "`Step2TorqueLowValue`, " & _
                                                                  "`Step2TorqueTarget`, " & _
                                                                  "`Step2TorqueValue`, " & _
                                                                  "`Step2TorqueUpValue`, " & _
                                                                  "`Step2AngleLowValue`, " & _
                                                                  "`Step2AngleTarget`, " & _
                                                                  "`Step2AngleValue`, " & _
                                                                  "`Step2AngleUpValue`, " & _
                                                                  "`Step3Value`, " & _
                                                                  "`Step3TorqueLowValue`, " & _
                                                                  "`Step3TorqueTarget`, " & _
                                                                  "`Step3TorqueValue`, " & _
                                                                  "`Step3TorqueUpValue`, " & _
                                                                  "`Step3AngleLowValue`, " & _
                                                                  "`Step3AngleTarget`, " & _
                                                                  "`Step3AngleValue`, " & _
                                                                  "`Step3AngleUpValue`, " & _
                                                                  "`Comment`) " & _
                                                                  "values ('" & strCell & _
                                                                  "','" & strStation & _
                                                                  "','" & strVariant & _
                                                                  "','" & strSFC & _
                                                                  "','" & strPartNumber & _
                                                                  "','" & strPKP & _
                                                                  "','" & strAST11 & _
                                                                  "','" & strActionNumber & _
                                                                  "','" & strProgram & _
                                                                  "','" & strStatus & _
                                                                  "','" & strTime & _
                                                                  "','" & strStepNumber & _
                                                                  "', " & dCycleTime & _
                                                                  "," & strStep1Value & _
                                                                  "," & dStep1TorqueLowValue & _
                                                                  "," & dStep1TorqueTarget & _
                                                                  "," & dStep1TorqueValue & _
                                                                  "," & dStep1TorqueUpValue & _
                                                                  "," & dStep1AngleLowValue & _
                                                                  "," & dStep1AngleTarget & _
                                                                  "," & dStep1AngleValue & _
                                                                  "," & dStep1AngleUpValue & _
                                                                  "," & strStep2Value & _
                                                                  "," & dStep2TorqueLowValue & _
                                                                  "," & dStep2TorqueTarget & _
                                                                  "," & dStep2TorqueValue & _
                                                                  "," & dStep2TorqueUpValue & _
                                                                  "," & dStep2AngleLowValue & _
                                                                  "," & dStep2AngleTarget & _
                                                                  "," & dStep2AngleValue & _
                                                                  "," & dStep2AngleUpValue & _
                                                                  "," & strStep3Value & _
                                                                  "," & dStep3TorqueLowValue & _
                                                                  "," & dStep3TorqueTarget & _
                                                                  "," & dStep3TorqueValue & _
                                                                  "," & dStep3TorqueUpValue & _
                                                                  "," & dStep3AngleLowValue & _
                                                                  "," & dStep3AngleTarget & _
                                                                  "," & dStep3AngleValue & _
                                                                  "," & dStep3AngleUpValue & _
                                                                  ",'" & strComment & "')"

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
                strDelCmd = "DELETE FROM hmidatabase.screwtable where ID = '" & strID & "'"
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
                        strSearchCmd = "where Status = '" & cListSearchContion(4) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Status = '" & cListSearchContion(4) & "'"
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
                        strSearchCmd = "where PartNumber Like '%" & cListSearchContion(6) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and PartNumber Like '%" & cListSearchContion(6) & "%'"
                    End If
                End If

                If cListSearchContion(7) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where ActionNumber = '" & cListSearchContion(7) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and ActionNumber = '" & cListSearchContion(7) & "'"
                    End If
                End If

                If cListSearchContion(8) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where PKP = '" & cListSearchContion(8) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and PKP = '" & cListSearchContion(8) & "'"
                    End If
                End If

                If cListSearchContion(9) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where AST11 = '" & cListSearchContion(9) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and AST11 = '" & cListSearchContion(9) & "'"
                    End If
                End If

                If cListSearchContion(10) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Program = '" & cListSearchContion(10) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Program = '" & cListSearchContion(10) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select ID," & _
                                    "Cell," & _
                                    "Station," & _
                                    "Variant," & _
                                    "SFC," & _
                                    "PartNumber," & _
                                    "PKP," & _
                                    "AST11," & _
                                    "ActionNumber," & _
                                    "Program," & _
                                    "Status," & _
                                    "Time," & _
                                    "StepNumber," & _
                                    "convert(CycleTime,decimal(18,2)) as CycleTime," & _
                                    "Step1Value," & _
                                    "convert(Step1TorqueLowValue,decimal(18,2)) as Step1TorqueLowValue," & _
                                    "convert(Step1TorqueTarget,decimal(18,2)) as Step1TorqueTarget," & _
                                    "convert(Step1TorqueValue,decimal(18,2)) as Step1TorqueValue," & _
                                    "convert(Step1TorqueUpValue,decimal(18,2)) as Step1TorqueUpValue," & _
                                    "convert(Step1AngleLowValue,decimal(18,0)) as Step1AngleLowValue," & _
                                    "convert(Step1AngleTarget,decimal(18,0)) as Step1AngleTarget," & _
                                    "convert(Step1AngleValue,decimal(18,0)) as Step1AngleValue," & _
                                    "convert(Step1AngleUpValue,decimal(18,0)) as Step1AngleUpValue," & _
                                    "Step2Value," & _
                                    "convert(Step2TorqueLowValue,decimal(18,2)) as Step2TorqueLowValue," & _
                                    "convert(Step2TorqueTarget,decimal(18,2)) as Step2TorqueTarget," & _
                                    "convert(Step2TorqueValue,decimal(18,2)) as Step2TorqueValue," & _
                                    "convert(Step2TorqueUpValue,decimal(18,2)) as Step2TorqueUpValue," & _
                                    "convert(Step2AngleLowValue,decimal(18,0)) as Step2AngleLowValue," & _
                                    "convert(Step2AngleTarget,decimal(18,0)) as Step2AngleTarget," & _
                                    "convert(Step2AngleValue,decimal(18,0)) as Step2AngleValue," & _
                                    "convert(Step2AngleUpValue,decimal(18,0)) as Step2AngleUpValue," & _
                                    "Step3Value," & _
                                    "convert(Step3TorqueLowValue,decimal(18,2)) as Step3TorqueLowValue," & _
                                    "convert(Step3TorqueTarget,decimal(18,2)) as Step3TorqueTarget," & _
                                    "convert(Step3TorqueValue,decimal(18,2)) as Step3TorqueValue," & _
                                    "convert(Step3TorqueUpValue,decimal(18,2)) as Step3TorqueUpValue," & _
                                    "convert(Step3AngleLowValue,decimal(18,0)) as Step3AngleLowValue," & _
                                    "convert(Step3AngleTarget,decimal(18,0)) as Step3AngleTarget," & _
                                    "convert(Step3AngleValue,decimal(18,0)) as Step3AngleValue," & _
                                    "convert(Step3AngleUpValue,decimal(18,0)) as Step3AngleUpValue," & _
                                    "Comment " & _
                                    "from hmidatabase.screwtable order by ID desc "
                Else
                    strInquiryCmd = "select ID," & _
                                    "Cell," & _
                                    "Station," & _
                                    "Variant," & _
                                    "SFC," & _
                                    "PartNumber," & _
                                    "PKP," & _
                                    "AST11," & _
                                    "ActionNumber," & _
                                    "Program," & _
                                    "Status," & _
                                    "Time," & _
                                    "StepNumber," & _
                                    "convert(CycleTime,decimal(18,2)) as CycleTime," & _
                                    "Step1Value," & _
                                    "convert(Step1TorqueLowValue,decimal(18,2)) as Step1TorqueLowValue," & _
                                    "convert(Step1TorqueTarget,decimal(18,2)) as Step1TorqueTarget," & _
                                    "convert(Step1TorqueValue,decimal(18,2)) as Step1TorqueValue," & _
                                    "convert(Step1TorqueUpValue,decimal(18,2)) as Step1TorqueUpValue," & _
                                    "convert(Step1AngleLowValue,decimal(18,0)) as Step1AngleLowValue," & _
                                    "convert(Step1AngleTarget,decimal(18,0)) as Step1AngleTarget," & _
                                    "convert(Step1AngleValue,decimal(18,0)) as Step1AngleValue," & _
                                    "convert(Step1AngleUpValue,decimal(18,0)) as Step1AngleUpValue," & _
                                    "Step2Value," & _
                                    "convert(Step2TorqueLowValue,decimal(18,2)) as Step2TorqueLowValue," & _
                                    "convert(Step2TorqueTarget,decimal(18,2)) as Step2TorqueTarget," & _
                                    "convert(Step2TorqueValue,decimal(18,2)) as Step2TorqueValue," & _
                                    "convert(Step2TorqueUpValue,decimal(18,2)) as Step2TorqueUpValue," & _
                                    "convert(Step2AngleLowValue,decimal(18,0)) as Step2AngleLowValue," & _
                                    "convert(Step2AngleTarget,decimal(18,0)) as Step2AngleTarget," & _
                                    "convert(Step2AngleValue,decimal(18,0)) as Step2AngleValue," & _
                                    "convert(Step2AngleUpValue,decimal(18,0)) as Step2AngleUpValue," & _
                                    "Step3Value," & _
                                    "convert(Step3TorqueLowValue,decimal(18,2)) as Step3TorqueLowValue," & _
                                    "convert(Step3TorqueTarget,decimal(18,2)) as Step3TorqueTarget," & _
                                    "convert(Step3TorqueValue,decimal(18,2)) as Step3TorqueValue," & _
                                    "convert(Step3TorqueUpValue,decimal(18,2)) as Step3TorqueUpValue," & _
                                    "convert(Step3AngleLowValue,decimal(18,0)) as Step3AngleLowValue," & _
                                    "convert(Step3AngleTarget,decimal(18,0)) as Step3AngleTarget," & _
                                    "convert(Step3AngleValue,decimal(18,0)) as Step3AngleValue," & _
                                    "convert(Step3AngleUpValue,decimal(18,0)) as Step3AngleUpValue," & _
                                    "Comment " & _
                                    "from hmidatabase.screwtable " & strSearchCmd & " order by ID desc "
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
