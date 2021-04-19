Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text



Public Enum enumMesStartTest
    MES_START_NOTHING = 0
    MES_START_NONE = 1
    MES_START_PASS = 2
    MES_START_FAIL = 3
End Enum


Public Enum enumMesCompleteTest
    MES_COMPLETE_NOTHING = 0
    MES_COMPLETE_NONE = 1
    MES_COMPLETE_PASS = 2
    MES_COMPLETE_FAIL = 3
End Enum

Public Enum enumMESStatus
    WindowsError = -99
    FailWhileMesStart = -5
    FailWhileMeComplete = -4
    FailWhileInit = -1
    NotInitialized = 0
    Initialized = 1
    Disabled = 2
    StartPass = 3
    CompletePass = 4
End Enum

Public Class MES
    Protected _Status As enumMESStatus
    Protected _StatusDescription As String = ""
    Protected _ParentIdString As String
    Protected _MesControllerName As String
    Protected _MesControllerIniFile As String
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station
    Protected _FileHandler As New FileHandler

    Protected Delegate Function dMesStartStamp(ByVal sSerialNumber As String, ByVal strLogParameter As String) As enumMESStatus
    Protected pMesStartStamp As New dMesStartStamp(AddressOf _MesStartStamp)
    Protected pMesStartStampCB As AsyncCallback = New AsyncCallback(AddressOf _MesStartStampCB)
    Protected _MesStartStamp_RUN As Boolean

    Protected Delegate Function dMesCompleteStamp(ByVal sSerialNumber As String, ByVal bResult As Boolean, ByVal strErrorMessage As String) As enumMESStatus
    Protected pMesCompleteStamp As New dMesCompleteStamp(AddressOf _MesCompleteStamp)
    Protected pMesCompleteStampCB As AsyncCallback = New AsyncCallback(AddressOf _MesCompleteStampCB)
    Protected _MesCompleteStamp_RUN As Boolean
    Protected cMESInterface As clsMESInterface
    Protected _LastMesStartStamp As Boolean
    Protected _LastMesCompleteStamp As Boolean


    Public ReadOnly Property MesStartStamp_RUN() As Boolean
        Get
            Return _MesStartStamp_RUN
        End Get
    End Property

    Public ReadOnly Property MesCompleteStamp_RUN() As Boolean
        Get
            Return _MesCompleteStamp_RUN
        End Get
    End Property


    Public ReadOnly Property Status() As enumMESStatus
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public Sub New()
    End Sub



    Public Function Init(ByVal ParentIdString As String, ByVal MesControllerName As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language, ByVal MesControllerIniFile As String) As Boolean
        Dim INISection As String = String.Empty
        Dim INIFile As String = String.Empty
        _ParentIdString = ParentIdString
        _MesControllerName = MesControllerName
        _MesControllerIniFile = MesControllerIniFile
        _Status = enumMESStatus.NotInitialized
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        _MesStartStamp_RUN = False
        _MesCompleteStamp_RUN = False
        cMESInterface = New clsMESInterface

        If _ParentIdString Is Nothing Or _ParentIdString = String.Empty Then
            _Status = enumMESStatus.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_INIT1)
            Return False
        End If

        If _MesControllerName Is Nothing Or _MesControllerName = String.Empty Then
            _Status = enumMESStatus.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_INIT2, _ParentIdString, _MesControllerName)
            Return False
        End If

        If _MesControllerIniFile = String.Empty Or _MesControllerIniFile = _FileHandler.ErrorString Then
            _Status = enumMESStatus.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_INIT2, _MesControllerIniFile, _ParentIdString, _MesControllerName)
            Return False
        End If


        INISection = "Setting"
        INIFile = AppSettings.ConfigFolder + _MesControllerIniFile

        cMESInterface.strUrl = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Address"))
        cMESInterface.strResourceId = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Resourceid"))
        cMESInterface.strOperationId = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Operation"))
        cMESInterface.strUserName = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Username"))
        cMESInterface.strPassword = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Password"))
        cMESInterface.strNccode = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "Password"))
        cMESInterface.strLogParameter = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, "Setting", "LogParameter"))


        If _FileHandler.ErrorString = _FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, _ParentIdString + "_" + _MesControllerName + "_SETTINGS", "Start") Then
            _Status = enumMESStatus.FailWhileInit
            _StatusDescription = _ParentIdString + "_" + _MesControllerName + "_SETTINGS does not Exist"
            Return False
        End If


        _Status = enumMESStatus.Initialized
        _StatusDescription = ""
        Return cMESInterface.Init(AppSettings.MesFolder)
    End Function


    Public Overloads Sub MesStart(ByVal sSerialNumber As String, ByVal strLogParameter As String)
        _MesStartStamp_RUN = True
        pMesStartStamp.BeginInvoke(sSerialNumber, strLogParameter, pMesStartStampCB, Nothing)
    End Sub

    Public Overloads Sub MesComplete(ByVal sSerialNumber As String, ByVal bResult As Boolean, ByVal strErrorMessage As String)
        _MesCompleteStamp_RUN = True
        pMesCompleteStamp.BeginInvoke(sSerialNumber, bResult, strErrorMessage, pMesCompleteStampCB, Nothing)
    End Sub

    Protected Sub _MesStartStampCB(ByVal Result As IAsyncResult)
        _LastMesStartStamp = pMesStartStamp.EndInvoke(Result)
        _MesStartStamp_RUN = False
    End Sub

    Public Function HasStart() As Boolean
        Dim INISection As String = String.Empty
        Dim INIFile As String = String.Empty
        INISection = _ParentIdString + "_" + _MesControllerName + "_SETTINGS"
        INIFile = AppSettings.ConfigFolder + _MesControllerIniFile
        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "Start")).ToUpper() <> "NONE" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function HasComplete() As Boolean
        Dim INISection As String = String.Empty
        Dim INIFile As String = String.Empty
        INISection = _ParentIdString + "_" + _MesControllerName + "_SETTINGS"
        INIFile = AppSettings.ConfigFolder + _MesControllerIniFile
        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "Complete")).ToUpper() <> "NONE" Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function _MesStartStamp(ByVal sSerialNumber As String, ByVal strLogParameter As String) As enumMESStatus
        Dim INISection As String = String.Empty
        Dim INIFile As String = String.Empty

        _StatusDescription = ""
        INISection = _ParentIdString + "_" + _MesControllerName + "_SETTINGS"
        INIFile = AppSettings.ConfigFolder + _MesControllerIniFile

        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "Start")).ToUpper() <> "NONE" Then
            If Not cMESInterface.Start(sSerialNumber, _StatusDescription) Then
                _Status = enumMESStatus.FailWhileMesStart
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_Start_Fail, _StatusDescription)
                Return False
            End If
        End If

        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "ValidateBOM")).ToUpper() <> "NONE" Then
            If Not cMESInterface.validateBOM(sSerialNumber, _StatusDescription) Then
                _Status = enumMESStatus.FailWhileMesStart
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_ValidateBOM_Fail, _StatusDescription)
                Return False
            End If
        End If


        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "LogParameter")).ToUpper() <> "NONE" Then
            Dim lListlogParameter As New List(Of clslogParameterCfg)
            Dim clogParameterCfg As New clslogParameterCfg
            clogParameterCfg.Identifier = cMESInterface.strLogParameter
            clogParameterCfg.Value = strLogParameter
            lListlogParameter.Add(clogParameterCfg)
            If Not cMESInterface.logParameters(sSerialNumber, lListlogParameter, _StatusDescription) Then
                _Status = enumMESStatus.FailWhileMesStart
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_ValidateBOM_Fail, _StatusDescription)
                Return False
            End If
        End If

        _Status = enumMESStatus.StartPass
        Return True

    End Function



    Protected Sub _MesCompleteStampCB(ByVal Result As IAsyncResult)
        _LastMesCompleteStamp = pMesCompleteStamp.EndInvoke(Result)
        _MesCompleteStamp_RUN = False
    End Sub

    Protected Function _MesCompleteStamp(ByVal sSerialNumber As String, ByVal bResult As Boolean, ByVal strErrorMessage As String) As enumMESStatus
        Dim INISection As String = String.Empty
        Dim INIFile As String = String.Empty

        _StatusDescription = ""
        INISection = _ParentIdString + "_" + _MesControllerName + "_SETTINGS"
        INIFile = AppSettings.ConfigFolder + _MesControllerIniFile

        If Not bResult Then
            If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "LogNonConformance")).ToUpper() <> "NONE" Then
                Dim lListNcData As New List(Of clsNcDataCfg)
                Dim clsNcDataCfg As New clsNcDataCfg
                clsNcDataCfg.NcComment = strErrorMessage
                If Not cMESInterface.logNonConformance(sSerialNumber, lListNcData, _StatusDescription) Then
                    _Status = enumMESStatus.FailWhileMeComplete
                    _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_LogNonConformance_Fail, _StatusDescription)
                    Return False
                End If
            End If
        End If


        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "AssembleEmptyComp")).ToUpper() <> "NONE" Then
            If Not cMESInterface.assembleEmptyComp(sSerialNumber, _StatusDescription) Then
                _Status = enumMESStatus.FailWhileMeComplete
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_AssembleEmptyComp_Fail, _StatusDescription)
                Return False
            End If
        End If

        If Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _MesControllerIniFile, INISection, "Complete")).ToUpper() <> "NONE" Then
            If Not cMESInterface.Complete(sSerialNumber, _StatusDescription) Then
                _Status = enumMESStatus.FailWhileMeComplete
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_MES_Complete_Fail, _StatusDescription)
                Return False
            End If
        End If

        _Status = enumMESStatus.CompletePass
        Return True
    End Function
