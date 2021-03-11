Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class IOParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Private cTextFont As Font
    Public DisableTriger As Boolean = False
    Public ShowType As Boolean = False
    Public ShowLevel As Boolean = False
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cIOLock As IOLock
    Private cDebugMode As Boolean
    Private cDebugLock As Boolean = True
    Private lListIO As New List(Of clsIOLockCfg)

    Public Property ListIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIO = value
        End Set
        Get
            Return cIOLock.ListIO
        End Get
    End Property

    Public Property IOManager As clsIOManager
        Set(ByVal value As clsIOManager)
            cIOManager = value
        End Set
        Get
            Return cIOManager
        End Get
    End Property
    Public Property CylinderManager As clsCylinderManager
        Set(ByVal value As clsCylinderManager)
            cCylinderManager = value
        End Set
        Get
            Return cCylinderManager
        End Get
    End Property

    Public Property ProgramButton As clsProgramButton
        Set(ByVal value As clsProgramButton)
            cProgramButton = value
        End Set
        Get
            Return cProgramButton
        End Get
    End Property


    Public Property ProgramCylinderButton As clsProgramCylinderButton
        Set(ByVal value As clsProgramCylinderButton)
            cProgramCylinderButton = value
        End Set
        Get
            Return cProgramCylinderButton
        End Get
    End Property

    Public Property TextFont As Font
        Set(ByVal value As Font)
            cTextFont = value
        End Set
        Get
            Return cTextFont
        End Get
    End Property

    Public Property DebugMode As Boolean
        Set(ByVal value As Boolean)
            cDebugMode = value
        End Set
        Get
            Return cDebugMode
        End Get
    End Property

    Public Property DebugLock As Boolean
        Set(ByVal value As Boolean)
            cDebugLock = value
        End Set
        Get
            Return cDebugLock
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        If cDebugLock Then
            cIOLock = New IOLock
            cIOLock.IOManager = cIOManager
            cIOLock.CylinderManager = cCylinderManager
            cIOLock.ProgramButton = cProgramButton
            cIOLock.ProgramCylinderButton = cProgramCylinderButton
            cIOLock.DebugMode = cDebugMode
            cIOLock.ListSourceIO = lListIO
            cIOLock.Init(cLocalElement, cSystemElement)
            TabPage2.Controls.Add(cIOLock.Panel_UI)
        Else
            TabControl_IO.TabPages.RemoveAt(1)
        End If


  
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Label_ID.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_ID")
        Label_Trigger.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_Trigger")
        Label_Reserve.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_Reserve")
        RadioButton_Toggle.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_Toggle")
        RadioButton_Tap.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_Tap")
        RadioButton_Y.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_Y")
        RadioButton_N.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_N")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Button_Cancel")
        TabPage1.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "TabPage1")
        TabPage2.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "TabPage2")

        Label_Type.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_Type")
        RadioButton_Input.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_Input")
        RadioButton_Output.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "RadioButton_Output")
        Label_UserLevel.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_UserLevel")

        Label_UserLevel.Font = cTextFont
        Label_Type.Font = cTextFont
        RadioButton_Input.Font = cTextFont
        RadioButton_Output.Font = cTextFont
        TabControl_IO.Font = cTextFont
        TabPage1.Font = cTextFont
        TabPage2.Font = cTextFont
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont
        TextBox_ID.Font = cTextFont
        Label_Reserve.Font = cTextFont
        Label_Trigger.Font = cTextFont
        RadioButton_Toggle.Font = cTextFont
        RadioButton_Tap.Font = cTextFont
        RadioButton_Y.Font = cTextFont
        RadioButton_N.Font = cTextFont
        HmiComboBox_Level.ComboBox.Font = cTextFont

        If cLanguageManager.SecondLanguageEnable Then
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_NameA", cLanguageManager.FirtLanguage)
            Label_NameA2.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_NameA2", cLanguageManager.SecondLanguage)
        Else
            Label_NameA2.Hide()
            TextBox_NameA2.Hide()
            TableLayoutPanel_Body.RowStyles(2).Height = 0
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.IOParameter.ToString, "Label_NameA3")
        End If
        If DisableTriger Then
            RadioButton_Toggle.Enabled = False
            RadioButton_Tap.Enabled = False
        End If

        HmiComboBox_Level.ComboBox.Items.Clear()
        For Each eType As enumUserLevel In [Enum].GetValues(GetType(enumUserLevel))
            If eType = enumUserLevel.Normal Then Continue For
            HmiComboBox_Level.ComboBox.Items.Add(eType.ToString)
        Next

        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim iCntNum As Integer = 5
            If ShowType Then
                iCntNum = 6
            End If

            If Not ShowLevel Then
                iCntNum = iCntNum - 1
            End If
            If cLanguageManager.SecondLanguageEnable Then
                Me.Height = (TextBox_ID.Height + 6) * (iCntNum + 1) + 200
            Else
                Me.Height = (TextBox_ID.Height + 6) * iCntNum + +200
            End If
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                If iCnt = 2 And Not cLanguageManager.SecondLanguageEnable Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                ElseIf iCnt = 3 And Not ShowType Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                ElseIf iCnt = 6 And Not ShowLevel Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                Else
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = TextBox_ID.Height + 6
                End If
                iCnt = iCnt + 1
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub RadioButton_Input_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RadioButton_Toggle.Enabled = False
        RadioButton_Tap.Enabled = False
    End Sub

    Private Sub RadioButton_Output_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RadioButton_Toggle.Enabled = True
        RadioButton_Tap.Enabled = True
    End Sub
End Class
