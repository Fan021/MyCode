Imports AutomationIntegrationServiceR4_5Client
Public Class Form1
    Private _systemaClient As New AutomationIntegrationServiceR4_5Client
 

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim aa As Object = _systemaClient.State
        Dim startReqECO As New startREQ

        startReqECO.resourceId = "109805"
        startReqECO.sfc = "/P10034369/SNYS016OHI00010"
        startReqECO.operation = "ICT"
        Dim start As New start
        start.startRequest = startReqECO

        Dim startResponse As New startResponse
        startResponse = _systemaClient.start(start)
        Dim startResInfo As New startRSP
        startResInfo = startResponse.return
        Dim returnCode As String = startResInfo.resultCode
        Dim returnTxt As String = startResInfo.resultText


        Dim start1 As New getSfcStatus
        Dim startReqECO1 As New getSfcStatusREQ
        startReqECO1.resourceId = ""
        startReqECO1.sfc = ""
        start1.getSfcStatusRequest = startReqECO1
        Dim startResponse1 As New getSfcStatusResponse
        startResponse1 = _systemaClient.getSfcStatus(start1)
        Dim startResInfo1 As New getSfcStatusRSP
        startResInfo1 = startResponse1.return

        Dim start2 As New checkRecipe
        Dim startReqECO2 As New checkRecipeREQ
        startReqECO2.resourceId = ""
        startReqECO2.recipe = ""
        startReqECO2.recipeVersion = ""
        start2.checkRecipeRequest = startReqECO2
        Dim startResponse2 As New checkRecipeResponse
        startResponse2 = _systemaClient.checkRecipe(start2)
        Dim startResInfo2 As New checkRecipeRSP
        startResInfo2 = startResponse2.return



        Dim start3 As New changeResourceState
        Dim startReqECO3 As New changeResourceStateREQ
        startReqECO3.resourceId = ""
        startReqECO3.newState = ""
        start3.changeResourceStateRequest = startReqECO3
        Dim startResponse3 As New changeResourceStateResponse
        startResponse3 = _systemaClient.changeResourceState(start3)
        Dim startResInfo3 As New genericRSP
        startResInfo3 = startResponse3.return

        Dim start4 As New validateBOM
        Dim startReqECO4 As New validateBOMREQ
        startReqECO4.resourceId = ""
        startReqECO4.operation = ""
        startReqECO4.sfc = ""
        start4.validateBOMRequest = startReqECO4
        Dim startResponse4 As New validateBOMResponse
        startResponse4 = _systemaClient.validateBOM(start4)
        Dim startResInfo4 As New validateBOMRSP
        startResInfo4 = startResponse4.return

        Dim start5 As New validateSfc
        Dim startReqECO5 As New validateSfcREQ
        startReqECO5.resourceId = ""
        startReqECO5.operation = ""
        startReqECO5.barcode = ""
        start5.validateSfcRequest = startReqECO5
        Dim startResponse5 As New validateSfcResponse
        startResponse5 = _systemaClient.validateSfc(start5)
        Dim startResInfo5 As New validateSfcRSP
        startResInfo5 = startResponse5.return


        Dim start6 As New setupComponent
        Dim startReqECO6 As New setupComponentREQ
        startReqECO6.resourceId = ""
        startReqECO6.barcodeHU = ""
        start6.setupComponentRequest = startReqECO6
        Dim startResponse6 As New setupComponentResponse
        startResponse6 = _systemaClient.setupComponent(start6)
        Dim startResInfo6 As New setupComponentRSP
        startResInfo6 = startResponse6.return
    End Sub
End Class
