Imports System
Imports System.Globalization
Imports System.Runtime.InteropServices

Public Module CultureHelper
    ''' <summary>
    ''' Change the Culture/Ui culture for the current thread.
    ''' Shows a messagebox if there is a problem.
    ''' </summary>
    ''' <param name="languageId">The language id of the culture to set. E.g. 'de' or 'de-DE'</param>
    ''' <returns>Returns <c>True</c> if no problem occur, otherwise <c>False</c>.</returns>
    Public Function SetCultureForApplication(languageId As String, ByRef possibleProblemText As String) As Boolean
        Dim message As String
        Dim cultureInfoToSet As CultureInfo
        Try
            cultureInfoToSet = New CultureInfo(languageId)

        Catch ex As Exception
            message = ex.GetMessagesIncludingInnerExceptions()
            If possibleProblemText IsNot Nothing Then
                possibleProblemText = "Problem when creating culture for language Id: """ + languageId + """. Exceptionmessage: " + message
            End If
            'Call MessageBox(Nothing, "Problem when creating culture for language Id: """ + languageId + """" + Environment.NewLine + Environment.NewLine + message, "Setting language", MessageBoxOptions.IconError)
            Return False
        End Try

        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfoToSet
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfoToSet
            CultureInfo.DefaultThreadCurrentCulture = cultureInfoToSet
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfoToSet
        Catch ex As Exception
            message = ex.GetMessagesIncludingInnerExceptions()
            If possibleProblemText IsNot Nothing Then
                possibleProblemText = "Problem when setting language for language Id: """ + languageId + """. Exceptionmessage: " + message
            End If
            'Call MessageBox(Nothing, "Problem when setting language for language Id: """ + languageId + """" + Environment.NewLine + Environment.NewLine + message, "Setting language", MessageBoxOptions.IconError)
            Return False
        End Try

        Return True
    End Function

    <DllImport("user32.dll", EntryPoint:="MessageBoxW", SetLastError:=True, Charset:=CharSet.Unicode)> _
    Private Function MessageBox(hwnd As IntPtr, <MarshalAs(UnmanagedType.LPTStr)> lpText As String, <MarshalAs(UnmanagedType.LPTStr)> lpCaption As String, <MarshalAs(UnmanagedType.U4)> uType As MessageBoxOptions) As <MarshalAs(UnmanagedType.U4)> MessageBoxResult
    End Function

    '''<summary>
    ''' Flags that define appearance and behaviour of a standard message box displayed by a call to the MessageBox function.
    ''' </summary>    
    <Flags> _
    Private Enum MessageBoxOptions As UInteger
        OkOnly = &H0
        OkCancel = &H1
        AbortRetryIgnore = &H2
        YesNoCancel = &H3
        YesNo = &H4
        RetryCancel = &H5
        CancelTryContinue = &H6
        IconHand = &H10
        IconQuestion = &H20
        IconExclamation = &H30
        IconAsterisk = &H40
        UserIcon = &H80
        IconWarning = IconExclamation
        IconError = IconHand
        IconInformation = IconAsterisk
        IconStop = IconHand
        DefButton1 = &H0
        DefButton2 = &H100
        DefButton3 = &H200
        DefButton4 = &H300
        ApplicationModal = &H0
        SystemModal = &H1000
        TaskModal = &H2000
        Help = &H4000
        NoFocus = &H8000
        SetForeground = &H10000
        DefaultDesktopOnly = &H20000
        Topmost = &H40000
        Right = &H80000
        RTLReading = &H100000
    End Enum
    ''' <summary>
    ''' Represents possible values returned by the MessageBox function.
    ''' </summary>
    Private Enum MessageBoxResult As UInteger
        Ok = 1
        Cancel
        Abort
        Retry
        Ignore
        Yes
        No
        Close
        Help
        TryAgain
        ContinueOn
        Timeout = 32000
    End Enum
End Module
