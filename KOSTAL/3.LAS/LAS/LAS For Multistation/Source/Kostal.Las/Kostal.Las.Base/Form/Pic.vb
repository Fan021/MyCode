Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image
Imports System.Windows.Forms
Public Class LAS
    Private _FileHandler As New FileHandler
    Private _XmlHandler As New XmlHandler
    Private _Settings As New Settings
    Private _Language As Language
    Private _i As New Station
    Private _Log As Logger
    Private _PicImage As Image
    Private _X As Integer
    Private _Y As Integer
    Private _W As Integer
    Private _H As Integer
    Private sResult As String
    Private ListPicture As New Dictionary(Of String, PicElement)
    Private _Inifile As String
    Private _DisplayNewPart As Boolean
    Private _iCurrentPicIndex As Integer
    Private _DisplayScanner As New Dictionary(Of String, String)
    Private StartPointDrawing As New Point
    Private StopPointDrawing As New Point
    Private StartPointCut As New Point
    Private StopPointCut As New Point
    Private StartPointCutSave As New Point
    Private StopPointCutSave As New Point
    Private _ImageProcess As Image
    Private _mPassword As PassWordForm

    Public ReadOnly Property DisplayScanner As Dictionary(Of String, String)
        Get
            Return _DisplayScanner
        End Get
    End Property

    Public ReadOnly Property DisplayNewPart As Boolean
        Get
            Return _DisplayNewPart
        End Get
    End Property

    Public ReadOnly Property Type As Label
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Msg As Label
        Get
            Return Label_Msg
        End Get
    End Property


    Public Function Init(ByVal MyParent As Station, ByVal Inifile As String, ByVal MySettings As Settings, ByVal MyLanguage As Language) As Boolean

        _Settings = MySettings
        _Language = MyLanguage
        _Log = New Logger(_Settings)
        _i.Name = "ShowPicture"
        _i.IdString = MyParent.IdString + "." + _i.Name
        _Inifile = Inifile
        _mPassword = New PassWordForm
        _mPassword.Init(_i, _Settings, "UserPassWord")


        ReadDataFromXml("Position", "X", _X)
        ReadDataFromXml("Position", "Y", _Y)
        ReadDataFromXml("Size", "Width", _W)
        ReadDataFromXml("Size", "Height", _H)
        ReadDataFromXml("DisPlayNewPart", "Enable", _DisplayNewPart)

        Me.Location = New Point(_X, _Y)
        Me.Width = _W
        Me.Height = _H
        TableLayoutPanel1.Visible = _DisplayNewPart


        If Not Add() Then ShowErrorMsg(enumLK_TEXT.LK_TEXT_SHOWPIC_ADD, "FAIL")
        If Not AddPic() Then ShowErrorMsg(enumLK_TEXT.LK_TEXT_INIT, "FAIL")
        ReLoadLanguage()
        Me.Show()
        ShowErrorMsg(enumLK_TEXT.LK_TEXT_INIT, "Successful", "", True)
        Return True
    End Function

    Public Function ReLoadLanguage() As Boolean
        _Language.ReadControlText(Me)
        _Language.ReadContextMenuStrip(ContextMenuStrip_Pic)
        Return True
    End Function

    Private Function AddPic() As Boolean
        For Each tempPicElement As PicElement In ListPicture.Values
            tempPicElement.GroupBox = New GroupBox
            tempPicElement.GroupBox.Location = New System.Drawing.Point(tempPicElement.FilePositionX, tempPicElement.FilePositionY)
            tempPicElement.GroupBox.Location = New System.Drawing.Point(tempPicElement.FilePositionX, tempPicElement.FilePositionY)
            tempPicElement.GroupBox.Name = tempPicElement.id
            tempPicElement.GroupBox.Text = tempPicElement.strFileName
            tempPicElement.GroupBox.Size = New System.Drawing.Size(tempPicElement.FileWidth, tempPicElement.FileHeight)

            tempPicElement.PictureBox = New PictureBox
            tempPicElement.PictureBox.Location = New System.Drawing.Point(3, 17)
            tempPicElement.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill
            tempPicElement.PictureBox.Name = tempPicElement.id
            tempPicElement.GroupBox.ContextMenuStrip = ContextMenuStrip_Pic
            tempPicElement.PictureBox.Size = New System.Drawing.Size(tempPicElement.FileWidth - 40, tempPicElement.FileHeight - 40)
            tempPicElement.GroupBox.Controls.Add(tempPicElement.PictureBox)
            AddHandler tempPicElement.GroupBox.MouseDown, AddressOf PictureBox_MouseDown
            AddHandler tempPicElement.GroupBox.MouseMove, AddressOf PictureBox_MouseMove
            AddHandler tempPicElement.GroupBox.MouseUp, AddressOf PictureBox_MouseUp
            AddHandler tempPicElement.PictureBox.MouseDown, AddressOf PictureBox_MouseDown
            AddHandler tempPicElement.PictureBox.MouseMove, AddressOf PictureBox_MouseMove
            AddHandler tempPicElement.PictureBox.MouseUp, AddressOf PictureBox_MouseUp
            Me.Controls.Add(tempPicElement.GroupBox)
        Next

        Return True
    End Function

    Private Function Add() As Boolean
        Dim tempPicElement As PicElement
        For Each element As Dictionary(Of String, Object) In _XmlHandler.GetAnyListFromXml(_Settings.ConfigFolder, _Inifile, "DisPlayScanners", "DisPlayScanner", New String() {"Name"})
            If CType(("Name"), String) <> XmlHandler.s_DEFAULT And CType(element("Name"), String) <> XmlHandler.s_Null Then
                If Not _DisplayScanner.ContainsKey(CType(element("Name"), String)) Then
                    _DisplayScanner.Add(CType(element("Name"), String), CType(element("Name"), String))
                Else
                    ShowErrorMsg(enumLK_TEXT.LK_TEXT_SHOWPIC_ADD, "Name")
                End If
            End If
        Next
        For Each element As Dictionary(Of String, Object) In _XmlHandler.GetAnyListFromXml(_Settings.ConfigFolder, _Inifile, "Pictures", "Picture", New String() {"Name", "Folder", "PositionX", "PositionY", "Width", "Height", "Type", "CutEnable", "StartCutPointX", "StartCutPointY", "StopCutPointX", "StopCutPointY"})
            tempPicElement = New PicElement
            tempPicElement.id = (ListPicture.Count + 1).ToString
            If CType(element("Name"), String) <> XmlHandler.s_DEFAULT And CType(element("Name"), String) <> XmlHandler.s_Null Then
                tempPicElement.strFileName = CType(element("Name"), String)
                tempPicElement.strFilePath = CType(element("Folder"), String)
                ChangeDataToObject(CType(element("PositionX"), String), tempPicElement.FilePositionX)
                ChangeDataToObject(CType(element("PositionY"), String), tempPicElement.FilePositionY)
                ChangeDataToObject(CType(element("Height"), String), tempPicElement.FileHeight)
                ChangeDataToObject(CType(element("Width"), String), tempPicElement.FileWidth)
                ChangeDataToObject(CType(element("CutEnable"), String), tempPicElement.CutEnable)
                tempPicElement.Type = PicElement.ChangeType(CType(element("Type"), String))
                ChangeDataToObject(CType(element("StartCutPointX"), String), tempPicElement.StartCutPoint, CType(element("StartCutPointY"), String))
                ChangeDataToObject(CType(element("StopCutPointX"), String), tempPicElement.StopCutPoint, CType(element("StopCutPointY"), String))
                ListPicture.Add(tempPicElement.id, tempPicElement)
            End If
        Next
        Return True
    End Function



    Public Overloads Function ShowPic(ByVal mArticle As String, ByVal mSN As String, ByVal mPicName() As String) As Boolean
        Dim tempPicElement As PicElement

        If ListPicture.Values.Count <> mPicName.Count Then
            ShowErrorMsg(enumLK_TEXT.LK_TEXT_SHOWPIC_COUNT, ListPicture.Values.Count.ToString, mPicName.Count.ToString)
            Return False
        End If

        For i = 0 To ListPicture.Values.Count - 1
            tempPicElement = ListPicture(ListPicture.Keys(i))
            If _FileHandler.FileExist(_Settings.ApplicationFolder + tempPicElement.strFilePath + "\" + mPicName(i)) Then
                If tempPicElement.CutEnable Then
                    Dim img As Image = Image.FromFile(_Settings.ApplicationFolder + tempPicElement.strFilePath + "\" + mPicName(i))
                    Dim bmp As Image = New Bitmap(img)
                    img.Dispose()
                    ImageProcess.CusForCustom(bmp, _ImageProcess, tempPicElement.StartCutPoint, tempPicElement.StopCutPoint)
                    tempPicElement.PictureBox.Image = _ImageProcess
                    tempPicElement.PictureBox.SizeMode = tempPicElement.Type
                    tempPicElement.GroupBox.Location = New Point(tempPicElement.FilePositionX, tempPicElement.FilePositionY)
                    tempPicElement.GroupBox.Size = New System.Drawing.Size(tempPicElement.FileWidth, tempPicElement.FileHeight)
                    tempPicElement.GroupBox.Text = tempPicElement.strFileName + "  " + mArticle + "  " + mSN
                Else
                    Dim img As Image = Image.FromFile(_Settings.ApplicationFolder + tempPicElement.strFilePath + "\" + mPicName(i))
                    Dim bmp As Image = New Bitmap(img)
                    img.Dispose()
                    tempPicElement.PictureBox.Image = bmp
                    tempPicElement.PictureBox.SizeMode = tempPicElement.Type
                    tempPicElement.GroupBox.Location = New Point(tempPicElement.FilePositionX, tempPicElement.FilePositionY)
                    tempPicElement.GroupBox.Size = New System.Drawing.Size(tempPicElement.FileWidth, tempPicElement.FileHeight)
                    tempPicElement.GroupBox.Text = tempPicElement.strFileName + "  " + mArticle + "  " + mSN
                End If
            Else
                tempPicElement.PictureBox.Image = Image.FromFile(_Settings.PicFolder + "Error.jpg")
                tempPicElement.PictureBox.SizeMode = tempPicElement.Type
                tempPicElement.GroupBox.Location = New Point(tempPicElement.FilePositionX, tempPicElement.FilePositionY)
                tempPicElement.GroupBox.Size = New System.Drawing.Size(tempPicElement.FileWidth, tempPicElement.FileHeight)
                tempPicElement.GroupBox.Text = tempPicElement.strFileName + "  " + mArticle + "  " + mSN
            End If
        Next
        Me.Show()
        Return True
    End Function

    Public Function DelectPic() As Boolean
        Dim tempPicElement As PicElement
        For i = 0 To ListPicture.Values.Count - 1
            tempPicElement = ListPicture(ListPicture.Keys(i))
            tempPicElement.GroupBox.Text = tempPicElement.strFileName
            tempPicElement.PictureBox.Image = Nothing
        Next
        Return True
    End Function
    Public Overloads Function ShowPic(ByVal iIndex As Integer) As Boolean
        If _FileHandler.FileExist(ListPicture(ListPicture.Keys(iIndex)).strFileFullPath) Then
            Dim img As Image = Image.FromFile(ListPicture(ListPicture.Keys(iIndex)).strFileFullPath)
            Dim bmp As Image = New Bitmap(img)
            img.Dispose()
            ListPicture(ListPicture.Keys(iIndex)).PictureBox.Image = bmp
            ListPicture(ListPicture.Keys(iIndex)).PictureBox.SizeMode = ListPicture(ListPicture.Keys(iIndex)).Type
        End If
        Return True
    End Function

    Public Overloads Function ShowPicDefault(ByVal iIndex As Integer) As Boolean
        If ListPicture(ListPicture.Keys(iIndex)).strFileFullPath <> "" Then
            If _FileHandler.FileExist(ListPicture(ListPicture.Keys(iIndex)).strFileFullPath) Then
                ListPicture(ListPicture.Keys(iIndex)).PictureBox.Image = Image.FromFile(ListPicture(ListPicture.Keys(iIndex)).strFileFullPath)
                ListPicture(ListPicture.Keys(iIndex)).PictureBox.SizeMode = ListPicture(ListPicture.Keys(iIndex)).Type
                ListPicture(ListPicture.Keys(iIndex)).GroupBox.Location = New Point(ListPicture(ListPicture.Keys(iIndex)).FilePositionX, ListPicture(ListPicture.Keys(iIndex)).FilePositionY)
                ListPicture(ListPicture.Keys(iIndex)).GroupBox.Size = New System.Drawing.Size(ListPicture(ListPicture.Keys(iIndex)).FileWidth, ListPicture(ListPicture.Keys(iIndex)).FileHeight)
            End If
        Else
            ListPicture(ListPicture.Keys(iIndex)).PictureBox.SizeMode = ListPicture(ListPicture.Keys(iIndex)).Type
            ListPicture(ListPicture.Keys(iIndex)).GroupBox.Location = New Point(ListPicture(ListPicture.Keys(iIndex)).FilePositionX, ListPicture(ListPicture.Keys(iIndex)).FilePositionY)
            ListPicture(ListPicture.Keys(iIndex)).GroupBox.Size = New System.Drawing.Size(ListPicture(ListPicture.Keys(iIndex)).FileWidth, ListPicture(ListPicture.Keys(iIndex)).FileHeight)
        End If
        Return True
    End Function

    Private Sub DisableItem(ByVal iIndex As Integer, ByVal bResult As Boolean)
        For i = 0 To ContextMenuStrip_Pic.Items.Count
            If i = iIndex Then
                ContextMenuStrip_Pic.Items(iIndex).Enabled = bResult
            End If
        Next
    End Sub

    Private Sub ContextMenuStrip_Pic_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip_Pic.Opening
        _iCurrentPicIndex = CInt(CType(sender, ContextMenuStrip).SourceControl.Name)
        UpdateItem()
    End Sub

    Private Sub UpdateItem()

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "" Then
            DisableItem(0, True)
            DisableItem(1, False)
            DisableItem(2, False)
            DisableItem(3, True)
            DisableItem(4, True)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Open" Then
            DisableItem(0, True)
            DisableItem(1, True)
            DisableItem(2, False)
            DisableItem(3, True)
            DisableItem(4, True)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Save" Then
            DisableItem(0, True)
            DisableItem(1, False)
            DisableItem(2, False)
            DisableItem(3, True)
            DisableItem(4, True)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Select" Then
            DisableItem(0, True)
            DisableItem(1, False)
            DisableItem(2, True)
            DisableItem(3, False)
            DisableItem(4, False)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Move" Then
            DisableItem(0, True)
            DisableItem(1, True)
            DisableItem(2, False)
            DisableItem(3, False)
            DisableItem(4, True)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Zoom" Then
            DisableItem(0, True)
            DisableItem(1, True)
            DisableItem(2, False)
            DisableItem(3, True)
            DisableItem(4, False)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Cut" Then
            DisableItem(0, True)
            DisableItem(1, False)
            DisableItem(2, False)
            DisableItem(3, True)
            DisableItem(4, True)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True Then
            DisableItem(5, True)
            DisableItem(6, True)
        Else
            DisableItem(5, False)
            DisableItem(6, False)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).OpenFile = True And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode <> "Select" Then
            DisableItem(7, True)
            DisableItem(8, True)
            DisableItem(9, True)
            DisableItem(10, True)
        Else
            DisableItem(7, False)
            DisableItem(8, False)
            DisableItem(9, False)
            DisableItem(10, False)
        End If

    End Sub

    Private Sub ContextMenuStrip_Pic_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip_Pic.ItemClicked
        ContextMenuStrip_Pic.Visible = False
        Select Case e.ClickedItem.Name
            Case "ToolStripMenuItem_Open"
                OpenFileDialog_Pic.InitialDirectory = _Settings.PicFolder
                If OpenFileDialog_Pic.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).strFileFullPath = OpenFileDialog_Pic.FileName
                    ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).OpenFile = True
                    ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = False
                    ShowPic(_iCurrentPicIndex - 1)
                    ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Open"
                End If

            Case "ToolStripMenuItem_Select"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Select"

            Case "ToolStripMenuItem_Move"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Move"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True

            Case "ToolStripMenuItem_Style_Zoom"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Zoom"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True

            Case "ToolStripMenuItemd_Default"
                ShowPicDefault(_iCurrentPicIndex - 1)
                StartPointCutSave = New Point(0, 0)
                StopPointCutSave = New Point(0, 0)
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CutEnable = False
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Open"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = False

            Case "ToolStripMenuItem_Cut"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Cut"
                ImageProcess.CusForCustom(ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.Image, _ImageProcess, StartPointCut, StopPointCut)
                StartPointCutSave = StartPointCut
                StopPointCutSave = StopPointCut
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.Image = _ImageProcess
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CutEnable = True
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Save"

            Case "ToolStripMenuItem_StretchImage"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Type"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True


            Case "ToolStripMenuItem_AutoSize"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.SizeMode = PictureBoxSizeMode.AutoSize
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Type"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True

            Case "ToolStripMenuItem_CenterImage"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.SizeMode = PictureBoxSizeMode.CenterImage
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Type"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True

            Case "ToolStripMenuItem_Zoom"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.SizeMode = PictureBoxSizeMode.Zoom
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Type"
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = True

            Case "ToolStripMenuItem_Save"
                _mPassword.ChangeMode = False
                _mPassword.ShowDialog()
                If _mPassword.PassWordValid Then
                    UpdateValue()
                End If
                ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Save = False

        End Select

    End Sub

    Private Sub PictureBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If _iCurrentPicIndex <= 0 Then
            Return
        End If

        If e.Button <> MouseButtons.Left Then
            Return
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Select" Then
            Cursor = Cursors.Hand
            StartPointDrawing = e.Location
            StartPointCut = ImageProcess.ChangePoint(ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox, e.Location)
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = True
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Move" Then
            Cursor = Cursors.Hand
            StartPointDrawing = e.Location
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = True
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Zoom" Then
            StartPointDrawing = e.Location
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = True
            PictureBoxZoom(StartPointDrawing)
        End If

    End Sub

    Private Sub PictureBox_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If _iCurrentPicIndex <= 0 Then
            Return
        End If
        If e.Button <> MouseButtons.Left Then
            Return
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Select" Then
            StopPointDrawing = e.Location
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.Refresh()
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.CreateGraphics().DrawRectangle(Pens.Lime, New Rectangle(StartPointDrawing, New Size(StopPointDrawing.X - StartPointDrawing.X, StopPointDrawing.Y - StartPointDrawing.Y)))
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Move" Then
            Cursor = Cursors.Hand
            StopPointDrawing = e.Location
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location = New Point(ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.X + StopPointDrawing.X - StartPointDrawing.X, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.Y + StopPointDrawing.Y - StartPointDrawing.Y)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Zoom" Then
            StopPointDrawing = e.Location
            PictureBoxZoom(StartPointDrawing, StopPointDrawing)
            StartPointDrawing = StopPointDrawing
        End If

    End Sub

    Private Sub PictureBox_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If _iCurrentPicIndex <= 0 Then
            Return
        End If
        If e.Button <> MouseButtons.Left Then
            Return
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Select" Then
            Cursor = Cursors.Default
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = False
            StopPointCut = ImageProcess.ChangePoint(ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox, e.Location)
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Move" Then
            Cursor = Cursors.Default
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = False
        End If

        If ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown And ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CurrentMode = "Zoom" Then
            Cursor = Cursors.Default
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).MouseDown = False
        End If
    End Sub

    Private Sub PictureBoxZoom(ByVal startPoint As Point)
        If startPoint.X < 50 Then
            Cursor = Cursors.VSplit
        End If

        If startPoint.X >= ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width - 50 Then
            Cursor = Cursors.VSplit
        End If
        If startPoint.Y < 50 Then
            Cursor = Cursors.HSplit
        End If
        If startPoint.Y > ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height - 50 Then
            Cursor = Cursors.HSplit
        End If
    End Sub

    Private Sub UpdateValue()
        Dim sResult As Boolean
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FilePositionX = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.X
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FilePositionY = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.Y
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FileWidth = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Width
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FileHeight = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Height
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StartCutPoint = StartPointCut
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StopCutPoint = StopPointCut
        ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Type = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).PictureBox.SizeMode
        sResult = _XmlHandler.SetAnyListFromXml(_Settings.ConfigFolder, _Inifile, _iCurrentPicIndex - 1, "Pictures", "Picture", New String() {"PositionX", "PositionY", "Width", "Height", "Type", "CutEnable", "StartCutPointX", "StartCutPointY", "StopCutPointX", "StopCutPointY"},
                                       New String() {ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FilePositionX.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FilePositionY.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FileWidth.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).FileHeight.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).Type.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).CutEnable.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StartCutPoint.X.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StartCutPoint.Y.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StopCutPoint.X.ToString, ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).StopCutPoint.Y.ToString})
        If sResult Then
            MessageBox.Show("Save OK")
        Else
            MessageBox.Show("Save Fail")
        End If
    End Sub

    Private Sub PictureBoxZoom(ByVal startPoint As Point, ByVal stopPoint As Point)
        Dim x As Integer
        Dim y As Integer
        Dim w As Integer
        Dim h As Integer
        If startPoint.X < 50 Then
            x = stopPoint.X - startPoint.X
            x = x + ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.X
            y = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.Y
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location = New Point(x, y)
            w = stopPoint.X - startPoint.X
            w = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width - w
            h = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size = New Size(w, h)
            Return
        End If

        If startPoint.X >= ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width - 50 Then
            w = stopPoint.X - startPoint.X
            w = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width + w
            h = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size = New Size(w, h)
            Return
        End If

        If startPoint.Y < 50 Then
            x = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.X
            y = stopPoint.Y - startPoint.Y
            y = y + ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location.Y
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Location = New Point(x, y)
            w = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width
            h = stopPoint.Y - startPoint.Y
            h = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height - h
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size = New Size(w, h)
            Return
        End If

        If startPoint.Y > ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height - 50 Then
            w = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Width
            h = stopPoint.Y - startPoint.Y
            h = ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size.Height + h
            ListPicture(ListPicture.Keys(_iCurrentPicIndex - 1)).GroupBox.Size = New Size(w, h)
            Return
        End If

    End Sub

    Protected Function ChangeDataToObject(ByVal str As String, ByRef strValue As Integer, Optional ByVal str2 As String = "") As Boolean
        Try
            strValue = CType(str, Integer)
        Catch ex As Exception
            _Log.Thrower(_i, "ChangeDataToObject Fail: Error Message:" + ex.Message.ToString, "ShowPicture.Init")
        End Try
        Return True
    End Function

    Protected Function ChangeDataToObject(ByVal str As String, ByRef strValue As Boolean, Optional ByVal str2 As String = "") As Boolean
        Try
            strValue = CBool(IIf(sResult.ToUpper = "TRUE", True, False))
        Catch ex As Exception
            _Log.Thrower(_i, "ChangeDataToObject Fail: Error Message:" + ex.Message.ToString, "ShowPicture.Init")
        End Try
        Return True
    End Function

    Protected Function ChangeDataToObject(ByVal str As String, ByRef strValue As Point, Optional ByVal str2 As String = "") As Boolean
        Try
            strValue = New Point(CInt(str), CInt(str2))
        Catch ex As Exception
            _Log.Thrower(_i, "ChangeDataToObject Fail: Error Message:" + ex.Message.ToString, "ShowPicture.Init")
        End Try
        Return True
    End Function

    Protected Function ReadDataFromXml(ByVal strSection As String, ByVal strKey As String, ByRef strValue As Integer) As Boolean
        Try
            sResult = _XmlHandler.GetSectionInformation(_Settings.ConfigFolder, _Inifile, strSection, strKey)
            strValue = CType(sResult, Integer)
        Catch ex As Exception
            _Log.Thrower(_i, "ReadDataFromXml Fail: Section:" + strSection + " Key:" + strKey + " Error Message:" + ex.Message.ToString, "ShowPicture.Init")
        End Try
        Return True
    End Function

    Protected Function ReadDataFromXml(ByVal strSection As String, ByVal strKey As String, ByRef strValue As Boolean) As Boolean
        Try
            sResult = _XmlHandler.GetSectionInformation(_Settings.ConfigFolder, _Inifile, strSection, strKey)
            strValue = CBool(IIf(sResult.ToUpper = "TRUE", True, False))
        Catch ex As Exception
            _Log.Thrower(_i, "ReadDataFromXml Fail: Section:" + strSection + " Key:" + strKey + " Error Message:" + ex.Message.ToString, "ShowPicture.Init")
        End Try
        Return True
    End Function

    Protected Sub ShowErrorMsg(ByVal enumLK_TEXT As enumLK_TEXT, Optional ByVal strAppend1 As String = "", Optional ByVal strAppend2 As String = "", Optional ByVal dLog As Boolean = False)

        If strAppend1 <> "" Then _i.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT, strAppend1)
        If strAppend2 <> "" Then _i.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT, strAppend1, strAppend2)

        _i.StepTextLine = "ShowPicture.Init"
        If dLog Then
            _Log.Logger(_i)
        Else
            _Log.Thrower(_i)
        End If

    End Sub
