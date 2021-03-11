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
    Protected iParentProgramUI As IParentProgramUI
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
    Private cMainStepCfg As clsMainStepCfg
    Private cSubStepCfg As clsSubStepCfg
    Private cActionLibManager As clsActionLibManager
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected lListPosition As New Dictionary(Of Integer, String)

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
        iParentProgramUI = CType(cLocalElement(enumUIName.ParentProgramForm.ToString), IParentProgramUI)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
        cMainStepCfg = CType(cLocalElement(clsMainStepCfg.Name), clsMainStepCfg)
        cSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
        HmiTextBox_Radius.TextBox.Text = 10
        HmiTextBox_X.TextBox.Text = 0
        HmiTextBox_Y.TextBox.Text = 0
        Label_Radius.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_Radius.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "Label_Radius")
        Label_X.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_X.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "Label_X")
        Label_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Label_Y.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "Label_Y")
        HmiButton_Confirm.Button.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiButton_Confirm")
        HmiButton_Cancel.Button.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiButton_Cancel")

        HmiTextBox_Radius.ValueType = GetType(Double)
        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)

        AddHandler HmiButton_Confirm.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Radius.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_X.TextBox.KeyUp, AddressOf TextBox_KeyUp
        AddHandler HmiTextBox_Y.TextBox.KeyUp, AddressOf TextBox_KeyUp
        AddHandler HmiTextBox_Radius.TextBox.KeyUp, AddressOf TextBox_KeyUp
        Return True
    End Function

    Public Function SetXYR(ByVal strPosition As String) As Boolean
        lListPosition.Clear()
        Dim strPostion As String = ""
        HmiTextBox_X.TextBox.Text = 0
        HmiTextBox_Y.TextBox.Text = 0
        HmiTextBox_Radius.TextBox.Text = 10
        For Each elementSubCfg As clsSubStepCfg In cMainStepCfg.SubStepList
            If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "" Then Continue For
            '  If elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then Continue For
            If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
            End If
            If TypeOf CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI Is IScrewActionUI Then
                CType(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI, IScrewActionUI).GetPicturePostion(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter)), strPostion)
                lListPosition.Add(CInt(elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), strPostion)
            End If
        Next
        Dim cPositon() As String = lListPosition(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).Split(",")
        If cPositon.Length >= 3 Then
            HmiTextBox_X.TextBox.Text = cPositon(0)
        End If

        If cPositon.Length >= 3 Then
            HmiTextBox_Y.TextBox.Text = cPositon(1)
        End If

        If cPositon.Length >= 3 Then
            HmiTextBox_Radius.TextBox.Text = cPositon(2)
        End If

        If File.Exists(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement)) Then
            img = Image.FromFile(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
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
                element.Height = HmiTextBox_Radius.TextBox.Height + 6 + 6
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
        lListPosition(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)) = HmiTextBox_X.TextBox.Text + "," + HmiTextBox_Y.TextBox.Text + "," + HmiTextBox_Radius.TextBox.Text
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
                lListPosition(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)) = iX.ToString + "," + iY.ToString + "," + HmiTextBox_Radius.TextBox.Text
                SetPositon()
            End If
        End SyncLock
    End Sub

    Private Sub SetPositon()
        SyncLock _oObject
            Dim iR As Integer
            Dim iX As Integer
            Dim iY As Integer
            Dim iPointX As Integer
            Dim iPointY As Integer
            Dim strPosition As String = ""
            Dim iIndex As Integer = CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))


            g.Clear(Color.White)
            g.SmoothingMode = SmoothingMode.AntiAlias
            If Not IsNothing(bmp) Then g.DrawImage(img, rectF)

            For i = 0 To lListPosition.Count - 1
                strPosition = lListPosition(lListPosition.Keys(i))
                If strPosition = "" Then
                    Continue For
                End If
                Dim cPosition() As String = strPosition.Split(",")
                If cPosition.Length < 2 Then
                    iX = 0
                    iY = 0
                    iR = 10
                Else
                    If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                        iR = 10
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

                If lListPosition.Keys(i) < iIndex Then
                    g.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                End If

                If lListPosition.Keys(i) = iIndex Then
                    g.FillEllipse(Brushes.Yellow, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                End If

                If lListPosition.Keys(i) > iIndex Then
                    g.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                End If
                Dim iSize As Integer = ChangeSize(iR, (i + 1).ToString)
                Dim iH As Integer = g.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height
                Dim iW As Integer = g.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width
                '  g.DrawString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold), New SolidBrush(Color.Black), New Point(iPointX - iW / 2, iPointY - iH / 2))
            Next

            Dim graphics As Graphics = PictureBox_Body.CreateGraphics()
            graphics.DrawImage(bmp, New Point(0, 0))
            graphics.Dispose()
        End SyncLock
    End Sub
    Private Function ChangeSize(ByVal iR As Integer, ByVal strValue As String) As Integer
        Dim iH As Integer = 2 * iR
        Dim iW As Integer = 2 * iR
        Dim iSize As Integer = 1
        Do While g.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width < iW And g.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height < iH
            iSize = iSize + 1
        Loop
        iSize = iSize - 2
        If iSize < 1 Then iSize = 1
        Return iSize
    End Function
    Public Function Quit() As Boolean
        Me.Dispose()
        Return True
    End Function
End Class