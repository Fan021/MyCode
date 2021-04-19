

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Xml
Imports Kostal.Las.Base


Public Class OverallView

    Protected g1 As Graphics
    Protected bmp1 As Bitmap
    Protected img1 As Image
    Protected scaleX1 As Single
    Protected scaleY1 As Single
    Protected rectF1 As RectangleF
    Protected iCnt As Integer = 0
    Private _dgViewColumnList As List(Of String)
    Private mouseDownPoint As Point = Point.Empty
    Private rect As Rectangle = Rectangle.Empty
    Private isDrag As Boolean = False
    Private cRectangle As Rectangle
    Private isEdit As Boolean = False
    Private AppSettings As Settings
    Private lListStationView As New Dictionary(Of String, clsPictureComponentCfg)
    Private strStationPicture As String = ""
    Private mXmlHandler As New XmlHandler
    Public MaxStation As Integer = 1
    Private cLanguageManager As Language
    Private iMaxX As Integer = 0
    Private iMaxY As Integer = 0
    Private iRatex As Double = 1
    Private iRateY As Double = 1
    Private cUserManager As clsUserManager
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _dgViewColumnList = New List(Of String)

        IOControl.Images(0) = My.Resources.Resource1.gray
        IOControl.Images(1) = My.Resources.Resource1.green

    End Sub

    Private Sub AddColmnList()

        _dgViewColumnList.Clear()

        For Each item As String In StationInformation.GetMemberList

            _dgViewColumnList.Add(item)

        Next

    End Sub


    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.DesignPanel
        End Get
    End Property
    Public Sub InitLanguage()
        RemoveHandler dgView.CellValueChanged, AddressOf dgView_CellValueChanged
        For Each c As DataGridViewColumn In dgView.Columns
            c.HeaderText = cLanguageManager.Read("OverView", c.HeaderText)
        Next
        AddHandler dgView.CellValueChanged, AddressOf dgView_CellValueChanged
    End Sub

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Me.AppSettings = _AppSettings
        AddColmnList()
        cLanguageManager = Devices(Language.Name)
        cUserManager = Devices(clsUserManager.Name)
        dgView.BorderStyle = BorderStyle.Fixed3D

        dgView.Columns.Clear()

        'Dim colWidth As Integer = (dgView.Width - dgView.RowHeadersWidth) / _dgViewColumnList.Count

        For Each item As String In _dgViewColumnList

            dgView.Columns.Add(item, item)
            dgView.Columns(dgView.Columns.Count - 1).ReadOnly = True
            dgView.Columns(dgView.Columns.Count - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        '    If item = StationInformation.conCarrierNumber Or item = StationInformation.conDestinationStation Then
        '        dgView.Columns(item).Width = colWidth * 0.5
        '    ElseIf item = StationInformation.conScheduleName Then
        '        dgView.Columns(item).Width = colWidth * 1.3
        '    ElseIf item = StationInformation.conSerialNumber Then
        '        dgView.Columns(item).Width = colWidth * 1.5
        '    Else
        '        dgView.Columns(item).Width = colWidth * 1.05
        '    End If

        'Next

        dgView.Rows.Clear()

        dgView.AllowUserToAddRows = False

        'dgView.ColumnHeadersHeight = dgView.ColumnHeadersHeight * 1.5

        CreateStationStatus()

        For i As Integer = 1 To dgView.Rows.Count
            dgView.Rows(i - 1).HeaderCell.Value = i.ToString
            ' dgView.Rows(i - 1).HeaderCell.s
            ' dgView.Rows(i - 1).Height = (dgView.Height - dgView.ColumnHeadersHeight) / 7.7
        Next

        dgView.AllowUserToAddRows = False

        'dgView.ScrollBars = ScrollBars.None
        CreateStation()
        ShowStationView(picBoxMain.Width, picBoxMain.Height)
        AddHandler picBoxMain.Resize, AddressOf picBoxMain_Resize

        Return True

    End Function

    Private Sub CreateStationStatus()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            lListStationView.Clear()
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationStatusViews")
            Dim iCnt As Integer = 0
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationStatusView")
                For Each _nodeList As XmlNode In _subNodes

                    dgView.Rows.Add(1)
                    dgView.Rows(dgView.Rows.Count - 1).Cells(0).Value = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    dgView.Height = (dgView.Rows.Count + 1) * dgView.Rows(dgView.Rows.Count - 1).Height + 10
                    iCnt = iCnt + 1
                Next

            Next
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try

    End Sub

    Private Sub CreateStation()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            lListStationView.Clear()
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationViews")
            strStationPicture = CType(_nodes(0), XmlElement).GetAttribute("Picture")
            Dim iCnt As Integer = 1
            For Each _node As XmlNode In _nodes

                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationView")
                For Each _nodeList As XmlNode In _subNodes
                    Dim cPictureComponent As New clsPictureComponentCfg
                    cPictureComponent.strName = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    cPictureComponent.strX = CType(_nodeList, XmlElement).GetElementsByTagName("Position_X")(0).InnerText
                    cPictureComponent.strY = CType(_nodeList, XmlElement).GetElementsByTagName("Position_Y")(0).InnerText
                    cPictureComponent.strR = CType(_nodeList, XmlElement).GetElementsByTagName("Position_R")(0).InnerText

                    lListStationView.Add(iCnt.ToString, cPictureComponent)
                    iCnt = iCnt + 1
                Next

            Next
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try
    End Sub


    Private Sub SaveStation()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationViews")
            strStationPicture = CType(_nodes(0), XmlElement).GetAttribute("Picture")
            Dim iCnt As Integer = 1
            For Each _node As XmlNode In _nodes

                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationView")
                For Each _nodeList As XmlNode In _subNodes
                    Dim cPictureComponent As clsPictureComponentCfg = lListStationView(iCnt.ToString)
                    CType(_nodeList, XmlElement).GetElementsByTagName("Position_X")(0).InnerText = cPictureComponent.strX
                    CType(_nodeList, XmlElement).GetElementsByTagName("Position_Y")(0).InnerText = cPictureComponent.strY
                    iCnt = iCnt + 1
                Next

            Next
            _doc.Save(s_FileName)
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try
    End Sub
    Private Sub PictureBox_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBoxMain.MouseClick
        If e.Button = MouseButtons.Right Then
            For Each cPictureCfg As clsPictureComponentCfg In lListStationView.Values
                If cPictureCfg.bEdit Then
                    cPictureCfg.strX = e.Location.X
                    cPictureCfg.strY = e.Location.Y
                    ShowStationView(picBoxMain.Width, picBoxMain.Height)
                    SaveStation()
                End If
            Next
        End If
    End Sub

    Private Sub picBoxMain_DoubleClick(sender As Object, e As EventArgs) Handles picBoxMain.DoubleClick
        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then Return
        Dim p As Point = picBoxMain.PointToClient(MousePosition)
        For Each cPictureCfg As clsPictureComponentCfg In lListStationView.Values
            If p.X > cPictureCfg.cRectangle.X And p.X < cPictureCfg.cRectangle.X + cPictureCfg.cRectangle.Width And p.Y > cPictureCfg.cRectangle.Y And p.Y < cPictureCfg.cRectangle.Y + cPictureCfg.cRectangle.Height Then
                cPictureCfg.bEdit = Not cPictureCfg.bEdit
                ShowStationView(picBoxMain.Width, picBoxMain.Height)
            Else
                cPictureCfg.bEdit = False
            End If
        Next


    End Sub

    Private Sub ShowStationView(ByVal W As Integer, ByVal H As Integer)
        Try
            Dim iR As Double
            Dim iX As Integer
            Dim iY As Integer
            Dim iPointX As Integer
            Dim iPointY As Integer
            Dim iIndex As Integer = 0
            Dim cFileHandler As New FileHandler
            Dim strBackImage As String = AppSettings.ApplicationFolder + strStationPicture

            If Not cFileHandler.FileExist(strBackImage) Then
                Return
            End If
            If Not IsNothing(picBoxMain.Image) Then
                picBoxMain.Image.Dispose()
                picBoxMain.Image = Nothing
            End If
            img1 = Nothing
            Using file1 As New FileStream(strBackImage, FileMode.Open, FileAccess.Read)
                Dim img = FileCompress.CompressionImage(file1, 50)
                img1 = FileCompress.BytesToImage(img)
                file1.Close()
            End Using

            scaleX1 = 1
            scaleY1 = 1
            rectF1 = New RectangleF()
            rectF1.Width = W
            rectF1.Height = H
            rectF1.X = 0
            rectF1.Y = 0
            bmp1 = New Bitmap(W, H)


            g1 = Graphics.FromImage(bmp1)
            g1.Clear(Color.White)
            g1.SmoothingMode = SmoothingMode.AntiAlias
            If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)



            For Each cPictureCfg As clsPictureComponentCfg In lListStationView.Values
                Dim strStationBackImage As String = AppSettings.ApplicationFolder + "Picture\green.png"
                If Not IsNumeric(cPictureCfg.strR) Or cPictureCfg.strR = "" Then
                    iR = 1.0
                Else
                    iR = cPictureCfg.strR
                End If

                If Not IsNumeric(cPictureCfg.strX) Or cPictureCfg.strX = "" Then
                    iX = 1.0
                Else
                    iX = cPictureCfg.strX
                End If

                If Not IsNumeric(cPictureCfg.strY) Or cPictureCfg.strY = "" Then
                    iY = 1.0
                Else
                    iY = cPictureCfg.strY
                End If
                iX = iX * iRatex
                iY = iY * iRateY
                iR = iR * iRateY
                iPointX = iX
                iPointY = iY

                If File.Exists(strStationBackImage) Then
                    Dim imgCompontent As Image
                    Using file2 As New FileStream(strStationBackImage, FileMode.Open, FileAccess.Read)
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


                    cRectangle = New Rectangle(New Point(rectCompontent.X, rectCompontent.Y), New Size(rectCompontent.Width, rectCompontent.Height))
                    cPictureCfg.cRectangle = cRectangle
                    If cPictureCfg.bEdit Then g1.DrawRectangle(New Pen(Color.Red), cRectangle)
                    g1.DrawString(cPictureCfg.strName, New System.Drawing.Font("Calibri", CInt(18 * iRateY), FontStyle.Bold), New SolidBrush(Color.Blue), New Point(rectCompontent.X + rectCompontent.Width, iPointY - 10))
                End If
            Next




            picBoxMain.Image = bmp1
            Dim graphics1 As Graphics = picBoxMain.CreateGraphics()
            graphics1.DrawImage(bmp1, New Point(0, 0))
            graphics1.Dispose()

        Catch ex As Exception
            Return
        End Try
    End Sub


    Public Function UpdateRow(ByVal stataionName As String, ByVal stationInfo As StationInformation) As Boolean

        Try

            If stataionName = "" Then Return False

            For Each item As String In StationInformation.GetMemberList

                Dim fi As PropertyInfo = stationInfo.GetType.GetProperty(item)

                Dim value As String = fi.GetValue(stationInfo).ToString
                For i = 0 To dgView.Rows.Count - 1
                    If dgView.Rows(i).Cells(0).Value = stataionName Then
                        If dgView.Rows(i).Cells(item).Value <> value Then
                            dgView.Rows(i).Cells(item).Value = value
                        End If
                    End If
                Next
            Next

            Return True


        Catch ex As Exception

            Dim strErr As String = ex.Message

        End Try


        Return False

    End Function

    Public Function UpdateAll(ByVal infos As IEnumerable(Of StationInformation)) As Boolean

        For Each item As StationInformation In infos

            UpdateRow(item.StationKey, item)

        Next
        'AutoResize(dgView)
        Return True

    End Function

    Private Sub dgView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

        'dgView.

    End Sub

    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)


        If e.ColumnIndex < 0 Then Return

        Dim sHeader As String = dgView.Columns(e.ColumnIndex).Name

        Select Case sHeader

            Case StationInformation.conRESULT

                Select Case dgView.Rows(e.RowIndex).Cells(sHeader).Value.ToString
                    Case True.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN) 'Color.Green
                    Case False.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED) 'Color.Red
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                End Select

            Case StationInformation.conAutoManual

                Select Case dgView.Rows(e.RowIndex).Cells(sHeader).Value.ToString
                    Case "Auto"
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN) 'Color.Green
                    Case "Manual"
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.Red
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                End Select

            Case StationInformation.conPlcStatus

                Select Case dgView.Rows(e.RowIndex).Cells(sHeader).Value.ToString
                    Case enumPLC_Status.Run.ToString, enumPLC_Status.LastCycle.ToString, enumPLC_Status.Start.ToString, enumPLC_Status.Ready.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN) 'Color.Green
                    Case enumPLC_Status.Calibrated.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_ORANGE)
                    Case enumPLC_Status.On.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = KostalLasColors.YELLOWLIGHT 'ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_YELLOW)
                    Case enumPLC_Status.Off.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE)
                    Case enumPLC_Status.Stop.ToString
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED) 'Color.Red
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(sHeader).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                End Select


            Case Else

        End Select


    End Sub

    Private Sub dgView_Resize(sender As Object, e As EventArgs)

        If _dgViewColumnList Is Nothing Then Return

        If _dgViewColumnList.Count < 1 Then Return

        Dim colWidth As Integer = (dgView.Width - dgView.RowHeadersWidth) / _dgViewColumnList.Count

        For Each item As String In _dgViewColumnList

            dgView.Columns(item).Width = colWidth

        Next

    End Sub

    Private Sub picBoxSystem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub picBoxSystem_Validated(sender As Object, e As EventArgs)

    End Sub

    Private Sub picBoxSystem_Resize(sender As Object, e As EventArgs)

    End Sub

    Private Sub dgView_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)

    End Sub

    Private Sub picBoxMain_Click(sender As Object, e As EventArgs) Handles picBoxMain.Click

    End Sub

    Private Sub picBoxMain_Resize(sender As Object, e As EventArgs)
        Try
            If picBoxMain.Width > iMaxX Then
                iMaxX = picBoxMain.Width
            End If
            If picBoxMain.Width < iMaxX Then
                iRatex = picBoxMain.Width / iMaxX
                ShowStationView(picBoxMain.Width, picBoxMain.Height)
            End If

            If picBoxMain.Height > iMaxY Then
                iMaxY = picBoxMain.Height
            End If
            If picBoxMain.Height < iMaxY Then
                iRateY = picBoxMain.Height / iMaxY
                ShowStationView(picBoxMain.Width, picBoxMain.Height)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function AutoResize(ByRef DGV As DataGridView) As Boolean
        For i As Integer = 0 To DGV.Columns.Count - 1
            DGV.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells)
        Next

        Return True
    End Function


End Class


Public Class clsPictureComponentCfg
    Public strName As String = ""
    Public strX As String = "0"
    Public strY As String = "0"
    Public strR As String = "0"
    Public cRectangle As New Rectangle
    Public iFontSize As Integer = 0
    Public strColor As String = "Blue"
    Public bEdit As Boolean = False
    Sub New()

    End Sub
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
