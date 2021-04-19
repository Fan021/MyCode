Public Class ProductionInterface
    Private cProductionData As clsProductionData

    Protected _Write_RUN As Boolean
    Protected _Status As enumProductionInterfaceStatus
    Protected _StatusDescription As String
    Protected Delegate Function dWrite(ByVal strVariant As String, ByVal strSFC As String, ByVal strCarrierId As String, ByVal strResult As String, ByVal strErrorCode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal strErrorStation As String, ByVal strTestStep As String, ByVal strErrorMessage As String, ByRef strActionResult As String) As Boolean
    Protected pWrite As New dWrite(AddressOf _Write)
    Protected pWriteCB As AsyncCallback = New AsyncCallback(AddressOf _WriteCB)
    Protected cDs_Data As DataSet
    Protected cLanguage As Language
    Public Enum enumProductionInterfaceStatus
        WindowsError = -99
        FailWhileWrite = -2
        NO_ERROR = 0
        Initialized = 1
        Disabled = 2
    End Enum
    Public ReadOnly Property Write_RUN() As Boolean
        Get
            Return _Write_RUN
        End Get
    End Property

    Public ReadOnly Property Status() As enumProductionInterfaceStatus
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            If _Status < enumProductionInterfaceStatus.NO_ERROR Then
                Return _Status.ToString & ";" & _StatusDescription
            Else
                Return _Status.ToString
            End If
        End Get
    End Property

    Public Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        Try
            cLanguage = MyLanguage
            cProductionData = New clsProductionData
            cProductionData.Init(mConfig, MyStation, _AppSettings, MyLanguage)
            _Status = enumProductionInterfaceStatus.Initialized
            Return True
        Catch ex As Exception
            _Status = enumProductionInterfaceStatus.WindowsError
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function


    Protected Overrides Sub Finalize()
        On Error Resume Next
        MyBase.Finalize()
    End Sub


    Public Function InSertData(ByVal strVariant As String, ByVal strSFC As String, ByVal strCarrierId As String, ByVal strResult As String, ByVal strErrorCode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal strErrorStation As String, ByVal strTestStep As String, ByVal strErrorMessage As String) As Boolean
        Try
            _Status = enumProductionInterfaceStatus.NO_ERROR
            _StatusDescription = ""
            _Write_RUN = True
            pWrite.BeginInvoke(strVariant, strSFC, strCarrierId, strResult, strErrorCode, strLowerLimit, strValue, strUpperLimit, strErrorStation, strTestStep, strErrorMessage, _StatusDescription, pWriteCB, Nothing)
            Return True
        Catch ex As Exception
            _Status = enumProductionInterfaceStatus.FailWhileWrite
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Private Function _Write(ByVal strVariant As String, ByVal strSFC As String, ByVal strCarrierId As String, ByVal strResult As String, ByVal strErrorCode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal strErrorStation As String, ByVal strTestStep As String, ByVal strErrorMessage As String, ByRef strActionResult As String) As Boolean
        Try
            Return cProductionData.InSertData(strVariant, strSFC, strCarrierId, strResult, strErrorCode, strLowerLimit, strValue, strUpperLimit, strErrorStation, strTestStep, strErrorMessage, Now.ToString("yyyy-MM-dd HH:mm:ss"))
        Catch ex As Exception
            _Status = enumProductionInterfaceStatus.FailWhileWrite
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Private Sub _WriteCB(ByVal Result As IAsyncResult)
        If pWrite.EndInvoke(_StatusDescription, Result) Then
            _Status = enumProductionInterfaceStatus.NO_ERROR
        Else
            _Status = enumProductionInterfaceStatus.FailWhileWrite
        End If
        _Write_RUN = False
    End Sub


End Class

