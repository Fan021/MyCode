Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Public Class ParentMainForm
    Implements IParentMainUI
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorMessageManager As clsErrorMessageManager
    Private mChildrenMainForm As ChildrenMainForm
    Private cMachineManager As clsMachineManager
    Private cChangePage As clsChangePage
    Private cUserManager As clsUserManager
    Private cLanguageManager As clsLanguageManager
    Private cFormFontResize As clsFormFontResize
    Private strButtonName As String
    Private _Object As New Object
    Private cMachineStatusManager As clsMachineStatusManager
    Private cLocalFormFontResize As clsFormFontResize
    Private mMainForm As MainForm
    Private strCurrentPage As String = ""

    Public ReadOnly Property CurrentPage As String Implements UI.IParentMainUI.CurrentPage
        Get
            Return strCurrentPage
        End Get
    End Property
    Public Property ButtonName As String Implements IParentUI.ButtonName
        Get
            SyncLock _Object
                Return strButtonName
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strButtonName = value
            End SyncLock
        End Set
    End Property
    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IParentUI.UI
        Get
            SyncLock _Object
                Return Panel_Body
            End SyncLock
        End Get
    End Property
    Public ReadOnly Property Button_Clean As System.Windows.Forms.Button Implements UI.IParentMainUI.Button_Clean
        Get
            Return MainButton_Clean.MainButton
        End Get
    End Property
    Public ReadOnly Property LocalElement As Dictionary(Of String, Object) Implements IParentUI.LocalElement
        Get
            SyncLock _Object
                Return cLocalElement
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Init
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cErrorMessageManager = New clsErrorMessageManager
                cErrorMessageManager.Init(cSystemElement)
                cSystemElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
                cSystemElement.Add(enumUIName.ParentMainForm.ToString, Me)
                cLocalFormFontResize = New clsFormFontResize
                cLocalElement.Add(clsFormFontResize.Name, cLocalFormFontResize)
                cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
                cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
                cLocalElement.Add(enumUIName.ParentMainForm.ToString, Me)
                cChangePage = New clsChangePage
                cChangePage.Init(cLocalElement, cSystemElement)
                cSystemElement.Add(clsChangePage.Name, cChangePage)
                cLocalElement.Add(clsChangePage.Name, cChangePage)
                Panel_Mid.Controls.Clear()
                mChildrenMainForm = New ChildrenMainForm
                mChildrenMainForm.Init(cLocalElement, cSystemElement)
                Panel_Mid.Controls.Add(mChildrenMainForm.UI)
                cChangePage.RegisterManager(Panel_Mid, mChildrenMainForm.UI)
                InitForm()
                InitControlText()
                AddHandler cUserManager.LoginOutChanged, AddressOf LoginOutChanged
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function InitForm() As Boolean
        SyncLock _Object
            Panel_Right.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormRight)
            Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
            MainRightButton_Variant.RegisterButton(cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "SelectVariant"), "SelectVariant")
            MainRightButton_Login.RegisterButton(cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "Login"), "Login")
            MainRightButton_ShortCut.RegisterButton(cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "ShortCut"), "ShortCut")
            MainRightButton_Back.RegisterButton(cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "Back"), "Back")
            cChangePage.AddMainButton(MainRightButton_Login)
            cChangePage.AddMainButton(MainRightButton_Variant)
            cChangePage.AddMainButton(MainRightButton_ShortCut)
            cChangePage.AddMainButton(MainRightButton_Back)
            TopLevel = False
            Return True
        End SyncLock
    End Function

    Public Function InitControlText() As Boolean
        SyncLock _Object
            HmiLabel_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "HmiLabel_Variant")
            HmiLabel_Variant.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiLabel_SFC.Text = cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "HmiLabel_SFC")
            HmiLabel_SFC.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiLabel_Count.Text = cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "HmiLabel_Count")
            HmiLabel_Count.Label.TextAlign = ContentAlignment.MiddleLeft
            MainButton_Clean.MainButton.Text = cLanguageManager.GetTextLine(enumUIName.ParentMainForm.ToString, "MainButton_Clean")
            DisableMainLeftButton()
            AddHandler MainRightButton_Login.MainButton.Click, AddressOf MainButton_Click
            AddHandler MainRightButton_Variant.MainButton.Click, AddressOf MainButton_Click
            AddHandler MainRightButton_ShortCut.MainButton.Click, AddressOf MainButton_Click
            AddHandler MainRightButton_Back.MainButton.Click, AddressOf MainButton_Click
            AddHandler Label_Variant.Paint, AddressOf Label_Paint
            AddHandler Label_SFC.Paint, AddressOf Label_Paint
            AddHandler Label_Total.Paint, AddressOf Label_Paint
            AddHandler Label_Pass.Paint, AddressOf Label_Paint
            AddHandler Label_Fail.Paint, AddressOf Label_Paint
            AddHandler Label_FailRate.Paint, AddressOf Label_Paint
            AddHandler Label_TotalTime.Paint, AddressOf Label_Paint
            MainRightButton_Back.SetIndicateBackColor("Back")
            Return True
        End SyncLock
    End Function

    Public Sub AutoLogin() Implements UI.IParentMainUI.AutoLogin
        If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.AutoLogin).ToString.ToUpper <> "TRUE" Then
            If cUserManager.CurrentUserCfg.Level = enumUserLevel.Normal Then
                cChangePage.ChangePage(New ChildrenLoginForm, "Login")
                strCurrentPage = "Login"
            End If
        Else
            If cUserManager.CurrentUserCfg.Level = enumUserLevel.Normal Then
                cUserManager.AutoLogin()
            End If
        End If
    End Sub


    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Quit
        RemoveHandler cUserManager.LoginOutChanged, AddressOf LoginOutChanged
        Me.Dispose()
        Return True
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Right.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)

    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Select Case sender.name
                Case "SelectVariant"
                    cChangePage.ChangePage(New ChildrenSelectVariantForm, sender.name)
                Case "Login"
                    cChangePage.ChangePage(New ChildrenLoginForm, sender.name)
                Case "ShortCut"
                    cChangePage.ChangePage(New ChildrenShortCutForm, sender.name)
                Case "Back"
                    cChangePage.BackPage()
            End Select
            strCurrentPage = sender.name
        End SyncLock
    End Sub

    Public Sub BackUI() Implements UI.IParentMainUI.BackUI
        cChangePage.BackPage()
        strCurrentPage = "Back"
    End Sub

    Private Sub Label_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Label_Pass.Paint
        SyncLock _Object
            Select Case sender.name
                Case "Label_Pass"
                    ControlPaint.DrawBorder(e.Graphics, CType(sender, Label).ClientRectangle,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Pass), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Pass), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Pass), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Pass), 4, ButtonBorderStyle.Solid)
                Case "Label_Fail"
                    ControlPaint.DrawBorder(e.Graphics, CType(sender, Label).ClientRectangle,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Fail), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Fail), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Fail), 4, ButtonBorderStyle.Solid,
                           ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Fail), 4, ButtonBorderStyle.Solid)
                Case Else
                    ControlPaint.DrawBorder(e.Graphics, CType(sender, Label).ClientRectangle,
                              ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Total), 4, ButtonBorderStyle.Solid,
                              ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Total), 4, ButtonBorderStyle.Solid,
                              ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Total), 4, ButtonBorderStyle.Solid,
                              ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Border_Total), 4, ButtonBorderStyle.Solid)
            End Select
        End SyncLock
    End Sub

    Public Sub DisableMainLeftButton() Implements UI.IParentMainUI.DisableMainLeftButton
        MainRightButton_Login.FunctionEnable = False
        MainRightButton_Variant.FunctionEnable = False
        MainRightButton_ShortCut.FunctionEnable = False
        MainRightButton_Back.FunctionEnable = False
    End Sub

    Public Sub EnableMainLeftButton() Implements UI.IParentMainUI.EnableMainLeftButton
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
        If cMachineStatusManager.MachineStatus.bulPowerON Then cChangePage.BackPage()
        ' MainRightButton_Login.FunctionEnable = Not cMachineStatusManager.MachineStatus.bulPowerON
        MainRightButton_Login.FunctionEnable = True
        MainRightButton_Variant.FunctionEnable = Not cMachineStatusManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= 1
        MainRightButton_ShortCut.FunctionEnable = cUserManager.CurrentUserCfg.Level >= 1
        MainRightButton_Back.FunctionEnable = cUserManager.CurrentUserCfg.Level >= 1
    End Sub

    Private Sub LoginOutChanged()
        cChangePage.BackPage()
        cChangePage.ChangePage(ChildrenLoginForm, "Login")
        strCurrentPage = "Login"
    End Sub



    Private Sub Panel_UI_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Mid.Resize
        If Not IsNothing(cLocalFormFontResize) Then
            cLocalFormFontResize.newExH = Panel_Mid.Height
            If cFormFontResize.Resized Then
                cLocalFormFontResize.cons = Panel_Mid
                cLocalFormFontResize.ChangeFontSize()
                cLocalFormFontResize.cons = Panel_Right
                cLocalFormFontResize.ChangeFontSize()
                If Not cChangePage.Back Then
                    cLocalFormFontResize.cons = mChildrenMainForm.UI
                    cLocalFormFontResize.ChangeFontSize()
                End If
            End If
            cLocalFormFontResize.OldExH = Panel_Mid.Height
        End If
    End Sub



    Public Sub ChangePageToVariant() Implements UI.IParentMainUI.ChangePageToVariant
        If cUserManager.CurrentUserCfg.Level = enumUserLevel.Normal Then Return
        cChangePage.BackPage()
        cChangePage.ChangePage(ChildrenSelectVariantForm, "SelectVariant")
        strCurrentPage = "SelectVariant"
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
    End Sub

  
End Class