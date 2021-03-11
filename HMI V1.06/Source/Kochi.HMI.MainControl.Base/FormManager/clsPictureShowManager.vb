Imports System.Reflection
Imports System.Math
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging


Public Class clsPictureShowManager
    Implements IDisposable
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected lstResults As Panel
    Protected mMainForm As IMainUI
    Protected _Object As New Object
    Protected WithEvents PictureBox_Material1 As New PictureBox
    Protected WithEvents PictureBox_Material2 As New PictureBox
    Protected cPictureManager As clsPictureManager
    Private cLanguageManager As clsLanguageManager
    Protected iPicType As Integer = 0
    Protected g1 As Graphics
    Protected bmp1 As Bitmap
    Protected img1 As Image
    Protected scaleX1 As Single
    Protected scaleY1 As Single
    Protected rectF1 As RectangleF
    Protected iCnt As Integer = 0
    Protected lListPosition As New Dictionary(Of Integer, String)
    Protected lListPositionStatus As New Dictionary(Of Integer, enumFlashType)
    Protected strLastPicPath As String = ""
    Public Const Name As String = "PictureShowManager"
    Protected Delegate Sub DShowPicture(ByVal strFilePath As String, ByVal strActionType As String, ByVal bFlash As Boolean, ByVal iIndex As Integer)
    Protected cShowPicture As DShowPicture
    Protected Delegate Sub DShowIndicate(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType)
    Protected cShowIndicate As DShowIndicate
    Protected Delegate Sub DFlashIndicate(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType)
    Protected cFlashIndicate As DFlashIndicate
    Protected Delegate Sub DShowActions(ByVal lListAction As List(Of clsShowActionCfg))
    Protected cShowActions As DShowActions
    Protected Delegate Sub DShowPictures(ByVal strFilePath1 As String, ByVal strFilePath2 As String)
    Protected cShowPictures As DShowPictures
    Protected Delegate Sub DShowComponents(ByVal lListCompontent As List(Of clsPictureComponentCfg))
    Protected cShowComponents As DShowComponents
    Protected iCurrentType As Integer = 0
    Protected iOldIndex As Integer = 0
    Protected bOldResultFlashType As enumFlashType
    Protected eOldFlashType As enumFlashType
    Protected lOldListAction As New List(Of clsShowActionCfg)
    Protected lOldListCompontent As New List(Of clsPictureComponentCfg)
    Protected strOldFilePath1 As String = ""
    Protected strOldFilePath2 As String = ""
    Protected Delegate Sub DShowCurrentPictures(ByVal strFilePath1 As String, ByVal strFilePath2 As String)
    Protected cShowCurrentPictures As DShowCurrentPictures
    Protected bIsRunning As Boolean = False
    Protected cMainButtonManager As clsMainButtonManager
    Public ReadOnly Property IsRunning
        Get
            Return bIsRunning
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMainButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)

                cShowPicture = New DShowPicture(AddressOf ShowPictureAction)
                cShowIndicate = New DShowIndicate(AddressOf ShowIndicateAction)
                cFlashIndicate = New DFlashIndicate(AddressOf FlashIndicateAction)
                cShowActions = New DShowActions(AddressOf ShowActionsAction)
                cShowPictures = New DShowPictures(AddressOf ShowPicturesAction)
                cShowComponents = New DShowComponents(AddressOf ShowComponentAction)
                cShowCurrentPictures = New DShowCurrentPictures(AddressOf ShowCurrentPicturesAction)
                ' AddHandler cMainButtonManager.PageChanged, AddressOf PageChanged
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal lstResults As Panel) As Boolean
        SyncLock _Object
            Try
                Me.lstResults = lstResults
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeKeyToPath(ByVal strFilePath As String) As String
        SyncLock _Object
            If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
                If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
                    Dim strKey As String = strFilePath.Replace("[", "").Replace("]", "")
                    If cPictureManager.HasPicture(strKey) Then
                        strFilePath = cPictureManager.GetPictureCfgFromName(strKey).Path
                    End If
                End If
            End If
            Return strFilePath
        End SyncLock
    End Function

    Public Function ShowPicture(ByVal strFilePath As String, Optional ByVal strActionType As String = "", Optional ByVal bFlash As Boolean = False, Optional ByVal iIndex As Integer = 0) As Boolean
        SyncLock _Object
            bIsRunning = True
            mMainForm.InvokeAction(cShowPicture, strFilePath, strActionType, bFlash, iIndex)
            Return True
        End SyncLock
    End Function

    Private Sub Picture_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim t As Integer = PictureBox_Material1.Height
        SyncLock _Object
            Select Case iCurrentType
                Case 1
                    ShowPicture(strLastPicPath)
                Case 2
                    ShowPicture(strLastPicPath)
                    ShowIndicate(iOldIndex, bOldResultFlashType)
                Case 3
                    ShowPicture(strLastPicPath)
                    FlashIndicate(iOldIndex, eOldFlashType)
                Case 4
                    ShowActions(lOldListAction)
                Case 5
                    ShowPictures(strOldFilePath1, strOldFilePath2)
                Case 6
                    ShowPicture(strLastPicPath)
                    ShowComponent(lOldListCompontent)
                Case 7
                    ShowCurrentPictures(strOldFilePath1, strOldFilePath2)
            End Select
        End SyncLock
    End Sub


    Private Sub ShowPictureAction(ByVal strFilePath As String, Optional ByVal strActionType As String = "", Optional ByVal bFlash As Boolean = False, Optional ByVal iIndex As Integer = 0)
        SyncLock _Object
            Try
                If strFilePath = "" Then
                    bIsRunning = False
                    Return
                End If

                iCnt = 0
                strFilePath = ChangeKeyToPath(strFilePath)
                If Not File.Exists(strFilePath) Then
                    Return
                End If
                If strLastPicPath = strFilePath Then
                    If iCurrentType = 1 Then
                        bIsRunning = False
                        Return
                    End If

                    'If strActionType = "ManualStationDoAction" Or strActionType = "AutoStationScrew" Then
                    '    Return
                    'End If
                End If
                iCurrentType = 1
                If iPicType <> 1 Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    lstResults.Controls.Clear()
                    RemoveHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged
                    PictureBox_Material1 = New PictureBox
                    PictureBox_Material1.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Material1.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Material1.Margin = New System.Windows.Forms.Padding(0)
                    PictureBox_Material1.Name = "PictureBox_Material"
                    PictureBox_Material1.Padding = New System.Windows.Forms.Padding(0)
                    lstResults.Controls.Add(PictureBox_Material1)
                    AddHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged
                    '  PictureBox_Material1.Image = Image.FromFile(strFilePath)
                    PictureBox_Material1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    iPicType = 1
                End If

                If File.Exists(strFilePath) Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    Using file1 As New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
                        Dim img = FileCompress.CompressionImage(file1, 50)
                        img1 = FileCompress.BytesToImage(img)
                        file1.Close()
                    End Using
                   
                    scaleX1 = PictureBox_Material1.Width * 1.0F / img1.Width
                    scaleY1 = PictureBox_Material1.Height * 1.0F / img1.Height
                    rectF1 = New RectangleF()
                    If (scaleX1 < scaleY1) Then
                        If scaleX1 < 1 Then
                            rectF1.Width = img1.Width * scaleX1
                            rectF1.Height = img1.Height * scaleX1
                        Else
                            scaleX1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    Else
                        If scaleY1 < 1 Then
                            rectF1.Width = img1.Width * scaleY1
                            rectF1.Height = img1.Height * scaleY1
                        Else
                            scaleY1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    End If
                    rectF1.X = (PictureBox_Material1.Width - rectF1.Width) / 2.0F
                    rectF1.Y = (PictureBox_Material1.Height - rectF1.Height) / 2.0F
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    'g1 = Graphics.FromImage(bmp1)
                    'g1.Clear(Color.White)
                    'PictureBox_Material1.Image = bmp1
                    'g1.SmoothingMode = SmoothingMode.AntiAlias
                    'g1.Clear(Color.White)
                    'g1.SmoothingMode = SmoothingMode.AntiAlias
                    'If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    'Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                    'graphics1.DrawImage(bmp1, New Point(0, 0))
                    'graphics1.Dispose()
                    If Not bFlash Then
                        g1 = Graphics.FromImage(bmp1)
                        g1.Clear(Color.White)
                        PictureBox_Material1.Image = bmp1
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        g1.Clear(Color.White)
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                        Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                        graphics1.DrawImage(bmp1, New Point(0, 0))
                        graphics1.Dispose()
                    Else
                        g1 = Graphics.FromImage(bmp1)
                        g1.Clear(Color.White)
                        PictureBox_Material1.Image = bmp1
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        g1.Clear(Color.White)
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                        Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                        ShowIndicate(iIndex, enumFlashType.Waiting)
                        '  graphics1.DrawImage(bmp1, New Point(0, 0))
                        '  graphics1.Dispose()
                    End If
                Else
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    scaleX1 = 1
                    scaleY1 = 1
                    rectF1.Width = PictureBox_Material1.Width
                    rectF1.Height = PictureBox_Material1.Height
                    rectF1.X = 0
                    rectF1.Y = 0
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    If Not bFlash Then
                        g1 = Graphics.FromImage(bmp1)
                        g1.Clear(Color.White)
                        PictureBox_Material1.Image = bmp1
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        g1.Clear(Color.White)
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                        Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                        graphics1.DrawImage(bmp1, New Point(0, 0))
                        graphics1.Dispose()
                    End If
                End If
                If bFlash Then
                    ShowIndicate(iIndex, enumFlashType.Waiting)
                End If
                strLastPicPath = strFilePath
                bIsRunning = False
            Catch ex As Exception
                bIsRunning = False
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Function ShowIndicate(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                mMainForm.InvokeAction(cShowIndicate, iIndex, eFlashType)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Private Sub ShowIndicateAction(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType)
        SyncLock _Object
            Try
                Dim iR As Integer
                Dim iX As Integer
                Dim iY As Integer
                Dim iPointX As Integer
                Dim iPointY As Integer
                Dim strPosition As String = ""
                g1.Clear(Color.White)
                g1 = Graphics.FromImage(bmp1)
                g1.Clear(Color.White)
                g1.SmoothingMode = SmoothingMode.AntiAlias
                If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)
                iCurrentType = 2
                If lListPositionStatus.ContainsKey(iIndex) Then
                    lListPositionStatus(iIndex) = eFlashType
                End If
                For i = 0 To lListPosition.Count - 1

                    strPosition = lListPosition(lListPosition.Keys(i))
                    If strPosition = "" Then
                        iR = 20
                        bIsRunning = False
                        Return
                    End If
                    Dim cPosition() As String = strPosition.Split(",")
                    If cPosition.Length < 2 Then
                        iX = 0
                        iY = 0
                        iR = 20
                        bIsRunning = False
                        Return
                    End If

                    If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                        iR = 20
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
                    Dim black_left_width As Integer
                    Dim black_top_height As Integer

                    If (scaleX1 < scaleY1) Then
                        black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleX1) / 2)
                        black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleX1) / 2)
                        iPointX = iX * scaleX1 + black_left_width
                        iPointY = iY * scaleX1 + black_top_height
                    Else
                        black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleY1) / 2)
                        black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleY1) / 2)
                        iPointX = iX * scaleY1 + black_left_width
                        iPointY = iY * scaleY1 + black_top_height
                    End If

                    If lListPosition.Keys(i) < iIndex Then
                        If lListPositionStatus(lListPosition.Keys(i)) = enumFlashType.Fail Then
                            g1.FillEllipse(Brushes.Red, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        Else
                            g1.FillEllipse(Brushes.Lime, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        End If
                    End If



                    If lListPosition.Keys(i) = iIndex Then
                        Select Case eFlashType
                            Case enumFlashType.Fail
                                g1.FillEllipse(Brushes.Red, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Ongoing
                                g1.FillEllipse(Brushes.Blue, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Waiting
                                g1.FillEllipse(Brushes.Yellow, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Pass
                                g1.FillEllipse(Brushes.Lime, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        End Select
                    End If


                    If lListPosition.Keys(i) > iIndex Then
                        g1.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                    End If

                    Dim iSize As Integer = ChangeSize(iR, (i + 1).ToString)
                    Dim iH As Integer = g1.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height
                    Dim iW As Integer = g1.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width
                    ' g1.DrawString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold), New SolidBrush(Color.Black), New Point(iPointX - iW / 2, iPointY - iH / 2))
                Next
                iOldIndex = iIndex
                bOldResultFlashType = eFlashType
                Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                graphics1.DrawImage(bmp1, New Point(0, 0))
                graphics1.Dispose()
                bIsRunning = False
            Catch ex As Exception
                bIsRunning = False
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Private Function ChangeSize(ByVal iR As Integer, ByVal strValue As String) As Integer
        SyncLock _Object
            Dim iH As Integer = 2 * iR
            Dim iW As Integer = 2 * iR
            Dim iSize As Integer = 1
            Do While g1.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width < iW And g1.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height < iH
                iSize = iSize + 1
            Loop
            iSize = iSize - 2
            If iSize < 1 Then iSize = 1
            Return iSize
        End SyncLock
    End Function

    Public Function FlashIndicate(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                If iCnt = 0 Then
                    mMainForm.InvokeAction(cFlashIndicate, iIndex, eFlashType)
                End If
                If iCnt = 20 Then
                    mMainForm.InvokeAction(cFlashIndicate, iIndex, eFlashType)
                End If
                iCnt = iCnt + 1
                If iCnt > 40 Then iCnt = 0
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Private Sub FlashIndicateAction(ByVal iIndex As Integer, ByVal eFlashType As enumFlashType)
        SyncLock _Object
            Try
                iCurrentType = 3
                Dim iR As Integer
                Dim iX As Integer
                Dim iY As Integer
                Dim iPointX As Integer
                Dim iPointY As Integer
                Dim strPosition As String = ""
                If iPicType <> 1 Then Return
                g1.Clear(Color.White)
                g1.SmoothingMode = SmoothingMode.AntiAlias
                If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)

                For i = 0 To lListPosition.Count - 1

                    strPosition = lListPosition(lListPosition.Keys(i))
                    If strPosition = "" Then
                        iR = 20
                        bIsRunning = False
                        Return
                    End If
                    Dim cPosition() As String = strPosition.Split(",")
                    If cPosition.Length < 2 Then
                        iX = 0
                        iY = 0
                        iR = 20
                        bIsRunning = False
                        Return
                    End If

                    If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                        iR = 20
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
                    Dim black_left_width As Integer
                    Dim black_top_height As Integer

                    If (scaleX1 < scaleY1) Then
                        black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleX1) / 2)
                        black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleX1) / 2)
                        iPointX = iX * scaleX1 + black_left_width
                        iPointY = iY * scaleX1 + black_top_height
                    Else
                        black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleY1) / 2)
                        black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleY1) / 2)
                        iPointX = iX * scaleY1 + black_left_width
                        iPointY = iY * scaleY1 + black_top_height
                    End If

                    If lListPosition.Keys(i) < iIndex Then
                        If lListPositionStatus(lListPosition.Keys(i)) = enumFlashType.Fail Then
                            g1.FillEllipse(Brushes.Red, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        Else
                            g1.FillEllipse(Brushes.Lime, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        End If
                    End If



                    If lListPosition.Keys(i) = iIndex Then
                        Select Case eFlashType
                            Case enumFlashType.Waiting
                                If iCnt >= 20 Then g1.FillEllipse(Brushes.Yellow, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Ongoing
                                If iCnt >= 20 Then g1.FillEllipse(Brushes.Blue, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Fail
                                If iCnt >= 20 Then g1.FillEllipse(Brushes.Red, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                            Case enumFlashType.Pass
                                If iCnt >= 20 Then g1.FillEllipse(Brushes.Lime, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                        End Select

                    End If


                    If lListPosition.Keys(i) > iIndex Then
                        g1.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
                    End If

                    Dim iSize As Integer = ChangeSize(iR, (i + 1).ToString)
                    Dim iH As Integer = g1.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height
                    Dim iW As Integer = g1.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width
                Next


                Dim graphics As Graphics = PictureBox_Material1.CreateGraphics()
                graphics.DrawImage(bmp1, New Point(0, 0))
                graphics.Dispose()
                eOldFlashType = eFlashType
                iOldIndex = iIndex
                bIsRunning = False



            Catch ex As Exception
                bIsRunning = False
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub


    Public Function ShowActions(ByVal lListAction As List(Of clsShowActionCfg)) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                mMainForm.InvokeAction(cShowActions, lListAction)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Private Sub ShowActionsAction(ByVal lListAction As List(Of clsShowActionCfg))
        SyncLock _Object
            Try
                Dim bExit As Boolean = True
                If iPicType = 3 Then
                    If lOldListAction.Count = lListAction.Count Then
                        For i = 0 To lListAction.Count - 1
                            If lOldListAction(i) <> lListAction(i) Then
                                bExit = False
                            End If
                        Next
                    Else
                        bExit = False
                    End If
                Else
                    bExit = False
                End If
                If bExit Then
                    bIsRunning = False
                    Return
                End If

                iCurrentType = 4
                Dim strFilePath As String = ""
                Dim iR As Double
                Dim iX As Integer
                Dim iY As Integer
                Dim iPointX As Integer
                Dim iPointY As Integer
                Dim strPosition As String = ""
                Dim iIndex As Integer = 0
                Dim iCnt As Integer = 0
                If lListAction.Count < 5 Then
                    iCnt = 5
                Else
                    iCnt = lListAction.Count
                End If

                lstResults.Controls.Clear()
                Dim TableLayoutPanel_Tab_Main As New HMITableLayoutPanel
                TableLayoutPanel_Tab_Main.ColumnCount = 4
                TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
                TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
                TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
                TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
                TableLayoutPanel_Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill
                TableLayoutPanel_Tab_Main.Margin = New System.Windows.Forms.Padding(0)
                TableLayoutPanel_Tab_Main.Name = "TableLayoutPanel_Tab_Main"
                TableLayoutPanel_Tab_Main.RowCount = iCnt
                TableLayoutPanel_Tab_Main.Dock = DockStyle.Fill
                For j = 1 To iCnt
                    TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / iCnt * 1.0!))
                Next
                For j = 1 To iCnt
                    TableLayoutPanel_Tab_Main.RowStyles(j - 1) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / iCnt * 1.0!)
                Next
                lstResults.Controls.Add(TableLayoutPanel_Tab_Main)
                For i = 0 To lListAction.Count - 1
                    Dim Label_Index As New Label
                    Label_Index.Dock = System.Windows.Forms.DockStyle.Fill
                    Label_Index.Font = lstResults.Font
                    Label_Index.Name = (i + 1).ToString
                    Label_Index.Size = New System.Drawing.Size(223, 32)
                    Label_Index.TextAlign = ContentAlignment.MiddleCenter
                    Label_Index.Text = (i + 1).ToString
                    Label_Index.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
                    TableLayoutPanel_Tab_Main.Controls.Add(Label_Index, 0, i)

                    Dim Label_Component As New Label
                    Label_Component.Dock = System.Windows.Forms.DockStyle.Fill
                    Label_Component.Font = lstResults.Font
                    Label_Component.Name = (i + 1).ToString
                    Label_Component.Size = New System.Drawing.Size(223, 32)
                    Label_Component.TextAlign = ContentAlignment.MiddleCenter
                    Label_Component.Text = lListAction(i).Component
                    Label_Component.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
                    TableLayoutPanel_Tab_Main.Controls.Add(Label_Component, 1, i)
                    ChangeSize(Label_Component)

                    Dim Label_Description As New Label
                    Label_Description.Dock = System.Windows.Forms.DockStyle.Fill
                    Label_Description.Font = lstResults.Font
                    Label_Description.Name = (i + 1).ToString
                    Label_Description.Size = New System.Drawing.Size(223, 32)
                    Label_Description.TextAlign = ContentAlignment.MiddleLeft
                    Label_Description.Text = lListAction(i).Description.Replace("\r", vbCr).Replace("\n", vbLf)
                    Label_Description.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
                    TableLayoutPanel_Tab_Main.Controls.Add(Label_Description, 2, i)
                    ChangeSize(Label_Description)



                    strFilePath = ChangeKeyToPath(lListAction(i).PicturePath)

                    Dim PictureBox_Pic As New PictureBox
                    PictureBox_Pic.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Pic.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Pic.Margin = New System.Windows.Forms.Padding(3)
                    PictureBox_Pic.Name = "PictureBox_Pic"
                    PictureBox_Pic.Padding = New System.Windows.Forms.Padding(0)
                    PictureBox_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    TableLayoutPanel_Tab_Main.Controls.Add(PictureBox_Pic, 3, i)
                    If File.Exists(strFilePath) Then
                        ' img1 = Image.FromFile(strFilePath)
                        img1 = Nothing
                        Using file1 As New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
                            Dim img = FileCompress.CompressionImage(file1, 50)
                            img1 = FileCompress.BytesToImage(img)
                            file1.Close()
                        End Using
                        scaleX1 = PictureBox_Pic.Width * 1.0F / img1.Width
                        scaleY1 = PictureBox_Pic.Height * 1.0F / img1.Height
                        rectF1 = New RectangleF()
                        If (scaleX1 < scaleY1) Then
                            If scaleX1 < 1 Then
                                rectF1.Width = img1.Width * scaleX1
                                rectF1.Height = img1.Height * scaleX1
                            Else
                                scaleX1 = 1
                                rectF1.Width = img1.Width
                                rectF1.Height = img1.Height
                            End If

                        Else
                            If scaleY1 < 1 Then
                                rectF1.Width = img1.Width * scaleY1
                                rectF1.Height = img1.Height * scaleY1
                            Else
                                scaleY1 = 1
                                rectF1.Width = img1.Width
                                rectF1.Height = img1.Height
                            End If

                        End If
                        rectF1.X = (PictureBox_Pic.Width - rectF1.Width) / 2.0F
                        rectF1.Y = (PictureBox_Pic.Height - rectF1.Height) / 2.0F
                        bmp1 = New Bitmap(PictureBox_Pic.Width, PictureBox_Pic.Height)
                        g1 = Graphics.FromImage(bmp1)
                        PictureBox_Pic.Image = bmp1


                        g1.Clear(Color.White)
                        g1.SmoothingMode = SmoothingMode.AntiAlias
                        If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)

                        For j = 0 To lListAction(i).ListComponent.Count - 1
                            Dim cPictureCfg As clsPictureComponentCfg = lListAction(j).ListComponent(i)
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

                            If (scaleX1 < scaleY1) Then
                                black_left_width = IIf(img1.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img1.Width * scaleX1) / 2)
                                black_top_height = IIf(img1.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img1.Height * scaleX1) / 2)
                                iPointX = iX * scaleX1 + black_left_width
                                iPointY = iY * scaleX1 + black_top_height
                            Else
                                black_left_width = IIf(img1.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img1.Width * scaleY1) / 2)
                                black_top_height = IIf(img1.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img1.Height * scaleY1) / 2)
                                iPointX = iX * scaleY1 + black_left_width
                                iPointY = iY * scaleY1 + black_top_height
                            End If

                            If File.Exists(cPictureCfg.strPicturePath) Then
                                ' Dim imgCompontent As Image = Image.FromFile(cPictureCfg.strPicturePath)
                                Dim file2 As FileStream = New FileStream(cPictureCfg.strPicturePath, FileMode.Open)
                                Dim img2 = FileCompress.CompressionImage(file2, 50)
                                Dim imgCompontent As Image = FileCompress.BytesToImage(img2)
                                file2.Close()

                                Dim rectCompontent As RectangleF = New RectangleF()
                                rectCompontent.Width = imgCompontent.Width * iR * scaleX1
                                rectCompontent.Height = imgCompontent.Height * iR * scaleY1
                                rectCompontent.X = iPointX - rectCompontent.Width / 2
                                rectCompontent.Y = iPointY - rectCompontent.Height / 2

                                g1.DrawImage(imgCompontent, rectCompontent)
                            End If
                        Next
                        Dim graphics2 As Graphics = PictureBox_Pic.CreateGraphics()
                        graphics2.DrawImage(bmp1, New Point(0, 0))
                        graphics2.Dispose()
                    End If
                Next
                lOldListAction.Clear()
                For Each element As clsShowActionCfg In lListAction
                    lOldListAction.Add(element.Clone)
                Next
                bIsRunning = False
                iPicType = 3
            Catch ex As Exception
                bIsRunning = False
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Function CleanPosition() As Boolean
        SyncLock _Object
            strLastPicPath = ""
            lListPosition.Clear()
            lListPositionStatus.Clear()
            Return True
        End SyncLock
    End Function

    Private Sub ChangeSize(ByVal con As Control)
        SyncLock _Object
            Dim t As SizeF = con.CreateGraphics().MeasureString(con.Text, con.Font)
            Do While t.Height > con.Height - 15
                con.Font = New System.Drawing.Font(con.Font.Name, Single.Parse(con.Font.Size * 0.7), con.Font.Style)
                t = con.CreateGraphics().MeasureString(con.Text, con.Font)
            Loop
        End SyncLock
    End Sub

    Public Function AddPosition(ByVal iIndex As Integer, ByVal strPosition As String) As Boolean
        SyncLock _Object
            If lListPosition.ContainsKey(iIndex) Then
                lListPosition(iIndex) = strPosition
            Else
                lListPosition.Add(iIndex, strPosition)
            End If

            If lListPositionStatus.ContainsKey(iIndex) Then
                lListPositionStatus(iIndex) = enumFlashType.Waiting
            Else
                lListPositionStatus.Add(iIndex, enumFlashType.Waiting)
            End If
            Return True
        End SyncLock
    End Function
    Public Function ShowPictures(ByVal strFilePath1 As String, ByVal strFilePath2 As String) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                mMainForm.InvokeAction(cShowPictures, strFilePath1, strFilePath2)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ShowCurrentPictures(ByVal strFilePath1 As String, ByVal strFilePath2 As String) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                mMainForm.InvokeAction(cShowCurrentPictures, strFilePath1, strFilePath2)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function



    Private Sub ShowCurrentPicturesAction(ByVal strFilePath1 As String, ByVal strFilePath2 As String)
        SyncLock _Object
            Try
                iCurrentType = 7
                strFilePath1 = ChangeKeyToPath(strFilePath1)
                strFilePath2 = ChangeKeyToPath(strFilePath2)
                If iPicType <> 3 Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If

                    lstResults.Controls.Clear()
                    Dim TabControl As New TabControl
                    TabControl.Dock = System.Windows.Forms.DockStyle.Fill
                    TabControl.Font = lstResults.Font
                    TabControl.Location = New System.Drawing.Point(3, 3)
                    TabControl.Name = "TabControl"
                    TabControl.SelectedIndex = 0
                    TabControl.Size = New System.Drawing.Size(461, 524)
                    lstResults.Controls.Add(TabControl)

                    Dim SubTabPage1 As New TabPage
                    SubTabPage1.Margin = New System.Windows.Forms.Padding(0)
                    SubTabPage1.Padding = New System.Windows.Forms.Padding(0)
                    SubTabPage1.Dock = System.Windows.Forms.DockStyle.Fill
                    SubTabPage1.Font = lstResults.Font
                    SubTabPage1.Name = "Current"
                    SubTabPage1.Text = cLanguageManager.GetTextLine("PictureShowManager", "Current")
                    SubTabPage1.BackColor = Color.White
                    RemoveHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged
                    PictureBox_Material1 = New PictureBox
                    PictureBox_Material1.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Material1.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Material1.Margin = New System.Windows.Forms.Padding(0)
                    PictureBox_Material1.Name = "PictureBox_Material1"
                    PictureBox_Material1.Padding = New System.Windows.Forms.Padding(0)
                    PictureBox_Material1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    SubTabPage1.Controls.Add(PictureBox_Material1)
                    TabControl.Controls.Add(SubTabPage1)
                    AddHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged

                    Dim SubTabPage2 As New TabPage
                    SubTabPage2.Dock = System.Windows.Forms.DockStyle.Fill
                    SubTabPage2.Margin = New System.Windows.Forms.Padding(0)
                    SubTabPage2.Padding = New System.Windows.Forms.Padding(0)
                    SubTabPage2.Font = lstResults.Font
                    SubTabPage2.Name = "OK"
                    SubTabPage2.Text = cLanguageManager.GetTextLine("PictureShowManager", "OK")
                    SubTabPage2.BackColor = Color.White
                    PictureBox_Material2 = New PictureBox
                    PictureBox_Material2.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Material2.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Material2.Margin = New System.Windows.Forms.Padding(0)
                    PictureBox_Material2.Name = "PictureBox_Material2"
                    PictureBox_Material2.Padding = New System.Windows.Forms.Padding(0)
                    PictureBox_Material2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    SubTabPage2.Controls.Add(PictureBox_Material2)
                    TabControl.Controls.Add(SubTabPage2)
                    iPicType = 3
                End If
                Dim img1 As Image
                Dim scaleX1 As Single = 0
                Dim scaleY1 As Single = 0
                Dim rectF1 As RectangleF
                If File.Exists(strFilePath1) Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing

                    Using file1 As New FileStream(strFilePath1, FileMode.Open, FileAccess.Read)
                        Dim img = FileCompress.CompressionImage(file1, 50)
                        img1 = FileCompress.BytesToImage(img)
                        file1.Close()
                    End Using
                    scaleX1 = 0
                    scaleY1 = 0
                    scaleX1 = PictureBox_Material1.Width * 1.0F / img1.Width
                    scaleY1 = PictureBox_Material1.Height * 1.0F / img1.Height
                    rectF1 = New RectangleF()
                    If (scaleX1 < scaleY1) Then
                        If scaleX1 < 1 Then
                            rectF1.Width = img1.Width * scaleX1
                            rectF1.Height = img1.Height * scaleX1
                        Else
                            scaleX1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    Else
                        If scaleY1 < 1 Then
                            rectF1.Width = img1.Width * scaleY1
                            rectF1.Height = img1.Height * scaleY1
                        Else
                            scaleY1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    End If
                    rectF1.X = (PictureBox_Material1.Width - rectF1.Width) / 2.0F
                    rectF1.Y = (PictureBox_Material1.Height - rectF1.Height) / 2.0F
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material1.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                    img1.Dispose()
                Else
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    scaleX1 = 1
                    scaleY1 = 1
                    rectF1.Width = PictureBox_Material1.Width
                    rectF1.Height = PictureBox_Material1.Height
                    rectF1.X = 0
                    rectF1.Y = 0
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material1.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                End If

                If File.Exists(strFilePath2) Then
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If
                    img1 = Nothing
                    Using file1 As New FileStream(strFilePath2, FileMode.Open, FileAccess.Read)
                        Dim img = FileCompress.CompressionImage(file1, 50)
                        img1 = FileCompress.BytesToImage(img)
                        file1.Close()
                    End Using
                    scaleX1 = 0
                    scaleY1 = 0
                    scaleX1 = PictureBox_Material2.Width * 1.0F / img1.Width
                    scaleY1 = PictureBox_Material2.Height * 1.0F / img1.Height
                    rectF1 = New RectangleF()
                    If (scaleX1 < scaleY1) Then
                        If scaleX1 < 1 Then
                            rectF1.Width = img1.Width * scaleX1
                            rectF1.Height = img1.Height * scaleX1
                        Else
                            scaleX1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    Else
                        If scaleY1 < 1 Then
                            rectF1.Width = img1.Width * scaleY1
                            rectF1.Height = img1.Height * scaleY1
                        Else
                            scaleY1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    End If
                    rectF1.X = (PictureBox_Material2.Width - rectF1.Width) / 2.0F
                    rectF1.Y = (PictureBox_Material2.Height - rectF1.Height) / 2.0F
                    bmp1 = New Bitmap(PictureBox_Material2.Width, PictureBox_Material2.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material2.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material2.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                    img1.Dispose()
                Else
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If
                    img1 = Nothing
                    scaleX1 = 1
                    scaleY1 = 1
                    rectF1.Width = PictureBox_Material2.Width
                    rectF1.Height = PictureBox_Material2.Height
                    rectF1.X = 0
                    rectF1.Y = 0
                    bmp1 = New Bitmap(PictureBox_Material2.Width, PictureBox_Material2.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material2.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material2.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                End If

                strOldFilePath1 = strFilePath1
                strOldFilePath2 = strFilePath2
                bIsRunning = False
            Catch ex As Exception
                bIsRunning = False
                Return
            End Try
        End SyncLock
    End Sub

    Private Sub ShowPicturesAction(ByVal strFilePath1 As String, ByVal strFilePath2 As String)
        SyncLock _Object
            Try
                iCurrentType = 5
                strFilePath1 = ChangeKeyToPath(strFilePath1)
                strFilePath2 = ChangeKeyToPath(strFilePath2)
                If iPicType <> 2 Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If
                    lstResults.Controls.Clear()
                    Dim TabControl As New TabControl
                    TabControl.Dock = System.Windows.Forms.DockStyle.Fill
                    TabControl.Font = lstResults.Font
                    TabControl.Location = New System.Drawing.Point(3, 3)
                    TabControl.Name = "TabControl"
                    TabControl.SelectedIndex = 0
                    TabControl.Size = New System.Drawing.Size(461, 524)
                    lstResults.Controls.Add(TabControl)

                    Dim SubTabPage1 As New TabPage
                    SubTabPage1.Margin = New System.Windows.Forms.Padding(0)
                    SubTabPage1.Padding = New System.Windows.Forms.Padding(0)
                    SubTabPage1.Dock = System.Windows.Forms.DockStyle.Fill
                    SubTabPage1.Font = lstResults.Font
                    SubTabPage1.Name = "NG"
                    SubTabPage1.Text = cLanguageManager.GetTextLine("PictureShowManager", "NG")
                    SubTabPage1.BackColor = Color.White
                    RemoveHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged
                    PictureBox_Material1 = New PictureBox
                    PictureBox_Material1.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Material1.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Material1.Margin = New System.Windows.Forms.Padding(0)
                    PictureBox_Material1.Name = "PictureBox_Material1"
                    PictureBox_Material1.Padding = New System.Windows.Forms.Padding(0)
                    PictureBox_Material1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    SubTabPage1.Controls.Add(PictureBox_Material1)
                    TabControl.Controls.Add(SubTabPage1)
                    AddHandler PictureBox_Material1.SizeChanged, AddressOf Picture_SizeChanged

                    Dim SubTabPage2 As New TabPage
                    SubTabPage2.Dock = System.Windows.Forms.DockStyle.Fill
                    SubTabPage2.Margin = New System.Windows.Forms.Padding(0)
                    SubTabPage2.Padding = New System.Windows.Forms.Padding(0)
                    SubTabPage2.Font = lstResults.Font
                    SubTabPage2.Name = "OK"
                    SubTabPage2.Text = cLanguageManager.GetTextLine("PictureShowManager", "OK")
                    SubTabPage2.BackColor = Color.White
                    PictureBox_Material2 = New PictureBox
                    PictureBox_Material2.Dock = System.Windows.Forms.DockStyle.Fill
                    PictureBox_Material2.Location = New System.Drawing.Point(0, 0)
                    PictureBox_Material2.Margin = New System.Windows.Forms.Padding(0)
                    PictureBox_Material2.Name = "PictureBox_Material2"
                    PictureBox_Material2.Padding = New System.Windows.Forms.Padding(0)
                    PictureBox_Material2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                    SubTabPage2.Controls.Add(PictureBox_Material2)
                    TabControl.Controls.Add(SubTabPage2)
                    iPicType = 2
                End If
                Dim img1 As Image
                Dim scaleX1 As Single = 0
                Dim scaleY1 As Single = 0
                Dim rectF1 As RectangleF
                If File.Exists(strFilePath1) Then
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    Using file1 As New FileStream(strFilePath1, FileMode.Open, FileAccess.Read)
                        Dim img = FileCompress.CompressionImage(file1, 50)
                        img1 = FileCompress.BytesToImage(img)
                        file1.Close()
                    End Using
                    scaleX1 = 0
                    scaleY1 = 0
                    scaleX1 = PictureBox_Material1.Width * 1.0F / img1.Width
                    scaleY1 = PictureBox_Material1.Height * 1.0F / img1.Height
                    rectF1 = New RectangleF()
                    If (scaleX1 < scaleY1) Then
                        If scaleX1 < 1 Then
                            rectF1.Width = img1.Width * scaleX1
                            rectF1.Height = img1.Height * scaleX1
                        Else
                            scaleX1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    Else
                        If scaleY1 < 1 Then
                            rectF1.Width = img1.Width * scaleY1
                            rectF1.Height = img1.Height * scaleY1
                        Else
                            scaleY1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    End If
                    rectF1.X = (PictureBox_Material1.Width - rectF1.Width) / 2.0F
                    rectF1.Y = (PictureBox_Material1.Height - rectF1.Height) / 2.0F
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material1.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                    img1.Dispose()
                Else
                    If Not IsNothing(PictureBox_Material1.Image) Then
                        PictureBox_Material1.Image.Dispose()
                        PictureBox_Material1.Image = Nothing
                    End If
                    img1 = Nothing
                    scaleX1 = 1
                    scaleY1 = 1
                    rectF1.Width = PictureBox_Material1.Width
                    rectF1.Height = PictureBox_Material1.Height
                    rectF1.X = 0
                    rectF1.Y = 0
                    bmp1 = New Bitmap(PictureBox_Material1.Width, PictureBox_Material1.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material1.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material1.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                End If

                If File.Exists(strFilePath2) Then
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If
                    img1 = Nothing
                    Using file1 As New FileStream(strFilePath2, FileMode.Open, FileAccess.Read)
                        Dim img = FileCompress.CompressionImage(file1, 50)
                        img1 = FileCompress.BytesToImage(img)
                        file1.Close()
                    End Using
                    scaleX1 = 0
                    scaleY1 = 0
                    scaleX1 = PictureBox_Material2.Width * 1.0F / img1.Width
                    scaleY1 = PictureBox_Material2.Height * 1.0F / img1.Height
                    rectF1 = New RectangleF()
                    If (scaleX1 < scaleY1) Then
                        If scaleX1 < 1 Then
                            rectF1.Width = img1.Width * scaleX1
                            rectF1.Height = img1.Height * scaleX1
                        Else
                            scaleX1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    Else
                        If scaleY1 < 1 Then
                            rectF1.Width = img1.Width * scaleY1
                            rectF1.Height = img1.Height * scaleY1
                        Else
                            scaleY1 = 1
                            rectF1.Width = img1.Width
                            rectF1.Height = img1.Height
                        End If

                    End If
                    rectF1.X = (PictureBox_Material2.Width - rectF1.Width) / 2.0F
                    rectF1.Y = (PictureBox_Material2.Height - rectF1.Height) / 2.0F
                    bmp1 = New Bitmap(PictureBox_Material2.Width, PictureBox_Material2.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material2.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material2.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                    img1.Dispose()
                Else
                    If Not IsNothing(PictureBox_Material2.Image) Then
                        PictureBox_Material2.Image.Dispose()
                        PictureBox_Material2.Image = Nothing
                    End If
                    img1 = Nothing
                    scaleX1 = 1
                    scaleY1 = 1
                    rectF1.Width = PictureBox_Material2.Width
                    rectF1.Height = PictureBox_Material2.Height
                    rectF1.X = 0
                    rectF1.Y = 0
                    bmp1 = New Bitmap(PictureBox_Material2.Width, PictureBox_Material2.Height)
                    g1 = Graphics.FromImage(bmp1)
                    g1.Clear(Color.White)
                    PictureBox_Material2.Image = bmp1
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(img1) Then g1.DrawImage(img1, rectF1)
                    Dim graphics1 As Graphics = PictureBox_Material2.CreateGraphics()
                    graphics1.DrawImage(bmp1, New Point(0, 0))
                    graphics1.Dispose()
                End If

                strOldFilePath1 = strFilePath1
                strOldFilePath2 = strFilePath2
                bIsRunning = False
            Catch ex As Exception
                bIsRunning = False
                Return
            End Try
        End SyncLock
    End Sub

    Public Function ShowComponent(ByVal lListCompontent As List(Of clsPictureComponentCfg)) As Boolean
        SyncLock _Object
            Try
                bIsRunning = True
                mMainForm.InvokeAction(cShowComponents, lListCompontent)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function


    Private Sub ShowComponentAction(ByVal lListCompontent As List(Of clsPictureComponentCfg))
        SyncLock _Object
            Try

                iCurrentType = 6
                lOldListCompontent = lListCompontent
                If iPicType = 1 Then
                    Dim iR As Double
                    Dim iX As Integer
                    Dim iY As Integer
                    Dim iPointX As Integer
                    Dim iPointY As Integer
                    Dim strPosition As String = ""
                    Dim iIndex As Integer = 0

                    g1.Clear(Color.White)
                    g1.SmoothingMode = SmoothingMode.AntiAlias
                    If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)

                    For i = 0 To lListCompontent.Count - 1
                        Dim cPictureCfg As clsPictureComponentCfg = lListCompontent(i)
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

                        If (scaleX1 < scaleY1) Then
                            black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleX1) / 2)
                            black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleX1) / 2)
                            iPointX = iX * scaleX1 + black_left_width
                            iPointY = iY * scaleX1 + black_top_height
                        Else
                            black_left_width = IIf(img1.Width = PictureBox_Material1.Width, 0, (PictureBox_Material1.Width - img1.Width * scaleY1) / 2)
                            black_top_height = IIf(img1.Height = PictureBox_Material1.Height, 0, (PictureBox_Material1.Height - img1.Height * scaleY1) / 2)
                            iPointX = iX * scaleY1 + black_left_width
                            iPointY = iY * scaleY1 + black_top_height
                        End If

                        If File.Exists(cPictureCfg.strPicturePath) Then
                            Dim imgCompontent As Image
                            Using file2 As New FileStream(cPictureCfg.strPicturePath, FileMode.Open, FileAccess.Read)
                                Dim img2 = FileCompress.CompressionImage(file2, 50)
                                imgCompontent = FileCompress.BytesToImage(img2)
                                file2.Close()
                            End Using

                            Dim rectCompontent As RectangleF = New RectangleF()
                            rectCompontent.Width = imgCompontent.Width * iR
                            rectCompontent.Height = imgCompontent.Height * iR
                            rectCompontent.X = iPointX - rectCompontent.Width / 2
                            rectCompontent.Y = iPointY - rectCompontent.Height / 2
                            g1.DrawImage(imgCompontent, rectCompontent)
                        End If
                    Next

                    Dim graphics As Graphics = PictureBox_Material1.CreateGraphics()
                    graphics.DrawImage(bmp1, New Point(0, 0))
                    graphics.Dispose()
                End If
                bIsRunning = False
            Catch ex As Exception
                bIsRunning = False
                Return
            End Try
        End SyncLock
    End Sub

    Public Sub PageChanged(ByVal strName As String, ByVal strCurrentName As String, ByVal eMainButtonType As enumMainButtonType)
        ' cMainFormButtonManager.EnableFunctionMainButton(cMachineStatusManager.MachineStatus, cUserManager.CurrentUserCfg.Level)
        If strCurrentName = enumHMI_LEFT_ITEM.Home.ToString Then
            Select Case iCurrentType
                Case 1
                    ShowPicture(strLastPicPath)
                Case 2
                    ShowPicture(strLastPicPath)
                    ShowIndicate(iOldIndex, bOldResultFlashType)
                Case 3
                    ShowPicture(strLastPicPath)
                    FlashIndicate(iOldIndex, eOldFlashType)
                    ShowIndicate(iOldIndex, eOldFlashType)
                Case 4
                    ShowActions(lOldListAction)
                Case 5
                    ShowPictures(strOldFilePath1, strOldFilePath2)
                Case 6
                    ShowPicture(strLastPicPath)
                    ShowComponent(lOldListCompontent)
                Case 7
                    ShowCurrentPictures(strOldFilePath1, strOldFilePath2)
            End Select
        End If
    End Sub

    Public Sub TapChanged()
        ' cMainFormButtonManager.EnableFunctionMainButton(cMachineStatusManager.MachineStatus, cUserManager.CurrentUserCfg.Level)
        Select Case iCurrentType
            Case 1
                ShowPicture(strLastPicPath)
            Case 2
                ShowPicture(strLastPicPath)
                ShowIndicate(iOldIndex, bOldResultFlashType)
            Case 3
                ShowPicture(strLastPicPath)
                FlashIndicate(iOldIndex, eOldFlashType)
                ShowIndicate(iOldIndex, eOldFlashType)
            Case 4
                ShowActions(lOldListAction)
            Case 5
                ShowPictures(strOldFilePath1, strOldFilePath2)
            Case 6
                ShowPicture(strLastPicPath)
                ShowComponent(lOldListCompontent)
            Case 7
                ShowCurrentPictures(strOldFilePath1, strOldFilePath2)
        End Select
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next
        MyBase.Finalize()
    End Sub
