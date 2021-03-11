Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class ProgramDebugForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Private cTextFont As Font
    Private iCurrentIndex As Integer = 0
    Private iMaxIndex As Integer = 0
    Private strCurrentType As String
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private iProgramNumber As Integer = 0
    Private iProgramCylinderNumber As Integer = 0

    Public Property CurrentIndex As Integer
        Set(ByVal value As Integer)
            iCurrentIndex = value
        End Set
        Get
            Return iCurrentIndex
        End Get
    End Property


    Public Property CurrentType As String
        Set(ByVal value As String)
            strCurrentType = value
        End Set
        Get
            Return strCurrentType
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


    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        AddHandler ComboBox_Name.SizeChanged, AddressOf TextBox_SizeChanged
        cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
        cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)

        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
        Label_Name.Text = cLanguageManager.GetTextLine("ProgramDebugForm", "Label_Name")
        Label_Type.Text = cLanguageManager.GetTextLine("ProgramDebugForm", "Label_Type")
        Button_Save.Text = cLanguageManager.GetTextLine("ProgramDebugForm", "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetTextLine("ProgramDebugForm", "Button_Cancel")
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        ComboBox_Name.Font = cTextFont
        Label_Name.Font = cTextFont
        Label_Type.Font = cTextFont

        RadioButton_A.Font = cTextFont
        RadioButton_AB.Font = cTextFont
        RadioButton_B.Font = cTextFont

        ComboBox_Name.Items.Clear()
        ComboBox_Name.Items.Add("NONE")
        ComboBox_Name.SelectedIndex = 0
        Dim iCnt As Integer = 1
        iProgramNumber = 0
        iProgramCylinderNumber = 0
        For Each element As clsIOPageCfg In cProgramButton.ListPage.Values
            For Each elementIO As clsIOCfg In element.ListIO.Values
                ComboBox_Name.Items.Add(element.ActiveText + "-" + elementIO.ActiveText)
                If iProgramNumber + 1 = iCurrentIndex And strCurrentType = clsProgramButton.Name Then
                    ComboBox_Name.SelectedIndex = iCnt
                End If
                iProgramNumber = iProgramNumber + 1
                iCnt = iCnt + 1
            Next
        Next

        For Each element As clsCylinderPageCfg In cProgramCylinderButton.ListPage.Values
            For Each elementIO As clsCylinderCfg In element.ListIO.Values
                ComboBox_Name.Items.Add(element.ActiveText + "-Cylinder-" + elementIO.ActiveTextA + "-" + elementIO.ActiveTextB)
                If iProgramCylinderNumber + 1 = iCurrentIndex And strCurrentType = clsProgramCylinderButton.Name Then
                    ComboBox_Name.SelectedIndex = iCnt
                End If
                iProgramCylinderNumber = iProgramCylinderNumber + 1
                iCnt = iCnt + 1
            Next
        Next

        If RadioButton_A.Checked = False And RadioButton_AB.Checked = False And RadioButton_B.Checked = False Then
            RadioButton_AB.Checked = True
        End If

        AddHandler ComboBox_Name.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click

        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Me.Height = (ComboBox_Name.Height + 6) * 2 + 150
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = ComboBox_Name.Height + 6
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

        If ComboBox_Name.SelectedIndex <= iProgramNumber Then
            strCurrentType = clsProgramButton.Name
            iCurrentIndex = ComboBox_Name.SelectedIndex
            RadioButton_A.Enabled = False
            RadioButton_AB.Enabled = False
            RadioButton_B.Enabled = False
            RadioButton_B.Checked = True
        Else
            RadioButton_A.Enabled = True
            RadioButton_AB.Enabled = True
            RadioButton_B.Enabled = True
            strCurrentType = clsProgramCylinderButton.Name
            iCurrentIndex = ComboBox_Name.SelectedIndex - iProgramNumber
        End If
        If ComboBox_Name.SelectedIndex = 0 Then
            strCurrentType = ""
            iCurrentIndex = ComboBox_Name.SelectedIndex
        End If

    End Sub
End Class
