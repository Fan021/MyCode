Imports System.ComponentModel
Public Class LaserUI
    Implements ILaserUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements ILaserUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements ILaserUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements ILaserUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements ILaserUI.DataList
        Get
            Return DG_Laser
        End Get
    End Property

    Public ReadOnly Property Cmd As System.Windows.Forms.ComboBox Implements ILaserUI.Cmd
        Get
            Return cmbTemplateCmd
        End Get
    End Property

    Public ReadOnly Property OK As System.Windows.Forms.Button Implements ILaserUI.OK
        Get
            Return btnChangeTemplate
        End Get
    End Property

    Public ReadOnly Property Start As System.Windows.Forms.Button Implements ILaserUI.Start
        Get
            Return Button_Start
        End Get
    End Property



    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strTemplate As String, ByVal strVar As String, ByVal bResult As Boolean) As Boolean
        If DG_Laser.Rows.Count >= 20 Then
            DG_Laser.Rows.RemoveAt(DG_Laser.Rows.Count - 1)
        End If
        DG_Laser.Rows.Add()
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Template").Value = strTemplate
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Var").Value = strVar
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_Laser.Rows(DG_Laser.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Laser.Sort(DG_Laser.Columns("Time"), ListSortDirection.Descending)
        DG_Laser.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Laser.Columns.Clear()
        DG_Laser.Columns.Add("SN", "SN")
        DG_Laser.Columns.Add("Article", "Article")
        DG_Laser.Columns.Add("Customer", "Customer")
        DG_Laser.Columns.Add("ProductFamily", "ProductFamily")
        DG_Laser.Columns.Add("Template", "Template")
        DG_Laser.Columns.Add("Var", "Var")
        DG_Laser.Columns.Add("Result", "Result")
        DG_Laser.Columns.Add("Time", "Time")
        DG_Laser.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Return True
    End Function
End Class