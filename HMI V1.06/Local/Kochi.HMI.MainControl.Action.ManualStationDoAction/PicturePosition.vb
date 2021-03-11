Imports Kochi.HMI.MainControl.Base
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class PicturePosition
    Protected cChangePage As clsChangePage
    Protected cLanguageManager As clsLanguageManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cPictureManager As clsPictureManager
    Private _oObject As New Object
    Private isCancal As Boolean = True
    Private g As Graphics
    Private bmp As Bitmap
    Private img As Image
    Private scaleX As Single
    Private scaleY As Single
    Private rectF As RectangleF
    Protected iParentProgramUI As IParentProgramUI
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected iIndex As Integer
    Protected ListPosition As List(Of clsPictureComponentCfg)

    Public ReadOnly Property Cancel As Boolean
        Get
            Return isCancal
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        TopLevel = False
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        iParentProgramUI = CType(cLocalElement(enumUIName.ParentProgramForm.ToString), IParentProgramUI)
        HmiTextBox_Zoom.TextBox.Text = 1.0
        HmiTextBox_X.TextBox.Text = 0
        HmiTextBox_Y.TextBox.Text = 0
        Label_Zoom.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_Zoom.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "Label_Zoom")
        Label_X.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_X.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "Label_X")
        Label_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_Y.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "Label_Y")
        HmiButton_Confirm.Button.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiButton_Confirm")
        HmiButton_Cancel.Button.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiButton_Cancel")

        HmiTextBox_Zoom.ValueType = GetType(Double)
        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)

        AddHandler HmiButton_Confirm.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Zoom.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_X.TextBox.KeyUp, AddressOf TextBox_KeyUp
        AddHandler HmiTextBox_Y.TextBox.KeyUp, AddressOf TextBox_KeyUp
        AddHandler HmiTextBox_Zoom.TextBox.KeyUp, AddressOf TextBox_KeyUp
        Return True
    End Function

    Public Function SetXYR(ByVal iIndex As Integer, ByVal ListPosition As List(Of clsPictureComponentCfg)) As Boolean
        Dim iX As Integer
        Dim iY As Integer
        Dim iR As Double
        HmiTextBox_X.TextBox.Text = 0
        HmiTextBox_Y.TextBox.Text = 0
        HmiTextBox_Zoom.TextBox.Text = 1.0
        Me.iIndex = iIndex
        Me.ListPosition = ListPosition

        Dim cPosition() As String = ListPosition(iIndex).strPicturePosition.Split(",")
        If cPosition.Length < 2 Then
            iX = 0
            iY = 0
            iR = 1.0
        Else
            If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                iR = 1.0
            Else
                iR = cPosition(2)
            End If

            If Not IsNumeric(cPosition(0)) Or cPosition(0) = "" Then
                iX = 0
            Else
                iX = cPosition(0)
            End If

            If Not IsNumeric(cPosition(1)) Or cPosition(1) = "" Then
                iY = 0
            Else
                iY = cPosition(1)
            End If
        End If
        HmiTextBox_X.TextBox.Text = iX.ToString
        HmiTextBox_Y.TextBox.Text = iY.ToString
        HmiTextBox_Zoom.TextBox.Text = iR.ToString

        If File.Exists(ChangeKeyToPath(iParentProgramUI.TextBox_Picture.TextBox.Text)) Then
            img = Image.FromFile(ChangeKeyToPath(iParentProgramUI.TextBox_Picture.TextBox.Text))
            scaleX = PictureBox_Body.Width * 1.0F / img.Width
            scaleY = PictureBox_Body.Height * 1.0F / img.Height

            rectF = New RectangleF()
            If (scaleX < scaleY) Then
                rectF.Width = img.Width * scaleX
                rectF.Height = img.Height * scaleX
                If scaleX < 1 Then
                    rectF.Width = img.Width * scaleX
                    rectF.Height = img.Height * scaleX
                Else
                    scaleX = 1
                    rectF.Width = img.Width * scaleX
                    rectF.Height = img.Height * scaleX
                End If
            Else
                If scaleY < 1 Then
                    rectF.Width = img.Width * scaleY
                    rectF.Height = img.Height * scaleY
                Else
                    scaleY = 1
                    rectF.Width = img.Width
                    rectF.Height = img.Height
                End If
            End If
            rectF.X = (PictureBox_Body.Width - rectF.Width) / 2.0F
            rectF.Y = (PictureBox_Body.Height - rectF.Height) / 2.0F
        Else
            img = New Bitmap(PictureBox_Body.Width, PictureBox_Body.Height)
            scaleX = 1
            scaleY = 1
            rectF.Width = PictureBox_Body.Width
            rectF.Height = PictureBox_Body.Height
            rectF.X = 0
            rectF.Y = 0
        End If
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
        bmp = New Bitmap(PictureBox_Body.Width, PictureBox_Body.Height)
        g = Graphics.FromImage(bmp)
        g.Clear(Color.White)
        PictureBox_Body.Image = bmp
        g.SmoothingMode = SmoothingMode.AntiAlias
        SetPositon()
        Return True
    End Function

    Public Function ChangeKeyToPath(ByVal strFilePath As String) As String
        If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
            If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
                Dim strKey As String = strFilePath.Replace("[", "").Replace("]", "")
                If cPictureManager.HasPicture(strKey) Then
                    strFilePath = cPictureManager.GetPictureCfgFromName(strKey).Path
                End If
            End If
        End If
        Return strFilePath
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each element As RowStyle In TableLayoutPanel_Body_Left.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Zoom.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Confirm"
                isCancal = False
            Case "HmiButton_Cancel"
                isCancal = True
        End Select
        cChangePage.BackPage()
        Me.Close()
    End Sub

    Private Sub TextBox_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ListPosition(iIndex).strPicturePosition = HmiTextBox_X.TextBox.Text.ToString + "," + HmiTextBox_Y.TextBox.Text.ToString + "," + HmiTextBox_Zoom.TextBox.Text
        SetPositon()
    End Sub

    Private Sub PictureBox_Body_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox_Body.MouseClick
        SyncLock _oObject
            If e.Button = MouseButtons.Right Then
                Dim black_left_width As Integer
                Dim black_top_height As Integer
                Dim iX As Integer
                Dim iY As Integer

                If (scaleX < scaleY) Then
                    black_left_width = IIf(img.Width = PictureBox_Body.Width, 0, (PictureBox_Body.Width - img.Width * scaleX) / 2)
                    black_top_height = IIf(img.Height = PictureBox_Body.Height, 0, (PictureBox_Body.Height - img.Height * scaleX) / 2)
                    iX = (e.X - black_left_width) / scaleX
                    iY = (e.Y - black_top_height) / scaleX
                Else
                    black_left_width = IIf(img.Width = PictureBox_Body.Width, 0, (PictureBox_Body.Width - img.Width * scaleY) / 2)
                    black_top_height = IIf(img.Height = PictureBox_Body.Height, 0, (PictureBox_Body.Height - img.Height * scaleY) / 2)
                    iX = (e.X - black_left_width) / scaleY
                    iY = (e.Y - black_top_height) / scaleY
                End If

                HmiTextBox_X.TextBox.Text = iX
                HmiTextBox_Y.TextBox.Text = iY
                ListPosition(iIndex).strPicturePosition = iX.ToString + "," + iY.ToString + "," + HmiTextBox_Zoom.TextBox.Text
                SetPositon()
            End If
        End SyncLock
    End Sub

    Private Sub SetPositon()
        SyncLock _oObject
            Dim iR As Double
            Dim iX As Integer
            Dim iY As Integer
            Dim iPointX As Integer
            Dim iPointY As Integer
            Dim strPosition As String = ""
            Dim iIndex As Integer = 0

            g.Clear(Color.White)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If Not IsNothing(bmp) Then g.DrawImage(img, rectF)

            For i = 0 To ListPosition.Count - 1
                Dim cPictureCfg As clsPictureComponentCfg = ListPosition(i)
                If cPictureCfg.strPicturePath = "" Then
                    Continue For
                End If
                Dim cPosition() As String = cPictureCfg.strPicturePosition.Split(",")
                If cPosition.Length < 2 Then
                    iX = 0
                    iY = 0
                    iR = 1.0
                Else
                    If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                        iR = 1.0
                    Else
                        iR = cPosition(2)
                    End If

                    If Not IsNumeric(cPosition(0)) Or cPosition(0) = "" Then
                        iX = 0
                    Else
                        iX = cPosition(0)
                    End If

                    If Not IsNumeric(cPosition(1)) Or cPosition(1) = "" Then
                        iY = 0
                    Else
                        iY = cPosition(1)
                    End If
                End If

                Dim black_left_width As Integer
                Dim black_top_height As Integer

                If (scaleX < scaleY) Then
                    black_left_width = IIf(img.Width = PictureBox_Body.Width, 0, (PictureBox_Body.Width - img.Width * scaleX) / 2)
                    black_top_height = IIf(img.Height = PictureBox_Body.Height, 0, (PictureBox_Body.Height - img.Height * scaleX) / 2)
                    iPointX = iX * scaleX + black_left_width
                    iPointY = iY * scaleX + black_top_height
                Else
                    black_left_width = IIf(img.Width = PictureBox_Body.Width, 0, (PictureBox_Body.Width - img.Width * scaleY) / 2)
                    black_top_height = IIf(img.Height = PictureBox_Body.Height, 0, (PictureBox_Body.Height - img.Height * scaleY) / 2)
                    iPointX = iX * scaleY + black_left_width
                    iPointY = iY * scaleY + black_top_height
                End If

                If File.Exists(cPictureCfg.strPicturePath) Then
                    Dim imgCompontent As Image = Image.FromFile(cPictureCfg.strPicturePath)
                    Dim rectCompontent As RectangleF = New RectangleF()
                    rectCompontent.Width = imgCompontent.Width * iR
                    rectCompontent.Height = imgCompontent.Height * iR
                    rectCompontent.X = iPointX - rectCompontent.Width / 2
                    rectCompontent.Y = iPointY - rectCompontent.Height / 2
                    g.DrawImage(imgCompontent, rectCompontent)
                End If
            Next

            Dim graphics As Graphics = PictureBox_Body.CreateGraphics()
            graphics.DrawImage(bmp, New Point(0, 0))
            graphics.Dispose()
        End SyncLock
    End Sub
    Public Function Quit() As Boolean
        Me.Dispose()
        Return True
    End Function
End Class

