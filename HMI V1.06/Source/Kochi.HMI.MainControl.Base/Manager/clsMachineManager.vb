Imports System.Reflection
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.Action

Public Class clsMachineManager
    Protected cMachineCellManager As clsMachineCellManager
    Protected cMachineGlobalParameter As clsMachineGlobalParameter
    Protected cMachineVariantParameter As clsMachineVariantParameter
    Protected cVaiantElememtManager As clsVaiantElememtManager
    Protected cActionParameterManager As clsActionParameterManager
    Protected cDeviceParameterManager As clsDeviceParameterManager
    Protected cMachineStatusParameterManager As clsMachineStatusParameterManager
    Private cLanguageManager As clsLanguageManager
    Private cStructMachineStatus As New StructMachineStatus
    Private cIOManager As New clsIOManager
    Private cCylinderManager As New clsCylinderManager
    Private cStationErrorCodeManager As clsStationErrorCodeManager
    Public Const Name As String = "MachineManager"
    Private _Object As New Object
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)

    Public ReadOnly Property MachineCellManager As clsMachineCellManager
        Get
            SyncLock _Object
                Return cMachineCellManager
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property VaiantElememtManager As clsVaiantElememtManager
        Get
            SyncLock _Object
                Return cVaiantElememtManager
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property MachineStatusParameterManager As clsMachineStatusParameterManager
        Get
            SyncLock _Object
                Return cMachineStatusParameterManager
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property DeviceParameterManager As clsDeviceParameterManager
        Get
            SyncLock _Object
                Return cDeviceParameterManager
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ActionParameterManager As clsActionParameterManager
        Get
            SyncLock _Object
                Return cActionParameterManager
            End SyncLock
        End Get
    End Property

    Public Property MachineStatus As StructMachineStatus
        Set(ByVal value As StructMachineStatus)
            SyncLock _Object
                cStructMachineStatus = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cStructMachineStatus
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property MachineVariantParameter As clsMachineVariantParameter
        Get
            SyncLock _Object
                Return cMachineVariantParameter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property MachineGlobalParameter As clsMachineGlobalParameter
        Get
            SyncLock _Object
                Return cMachineGlobalParameter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return cMachineCellManager.IsChanged Or cMachineGlobalParameter.IsChanged Or cVaiantElememtManager.IsChanged Or cActionParameterManager.IsChanged Or cDeviceParameterManager.IsChanged Or cMachineStatusParameterManager.IsChanged Or cMachineVariantParameter.IsChanged
            End SyncLock
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineCellManager = New clsMachineCellManager
                cMachineCellManager.Init(cSystemElement)
                cMachineGlobalParameter = New clsMachineGlobalParameter
                cMachineGlobalParameter.Init(cSystemElement)
                cMachineVariantParameter = New clsMachineVariantParameter
                cMachineVariantParameter.Init(cSystemElement)
                cMachineVariantParameter.GlobalProgramManager = cMachineGlobalParameter
                cVaiantElememtManager = New clsVaiantElememtManager
                cVaiantElememtManager.Init(cSystemElement)
                cActionParameterManager = New clsActionParameterManager
                cActionParameterManager.Init(cSystemElement)
                cDeviceParameterManager = New clsDeviceParameterManager
                cDeviceParameterManager.Init(cSystemElement)
                cMachineStatusParameterManager = New clsMachineStatusParameterManager
                cMachineStatusParameterManager.Init(cSystemElement)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function LoadManagerData() As Boolean
        SyncLock _Object
            Try
                cMachineCellManager.LoadMachineCellManager()
                cVaiantElememtManager.LoadData()
                cActionParameterManager.LoadData()
                cMachineStatusParameterManager.LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadDeviceData() As Boolean
        SyncLock _Object
            Try
                cMachineGlobalParameter.LoadMachineGlobalParameter()
                cMachineVariantParameter.LoadMachineGlobalParameter()
                cDeviceParameterManager.LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentCfg() As Boolean
        SyncLock _Object
            Try
                cStationErrorCodeManager = CType(cSystemElement(clsStationErrorCodeManager.Name), clsStationErrorCodeManager)
                cMachineCellManager.CellChanged = cMachineCellManager.StationChanged
                cMachineCellManager.SaveCurrentCfg()
                cMachineGlobalParameter.SaveCurrentGlobalParameter()
                cMachineVariantParameter.SaveCurrentGlobalParameter()
                cVaiantElememtManager.SaveCurrentData()
                cActionParameterManager.SaveCurrentData()
                cDeviceParameterManager.SaveCurrentData()
                cMachineStatusParameterManager.SaveCurrentData()
                cCylinderManager.RemoveData(cSystemElement)
                cIOManager.RemoveData(cSystemElement)
                cStationErrorCodeManager.LoadIni()
                Return (True)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentCfg() As Boolean
        SyncLock _Object
            Try
                cMachineCellManager.CheckCurrentCfg()
                cMachineGlobalParameter.CheckCurrentGlobalParameter()
                cMachineVariantParameter.CheckCurrentGlobalParameter()
                cVaiantElememtManager.CheckCurrentData()
                cActionParameterManager.CheckCurrentData()
                cDeviceParameterManager.CheckCurrentData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentCfg() As Boolean
        SyncLock _Object
            Try
                cMachineCellManager.CancelCurrentCfg()
                cMachineGlobalParameter.CancelCurrentGlobalParameter()
                cMachineVariantParameter.CancelCurrentGlobalParameter()
                cActionParameterManager.CancelCurrentData()
                cDeviceParameterManager.CancelCurrentData()
                cVaiantElememtManager.CancelCurrentData()
                cMachineStatusParameterManager.CancelCurrentData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class

Public Class clsMachineGlobalParameter
    Public Const Name As String = "MachineGlobalParameter"
    Protected lMachineGlobalParameter As New Dictionary(Of String, Object)
    Protected lCurrentMachineGlobalParameter As New Dictionary(Of String, Object)
    Protected cIniHandler As New clsIniHandler
    Protected cSystemManager As clsSystemManager
    Protected cHMIGlobalParameter As clsHMIGlobalParameter
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object

    Public ReadOnly Property HMIGlobalParameter As clsHMIGlobalParameter
        Get
            SyncLock _Object
                Return cHMIGlobalParameter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cHMIGlobalParameter = New clsHMIGlobalParameter(cSystemElement)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetMachineGlobalParameterListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lMachineGlobalParameter.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineGlobalParameterFromKey(ByVal strKey As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strKey) Then
                    Return lMachineGlobalParameter(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMachineGlobalParameterListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lCurrentMachineGlobalParameter.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMachineGlobalParameterFromKey(ByVal strKey As String) As Object
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strKey) Then
                    Return lCurrentMachineGlobalParameter(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.Count <> lCurrentMachineGlobalParameter.Count Then
                    Return False
                End If
                For Each element In lCurrentMachineGlobalParameter.Keys
                    If Not lMachineGlobalParameter.ContainsKey(element) Then
                        Return False
                    End If
                    If lMachineGlobalParameter(element) <> lCurrentMachineGlobalParameter(element) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentGlobalParameter(ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    Return lCurrentMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetCurrentGlobalParameter(ByVal strName As String, ByVal oSource As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    lCurrentMachineGlobalParameter(strName) = cHMIGlobalParameter.ChangeParameterValueToType(strName, oSource)
                Else
                    lCurrentMachineGlobalParameter.Add(strName, oSource)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetGlobalParameter(ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    Return lMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetGlobalParameter(ByVal strName As String, ByVal oSource As Object) As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    lMachineGlobalParameter(strName) = cHMIGlobalParameter.ChangeParameterValueToType(strName, oSource)
                Else
                    lMachineGlobalParameter.Add(strName, oSource)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function Clone(ByRef SrcList As Dictionary(Of String, Object), ByRef TarList As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                TarList.Clear()
                For Each element In SrcList.Keys
                    TarList.Add(element, SrcList(element))
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadMachineGlobalParameter() As Boolean
        SyncLock _Object
            Try
                cHMIGlobalParameter.InitData()
                lMachineGlobalParameter.Clear()
                For Each element As clsGlobalParameterCfg In cHMIGlobalParameter.GetUserDefinedKeys
                    If element.Name = clsHMIGlobalParameter.DataBaseSaveTime Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "1"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.MachineStatus Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "NONE"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.Company Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "KOCHI"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.Program_Max_Page Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "1"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))

                    ElseIf element.Name = clsHMIGlobalParameter.AutoLoginLevel Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = enumUserLevel.Operator.ToString
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.WrtieCurrentStation Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "FALSE"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))

                    ElseIf element.Name = clsHMIGlobalParameter.MachineStatusType Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "FALSE"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.Process Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "TRUE"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))
                    ElseIf element.Name = clsHMIGlobalParameter.LogPath Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "C:\HMI\Log"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))

                        cSystemManager.Settings.LogFolder = mTempValue
                    ElseIf element.Name = clsHMIGlobalParameter.AutoCreateDB Then
                        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)
                        If mTempValue = "" Then mTempValue = "FALSE"
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, mTempValue))

                    Else
                        lMachineGlobalParameter.Add(element.Name, cHMIGlobalParameter.ChangeParameterValueToType(element.Name, cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Config", element.Name)))
                    End If

                Next
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each element As String In lCurrentMachineGlobalParameter.Keys
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "Config", element, lCurrentMachineGlobalParameter(element).ToString)
                Next
                Clone(lCurrentMachineGlobalParameter, lMachineGlobalParameter)
                cSystemManager.Settings.LogFolder = lMachineGlobalParameter(clsHMIGlobalParameter.LogPath).ToString
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each element As String In lCurrentMachineGlobalParameter.Keys
                    If lCurrentMachineGlobalParameter(element).ToString = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "1", element), enumExceptionType.Alarm)
                    End If
                Next
                If lCurrentMachineGlobalParameter(clsHMIGlobalParameter.FirstLanguage).ToString = lCurrentMachineGlobalParameter(clsHMIGlobalParameter.SecondLanguage).ToString Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "3", lCurrentMachineGlobalParameter(clsHMIGlobalParameter.FirstLanguage).ToString), enumExceptionType.Alarm)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class

