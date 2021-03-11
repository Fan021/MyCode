Imports System.Windows.Forms

Public Class HMIButton
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private iMarginHeight As Integer = 6
    Public Overloads Property Name As String
        Set(ByVal value As String)
            ButtonValue.Name = value
        End Set
        Get
            Return ButtonValue.Name
        End Get
    End Property
    Public Property MarginHeight As Integer
        Get
            Return iMarginHeight
        End Get
        Set(ByVal value As Integer)
            iMarginHeight = value
        End Set

    End Property

    Public ReadOnly Property Button As Button
        Get
            Return ButtonValue
        End Get
    End Property

    Public Overrides Property Text As String
        Set(ByVal value As String)
            ButtonValue.Text = value
        End Set
        Get
            Return ButtonValue.Text
        End Get
    End Property

    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            ButtonValue.Width = Panel_Body.Width - 6
            ButtonValue.Height = Panel_Body.Height - iMarginHeight
            If iMarginHeight = 0 Then
                ButtonValue.Location = New System.Drawing.Point(3, 0)
            Else
                ButtonValue.Location = New System.Drawing.Point(3, 3)
            End If
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

   
End Class
