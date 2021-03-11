Imports System.Windows.Forms

Public Class IOParameter
    Public ReadOnly Property MainButton As Button
        Get
            Return Button_Name
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
        Button_Name.Text = strText
        Button_Name.Name = strName
        Return True
    End Function
End Class