End Class

Public Class PicElement
    Private _id As String = String.Empty
    Private _strFileName As String = String.Empty
    Private _strFilePath As String = String.Empty
    Private _strFileFullPath As String = String.Empty
    Private _FilePositionX As Integer = 0
    Private _FilePositionY As Integer = 0
    Private _FileHeight As Integer = 0
    Private _FileWidth As Integer = 0
    Private _OpenFile As Boolean = False
    Private _MouseDown As Boolean = False
    Private _Cut As Boolean = False
    Private _Save As Boolean = False
    Private _CutEnable As Boolean = False
    Private _StartCutPoint As Point
    Private _StopCutPoint As Point
    Private _Type As PictureBoxSizeMode
    Private _PictureBox As PictureBox
    Private _GroupBox As GroupBox
    Private _CurrentMode As String = String.Empty

    Public Property CurrentMode As String
        Set(ByVal value As String)
            _CurrentMode = value
        End Set
        Get
            Return _CurrentMode
        End Get

    End Property

    Public Property StartCutPoint As Point
        Set(ByVal value As Point)
            _StartCutPoint = value
        End Set
        Get
            Return _StartCutPoint
        End Get

    End Property


    Public Property StopCutPoint As Point
        Set(ByVal value As Point)
            _StopCutPoint = value
        End Set
        Get
            Return _StopCutPoint
        End Get

    End Property

    Public Property Type As PictureBoxSizeMode
        Set(ByVal value As PictureBoxSizeMode)
            _Type = value
        End Set
        Get
            Return _Type
        End Get

    End Property

    Public Property Save As Boolean
        Set(ByVal value As Boolean)
            _Save = value
        End Set
        Get
            Return _Save
        End Get

    End Property
    Public Property Cut As Boolean
        Set(ByVal value As Boolean)
            _Cut = value
        End Set
        Get
            Return _Cut
        End Get
    End Property

    Public Property CutEnable As Boolean
        Set(ByVal value As Boolean)
            _CutEnable = value
        End Set
        Get
            Return _CutEnable
        End Get
    End Property

    Public Property MouseDown As Boolean
        Set(ByVal value As Boolean)
            _MouseDown = value
        End Set
        Get
            Return _MouseDown
        End Get
    End Property

    Public Property OpenFile As Boolean
        Set(ByVal value As Boolean)
            _OpenFile = value
        End Set
        Get
            Return _OpenFile
        End Get
    End Property

    Public Property id As String
        Set(ByVal value As String)
            _id = value
        End Set
        Get
            Return _id
        End Get
    End Property

    Public Property strFileName As String
        Set(ByVal value As String)
            _strFileName = value
        End Set
        Get
            Return _strFileName
        End Get
    End Property

    Public Property strFileFullPath As String
        Set(ByVal value As String)
            _strFileFullPath = value
        End Set
        Get
            Return _strFileFullPath
        End Get
    End Property

    Public Property strFilePath As String
        Set(ByVal value As String)
            _strFilePath = value
        End Set
        Get
            Return _strFilePath
        End Get
    End Property

    Public Property FilePositionY As Integer
        Set(ByVal value As Integer)
            _FilePositionY = value
        End Set
        Get
            Return _FilePositionY
        End Get
    End Property


    Public Property FileHeight As Integer
        Set(ByVal value As Integer)
            _FileHeight = value
        End Set
        Get
            Return _FileHeight
        End Get
    End Property



    Public Property FileWidth As Integer
        Set(ByVal value As Integer)
            _FileWidth = value
        End Set
        Get
            Return _FileWidth
        End Get
    End Property



    Public Property FilePositionX As Integer
        Set(ByVal value As Integer)
            _FilePositionX = value
        End Set
        Get
            Return _FilePositionX
        End Get
    End Property

    Public Property PictureBox As PictureBox
        Set(ByVal value As PictureBox)
            _PictureBox = value
        End Set
        Get
            Return _PictureBox
        End Get
    End Property

    Public Property GroupBox As GroupBox
        Set(ByVal value As GroupBox)
            _GroupBox = value
        End Set
        Get
            Return _GroupBox
        End Get
    End Property

    Public Shared Function ChangeType(ByVal mType As String) As PictureBoxSizeMode

        If mType.ToUpper = "StretchImage".ToUpper Then Return PictureBoxSizeMode.StretchImage
        If mType.ToUpper = "AutoSize".ToUpper Then Return PictureBoxSizeMode.AutoSize
        If mType.ToUpper = "CenterImage".ToUpper Then Return PictureBoxSizeMode.CenterImage
        If mType.ToUpper = "Zoom".ToUpper Then Return PictureBoxSizeMode.Zoom
        Return PictureBoxSizeMode.Normal
    End Function

