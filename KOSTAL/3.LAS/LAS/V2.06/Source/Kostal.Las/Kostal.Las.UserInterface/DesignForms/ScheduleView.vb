Imports Kostal.Las.Base
Imports Kostal.Las.UserInterface

Public Enum enum_ScheduleViewErrorCodes
    SHIFT_ERROR_FILE_ERROR = -99
    SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
    SHIFT_ERROR_INVALID_SHIFTDATACOUNT_ERROR
    SHIFT_ERROR_INVALID_SHIFTDATASORT_ERROR
    SHIFT_ERROR_INVALID_SHIFTTIME_ERROR
    SHIFT_ERROR_INVALID_SHIFTNOWTIME_ERROR
    SHIFT_ERROR_WINDOWS_ERROR
    SHIFT_ERROR_INVALID_GETCURRENT_ERROR
End Enum

Public Class ScheduleView
    Implements IViewDefine

    Protected _Status As enum_ErrorCodes
    Protected _StatusDescription As String = String.Empty
    Private _ShowSchedule As Boolean
    Private _ScheduleData As New Dictionary(Of String, ScheduleDataElement)
    Private _ScheduleName As New Dictionary(Of String, ScheduleNameElement)
    Public Const sName As String = "_mScheduleView"

    Public Property ShowSchedule As Boolean
        Get
            Return _ShowSchedule
        End Get
        Set(ByVal value As Boolean)
            _ShowSchedule = value
        End Set
    End Property

    Public Property ScheduleData As Dictionary(Of String, ScheduleDataElement)
        Get
            Return _ScheduleData
        End Get
        Set(ByVal value As Dictionary(Of String, ScheduleDataElement))
            _ScheduleData = value
        End Set
    End Property

    Public Property ScheduleName As Dictionary(Of String, ScheduleNameElement)
        Get
            Return _ScheduleName
        End Get
        Set(ByVal value As Dictionary(Of String, ScheduleNameElement))
            _ScheduleName = value
        End Set
    End Property

    Public ReadOnly Property Status() As enum_ErrorCodes
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property
    Public ReadOnly Property GetPannel As Panel Implements IViewDefine.GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Public Sub Run()
        If Not _ShowSchedule Then
            If Me.Visible Then Me.Visible = False
        Else
            If Not Me.Visible Then
                Me.Visible = True
                ShowData()
            End If

        End If
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        _ShowSchedule = False
    End Sub

    Public Sub ShowData()
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim iStartCheckSum As Integer = 0
            Dim iStopCheckSum As Integer = 0
            Dim iNameNum As Integer = 0
            Dim bRemove As Boolean = False
            Dim RowsValues() As String
            Dim RowsDescription As New List(Of String)
            Dim RowsDescriptionData As New Dictionary(Of String, String)
            DG_Schedule.Columns.Clear()
            DG_Schedule.Rows.Clear()
            RowsDescription.Clear()
            RowsDescriptionData.Clear()
            DG_Schedule.ClearSpanInfo()

            If Not CheckData() Then Return

            For Each element As ScheduleNameElement In _ScheduleName.Values
                If _ScheduleName(element.Key).Hide Then Continue For
                bRemove = True

                '若此列ScheduleData值全部为空,则隐藏此列
                For Each scheduleelement As ScheduleDataElement In _ScheduleData.Values
                    If scheduleelement.Hide Then
                        Continue For
                    End If
                    If scheduleelement.ScheduleElement.ContainsKey(element.Key) Then
                        If scheduleelement.ScheduleElement(element.Key).Value <> "" And scheduleelement.ScheduleElement(element.Key).Value <> BaseScheduleDataElement.SecurityChecksum.ToString Then
                            bRemove = False
                            Exit For
                        End If
                    End If
                Next

                If bRemove Then
                    _ScheduleName(element.Key).Hide = True
                Else
                    '若元素为PassST,则添加表头Description
                    If element.Key.IndexOf("PassST") >= 0 Then
                        If _ScheduleName.ContainsKey("Description" + element.Key.Substring(element.Key.IndexOf("ST"))) Then
                            RowsDescription.Add("Description" + element.Key.Substring(element.Key.IndexOf("ST")))
                        End If
                    End If
                End If

                '隐藏Description
                If element.Key.IndexOf("DescriptionST") >= 0 Then
                    _ScheduleName(element.Key).Hide = True
                End If
            Next

            '添加表头
            For Each element As ScheduleNameElement In _ScheduleName.Values
                If Not element.Hide Then
                    DG_Schedule.Columns.Add(element.Key, element.Value)
                End If
            Next

            '添加合并表头
            For Each element As String In RowsDescription
                RowsDescriptionData.Add(element, _ScheduleData(_ScheduleData.Keys(0)).ScheduleElement(element).Value)
            Next

            For i = 0 To DG_Schedule.Columns.Count - 1
                DG_Schedule.Columns(i).DataPropertyName = i.ToString
            Next

            For Each scheduleelement As ScheduleDataElement In _ScheduleData.Values
                If scheduleelement.Hide Then Continue For
                ReDim RowsValues(_ScheduleName.Count - 1)
                i = 0
                '添加元素值
                For Each element As ScheduleNameElement In _ScheduleName.Values
                    If element.Hide Then Continue For
                    RowsValues(i) = scheduleelement.ScheduleElement(element.Key).Value
                    i = i + 1
                Next
                DG_Schedule.Rows.Add(RowsValues)
            Next

            '合并表头
            DG_Schedule.ColumnHeadersHeight = 40
            DG_Schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            For i = 0 To DG_Schedule.Columns.Count - 1
                If DG_Schedule.Columns(i).Name.IndexOf("PassST") >= 0 And DG_Schedule.Columns(i).Visible Then
                    Dim nName As String = "Description" + DG_Schedule.Columns(i).Name.Substring(DG_Schedule.Columns(i).Name.IndexOf("ST"))
                    DG_Schedule.AddSpanHeader(CInt(DG_Schedule.Columns(i).DataPropertyName), 2, RowsDescriptionData(nName))
                End If
                If DG_Schedule.Columns(i).Name = BaseScheduleDataElement.SecurityChecksum.ToString Then iStartCheckSum = i
                If DG_Schedule.Columns(i).Name = BaseScheduleDataElement.ManualChecksum.ToString Then iStopCheckSum = i
                If DG_Schedule.Columns(i).Name = BaseScheduleDataElement.ScheduleName.ToString Then iNameNum = i
            Next

            For i = 0 To DG_Schedule.Rows.Count - 1
                If Not IsNothing(DG_Schedule.Rows(i).Cells(iStartCheckSum).Value) And Not IsNothing(DG_Schedule.Rows(i).Cells(iStopCheckSum).Value) And Not IsNothing(DG_Schedule.Rows(i).Cells(iNameNum).Value) Then
                    If CheckScheduleMode(DG_Schedule.Rows(i).Cells(iNameNum).Value.ToString) Then
                        If DG_Schedule.Rows(i).Cells(iStartCheckSum).Value.ToString <> DG_Schedule.Rows(i).Cells(iStopCheckSum).Value.ToString Then
                            DG_Schedule.Rows(i).Cells(iStartCheckSum).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                            DG_Schedule.Rows(i).Cells(iStopCheckSum).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                        End If
                    Else
                        DG_Schedule.Rows(i).Cells(iStartCheckSum).Value = ""
                        DG_Schedule.Rows(i).Cells(iStopCheckSum).Value = ""
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Show Data Fail. Error Message:" + ex.Message.ToString)
        End Try
    End Sub

    Public Function CheckScheduleMode(ByVal strName As String) As Boolean
        For Each TypeElemet As ScheduleMode In [Enum].GetValues(GetType(ScheduleMode))
            If strName.IndexOf([Enum].GetName(GetType(ScheduleMode), TypeElemet)) >= 0 Then
                Return True
            End If
        Next
        Return False
    End Function


    Public Function CheckData() As Boolean
        '检查表头数据
        For Each TypeElemet As BaseScheduleDataElement In [Enum].GetValues(GetType(BaseScheduleDataElement))
            If Not _ScheduleName.ContainsKey([Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet)) Then
                Throw New Exception("Please Add Element Name: " + [Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet))
                Return False
            End If
        Next

        For Each element As ScheduleNameElement In _ScheduleName.Values
            If element.Key.IndexOf("PassST") >= 0 Then
                If Not _ScheduleName.ContainsKey("Description" + element.Key.Substring(element.Key.IndexOf("ST"))) Then
                    Throw New Exception("Please Add Element Name: " + "Description" + element.Key.Substring(element.Key.IndexOf("ST")))
                End If
                If Not _ScheduleName.ContainsKey("Fail" + element.Key.Substring(element.Key.IndexOf("ST"))) Then
                    Throw New Exception("Please Add Element Name: " + "Fail" + element.Key.Substring(element.Key.IndexOf("ST")))
                End If
            End If
        Next

        '检查元素数据
        For Each element As ScheduleNameElement In _ScheduleName.Values
            For Each scheduleelement As ScheduleDataElement In _ScheduleData.Values
                If Not scheduleelement.ScheduleElement.ContainsKey(element.Key) Then
                    Throw New Exception("Please Add Element Value: " + element.Key)
                End If
            Next
        Next

        Return True
    End Function

    Protected Sub ContextMenuStrip_Schedule_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip_Schedule.ItemClicked

        Select Case e.ClickedItem.Name
            Case "ToolStripMenuItem_Hide_Row"
                If Not IsNothing(DG_Schedule.CurrentRow) Then
                    _ScheduleData(DG_Schedule.CurrentRow.Cells(0).Value.ToString).Hide = True
                    ShowData()
                End If

            Case "ToolStripMenuItem_Hide_Column"
                If Not IsNothing(DG_Schedule.CurrentCell) Then
                    _ScheduleName(DG_Schedule.Columns(DG_Schedule.CurrentCell.ColumnIndex).Name).Hide = True
                    ShowData()
                End If

            Case "ToolStripMenuItem_Reset"
                For i = 0 To _ScheduleName.Count - 1
                    _ScheduleName(_ScheduleName.Keys(i)).Hide = False
                Next
                For i = 0 To _ScheduleData.Count - 1
                    _ScheduleData(_ScheduleData.Keys(i)).Hide = False
                Next
                ShowData()
        End Select

    End Sub
