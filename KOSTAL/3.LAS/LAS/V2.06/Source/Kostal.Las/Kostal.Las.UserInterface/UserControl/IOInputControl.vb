Imports System.Drawing.Drawing2D
Public Class IOControl

    Public Event OutputChangedEvent(ByVal sender As Object, ByVal e As OutputChangedArgs)

    Private _Status As Boolean = False
    Private _Type As IOType

    Public Shared Images(1) As System.Drawing.Bitmap

    Sub New(ByVal name As String, Optional ByVal Type As IOType = IOType.INPUT)

        MyBase.New()

        Me.InitializeComponent()

        Me.lblAdsVariable.Text = name

        If Type = IOType.INPUT Then
            picbox.Enabled = False
        Else
            picbox.Enabled = True
        End If

        Status = True

    End Sub

    Shadows ReadOnly Property SignalName As String
        Get
            Return Me.lblAdsVariable.Text
        End Get
    End Property

    Public Property Status As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            If value <> _Status Then
                _Status = value
                Me.picbox.BackgroundImage = CType(IIf(_Status, Images(1), Images(0)), Image)
                Me.picbox.Refresh()
                Application.DoEvents()
            End If
        End Set
    End Property

    Private Sub picbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picbox.Click
        RaiseEvent OutputChangedEvent(Me, New OutputChangedArgs(Me.lblAdsVariable.Text, Not _Status))
    End Sub

End Class

Public Enum IOType As Byte
    INPUT
    OUTPUT
End Enum

Public Class OutputChangedArgs
    Inherits EventArgs
    Private _name As String = ""
    Private _bValue As Boolean = False
    Sub New(ByVal name As String, ByVal bValue As Boolean)
        _name = name
        _bValue = bValue
    End Sub

    ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    ReadOnly Property Value As Boolean
        Get
            Return _bValue
        End Get
    End Property

End Class