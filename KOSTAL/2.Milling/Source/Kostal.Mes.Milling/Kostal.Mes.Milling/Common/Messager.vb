Option Explicit On
Imports System.Windows.Forms

Public Class Messager
    Protected mBoxLabel As Label
    Protected mBoxListBox As ListBox
    Protected Const mMaxList As Long = 255


    Public Overloads Sub Construct(ByVal Box As ListBox)
        mBoxLabel = Nothing
        If Not IsNothing(Box) Then
            mBoxListBox = Box
        Else
            mBoxListBox = Nothing
        End If
    End Sub

    Public Overloads Sub Construct(ByVal Box As Label)
        mBoxListBox = Nothing
        If Not IsNothing(Box) Then
            mBoxLabel = Box
        Else
            mBoxLabel = Nothing
        End If
    End Sub

    Public Function GetMessage() As String
        Try
            If Not IsNothing(mBoxLabel) Then
                Return mBoxLabel.Text
            ElseIf Not IsNothing(mBoxListBox) Then
                Return mBoxListBox.Items(mBoxListBox.Items.Count).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub ShowMessage(ByVal Message As String)
        Try
            If Not IsNothing(mBoxLabel) Then
                mBoxLabel.Text = Message
            ElseIf Not IsNothing(mBoxListBox) Then
                Try
                    mBoxListBox.Items.Add(Message)
                    SetLastMessage()
                    If mBoxListBox.Items.Count >= mMaxList Then mBoxListBox.Items.Remove(0)
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub SetLastMessage()
        If Not IsNothing(mBoxListBox) Then
            Try
                mBoxListBox.SelectedIndex = mBoxListBox.Items.Count - 1
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub ShowMsg(ByVal mMsg As String)
        MsgBox(mMsg, vbCritical, mMsg)
    End Sub

End Class