Public Class clsHMIGlobalParameter
    Protected _Variantkeylist As New List(Of clsGlobalParameterCfg)
    Public Shared IO_Max_Page As String = "IO Max Page"
    Public Shared Cylinder_Max_Page As String = "Cylinder Max Page"
    Public Shared Program_Max_Page As String = "Program Max Page"
    Public Shared MES As String = "MES Enable"
    Public Shared Process As String = "Process Enable"
    Public Shared Company As String = "Company"
    Public Shared MachineStatus As String = "MachineStatus"
    Public Shared MachineStatusType As String = "Send MachineStatus By Station"
    Public Shared Manual_Screw_Repeat As String = "Manual Screw Repeat"
    Public Shared Manual_Screw_ToleranceX As String = "Manual Screw Tolerance X(mm)"
    Public Shared Manual_Screw_ToleranceY As String = "Manual Screw Tolerance Y(mm)"
    Public Shared Manual_Screw_ToleranceZ As String = "Manual Screw Tolerance Z(mm)"
    Public Shared LogPath As String = "LogPath"
    Public Shared DataBaseIP As String = "DataBaseIP"
    Public Shared AutoCreateDB As String = "AutoCreateDB"
    Public Shared DataBaseName As String = "DataBaseName"
    Public Shared DataBasePassword As String = "DataBasePassword"
    Public Shared DataBaseSaveTime As String = "DataBaseSaveTime"
    Public Shared AutoLogin As String = "Auto Login"
    Public Shared AutoLoginLevel As String = "Auto Login Level"
    Public Shared TouchKeyBoard As String = "TouchKeyBoard"
    Public Shared FirstLanguage As String = "First Language"
    Public Shared SecondLanguage As String = "Second Language"
    Public Shared Language As String = "Language"
    Public Shared WrtieCurrentStation As String = "WrtieCurrentStation"
    Public Shared CleanSFC As String = "CleanSFC"
    Public Shared DebugAutoOn1 As String = "Debug Auto On1"
    Public Shared DebugAutoOn2 As String = "Debug Auto On2"
    Public Shared DebugAutoOn3 As String = "Debug Auto On3"
    Public Shared DebugAutoOn4 As String = "Debug Auto On4"
    Public Shared DebugAutoOn5 As String = "Debug Auto On5"
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorCodeManager As clsErrorCodeManager
    Private cDeviceManager As clsDeviceManager

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
    End Sub

    Public Function InitData() As Boolean
        SyncLock _Object
            _Variantkeylist.Clear()
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cErrorCodeManager = CType(cSystemElement(clsErrorCodeManager.Name), clsErrorCodeManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            Dim lValueList As New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(MES, GetType(Boolean), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(Process, GetType(Boolean), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("KOCHI")
            lValueList.Add("KOI")
            _Variantkeylist.Add(New clsGlobalParameterCfg(Company, GetType(String), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("NONE")
            Dim lListDeviceCfg As List(Of clsDeviceCfg)
            lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(0, GetType(clsHMIMESBase))
            If Not IsNothing(lListDeviceCfg) Then
                For Each element As clsDeviceCfg In lListDeviceCfg
                    lValueList.Add(element.StationIndex.ToString)
                Next
            End If
            _Variantkeylist.Add(New clsGlobalParameterCfg(MachineStatus, GetType(String), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(MachineStatusType, GetType(Boolean), lValueList))


            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_Repeat, GetType(Integer)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(IO_Max_Page, GetType(Integer)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Cylinder_Max_Page, GetType(Integer)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Program_Max_Page, GetType(Integer)))
            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(AutoLogin, GetType(Boolean), lValueList))

            lValueList = New List(Of Object)
            For Each eType As enumUserLevel In [Enum].GetValues(GetType(enumUserLevel))
                If eType = enumUserLevel.Normal Then Continue For
                lValueList.Add(eType.ToString)
            Next
            _Variantkeylist.Add(New clsGlobalParameterCfg(AutoLoginLevel, GetType(String), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(TouchKeyBoard, GetType(Boolean), lValueList))
            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(WrtieCurrentStation, GetType(Boolean), lValueList))

            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceX, GetType(Double)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceY, GetType(Double)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceZ, GetType(Double)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(LogPath, GetType(String)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DataBaseIP, GetType(String)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DataBaseName, GetType(String)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DataBasePassword, GetType(String)))

            _Variantkeylist.Add(New clsGlobalParameterCfg(CleanSFC, GetType(String)))


            lValueList = New List(Of Object)
            lValueList.Add("TRUE")
            lValueList.Add("FALSE")
            _Variantkeylist.Add(New clsGlobalParameterCfg(AutoCreateDB, GetType(Boolean), lValueList))


            lValueList = New List(Of Object)
            lValueList.Add("1")
            lValueList.Add("2")
            lValueList.Add("3")
            lValueList.Add("4")
            lValueList.Add("5")
            _Variantkeylist.Add(New clsGlobalParameterCfg(DataBaseSaveTime, GetType(Integer), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("NONE")
            For Each elememt In cLanguageManager.GetTextLanguageKey
                lValueList.Add(elememt)
            Next
            _Variantkeylist.Add(New clsGlobalParameterCfg(FirstLanguage, GetType(String), lValueList))

            lValueList = New List(Of Object)
            lValueList.Add("NONE")
            For Each elememt In cLanguageManager.GetTextLanguageKey
                lValueList.Add(elememt)
            Next
            _Variantkeylist.Add(New clsGlobalParameterCfg(SecondLanguage, GetType(String), lValueList))

            lValueList = New List(Of Object)
            For Each elememt In cLanguageManager.GetTextLanguageKey
                lValueList.Add(elememt)
            Next
            _Variantkeylist.Add(New clsGlobalParameterCfg(Language, GetType(String), lValueList))



            lValueList = New List(Of Object)
            lValueList.Add("NONE")
            For Each elememt In cLanguageManager.GetTextLanguageKey

            Next
            For i = 1 To 15
                For j = 0 To 7
                    lValueList.Add("EL2008-" + i.ToString + "." + j.ToString)
                Next
            Next
            _Variantkeylist.Add(New clsGlobalParameterCfg(DebugAutoOn1, GetType(String), lValueList))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DebugAutoOn2, GetType(String), lValueList))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DebugAutoOn3, GetType(String), lValueList))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DebugAutoOn4, GetType(String), lValueList))
            _Variantkeylist.Add(New clsGlobalParameterCfg(DebugAutoOn5, GetType(String), lValueList))

            Return True
        End SyncLock
    End Function
    Public Function GetParameterKeysFromName(ByVal strName As String) As clsGlobalParameterCfg
        SyncLock _Object
            If _Variantkeylist.Any(Function(e) e.Name = strName) Then
                Return _Variantkeylist.Where(Function(e) e.Name = strName).First
            End If
            Return Nothing
        End SyncLock
    End Function
    Public Function ChangeParameterValueToType(ByVal strName As String, ByVal oSource As String) As Object
        SyncLock _Object
            Try
                Select Case GetParameterKeysFromName(strName).ValueType
                    Case GetType(Boolean)
                        Return IIf(oSource.ToUpper = "TRUE", True, False)
                    Case GetType(Integer)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "2", oSource), enumExceptionType.Alarm)
                        End If
                        Return CInt(oSource)
                    Case GetType(Double)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "2", oSource), enumExceptionType.Alarm)
                        End If
                        Return CDbl(oSource)
                    Case GetType(String)
                        Return oSource.ToString
                End Select
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetUserDefinedKeys() As List(Of clsGlobalParameterCfg)
        SyncLock _Object
            Return _Variantkeylist
        End SyncLock
    End Function
End Class


