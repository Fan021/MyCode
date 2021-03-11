Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class ParentSystemForm
    Implements IParentUI
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cFormFontResize As clsFormFontResize
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMainButtonManager As clsMainButtonManager
    Private mMainForm As MainForm
    Private strButtonName As String
    Private cStatisticsLibManager As clsStatisticsLibManager
    Private cDeviceManager As clsDeviceManager
    Private cDeviceLibManager As clsDeviceLibManager
    Private cLanguageManager As clsLanguageManager
    Private cLocalFormFontResize As clsFormFontResize
    Private cHMIPLC As clsHMIPLC
    Private cIOManager As clsIOManager
    Public Property ButtonName As String Implements IParentUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IParentUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public ReadOnly Property LocalElement As Dictionary(Of String, Object) Implements IParentUI.LocalElement
        Get
            Return cLocalElement
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cLocalFormFontResize = New clsFormFontResize
            cStatisticsLibManager = CType(cSystemElement(clsStatisticsLibManager.Name), clsStatisticsLibManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cDeviceLibManager = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cErrorMessageManager = New clsErrorMessageManager
            cErrorMessageManager.Init(cSystemElement)
            cErrorMessageManager.RegisterManager(TableLayoutPanel_Body)
            cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
            cLocalElement.Add(enumUIName.ParentSystemForm.ToString, Me)
            cMainButtonManager = New clsMainButtonManager
            cIOManager = New clsIOManager
            cIOManager.Init(cSystemElement)
            cMainButtonManager.Init(cLocalElement, cSystemElement)
            cMainButtonManager.ErrorMessageManager = cErrorMessageManager
            CreateButton(strButtonName)

            InitForm()
            InitControlText()

            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Right.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormRight)
        Panel_Mid.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        Return True
    End Function

    Public Function CreateButton(ByVal strParentName As String) As Boolean
        Try
            Select Case CType([Enum].Parse(GetType(enumHMI_LEFT_ITEM), strParentName), enumHMI_LEFT_ITEM)

                Case enumHMI_LEFT_ITEM.IO
                    cMainButtonManager.RegisterManager(Panel_Mid,
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "IO", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "IO"), GetType(ChildrenIOForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "Cylinder", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "Cylinder"), GetType(ChildrenCylinderForm))
                                          )
                Case enumHMI_LEFT_ITEM.Statistics
                    Dim cListMainButtonElement As New List(Of clsMainButtonManagerCfg)
                    cListMainButtonElement.Add(New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "Alarm", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "Alarm Logging"), GetType(ChildrenAlarmForm)))
                    cListMainButtonElement.Add(New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "Machine", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "Machine Logging"), GetType(ChildrenMachineForm)))
                    cListMainButtonElement.Add(New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "Production", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "Production Logging"), GetType(ChildrenProductionForm)))
                    For Each element As String In cStatisticsLibManager.GetListDllKey
                        Dim cStatisticsLibCfg As clsStatisticsLibCfg = cStatisticsLibManager.GetStatisticsLibCfgFromKey(element)
                        If cDeviceManager.HasDeviceFromType(cStatisticsLibCfg.DeviceType) Then
                            cListMainButtonElement.Add(New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, cStatisticsLibCfg.UIName, cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, cStatisticsLibCfg.UIName), cStatisticsLibCfg.DeviceType))
                        End If
                    Next
                    cMainButtonManager.RegisterManager(Panel_Mid, cListMainButtonElement.ToArray)

                Case enumHMI_LEFT_ITEM.System
                    cMainButtonManager.RegisterManager(Panel_Mid,
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "User", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "User"), GetType(ChildrenUserForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "Variant", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "Variant"), GetType(ChildrenVariantForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "TextList", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "TextList"), GetType(ChildrenTextListForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "PictureList", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "PictureList"), GetType(ChildrenPictureListForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "ErrorCode", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "ErrorCode"), GetType(ChildrenErrorCodeListForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "PLCMessage", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "PLCMessage"), GetType(ChildrenPlcMessageListForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "StationError", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "StationError"), GetType(ChildrenStationErrorForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "CarrierError", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "CarrierError"), GetType(ChildrenCarrierErrorForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "DeviceManager", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "DeviceManager"), GetType(ChildrenDevicesManagerForm)),
                                          New clsMainButtonManagerCfg(TableLayoutPanel_Right_Item, "System", cLanguageManager.GetTextLine(enumUIName.ParentSystemForm.ToString, "System"), GetType(ChildrenSystemParameterForm))
                                          )
            End Select
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
            Return False
        End Try
    End Function


    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Quit
        Try
            If Not IsNothing(cMainButtonManager.ChildrenUI) Then If Not cMainButtonManager.ChildrenUI.Quit(cLocalElement, cSystemElement) Then Return False
            cMainButtonManager.Dispose()
            cErrorMessageManager.Dispose()
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Right.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)

    End Sub

    Private Sub Panel_Mid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Mid.Resize
        If Not IsNothing(cLocalFormFontResize) Then
            cLocalFormFontResize.WinFromH = mMainForm.Panel_Body.Height
            cLocalFormFontResize.newExH = Panel_Mid.Height
            If cFormFontResize.Resized And cLocalFormFontResize.Resized Then
                cLocalFormFontResize.cons = Panel_Mid
                cLocalFormFontResize.ChangeFontSize()
                cLocalFormFontResize.cons = Panel_Right
                cLocalFormFontResize.ChangeFontSize()
            End If
            cLocalFormFontResize.OldExH = Panel_Mid.Height
        End If
    End Sub
End Class