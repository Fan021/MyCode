Public Class clsBarcodeManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cSystemManager As clsSystemManager
    Private strSFC As String = String.Empty
    Private strMaterialNumber As String = String.Empty
    Private strMaterialVersion As String = String.Empty
    Private strQuantity As String = String.Empty
    Private strDate As String = String.Empty
    Private strBatch As String = String.Empty
    Private strVendor As String = String.Empty
    Private strHandlingUnit As String = String.Empty
    Public Const Name As String = "BarcodeManager"

    Public Property SFC As String
        Set(ByVal value As String)
            strSFC = value
        End Set
        Get
            Return strSFC
        End Get
    End Property

    Public Property MaterialNumber As String
        Set(ByVal value As String)
            strMaterialNumber = value
        End Set
        Get
            Return strMaterialNumber
        End Get
    End Property

    Public Property MaterialVersion As String
        Set(ByVal value As String)
            strMaterialVersion = value
        End Set
        Get
            Return strMaterialVersion
        End Get
    End Property

    Public Property Quantity As String
        Set(ByVal value As String)
            strQuantity = value
        End Set
        Get
            Return strQuantity
        End Get
    End Property

    Public Property [Date] As String
        Set(ByVal value As String)
            strDate = value
        End Set
        Get
            Return strDate
        End Get
    End Property

    Public Property Batch As String
        Set(ByVal value As String)
            strBatch = value
        End Set
        Get
            Return strBatch
        End Get
    End Property

    Public Property Vendor As String
        Set(ByVal value As String)
            strVendor = value
        End Set
        Get
            Return strVendor
        End Get
    End Property

    Public Property HandlingUnit As String
        Set(ByVal value As String)
            strHandlingUnit = value
        End Set
        Get
            Return strHandlingUnit
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class
