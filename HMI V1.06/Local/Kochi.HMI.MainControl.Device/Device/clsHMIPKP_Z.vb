﻿Imports System.Collections.Concurrent

Public MustInherit Class clsHMIPKP_Z
    Inherits clsHMIPKP

    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function GetXValue() As Double
    Public MustOverride Overrides Function GetYValue() As Double
    Public MustOverride Function GetZValue() As Double
End Class
