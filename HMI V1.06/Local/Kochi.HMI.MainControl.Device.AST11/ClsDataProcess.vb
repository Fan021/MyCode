Imports System.IO.File
Imports System.Text.RegularExpressions
Imports System.Reflection

Public Class ClsDataProcess
    Public Enum StepType
        LA = 0
        FA = 1
        FT = 2
        Other = 3
    End Enum
    Public Enum ValueType
        Angle = 0
        Torque = 1
    End Enum
    Public Enum LimitMode
        Down = 0
        Up = 1
    End Enum
    Private Class AngleStep : Implements ICloneable
        Public m_Maximum_time As Double = 0
        Public m_Minimum_angle As Double = 0
        Public m_Maximum_angle As Double = 0
        Public m_Minimum_torque As Double = 0
        Public m_Maximum_torque As Double = 0
        Public m_Speed As Double = 0
        Public m_Shut_off_angle As Double = 0
        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return MemberwiseClone()
        End Function
        Public Property Maximum_time As Double
            Get
                Return m_Maximum_time
            End Get
            Set(ByVal value As Double)
                m_Maximum_time = value
            End Set
        End Property
        Public Property Minimum_angle As Double
            Get
                Return m_Minimum_angle
            End Get
            Set(ByVal value As Double)
                m_Minimum_angle = value
            End Set
        End Property
        Public Property Maximum_angle As Double
            Get
                Return m_Maximum_angle
            End Get
            Set(ByVal value As Double)
                m_Maximum_angle = value
            End Set
        End Property
        Public Property Minimum_torque As Double
            Get
                Return m_Minimum_torque
            End Get
            Set(ByVal value As Double)
                m_Minimum_torque = value
            End Set
        End Property
        Public Property Maximum_torque As Double
            Get
                Return m_Maximum_torque
            End Get
            Set(ByVal value As Double)
                m_Maximum_torque = value
            End Set
        End Property
        Public Property Speed As Double
            Get
                Return m_Speed
            End Get
            Set(ByVal value As Double)
                m_Speed = value
            End Set
        End Property
        Public Property Shut_off_angle As Double
            Get
                Return m_Shut_off_angle
            End Get
            Set(ByVal value As Double)
                m_Shut_off_angle = value
            End Set
        End Property

    End Class
    Private Class TorqueStep : Implements ICloneable
        Public m_Maximum_time As Double = 0
        Public m_Minimum_angle As Double = 0
        Public m_Maximum_angle As Double = 0
        Public m_Minimum_torque As Double = 0
        Public m_Maximum_torque As Double = 0
        Public m_Speed As Double = 0
        Public m_Shut_off_torque As Double = 0
        Public m_Torque_Hold_Time As Double = 0
        Public m_Threshold_torque As Double = 0
        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return MemberwiseClone()
        End Function
        Public Property Maximum_time As Double
            Get
                Return m_Maximum_time
            End Get
            Set(ByVal value As Double)
                m_Maximum_time = value
            End Set
        End Property
        Public Property Minimum_angle As Double
            Get
                Return m_Minimum_angle
            End Get
            Set(ByVal value As Double)
                m_Minimum_angle = value
            End Set
        End Property
        Public Property Maximum_angle As Double
            Get
                Return m_Maximum_angle
            End Get
            Set(ByVal value As Double)
                m_Maximum_angle = value
            End Set
        End Property
        Public Property Minimum_torque As Double
            Get
                Return m_Minimum_torque
            End Get
            Set(ByVal value As Double)
                m_Minimum_torque = value
            End Set
        End Property
        Public Property Maximum_torque As Double
            Get
                Return m_Maximum_torque
            End Get
            Set(ByVal value As Double)
                m_Maximum_torque = value
            End Set
        End Property
        Public Property Speed As Double
            Get
                Return m_Speed
            End Get
            Set(ByVal value As Double)
                m_Speed = value
            End Set
        End Property
        Public Property Shut_off_torque As Double
            Get
                Return m_Shut_off_torque
            End Get
            Set(ByVal value As Double)
                m_Shut_off_torque = value
            End Set
        End Property
        Public Property Torque_Hold_Time As Double
            Get
                Return m_Torque_Hold_Time
            End Get
            Set(ByVal value As Double)
                m_Torque_Hold_Time = value
            End Set
        End Property
        Public Property Threshold_torque As Double
            Get
                Return m_Threshold_torque
            End Get
            Set(ByVal value As Double)
                m_Threshold_torque = value
            End Set
        End Property
    End Class

    Public Structure strStepResults
        Dim index As Integer
        Dim name As String
        Dim m_Step As Integer
        Dim m_Torque_value As Double
        Dim m_Angle_value As Double
        Dim m_Torque_Ulimit As Double
        Dim m_Torque_Llimit As Double
        Dim m_Torque_Traget As Double
        Dim m_Angle_Ulimit As Double
        Dim m_Angle_LLimit As Double
        Dim m_Angle_Traget As Double
        Dim m_Torque_Unit As String
        Dim m_Angle_Unit As String
    End Structure
    Public Structure strScrewResults
        Dim m_date As String
        Dim m_time As String
        Dim m_Programme As Integer
        Dim m_Status As Integer
        Dim m_Total_Time As Double
        Dim io As Boolean
        Dim Steplist() As strStepResults
    End Structure
    Public Structure StepInfo
        Dim name As String
        Dim Type As StepType
        Dim Info As Object
        Dim index As Integer
    End Structure
    Public Structure ProgrammeInfo
        Dim m_stepInfo() As StepInfo
        Dim program_no As Integer
    End Structure
    Public Structure ProgrammeStepInfo
        Dim m_stepInfo() As StepInfo
        Dim program_no As Integer

    End Structure
    Public m_ProgrammeInfo(16) As ProgrammeInfo
    Public m_ProgrammeStepInfo(16) As ProgrammeStepInfo
    Public ScrewResults() As strScrewResults
    Public StepNameLA As String
    Public StepNameFA As String
    Public StepNameFT As String
    Public CSVFileName As String

    Public Sub New()
        StepNameLA = "Loosen to Angle"
        StepNameFA = "Fasten to Angle"
        StepNameFT = "Fasten to Torque"
        Array.Resize(m_ProgrammeInfo, 16)
    End Sub

    Private Function TransformStepNo(ByVal checkindex As String, ByVal SaveStep As Integer) As Integer
        Dim str() As String
        Dim tmpindex As Integer
        Try
            If checkindex.Contains(SaveStep.ToString) Then
                tmpindex = InStr(checkindex, SaveStep.ToString) - 3
                If checkindex.Length > 4 Then
                    str = checkindex.Substring(tmpindex, 4).Split(";")
                Else
                    str = checkindex.Substring(tmpindex, checkindex.Length).Split(";")
                End If

                If CInt(str(0).Split(",")(1)) = SaveStep Then
                    Return CInt(str(0).Split(",")(0))
                End If
            End If
            Return 0
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return -1
        End Try
    End Function
    Private Function LoadStepName(ByVal programmeno As Integer, ByVal stepno As Integer) As String
        Try
            If m_ProgrammeInfo(programmeno - 1).m_stepInfo IsNot Nothing Then
                Return m_ProgrammeInfo(programmeno - 1).m_stepInfo(stepno).name
            Else
                Return "error"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return "error"
        End Try
    End Function
    Private Function LoadLimitInfo(ByVal programmeno As Integer, ByVal stepindex As Integer, ByVal ValueType As ValueType, ByVal LimitType As LimitMode) As Double
        Try
            Select Case ValueType
                Case ValueType.Angle
                    Select Case LimitType
                        Case LimitMode.Down
                            Return m_ProgrammeInfo(programmeno - 1).m_stepInfo(stepindex).Info.Minimum_angle
                        Case LimitMode.Up
                            Return m_ProgrammeInfo(programmeno - 1).m_stepInfo(stepindex).Info.Maximum_angle
                    End Select
                Case ValueType.Torque
                    Select Case LimitType
                        Case LimitMode.Down
                            Return m_ProgrammeInfo(programmeno - 1).m_stepInfo(stepindex).Info.Minimum_torque
                        Case LimitMode.Up
                            Return m_ProgrammeInfo(programmeno - 1).m_stepInfo(stepindex).Info.Maximum_torque
                    End Select
            End Select
            Return 0
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return -999
        End Try
    End Function
    Private Function CheckActuralFile(ByVal url As String) As String
        Dim httpWebRequence As System.Net.HttpWebRequest
        Dim WebResponse As System.Net.WebResponse
        Dim resStream As System.IO.Stream
        Dim sr As System.IO.StreamReader
        Dim content As String
        Dim searchstr As String
        Dim tmpstr As String
        Dim index As Integer
        Dim acturaldate As String
        Dim acturalfilename As String

        If url.Length > 1 Then
            httpWebRequence = System.Net.WebRequest.Create(New Uri(url))
            System.Net.ServicePointManager.Expect100Continue = False
            Try

                WebResponse = httpWebRequence.GetResponse()
                resStream = WebResponse.GetResponseStream()

                sr = New System.IO.StreamReader(resStream)
                content = sr.ReadToEnd
                searchstr = "To download final data please click the appropriate link"
                tmpstr = content
                index = InStr(tmpstr, searchstr) + 100
                tmpstr = tmpstr.Substring(index, tmpstr.Length - index)

                searchstr = ">actual.csv"

                index = InStr(tmpstr, searchstr) + 12
                acturaldate = tmpstr.Substring(index, 10)

                index = InStr(tmpstr, acturaldate) - 20
                tmpstr = tmpstr.Substring(index, 20)
                acturalfilename = tmpstr.Split(">")(1).Split("(")(0).Trim

                Return acturalfilename
            Catch ex As System.Net.WebException
                If ex.Status <> System.Net.WebExceptionStatus.ProtocolError Then
                    Return 1
                End If
                If ex.Message.IndexOf("500") > 0 Then
                    Return 500
                End If
                If ex.Message.IndexOf("401") > 0 Then
                    Return 401
                End If
                If ex.Message.IndexOf("404") > 0 Then
                    Return 404
                End If
            End Try
            Return ""
        Else
            Return "error"
        End If
    End Function
    Public Function ChangeParameters(ByVal programmeno As Integer) As Integer
        Array.Resize(m_ProgrammeStepInfo(programmeno - 1).m_stepInfo, 3)
        Dim j As Integer = 0
        For i = 0 To m_ProgrammeInfo(programmeno - 1).m_stepInfo.GetUpperBound(0) - 1
            If m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.FA Or m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.FT Or m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.LA Then
                If j < 3 Then
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(j).index = i + 1
                    m_ProgrammeStepInfo(programmeno - 1).program_no = m_ProgrammeInfo(programmeno - 1).program_no
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(j).Info = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Info
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(j).Type = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(j).name = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name
                    j = j + 1
                Else



                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(0).Info = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).Info
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(0).index = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).index
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(0).Type = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).Type
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(0).name = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).name

                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).Info = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).Info
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).index = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).index
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).Type = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).Type
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(1).name = m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).name

                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).Info = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Info
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).index = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).index
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).Type = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).name = m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name
                    m_ProgrammeStepInfo(programmeno - 1).m_stepInfo(2).index = i + 1
                    m_ProgrammeStepInfo(programmeno - 1).program_no = m_ProgrammeInfo(programmeno - 1).program_no
                    j = j + 1
                End If
            End If
        Next
        Return 0
    End Function
    Public Function LoadParameters(ByVal url As String, ByVal programmeno As Integer) As Integer
        Dim httpWebRequence As System.Net.HttpWebRequest
        Dim WebResponse As System.Net.WebResponse
        Dim resStream As System.IO.Stream
        Dim sr As System.IO.StreamReader
        Dim content As String
        Dim m_match As Match
        Dim i, j, index, index_LA, index_FA, index_FT As Integer
        Dim tempcontent As String = String.Empty, tmpstr As String
        Dim matchstr As String = ""
        Dim m_objtmp_angle As AngleStep
        Dim m_objtmp_torque As TorqueStep
        Dim StepID As String
        Dim StepIDIndex As Integer
        Dim searchstr As String




        If url.Length > 1 Then
            httpWebRequence = System.Net.WebRequest.Create(New Uri(url))
            httpWebRequence.Timeout = 15000
            httpWebRequence.Proxy = Nothing
            Net.HttpWebRequest.DefaultWebProxy = Nothing
            System.Net.ServicePointManager.Expect100Continue = False
            Try

                WebResponse = httpWebRequence.GetResponse()
                resStream = WebResponse.GetResponseStream()

                sr = New System.IO.StreamReader(resStream)
                content = sr.ReadToEnd
                If content.Length < 10 Then Return -1
                searchstr = "lastStep"

                If content.IndexOf("程序") >= 0 Then Return -2
                index = InStr(content, searchstr) + 9
                StepIDIndex = CInt(content.Substring(index, 3).Replace(";", ""))

                Array.Resize(m_ProgrammeInfo(programmeno - 1).m_stepInfo, StepIDIndex)

                For i = 0 To StepIDIndex - 1
                    tmpstr = content

                    If content.Contains(StepNameLA) Then
                        index_LA = InStr(content, StepNameLA)
                    End If

                    If content.Contains(StepNameFA) Then
                        index_FA = InStr(content, StepNameFA)
                    End If

                    If content.Contains(StepNameFT) Then
                        index_FT = InStr(content, StepNameFT)
                    End If
                    StepID = "p" + (i + 1).ToString("00")
                    searchstr = "<a href=" + """" + "javascript:markStep('" + StepID + "')" + """" + "><span class=" + """" + "patterntitle" + """" + ">"
                    If content.Contains(searchstr) Then
                        index = InStr(tmpstr, searchstr) - 200
                        tmpstr = tmpstr.Substring(index, tmpstr.Length - index)
                        searchstr = "<td class=" + """" + "paramcell" + """" + ">"
                        index = InStr(tmpstr, searchstr) + searchstr.Length - 1
                        tmpstr = tmpstr.Substring(index, 200)
                        searchstr = "<a href=" + """" + "javascript:markStep('" + StepID + "')" + """" + "><span class=" + """" + "patterntitle" + """" + ">"
                        index = InStr(tmpstr, searchstr) + searchstr.Length - 1
                        tmpstr = tmpstr.Substring(index, tmpstr.Length - index).Replace("<br>", " ")
                        m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name = tmpstr.Split("</")(0).Trim
                    End If

                    Select Case m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name
                        Case StepNameLA
                            Dim contentNew As String = content.Substring(content.IndexOf(searchstr) + searchstr.Length)
                            index_LA = InStr(contentNew, StepNameLA)
                            tempcontent = contentNew.Substring(index_LA - 1)
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.LA
                        Case StepNameFA
                            Dim contentNew As String = content.Substring(content.IndexOf(searchstr) + searchstr.Length)
                            index_FA = InStr(contentNew, StepNameFA)
                            tempcontent = contentNew.Substring(index_FA - 1)
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.FA
                        Case StepNameFT
                            Dim contentNew As String = content.Substring(content.IndexOf(searchstr) + searchstr.Length)
                            index_FT = InStr(contentNew, StepNameFT)
                            tempcontent = contentNew.Substring(index_FT - 1)
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.FT
                        Case Else
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Type = StepType.Other
                    End Select

                    Select Case m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name
                        Case StepNameLA, StepNameFA
                            m_objtmp_angle = New AngleStep
                            For Each m_obj As Object In m_objtmp_angle.GetType.GetProperties
                                'Dim obj As Object = System.Activator.CreateInstance(_type, p)
                                Dim str() As String = m_obj.Name.Split("_")
                                matchstr = ""
                                For j = 0 To str.Length - 1
                                    If str(j) = "Shut" Then
                                        matchstr += str(0) + "-"
                                    Else
                                        matchstr += str(j) + " "
                                    End If
                                Next

                                If tempcontent.Contains(matchstr.Trim) Then
                                    tmpstr = tempcontent.Substring(InStr(tempcontent, matchstr.Trim), 100)
                                    Dim match As Match = Regex.Match(tmpstr, "name=" + "\" + """" + "(.*?)\" + """")
                                    'm_obj.value = match.Value         
                                    m_match = Regex.Match(content, "\$\(" + """" + "form\[name='MainForm'\] input\[name='" + match.Value.Split("""")(1) + "'\]" + """" + "\).val\(" + """" + "(\-|\+?)(([0-9]+|0)\.[0-9]*|([0-9]*|0)\.[0-9]+|[0-9]+)" + """" + "\)")

                                    If m_match.Success Then
                                        Select Case m_obj.Name
                                            Case "Maximum_time"
                                                m_objtmp_angle.m_Maximum_time = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Minimum_angle"
                                                m_objtmp_angle.m_Minimum_angle = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Maximum_angle"
                                                m_objtmp_angle.m_Maximum_angle = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Minimum_torque"
                                                m_objtmp_angle.m_Minimum_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Maximum_torque"
                                                m_objtmp_angle.m_Maximum_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Shut_off_angle"
                                                m_objtmp_angle.m_Shut_off_angle = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Speed"
                                                m_objtmp_angle.m_Speed = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                        End Select
                                    End If

                                End If
                            Next
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Info = m_objtmp_angle.Clone
                        Case StepNameFT
                            m_objtmp_torque = New TorqueStep
                            For Each m_obj As Object In m_objtmp_torque.GetType.GetProperties

                                Dim str() As String = m_obj.Name.Split("_")
                                matchstr = ""
                                For j = 0 To str.Length - 1
                                    If str(j) = "Shut" Then
                                        matchstr += str(0) + "-"
                                    Else
                                        matchstr += str(j) + " "
                                    End If
                                Next
                                If tempcontent.Contains(matchstr.Trim) Then
                                    tmpstr = tempcontent.Substring(InStr(tempcontent, matchstr.Trim), 100)
                                    Dim match As Match = Regex.Match(tmpstr, "name=" + "\" + """" + "(.*?)\" + """")
                                    'm_obj.value = match.Value         
                                    m_match = Regex.Match(content, "\$\(" + """" + "form\[name='MainForm'\] input\[name='" + match.Value.Split("""")(1) + "'\]" + """" + "\).val\(" + """" + "(\-|\+?)(([0-9]+|0)\.[0-9]*|([0-9]*|0)\.[0-9]+|[0-9]+)" + """" + "\)")

                                    If m_match.Success Then
                                        Select Case m_obj.Name
                                            Case "Maximum_time"
                                                m_objtmp_torque.m_Maximum_time = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Minimum_angle"
                                                m_objtmp_torque.m_Minimum_angle = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Maximum_angle"
                                                m_objtmp_torque.m_Maximum_angle = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Minimum_torque"
                                                m_objtmp_torque.m_Minimum_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Maximum_torque"
                                                m_objtmp_torque.m_Maximum_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Shut_off_torque"
                                                m_objtmp_torque.m_Shut_off_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Speed"
                                                m_objtmp_torque.m_Speed = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Threshold_torque"
                                                m_objtmp_torque.m_Threshold_torque = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                            Case "Torque_Hold_Time"
                                                m_objtmp_torque.m_Torque_Hold_Time = CDbl(m_match.Value.Split("val")(1).Split("""")(1))
                                        End Select
                                    End If

                                End If
                            Next
                            m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Info = m_objtmp_torque.Clone
                    End Select

                Next

                Return 0
            Catch ex As System.Net.WebException
                If ex.Status <> System.Net.WebExceptionStatus.ProtocolError Then
                    Return 1
                End If
                If ex.Message.IndexOf("500") > 0 Then
                    Return 500
                End If
                If ex.Message.IndexOf("401") > 0 Then
                    Return 401
                End If
                If ex.Message.IndexOf("404") > 0 Then
                    Return 404
                End If
            End Try
            Return -1
        Else
            Return -1
        End If
    End Function



    Public Function CheckIndex(ByVal programmeno As Integer) As String
        Dim i, j As Integer
        Dim Index As String

        Try
            Index = ""
            j = 0
            For i = 0 To m_ProgrammeInfo(programmeno - 1).m_stepInfo.Length - 1
                If m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).Info IsNot Nothing Then
                    Index += (i + 1).ToString + ","
                End If
                If m_ProgrammeInfo(programmeno - 1).m_stepInfo(i).name = "Save Values" Then
                    Index += (i + 1).ToString + ";"
                End If
            Next
            Return Index
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Sub LoadActuralValues(ByVal url As String)
        Dim str_tmp As String
        Dim index_str As String
        Dim i As Integer
        Dim programmeindex As Integer

        Try
            Dim MyClient As New System.Net.WebClient()

            str_tmp = MyClient.DownloadString(url)

            Array.Resize(ScrewResults, 1)
            ScrewResults(0).m_date = str_tmp.Split(",")(0)
            ScrewResults(0).m_time = str_tmp.Split(",")(1)
            ScrewResults(0).m_Programme = CInt(str_tmp.Split(",")(2))

            programmeindex = ScrewResults(0).m_Programme

            index_str = CheckIndex(programmeindex)

            ScrewResults(0).m_Status = CInt(str_tmp.Split(",")(3))
            ScrewResults(0).m_Total_Time = CDbl(str_tmp.Split(",")(4))
            i = 1
            If ScrewResults(0).m_Status = 0 Then

                If CInt(str_tmp.Split(",")(5)) > 0 Then
                    Array.Resize(ScrewResults(0).Steplist, i)
                    ScrewResults(0).Steplist(i - 1).m_Step = TransformStepNo(index_str, CInt(str_tmp.Split(",")(5)))
                    ScrewResults(0).Steplist(i - 1).index = ScrewResults(0).Steplist(i - 1).m_Step - 1
                    ScrewResults(0).Steplist(i - 1).m_Torque_Unit = str_tmp.Split(",")(19)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Unit = str_tmp.Split(",")(20)
                    ScrewResults(0).Steplist(i - 1).m_Torque_value = CDbl(str_tmp.Split(",")(6))
                    ScrewResults(0).Steplist(i - 1).m_Angle_value = CDbl(str_tmp.Split(",")(7))

                    If LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index) <> "error" Then
                        ScrewResults(0).Steplist(i - 1).name = LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index)
                    End If
                    ScrewResults(0).Steplist(i - 1).m_Angle_LLimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Up)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Llimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Up)
                    i += 1
                End If
                If CInt(str_tmp.Split(",")(8)) > 0 Then
                    Array.Resize(ScrewResults(0).Steplist, i)
                    ScrewResults(0).Steplist(i - 1).m_Step = TransformStepNo(index_str, CInt(str_tmp.Split(",")(8)))
                    ScrewResults(0).Steplist(i - 1).index = ScrewResults(0).Steplist(i - 1).m_Step - 1
                    ScrewResults(0).Steplist(i - 1).m_Torque_Unit = str_tmp.Split(",")(19)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Unit = str_tmp.Split(",")(20)
                    ScrewResults(0).Steplist(i - 1).m_Torque_value = CDbl(str_tmp.Split(",")(9))
                    ScrewResults(0).Steplist(i - 1).m_Angle_value = CDbl(str_tmp.Split(",")(10))

                    If LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index) <> "error" Then
                        ScrewResults(0).Steplist(i - 1).name = LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index)
                    End If
                    ScrewResults(0).Steplist(i - 1).m_Angle_LLimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Up)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Llimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Up)
                    i += 1
                End If
                If CInt(str_tmp.Split(",")(11)) > 0 Then
                    Array.Resize(ScrewResults(0).Steplist, i)
                    ScrewResults(0).Steplist(i - 1).m_Step = TransformStepNo(index_str, CInt(str_tmp.Split(",")(11)))
                    ScrewResults(0).Steplist(i - 1).index = ScrewResults(0).Steplist(i - 1).m_Step - 1
                    ScrewResults(0).Steplist(i - 1).m_Torque_Unit = str_tmp.Split(",")(19)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Unit = str_tmp.Split(",")(20)
                    ScrewResults(0).Steplist(i - 1).m_Torque_value = CDbl(str_tmp.Split(",")(12))
                    ScrewResults(0).Steplist(i - 1).m_Angle_value = CDbl(str_tmp.Split(",")(13))

                    If LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index) <> "error" Then
                        ScrewResults(0).Steplist(i - 1).name = LoadStepName(programmeindex, ScrewResults(0).Steplist(i - 1).index)
                    End If
                    ScrewResults(0).Steplist(i - 1).m_Angle_LLimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Angle_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Angle, LimitMode.Up)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Llimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Down)
                    ScrewResults(0).Steplist(i - 1).m_Torque_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(i - 1).index, ValueType.Torque, LimitMode.Up)
                    i += 1
                End If
                ScrewResults(0).io = True
            Else
                Array.Resize(ScrewResults(0).Steplist, 1)
                ScrewResults(0).Steplist(0).m_Step = CInt(str_tmp.Split(",")(14))
                ScrewResults(0).Steplist(0).index = ScrewResults(0).Steplist(i - 1).m_Step - 1
                ScrewResults(0).Steplist(0).m_Torque_Unit = str_tmp.Split(",")(19)
                ScrewResults(0).Steplist(0).m_Angle_Unit = str_tmp.Split(",")(20)
                ScrewResults(0).Steplist(0).m_Torque_value = CDbl(str_tmp.Split(",")(15))
                ScrewResults(0).Steplist(0).m_Angle_value = CDbl(str_tmp.Split(",")(16))


                If LoadStepName(programmeindex, ScrewResults(0).Steplist(0).index) <> "error" Then
                    ScrewResults(0).Steplist(0).name = LoadStepName(programmeindex, ScrewResults(0).Steplist(0).index)
                End If
                ScrewResults(0).Steplist(0).m_Angle_LLimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(0).index, ValueType.Angle, LimitMode.Down)
                ScrewResults(0).Steplist(0).m_Angle_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(0).index, ValueType.Angle, LimitMode.Up)
                ScrewResults(0).Steplist(0).m_Torque_Llimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(0).index, ValueType.Torque, LimitMode.Down)
                ScrewResults(0).Steplist(0).m_Torque_Ulimit = LoadLimitInfo(programmeindex, ScrewResults(0).Steplist(0).index, ValueType.Torque, LimitMode.Up)

                ScrewResults(0).io = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
