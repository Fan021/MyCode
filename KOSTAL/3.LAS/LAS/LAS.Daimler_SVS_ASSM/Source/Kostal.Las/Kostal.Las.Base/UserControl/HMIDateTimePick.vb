Public Class HMIDateTimePick
    Private Sub HMIDateTimePick_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i = 0 To 23
            ComboBox_Hour.Items.Add(i.ToString("D2"))
        Next
        For i = 0 To 59
            ComboBox_Minute.Items.Add(i.ToString("D2"))
            ComboBox_Second.Items.Add(i.ToString("D2"))
        Next

        ComboBox_Hour.Text = "00"
        ComboBox_Minute.Text = "00"
        ComboBox_Second.Text = "00"
    End Sub
End Class
