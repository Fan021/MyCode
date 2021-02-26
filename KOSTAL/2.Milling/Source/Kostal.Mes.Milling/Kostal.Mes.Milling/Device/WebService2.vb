Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Web.Services
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Public Class WebService2
    Private web As New WebClient
    Private stream As Stream
    Private description As ServiceDescription
    Private importer As New ServiceDescriptionImporter()
    Private nmspace As New CodeNamespace
    Private unit As New CodeCompileUnit
    Private warning As ServiceDescriptionImportWarnings
    Private provide As CodeDomProvider
    Private parameter As New CompilerParameters
    Private result As CompilerResults
    Private _StatusDescription As String
    Private _Setting As Settings
    Private ass As Assembly
    Private type As Type
    Private obj As Object
    Private pro As PropertyInfo
    Private method As MethodInfo
    Private _isRun As Boolean
    Public Const Name As String = "WebService2"
    Public Property isRun As Boolean
        Set(ByVal value As Boolean)
            _isRun = value
        End Set
        Get
            Return _isRun
        End Get

    End Property
    Public ReadOnly Property StatusDescription As String
        Get
            Return _StatusDescription
        End Get
    End Property


    Public Function Init(ByVal Setting As Settings) As Boolean
        _Setting = Setting
        Return True
    End Function


    Public Function CreateDll() As Boolean
        Try

            Dim nWeb As WebClient = New WebClient()
            Dim stream As Stream
            Dim nParameter As CompilerParameters = New CompilerParameters()
            Dim provider As New VBCodeProvider
            Dim unit As New CodeCompileUnit
            Dim nmspace As New CodeNamespace
            Dim result As CompilerResults
            'save to a *.wsdl file
            stream = nWeb.OpenRead(_Setting.WebServiceCfg.Url2)
            Dim fileread As New StreamReader(stream)
            Dim s As String = fileread.ReadToEnd()
            fileread.Close()

            Dim writer As New StreamWriter(_Setting.LibFolder + "AutomationIntegrationBean5.wsdl")
            writer.Write(s)
            writer.Close()

            writer = New StreamWriter(_Setting.LibFolder + "GenerateSource.bat", False)
            writer.WriteLine(_Setting.ApplicationFolder.Split(CChar("\"))(0))
            writer.WriteLine("cd\")
            writer.WriteLine("cd " & _Setting.LibFolder)
            writer.WriteLine("svcutil AutomationIntegrationBean5.wsdl /language:vb")
            writer.Close()

            Dim tarBat As String = _Setting.LibFolder & "GenerateSource.bat"


            If System.IO.File.Exists(tarBat) Then

                Dim pHandle As Process = Process.Start(tarBat)

                Do Until pHandle.HasExited                      'run bat file until finished
                    System.Threading.Thread.Sleep(1)
                Loop
            End If

            nParameter.GenerateExecutable = False
            nParameter.GenerateInMemory = True
            nParameter.TempFiles.KeepFiles = False
            nParameter.OutputAssembly = _Setting.LibFolder & "AutomationIntegrationBean5.dll"

            nParameter.ReferencedAssemblies.Add("System.dll")
            nParameter.ReferencedAssemblies.Add("System.XML.dll")
            nParameter.ReferencedAssemblies.Add("System.ServiceModel.dll")
            nParameter.ReferencedAssemblies.Add("System.Web.Services.dll")
            nParameter.ReferencedAssemblies.Add("System.Data.dll")
            nParameter.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
            nParameter.ReferencedAssemblies.Add("System.Runtime.Serialization.dll")


            result = provider.CompileAssemblyFromFile(nParameter, New String() {_Setting.LibFolder & "AutomationIntegrationServiceR4_5.vb"})
            If IsNothing(result) Then
                _StatusDescription = "Init Fail"
                Return False
            End If
            If result.Errors.Count > 0 Then
                _StatusDescription = result.Errors(0).ErrorText
                Return False
            End If
        Catch ex As Exception
            _StatusDescription = "WebService2 Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function


    Public Function LoadDll() As Boolean
        Try
            ass = Assembly.LoadFile(_Setting.LibFolder & "AutomationIntegrationBean5.dll")
            type = ass.GetType("AutomationIntegrationServiceR4_5Client")
            obj = ass.CreateInstance("AutomationIntegrationServiceR4_5Client")
            Dim ClientCredentials As New System.ServiceModel.Description.ClientCredentials
            pro = type.GetProperty("ClientCredentials")
            ClientCredentials = CType(pro.GetValue(obj, Nothing), System.ServiceModel.Description.ClientCredentials)
            ClientCredentials.UserName.UserName = _Setting.WebServiceCfg.UserName
            ClientCredentials.UserName.Password = _Setting.WebServiceCfg.PassWord
            ' pro.SetValue(obj, ClientCredentials, Nothing)
            method = type.GetMethod("Open")
            method.Invoke(obj, Nothing)
        Catch ex As Exception
            _StatusDescription = "WebService2 Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function
    Public Function Close() As Boolean
        Try
            method = type.GetMethod("Close")
            method.Invoke(obj, Nothing)
        Catch ex As Exception
            _StatusDescription = "WebService2 Close Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function Start(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            Dim typestartReqECO As Type = ass.GetType("startREQ")
            Dim objstartReqECO As Object = ass.CreateInstance("startREQ")
            pro = typestartReqECO.GetProperty("resourceId")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.ResourceId, Nothing)

            pro = typestartReqECO.GetProperty("sfc")
            pro.SetValue(objstartReqECO, strSN, Nothing)

            pro = typestartReqECO.GetProperty("operation")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.OperationId, Nothing)


            Dim typestart As Type = ass.GetType("start")
            Dim objstart As Object = ass.CreateInstance("start")
            pro = typestart.GetProperty("startRequest")
            pro.SetValue(objstart, objstartReqECO, Nothing)

            Dim typestartResponse As Type = ass.GetType("startResponse")
            Dim objstartResponse As Object = ass.CreateInstance("startResponse")
            method = type.GetMethod("start")
            objstartResponse = method.Invoke(obj, New Object() {objstart})

            Dim typesstartResInfo As Type = ass.GetType("startRSP")
            Dim objstartResInfo As Object = ass.CreateInstance("startRSP")
            pro = typestartResponse.GetProperty("return")
            objstartResInfo = pro.GetValue(objstartResponse, Nothing)

            pro = typesstartResInfo.GetProperty("resultCode")
            Dim sResultCode As Integer = pro.GetValue(objstartResInfo, Nothing)

            pro = typesstartResInfo.GetProperty("resultText")
            strResult = pro.GetValue(objstartResInfo, Nothing)

            If sResultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strResult = "WebService2 Start Fail. Message:" + ex.Message
            _StatusDescription = "WebService2 Start Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function signOff(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            Dim typestartReqECO As Type = ass.GetType("operSFCREQ")
            Dim objstartReqECO As Object = ass.CreateInstance("operSFCREQ")
            pro = typestartReqECO.GetProperty("resourceId")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.ResourceId, Nothing)
            pro = typestartReqECO.GetProperty("sfc")
            pro.SetValue(objstartReqECO, strSN, Nothing)

            pro = typestartReqECO.GetProperty("operation")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.OperationId, Nothing)

            Dim typestart As Type = ass.GetType("signOff")
            Dim objstart As Object = ass.CreateInstance("signOff")
            pro = typestart.GetProperty("signOffRequest")
            pro.SetValue(objstart, objstartReqECO, Nothing)

            Dim typestartResponse As Type = ass.GetType("signOffResponse")
            Dim objstartResponse As Object = ass.CreateInstance("signOffResponse")
            method = type.GetMethod("signOff")
            objstartResponse = method.Invoke(obj, New Object() {objstart})

            Dim typesstartResInfo As Type = ass.GetType("sfcMaterialRSP")
            Dim objstartResInfo As Object = ass.CreateInstance("sfcMaterialRSP")
            pro = typestartResponse.GetProperty("return")
            objstartResInfo = pro.GetValue(objstartResponse, Nothing)

            pro = typesstartResInfo.GetProperty("resultCode")
            Dim sResultCode As Integer = pro.GetValue(objstartResInfo, Nothing)

            pro = typesstartResInfo.GetProperty("resultText")
            strResult = pro.GetValue(objstartResInfo, Nothing)

            If sResultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strResult = "WebService2 signOff Fail. Message:" + ex.Message
            _StatusDescription = "WebService2 signOff Fail. Message:" + ex.Message
            '  Throw New Exception(_StatusDescription)
            Return True

        End Try
    End Function

End Class
