Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Public Class clsDeviceProgramButton
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private lListIndex As New Dictionary(Of Integer, clsDeviceProgramCfg)
    Private strFilePath As String = ""
    Private iMax As Integer = 0
    Public Const Name As String = "DeviceProgramButton"

    Public ReadOnly Property ListIndex As Dictionary(Of Integer, clsDeviceProgramCfg)
        Get
            Return lListIndex
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData(ByVal strFilePath As String, ByVal iMax As Integer) As Boolean
        SyncLock _Object
            Try
                lListIndex.Clear()
                Dim strTempValue As String = ""
                Me.strFilePath = strFilePath
                Me.iMax = iMax
                For j = 1 To iMax
                    Dim cDeviceProgramCfg As New clsDeviceProgramCfg
                    strTempValue = cIniHandler.ReadIniFile(strFilePath, "Item" + j.ToString, "Type")
                    cDeviceProgramCfg.Type = strTempValue

                    strTempValue = cIniHandler.ReadIniFile(strFilePath, "Item" + j.ToString, "CylinderType")
                    If strTempValue = "" Then
                        strTempValue = "AB"
                    End If
                    cDeviceProgramCfg.CylinderType = strTempValue

                    strTempValue = cIniHandler.ReadIniFile(strFilePath, "Item" + j.ToString, "Index")
                    If strTempValue = "" Then
                        strTempValue = "0"
                    End If
                    cDeviceProgramCfg.Index = strTempValue

                  
                    lListIndex.Add(j, cDeviceProgramCfg)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function



    Public Function ChangeIO(ByVal strID As String, ByVal strType As String, ByVal strCylinderType As String, ByVal iIndex As Integer) As Boolean
        SyncLock _Object
            Try
                lListIndex(strID).Index = iIndex
                lListIndex(strID).Type = strType
                lListIndex(strID).CylinderType = strCylinderType
                SaveData()
                LoadData(strFilePath, iMax)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangPage(ByVal listNewIndex As Dictionary(Of String, String)) As Boolean
        SyncLock _Object
            Try
                Dim iIndex As Integer = 0
                Dim jIndex As Integer = 0
                For Each element As clsDeviceProgramCfg In lListIndex.Values
                    Dim iCnt As Integer = 0
                    Dim mTempValue As Integer = element.Index
                    Do While mTempValue > 8
                        mTempValue = mTempValue - 8
                        iCnt = iCnt + 1
                    Loop
                    If element.Index = 0 Then
                        iIndex = 0
                    Else
                        iIndex = iCnt + 1
                    End If

                    jIndex = mTempValue
                    If listNewIndex.ContainsKey(iIndex) Then
                        element.Index = (listNewIndex(iIndex) - 1) * 8 + jIndex
                    End If
                Next
                SaveData()
                LoadData(strFilePath, iMax)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetIOCfgFromID(ByVal strID As String) As clsDeviceProgramCfg
        Return lListIndex(strID)
    End Function


    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim j As Integer = 1
                For Each subelment As clsDeviceProgramCfg In lListIndex.Values
                    cIniHandler.WriteIniFile(strFilePath, "Item" + j.ToString, "Type", subelment.Type)
                    cIniHandler.WriteIniFile(strFilePath, "Item" + j.ToString, "CylinderType", subelment.CylinderType)
                    cIniHandler.WriteIniFile(strFilePath, "Item" + j.ToString, "Index", subelment.Index)

                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class

Public Class clsDeviceProgramCfg
    Public Type As String
    Public Index As Integer
    Public CylinderType As String
End Class


