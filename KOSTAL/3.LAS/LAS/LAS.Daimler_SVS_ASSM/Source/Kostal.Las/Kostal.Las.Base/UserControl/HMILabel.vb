Imports System.Windows.Forms
Imports System.Drawing

Public Class HMILabel
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg

    Public Overloads Property Name As String
        Set(ByVal value As String)
            LabelValue.Name = value

        End Set
        Get
            Return LabelValue.Name
        End Get
    End Property
    Public ReadOnly Property Label As Label
        Get
            Return LabelValue
        End Get
    End Property

    Public Overrides Property Text As String
        Set(ByVal value As String)
            LabelValue.Text = value
        End Set
        Get
            Return LabelValue.Text
        End Get
    End Property


    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            If LabelValue.TextAlign = ContentAlignment.MiddleLeft Then
                LabelValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - LabelValue.Height) / 2)
            Else
                LabelValue.Location = New System.Drawing.Point((Panel_Body.Width - LabelValue.Width) / 2, (Panel_Body.Height - LabelValue.Height) / 2)
            End If
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    Private Sub Label_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelValue.SizeChanged
        cSizeChangeSizeCfg.newH = Me.LabelValue.Height
        If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
            If LabelValue.TextAlign = ContentAlignment.MiddleLeft Then
                LabelValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - LabelValue.Height) / 2)
            Else
                LabelValue.Location = New System.Drawing.Point((Panel_Body.Width - LabelValue.Width) / 2, (Panel_Body.Height - LabelValue.Height) / 2)
            End If
        End If
        cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    End Sub


End Class
