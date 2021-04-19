Imports System.Windows.Forms
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
Imports System.Xml
Imports System.Text

Public Class clsMES
    Private _Object As New Object
    Public Shared isCreateDll As Boolean = False
    Public Shared lListUrl As New List(Of String)
    Private ass As Assembly
    Private type As Type
    Private obj As Object
    Private pro As PropertyInfo
    Private method As MethodInfo
    Private strUrl As String = ""
    Public cUrl() As String
    Public strResourceId As String = ""
    Public strOperationId As String = ""
    Public strNCCode As String = ""
    Public strUserName As String = ""
    Public strPassword As String = ""
    Private strXmlPath As String = ""
    Private bDisconnect As Boolean = False
    Public webRequests As HttpWebRequest
    Private parameters As New Dictionary(Of String, Object)
    Private strIP As String = ""
    Private bEnable As Boolean = False
    Public Function Init(ByVal bEnable As Boolean, ByVal strXmlPath As String, ByVal strUrl As String, ByVal strResourceId As String, ByVal strOperationId As String, ByVal strNCCode As String, ByVal strUserName As String, ByVal strPassword As String) As Boolean
        Me.strUrl = strUrl
        Me.cUrl = strUrl.Split("/")
        Me.strResourceId = strResourceId
        Me.strOperationId = strOperationId
        Me.strUserName = strUserName
        Me.strPassword = strPassword
        Me.strXmlPath = strXmlPath
        Me.strNCCode = strNCCode
        Me.bEnable = bEnable
        Open()
        GetIP()
        Return True
    End Function
    Private Function GetIP() As Boolean
        Try
            Dim cUrl1() As String = strUrl.Split("/")
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
            Throw New Exception("Can't access MES Server:" + strUrl)
            Return False
        End Try
    End Function
    Public Function Quit() As Boolean
        Try
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
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
        Try
            CreateHttpWebRequest()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Function Close() As Boolean
        Try
            'method = type.GetMethod("Close")
            'method.Invoke(obj, Nothing)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Start(ByVal strSN As String, ByRef strResult As String, Optional ByVal strRecipe As String = "") As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/start.xml")
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
            Return False

        End Try
    End Function

    Public Function assembleEmptyComp(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/assembleEmptyComp.xml")
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
            Return False

        End Try
    End Function


    Public Function Complete(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/complete.xml")
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
            Return False

        End Try
    End Function

    Public Function logNonConformance(ByVal strSN As String, ByVal lListNcData As List(Of clsNcDataCfg), ByRef strResult As String, Optional ByVal bRetry As Boolean = False) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            If IsNothing(lListNcData) Then
                Return False
            End If
            If lListNcData.Count < 1 Then
                Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/logNonConformance.xml")
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
                lListNcData(i).Identifier = strNCCode
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
            Return False

        End Try
    End Function

    Public Function logParameters(ByVal strSN As String, ByVal lListlogParameter As List(Of clslogParameterCfg), ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            If IsNothing(lListlogParameter) Then
                Return False
            End If
            If lListlogParameter.Count < 1 Then
                Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/logParameters.xml")
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
            strResult = "logParameters:" + strResult
            Return False

        End Try
    End Function

  
    Public Function Assemble(ByVal strSN As String, ByVal quantity As Decimal, ByVal lListComponentData As List(Of clsComponentDataCfg), ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()
            If IsNothing(lListComponentData) Then
                Return False
            End If
            If lListComponentData.Count < 1 Then
                 Return False
            End If


            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/assemble.xml")
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
            strResult = "Assemble:" + strResult
            Return False


        End Try
    End Function

 

    Public Function getSfcStatus(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/getSfcStatus.xml")
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
            strResult = "getSfcStatus:" + strResult
            Return False

        End Try
    End Function

    

    Public Function validateBOM(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/validateBOM.xml")
            Dim mgr As XmlNamespaceManager = New XmlNamespaceManager(xmlDocs.NameTable)
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim requestNode As Object = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr)
            Dim noVersion As XmlNode = requestNode.SelectSingleNode("resourceId")
            noVersion.InnerText = strResourceId
            noVersion = requestNode.SelectSingleNode("operation")
            noVersion.InnerText = strOperationId
            noVersion = requestNode.SelectSingleNode("sfc")
            noVersion.InnerText = strSFC

            ' For i = 0 To lListbillOfMaterialCfg.Count - 1
            '  Dim curParentNode As Object = xmlDocs.CreateNode("element", "bom", "")
            '  Dim item As XmlNode = xmlDocs.CreateNode("element", "item", "")
            '  item.InnerText = lListbillOfMaterialCfg(i).Item
            ''   curParentNode.AppendChild(item)
            '   noVersion.ParentNode.InsertAfter(curParentNode, noVersion)
            ' Next


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
            strResult = "validateBOM:" + strResult
            Return False

        End Try
    End Function

    Public Function validateSfc(ByVal strMainSFC As String, ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try

            If Not bEnable Then
                Return True
            End If
            CreateHttpWebRequest()

            Dim xmlDocs As XmlDocument = New XmlDocument()
            xmlDocs.Load(strXmlPath + "/validateSfc.xml")
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
            strResult = "validateSfc:" + strResult
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