End Class

Public Enum ScheduleMode
    ProductionMode
    RetestMode
    SelfResistanceTest
    MasterPartTest
    AssemblyOnly
End Enum

Public Enum BaseScheduleDataElement
    SecurityChecksum
    ManualChecksum
    ScheduleName
    ID
End Enum

Public Enum ScheduleNameType
    csv
    ini
    Manual
End Enum
Public Class ScheduleDataElement
    Private dHide As Boolean = False
    Private dScheduleElement As New Dictionary(Of String, ScheduleElement)
    Public Property Hide As Boolean
        Set(ByVal value As Boolean)
            dHide = value
        End Set
        Get
            Return dHide
        End Get
    End Property

    Public Property ScheduleElement As Dictionary(Of String, ScheduleElement)
        Set(ByVal value As Dictionary(Of String, ScheduleElement))
            dScheduleElement = value
        End Set
        Get
            Return dScheduleElement
        End Get
    End Property
End Class
Public Class ScheduleElement
    Private mKey As String = String.Empty
    Private mValue As String = String.Empty

    Public Property Key As String
        Set(ByVal value As String)
            mKey = value
        End Set
        Get
            Return mKey
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            mValue = value
        End Set
        Get
            Return mValue
        End Get
    End Property

    Sub New(ByVal Key As String, ByVal Value As String)
        mKey = Key
        mValue = Value
    End Sub
End Class
Public Class ScheduleNameElement
    Private dHide As Boolean = False
    Private mKey As String = String.Empty
    Private mValue As String = String.Empty
    Private mValueFrom As ScheduleNameType


    Public Property Hide As Boolean
        Set(ByVal value As Boolean)
            dHide = value
        End Set
        Get
            Return dHide
        End Get
    End Property

    Public Property Key As String
        Set(ByVal value As String)
            mKey = value
        End Set
        Get
            Return mKey
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            mValue = value
        End Set
        Get
            Return mValue
        End Get
    End Property

    Public Property ValueFrom As ScheduleNameType
        Set(ByVal value As ScheduleNameType)
            mValueFrom = value
        End Set
        Get
            Return mValueFrom
        End Get
    End Property

    Sub New(ByVal Key As String, ByVal Value As String, ByVal ValueFrom As ScheduleNameType)
        mKey = Key
        mValue = Value
        mValueFrom = ValueFrom
    End Sub
End Class