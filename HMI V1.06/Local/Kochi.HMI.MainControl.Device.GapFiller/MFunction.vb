Imports System.Windows.Forms
Imports System.Drawing
Imports Kochi.HMI.MainControl.UI

Public Class MFunction
    Private bOldIndicateValue As Boolean = False
    Public ReadOnly Property MainButton As Button
        Get
            Return Button_Name
        End Get
    End Property


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
        Button_Name.Text = strText
        Button_Name.Name = strName
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
        Return True
    End Function
End Class

