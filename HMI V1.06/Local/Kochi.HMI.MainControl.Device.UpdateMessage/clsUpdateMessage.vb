Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Threading

<clsHMIDeviceNameAttribute("UpdateMessage", "UpdateMessage")>
Public Class clsUpdateMessage
    Inherits clsHMIDeviceBase
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceCfg As clsDeviceCfg
    Private cSystemManager As clsSystemManager
    Private cIniHandler As clsIniHandler
    Private cThread As Thread
    Private bExit As Boolean = False
    Private cErrorMessageManager As clsErrorMessageManager
    Private FormName As String = "UpdateMessage"
    Private cMainTipsManager As clsMainTipsManager
    Private cPictureShowManager As clsPictureShowManager
    Private cVariantManager As clsVariantManager
    Private cMachineStatusManager As clsMachineStatusManager
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.cSystemElement = cSystemElement
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "3"), enumExceptionType.Crash)
            Return False
        End If
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(Name)
       
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(1))
        Me.lListInitParameter = lListInitParameter
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()
        Return True
    End Function


    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("UpdateMessage", "3"), enumExceptionType.Alarm, FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("UpdateMessage", "3", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(1), GetType(StructUpdateMessage), New StructUpdateMessage)
                        iStep = iStep + 1

                    Case 4

                        Dim cUpdateMessage As StructUpdateMessage = cHMIPLC.GetValue(lListInitParameter(1))
                        If cUpdateMessage.bulPLCUpdate Then
                            iStep = iStep + 1
                        End If
                    Case 5
                        If cMachineStatusManager.lListPictureShowManager.ContainsKey(lListInitParameter(0)) Then
                            cPictureShowManager = cMachineStatusManager.lListPictureShowManager(lListInitParameter(0))
                        End If
                        If cMachineStatusManager.lListVariantManager.ContainsKey(lListInitParameter(0)) Then
                            cVariantManager = cMachineStatusManager.lListVariantManager(lListInitParameter(0))
                        End If
                        If Not IsNothing(cPictureShowManager) And Not IsNothing(cVariantManager) Then
                            cPictureShowManager.ShowPicture(cVariantManager.CurrentVariantCfg.PicturePath)
                        End If

                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(lListInitParameter(0), cLanguageManager.GetUserTextLine("UpdateMessage", "4")))
                        iStep = iStep + 1

                    Case 6
                        cHMIPLC.WriteAny(lListInitParameter(1) + ".bulHMIUpdate", True)
                        iStep = iStep + 1

                    Case 7
                        Dim cUpdateMessage As StructUpdateMessage = cHMIPLC.GetValue(lListInitParameter(1))
                        If Not cUpdateMessage.bulPLCUpdate And Not cUpdateMessage.bulHMIUpdate Then
                            iStep = 4
                        End If
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, FormName))
            End Try


        End While

    End Sub

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try

            bExit = True
            Dim iCnt As Integer = 100
            Do While iCnt > 0
                If IsNothing(cThread) Then
                    Exit Do
                End If
                If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                    Exit Do
                End If
                iCnt = iCnt - 1
                System.Threading.Thread.Sleep(1)
            Loop
            If Not IsNothing(cThread) Then cThread.Abort()

            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iProgramUI) Then
                iProgramUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iControlUI) Then
                iControlUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If


            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then
            RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iInitUI.Quit(cLocalElement, cSystemElement)
        End If

        iInitUI = New InitUI
        AddHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructUpdateMessage
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCUpdate As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIUpdate As Boolean = False
End Class

