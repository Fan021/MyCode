
Imports System.Drawing
Imports Kostal.Las.Base

Public Class KostalLasColors


    'LK_COLOR_WHITE = -2147483643
    'LK_COLOR_BLACK = 0
    'LK_COLOR_RED = 155
    'LK_COLOR_LIGHTRED = 255
    'LK_COLOR_YELLOW = 65535
    'LK_COLOR_GREEN = 3329460
    'LK_COLOR_ORANGE = 3654650
    'LK_COLOR_BLUE = 8209950
    'LK_COLOR_GREY = 13487560
    'LK_COLOR_NOMALBLUE = 14417920
    'LK_COLOR_LIGHTBLUE = 15459282
    'LK_COLOR_LIGHTGREY = 15592939


    Private Shared ReadOnly _RED As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
    Private Shared ReadOnly _GREEN As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
    Private Shared ReadOnly _KOSTALBLUE As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)

    Private Shared ReadOnly _BLACK As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLACK)
    Private Shared ReadOnly _GRAY As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREY)
    Private Shared ReadOnly _WHITE As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE)

    Private Shared ReadOnly _YELLOW As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_YELLOW)
    Private Shared ReadOnly _ORANGE As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_ORANGE)
    Private Shared ReadOnly _NORMALBLUE As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_NOMALBLUE)

    Private Shared ReadOnly _LIGHTRED As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED)
    Private Shared ReadOnly _LIGHTBLUE As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTBLUE)
    Private Shared ReadOnly _LIGHTGRAY As Color = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTGREY)

    Private Shared ReadOnly _BUTTONFACE As Color = SystemColors.ButtonFace

    Private Shared ReadOnly _YELLOWLIGHT As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 127)
    Private Shared ReadOnly _YELLOWWHITE As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 204)

    Public Shared ReadOnly Property YELLOWLIGHT As Color
        Get
            Return Color.FromArgb(_YELLOWLIGHT.A, _YELLOWLIGHT.R, _YELLOWLIGHT.G, _YELLOWLIGHT.B)
        End Get
    End Property

    Public Shared ReadOnly Property YELLOWWHITE As Color
        Get
            Return Color.FromArgb(_YELLOWWHITE.A, _YELLOWWHITE.R, _YELLOWWHITE.G, _YELLOWWHITE.B)
        End Get
    End Property

    Public Shared ReadOnly Property BUTTONFACE As Color
        Get
            Return _BUTTONFACE
        End Get
    End Property

    Public Shared ReadOnly Property RED As Color
        Get
            Return _RED
        End Get
    End Property

    Public Shared ReadOnly Property GREEN As Color
        Get
            Return _GREEN
        End Get
    End Property

    Public Shared ReadOnly Property KOSTALBLUE As Color
        Get
            Return _KOSTALBLUE
        End Get
    End Property

    Public Shared ReadOnly Property BLACK As Color
        Get
            Return _BLACK
        End Get
    End Property

    Public Shared ReadOnly Property GRAY As Color
        Get
            Return _GRAY
        End Get
    End Property

    Public Shared ReadOnly Property WHITE As Color
        Get
            Return _WHITE
        End Get
    End Property

    Public Shared ReadOnly Property YELLOW As Color
        Get
            Return _YELLOW
        End Get
    End Property

    Public Shared ReadOnly Property ORANGE As Color
        Get
            Return _ORANGE
        End Get
    End Property

    Public Shared ReadOnly Property NORMALBLUE As Color
        Get
            Return _NORMALBLUE
        End Get
    End Property

    Public Shared ReadOnly Property LIGHTRED As Color
        Get
            Return _LIGHTRED
        End Get
    End Property

    Public Shared ReadOnly Property LIGHTBLUE As Color
        Get
            Return _LIGHTBLUE
        End Get
    End Property
    Public Shared ReadOnly Property LIGHTGRAY As Color
        Get
            Return _LIGHTGRAY
        End Get
    End Property

End Class
