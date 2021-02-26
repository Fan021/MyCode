Public Class AlarmMessage
    Private Setting As Settings
    Private mLanguage As Language
    Private mTime As Integer = 0
    Private sw As New Stopwatch
    Public Property Message As String
        Set(ByVal value As String)
            TextBox_Message.Text = value
        End Set
        Get
            Return TextBox_Message.Text
        End Get
    End Property


    Public Property ShowTime As Integer
        Set(ByVal value As Integer)
            mTime = value
        End Set
        Get
            Return mTime
        End Get
    End Property


    Public Function Init(ByVal Devices As Dictionary(Of String, Object)) As Boolean
        mLanguage = CType(Devices(Language.Name), Language)
        Setting = CType(Devices(Settings.Name), Settings)
        mLanguage.ReadControlText(Me)
        Button_Save.Enabled = False
        sw = New Stopwatch
        sw.Start()
        Timer_Show.Enabled = True
        Return True
    End Function

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click
        Me.Close()
    End Sub

    Private Sub AlarmMessage_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Button_Save.Focus()
        TextBox_Message.SelectionStart = 0
        TextBox_Message.SelectionLength = 0
    End Sub

    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False

        ElapseTime.Text = (mTime - sw.ElapsedMilliseconds / 1000).ToString() + " s"
        If (mTime - sw.ElapsedMilliseconds / 1000) < 0 Then
            Button_Save.Enabled = True
        Else
            Timer_Show.Enabled = True
        End If
    End Sub
End Class