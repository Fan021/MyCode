Imports System.Windows.Forms

Public Class HMITextBox
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg
    Private cValueType As Type = GetType(String)
    Private iNumber As Integer

    Public Property ValueType As Type
        Set(ByVal value As Type)
            cValueType = value
        End Set
        Get
            Return cValueType
        End Get
    End Property


    Public Property Number As Integer
        Set(ByVal value As Integer)
            iNumber = value
        End Set
        Get
            Return iNumber
        End Get
    End Property

    Public Overloads Property Name As String
        Set(ByVal value As String)
            TextBoxValue.Name = value
        End Set
        Get
            Return TextBoxValue.Name
        End Get
    End Property


    Public ReadOnly Property TextBox As TextBox
        Get
            Return TextBoxValue
        End Get
    End Property

    Public Overrides Property Text As String
        Set(ByVal value As String)
            TextBoxValue.Text = value
        End Set
        Get
            Return TextBoxValue.Text
        End Get
    End Property

    Public Property TextBoxReadOnly As Boolean
        Set(ByVal value As Boolean)
            If value Then
                TextBoxValue.BackColor = Drawing.Color.WhiteSmoke
            Else
                TextBoxValue.BackColor = Drawing.Color.White
            End If
            TextBoxValue.ReadOnly = value
        End Set
        Get

            Return TextBoxValue.ReadOnly
        End Get
    End Property

    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            TextBoxValue.Width = Panel_Body.Width - 6
            TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxValue.SizeChanged
        cSizeChangeSizeCfg.newH = Me.TextBoxValue.Height
        If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
            TextBoxValue.Width = Panel_Body.Width - 6
            TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
        End If
        cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    End Sub

        


    Private Sub TextBoxValue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxValue.KeyPress

        Select Case cValueType
            Case GetType(Double), GetType(Single)
                If Not Char.IsNumber(e.KeyChar) And Not Char.IsPunctuation(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                    e.Handled = True
                End If

                If Char.IsPunctuation(e.KeyChar) Then
                    If e.KeyChar <> "." And CType(sender, TextBox).TextLength = 0 Then
                        e.Handled = True
                    End If
                    If CType(sender, TextBox).Text.LastIndexOf(".") <> -1 Then
                        e.Handled = True
                    End If
                End If
            Case GetType(Integer)
                If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                    e.Handled = True
                End If
                If iNumber > 0 Then
                    If CType(sender, TextBox).SelectedText = "" Then
                        If CType(sender, TextBox).Text.Length > iNumber - 1 Then
                            If e.KeyChar <> Chr(Keys.Back) Then
                                e.Handled = True
                            End If

                        End If
                    End If
                    
                End If
            Case GetType(clsIP)
                If Not Char.IsNumber(e.KeyChar) And Not Char.IsPunctuation(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                    e.Handled = True
                End If

                If Char.IsPunctuation(e.KeyChar) Then
                    If e.KeyChar <> "." And CType(sender, TextBox).TextLength = 0 Then
                        e.Handled = True
                    End If
                End If
        End Select


    End Sub
End Class

Public Class clsIP

End Class
