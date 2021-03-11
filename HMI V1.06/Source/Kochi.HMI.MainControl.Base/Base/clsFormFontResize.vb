Imports Kochi.HMI.MainControl.UI

Public Class clsFormFontResize
    Private ioldH As Integer = 0
    Private inewH As Integer = 0
    Private iWinFromH As Integer = Integer.MaxValue
    Private iOriginalH As Integer = 0
    Private isResized As Boolean = False
    Private iCurrentRate As Single = 0
    Private iLastFontSize As Single = 0
    Private ccons As Control = Nothing
    Private strFontName As String = "Calibri"
    Public Const Name As String = "FormFontResize"

    Public Property newH As Integer
        Set(ByVal value As Integer)
            If value >= iWinFromH And value >= 999 Then
                inewH = value
                isResized = True
                CalculateFontSize()
            End If

        End Set
        Get
            Return inewH
        End Get
    End Property

    Public Property oldH As Integer
        Set(ByVal value As Integer)
            ioldH = value
        End Set
        Get
            Return ioldH
        End Get
    End Property

    Public Property WinFromH As Integer
        Set(ByVal value As Integer)
            iWinFromH = value
        End Set
        Get
            Return iWinFromH
        End Get
    End Property

    Public ReadOnly Property OriginalH As Integer
        Get
            Return iOriginalH
        End Get
    End Property

    Public Property newExH As Integer
        Set(ByVal value As Integer)
            If value > iOriginalH Then iOriginalH = value
            If inewH >= iWinFromH - 20 Then
                isResized = True
            End If
            inewH = value
        End Set
        Get
            Return inewH
        End Get
    End Property

    Public Property OldExH As Integer
        Set(ByVal value As Integer)
            ioldH = value
        End Set
        Get
            Return ioldH
        End Get
    End Property

    Public ReadOnly Property Resized As Boolean
        Get
            Return isResized
        End Get
    End Property

    Public ReadOnly Property OriginalCurrentRate As Single
        Get
            Return inewH / iOriginalH * 1.0
        End Get
    End Property

    Public ReadOnly Property CurrentRate As Single
        Get
            Return iCurrentRate
        End Get
    End Property



    Public Property cons As Control
        Set(ByVal value As Control)
            ccons = value
        End Set
        Get
            Return ccons
        End Get
    End Property

    Private Sub CalculateFontSize()
        If inewH < 100 Then
            Return
        End If
        If inewH <> ioldH And ioldH > 0 Then
            Dim iFontSizeRate As Single = inewH / ioldH * 1.0
            iCurrentRate = iFontSizeRate
            If iCurrentRate > 0 And iCurrentRate < 10 Then
                SetControls(iFontSizeRate, ccons)
            End If
        End If
        ioldH = inewH
    End Sub

    Public Sub ChangeFontSize()
        If inewH < 100 Then
            Return
        End If
        If ioldH > 0 Then
            Dim iFontSizeRate As Single = inewH / ioldH * 1.0
            iCurrentRate = iFontSizeRate
            If iCurrentRate > 0 And iCurrentRate < 10 Then
                SetControls(iFontSizeRate, ccons)
            End If
        End If
    End Sub


    Public Sub SetControlFronts(ByVal FontSize As Single, ByVal cons As Control)

        For Each con As Control In cons.Controls

            If TypeOf con Is Panel Then
            Else
                con.Font = New Font("Calibri", FontSize!)
            End If
            If con.Controls.Count > 0 Then
                SetControlFronts(FontSize, con)
            End If
        Next
    End Sub

    Public Sub SetControls(ByVal FontSizeRate As Single, ByVal cons As Control)
        If FontSizeRate <= 0 Then Return
        Dim iOldFontSize As Single
        Dim iNewFontSize As Single
        For Each con As Control In cons.Controls

            If TypeOf con Is Panel Then
            Else
                If TypeOf con Is DataGridView Then
                    If TypeOf con Is MachineListView AndAlso con.Name = "MachineListView_Data" Then
                    ElseIf TypeOf con Is HMIDataView AndAlso con.Name = "MachineListView_Data_Carrier" Then
                    ElseIf TypeOf con Is MachineListView AndAlso con.Name = "MachineListView_Parameter_Value" Then
                    ElseIf TypeOf con Is MachineListView AndAlso con.Name = "MachineListView_MachineStatus" Then
                    ElseIf TypeOf con Is MachineListView AndAlso con.Name = "MachineListView_VariantParameter" Then
                    Else
                        iOldFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size
                        iNewFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size * FontSizeRate
                        CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font(strFontName, iNewFontSize, CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Style)
                    End If

                    iOldFontSize = CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font.Size
                    iNewFontSize = CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font.Size * FontSizeRate
                    CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font(strFontName, iNewFontSize, CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font.Style)

                    iOldFontSize = CType(con, DataGridView).RowsDefaultCellStyle.Font.Size
                    iNewFontSize = CType(con, DataGridView).RowsDefaultCellStyle.Font.Size * FontSizeRate
                    CType(con, DataGridView).RowsDefaultCellStyle.Font = New System.Drawing.Font(strFontName, iNewFontSize, CType(con, DataGridView).RowsDefaultCellStyle.Font.Style)

                Else
                    iOldFontSize = con.Font.Size
                    iNewFontSize = con.Font.Size * FontSizeRate
                    con.Font = New System.Drawing.Font(con.Font.Name, iNewFontSize, con.Font.Style)

                End If
            End If

            If con.Controls.Count > 0 Then
                SetControls(FontSizeRate, con)
            End If
        Next
    End Sub


End Class
