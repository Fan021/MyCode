﻿Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class CylinderParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Private cTextFont As Font
    Private cIOManager As clsIOManager
    Protected eSensorAType As enumIOType
    Protected eSensorBType As enumIOType
    Protected iSensorAXIndex As Integer = 0
    Protected iSensorAYIndex As Integer = 0
    Protected iSensorBXIndex As Integer = 0
    Protected iSensorBYIndex As Integer = 0
    Protected lListSensorAID As New Dictionary(Of Integer, String)
    Protected lListSensorBID As New Dictionary(Of Integer, String)

    Private cDebugMode As Boolean
    Private cCylinderManager As clsCylinderManager
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cIOLockA As IOLock
    Private cIOLockB As IOLock
    Private lListIOA As New List(Of clsIOLockCfg)
    Private lListIOB As New List(Of clsIOLockCfg)
    Public Property ListIOA As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIOA = value
        End Set
        Get
            Return cIOLockA.ListIO
        End Get
    End Property

    Public Property ListIOB As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIOB = value
        End Set
        Get
            Return cIOLockB.ListIO
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

    Public Property SensorAType As enumIOType
        Set(ByVal value As enumIOType)
            eSensorAType = value
        End Set
        Get
            Return eSensorAType
        End Get
    End Property


    Public Property SensorBType As enumIOType
        Set(ByVal value As enumIOType)
            eSensorBType = value
        End Set
        Get
            Return eSensorBType
        End Get
    End Property

    Public Property SensorAXIndex As Integer
        Set(ByVal value As Integer)
            iSensorAXIndex = value
        End Set
        Get
            Return iSensorAXIndex
        End Get
    End Property


    Public Property SensorAYIndex As Integer
        Set(ByVal value As Integer)
            iSensorAYIndex = value
        End Set
        Get
            Return iSensorAYIndex
        End Get
    End Property

    Public Property SensorBXIndex As Integer
        Set(ByVal value As Integer)
            iSensorBXIndex = value
        End Set
        Get
            Return iSensorBXIndex
        End Get
    End Property


    Public Property SensorBYIndex As Integer
        Set(ByVal value As Integer)
            iSensorBYIndex = value

        End Set
        Get
            Return iSensorBYIndex
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

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_SensorB, 1, 10)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_SensorA, 1, 9)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameB2, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameB, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_SensorB, 0, 10)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_SensorA, 0, 9)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameB2, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameB, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Reserve, 1, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel1, 1, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ReserveA, 0, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ReserveB, 0, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel2, 1, 8)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_OneCylinder, 0, 8)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Type, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Trigger, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA2, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA2, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ID, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 1, 11)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA, 1, 1)

        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged

        cIOLockA = New IOLock

        cIOLockA.IOManager = cIOManager
        cIOLockA.CylinderManager = cCylinderManager
        cIOLockA.ProgramButton = cProgramButton
        cIOLockA.ProgramCylinderButton = cProgramCylinderButton
        cIOLockA.DebugMode = cDebugMode
        cIOLockA.ListSourceIO = lListIOA
        cIOLockA.Init(cLocalElement, cSystemElement)
        TabPage2.Controls.Add(cIOLockA.Panel_UI)

        cIOLockB = New IOLock
        cIOLockB.IOManager = cIOManager
        cIOLockB.CylinderManager = cCylinderManager
        cIOLockB.ProgramButton = cProgramButton
        cIOLockB.ProgramCylinderButton = cProgramCylinderButton
        cIOLockB.DebugMode = cDebugMode
        cIOLockB.ListSourceIO = lListIOB
        cIOLockB.Init(cLocalElement, cSystemElement)
        TabPage3.Controls.Add(cIOLockB.Panel_UI)

        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Label_ID.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_ID")
        Label_Trigger.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_Trigger")
        Label_ReserveA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_ReserveA")
        Label_ReserveB.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_ReserveB")
        RadioButton_Toggle.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "RadioButton_Toggle")
        RadioButton_Tap.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "RadioButton_Tap")
        RadioButton_YA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "RadioButton_Y")
        RadioButton_NA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "RadioButton_N")
        Label_SensorA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_SensorA")
        Label_SensorB.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_SensorB")
        Label_OneCylinder.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_OneCylinder")

        TabPage1.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "TabPage1")
        TabPage2.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "TabPage2")
        TabPage3.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "TabPage3")


        TabControl_IO.Font = cTextFont
        TabPage1.Font = cTextFont
        TabPage2.Font = cTextFont
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Button_Cancel")
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        TextBox_NameB.Font = cTextFont
        TextBox_NameB2.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_OneCylinder.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont
        Label_NameB.Font = cTextFont
        Label_NameB2.Font = cTextFont
        TextBox_ID.Font = cTextFont
        RadioButton_Toggle.Font = cTextFont
        RadioButton_Tap.Font = cTextFont
        RadioButton_YA.Font = cTextFont
        RadioButton_NA.Font = cTextFont
        Label_Trigger.Font = cTextFont
        Label_ReserveA.Font = cTextFont
        Label_ReserveB.Font = cTextFont
        RadioButton_YB.Font = cTextFont
        RadioButton_NB.Font = cTextFont
        RadioButton_YOne.Font = cTextFont
        RadioButton_NOne.Font = cTextFont
        Label_SensorA.Font = cTextFont
        Label_SensorB.Font = cTextFont

        ComboBoxEx1_SensorA.Font = cTextFont
        ComboBoxEx1_SensorB.Font = cTextFont
        ComboBoxEx_SensorAType.Font = cTextFont
        ComboBoxEx_SensorBType.Font = cTextFont
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont

        If cLanguageManager.SecondLanguageEnable Then
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameA", cLanguageManager.FirtLanguage)
            Label_NameA2.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameA2", cLanguageManager.SecondLanguage)
            Label_NameB.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameB", cLanguageManager.FirtLanguage)
            Label_NameB2.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameB2", cLanguageManager.SecondLanguage)
        Else
            Label_NameA2.Hide()
            TextBox_NameA2.Hide()
            TableLayoutPanel_Body.RowStyles(2).Height = 0
            Label_NameB2.Hide()
            TextBox_NameB2.Hide()
            TableLayoutPanel_Body.RowStyles(4).Height = 0
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameA3")
            Label_NameB.Text = cLanguageManager.GetTextLine(enumUIName.CylinderParameter.ToString, "Label_NameB3")
        End If

        cIOManager = New clsIOManager
        cIOManager.Init(cSystemElement)
        ComboBoxEx_SensorAType.Items.Clear()
        ComboBoxEx_SensorAType.Items.Add("NONE")
        Dim i As Integer = 0
        Dim k As Integer = 1
        Dim j As Integer = 0
        For Each element As clsIOPageCfg In cIOManager.ListPage.Values
            If element.IOType = enumIOType.EL2008 Or element.IOType = enumIOType.NONE Then Continue For
            ComboBoxEx_SensorAType.Items.Add(element.ActiveText)
            lListSensorAID.Add(k, element.ID)
            If SensorAType <> enumIOType.NONE Then
                If SensorAType = element.IOType Then
                    i = i + 1
                    If i = iSensorAXIndex Then
                        ComboBoxEx_SensorAType.SelectedIndex = k
                        ComboBoxEx1_SensorA.Items.Clear()
                        j = 0
                        For Each subElement As clsIOCfg In element.ListIO.Values
                            ComboBoxEx1_SensorA.Items.Add(subElement.ActiveText)
                            If j = iSensorAYIndex Then
                                ComboBoxEx1_SensorA.SelectedIndex = j - 1
                            End If
                            j = j + 1
                        Next
                    End If
                End If
            End If
            k = k + 1
        Next

        ComboBoxEx_SensorBType.Items.Clear()
        ComboBoxEx_SensorBType.Items.Add("NONE")
        i = 0
        k = 1
        For Each element As clsIOPageCfg In cIOManager.ListPage.Values
            If element.IOType = enumIOType.EL2008 Or element.IOType = enumIOType.NONE Then Continue For
            ComboBoxEx_SensorBType.Items.Add(element.ActiveText)
            lListSensorBID.Add(k, element.ID)
            If SensorBType <> enumIOType.NONE Then
                If SensorBType = element.IOType Then
                    i = i + 1
                    If i = iSensorBXIndex Then
                        ComboBoxEx_SensorBType.SelectedIndex = k
                        ComboBoxEx1_SensorB.Items.Clear()
                        j = 0
                        For Each subElement As clsIOCfg In element.ListIO.Values
                            ComboBoxEx1_SensorB.Items.Add(subElement.ActiveText)
                            j = j + 1
                            If j = iSensorBYIndex Then
                                ComboBoxEx1_SensorB.SelectedIndex = j - 1
                            End If

                        Next
                    End If
                End If
            End If
            k = k + 1
        Next

        AddHandler ComboBoxEx1_SensorA.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler ComboBoxEx1_SensorB.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler ComboBoxEx_SensorAType.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler ComboBoxEx_SensorBType.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If cLanguageManager.SecondLanguageEnable Then
                Me.Height = (TextBox_ID.Height + 6) * 11 + 200
            Else
                Me.Height = (TextBox_ID.Height + 6) * 9 + 200
            End If
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                If (iCnt = 2 Or iCnt = 4) And Not cLanguageManager.SecondLanguageEnable Then
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

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "ComboBoxEx_SensorAType"
                If ComboBoxEx_SensorAType.SelectedIndex <= 0 Then
                    eSensorAType = enumIOType.NONE
                    iSensorAXIndex = 0
                    ComboBoxEx1_SensorA.Items.Clear()
                    ComboBoxEx1_SensorA.SelectedIndex = -1
                Else
                    Dim cIOPageCfg As clsIOPageCfg = cIOManager.GetIOPageCfgFromID(lListSensorAID(ComboBoxEx_SensorAType.SelectedIndex))
                    eSensorAType = cIOPageCfg.IOType
                    Dim i As Integer = 0
                    For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                        If element.IOType = eSensorAType Then
                            i = i + 1
                        End If
                        If element.ID = cIOPageCfg.ID Then
                            Exit For
                        End If
                    Next
                    iSensorAXIndex = i
                    ComboBoxEx1_SensorA.Items.Clear()
                    For Each subElement As clsIOCfg In cIOPageCfg.ListIO.Values
                        ComboBoxEx1_SensorA.Items.Add(subElement.ActiveText)
                    Next
                End If


            Case "ComboBoxEx_SensorBType"
                If ComboBoxEx_SensorAType.SelectedIndex <= 0 Then
                    eSensorBType = enumIOType.NONE
                    iSensorBXIndex = 0
                    ComboBoxEx1_SensorB.Items.Clear()
                    ComboBoxEx1_SensorB.SelectedIndex = -1
                Else
                    Dim cIOPageCfg As clsIOPageCfg = cIOManager.GetIOPageCfgFromID(lListSensorBID(ComboBoxEx_SensorBType.SelectedIndex))
                    eSensorBType = cIOPageCfg.IOType
                    Dim i As Integer = 0
                    For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                        If element.IOType = eSensorBType Then
                            i = i + 1
                        End If
                        If element.ID = cIOPageCfg.ID Then
                            Exit For
                        End If
                    Next
                    iSensorBXIndex = i
                    ComboBoxEx1_SensorB.Items.Clear()
                    For Each subElement As clsIOCfg In cIOPageCfg.ListIO.Values
                        ComboBoxEx1_SensorB.Items.Add(subElement.ActiveText)
                    Next
                End If

            Case "ComboBoxEx1_SensorA"
                iSensorAYIndex = ComboBoxEx1_SensorA.SelectedIndex + 1

            Case "ComboBoxEx1_SensorB"
                iSensorBYIndex = ComboBoxEx1_SensorB.SelectedIndex + 1
        End Select

    End Sub

End Class
