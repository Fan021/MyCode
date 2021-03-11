Imports System.Reflection
Imports System.Math
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class clsMainButtonManager
    Implements IDisposable
    Public Const Name As String = "MainButtonManager"
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cSystemManager As clsSystemManager
    Protected cFormFontResize As clsFormFontResize
    Protected cErrorMessageManager As clsErrorMessageManager
    Protected lListMainButtonElement As List(Of clsMainButtonManagerCfg)
    Protected iCntRightButton As Integer = 0
    Protected iCntLeftButton As Integer = 2
    Protected iCntFunctionButton As Integer = 0
    Protected cCurrentMainButtonManagerCfg As clsMainButtonManagerCfg
    Protected cCurrentMainFunctionManagerCfg As clsMainButtonManagerCfg
    Protected iParentUI As IParentUI
    Protected iChildrenUI As IChildrenUI
    Protected iHomeUI As IParentMainUI
    Protected cPanel_Body As Panel
    Protected strLastName As String = String.Empty
    Protected isHasHome As Boolean = False
    Protected cStatisticsLibManager As clsStatisticsLibManager
    Protected iMainUI As IMainUI
    Protected cChangePage As clsChangePage
    Protected cMachineManager As clsMachineManager
    Protected cUserManager As clsUserManager
    Protected cVariantManager As clsVariantManager
    Protected cMainButtonManager As clsMainButtonManager
    Protected cDeviceManager As clsDeviceManager
    Public Event PageChanged(ByVal strName As String, ByVal strCurentName As String, ByVal eMainButtonType As enumMainButtonType)


    Public Property ErrorMessageManager As clsErrorMessageManager
        Set(ByVal value As clsErrorMessageManager)
            cErrorMessageManager = value
        End Set
        Get
            Return cErrorMessageManager
        End Get
    End Property

    Public ReadOnly Property CurrentMainButtonManagerCfg As clsMainButtonManagerCfg
        Get
            Return cCurrentMainButtonManagerCfg
        End Get
    End Property

    Public ReadOnly Property ParentUI As IParentUI
        Get
            Return iParentUI
        End Get
    End Property
    Public ReadOnly Property ChildrenUI As IChildrenUI
        Get
            Return iChildrenUI
        End Get
    End Property


    Public ReadOnly Property HomeUI As IParentUI
        Get
            Return iHomeUI
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cStatisticsLibManager = CType(cSystemElement(clsStatisticsLibManager.Name), clsStatisticsLibManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            iMainUI = CType(cSystemElement(enumUIName.MainForm.ToString), IMainUI)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function RegisterManager(ByVal cPanel_Body As Panel, ByVal ParamArray cListMainButtonElement() As clsMainButtonManagerCfg) As Boolean
        Try
            Me.cPanel_Body = cPanel_Body
            Me.lListMainButtonElement = New List(Of clsMainButtonManagerCfg)
            For Each element As clsMainButtonManagerCfg In cListMainButtonElement
                Me.lListMainButtonElement.Add(element)
                If element.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                    isHasHome = True
                End If
            Next
            CreateMainButton()
            DisableMainLeftButton()
            DisableFunctionMainButton()
            DisableMainRightButton()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Sub CreateMainButton()
        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            Select Case element.MainButtonType
                Case enumMainButtonType.LeftMainButton
                    If element.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                        iHomeUI = CreateInstance(element.InterfaceType)
                        iHomeUI.ButtonName = element.Name
                        strLastName = element.Name
                        cCurrentMainButtonManagerCfg = element
                    End If
                    If IsNothing(iParentUI) And Not isHasHome Then
                        iParentUI = CreateInstance(element.InterfaceType)
                        iParentUI.ButtonName = element.Name
                        strLastName = element.Name
                        cCurrentMainButtonManagerCfg = element
                    End If
                    CreateLeftMainButton(element)
                Case enumMainButtonType.RightMainButton
                    CreateRightMainButton(element)
                    If IsNothing(iChildrenUI) And Not isHasHome Then
                        iChildrenUI = CreateInstance(element.InterfaceType)
                        iChildrenUI.ButtonName = element.Name
                        strLastName = element.Name
                        cCurrentMainButtonManagerCfg = element
                    End If
                Case enumMainButtonType.FunctionMainButton
                    CreateMainFunctionButton(element)
            End Select
        Next
        If Not IsNothing(iHomeUI) Then
            cPanel_Body.Controls.Clear()
            iHomeUI.Init(cSystemElement)
            cFormFontResize.SetControls(cFormFontResize.CurrentRate, iHomeUI)
            cPanel_Body.Controls.Add(iHomeUI.UI)
            cCurrentMainButtonManagerCfg = GetMainButtonManagerCfgFromName(strLastName)
            cCurrentMainButtonManagerCfg.UISource = iHomeUI
            SetMainIndicate(GetMainButtonManagerCfgFromName(strLastName), GetMainButtonManagerCfgFromName(strLastName).Name)
        ElseIf Not IsNothing(iParentUI) Then
            cPanel_Body.Controls.Clear()
            iParentUI.Init(cSystemElement)
            cPanel_Body.Controls.Add(iParentUI.UI)
            cCurrentMainButtonManagerCfg = GetMainButtonManagerCfgFromName(strLastName)
            cCurrentMainButtonManagerCfg.UISource = iParentUI
            SetMainIndicate(GetMainButtonManagerCfgFromName(strLastName), GetMainButtonManagerCfgFromName(strLastName).Name)
        Else
            cPanel_Body.Controls.Clear()
            iChildrenUI.Init(cLocalElement, cSystemElement)
            cPanel_Body.Controls.Add(iChildrenUI.UI)
            cCurrentMainButtonManagerCfg = GetMainButtonManagerCfgFromName(strLastName)
            cCurrentMainButtonManagerCfg.UISource = iChildrenUI
            SetMainIndicate(GetMainButtonManagerCfgFromName(strLastName), GetMainButtonManagerCfgFromName(strLastName).Name)
        End If
    End Sub

    Public Sub CreateRightMainButton(ByVal cMainButtonManagerCfg As clsMainButtonManagerCfg)
        Dim MainButton_Item As New MainRightButton
        MainButton_Item.Dock = System.Windows.Forms.DockStyle.Fill
        MainButton_Item.RegisterButton(cMainButtonManagerCfg.Text, cMainButtonManagerCfg.Name)
        MainButton_Item.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        cMainButtonManagerCfg.TableLayoutPanel.Controls.Add(MainButton_Item, 0, iCntRightButton)
        AddHandler MainButton_Item.MainButton.Click, AddressOf MainButton_Click
        cMainButtonManagerCfg.Source = MainButton_Item
        iCntRightButton = iCntRightButton + 1
    End Sub

    Public Sub CreateLeftMainButton(ByVal cMainButtonManagerCfg As clsMainButtonManagerCfg)
        Dim MainButton_Item As New MainLeftButton
        MainButton_Item.Dock = System.Windows.Forms.DockStyle.Fill
        MainButton_Item.RegisterButton(cMainButtonManagerCfg.Text, cMainButtonManagerCfg.Name)
        MainButton_Item.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        cMainButtonManagerCfg.TableLayoutPanel.Controls.Add(MainButton_Item, 0, iCntLeftButton)
        AddHandler MainButton_Item.MainButton.Click, AddressOf MainButton_Click
        cMainButtonManagerCfg.Source = MainButton_Item
        iCntLeftButton = iCntLeftButton + 1
    End Sub

    Public Sub CreateMainFunctionButton(ByVal cMainButtonManagerCfg As clsMainButtonManagerCfg)
        Dim MainButton_Item As New MainFunctionButton
        MainButton_Item.Dock = System.Windows.Forms.DockStyle.Fill
        MainButton_Item.RegisterButton(cMainButtonManagerCfg.Text, cMainButtonManagerCfg.Name)
        MainButton_Item.Margin = New System.Windows.Forms.Padding(3, 2, 3, 1)
        Dim i As Integer = Floor(iCntFunctionButton / 2)
        Dim j As Integer = iCntFunctionButton Mod 2
        cMainButtonManagerCfg.TableLayoutPanel.Controls.Add(MainButton_Item, j, i)
        AddHandler MainButton_Item.MainButton.Click, AddressOf MainFunctionButton_Click
        cMainButtonManagerCfg.Source = MainButton_Item
        iCntFunctionButton = iCntFunctionButton + 1
    End Sub


    Public Function GetMainButtonManagerCfgFromName(ByVal strName As String) As clsMainButtonManagerCfg
        Try
            For Each element As clsMainButtonManagerCfg In lListMainButtonElement
                If element.Name = strName Then
                    Return element
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Alarm)
            Return Nothing
        End Try
    End Function
    Public Sub BackHome()
        Dim cButton As New Button
        cButton.Name = enumHMI_LEFT_ITEM.Home.ToString
        MainButton_Click(cButton, New EventArgs)
    End Sub
    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim cTempMainButtonManagerCfg As clsMainButtonManagerCfg
            cTempMainButtonManagerCfg = GetMainButtonManagerCfgFromName(CType(sender, Button).Name)

            If strLastName <> cTempMainButtonManagerCfg.Name Then
                If Not IsNothing(iParentUI) Then
                    If Not iParentUI.Quit(cSystemElement) Then Return
                    iParentUI = Nothing
                End If

                If Not IsNothing(iChildrenUI) Then
                    If Not iChildrenUI.Quit(cLocalElement, cSystemElement) Then Return
                    iChildrenUI = Nothing
                End If

                If strLastName = enumHMI_LEFT_ITEM.Home.ToString Then
                    iHomeUI.BackUI()
                End If

                If cTempMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                    cCurrentMainButtonManagerCfg = cTempMainButtonManagerCfg
                    cPanel_Body.Controls.Clear()
                    cPanel_Body.Controls.Add(iHomeUI.UI)
                    strLastName = cTempMainButtonManagerCfg.Name
                    cChangePage = CType(cSystemElement(clsChangePage.Name), clsChangePage)
                    cChangePage.BackPage()
                    EnableFunctionMainButton()
                    EnableMainLeftButton()
                    RaiseEvent PageChanged(strLastName, cTempMainButtonManagerCfg.Name, enumMainButtonType.LeftMainButton)
                ElseIf cTempMainButtonManagerCfg.MainButtonType = enumMainButtonType.LeftMainButton Then
                    cCurrentMainButtonManagerCfg = cTempMainButtonManagerCfg
                    iParentUI = CreateInstance(cTempMainButtonManagerCfg.InterfaceType)
                    iParentUI.ButtonName = cTempMainButtonManagerCfg.Name
                    cPanel_Body.Controls.Clear()
                    iParentUI.Init(cSystemElement)
                    cFormFontResize.SetControls(cFormFontResize.CurrentRate, iParentUI)
                    cPanel_Body.Controls.Add(iParentUI.UI)
                    strLastName = cTempMainButtonManagerCfg.Name
                    cCurrentMainButtonManagerCfg.UISource = iParentUI
                    EnableFunctionMainButton()
                    EnableMainLeftButton()
                    RaiseEvent PageChanged(strLastName, cTempMainButtonManagerCfg.Name, enumMainButtonType.LeftMainButton)
                ElseIf cTempMainButtonManagerCfg.MainButtonType = enumMainButtonType.RightMainButton Then
                    iChildrenUI = CreateInstance(cTempMainButtonManagerCfg.InterfaceType)
                    iChildrenUI.ButtonName = cTempMainButtonManagerCfg.Name
                    cPanel_Body.Controls.Clear()
                    iChildrenUI.Init(cLocalElement, cSystemElement)
                    cFormFontResize.SetControls(cFormFontResize.CurrentRate, iChildrenUI)
                    cPanel_Body.Controls.Add(iChildrenUI.UI)
                    strLastName = cTempMainButtonManagerCfg.Name
                    cCurrentMainButtonManagerCfg = cTempMainButtonManagerCfg
                    cCurrentMainButtonManagerCfg.UISource = iChildrenUI
                    EnableMainRightButton()
                    RaiseEvent PageChanged(strLastName, cTempMainButtonManagerCfg.Name, enumMainButtonType.RightMainButton)
                End If
            End If
            SetMainIndicate(cTempMainButtonManagerCfg, CType(sender, Button).Name)
        Catch ex As Exception
            If Not IsNothing(cErrorMessageManager) Then
                cErrorMessageManager.AddHMIException(ex)
            Else
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            End If
        End Try
    End Sub


    Private Sub MainFunctionButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cCurrentMainFunctionManagerCfg = GetMainButtonManagerCfgFromName(CType(sender, Button).Name)
    End Sub

    Private Sub SetMainIndicate(ByVal cMainButtonManagerCfg As clsMainButtonManagerCfg, ByVal strName As String)
        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            If element.MainButtonType = cMainButtonManagerCfg.MainButtonType Then
                element.Source.SetIndicateBackColor(strName)
            End If
        Next
    End Sub

    Public Function CreateInstance(ByVal ccType As Type) As Object
        Try

            Dim cls As Type = Nothing, Obj As Object
            If Not cStatisticsLibManager.HasStatisticsLibCfgFromKey(ccType.ToString) Then
                cls = Assembly.LoadFrom(cSystemManager.Settings.ApplicationFullName).GetType(ccType.FullName)
                Obj = Activator.CreateInstance(cls)
                Return Obj
            Else
                Obj = cStatisticsLibManager.GetStatisticsLibCfgFromKey(ccType.ToString).CreateInstance
                Return Obj
            End If

        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Alarm)
            Return Nothing
        End Try
    End Function

    Public Sub DisableFunctionMainButton()
        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            If element.MainButtonType = enumMainButtonType.FunctionMainButton Then
                If element.Name <> enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString And element.Name <> enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString Then
                    CType(element.Source, MainFunctionButton).FunctionEnable = False
                End If
            End If
        Next
    End Sub

    Public Sub EnableFunctionMainButton()

        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            If element.MainButtonType = enumMainButtonType.FunctionMainButton Then
                If element.Name = enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString Then
                    CType(element.Source, MainFunctionButton).FunctionEnable = True
                Else
                    If element.Name = enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString Then
                        CType(element.Source, MainFunctionButton).FunctionEnable = ((cMachineManager.MachineCellManager.MachineCellCfg.HasManualSelectVariant() And cVariantManager.CurrentVariantCfg.Variant <> "") Or (Not cMachineManager.MachineCellManager.MachineCellCfg.HasManualSelectVariant())) And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator And CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString
                    ElseIf element.Name = enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString Then
                        CType(element.Source, MainFunctionButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator And CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString And Not cMachineManager.MachineStatus.bulTeachMode And Not cMachineManager.MachineStatus.bulDebugMode
                    ElseIf element.Name = enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString Then
                        CType(element.Source, MainFunctionButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Normal And CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString And Not cMachineManager.MachineStatus.bulPowerON
                    Else
                        CType(element.Source, MainFunctionButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator And CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString
                    End If
                End If
            End If
        Next
        iMainUI.EnableMainLeftButton()
    End Sub

    Public Sub EnableMainLeftButton(Optional ByVal strClickName As String = "")
        If Not IsNothing(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString)) Then
            If strClickName = "bulManual" Or strClickName = "bulTeachMode" Then
                If cSystemElement.ContainsKey(clsDeviceManager.Name) Then
                    cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
                    Dim cHMIPLC As clsHMIPLC = cDeviceManager.GetPLCDevice
                    If Not IsNothing(cHMIPLC) Then
                        Dim cPLCStepStatus As StructPLCStepStatus = cHMIPLC.ReadAny(HMI_PLC_Interface.PLC_StepStatus + "[1]", GetType(StructPLCStepStatus))
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = ((cMachineManager.MachineStatus.bulPowerON And cMachineManager.MachineStatus.bulManual)) And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer And (cPLCStepStatus.intStepID = 0 Or (cPLCStepStatus.intStepID <> 0 And (cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode)))
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString).Source, MainLeftButton).FunctionEnable = True
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.IO.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Program.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Statistics.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                        CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.System.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    End If
                End If
            Else
                Dim cHMIPLC As clsHMIPLC = Nothing
                If Not IsNothing(cDeviceManager) Then
                    cHMIPLC = cDeviceManager.GetPLCDevice()
                End If
                If Not IsNothing(cHMIPLC) Then
                    Dim cPLCStepStatus As StructPLCStepStatus = cHMIPLC.ReadAny(HMI_PLC_Interface.PLC_StepStatus + "[1]", GetType(StructPLCStepStatus))
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = ((cMachineManager.MachineStatus.bulPowerON And cMachineManager.MachineStatus.bulManual)) And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer And (cPLCStepStatus.intStepID = 0 Or (cPLCStepStatus.intStepID <> 0 And (cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode)))
                    ' CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = True
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString).Source, MainLeftButton).FunctionEnable = True
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.IO.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Program.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Statistics.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.System.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                Else
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = ((cMachineManager.MachineStatus.bulPowerON And cMachineManager.MachineStatus.bulManual)) And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer
                    ' CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = True
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString).Source, MainLeftButton).FunctionEnable = True
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.IO.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Program.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Statistics.ToString).Source, MainLeftButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                    CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.System.ToString).Source, MainLeftButton).FunctionEnable = Not cMachineManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator

                End If

            End If
        End If

    End Sub


    Public Sub EnableMainRightButton()
        cMainButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            If element.MainButtonType = enumMainButtonType.RightMainButton Then
                If element.Name = "System" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Administrator
                ElseIf element.Name = "System" Or element.Name = "DeviceManager" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator
                ElseIf element.Name = "User" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                ElseIf cMainButtonManager.CurrentMainButtonManagerCfg.Name = "System" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer
                Else
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                End If
            End If
        Next


    End Sub

    Public Sub DisableMainRightButton()
        cMainButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
        For Each element As clsMainButtonManagerCfg In lListMainButtonElement
            If element.MainButtonType = enumMainButtonType.RightMainButton Then
                If element.Name = "System" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Administrator
                ElseIf element.Name = "DeviceManager" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator
                ElseIf element.Name = "User" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                ElseIf cMainButtonManager.CurrentMainButtonManagerCfg.Name = "System" Then
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer
                Else
                    CType(element.Source, MainRightButton).FunctionEnable = cUserManager.CurrentUserCfg.Level >= enumUserLevel.Operator
                End If
            End If
        Next

    End Sub

    Public Sub DisableMainLeftButton()
        If Not IsNothing(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString)) Then
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.IO.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Program.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Statistics.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.System.ToString).Source, MainLeftButton).FunctionEnable = False
        End If
    End Sub

    Public Sub DisableMainLeftButtonEx()
        If Not IsNothing(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString)) Then
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Devices.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Home.ToString).Source, MainLeftButton).FunctionEnable = True
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.IO.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Program.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.Statistics.ToString).Source, MainLeftButton).FunctionEnable = False
            CType(GetMainButtonManagerCfgFromName(enumHMI_LEFT_ITEM.System.ToString).Source, MainLeftButton).FunctionEnable = False
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                For Each element As clsMainButtonManagerCfg In lListMainButtonElement
                    Select Case element.MainButtonType
                        Case enumMainButtonType.LeftMainButton
                            RemoveHandler CType(element.Source, MainLeftButton).MainButton.Click, AddressOf MainButton_Click
                        Case enumMainButtonType.RightMainButton
                            RemoveHandler CType(element.Source, MainRightButton).MainButton.Click, AddressOf MainButton_Click
                        Case enumMainButtonType.FunctionMainButton
                            RemoveHandler CType(element.Source, MainFunctionButton).MainButton.Click, AddressOf MainFunctionButton_Click
                    End Select
                Next
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next
        MyBase.Finalize()
    End Sub
