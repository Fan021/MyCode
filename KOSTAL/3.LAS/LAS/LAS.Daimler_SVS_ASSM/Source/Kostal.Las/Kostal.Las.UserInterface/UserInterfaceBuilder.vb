
Imports System.Windows.Forms

Public Class UserInterfaceBuilder

    Private _mainForm As Form
    Public Sub New()

        _mainForm = New MainForm_Mul

    End Sub

    Public ReadOnly Property MainUI As Form
        Get
            Return _mainForm
        End Get
    End Property

End Class
