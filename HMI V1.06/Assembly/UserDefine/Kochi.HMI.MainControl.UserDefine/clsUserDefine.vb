Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device

Public Class clsPrinterFild
    Implements IPrinter
    Public Function GetPrinterFild(ByVal strDeviceName As String, ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cHMILKSN As clsHMILKSN, ByRef lListPrinterCfg As System.Collections.Generic.List(Of Device.clsPrinterCfg), ByRef strErrorMessage As String) As Boolean Implements Device.IPrinter.GetPrinterFild
        Try
            Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
            lListPrinterCfg.Clear()
            lListPrinterCfg.Add(New clsPrinterCfg(2, cVariantManager.CurrentVariantCfg.Variant.Split("-")(0)))
            'Dim mSN As String = "/3OS" + cVariantManager.CurrentVariantCfg.Variant.Split("-")(0) + "/SN" + cHMILKSN.GetSN
            Dim mSN As String = "/3OS" + cVariantManager.CurrentVariantCfg.Variant + "/SN" + cHMILKSN.GetSN
            lListPrinterCfg.Add(New clsPrinterCfg(4, mSN))

            ' Dim cDeviceManager As clsDeviceManager = cSystemElement(clsDeviceManager.Name)
            ' Dim c As clsDeviceCfg = cDeviceManager.GetDeviceFromName("HHH")
            ' Dim cMES As clsHMIMES = c.Source

        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        End Try
        Return True
    End Function

End Class