#End Region

End Class


Public Class clsMainButtonManagerCfg
    Protected cTableLayoutPanel As TableLayoutPanel
    Protected strName As String
    Protected strText As String
    Protected cInterfaceType As Type
    Protected eMainButtonType As enumMainButtonType
    Protected oSource As Object
    Protected oUISource As Object

    Public Property TableLayoutPanel As TableLayoutPanel
        Set(ByVal value As TableLayoutPanel)
            cTableLayoutPanel = value
        End Set
        Get
            Return cTableLayoutPanel
        End Get
    End Property

    Public Property Name As String
        Set(ByVal value As String)
            strName = value
        End Set
        Get
            Return strName
        End Get
    End Property

    Public Property Text As String
        Set(ByVal value As String)
            strText = value
        End Set
        Get
            Return strText
        End Get
    End Property

    Public Property InterfaceType As Type
        Set(ByVal value As Type)
            cInterfaceType = value
        End Set
        Get
            Return cInterfaceType
        End Get
    End Property

    Public Property MainButtonType As enumMainButtonType
        Set(ByVal value As enumMainButtonType)
            eMainButtonType = value
        End Set
        Get
            Return eMainButtonType
        End Get
    End Property


    Public Property Source As Object
        Set(ByVal value As Object)
            oSource = value
        End Set
        Get
            Return oSource
        End Get
    End Property

    Public Property UISource As Object
        Set(ByVal value As Object)
            oUISource = value
        End Set
        Get
            Return oUISource
        End Get
    End Property

    Sub New(ByVal cTableLayoutPanel As TableLayoutPanel, ByVal strName As String, ByVal strText As String, ByVal cInterfaceType As Type, Optional ByVal eMainButtonType As enumMainButtonType = enumMainButtonType.RightMainButton)
        Me.cTableLayoutPanel = cTableLayoutPanel
        Me.strName = strName
        Me.strText = strText
        Me.cInterfaceType = cInterfaceType
        Me.eMainButtonType = eMainButtonType
    End Sub

End Class

Public Enum enumMainButtonType
    LeftMainButton = 0
    RightMainButton
    FunctionMainButton
End Enum
