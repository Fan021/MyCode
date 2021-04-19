Imports Kostal.Las.Base

Public Class clsGlobalParameter
    Public Const Name As String = "MachineGlobalParameter"
    Public lMachineGlobalParameter As New Dictionary(Of String, clsParameterCfg)
    Protected lCurrentMachineGlobalParameter As New Dictionary(Of String, clsParameterCfg)
    Protected cIniHandler As New clsIniHandler
    Private _Object As New Object
    Private AppSettings As Settings
    Private XmlHandler As New XmlHandler
    Private Devices As Dictionary(Of String, Object)
    Private _AppArticle As Article
    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        SyncLock _Object
            Try
                Me.Devices = Devices
                _AppArticle = Devices(Article.Name)
                Me.AppSettings = _AppSettings
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasMachineGlobalParameterFromKey(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strKey) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.Count <> lCurrentMachineGlobalParameter.Count Then
                    Return False
                End If
                For Each element In lCurrentMachineGlobalParameter.Keys
                    If Not lMachineGlobalParameter.ContainsKey(element) Then
                        Return False
                    End If
                    If lMachineGlobalParameter(element) <> lCurrentMachineGlobalParameter(element) Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentGlobalParameter(ByVal strName As String) As clsParameterCfg
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    Return lCurrentMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetCurrentGlobalParameter(ByVal strName As String, ByVal oSource As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentMachineGlobalParameter.ContainsKey(strName) Then
                    lCurrentMachineGlobalParameter(strName).Value = oSource
                End If
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetGlobalParameter(ByVal strName As String) As Object
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    Return lMachineGlobalParameter(strName)
                End If
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetGlobalParameter(ByVal strName As String, ByVal oSource As Object) As Boolean
        SyncLock _Object
            Try
                If lMachineGlobalParameter.ContainsKey(strName) Then
                    lMachineGlobalParameter(strName) = oSource
                End If
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function


    Public Function Clone(ByRef SrcList As Dictionary(Of String, clsParameterCfg), ByRef TarList As Dictionary(Of String, clsParameterCfg)) As Boolean
        SyncLock _Object
            Try
                TarList.Clear()
                For Each element In SrcList.Keys
                    TarList.Add(element, SrcList(element).Clone)
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadMachineGlobalParameter() As Boolean
        SyncLock _Object
            Try
                lMachineGlobalParameter.Clear()
                lMachineGlobalParameter = XmlHandler.GetParameterCfgs(AppSettings.ConfigFolder, "Parameter.xml")
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeParameterValueToType(ByVal ValueType As Type, ByVal oSource As String) As Object
        SyncLock _Object
            Try
                Select Case ValueType
                    Case GetType(Boolean)
                        Return IIf(oSource.ToUpper = "TRUE", True, False)
                    Case GetType(Integer)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New Exception(oSource + "  is not Numeric")
                        End If
                        Return CInt(oSource)
                    Case GetType(Byte)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New Exception(oSource + "  is not Numeric")
                        End If
                        Return Byte.Parse(oSource)

                    Case GetType(Single)
                        If oSource = "" Then
                            Return 0
                        End If
                        If Not IsNumeric(oSource) Then
                            Throw New Exception(oSource + "  is not Numeric")
                        End If
                        Return Single.Parse(oSource)
                    Case GetType(String)
                        Return oSource.ToString
                End Select
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                XmlHandler.SaveParameterCfgs(AppSettings.ConfigFolder, "Parameter.xml", lCurrentMachineGlobalParameter)
                Clone(lCurrentMachineGlobalParameter, lMachineGlobalParameter)
                WriteValue()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function WriteValue() As Boolean
        SyncLock _Object
            Try
                For Each element As clsParameterCfg In lMachineGlobalParameter.Values
                    If Not Devices.ContainsKey(element.PLC) Then
                        Throw New Exception("Name:" + element.Name + " PLC:" + element.PLC + " is Null")
                    End If
                    Dim tTwinCatAds As TwinCatAds = Devices(element.PLC)
                    Dim strValue As String = ""
                    If element.VariantChange Then
                        If Not _AppArticle.ArticleElements.ContainsKey(element.VariantElement) Then
                            Throw New Exception("Name:" + element.Name + " VariantElement:" + element.VariantElement + " is Null")
                        End If
                        strValue = _AppArticle.ArticleElements(element.VariantElement).Data.ToString
                    Else
                        strValue = element.Value
                    End If

                    Select Case element.Type
                        Case GetType(Boolean)
                            Dim oBjectValue As Boolean
                            oBjectValue = IIf(strValue.ToUpper = "TRUE", True, False)
                            tTwinCatAds.WriteAny(element.AdsName, oBjectValue)
                        Case GetType(Integer)
                            If strValue = "" Then
                                Return 0
                            End If
                            If Not IsNumeric(strValue) Then
                                Throw New Exception(strValue + "  is not Numeric")
                            End If
                            Dim oBjectValue As Int16
                            oBjectValue = Int16.Parse(strValue)
                            tTwinCatAds.WriteAny(element.AdsName, oBjectValue)
                        Case GetType(Byte)
                            If strValue = "" Then
                                Return 0
                            End If
                            If Not IsNumeric(strValue) Then
                                Throw New Exception(strValue + "  is not Numeric")
                            End If
                            Dim oBjectValue As Byte
                            oBjectValue = Byte.Parse(strValue)
                            tTwinCatAds.WriteAny(element.AdsName, oBjectValue)

                        Case GetType(Single)
                            If strValue = "" Then
                                Return 0
                            End If
                            If Not IsNumeric(strValue) Then
                                Throw New Exception(strValue + "  is not Numeric")
                            End If
                            Dim oBjectValue As Single
                            oBjectValue = Single.Parse(strValue)
                            tTwinCatAds.WriteAny(element.AdsName, oBjectValue)
                        Case GetType(String)
                            Dim oBjectValue As String
                            oBjectValue = strValue
                            tTwinCatAds.WriteAny(element.AdsName, oBjectValue, New Integer() {oBjectValue.Length})
                    End Select
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                For Each element As String In lCurrentMachineGlobalParameter.Keys
                    ' If lCurrentMachineGlobalParameter(element).ToString = "" Then
                    'Throw New clsHMIException(cLanguageManager.GetTextLine("MachineGlobalParameter", "1", element), enumExceptionType.Alarm)
                    ' End If
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentGlobalParameter() As Boolean
        SyncLock _Object
            Try
                Clone(lMachineGlobalParameter, lCurrentMachineGlobalParameter)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

End Class

