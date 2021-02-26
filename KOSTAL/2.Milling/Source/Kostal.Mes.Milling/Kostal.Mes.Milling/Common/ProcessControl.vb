
Imports System.Diagnostics

Public Class ProcessControl
    Public ReadOnly Property FileName As String
        Get
            Return My.Application.Info.AssemblyName + ".exe"
        End Get
    End Property

	Public Function IsMyProcessRunning() As Boolean

		Dim _ProcessList As Process() = Process.GetProcesses
		Dim _OneProcess As Process
		Dim _Split() As String
		Dim _FileString As String = String.Empty
		Dim _FolderString As String = String.Empty
		Dim Index As Integer
		Dim _FileCount As Integer = 0

		_Split = My.Application.Info.DirectoryPath.Split(CChar("\"))

		For Index = _Split.GetLowerBound(0) To _Split.GetUpperBound(0) - 1
			_FolderString = _FolderString + _Split(Index) + "\"
		Next
        _FileString = My.Application.Info.AssemblyName + ".exe"

		For Each _OneProcess In _ProcessList

			Try
                If InStr(_OneProcess.MainModule.FileName.ToUpper, _FolderString, CompareMethod.Text) <> 0 _
                And InStr(_OneProcess.MainModule.FileName.ToUpper, _FileString.ToUpper) <> 0 Then

					_FileCount += 1

					If _FileCount > 1 Then
						Return True
					End If

				End If

			Catch ex As Exception
				Return False
			End Try

		Next

		Return False

	End Function
End Class
