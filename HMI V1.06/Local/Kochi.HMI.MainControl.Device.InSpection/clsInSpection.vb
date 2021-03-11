Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.IO
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("Inspection", "Inspection")>
Public Class clsInSpection
    Inherits clsHMIInSpection


    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Protected cMachineManager As clsMachineManager
    Private strFailPath As String = String.Empty
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "1"), enumExceptionType.Crash)
            Return False
        End If
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        DeleteFile()
        Return True
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
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then
            RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If

        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
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
    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        MyBase.CreateShortcutUI(cLocalElement, cSystemElement)
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function BackUpPicture(ByVal strVariant As String, ByVal strSFC As String) As Boolean
        strFailPath = ""
        Dim bFind As Boolean = False
        Dim sw As New Stopwatch
        sw.Start()
        If lListInitParameter(1) = enumInSpectionType.Vision.ToString() Then
            If lListInitParameter(3) <> "" And lListInitParameter(4) <> "" Then
                Do While sw.ElapsedMilliseconds < lListInitParameter(6)
                    Dim directroyInfo As DirectoryInfo = New DirectoryInfo(lListInitParameter(3))
                    Dim c() As FileInfo = directroyInfo.GetFiles
                    Dim lList As List(Of FileInfo) = c.ToList
                    SortAsFileCreationTime(lList)
                    For Each tempFile As FileInfo In lList
                        If IsPicture(tempFile.FullName) Then

                            If lListInitParameter(5) <> "" Then
                                If tempFile.Name.IndexOf(lListInitParameter(5)) < 0 Then
                                    Continue For
                                End If
                            End If
                            If Not Directory.Exists(lListInitParameter(4) + "\" + strVariant) Then
                                Directory.CreateDirectory(lListInitParameter(4) + "\" + strVariant)
                            End If
                            If Not Directory.Exists(lListInitParameter(4) + "\" + strVariant + "\" + Now.ToString("yyyy-MM-dd")) Then
                                Directory.CreateDirectory(lListInitParameter(4) + "\" + strVariant + "\" + Now.ToString("yyyy-MM-dd"))
                            End If
                            If bFind Then
                                File.Delete(tempFile.FullName)
                            Else
                                Dim strFileName As String = lListInitParameter(4) + "\" + strVariant + "\" + Now.ToString("yyyy-MM-dd") + "\" + strSFC + "-" + Now.ToString("yyyy-MM-dd-HH-mm-ss") + tempFile.Extension
                                If File.Exists(strFileName) Then
                                    File.Delete(strFileName)
                                End If
                                File.Move(tempFile.FullName, strFileName)
                                strFailPath = strFileName
                                System.Threading.Thread.Sleep(100)
                                bFind = True
                            End If
                        End If
                    Next
                    If bFind Then
                        Exit Do
                    End If
                Loop
            End If
        End If
        If lListInitParameter(1) = enumInSpectionType.Vision.ToString() Then
            If lListInitParameter(3) <> "" Then
                If Not bFind Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "9"), enumExceptionType.Crash)
                    Return False
                End If
            End If
        End If

        Return True
    End Function
    Private Sub SortAsFileCreationTime(ByRef arrFi As List(Of FileInfo))
        arrFi.Sort(Function(x As FileInfo, y As FileInfo) y.CreationTime.CompareTo(x.CreationTime))
    End Sub



    Public Function IsPicture(ByVal fileName As String) As Boolean
        Dim strFilter As String = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|"
        Dim tempFileds() As String = strFilter.Split("|")
        For Each Str As String In tempFileds
            If (Str.ToUpper() = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf(".")).ToUpper()) Then
                Return True
            End If

        Next
        Return False
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iParameterUI) Then
            RemoveHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iParameterUI.Quit(cLocalElement, cSystemElement)
        End If
        iParameterUI = New ParameterUI
        AddHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True

    End Function

    Private Sub DeleteFile()
        If lListInitParameter(4) <> "" Then
            If Directory.Exists(lListInitParameter(4)) Then
                Dim directroyInfo As DirectoryInfo = New DirectoryInfo(lListInitParameter(4))
                Dim t1 As DateTime
                Dim t2 As DateTime
                t2 = DateTime.Parse(Date.Now.AddYears(-CInt(cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DataBaseSaveTime))).ToString)
                For Each tempFile As DirectoryInfo In directroyInfo.GetDirectories
                    t1 = DateTime.Parse(tempFile.CreationTime.ToString)
                    If DateTime.Compare(t1, t2) <= 0 Then
                        Directory.Delete(tempFile.FullName, True)
                    End If
                Next
            End If
        End If
    End Sub

    Public Overrides ReadOnly Property FailPicPath As String
        Get
            Return strFailPath
        End Get
    End Property

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


    Public Overrides Function DeletePicture() As Boolean
        Try
            strFailPath = ""
            Dim bFind As Boolean = False
            Dim sw As New Stopwatch
            sw.Start()
            If lListInitParameter(1) = enumInSpectionType.Vision.ToString() Then
                If lListInitParameter(3) <> "" Then
                    Dim directroyInfo As DirectoryInfo = New DirectoryInfo(lListInitParameter(3))
                    Dim c() As FileInfo = directroyInfo.GetFiles
                    Dim lList As List(Of FileInfo) = c.ToList
                    SortAsFileCreationTime(lList)
                    For Each tempFile As FileInfo In lList
                        If IsPicture(tempFile.FullName) Then
                            File.Delete(tempFile.FullName)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructInSpection
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoAciton As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIProg As Int16 = 0

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCResult As Boolean = False
End Class
