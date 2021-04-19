Imports System.Windows.Forms
Imports System.Drawing

Public Class OutputIO
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
        If Not ControlDisable Then Return
        Button_Name.BackColor = Color.White
        Button_Name.FlatAppearance.MouseOverBackColor = Color.White
        Button_Name.FlatAppearance.MouseDownBackColor = Color.White
    End Sub


    Private Sub Button_Name_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseLeave
        If Not ControlDisable Then Return
        Button_Name.BackColor = Color.White
        Button_Name.FlatAppearance.MouseOverBackColor = Color.White
        Button_Name.FlatAppearance.MouseDownBackColor = Color.White
    End Sub

End Class
