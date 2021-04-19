Imports System.Windows.Forms
Imports System.Drawing
Public Class clsFormFontResize
    ' Private ioldH As Integer = 984
    Private ioldH As Integer = 984
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
            inewH = value
            isResized = True
            CalculateFontSize()
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
            Return Single.Parse((inewH / iOriginalH * 1.0).ToString)
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
        If inewH <> ioldH Then
            Dim iFontSizeRate As Single = 1.0
            If inewH > ioldH Then
                iFontSizeRate = 1.0
            Else
                iFontSizeRate = Single.Parse((inewH * 1.2 / ioldH * 1.0).ToString)
            End If
            iCurrentRate = iFontSizeRate
            If iCurrentRate > 0 And iCurrentRate < 10 Then
                SetControls(iFontSizeRate, ccons)
            End If
        Else
            iCurrentRate = 1.0
            If iCurrentRate > 0 And iCurrentRate < 10 Then
                SetControls(iCurrentRate, ccons)
            End If
        End If
        ' ioldH = inewH
    End Sub

    Public Sub ChangeFontSize()
        If inewH < 100 Then
            Return
        End If
        If ioldH > 0 Then
            Dim iFontSizeRate As Single = Single.Parse((inewH / ioldH * 1.0).ToString)
            iCurrentRate = iFontSizeRate
            If iCurrentRate > 0 And iCurrentRate < 10 Then
                SetControls(iFontSizeRate, ccons)
            End If
        End If
    End Sub



    Public Sub SetControls(ByVal FontSizeRate As Single, ByVal cons As Control)
        Try
            If IsNothing(cons) Then Return
            If FontSizeRate <= 0 Then Return
            Dim iOldFontSize As Single
            Dim iNewFontSize As Single
            For Each con As Control In cons.Controls
                If IsNothing(con) Then Continue For

                If TypeOf con Is Panel Then
                Else
                    If TypeOf con Is DataGridView Then
                        If con.Name = "dgView" Then
                            If Not IsNothing(CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font) Then
                                iOldFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size
                                iNewFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size * FontSizeRate

                                If FontSizeRate = 1 Then
                                    CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Bold)
                                    CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Bold)
                                    CType(con, DataGridView).RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Bold)
                                Else
                                    iNewFontSize = Single.Parse((iNewFontSize * 0.95).ToString)
                                    CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Regular)
                                    CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Regular)
                                    CType(con, DataGridView).RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, FontStyle.Regular)
                                End If
                            End If
                        Else
                            If Not IsNothing(CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font) Then
                                iOldFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size
                                iNewFontSize = CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Size * FontSizeRate
                                CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Style)
                                CType(con, DataGridView).AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Style)
                                CType(con, DataGridView).RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iNewFontSize, CType(con, DataGridView).ColumnHeadersDefaultCellStyle.Font.Style)

                            End If
                        End If

                    Else
                        iOldFontSize = con.Font.Size
                        iNewFontSize = con.Font.Size * FontSizeRate
                        con.Font = New System.Drawing.Font("Calibri", iNewFontSize, con.Font.Style)

                    End If
                End If

                If con.Controls.Count > 0 Then
                    SetControls(FontSizeRate, con)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Class
