Imports System.Windows.Forms
Imports System.Drawing
Public Enum enumHMI_COLOR
    HMI_COLOR_FormLine = &HE6996F
    HMI_COLOR_FormHead = &HFFFFFF
    HMI_COLOR_FormLeft = &HF3EAE0
    HMI_COLOR_FormRight = &HF3EAE0
    HMI_COLOR_FormMid = &HFFFFFF
    HMI_COLOR_FormBody = &HDBDBDB
    HMI_COLOR_MainButton_Bottom = &HF58547
    HMI_COLOR_MainButton_Button = &HFFFFFF
    HMI_COLOR_MainButton_Button_Enter = &HFCF0DB
    HMI_COLOR_MainButton_Button_Level = &HEAD1B9
    HMI_COLOR_MainButton_Indicate = &HFFFFFF
    HMI_COLOE_MainButton_Indicate_Active = &H34CD34
    HMI_COLOE_MainButton_Indicate_Inactive = &HB4B4B4
    HMI_COLOR_InputButton_Level = &HFFFFFF
    HMI_COLOR_Label_Border_Total = &H999999
    HMI_COLOR_Label_Border_Pass = &H6600
    HMI_COLOR_Label_Border_Fail = &H333399
    HMI_COLOR_Label_Back_Total = &HFFFFFF
    HMI_COLOR_Label_Back_Pass = &H34CD34
    HMI_COLOR_Label_Back_Fail = &H3C14DC

End Enum
Public Interface IIO
    ReadOnly Property MainButton As Button
    ReadOnly Property MainIndicate As Panel
    Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
    Function SetIndicateBackColor(ByVal bValue As Boolean) As Boolean
End Interface

Public Class InputIO
    Implements IIO
    Private bOldIndicateValue As Boolean = False
    Public ControlDisable As Boolean = False
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public ReadOnly Property MainButton As Button Implements IIO.MainButton
        Get
            Return Button_Name
        End Get
    End Property


    Public ReadOnly Property MainIndicate As Panel Implements IIO.MainIndicate
        Get
            Return Panel_Indicate
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean Implements IIO.RegisterButton
        Button_Name.Text = strText
        Button_Name.Name = strName

        Return True
    End Function

    Public Function SetIndicateBackColor(ByVal bValue As Boolean) As Boolean Implements IIO.SetIndicateBackColor
        If bOldIndicateValue = bValue Then Return True
        If bValue Then
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        bOldIndicateValue = bValue
        Return True
    End Function

    Private Sub Button_Name_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseEnter
        Button_Name.BackColor = Color.White
        Button_Name.FlatAppearance.MouseOverBackColor = Color.White
        Button_Name.FlatAppearance.MouseDownBackColor = Color.White
    End Sub


    Private Sub Button_Name_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseLeave
        Button_Name.BackColor = Color.White
        Button_Name.FlatAppearance.MouseOverBackColor = Color.White
        Button_Name.FlatAppearance.MouseDownBackColor = Color.White
    End Sub


End Class
