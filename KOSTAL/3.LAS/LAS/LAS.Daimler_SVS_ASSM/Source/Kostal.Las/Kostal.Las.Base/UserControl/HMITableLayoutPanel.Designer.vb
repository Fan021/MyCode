Partial Class HMITableLayoutPanel
    Inherits System.Windows.Forms.TableLayoutPanel

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()
        Ini()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

End Class
