Imports System.Windows.Forms
Public Interface IChildrenDevicesManagerUI
    Inherits IChildrenUI
    ReadOnly Property TextBox_ID As HMITextBox
    ReadOnly Property TextBox_Name As HMITextBox
    ReadOnly Property ComboBox_Type As HMIComboBox
End Interface
