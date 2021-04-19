Imports Kostal.Las.Base

Public Class clsProductionManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Public cDs_Data As DataSet
    Protected cLanguage As Language
    Private cDataGridViewPage_Data As clsDataGridViewPage_Bosh
    Private cHMIDataView_Data As HMIDataView
    Private AppSettings As Settings
    Private cProductionData As clsProductionData
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = Devices
                Me.AppSettings = _AppSettings
                cLanguage = CType(Devices(Language.Name), Language)
                cProductionData = New clsProductionData
                cProductionData.Init("MySqlServ.ini", Nothing, AppSettings, cLanguage)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
    Public Function RegisterManager(ByVal cDataGridViewPage_Data As clsDataGridViewPage_Bosh, ByVal cHMIDataView_Data As HMIDataView) As Boolean
        Me.cDataGridViewPage_Data = cDataGridViewPage_Data
        Me.cHMIDataView_Data = cHMIDataView_Data
        Return True
    End Function

    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        Try
            cDs_Data = New DataSet
            cProductionData.SelectToDataView(cDs_Data, cListSearchContion)
            For Each mDc As DataColumn In cDs_Data.Tables(0).Columns
                mDc.ColumnName = cLanguage.Read("ProductionDataManager", mDc.ColumnName)
            Next

            If Not IsNothing(cDataGridViewPage_Data) Then
                cDataGridViewPage_Data.SetDataView = cDs_Data.Tables(0).DefaultView
                cDataGridViewPage_Data.Paging(cViewPageType)
            Else
                cHMIDataView_Data.DataSource = cDs_Data.Tables(0)
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
End Class
