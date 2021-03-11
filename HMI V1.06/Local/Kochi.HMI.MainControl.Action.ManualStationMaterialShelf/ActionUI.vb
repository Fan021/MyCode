Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing

Public Class ActionUI
    Implements IActionUI

    Protected lListInitParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected iParentProgramUI As IParentProgramUI
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cHMIPKP As clsHMIPKP
    Private cMachineStationCfg As clsMachineStationCfg
    Private cTextManager As clsTextManager
    Private cPictureManager As clsPictureManager
    Private cSystemManager As clsSystemManager
    Public ReadOnly Property UI As Panel Implements IActionUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        iParentProgramUI = CType(cLocalElement(enumUIName.ParentProgramForm.ToString), IParentProgramUI)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cTextManager = CType(cSystemElement(clsTextManager.Name), clsTextManager)
        cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        InitForm()
        InitControlText()
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Quit
        ' cChangePage.BackPage()
        Me.Dispose()
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        HmiComboBox_Position.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)

        HmiComboBox_Position.ComboBox.Items.Clear()

        For i = 0 To 100
            HmiComboBox_Position.ComboBox.Items.Add(i.ToString)
        Next

        If lListParameter.Count >= 1 Then
            For iCnt = 0 To HmiComboBox_Position.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_Position.ComboBox.SelectedIndex = iCnt
                End If
            Next
        End If

        If lListParameter.Count >= 2 Then
            HmiTextBox_Description.TextBox.Text = lListParameter(1)
        End If
        If lListParameter.Count >= 3 Then
            HmiTextBox_Picture.TextBox.Text = lListParameter(2)
        End If

        Return True
    End Function



    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 2 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "6"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "7"), enumExceptionType.Alarm)
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Position.Label.Text = cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "HmiLabel_Position")
        HmiLabel_Position.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_FailuerText.Label.Text = cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "HmiLabel_FailuerText")
        HmiLabel_FailuerText.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_FailurePicture.Label.Text = cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "HmiLabel_FailurePicture")
        HmiLabel_FailurePicture.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Choose.Button.Text = cLanguageManager.GetUserTextLine("ManualStationMaterialShelf", "HmiButton_Choose")
        HmiButton_Choose.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Picture.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Description.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        Dim lListKey As New Dictionary(Of String, String)
        For Each ElementIndex As Integer In cTextManager.GetTextListKey
            Dim Element As clsTextCfg = cTextManager.GetTextCfgFromKey(ElementIndex)
            If cLanguageManager.SecondLanguageActive Then
                If Element.Message2 = "" Then
                    lListKey.Add(Element.Key, Element.Message)
                Else
                    lListKey.Add(Element.Key, Element.Message2)
                End If
            Else
                lListKey.Add(Element.Key, Element.Message)
            End If

        Next
        HmiTextBox_Description.RegisterButton(lListKey, enumInsertType.Insert)

        HmiComboBox_Position.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        AddHandler HmiComboBox_Position.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Position.ComboBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiButton_Choose.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Picture.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Description.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Choose"
                Choose()
        End Select
    End Sub


    Private Sub Choose()

        OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                    "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                     "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
        OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.PictureFolder
        OpenFileDialog_Path.RestoreDirectory = True
        OpenFileDialog_Path.FilterIndex = 1
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            HmiTextBox_Picture.TextBox.Text = OpenFileDialog_Path.FileName
        End If
    End Sub

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiComboBox_Position.ComboBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub


    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        If HmiComboBox_Position.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_Position.ComboBox.SelectedIndex)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_Description.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Picture.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count >= 3 Then
            lTargetListParameter(2) = clsSystemPath.ToSystemPath(lTargetListParameter(2))
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count >= 3 Then
            lTargetListParameter(2) = clsSystemPath.ToIniPath(lTargetListParameter(2))
        End If
        Return True
    End Function
End Class

