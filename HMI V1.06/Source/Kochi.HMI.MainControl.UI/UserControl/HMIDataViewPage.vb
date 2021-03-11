Imports System.Windows.Forms

Public Class HMIDataViewPage
    Private bButton_UpFirstEnable As Boolean = False
    Private bButton_UpEnable As Boolean = False
    Private bButton_DownLastEnable As Boolean = False
    Private bButton_DownEnable As Boolean = False
    Private bButton_GoEnable As Boolean = False
    Private iTotalRecord As Integer = 0
    Private iTotallPage As Integer = 0
    Private iCurrentPage As Integer = 0
    Private Sub HMIDataViewPage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HmiTextBox_Page.TextBox.TextAlign = HorizontalAlignment.Center
        AddHandler HmiTextBox_Page.TextBox.TextChanged, AddressOf TextBox_TextChanged
        UpdateForm()
    End Sub

    Public Property TotalRecord As Integer
        Set(ByVal value As Integer)
            iTotalRecord = value
        End Set
        Get
            Return iTotalRecord
        End Get
    End Property

    Public Property TotallPage As Integer
        Set(ByVal value As Integer)
            iTotallPage = value
        End Set
        Get
            Return iTotallPage
        End Get
    End Property


    Public Property CurrentPage As Integer
        Set(ByVal value As Integer)
            iCurrentPage = value
        End Set
        Get
            Return iCurrentPage
        End Get
    End Property

    Public Property Button_UpFirstEnable As Boolean
        Set(ByVal value As Boolean)
            bButton_UpFirstEnable = value
        End Set
        Get
            Return bButton_UpFirstEnable
        End Get
    End Property

    Public Property Button_UpEnable As Boolean
        Set(ByVal value As Boolean)
            bButton_UpEnable = value
        End Set
        Get
            Return bButton_UpEnable
        End Get
    End Property

    Public Property Button_GoEnable As Boolean
        Set(ByVal value As Boolean)
            bButton_GoEnable = value
            'InitForm()
        End Set
        Get
            Return bButton_GoEnable
        End Get
    End Property

    Public Property Button_DownEnable As Boolean
        Set(ByVal value As Boolean)
            bButton_DownEnable = value
        End Set
        Get
            Return bButton_DownEnable
        End Get
    End Property

    Public Property Button_DownLastEnable As Boolean
        Set(ByVal value As Boolean)
            bButton_DownLastEnable = value
        End Set
        Get
            Return bButton_DownLastEnable
        End Get
    End Property

    Public Sub UpdateForm()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HMIDataViewPage))
        If Not bButton_UpFirstEnable Then
            Button_UpFirst.BackgroundImage = CType(resources.GetObject("ArrowUpFirstDisable"), System.Drawing.Image)
            Button_UpFirst.Enabled = False
        Else
            Button_UpFirst.BackgroundImage = CType(resources.GetObject("ArrowUpFirst"), System.Drawing.Image)
            Button_UpFirst.Enabled = True
        End If
        If Not bButton_UpEnable Then
            Button_Up.BackgroundImage = CType(resources.GetObject("ArrowUpDisable"), System.Drawing.Image)
            Button_Up.Enabled = False
        Else
            Button_Up.BackgroundImage = CType(resources.GetObject("ArrowUp"), System.Drawing.Image)
            Button_Up.Enabled = True
        End If

        If Not bButton_GoEnable Then
            Button_Go.BackgroundImage = CType(resources.GetObject("GoDisable"), System.Drawing.Image)
            Button_Go.Enabled = False
        Else
            Button_Go.BackgroundImage = CType(resources.GetObject("Go"), System.Drawing.Image)
            Button_Go.Enabled = True
        End If

        If Not bButton_DownEnable Then
            Button_Down.BackgroundImage = CType(resources.GetObject("ArrowDownDisable"), System.Drawing.Image)
            Button_Down.Enabled = False
        Else
            Button_Down.BackgroundImage = CType(resources.GetObject("ArrowDown"), System.Drawing.Image)
            Button_Down.Enabled = True
        End If
        If Not bButton_DownLastEnable Then
            Button_DownLast.BackgroundImage = CType(resources.GetObject("ArrowDowbLastDisable"), System.Drawing.Image)
            Button_DownLast.Enabled = False
        Else
            Button_DownLast.BackgroundImage = CType(resources.GetObject("ArrowDownLast"), System.Drawing.Image)
            Button_DownLast.Enabled = True
        End If

        Label_Total.Text = "Total:" + iTotalRecord.ToString
        Label_TotalPage.Text = "/" + iTotallPage.ToString
        HmiTextBox_Page.TextBox.Text = iCurrentPage.ToString
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not IsNumeric(HmiTextBox_Page.TextBox.Text) Then
                HmiTextBox_Page.TextBox.Text = iCurrentPage.ToString

            End If
            If CInt(HmiTextBox_Page.TextBox.Text) >= Integer.MaxValue Then
                HmiTextBox_Page.TextBox.Text = iCurrentPage.ToString
                Return
            End If
            If CInt(HmiTextBox_Page.TextBox.Text) <= 0 Then
                HmiTextBox_Page.TextBox.Text = iCurrentPage.ToString
                Return
            End If
            iCurrentPage = CInt(HmiTextBox_Page.TextBox.Text)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