Public Class clsMachineVariantParameter
    Public Const Name As String = "MachineVariantParameterCfg"
    Protected lMachineGlobalParameter As New Dictionary(Of String, clsMachineVariantParameterCfg)
     Protected cIniHandler As New clsIniHandler
    Protected cSystemManager As clsSystemManager
    Protected cHMIVariantParameter As clsHMIVariantParameter
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Private cVariantManager As clsVariantManager
    Private cSystemElement As Dictionary(Of String, Object)
    Private cMachineGlobalParameter As clsMachineGlobalParameter
    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property HMIVariantParameter As clsHMIVariantParameter
        Get
            SyncLock _Object
                Return cHMIVariantParameter
            End SyncLock
        End Get
    End Property

    Public Property GlobalProgramManager As clsMachineGlobalParameter
        Set(ByVal value As clsMachineGlobalParameter)
            cMachineGlobalParameter = value
        End Set
        Get
            SyncLock _Object
                Return cMachineGlobalParameter
            End SyncLock
        End Get
    End Property

    Public Function GetMachineGlobalParameterListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lMachineGlobalParameter.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineGlobalParameterFromKey(ByVal strKey As String) As clsMachineVariantParameterCfg
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strKey) Then
                    Return lMachineGlobalParameter(strKey)
                Else
                    Dim cMachineVariantParameterCfg As New clsMachineVariantParameterCfg
                    cMachineVariantParameterCfg.Init(cSystemElement)
                    cMachineVariantParameterCfg.LoadMachineGlobalParameter(strKey)
                    lMachineGlobalParameter.Add(strKey, cMachineVariantParameterCfg)
                    Return lMachineGlobalParameter(strKey)
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cHMIVariantParameter = New clsHMIVariantParameter(cSystemElement)
                cHMIVariantParameter.InitData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                For Each element In lMachineGlobalParameter.Keys
                    If Not lMachineGlobalParameter.ContainsKey(element) Then
                        Return False
                    End If
                    If lMachineGlobalParameter(element).IsChanged Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadMachineGlobalParameter() As Boolean
        SyncLock _Object
            Try
                cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
                lMachineGlobalParameter.Clear()
                For Each elementIndex As String In cVariantManager.GetVariantListKey
                    Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
                    Dim cMachineVariantParameterCfg As New clsMachineVariantParameterCfg
                    cMachineVariantParameterCfg.Init(cSystemElement)
                    cMachineVariantParameterCfg.LoadMachineGlobalParameter(element.Variant)
                    lMachineGlobalParameter.Add(element.Variant, cMachineVariantParameterCfg)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As String In cVariantManager.GetVariantListKey
                    Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
                    lMachineGlobalParameter(element.Variant).SaveCurrentGlobalParameter(element.Variant)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SetCurrentGlobalParameter(ByVal strVariant As String, ByVal strName As String, ByVal oSource As String) As Boolean
        SyncLock _Object
            Try
                lMachineGlobalParameter(strVariant).SetCurrentGlobalParameter(strName, oSource)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function AddCurrentGlobalParameter(ByVal strVariant As String, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                lMachineGlobalParameter(strVariant).AddCurrentGlobalParameter(strName)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DelCurrentGlobalParameter(ByVal strVariant As String, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                lMachineGlobalParameter(strVariant).DelCurrentGlobalParameter(strName)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMachineGlobalParameterFromKey(ByVal strVariant As String, ByVal strKey As String) As Object
        SyncLock _Object
            Try
                lMachineGlobalParameter(strVariant).GetCurrentGlobalParameter(strKey)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentGlobalParameter(ByVal strVariant As String, ByVal strName As String) As Object
        SyncLock _Object
            Try
                Return lMachineGlobalParameter(strVariant).GetCurrentGlobalParameter(strName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetGlobalParameter(ByVal strVariant As String, ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strVariant) Then
                    Dim oSource As Object = lMachineGlobalParameter(strVariant).GetGlobalParameter(strName)
                    If IsNothing(oSource) Then
                        Return cMachineGlobalParameter.GetGlobalParameter(strName)
                    Else
                        Return oSource
                    End If
                Else
                    Return cMachineGlobalParameter.GetGlobalParameter(strName)
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function CheckCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As String In cVariantManager.GetVariantListKey
                    Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
                    lMachineGlobalParameter(element.Variant).CheckCurrentGlobalParameter()
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As String In cVariantManager.GetVariantListKey
                    Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
                    lMachineGlobalParameter(element.Variant).CancelCurrentGlobalParameter()
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


End Class
Public Class clsMachineVariantParameterCfg
    Public Const Name As String = "MachineVariantParameterCfg"
    Protected lMachineGlobalParameter As New Dictionary(Of String, Object)
    Protected lCurrentMachineGlobalParameter As New Dictionary(Of String, Object)
    Protected cIniHandler As New clsIniHandler
    Protected cSystemManager As clsSystemManager
    Protected cHMIVariantParameter As clsHMIVariantParameter
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object

    Public ReadOnly Property HMIVariantParameter As clsHMIVariantParameter
        Get
            SyncLock _Object
                Return cHMIVariantParameter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cHMIVariantParameter = New clsHMIVariantParameter(cSystemElement)
                cHMIVariantParameter.InitData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetMachineGlobalParameterListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lMachineGlobalParameter.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineGlobalParameterFromKey(ByVal strKey As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strKey) Then
                    Return lMachineGlobalParameter(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMachineGlobalParameterListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lCurrentMachineGlobalParameter.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMachineGlobalParameterFromKey(ByVal strKey As String) As Object
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strKey) Then
                    Return lCurrentMachineGlobalParameter(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.Count <> lCurrentMachineGlobalParameter.Count Then
                    Return False
                End If
                For Each element In lCurrentMachineGlobalParameter.Keys
                    If Not lMachineGlobalParameter.ContainsKey(element) Then
                        Return False
                    End If
                    If lMachineGlobalParameter(element) <> lCurrentMachineGlobalParameter(element) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentGlobalParameter(ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    Return lCurrentMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetCurrentGlobalParameter(ByVal strName As String, ByVal oSource As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    lCurrentMachineGlobalParameter(strName) = cHMIVariantParameter.ChangeParameterValueToType(strName, oSource)
                Else
                    lCurrentMachineGlobalParameter.Add(strName, oSource)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddCurrentGlobalParameter(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    lCurrentMachineGlobalParameter(strName) = Nothing
                Else
                    lCurrentMachineGlobalParameter.Add(strName, Nothing)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function DelCurrentGlobalParameter(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    lCurrentMachineGlobalParameter.Remove(strName)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetGlobalParameter(ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    Return lMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetGlobalParameter(ByVal strName As String, ByVal oSource As Object) As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    lMachineGlobalParameter(strName) = cHMIVariantParameter.ChangeParameterValueToType(strName, oSource)
                Else
                    lMachineGlobalParameter.Add(strName, oSource)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function



    Public Function Clone(ByRef SrcList As Dictionary(Of String, Object), ByRef TarList As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                TarList.Clear()
                For Each element In SrcList.Keys
                    TarList.Add(element, SrcList(element))
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadMachineGlobalParameter(ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.ConfigFile, "Config_" + strVariant, New String() {"ID", "KeyName", "KeyValue"})
                    If CType(element("KeyName"), String) = "" Then
                        Continue For
                    End If
                    lMachineGlobalParameter.Add(CType(element("KeyName"), String), cHMIVariantParameter.ChangeParameterValueToType(CType(element("KeyName"), String), CType(element("KeyValue"), String)))
                Next
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentGlobalParameter(ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                For Each element As String In lCurrentMachineGlobalParameter.Keys
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "Config_" + strVariant + iCnt.ToString, "KeyName", element)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "Config_" + strVariant + iCnt.ToString, "KeyValue", lCurrentMachineGlobalParameter(element).ToString)
                    iCnt = iCnt + 1
                Next
                Clone(lCurrentMachineGlobalParameter, lMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function CheckCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each element As String In lCurrentMachineGlobalParameter.Keys
                    If lCurrentMachineGlobalParameter(element).ToString = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "1", element), enumExceptionType.Alarm)
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class


Public Class clsHMIVariantParameter
    Protected _Variantkeylist As New List(Of clsGlobalParameterCfg)
    Public Shared Manual_Screw_Repeat As String = "Manual Screw Repeat"
    Public Shared Manual_Screw_ToleranceX As String = "Manual Screw Tolerance X(mm)"
    Public Shared Manual_Screw_ToleranceY As String = "Manual Screw Tolerance Y(mm)"
    Public Shared Manual_Screw_ToleranceZ As String = "Manual Screw Tolerance Z(mm)"
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorCodeManager As clsErrorCodeManager
    Private cDeviceManager As clsDeviceManager

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
    End Sub

    Public Function InitData() As Boolean
        SyncLock _Object
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            Dim lValueList As New List(Of Object)

            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_Repeat, GetType(Integer)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceX, GetType(Double)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceY, GetType(Double)))
            _Variantkeylist.Add(New clsGlobalParameterCfg(Manual_Screw_ToleranceZ, GetType(Double)))
         
            Return True
        End SyncLock
    End Function
    Public Function GetParameterKeysFromName(ByVal strName As String) As clsGlobalParameterCfg
        SyncLock _Object
            If _Variantkeylist.Any(Function(e) e.Name = strName) Then
                Return _Variantkeylist.Where(Function(e) e.Name = strName).First
            End If
            Return Nothing
        End SyncLock
    End Function
    Public Function ChangeParameterValueToType(ByVal strName As String, ByVal oSource As String) As Object
        SyncLock _Object
            Try
                Select Case GetParameterKeysFromName(strName).ValueType
                    Case GetType(Boolean)
                        Return IIf(oSource.ToUpper = "TRUE", True, False)
                    Case GetType(Integer)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "2", oSource), enumExceptionType.Alarm)
                        End If
                        Return CInt(oSource)
                    Case GetType(Double)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "2", oSource), enumExceptionType.Alarm)
                        End If
                        Return CDbl(oSource)
                    Case GetType(String)
                        Return oSource.ToString
                End Select
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetUserDefinedKeys() As List(Of clsGlobalParameterCfg)
        SyncLock _Object
            Return _Variantkeylist
        End SyncLock
    End Function
End Class


Public Class clsGlobalParameterCfg
    Protected strName As String = String.Empty
    Protected cValueType As Type = Nothing
    Protected lValueList As New List(Of Object)
    Private _Object As New Object

    Public Property ValueList As List(Of Object)
        Set(ByVal value As List(Of Object))
            SyncLock _Object
                lValueList = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return lValueList
            End SyncLock
        End Get

    End Property
    Public Property Name As String
        Set(ByVal value As String)
            SyncLock _Object
                strName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strName
            End SyncLock
        End Get
    End Property

    Public Property ValueType As Type
        Set(ByVal value As Type)
            SyncLock _Object
                cValueType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cValueType
            End SyncLock
        End Get
    End Property

    Sub New(ByVal strName As String, ByVal cValueType As Type)
        SyncLock _Object
            Me.strName = strName
            Me.cValueType = cValueType
        End SyncLock
    End Sub

    Sub New(ByVal strName As String, ByVal cValueType As Type, ByVal lValueList As List(Of Object))
        SyncLock _Object
            Me.strName = strName
            Me.cValueType = cValueType
            Me.lValueList = lValueList
        End SyncLock
    End Sub
End Class

Public Class clsVaiantElememtManager
    Protected lListElement As New List(Of String)
    Protected lCurrentListElement As New List(Of String)
    Protected cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object

    Public ReadOnly Property ListElement As List(Of String)
        Get
            Return lListElement
        End Get
    End Property


    Public ReadOnly Property CurrentListElement As List(Of String)
        Get
            Return lCurrentListElement
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                lListElement.Clear()
                lCurrentListElement.Clear()
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.ConfigFile, "VariantElement", New String() {"KeyName"})
                    Dim mTempValue As String = String.Empty
                    If CType(element("KeyName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("KeyName"), String) <> clsXmlHandler.s_Null Then
                        mTempValue = CType(element("KeyName"), String)
                    End If
                    lListElement.Add(mTempValue)
                    lCurrentListElement.Add(mTempValue)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lListElement.Count <> lCurrentListElement.Count Then
                    Return False
                End If
                For i = 0 To lListElement.Count - 1
                    If lListElement(i) <> lCurrentListElement(i) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function



    Public Function AddCurrentData() As String
        lCurrentListElement.Add("")
        Return ""
    End Function

    Public Function DeleteCurrentData(ByVal iID As Integer) As Boolean
        lCurrentListElement.RemoveAt(iID)
        Return True
    End Function

    Public Function HasCurrentData(ByVal iID As Integer, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                For i = 0 To lCurrentListElement.Count - 1
                    If lCurrentListElement(i) = strName And i <> iID Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DownRowCurrentData(ByVal iID As Integer) As Boolean
        SyncLock _Object
            Try
                Dim mTempValue As String = ""
                mTempValue = lCurrentListElement(iID + 1)
                lCurrentListElement(iID + 1) = lCurrentListElement(iID)
                lCurrentListElement(iID) = mTempValue
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpRowCurrentData(ByVal iID As Integer) As Boolean
        SyncLock _Object
            Try
                Dim mTempValue As String = ""
                mTempValue = lCurrentListElement(iID - 1)
                lCurrentListElement(iID - 1) = lCurrentListElement(iID)
                lCurrentListElement(iID) = mTempValue
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentData(ByVal iID As Integer, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                lCurrentListElement(iID) = strName
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentData() As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                For Each element As String In lCurrentListElement
                    If element = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("VariantElement", "1", iCnt.ToString), enumExceptionType.Alarm)
                        Return False
                    End If
                    iCnt = iCnt + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentData() As Boolean
        SyncLock _Object
            Try
                Return LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentData() As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(cSystemManager.Settings.ConfigFile, "VariantElement")
                For i = 1 To lCurrentListElement.Count
                    cIniHandler.SetAnyListToIni(cSystemManager.Settings.ConfigFile, "VariantElement" + i.ToString, New String() {"KeyName"},
                                      New String() {lCurrentListElement(i - 1)})
                Next
                LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Class clsActionParameterManager
    Protected lListElement As New Dictionary(Of String, String)
    Protected lCurrentListElement As New Dictionary(Of String, String)
    Protected cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private cActionLibManager As clsActionLibManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Public ReadOnly Property ListElement As Dictionary(Of String, String)
        Get
            Return lListElement
        End Get
    End Property


    Public ReadOnly Property CurrentListElement As Dictionary(Of String, String)
        Get
            Return lCurrentListElement
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                lListElement.Clear()
                lCurrentListElement.Clear()
                For Each element As String In cActionLibManager.GetListDllKey
                    Dim cActionLibCfg As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(element)
                    Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "ActionParameter", cActionLibCfg.ActionName)
                    lListElement.Add(cActionLibCfg.ActionName, mTempValue)
                    lCurrentListElement.Add(cActionLibCfg.ActionName, mTempValue)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetActionParameterDevice(ByVal strActionName As String, ByVal iStationID As Integer, ByVal iParameterIndex As Integer) As String
        Dim strDevice As String = String.Empty
        If lListElement.ContainsKey(strActionName) Then
            Dim lList As List(Of String) = clsParameter.ToList(lListElement(strActionName))
            If lList.Count >= iParameterIndex + 1 Then
                Dim cValue() As String = lList(iParameterIndex).Split(";")
                For i = 0 To cValue.Count - 1
                    Dim cTempValue() As String = cValue(i).Split(",")
                    If cTempValue.Length <> 2 Then Continue For
                    If cTempValue(0) = iStationID.ToString Then
                        strDevice = cTempValue(1)
                    End If
                Next
            End If
        End If
        Return strDevice
    End Function

    Public Function GetActionParameterErrorCode(ByVal strActionName As String, ByVal strErrorType As String, ByVal iParameterIndex As Integer) As String
        Dim strErrorCode As String = String.Empty
        If lListElement.ContainsKey(strActionName) Then
            Dim lList As List(Of String) = clsParameter.ToList(lListElement(strActionName))
            If lList.Count >= iParameterIndex + 1 Then
                Dim cValue() As String = lList(iParameterIndex).Split(";")
                For i = 0 To cValue.Count - 1
                    Dim cTempValue() As String = cValue(i).Split(",")
                    If cTempValue.Length <> 2 Then Continue For
                    If cTempValue(0) = strErrorType.ToString Then
                        strErrorCode = cTempValue(1)
                    End If
                Next
            End If
        End If
        Return strErrorCode
    End Function

    Public Function GetActionParameterParameterValue(ByVal strActionName As String, ByVal strParameterType As String, ByVal iParameterIndex As Integer) As String
        Dim strErrorCode As String = String.Empty
        If lListElement.ContainsKey(strActionName) Then
            Dim lList As List(Of String) = clsParameter.ToList(lListElement(strActionName))
            If lList.Count >= iParameterIndex + 1 Then
                Dim cValue() As String = lList(iParameterIndex).Split(";")
                For i = 0 To cValue.Count - 1
                    Dim cTempValue() As String = cValue(i).Split(",")
                    If cTempValue.Length <> 2 Then Continue For
                    If cTempValue(0) = strParameterType.ToString Then
                        strErrorCode = cTempValue(1)
                    End If
                Next
            End If
        End If
        Return strErrorCode
    End Function


    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lListElement.Count <> lCurrentListElement.Count Then
                    Return False
                End If
                For i = 0 To lListElement.Count - 1
                    If lListElement(lListElement.Keys(i)) <> lCurrentListElement(lCurrentListElement.Keys(i)) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeCurrentData(ByVal strName As String, ByVal strModifyValue As String) As Boolean
        SyncLock _Object
            Try
                lCurrentListElement(strName) = strModifyValue
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentData() As Boolean
        SyncLock _Object
            Try
                Dim cHMIActionBase As clsHMIActionBase
                For Each elementIndex As String In cActionLibManager.GetListDllKey
                    Dim element As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(elementIndex)
                    cHMIActionBase = element.Source
                    If IsNothing(cHMIActionBase) Then Continue For
                    If IsNothing(cHMIActionBase.ParameterUI) Then
                        cHMIActionBase.CreateParameterUI(Nothing, cSystemElement)
                    End If
                    If Not IsNothing(cHMIActionBase.ParameterUI) Then
                        cHMIActionBase.ParameterUI.CheckParameter(Nothing, cSystemElement, clsParameter.ToList(lCurrentListElement(element.ActionName)))
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentData() As Boolean
        SyncLock _Object
            Try
                Return LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentData() As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveSection(cSystemManager.Settings.ConfigFile, "ActionParameter")
                For i = 0 To lCurrentListElement.Keys.Count - 1
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "ActionParameter", lCurrentListElement.Keys(i), lCurrentListElement(lCurrentListElement.Keys(i)))
                Next
                LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Class clsDeviceParameterManager
    Protected lListElement As New Dictionary(Of String, String)
    Protected lCurrentListElement As New Dictionary(Of String, String)
    Protected cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private cDeviceLibManager As clsDeviceLibManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)

    Public ReadOnly Property ListElement As Dictionary(Of String, String)
        Get
            Return lListElement
        End Get
    End Property


    Public ReadOnly Property CurrentListElement As Dictionary(Of String, String)
        Get
            Return lCurrentListElement
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

                ' LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                cDeviceLibManager = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
                lListElement.Clear()
                lCurrentListElement.Clear()
                For Each element As String In cDeviceLibManager.GetListDllKey
                    Dim cDeviceLibCfg As clsDeviceLibCfg = cDeviceLibManager.GetDeviceLibCfgFromKey(element)
                    Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "DeviceParameter", cDeviceLibCfg.DeviceName)
                    lListElement.Add(cDeviceLibCfg.DeviceName, mTempValue)
                    lCurrentListElement.Add(cDeviceLibCfg.DeviceName, mTempValue)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lListElement.Count <> lCurrentListElement.Count Then
                    Return False
                End If
                For i = 0 To lListElement.Count - 1
                    If lListElement(lListElement.Keys(i)) <> lCurrentListElement(lCurrentListElement.Keys(i)) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetParameterDevice(ByVal strActionName As String, ByVal iStationID As Integer, ByVal iParameterIndex As Integer) As String
        Dim strDevice As String = String.Empty
        If lListElement.ContainsKey(strActionName) Then
            Dim lList As List(Of String) = clsParameter.ToList(lListElement(strActionName))
            If lList.Count >= iParameterIndex + 1 Then
                Dim cValue() As String = lList(iParameterIndex).Split(";")
                For i = 0 To cValue.Count - 1
                    Dim cTempValue() As String = cValue(i).Split(",")
                    If cTempValue.Length <> 2 Then Continue For
                    If cTempValue(0) = iStationID.ToString Then
                        strDevice = cTempValue(1)
                    End If
                Next
            End If
        End If
        Return strDevice
    End Function

    Public Function ChangeCurrentData(ByVal strName As String, ByVal strModifyValue As String) As Boolean
        SyncLock _Object
            Try
                lCurrentListElement(strName) = strModifyValue
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentData() As Boolean
        SyncLock _Object
            Try
                Dim cHMIActionBase As clsHMIDeviceBase
                For Each elementIndex As String In cDeviceLibManager.GetListDllKey
                    Dim element As clsDeviceLibCfg = cDeviceLibManager.GetDeviceLibCfgFromKey(elementIndex)
                    cHMIActionBase = element.Source
                    If IsNothing(cHMIActionBase) Then Continue For
                    If IsNothing(cHMIActionBase.ParameterUI) Then
                        cHMIActionBase.CreateParameterUI(Nothing, cSystemElement)
                    End If
                    If Not IsNothing(cHMIActionBase.ParameterUI) Then
                        cHMIActionBase.ParameterUI.CheckParameter(Nothing, cSystemElement, clsParameter.ToList(lCurrentListElement(element.DeviceName)))
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentData() As Boolean
        SyncLock _Object
            Try
                Return LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentData() As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveSection(cSystemManager.Settings.ConfigFile, "DeviceParameter")
                For i = 0 To lCurrentListElement.Keys.Count - 1
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "DeviceParameter", lCurrentListElement.Keys(i), lCurrentListElement(lCurrentListElement.Keys(i)))
                Next
                LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Class clsMachineCellManager
    Protected cMachineCellCfg As New clsMachineCellCfg
    Protected cCurrentMachineCellCfg As New clsMachineCellCfg
    Protected cIniHandler As New clsIniHandler
    Protected cSystemManager As clsSystemManager
    Private _Object As New Object
    Private bCellChanged As Boolean
    Private cLanguageManager As clsLanguageManager
    Public Const Name As String = "MachineCellManager"


    Public Property MachineCellCfg As clsMachineCellCfg
        Set(ByVal value As clsMachineCellCfg)
            SyncLock _Object
                cMachineCellCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cMachineCellCfg
            End SyncLock
        End Get
    End Property

    Public Property CurrentMachineCfg As clsMachineCellCfg
        Set(ByVal value As clsMachineCellCfg)
            SyncLock _Object
                cCurrentMachineCellCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cCurrentMachineCellCfg
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Property CellChanged As Boolean
        Set(ByVal value As Boolean)
            bCellChanged = value
        End Set
        Get
            SyncLock _Object
                Return bCellChanged
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function StationChanged() As Boolean
        SyncLock _Object
            Return clsMachineCellCfg.StationChanged(cMachineCellCfg, cCurrentMachineCellCfg)
        End SyncLock
    End Function

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                Return cMachineCellCfg = cCurrentMachineCellCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddCurrentMachineStation() As clsMachineStationCfg
        SyncLock _Object
            Try
                'Dim t As Dictionary(Of Integer, clsMachineStationCfg) = cCurrentMachineCellCfg.MachineStationCfg.OrderBy(Function(e) e.Value.ID).ToDictionary(Function(e) e.Key, Function(f) f.Value)
                Dim mTempMachineStationCfg As New clsMachineStationCfg
                mTempMachineStationCfg.ID = (cCurrentMachineCellCfg.GetMachineStationListKey.Count + 1).ToString
                mTempMachineStationCfg.StationName = "Station:" + mTempMachineStationCfg.ID.ToString
                mTempMachineStationCfg.MachineStationType = enumMachineStationType.Manual
                mTempMachineStationCfg.VariantType = enumMachineStationType.Manual
                mTempMachineStationCfg.CheckSNType = enumCheckType.True
                mTempMachineStationCfg.ShowWaitingMessage = enumCheckType.False
                mTempMachineStationCfg.AutoConfirm = enumCheckType.False
                mTempMachineStationCfg.FailAutoRun = enumCheckType.False
                mTempMachineStationCfg.FailRunNextStation = enumCheckType.False
                mTempMachineStationCfg.MES = "NONE"
                mTempMachineStationCfg.NotInqueueColor = "Blue"
                mTempMachineStationCfg.AutoChangePage = 0
                mTempMachineStationCfg.Index = mTempMachineStationCfg.ID
                cCurrentMachineCellCfg.AddMachineStation(mTempMachineStationCfg)
                mTempMachineStationCfg.Estop = enumCheckType.False
                mTempMachineStationCfg.CheckCarrier = enumCheckType.True
                mTempMachineStationCfg.ResetCarrier = enumCheckType.False
                mTempMachineStationCfg.CompleteStep = "2"
                Return mTempMachineStationCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function DeleteCurrentMachineStation(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                cCurrentMachineCellCfg.RemoveMachineStationCfgFromKey(CInt(strID) - 1)
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasAutoStation() As Boolean
        SyncLock _Object
            For Each element In cMachineCellCfg.GetMachineStationListKey
                Dim cMachineStationCfg As clsMachineStationCfg = cMachineCellCfg.GetMachineStationCfgFromKey(element)
                If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Function


    Public Function HasManualStation() As Boolean
        SyncLock _Object
            For Each element In cMachineCellCfg.GetMachineStationListKey
                Dim cMachineStationCfg As clsMachineStationCfg = cMachineCellCfg.GetMachineStationCfgFromKey(element)
                If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Function


    Public Function HasOneManualStation() As Boolean
        SyncLock _Object
            Dim iCnt As Integer = 0
            For Each element In cMachineCellCfg.GetMachineStationListKey
                Dim cMachineStationCfg As clsMachineStationCfg = cMachineCellCfg.GetMachineStationCfgFromKey(element)
                If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then
                    iCnt = iCnt + 1
                End If
            Next
            Return IIf(iCnt = 1, True, False)
        End SyncLock
    End Function

    Public Function HasCurrentMachineStation(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If cCurrentMachineCellCfg.HasMachineStationFromName(strName) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMachineStationName(ByVal strID As String, ByVal strModifyName As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.StationName = strModifyName
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMachineStationDescription(ByVal strID As String, ByVal strModifyDescription As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.Description = strModifyDescription
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMachineStationType(ByVal strID As String, ByVal strModifyType As enumMachineStationType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.MachineStationType = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMachineStationChangePage(ByVal strID As String, ByVal strModifyType As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.AutoChangePage = CInt(strModifyType)
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMachineStationHMIError(ByVal strID As String, ByVal strModifyType As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.HMIError = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentVariantType(ByVal strID As String, ByVal strModifyType As enumMachineStationType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.VariantType = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentCheckSNType(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.CheckSNType = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentShowWaitingMessage(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.ShowWaitingMessage = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentAutoConfirm(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.AutoConfirm = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentFailAutoRun(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.FailAutoRun = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentFailRunNextStation(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.FailRunNextStation = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentMES(ByVal strID As String, ByVal strModifyType As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.MES = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentESTOP(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.Estop = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function ChangeCurrentCarrier(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.CheckCarrier = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentResetCarrier(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.ResetCarrier = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function ChangeCurrentNotInqueueColor(ByVal strID As String, ByVal strModifyType As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.NotInqueueColor = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentComplete(ByVal strID As String, ByVal strModifyType As String) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.CompleteStep = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentCheckArticleType(ByVal strID As String, ByVal strModifyType As enumCheckType) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.CheckArticleType = strModifyType
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeCurrentMachineStationIndex(ByVal strID As String, ByVal iIndex As Integer) As Boolean
        SyncLock _Object
            Try
                For Each elementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.ID = strID Then
                        element.Index = iIndex
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpRowCurrentMachineStation(ByVal iID As Integer) As Boolean
        SyncLock _Object
            Try
                Dim TempMachineStationCfg As clsMachineStationCfg
                If iID <= 1 Then Return True
                TempMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(iID - 2).Clone
                cCurrentMachineCellCfg.ChangeMachineStationCfgFromKey(iID - 2, cCurrentMachineCellCfg.GetMachineStationCfgFromKey(iID - 1).Clone)
                cCurrentMachineCellCfg.ChangeMachineStationCfgFromKey(iID - 1, TempMachineStationCfg.Clone)
                cCurrentMachineCellCfg.ChangeMachineStationIDFromKey(iID - 1, iID.ToString)
                cCurrentMachineCellCfg.ChangeMachineStationIDFromKey(iID - 2, (iID - 1).ToString)
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function DownRowCurrentMachineStation(ByVal iID As Integer) As Boolean
        SyncLock _Object
            Try
                Dim TempMachineStationCfg As clsMachineStationCfg
                TempMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(iID).Clone
                cCurrentMachineCellCfg.ChangeMachineStationCfgFromKey(iID, cCurrentMachineCellCfg.GetMachineStationCfgFromKey(iID - 1).Clone)
                cCurrentMachineCellCfg.ChangeMachineStationCfgFromKey(iID - 1, TempMachineStationCfg.Clone)
                cCurrentMachineCellCfg.ChangeMachineStationIDFromKey(iID - 1, iID.ToString)
                cCurrentMachineCellCfg.ChangeMachineStationIDFromKey(iID, (iID + 1).ToString)
                ChangeID()
                Return (True)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                Dim j As Integer = 1
                For i = 0 To cCurrentMachineCellCfg.GetMachineStationListKey.Count - 1
                    cCurrentMachineCellCfg.GetMachineStationCfgFromKey(i).ID = j.ToString
                    cCurrentMachineCellCfg.GetMachineStationCfgFromKey(i).Index = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Public Function LoadMachineCellManager() As Boolean
        SyncLock _Object
            Try
                cMachineCellCfg.ProjectName = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "Project", "Name")
                cMachineCellCfg.CellName = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Name")
                cMachineCellCfg.CellDescription = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Description")
                cMachineCellCfg.CellPicture = clsSystemPath.ToSystemPath(cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Picture"))
                If cMachineCellCfg.CellPicture = "" Then
                    cMachineCellCfg.CellPicture = cSystemManager.Settings.ResourcesFolder + "\StationBJ.jpg"
                End If
                cMachineCellCfg.CleanMachineStation()
                Dim cMachineElement As clsMachineStationCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.ConfigFile, "Station", New String() {"ID", "Type", "Name", "SelectVariant", "CheckArticle", "CheckSN", "Description", "AutoChangePage", "HMIError", "Index", "ShowWaitingMessage", "AutoConfirm", "FailAutoRun", "FailAutoRunNext", "MES", "NotInqueueColor", "ESTOP", "CheckCarrier", "ResetCarrier", "CompleteStep"})
                    cMachineElement = New clsMachineStationCfg
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("Type"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Type"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.MachineStationType = [Enum].Parse(GetType(enumMachineStationType), CType(element("Type"), String))
                    End If
                    If CType(element("Name"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Name"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.StationName = CType(element("Name"), String)
                    End If
                    If CType(element("SelectVariant"), String) <> clsXmlHandler.s_DEFAULT And CType(element("SelectVariant"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.VariantType = [Enum].Parse(GetType(enumMachineStationType), CType(element("SelectVariant"), String))
                    End If

                    If CType(element("CheckArticle"), String) <> clsXmlHandler.s_DEFAULT And CType(element("CheckArticle"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.CheckArticleType = [Enum].Parse(GetType(enumCheckType), CType(element("CheckArticle"), String))
                    Else
                        cMachineElement.CheckArticleType = enumCheckType.True
                    End If
                    If CType(element("CheckSN"), String) <> clsXmlHandler.s_DEFAULT And CType(element("CheckSN"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.CheckSNType = [Enum].Parse(GetType(enumCheckType), CType(element("CheckSN"), String))
                    Else
                        cMachineElement.CheckSNType = enumCheckType.True
                    End If

                    If CType(element("ShowWaitingMessage"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ShowWaitingMessage"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.ShowWaitingMessage = [Enum].Parse(GetType(enumCheckType), CType(element("ShowWaitingMessage"), String))
                    Else
                        cMachineElement.ShowWaitingMessage = enumCheckType.False
                    End If

                    If CType(element("AutoConfirm"), String) <> clsXmlHandler.s_DEFAULT And CType(element("AutoConfirm"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.AutoConfirm = [Enum].Parse(GetType(enumCheckType), CType(element("AutoConfirm"), String))
                    Else
                        cMachineElement.AutoConfirm = enumCheckType.False
                    End If

                    If CType(element("FailAutoRun"), String) <> clsXmlHandler.s_DEFAULT And CType(element("FailAutoRun"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.FailAutoRun = [Enum].Parse(GetType(enumCheckType), CType(element("FailAutoRun"), String))
                    Else
                        cMachineElement.FailAutoRun = enumCheckType.False
                    End If

                    If CType(element("FailAutoRunNext"), String) <> clsXmlHandler.s_DEFAULT And CType(element("FailAutoRunNext"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.FailRunNextStation = [Enum].Parse(GetType(enumCheckType), CType(element("FailAutoRunNext"), String))
                    Else
                        cMachineElement.FailRunNextStation = enumCheckType.False
                    End If

                    If CType(element("MES"), String) <> clsXmlHandler.s_DEFAULT And CType(element("MES"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.MES = CType(element("MES"), String)
                    Else
                        cMachineElement.MES = "NONE"
                    End If

                    If CType(element("ESTOP"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ESTOP"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.Estop = [Enum].Parse(GetType(enumCheckType), CType(element("ESTOP"), String))
                    Else
                        cMachineElement.Estop = enumCheckType.False
                    End If

                    If CType(element("CheckCarrier"), String) <> clsXmlHandler.s_DEFAULT And CType(element("CheckCarrier"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.CheckCarrier = [Enum].Parse(GetType(enumCheckType), CType(element("CheckCarrier"), String))
                    Else
                        cMachineElement.CheckCarrier = enumCheckType.True
                    End If

                    If CType(element("ResetCarrier"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ResetCarrier"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.ResetCarrier = [Enum].Parse(GetType(enumCheckType), CType(element("ResetCarrier"), String))
                    Else
                        cMachineElement.ResetCarrier = enumCheckType.False
                    End If

                    If CType(element("NotInqueueColor"), String) <> clsXmlHandler.s_DEFAULT And CType(element("NotInqueueColor"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.NotInqueueColor = CType(element("NotInqueueColor"), String)
                    Else
                        cMachineElement.NotInqueueColor = "Blue"
                    End If


                    If CType(element("CompleteStep"), String) <> clsXmlHandler.s_DEFAULT And CType(element("CompleteStep"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.CompleteStep = CType(element("CompleteStep"), String)
                    Else
                        cMachineElement.CompleteStep = "3"
                    End If

                    If CType(element("Description"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Description"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.Description = CType(element("Description"), String)
                    End If
                    If CType(element("AutoChangePage"), String) <> clsXmlHandler.s_DEFAULT And CType(element("AutoChangePage"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.AutoChangePage = CType(element("AutoChangePage"), Integer)
                    End If

                    If CType(element("HMIError"), String) <> clsXmlHandler.s_DEFAULT And CType(element("HMIError"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.HMIError = CType(element("HMIError"), String)
                    Else
                        cMachineElement.HMIError = "NONE"
                    End If
                    If CType(element("Index"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Index"), String) <> clsXmlHandler.s_Null Then
                        cMachineElement.Index = CType(element("Index"), Integer)
                    Else
                        cMachineElement.Index = cMachineElement.ID
                    End If
                    cMachineCellCfg.AddMachineStation(cMachineElement)
                Next
                cCurrentMachineCellCfg = cMachineCellCfg.Clone
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentCfg() As Boolean
        SyncLock _Object
            Try
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "Project", "Name", cCurrentMachineCellCfg.ProjectName)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Name", cCurrentMachineCellCfg.CellName)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Description", cCurrentMachineCellCfg.CellDescription)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "MachineCell", "Picture", clsSystemPath.ToIniPath(cCurrentMachineCellCfg.CellPicture))
                cIniHandler.RemoveAllSection(cSystemManager.Settings.ConfigFile, "Station")

                For Each ElementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim cMachineElement As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(ElementIndex)
                    cIniHandler.SetAnyListToIni(cSystemManager.Settings.ConfigFile, "Station" + cMachineElement.ID.ToString, New String() {"ID", "Type", "Name", "SelectVariant", "CheckArticle", "CheckSN", "Description", "AutoChangePage", "HMIError", "Index", "ShowWaitingMessage", "AutoConfirm", "FailAutoRun", "FailAutoRunNext", "MES", "NotInqueueColor", "ESTOP", "CheckCarrier", "ResetCarrier", "CompleteStep"},
                                          New String() {cMachineElement.ID.ToString, cMachineElement.MachineStationType.ToString, cMachineElement.StationName, cMachineElement.VariantType.ToString, cMachineElement.CheckArticleType.ToString, cMachineElement.CheckSNType.ToString, cMachineElement.Description, cMachineElement.AutoChangePage, cMachineElement.HMIError, cMachineElement.Index, cMachineElement.ShowWaitingMessage, cMachineElement.AutoConfirm, cMachineElement.FailAutoRun, cMachineElement.FailRunNextStation, cMachineElement.MES, cMachineElement.NotInqueueColor, cMachineElement.Estop.ToString, cMachineElement.CheckCarrier.ToString, cMachineElement.ResetCarrier.ToString, cMachineElement.CompleteStep.ToString})
                Next
                cMachineCellCfg = cCurrentMachineCellCfg.Clone
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentCfg() As Boolean
        SyncLock _Object
            Try
                If cCurrentMachineCellCfg.ProjectName = "" Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "1"), enumExceptionType.Alarm)
                End If
                If cCurrentMachineCellCfg.CellName = "" Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "2"), enumExceptionType.Alarm)
                End If
                If cCurrentMachineCellCfg.CellDescription = "" Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "3"), enumExceptionType.Alarm)
                End If

                For Each ElementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim cMachineElement As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(ElementIndex)
                    If cMachineElement.StationName = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "4", cMachineElement.ID), enumExceptionType.Alarm)
                    End If
                    If IsNothing(cMachineElement.MachineStationType) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "5", cMachineElement.ID), enumExceptionType.Alarm)
                    End If

                    If IsNothing(cMachineElement.VariantType) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "6", cMachineElement.ID), enumExceptionType.Alarm)
                    End If

                    If IsNothing(cMachineElement.CheckSNType) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "7", cMachineElement.ID), enumExceptionType.Alarm)
                    End If

                    If IsNothing(cMachineElement.CheckArticleType) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "12", cMachineElement.ID), enumExceptionType.Alarm)
                    End If

                    If IsNothing(cMachineElement.AutoChangePage) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "9", cMachineElement.ID), enumExceptionType.Alarm)
                    End If

                    If cMachineElement.Index < 1 Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "10", cMachineElement.ID), enumExceptionType.Alarm)
                    End If
                Next
                For Each ElementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                    Dim cMachineElement As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(ElementIndex)

                    For Each subElementIndex As Integer In cCurrentMachineCellCfg.GetMachineStationListKey
                        Dim csubMachineElement As clsMachineStationCfg = cCurrentMachineCellCfg.GetMachineStationCfgFromKey(subElementIndex)
                        If cMachineElement.Index = csubMachineElement.Index And cMachineElement.ID <> csubMachineElement.ID Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MachineCellManager", "11", cMachineElement.ID, csubMachineElement.ID), enumExceptionType.Alarm)
                        End If
                    Next
                Next

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentCfg() As Boolean
        SyncLock _Object
            Try
                cCurrentMachineCellCfg = cMachineCellCfg.Clone
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class

Public Class clsMachineCellCfg
    Protected strProjectName As String = String.Empty
    Protected strCellName As String = String.Empty
    Protected strCellPicture As String = String.Empty
    Protected strCellDescription As String = String.Empty
    Protected _Object As New Object
    Protected Shared _Object2 As New Object
    Protected lMachineStationCfg As New List(Of clsMachineStationCfg)

    Public Property ProjectName As String
        Set(ByVal value As String)
            SyncLock _Object
                strProjectName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strProjectName
            End SyncLock
        End Get
    End Property

    Public Property CellName As String
        Set(ByVal value As String)
            SyncLock _Object
                strCellName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strCellName
            End SyncLock
        End Get
    End Property

    Public Property CellPicture As String
        Set(ByVal value As String)
            SyncLock _Object
                strCellPicture = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strCellPicture
            End SyncLock
        End Get
    End Property

    Public Property CellDescription As String
        Set(ByVal value As String)
            SyncLock _Object
                strCellDescription = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strCellDescription
            End SyncLock
        End Get
    End Property

    Public Function GetMachineStationListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lMachineStationCfg.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineStationCfgFromKey(ByVal iKey As Integer) As clsMachineStationCfg
        SyncLock _Object
            Try
                If iKey <= lMachineStationCfg.Count - 1 Then
                    Return lMachineStationCfg(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function ChangeMachineStationCfgFromKey(ByVal iKey As Integer, ByVal MachineStationCfg As clsMachineStationCfg) As Boolean
        SyncLock _Object
            Try
                lMachineStationCfg(iKey) = MachineStationCfg
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function ChangeMachineStationIDFromKey(ByVal iKey As Integer, ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                lMachineStationCfg(iKey).ID = strID
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveMachineStationCfgFromKey(ByVal iKey As Integer) As Boolean
        SyncLock _Object
            Try
                lMachineStationCfg.RemoveAt(iKey)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddMachineStation(ByVal cMachineStationCfg As clsMachineStationCfg) As Boolean
        SyncLock _Object
            Try
                lMachineStationCfg.Add(cMachineStationCfg)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CleanMachineStation() As Boolean
        SyncLock _Object
            Try
                lMachineStationCfg.Clear()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasMachineStationFromName(ByVal strStationName As String) As Boolean
        SyncLock _Object
            Try
                Return lMachineStationCfg.Any(Function(e) e.StationName = strStationName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasManualSelectVariant() As Boolean
        SyncLock _Object
            Try
                Return lMachineStationCfg.Any(Function(e) e.VariantType = enumMachineStationType.Manual)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetMachineStationCfgFromName(ByVal strName As String) As clsMachineStationCfg
        SyncLock _Object
            Try
                For Each element As clsMachineStationCfg In lMachineStationCfg
                    If element.StationName = strName Then
                        Return element
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineStationCfgFromID(ByVal strID As String) As clsMachineStationCfg
        SyncLock _Object
            Try
                For Each element As clsMachineStationCfg In lMachineStationCfg
                    If element.ID = strID Then
                        Return element
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function Equal(ByVal TarList As List(Of clsMachineStationCfg)) As Boolean
        SyncLock _Object
            If lMachineStationCfg.Count <> TarList.Count Then
                Return False
            End If
            Dim element As clsMachineStationCfg
            For Each element In TarList
                If Not TarList.Any(Function(e) e.ID = element.ID) Then
                    Return False
                End If
                If GetMachineStationCfgFromID(element.ID) <> element Then
                    Return False
                End If
            Next
            Return True
        End SyncLock
    End Function

    Public Function Clone() As clsMachineCellCfg
        SyncLock _Object
            Dim cTemp As New clsMachineCellCfg
            cTemp.ProjectName = ProjectName
            cTemp.CellName = CellName
            cTemp.CellPicture = CellPicture
            cTemp.strCellDescription = CellDescription
            For Each element In lMachineStationCfg
                cTemp.lMachineStationCfg.Add(element.Clone)
            Next
            Return cTemp
        End SyncLock
    End Function


    Public Shared Function StationChanged(ByVal x As clsMachineCellCfg, ByVal y As clsMachineCellCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return Not x.Equal(y.lMachineStationCfg)
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsMachineCellCfg, ByVal y As clsMachineCellCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ProjectName <> y.ProjectName Or x.CellName <> y.CellName Or x.CellPicture <> y.CellPicture Or x.CellDescription <> y.CellDescription Or x.Equal(y.lMachineStationCfg)
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsMachineCellCfg, ByVal y As clsMachineCellCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ProjectName = y.ProjectName And x.CellName = y.CellName And x.CellPicture = y.CellPicture And x.CellDescription = y.CellDescription And x.Equal(y.lMachineStationCfg)
        End SyncLock
    End Operator

End Class

Public Class clsMachineStationCfg
    Protected strID As Integer = 0
    Protected strStationName As String = String.Empty
    Protected strDescription As String = String.Empty
    Protected _Object As New Object
    Protected Shared _Object2 As New Object
    Protected eMachineStationType As enumMachineStationType
    Protected eVariantType As enumMachineStationType
    Protected eCheckArticleType As enumCheckType
    Protected eCheckSNType As enumCheckType
    Protected iAutoChangePage As Integer = 0
    Protected iIndex As Integer = 0
    Protected strHMIError As String = "NONE"
    Protected eShowWaitingMessage As enumCheckType
    Protected eAutoConfirm As enumCheckType
    Public Const Name As String = "MachineStationCfg"
    Protected strMES As String = "NONE"
    Protected eFailAutoRun As enumCheckType
    Protected eFailRunNextStation As enumCheckType
    Protected strNotInqueueColor As String = "Blue"
    Protected eEstop As enumCheckType
    Protected eCheckCarrier As enumCheckType
    Protected eResetCarrier As enumCheckType
    Protected strCompleteStep As String = "2"

    Public Property CompleteStep As String
        Set(ByVal value As String)
            SyncLock _Object
                strCompleteStep = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strCompleteStep
            End SyncLock
        End Get
    End Property

    Public Property NotInqueueColor As String
        Set(ByVal value As String)
            SyncLock _Object
                strNotInqueueColor = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strNotInqueueColor
            End SyncLock
        End Get
    End Property
    Public Property ID As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                strID = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strID
            End SyncLock
        End Get
    End Property

    Public Property StationName As String
        Set(ByVal value As String)
            SyncLock _Object
                strStationName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strStationName
            End SyncLock
        End Get
    End Property

    Public Property HMIError As String
        Set(ByVal value As String)
            SyncLock _Object
                strHMIError = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strHMIError
            End SyncLock
        End Get
    End Property

    Public Property MES As String
        Set(ByVal value As String)
            SyncLock _Object
                strMES = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strMES
            End SyncLock
        End Get
    End Property

    Public Property Description As String
        Set(ByVal value As String)
            SyncLock _Object
                strDescription = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strDescription
            End SyncLock
        End Get
    End Property

    Public Property MachineStationType As enumMachineStationType
        Set(ByVal value As enumMachineStationType)
            SyncLock _Object
                eMachineStationType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eMachineStationType
            End SyncLock
        End Get
    End Property

    Public Property ShowWaitingMessage As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eShowWaitingMessage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eShowWaitingMessage
            End SyncLock
        End Get
    End Property

    Public Property AutoConfirm As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eAutoConfirm = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eAutoConfirm
            End SyncLock
        End Get
    End Property

    Public Property Estop As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eEstop = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eEstop
            End SyncLock
        End Get
    End Property

    Public Property CheckCarrier As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eCheckCarrier = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eCheckCarrier
            End SyncLock
        End Get
    End Property

    Public Property ResetCarrier As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eResetCarrier = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eResetCarrier
            End SyncLock
        End Get
    End Property

    Public Property CheckSNType As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eCheckSNType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eCheckSNType
            End SyncLock
        End Get
    End Property

    Public Property FailAutoRun As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eFailAutoRun = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eFailAutoRun
            End SyncLock
        End Get
    End Property

    Public Property FailRunNextStation As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eFailRunNextStation = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eFailRunNextStation
            End SyncLock
        End Get
    End Property

    Public Property CheckArticleType As enumCheckType
        Set(ByVal value As enumCheckType)
            SyncLock _Object
                eCheckArticleType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eCheckArticleType
            End SyncLock
        End Get
    End Property

    Public Property VariantType As enumMachineStationType
        Set(ByVal value As enumMachineStationType)
            SyncLock _Object
                eVariantType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eVariantType
            End SyncLock
        End Get
    End Property

    Public Property AutoChangePage As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iAutoChangePage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iAutoChangePage
            End SyncLock
        End Get
    End Property


    Public Property Index As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iIndex
            End SyncLock
        End Get
    End Property

    Public Sub New()
        SyncLock _Object

        End SyncLock
    End Sub

    Public Sub New(ByVal strName As String)
        SyncLock _Object
            Me.strStationName = strName
        End SyncLock
    End Sub

    Public Sub New(ByVal strID As String, ByVal strName As String)
        SyncLock _Object
            Me.strID = strID
            Me.strStationName = strName
        End SyncLock
    End Sub

    Public Sub New(ByVal eMachineStationType As enumMachineStationType)
        SyncLock _Object
            Me.eMachineStationType = eMachineStationType
        End SyncLock
    End Sub

    Public Function Clone() As clsMachineStationCfg
        SyncLock _Object
            Dim cTemp As New clsMachineStationCfg
            cTemp.ID = ID
            cTemp.StationName = StationName
            cTemp.Description = Description
            cTemp.MachineStationType = MachineStationType
            cTemp.VariantType = VariantType
            cTemp.CheckSNType = CheckSNType
            cTemp.CheckArticleType = CheckArticleType
            cTemp.ShowWaitingMessage = ShowWaitingMessage
            cTemp.AutoChangePage = AutoChangePage
            cTemp.AutoConfirm = AutoConfirm
            cTemp.FailAutoRun = FailAutoRun
            cTemp.FailRunNextStation = FailRunNextStation
            cTemp.Index = Index
            cTemp.HMIError = HMIError
            cTemp.MES = MES
            cTemp.Estop = Estop
            cTemp.NotInqueueColor = NotInqueueColor
            cTemp.CheckCarrier = CheckCarrier
            cTemp.ResetCarrier = ResetCarrier
            cTemp.CompleteStep = CompleteStep
            Return cTemp
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsMachineStationCfg, ByVal y As clsMachineStationCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ID <> y.ID Or x.StationName <> y.StationName Or x.MachineStationType <> y.MachineStationType Or x.VariantType <> y.VariantType Or x.CheckSNType <> y.CheckSNType Or x.Description <> y.Description Or x.AutoChangePage <> y.AutoChangePage Or x.Index <> y.Index Or x.HMIError <> y.HMIError Or x.CheckArticleType <> y.CheckArticleType Or x.ShowWaitingMessage <> y.ShowWaitingMessage Or x.AutoConfirm <> y.AutoConfirm Or x.FailAutoRun <> y.FailAutoRun Or x.FailRunNextStation <> y.FailRunNextStation Or x.MES <> y.MES Or x.NotInqueueColor <> y.NotInqueueColor Or x.Estop <> y.Estop Or x.CheckCarrier <> y.CheckCarrier Or x.ResetCarrier <> y.ResetCarrier Or x.CompleteStep <> y.CompleteStep
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsMachineStationCfg, ByVal y As clsMachineStationCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ID = y.ID And x.StationName = y.StationName And x.MachineStationType = y.MachineStationType And x.VariantType = y.VariantType And x.CheckSNType = y.CheckSNType And x.Description = y.Description And x.AutoChangePage = y.AutoChangePage And x.Index = y.Index And x.HMIError = y.HMIError And x.CheckArticleType = y.CheckArticleType And x.ShowWaitingMessage = y.ShowWaitingMessage And x.AutoConfirm = y.AutoConfirm And x.FailAutoRun = y.FailAutoRun And x.FailRunNextStation = y.FailRunNextStation And x.MES = y.MES And x.NotInqueueColor = y.NotInqueueColor And x.Estop = y.Estop And x.CheckCarrier = y.CheckCarrier And x.ResetCarrier = y.ResetCarrier And x.CompleteStep = y.CompleteStep
        End SyncLock
    End Operator
End Class


Public Class clsMachineStatusParameterManager
    Protected lListElement As New Dictionary(Of String, String)
    Protected lCurrentListElement As New Dictionary(Of String, String)
    Protected cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Public ReadOnly Property ListElement As Dictionary(Of String, String)
        Get
            Return lListElement
        End Get
    End Property


    Public ReadOnly Property CurrentListElement As Dictionary(Of String, String)
        Get
            Return lCurrentListElement
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                lListElement.Clear()
                lCurrentListElement.Clear()
                For Each element As String In {enumManchineActionType.PowerOn.ToString, enumManchineActionType.PowerOff.ToString, enumManchineActionType.Auto.ToString, enumManchineActionType.Work.ToString, enumManchineActionType.Alarm.ToString, enumManchineActionType.Waiting.ToString}
                    Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFile, "MachineStatusParameter", element)
                    lListElement.Add(element, mTempValue)
                    lCurrentListElement.Add(element, mTempValue)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetMachineStatusParameter(ByVal strTypeName As String) As String
        Dim strDevice As String = String.Empty
        If lListElement.ContainsKey(strTypeName) Then
            Return lListElement(strTypeName)
        Else
            Return ""
        End If
    End Function


    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lListElement.Count <> lCurrentListElement.Count Then
                    Return False
                End If
                For i = 0 To lListElement.Count - 1
                    If lListElement(lListElement.Keys(i)) <> lCurrentListElement(lCurrentListElement.Keys(i)) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeCurrentData(ByVal strName As String, ByVal strModifyValue As String) As Boolean
        SyncLock _Object
            Try
                lCurrentListElement(strName) = strModifyValue
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentData() As Boolean
        SyncLock _Object
            Try
                For i = 0 To lCurrentListElement.Keys.Count - 1
                    If lCurrentListElement(lCurrentListElement.Keys(i)) = "" Then
                        Throw (New clsHMIException(cLanguageManager.GetTextLine("MachineStatusParameterManager", "1", lCurrentListElement.Keys(i)), enumExceptionType.Alarm))
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentData() As Boolean
        SyncLock _Object
            Try
                Return LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentData() As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveSection(cSystemManager.Settings.ConfigFile, "MachineStatusParameter")
                For i = 0 To lCurrentListElement.Keys.Count - 1
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFile, "MachineStatusParameter", lCurrentListElement.Keys(i), lCurrentListElement(lCurrentListElement.Keys(i)))
                Next
                LoadData()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class


Public Enum enumMachineStationType
    Auto
    Manual
End Enum


Public Enum enumCheckType
    [True]
    [False]
End Enum
