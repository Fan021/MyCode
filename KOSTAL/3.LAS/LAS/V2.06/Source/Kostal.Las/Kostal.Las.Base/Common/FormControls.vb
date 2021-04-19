
'FormControls
'Author		Frank Dümpelmann

'Version	1.0.0.1 Build 2010_02_15	- 
'
Imports System.Windows.Forms

Public Class FormControls


    Public ReadOnly Property Empty() As String
        Get
            Return Nothing
        End Get
    End Property

    Public Overloads Sub FormControl _
                     ( _
                         ByRef ToControl As GroupBox, _
                         Optional ByVal Visible As Boolean = True _
                     )

        On Error Resume Next
        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            ToControl.Refresh()
            Exit Sub
        Else
            If Not ToControl.Visible Then
                ToControl.Visible = True
                ToControl.Refresh()
            End If
        End If
    End Sub

    Public Overloads Sub FormControl _
                            ( _
                                ByRef ToControl As CheckBox, _
                                ByVal NoStatus_Color As Drawing.Color, _
                                ByVal NoStatus_String As String, _
                                ByVal Status_1 As Boolean, _
                                ByVal Status_1_Color As Drawing.Color, _
                                ByVal Status_1_String As String, _
                                Optional ByVal Visible As Boolean = True _
                            )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Not Status_1 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
            If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
        Else
            If Not Status_1_Color.IsEmpty Then
                If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
            End If
            If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
        End If

        ToControl.Refresh()
    End Sub

    Public Overloads Sub FormControl _
                        ( _
                            ByRef ToControl As CheckBox, _
                            ByVal NoStatus_Color As Drawing.Color, _
                            ByVal NoStatus_String As String, _
                            ByVal Status_1 As Boolean, _
                            ByVal Status_1_Color As Drawing.Color, _
                            ByVal Status_1_String As String, _
                            ByVal Status_2 As Boolean, _
                            ByVal Status_2_Color As Drawing.Color, _
                            ByVal Status_2_String As String, _
                            ByVal BothStatus_Color As Drawing.Color, _
                            ByVal BothStatus_String As String, _
                            Optional ByVal Visible As Boolean = True _
                        )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Not Status_1 And Not Status_2 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 And Status_2 Then
			If Not BothStatus_Color.IsEmpty Then
				If ToControl.BackColor <> BothStatus_Color Then ToControl.BackColor = BothStatus_Color
			End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 Then
			If Not Status_1_Color.IsEmpty Then
				If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
			End If
			If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
		Else 'If Status_2 Then
			If Not Status_2_Color.IsEmpty Then
				If ToControl.BackColor <> Status_2_Color Then ToControl.BackColor = Status_2_Color
			End If
			If ToControl.Text <> Status_2_String Then ToControl.Text = Status_2_String
		End If

    End Sub


    Public Overloads Sub FormControl _
                           ( _
                               ByRef ToControl As Label, _
                               ByVal NoStatus_Color As Drawing.Color, _
                               ByVal NoStatus_String As String, _
                               ByVal Status_1 As Boolean, _
                               ByVal Status_1_Color As Drawing.Color, _
                               ByVal Status_1_String As String, _
                               Optional ByVal Visible As Boolean = True, _
                               Optional ByVal Tranparent As Boolean = False, _
                               Optional ByVal Border As Boolean = True _
                           )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Tranparent And (ToControl.BackColor <> ToControl.Parent.BackColor) Then ToControl.BackColor = ToControl.Parent.BackColor
        If Not Border Then
            If ToControl.BorderStyle <> BorderStyle.None Then ToControl.BorderStyle = BorderStyle.None
        Else
            If ToControl.BorderStyle <> BorderStyle.Fixed3D Then ToControl.BorderStyle = BorderStyle.Fixed3D
        End If

        If Not Status_1 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		Else
			If Not Status_1_Color.IsEmpty Then
				If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
			End If
			If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
		End If

    End Sub


    Public Overloads Sub FormControl _
                       ( _
                           ByRef ToControl As Label, _
                           ByVal NoStatus_Color As Drawing.Color, _
                           ByVal NoStatus_String As String, _
                           ByVal Status_1 As Boolean, _
                           ByVal Status_1_Color As Drawing.Color, _
                           ByVal Status_1_String As String, _
                           ByVal Status_2 As Boolean, _
                           ByVal Status_2_Color As Drawing.Color, _
                           ByVal Status_2_String As String, _
                           ByVal BothStatus_Color As Drawing.Color, _
                           ByVal BothStatus_String As String, _
                           Optional ByVal Visible As Boolean = True, _
                           Optional ByVal Tranparent As Boolean = False, _
                           Optional ByVal Border As Boolean = True _
                       )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Tranparent And (ToControl.BackColor <> ToControl.Parent.BackColor) Then ToControl.BackColor = ToControl.Parent.BackColor
        If Not Border Then
            If ToControl.BorderStyle <> BorderStyle.None Then ToControl.BorderStyle = BorderStyle.None
        Else
            If ToControl.BorderStyle <> BorderStyle.Fixed3D Then ToControl.BorderStyle = BorderStyle.Fixed3D
        End If

        If Not Status_1 And Not Status_2 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 And Status_2 Then
			If Not BothStatus_Color.IsEmpty Then
				If ToControl.BackColor <> BothStatus_Color Then ToControl.BackColor = BothStatus_Color
			End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 Then
			If Not Status_1_Color.IsEmpty Then
				If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
			End If
			If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
		Else 'If Status_2 Then
			If Not Status_2_Color.IsEmpty Then
				If ToControl.BackColor <> Status_2_Color Then ToControl.BackColor = Status_2_Color
			End If
			If ToControl.Text <> Status_2_String Then ToControl.Text = Status_2_String
		End If

    End Sub


    Public Overloads Sub FormControl _
                           ( _
                               ByRef ToControl As Button, _
                               ByVal NoStatus_Color As Drawing.Color, _
                               ByVal NoStatus_String As String, _
                               ByVal Status_1 As Boolean, _
                               ByVal Status_1_Color As Drawing.Color, _
                               ByVal Status_1_String As String, _
                               Optional ByVal Visible As Boolean = True, _
                               Optional ByVal Tranparent As Boolean = False _
                           )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Tranparent And (ToControl.BackColor <> ToControl.Parent.BackColor) Then ToControl.BackColor = ToControl.Parent.BackColor

        If Not Status_1 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		Else
			If Not Status_1_Color.IsEmpty Then
				If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
			End If
			If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
		End If

    End Sub


    Public Overloads Sub FormControl _
                       ( _
                           ByRef ToControl As Button, _
                           ByVal NoStatus_Color As Drawing.Color, _
                           ByVal NoStatus_String As String, _
                           ByVal Status_1 As Boolean, _
                           ByVal Status_1_Color As Drawing.Color, _
                           ByVal Status_1_String As String, _
                           ByVal Status_2 As Boolean, _
                           ByVal Status_2_Color As Drawing.Color, _
                           ByVal Status_2_String As String, _
                           ByVal BothStatus_Color As Drawing.Color, _
                           ByVal BothStatus_String As String, _
                           Optional ByVal Visible As Boolean = True, _
                           Optional ByVal Tranparent As Boolean = False _
                       )

        If IsNothing(ToControl) Then Exit Sub

        If Not Visible Then
            If ToControl.Visible Then ToControl.Visible = False
            Exit Sub
        End If
        If Not ToControl.Visible Then ToControl.Visible = True

        If Tranparent And (ToControl.BackColor <> ToControl.Parent.BackColor) Then ToControl.BackColor = ToControl.Parent.BackColor

        If Not Status_1 And Not Status_2 Then
            If Not NoStatus_Color.IsEmpty Then
                If ToControl.BackColor <> NoStatus_Color Then ToControl.BackColor = NoStatus_Color
            End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 And Status_2 Then
			If Not BothStatus_Color.IsEmpty Then
				If ToControl.BackColor <> BothStatus_Color Then ToControl.BackColor = BothStatus_Color
			End If
			If ToControl.Text <> NoStatus_String Then ToControl.Text = NoStatus_String
		ElseIf Status_1 Then
			If Not Status_1_Color.IsEmpty Then
				If ToControl.BackColor <> Status_1_Color Then ToControl.BackColor = Status_1_Color
			End If
			If ToControl.Text <> Status_1_String Then ToControl.Text = Status_1_String
		Else 'If Status_2 Then
			If Not Status_2_Color.IsEmpty Then
				If ToControl.BackColor <> Status_2_Color Then ToControl.BackColor = Status_2_Color
			End If
			If ToControl.Text <> Status_2_String Then ToControl.Text = Status_2_String
		End If

    End Sub


    Public Overloads Sub FormControl( _
                            ByVal ListToControl As ListBox, _
                            ByVal NewContent As String, _
                            Optional ByVal IsDisabeld As Boolean = False, _
                            Optional ByVal MaxEntry As Long = 255 _
                           )
        If Not IsDisabeld Then
            If ListToControl.Items.Count = 0 Then
                If (NewContent <> "") Then ListToControl.Items.Add(NewContent)
            ElseIf Not (ListToControl.Items(ListToControl.Items.Count - 1) Is NewContent) Then
                ListToControl.Items.Add(NewContent)
            End If
            If ListToControl.SelectedIndex <> (ListToControl.Items.Count - 1) Then ListToControl.SelectedIndex = ListToControl.Items.Count - 1
        Else
            ListToControl.Items.Clear()
        End If
        If ListToControl.Items.Count > MaxEntry Then ListToControl.Items.RemoveAt(0)

    End Sub

	Public Overloads Sub FormControl _
	  ( _
	   ByRef ToControl As ProgressBar, _
	   ByVal BackColor As Drawing.Color, _
	   ByVal ForeColor As Drawing.Color, _
	   ByVal Value As Integer, _
	   Optional ByVal Visible As Boolean = True _
	  )

		If IsNothing(ToControl) Then Exit Sub

		If Not Visible Then
			If ToControl.Visible Then ToControl.Visible = False
			Exit Sub
		End If
		If Not ToControl.Visible Then ToControl.Visible = True


		If Value < ToControl.Minimum Or Value > ToControl.Maximum Then
			If ToControl.Value <> ToControl.Minimum Then
				ToControl.Value = ToControl.Minimum
			End If
		ElseIf ToControl.Value <> Value Then
			ToControl.Value = Value
		End If

		If ToControl.BackColor <> BackColor Then
			ToControl.BackColor = BackColor
		End If

		If ToControl.ForeColor <> ForeColor Then
			ToControl.ForeColor = ForeColor
		End If

	End Sub


End Class

    
 

