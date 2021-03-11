Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.ServiceModel
Imports System.ServiceModel.Configuration
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Web.Services
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Kochi.HMI.MainControl.LocalDevice
Imports System.Xml
Imports System.Text

<clsHMIDeviceNameAttribute("MES", "MES")>
Public Class clsMES
    Inherits clsHMIMES
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private isRunning As Boolean = False
    Public Shared isCreateDll As Boolean = False
    Public Shared lListUrl As New List(Of String)
    Private ass As Assembly
    Private type As Type
    Private obj As Object
    Private pro As PropertyInfo
    Private method As MethodInfo
    Private strUrl As String = ""
    Private cUrl() As String
    Private strResourceId As String = ""
    Private strOperationId As String = ""
    Private bDisconnect As Boolean = False
    Protected cLogHandler As clsLogHandler
    Public webRequests As HttpWebRequest
    Private parameters As New Dictionary(Of String, Object)
    Public Overrides ReadOnly Property NotInqueue As String
        Get
            If IsNothing(lListInitParameter) Then
                Return ""
            End If
            If lListInitParameter.Count < 6 Then
                Return ""
            End If
            If IsNothing(lListInitParameter(5)) Then
                Return ""
            End If
            Return lListInitParameter(5)
        End Get
    End Property

    Public ReadOnly Property NotInqueue2 As String
        Get
            If IsNothing(lListInitParameter) Then
                Return ""
            End If
            If lListInitParameter.Count < 7 Then
                Return ""
            End If
            If IsNothing(lListInitParameter(6)) Then
                Return ""
            End If
            Return lListInitParameter(6)
        End Get
    End Property
    Private strIP As String = ""
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        strUrl = lListInitParameter(2).Substring(0, lListInitParameter(2).LastIndexOf("?"))
        cUrl = strUrl.Split("/")
        strResourceId = lListInitParameter(0)
        strOperationId = lListInitParameter(1)
        Open()
        GetIP()
        Return True
    End Function
    Private Function GetIP() As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            Dim cUrl1() As String = lListInitParameter(2).Split("/")
            If cUrl1.Length > 3 Then
                Dim mTempHost As String = cUrl1(2)
                If mTempHost.IndexOf(":") > 0 Then
                    mTempHost = mTempHost.Substring(0, mTempHost.IndexOf(":"))
                Else
                    mTempHost = mTempHost
                End If
                Dim ip() As IPAddress = Dns.GetHostAddresses(mTempHost)
                For i = 0 To ip.Length - 1
                    If ip(i).ToString.IndexOf(".") >= 0 Then
                        strIP = ip(i).ToString
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New clsHMIException("Can't access MES Server:" + lListInitParameter(2), enumExceptionType.Alarm)
            Return False
        End Try
    End Function
    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iControlUI) Then
                iControlUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If
            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function


    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then
            RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iInitUI.Quit(cLocalElement, cSystemElement)
        End If
        iInitUI = New InitUI
        AddHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        Return True
    End Function


    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
        Return True
    End Function
    Public Overrides Property Running As Boolean
        Get
            Return isRunning
        End Get
        Set(ByVal value As Boolean)
            isRunning = value
        End Set
    End Property

    Protected Function CreateHttpWebRequest() As Boolean
        Try
            Net.HttpWebRequest.DefaultWebProxy = Nothing
            webRequests = Nothing
            webRequests = WebRequest.Create(lListInitParameter(2))
            webRequests.ContentType = "text/xml"
            webRequests.Method = "POST"
            webRequests.KeepAlive = False
            webRequests.Proxy = Nothing
            webRequests.Credentials = New NetworkCredential(lListInitParameter(3), lListInitParameter(4))
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
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
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            CreateHttpWebRequest()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function
    Public Function Close() As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            'method = type.GetMethod("Close")
            'method.Invoke(obj, Nothing)
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Overrides Function Start(ByVal strSN As String, ByRef strResult As String, Optional ByVal strRecipe As String = "") As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/start.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")

            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSN
            noVersion = requestNode.SelectSingleNode("recipe")
            noVersion.InnerText = strRecipe

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)
 
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

    Public Overrides Function Complete(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/complete.xml")
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

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)


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

    Public Overrides Function logNonConformance(ByVal strSN As String, ByVal lListNcData As List(Of clsNcDataCfg), ByRef strResult As String, Optional ByVal bRetry As Boolean = False) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            If IsNothing(lListNcData) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "9"), enumExceptionType.Alarm)
                Return False
            End If
            If lListNcData.Count < 1 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "9"), enumExceptionType.Alarm)
                Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/logNonConformance.xml")
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
                If bRetry Then
                    If lListInitParameter(8) = "" Then
                        lListNcData(i).Identifier = strOperationId + "_RETRY"
                    Else
                        lListNcData(i).Identifier = lListInitParameter(8)
                    End If
                Else
                    If lListInitParameter(7) = "" Then
                        lListNcData(i).Identifier = strOperationId + "_FAIL"
                    Else
                        lListNcData(i).Identifier = lListInitParameter(7)
                    End If
                End If
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
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)


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

    Public Overrides Function logParameters(ByVal strSN As String, ByVal lListlogParameter As List(Of clslogParameterCfg), ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            If IsNothing(lListlogParameter) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "10"), enumExceptionType.Alarm)
                Return False
            End If
            If lListlogParameter.Count < 1 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "10"), enumExceptionType.Alarm)
                Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/logParameters.xml")
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
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)


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

    Public Overrides Function GetHUDetails(ByVal strHU1 As String, ByVal strHU2 As String, ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "GetHUDetails:" + strResult
            Return False

        End Try
    End Function

    Public Overrides Function Assemble(ByVal strSN As String, ByVal quantity As Decimal, ByVal lListComponentData As List(Of clsComponentDataCfg), ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            If IsNothing(lListComponentData) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "11"), enumExceptionType.Alarm)
                Return False
            End If
            If lListComponentData.Count < 1 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "11"), enumExceptionType.Alarm)
                Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/assemble.xml")
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

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)

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

    Public Overrides Function LogResourceState(ByVal lListResourceState As List(Of clsResourceStateCfg), ByRef strResult As String) As Boolean
        Try


            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()
            If IsNothing(lListResourceState) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "11"), enumExceptionType.Alarm)
                Return False
            End If
            If lListResourceState.Count < 1 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "11"), enumExceptionType.Alarm)
                Return False
            End If

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/LogResourceState.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As Object = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId

            For i = 0 To lListResourceState.Count - 1

                Dim curParentNode As Object = xmlDocs.CreateNode("element", "resourceStates", "")

                Dim entryReasonId As XmlNode = xmlDocs.CreateNode("element", "entryReasonId", "")
                entryReasonId.InnerText = lListResourceState(i).EntryReasonId

                Dim entryReasonText As XmlNode = xmlDocs.CreateNode("element", "entryReasonText", "")
                entryReasonText.InnerText = lListResourceState(i).EntryReasonText

                Dim entryTimeStamp As XmlNode = xmlDocs.CreateNode("element", "entryTimeStamp", "")
                entryTimeStamp.InnerText = lListResourceState(i).EntryTimeStamp

                Dim leaveTimeStamp As XmlNode = xmlDocs.CreateNode("element", "leaveTimeStamp", "")
                leaveTimeStamp.InnerText = lListResourceState(i).LeaveTimeStamp

                curParentNode.AppendChild(entryReasonId)
                curParentNode.AppendChild(entryReasonText)
                curParentNode.AppendChild(entryTimeStamp)
                curParentNode.AppendChild(leaveTimeStamp)
                noVersion.ParentNode.InsertAfter(curParentNode, noVersion.ParentNode.LastChild)
            Next

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)


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
            strResult = "LogResourceState:" + strResult
            Return False

        End Try
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


    Public Overrides Function checkRecipe(ByVal strRecipe As String, ByVal strRecipeVersion As String, ByRef strResult As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "checkRecipe:" + strResult
            Return False

        End Try
    End Function

    Public Overrides Function getSfcStatus(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try

            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/getSfcStatus.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
             Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)


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
            strResult = "getSfcStatus:" + strResult
            Return False

        End Try
    End Function

    Public Overrides Function changeResourceState(ByVal strNewState As String, ByRef strResult As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "changeResourceState:" + strResult
            Return False

        End Try
    End Function

    Public Overrides Function validateBOM(ByVal strSFC As String, ByVal lListbillOfMaterialCfg As List(Of clsbillOfMaterialCfg), ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/validateBOM.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSFC

            For i = 0 To lListbillOfMaterialCfg.Count - 1
                Dim curParentNode As Object = xmlDocs.CreateNode("element", "bom", "")
                Dim item As XmlNode = xmlDocs.CreateNode("element", "item", "")
                item.InnerText = lListbillOfMaterialCfg(i).Item
                curParentNode.AppendChild(item)
                noVersion.ParentNode.InsertAfter(curParentNode, noVersion)
            Next


            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)

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

    Public Overrides Function validateSfc(ByVal strMainSFC As String, ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try

            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/validateSfc.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("barcodeParentSfc")
            noVersion.InnerText = strMainSFC
            noVersion = requestNode.SelectSingleNode("barcode")
            noVersion.InnerText = strSFC

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)

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
            strResult = "validateSfc:" + strResult
            Return False

        End Try
    End Function


    Public Overrides Function SetupComponent(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "SetupComponent:" + strResult
            Return False

        End Try
    End Function

    Private Function CheckNetwork(ByRef strResult As String) As Boolean
        Try
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
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
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
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

    Public Overrides Function getSFCOperation(ByVal strSFC As String, ByRef strResult As String) As Integer
        Try

            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return True
            End If
            ReTry(strResult)
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(cSystemManager.Settings.ApplicationFolder + "/SampleXML/getSFCOperation.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSFC

            Dim payload As String = xmlDocs.InnerXml.ToString()
            Dim byteArray() As Byte = Encoding.UTF8.GetBytes(payload)
            webRequests.ContentLength = byteArray.Length
            Dim requestStream As Stream = webRequests.GetRequestStream()
            requestStream.Write(byteArray, 0, byteArray.Length)
            requestStream.Close()

            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Send reqeust to MES service: " + xmlDocs.InnerXml)

            xmlDocs = ReadXmlResponse(webRequests.GetResponse())
            If IsNothing(xmlDocs) Then
                strResult = "MES Return Fail"
                Return False
            End If
            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "Receive From MES service: " + xmlDocs.InnerXml)

            mgr = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim repon As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim resultCode As Object = repon.SelectSingleNode("resultCode").InnerText
            strResult = repon.SelectSingleNode("resultText").InnerText
            If resultCode = 0 Then
                Dim stroperation As String = repon.SelectSingleNode("operation").InnerText

                If stroperation = strOperationId Then
                    Return -1 'Inqueue
                Else
                    If NotInqueue2 <> "" Then
                        Dim cValue() As String = NotInqueue2.Split(",")
                        For i = 0 To cValue.Length - 1
                            If stroperation = cValue(i) Then
                                Return -4 'Other Station Inqueue2
                            End If
                        Next
                    End If

                    If NotInqueue = "" Then
                        Return -2 'Not Inqueue
                    Else
                        Dim cValue() As String = NotInqueue.Split(",")
                        For i = 0 To cValue.Length - 1
                            If stroperation = cValue(i) Then
                                Return -3 'Other Station Inqueue
                            End If
                        Next
                        Return -2 'Not Inqueue
                    End If

                End If
            Else
                Return -1
            End If

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            CheckNetwork(strResult)
            strResult = "getSFCOperation:" + strResult
            Return -5

        End Try
    End Function

    Private Sub SetValue(ByRef pro As PropertyInfo, ByVal obj As Object, ByVal value As Object, ByVal index() As Object)
        pro.SetValue(obj, value, index)
        Dim strMessage As String = "MES|SetValue|" + value.ToString
        cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, strMessage)
    End Sub

    Private Function GetValue(ByRef pro As PropertyInfo, ByVal obj As Object, ByVal index() As Object) As Object
        Dim value As Object = pro.GetValue(obj, index)
        Dim strMessage As String = "MES|GetValue|" + value.ToString
        cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, strMessage)
        Return value
    End Function

    Private Function GetProperty(ByRef typeReq As Type, ByVal name As String) As Object
        Dim strMessage As String = "MES|Name|" + name.ToString
        cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, strMessage)
        Return typeReq.GetProperty(name)
    End Function

    Public Overrides ReadOnly Property Enable As Boolean
        Get
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "FALSE" Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property
End Class

