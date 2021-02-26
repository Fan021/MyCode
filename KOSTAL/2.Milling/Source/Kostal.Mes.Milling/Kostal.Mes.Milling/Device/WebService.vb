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

Public Class WebServiceCfg
    Public Url As String = String.Empty
    Public Url2 As String = String.Empty
    Public UserName As String = String.Empty
    Public PassWord As String = String.Empty
    Public ResourceId As String = String.Empty
    Public OperationId As String = String.Empty
    Public Timeout As Integer = 0
    Public PassiveMode As Boolean
    Public Enable As Boolean
End Class
Public Class ScapLocation
    Public Location As String
    Public Result As String
    Sub New(ByVal strLocation As String, ByVal strResult As String)
        Location = strLocation
        Result = strResult
    End Sub
End Class

Public Class WebService
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
    Public Const Name As String = "WebService"
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
            stream = nWeb.OpenRead(_Setting.WebServiceCfg.Url)
            Dim fileread As New StreamReader(stream)
            Dim s As String = fileread.ReadToEnd()
            fileread.Close()

            Dim writer As New StreamWriter(_Setting.LibFolder + "MillingIntegrationBean4.wsdl")
            writer.Write(s)
            writer.Close()

            writer = New StreamWriter(_Setting.LibFolder + "GenerateSource.bat", False)
            writer.WriteLine(_Setting.ApplicationFolder.Split(CChar("\"))(0))
            writer.WriteLine("cd\")
            writer.WriteLine("cd " & _Setting.LibFolder)
            writer.WriteLine("svcutil MillingIntegrationBean4.wsdl /language:vb")
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
            nParameter.OutputAssembly = _Setting.LibFolder & "MillingIntegrationBean4.dll"

            nParameter.ReferencedAssemblies.Add("System.dll")
            nParameter.ReferencedAssemblies.Add("System.XML.dll")
            nParameter.ReferencedAssemblies.Add("System.ServiceModel.dll")
            nParameter.ReferencedAssemblies.Add("System.Web.Services.dll")
            nParameter.ReferencedAssemblies.Add("System.Data.dll")
            nParameter.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
            nParameter.ReferencedAssemblies.Add("System.Runtime.Serialization.dll")


            result = provider.CompileAssemblyFromFile(nParameter, New String() {_Setting.LibFolder & "MillingIntegrationR1_5.vb"})
            If IsNothing(result) Then
                _StatusDescription = "Init Fail"
                Return False
            End If
            If result.Errors.Count > 0 Then
                _StatusDescription = result.Errors(0).ErrorText
                Return False
            End If
        Catch ex As Exception
            _StatusDescription = "WebService Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function


    Public Function LoadDll() As Boolean
        Try
            ass = Assembly.LoadFile(_Setting.LibFolder & "MillingIntegrationBean4.dll")
            type = ass.GetType("MillingIntegrationServiceR1_5Client")
            obj = ass.CreateInstance("MillingIntegrationServiceR1_5Client")
            Dim ClientCredentials As New System.ServiceModel.Description.ClientCredentials
            pro = type.GetProperty("ClientCredentials")
            ClientCredentials = CType(pro.GetValue(obj, Nothing), System.ServiceModel.Description.ClientCredentials)
            ClientCredentials.UserName.UserName = _Setting.WebServiceCfg.UserName
            ClientCredentials.UserName.Password = _Setting.WebServiceCfg.PassWord
            ' pro.SetValue(obj, ClientCredentials, Nothing)
            method = type.GetMethod("Open")
            method.Invoke(obj, Nothing)
        Catch ex As Exception
            _StatusDescription = "WebService Init Fail. Message:" + ex.Message
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
            _StatusDescription = "WebService Close Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function ValidateMilling(ByVal strSN As String, ByVal amountOfBoards As Integer, ByRef strResult As String, ByRef ListScap As Dictionary(Of String, ScapLocation)) As Boolean
        Try
            ListScap.Clear()
            Dim typestartReqECO As Type = ass.GetType("validateMillingREQ")
            Dim objstartReqECO As Object = ass.CreateInstance("validateMillingREQ")
            Dim strLocation As String
            Dim strLocationResult As String
            pro = typestartReqECO.GetProperty("resourceId")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.ResourceId, Nothing)

            pro = typestartReqECO.GetProperty("sfc")
            pro.SetValue(objstartReqECO, strSN, Nothing)

            pro = typestartReqECO.GetProperty("operation")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.OperationId, Nothing)

            pro = typestartReqECO.GetProperty("amountOfBoards")
            pro.SetValue(objstartReqECO, amountOfBoards.ToString, Nothing)

            Dim typestart As Type = ass.GetType("validateMilling")
            Dim objstart As Object = ass.CreateInstance("validateMilling")
            pro = typestart.GetProperty("validateMillingRequest")
            pro.SetValue(objstart, objstartReqECO, Nothing)

            Dim typestartResponse As Type = ass.GetType("validateMillingResponse")
            Dim objstartResponse As Object = ass.CreateInstance("validateMillingResponse")
            method = type.GetMethod("validateMilling")
            objstartResponse = method.Invoke(obj, New Object() {objstart})

            Dim typesstartResInfo As Type = ass.GetType("validateMillingRSP")
            Dim objstartResInfo As Object = ass.CreateInstance("validateMillingRSP")
            pro = typestartResponse.GetProperty("return")
            objstartResInfo = pro.GetValue(objstartResponse, Nothing)

            pro = typesstartResInfo.GetProperty("resultCode")
            Dim sResultCode As Integer = pro.GetValue(objstartResInfo, Nothing)

            pro = typesstartResInfo.GetProperty("resultText")
            strResult = pro.GetValue(objstartResInfo, Nothing)

            pro = typesstartResInfo.GetProperty("singleBoards")
            Dim sResultsingleBoards() As Object = pro.GetValue(objstartResInfo, Nothing)
            If Not IsNothing(sResultsingleBoards) Then
                For Each objct As Object In sResultsingleBoards
                    Dim objcttype As Type = objct.GetType
                    pro = objcttype.GetProperty("state")
                    strLocationResult = pro.GetValue(objct, Nothing).ToString.ToUpper
                    pro = objcttype.GetProperty("location")
                    strLocation = pro.GetValue(objct, Nothing)
                    ' If strLocationResult <> "GOOD" Then
                    ListScap.Add(strLocation, New ScapLocation(strLocation, strLocationResult))
                    ' End If
                Next
            End If
            If sResultCode = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strResult = "WebService ValidateMilling Fail. Message:" + ex.Message
            _StatusDescription = "WebService ValidateMilling Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function startBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            Dim typestartReqECO As Type = ass.GetType("startBoardREQ")
            Dim objstartReqECO As Object = ass.CreateInstance("startBoardREQ")
            pro = typestartReqECO.GetProperty("resourceId")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.ResourceId, Nothing)

            pro = typestartReqECO.GetProperty("sfc")
            pro.SetValue(objstartReqECO, strSN, Nothing)

            pro = typestartReqECO.GetProperty("operation")
            pro.SetValue(objstartReqECO, _Setting.WebServiceCfg.OperationId, Nothing)

            Dim typestart As Type = ass.GetType("startBoard")
            Dim objstart As Object = ass.CreateInstance("startBoard")
            pro = typestart.GetProperty("startBoardRequest")
            pro.SetValue(objstart, objstartReqECO, Nothing)

            Dim typestartResponse As Type = ass.GetType("startBoardResponse")
            Dim objstartResponse As Object = ass.CreateInstance("startBoardResponse")
            method = type.GetMethod("startBoard")
            objstartResponse = method.Invoke(obj, New Object() {objstart})

            Dim typesstartResInfo As Type = ass.GetType("genericRSP")
            Dim objstartResInfo As Object = ass.CreateInstance("genericRSP")
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
            strResult = "WebService startBoard Fail. Message:" + ex.Message
            _StatusDescription = "WebService startBoard Fail. Message:" + ex.Message
            '  Throw New Exception(_StatusDescription)
            Return True

        End Try
    End Function

    Public Function completeBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
        Try
            Dim typeoperSFCREQ As Type = ass.GetType("operSFCREQ")
            Dim objoperSFCREQ As Object = ass.CreateInstance("operSFCREQ")
            pro = typeoperSFCREQ.GetProperty("resourceId")
            pro.SetValue(objoperSFCREQ, _Setting.WebServiceCfg.ResourceId, Nothing)

            pro = typeoperSFCREQ.GetProperty("sfc")
            pro.SetValue(objoperSFCREQ, strSN, Nothing)

            pro = typeoperSFCREQ.GetProperty("operation")
            pro.SetValue(objoperSFCREQ, _Setting.WebServiceCfg.OperationId, Nothing)

            Dim typecompleteBoard As Type = ass.GetType("completeBoard")
            Dim objcompleteBoard As Object = ass.CreateInstance("completeBoard")
            pro = typecompleteBoard.GetProperty("CompleteBoardRequest")
            pro.SetValue(objcompleteBoard, objoperSFCREQ, Nothing)

            Dim typecompleteBoardResponse As Type = ass.GetType("completeBoardResponse")
            Dim objcompleteBoardResponse As Object = ass.CreateInstance("completeBoardResponse")
            method = type.GetMethod("completeBoard")
            objcompleteBoardResponse = method.Invoke(obj, New Object() {objcompleteBoard})

            Dim typesstartResInfo As Type = ass.GetType("genericRSP")
            Dim objstartResInfo As Object = ass.CreateInstance("genericRSP")
            pro = typecompleteBoardResponse.GetProperty("return")
            objstartResInfo = pro.GetValue(objcompleteBoardResponse, Nothing)

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
            strResult = "WebService completeBoard Fail. Message:" + ex.Message
            _StatusDescription = "WebService completeBoard Fail. Message:" + ex.Message
            Return False
        End Try
    End Function
End Class
