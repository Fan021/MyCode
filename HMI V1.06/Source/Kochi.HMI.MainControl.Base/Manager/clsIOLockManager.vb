Imports Kochi.HMI.MainControl.Device
Public Class clsIOLockManager
    Private cDebugMode As Boolean
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Public Const Name As String = "IOLockManager"
    Private lListIO As New List(Of clsIOLockCfg)
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Public Property ListIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIO = value
        End Set
        Get
            Return lListIO
        End Get
    End Property

    Public Property IOManager As clsIOManager
        Set(ByVal value As clsIOManager)
            cIOManager = value
        End Set
        Get
            Return cIOManager
        End Get
    End Property
    Public Property CylinderManager As clsCylinderManager
        Set(ByVal value As clsCylinderManager)
            cCylinderManager = value
        End Set
        Get
            Return cCylinderManager
        End Get
    End Property

    Public Property ProgramButton As clsProgramButton
        Set(ByVal value As clsProgramButton)
            cProgramButton = value
        End Set
        Get
            Return cProgramButton
        End Get
    End Property


    Public Property ProgramCylinderButton As clsProgramCylinderButton
        Set(ByVal value As clsProgramCylinderButton)
            cProgramCylinderButton = value
        End Set
        Get
            Return cProgramCylinderButton
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
                cHMIPLC = cDeviceManager.GetPLCDevice
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckIO(ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                For Each element As clsIOLockCfg In lListLockIO
                    If element.TypeName = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                    End If
                    If element.IndexX <= 0 Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                    End If
                    If element.IndexY <= 0 Then
                        Select Case element.TypeName
                            Case "EL1008", "EP1008", "EL2008"
                                Dim cIOPageCfg As clsIOPageCfg = cIOManager.GetIOPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "2", iCnt, cIOPageCfg.ActiveText), enumExceptionType.Alarm)
                                End If

                            Case "Cylinder"
                                Dim cIOPageCfg As clsCylinderPageCfg = cCylinderManager.GetCylinderPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "2", iCnt, cIOPageCfg.ActiveText), enumExceptionType.Alarm)
                                End If

                            Case "ProgramButton"
                                Dim cIOPageCfg As clsIOPageCfg = cProgramButton.GetIOPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "2", iCnt, cIOPageCfg.ActiveText), enumExceptionType.Alarm)
                                End If

                            Case "ProgramCylinder"
                                Dim cIOPageCfg As clsCylinderPageCfg = cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "2", iCnt, cIOPageCfg.ActiveText), enumExceptionType.Alarm)
                                End If
                        End Select
                    End If

                    If element.Status = "" Then
                        Select Case element.TypeName
                            Case "EL1008", "EP1008", "EL2008"
                                Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(element.IndexX, element.IndexY)
                                If IsNothing(cIOCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cIOManager.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText), enumExceptionType.Alarm)
                                End If


                            Case "Cylinder"
                                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                                If IsNothing(cIOCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    If element.IndexY Mod 2 = 0 Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB), enumExceptionType.Alarm)
                                    Else
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA), enumExceptionType.Alarm)
                                    End If
                                End If

                            Case "ProgramButton"
                                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromID(element.IndexX, element.IndexY)
                                If IsNothing(cIOCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cProgramButton.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText), enumExceptionType.Alarm)
                                End If

                            Case "ProgramCylinder"
                                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                                If IsNothing(cIOCfg) Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    If element.IndexY Mod 2 = 0 Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB), enumExceptionType.Alarm)
                                    Else
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "3", iCnt, cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA), enumExceptionType.Alarm)
                                    End If
                                End If
                        End Select
                    End If

                    iCnt = iCnt + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckLockIO(ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                Dim bReadResult As Boolean = False
                For Each element As clsIOLockCfg In lListLockIO
                    Select Case element.TypeName
                        Case "EL1008", "EP1008", "EL2008"
                            Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(element.IndexX, element.IndexY)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                bReadResult = CType(cHMIPLC.ReadAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", GetType(Boolean)), Boolean)
                                If element.Status = "ON" Then
                                    If Not bReadResult Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cIOManager.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText, element.Status), enumExceptionType.Alarm)
                                    End If
                                End If
                                If element.Status = "OFF" Then
                                    If bReadResult Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cIOManager.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText, element.Status), enumExceptionType.Alarm)
                                    End If
                                End If
                            End If


                        Case "Cylinder"
                            Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                If element.IndexY Mod 2 = 0 Then
                                    bReadResult = CType(cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_Cylinder_AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                Else
                                    bReadResult = CType(cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_Cylinder_AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                End If

                            End If

                        Case "ProgramButton"
                            Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromID(element.IndexX, element.IndexY)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                bReadResult = CType(cHMIPLC.ReadAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + cIOCfg.YIndex.ToString + "]", GetType(Boolean)), Boolean)
                                If element.Status = "ON" Then
                                    If Not bReadResult Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramButton.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText, element.Status), enumExceptionType.Alarm)
                                    End If
                                End If
                                If element.Status = "OFF" Then
                                    If bReadResult Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramButton.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText, element.Status), enumExceptionType.Alarm)
                                    End If
                                End If
                            End If

                        Case "ProgramCylinder"
                            Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                If element.IndexY Mod 2 = 0 Then
                                    bReadResult = CType(cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                Else
                                    bReadResult = CType(cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "4", cProgramCylinderButton.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA, element.Status), enumExceptionType.Alarm)
                                        End If
                                    End If
                                End If

                            End If
                    End Select


                    iCnt = iCnt + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function
End Class

Public Class clsIOLockCfg
    Public TypeName As String = String.Empty
    Public IndexX As Integer = -1
    Public IndexY As Integer = -1
    Public Status As String = String.Empty
    Public Function Clone() As clsIOLockCfg
        Dim cTempIOLockCfg As New clsIOLockCfg
        cTempIOLockCfg.TypeName = TypeName
        cTempIOLockCfg.IndexX = IndexX
        cTempIOLockCfg.IndexY = IndexY
        cTempIOLockCfg.Status = Status
        Return cTempIOLockCfg
    End Function

End Class