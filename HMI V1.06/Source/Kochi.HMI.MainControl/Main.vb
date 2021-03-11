Imports System.Threading
Imports System.Windows.Forms
Imports System.Reflection
Imports Kochi.HMI.MainControl.Base
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Runner
Imports System.IO

<Assembly: AssemblyVersion("1.0.0.1")> 


Module Main
    Private WithEvents mMainForm As MainForm
    Private cSystemElement As New Dictionary(Of String, Object)
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cRunnerElement As New Dictionary(Of String, Object)
    Private cSystemManager As clsSystemManager
    Private cDeviceManager As clsDeviceManager
    Private cDeviceLibManager As clsDeviceLibManager
    Private cActionLibManager As clsActionLibManager
    Private cStatisticsLibManager As clsStatisticsLibManager
    Private cUserManager As clsUserManager
    Private cMachineManager As clsMachineManager
    Private cGlobalProgramManager As clsGlobalProgramManager
    Private cVariantManager As clsVariantManager
    Private cTextManager As clsTextManager
    Private cPictureManager As clsPictureManager
    Private cErrorCodeManager As clsErrorCodeManager
    Private cPlcMessageManager As clsPlcMessageManager
    Private cLogHandler As clsLogHandler
    Private cIniHandler As clsIniHandler
    Private cMainButtonManager As clsMainButtonManager
    Private cFormFontResize As clsFormFontResize
    Private cActionManager As clsActionManager
    Private cLanguageManager As clsLanguageManager
    Private mMainFormIsClosing As Boolean
    Private sw As New Stopwatch
    Private cExceptionForm As ExceptionForm
    Private cLoadScreenForm As LoadScreenForm
    Private cMainButtonRunner As clsMainButtonRunner
    Private cMachineDataRunner As clsMachineDataRunner
    Private cErrorAndMessageRunner As clsErrorAndMessageRunner
    Private cStationErrorCodeManager As clsStationErrorCodeManager
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cMainRunner As clsMainRunner
    Private glExitApp As Boolean = False
    Private CycleCounter As Integer
    Private cProcessStart As clsProcessStart

    Public Sub Main()
        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
            AddHandler Application.ThreadException, AddressOf Application_ThreadException
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException



            cLoadScreenForm = New LoadScreenForm
            cLoadScreenForm.Show()

            ProcessControl.IsMyProcessRunning()
            cSystemManager = New clsSystemManager
            cSystemManager.Init()
            cSystemElement.Add(clsSystemManager.Name, cSystemManager)


            cLogHandler = New clsLogHandler
            cLogHandler.Init(cSystemElement)
            cSystemElement.Add(clsLogHandler.Name, cLogHandler)

            cIniHandler = New clsIniHandler
            cSystemElement.Add(clsIniHandler.Name, cIniHandler)

            cLanguageManager = New clsLanguageManager
            cLanguageManager.Init(cSystemElement)
            cSystemElement.Add(clsLanguageManager.Name, cLanguageManager)

            cActionLibManager = New clsActionLibManager
            cActionLibManager.Init(cSystemElement)
            cSystemElement.Add(clsActionLibManager.Name, cActionLibManager)

            cMachineManager = New clsMachineManager
            cMachineManager.Init(cSystemElement)
            cMachineManager.LoadManagerData()
            cSystemElement.Add(clsMachineManager.Name, cMachineManager)


            cDeviceLibManager = New clsDeviceLibManager
            cDeviceLibManager.Init(cSystemElement)
            cSystemElement.Add(clsDeviceLibManager.Name, cDeviceLibManager)

            cErrorCodeManager = New clsErrorCodeManager
            cErrorCodeManager.Init(cSystemElement)
            cErrorCodeManager.LoadErrorCodeCfg()
            cSystemElement.Add(clsErrorCodeManager.Name, cErrorCodeManager)

        
            cDeviceManager = New clsDeviceManager
            cDeviceManager.Init(cSystemElement)
            cDeviceManager.LoadDeviceCfg()
            cSystemElement.Add(clsDeviceManager.Name, cDeviceManager)

            cVariantManager = New clsVariantManager
            cVariantManager.Init(cSystemElement)
            cVariantManager.LoadVariantCfg()
            cSystemElement.Add(clsVariantManager.Name, cVariantManager)
            cMachineManager.LoadDeviceData()
            cLanguageManager.LoadActiveLanguage()


            cStatisticsLibManager = New clsStatisticsLibManager
            cStatisticsLibManager.Init(cSystemElement)
            cSystemElement.Add(clsStatisticsLibManager.Name, cStatisticsLibManager)

            

            cUserManager = New clsUserManager
            cUserManager.Init(cSystemElement)
            cUserManager.LoadUserCfg()
            cSystemElement.Add(clsUserManager.Name, cUserManager)

            cGlobalProgramManager = New clsGlobalProgramManager
            cGlobalProgramManager.Init(cSystemElement)
            cGlobalProgramManager.LoadGlobalProgramCfg()
            cSystemElement.Add(clsGlobalProgramManager.Name, cGlobalProgramManager)

  

            cTextManager = New clsTextManager
            cTextManager.Init(cSystemElement)
            cTextManager.LoadTextCfg()
            cSystemElement.Add(clsTextManager.Name, cTextManager)

   

            cPlcMessageManager = New clsPlcMessageManager
            cPlcMessageManager.Init(cSystemElement)
            cPlcMessageManager.LoadPLCMessageCfg()
            cSystemElement.Add(clsPlcMessageManager.Name, cPlcMessageManager)

            cPictureManager = New clsPictureManager
            cPictureManager.Init(cSystemElement)
            cPictureManager.LoadPictureCfg()
            cSystemElement.Add(clsPictureManager.Name, cPictureManager)

            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cSystemElement.Add(clsActionManager.Name, cActionManager)

            cStationErrorCodeManager = New clsStationErrorCodeManager
            cStationErrorCodeManager.Init(cSystemElement)
            cSystemElement.Add(clsStationErrorCodeManager.Name, cStationErrorCodeManager)

            cProgramButton = New clsProgramButton
            cProgramButton.Init(cSystemElement)
            cSystemElement.Add(clsProgramButton.Name, cProgramButton)

            cProgramCylinderButton = New clsProgramCylinderButton
            cProgramCylinderButton.Init(cSystemElement)
            cSystemElement.Add(clsProgramCylinderButton.Name, cProgramCylinderButton)

            cLoadScreenForm.Dispose()

            mMainForm = New MainForm
            mMainForm.Init(cSystemElement)
            mMainForm.Show()
            mMainForm.Refresh()
            mMainForm.Update()

            cMainButtonRunner = New clsMainButtonRunner
            cMainButtonRunner.Init(cSystemElement, cRunnerElement)
            cRunnerElement.Add(clsMainButtonRunner.Name, cMainButtonRunner)
            cSystemElement.Add(clsMainButtonRunner.Name, cMainButtonRunner)


            cMachineDataRunner = New clsMachineDataRunner
            cMachineDataRunner.Init(cSystemElement, cRunnerElement)
            cRunnerElement.Add(clsMachineDataRunner.Name, cMachineDataRunner)
            cSystemElement.Add(clsMachineDataRunner.Name, cMachineDataRunner)

            cErrorAndMessageRunner = New clsErrorAndMessageRunner
            cErrorAndMessageRunner.Init(cSystemElement, cRunnerElement)
            cRunnerElement.Add(clsErrorAndMessageRunner.Name, cErrorAndMessageRunner)
            cSystemElement.Add(clsErrorAndMessageRunner.Name, cErrorAndMessageRunner)

            cMainRunner = New clsMainRunner
            cMainRunner.Init(cSystemElement, cRunnerElement)
            cRunnerElement.Add(clsMainRunner.Name, cMainRunner)
            sw.Start()

            Do
                Application.DoEvents()

                If mMainFormIsClosing Then
                    glExitApp = True
                    mMainForm.Quit()
                    cMainRunner.Quit()
                    glExitApp = True
                End If
                If glExitApp Then
                    mMainForm.Quit()
                    cMainRunner.Quit()
                    Exit Do
                End If

                If CycleCounter = 500 Then
                    sw.Stop()
                    Dim swTime As Double = sw.ElapsedMilliseconds
                    Dim Cycle As Double = CType(swTime, Double) / CType(CycleCounter, Double)
                    mMainForm.InvokeAction(Sub()
                                               mMainForm.SetCycleTime(Cycle)
                                           End Sub)

                    sw.Reset()
                    sw.Start()
                    CycleCounter = 0
                End If
                CycleCounter = CycleCounter + 1
                cMainRunner.Run()
                System.Threading.Thread.Sleep(mMainForm.RunTime)
            Loop
            System.Threading.Thread.Sleep(50)
            cProcessStart = New clsProcessStart
            cProcessStart.Start(cSystemManager.Settings.ExeFolder, "KillProcess.bat", False)
            System.Threading.Thread.Sleep(50)
            Application.Exit()
            Application.ExitThread()
            System.Environment.Exit(0)
        Catch ex As Exception
            If glExitApp Then Return
            cExceptionForm = New ExceptionForm
            cExceptionForm.TextBox_Msg.Text = ex.Message
            cExceptionForm.TextBox_Stack.Text = ex.ToString
            cExceptionForm.ShowDialog()
            Application.Exit()
            Application.ExitThread()
            System.Environment.Exit(0)
        End Try
    End Sub

    Private Sub Application_ThreadException(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
        If glExitApp Then Return
        cExceptionForm = New ExceptionForm
        cExceptionForm.TextBox_Msg.Text = e.Exception.Message
        cExceptionForm.TextBox_Stack.Text = e.Exception.ToString
        cExceptionForm.ShowDialog()
        glExitApp = True
    End Sub

    Private Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        If glExitApp Then Return
        cExceptionForm = New ExceptionForm
        cExceptionForm.TextBox_Msg.Text = e.ExceptionObject.Message
        cExceptionForm.TextBox_Stack.Text = e.ExceptionObject.ToString
        cExceptionForm.ShowDialog()
        glExitApp = True
    End Sub

    Private Sub CurrentDomain_ProcessExit(ByVal sender As Object, ByVal e As EventArgs)
        glExitApp = True
    End Sub

    Private Sub mMainForm_IamClosing() Handles mMainForm.IamClosing
        mMainFormIsClosing = True
    End Sub
End Module
