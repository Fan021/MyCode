Public Class FormConfig
    Public SessionData As New SessionData
    Public dataStoreCfg As New DataCfg
    Private Sql As UserData
    Private ds As DataSet
    Private dtInfo As DataTable
    Private pageSize As Integer = 10
    Private nMax As Integer
    Private pageCount As Integer
    Private pageCurrent As Integer
    Private nCurrent As Integer
    Private SqlConfig As ConfigData
    Private mTempValue As String = String.Empty

    Private Sub FormConfig_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub FormConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If SessionData.Level < 3 Then
            TabPage2.Parent = Nothing
        End If
        If SessionData.Level < 2 Then
            MESEnable.Hide()
            MESEnableN.Hide()
            PassiveModeEnable.Hide()
            PassiveModeEnableN.Hide()
            LineControlEnable.Hide()
            LineControlEnableN.Hide()
            Label38.Hide()
            Label39.Hide()
            Label55.Hide()
        End If
        'Sql = New UserData
        'Sql.Init(dataStoreCfg)
        'DisplayData()
        'SqlConfig = New ConfigData
        'SqlConfig.Init(dataStoreCfg)
        'LoadConfigData()
    End Sub

    Private Function DisplayData() As Boolean

        DataGridView1.DataSource = Nothing
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_User.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("Loading Data Fail")
            Return False
        End If
        Return True
    End Function

    Private Sub InitDataSet()

        nMax = dtInfo.Rows.Count
        pageCount = nMax \ pageSize
        If ((nMax Mod pageSize) > 0) Then pageCount = pageCount + 1
        pageCurrent = 1
        nCurrent = 0
        If (nMax = 0) Then pageCount = 1
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim nStartPos As Integer = 0
        Dim nEndPos As Integer = 0
        Dim dtTemp As DataTable = dtInfo.Clone
        If (pageCurrent = pageCount) Then
            nEndPos = nMax
        Else
            nEndPos = pageSize * pageCurrent
        End If
        nStartPos = nCurrent

        lblPageCount.Text = "/" + pageCount.ToString()
        txtCurrentPage.Text = Convert.ToString(pageCurrent)
        For i = nStartPos To nEndPos - 1
            dtTemp.ImportRow(dtInfo.Rows(i))
            nCurrent = nCurrent + 1
        Next
        bdsInfo.DataSource = dtTemp
        bdnInfo.BindingSource = bdsInfo
        DataGridView1.DataSource = bdsInfo
    End Sub


    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.CurrentRow.Index < DataGridView1.Rows.Count - 1 Then
            TextBox_Edit_Id.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value
            TextBox_Edit_UserName.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(1).Value
            TextBox_Edit_Password.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value
            For i = 0 To ComboBox_Level.Items.Count - 1
                If ComboBox_Level.Items(i) = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(3).Value Then
                    ComboBox_Level.SelectedIndex = i
                End If
            Next
        End If
    End Sub

    Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click
        If TextBox_Edit_UserName.Text = "" Then
            MessageBox.Show("用户名为空")
            Return
        End If
        If TextBox_Edit_Password.Text = "" Then
            MessageBox.Show("密码为空")
            Return
        End If
        If ComboBox_Level.SelectedIndex < 0 Then
            MessageBox.Show("等级为空")
            Return
        End If

        If Sql.GetUserName(TextBox_Edit_UserName.Text) Then
            MessageBox.Show("用户名已经存在")
            Return
        End If
        If Not Sql.InSertData(TextBox_Edit_UserName.Text, TextBox_Edit_Password.Text, CInt(ComboBox_Level.SelectedItem.ToString)) Then
            MessageBox.Show("新增失败")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("新增成功")
        End If
    End Sub

    Private Sub Button_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Modify.Click
        If TextBox_Edit_UserName.Text = "" Then
            MessageBox.Show("用户名为空")
            Return
        End If
        If TextBox_Edit_Password.Text = "" Then
            MessageBox.Show("密码为空")
            Return
        End If
        If ComboBox_Level.SelectedIndex < 0 Then
            MessageBox.Show("等级为空")
            Return
        End If

        If Not Sql.GetUserName(TextBox_Edit_UserName.Text) Then
            MessageBox.Show("用户名不存在")
            Return
        End If
        If Not Sql.UpdateData(TextBox_Edit_Id.Text, TextBox_Edit_UserName.Text, TextBox_Edit_Password.Text, CInt(ComboBox_Level.SelectedItem.ToString)) Then
            MessageBox.Show("成功失败")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("修改成功")
        End If
    End Sub

    Private Sub Button_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Delete.Click
        If TextBox_Edit_Id.Text = "" Then
            MessageBox.Show("用户ID不能为空")
            Return
        End If

        If Not Sql.GetUserName(TextBox_Edit_UserName.Text) Then
            MessageBox.Show("用户名不存在")
            Return
        End If

        If Not Sql.DelectData(TextBox_Edit_Id.Text) Then
            MessageBox.Show("删除失败")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("删除成功")
        End If
    End Sub

    Private Sub Button_Check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Check.Click
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_Edit_UserName.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("查询失败")
        End If
    End Sub

    Private Sub Button_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Reset.Click
        TextBox_Edit_UserName.Text = ""
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_Edit_UserName.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("查询失败")
        End If
    End Sub

    Private Sub bdnInfo_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles bdnInfo.ItemClicked
        If (e.ClickedItem.Name = "ToolStripLabel1") Then
            pageCurrent = pageCurrent - 1
            If (pageCurrent <= 0) Then
                pageCurrent = 0
                Return
            Else
                nCurrent = pageSize * (pageCurrent - 1)
            End If
            LoadData()
        End If

        If (e.ClickedItem.Name = "ToolStripLabel2") Then
            pageCurrent = pageCurrent + 1
            If (pageCurrent > pageCount) Then
                pageCurrent = pageCount
                Return
            Else
                nCurrent = pageSize * (pageCurrent - 1)
            End If
            LoadData()
        End If
    End Sub

    Private Sub LoadConfigData()

        For Each con As Control In TabPage1.Controls
            If TypeOf (con) Is GroupBox Then
                For Each conchild As Control In con.Controls
                    '1
                    If TypeOf (conchild) Is ComboBox Then
                        If SqlConfig.GetItemValue(con.Name + "." + conchild.Name, mTempValue) Then
                            For i = 0 To CType(conchild, ComboBox).Items.Count - 1
                                If CType(conchild, ComboBox).Items(i).ToString = mTempValue Then
                                    CType(conchild, ComboBox).SelectedIndex = i
                                End If
                            Next
                        End If
                    End If
                    '2
                    If TypeOf (conchild) Is TextBox Then
                        If SqlConfig.GetItemValue(con.Name + "." + conchild.Name, mTempValue) Then
                            CType(conchild, TextBox).Text = mTempValue
                        Else
                            CType(conchild, TextBox).Text = ""
                        End If
                    End If

                    '3
                    If TypeOf (conchild) Is RadioButton Then
                        If conchild.Name.IndexOf("Enable") > 0 And conchild.Name.IndexOf("EnableN") < 0 Then
                            If SqlConfig.GetItemValue(con.Name + "." + conchild.Name, mTempValue) Then
                                If mTempValue.ToUpper = "TRUE" Then
                                    CType(conchild, RadioButton).Checked = True
                                    CType(con.Controls.Find(conchild.Name + "N", False)(0), RadioButton).Checked = False
                                Else
                                    CType(conchild, RadioButton).Checked = False
                                    CType(con.Controls.Find(conchild.Name + "N", False)(0), RadioButton).Checked = True
                                End If
                            Else
                                CType(conchild, RadioButton).Checked = False
                                CType(con.Controls.Find(conchild.Name + "N", False)(0), RadioButton).Checked = False
                            End If
                        End If
                    End If

                    '4
                    If TypeOf (conchild) Is Panel Then
                        For Each conchild1 As Control In conchild.Controls
                            If TypeOf (conchild1) Is RadioButton Then
                                If conchild1.Name.IndexOf("Enable") > 0 And conchild1.Name.IndexOf("EnableN") < 0 Then
                                    If SqlConfig.GetItemValue(con.Name + "." + conchild1.Name, mTempValue) Then
                                        If mTempValue.ToUpper = "TRUE" Then
                                            CType(conchild1, RadioButton).Checked = True
                                            CType(conchild.Controls.Find(conchild1.Name + "N", False)(0), RadioButton).Checked = False
                                        Else
                                            CType(conchild1, RadioButton).Checked = False
                                            CType(conchild.Controls.Find(conchild1.Name + "N", False)(0), RadioButton).Checked = True
                                        End If
                                    Else
                                        CType(conchild1, RadioButton).Checked = False
                                        CType(conchild.Controls.Find(conchild1.Name + "N", False)(0), RadioButton).Checked = False
                                    End If

                                End If

                            End If
                        Next

                    End If

                Next
            End If

        Next




    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click
       
        For Each con As Control In TabPage1.Controls
            If TypeOf (con) Is GroupBox Then
                For Each conchild As Control In con.Controls
                    '1
                    If TypeOf (conchild) Is ComboBox Then
                        If CType(conchild, ComboBox).SelectedIndex < 0 Then
                            MessageBox.Show(con.Name + "." + conchild.Name + " 检查失败")
                            Return
                        End If
                        If Not SqlConfig.InSertData(con.Name + "." + conchild.Name, CType(conchild, ComboBox).SelectedItem.ToString) Then
                            MessageBox.Show(con.Name + "." + conchild.Name + " 保存失败")
                            Return
                        End If
                    End If
                    '2
                    If TypeOf (conchild) Is TextBox Then
                        If CType(conchild, TextBox).Text = "" Then
                            MessageBox.Show(con.Name + "." + conchild.Name + " 检查失败")
                            Return
                        End If
                        If Not SqlConfig.InSertData(con.Name + "." + conchild.Name, CType(conchild, TextBox).Text) Then
                            MessageBox.Show(con.Name + "." + conchild.Name + " 保存失败")
                            Return
                        End If
                    End If
                    '3
                    If TypeOf (conchild) Is RadioButton Then
                        If conchild.Name.IndexOf("Enable") > 0 And conchild.Name.IndexOf("EnableN") < 0 Then
                            If CType(conchild, RadioButton).Checked = False And CType(con.Controls.Find(conchild.Name + "N", False)(0), RadioButton).Checked = False Then
                                MessageBox.Show(con.Name + "." + conchild.Name + " 检查失败")
                                Return
                            End If
                            If CType(conchild, RadioButton).Checked = True Then
                                If Not SqlConfig.InSertData(con.Name + "." + conchild.Name, "TRUE") Then
                                    MessageBox.Show(con.Name + "." + conchild.Name + " 保存失败")
                                    Return
                                End If
                            Else
                                If Not SqlConfig.InSertData(con.Name + "." + conchild.Name, "FALSE") Then
                                    MessageBox.Show(con.Name + "." + conchild.Name + " 保存失败")
                                    Return
                                End If
                            End If
                        End If
                    End If

                    '4
                    If TypeOf (conchild) Is Panel Then
                        For Each conchild1 As Control In conchild.Controls
                            If TypeOf (conchild1) Is RadioButton Then
                                If conchild1.Name.IndexOf("Enable") > 0 And conchild1.Name.IndexOf("EnableN") < 0 Then
                                    If CType(conchild1, RadioButton).Checked = False And CType(conchild.Controls.Find(conchild1.Name + "N", False)(0), RadioButton).Checked = False Then
                                        MessageBox.Show(con.Name + "." + conchild1.Name + " 检查失败")
                                        Return
                                    End If
                                    If CType(conchild1, RadioButton).Checked = True Then
                                        If Not SqlConfig.InSertData(con.Name + "." + conchild1.Name, "TRUE") Then
                                            MessageBox.Show(con.Name + "." + conchild1.Name + " 保存失败")
                                            Return
                                        End If
                                    Else
                                        If Not SqlConfig.InSertData(con.Name + "." + conchild1.Name, "FALSE") Then
                                            MessageBox.Show(con.Name + "." + conchild1.Name + " 保存失败")
                                            Return
                                        End If
                                    End If


                                End If

                            End If
                        Next

                    End If


                Next
            End If
        Next
        MessageBox.Show("保存成功")
        LoadConfigData()
    End Sub

End Class