#End Region
End Class

Public Enum enumFlashType
    Waiting = 0
    Ongoing
    Fail
    Pass
End Enum

Public Class clsShowActionCfg
    Private strComponent As String = ""
    Private strDescription As String = ""
    Private strPicturePath As String = ""
    Private lListComponent As New List(Of clsPictureComponentCfg)

    Public Property ListComponent As List(Of clsPictureComponentCfg)
        Set(ByVal value As List(Of clsPictureComponentCfg))
            lListComponent = value
        End Set
        Get
            Return lListComponent
        End Get
    End Property

    Public Property Component As String
        Set(ByVal value As String)
            strComponent = value
        End Set
        Get
            Return strComponent
        End Get
    End Property

    Public Property Description As String
        Set(ByVal value As String)
            strDescription = value
        End Set
        Get
            Return strDescription
        End Get
    End Property

    Public Property PicturePath As String
        Set(ByVal value As String)
            strPicturePath = value
        End Set
        Get
            Return strPicturePath
        End Get
    End Property

    Sub New()

    End Sub

    Sub New(ByVal strComponent As String, ByVal strDescription As String, ByVal strPicturePath As String, ByVal lListComponent As List(Of clsPictureComponentCfg))
        Me.strComponent = strComponent
        Me.strDescription = strDescription
        Me.strPicturePath = strPicturePath
        Me.lListComponent = lListComponent
    End Sub

    Public Function Clone() As clsShowActionCfg
        Dim cTempShowActionCfg As New clsShowActionCfg
        cTempShowActionCfg.strComponent = strComponent
        cTempShowActionCfg.strDescription = strDescription
        cTempShowActionCfg.strPicturePath = strPicturePath
        cTempShowActionCfg.lListComponent.Clear()
        For Each elment As clsPictureComponentCfg In lListComponent
            cTempShowActionCfg.lListComponent.Add(elment.Clone)
        Next
        Return cTempShowActionCfg
    End Function

    Public Shared Operator <>(ByVal x As clsShowActionCfg, ByVal y As clsShowActionCfg) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        If x.strComponent = y.strComponent Then Return False
        If x.strDescription = y.strDescription Then Return False
        If x.strPicturePath = y.strPicturePath Then Return False
        If x.ListComponent.Count <> y.ListComponent.Count Then Return True
        For i = 0 To x.ListComponent.Count - 1
            If x.ListComponent(i) = y.ListComponent(i) Then
                Return False
            End If
        Next
        Return True
    End Operator
    Public Shared Operator =(ByVal x As clsShowActionCfg, ByVal y As clsShowActionCfg) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        If x.strComponent <> y.strComponent Then Return False
        If x.strDescription <> y.strDescription Then Return False
        If x.strPicturePath <> y.strPicturePath Then Return False
        If x.ListComponent.Count <> y.ListComponent.Count Then Return False
        For i = 0 To x.ListComponent.Count - 1
            If x.ListComponent(i) <> y.ListComponent(i) Then
                Return False
            End If
        Next
        Return True
    End Operator

