Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base
Imports System.Drawing
Imports System.Windows.Forms

Public Class IOParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cTextFont As Font
    Public DisableTriger As Boolean = False
    Public ShowType As Boolean = False
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private cIOLock As IOLock
    Private cDebugMode As Boolean
    Private cUserLevel As Boolean = False
    Private cDebugLock As Boolean = True
    Private lListIO As New List(Of clsIOLockCfg)
    Private cLanguageManager As Language
    Private cTips As clsTips
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


    Public Property UserLevel As Boolean
        Set(ByVal value As Boolean)
            cUserLevel = value
        End Set
        Get
            Return cUserLevel
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        cLanguageManager = CType(cSystemElement(Language.Name), Language)
        cTips = CType(cSystemElement(clsTips.Name), clsTips)
        If cDebugLock Then
            cIOLock = New IOLock
            cIOLock.IOManager = cIOManager
            cIOLock.CylinderManager = cCylinderManager
            cIOLock.DebugMode = cDebugMode
            cIOLock.ListSourceIO = lListIO
            cIOLock.Init(cLocalElement, cSystemElement)
            TabPage2.Controls.Add(cIOLock.Panel_UI)
        Else
            TabControl_IO.TabPages.RemoveAt(1)
        End If



        'cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        'cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        'cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Label_ID.Text = cLanguageManager.Read("IOParameter", "Label_ID")
        Label_Trigger.Text = cLanguageManager.Read("IOParameter", "Label_Trigger")
        Label_Reserve.Text = cLanguageManager.Read("IOParameter", "Label_Reserve")
        RadioButton_Toggle.Text = cLanguageManager.Read("IOParameter", "RadioButton_Toggle")
        RadioButton_Tap.Text = cLanguageManager.Read("IOParameter", "RadioButton_Tap")
        RadioButton_Y.Text = cLanguageManager.Read("IOParameter", "RadioButton_Y")
        RadioButton_N.Text = cLanguageManager.Read("IOParameter", "RadioButton_N")
        Button_Save.Text = cLanguageManager.Read("IOParameter", "Button_Save")
        Button_Cancel.Text = cLanguageManager.Read("IOParameter", "Button_Cancel")
        TabPage1.Text = cLanguageManager.Read("IOParameter", "TabPage1")
        TabPage2.Text = cLanguageManager.Read("IOParameter", "TabPage2")

        Label_Type.Text = cLanguageManager.Read("IOParameter", "Label_Type")
        RadioButton_Input.Text = cLanguageManager.Read("IOParameter", "RadioButton_Input")
        RadioButton_Output.Text = cLanguageManager.Read("IOParameter", "RadioButton_Output")
        Label_NameA.Text = cLanguageManager.Read("IOParameter", "Label_NameA")
        Label_NameA2.Text = cLanguageManager.Read("IOParameter", "Label_NameA2")
        Level.Text = cLanguageManager.Read("IOParameter", "Level")

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
        Level.Font = cTextFont
        HmiComboBox_Level.ComboBox.Font = cTextFont

        HmiComboBox_Level.ComboBox.Items.Clear()
        HmiComboBox_Level.ComboBox.Items.Add(enumUserLevel.Operator.ToString)
        HmiComboBox_Level.ComboBox.Items.Add(enumUserLevel.Administrator.ToString)

        If DisableTriger Then
            RadioButton_Toggle.Enabled = False
            RadioButton_Tap.Enabled = False
        End If

        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim iCntNum As Integer = 4
            If ShowType Then
                iCntNum = 5
            End If

            If cUserLevel Then
                iCntNum = iCntNum + 1
            End If

            Me.Height = (TextBox_ID.Height + 6) * (iCntNum + 1) + 200

            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                If iCnt = 3 And Not ShowType Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                ElseIf iCnt = 6 And Not cUserLevel Then
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
