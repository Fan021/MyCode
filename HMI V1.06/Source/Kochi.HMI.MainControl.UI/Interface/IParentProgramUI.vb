Imports System.Windows.Forms
Public Interface IParentProgramUI
    Inherits IParentUI
    ReadOnly Property TextBox_Number As HMITextBox
    ReadOnly Property TextBox_Description As HMITextBoxWithButtonAnd2Layer
    ReadOnly Property TextBox_Picture As HMITextBoxWithButtonAnd2Layer
    ReadOnly Property TextBox_Component As HMITextBoxWithButtonAnd2Layer
    ReadOnly Property ComboBox_ActionType As HMIComboBox
    Function ShowButton(ByVal eProgramButtonType As enumProgramButtonType) As Boolean
    Function HiddenButton(ByVal eProgramButtonType As enumProgramButtonType) As Boolean
    Function SetRepeat(ByVal eProgramCounType As enumProgramCounType, Optional ByVal iCnt As Integer = 0) As Boolean
End Interface

Public Enum enumProgramButtonType
    HmiTextBox_ID = 1
    HmiTextBox_Number
    HmiTextBox_Description
    HmiTextBox_Description2
    HmiTextBox_Picture
    HmiTextBox_Component
    HmiTextBox_Repeat
    HmiComboBox_ActionType
    HmiLabel_Detail
End Enum

Public Enum enumProgramCounType
    Manual_Screw_Repeat
    Manual_Continue
    Manual_Insert
End Enum