End Class

Public Class ImageProcess
    Public Shared Sub CusForCustom(ByVal fromFile As Image, ByRef outImage As Image, ByVal startPoint As Point, ByVal stopPoint As Point)
        Dim initImage As Image = fromFile
        Dim iStart As Integer = 0
        Dim iStop As Integer = 0
        Dim maxWidth As Integer
        Dim maxHeight As Integer
        If startPoint.X < stopPoint.X And startPoint.Y < stopPoint.Y Then
            iStart = startPoint.X
            iStop = startPoint.Y
        End If

        If startPoint.X <= stopPoint.X And startPoint.Y >= stopPoint.Y Then
            iStart = startPoint.X
            iStop = stopPoint.Y
        End If

        If startPoint.X > stopPoint.X And startPoint.Y < stopPoint.Y Then
            iStart = stopPoint.X
            iStop = startPoint.Y
        End If

        If startPoint.X >= stopPoint.X And startPoint.Y >= stopPoint.Y Then
            iStart = stopPoint.X
            iStop = stopPoint.Y
        End If

        maxWidth = Math.Abs(stopPoint.X - startPoint.X)
        maxHeight = Math.Abs(stopPoint.Y - startPoint.Y)

        If maxWidth = 0 Or maxHeight = 0 Then
            outImage = CType(initImage.Clone, Image)
            Return
        End If

        If initImage.Width <= maxWidth And initImage.Height >= maxHeight Then
            outImage = CType(initImage.Clone, Image)
            Return
        Else
            Dim templateRate As Single = CSng(maxWidth / maxHeight)
            Dim initRate As Single = CSng(initImage.Width / initImage.Height)
            If templateRate = initRate Then
                Dim templateImage As Image = New Bitmap(maxWidth, maxHeight)
                Dim templateG As Graphics = Graphics.FromImage(templateImage)
                templateG.InterpolationMode = InterpolationMode.High
                templateG.SmoothingMode = SmoothingMode.HighQuality
                templateG.Clear(Color.Blue)
                templateG.DrawImage(initImage, New Rectangle(iStart, iStop, maxWidth, maxHeight), New Rectangle(iStart, iStop, maxWidth, maxHeight), GraphicsUnit.Pixel)
                outImage = CType(templateImage.Clone, Image)
            Else
                Dim pickImage As Image = Nothing
                Dim pickG As Graphics = Nothing
                Dim fromR As Rectangle = New Rectangle(0, 0, 0, 0)
                Dim toR As Rectangle = New Rectangle(0, 0, 0, 0)
                If templateRate > initRate Then
                    pickImage = New Bitmap(initImage.Width, CInt(System.Math.Floor(initImage.Width / templateRate)))
                    pickG = Graphics.FromImage(pickImage)
                    fromR.X = 0
                    fromR.Y = CInt(Math.Floor((initImage.Height - initImage.Width / templateRate) / 2))
                    fromR.Width = initImage.Width
                    fromR.Height = CInt(Math.Floor(initImage.Width / templateRate))
                    toR.X = 0
                    toR.Y = 0
                    toR.Width = initImage.Width
                    toR.Height = CInt(Math.Floor(initImage.Width / templateRate))
                Else
                    pickImage = New Bitmap(CInt(initImage.Height * templateRate), initImage.Height)
                    pickG = Graphics.FromImage(pickImage)
                    fromR.X = CInt(Math.Floor((initImage.Width - initImage.Height * templateRate) / 2))
                    fromR.Y = 0
                    fromR.Width = CInt(Math.Floor(initImage.Height * templateRate))
                    fromR.Height = initImage.Height
                    toR.X = 0
                    toR.Y = 0
                    toR.Width = CInt(Math.Floor(initImage.Height * templateRate))
                    toR.Height = initImage.Height
                End If
                pickG.InterpolationMode = InterpolationMode.HighQualityBicubic
                pickG.SmoothingMode = SmoothingMode.HighQuality
                pickG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel)

                Dim templateImage As Image = New Bitmap(maxWidth, maxHeight)
                Dim templateG As Graphics = Graphics.FromImage(templateImage)
                templateG.InterpolationMode = InterpolationMode.High
                templateG.SmoothingMode = SmoothingMode.HighQuality
                templateG.Clear(Color.Blue)
                templateG.DrawImage(initImage, New Rectangle(0, 0, maxWidth, maxHeight), New Rectangle(iStart, iStop, maxWidth, maxHeight), GraphicsUnit.Pixel)
                outImage = CType(templateImage.Clone, Image)

                templateG.Dispose()
                templateImage.Dispose()
                pickG.Dispose()
                pickImage.Dispose()
            End If
        End If
    End Sub

    Public Shared Function ChangePoint(ByVal picbox As PictureBox, ByVal Point As Point) As Point
        Dim msPoint As Point
        Dim rectangleProperty As System.Reflection.PropertyInfo
        rectangleProperty = picbox.GetType.GetProperty("ImageRectangle", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        Dim rectangle As Rectangle = CType(rectangleProperty.GetValue(picbox, Nothing), Drawing.Rectangle)
        Dim rate As Double = rectangle.Height / rectangle.Width
        Dim back_left_width As Integer = CInt(IIf(rectangle.Width = picbox.Width, 0, (picbox.Width - rectangle.Width) / 2))
        Dim back_top_height As Integer = CInt(IIf(rectangle.Height = picbox.Height, 0, (picbox.Height - rectangle.Height) / 2))
        Dim zoom_x As Integer = Point.X - back_left_width
        Dim zoom_y As Integer = Point.Y - back_top_height
        Dim original_x As Integer = CInt(zoom_x / (rectangle.Width / picbox.Image.Width))
        Dim original_y As Integer = CInt(zoom_y / (rectangle.Height / picbox.Image.Height))
        msPoint.X = original_x
        msPoint.Y = original_y
        Return msPoint
    End Function


End Class
