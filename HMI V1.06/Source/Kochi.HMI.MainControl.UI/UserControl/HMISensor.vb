Imports System.Windows.Forms
Imports System.Drawing

Public Class HMISensor
    Private bOldIndicateValue As Boolean = False
    Private bOldIndicateErrorValue As Boolean = False
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public ReadOnly Property MainIndicate As Panel
        Get
            Return Panel_Indicate
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
        Panel_Indicate.Text = strText
        Panel_Indicate.Name = strName
        Return True
    End Function

    Public Function SetIndicateBackColor(ByVal bValue As Boolean) As Boolean
        If bOldIndicateValue = bValue Then Return True
        If bValue Then
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        bOldIndicateValue = bValue
        bOldIndicateErrorValue = False
        Return True
    End Function

    Public Function SetIndicateErrorBackColor(ByVal bValue As Boolean) As Boolean
        If bOldIndicateErrorValue = bValue Then Return True
        If bValue Then
            Panel_Indicate.BackColor = Color.Red
        Else
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        bOldIndicateErrorValue = bValue
        bOldIndicateValue = False
        Return True
    End Function
   
    Private Sub Panel_Indicate_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Indicate.SizeChanged
        Panel_Indicate.Dock = DockStyle.Fill
        Panel_Indicate.Location = New Point(3, 1)
        Panel_Indicate.Size = New Size(TableLayoutPanel_Body.Width * 0.4, TableLayoutPanel_Body.Height - 3)
    End Sub
End Class
