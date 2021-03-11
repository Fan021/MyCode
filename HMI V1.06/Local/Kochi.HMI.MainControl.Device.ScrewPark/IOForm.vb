Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing

Public Class IOForm
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Public Const FormName As String = "ProcessControl"
    Protected cLanguageManager As clsLanguageManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    Private _Object As New Object
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private cVariantManager As clsVariantManager
    Private bEdit As Boolean = False
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cObjectSource As clsScrewPark
    Public Property [ReadOnly] As Boolean
        Set(ByVal value As Boolean)
            bReadOnly = value
        End Set
        Get
            Return bReadOnly
        End Get
    End Property

    Public Property FontSize As Integer
        Set(ByVal value As Integer)
            iFontSize = value
        End Set
        Get
            Return iFontSize
        End Get
    End Property

    Public Property IO As clsScrewPark
        Set(ByVal value As clsScrewPark)
            cObjectSource = value
        End Set
        Get
            Return cObjectSource
        End Get
    End Property
    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.cLocalElement = cLocalElement
        Me.cSystemElement = cSystemElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cObjectSource.Name)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        GetPageMode()
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
              Return True
    End Function
    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
        End SyncLock
    End Sub

    Public Function InitControlText() As Boolean
        HmiLabel_Count.Label.Text = cLanguageManager.GetUserTextLine("ScrewPark", "HmiLabel_Count")
        HmiButton_Reset.Button.Text = cLanguageManager.GetUserTextLine("ScrewPark", "HmiButton_Reset")
        HmiLabel_Count.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButton_Reset.Button.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Count.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        If ePageMode <> enumPageMode.Edit Then
            HmiButton_Reset.Enabled = False
        End If
        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewPart.ini", "Configure", "CurrentCount")
        HmiTextBox_Count.TextBox.Text = mTempValue
        If mTempValue = "" Then mTempValue = "0"
        HmiTextBox_Count.TextBoxReadOnly = True
        AddHandler HmiButton_Reset.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiTextBox_Count.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each element As RowStyle In HmiTableLayoutPanel_Body_Top_Right.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Count.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Private Sub HmiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try    
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewPart.ini", "Configure", "CurrentCount", "0")
            HmiTextBox_Count.TextBox.Text = "0"
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub



   
    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Return True
    End Function

    Private Sub RefreshUI()
        Return
    End Sub


    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
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
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

End Class