Public Class clsProductionData
    Private cMySqlAdapter As New clsMySqlAdapter
    Private _Object As New Object
    Private cFileHandler As New FileHandler
    Private AppSettings As Settings
    Private _Language As Language
    Private strSaveDay As String = "360"
    Public Function Init(ByVal strFileName As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        SyncLock _Object
            Try
                AppSettings = _AppSettings
                _Language = MyLanguage
                Dim strIP As String = "127.0.0.1"
                Dim strUserName As String = "root"
                Dim strPassWord As String = "apb34eol"
                If cFileHandler.FileExist(AppSettings.ConfigFolder + strFileName) Then
                    strIP = cFileHandler.ReadIniFile(AppSettings.ConfigFolder + strFileName, "COMMON", "IP")
                    strUserName = cFileHandler.ReadIniFile(AppSettings.ConfigFolder + strFileName, "COMMON", "UserName")
                    strPassWord = cFileHandler.ReadIniFile(AppSettings.ConfigFolder + strFileName, "COMMON", "PassWord")
                    strSaveDay = cFileHandler.ReadIniFile(AppSettings.ConfigFolder + strFileName, "COMMON", "SaveDay")
                End If
                'cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                Dim strCmd As String = String.Empty
                'strCmd = "Persist Security Info=False;server=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseIP) + ";user id=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBaseName) + "; pwd=" + cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.DataBasePassword) + ""
                strCmd = "Persist Security Info=False;server=" + strIP + ";user id=" + strUserName + "; pwd=" + strPassWord + ""
                If cMySqlAdapter.Init(strCmd) Then
                    Return CreateDB()
                End If
                Return True
            Catch ex As Exception
                Throw ex
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
                strCreateDatabase = "create schema if not exists Las"
                strCheckTable = " select * from Las.productiontable"
                strCreateTable = "CREATE TABLE Las.productiontable" &
                                   "(`ID` INT NOT NULL AUTO_INCREMENT , " &
                                    " `Variant` VARCHAR(45) NOT NULL," &
                                    " `SFC` VARCHAR(45) NOT NULL," &
                                    " `CarrierId` VARCHAR(5) NOT NULL," &
                                    " `Result` VARCHAR(10) NOT NULL," &
                                    " `ErrorCode` VARCHAR(45) NOT NULL," &
                                    " `LowerLimit` VARCHAR(45) NOT NULL," &
                                    " `Value` VARCHAR(45) NOT NULL," &
                                    " `UpperLimit` VARCHAR(45) NOT NULL," &
                                    " `ErrorStation` VARCHAR(45) NOT NULL," &
                                    " `TestStep` VARCHAR(45) NOT NULL," &
                                    " `ErrorMessage` VARCHAR(255) NOT NULL," &
                                    " `Time` VARCHAR(45) NOT NULL," &
                                    " PRIMARY KEY ( `ID`  )" &
                                    " )"
                Return cMySqlAdapter.CreateDB(strCreateDatabase, strCheckTable, strCreateTable)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Private Function DeleteData() As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM Las.productiontable where StartTime < '" & Now.AddDays(-CInt(strSaveDay)).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strVariant As String, ByVal strSFC As String, ByVal strCarrierId As String, ByVal strResult As String, ByVal strErrorCode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal strErrorStation As String, ByVal strTestStep As String, ByVal strErrorMessage As String, ByVal strTime As String) As Boolean
        SyncLock _Object
            Try
                If strErrorMessage.Length >= 253 Then
                    strErrorMessage = strErrorMessage.Substring(0, 253)
                Else
                    strErrorMessage = strErrorMessage.Substring(0, strErrorMessage.Length)
                End If
                strErrorMessage = strErrorMessage.Replace("'", "")
                Dim strInserCmd As String = String.Empty
                strInserCmd = "insert into Las.productiontable (`Variant`,`SFC`,`CarrierId`,`Result`,`ErrorCode`, `LowerLimit`, `Value`, `UpperLimit`, `ErrorStation`, `TestStep`, `ErrorMessage`, `Time`) values ('" & strVariant & "','" & strSFC & "','" & strCarrierId & "', '" & strResult & "','" & strErrorCode & "', '" & strLowerLimit & "', '" & strValue & "', '" & strUpperLimit & "','" & strErrorStation & "', '" & strTestStep & "', '" & strErrorMessage & "', '" & strTime & "')"
                Return cMySqlAdapter.InSertData(strInserCmd)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Dim strDelCmd As String = String.Empty
                strDelCmd = "DELETE FROM Las.productiontable where ID = '" & strID & "'"
                Return cMySqlAdapter.DeleteData(strDelCmd)
            Catch ex As Exception
                Throw ex
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
                Throw ex
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
                        strSearchCmd = "where Variant = '" & cListSearchContion(2) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Variant = '" & cListSearchContion(2) & "'"
                    End If
                End If

                If cListSearchContion(3) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where SFC like '%" & cListSearchContion(3) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and SFC like '%" & cListSearchContion(3) & "%'"
                    End If
                End If

                If cListSearchContion(4) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where CarrierId = '" & cListSearchContion(4) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and CarrierId = '" & cListSearchContion(4) & "'"
                    End If
                End If

                If cListSearchContion(5) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where Result = '" & cListSearchContion(5) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and Result = '" & cListSearchContion(5) & "'"
                    End If
                End If

                If cListSearchContion(6) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where TestStep like '%" & cListSearchContion(6) & "%'"
                    Else
                        strSearchCmd = strSearchCmd + " and TestStep like '%" & cListSearchContion(6) & "%'"
                    End If
                End If

                If cListSearchContion(7) <> "" Then
                    If strSearchCmd = "" Then
                        strSearchCmd = "where ErrorStation = '" & cListSearchContion(7) & "'"
                    Else
                        strSearchCmd = strSearchCmd + " and ErrorStation = '" & cListSearchContion(7) & "'"
                    End If
                End If

                If strSearchCmd = "" Then
                    strInquiryCmd = "select * from Las.productiontable order by ID desc "
                Else
                    strInquiryCmd = "select * from Las.productiontable " & strSearchCmd & " order by ID desc "
                End If
                Return cMySqlAdapter.SelectToDataView(strInquiryCmd, Ds)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function
End Class