Public Class clsScanResult
    Implements IScanner
    Private cBarcodeManager As clsBarcodeManager
    Public Function Scanner(ByVal strDeviceName As String, ByVal strActionName As String, ByVal eScannerMethod As String, ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal strScanMsg As String, ByRef strErrorMessage As String) As Boolean Implements Device.IScanner.Scanner
        Try
            ' Dim cDeviceManager As clsDeviceManager = cSystemElement(clsDeviceManager.Name)
            ' Dim c As clsDeviceCfg = cDeviceManager.GetDeviceFromName("HHH")
            ' Dim cMES As clsHMIMES = c.Source

            cBarcodeManager = cLocalElement(clsBarcodeManager.Name)
            strErrorMessage = ""
            If eScannerMethod = "ScanHousingAndWriteAndCheckSN" Then
                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                '  strScanMsg = "/P10326344/SNXT006QFU00400"

                If strScanMsg.Length <> 26 Then
                    strErrorMessage = "Barcode Length is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 3 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                cVariantManager.CurrentVariantCfg.SFC = cValue(2).Substring(2)

                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cValue(1).Substring(1) Then
                    strErrorMessage = "Expected Article:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Article:" + cValue(1).Substring(1)
                    Return False
                End If
                If strErrorMessage <> "" Then
                    Return False
                End If
            ElseIf eScannerMethod = "ScanHousingAndWriteSN" Or eScannerMethod = "ScanHousing" Then
                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                '  strScanMsg = "/P10326344/SNXT006QFU00400"

                If strScanMsg.Length <> 26 Then
                    strErrorMessage = "Barcode Length is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 3 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                cVariantManager.CurrentVariantCfg.SFC = cValue(2).Substring(2)

                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cValue(1).Substring(1) Then
                    strErrorMessage = "Expected Article:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Article:" + cValue(1).Substring(1)
                    Return False
                End If
                If strErrorMessage <> "" Then
                    Return False
                End If

            ElseIf eScannerMethod = "ScanHousingAndCheckSN" Then
                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                ' strScanMsg = "/P10326344/SNXT006QFU00400"

                If strScanMsg.Length <> 26 Then
                    strErrorMessage = "Barcode Length is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 3 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If

                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cValue(1).Substring(1) Then
                    strErrorMessage = "Expected Article:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Article:" + cValue(1).Substring(1)
                    Return False
                End If

                If cVariantManager.CurrentVariantCfg.SFC <> cValue(2).Substring(2) Then
                    strErrorMessage = "Expected SN:" + cVariantManager.CurrentVariantCfg.SFC & vbCrLf + " Scanned SN:" + cValue(2).Substring(2)
                    Return False
                End If

                If strErrorMessage <> "" Then
                    Return False
                End If

            ElseIf eScannerMethod = "ScanChoker" Or eScannerMethod = "ScanHU" Then
                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                ' strScanMsg = "/3OS10326578-04/Q1/HFO-Thermo/V802340/S00308"

                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 6 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If

                If cValue(1).IndexOf("-") < 0 Then
                    cValue(1) = cValue(1) + "-"
                End If
                cBarcodeManager.MaterialNumber = cValue(1).Substring(3).Split("-")(0)
                cBarcodeManager.MaterialVersion = cValue(1).Substring(3).Split("-")(1)
                cBarcodeManager.HandlingUnit = cValue(5).Substring(1)
                cBarcodeManager.Vendor = cValue(4).Substring(1)
                cBarcodeManager.Quantity = cValue(2).Substring(1)
                cBarcodeManager.SFC = ""

                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cBarcodeManager.MaterialNumber Then
                    strErrorMessage = "Expected Component:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Component:" + cBarcodeManager.MaterialNumber
                    Return False
                End If

                If strErrorMessage <> "" Then
                    Return False
                End If
            ElseIf eScannerMethod = "ScanDCPCB_AID" Then
                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cMachineStatusManager As clsMachineStatusManager = cSystemElement(clsMachineStatusManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                Dim cRunnerCfg As clsRunnerCfg = cLocalElement(clsRunnerCfg.Name)
                'strScanMsg = "/P10315796/SNYK006QGB34272"
                If strScanMsg.Length <> 26 Then
                    strErrorMessage = "Barcode Length is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 3 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If

                cBarcodeManager.MaterialNumber = cValue(1).Substring(1)
                cBarcodeManager.MaterialVersion = ""
                cBarcodeManager.HandlingUnit = ""
                cBarcodeManager.Vendor = ""
                cBarcodeManager.Quantity = ""
                cBarcodeManager.SFC = cValue(2).Substring(2)
                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cBarcodeManager.MaterialNumber Then
                    strErrorMessage = "Expected Component:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Component:" + cBarcodeManager.MaterialNumber
                    Return False
                End If

                If strErrorMessage <> "" Then
                    Return False
                End If
                cVariantManager.CurrentVariantCfg.SFC = cBarcodeManager.SFC
                cMachineStatusManager.SetSFC(cRunnerCfg.StationName, cVariantManager.CurrentVariantCfg.SFC)

            Else

                Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
                Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
                'strScanMsg = "/P10315796/SNYK006QGB34272"
                If strScanMsg.Length <> 26 Then
                    strErrorMessage = "Barcode Length is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If
                Dim cValue() As String = strScanMsg.Split("/")
                If cValue.Length <> 3 Then
                    strErrorMessage = "Barcode Format is Failure. Scanned Message:" + strScanMsg
                    Return False
                End If

                cBarcodeManager.MaterialNumber = cValue(1).Substring(1)
                cBarcodeManager.MaterialVersion = ""
                cBarcodeManager.HandlingUnit = ""
                cBarcodeManager.Vendor = ""
                cBarcodeManager.Quantity = ""
                cBarcodeManager.SFC = cValue(2).Substring(2)

                If cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) <> cBarcodeManager.MaterialNumber Then
                    strErrorMessage = "Expected Component:" + cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement) & vbCrLf + " Scanned Component:" + cBarcodeManager.MaterialNumber
                    Return False
                End If

                If strErrorMessage <> "" Then
                    Return False
                End If

            End If
            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        End Try

        Return True
    End Function
    Private Function CheckSN(ByVal strSN As String) As String
        Dim strErrorMessage As String = String.Empty
        '=======================检查数据库============================================
        If SerialNoTracer.SerialNoManager.SM_SetParameters("127.0.0.1", "OBC", "root", "apb34eol", "OBC", 3306) <> 1 Then
            strErrorMessage = "SerialNoTracer.SerialNoManager.SM_SetParameters Fail"
            Return strErrorMessage
        End If

        If SerialNoTracer.SerialNoManager.SM_CheckDatabase() <> 1 Then
            strErrorMessage = "SerialNoTracer.SerialNoManager.SM_CheckDatabase Fail"
            Return strErrorMessage
        End If

        If SerialNoTracer.SerialNoManager.SM_IsLabelSerialNoExist(strSN) <> 0 Then
            strErrorMessage = "SN:" + strSN + " has existed"
            Return strErrorMessage
        End If

        If SerialNoTracer.SerialNoManager.SM_SaveSerialNo(strSN, "") <> 1 Then
            strErrorMessage = "SerialNoTracer.SerialNoManager.SM_SaveSerialNo Fail"
            Return strErrorMessage
        End If
        Return strErrorMessage
    End Function
End Class
