
Imports System.Diagnostics
'这个类用来检测LAS程序是否已在运行
Public Class ProcessControl

	Public Function IsMyProcessRunning() As Boolean
        '获得所有的进程
        Dim _ProcessList As Process() = Process.GetProcesses
		Dim _OneProcess As Process
		Dim _Split() As String
		Dim _FileString As String = String.Empty
		Dim _FolderString As String = String.Empty
		Dim Index As Integer
		Dim _FileCount As Integer = 0
        Dim mName As String = ""
        _Split = My.Application.Info.DirectoryPath.Split(CChar("\"))

		For Index = _Split.GetLowerBound(0) To _Split.GetUpperBound(0) - 1
			_FolderString = _FolderString + _Split(Index) + "\"
		Next

        _Split = My.Application.Info.AssemblyName.Split(CChar("."))
        '获得执行程序的名字
        _FileString = My.Application.Info.AssemblyName + ".exe"
        '对比每一个进程名字
        For Each _OneProcess In _ProcessList
            Try
                mName = _OneProcess.MainModule.ModuleName.ToUpper
            Catch ex As Exception
                mName = ""
            End Try

            If mName = _FileString.ToUpper Then

                _FileCount += 1

                If _FileCount > 1 Then
                    Throw New Exception(_FileString + " have running")
                    Return True
                End If

            End If



        Next

		Return False

	End Function
End Class
