Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices

Public Interface ICylinderIO
    ReadOnly Property MainButtonA As Button
    ReadOnly Property MainIndicateA As Panel
    ReadOnly Property MainButtonB As Button
    ReadOnly Property MainIndicateB As Panel
    Property DisableA As Boolean
    Property DisableB As Boolean
    Function RegisterButton(ByVal strTextA As String, ByVal strTextB As String, Optional ByVal strNameA As String = "", Optional ByVal strNameB As String = "") As Boolean
    Function SetIndicateBackColorA(ByVal bValue As Boolean) As Boolean
    Function SetIndicateBackColorB(ByVal bValue As Boolean) As Boolean
    Function SetButtonBackColorA(ByVal bValue As Boolean) As Boolean
    Function SetButtonBackColorB(ByVal bValue As Boolean) As Boolean
End Interface

Public Class CylinderIO
    Implements ICylinderIO
    Public Const GWL_STYLE As Integer = -16
    Public Const WS_DISABLED As Integer = &H8000000

    Private bOldIndicateValueA As Boolean = False
    Private bOldIndicateValueB As Boolean = False
    Private bOldButtonValueA As Boolean = False
    Private bOldButtonValueB As Boolean = False
    Private cDisableA As Boolean = False
    Private cDisableB As Boolean = False

    <DllImport("user32.dll ")>
    Protected Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal wndproc As Integer) As Integer

    End Function
    <DllImport("user32.dll ")>
    Protected Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer

    End Function

    Public Sub SetControlEnabled(ByVal c As Control, ByVal enabled As Boolean)
        SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE))
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Panel_IndicateA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        Panel_IndicateB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Property DisableA As Boolean Implements ICylinderIO.DisableA
        Get

            Return cDisableA
        End Get
        Set(ByVal value As Boolean)
            If cDisableA Then
                SetControlEnabled(Button_NameA, Not cDisableA)
                Button_NameA.ForeColor = Drawing.Color.Black
            End If
            cDisableA = value
        End Set
    End Property

    Public Property DisableB As Boolean Implements ICylinderIO.DisableB
        Get

            Return cDisableB
        End Get
        Set(ByVal value As Boolean)
            If cDisableB Then
                SetControlEnabled(Button_NameB, Not DisableB)
                Button_NameB.ForeColor = Drawing.Color.Black
            End If
            cDisableB = value
        End Set
    End Property
    Public ReadOnly Property MainButtonA As Button Implements ICylinderIO.MainButtonA
        Get
            Return Button_NameA
        End Get
    End Property

    Public ReadOnly Property MainButtonB As Button Implements ICylinderIO.MainButtonB
        Get
            Return Button_NameB
        End Get
    End Property



    Public ReadOnly Property MainIndicateA As Panel Implements ICylinderIO.MainIndicateA
        Get
            Return Panel_IndicateA
        End Get
    End Property

    Public ReadOnly Property MainIndicateB As Panel Implements ICylinderIO.MainIndicateB
        Get
            Return Panel_IndicateB
        End Get
    End Property

    Public Function RegisterButton(ByVal strTextA As String, ByVal strTextB As String, Optional ByVal strNameA As String = "", Optional ByVal strNameB As String = "") As Boolean Implements ICylinderIO.RegisterButton
        Button_NameA.Text = strTextA
        Button_NameA.Name = strNameA
        Button_NameB.Text = strTextB
        Button_NameB.Name = strNameB
    
        Return True
    End Function

    Public Function SetIndicateBackColorA(ByVal bValue As Boolean) As Boolean Implements ICylinderIO.SetIndicateBackColorA
        If bOldIndicateValueA = bValue Then Return True
        If bValue Then
            Panel_IndicateA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_IndicateA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        bOldIndicateValueA = bValue
        Return True
    End Function

    Public Function SetIndicateBackColorB(ByVal bValue As Boolean) As Boolean Implements ICylinderIO.SetIndicateBackColorB
        If bOldIndicateValueB = bValue Then Return True
        If bValue Then
            Panel_IndicateB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_IndicateB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        bOldIndicateValueB = bValue
        Return True
    End Function

    Public Function SetButtonBackColorA(ByVal bValue As Boolean) As Boolean Implements ICylinderIO.SetButtonBackColorA
        If bOldButtonValueA = bValue Then Return True
        If bValue Then
            Button_NameA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Button_NameA.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Button_NameA.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            Button_NameA.BackColor = System.Drawing.Color.White
            Button_NameA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
            Button_NameA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        End If
        bOldButtonValueA = bValue
        Return True
    End Function

    Public Function SetButtonBackColorB(ByVal bValue As Boolean) As Boolean Implements ICylinderIO.SetButtonBackColorB
        If bOldButtonValueB = bValue Then Return True
        If bValue Then
            Button_NameB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Button_NameB.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Button_NameB.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            Button_NameB.BackColor = System.Drawing.Color.White
            Button_NameB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
            Button_NameB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        End If
        bOldButtonValueB = bValue
        Return True
    End Function

    Private Sub Button_NameA_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NameA.MouseEnter
        If cDisableA Then
            Button_NameA.BackColor = Color.White
            Button_NameA.FlatAppearance.MouseOverBackColor = Color.White
            Button_NameA.FlatAppearance.MouseDownBackColor = Color.White
        Else
            If bOldButtonValueA Then
                Button_NameA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameA.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameA.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            End If
        End If
    End Sub

    Private Sub Button_NameB_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NameB.MouseEnter
        If cDisableB Then
            Button_NameB.BackColor = Color.White
            Button_NameB.FlatAppearance.MouseOverBackColor = Color.White
            Button_NameB.FlatAppearance.MouseDownBackColor = Color.White
        Else
            If bOldButtonValueB Then
                Button_NameB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameB.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameB.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            End If
        End If
    End Sub

    Private Sub Button_NameA_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NameA.MouseLeave

        If cDisableA Then
            If bOldButtonValueA Then
                Button_NameA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameA.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameA.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Else
                Button_NameA.BackColor = System.Drawing.Color.White
                Button_NameA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
                Button_NameA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
            End If
        Else
            If bOldButtonValueA Then
                Button_NameA.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Else
                Button_NameA.BackColor = System.Drawing.Color.White
            End If
        End If
    End Sub

    Private Sub Button_NameB_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_NameB.MouseLeave
        If cDisableB Then
            If bOldButtonValueB Then
                Button_NameB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameB.FlatAppearance.MouseOverBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
                Button_NameB.FlatAppearance.MouseDownBackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Else
                Button_NameB.BackColor = System.Drawing.Color.White
                Button_NameB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
                Button_NameB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
            End If
        Else
            If bOldButtonValueB Then
                Button_NameB.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Else
                Button_NameB.BackColor = System.Drawing.Color.White
            End If
        End If
    End Sub

End Class
