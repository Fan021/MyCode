Imports System.Windows.Forms

Public Interface IChildrenMainUI
    Inherits IChildrenUI
    ReadOnly Property TabControl_Station As TabControl
    ReadOnly Property UI_Station As Panel

    Sub EnableMainLeftButton()
    Sub DisableMainLeftButton()
End Interface
