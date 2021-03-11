Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Public Class clsDebugButtonManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private lListIO As New Dictionary(Of String, clsIOCfg)
    Public ReadOnly Property ListIO As Dictionary(Of String, clsIOCfg)
        Get
            Return lListIO
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
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                lListIO.Clear()
                Dim strTempValue As String = ""

                Dim i As Integer = 1
                For j = 1 To 10
                    Dim cIOCfg As New clsIOCfg(cSystemElement)
                    cIOCfg.ID = j.ToString
                    cIOCfg.Text = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text")
                    cIOCfg.Text2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text2")
                    If cIOCfg.Text = "" Then
                        cIOCfg.Text = cLanguageManager.GetTextLine("ChildrenIOForm", "Reserve")
                    End If
                    If cIOCfg.Text2 = "" Then
                        cIOCfg.Text2 = cLanguageManager.GetTextLine("ChildrenIOForm", "Reserve")
                    End If
                    cIOCfg.AdsName = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_AdsName")
                    cIOCfg.XIndex = i
                    cIOCfg.YIndex = j.ToString
                    cIOCfg.Reserve = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Reserve") = "False", False, True)
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType")
                    If strTempValue <> "" Then cIOCfg.IOTriggerType = [Enum].Parse(GetType(enumIOTriggerType), strTempValue)

                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Level")
                    If strTempValue <> "" Then
                        cIOCfg.Level = [Enum].Parse(GetType(enumUserLevel), strTempValue)
                    Else
                        cIOCfg.Level = enumUserLevel.Operator
                    End If



                    cIOCfg.IO = New OutputIO
                    cIOCfg.AdsName = HMI_PLC_Interface.HMI_DebugButton
                    lListIO.Add(j.ToString, cIOCfg)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function



    Public Function ChangeIO(ByVal strID As String, ByVal strText As String, ByVal strText2 As String, ByVal bReserve As Boolean, ByVal eIOTriggerType As enumIOTriggerType, ByVal eLevel As enumUserLevel) As Boolean
        SyncLock _Object
            Try
                lListIO(strID).Reserve = bReserve
                lListIO(strID).IOTriggerType = eIOTriggerType
                lListIO(strID).Level = eLevel
                lListIO(strID).Text = strText
                lListIO(strID).Text2 = strText2
                If lListIO(strID).Text = "" Then
                    lListIO(strID).Text = cLanguageManager.GetTextLine("ChildrenIOForm", "Reserve")
                End If
                If lListIO(strID).Text2 = "" Then
                    lListIO(strID).Text2 = cLanguageManager.GetTextLine("ChildrenIOForm", "Reserve")
                End If
                SaveData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetIOCfgFromID(ByVal strID As String) As clsIOCfg
        Return lListIO(strID)
    End Function


    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim j As Integer = 1
                Dim i As Integer = 1
                For Each subelment As clsIOCfg In lListIO.Values
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text", subelment.Text)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text2", subelment.Text2)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_AdsName", subelment.AdsName)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Reserve", subelment.Reserve.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType", subelment.IOTriggerType.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Level", subelment.Level.ToString)
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