End Class

Public Class clsPictureComponentCfg
    Public strPicturePath As String
    Public strPicturePosition As String
    Sub New()

    End Sub
    Sub New(ByVal PicturePath As String, ByVal PicturePosition As String)
        strPicturePath = PicturePath
        strPicturePosition = PicturePosition
    End Sub

    Public Function Clone() As clsPictureComponentCfg
        Dim cTempPictureComponentCfg As New clsPictureComponentCfg
        cTempPictureComponentCfg.strPicturePath = strPicturePath
        cTempPictureComponentCfg.strPicturePosition = strPicturePosition
        Return cTempPictureComponentCfg
    End Function

    Public Shared Operator <>(ByVal x As clsPictureComponentCfg, ByVal y As clsPictureComponentCfg) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        If x.strPicturePath = y.strPicturePath Then Return False
        If x.strPicturePosition = y.strPicturePosition Then Return False
        Return True
    End Operator
    Public Shared Operator =(ByVal x As clsPictureComponentCfg, ByVal y As clsPictureComponentCfg) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        If x.strPicturePath <> y.strPicturePath Then Return False
        If x.strPicturePosition <> y.strPicturePosition Then Return False
        Return True
    End Operator
End Class

Public Class FileCompress

    Public Shared Function BytesToImage(ByVal buffer() As Byte) As Image
        Dim ms As MemoryStream = New MemoryStream(buffer)
        Dim image As Image = System.Drawing.Image.FromStream(ms)
        Return image
    End Function
    Public Shared Function CreateImageFromBytes(ByVal fileName As String, ByVal buffer() As Byte) As String
        Dim file As String = fileName
        Dim image As Image = BytesToImage(buffer)
        Dim format As ImageFormat = image.RawFormat
        If format.Equals(ImageFormat.Jpeg) Then
            file += ".jpeg"
        ElseIf format.Equals(ImageFormat.Png) Then
            file += ".png"
        ElseIf format.Equals(ImageFormat.Bmp) Then
            file += ".bmp"
        ElseIf format.Equals(ImageFormat.Gif) Then
            file += ".gif"
        ElseIf format.Equals(ImageFormat.Icon) Then
            file += ".icon"
        End If

        Dim info As System.IO.FileInfo = New System.IO.FileInfo(file)
        System.IO.Directory.CreateDirectory(info.Directory.FullName)
        System.IO.File.WriteAllBytes(file, buffer)
        Return file
    End Function

    Public Shared Function CompressionImage(ByVal fileStream As Stream, ByVal quality As Long) As Byte()
        Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(fileStream)
        Dim Bitmap As Bitmap = New Bitmap(img)
        Dim CodecInfo As ImageCodecInfo = GetEncoder(img.RawFormat)
        Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
        Dim myEncoderParameters As EncoderParameters = New EncoderParameters(1)
        Dim myEncoderParameter As EncoderParameter = New EncoderParameter(myEncoder, quality)
        myEncoderParameters.Param(0) = myEncoderParameter
        Dim MS As MemoryStream = New MemoryStream()
        Bitmap.Save(MS, CodecInfo, myEncoderParameters)
        myEncoderParameters.Dispose()
        myEncoderParameter.Dispose()

        MS.Close()
        MS.Dispose()
        img.Dispose()
        fileStream.Close()
        fileStream.Dispose()
        Bitmap.Dispose()
        Bitmap.Dispose()
        Return MS.ToArray()
    End Function

    Public Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo
        Dim codecs() As ImageCodecInfo = ImageCodecInfo.GetImageDecoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next
        Return Nothing
    End Function


       
End Class