Public Class BaseForUserLevelToConverter

    Public Shared Function Check(values As Object(), parameter As Object) As Boolean
        Try
            Dim result As Boolean = False
            If values.Any(Function(x As Object)
                              OutputDebugStringConditionalDebug("{0}", x.ToString())
                              Return x Is System.Windows.DependencyProperty.UnsetValue
                          End Function) Then
                System.Diagnostics.Trace.WriteLine(String.Format("BaseForUserLevelToConverter.Check() values.Any(Function(x As Object) x Is DependencyProperty.UnsetValue"))
                Return True
            End If

            If values.Count() <> 2 Then
                Throw New ArgumentException("Only 2 binding values are supported.")
            End If

            Dim userlevel As Global.Kostal.Testman.Framework.Base.UserLevel
            If (values(0)).GetType() IsNot GetType(Global.Kostal.Testman.Framework.Base.UserLevel) Then
                Throw New ArgumentException("First value is not of type Kostal.Testman.Plugin.Base.UserLevel")
            End If
            userlevel = DirectCast(values(0), Global.Kostal.Testman.Framework.Base.UserLevel)

            Dim visibilitiesOfControl As Kostal.Testman.UserInterface.Design.Visibilities
            If (values(1)).GetType() IsNot GetType(Kostal.Testman.UserInterface.Design.Visibilities) Then
                Throw New ArgumentException("Second value is not of type Kostal.Testman.UserInterface.Design.Visibilities")
            End If
            visibilitiesOfControl = DirectCast(values(1), Kostal.Testman.UserInterface.Design.Visibilities)

            Dim invertieren As Boolean = ParameterToBool(parameter)

            Select Case userlevel
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Operator
                    If (visibilitiesOfControl And Design.Visibilities.Operator) = Design.Visibilities.Operator Then
                        result = True
                    End If
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Developer
                    If (visibilitiesOfControl And Design.Visibilities.Developer) = Design.Visibilities.Developer Then
                        result = True
                    End If
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Service
                    If (visibilitiesOfControl And Design.Visibilities.Service) = Design.Visibilities.Service Then
                        result = True
                    End If
            End Select

            If invertieren Then
                result = Not result
            End If

            Return result
        Catch ex As Exception
            OutputDebugString("BaseForUserLevelToConverter Check {0}", ex.ToString())
        End Try
        Return True
    End Function

    Private Shared Function ParameterToBool(parameter As Object) As Boolean
        If TypeOf parameter Is Boolean Then
            Return CBool(parameter)
        End If

        Dim value As String = TryCast(parameter, String)
        If value IsNot Nothing AndAlso value.ToLowerInvariant() = "true" Then
            Return True
        End If

        Return False
    End Function

End Class