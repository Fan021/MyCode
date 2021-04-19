Imports System

Namespace Globalization

    ''' <summary>
    ''' Class the get localized strings out of a translation file
    ''' </summary>
    Public Class Localizer

        Private ReadOnly _fileNameOfFrameworkDll As String = Nothing
        Private ReadOnly _fileNameOfCallingDll As String = Nothing
        Private ReadOnly _topicName As String = "Common"

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Localizer"/> class.
        ''' </summary>
        ''' <param name="isFrameworkDllBetweenInCallStack">if set to <c>true</c> [is framework DLL in call stack].</param>
        Public Sub New(isFrameworkDllBetweenInCallStack As Boolean, Optional numberOfInnerFrameworkDlls As Integer = 1)

            Dim typenameOfCallingDll As String = Nothing

            'Try-Catch to be prepared from calling in visual studio/per reflection/other AppDomain/etc
            Try
                Dim st As New Diagnostics.StackTrace
                For Each sf As Diagnostics.StackFrame In st.GetFrames()
                    Dim fn As String = sf.GetMethod().Name + "   of   " + sf.GetMethod().DeclaringType.ToString() + "   in   " + sf.GetMethod().DeclaringType.Assembly.FullName
                    System.Diagnostics.Trace.WriteLine(fn)
                Next

                Dim filenameOfCommonDll As String = Nothing
                Dim filenameOfFrameworkDll As String = Nothing
                Dim filepathOfFrameworkDll As String = Nothing
                Dim filenameOfCallingDll As String = Nothing
                Dim filepathOfCallingDll As String = Nothing
                Dim numberOfSkippedInnerFrameworkDlls As Integer = 0
                For Each sf As Diagnostics.StackFrame In st.GetFrames()
                    Dim typeOfFrameDll As Type = sf.GetMethod().DeclaringType
                    Dim filenameOfFrameDll As String = typeOfFrameDll.Assembly.GetName().Name
                    If filenameOfCommonDll Is Nothing Then
                        filenameOfCommonDll = filenameOfFrameDll
                        Continue For
                    ElseIf filenameOfFrameDll = filenameOfCommonDll Then
                        Continue For
                    ElseIf isFrameworkDllBetweenInCallStack Then
                        If System.String.IsNullOrWhiteSpace(filenameOfFrameworkDll) Then
                            numberOfSkippedInnerFrameworkDlls += 1
                            If Not numberOfSkippedInnerFrameworkDlls < numberOfInnerFrameworkDlls Then
                                filenameOfFrameworkDll = filenameOfFrameDll
                                filepathOfFrameworkDll = typeOfFrameDll.Assembly.Location
                            End If
                            Continue For
                        ElseIf filenameOfFrameDll = filenameOfFrameworkDll Then
                            Continue For
                        Else
                            filenameOfCallingDll = filenameOfFrameDll
                            filepathOfCallingDll = typeOfFrameDll.Assembly.Location
                            typenameOfCallingDll = typeOfFrameDll.FullName
                            Exit For
                        End If
                        Else
                            If System.String.IsNullOrWhiteSpace(filenameOfCallingDll) Then
                                filenameOfCallingDll = filenameOfFrameDll
                                filepathOfCallingDll = typeOfFrameDll.Assembly.Location
                                typenameOfCallingDll = typeOfFrameDll.FullName
                                Continue For
                            ElseIf filenameOfFrameDll = filenameOfCallingDll Then
                                Continue For
                            Else
                                filenameOfFrameworkDll = filenameOfFrameDll
                                filepathOfFrameworkDll = typeOfFrameDll.Assembly.Location
                                Exit For
                            End If
                        End If
                Next
                'System.Diagnostics.Trace.WriteLine("SKIPPED1 = " + nameOfThisDll)
                'System.Diagnostics.Trace.WriteLine("SKIPPED2 = " + nameOfFrameworkDll)
                'System.Diagnostics.Trace.WriteLine("RESULT   = " + nameOfCallerDll)

                'OutputDebugStringConditionalDebug("1 {0}", typeCallingClass.FullName)

                'Generate topic name
                '_topicName = typeCallingClass.FullName
                'assemblyOfCallingClass = typeCallingClass.Assembly

                _topicName = typenameOfCallingDll
                _fileNameOfCallingDll = GetFileNameAndCreateLngFile(filepathOfCallingDll, filenameOfCallingDll, True)
                _fileNameOfFrameworkDll = GetFileNameAndCreateLngFile(filepathOfFrameworkDll, filenameOfFrameworkDll, True)
                OutputDebugStringConditionalDebug("Creating localisation class from stackframe for {0}. In File: {1} with Topic: {2}", typenameOfCallingDll.ToString(), _fileNameOfCallingDll, _topicName)

            Catch ex As System.Exception
                System.Media.SystemSounds.Exclamation.Play()
                OutputDebugString("Problem when creating localisation from stackframe for {0}. Ex: {1}", typenameOfCallingDll, ex.GetMessagesIncludingInnerExceptions())
            End Try
        End Sub

        Public Sub New(fileName As String)
            _fileNameOfCallingDll = GetFileNameAndCreateLngFile(System.IO.Path.GetFileNameWithoutExtension(fileName))
        End Sub

        Public Sub New(fileName As String, topicName As String)
            _fileNameOfCallingDll = GetFileNameAndCreateLngFile(System.IO.Path.GetFileNameWithoutExtension(fileName))
            _topicName = topicName
        End Sub

        Public Sub New(callingClass As System.Object)

            'Try-Catch to be prepared from calling in visual studio/per reflection/other AppDomain/etc
            Try
                'Generate topic name
                Dim assemblyOfCallingClass As System.Reflection.Assembly
                If callingClass Is Nothing Then
                    assemblyOfCallingClass = System.Reflection.Assembly.GetExecutingAssembly()
                Else
                    Dim typeCallingClass As System.Type = callingClass.GetType()
                    _topicName = typeCallingClass.FullName
                    assemblyOfCallingClass = callingClass.GetType().Assembly
                End If
                _fileNameOfCallingDll = GetFileNameAndCreateLngFile(assemblyOfCallingClass)
                OutputDebugStringConditionalDebug("Creating localisation class for {0}. In File: {1} with Topic: {2}", callingClass.GetType().ToString(), _fileNameOfCallingDll, _topicName)

            Catch ex As System.Exception
                System.Media.SystemSounds.Exclamation.Play()
                OutputDebugString("Problem when creating localisation class for {0}. Ex: {1}", callingClass.GetType().ToString(), ex.GetMessagesIncludingInnerExceptions())
            End Try
        End Sub

        Public Sub New(Optional skipFrames As Integer = 1)
            Dim assemblyOfCallingClass As System.Reflection.Assembly
            Dim typeCallingClass As System.Type = Nothing

            'Try-Catch to be prepared from calling in visual studio/per reflection/other AppDomain/etc
            Try
                Dim frame As System.Diagnostics.StackFrame
                frame = New System.Diagnostics.StackFrame(skipFrames, False)
                typeCallingClass = frame.GetMethod().DeclaringType

                OutputDebugStringConditionalDebug("1 {0}", typeCallingClass.FullName)

                'Generate topic name
                _topicName = typeCallingClass.FullName
                assemblyOfCallingClass = typeCallingClass.Assembly

                _fileNameOfCallingDll = GetFileNameAndCreateLngFile(assemblyOfCallingClass)
                OutputDebugStringConditionalDebug("Creating localisation class from stackframe for {0}. In File: {1} with Topic: {2}", typeCallingClass.ToString(), _fileNameOfCallingDll, _topicName)

            Catch ex As System.Exception
                System.Media.SystemSounds.Exclamation.Play()
                OutputDebugString("Problem when creating localisation from stackframe for {0}. Ex: {1}", typeCallingClass, ex.GetMessagesIncludingInnerExceptions())
            End Try
        End Sub

        Private Function GetFileNameAndCreateLngFile(assemblyCallingClass As System.Reflection.Assembly) As String
            Return GetFileNameAndCreateLngFile(assemblyCallingClass.Location, assemblyCallingClass.GetName().Name, True)
        End Function

        Private Function GetFileNameAndCreateLngFile(filePath As String, fileName As String, pathMustBeCorrected As Boolean) As String
            If System.String.IsNullOrWhiteSpace(fileName) Then Return System.String.Empty
            If System.String.IsNullOrWhiteSpace(filePath) Then Return System.String.Empty
            If pathMustBeCorrected Then filePath = System.IO.Path.GetDirectoryName(filePath)
            Return GetFileNameAndCreateLngFile(System.IO.Path.Combine(filePath, fileName))
        End Function

        Private Function GetFileNameAndCreateLngFile(fileName As String) As String

            Dim cultureName As String
            cultureName = System.Threading.Thread.CurrentThread.CurrentCulture().Name

            Dim posOfMinus As Integer
            posOfMinus = cultureName.IndexOf("-", StringComparison.InvariantCultureIgnoreCase)

            If posOfMinus > -1 Then
                cultureName = cultureName.Substring(0, posOfMinus)
            End If

            'Generate file name
            fileName += "."
            fileName += cultureName
            fileName += ".lng"

            'If file not exists, file must created in UTF-8
            Kostal.Runtime.Ini.CreateIniFileIfNotExists(fileName)
            Return fileName
        End Function

        Public Function GetLocalizedString(originalText As String) As String

            If System.String.IsNullOrWhiteSpace(originalText) Then Return originalText
            If System.String.IsNullOrWhiteSpace(_fileNameOfCallingDll) AndAlso System.String.IsNullOrWhiteSpace(_fileNameOfFrameworkDll) Then Return originalText

            Dim searchText As String = originalText.Replace(System.Environment.NewLine, "#").Trim()
            If searchText.StartsWith("[") Then
                searchText = "&" + searchText
            End If

            Dim localizedText As String = "??"
            If Not System.String.IsNullOrWhiteSpace(_fileNameOfCallingDll) Then Kostal.Runtime.Ini.ReadItem(_topicName, searchText, "??", _fileNameOfCallingDll)
            If localizedText = "??" AndAlso Not System.String.IsNullOrWhiteSpace(_fileNameOfFrameworkDll) Then Kostal.Runtime.Ini.ReadItem(_topicName, searchText, "??", _fileNameOfFrameworkDll)
            If localizedText = "??" Then
                If Not System.String.IsNullOrWhiteSpace(_fileNameOfCallingDll) Then
                    Kostal.Runtime.Ini.WriteItem(_topicName, searchText, "?", _fileNameOfCallingDll)
                ElseIf Not System.String.IsNullOrWhiteSpace(_fileNameOfFrameworkDll) Then
                    Kostal.Runtime.Ini.WriteItem(_topicName, searchText, "?", _fileNameOfFrameworkDll)
                End If
                Return originalText
            End If

            If localizedText.StartsWith("&[") Then
                Return localizedText.Substring(1).Replace("#", System.Environment.NewLine)
            Else
                Return localizedText.Replace("#", System.Environment.NewLine)
            End If
        End Function

        Public ReadOnly Property FileNameForCallingDll As String
            Get
                Return _fileNameOfCallingDll
            End Get
        End Property

        Public ReadOnly Property FileNameForFrameworkDll As String
            Get
                Return _fileNameOfFrameworkDll
            End Get
        End Property

    End Class

End Namespace