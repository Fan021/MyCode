Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

Public Class clsChangePage
    Implements IDisposable
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cSystemManager As clsSystemManager
    Protected cFormFontResize As clsFormFontResize
    Protected cLocalFormFontResize As clsFormFontResize
    Protected cErrorMessageManager As clsErrorMessageManager
    Protected cPanel_Body As Panel
    Protected cPanel_Source As Panel
    Protected cPanel_Target As Panel
    Protected _Object As New Object
    Protected mMainForm As IMainUI
    Protected lListMainButton As New List(Of MainRightButton)
    Public Const Name As String = "ChangePage"
    Public Event BackPageChanged(ByVal strUIName As String)
    Protected strUIName As String = String.Empty
    Protected isBack As Boolean = True
    Private iChildrenUI As IChildrenUI
    Protected isBacking As Boolean = False
    Public ReadOnly Property Back As Boolean
        Get
            Return isBack
        End Get
    End Property
    Public Property ErrorMessageManager As clsErrorMessageManager
        Set(ByVal value As clsErrorMessageManager)
            SyncLock _Object
                cErrorMessageManager = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cErrorMessageManager
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                Me.cLocalElement = cLocalElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                If cLocalElement.ContainsKey(clsFormFontResize.Name) Then
                    cLocalFormFontResize = CType(cLocalElement(clsFormFontResize.Name), clsFormFontResize)
                End If
                cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cPanel_Body As Panel, ByVal cPanel_Source As Panel) As Boolean
        SyncLock _Object
            Try
                Me.cPanel_Body = cPanel_Body
                Me.cPanel_Source = cPanel_Source
                Return (True)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddMainButton(ByVal cMainButton As MainRightButton) As Boolean
        SyncLock _Object
            Try
                lListMainButton.Add(cMainButton)
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(ex)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangePage(ByVal cPanel_Target As Panel, Optional ByVal strUIName As String = "") As Boolean
        SyncLock _Object
            Try
                cPanel_Body.Controls.Clear()
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, cPanel_Target)
                If Not IsNothing(cLocalFormFontResize) Then
                    If cLocalFormFontResize.OriginalCurrentRate <> 1 And cLocalFormFontResize.OriginalCurrentRate > 0 Then
                        cLocalFormFontResize.SetControls(cLocalFormFontResize.OriginalCurrentRate, cPanel_Target)
                    End If
                End If
                cPanel_Body.Controls.Add(cPanel_Target)
                Me.strUIName = strUIName
                isBack = False
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(ex)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangePage(ByVal cPanel_Target As IChildrenUI, Optional ByVal strUIName As String = "") As Boolean
        SyncLock _Object
            Try
                If Me.strUIName = strUIName Then
                    Return True
                End If
                If Not IsNothing(iChildrenUI) Then
                    iChildrenUI.Quit(cLocalElement, cSystemElement)
                End If
                iChildrenUI = cPanel_Target
                iChildrenUI.Init(cLocalElement, cSystemElement)
                cPanel_Body.Controls.Clear()
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, iChildrenUI.UI)
                If Not IsNothing(cLocalFormFontResize) Then
                    If cLocalFormFontResize.OriginalCurrentRate <> 1 And cLocalFormFontResize.OriginalCurrentRate > 0 Then
                        cLocalFormFontResize.SetControls(cLocalFormFontResize.OriginalCurrentRate, iChildrenUI.UI)
                    End If
                End If
                For Each element As MainRightButton In lListMainButton
                    element.SetIndicateBackColor(strUIName)
                Next
                cPanel_Body.Controls.Add(iChildrenUI.UI)
                Me.strUIName = strUIName
                isBack = False
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(ex)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function BackPage() As Boolean
        SyncLock _Object
            Try
                If isBack Then Return True
                If Not IsNothing(iChildrenUI) Then
                    iChildrenUI.Quit(cLocalElement, cSystemElement)
                    iChildrenUI = Nothing
                End If

                mMainForm.InvokeAction(Sub()
                                           cPanel_Body.Controls.Clear()
                                           cPanel_Body.Controls.Add(cPanel_Source)
                                           For Each element As MainRightButton In lListMainButton
                                               element.SetIndicateBackColor("Back")
                                           Next
                                       End Sub)
                isBack = True

                RaiseEvent BackPageChanged(strUIName)
                Me.strUIName = "Back"
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(ex)
                Return False
            End Try
        End SyncLock
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
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
