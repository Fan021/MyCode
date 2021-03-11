Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("CarrierManager", "CarrierManager")>
Public Class clsCarrierManager
    Inherits clsHMICarrierManager
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Public cCarrierDataManager As clsCarrierDataManager
    Private strOperationId As String = ""
    Private strIP As String = ""
    Private strUserName As String = ""
    Private strPassWord As String = ""
    Private strEnable As String = ""
    Public Event ValueChanged()
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("CarrierManager", "1"), enumExceptionType.Crash)
            Return False
        End If
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))

        strOperationId = lListInitParameter(1)
        strIP = lListInitParameter(2)
        strUserName = lListInitParameter(3)
        strPassWord = lListInitParameter(4)
        strEnable = lListInitParameter(5)

        cCarrierDataManager = New clsCarrierDataManager
        cCarrierDataManager.strIP = strIP
        cCarrierDataManager.strUserName = strUserName
        cCarrierDataManager.strPassWord = strPassWord
        cCarrierDataManager.strEnable = strEnable
        cCarrierDataManager.Init(cSystemElement)
        Return True
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
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
        If Not IsNothing(iControlUI) Then
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
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

    Public Overrides Function ResetCarrierID(ByVal strCarrierID As String, ByRef strResult As String) As Boolean
        Try
            If strEnable <> "TRUE" Then
                Return True
            End If
            Dim strStation As String = ""
            If cCarrierDataManager.HasCarrierID(strCarrierID, strStation) Then
                cCarrierDataManager.UpdateData(strCarrierID, "")
            Else
                cCarrierDataManager.InSertData(strCarrierID, "")
            End If
            RaiseEvent ValueChanged()
            Return True
        Catch ex As Exception
            strResult = ex.InnerException.Message
            Return False
        End Try
    End Function

    Public Overrides Function CheckRepeat(ByVal strCarrierID As String, ByRef strResult As String) As Integer
        Try
            If strEnable <> "TRUE" Then
                Return 0
            End If
            Dim strStation As String = ""
            If cCarrierDataManager.HasCarrierID(strCarrierID, strStation) Then
                If strStation = "Abort" Then
                    strResult = cLanguageManager.GetUserTextLine("CarrierManager", "8", strCarrierID)
                    Return -1
                ElseIf strStation <> strOperationId Then
                    Return 0
                Else
                    strResult = cLanguageManager.GetUserTextLine("CarrierManager", "9", strCarrierID)
                    Return -2
                End If

            End If
            RaiseEvent ValueChanged()
            Return 0
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return -4
        End Try
    End Function

    Public Overrides Function UpdateCarrier(ByVal strCarrierID As String, ByRef strResult As String) As Boolean
        Try
            If strEnable <> "TRUE" Then
                Return True
            End If
            If cCarrierDataManager.HasCarrierID(strCarrierID) Then
                cCarrierDataManager.UpdateData(strCarrierID, strOperationId)
            Else
                cCarrierDataManager.InSertData(strCarrierID, strOperationId)
            End If
            RaiseEvent ValueChanged()
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False
        End Try
    End Function

    Public Overrides Function UpdateCarrier(ByVal strCarrierID As String, ByVal strStation As String, ByRef strResult As String) As Boolean
        Try
            If strEnable <> "TRUE" Then
                Return True
            End If
            If cCarrierDataManager.HasCarrierID(strCarrierID) Then
                cCarrierDataManager.UpdateData(strCarrierID, strStation)
            Else
                cCarrierDataManager.InSertData(strCarrierID, strStation)
            End If
            RaiseEvent ValueChanged()
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False
        End Try
    End Function

    Public Overrides Function GetCarrierStation(ByVal strCarrierID As String) As String
        Try
            If strEnable <> "TRUE" Then
                Return ""
            End If
            Dim strStation As String = ""
            If cCarrierDataManager.HasCarrierID(strCarrierID, strStation) Then
                Return strStation
            Else
                Return strStation
            End If
            RaiseEvent ValueChanged()
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructCarrierManager
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> Public strKostalNr As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=51)> Public strSerialNr As String = ""
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public intCarrierID As Byte = 0
    Public bulHMIWrite As StructGapFillerButton
    Public bulHMIRead As StructGapFillerButton
    Public bulHMIReset As StructGapFillerButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public intHMICarrierID As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCarrierPresent As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCarrierError As Boolean
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructGapFillerButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoAction As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean
End Structure
