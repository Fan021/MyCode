Public Class KeyPad

    Event PressedKey(ByVal Number As Integer)

    Private Const mKEYCODE_0 As Integer = 0
    Private Const mKEYCODE_1 As Integer = 1
    Private Const mKEYCODE_2 As Integer = 2
    Private Const mKEYCODE_3 As Integer = 3
    Private Const mKEYCODE_4 As Integer = 4
    Private Const mKEYCODE_5 As Integer = 5
    Private Const mKEYCODE_6 As Integer = 6
    Private Const mKEYCODE_7 As Integer = 7
    Private Const mKEYCODE_8 As Integer = 8
    Private Const mKEYCODE_9 As Integer = 9
    Private Const mKEYCODE_CLEAR As Integer = 10
    Private Const mKEYCODE_ESC As Integer = 11
    Private Const mKEYCODE_ENTER As Integer = 12

    Public ReadOnly Property KeyCode_0() As Integer
        Get
            Return mKEYCODE_0
        End Get
    End Property

    Public ReadOnly Property KeyCode_1() As Integer
        Get
            Return mKEYCODE_1
        End Get
    End Property

    Public ReadOnly Property KeyCode_2() As Integer
        Get
            Return mKEYCODE_2
        End Get
    End Property

    Public ReadOnly Property KeyCode_3() As Integer
        Get
            Return mKEYCODE_3
        End Get
    End Property

    Public ReadOnly Property KeyCode_4() As Integer
        Get
            Return mKEYCODE_4
        End Get
    End Property

    Public ReadOnly Property KeyCode_5() As Integer
        Get
            Return mKEYCODE_5
        End Get
    End Property

    Public ReadOnly Property KeyCode_6() As Integer
        Get
            Return mKEYCODE_6
        End Get
    End Property

    Public ReadOnly Property KeyCode_7() As Integer
        Get
            Return mKEYCODE_7
        End Get
    End Property

    Public ReadOnly Property KeyCode_8() As Integer
        Get
            Return mKEYCODE_8
        End Get
    End Property

    Public ReadOnly Property KeyCode_9() As Integer
        Get
            Return mKEYCODE_9
        End Get
    End Property

    Public ReadOnly Property KeyCode_Clear() As Integer
        Get
            Return mKEYCODE_CLEAR
        End Get
    End Property

    Public ReadOnly Property KeyCode_Esc() As Integer
        Get
            Return mKEYCODE_ESC
        End Get
    End Property

    Public ReadOnly Property KeyCode_Enter() As Integer
        Get
            Return mKEYCODE_ENTER
        End Get
    End Property



    Private Sub Button_0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_0.Click
        RaiseEvent PressedKey(mKEYCODE_0)
    End Sub

    Private Sub Button_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_1.Click
        RaiseEvent PressedKey(mKEYCODE_1)
    End Sub

    Private Sub Button_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_2.Click
        RaiseEvent PressedKey(mKEYCODE_2)
    End Sub

    Private Sub Button_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_3.Click
        RaiseEvent PressedKey(mKEYCODE_3)
    End Sub

    Private Sub Button_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_4.Click
        RaiseEvent PressedKey(mKEYCODE_4)
    End Sub

    Private Sub Button_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_5.Click
        RaiseEvent PressedKey(mKEYCODE_5)
    End Sub

    Private Sub Button_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_6.Click
        RaiseEvent PressedKey(mKEYCODE_6)
    End Sub

    Private Sub Button_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_7.Click
        RaiseEvent PressedKey(mKEYCODE_7)
    End Sub

    Private Sub Button_8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_8.Click
        RaiseEvent PressedKey(mKEYCODE_8)
    End Sub

    Private Sub Button_9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_9.Click
        RaiseEvent PressedKey(mKEYCODE_9)
    End Sub

    Private Sub Button_Enter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Enter.Click
        RaiseEvent PressedKey(mKEYCODE_ENTER)
    End Sub

    Private Sub Button_Escape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Escape.Click
        RaiseEvent PressedKey(mKEYCODE_ESC)
    End Sub

    Private Sub Button_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Clear.Click
        RaiseEvent PressedKey(mKEYCODE_CLEAR)
    End Sub

End Class

