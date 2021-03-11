Imports System.Windows.Forms
Imports System.Drawing

Public Class HMIComboBox
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg
    Dim iLastX As Integer = 0
    Dim iLastY As Integer = 0
    Dim iLastIndex As Integer = 0
    Dim toolTip1 As New ToolTip
    Private _Object As New Object
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Public ReadOnly Property ComboBox As ComboBox
        Get
            Return ComboBoxValue
        End Get
    End Property
    Public Overloads Property Name As String
        Set(ByVal value As String)
            ComboBoxValue.Name = value
        End Set
        Get
            Return ComboBoxValue.Name
        End Get
    End Property

    Private Sub HMIComboBox_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            ComboBoxValue.Hide()
            ComboBoxValue.Width = Panel_Body.Width - 6
            ComboBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - ComboBoxValue.Height) / 2)
            ComboBoxValue.Show()
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    Private Sub ComboBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxValue.SizeChanged
        cSizeChangeSizeCfg.newH = Me.ComboBoxValue.Height
        If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
            ComboBoxValue.Hide()
            ComboBoxValue.Width = Panel_Body.Width - 6
            ComboBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - ComboBoxValue.Height) / 2)
            ComboBoxValue.Show()
        End If
        cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    End Sub
End Class
