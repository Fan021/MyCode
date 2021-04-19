Imports Kostal.Las.UserInterface.Design

Namespace Helpers

    Public Class VisibilitiesHelper
        Public Shared Function UserLevelToVisibilities(ByVal usrLevel As UserLevel) As Visibilities
            Dim vis As Visibilities = 0
            If (usrLevel And UserLevel.Operator) = UserLevel.Operator Then
                vis = vis Or Visibilities.Operator
            End If
            If (usrLevel And UserLevel.Developer) = UserLevel.Developer Then
                vis = vis Or Visibilities.Developer
            End If
            If (usrLevel And UserLevel.Service) = UserLevel.Service Then
                vis = vis Or Visibilities.Service
            End If
            Return vis
        End Function

        Public Shared Function IsVisibleWithUserLevelAndVisibility(ByVal usrLevel As UserLevel, ByVal visibility As Visibilities) As Boolean
            If ((usrLevel And UserLevel.Operator) = UserLevel.Operator) AndAlso ((visibility And Visibilities.Operator) = Visibilities.Operator) Then
                Return True
            End If
            If ((usrLevel And UserLevel.Developer) = UserLevel.Developer) AndAlso ((visibility And Visibilities.Developer) = Visibilities.Developer) Then
                Return True
            End If
            If ((usrLevel And UserLevel.Service) = UserLevel.Service) AndAlso ((visibility And Visibilities.Service) = Visibilities.Service) Then
                Return True
            End If
            Return False
        End Function
    End Class
End NameSpace