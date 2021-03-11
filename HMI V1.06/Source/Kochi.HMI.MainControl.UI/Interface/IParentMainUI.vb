Imports System.Windows.Forms
Public Interface IParentMainUI
    Inherits IParentUI
    ReadOnly Property Button_Clean As Button
    ReadOnly Property CurrentPage As String
    Sub EnableMainLeftButton()
    Sub DisableMainLeftButton()
    Sub BackUI()
    Sub ChangePageToVariant()
    Sub AutoLogin()
End Interface
