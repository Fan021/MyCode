Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenMainForm
    Implements IChildrenMainUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMainTipsManager As clsMainTipsManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private mParentMainForm As ParentMainForm
    Private strButtonName As String
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager

    Public Property ButtonName As String Implements IChildrenMainUI.ButtonName
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
    Public ReadOnly Property UI As Panel Implements IChildrenMainUI.UI
        Get
            SyncLock _Object
                Return Panel_Body
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property TabControl_Station As System.Windows.Forms.TabControl Implements IChildrenMainUI.TabControl_Station
        Get
            SyncLock _Object
                Return cTabControl_Station
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property UI_Station As System.Windows.Forms.Panel Implements UI.IChildrenMainUI.UI_Station
        Get
            Return Panel_UI
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenMainUI.Init
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                Me.cLocalElement = cLocalElement
                mParentMainForm = CType(cSystemElement(enumUIName.ParentMainForm.ToString), ParentMainForm)
                cMainTipsManager = New clsMainTipsManager
                cMainTipsManager.Init(cSystemElement)
                cMainTipsManager.RegisterManager(mParentMainForm.TableLayoutPanel_Body, cTabControl_Station)
                cSystemElement.Add(clsMainTipsManager.Name, cMainTipsManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
                cErrorMessageManager.RegisterManager(mParentMainForm.TableLayoutPanel_Body)

                cMachineStatusManager = New clsMachineStatusManager
                cMachineStatusManager.Init(cSystemElement)
                cMachineStatusManager.RegisterManager(cTabControl_Station, mParentMainForm.Label_Variant, mParentMainForm.Label_SFC, mParentMainForm.Label_Total, mParentMainForm.Label_Pass, mParentMainForm.Label_Fail, mParentMainForm.Label_FailRate, mParentMainForm.Label_TotalTime)
                cSystemElement.Add(clsMachineStatusManager.Name, cMachineStatusManager)
                InitForm()
                InitControlText()
                Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
                Me.UpdateStyles()
                cSystemElement.Add(enumUIName.ChildrenMainForm.ToString, Me)
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function InitForm() As Boolean
        SyncLock _Object
            Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
            TopLevel = False
            Return True
        End SyncLock
    End Function

    Public Function InitControlText() As Boolean
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenMainUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenMainForm.ToString)
        Me.Dispose()
        Return True
    End Function

    Public Sub DisableMainLeftButton() Implements UI.IChildrenMainUI.DisableMainLeftButton

    End Sub

    Public Sub EnableMainLeftButton() Implements UI.IChildrenMainUI.EnableMainLeftButton

    End Sub


End Class