End Class





Public Class clsMESInterface
    Private _Object As New Object
    Private isRunning As Boolean = False
    Public Shared isCreateDll As Boolean = False
    Public Shared lListUrl As New List(Of String)
    Private ass As Assembly
    Private type As Type
    Private obj As Object
    Private pro As PropertyInfo
    Private method As MethodInfo
    Public strUrl As String = ""
    Public strResourceId As String = ""
    Public strOperationId As String = ""
    Public strUserName As String = ""
    Public strPassword As String = ""
    Public strNccode As String = ""
    Public strLogParameter As String = ""

    Private bDisconnect As Boolean = False
    Public webRequests As HttpWebRequest
    Private parameters As New Dictionary(Of String, Object)
    Private strIP As String = ""
    Private strPath As String = String.Empty


    Public Function Init(ByVal strPath As String) As Boolean
        Me.strPath = strPath
        Open()
        Return True
    End Function


    Protected Function CreateHttpWebRequest() As Boolean
        Try


            Net.HttpWebRequest.DefaultWebProxy = Nothing
            webRequests = Nothing
            webRequests = WebRequest.Create(strUrl)
            webRequests.ContentType = "text/xml"
            webRequests.Method = "POST"
            webRequests.KeepAlive = False
            webRequests.Proxy = Nothing
            webRequests.Credentials = New NetworkCredential(strUserName, strPassword)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    Protected Function ReadXmlResponse(ByVal response As WebResponse) As XmlDocument
        Dim doc As XmlDocument = New XmlDocument()
        Dim sr As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.UTF8)
        Dim retXml As String = sr.ReadToEnd()
        sr.Close()
        doc.LoadXml(retXml)
        Return doc
    End Function

    Public Function Open() As Boolean
        Return True
    End Function
    Public Function Close() As Boolean
        Return True
    End Function

    Public Function Start(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/start.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")

            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()


            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If

            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText

            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            Return False

        End Try
    End Function

    Public Function Complete(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            ReTry(strResult)
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/complete.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN
            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()


            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If


            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            Return False

        End Try
    End Function

    Public Function logNonConformance(ByVal strSN As String, ByVal lListNcData As List(Of clsNcDataCfg), ByRef strResult As String, Optional ByVal bRetry As Boolean = False) As Boolean
        Try
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/logNonConformance.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN

            For i = 0 To lListNcData.Count - 1

                Dim curParentNode As Object = xmlDocs.CreateNode("element", "ncDatas", "")
                Dim identifierNode As XmlNode = xmlDocs.CreateNode("element", "identifier", "")

                lListNcData(i).Identifier = strNccode
                identifierNode.InnerText = lListNcData(i).Identifier


                Dim ncComment As XmlNode = xmlDocs.CreateNode("element", "ncComment", "")
                ncComment.InnerText = lListNcData(i).NcComment

                Dim compareMode As XmlNode = xmlDocs.CreateNode("element", "compareMode", "")
                compareMode.InnerText = lListNcData(i).CompareMode

                Dim lowerLimit As XmlNode = xmlDocs.CreateNode("element", "lowerLimit", "")
                lowerLimit.InnerText = lListNcData(i).LowerLimit

                Dim upperLimit As XmlNode = xmlDocs.CreateNode("element", "upperLimit", "")
                upperLimit.InnerText = lListNcData(i).UpperLimit

                Dim value As XmlNode = xmlDocs.CreateNode("element", "value", "")
                value.InnerText = lListNcData(i).Value

                Dim referenceDesignator As XmlNode = xmlDocs.CreateNode("element", "referenceDesignator", "")
                referenceDesignator.InnerText = lListNcData(i).ReferenceDesignator

                curParentNode.AppendChild(identifierNode)
                curParentNode.AppendChild(ncComment)
                curParentNode.AppendChild(compareMode)
                curParentNode.AppendChild(lowerLimit)
                curParentNode.AppendChild(upperLimit)
                curParentNode.AppendChild(value)
                curParentNode.AppendChild(referenceDesignator)
                noVersion.ParentNode.InsertAfter(curParentNode, noVersion.ParentNode.LastChild)
            Next


            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If


            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            Return False

        End Try
    End Function

    Public Function logParameters(ByVal strSN As String, ByVal lListlogParameter As List(Of clslogParameterCfg), ByRef strResult As String) As Boolean
        Try
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/logParameters.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN

            For i = 0 To lListlogParameter.Count - 1
                Dim curParentNode As Object = xmlDocs.CreateNode("element", "logParameters", "")

                Dim compareMode As XmlNode = xmlDocs.CreateNode("element", "compareMode", "")
                compareMode.InnerText = lListlogParameter(i).CompareMode

                Dim dataType As XmlNode = xmlDocs.CreateNode("element", "dataType", "")
                dataType.InnerText = lListlogParameter(i).DataType

                Dim identifier As XmlNode = xmlDocs.CreateNode("element", "identifier", "")
                identifier.InnerText = lListlogParameter(i).Identifier

                Dim lowerLimit As XmlNode = xmlDocs.CreateNode("element", "lowerLimit", "")
                lowerLimit.InnerText = lListlogParameter(i).LowerLimit

                Dim passFailed As XmlNode = xmlDocs.CreateNode("element", "passFailed", "")
                passFailed.InnerText = lListlogParameter(i).PassFailed

                Dim value As XmlNode = xmlDocs.CreateNode("element", "value", "")
                If lListlogParameter(i).Value = "" Then lListlogParameter(i).Value = "NA"
                value.InnerText = lListlogParameter(i).Value

                Dim upperLimit As XmlNode = xmlDocs.CreateNode("element", "upperLimit", "")
                upperLimit.InnerText = lListlogParameter(i).UpperLimit

                curParentNode.AppendChild(compareMode)
                curParentNode.AppendChild(dataType)
                curParentNode.AppendChild(identifier)
                curParentNode.AppendChild(lowerLimit)
                curParentNode.AppendChild(passFailed)
                curParentNode.AppendChild(value)
                curParentNode.AppendChild(upperLimit)
                noVersion.ParentNode.InsertAfter(curParentNode, noVersion.ParentNode.LastChild)
            Next

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If


            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "logParameters:" + strResult
            Return False

        End Try
    End Function



    Public Function Assemble(ByVal strSN As String, ByVal quantity As Decimal, ByVal lListComponentData As List(Of clsComponentDataCfg), ByRef strResult As String) As Boolean
        Try

            ReTry(strResult)
            CreateHttpWebRequest()


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/assemble.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN

            For i = 0 To lListComponentData.Count - 1

                Dim curParentNode As Object = xmlDocs.CreateNode("element", "componentDatas", "")
                Dim inventory As XmlNode = xmlDocs.CreateNode("element", "inventory", "")
                inventory.InnerText = lListComponentData(i).Inventory


                Dim materialId As XmlNode = xmlDocs.CreateNode("element", "materialId", "")
                materialId.InnerText = lListComponentData(i).MaterialId

                Dim materialRevision As XmlNode = xmlDocs.CreateNode("element", "materialRevision", "")
                materialRevision.InnerText = lListComponentData(i).MaterialRevision

                Dim quantity1 As XmlNode = xmlDocs.CreateNode("element", "quantity", "")
                quantity1.InnerText = lListComponentData(i).Quantity



                curParentNode.AppendChild(inventory)
                curParentNode.AppendChild(materialId)
                curParentNode.AppendChild(materialRevision)
                curParentNode.AppendChild(quantity1)
                noVersion.ParentNode.InsertAfter(curParentNode, noVersion)
            Next
            noVersion = requestNode.SelectSingleNode("quantity")
            noVersion.InnerText = quantity.ToString

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()


            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If

            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "Assemble:" + strResult
            Return False


        End Try
    End Function

    Public Function assembleEmptyComp(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try

            ReTry(strResult)
            CreateHttpWebRequest()


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/assembleEmptyComp.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()


            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If

            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "assembleEmptyComp:" + strResult
            Return False


        End Try
    End Function

    Public Function validateBOM(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try

            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strPath + "/validateBOM.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSFC

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()


            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If

            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "validateBOM:" + strResult
            Return False

        End Try
    End Function


    Private Function CheckNetwork(ByRef strResult As String) As Boolean
        Try
            If strIP <> "" Then

                Dim p As System.Net.NetworkInformation.Ping = New System.Net.NetworkInformation.Ping()
                Dim options As System.Net.NetworkInformation.PingOptions = New System.Net.NetworkInformation.PingOptions()
                options.DontFragment = True
                Dim reply As System.Net.NetworkInformation.PingReply = p.Send(strIP, 2000)
                If reply.Status <> System.Net.NetworkInformation.IPStatus.Success Then
                    bDisconnect = True
                    strResult = "Can't access mes server"
                    Return False
                End If
            Else
            End If
            bDisconnect = False
            Return True
        Catch ex As Exception
            bDisconnect = True
            strResult = "Can't access mes server"
            Return False
        End Try
    End Function

    Private Function ReTry(ByRef strResult As String) As Boolean
        Try
            If bDisconnect Then
                '  Close()
                '  LoadDll()
                ' Open()
            End If
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False
        End Try
    End Function

End Class



Public Class clsbillOfMaterialCfg
    Private strItem As String = ""
    Public Property Item As String
        Set(ByVal value As String)
            strItem = value
        End Set
        Get
            Return strItem
        End Get
    End Property
    Sub New()

    End Sub
    Sub New(ByVal strItem As String)
        Me.strItem = strItem
    End Sub
End Class

Public Class clsNcDataCfg
    Private strIdentifier As String = ""
    Private strNcComment As String = ""
    Private strLocation As String = ""
    Private strCompareMode As String = ""
    Private strLowerLimit As String = ""
    Private strValue As String = ""
    Private strUpperLimit As String = ""
    Private strReferenceDesignator As String = ""

    Public Property ReferenceDesignator As String
        Set(ByVal value As String)
            strReferenceDesignator = value
        End Set
        Get
            Return strReferenceDesignator
        End Get
    End Property

    Public Property Identifier As String
        Set(ByVal value As String)
            strIdentifier = value
        End Set
        Get
            Return strIdentifier
        End Get
    End Property

    Public Property NcComment As String
        Set(ByVal value As String)
            strNcComment = value
        End Set
        Get
            Return strNcComment
        End Get
    End Property

    Public Property Location As String
        Set(ByVal value As String)
            strLocation = value
        End Set
        Get
            Return strLocation
        End Get
    End Property

    Public Property CompareMode As String
        Set(ByVal value As String)
            strCompareMode = value
        End Set
        Get
            Return strCompareMode
        End Get
    End Property

    Public Property LowerLimit As String
        Set(ByVal value As String)
            strLowerLimit = value
        End Set
        Get
            Return strLowerLimit
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            strValue = value
        End Set
        Get
            Return strValue
        End Get
    End Property

    Public Property UpperLimit As String
        Set(ByVal value As String)
            strUpperLimit = value
        End Set
        Get
            Return strUpperLimit
        End Get
    End Property

    Sub New()

    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String, ByVal strLocation As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
        Me.strLocation = strLocation
    End Sub

    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String, ByVal strLocation As String, ByVal strCompareMode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
        Me.strLocation = strLocation
        Me.strCompareMode = strCompareMode
        Me.strLowerLimit = strLowerLimit
        Me.strValue = strValue
        Me.strUpperLimit = strUpperLimit
    End Sub
End Class

Public Class clslogParameterCfg
    Private strIdentifier As String = ""
    Private strCompareMode As String = ""
    Private strDataType As String = ""
    Private strLowerLimit As String = ""
    Private strValue As String = ""
    Private strUpperLimit As String = ""
    Private bPassFailed As Boolean = False

    Public Property Identifier As String
        Set(ByVal value As String)
            strIdentifier = value
        End Set
        Get
            Return strIdentifier
        End Get
    End Property

    Public Property CompareMode As String
        Set(ByVal value As String)
            strCompareMode = value
        End Set
        Get
            Return strCompareMode
        End Get
    End Property

    Public Property DataType As String
        Set(ByVal value As String)
            strDataType = value
        End Set
        Get
            Return strDataType
        End Get
    End Property


    Public Property LowerLimit As String
        Set(ByVal value As String)
            strLowerLimit = value
        End Set
        Get
            Return strLowerLimit
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            strValue = value
        End Set
        Get
            Return strValue
        End Get
    End Property

    Public Property UpperLimit As String
        Set(ByVal value As String)
            strUpperLimit = value
        End Set
        Get
            Return strUpperLimit
        End Get
    End Property

    Public Property PassFailed As Boolean
        Set(ByVal value As Boolean)
            bPassFailed = value
        End Set
        Get
            Return bPassFailed
        End Get
    End Property

    Sub New()

    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strValue As String)
        Me.strIdentifier = strIdentifier
        Me.strValue = strValue
    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strValue As String, ByVal strDataType As String)
        Me.strIdentifier = strIdentifier
        Me.strValue = strValue
        Me.strDataType = strDataType
    End Sub

    Sub New(ByVal strIdentifier As String, ByVal strCompareMode As String, ByVal strDataType As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal bPassFailed As Boolean)
        Me.strIdentifier = strIdentifier
        Me.strCompareMode = strCompareMode
        Me.strDataType = strDataType
        Me.strLowerLimit = strLowerLimit
        Me.strValue = strValue
        Me.strUpperLimit = strUpperLimit
        Me.bPassFailed = bPassFailed
    End Sub
End Class


Public Class clsComponentDataCfg
    Private strInventory As String = ""
    Private strMaterialId As String = ""
    Private strmaterialRevision As String = ""
    Private dQuantity As Decimal = 0


    Public Property Inventory As String
        Set(ByVal value As String)
            strInventory = value
        End Set
        Get
            Return strInventory
        End Get
    End Property

    Public Property MaterialId As String
        Set(ByVal value As String)
            strMaterialId = value
        End Set
        Get
            Return strMaterialId
        End Get
    End Property


    Public Property MaterialRevision As String
        Set(ByVal value As String)
            strmaterialRevision = value
        End Set
        Get
            Return strmaterialRevision
        End Get
    End Property

    Public Property Quantity As Decimal
        Set(ByVal value As Decimal)
            dQuantity = value
        End Set
        Get
            Return dQuantity
        End Get
    End Property


    Sub New()

    End Sub

    Sub New(ByVal strInventory As String, ByVal strMaterialId As String, ByVal strmaterialRevision As String, ByVal dQuantity As Decimal)
        Me.strInventory = strInventory
        Me.strMaterialId = strMaterialId
        Me.strmaterialRevision = strmaterialRevision
        Me.dQuantity = dQuantity
    End Sub

End Class


Public Class clsResourceStateCfg
    Private strEntryReasonId As String = ""
    Private strEntryReasonText As String = ""
    Private cEntryTimeStamp As Date = Now
    Private cLeaveTimeStamp As Date = Now


    Public Property EntryReasonId As String
        Set(ByVal value As String)
            strEntryReasonId = value
        End Set
        Get
            Return strEntryReasonId
        End Get
    End Property

    Public Property EntryReasonText As String
        Set(ByVal value As String)
            strEntryReasonText = value
        End Set
        Get
            Return strEntryReasonText
        End Get
    End Property

    Public Property EntryTimeStamp As Date
        Set(ByVal value As Date)
            cEntryTimeStamp = value
        End Set
        Get
            Return cEntryTimeStamp
        End Get
    End Property

    Public Property LeaveTimeStamp As Date
        Set(ByVal value As Date)
            cLeaveTimeStamp = value
        End Set
        Get
            Return cLeaveTimeStamp
        End Get
    End Property
    Sub New()

    End Sub

    Sub New(ByVal strEntryReasonId As String, ByVal strEntryReasonText As String, ByVal cEntryTimeStamp As Date)
        Me.strEntryReasonId = strEntryReasonId
        Me.strEntryReasonText = strEntryReasonText
        Me.cEntryTimeStamp = cEntryTimeStamp
    End Sub
    Sub New(ByVal strEntryReasonId As String, ByVal strEntryReasonText As String, ByVal cEntryTimeStamp As Date, ByVal cLeaveTimeStamp As Date)
        Me.strEntryReasonId = strEntryReasonId
        Me.strEntryReasonText = strEntryReasonText
        Me.cEntryTimeStamp = cEntryTimeStamp
        Me.cLeaveTimeStamp = cLeaveTimeStamp
    End Sub
End Class