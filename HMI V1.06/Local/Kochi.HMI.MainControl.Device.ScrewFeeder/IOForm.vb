Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System.Reflection

Public Class IOForm
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Public Const FormName As String = "IOForm"
    Public Const GWL_STYLE As Integer = -16
    Public Const WS_DISABLED As Integer = &H8000000
    Public TempStructScrewFeeder As New StructScrewFeederDR
    Public OldStructScrewFeeder As New StructScrewFeederDR
    Protected cLanguageManager As clsLanguageManager
    Protected cScrewFeeder As clsScrewFeeder
    Protected bReadOnly As Boolean = False
    Protected lListInput As New Dictionary(Of String, clsInput)
    Protected lListOutput As New Dictionary(Of String, clsOutput)
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private iFontSize As Integer = 10
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    <DllImport("user32.dll ")>
    Protected Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal wndproc As Integer) As Integer

    End Function
    <DllImport("user32.dll ")>
    Protected Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer

    End Function

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
    Public Property ScrewFeeder As clsScrewFeeder
        Set(ByVal value As clsScrewFeeder)
            cScrewFeeder = value
        End Set
        Get
            Return cScrewFeeder
        End Get
    End Property

    Public Sub SetControlEnabled(ByVal c As Control, ByVal enabled As Boolean)
        SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE))
    End Sub

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cScrewFeeder.Name)

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
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer And Not [ReadOnly] Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Public Function InitControlText() As Boolean
        lListInput.Clear()
        lListInput.Add("InputIO1", New clsInput("PLC_ScrewInSeparator"))
        lListInput.Add("InputIO2", New clsInput("PLC_ScrewInPipe"))
        lListInput.Add("InputIO3", New clsInput("PLC_ScrewInWPosition"))
        SetControl(GroupBox_Top_input)

        lListOutput.Clear()
        lListOutput.Add("OutputIO1", New clsOutput("HMI_ScrewInSeparator", False))
        lListOutput.Add("OutputIO2", New clsOutput("HMI_ScrewInPipe", False))
        lListOutput.Add("OutputIO3", New clsOutput("HMI_ScrewInWPosition", False))

        SetControl(GroupBox_Mid_Output)

        GroupBox_Top_input.Font = New System.Drawing.Font("Calibri", iFontSize)
        GroupBox_Top_input.Text = cLanguageManager.GetUserTextLine("TapeFeeder", "GroupBox_Top_input")

        GroupBox_Mid_Output.Font = New System.Drawing.Font("Calibri", iFontSize)
        GroupBox_Mid_Output.Text = cLanguageManager.GetUserTextLine("TapeFeeder", "GroupBox_Mid_Output")

        Return True
    End Function


    Public Sub SetControl(ByVal cons As Control)

        For Each con As Control In cons.Controls

            If TypeOf con Is InputIO Then
                CType(con, InputIO).RegisterButton(cLanguageManager.GetUserTextLine("ScrewFeeder", lListInput(con.Name).AdsName), con.Name)
                CType(con, InputIO).MainButton.Font = New System.Drawing.Font("Calibri", iFontSize)
                SetControlEnabled(con, False)
                CType(con, InputIO).MainButton.ForeColor = Drawing.Color.Black
                CType(con, InputIO).SetIndicateBackColor(False)
                lListInput(con.Name).ObjectSource = con

            ElseIf TypeOf con Is OutputIO Then
                CType(con, OutputIO).RegisterButton(cLanguageManager.GetUserTextLine("ScrewFeeder", lListOutput(con.Name).AdsName), con.Name)
                CType(con, OutputIO).MainButton.Font = New System.Drawing.Font("Calibri", iFontSize)
                If ePageMode = enumPageMode.Debug Then
                    SetControlEnabled(con, False)
                    CType(con, OutputIO).MainButton.ForeColor = Drawing.Color.Black
                End If
                lListOutput(con.Name).ObjectSource = con
                If lListOutput(con.Name).Tap Then
                    AddHandler CType(con, OutputIO).MainButton.MouseUp, AddressOf Button_MouseUp
                    AddHandler CType(con, OutputIO).MainButton.MouseDown, AddressOf Button_MouseDown
                Else
                    AddHandler CType(con, OutputIO).MainButton.Click, AddressOf Button_Click
                End If

            End If
            If con.Controls.Count > 0 Then
                SetControl(con)
            End If
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter

        Return True
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try

                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(IOForm.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException("PLC is Nothing, Please Add first", enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException("Device:" + cHMIPLC.Name + " Status:" + cHMIPLC.DeviceState.ToString, enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        TempStructScrewFeeder = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructScrewFeederDR))
                        iStep = iStep + 1

                    Case 3
                        iStep = iStep + 1

                    Case 4
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewFeederDR), New StructScrewFeederDR)
                        iStep = iStep + 1

                    Case 5
                        Dim TempStructScrewFeeder As StructScrewFeederDR = cHMIPLC.GetValue(lListInitParameter(0))
                        For Each element As clsInput In lListInput.Values
                            If Readfield(TempStructScrewFeeder, element.AdsName) <> Readfield(OldStructScrewFeeder, element.AdsName) Then
                                element.ObjectSource.SetIndicateBackColor(Readfield(TempStructScrewFeeder, element.AdsName))
                                Writefield(OldStructScrewFeeder, element.AdsName, Readfield(TempStructScrewFeeder, element.AdsName))
                            End If
                        Next
                        For Each element As clsOutput In lListOutput.Values
                            If Readfield(TempStructScrewFeeder, element.AdsName) <> Readfield(OldStructScrewFeeder, element.AdsName) Then
                                element.ObjectSource.SetIndicateBackColor(Readfield(TempStructScrewFeeder, element.AdsName))
                                Writefield(OldStructScrewFeeder, element.AdsName, Readfield(TempStructScrewFeeder, element.AdsName))
                            End If
                        Next

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, IOForm.FormName))
            End Try


        End While
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, GetType(Boolean))
        Dim dNewValue As Boolean = Not dOldValue
        cHMIPLC.WriteAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, dNewValue)
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, GetType(Boolean))
        Dim dNewValue As Boolean = False
        cHMIPLC.WriteAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, dNewValue)
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, GetType(Boolean))
        Dim dNewValue As Boolean = True
        cHMIPLC.WriteAny(lListInitParameter(0) + "." + lListOutput(sender.name).AdsName, dNewValue)
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count >= 1 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Private Function Readfield(ByVal o As Object, ByVal field As String) As Object
        Dim res As Object = Nothing
        Dim currName As String = ""
        Dim strArr As String() = o.ToString.Split(CChar(";"))
        Dim str As String = ""

        Dim fields As New List(Of String)
        Try


            If o IsNot Nothing Then
                For Each str In strArr
                    If str.Contains(field) Then
                        strArr = str.Split(CChar("="))
                        str = strArr(1)
                        Return str
                    End If
                Next

                fields.AddRange(field.Split(CChar(".")))
            End If

            If fields.Count = 1 Then

                If o.ToString.Contains(field) Then

                End If

                Return o.GetType.GetField(fields(0)).GetValue(o)
            Else
                'Dim currObj As Object = o.GetType.GetField(fields(0)).GetValue(o)
                fields.RemoveAt(0)
                Return Readfield(o, field)
            End If

            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Writefield(ByVal o As Object, ByVal field As String, ByVal value As Object)
        Dim res As Object = Nothing
        Dim currName As String = ""

        Dim fields As New List(Of String)

        If o IsNot Nothing Then

            fields.AddRange(field.Split(CChar(".")))

            'Get field type
            Dim fi As FieldInfo = o.GetType.GetField(fields(0))

            'convert data to target type
            Dim p1 = System.Convert.ChangeType(value, fi.FieldType)

            'write value to filed
            ' Dim v As ValueType = CType(o, ValueType)

            fi.SetValue(o, p1)

        Else
            Dim f As FieldInfo = o.GetType.GetField(fields(0))

            Dim curr As ValueType = CType(f.GetValue(o), ValueType)

            fields.RemoveAt(0)

            Writefield(o, field, value)

            'set o back
            '   Dim obj As ValueType = CType(o, ValueType)
            f.SetValue(o, curr)

        End If

    End Sub


End Class

Public Class clsInput
    Public AdsName As String = ""
    Public ObjectSource As InputIO
    Public Sub New(ByVal strAdsName As String)
        Me.AdsName = strAdsName
    End Sub
End Class

Public Class clsOutput
    Public AdsName As String = ""
    Public ObjectSource As OutputIO
    Public Tap As Boolean = False
    Public Sub New(ByVal strAdsName As String, ByVal bTap As Boolean)
        Me.AdsName = strAdsName
        Me.Tap = bTap
    End Sub
End Class