CoDeSys+o                     @        @   2.3.9.50    @/    @                             55^ +    @       wРш"             ЇU        z  @   q   C:\TWINCAT\PLC\LIB\COMlibV2.lib @                                                                                          _RECEIVESTRING     
      RxString                §џ              state            §џ              c            §џ              l            §џ              l2            §џ              pl            §џ              sl            §џ              iTimeout            §џ              Receive                  ReceiveByte    §џ              TimeoutTimer                    TOF    §џ                 Prefix    Q       Q    §џ              Suffix    Q       Q    §џ              Timeout           §џ              Reset            §џ              pReceivedString                 §џ       #    Pointer to variable length string    SizeReceivedString           §џ           variable string size       StringReceived            §џ              Busy            §џ              Error            
   ComError_t   §џ           	   RxTimeout            §џ              ErrorCountPrefix           §џ       +    invalid characters received before prefix    ReceivedPrefix    Q       Q    §џ       .    received prefix including invalid characters       RXbuffer                   	   COMbuffer  §џ                   /u\      џџџџ           _SENDSTRING           pos            §џ              Send                  SendByte    §џ              c            §џ              ls            §џ              
   SendString               §џ                 Busy            §џ              Error            
   ComError_t   §џ                 TXbuffer                   	   COMbuffer  §џ
                   /u\      џџџџ           _STRNCPY           d            §џ              i            §џ	                 pTargetString                 §џ              pSourceString                 §џ              MaxSize           §џ                 _strncpy                                      /u\      џџџџ           ASC           pChar                  §џ                 str    Q       Q    §џ                 ASC                                     /u\      џџџџ           CHR           str1                §џ              pChar                  §џ                 c           §џ                 CHR    Q       Q                              /u\      џџџџ           CLEARCOMBUFFER                       Buffer                   	   COMbuffer  §џ                   /u\      џџџџ           COMRESET     	      TIME_OUT_VALUE    d      §џ              RESETMASK_STANDARD          §џ              RESETMASK_ALTERNATIVE          §џ              Timeout                    TON    §џ              state            §џ              pCTRL                  §џ              pSTATUS                  §џ           	   ResetMask            §џ              TriggerExecute                 R_TRIG    §џ                 Execute            §џ	              pComIn    	  A                            §џ
       .    pointer to any KL6 or COM-Port process image    pComOut    	  A                            §џ       .    pointer to any KL6 or COM-Port process image 	   SizeComIn           §џ                 Done            §џ              Busy            §џ              Error            §џ              ErrorID            
   ComError_t   §џ                       /u\      џџџџ           GET_COMLIB_VERSION               bGet            §џ                 Get_ComLib_Version                                         /u\      џџџџ           KL6CONFIGURATION           TIME_OUT_VALUE    d      §џ              RESETMASK_STANDARD          §џ              RESETMASK_ALTERNATIVE          §џ              REGISTERACCESSMASK          §џ           2011-09-12 KSt    state            §џ              TriggerExecute                 R_TRIG    §џ              R32            §џ              R33            §џ              R34            §џ               CheckOK             §џ!              RegisterList   	  #                   ComRegisterData_t            §џ"              WriteRegisterList                                      KL6WriteRegisters    §џ#              Timeout                    TON    §џ$              pCTRL                  §џ%              pSTATUS                  §џ&           	   ResetMask            §џ'                 Execute            §џ              Mode               ComSerialLineMode_t   §џ              Baudrate           §џ       ?   	115200, 57600, 38400, 19200, 9600, 4800, 2400, 1200, 600, 300 
   NoDatabits           §џ          	7 or 8    Parity               ComParity_t   §џ       ,   	PARITY_NONE=0, PARITY_EVEN=1, PARITY_ODD=2    Stopbits           §џ          	1 or 2 	   Handshake               ComHandshake_t   §џ	       ;   	HANDSHAKE_NONE=0, HANDSHAKE_RTSCTS=1, HANDSHAKE_XONXOFF=2    ContinousMode            §џ
       ;    don't start transmission before transmit buffer is filled    pComIn    	  A                            §џ       &    for universal register communication    pComOut    	  A                            §џ       &    for universal register communication 	   SizeComIn           §џ                 Done            §џ              Busy            §џ              Error            §џ              ErrorId            
   ComError_t   §џ                       /u\      џџџџ           KL6READREGISTERS           TIME_OUT_VALUE    d      §џ              REGISTERNUMBERMASK    ?      §џ               REGISTERREADMASK          §џ!              state            §џ$              TriggerExecute                 R_TRIG    §џ%              NumRegisters            §џ&              n            §џ'              Register            §џ(              Value            §џ)              Timeout                    TON    §џ*              pCTRL                  §џ+              pSTATUS                  §џ,          	NumRegistersInList: INT;   i            §џ.              SaveCTRL            §џ/           	      Execute            §џ              FirstRegister           §џ              RegisterCount           §џ              Mode               ComSerialLineMode_t   §џ              pComIn    	  A                            §џ       &    for universal register communication    pComOut    	  A                            §џ       &    for universal register communication 	   SizeComIn           §џ              pRegisterList    	  ?                    ComRegisterData_t                §џ              SizeRegisterList           §џ                 Done            §џ              Busy            §џ              Error            §џ              ErrorId            
   ComError_t   §џ                       /u\      џџџџ           KL6WRITEREGISTERS           TIME_OUT_VALUE    d      §џ              REGISTERNUMBERMASK    ?      §џ              REGISTERWRITEMASK    Р      §џ              state            §џ"              TriggerExecute                 R_TRIG    §џ#              NumRegisters            §џ$              n            §џ%              Register            §џ&              Value            §џ'              Timeout                    TON    §џ(              pCTRL                  §џ)              pSTATUS                  §џ*              SaveCTRL            §џ+                 Execute            §џ              Mode               ComSerialLineMode_t   §џ              pComIn    	  A                            §џ       &    for universal register communication    pComOut    	  A                            §џ       &    for universal register communication 	   SizeComIn           §џ              pRegisterList    	  ?                    ComRegisterData_t                §џ              SizeRegisterList           §џ                 Done            §џ              Busy            §џ              Error            §џ              ErrorId            
   ComError_t   §џ                       /u\      џџџџ           M8000CONFIGURATION           state            §џ              TriggerExecute                 R_TRIG    §џ              TimerTimeout                    TON    §џ                 Execute            §џ              Channel           §џ           	1..3    Baudrate           §џ       5   	115200, 57600, 38400, 19200, 9600, 4800, 2400, 1200 
   NoDatabits           §џ          	7 or 8    Parity               ComParity_t   §џ       ,   	PARITY_NONE=0, PARITY_EVEN=1, PARITY_ODD=2    Stopbits           §џ          	1 or 2 	   Handshake               ComHandshake_t   §џ	       ;   	HANDSHAKE_NONE=0, HANDSHAKE_RTSCTS=1, HANDSHAKE_XONXOFF=2       Done            §џ              Busy            §џ              Error            §џ              ErrorId            
   ComError_t   §џ                 ComIn                M8000inData  §џ       &    for universal register communication    ComOut                M8000outData  §џ       &    for universal register communication         /u\      џџџџ           RECEIVEBYTE                   ByteReceived            §џ              ReceivedByte           §џ              Error            
   ComError_t   §џ                 RxBuffer                   	   COMbuffer  §џ                   /u\      џџџџ           RECEIVEDATA           state            §џ              c            §џ              l            §џ              iTimeout            §џ              Receive                  ReceiveByte    §џ              TimeoutTimer                    TOF    §џ           	   ptrPrefix                  §џ              pData                  §џ              p1                  §џ              p2                  §џ               isEqual             §џ!              i            §џ"              pw            §џ#                 pPrefix                 §џ           	   LenPrefix           §џ              pSuffix                 §џ           	   LenSuffix           §џ              pReceiveData                 §џ              SizeReceiveData           §џ              Timeout           §џ	              Reset            §џ
                 DataReceived            §џ              busy            §џ              Error            
   ComError_t   §џ           	   RxTimeout            §џ              LenReceiveData           §џ                 RXbuffer                   	   COMbuffer  §џ                   /u\      џџџџ           RECEIVESTRING           ReceiveStringStandard                                     _ReceiveString    §џ              ErrorCountPrefix            §џ       +    invalid characters received before prefix    ReceivedPrefix    Q       Q     §џ       .    received prefix including invalid characters       Prefix    Q       Q    §џ              Suffix    Q       Q    §џ              Timeout           §џ              Reset            §џ                 StringReceived            §џ	              Busy            §џ
              Error            
   ComError_t   §џ           	   RxTimeout            §џ                 ReceivedString     Q       Q   §џ              RXbuffer                   	   COMbuffer  §џ                   /u\      џџџџ           RECEIVESTRING255           ReceiveStringStandard                                     _ReceiveString    §џ              ErrorCountPrefix            §џ       +    invalid characters received before prefix    ReceivedPrefix    Q       Q     §џ       .    received prefix including invalid characters       Prefix    Q       Q    §џ              Suffix    Q       Q    §џ              Timeout           §џ              Reset            §џ                 StringReceived            §џ	              Busy            §џ
              Error            
   ComError_t   §џ           	   RxTimeout            §џ                 ReceivedString               §џ              RXbuffer                   	   COMbuffer  §џ                   /u\      џџџџ           SENDBYTE               SendByte           §џ                 Busy            §џ              Error            
   ComError_t   §џ                 TxBuffer                   	   COMbuffer  §џ                   /u\      џџџџ           SENDDATA           pos            §џ              Send                  SendByte    §џ              c            §џ              dp                  §џ              dpw            §џ              	   pSendData                 §џ              Length           §џ                 Busy            §џ              Error            
   ComError_t   §џ                 TXbuffer                   	   COMbuffer  §џ                   /u\      џџџџ        
   SENDSTRING           SendStringStandard                      _SendString    §џ              
   SendString    Q       Q    §џ                 Busy            §џ              Error            
   ComError_t   §џ                 TXbuffer                   	   COMbuffer  §џ
                   /u\      џџџџ           SENDSTRING255           SendStringStandard                      _SendString    §џ              
   SendString               §џ                 Busy            §џ              Error            
   ComError_t   §џ                 TXbuffer                   	   COMbuffer  §џ
                   /u\      џџџџ           SERIALLINECONTROL           ComPortDebugBuffer                 ComDebugBuffer    §џ              InvalidDataExchangeMode            §џ               RegisterMode            §џ!              pCTRL                  §џ"              pSTATUS                  §џ#              RxCount            §џ$              TxCount            §џ%           	   DataIndex            §џ&              DataCountMask            §џ'              DataCountShift            §џ(              ReceiveHandshakeBit            §џ)              TransmitHandshakeBit            §џ*              TransmitBufferSentBit            §џ+              ContinousModeStartBit            §џ,              ReceiveBufferFullBit            §џ-              ResetBit            §џ.              RR            §џ/              RA            §џ0              TR            §џ1              TA            §џ2              IA            §џ3              BUF_F            §џ4              noTAcounter            §џ5              initialized             §џ6              TransmitDataSent             §џ7              i            §џ8                 Mode               ComSerialLineMode_t   §џ              pComIn    	  A                            §џ       A    must meet the maximum size of a hardware related data structure    pComOut    	  A                            §џ       A    must meet the maximum size of a hardware related data structure 	   SizeComIn           §џ                 Error            §џ              ErrorID            
   ComError_t   §џ                 TxBuffer                   	   COMbuffer  §џ              RxBuffer                   	   COMbuffer  §џ                   /u\      џџџџ           SERIALLINECONTROLADS     8   	   NetId_Act       
    'invalid'    
   T_AmsNetId ` §џ       4    initialized with invalid NetId to force INIT state    Timeout_Act       ` §џ           Timeout for ADS calls    SerialCfg_Act           (Baudrate:=0,Parity:=-1,RTS:=-1)                PARITY_NONE       ComParity_t                  RTS_CTRL_HANDSHAKE       ComRTSCtrl_t                 ComSerialConfig ` §џ       X    internal used config struct - invalid initial values to ensure copying of input struct    AdsSerialCfg1                           ComADSSerialConfig_1 ` §џ        1    config struct which is transfered to Ads server    pAdsSerialCfg         ` §џ!              cbAdsSerialCfg         ` §џ"              SrvName             ` §џ#              SrvVer         ` §џ$           
   SrvVer_Act         ` §џ%              State         ` §џ'           	   StateLast         ` §џ(       j    Only set one of the following triggers  on 'TRUE' at the same time! Wait until 'bBusy' is 'FALSE' again.    ExeOpenPort          ` §џ+       F    Set this value to 'TRUE' to perform the "Open serial port"-operation    ExeCheckPort          ` §џ,       N    Set this value to 'TRUE' to perform the "Check if  port is opened"-operation    ExeWriteData          ` §џ-       H    Set this value to 'TRUE' to perform the "Write data to port"-operation    ExeCheckForData          ` §џ.       \    Set this value to 'TRUE' to perform the "Check if serial port has buffered data"-operation    ExeReadData          ` §џ/       J    Set this value to 'TRUE' to perform the "Read out serial port"-operation    ExeClosePort          ` §џ0       G    Set this value to 'TRUE' to perform the "Close serial port"-operation    ExeCheckVersion          ` §џ1           
   pWriteData         ` §џ3       )    Pointer to Data to write on serial port    cbWriteData         ` §џ4       (    Length of data to write on serial port 	   pReadData         ` §џ5       4    Pointer to Buffer for the read-data of serial port 
   cbReadData         ` §џ6       3    Length of buffer for the read-data of serial port    cbDataWaiting         ` §џ7       D    Indicates the number of bytes waiting in the buffer of serial port    AdsBusy          ` §џ9       A    This output remains TRUE until the block has executed a command    AdsError          ` §џ:           'TRUE' if an error occured 
   AdsErrorID         ` §џ;       '    Displays the error code; 0 = no error    AdsCount         ` §џ<       B    Displays the number of bytes read out by the ADSREADEX-functions    AdsWR_OpenPort                          ADSWRITE ` §џ?              AdsWR_WriteData                          ADSWRITE ` §џ@              AdsWR_ClosePort                          ADSWRITE ` §џA              AdsRD_CheckPort                        	   ADSREADEX ` §џB              AdsRD_CheckForData                        	   ADSREADEX ` §џC              AdsRD_ReadData                        	   ADSREADEX ` §џD              AdsRD_CheckVersion        
                ADSRDDEVINFO ` §џE           internal buffers (FiFo)    RxBufferInt   	  Я                    ` §џH              cbRxBufferInt         ` §џI       ;    number of bytes of valid data which need to be transfered    TxBufferInt   	  Я                    ` §џJ              cbTxBufferInt         ` §џK       ;    number of bytes of valid data which need to be transfered    cbTxCopyData         ` §џM              cbRxCopyData         ` §џN              iTemp         ` §џP              timerDelayAfterErr          (PT:=T#100ms)       d        TON ` §џR              BlockedAfterErr          ` §џS              ErrInit         ` §џU       I    for usage of error code constants to avoid warnings of unsued variables    BUFFERSIZE_M1    Я  @` §џY       [    -> internal buffer size 2000 bytes; buffer of server has to be bigger than buffer in Plc! 
   SERVERPORT    jV   ` §џ\       #    Port of the 'AdsSerialCommServer' 	   MINSRVVER      ` §џ]       ]    v2.1.13; minimum required server version; previous versions were beta and are not supported 
   STATE_WAIT        @` §џ_              STATE_OPENPORT        ` §џ`              STATE_CHECKPORT        ` §џa              STATE_CLOSEPORT        ` §џb              STATE_CHECKFORDATA        ` §џc              STATE_READDATA        ` §џd              STATE_WRITEDATA        ` §џe           
   STATE_INIT        ` §џf              WIN32_ERR_PREFIX      	= ` §џh                 Connect            §џ
       9    connect to serial port [TRUE=connect, FALSE=disconnect] 	   SerialCfg                           ComSerialConfig   §џ              NetId          ''    
   T_AmsNetId   §џ           host NetId    Timeout         §џ           Timeout for ADS calls    
   PortOpened            §џ       -    Indicates if selected serial port is opened    Error            §џ           'TRUE' if an error occurred    ErrorID           §џ       '    Displays the error code; 0 = no error    Busy            §џ       .    'TRUE' if internal ADS communication is busy 
   TxBufCount           §џ       '    number of bytes in internal Tx buffer 
   RxBufCount           §џ       '    number of bytes in internal Rx buffer       TxBuffer                   	   ComBuffer  §џ           serial Tx ComBuffer    RxBuffer                   	   ComBuffer  §џ           serial Rx ComBuffer         /u\     џџџџ           SERIALLINECONTROLM8000     
      ComPortDebugBuffer                 ComDebugBuffer    §џ              InvalidDataExchangeMode            §џ              CommandMode            §џ              pCTRL                  §џ              pSTATUS                  §џ              TransmitBufferFullBit            §џ              TransmitDataValidBit            §џ              ReceiveDataValidBit            §џ           	noTAcounter: BYTE;   initialized             §џ"          	i: INT;   TransmitBufferSentBit            §џ$                 Channel           §џ              pComIn    	  A                            §џ       A    must meet the maximum size of a hardware related data structure    pComOut    	  A                            §џ       A    must meet the maximum size of a hardware related data structure 	   SizeComIn           §џ                 Error            §џ              ErrorID            
   ComError_t   §џ                 TxBuffer                   	   COMbuffer  §џ              RxBuffer                   	   COMbuffer  §џ                   /u\      џџџџ    l   C:\TWINCAT\PLC\LIB\HMI.lib @                                                                                	          CENTER20           sEmpty                                  §џ
                 InString               §џ                 Center20                                         Ы3[      џџџџ           CX_DISPLAY_INTERN           WriteToInternDisplay                                               FB_CXSetTextDisplay    §џ           	   CxNaviKey               E_CX1100_NaviSwitch    §џ              TextTickerLine_1                      TextTicker16_FB    §џ              TextTickerLine_2                      TextTicker16_FB    §џ              SendStringRow_1                §џ              SendStringRow_2                §џ              OldStringRow_1                §џ              OldStringRow_2                §џ              SQNO            §џ              SINO            §џ              Trigger_Plus                 R_TRIG    §џ              Trigger_Minus                 R_TRIG    §џ           
   Trigger_Up                 R_TRIG    §џ               Trigger_Down                 R_TRIG    §џ!              Trigger_Press                 R_TRIG    §џ"              Trigger_TopRight                 R_TRIG    §џ#              OldInDisplay                 structDisplayData    §џ%           
   MAX_BUFFER    @      §џ)              LINE_LENGHT          §џ*              EMPTY_STRING                              §џ+                 Device_ID_CX           §џ              Cx_IN_NaviSwitch          §џ              Reset            §џ              DisableKeyboard            §џ              Display                 structDisplayData   §џ              BannerStartTime           §џ           
   TickerTime           §џ	                 Keys                   structKeyBoardData   §џ                       Ы3[     џџџџ           ESA_VT50_SERIELL_FB           SendStr                
   SendString    §џ           WriteString    SendByte                  SendByte    §џ              ReceiveByte                  ReceiveByte    §џ              SendStringRow_1                §џ!              SendStringRow_2                §џ"              OldStringRow_1                §џ$              OldStringRow_2                §џ%              PagePrinted             §џ'              myTicker                      TextTicker20_FB    §џ)              myTickerLine2                      TextTicker20_FB    §џ*              OldLED             §џ,              RefreshTimer                    TON    §џ-              OldInDisplay                 structDisplayData    §џ/           
   MAX_BUFFER    @      §џ3              LINE_LENGHT          §џ4              EMPTY_STRING                                  §џ5                 Reset            §џ       (    Rising edge set Pilz into Monitor mode    DisableKeyboard            §џ              Display                 structDisplayData   §џ              BannerStartTime           §џ           
   TickerTime           §џ                 Keys                   structKeyBoardData   §џ              sINTERFACE_BAUDRATE          19200    §џ              sINTERFACE_DATABITS          8    §џ              sINTERFACE_PARITY          none    §џ              sINTERFACE_STOPPBIT          1    §џ                 RxBuffer                   	   ComBuffer  §џ              TxBuffer                   	   ComBuffer  §џ                   Ы3[      џџџџ           LASTKEYSOFF               pLastKey                                    structKeyBoard        §џ                 LastKeysOff                                      Ы3[      џџџџ           PILZ_PX30_SERIELL_FB           SendStr                
   SendString    §џ           WriteString    SendByte                  SendByte    §џ              ReceiveByte                  ReceiveByte    §џ               StartMonitormode                 R_TRIG    §џ!              InternSetMonitorMode             §џ#              SendStringRow_1                §џ%              SendStringRow_2                §џ&              OldStringRow_1                §џ(              OldStringRow_2                §џ)              TimeOut                    TON    §џ+              MonitorModeSet             §џ,              PagePrinted             §џ-              myTicker                      TextTicker20_FB    §џ/              myTickerLine2                      TextTicker20_FB    §џ0              OldLED             §џ1              RefreshTimer                    TON    §џ2              OldInDisplay                 structDisplayData    §џ4           
   MAX_BUFFER    @      §џ8              LINE_LENGHT          §џ9              EMPTY_STRING                                  §џ:                 Reset            §џ       (    Rising edge set Pilz into Monitor mode    DisableKeyboard            §џ              Display                 structDisplayData   §џ              BannerStartTime           §џ           
   TickerTime           §џ                 Keys                   structKeyBoardData   §џ              AckMonitorModeActiv            §џ              sINTERFACE_BAUDRATE          9600    §џ              sINTERFACE_DATABITS          8    §џ              sINTERFACE_PARITY          none    §џ              sINTERFACE_STOPPBIT          1    §џ                 RxBuffer                   	   ComBuffer  §џ              TxBuffer                   	   ComBuffer  §џ                   Ы3[      џџџџ           SERIALLOGGER_FB        
   mLogString                §џ              SendStr                
   SendString    §џ              OldLogString                §џ           
   OldLogData                 structDisplayData    §џ              sDateAndTimeForLog    Q       Q     §џ              sMonthAndYearForLog    Q       Q     §џ              GETSYSTEMTIME                 GETSYSTEMTIME    §џ              FileTime             
   T_FILETIME    §џ              LocalDateAndTime                   
   TIMESTRUCT    §џ              rDay             §џ               rMonth             §џ!              rYear             §џ"              rHour             §џ#              rMinute             §џ$              rSecond             §џ%           
   FormatDate                                     FB_FormatString    §џ&                 Enable           §џ       	    Default    LogData                 structDisplayData   §џ           	   LogString               §џ       	    Default       Ready            §џ                 RxBuffer                   	   ComBuffer  §џ              TxBuffer                   	   ComBuffer  §џ                   Ы3[      џџџџ           SMALLKEYBOARD_FB           Key_On            §џ              Key_Step            §џ           
   Key_Escape            §џ           	   Key_Minus            §џ           
   Trigger_On                 R_TRIG    §џ              Trigger_Step                 R_TRIG    §џ              Trigger_Escape                 R_TRIG    §џ              Trigger_Minus                 R_TRIG    §џ                 Reset            §џ       (    Rising edge set Pilz into Monitor mode    DisableKeyboard            §џ              Display                 structDisplayData   §џ                 Keys                   structKeyBoardData   §џ	                       Ы3[     џџџџ           TEXTTICKER16_FB           Timer                    TON    §џ              BannerStartTimer                    TON    §џ           
   TempString                §џ              
   LongString               §џ           
   TickerTime           §џ              BannerStartTime           §џ              Reset            §џ                 TextTicker16               §џ                       Ы3[      џџџџ           TEXTTICKER20_FB           Timer                    TON    §џ              BannerStartTimer                    TON    §џ           
   TempString                §џ              
   LongString               §џ           
   TickerTime           §џ              BannerStartTime           §џ              Reset            §џ                 TextTicker20               §џ                       Ы3[      џџџџ    s   C:\TWINCAT\PLC\LIB\TcSystemCX.lib @                                                                                          F_CXNAVISWITCH            
   iCX1100_IN           §џ                 F_CXNaviSwitch               E_CX1100_NaviSwitch                             ОЯW      џџџџ           F_CXNAVISWITCHUSB            
   iCX2100_IN           §џ                 F_CXNaviSwitchUSB               E_CX2100_NaviSwitch                             ОЯW      џџџџ           F_CXSUBTIMESTAMP           nDeltaTimeStampLoDW            §џ       +    2*32 bit delta time stamp for low DWORD			   nDeltaTimeStampHiDW            §џ       ,    2*32 bit delta time stamp for high DWORD			   fSCALE_LOWDW    Й?   0.1Й?   §џ       .    time stamp in [100*ns]: 1=0.1 us (1.0 / 10.0)   fSCALE_HIGHDW    ЙA   429496729.6ЙA   §џ           4294967296(2^32) / 10				      nTimeStampLoDW_A           §џ       &    2*32 bit time stamp A: low DWORD					   nTimeStampHiDW_A           §џ       &    2*32 bit time stamp A: high DWORD				   nTimeStampLoDW_B           §џ       &    2*32 bit time stamp B: low DWORD					   nTimeStampHiDW_B           §џ       &    2*32 bit time stamp B: high DWORD				      F_CXSubTimeStamp                                     ОЯW      џџџџ           F_GETVERSIONTCSYSTEMCX               nVersionElement           §џ                 F_GetVersionTcSystemCX                                     ОЯW      џџџџ           FB_CXGETDEVICEIDENTIFICATION        	   iDataSize       @  §џ           
   byTagStart    <      §џ           '<'    byTagEnd    >      §џ           '>' 
   byTagSlash    /      §џ           '/' 	   fbAdsRead                          ADSREAD    §џ              bExecutePrev             §џ              iState            §џ              iData   	                         §џ           
   strActPath                §џ              iLoopEndIdx            §џ              iStructSize            §џ              strHardwareCPU                §џ              strTags   	  	        )       )             §џ           	   iTagsSize   	  	                        §џ              iCurrTag   	  (                        §џ              iCurrTagData   	  P                        §џ           	   iPathSize            §џ               iTagIdx            §џ"              iCurrTagIdx            §џ#              iDataIdx            §џ$              iCurrTagDataIdx            §џ%              k            §џ&              iMinCurrData            §џ'           	   iFirstIdx            §џ(              iLastIdx            §џ)           	   bTagStart             §џ+              bTagEnd             §џ,           	   bTagSlash             §џ-              bTagOpen             §џ.                 bExecute            §џ              tTimeout           §џ                 bBusy            §џ              bError            §џ              nErrorID           §џ	           
   stDevIdent                              ST_CxDeviceIdentification   §џ
                       ОЯW     џџџџ           FB_CXGETDEVICEIDENTIFICATIONEX        	   iDataSize       @` §џ           
   byTagStart    <    ` §џ           '<'    byTagEnd    >    ` §џ           '>' 
   byTagSlash    /    ` §џ           '/' 	   fbAdsRead                          ADSREAD ` §џ              bExecutePrev          ` §џ              iState         ` §џ              iData   	                      ` §џ           
   strActPath             ` §џ              iLoopEndIdx         ` §џ              iStructSize         ` §џ              strHardwareCPU             ` §џ              strTags   	  	        )       )          ` §џ           	   iTagsSize   	  	                     ` §џ              iCurrTag   	  (                     ` §џ               iCurrTagData   	  P                     ` §џ!           	   iPathSize         ` §џ"              iTagIdx         ` §џ$              iCurrTagIdx         ` §џ%              iDataIdx         ` §џ&              iCurrTagDataIdx         ` §џ'              k         ` §џ(              iMinCurrData         ` §џ)           	   iFirstIdx         ` §џ*              iLastIdx         ` §џ+           	   bTagStart          ` §џ-              bTagEnd          ` §џ.           	   bTagSlash          ` §џ/              bTagOpen          ` §џ0                 bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ              sNetId            
   T_AmsNetId   §џ           Ams net id of target system       bBusy            §џ              bError            §џ	              nErrorID           §џ
           
   stDevIdent                              ST_CxDeviceIdentificationEx   §џ       5    structure with available device identification data             ОЯW     џџџџ           FB_CXGETTEXTDISPLAYUSB           eModeAct               E_CX2100_DisplayModesRd ` §џ              nState         ` §џ              bStarted          ` §џ              fbOnTrigger                 R_TRIG ` §џ           	   fbADSRead                        	   ADSREADEX ` §џ              nIndexOffset         ` §џ           	   nReadMode         ` §џ           
   nReadState         ` §џ              nReadStateMax         ` §џ              READ_MODE_IDLE        ` §џ!              READ_MODE_BYTE        ` §џ"              READ_MODE_STRING        ` §џ#           CX2100   IOADS_IOF_CX2100_BACKLIGHT_INT       ` §џ&           backlight 0..255    IOADS_IOF_CX2100_LINE1      ` §џ'           read/write line 1 STRING(80)    IOADS_IOF_CX2100_LINE2      ` §џ(           read/write line 2 STRING(80)    IOADS_IOF_CX2100_CURSOR      ` §џ)       !    cursor ... 0 Off; 1 On; 2 Blink    IOADS_IOF_CX2100_CURSOR_XPOS      ` §џ*           cursor position in X 0..15    IOADS_IOF_CX2100_CURSOR_YPOS      ` §џ+           cursor position in Y 0..1    IOADS_IOF_IGRP    ѓ   ` §џ,       '    ADS index group OF the CX2100 mailbox       bExecute            §џ              sNetID            
   T_AmsNetID   §џ              nPort            	   T_AmsPort   §џ              eMode               E_CX2100_DisplayModesRd   §џ           	      bBusy            §џ	              bError            §џ
              nErrorID           §џ              sLine1    Q       Q    §џ              sLine2    Q       Q    §џ              nCursorPosX           §џ              nCursorPosY           §џ              nCursorMode           §џ           
   nBacklight           §џ                       ОЯW      џџџџ           FB_CXPROFILER           iMAX_DATABUFF_SIZE    d   @  §џ              iMAX_AVERAGE_MEASURES    d      §џ              fbRisingEdgeStart                 R_TRIG    §џ              fbRisingEdgeReset                 R_TRIG    §џ              fbFallingEdgeStart                 F_TRIG    §џ              fbGetCPUCounter                 GETCPUCOUNTER    §џ              dwOldCpuCntLo            §џ              dwOldCpuCntHi            §џ              dwOldCpuCntDiff            §џ              dwNewCpuCntLo            §џ              dwNewCpuCntHi            §џ              dwNewCpuCntDiff            §џ              aMeasureData   	  d                        §џ           	   dwTimeSum            §џ              iMaxData           §џ              iIdx            §џ                 bStart            §џ       0   rising edge starts measurement and falling stops   bReset            §џ                 bBusy            §џ              stData                   ST_CX_ProfilerStruct   §џ                       ОЯW     џџџџ           FB_CXSETTEXTDISPLAY           step    d       §џ           
   fbADSWrite                          ADSWRITE    §џ              bStarted             §џ              nIndexOffset            §џ           
   nWriteMode            §џ              fbOnTrigger                 R_TRIG    §џ              temp            §џ              WRITE_MODE_IDLE          §џ              WRITE_MODE_BYTE          §џ              WRITE_MODE_STRING          §џ       	     CX1100    IOADS_IOF_CX1100_WRITE1LINE    џ џџ   §џ              IOADS_IOF_CX1100_WRITE2LINE    џ@џџ   §џ              IOADS_IOF_CX1100_WRITECURSOR    џџџ   §џ               IOADS_IOF_CX1100_FILLRAW    џџџ   §џ!              IOADS_IOF_CX1100_CURSOR_OFF    џџџ   §џ"              IOADS_IOF_CX1100_CURSOR_ON    џџџ   §џ#              IOADS_IOF_CX1100_CURSOR_BOFF    џџџ   §џ$              IOADS_IOF_CX1100_CURSOR_BON    џџџ   §џ%              IOADS_IOF_CX1100_DISPLAY_OFF    џџџ   §џ&              IOADS_IOF_CX1100_DISPLAY_ON    џџџ   §џ'              IOADS_IOF_CX1100_BACKLIGHT_ON    џџџ   §џ(              IOADS_IOF_CX1100_BACKLIGHT_OFF    џџџ   §џ)              IOADS_IOF_CX1100_CLEARDISPLAY    џ џџ   §џ*              IOADS_IOF_CX1100_TERMTYPES     џџ   §џ,                 bExecute            §џ              nDevID           §џ              nMode               E_CX1100_DisplayModes   §џ              stLine               §џ           
   nCursorPos           §џ                 bBusy            §џ
              bErr            §џ              nErrorID           §џ                       ОЯW      џџџџ           FB_CXSETTEXTDISPLAYUSB           eModeAct               E_CX2100_DisplayModesWr ` §џ              nState         ` §џ              bStarted          ` §џ              fbOnTrigger                 R_TRIG ` §џ           
   fbADSWrite                          ADSWRITE ` §џ              nIndexOffset         ` §џ           
   nWriteMode         ` §џ              nCursorMode         ` §џ              nBackLightVal         ` §џ              nWriteState         ` §џ              nWriteStateMax         ` §џ           
   sClearLine    Q       Q  ` §џ              WRITE_MODE_IDLE        ` §џ"              WRITE_MODE_BYTE        ` §џ#              WRITE_MODE_STRING        ` §џ$           CX1200   IOADS_IOF_CX2100_BACKLIGHT_INT       ` §џ'           backlight 0..255    IOADS_IOF_CX2100_LINE1      ` §џ(           read/write line 1    IOADS_IOF_CX2100_LINE2      ` §џ)           read/write line 2    IOADS_IOF_CX2100_CURSOR      ` §џ*       !    cursor ... 0 Off; 1 On; 2 Blink    IOADS_IOF_CX2100_CURSOR_XPOS      ` §џ+           cursor position in X    IOADS_IOF_CX2100_CURSOR_YPOS      ` §џ,           cursor position in Y    IOADS_IOF_IGRP    ѓ   ` §џ-       '    ADS index group OF the CX2100 mailbox       bExecute            §џ              sNetID            
   T_AmsNetID   §џ              nPort            	   T_AmsPort   §џ              eMode               E_CX2100_DisplayModesWr   §џ              sLine1    Q       Q    §џ              sLine2    Q       Q    §џ              nCursorPosX           §џ	              nCursorPosY           §џ
                 bBusy            §џ              bError            §џ              nErrorID           §џ                       ОЯW      џџџџ           FB_CXSIMPLEUPS        
   Ii24VState           §џ              IiChargeState           §џ           	   QiControl           §џ           set bit 3 - auto reset    QiDipControl            §џ              iState            §џ       "    0: IDLE, 1: HOLDING, 2: SHUTDOWN 	   HoldTimer                    TON    §џ           
   fbShutdown                       	   ADSWRTCTL    §џ           Windows shutdown    dwDelay            §џ       &    0 immediate shutdown of Windows, [s]    WaitStateTime                    TON    §џ          Wait for state UPS   DischargeTime                    TON    §џ       $   Catch state UPS discharging-restart    bEndOfHolding             §џ          Phase before Shutdown activ   tWaitStateTime    Ф	     §џ               tDischargeTime    Ф	     §џ!                 bDIPDisable            §џ           If TRUE override Dip Switch    iDischargeLevel           §џ       O    Discharge Switch Off Level: 0 = 100%, 9 = 90%, 8 = 80%, ..., 2 = 20%, 1 = 10%    tDelay           §џ       Q    Time to hold during power failure (shutdown after timer ellapses) [0.5s .. 10s]       bPowerFailure            §џ           True if power fault detected    bShutdownActive            §џ	       '    True if shutdown is actively executed 	   bUpsReady            §џ
           True if UPS 24V Out is OK    b24VInOK            §џ           True if UPS 24V In is OK    bHolding            §џ       :    True if power fault detected and tDelay not yet ellapsed    tTimeUntilShutdown           §џ       =    Remaining Time until system shuts down during power failure 	   eUpsState               E_UPS_STATE   §џ       o    UPS-State [UNDEF | CHARGING | CHARGED | DISCHARGE |
													 DISCHARGE_RESTART | OUTPUT_OFF | OVERLOAD]             ОЯW     џџџџ    o   C:\TWINCAT\PLC\LIB\TcBase.lib @                                                                                          FW_ADSCLEAREVENTS           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ           
   READ_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              bClear            §џ              nMode           §џ              tTimeout           §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ                       J     џџџџ           FW_ADSLOGDINT            	   nCtrlMask           §џ              sMsgFmt               §џ              nArg           §џ                 FW_AdsLogDINT                                     J     џџџџ           FW_ADSLOGEVENT        
   STAMPREQ_I            §џ           
   STAMPRES_I            §џ           
   STAMPSIG_I            §џ           
   STAMPCON_I            §џ              ACCESSCNT_I            §џ           	   AMSADDR_I   	                         §џ              EVENT_I                      
   FW_TcEvent    §џ              pTCEVENTSTREAM_I            §џ              CBEVENTSTREAM_I            §џ              nSTATE_I            §џ              nSTATEREQ_I            §џ              nSTATERES_I            §џ              nSTATESIG_I            §џ               nSTATECON_I            §џ!              ERR_I             §џ"              ERRID_I            §џ#              bEVENT_SAV_I             §џ$              bEVENTQUIT_SAV_I             §џ%              TICKSTART_I            §џ&           	      sNetId               §џ              nPort           §џ              bEvent            §џ           
   bEventQuit            §џ              stEventConfigData                      
   FW_TcEvent   §џ              pEventDataAddress           §џ       	    pointer    cbEventDataLength           §џ	           
   bFbCleanup            §џ
              tTimeout           §џ                 nEventState           §џ              bError            §џ              nErrId           §џ              bQuit            §џ                       J     џџџџ           FW_ADSLOGLREAL            	   nCtrlMask           §џ              sMsgFmt               §џ              fArg                        §џ                 FW_AdsLogLREAL                                     J     џџџџ           FW_ADSLOGSTR            	   nCtrlMask           §џ              sMsgFmt               §џ              sArg               §џ                 FW_AdsLogSTR                                     J     џџџџ           FW_ADSRDWRT           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ              WRTRD_SAV_I             §џ              PDESTADDR_I            §џ              TICKSTART_I            §џ           
      sNetId               §џ              nPort           §џ              nIdxGrp           §џ              nIdxOffs           §џ           
   cbWriteLen           §џ           	   cbReadLen           §џ           
   pWriteBuff           §џ	           	   pReadBuff           §џ
              bExecute            §џ              tTimeout           §џ                 bBusy            §џ              bError            §џ              nErrId           §џ              cbRead           §џ           count of bytes actually read             J     џџџџ           FW_ADSRDWRTIND           CLEAR_I             §џ                 bClear            §џ           	      bValid            §џ              sNetId               §џ              nPort           §џ           	   nInvokeId           §џ	              nIdxGrp           §џ
              nIdxOffs           §џ           	   cbReadLen           §џ           
   cbWriteLen           §џ           
   pWriteBuff           §џ                       J     џџџџ           FW_ADSRDWRTRES        	   RESPOND_I             §џ                 sNetId               §џ              nPort           §џ           	   nInvokeId           §џ              nErrId           §џ           	   cbReadLen           §џ           	   pReadBuff           §џ              bRespond            §џ	                           J     џџџџ        
   FW_ADSREAD           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ           
   READ_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              nPort           §џ              nIdxGrp           §џ              nIdxOffs           §џ           	   cbReadLen           §џ           	   pReadBuff           §џ              bExecute            §џ	              tTimeout           §џ
                 bBusy            §џ              bError            §џ              nErrId           §џ              cbRead           §џ           count of bytes actually read             J     џџџџ           FW_ADSREADDEVICEINFO           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ              RDINFO_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              nPort           §џ              bExecute            §џ              tTimeout           §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ              sDevName               §џ              nDevVersion           §џ                       J     џџџџ           FW_ADSREADIND           CLEAR_I             §џ                 bClear            §џ                 bValid            §џ              sNetId               §џ              nPort           §џ           	   nInvokeId           §џ	              nIdxGrp           §џ
              nIdxOffs           §џ           	   cbReadLen           §џ                       J     џџџџ           FW_ADSREADRES        	   RESPOND_I             §џ                 sNetId               §џ              nPort           §џ           	   nInvokeId           §џ              nErrId           §џ           	   cbReadLen           §џ           	   pReadBuff           §џ              bRespond            §џ	                           J     џџџџ           FW_ADSREADSTATE           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ              RDSTATE_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              nPort           §џ              bExecute            §џ              tTimeout           §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ           	   nAdsState           §џ           	   nDevState           §џ                       J     џџџџ           FW_ADSWRITE           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ              WRITE_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              nPort           §џ              nIdxGrp           §џ              nIdxOffs           §џ           
   cbWriteLen           §џ           
   pWriteBuff           §џ              bExecute            §џ	              tTimeout           §џ
                 bBusy            §џ              bError            §џ              nErrId           §џ                       J     џџџџ           FW_ADSWRITECONTROL           STAMP_I            §џ              ACCESSCNT_I            §џ              BUSY_I             §џ              ERR_I             §џ              ERRID_I            §џ              WRITE_SAV_I             §џ              TICKSTART_I            §џ                 sNetId               §џ              nPort           §џ           	   nAdsState           §џ           	   nDevState           §џ           
   cbWriteLen           §џ           
   pWriteBuff           §џ              bExecute            §џ	              tTimeout           §џ
                 bBusy            §џ              bError            §џ              nErrId           §џ                       J     џџџџ           FW_ADSWRITEIND           CLEAR_I             §џ                 bClear            §џ                 bValid            §џ              sNetId               §џ              nPort           §џ           	   nInvokeId           §џ	              nIdxGrp           §џ
              nIdxOffs           §џ           
   cbWriteLen           §џ           
   pWriteBuff           §џ                       J     џџџџ           FW_ADSWRITERES        	   RESPOND_I             §џ                 sNetId               §џ              nPort           §џ           	   nInvokeId           §џ              nErrId           §џ              bRespond            §џ                           J     џџџџ           FW_DRAND           FirstCall_i             §џ	           
   HoldRand_i            §џ
              R250_Buffer_i   	  љ                        §џ           
   R250_Index            §џ                 nSeed           §џ                 fRndNum                        §џ                       J     џџџџ           FW_GETCPUACCOUNT                   dwCpuAccount           §џ                       J     џџџџ           FW_GETCPUCOUNTER                
   dwCpuCntLo           §џ           
   dwCpuCntHi           §џ                       J     џџџџ           FW_GETCURTASKINDEX                   nIndex           §џ                       J     џџџџ           FW_GETSYSTEMTIME                   dwTimeLo           §џ              dwTimeHi           §џ                       J     џџџџ           FW_GETVERSIONTCBASE               nVersionElement           §џ                 FW_GetVersionTcBase                                     J     џџџџ           FW_LPTSIGNAL            	   nPortAddr           §џ              nPinNo           §џ              bOnOff            §џ	                 FW_LptSignal                                      J     џџџџ        	   FW_MEMCMP               pBuf1           §џ           First buffer    pBuf2           §џ           Second buffer    cbLen           §џ           Number of characters    	   FW_MemCmp                                     J     џџџџ        	   FW_MEMCPY               pDest           §џ           New buffer    pSrc           §џ           Buffer to copy from    cbLen           §џ           Number of characters to copy    	   FW_MemCpy                                     J     џџџџ        
   FW_MEMMOVE               pDest           §џ           New buffer    pSrc           §џ           Buffer to copy from    cbLen           §џ           Number of characters to copy    
   FW_MemMove                                     J     џџџџ        	   FW_MEMSET               pDest           §џ           Pointer to destination 	   nFillByte           §џ           Character to set    cbLen           §џ           Number of characters    	   FW_MemSet                                     J     џџџџ           FW_PORTREAD            	   nPortAddr           §џ           	   eNoOfByte               FW_NoOfByte   §џ                 FW_PortRead                                     J     џџџџ           FW_PORTWRITE            	   nPortAddr           §џ           	   eNoOfByte               FW_NoOfByte   §џ              nValue           §џ                 FW_PortWrite                                      J     џџџџ    q   C:\TWINCAT\PLC\LIB\TcSystem.lib @                                                                                T          ADSCLEAREVENTS           fbAdsClearEvents                            FW_AdsClearEvents ` §џ                 NetID            
   T_AmsNetId   §џ              bClear            §џ              iMode           §џ              tTimeout         §џ                 bBusy            §џ	              bErr            §џ
              iErrId           §џ                       ђ*VW     џџџџ        
   ADSLOGDINT               msgCtrlMask           §џ           	   msgFmtStr               T_MaxString   §џ              dintArg           §џ              
   ADSLOGDINT                                     ђ*VW      џџџџ           ADSLOGEVENT           fbAdsLogEvent                                               FW_AdsLogEvent ` §џ           	      NETID            
   T_AmsNetId   §џ              PORT           §џ              Event            §џ           	   EventQuit            §џ              EventConfigData               TcEvent   §џ              EventDataAddress           §џ       	    pointer    EventDataLength           §џ	           	   FbCleanup            §џ
              TMOUT         §џ              
   EventState           §џ              Err            §џ              ErrId           §џ              Quit            §џ                       ђ*VW     џџџџ           ADSLOGLREAL               msgCtrlMask           §џ           	   msgFmtStr               T_MaxString   §џ              lrealArg                        §џ                 ADSLOGLREAL                                     ђ*VW      џџџџ        	   ADSLOGSTR               msgCtrlMask           §џ           	   msgFmtStr               T_MaxString   §џ              strArg               T_MaxString   §џ              	   ADSLOGSTR                                     ђ*VW      џџџџ           ADSRDDEVINFO           fbAdsReadDeviceInfo                              FW_AdsReadDeviceInfo    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              RDINFO            §џ              TMOUT         §џ                 BUSY            §џ	              ERR            §џ
              ERRID           §џ              DEVNAME               §џ              DEVVER           §џ                       ђ*VW     џџџџ        
   ADSRDSTATE           fbAdsReadState                              FW_AdsReadState    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              RDSTATE            §џ              TMOUT         §џ                 BUSY            §џ	              ERR            §џ
              ERRID           §џ              ADSSTATE           §џ              DEVSTATE           §џ                       ђ*VW     џџџџ           ADSRDWRT        
   fbAdsRdWrt                                    FW_AdsRdWrt    §џ           
      NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              IDXGRP           §џ              IDXOFFS           §џ              WRITELEN           §џ              READLEN           §џ              SRCADDR           §џ	              DESTADDR           §џ
              WRTRD            §џ              TMOUT         §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ                       ђ*VW     џџџџ        
   ADSRDWRTEX        
   fbAdsRdWrt                                    FW_AdsRdWrt    §џ           
      NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              IDXGRP           §џ              IDXOFFS           §џ              WRITELEN           §џ              READLEN           §џ              SRCADDR           §џ	              DESTADDR           §џ
              WRTRD            §џ              TMOUT         §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ              COUNT_R           §џ           count of bytes actually read             ђ*VW     џџџџ           ADSRDWRTIND           fbAdsRdWrtInd                         FW_AdsRdWrtInd    §џ                 CLEAR            §џ           	      VALID            §џ              NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ	              IDXGRP           §џ
              IDXOFFS           §џ              RDLENGTH           §џ           	   WRTLENGTH           §џ              DATAADDR           §џ                       ђ*VW      џџџџ           ADSRDWRTRES           fbAdsRdWrtRes                      FW_AdsRdWrtRes    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ              RESULT           §џ              LEN           §џ              DATAADDR           §џ              RESPOND            §џ	                           ђ*VW      џџџџ           ADSREAD        	   fbAdsRead                              
   FW_AdsRead    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              IDXGRP           §џ              IDXOFFS           §џ              LEN           §џ              DESTADDR           §џ              READ            §џ	              TMOUT         §џ
                 BUSY            §џ              ERR            §џ              ERRID           §џ                       ђ*VW     џџџџ        	   ADSREADEX        	   fbAdsRead                              
   FW_AdsRead    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              IDXGRP           §џ              IDXOFFS           §џ              LEN           §џ              DESTADDR           §џ              READ            §џ	              TMOUT         §џ
                 BUSY            §џ              ERR            §џ              ERRID           §џ              COUNT_R           §џ           count of bytes actually read             ђ*VW     џџџџ        
   ADSREADIND           fbAdsReadInd        	               FW_AdsReadInd    §џ                 CLEAR            §џ                 VALID            §џ              NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ	              IDXGRP           §џ
              IDXOFFS           §џ              LENGTH           §џ                       ђ*VW      џџџџ        
   ADSREADRES           fbAdsReadRes                      FW_AdsReadRes    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ              RESULT           §џ              LEN           §џ              DATAADDR           §џ              RESPOND            §џ	                           ђ*VW      џџџџ           ADSWRITE        
   fbAdsWrite                                FW_AdsWrite    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              IDXGRP           §џ              IDXOFFS           §џ              LEN           §џ              SRCADDR           §џ              WRITE            §џ	              TMOUT         §џ
                 BUSY            §џ              ERR            §џ              ERRID           §џ                       ђ*VW     џџџџ           ADSWRITEIND           fbAdsWriteInd        
                FW_AdsWriteInd    §џ                 CLEAR            §џ                 VALID            §џ              NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ	              IDXGRP           §џ
              IDXOFFS           §џ              LENGTH           §џ              DATAADDR           §џ                       ђ*VW      џџџџ           ADSWRITERES           fbAdsWriteRes                    FW_AdsWriteRes    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              INVOKEID           §џ              RESULT           §џ              RESPOND            §џ                           ђ*VW      џџџџ        	   ADSWRTCTL           fbAdsWriteControl                                FW_AdsWriteControl    §џ                 NETID            
   T_AmsNetId   §џ              PORT            	   T_AmsPort   §џ              ADSSTATE           §џ              DEVSTATE           §џ              LEN           §џ              SRCADDR           §џ              WRITE            §џ	              TMOUT         §џ
                 BUSY            §џ              ERR            §џ              ERRID           §џ                       ђ*VW     џџџџ           ANALYZEEXPRESSION               InputExp            §џ           	   DoAnalyze            §џ              	   ExpResult            §џ           	   OutString               §џ                       ђ*VW      џџџџ           ANALYZEEXPRESSIONCOMBINED           Index            §џ                 InputExp            §џ           	   DoAnalyze            §џ              	   ExpResult            §џ              OutTable   	                        ExpressionResult           §џ           	   OutString               §џ	                       ђ*VW      џџџџ           ANALYZEEXPRESSIONTABLE           Index            §џ                 InputExp            §џ           	   DoAnalyze            §џ              	   ExpResult            §џ              OutTable   	                        ExpressionResult           §џ                       ђ*VW      џџџџ           APPENDERRORSTRING               strOld               §џ              strNew               §џ                 AppendErrorString                                         ђ*VW      џџџџ           BAVERSION_TO_DWORD               nVersion         ` §џ           	   nRevision         ` §џ	              nBuild         ` §џ
                 BAVERSION_TO_DWORD                                     ђ*VW      џџџџ        
   CLEARBIT32           dwConst        ` §џ                 inVal32           §џ              bitNo           §џ              
   CLEARBIT32                                     ђ*VW      џџџџ        	   CSETBIT32           dwConst        ` §џ	                 inVal32           §џ              bitNo           §џ              bitVal            §џ       &    value to which the bit should be set    	   CSETBIT32                                     ђ*VW      џџџџ           DRAND           fbDRand                    FW_DRand ` §џ
                 Seed           §џ                 Num                        §џ                       ђ*VW      џџџџ           F_COMPAREFWVERSION           soll         ` §џ              ist         ` §џ                 major         ` §џ           requiered major version    minor         ` §џ	           requiered minor version    revision         ` §џ
       )    requiered revision/service pack version    patch         ` §џ       0    required patch version (reserved, default = 0 )      F_CompareFwVersion                                      ђ*VW      џџџџ           F_CREATEAMSNETID           idx         ` §џ                 nIds               T_AmsNetIdArr   §џ           Ams Net ID as array of bytes.       F_CreateAmsNetId            
   T_AmsNetId                             ђ*VW      џџџџ           F_CREATEIPV4ADDR           idx         ` §џ                 nIds               T_IPv4AddrArr   §џ       <    Internet Protocol dotted address (ipv4) as array of bytes.       F_CreateIPv4Addr            
   T_IPv4Addr                             ђ*VW      џџџџ           F_GETSTRUCTMEMBERALIGNMENT           tmp                ST_StructMemberAlignmentProbe ` §џ                     F_GetStructMemberAlignment                                     ђ*VW      џџџџ           F_GETVERSIONTCSYSTEM               nVersionElement           §џ                 F_GetVersionTcSystem                                     ђ*VW      џџџџ           F_IOPORTREAD               nAddr           §џ           Port address    eSize               E_IOAccessSize   §џ           Number of bytes to read       F_IOPortRead                                     ђ*VW      џџџџ           F_IOPORTWRITE               nAddr           §џ           Port address    eSize               E_IOAccessSize   §џ           Number of bytes to write    nValue           §џ           Value to write       F_IOPortWrite                                      ђ*VW      џџџџ           F_SCANAMSNETIDS           pNetID               ` §џ              b               T_AmsNetIdArr ` §џ              w         ` §џ	              id         ` §џ
           	   Index7001                            sNetID            
   T_AmsNetID   §џ       :    String containing the Ams Net ID. E.g. '127.16.17.3.1.1'       F_ScanAmsNetIds               T_AmsNetIdArr                             ђ*VW      џџџџ           F_SCANIPV4ADDRIDS           b               T_AmsNetIdArr ` §џ           	   Index7001                            sIPv4            
   T_IPv4Addr   §џ       M    String containing the Internet Protocol dotted address. E.g. '172.16.7.199'       F_ScanIPv4AddrIds               T_IPv4AddrArr                             ђ*VW      џџџџ           F_SPLITPATHNAME           pPath               ` §џ              pSlash               ` §џ              pDot               ` §џ              p               ` §џ              length         ` §џ              	   sPathName               T_MaxString   §џ                 F_SplitPathName                                sDrive               §џ              sDir                T_MaxString  §џ           	   sFileName                T_MaxString  §џ              sExt                T_MaxString  §џ	                   ђ*VW      џџџџ           F_TOASC           pChar               ` §џ                 str    Q       Q    §џ                 F_ToASC                                     ђ*VW      џџџџ           F_TOCHR           pChar    	                            ` §џ                 c           §џ                 F_ToCHR    Q       Q                              ђ*VW      џџџџ           FB_ADSREADWRITELIST           para                ST_AdsRdWrtListPara ` §џ           	   fbTrigger                 R_TRIG ` §џ              nState         ` §џ              fbCall       т    ( 	sNetID := '', nPort := 16#1234,
									nIdxGrp := GENERIC_FB_GRP_ADS,
									nIdxOffs := GENERIC_FB_ADS_RDWRTLIST,
									bExecute := FALSE,  ACCESSCNT_I := 16#0000BEC1,
									tTimeout := DEFAULT_ADS_TIMEOUT )     СО                 4                              FW_AdsRdWrt ` §џ           
      sNetId           ''    
   T_AmsNetID ` §џ              nPort           0    	   T_AmsPort ` §џ              nIdxGrp         ` §џ              nIdxOffs         ` §џ              cbWriteList         ` §џ	           Byte size of list array 
   pWriteList                   ST_AdsReadWriteListEntry      ` §џ
       !    Pointer to the first list entry 	   cbReadLen         ` §џ           	   pReadBuff           0       PVOID ` §џ              bExecute          ` §џ              tTimeout       ` §џ                 bBusy          ` §џ              bError          ` §џ              nErrID         ` §џ              cbRead         ` §џ                       ђ*VW     џџџџ           FB_BADEVICEIOCONTROL           fbRW       O    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_BADEVAPI, IDXOFFS := 0 )              	   T_AmsPort         L                 
   ADSRDWRTEX ` §џ              req                ST_AdsBaDevApiReq ` §џ              state         ` §џ              rtrig                 R_TRIG ` §џ                 sNetID           ''    
   T_AmsNetID ` §џ           Ams net id    affinity           ( lower :=0, higher := 0 )                T_U64KAFFINITY ` §џ       )    Affinity mask (default  = 0 = not used) 	   nModifier         ` §џ       +    Optional command modifier (0 == not used)    nIdxGrp         ` §џ           Api function group    nIdxOffs         ` §џ           Api function offset 
   cbWriteLen         ` §џ	           Input data byte size 	   cbReadLen         ` §џ
           Output data byte size 
   pWriteBuff         ` §џ           Pointer to input data buffer 	   pReadBuff         ` §џ           Pointer to output data buffer    bExecute          ` §џ       &    Rising edge starts command execution    tTimeout       ` §џ                 bBusy          ` §џ              bError          ` §џ              nErrID         ` §џ              cbRead         ` §џ           Count of bytes actually read             ђ*VW     џџџџ           FB_BAGENGETVERSION           fbCtrl           ( nModifier := 0, affinity := ( lower := 0, higher := 0 ), nIdxGrp := BADEVAPIIGRP_GENERAL, nIdxOffs := BADEVAPIIOFFS_GENERAL_VERSION )                ( lower :=0, higher := 0 )                T_U64KAFFINITY                                             FB_BaDeviceIoControl ` §џ              rtrig                 R_TRIG ` §џ              state         ` §џ              rsp         ` §џ                 sNetID           ''    
   T_AmsNetID ` §џ           ams net id    bExecute          ` §џ       &    rising edge starts command execution    tTimeout       ` §џ                 bBusy          ` §џ	              bError          ` §џ
              nErrID         ` §џ              nVersion         ` §џ           	   nRevision         ` §џ              nBuild         ` §џ                       ђ*VW     џџџџ           FB_CREATEDIR        
   fbAdsRdWrt       ]    ( nPort:= AMSPORT_R3_SYSSERV, nIdxGrp:= SYSTEMSERVICE_MKDIR, cbReadLen := 0, pReadBuff:= 0 )             '                            FW_AdsRdWrt ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id 	   sPathName               T_MaxString   §џ           max directory length = 255    ePath           PATH_GENERIC    
   E_OpenPath   §џ       +    Default: Create directory at generic path    bExecute            §џ       %    rising edge start command execution    tTimeout         §џ                 bBusy            §џ
              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_EOF        
   fbAdsRdWrt       `    (nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FEOF, cbWriteLen := 0, pWriteBuff := 0 )             '                            FW_AdsRdWrt ` §џ              iEOF         ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle    bExecute            §џ           control input    tTimeout         §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ              bEOF            §џ                       ђ*VW     џџџџ           FB_FILECLOSE        
   fbAdsRdWrt           ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FCLOSE, cbWriteLen := 0,	cbReadLen := 0,	pWriteBuff := 0, pReadBuff := 0 )             '   y                                 FW_AdsRdWrt ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ       %    file handle obtained through 'open'    bExecute            §џ           close control input    tTimeout         §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ                       ђ*VW     џџџџ           FB_FILEDELETE        
   fbAdsRdWrt       a    (nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FDELETE, cbReadLen := 0, pReadBuff := 0 )             '                            FW_AdsRdWrt ` §џ              tmpOpenMode         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id 	   sPathName               T_MaxString   §џ           file path and name    ePath           PATH_GENERIC    
   E_OpenPath   §џ           Default: Open generic file    bExecute            §џ           open control input    tTimeout         §џ                 bBusy            §џ
              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_FILEGETS        
   fbAdsRdWrt       b    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FGETS, cbWriteLen := 0, pWriteBuff := 0 )             '   ~                         FW_AdsRdWrt ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle    bExecute            §џ           control input    tTimeout         §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ              sLine               T_MaxString   §џ              bEOF            §џ                       ђ*VW     џџџџ           FB_FILEOPEN        
   fbAdsRdWrt       @    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FOPEN )             '   x                 FW_AdsRdWrt ` §џ              tmpOpenMode         ` §џ              tmpHndl         ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id 	   sPathName               T_MaxString   §џ           max filename length = 255    nMode           §џ           open mode flags    ePath           PATH_GENERIC    
   E_OpenPath   §џ           Default: Open generic file    bExecute            §џ           open control input    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrId           §џ              hFile           §џ           file handle             ђ*VW     џџџџ           FB_FILEPUTS        
   fbAdsRdWrt       `    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FPUTS, cbReadLen := 0, pReadBuff := 0 )             '                            FW_AdsRdWrt ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle    sLine               T_MaxString   §џ           string to write    bExecute            §џ           control input    tTimeout         §џ                 bBusy            §џ
              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_FILEREAD        
   fbAdsRdWrt       b    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FREAD, cbWriteLen := 0, pWriteBuff := 0 )             '   z                         FW_AdsRdWrt ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle 	   pReadBuff           §џ           buffer address for read 	   cbReadLen           §џ           count of bytes for read    bExecute            §џ           read control input    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrId           §џ              cbRead           §џ           count of bytes actually read    bEOF            §џ                       ђ*VW     џџџџ           FB_FILERENAME        
   fbAdsRdWrt       b    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FRENAME, cbReadLen := 0, pReadBuff := 0 )             '                            FW_AdsRdWrt ` §џ              tmpOpenMode         ` §џ           
   sBothNames   	                    T_MaxString         ` §џ           = SIZEOF( T_MaxString ) * 2    nOldLen         ` §џ              nNewLen         ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    sOldName               T_MaxString   §џ           max filename length = 255    sNewName               T_MaxString   §џ           max filename length = 255    ePath           PATH_GENERIC    
   E_OpenPath   §џ           Default: generic file path   bExecute            §џ           open control input    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_FILESEEK        
   fbAdsRdWrt       `    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FSEEK, cbReadLen := 0, pReadBuff := 0 )             '   |                         FW_AdsRdWrt ` §џ           
   tmpSeekPos   	                       ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ	           file handle    nSeekPos           §џ
           new seek pointer position    eOrigin       	    SEEK_SET       E_SeekOrigin   §џ              bExecute            §џ           seek control input    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_FILETELL        
   fbAdsRdWrt       b    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FTELL, cbWriteLen := 0, pWriteBuff := 0 )             '   }                         FW_AdsRdWrt ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle    bExecute            §џ           control input    tTimeout         §џ                 bBusy            §џ	              bError            §џ
              nErrId           §џ              nSeekPos           §џ          	On error, nSEEKPOS returns -1             ђ*VW     џџџџ           FB_FILEWRITE        
   fbAdsRdWrt       A    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_FWRITE )             '   {                 FW_AdsRdWrt ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id    hFile           §џ           file handle 
   pWriteBuff           §џ           buffer address for write 
   cbWriteLen           §џ           count of bytes for write    bExecute            §џ           write control input    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrId           §џ              cbWrite           §џ       !    count of bytes actually written             ђ*VW     џџџџ           FB_PCWATCHDOG           bRetVal          ` §џ              iTime         ` §џ              iIdx         ` §џ              iPortArr   	                 >    16#2E, 16#2E, 16#2E, 16#2F, 16#2E, 16#2F, 16#2E, 16#2F, 16#2E	      .      .      .      /      .      /      .      /      .    ` §џ              iArrEn   	                 >    16#87, 16#87, 16#07, 16#08, 16#F6, 16#05, 16#30, 16#01, 16#AA	                              і            0            Њ    ` §џ              iArrDis   	                 >    16#87, 16#87, 16#07, 16#08, 16#F6, 16#00, 16#30, 16#00, 16#AA	                              і             0             Њ    ` §џ                 tTimeOut           §џ       ;    Watchdog TimeOut Time 1s..255s, disabled if tTimeOut < 1s    bEnable            §џ           Enable / Disable Watchdog       bEnabled            §џ       2    TRUE: Watchdog Enabled; FALSE: Watchdog Disabled    bBusy            §џ           FB still busy    bError            §џ	           FB has error     nErrId           §џ
           FB error ID               ђ*VW      џџџџ           FB_PCWATCHDOG_BAPI           nState         ` §џ              bInit         ` §џ              eConfig           eWATCHDOG_TIME_DISABLED       E_WATCHDOG_TIME_CONFIG ` §џ           
   nWatchTime         ` §џ           watchdog watch time 	   nTimeBase         ` §џ       *    watchdog time base: seconds/milliseconds    nPwrCtrlIoWd        ` §џ       >    1 use power controller IO watchdog; 0 use compatibility mode    fbGetVersion                           FB_BaGenGetVersion ` §џ              nVersion         ` §џ           
   stGpioInfo                 ST_WD_GPIO_Info ` §џ              bUseInfo          ` §џ              stGpioInfoEx                      ST_WD_GPIO_InfoEx ` §џ           
   bUseInfoEx          ` §џ              nWDByte         ` §џ              fbCtrl           ( affinity := ( lower := 0, higher := 0 ), nModifier := 0, nIdxGrp := BIOSIGRP_WATCHDOG, nIdxOffs := BIOSIOFFS_WATCHDOG_CONFIG )                ( lower :=0, higher := 0 )                T_U64KAFFINITY                        `                   FB_BaDeviceIoControl ` §џ              rtrig                 R_TRIG ` §џ              bRetVal          ` §џ           	   pAddress1               ` §џ               dxValue1         ` §џ!           
   dxBitMask1         ` §џ"           	   pAddress2               ` §џ#              dxValue2         ` §џ$           
   dxBitMask2         ` §џ%           	   pAddress4               ` §џ&              dxValue4         ` §џ'           
   dxBitMask4         ` §џ(                 sNetID           ''    
   T_AmsNetID   §џ       ;    ams net id, only empty string or local netid is supported    nWatchdogTimeS             Ф;             §џ       -    Watchdog time [s], 0 = disable, >0 = enable    bExecute            §џ       &    rising edge starts command execution    tTimeout         §џ           ADS communication timeout       bEnabled            §џ	       2    TRUE: Watchdog Enabled; FALSE: Watchdog Disabled    bBusy            §џ
           TRUE: function block is busy    bError            §џ            TRUE: function block has error    nErrID           §џ           FB error ID               ђ*VW     џџџџ           FB_REMOVEDIR        
   fbAdsRdWrt       `    ( nPort := AMSPORT_R3_SYSSERV, nIdxGrp := SYSTEMSERVICE_RMDIR, cbReadLen := 0, pReadBuff := 0 )             '                            FW_AdsRdWrt ` §џ                 sNetId            
   T_AmsNetId   §џ           ams net id 	   sPathName               T_MaxString   §џ           max filename length = 255    ePath           PATH_GENERIC    
   E_OpenPath   §џ       +    Default: Delete directory at generic path    bExecute            §џ       &    rising edge starts command execution    tTimeout         §џ                 bBusy            §џ
              bError            §џ              nErrId           §џ                       ђ*VW     џџџџ           FB_SETLEDCOLOR_BAPI           nState         ` §џ              fbCtrl       y    ( affinity := ( lower := 0, higher := 0 ), nModifier := 0, nIdxGrp := BIOSIGRP_LED, nIdxOffs := BIOSIOFFS_LED_SET_USER )                ( lower :=0, higher := 0 )                T_U64KAFFINITY                                           FB_BaDeviceIoControl ` §џ              rtrig                 R_TRIG ` §џ              nLED         ` §џ                 sNetID           ''    
   T_AmsNetID   §џ       ;    ams net id, only empty string or local netid is supported 	   eNewColor               E_UsrLED_Color   §џ           new LED Color    bExecute            §џ           change LED Color    tTimeout         §џ           ADS communication timeout       bBusy            §џ	           TRUE: function block is busy    bError            §џ
            TRUE: function block has error    nErrID           §џ           FB error ID               ђ*VW     џџџџ           FB_SIMPLEADSLOGEVENT           fbEvent       9    ( NETID := '', PORT := AMSPORT_EVENTLOG, TMOUT:= t#15s )             
   T_AmsNetId         n          :         ADSLOGEVENT ` §џ              cfgEvent               TcEvent ` §џ              bInit         ` §џ                 SourceID           §џ              EventID           §џ           	   bSetEvent           §џ              bQuit            §џ                 ErrId           §џ	              Error            §џ
                       ђ*VW     џџџџ        	   FILECLOSE        
   fbAdsWrite                                FW_AdsWrite    §џ                 NETID            
   T_AmsNetId   §џ           ams net id    HFILE           §џ       )    file handle obtained through 'FILEOPEN'    CLOSE            §џ           close control input    TMOUT         §џ                 BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ђ*VW     џџџџ           FILEOPEN        
   fbAdsWrite                                FW_AdsWrite    §џ           
   RisingEdge                 R_TRIG    §џ              FallingEdge                 F_TRIG    §џ                 NETID            
   T_AmsNetId   §џ           ams net id 	   FPATHNAME               T_MaxString   §џ       #    default max filename length = 255    OPENMODE           §џ           open mode flags    OPEN            §џ           open control input    TMOUT         §џ                 BUSY            §џ
              ERR            §џ              ERRID           §џ              HFILE           §џ           file handle             ђ*VW     џџџџ           FILEREAD        	   fbAdsRead                              
   FW_AdsRead    §џ                 NETID            
   T_AmsNetId   §џ           ams net id    HFILE           §џ           file handle    BUFADDR           §џ           buffer address for read    COUNT           §џ           count of bytes for read    READ            §џ           read control input    TMOUT         §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ              COUNT_R           §џ           count of bytes actually read             ђ*VW     џџџџ           FILESEEK        
   fbAdsWrite                                FW_AdsWrite    §џ                 NETID            
   T_AmsNetId   §џ           ams net id    HFILE           §џ           file handle    SEEKPOS           §џ           new seek pointer position    SEEK            §џ           seek control input    TMOUT         §џ                 BUSY            §џ
              ERR            §џ              ERRID           §џ                       ђ*VW     џџџџ        	   FILEWRITE        
   fbAdsWrite                                FW_AdsWrite    §џ           
   RisingEdge                 R_TRIG    §џ              FallingEdge                 F_TRIG    §џ              tmpCount            §џ                 NETID            
   T_AmsNetId   §џ           ams net id    HFILE           §џ           file handle    BUFADDR           §џ           buffer address for write    COUNT           §џ           count of bytes for write    WRITE            §џ           write control input    TMOUT         §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ              COUNT_W           §џ       !    count of bytes actually written             ђ*VW     џџџџ           FW_CALLGENERICFB           fbCall       w    ( 	sNetID := '', nPort := 16#1234,
								bExecute := FALSE, tTimeout := T#0s,
								ACCESSCNT_I := 16#0000BEC1 )     СО                 4                          FW_AdsRdWrt ` §џ                 funGrp         ` §џ       #    Function block group (identifier)    funNum         ` §џ       $    Function block number (identifier)    pWrite               PVOID ` §џ       7    Pointer to generic function input parameter structure    cbWrite         ` §џ       ;    Byte length of generic function input parameter structure    pRead               PVOID ` §џ	           Pointer to output data buffer    cbRead         ` §џ
       #    Byte length of output data buffer       nErrID         ` §џ           0 => no error, <> 0 => error
   cbReturned         ` §џ       ,    Number of successfully returned data bytes             ђ*VW      џџџџ           FW_CALLGENERICFBEX           fbCall        	               FW_CallGenericFb ` §џ              in                  ST_AdsCallGenericFbExReq ` §џ                 funGrp         ` §џ       #    Function block group (identifier)    funNum         ` §џ       $    Function block number (identifier)    nIdxGrp         ` §џ           Index group parameter    nIdxOffs         ` §џ           Index offset parameter    pWrite               PVOID ` §џ	       7    Pointer to generic function input parameter structure    cbWrite         ` §џ
       ;    Byte length of generic function input parameter structure    pRead               PVOID ` §џ           Pointer to output data buffer    cbRead         ` §џ       #    Byte length of output data buffer       nErrID         ` §џ           0 => no error, <> 0 => error
   cbReturned         ` §џ       ,    Number of successfully returned data bytes             ђ*VW      џџџџ           FW_CALLGENERICFUN           fbCall       y    ( 	sNetID := '', nPort := 16#1234,
									bExecute := FALSE, tTimeout := T#0s,
									ACCESSCNT_I := 16#0000BEC2 )     ТО                 4                          FW_AdsRdWrt ` §џ           don't use it!        funGrp         ` §џ           Function group (identifier)    funNum         ` §џ       $    Function block number (identifier)    pWrite               PVOID ` §џ       7    Pointer to generic function input parameter structure    cbWrite         ` §џ       ;    Byte length of generic function input parameter structure    pRead               PVOID ` §џ	           Pointer to output data buffer    cbRead         ` §џ
       #    Byte length of output data buffer    pcbReturned               ` §џ       ,    Number of successfully returned data bytes       FW_CallGenericFun                                     ђ*VW      џџџџ           GETBIT32           dwConst        ` §џ                 inVal32           §џ              bitNo           §џ                 GETBIT32                                      ђ*VW      џџџџ           GETCPUACCOUNT           fbGetCpuAccount               FW_GetCpuAccount ` §џ                     cpuAccountDW           §џ                       ђ*VW      џџџџ           GETCPUCOUNTER           fbGetCpuCounter                FW_GetCpuCounter ` §џ                  
   cpuCntLoDW           §џ           
   cpuCntHiDW           §џ                       ђ*VW      џџџџ           GETCURTASKINDEX           fbGetCurTaskIndex               FW_GetCurTaskIndex ` §џ                     index           §џ           Task index [1..4]             ђ*VW      џџџџ           GETSYSTEMTIME           fbGetSystemTime                FW_GetSystemTime ` §џ                     timeLoDW           §џ              timeHiDW           §џ                       ђ*VW      џџџџ           GETTASKTIME           out   	                       ` §џ	           
   cbReturned         ` §џ
                     timeLoDW           §џ              timeHiDW           §џ                       ђ*VW      џџџџ        	   LPTSIGNAL               PortAddr           §џ              PinNo           §џ              OnOff            §џ	              	   LPTSIGNAL                                      ђ*VW      џџџџ           MEMCMP               pBuf1           §џ           First buffer    pBuf2           §џ           Second buffer    n           §џ           Number of characters       MEMCMP                                     ђ*VW      џџџџ           MEMCPY               destAddr           §џ           New buffer    srcAddr           §џ           Buffer to copy from    n           §џ           Number of characters to copy       MEMCPY                                     ђ*VW      џџџџ           MEMMOVE               destAddr           §џ           New buffer    srcAddr           §џ           Buffer to copy from    n           §џ           Number of characters to copy       MEMMOVE                                     ђ*VW      џџџџ           MEMSET               destAddr           §џ           Pointer to destination    fillByte           §џ           Character to set    n           §џ           Number of characters       MEMSET                                     ђ*VW      џџџџ           ROL32               inVal32           §џ              n           §џ                 ROL32                                     ђ*VW      џџџџ           ROR32               inVal32           §џ              n           §џ                 ROR32                                     ђ*VW      џџџџ           SETBIT32           dwConst        ` §џ                 inVal32           §џ              bitNo           §џ                 SETBIT32                                     ђ*VW      џџџџ           SFCACTIONCONTROL     
      S_FF                 RS    §џ              L_TMR                    TON    §џ              D_TMR                    TON    §џ              P_TRIG                 R_TRIG    §џ              SD_TMR                    TON    §џ              SD_FF                 RS    §џ              DS_FF                 RS    §џ              DS_TMR                    TON    §џ              SL_FF                 RS    §џ              SL_TMR                    TON    §џ           
      N            §џ              R0            §џ              S0            §џ              L            §џ              D            §џ              P            §џ              SD            §џ	              DS            §џ
              SL            §џ              T           §џ                 Q            §џ                       ђ*VW      џџџџ           SHL32               inVal32           §џ              n           §џ                 SHL32                                     ђ*VW      џџџџ           SHR32               inVal32           §џ              n           §џ                 SHR32                                     ђ*VW      џџџџ    t   C:\TWINCAT\PLC\LIB\TcUtilities.lib @                                                                                         ARG_TO_CSVFIELD           pSrc               ` §џ           Pointer to the source buffer    pDest               ` §џ       #    Pointer to the destination buffer    cbMax         ` §џ           Max. number of input bytes    cbScan         ` §џ           Input stream data byte number    cbReturn         ` §џ           Number of result data bytes       in                 T_Arg   §џ       T    Input data in PLC format (any data type, string, integer, floating point value...)    bQM            §џ	       h    TRUE => Enclose result data in quotation marks, FALSE => Don't enclose result data in quotation marks.    pOutput           §џ
       /    Address of output buffer (destination buffer)    cbOutput           §џ       !    Max. byte size of output buffer       ARG_TO_CSVFIELD                                     ЇБV      џџџџ        
   BCD_TO_DEC        
   RisingEdge                 R_TRIG ` §џ                 START            §џ              BIN           §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ	              DOUT           §џ
       П   
	Error codes:
		0x00		: No Errors
		0x0F	: Parameter value NOT correct. Wrong BCD input value in Low Nibble.
		0xF0	: Parameter value NOT correct. Wrong BCD input value in High Nibble.
            ЇБV      џџџџ           BE128_TO_HOST               in                T_UHUGE_INTEGER   §џ                 BE128_TO_HOST                T_UHUGE_INTEGER                             ЇБV      џџџџ           BE16_TO_HOST               in           §џ                 BE16_TO_HOST                                     ЇБV      џџџџ           BE32_TO_HOST           parr    	                            ` §џ                 in           §џ                 BE32_TO_HOST                                     ЇБV      џџџџ           BE64_TO_HOST               in                T_ULARGE_INTEGER   §џ                 BE64_TO_HOST                T_ULARGE_INTEGER                             ЇБV      џџџџ           BYTE_TO_BINSTR           bits   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant bits    iPad            §џ           Number of padding zeros    i            §џ           	   Index7001                            in           §џ           BYTE input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       BYTE_TO_BINSTR               T_MaxString                             ЇБV      џџџџ           BYTE_TO_DECSTR           dec   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant decades    iPad            §џ           Number of padding zeros    i            §џ              digit            §џ           	   Index7001                            in           §џ           BYTE input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       BYTE_TO_DECSTR               T_MaxString                             ЇБV      џџџџ           BYTE_TO_HEXSTR           hex   	                          §џ       6    array of ASCII characters (inclusive null delimiter)    iSig            §џ           number of significant nibbles    iPad            §џ           number of padding zeros    i            §џ           	   Index7001                            in           §џ           BYTE input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.    bLoCase            §џ	       8   Default: use "ABCDEF", if TRUE use "abcdef" characters.       BYTE_TO_HEXSTR               T_MaxString                             ЇБV      џџџџ           BYTE_TO_LREALEX               in           §џ                 BYTE_TO_LREALEX                                                  ЇБV      џџџџ           BYTE_TO_OCTSTR           oct   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant nibbles    iPad            §џ           Number of padding zeros    i            §џ           	   Index7001                            in           §џ           BYTE input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       BYTE_TO_OCTSTR               T_MaxString                             ЇБV      џџџџ           BYTEARR_TO_MAXSTRING               in   	  џ                       §џ                 BYTEARR_TO_MAXSTRING               T_MaxString                             ЇБV     џџџџ           CSVFIELD_TO_ARG           pSrc               ` §џ           Pointer to the source buffer    pDest               ` §џ       $     Pointer to the destination buffer    cbMax         ` §џ           Max. number of output bytes    cbScan         ` §џ           Input stream data byte number    cbReturn         ` §џ           Number of result data bytes    bQMPrior          ` §џ       c    TRUE => Previous character was quotation mark. FALSE => Previous character was not quotation mark       pInput           §џ       G    Address of input buffer with data in CSV field format (source buffer )   cbInput           §џ	           Byte size of input data    bQM            §џ
       \    TRUE => Remove enclosing quotation marks. FALSE => Don't remove enclosing quotation marks.    out                 T_Arg   §џ       U    Output data in PLC format (any data type, string, integer, floating point value...)       CSVFIELD_TO_ARG                                     ЇБV      џџџџ           CSVFIELD_TO_STRING           cbField         ` §џ                 in               T_MaxString   §џ       "    Input string in CSV field format    bQM            §џ	       \    TRUE => Remove enclosing quotation marks. FALSE => Don't remove enclosing quotation marks.       CSVFIELD_TO_STRING               T_MaxString                             ЇБV      џџџџ           DATA_TO_HEXSTR           iCase         ` §џ              pCells    	  џ                          ` §џ              idx         ` §џ                 pData           §џ           Pointer to data buffer    cbData             U              §џ           Byte size of data buffer    bLoCase            §џ       9    Default: use "ABCDEF", if TRUE use "abcdef" characters.       DATA_TO_HEXSTR               T_MaxString                             ЇБV     џџџџ        
   DCF77_TIME     "      DataBits   	  <                         §џ              BitNo            §џ              dtCurr            §џ              dtNext            §џ              tziCurr               E_TimeZoneID    §џ       6    Time zone information extracted from latest telegram    tziPrev               E_TimeZoneID    §џ       8    Time zone information extracted from previous telegram    tDiff            §џ           Two telegram time difference    bCheck             §џ              Step         ` §џ!           	   StartEdge                 R_TRIG ` §џ"              RisingPulse                 R_TRIG ` §џ$              FallingPulse                 F_TRIG ` §џ%           	   LongPulse                    TON ` §џ&           
   ShortPulse                    TON ` §џ'           
   DetectSync                    TOF ` §џ(              NoDCFSignal                    TON ` §џ)              DCFCycleLen                    TON ` §џ*           	   bIsRising          ` §џ,           
   bIsFalling          ` §џ-              bIsLong          ` §џ.              bIsShort          ` §џ/              Working          ` §џ0           	   DataValid          ` §џ2           
   ParitySum1         ` §џ3           
   ParitySum2         ` §џ4           
   ParitySum3         ` §џ5              i         ` §џ7           	   DummyByte         ` §џ8              DummyString    Q       Q  ` §џ9              Hour         ` §џ;              Minute         ` §џ<              Year         ` §џ=              Month         ` §џ>              Day         ` §џ?              	   DCF_PULSE            §џ           DCF77 pulse: 101010101...    RUN            §џ       *    Enable/disable function block execution.       BUSY            §џ           TRUE = Decoding in progress    ERR            §џ	       ,    Error flag: TRUE = Error, FALSE = No error    ERRID           §џ
           Error code    ERRCNT           §џ           Error counter    READY            §џ       1    TRUE => CDT is valid, FALSE => CDT is not valid    CDT           §џ           date and time information             ЇБV      џџџџ           DCF77_TIME_EX     #      DataBits   	  <                         §џ           Decoded data bits    BitNo            §џ           Decoded bit number    dtCurr            §џ       %    Time extracted from latest telegram    dtNext            §џ            Supposed next time    tziCurr               E_TimeZoneID    §џ!       6    Time zone information extracted from latest telegram    tziPrev               E_TimeZoneID    §џ"       8    Time zone information extracted from previous telegram    tDiff            §џ#       )    Time difference of two latest telegrams    bCheck             §џ$       H    TRUE = Plausibility check over two telegrams enabled, FALSE = disabled    Step         ` §џ&           	   StartEdge                 R_TRIG ` §џ'              RisingPulse                 R_TRIG ` §џ)              FallingPulse                 F_TRIG ` §џ*           	   LongPulse                    TON ` §џ+           
   ShortPulse                    TON ` §џ,           
   DetectSync                    TOF ` §џ-              NoDCFSignal                    TON ` §џ.              DCFCycleLen                    TON ` §џ/           	   bIsRising          ` §џ1           
   bIsFalling          ` §џ2              bIsLong          ` §џ3              bIsShort          ` §џ4              Working          ` §џ5           	   DataValid          ` §џ7           
   ParitySum1         ` §џ8           
   ParitySum2         ` §џ9           
   ParitySum3         ` §џ:              i         ` §џ<           	   DummyByte         ` §џ=              DummyString    Q       Q  ` §џ>              Hour         ` §џ@              Minute         ` §џA              Year         ` §џB              Month         ` §џC              Day         ` §џD           	   DayOfWeek         ` §џE              	   DCF_PULSE            §џ           DCF77 pulse: 101010101...    RUN            §џ       *    Enable/disable function block execution.    TLP          §џ           Short/long pulse split point       BUSY            §џ	           TRUE = Decoding in progress    ERR            §џ
       ,    Error flag: TRUE = Error, FALSE = No error    ERRID           §џ           Error code    ERRCNT           §џ           Error counter    READY            §џ       1    TRUE => CDT is valid, FALSE => CDT is not valid    CDT           §џ           date and time information    DOW                         §џ       0     ISO 8601 day of week: 1 = Monday.. 7 = Sunday    TZI               E_TimeZoneID   §џ           time zone information    ADVTZI            §џ       1    MEZ->MESZ or MESZ->MEZ time change notification    LEAPSEC            §џ           TRUE = Leap second    RAWDT   	  <                        §џ           Raw decoded data bits             ЇБV      џџџџ        
   DEC_TO_BCD        
   RisingEdge                 R_TRIG ` §џ                 START            §џ              DIN           §џ                 BUSY            §џ              ERR            §џ              ERRID           §џ	              BOUT           §џ
       h   
	Error codes:
		0x00		: No errors
		0xFF	: Parameter value NOT correct. Wrong DECIMAL input value.
            ЇБV      џџџџ        
   DEG_TO_RAD               ANGLE                        §џ              
   DEG_TO_RAD                                                  ЇБV      џџџџ           DINT_TO_DECSTR               in           §џ           
   iPrecision           §џ	                 DINT_TO_DECSTR               T_MaxString                             ЇБV      џџџџ           DT_TO_FILETIME           ui64                T_ULARGE_INTEGER ` §џ                 DTIN           §џ                 DT_TO_FILETIME             
   T_FILETIME                             ЇБV      џџџџ           DT_TO_SYSTEMTIME           sDT             ` §џ              nDay         ` §џ              b   	                 
    24(16#30)      0    ` §џ              TS                   
   TIMESTRUCT ` §џ           	   Index7001                            DTIN           §џ                 DT_TO_SYSTEMTIME                   
   TIMESTRUCT                             ЇБV      џџџџ           DWORD_TO_BINSTR           bits   	                        ` §џ       6    array of ASCII characters (inclusive null delimiter)    iSig         ` §џ           number of significant bits    iPad         ` §џ           number of padding zeros    i         ` §џ           	   Index7001                            in           §џ           
   iPrecision           §џ                 DWORD_TO_BINSTR               T_MaxString                             ЇБV      џџџџ           DWORD_TO_DECSTR           dec   	                       ` §џ       6    array of ASCII characters (inclusive null delimiter)    iSig         ` §џ           number of significant nibbles    iPad         ` §џ           number of padding zeros    i         ` §џ              divider     Ъ; ` §џ              number         ` §џ           	   Index7001                            in           §џ           
   iPrecision           §џ                 DWORD_TO_DECSTR               T_MaxString                             ЇБV      џџџџ           DWORD_TO_HEXSTR           hex   	                       ` §џ       6    array of ASCII characters (inclusive null delimiter)    iSig         ` §џ           number of significant nibbles    iPad         ` §џ           number of padding zeros    i         ` §џ           	   Index7001                            in           §џ           
   iPrecision           §џ              bLoCase            §џ	       8   Default: use "ABCDEF", if TRUE use "abcdef" characters.       DWORD_TO_HEXSTR               T_MaxString                             ЇБV      џџџџ           DWORD_TO_LREALEX               in           §џ                 DWORD_TO_LREALEX                                                  ЇБV      џџџџ           DWORD_TO_OCTSTR           oct   	                       ` §џ       6    array of ASCII characters (inclusive null delimiter)    iSig         ` §џ           number of significant nibbles    iPad         ` §џ           number of padding zeros    i         ` §џ           	   Index7001                            in           §џ           
   iPrecision           §џ                 DWORD_TO_OCTSTR               T_MaxString                             ЇБV      џџџџ           F_ARGCMP               typeSafe            §џ              arg1                 T_Arg   §џ              arg2                 T_Arg   §џ                 F_ARGCMP                                     ЇБV      џџџџ           F_ARGCPY               typeSafe            §џ                 F_ARGCPY                               dest                  T_Arg  §џ
              src                  T_Arg  §џ                   ЇБV      џџџџ           F_ARGISZERO               arg                 T_Arg   §џ                 F_ARGIsZero                                      ЇБV      џџџџ        	   F_BIGTYPE               pData           §џ            Address pointer of data buffer    cbLen           §џ           Byte length of data buffer    	   F_BIGTYPE                 T_Arg                             ЇБV      џџџџ           F_BOOL                   F_BOOL                 T_Arg                       in            §џ                   ЇБV      џџџџ           F_BYTE                   F_BYTE                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_BYTE_TO_CRC16_CCITT               value           §џ           Data value    crc           §џ       >    Initial value (16#FFFF or 16#0000) or previous CRC-16 result       F_BYTE_TO_CRC16_CCITT                                     ЇБV      џџџџ           F_CHECKSUM16        	   wChkSum_I         ` §џ	       %    internal ChkSum                        dataWord         ` §џ
       %    current data byte                      iIdx         ` §џ       %    current data buffer index              ptrData               ` §џ       %    pointer to current data byte           	   dwSrcAddr           §џ       %    address of data buffer                 cbLen           §џ       %    length of data buffer                  wChkSum           §џ       %    init value (16#0000) or last ChkSum       F_CheckSum16                                     ЇБV      џџџџ           F_CRC16_CCITT           wCRC_I         ` §џ
       $    internal CRC                          dataWord         ` §џ       $    current data byte                     iIdx         ` §џ       $    current data buffer index             ptrData               ` §џ       $    pointer to current data byte          	   dwSrcAddr           §џ       $    address of data buffer                cbLen           §џ       $    length of data buffer                 wLastCRC           §џ       $    init value (16#FFFF) or last CRC16       F_CRC16_CCITT                                     ЇБV      џџџџ           F_CREATEHASHTABLEHND           p                     T_HashTableEntry      ` §џ              i         ` §џ                 pEntries                     T_HashTableEntry        §џ       C    Pointer to the first entry of hash table database (element array) 	   cbEntries           §џ       ;    Byte size (length) of hash table database (element array)       F_CreateHashTableHnd                                hTable         	               T_HHASHTABLE  §џ           Hash table handle         ЇБV      џџџџ           F_CREATELINKEDLISTHND           p                   T_LinkedListEntry      ` §џ           Temp. previous node    n                   T_LinkedListEntry      ` §џ           Temp. next node    i         ` §џ           loop iterator       pEntries                   T_LinkedListEntry        §џ       @    Pointer to the first linked list node database (element array) 	   cbEntries           §џ       <    Byte size (length) of linked list database (element array)       F_CreateLinkedListHnd                                hList         	               T_HLINKEDLIST  §џ           Linked list handle         ЇБV      џџџџ           F_DATA_TO_CRC16_CCITT           i         ` §џ                 pData           §џ           Pointer to data    cbData           §џ           Length of data    crc           §џ       >    Initial value (16#FFFF or 16#0000) or previous CRC-16 result       F_DATA_TO_CRC16_CCITT                                     ЇБV      џџџџ           F_DINT                   F_DINT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_DWORD                   F_DWORD                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_FORMATARGTOSTR     	      pOut               ` §џ              longword         ` §џ              double                      ` §џ              single          ` §џ              short         ` §џ              small         ` §џ              longint         ` §џ              iPaddingLen         ` §џ              iCurrLen         ` §џ           
      bSign            §џ           Sign prefix flag    bBlank            §џ           Blank prefix flag    bNull            §џ           Null prefix flag    bHash            §џ           Hash prefix flag    bLAlign            §џ       4    FALSE => Right align (default), TRUE => Left align    bWidth            §џ       C    FALSE => no width padding, TRUE => blank or zeros padding enabled    iWidth           §џ	           Width length parameter 
   iPrecision           §џ
           Precision length parameter    eFmtType               E_TypeFieldParam   §џ           Format type field parameter    arg                 T_Arg   §џ           Format argument       F_FormatArgToStr                               sOut                T_MaxString  §џ                   ЇБV      џџџџ           F_GETDAYOFMONTHEX           dom         ` §џ           Day of month    dow         ` §џ           Day of week    n         ` §џ              dwYears         ` §џ              dwDays         ` §џ                 wYear     A  A  kx             §џ           Year: 1601..30827    wMonth                         §џ           Month: 1..12    wWOM                         §џ       Г     Week of month. Occurrence of the day of the week within the month (1..5, where 5 indicates the final occurrence during the month if that day of the week does not occur 5 times).   wDOW                           §џ       4    Day of week (0 = Sunday, 1 = Monday.. 6 = Saturday       F_GetDayOfMonthEx                                     ЇБV      џџџџ           F_GETDAYOFWEEK           sysTime                   
   TIMESTRUCT ` §џ	                 in           §џ                 F_GetDayOfWeek                                     ЇБV      џџџџ           F_GETDOYOFYEARMONTHDAY           bLY          ` §џ
                 wYear           §џ           Year: 0..2xxx    wMonth           §џ           Month 1..12    wDay           §џ           Day: 1..31       F_GetDOYOfYearMonthDay                                     ЇБV      џџџџ           F_GETFLOATREC     
   	   ptrDouble    	                               §џ              fValue                         §џ
              fBegin                         §џ              nBegin            §џ              fDiv                         §џ              nDig            §џ              nDigit            §џ              fMaxPrecision                         §џ              i            §џ              nLastDecDigit            §џ                 fVal                        §џ           
   iPrecision           §џ              bRound            §џ                 F_GetFloatRec              
   T_FloatRec                             ЇБV      џџџџ           F_GETMAXMONTHDAYS               wYear           §џ	              wMonth           §џ
                 F_GetMaxMonthDays                                     ЇБV      џџџџ           F_GETMONTHOFDOY           bLY          ` §џ	              wMonth         ` §џ
                 wYear           §џ           Year: 0..2xxx    wDOY           §џ           Year's day number: 1..366       F_GetMonthOfDOY                                     ЇБV      џџџџ           F_GETVERSIONTCUTILITIES               nVersionElement           §џ       d   
	Possible nVersionElement parameter:
	1	:	major number
	2	:	minor number
	3	:	revision number
      F_GetVersionTcUtilities                                     ЇБV      џџџџ           F_GETWEEKOFTHEYEAR           date_sec         ` §џ           date seconds    dow         ` §џ	           day of week    year         ` §џ
              KWStart         ` §џ              first    yG ` §џ              ff                      ` §џ                 in           §џ                 F_GetWeekOfTheYear                                     ЇБV      џџџџ           F_HUGE                   F_HUGE                 T_Arg                       in                 T_HUGE_INTEGER  §џ                   ЇБV      џџџџ           F_INT                   F_INT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_LARGE                   F_LARGE                 T_Arg                       in                 T_LARGE_INTEGER  §џ                   ЇБV      џџџџ           F_LREAL                   F_LREAL                 T_Arg                       in                        §џ                   ЇБV      џџџџ           F_LTRIM           pChar               ` §џ              pStr                T_MaxString      ` §џ	                 in               T_MaxString   §џ                 F_LTrim               T_MaxString                             ЇБV      џџџџ           F_PVOID                   F_PVOID                 T_Arg                       in                PVOID  §џ                   ЇБV      џџџџ           F_REAL                   F_REAL                 T_Arg                       in            §џ                   ЇБV      џџџџ           F_RTRIM           n         ` §џ              pChar               ` §џ	                 in               T_MaxString   §џ                 F_RTrim               T_MaxString                             ЇБV      џџџџ           F_SINT                   F_SINT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_STRING                   F_STRING                 T_Arg                       in                T_MaxString  §џ                   ЇБV      џџџџ        
   F_SWAPREAL           pReal    	                               §џ              pResult    	                               §џ                 fVal            §џ              
   F_SwapReal                                      ЇБV      џџџџ           F_SWAPREALEX           pIN    	                            ` §џ              wSave         ` §џ	                     F_SwapRealEx                                fVal            §џ                   ЇБV      џџџџ        	   F_TOLCASE           pDest               ` §џ              idx                 MIN_SBCS_TABLE   MAX_SBCS_TABLE ` §џ	                 in               T_MaxString   §џ              	   F_ToLCase               T_MaxString                             ЇБV     џџџџ        	   F_TOUCASE           pDest               ` §џ              idx                 MIN_SBCS_TABLE   MAX_SBCS_TABLE ` §џ	                 in               T_MaxString   §џ              	   F_ToUCase               T_MaxString                             ЇБV     џџџџ           F_TRANSLATEFILETIMEBIAS           ui64In                T_ULARGE_INTEGER ` §џ       E    Input file time as 64 bit unsigned integer (number of 100ns ticks)     ui64Bias                T_ULARGE_INTEGER ` §џ       ?    Bias value as 64 bit unsigned integer (number of 100ns ticks)    ui64Out                T_ULARGE_INTEGER ` §џ       @    Local time as 64 bit unsigned integer (number of 100ns ticks)        in             
   T_FILETIME   §џ       1    Input time in file time format to be translated    bias           §џ       y    Bias value in minutes. The bias is the difference, in minutes, between Coordinated Universal Time (UTC) and local time.    toUTC            §џ       U    TRUE => Translate from local time to UTC, FALSE => Translate from UTC to local Time       F_TranslateFileTimeBias             
   T_FILETIME                             ЇБV      џџџџ           F_UDINT                   F_UDINT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_UHUGE                   F_UHUGE                 T_Arg                       in                 T_UHUGE_INTEGER  §џ                   ЇБV      џџџџ           F_UINT                   F_UINT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_ULARGE                   F_ULARGE                 T_Arg                       in                 T_ULARGE_INTEGER  §џ                   ЇБV      џџџџ           F_USINT                   F_USINT                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_WORD                   F_WORD                 T_Arg                       in           §џ                   ЇБV      џџџџ           F_YEARISLEAPYEAR               wYear           §џ                 F_YearIsLeapYear                                      ЇБV      џџџџ           FB_ADDROUTEENTRY        
   fbAdsWrite       P    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_ADDREMOTE, IDXOFFS := 0 )              	   T_AmsPort         !                 ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ           	   dataEntry                ST_AmsRouteSystemEntry ` §џ                 sNetID            
   T_AmsNetID   §џ       &    TwinCAT network address (ams net id)    stRoute                    ST_AmsRouteEntry   §џ       !    Structure with route parameters    bExecute            §џ       -    Rising edge starts function block execution    tTimeout         §џ           Max fb execution time       bBusy            §џ
              bError            §џ              nErrID           §џ                       ЇБV     џџџџ           FB_AMSLOGGER        
   fbAdsWrite       [    ( PORT:= AMSPORT_AMSLOGGER, IDXGRP:= AMSLOGGER_IGR_GENERAL, IDXOFFS:= AMSLOGGER_IOF_MODE )              	   T_AmsPort                          ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              stReq                ST_AmsLoggerReq ` §џ                 sNetId           ''    
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    eMode           AMSLOGGER_RUN       E_AmsLoggerMode   §џ              sCfgFilePath           ''       T_MaxString   §џ              bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ
              bError            §џ              nErrId           §џ                       ЇБV     џџџџ           FB_BASICPID           nERR_NOERROR           §џ           no error						   nERR_INVALIDPARAM          §џ           invalid parameter				   nERR_INVALIDCYCLETIME          §џ           invalid cycle time				   fE               0.0            §џ            error input					   fE_1               0.0            §џ!           error input z^(-1)				   fY               0.0            §џ"           control output				   fY_1               0.0            §џ#           control output  z^(-1)			   fYP               0.0            §џ$           P-part						   fYI               0.0            §џ%           I-part						   fYI_1               0.0            §џ&           I-part  z^(-1)					   fYD               0.0            §џ'           D-T1-part					   fYD_1               0.0            §џ(           D-T1-part  z^(-1)				   bInit            §џ*       %    initialization flag for first cycle	   bIsIPart             §џ+           I-part active ?				   bIsDPart             §џ,           D-part active ?				   fDi               0.0            §џ.           internal I param				   fDd               0.0            §џ/           internal D param				   fCd               0.0            §џ0           internal D param				   fCtrlCycleTimeOld               0.0            §џ2              fKpOld               0.0            §џ3              fTnOld               0.0            §џ4              fTvOld               0.0            §џ5              fTdOld               0.0            §џ6                 fSetpointValue                        §џ           setpoint value							   fActualValue                        §џ           actual value							   bReset            §џ           controller values    fCtrlCycleTime                        §џ	       (    controller cycle time in seconds [s]			   fKp                        §џ           proportional gain Kp	(P)					   fTn                        §џ           integral gain Tn (I) [s]						   fTv                        §џ       "    derivative gain Tv (D-T1) [s]				   fTd                        §џ       (    derivative damping time Td (D-T1) [s]		      fCtrlOutput                        §џ           controller output command				   nErrorStatus           §џ       1    controller error output (0: no error; >0:error)	            ЇБV      џџџџ           FB_BUFFEREDTEXTFILEWRITER           fbFile                               FB_TextFileRingBuffer ` §џ           
   closeTimer                    TON ` §џ           auto close timer    bRemove          ` §џ              nStep         ` §џ                 sNetId           ''    
   T_AmsNetId ` §џ           ams net id 	   sPathName           'c:\Temp\data.dat'       T_MaxString ` §џ	       6    file buffer path name (max. length = 255 characters)    ePath           PATH_GENERIC    
   E_OpenPath ` §џ
           default: Open generic file    bAppend         ` §џ       )    TRUE = append lines, FALSE = not append 
   tAutoClose       ` §џ              tTimeout       ` §џ                 bBusy          ` §џ              bError          ` §џ              nErrID         ` §џ                 fbBuffer                 FB_StringRingBuffer` §џ           string ring buffer         ЇБV     џџџџ           FB_CONNECTSCOPESERVER           stRecordDesc       d    (nRunMode:=0, nSopMode:=0, bStoreOnDisk:=FALSE, bUseLocalServer:=FALSE, bStartServerFromFile:=TRUE)                                #   ST_ScopeServerRecordModeDescription    §џ              nState            §џ              nErrorState            §џ           
   fbAdsWrite                          ADSWRITE    §џ              fbQueryRegistry                                 FB_RegQueryValue    §џ              sScopeServerDir                §џ              sScopeServerPath                §џ              fbStartServer                              NT_StartProcess    §џ              fbWait                    TON    §џ               bTriggerServerStart            §џ!              nDwellTimeCounter            §џ"              nPort           27110    	   T_AmsPort   §џ%              Connect_IdxGrp     u     §џ&          0x7500      sNetId           ''    
   T_AmsNetId   §џ              bExecute            §џ              sConfigFile    Q       Q    §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ              bError            §џ              nErrorId           §џ                       ЇБV     џџџџ           FB_CSVMEMBUFFERREADER           state         ` §џ              getBufferIndex         ` §џ              scanPtr               ` §џ              scanSize         ` §џ              bField          ` §џ              cbCopied         ` §џ           
   bFirstChar          ` §џ              bDQField          ` §џ           	   bDQBefore          ` §џ              pField         ` §џ       U    If successfull then this variable returns the address of the first/next field value    cbField         ` §џ       W    If successfull then this variable returns the byte size of the first/next field value    bEOF          ` §џ           TRUE => End of field found    bBreak          ` §џ                 eCmd           eEnumCmd_First       E_EnumCmdType   §џ       )    Command type: read first or next field ?   pBuffer           §џ       #    Address ( pointer) of data buffer    cbBuffer           §џ           Max. byte size of data buffer       bOk            §џ	       &    TRUE => Successfull, FALSE => Failed    getValue           ''       T_MaxString   §џ
       N    If successfull then this output returns the first/next field value as string    pValue           §џ       s    Pointer to the first value byte (HINT: String values are not null terminated. Empty string returns Null pointer )    cbValue           §џ           Field value byte size    bCRLF            §џ       .    TRUE => End of record separator found (CRLF)    cbRead           §џ       )    Number of successfully parse data bytes             ЇБV      џџџџ           FB_CSVMEMBUFFERWRITER           fbReader                                    FB_CSVMemBufferReader ` §џ              temp   	  ,                    ` §џ           Temp buffer    cbTemp         ` §џ       %    Number of data bytes in temp buffer    cbCopied         ` §џ       9    Number of data bytes copied to the external data buffer    bNewLine         ` §џ           TRUE => start with new line       eCmd           eEnumCmd_First       E_EnumCmdType   §џ       *    Command type: write first or next field ?   putValue           ''       T_MaxString   §џ       &    New first/next field value as string    pValue           §џ       C    OPTIONAL: Pointer to external buffer containing field value data.    cbValue           §џ       F    OPTIONAL: Byte size of external buffer containing field value data.     bCRLF            §џ       0    TRUE = > Append end of record separator (CRLF)    pBuffer           §џ	       #    Address ( pointer) of data buffer    cbBuffer           §џ
           Max. byte size of data buffer       bOk            §џ       &    TRUE => Successfull, FALSE => Failed    cbSize           §џ           Number fo used data bytes    cbFree           §џ           Number of free data bytes    nFields           §џ           Number of fields    nRecords           §џ           Number of records    cbWrite           §џ       +    Number of successfully written data bytes             ЇБV      џџџџ           FB_DBGOUTPUTCTRL           fbFormat                                     FB_FormatString ` §џ              fbBuffer        	               FB_StringRingBuffer ` §џ              fbFile       +    (ePath := PATH_BOOTPATH, bAppend := TRUE )                 PATH_GENERIC    
   E_OpenPath                      FB_BufferedTextFileWriter ` §џ              buffer   	  '                   ` §џ              state         ` §џ              nItems         ` §џ              k         ` §џ               bInit         ` §џ!           Hex logging    i         ` §џ$              cells   	                               ` §џ%              pCells                T_MaxString      ` §џ&              cbL1         ` §џ'              cbL2         ` §џ'              idx         ` §џ'              pSrc1               ` §џ(              pSrc2               ` §џ(                 dwCtrl         ` §џ       &    Debug message target: DBG_OUTPUT_LOG    sFormat           ''       T_MaxString ` §џ           Debug message format string    arg1                 T_Arg ` §џ           Format string argument    arg2                 T_Arg ` §џ              arg3                 T_Arg ` §џ	              arg4                 T_Arg ` §џ
              arg5                 T_Arg ` §џ              arg6                 T_Arg ` §џ              arg7                 T_Arg ` §џ              arg8                 T_Arg ` §џ              arg9                 T_Arg ` §џ              arg10                 T_Arg ` §џ              sFilter           ''       T_MaxString ` §џ                 bError          ` §џ              nError         ` §џ           	   nOverflow         ` §џ                       ЇБV     џџџџ           FB_DISCONNECTSCOPESERVER        
   fbAdsWrite                          ADSWRITE    §џ              nState            §џ                 sNetId            
   T_AmsNetId   §џ              bExecute            §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ	              bError            §џ
              nErrorId           §џ                       ЇБV     џџџџ           FB_ENUMFINDFILEENTRY        
   fbAdsRdWrt       B    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_FFILEFIND )              	   T_AmsPort                         ADSRDWRT ` §џ           
   fbAdsWrite       D    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_CLOSEHANDLE )              	   T_AmsPort         o              ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ           	   dataEntry                            ST_AmsFindFileSystemEntry ` §џ              eFindCmd               E_EnumCmdType ` §џ                 sNetId            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id) 	   sPathName               T_MaxString   §џ       %    dir/path/file name, wildcards [*|?]    eCmd           eEnumCmd_First       E_EnumCmdType   §џ           Enumerator navigation command    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ              bError            §џ              nErrID           §џ              bEOE            §џ           End of enumeration 
   stFindFile                     ST_FindFileEntry   §џ           Find file entry             ЇБV     џџџџ           FB_ENUMFINDFILELIST           fbEnum                              FB_EnumFindFileEntry ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              cbEntry         ` §џ              nEntries         ` §џ              pEntry                      ST_FindFileEntry      ` §џ                 sNetId            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id) 	   sPathName               T_MaxString   §џ       %    dir/path/file name, wildcards [*|?]    eCmd           eEnumCmd_First       E_EnumCmdType   §џ           Enumerator navigation command 	   pFindList           §џ       &    POINTER TO ARRAY OF ST_FindFileEntry 
   cbFindList           §џ       (    Byte size of ARRAY OF ST_FindFileEntry    bExecute            §џ	       6    Rising edge on this input activates the fb execution    tTimeout         §џ
           Max fb execution time       bBusy            §џ              bError            §џ              nErrID           §џ              bEOE            §џ           End of enumeration 
   nFindFiles           §џ           Number of find files             ЇБV     џџџџ           FB_ENUMROUTEENTRY        	   fbAdsRead       Z    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_ENUMREMOTE (*, IDXGRP := index *) )              	   T_AmsPort         #             ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              index    џџџџ ` §џ           	   dataEntry                ST_AmsRouteSystemEntry ` §џ                 sNetID            
   T_AmsNetID   §џ       '    TwinCAT network address (ams net id )    eCmd           eEnumCmd_First       E_EnumCmdType   §џ           Enumerator navigation command    bExecute            §џ       -    Rising edge starts function block execution    tTimeout         §џ           Max fb execution time       bBusy            §џ
              bError            §џ              nErrID           §џ              bEOE            §џ       l    End of enumeration. This value is TRUE after the first read that attempts to read next non existing entry.    stRoute                    ST_AmsRouteEntry   §џ       !    Structure with route parameters             ЇБV     џџџџ           FB_ENUMSTRINGNUMBERS           pSrc               ` §џ              pDest               ` §џ              pNext               ` §џ              char         ` §џ              state         ` §џ              bEat          ` §џ                 sSearch               T_MaxString   §џ           Source string    eCmd           eEnumCmd_First       E_EnumCmdType   §џ           Enumerator navigation command    eType           eNumGroup_Float       E_NumGroupTypes   §џ           String number format type       sNumber               T_MaxString   §џ           Found string number    nPos           §џ	       )    <> 0 => Next scan/search start position    bEOS            §џ
           TRUE = End of string             ЇБV      џџџџ           FB_FILERINGBUFFER           fbOpen                             FB_FileOpen ` §џ              fbClose                      FB_FileClose ` §џ              fbWrite                           FB_FileWrite ` §џ              fbRead                            FB_FileRead ` §џ               fbSeek                         FB_FileSeek ` §џ!              nStep         ` §џ"       X    0=idle, 1=init, 10,11=open, 20,21=seek, 30,31=read, 40,41=write, 50,51=close, 100=exit    bInit          ` §џ#       5    TRUE=reading length chunk, FALSE=reading data chunk    bExit          ` §џ$       O    FALSE=repeat reading/writing, TRUE=abort reading/writing, go to the exit step    bReopen          ` §џ%       t    Open mode: TRUE=try to open existing file, FALSE=create new file, if open fails => try to create and open new file    bOpen          ` §џ&       %    TRUE=file opened, FALSE=file closed    bGet          ` §џ'       $    TRUE=get entry, FALSE=remove entry    bOW          ` §џ(       b    TRUE=removing oldest entry (bOverwrite=TRUE), FALSE=don't remove oldest entry (bOverwrite=FALSE)    cbOW         ` §џ)       /    Temp length of ovwerwritten length/data chunk    cbMoved         ` §џ*       =    Number of successfully read/written length/data chunk bytes    ptrSaved         ` §џ+       M    Seek pointer previous position (used by A_GetHead or read buffer underflow)    ptrMax         ` §џ,       D    Seek pointer max. position = SIZEOF(ring buffer header) + cbBuffer    eCmd           eFileRBuffer_None       E_FileRBufferCmd ` §џ-              eOldCmd           eFileRBuffer_None       E_FileRBufferCmd ` §џ.                 sNetId           ''    
   T_AmsNetId   §џ           ams net id 	   sPathName           'c:\Temp\data.dat'       T_MaxString   §џ       6    file buffer path name (max. length = 255 characters)    ePath           PATH_GENERIC    
   E_OpenPath   §џ           default: Open generic file    nID           §џ           user defined version ID    cbBuffer          §џ           max. file buffer byte size 
   bOverwrite            §џ	       :    FALSE = don't overwrite, TRUE = overwrite oldest entries 
   pWriteBuff           §џ
       "    pointer to external write buffer 
   cbWriteLen           §џ       $    byte size of external write buffer 	   pReadBuff           §џ       !    pointer to external read buffer 	   cbReadLen           §џ       #    byte size of external read buffer    tTimeout         §џ                 bBusy            §џ              bError            §џ              nErrID           §џ       х    ADS or function specific error codes:
	16#8000	= (File) buffer empty or overflow 
	16#8001 = (Application) buffer underflow (cbReadLen to small),  	
	16#8002	= Buffer is not opened  
	16#8003	= Invalid input parameter value    cbReturn           §џ       !    number of recend read data bytes   stHeader                          ST_FileRBufferHead   §џ           buffer status             ЇБV     џџџџ            FB_FILETIMETOTZSPECIFICLOCALTIME           fbBase       !    ( wStdYear := 0, wDldYear := 0 )                                   "   FB_TranslateUtcToLocalTimeByZoneID ` §џ           Underlaid base function block       in             
   T_FILETIME   §џ           Time to be converted (UTC, file time format), 64-bit value representing the number of 100-nanosecond intervals since January 1, 1601   tzInfo                     ST_TimeZoneInformation   §џ           Time zone settings       out             
   T_FILETIME   §џ       *    Converted time in local file time format    eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ	       "    Daylight saving time information    bB            §џ
            FALSE => A time, TRUE => B time            ЇБV      џџџџ           FB_FORMATSTRING     	      pFormat               ` §џ           pointer to the format string    pOut               ` §џ           pointer to the result string 
   iRemOutLen         ` §џ       $    Max remaining length of sOut buffer   bValid          ` §џ       8    if set, the string character is valid format parameter    stFmt                              ST_FormatParameters ` §џ           
   nArrayElem         ` §џ           	   nArgument         ` §џ              parArgs   	  
                     T_Arg              ` §џ              sArgStr               T_MaxString ` §џ                 sFormat               T_MaxString   §џ              arg1                 T_Arg   §џ              arg2                 T_Arg   §џ              arg3                 T_Arg   §џ              arg4                 T_Arg   §џ              arg5                 T_Arg   §џ              arg6                 T_Arg   §џ	              arg7                 T_Arg   §џ
              arg8                 T_Arg   §џ              arg9                 T_Arg   §џ              arg10                 T_Arg   §џ                 bError            §џ              nErrId           §џ              sOut               T_MaxString   §џ                       ЇБV     џџџџ           FB_GETADAPTERSINFO     
   	   fbAdsRead       f    ( PORT:=AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_IPHELPERAPI, IDXOFFS:= IPHELPERAPI_ADAPTERSINFO )              	   T_AmsPort         Н                ADSREAD ` §џ           
   fbRegQuery       P    ( sSubKey:= '\Software\Beckhoff\TwinCAT\Remote', sValName := 'DefaultAdapter' )                        T_MaxString                    T_MaxString                   FB_RegQueryValue ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              cbInfo         ` §џ              idx         ` §џ              info   	                                      ST_IP_ADAPTER_INFO         ` §џ           buffer for 12 entries    pInfo                                 ST_IP_ADAPTER_INFO      ` §џ           
   nRealCount         ` §џ           	   sDefaultA               T_MaxString ` §џ                 sNetID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ	              bError            §џ
              nErrID           §џ              arrAdapters   	                                    ST_IpAdapterInfo           §џ              nCount           §џ           Max. number of found adapters    nGet           §џ       %    Number of read adapter info entries             ЇБV     џџџџ           FB_GETDEVICEIDENTIFICATION        	   iDataSize       @` §џ           
   byTagStart    <    ` §џ           '<'    byTagEnd    >    ` §џ           '>' 
   byTagSlash    /    ` §џ           '/' 	   fbAdsRead                          ADSREAD ` §џ              bExecutePrev          ` §џ              iState         ` §џ              iData   	                      ` §џ           
   strActPath             ` §џ              iLoopEndIdx         ` §џ              iStructSize         ` §џ              strHardwareCPU             ` §џ              strTags   	  	        )       )          ` §џ           	   iTagsSize   	  	                     ` §џ              iCurrTag   	  (                     ` §џ               iCurrTagData   	  P                     ` §џ!           	   iPathSize         ` §џ"              iTagIdx         ` §џ$              iCurrTagIdx         ` §џ%              iDataIdx         ` §џ&              iCurrTagDataIdx         ` §џ'              k         ` §џ(              iMinCurrData         ` §џ)           	   iFirstIdx         ` §џ*              iLastIdx         ` §џ+           	   bTagStart          ` §џ-              bTagEnd          ` §џ.           	   bTagSlash          ` §џ/              bTagOpen          ` §џ0                 bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ              sNetId            
   T_AmsNetId   §џ           ams net id of target system       bBusy            §џ              bError            §џ	              nErrorID           §џ
           
   stDevIdent                              ST_DeviceIdentification   §џ       5    structure with available device identification data             ЇБV     џџџџ           FB_GETDEVICEIDENTIFICATIONEX        	   iDataSize       @` §џ           
   byTagStart    <    ` §џ           '<'    byTagEnd    >    ` §џ           '>' 
   byTagSlash    /    ` §џ           '/' 	   fbAdsRead                          ADSREAD ` §џ              bExecutePrev          ` §џ              iState         ` §џ              iData   	                      ` §џ           
   strActPath             ` §џ              iLoopEndIdx         ` §џ              iStructSize         ` §џ              strHardwareCPU             ` §џ              strTags   	  	        )       )          ` §џ           	   iTagsSize   	  	                     ` §џ              iCurrTag   	  (                     ` §џ               iCurrTagData   	  P                     ` §џ!           	   iPathSize         ` §џ"              iTagIdx         ` §џ$              iCurrTagIdx         ` §џ%              iDataIdx         ` §џ&              iCurrTagDataIdx         ` §џ'              k         ` §џ(              iMinCurrData         ` §џ)           	   iFirstIdx         ` §џ*              iLastIdx         ` §џ+           	   bTagStart          ` §џ-              bTagEnd          ` §џ.           	   bTagSlash          ` §џ/              bTagOpen          ` §џ0                 bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ              sNetId            
   T_AmsNetId   §џ           Ams net id of target system       bBusy            §џ              bError            §џ	              nErrorID           §џ
           
   stDevIdent                              ST_DeviceIdentificationEx   §џ       5    structure with available device identification data             ЇБV     џџџџ           FB_GETHOSTADDRBYNAME           fbAdsRW       j    ( PORT:= AMSPORT_R3_SYSSERV, IDXGRP:= SYSTEMSERVICE_IPHELPERAPI, IDXOFFS:= IPHELPERAPI_IPADDRBYHOSTNAME )              	   T_AmsPort         Н                
   ADSRDWRTEX ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id) 	   sHostName           ''       T_MaxString   §џ       1    String containing host name. E.g. 'DataServer1'    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout    ШЏ     §џ           Max fb execution time       bBusy            §џ
              bError            §џ              nErrID           §џ              sAddr           ''    
   T_IPv4Addr   §џ       S    String containing an (Ipv4) Internet Protocol dotted address. E.g. '172.16.7.199'    arrAddr           0, 0, 0, 0       T_IPv4AddrArr   §џ       C    Byte array containing an (Ipv4) Internet Protocol dotted address.             ЇБV     џџџџ           FB_GETHOSTNAME        	   fbAdsRead       R    ( PORT :=  AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_IPHOSTNAME, IDXOFFS := 0 )              	   T_AmsPort         О                 ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ                 sNetID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ	              bError            §џ
              nErrID           §џ           	   sHostName               T_MaxString   §џ           The local host name             ЇБV     џџџџ           FB_GETLOCALAMSNETID           fbRegQueryValue       W    ( sNetId:= '', sSubKey := 'SOFTWARE\Beckhoff\TwinCAT\System', sValName := 'AmsNetId' )                    
   T_AmsNetId                    T_MaxString                    T_MaxString                   FB_RegQueryValue ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              tmpBytes               T_AmsNetIdArr ` §џ                 bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeOut         §џ           Max fb execution time       bBusy            §џ              bError            §џ	              nErrId           §џ
           
   AddrString           '0.0.0.0.0.0'    
   T_AmsNetId   §џ       -    TwinCAT -specific network address as string 	   AddrBytes           0,0,0,0,0,0       T_AmsNetIdArr   §џ       3    TwinCAT-specific network address as array of byte             ЇБV     џџџџ           FB_GETROUTERSTATUSINFO        	   fbAdsRead       &    ( PORT:= 1, IDXGRP:= 1, IDXOFFS:= 1 )              	   T_AmsPort                          ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              adsRes   	                       ` §џ                 sNetId           ''    
   T_AmsNetID   §џ           Ams net id    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ	              bError            §џ
              nErrID           §џ              info                   ST_TcRouterStatusInfo   §џ       #    TwinCAT Router status information             ЇБV     џџџџ           FB_GETTIMEZONEINFORMATION        	   fbAdsRead       p    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_TIMESERVICES, IDXOFFS := TIMESERVICE_TIMEZONINFORMATION )              	   T_AmsPort                         ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              res                ST_AmsGetTimeZoneInformation ` §џ                 sNetID            
   T_AmsNetID   §џ       &    TwinCAT network address (ams net id)    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ              bError            §џ	              nErrID           §џ
              tzID               E_TimeZoneID   §џ              tzInfo                     ST_TimeZoneInformation   §џ                       ЇБV     џџџџ           FB_HASHTABLECTRL           p                     T_HashTableEntry      ` §џ              n                     T_HashTableEntry      ` §џ              nHash         ` §џ                 key           §џ       d    Entry key: used by A_Lookup, A_Remove method, the key.lookup variable is also used by A_Add method    putValue           §џ           Entry value 	   putPosPtr                     T_HashTableEntry        §џ	                 bOk            §џ           TRUE = success, FALSE = error    getValue           §џ           	   getPosPtr                     T_HashTableEntry        §џ       R    returned by A_GetFirstEntry, A_GetNextEntry, A_Add, A_Lookup and A_Remove method       hTable         	               T_HHASHTABLE  §џ           Hash table handle variable         ЇБV      џџџџ           FB_LINKEDLISTCTRL           p                   T_LinkedListEntry      ` §џ           Temp. previous node    n                   T_LinkedListEntry      ` §џ           Temp. next node       putValue           §џ           Linked list node value 	   putPosPtr                   T_LinkedListEntry        §џ           Linked list node pointer       bOk            §џ           TRUE = success, FALSE = error    getValue           §џ           Linked list node value 	   getPosPtr                   T_LinkedListEntry        §џ           Linked list node pointer       hList         	               T_HLINKEDLIST  §џ           Linked list table handle         ЇБV      џџџџ           FB_LOCALSYSTEMTIME     	      rtrig                 R_TRIG ` §џ              state         ` §џ              fbNT                   
   NT_GetTime ` §џ              fbTZ                          FB_GetTimeZoneInformation ` §џ              fbSET                           NT_SetTimeToRTCTime ` §џ              fbRTC                             RTC_EX2 ` §џ              timer                    TON ` §џ              nSync         ` §џ              bNotSup          ` §џ                 sNetID           ''    
   T_AmsNetID   §џ       +    The target TwinCAT system network address    bEnable            §џ       `    Enable/start cyclic time synchronisation (output is synchronized to Local Windows System Time)    dwCycle           Q            §џ       &    Time synchronisation cycle (seconds)    dwOpt          §џ       R    Additional option flags: If bit 0 is set => Synchronize Windows Time to RTC time    tTimeout         §џ       J    Max. ADS function block execution time (internal communication timeout).       bValid            §џ       \    TRUE => The systemTime and tzID output is valid, FALSE => systemTime and tzID is not valid 
   systemTime                   
   TIMESTRUCT   §џ       "    Local Windows System Time struct    tzID           eTimeZoneID_Invalid       E_TimeZoneID   §џ       )    Daylight/standard time zone information             ЇБV     џџџџ           FB_MEMBUFFERMERGE           pDest         ` §џ              cbDest         ` §џ                 eCmd           eEnumCmd_First       E_EnumCmdType   §џ              pBuffer           §џ           Pointer to destination buffer    cbBuffer           §џ       &    Max. byte size of destination buffer    pSegment           §џ       .    Pointer to data segment (optional, may be 0) 	   cbSegment           §џ       -    Number of data segments (optional, may be 0)      bOk            §џ       M    TRUE => Successfull, FALSE => End of enumeration or invalid input parameter    cbSize           §џ           Data buffer fill state             ЇБV      џџџџ           FB_MEMBUFFERSPLIT           pSrc         ` §џ              cbSrc         ` §џ                 eCmd           eEnumCmd_First       E_EnumCmdType   §џ              pBuffer           §џ           Pointer to source data buffer    cbBuffer           §џ       !    Byte size of source data buffer    cbSize           §џ           Max. segment byte size       bOk            §џ
       N    TRUE => Successfull, FALSE => End of segmentation or invalid input parameter    pSegment           §џ           Pointer to data segment 	   cbSegment           §џ           Byte size of data segment    bEOS            §џ       7    TRUE = End/last segment, FALSE = Next segment follows             ЇБV      џџџџ           FB_MEMRINGBUFFER           idxLast         ` §џ            byte index of last buffer byte    idxFirst         ` §џ       "    byte buffer of first buffer byte    idxGet         ` §џ              pTmp         ` §џ              cbTmp         ` §џ              cbCopied         ` §џ                 pWrite           §џ           pointer to write data    cbWrite           §џ           byte size of write data    pRead           §џ	           pointer to read data buffer    cbRead           §џ
           byte size of read data buffer    pBuffer           §џ       #    pointer to ring buffer data bytes    cbBuffer           §џ           max. ring buffer byte size       bOk            §џ       T    TRUE = new entry added or removed succesfully, FALSE = fifo overflow or fifo empty    nCount           §џ           number of fifo entries    cbSize           §џ       "    current byte length of fifo data    cbReturn           §џ       О    If bOk == TRUE => Number of recend realy returned (removed or get) data bytes
									   If bOk == FALSE and cbReturn <> 0 => Number of required read buffer data bytes (cbRead underflow)             ЇБV      џџџџ           FB_MEMRINGBUFFEREX           idxLast         ` §џ       *    byte index of last (newest) buffer entry    idxFirst         ` §џ       +    byte index of first (oldest) buffer entry    idxGet         ` §џ           temporary index    idxEnd         ` §џ       "    index of unused/free end segment    cbEnd         ` §џ       &    byte size of unused/free end segment    cbAdd         ` §џ!                 pWrite           §џ           pointer to write data    cbWrite           §џ           byte size of write data    pBuffer           §џ       #    pointer to ring buffer data bytes    cbBuffer           §џ           max. ring buffer byte size       bOk            §џ       W    TRUE = new entry added or get, freed succesfully, FALSE = fifo overflow or fifo empty    pRead           §џ       (    A_GetHead returns pointer to read data    cbRead           §џ       *    A_GetHead returns byte size of read data    nCount           §џ           number of fifo entries    cbSize           §џ       "    current byte length of fifo data    cbFree           §џ            biggest available free segment             ЇБV      џџџџ           FB_MEMSTACKBUFFER               pWrite           §џ           pointer to write data    cbWrite           §џ           byte size of write data    pRead           §џ	           pointer to read data buffer    cbRead           §џ
           byte size of read data buffer    pBuffer           §џ       #    pointer to LIFO buffer data bytes    cbBuffer           §џ           max. LIFO buffer byte size       bOk            §џ       T    TRUE = new entry added or removed succesfully, FALSE = LIFO overflow or LIFO empty    nCount           §џ           number of LIFO entries    cbSize           §џ       "    current byte length of LIFO data    cbReturn           §џ       О    If bOk == TRUE => Number of recend realy returned (removed or get) data bytes
									   If bOk == FALSE and cbReturn <> 0 => Number of required read buffer data bytes (cbRead underflow)             ЇБV      џџџџ           FB_REGQUERYVALUE           fbAdsRdWrtEx       [    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_REG_HKEYLOCALMACHINE, IDXOFFS := 0 )              	   T_AmsPort         Ш                  
   ADSRDWRTEX ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              s1Len         ` §џ              s2Len         ` §џ              ptr         ` §џ              cbBuff         ` §џ              tmpBuff                ST_HKeySrvRead ` §џ                 sNetId            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    sSubKey               T_MaxString   §џ       #    HKEY_LOCAL_MACHINE \ sub key name    sValName               T_MaxString   §џ           Value name    cbData           §џ           Number of data bytes to read    pData           §џ       $    Points to registry key data buffer    bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeOut         §џ	           Max fb execution time       bBusy            §џ              bError            §џ              nErrId           §џ              cbRead           §џ       '    Number of succesfully read data bytes             ЇБV     џџџџ           FB_REGSETVALUE        
   fbAdsWrite       [    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_REG_HKEYLOCALMACHINE, IDXOFFS := 0 )              	   T_AmsPort         Ш                  ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              s1Len         ` §џ              s2Len         ` §џ              s3Len         ` §џ              ptr         ` §џ              nType         ` §џ              cbBuff         ` §џ              cbRealWrite         ` §џ              tmpBuff                  ST_HKeySrvWrite ` §џ                 sNetId            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    sSubKey               T_MaxString   §џ       #    HKEY_LOCAL_MACHINE \ sub key name    sValName               T_MaxString   §џ           Value name    eValType               E_RegValueType   §џ           Value type    cbData           §џ           Size of value data in bytes    pData           §џ           Pointer to value data buffer   bExecute            §џ	       6    Rising edge on this input activates the fb execution    tTimeOut         §џ
           Max fb execution time       bBusy            §џ              bError            §џ              nErrId           §џ              cbWrite           §џ       +    Number of successfully written data bytes             ЇБV     џџџџ           FB_REMOVEROUTEENTRY        
   fbAdsWrite       P    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_DELREMOTE, IDXOFFS := 0 )              	   T_AmsPort         "                 ADSWRITE ` §џ                 sNetID            
   T_AmsNetID   §џ       '    TwinCAT network address (ams net id )    sName                 §џ           Route name as string    bExecute            §џ       -    Rising edge starts function block execution    tTimeout         §џ           Max fb execution time       bBusy            §џ
              bError            §џ              nErrID           §џ                       ЇБV     џџџџ           FB_RESETSCOPESERVERCONTROL        
   fbAdsWrite                          ADSWRITE    §џ              nState            §џ                 sNetId            
   T_AmsNetId   §џ              bExecute            §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ	              bError            §џ
              nErrorId           §џ                       ЇБV     џџџџ           FB_SAVESCOPESERVERDATA           nState            §џ           
   fbAdsWrite       D    ( PORT := AMSPORT_R3_SCOPESERVER, IDXGRP := 16#750E, IDXOFFS := 0 )              	   T_AmsPort         u                 ADSWRITE    §џ                 sNetId            
   T_AmsNetId   §џ              bExecute            §џ           	   sSaveFile    Q       Q    §џ              tTimeout         §џ                 bBusy            §џ	              bDone            §џ
              bError            §џ              nErrorId           §џ                       ЇБV     џџџџ           FB_SCOPESERVERCONTROL           eCurrentState           SCOPE_SERVER_IDLE       E_ScopeServerState    §џ           	   fbConnect                                   FB_ConnectScopeServer    §џ              fbStart        
                FB_StartScopeServer    §џ              fbStop        
                FB_StopScopeServer    §џ              fbSave        
                FB_SaveScopeServerData    §џ              fbDisconnect        	               FB_DisconnectScopeServer    §џ              fbReset        	               FB_ResetScopeServerControl    §џ                  sNetId            
   T_AmsNetId   §џ           	   eReqState               E_ScopeServerState   §џ              sConfigFile    Q       Q    §џ           	   sSaveFile    Q       Q    §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ              bError            §џ              nErrorId           §џ                       ЇБV     џџџџ           FB_SETTIMEZONEINFORMATION        
   fbAdsWrite       o    ( PORT:= AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_TIMESERVICES, IDXOFFS	:= TIMESERVICE_TIMEZONINFORMATION )              	   T_AmsPort                         ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              req                ST_AmsGetTimeZoneInformation ` §џ                 sNetID           ''    
   T_AmsNetID   §џ       &    TwinCAT network address (ams net id)    tzInfo       ]   ( (*West Euoropa Standard Time *)
					bias:=-60,
					standardName:='W. Europe Standard Time',
					standardDate:=(wYear:=0, wMonth:=10, wDayOfWeek:=0, wDay:=5, wHour:=3),
					standardBias:=0,
					daylightName:='W. Europe Daylight Time',
					daylightDate:=(wYear:=0, wMonth:=3, wDayOfWeek:=0, wDay:=5, wHour:=2),
					daylightBias:=-60 )    Фџџџ        W. Europe Standard Time                
   TIMESTRUCT             
                                W. Europe Daylight Time                
   TIMESTRUCT                                 Фџџџ   ST_TimeZoneInformation   §џ              bExecute            §џ       6    Rising edge on this input activates the fb execution    tTimeout         §џ           Max fb execution time       bBusy            §џ              bError            §џ              nErrID           §џ                       ЇБV     џџџџ           FB_STARTSCOPESERVER           nState            §џ           
   fbAdsWrite                          ADSWRITE    §џ              nDummy   	                    0,0                     §џ                 sNetId            
   T_AmsNetId   §џ              bExecute            §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ	              bError            §џ
              nErrorId           §џ                       ЇБV     џџџџ           FB_STOPSCOPESERVER           nState            §џ           
   fbAdsWrite                          ADSWRITE    §џ              nDummy   	                    0,0                     §џ                 sNetId            
   T_AmsNetId   §џ              bExecute            §џ              tTimeout         §џ                 bBusy            §џ              bDone            §џ	              bError            §џ
              nErrorId           §џ                       ЇБV     џџџџ           FB_STRINGRINGBUFFER           fbBuffer                              FB_MemRingBuffer ` §џ       4    Internal (low level) buffer control function block    
   bOverwrite            §џ       8    TRUE = overwrite oldest entry, FALSE = don't overwrite    putValue           ''       T_MaxString   §џ       %    String to add (write) to the buffer    pBuffer           §џ	       #    Pointer to ring buffer data bytes    cbBuffer           §џ
           Max. ring buffer byte size       bOk            §џ       T    TRUE = new entry added or removed succesfully, FALSE = fifo overflow or fifo empty    getValue           ''       T_MaxString   §џ       #    String removed (read) from buffer    nCount           §џ           Number of fifo entries    cbSize           §џ       "    Current byte length of fifo data             ЇБV      џџџџ        "   FB_SYSTEMTIMETOTZSPECIFICLOCALTIME           fbBase                                   "   FB_TranslateUtcToLocalTimeByZoneID ` §џ           Underlaid base function block       in                   
   TIMESTRUCT   §џ       p    Time to be converted (UTC, system time format). Structure that specifies the system time since January 1, 1601    tzInfo                     ST_TimeZoneInformation   §џ           Time zone settings       out                   
   TIMESTRUCT   §џ       ,    Converted time in local system time format    eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ	       "    Daylight saving time information    bB            §џ
            FALSE => A time, TRUE => B time            ЇБV      џџџџ           FB_TEXTFILERINGBUFFER           fbOpen                             FB_FileOpen ` §џ              fbClose                      FB_FileClose ` §џ              fbPuts        	               FB_FilePuts ` §џ              nStep         ` §џ       @    0=idle, 1=init, 10,11=open, 40,41=write, 50,51=close, 100=exit    eCmd           eFileRBuffer_None       E_FileRBufferCmd ` §џ                 sNetId           ''    
   T_AmsNetId ` §џ           ams net id 	   sPathName           'c:\Temp\data.dat'       T_MaxString ` §џ       6    file buffer path name (max. length = 255 characters)    ePath           PATH_GENERIC    
   E_OpenPath ` §џ           default: Open generic file    bAppend         ` §џ       #    TRUE = append, FALSE = not append    putLine           ''       T_MaxString ` §џ	              cbBuffer        ` §џ
       5    max. file buffer byte size(RESERVED for future use)    tTimeout       ` §џ                 bBusy          ` §џ              bError          ` §џ              nErrID         ` §џ              bOpened          ` §џ       )    TRUE = file opened, FALSE = file closed    getLine           ''       T_MaxString ` §џ                       ЇБV     џџџџ        "   FB_TRANSLATELOCALTIMETOUTCBYZONEID           inLocal                   
   TIMESTRUCT ` §џ       9    Input time in local system time format (time structure) 	   tziSommer                   
   TIMESTRUCT ` §џ       A    tzInfo.daylightDate transition time in local system time format 	   tziWinter                   
   TIMESTRUCT ` §џ       A    tzInfo.standardDate transition time in local system time Format    tziLocalSommer             
   T_FILETIME ` §џ       ?    tzInfo.daylightDate transition time in local file time format    tziLocalWinter             
   T_FILETIME ` §џ       ?    tzInfo.standardDate transition time in local file time Format    tziLocalSommerJump             
   T_FILETIME ` §џ              tziLocalWinterJump             
   T_FILETIME ` §џ              ui64LocalIn                T_ULARGE_INTEGER ` §џ       (    Local input time as unsigned 64 number    ui64LocalSommer                T_ULARGE_INTEGER ` §џ       5    Local tzInfo.daylightDate as unsigned 64 bit number    ui64LocalWinter                T_ULARGE_INTEGER ` §џ       5    Local tzInfo.standardDate as unsigned 64 bit number    in_to_s         ` §џ       <    Input time[Local] to tzInfo.daylightDate[Local] cmp result    in_to_w         ` §џ       <    Input time[Local] to tzInfo.standardDate[Local] cmp result    s_to_w         ` §џ       E    tzInfo.daylightDate[Local] to tzInfo.standardDate[Local] cmp result    in_to_s_jump         ` §џ        2    Input time[Local] to tzInfo jump time cmp result    in_to_w_jump         ` §џ!       2    Input time[Local] to tzInfo jump time cmp result    iStandardBias         ` §џ#              iDaylightBias         ` §џ$              ui64PreviousIn                T_ULARGE_INTEGER ` §џ&              ui64FallDiff                T_ULARGE_INTEGER ` §џ'           	   bFallDiff          ` §џ(           Nur zu Testzwecken    dtSommerJump         ` §џ*              dtWinterJump         ` §џ+                 in             
   T_FILETIME   §џ       /    Time to be converted (Local file time format)    tzInfo                     ST_TimeZoneInformation   §џ           Time zone information    wDldYear           §џ       p    Optional daylightDate.wYear value. If 0 => not used (default) else used only if tzInfo.daylightDate.wYear = 0.    wStdYear           §џ       p    Optional standardDate.wYear value. If 0 => not used (default) else used only if tzInfo.standardDate.wYear = 0.       out             
   T_FILETIME   §џ
       '    Converted time (UTC file time format)    eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ       +    Detected daylight saving time information    bB            §џ            FALSE => A time, TRUE => B time   bias           §џ           Bias value in minutes             ЇБV      џџџџ        "   FB_TRANSLATEUTCTOLOCALTIMEBYZONEID           inUtc                   
   TIMESTRUCT ` §џ       7    Input time in UTC system time format (time structure)    bInAsStruct          ` §џ       k    TRUE => inUtc is valid/converted to UTC system time format, FALSE => inUtc is not valid/not converted yet 	   tziSommer                   
   TIMESTRUCT ` §џ       A    tzInfo.daylightDate transition time in local system time format 	   tziWinter                   
   TIMESTRUCT ` §џ       A    tzInfo.standardDate transition time in local system time Format    tziLocalSommer             
   T_FILETIME ` §џ       ?    tzInfo.daylightDate transition time in local file time format    tziLocalWinter             
   T_FILETIME ` §џ       ?    tzInfo.standardDate transition time in local file time Format    tziUtcSommer             
   T_FILETIME ` §џ       =    tzInfo.daylightDate transition time in UTC file time format    tziUtcWinter             
   T_FILETIME ` §џ       =    tzinfo.standardDate transition time in UTC file time format 	   ui64UtcIn                T_ULARGE_INTEGER ` §џ       &    UTC input time as unsigned 64 number    ui64UtcSommer                T_ULARGE_INTEGER ` §џ       3    UTC tzInfo.daylightDate as unsigned 64 bit number    ui64UtcWinter                T_ULARGE_INTEGER ` §џ       3    UTC tzInfo.standardDate as unsigned 64 bit number    in_to_s         ` §џ       8    Input time[UTC] to tzInfo.daylightDate[UTC] cmp result    in_to_w         ` §џ       8    Input time[UTC] to tzInfo.standardDate[UTC] cmp result    s_to_w         ` §џ        A    tzInfo.daylightDate[UTC] to tzInfo.standardDate[UTC] cmp result    out_to_s         ` §џ!       =    Output time[local] to tzInfo.daylightDate[local] cmp result    out_to_w         ` §џ"       =    Output time[local] to tzInfo.standardDate[local] cmp result       in             
   T_FILETIME   §џ       .    Time to be converted (UTC, file time format)    tzInfo                     ST_TimeZoneInformation   §џ           Time zone information    wDldYear           §џ       p    Optional daylightDate.wYear value. If 0 => not used (default) else used only if tzInfo.daylightDate.wYear = 0.    wStdYear           §џ       p    Optional standardDate.wYear value. If 0 => not used (default) else used only if tzInfo.standardDate.wYear = 0.       out             
   T_FILETIME   §џ
       (    Converted time (local file time format)   eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ       0    Detected daylight saving time/zone information    bB            §џ            FALSE => A time, TRUE => B time   bias           §џ           Bias value in minutes             ЇБV      џџџџ            FB_TZSPECIFICLOCALTIMETOFILETIME           fbBase       !    ( wStdYear := 0, wDldYear := 0 )                                         "   FB_TranslateLocalTimeToUtcByZoneID ` §џ           Underlaid base function block       in             
   T_FILETIME   §џ       }    Time zone's specific local file time. 64-bit value representing the number of 100-nanosecond intervals since January 1, 1601   tzInfo                     ST_TimeZoneInformation   §џ           Time zone settings       out             
   T_FILETIME   §џ       E    Converted time in Coordinated Universal Time (UTC) file time format    eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ	       "    Daylight saving time information    bB            §џ
            FALSE => A time, TRUE => B time            ЇБV      џџџџ        "   FB_TZSPECIFICLOCALTIMETOSYSTEMTIME           fbBase                                         "   FB_TranslateLocalTimeToUtcByZoneID ` §џ           Underlaid base function block       in                   
   TIMESTRUCT   §џ       g    Time zone's specific local system time. Structure that specifies the system time since January 1, 1601   tzInfo                     ST_TimeZoneInformation   §џ           Time zone settings       out                   
   TIMESTRUCT   §џ       8    Coordinated Universal Time (UTC) in system time format    eTzID           eTimeZoneID_Unknown       E_TimeZoneID   §џ	       "    Daylight saving time information    bB            §џ
            FALSE => A time, TRUE => B time            ЇБV      џџџџ           FB_WRITEPERSISTENTDATA           fbAdsWrtCtl       9    ( ADSSTATE := ADSSTATE_SAVECFG, LEN := 0, SRCADDR := 0 )                          	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ       l    Contains the ADS port number of the PLC run-time system whose persistent data is to be stored (801, 811...)   START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time    MODE           SPDM_2PASS       E_PersistentMode   §џ       D    =SPDM_2PASS: optimized boost; =SPDM_VAR_BOOST: boost per variable;       BUSY            §џ              ERR            §џ              ERRID           §џ                       ЇБV     џџџџ           FILETIME_TO_DT           ui64                T_ULARGE_INTEGER ` §џ	                 fileTime             
   T_FILETIME   §џ           Windows file time.       FILETIME_TO_DT                                     ЇБV      џџџџ           FILETIME_TO_SYSTEMTIME     	      D         ` §џ              M         ` §џ              Y         ` §џ           
   uiPastDays                T_ULARGE_INTEGER ` §џ	              uiPastYears                T_ULARGE_INTEGER ` §џ
              uiRemainder                T_ULARGE_INTEGER ` §џ           
   dwPastDays         ` §џ              dwPastYears         ` §џ           
   dwYearDays         ` §џ                 fileTime             
   T_FILETIME   §џ                 FILETIME_TO_SYSTEMTIME                   
   TIMESTRUCT                             ЇБV      џџџџ           FIX16_TO_LREAL               in                 T_FIX16   §џ                 FIX16_TO_LREAL                                                  ЇБV      џџџџ           FIX16_TO_WORD               in                 T_FIX16   §џ           16 bit fixed point number       FIX16_TO_WORD                                     ЇБV      џџџџ           FIX16ADD               augend                 T_FIX16   §џ              addend                 T_FIX16   §џ                 FIX16Add                 T_FIX16                             ЇБV      џџџџ        
   FIX16ALIGN               in                 T_FIX16   §џ       #    16 bit signed fixed point number.    n                           §џ       ,    Number of fractional bits (decimal places)    
   FIX16Align                 T_FIX16                             ЇБV      џџџџ           FIX16DIV           tmpA         ` §џ	                 dividend                 T_FIX16   §џ              divisor                 T_FIX16   §џ                 FIX16Div                 T_FIX16                             ЇБV      џџџџ           FIX16MUL           tmp         ` §џ	                 multiA                 T_FIX16   §џ              multiB                 T_FIX16   §џ                 FIX16Mul                 T_FIX16                             ЇБV      џџџџ           FIX16SUB               minuend                 T_FIX16   §џ           
   subtrahend                 T_FIX16   §џ                 FIX16Sub                 T_FIX16                             ЇБV      џџџџ           GETREMOTEPCINFO        	   fbAdsRead       #    ( PORT:=1, IDXGRP:=3, IDXOFFS:=1 )              	   T_AmsPort                          ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ           
   RouterInfo   	  c            
                ST_AmsRouterInfoEntry         ` §џ              iIndex         ` §џ                 NETID            
   T_AmsNetId   §џ       D    Target NetID, usually left as empty string for reading local infos    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ       
    Ads busy    ERR            §џ	           Ads error    ERRID           §џ
           Ads Error    RemotePCInfo               REMOTEPCINFOSTRUCT   §џ       N    field with all NetIDДs and PC names found in router, ordered as router gives             ЇБV     џџџџ           GUID_TO_REGSTRING           sGuid    Q       Q     §џ                 in                  GUID   §џ           Structure with GUID       GUID_TO_REGSTRING    '       '                              ЇБV      џџџџ           GUID_TO_STRING           sRetVal    Q       Q     §џ              nDummyW            §џ	              nDummyDW            §џ
              sHex               T_MaxString    §џ                 stIn                  GUID   §џ           Structure with a GUID       GUID_TO_STRING    Q       Q                              ЇБV      џџџџ           GUIDSEQUALBYVAL               guidA                  GUID   §џ              guidB                  GUID   §џ                 GuidsEqualByVal                                      ЇБV      џџџџ           HEXASCNIBBLE_TO_BYTE               asc           §џ       R   Ascii code of hexadecimal nibble character, ASC('0'..'9' or 'a'..'f' or 'A'..'F')       HEXASCNIBBLE_TO_BYTE                                     ЇБV      џџџџ           HEXCHRNIBBLE_TO_BYTE               chr               §џ       8    One string character: '0'..'9' or 'a'..'f' or 'A'..'F'       HEXCHRNIBBLE_TO_BYTE                                     ЇБV      џџџџ           HEXSTR_TO_DATA           pSrc               ` §џ
              pDest               ` §џ              ascii         ` §џ              nibble         ` §џ              bAdd          ` §џ              bLN          ` §џ           hi/lo nibble       sHex               T_MaxString   §џ           Hex string to convert    pData           §џ           Pointer to destination buffer    cbData           §џ       !    Byte size of destination buffer       HEXSTR_TO_DATA                                     ЇБV      џџџџ           HOST_TO_BE128               in                T_UHUGE_INTEGER   §џ                 HOST_TO_BE128                T_UHUGE_INTEGER                             ЇБV      џџџџ           HOST_TO_BE16               in           §џ                 HOST_TO_BE16                                     ЇБV      џџџџ           HOST_TO_BE32           parr    	                            ` §џ                 in           §џ                 HOST_TO_BE32                                     ЇБV      џџџџ           HOST_TO_BE64               in                T_ULARGE_INTEGER   §џ                 HOST_TO_BE64                T_ULARGE_INTEGER                             ЇБV      џџџџ           INT64_TO_LREAL               in                T_LARGE_INTEGER   §џ                 INT64_TO_LREAL                                                  ЇБV      џџџџ        
   INT64ADD64           bOV          ` §џ	                 i64a                T_LARGE_INTEGER   §џ              i64b                T_LARGE_INTEGER   §џ              
   Int64Add64                T_LARGE_INTEGER                             ЇБV      џџџџ           INT64ADD64EX               augend                T_LARGE_INTEGER   §џ              addend                T_LARGE_INTEGER   §џ                 Int64Add64Ex                T_LARGE_INTEGER                       bOV            §џ       3    TRUE => arithmetic overflow, FALSE => no overflow         ЇБV      џџџџ        
   INT64CMP64               i64a                T_LARGE_INTEGER   §џ              i64b                T_LARGE_INTEGER   §џ	              
   Int64Cmp64                                     ЇБV      џџџџ           INT64DIV64EX           bIsNegative          ` §џ           
   sRemainder                T_ULARGE_INTEGER ` §џ                 dividend                T_LARGE_INTEGER   §џ              divisor                T_LARGE_INTEGER   §џ                 Int64Div64Ex                T_LARGE_INTEGER                    	   remainder                 T_LARGE_INTEGER  §џ                   ЇБV      џџџџ           INT64ISZERO               i64                T_LARGE_INTEGER   §џ                 Int64IsZero                                      ЇБV      џџџџ           INT64NEGATE               i64                T_LARGE_INTEGER   §џ                 Int64Negate                T_LARGE_INTEGER                             ЇБV      џџџџ           INT64NOT               i64                T_LARGE_INTEGER   §џ                 Int64Not                T_LARGE_INTEGER                             ЇБV      џџџџ        
   INT64SUB64               i64a                T_LARGE_INTEGER   §џ       	    minuend    i64b                T_LARGE_INTEGER   §џ           substrahend    
   Int64Sub64                T_LARGE_INTEGER                             ЇБV      џџџџ           ISFINITE        	   ptrDouble    	                            ` §џ           	   ptrSingle               ` §џ	                 x                 T_ARG   §џ                 IsFinite                                      ЇБV      џџџџ           LARGE_INTEGER            
   dwHighPart           §џ           	   dwLowPart           §џ                 LARGE_INTEGER                T_LARGE_INTEGER                             ЇБV      џџџџ           LARGE_TO_ULARGE               in                T_LARGE_INTEGER   §џ                 LARGE_TO_ULARGE                T_ULARGE_INTEGER                             ЇБV      џџџџ           LREAL_TO_FIX16               in                        §џ           LREAL number to convert    n                          §џ       ,    Number of fractional bits (decimal places)       LREAL_TO_FIX16                 T_FIX16                             ЇБV      џџџџ           LREAL_TO_FMTSTR           rec              
   T_FloatRec ` §џ              pOut               ` §џ              iStart         ` §џ              iEnd         ` §џ              i         ` §џ                 in                        §џ
           
   iPrecision           §џ              bRound            §џ                 LREAL_TO_FMTSTR    џ      џ                             ЇБV      џџџџ           LREAL_TO_INT64               in                        §џ                 LREAL_TO_INT64                T_LARGE_INTEGER                             ЇБV      џџџџ           LREAL_TO_UINT64           f64                      ` §џ                 in                        §џ                 LREAL_TO_UINT64                T_ULARGE_INTEGER                             ЇБV      џџџџ           MAXSTRING_TO_BYTEARR           cbCopy         ` §џ           	   Index7001                            in               T_MaxString   §џ                 MAXSTRING_TO_BYTEARR   	  џ                                                 ЇБV     џџџџ           NT_ABORTSHUTDOWN           fbAdsWrtCtl       N    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_POWERGOOD, DEVSTATE := 0 )              	   T_AmsPort         
               	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ              ERR            §џ	              ERRID           §џ
                       ЇБV     џџџџ        
   NT_GETTIME        	   fbAdsRead       i    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := SYSTEMSERVICE_TIMESERVICES, IDXOFFS := TIMESERVICE_DATEANDTIME )              	   T_AmsPort                         ADSREAD ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ              ERR            §џ	              ERRID           §џ
              TIMESTR                   
   TIMESTRUCT   §џ           Local windows system time             ЇБV     џџџџ        	   NT_REBOOT           fbAdsWrtCtl       M    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_SHUTDOWN, DEVSTATE := 1 )              	   T_AmsPort                       	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    DELAY           §џ           Reboot delay time [seconds]    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           NT_SETLOCALTIME        
   fbAdsWrite       d    ( PORT:= AMSPORT_R3_SYSSERV, IDXGRP:= SYSTEMSERVICE_TIMESERVICES, IDXOFFS:=TIMESERVICE_DATEANDTIME)              	   T_AmsPort                         ADSWRITE ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    TIMESTR                   
   TIMESTRUCT   §џ           New local system time    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           NT_SETTIMETORTCTIME        
   fbAdsWrite       :    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP := 4, IDXOFFS := 0 )              	   T_AmsPort                           ADSWRITE ` §џ           
   fbRegQuery       K    ( sSubKey := 'Software\Beckhoff\TwinCAT\System', sValName := 'NumOfCPUs' )                        T_MaxString                    T_MaxString                   FB_RegQueryValue ` §џ           	   fbTrigger                 R_TRIG ` §џ              bTmp         ` §џ              state         ` §џ              bInit         ` §џ           	   numOfCPUs         ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    SET            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ              ERR            §џ	              ERRID           §џ
                       ЇБV     џџџџ           NT_SHUTDOWN           fbAdsWrtCtl       M    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_SHUTDOWN, DEVSTATE := 0 )              	   T_AmsPort                        	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    DELAY           §џ           Shutdown delay time [seconds]    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           NT_STARTPROCESS        
   fbAdsWrite       O    ( PORT := AMSPORT_R3_SYSSERV, IDXGRP:=SYSTEMSERVICE_STARTPROCESS, IDXOFFS:=0 )              	   T_AmsPort         є                 ADSWRITE ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              LenPath         ` §џ              LenDir         ` §џ           
   LenComLine         ` §џ              req                  ST_AmsStartProcessReq ` §џ           data buffer       NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PATHSTR               T_MaxString   §џ              DIRNAME               T_MaxString   §џ           	   COMNDLINE               T_MaxString   §џ              START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ              ERR            §џ              ERRID           §џ                       ЇБV     џџџџ           OTSTRUCT_TO_TIME           tmpMilli         ` §џ                 OTIN                    OTSTRUCT   §џ                 OTSTRUCT_TO_TIME                                     ЇБV      џџџџ           PBOOL_TO_BOOL               in                  §џ                 PBOOL_TO_BOOL                                      ЇБV      џџџџ           PBYTE_TO_BYTE               in                 §џ                 PBYTE_TO_BYTE                                     ЇБV      џџџџ           PDATE_TO_DATE               in                 §џ                 PDATE_TO_DATE                                     ЇБV      џџџџ           PDINT_TO_DINT               in                 §џ                 PDINT_TO_DINT                                     ЇБV      џџџџ        	   PDT_TO_DT               in                 §џ              	   PDT_TO_DT                                     ЇБV      џџџџ           PDWORD_TO_DWORD               in                 §џ                 PDWORD_TO_DWORD                                     ЇБV      џџџџ           PHUGE_TO_HUGE               in                 T_HUGE_INTEGER        §џ                 PHUGE_TO_HUGE                T_HUGE_INTEGER                             ЇБV      џџџџ           PINT_TO_INT               in                 §џ                 PINT_TO_INT                                     ЇБV      џџџџ           PLARGE_TO_LARGE               in                 T_LARGE_INTEGER        §џ                 PLARGE_TO_LARGE                T_LARGE_INTEGER                             ЇБV      џџџџ           PLC_READSYMINFO        	   fbAdsRead       3    ( IDXGRP := ADSIGRP_SYM_UPLOADINFO, IDXOFFS := 0 )       №                 ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              SymInfoStruct   	                       ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ              SYMCOUNT           §џ              SYMSIZE           §џ                       ЇБV     џџџџ           PLC_READSYMINFOBYNAME           fbReadEx                                       PLC_ReadSymInfoByNameEx ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              SYMNAME               T_MaxString   §џ              START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ	           Max fb execution time       BUSY            §џ              ERR            §џ              ERRID           §џ              SYMINFO                     SYMINFOSTRUCT   §џ                       ЇБV     џџџџ           PLC_READSYMINFOBYNAMEEX        
   fbAdsRdWrt       5    ( IDXGRP := ADSIGRP_SYM_INFOBYNAMEEX, IDXOFFS := 0 )       	№                   ADSRDWRT ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              symInfoBuffer                            ST_AmsSymbolInfoEntry ` §џ           
   nameLength         ` §џ           
   typeLength         ` §џ              commentLength         ` §џ              nameAdrOffset         ` §џ              typeAdrOffset         ` §џ              commentAdrOffset         ` §џ              nameCpyLength         ` §џ              typeCpyLength         ` §џ               commentCpyLength         ` §џ!              endOfBufAdrOffset         ` §џ"                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              SYMNAME               T_MaxString   §џ              START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ	           Max fb execution time       BUSY            §џ              ERR            §џ              ERRID           §џ              SYMINFO                     SYMINFOSTRUCT   §џ              OVTYPE            §џ       @    TRUE => Type name string length overflow, FALSE => no overflow 	   OVCOMMENT            §џ       >    TRUE => Comment string length overflow, FALSE => no overflow             ЇБV     џџџџ        	   PLC_RESET           fbAdsWrtCtl       F    ( ADSSTATE := ADSSTATE_RESET, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )                              	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              RESET            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ        	   PLC_START           fbAdsWrtCtl       D    ( ADSSTATE := ADSSTATE_RUN, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )                              	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           PLC_STOP           fbAdsWrtCtl       E    ( ADSSTATE := ADSSTATE_STOP, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )                              	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ              STOP            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           PLREAL_TO_LREAL               in                              §џ                 PLREAL_TO_LREAL                                                  ЇБV      џџџџ           PMAXSTRING_TO_MAXSTRING               in                T_MaxString        §џ                 PMAXSTRING_TO_MAXSTRING               T_MaxString                             ЇБV      џџџџ           PREAL_TO_REAL               in                  §џ                 PREAL_TO_REAL                                      ЇБV      џџџџ           PROFILER     
      MAX_DATABUFF_SIZE    d   @  §џ              RisingEdgeStart                 R_TRIG ` §џ              RisingEdgeReset                 R_TRIG ` §џ              FallingEdgeStart                 F_TRIG ` §џ              GETCPUACCOUNT1                GETCPUACCOUNT ` §џ              OldCpuCntDW         ` §џ              MeasureData   	  d                     ` §џ              TimeSum         ` §џ              MaxData        ` §џ              idx         ` §џ                 START            §џ       0   rising edge starts measurement and falling stops   RESET            §џ                 BUSY            §џ              DATA                   PROFILERSTRUCT   §џ                       ЇБV     џџџџ           PSINT_TO_SINT               in                 §џ                 PSINT_TO_SINT                                     ЇБV      џџџџ           PSTRING_TO_STRING               in     Q       Q         §џ                 PSTRING_TO_STRING    Q       Q                              ЇБV      џџџџ           PTIME_TO_TIME               in                 §џ                 PTIME_TO_TIME                                     ЇБV      џџџџ           PTOD_TO_TOD               in                 §џ                 PTOD_TO_TOD                                     ЇБV      џџџџ           PUDINT_TO_UDINT               in                 §џ                 PUDINT_TO_UDINT                                     ЇБV      џџџџ           PUHUGE_TO_UHUGE               in                 T_UHUGE_INTEGER        §џ                 PUHUGE_TO_UHUGE                T_UHUGE_INTEGER                             ЇБV      џџџџ           PUINT64_TO_UINT64               in                 T_ULARGE_INTEGER        §џ                 PUINT64_TO_UINT64                T_ULARGE_INTEGER                             ЇБV      џџџџ           PUINT_TO_UINT               in                 §џ                 PUINT_TO_UINT                                     ЇБV      џџџџ           PULARGE_TO_ULARGE               in                 T_ULARGE_INTEGER        §џ                 PULARGE_TO_ULARGE                T_ULARGE_INTEGER                             ЇБV      џџџџ           PUSINT_TO_USINT               in                 §џ                 PUSINT_TO_USINT                                     ЇБV      џџџџ           PVOID_TO_BINSTR               in               PVOID   §џ       -    PVOID input value (pointer) to be converted 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       PVOID_TO_BINSTR               T_MaxString                             ЇБV      џџџџ           PVOID_TO_DECSTR               in               PVOID   §џ       -    PVOID input value (pointer) to be converted 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       PVOID_TO_DECSTR               T_MaxString                             ЇБV      џџџџ           PVOID_TO_HEXSTR               in               PVOID   §џ       -    PVOID input value (pointer) to be converted 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.    bLoCase            §џ	       8   Default: use "ABCDEF", if TRUE use "abcdef" characters.       PVOID_TO_HEXSTR               T_MaxString                             ЇБV      џџџџ           PVOID_TO_OCTSTR               in               PVOID   §џ       -    PVOID input value (pointer) to be converted 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       PVOID_TO_OCTSTR               T_MaxString                             ЇБV      џџџџ           PVOID_TO_STRING               in               PVOID   §џ                 PVOID_TO_STRING               T_MaxString                             ЇБV      џџџџ           PWORD_TO_WORD               in                 §џ                 PWORD_TO_WORD                                     ЇБV      џџџџ        
   RAD_TO_DEG               ANGLE                        §џ              
   RAD_TO_DEG                                                  ЇБV      џџџџ           REGSTRING_TO_GUID               in    '       '    §џ       A    String containig GUID, '{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}'       REGSTRING_TO_GUID                  GUID                             ЇБV      џџџџ           ROUTETRANSPORT_TO_STRING               eType               E_RouteTransportType   §џ                 ROUTETRANSPORT_TO_STRING    Q       Q                              ЇБV      џџџџ           RTC           fbGetCpuCounter                 GETCPUCOUNTER ` §џ           
   risingEdge                 R_TRIG ` §џ              oldTick         ` §џ              currTick         ` §џ              nanoDiff         ` §џ              nanoRest         ` §џ              secDiff         ` §џ              init         ` §џ                 EN            §џ              PDT           §џ                 Q            §џ              CDT           §џ	                       ЇБV      џџџџ           RTC_EX           fbGetCpuCounter                 GETCPUCOUNTER ` §џ           
   risingEdge                 R_TRIG ` §џ              oldTick         ` §џ              currTick         ` §џ              nanoDiff         ` §џ              nanoRest         ` §џ              secDiff         ` §џ              init         ` §џ                 EN            §џ              PDT           §џ              PMSEK           §џ                 Q            §џ	              CDT           §џ
              CMSEK           §џ                       ЇБV      џџџџ           RTC_EX2     	      fbGetCpuCounter                 GETCPUCOUNTER ` §џ           
   risingEdge                 R_TRIG ` §џ              oldTick         ` §џ              currTick         ` §џ              nanoDiff         ` §џ              nanoRest         ` §џ              secDiff         ` §џ              dateTime         ` §џ              init         ` §џ                 EN            §џ              PDT                   
   TIMESTRUCT   §џ              PMICRO           §џ                 Q            §џ	              CDT       ;    ( wYear := 1970, wMonth := 1, wDay := 1, wDayOfWeek := 4 )    В                  
   TIMESTRUCT   §џ
              CMICRO           §џ                       ЇБV      џџџџ           SCOPEASCIIEXPORT        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ           	   sFilePath               T_MaxString   §џ              tTimeout         §џ                 bBusy            §џ              bError            §џ	              iErrorId           §џ
                       ЇБV     џџџџ        	   SCOPEEXIT        
   fbAdsWrite                          ADSWRITE    §џ           
   RisingEdge                 R_TRIG    §џ              step            §џ              fbDelay                    TON    §џ                 bExecute            §џ       -    Rising edge starts function block execution    tTimeOut         §џ       >    Maximum time allowed for the execution of the function block       bBusy            §џ              bError            §џ              iErrorId           §џ	                       ЇБV     џџџџ           SCOPEGETRECORDLEN        	   fbAdsRead                          ADSREAD    §џ                 bExecute            §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ           
   fRecordLen                        §џ	                       ЇБV      џџџџ           SCOPEGETSTATE        	   fbAdsRead                          ADSREAD    §џ              State            §џ                 bExecute            §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ              bOnline            §џ	                       ЇБV      џџџџ           SCOPELOADFILE        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ           	   sFilePath               T_MaxString   §џ              tTimeout         §џ                 bBusy            §џ              bError            §џ	              iErrorId           §џ
                       ЇБV     џџџџ           SCOPEMANUALTRIGGER        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ                       ЇБV      џџџџ           SCOPESAVEAS        
   RisingEdge                 R_TRIG ` §џ           
   fbAdsWrite       D    ( NETID := '', PORT := 14000, IDXGRP := 16#2000, IDXOFFS := 16#11 )             
   T_AmsNetId                 	   T_AmsPort                           ADSWRITE ` §џ              step         ` §џ                 bExecute            §џ       -    Rising edge starts function block execution 	   sFilePath               T_MaxString   §џ           e.g. c:\Axis1.scp   tTimeout         §џ       >    Maximum time allowed for the execution of the function block       bBusy            §џ              bError            §џ	              iErrorId           §џ
                       ЇБV     џџџџ           SCOPESETOFFLINE        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ                       ЇБV      џџџџ           SCOPESETONLINE        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ                       ЇБV      џџџџ           SCOPESETRECORDLEN        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ           
   fRecordLen                        §џ                 bBusy            §џ              bError            §џ              iErrorId           §џ	                       ЇБV      џџџџ           SCOPEVIEWEXPORT        
   fbAdsWrite                          ADSWRITE    §џ                 bExecute            §џ           	   sFilePath               T_MaxString   §џ              tTimeout         §џ                 bBusy            §џ              bError            §џ	              iErrorId           §џ
                       ЇБV     џџџџ           STRING_TO_CSVFIELD           cbField         ` §џ                 in               T_MaxString   §џ       !    Input data in PLC string format    bQM            §џ	       l    TRUE => Enclose result string in quotation marks, FALSE => Don't enclose result string in quotation marks.       STRING_TO_CSVFIELD               T_MaxString                             ЇБV      џџџџ           STRING_TO_GUID           b   	  #                        §џ	              g                  GUID    §џ
              n            §џ              nibble            §џ           	   Index7001                            in    %       %    §џ       @    String containing GUID, 'xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx'       STRING_TO_GUID                  GUID                             ЇБV      џџџџ           STRING_TO_PVOID           ui32            §џ                 in    Q       Q    §џ       .    String containing the 32 bit pointer address       STRING_TO_PVOID               PVOID                             ЇБV      џџџџ           STRING_TO_SYSTEMTIME           b   	                    16#31, 16#36, 16#30, 16#31, 	(* year 1601 *)
								16#2D(*-*), 16#30, 16#31(*01*),	(* month *)
								16#2D(*-*), 16#30, 16#31(*01*),	(* day *)
								16#2D(*-*), 16#30, 16#30(*00*),	(* hour *)
								16#3A(*:*), 16#30, 16#30(*00*),	(* minute *)
								16#3A(*:*), 16#30, 16#30(*00*),	(* second *)
								16#2E(*.*), 16#30, 16#30, 16#30(*000*), (* milliseconds *)
								16#00      1      6      0      1      -      0      1      -      0      1      -      0      0      :      0      0      :      0      0      .      0      0      0           ` §џ           null delimiter    ts       *    ( wYear := 1601, wMonth := 1, wDay := 1 )    A               
   TIMESTRUCT ` §џ              n         ` §џ              bFmt          ` §џ              dwYears         ` §џ              dwDays         ` §џ           	   Index7001                            in               §џ       1    Input string, format: '2007-03-05-17:35:09.223'       STRING_TO_SYSTEMTIME                   
   TIMESTRUCT                             ЇБV      џџџџ           STRING_TO_UINT64           ptr               ` §џ              constTen       &     ( dwHighPart := 0, dwLowPart := 10 )    
           T_ULARGE_INTEGER ` §џ	                 in               §џ                 STRING_TO_UINT64                T_ULARGE_INTEGER                             ЇБV      џџџџ           SYSTEMTIME_TO_DT           b   	                 ќ    16#44, 16#54, 16#23(*DT#*),
										16#31, 16#39, 16#37, 16#30(*1970*),
										16#2D(*-*), 16#30, 16#31(*01*), 16#2D(*-*), 16#30, 16#31(*01*), 16#2D(*-*), 16#30, 16#30(*00*), 16#3A(*:*), 16#30, 16#30(*00*), 16#3A(*:*), 16#30, 16#30(*00*), 16#00      D      T      #      1      9      7      0      -      0      1      -      0      1      -      0      0      :      0      0      :      0      0           ` §џ              str             ` §џ
              nSeconds         ` §џ           	   Index7001                            TIMESTR                   
   TIMESTRUCT   §џ                 SYSTEMTIME_TO_DT                                     ЇБV      џџџџ           SYSTEMTIME_TO_FILETIME           tmp1                T_ULARGE_INTEGER ` §џ	              tmp2                T_ULARGE_INTEGER ` §џ
              pastDays         ` §џ              i         ` §џ              
   systemTime                   
   TIMESTRUCT   §џ                 SYSTEMTIME_TO_FILETIME             
   T_FILETIME                             ЇБV      џџџџ           SYSTEMTIME_TO_STRING           b   	                 Љ   16#31, 16#36, 16#30, 16#31(*1601*),		(* year *)
										16#2D(*-*), 16#30, 16#31(*01*),				(* month *)
										16#2D(*-*), 16#30, 16#31(*01*),				(* day *)
										16#2D(*-*), 16#30, 16#30(*00*),				(* hour *)
										16#3A(*:*), 16#30, 16#30(*00*),				(* minute *)
										16#3A(*:*), 16#30, 16#30(*00*),				(* second *)
										16#2E(*.*), 16#30, 16#30, 16#30(*000*),		(* milliseconds *)
										16#00      1      6      0      1      -      0      1      -      0      1      -      0      0      :      0      0      :      0      0      .      0      0      0           ` §џ           	   Index7001                            in                   
   TIMESTRUCT   §џ                 SYSTEMTIME_TO_STRING                                         ЇБV      џџџџ        	   TC_CONFIG           fbAdsWrtCtl       e    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_RECONFIG, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )              	   T_AmsPort                                	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    SET            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           TC_CPUUSAGE        	   fbAdsRead       5    ( PORT:= AMSPORT_R0_RTIME, IDXGRP:= 1, IDXOFFS:= 6 )              	   T_AmsPort                          ADSREAD ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ              USAGE           §џ          in %            ЇБV     џџџџ        
   TC_RESTART           fbAdsWrtCtl       b    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_RESET, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )              	   T_AmsPort                                	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    RESTART            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           TC_STOP           fbAdsWrtCtl       a    ( PORT := AMSPORT_R3_SYSSERV, ADSSTATE := ADSSTATE_STOP, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )              	   T_AmsPort                                	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    STOP            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ                       ЇБV     џџџџ           TC_SYSLATENCY        	   fbAdsRead       8    ( PORT := AMSPORT_R0_RTIME, IDXGRP := 1, IDXOFFS := 2 )              	   T_AmsPort                          ADSREAD ` §џ           	   fbTrigger                 R_TRIG ` §џ              state         ` §џ              tmpData   	                      ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ	              ERR            §џ
              ERRID           §џ              ACTUAL           §џ           Actual latency in Еs    MAXIMUM           §џ           Maximum latency in Еs             ЇБV     џџџџ           TIME_TO_OTSTRUCT           tmpMilli         ` §џ                 TIN           §џ                 TIME_TO_OTSTRUCT                    OTSTRUCT                             ЇБV      џџџџ           UDINT_TO_LREALEX               in           §џ                 UDINT_TO_LREALEX                                                  ЇБV      џџџџ           UINT32X32TO64           Tmp1         ` §џ	              Tmp2         ` §џ
              Tmp3         ` §џ              Tmp4         ` §џ              DW1         ` §џ              DW2         ` §џ              DW3         ` §џ              DW4         ` §џ                 ui32a           §џ              ui32b           §џ                 UInt32x32To64                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64_TO_LREAL               in                T_ULARGE_INTEGER   §џ                 UINT64_TO_LREAL                                                  ЇБV      џџџџ           UINT64_TO_STRING        	   remainder                T_ULARGE_INTEGER ` §џ              constTen       &     ( dwHighPart := 0, dwLowPart := 10 )    
           T_ULARGE_INTEGER ` §џ	                 in                T_ULARGE_INTEGER   §џ                 UINT64_TO_STRING                                         ЇБV      џџџџ           UINT64ADD64           bOV          ` §џ	                 ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ                 UInt64Add64                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64ADD64EX               augend                T_ULARGE_INTEGER   §џ              addend                T_ULARGE_INTEGER   §џ                 UInt64Add64Ex                T_ULARGE_INTEGER                       bOV            §џ       3    TRUE => arithmetic overflow, FALSE => no overflow         ЇБV      џџџџ        	   UINT64AND               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ              	   UInt64And                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64CMP64               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ	                 UInt64Cmp64                                     ЇБV      џџџџ           UINT64DIV16EX        	   pDividend    	                            ` §џ              pResult    	                            ` §џ              rest         ` §џ                 dividend                T_ULARGE_INTEGER   §џ              divisor           §џ                 UInt64Div16Ex                T_ULARGE_INTEGER                    	   remainder                 T_ULARGE_INTEGER  §џ                   ЇБV      џџџџ           UINT64DIV64        	   remainder                T_ULARGE_INTEGER ` §џ	                 dividend                T_ULARGE_INTEGER   §џ              divisor                T_ULARGE_INTEGER   §џ                 UInt64Div64                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64DIV64EX           msBit       /    ( dwHighPart := 16#80000000, 	dwLowPart := 0 )               T_ULARGE_INTEGER ` §џ              bitShift         ` §џ              cmp         ` §џ              in   	                      T_ULARGE_INTEGER         ` §џ              out   	                      T_ULARGE_INTEGER         ` §џ           
   cbReturned         ` §џ           	   Index7001                            dividend                T_ULARGE_INTEGER   §џ              divisor                T_ULARGE_INTEGER   §џ                 UInt64Div64Ex                T_ULARGE_INTEGER                    	   remainder                 T_ULARGE_INTEGER  §џ                   ЇБV      џџџџ           UINT64ISZERO               ui64                T_ULARGE_INTEGER   §џ                 UInt64isZero                                      ЇБV      џџџџ           UINT64LIMIT               ui64min                T_ULARGE_INTEGER   §џ              ui64in                T_ULARGE_INTEGER   §џ              ui64max                T_ULARGE_INTEGER   §џ                 UInt64Limit                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64MAX               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ              	   UInt64Max                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64MIN               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ              	   UInt64Min                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64MOD64               dividend                T_ULARGE_INTEGER   §џ              divisor                T_ULARGE_INTEGER   §џ                 UInt64Mod64                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64MUL64           bOV          ` §џ	                 multiplicand                T_ULARGE_INTEGER   §џ           
   multiplier                T_ULARGE_INTEGER   §џ                 UInt64Mul64                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64MUL64EX           bCarry          ` §џ           	   bSumCarry          ` §џ              n         ` §џ              m         ` §џ                 multiplicand                T_ULARGE_INTEGER   §џ           
   multiplier                T_ULARGE_INTEGER   §џ                 UInt64Mul64Ex                T_ULARGE_INTEGER                       bOV            §џ       3    TRUE => Arithmetic overflow, FALSE => no overflow         ЇБV      џџџџ        	   UINT64NOT               ui64                T_ULARGE_INTEGER   §џ              	   UInt64Not                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64OR               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ                 UInt64Or                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64ROL           bMSB          ` §џ	                 ui64                T_ULARGE_INTEGER   §џ              n           §џ              	   UInt64Rol                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64ROR           bLSB          ` §џ	                 ui64                T_ULARGE_INTEGER   §џ              n           §џ              	   UInt64Ror                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64SHL               ui64                T_ULARGE_INTEGER   §џ              n           §џ              	   UInt64Shl                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64SHR               ui64                T_ULARGE_INTEGER   §џ              n           §џ              	   UInt64Shr                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT64SUB64               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ                 UInt64Sub64                T_ULARGE_INTEGER                             ЇБV      џџџџ        	   UINT64XOR               ui64a                T_ULARGE_INTEGER   §џ              ui64b                T_ULARGE_INTEGER   §џ              	   UInt64Xor                T_ULARGE_INTEGER                             ЇБV      џџџџ           UINT_TO_LREALEX               in           §џ                 UINT_TO_LREALEX                                                  ЇБV      џџџџ           ULARGE_INTEGER            
   dwHighPart           §џ           	   dwLowPart           §џ                 ULARGE_INTEGER                T_ULARGE_INTEGER                             ЇБV      џџџџ           ULARGE_TO_LARGE               in                T_ULARGE_INTEGER   §џ                 ULARGE_TO_LARGE                T_LARGE_INTEGER                             ЇБV      џџџџ           USINT_TO_LREALEX               in           §џ                 USINT_TO_LREALEX                                                  ЇБV      џџџџ           WORD_TO_BINSTR           bits   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant bits    iPad            §џ           Number of padding zeros    i            §џ           	   Index7001                            in           §џ           WORD input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       WORD_TO_BINSTR               T_MaxString                             ЇБV      џџџџ           WORD_TO_DECSTR           dec   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant decades    iPad            §џ           Number of padding zeros    i            §џ              digit            §џ           	   Index7001                            in           §џ           WORD input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       WORD_TO_DECSTR               T_MaxString                             ЇБV      џџџџ           WORD_TO_FIX16               in           §џ           16 bit fixed point number    n                           §џ           number of fractional bits       WORD_TO_FIX16                 T_FIX16                             ЇБV      џџџџ           WORD_TO_HEXSTR           hex   	                          §џ       6    array of ASCII characters (inclusive null delimiter)    iSig            §џ           number of significant nibbles    iPad            §џ           number of padding zeros    i            §џ           	   Index7001                            in           §џ           WORD input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.    bLoCase            §џ	       8   Default: use "ABCDEF", if TRUE use "abcdef" characters.       WORD_TO_HEXSTR               T_MaxString                             ЇБV      џџџџ           WORD_TO_LREALEX               in           §џ                 WORD_TO_LREALEX                                                  ЇБV      џџџџ           WORD_TO_OCTSTR           oct   	                          §џ       6    Array of ASCII characters (inclusive null delimiter)    iSig            §џ           Number of significant nibbles    iPad            §џ           Number of padding zeros    i            §џ           	   Index7001                            in           §џ           WORD input value 
   iPrecision           §џ       ,    Precision. Number of digits to be printed.       WORD_TO_OCTSTR               T_MaxString                             ЇБV      џџџџ           WRITEPERSISTENTDATA           fbAdsWrtCtl       H    ( ADSSTATE := ADSSTATE_SAVECFG, DEVSTATE := 0, LEN := 0, SRCADDR := 0 )                              	   ADSWRTCTL ` §џ                 NETID            
   T_AmsNetId   §џ       &    TwinCAT network address (ams net id)    PORT           §џ       l    Contains the ADS port number of the PLC run-time system whose persistent data is to be stored (801, 811...)   START            §џ       6    Rising edge on this input activates the fb execution    TMOUT         §џ           Max fb execution time       BUSY            §џ
              ERR            §џ              ERRID           §џ                       ЇБV     џџџџ    q   C:\TWINCAT\PLC\LIB\STANDARD.LIB @                                                                                          CONCAT               STR1               §џ              STR2               §џ                 CONCAT                                         d66     џџџџ           CTD           M             §џ           Variable for CD Edge Detection      CD            §џ           Count Down on rising edge    LOAD            §џ           Load Start Value    PV           §џ           Start Value       Q            §џ           Counter reached 0    CV           §џ           Current Counter Value             d66     џџџџ           CTU           M             §џ            Variable for CU Edge Detection       CU            §џ       
    Count Up    RESET            §џ           Reset Counter to 0    PV           §џ           Counter Limit       Q            §џ           Counter reached the Limit    CV           §џ           Current Counter Value             d66     џџџџ           CTUD           MU             §џ            Variable for CU Edge Detection    MD             §џ            Variable for CD Edge Detection       CU            §џ	       
    Count Up    CD            §џ
           Count Down    RESET            §џ           Reset Counter to Null    LOAD            §џ           Load Start Value    PV           §џ           Start Value / Counter Limit       QU            §џ           Counter reached Limit    QD            §џ           Counter reached Null    CV           §џ           Current Counter Value             d66     џџџџ           DELETE               STR               §џ              LEN           §џ              POS           §џ                 DELETE                                         d66     џџџџ           F_TRIG           M             §џ
                 CLK            §џ           Signal to detect       Q            §џ           Edge detected             d66     џџџџ           FIND               STR1               §џ              STR2               §џ                 FIND                                     d66     џџџџ           INSERT               STR1               §џ              STR2               §џ              POS           §џ                 INSERT                                         d66     џџџџ           LEFT               STR               §џ              SIZE           §џ                 LEFT                                         d66     џџџџ           LEN               STR               §џ                 LEN                                     d66     џџџџ           MID               STR               §џ              LEN           §џ              POS           §џ                 MID                                         d66     џџџџ           R_TRIG           M             §џ
                 CLK            §џ           Signal to detect       Q            §џ           Edge detected             d66     џџџџ           REPLACE               STR1               §џ              STR2               §џ              L           §џ              P           §џ                 REPLACE                                         d66     џџџџ           RIGHT               STR               §џ              SIZE           §џ                 RIGHT                                         d66     џџџџ           RS               SET            §џ              RESET1            §џ                 Q1            §џ
                       d66     џџџџ           SEMA           X             §џ                 CLAIM            §џ	              RELEASE            §џ
                 BUSY            §џ                       d66     џџџџ           SR               SET1            §џ              RESET            §џ                 Q1            §џ	                       d66     џџџџ           TOF           M             §џ           internal variable 	   StartTime            §џ           internal variable       IN            §џ       ?    starts timer with falling edge, resets timer with rising edge    PT           §џ           time to pass, before Q is set       Q            §џ	       2    is FALSE, PT seconds after IN had a falling edge    ET           §џ
           elapsed time             d66     џџџџ           TON           M             §џ           internal variable 	   StartTime            §џ           internal variable       IN            §џ       ?    starts timer with rising edge, resets timer with falling edge    PT           §џ           time to pass, before Q is set       Q            §џ	       0    is TRUE, PT seconds after IN had a rising edge    ET           §џ
           elapsed time             d66     џџџџ           TP        	   StartTime            §џ           internal variable       IN            §џ       !    Trigger for Start of the Signal    PT           §џ       '    The length of the High-Signal in 10ms       Q            §џ	           The pulse    ET           §џ
       &    The current phase of the High-Signal             d66     џџџџ    R    @                                                                                +          CYLINDER_V2           SetError                  FB_SetError    ' /              SetMasterError                  FB_SetMasterError    ' 0              SetMasterMessage                  FB_SetMasterMessage    ' 1           
   SetMessage                  FB_SetMessage    ' 2              SetStep        
             
   FB_SetStep    ' 3              ConditionTimer                    TON    ' 4              ConditionTimerIsExpired             ' 5              ConditionTimerRequest             ' 6                 User           '            static variables    D_Enable            '               D_Text           '               D_Value    )       )    ' 	              T_Enable            '               T_Text           '            
   T_ValueToA    )       )    '            
   T_ValueToB    )       )    '            	   TextLine0    )       )    '            	   Direction            '               ConditionToA            '               ConditionToB            '               ConditionTime           '               TimeOut    И     '            
   BackEnable           '            dynamic variables    xInit           '               xSet            '               xPressureless            '            
   StepEnable           '                Freeze            ' !              Fill            ' "              JumpAddress           ' #           	   MultiProg            ' $                 ChannelA            ' (              ChannelB            ' )              ConditionAck            ' +                       -3%]  @    џџџџ           FB_ADSDEVICEINTERACTION           Step            (               MessageTimer                    TON    (               EmptyDeviceInteraction                   StructDeviceInteraction    (               SetError                  FB_SetError    (               SetMasterError                  FB_SetMasterError    (               SetMasterMessage                  FB_SetMasterMessage    (            
   SetMessage                  FB_SetMessage    (               SetTips               
   FB_SetTips    (               SetStep        
             
   FB_SetStep    (                TimeOut    р     ( "              Me             ( #                 User           (            	   StartTask            (               RequestType               (               pAgent                    StructDeviceInteraction        (                  Done            (               Abort            (                        -3%]  @    џџџџ           FB_CHECKDOUBLESENSE           SetError                  FB_SetError    )               SetMasterError                  FB_SetMasterError    )               SetMasterMessage                  FB_SetMasterMessage    )            
   SetMessage                  FB_SetMessage    )               SetStep        
             
   FB_SetStep    )                  SxA            )               SxB            )               User           )            	   TextLine0    )       )    )               Text           )               sValue01    )       )    ) 	                           -3%]  @    џџџџ           FB_EPSON_ROBOT           Step            [)              Timer                    TON    [*              SafeguardOn_Timer                    TON    [+              PusleRobotDone                 R_TRIG    [,                 User           [              Run            [              Pos           [              Start            [              Continue            [              Reset            [              SelMode            [	              SelSpeed            [
              R_Ready            [           	   R_Running            [              R_Paused            [              R_Error            [              R_Estop            [              R_SafeguardOn            [              R_SError            [           	   R_Warning            [              R_Busy            [              R_Done            [              R_NC2            [              R_InitialFinish            [                 Done            [              Ready            [              Running            [              R_Start            [           
   R_SelSpeed            [              R_Run            [           	   R_SelMode            [               R_Stop            [!              R_Pause            ["           
   R_Continue            [#              R_Reset            [$              R_Pos           [%                       -3%]  @    џџџџ           FB_GETNEWPARTINFO           SetError                  FB_SetError    n              SetMasterError                  FB_SetMasterError    n              SetMasterMessage                  FB_SetMasterMessage    n           
   SetMessage                  FB_SetMessage    n              SetTips               
   FB_SetTips    n              SetStep        
             
   FB_SetStep    n              Step            n              JumpAddress            n              pWT                          StructCarrierInfo         n              MessageTimer                    TON    n              rTrig_Abort                 R_TRIG    n               EmptyRequestAction                   StructRequestAction    n"              EmptyResponseAction                  StructResponseAction    n#              TimeOut    р     n%              EmptyCarrierInfo                         StructCarrierInfo    n&              FB_NAME    Q      New PartQ    n,                 User           n              WT          n              AbortAddress           n           	   StartTask            n              S_DUT_Presence            n           
   UpdateStep           n                 Done            n              Abort            n                       -3%]  @    џџџџ        
   FB_IAIPCON           HomeFinished             + >              Ready             + ?              Homing             + @              Step            + A              timer                    TON    + B           
   PusleStart                 R_TRIG    + C              APos            + E       %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ       Run            +               Pos           +               Start            +               Reset            +            	   HomeStart            +               HomePosition            +               SafePosition            + 	              Continue            + 
              APosbit0            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit1            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit2            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit3            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit4            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit5            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit6            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    APosbit7            +        %     ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ    PZONE            +               RMDS            +        /    AUTO ФЃЪНзДЬЌЯТаХКХOFFЃЌMANU ФЃЪНзДЬЌЯТаХКХON    PEND            +        &    вЦЖЏжСФПБъЮЛжУЃЌНјШыОЭЮЛЧјМфКѓаХКХON    HEND            +        )    НгЭЈЕчдДЪБаХКХOFFЃЌд­ЕуИДЮЛЭъГЩКѓаХКХON    Alarm            +        !    е§ГЃЪБаХКХOFFЃЌЗЂЩњБЈОЏЪБаХКХON    SV            +            ЫХЗўONЃЌПЩдЫзЊЕФзДЬЌЯТЪфГі    EMGS            +            аХКХON ЪБЮЊМБЭЃзДЬЌ    CLK            +            ИљОнЙтеЄON/OFFВњЩњИіТіГхаХКХ    STOP            +               IJogUp            +        $    Using for Teach MODE ONLY as  PIO 1   IJogDown            +         $    Using for Teach MODE ONLY as  PIO 1      Done            + %              Running            + &           	   HomeError            + '           	   PCPosBit0            + (           	   PCPosBit1            + )           	   PCPosBit2            + *           	   PCPosBit3            + +           	   PCPosBit4            + ,           	   PCPosBit5            + -           	   PCPosBit6            + .           	   PCPosBit7            + /              OJogUp            + 1       $    Using for Teach MODE ONLY as  PIO 1   OJogDown            + 2       $    Using for Teach MODE ONLY as  PIO 1   BLRL            + 4           ЧПжЦНтГ§ДјЩВГЕЕФЧ§ЖЏжсЕФЩВГЕ    RMOD            + 5           AUTO/MANU ЕФЧаЛЛ    CSTR            + 6           дкаХКХON ЫВМфПЊЪМвЦЖЏ    HOME            + 7       !    дкаХКХON ЫВМфПЊЪМд­ЕуИДЮЛЖЏзїЁЃ    STP            + 8           ON ЃКПЩвЦЖЏЃЌOFF ЃКМѕЫйЭЃжЙ    RES            + 9           дкаХКХON ЫВМфБЈОЏЧхСу    SON            + :       %    НгЭЈзДЬЌЯТЫХЗўONЃЌЖЯПЊзДЬЌЯТЫХЗўOFF    MON            + ;          ЭЈаХЖЫПкСЌНгДђПЊ            -3%]  @    џџџџ           FB_IAIPOSITIONSERVER     	      SetError                  FB_SetError    ,               SetMasterError                  FB_SetMasterError    ,               SetMasterMessage                  FB_SetMasterMessage    ,            
   SetMessage                  FB_SetMessage    ,               SetStep        
             
   FB_SetStep    ,               Step            ,               MessageTimer                    TON    ,               Timer                    TON    ,               TimeOut    р     ,                   User           ,               Position           ,            	   StartTask            ,            	   StartHome            , 	              pAgent         7                                                          
   FB_IAIPCON        ,                  Done            ,               Abort            ,                        -3%]  @    џџџџ           FB_INITUSER           iUser            -               iCnt            -               i             -                      InitDone            -                        -3%]  @    џџџџ           FB_KEYBOARD           Trigger_xAutomatic                 R_TRIG    .               Trigger_xManual                 R_TRIG    .                Trigger_xData                 R_TRIG    . !              Trigger_xClear                 R_TRIG    . "              Trigger_xOn                 R_TRIG    . #              Trigger_xEsc                 R_TRIG    . $              RepeatTimer                    TON    . &              PC_PLC_GreenButton             . '                 InKeys                   structKeyBoardData   .               xKeyAutomatik            .            
   xKeyManual            .            
   xKeyRepeat            .            	   xKeyClear            .               xPressButton_Green            . 	              xPressButton_White            . 
              xPressButton_Red            .               xPressButton_Yellow            .               xButtonLight_StartIndicator            .               xButtonLight_RedIndicator            .               xButtonLight_GreenIndicator            .                  xButtonLight_Green            .               xButtonLight_White            .               xButtonLight_Red            .               xButtonLight_Yellow            .               Key                   structKeyBoardData   .                        -3%]  @    џџџџ        
   FB_KEYENCE           Timer                    TON    /            
   Start_Trig                 R_TRIG    /                KeyenceConfirm_Trig                 F_TRIG    / !              Step            / "                 Run            /               Start            /               Variant           /            
   Keyence_OR            /               Keyence_TrigConfirm            /               Keyence_ST0            /               Keyence_RUN            / 	                 Ready            /               Pass            /               Fail            /               Keyence_IN0            /               Keyence_IN1            /               Keyence_IN2            /               Keyence_IN3            /               Keyence_IN4            /               Keyence_IN5            /               Keyence_IN6            /               Keyence_IN7            /               Keyence_IN8            /               Keyence_IN9            /               Keyence_IN10            /               Keyence_IN11            /               Keyence_CST            /               Keyence_TRG            /                        -3%]  @    џџџџ           FB_LAMPS           TimeOn                    TON    0               xTrigger             0                	      H0T_On            0               H1T_Plus            0            	   H2T_Minus            0               H3T_Esc            0            	   H4T_Clear            0               H5_TopLightRed            0 	              H6_TopLightOrange            0 
              H7_TopLightGreen            0               xFlash            0           Blinker            -3%]  @    џџџџ           FB_LASCONTROL           SetError                  FB_SetError    1 
              SetMasterError                  FB_SetMasterError    1               SetMasterMessage                  FB_SetMasterMessage    1            
   SetMessage                  FB_SetMessage    1               SetTips               
   FB_SetTips    1               SetStep        
             
   FB_SetStep    1               WaitTime        
                FB_WaitTime    1               TimeOut                       
   FB_TimeOut    1               NOP                 NOP_FB    1               LasMethControl                                       FB_LasMethControl    1               EOLTest                                    FB_RequestResponseInteraction    1               ADS_LasFinishedPart                                    FB_RequestResponseInteraction    1               ADS_ReferencePart                                    FB_RequestResponseInteraction    1               ADS_LasGetNewPart                                      FB_GetNewPartInfo    1               ADS_LasScannerSr752                               FB_AdsDeviceInteraction    1                                -3%]  @    џџџџ           FB_LASMETHCONTROL           SetError                  FB_SetError    Z              SetMasterError                  FB_SetMasterError    Z              SetMasterMessage                  FB_SetMasterMessage    Z           
   SetMessage                  FB_SetMessage    Z              SetTips               
   FB_SetTips    Z              SetStep        
             
   FB_SetStep    Z              Step            Z              UpdateTarget             Z              TriggerClearMode                 R_TRIG    Z              StampRoundNumber            Z              CurrentSchedule                Z              EmptyCarrierInfo                         StructCarrierInfo    Z                 User           Z              LasType           Z              WT          Z              S_DUT_Presence            Z              LightCurtainControl            Z              pAgent                   StructStationCfg        Z              pEOL                 FB_RequestResponseInteraction        Z	              pADS_LasFinishedPart                 FB_RequestResponseInteraction        Z
              pADS_ReferencePart                 FB_RequestResponseInteraction        Z              pADS_LasGetNewPart                                       FB_GetNewPartInfo        Z              pADS_LasScannerSr752                                FB_AdsDeviceInteraction        Z                 Done            Z           	   CleanMode            Z                       -3%]  @    џџџџ           FB_MAINCONTROL           SystemPressure                    TON    "$              BranchK1AirPressure                    TON    "%              BranchK2AirPressure                    TON    "&              EtherCATErrorTimer                    TON    "'              Timer_PowerOn                    TON    ")              rTrig_PowerOn                 R_TRIG    "*           
   LasControl                             FB_LasControl    ",           
   StationCfg                  StructStationCfg    "-              McCfgSlaveCount             ".              ADR_OFF           "2              ADR_STOP          "3              ADR_RUN          "4              ADR_LASTCYCLE          "5                 Me           "              EmergencyStop            "              VirtualEmergencyStop            "              S2P_SystemAirPower            "              S1P_SystemAirPower            "              VirtualSystemAirPower            "	              ProtectionDoorKey1            "
              ProtectionDoorKey2            "              ProtectionDoorKey3            "              ProtectionDoorKey4            "              ProtectionDoorKey5            "              ProtectionDoorKey6            "              ProtectionDoorKey7            "              ProtectionDoorKey8            "              ProtectionDoorKey9            "              ProtectionDoorKey10            "              VirtualProtectionDoor            "              LightCurtainSafetyRelay            "              EStopSafetyRelay            "              SafetyDoorSafetyRelay            "                 K0_MainValve            "              K1_MainValve            "              K2_MainValve            "                       -3%]  @    џџџџ           FB_MANAGEOVERVIEWINFO           iCounter            J               AUTO    Q      AutoQ    J               MANUAL    Q      ManualQ    J               NONE    Q      ---Q    J                                -3%]  @    џџџџ           FB_MULTICYLINDER           iX            K               xAllCylinderAck             K               i            K               SetError                  FB_SetError    K               SetMasterError                  FB_SetMasterError    K               SetMasterMessage                  FB_SetMasterMessage    K            
   SetMessage                  FB_SetMessage    K               SetStep        
             
   FB_SetStep    K                  Me           K               JumpAddress           K               UsedCylinder           K            	   Cylinders   	  2                   structMultiCylinder           K                  MAX_CYLINDER    2      K                        -3%]  @    џџџџ           FB_OMRON           Timer                    TON    N            
   Start_Trig                 R_TRIG    N               Step            N                  Run            N               Start            N               Variant           N               Omron_OR            N               Omron_Error            N               Omron_Enable            N                  Ready            N               Pass            N               Fail            N               Omron_Reset            N            
   Omron_Trig            N               Omron_Bank0            N               Omron_Bank1            N               Omron_Bank2            N               Omron_Bank3            N               Omron_Bank4            N            	   Omron_DI5            N            	   Omron_DI6            N            	   Omron_DI7            N            	   Omron_DI8            N            	   Omron_DSA            N                        -3%]  @    џџџџ           FB_REPORTHANDLER           LastUser_MasterError            U               LastUser_Error            U               LastUser_MasterMessage            U               LastUser_Message            U               LastUser_Tips            U               ErrorID            U               iX            U               i            U            	   InfoTimer                    TON    U               InfoRun             U               InfoPointer            U               sTemp1    Q       Q     U               LastManualSelectedUser           U               LastErrorCode            U               MasterError    Q      MasterErrorQ    U                Error    Q      ErrorQ    U !              MasterMessage    Q      MasterMessageQ    U "              Message    Q      MessageQ    U #              Tips    Q      TipsQ    U $                     SelectedUser           U               ErrorMessageSet                      structErrorMessageSet   U                        -3%]  @    џџџџ           FB_REQUESTRESPONSEINTERACTION           SetError                  FB_SetError    Z               SetMasterError                  FB_SetMasterError    Z               SetMasterMessage                  FB_SetMasterMessage    Z            
   SetMessage                  FB_SetMessage    Z               SetTips               
   FB_SetTips    Z               SetStep        
             
   FB_SetStep    Z               Step            Z               SkipTask             Z           Skip EOL Test   MessageTimer                    TON    Z                EmptyRequestAction                   StructRequestAction    Z "              EmptyResponseAction                  StructResponseAction    Z #              EmptyFailedPartInfo                           StructFailedPartInfo    Z %              TimeOut     Ё     Z '                 User           Z            	   StartTask            Z               PositiveAction            Z               UpdateTarget            Z           Update Destination   Complete            Z           Final LineControl    pRequest                    StructRequestAction        Z 	           	   pResponse                   StructResponseAction        Z 
                 Done            Z               Abort            Z                        -3%]  @    џџџџ           FB_ROBOTPOSITIONSERVER     	      SetError                  FB_SetError    \              SetMasterError                  FB_SetMasterError    \              SetMasterMessage                  FB_SetMasterMessage    \           
   SetMessage                  FB_SetMessage    \              SetStep        
             
   FB_SetStep    \              Step            \              MessageTimer                    TON    \              Timer                    TON    \              TimeOut    р     \                 User           \              Run            \              Position           \           	   StartTask            \           	   StartHome            \              pAgent         $                                          FB_EPSON_ROBOT        \                 Done            \              Abort            \                       -3%]  @    џџџџ           FB_SETERROR               User           [               Title    )       )    [               Key           [               sValue01    )       )    [                            -3%]  @    џџџџ           FB_SETMASTERERROR               User           \               Title    )       )    \               Key           \               sValue01    )       )    \                            -3%]  @    џџџџ           FB_SETMASTERMESSAGE               User           ]               Title    )       )    ]               Key           ]               sValue01    )       )    ]                            -3%]  @    џџџџ           FB_SETMESSAGE               User           ^               Title    )       )    ^               Key           ^               sValue01    )       )    ^                            -3%]  @    џџџџ        
   FB_SETSTEP           iRequestedStepNumber            _               iActualStepNumber            _                  User           _               ExternalSign            _               JumpAddress           _               BackStep            _               Reset            _                  Ack            _               BackStepAck            _               Step           _                        -3%]  @    џџџџ        
   FB_SETTEXT               User           `            	   TextLine0    )       )    `               Text           `               sValue01    )       )    `                            -3%]  @    џџџџ        
   FB_SETTIPS               User                         Title    )       )                  Key                         sValue01    )       )                               -3%]  @    џџџџ           FB_SIGNCONTROL           iCounter            a                
      xHardwareHome            a               xSoftwareHome            a               xError            a               xMasterError            a               xMessage            a 	              xTips            a 
              xMasterMessage            a            
   xCalibrate            a               xOff            a            	   xStartOff            a                        -3%]  @    џџџџ           FB_STATION_01           WaitTime        
                FB_WaitTime    r 0              MessageTime                    TON    r 1           
   RedboxTime                    TON    r 2              TriggerRedBox                 R_TRIG    r 4              Trigger_xPrintLabel                 R_TRIG    r 5           %   mZ13_Cylinder_S1_Air_Supply_Connector        "                                        Cylinder_V2    r 8           !   mZ18_Cylinder_S1_Box_Illumination        "                                        Cylinder_V2    r 9           !   mZ09_Cylinder_S1_Red_Box_Big_Lock        "                                        Cylinder_V2    r :          backup   MultiCylinder                           FB_MultiCylinder    r <              Status_S1_Red_Box_Big_Lock             r =              EmptyDeviceInteraction                   StructDeviceInteraction    r ?           
   LasControl                             FB_LasControl    r @           
   StationCfg                  StructStationCfg    r A              Scanner                   StructDeviceInteraction    r C                 Me           r               Master           r               xPressButton_PrintLabel            r               Sz13a_S1_Air_Supply_Connector            r               Sz13b_S1_Air_Supply_Connector            r               Sz18a_S1_Box_Illumination            r 	              Sz18b_S1_Box_Illumination            r 
              Sz16b_S1_Red_Box_Front_Lock            r               Sz16b_S1_Red_Box_Product_in            r               Sz17a_S1_Red_Box_Big_Lock            r               Sz17b_S1_Red_Box_Big_Lock            r               S_S1_Fixture_Presence            r               S_S1_DUT_Presence            r               S_S1_Adapter_Presence            r               S_S1_Airbag_Cable_Presence            r               S_S1_Red_Box_Close            r               pRequest                    StructRequestAction        r            	   pResponse                   StructResponseAction        r               pRequestFinished                    StructRequestAction        r               pResponseFinished                   StructResponseAction        r               pScanner                    StructDeviceInteraction        r               %   z13a_Cylinder_S1_Air_Supply_Connector            r !           %   z13b_Cylinder_S1_Air_Supply_Connector            r "              z14b_Cylinder_S1_Fix            r #              z15b_Cylinder_S1_Mark            r $           #   z16b_Cylinder_S1_Red_Box_Front_Lock            r %           !   z17b_Cylinder_S1_Red_Box_Big_Lock            r &           !   z18a_Cylinder_S1_Box_Illumination            r '           !   z18b_Cylinder_S1_Box_Illumination            r (           
   xLineLight            r *              xCircleLight            r +           	   xBoxLight            r ,                       -3%]  @    џџџџ           FB_STATION_02           MultiCylinder                           FB_MultiCylinder                '   mZ01_Cylinder_S3_Stator_Right_Connector        "                                        Cylinder_V2                
   LasControl                             FB_LasControl                
   StationCfg                  StructStationCfg                      Me                          Master                          Sz01a_S2_Stator_Right_Connector                           Sz01b_S2_Stator_Right_Connector                           pRequest                    StructRequestAction         	           	   pResponse                   StructResponseAction         
              '   z01a_Cylinder_S2_Stator_Right_Connector                        '   z01b_Cylinder_S2_Stator_Right_Connector                                    -3%]  @    џџџџ           FB_STATION_03           MultiCylinder                           FB_MultiCylinder                '   mZ01_Cylinder_S3_Stator_Right_Connector        "                                        Cylinder_V2                
   LasControl                             FB_LasControl                
   StationCfg                  StructStationCfg                      Me                          Master                          Sz01a_S3_Stator_Right_Connector                           Sz01b_S3_Stator_Right_Connector                           pRequest                    StructRequestAction                    	   pResponse                   StructResponseAction         	              '   z01a_Cylinder_S3_Stator_Right_Connector                        '   z01b_Cylinder_S3_Stator_Right_Connector                                    -3%]  @    џџџџ           FB_STATION_04           MultiCylinder                           FB_MultiCylinder               '   mZ01_Cylinder_S3_Stator_Right_Connector        "                                        Cylinder_V2               
   LasControl                             FB_LasControl               
   StationCfg                  StructStationCfg                     Me                         Master                         Sz01a_S3_Stator_Right_Connector                          Sz01b_S3_Stator_Right_Connector                          pRequest                    StructRequestAction                   	   pResponse                   StructResponseAction        	              '   z01a_Cylinder_S3_Stator_Right_Connector                       '   z01b_Cylinder_S3_Stator_Right_Connector                                   -3%]  @    џџџџ           FB_STEPCONTROL               User           Ѕ               ExternalSign            Ѕ               ExternalNoContinue            Ѕ               JumpAddress           Ѕ               BackStep            Ѕ        '    If False (default) Backstep is denied    Reset            Ѕ                  Ack            Ѕ               BackStepAck            Ѕ               Step           Ѕ                        -3%]  @    џџџџ           FB_TABLE           WaitTime        
                FB_WaitTime    X'           	   StartTime                    TON    X(              MoveTime                    TON    X)              RunOverTime                    TON    X*              TablePosTimer                    TON    X+           
   ResetAlarm                    TON    X,           
   StartPulse                 R_TRIG    X-           
   QuittPulse                 R_TRIG    X.              OffPulse             X/              PhysicalPosition            X1              TheoreticalPosition            X2              ActualStepNumber            X4              ReturnAddress            X5           	   MoveTable             X7              MeetConditions             X9              SafePositions             X:              iCounter            X<           
   iCarrierNr            X=              MaxUserCount            X>           
   LasControl                             FB_LasControl    X?           
   StationCfg                  StructStationCfg    X@                 Me           X              Master           X              FromUser           X              ToUser           X              S_0bit            X	              S_1bit            X
              S_2bit            X              S_3bit            X              S_4bit            X              TableinPosi            X              ReadytoStart            X           	   Automatic            X              Alarm_TimeOut            X              Alarm_Overrun            X              Alarm            X              ClockwiseDirection            X           
   	   Direction            X              Start_level            X           
   Start_edge            X              SoftwareEnable            X              SpecialMode            X           	   ParamSet3            X           	   ParamSet2            X            
   AlarmReset            X!              Relay            X"              RoundCounter           X#                       -3%]  @    џџџџ        
   FB_TIMEOUT           TimeOutTime                    TON    е               SetError                  FB_SetError    е               SetMasterError                  FB_SetMasterError    е               SetMasterMessage                  FB_SetMasterMessage    е            
   SetMessage                  FB_SetMessage    е               SetStep        
             
   FB_SetStep    е                  User           е            	   TextLine0    )       )    е               Text           е               sValue01    )       )    е               xEnable            е 	              ExternalTimeOut           е 
                           -3%]  @    џџџџ           FB_WAITTIME        	   WaitTimer                    TON    ж               SetStep        
             
   FB_SetStep    ж                  User           ж               Start            ж               WaitTimeValue           ж               JumpAddress           ж               UpDate            ж               BackStep            ж                  Ack            ж               BackStepAck            ж                        -3%]  @    џџџџ           GETSTATIONNAME           CON_MAX    d       з               CON_MIN            з            
   TempString    3       3     з                  Station           з                  GetStationName    Q       Q                              -3%]  @    џџџџ           MAIN           SetError                  FB_SetError    л               SetMasterError                  FB_SetMasterError    л               SetMasterMessage                  FB_SetMasterMessage    л            
   SetMessage                  FB_SetMessage    л               SetStep        
             
   FB_SetStep    л               InitFlag             л               Alarm             л 	              ix_BI            л 
              ix_PI            л               ix_CO            л               ix_FE            л               ix_X            л               ix_Y            л               Manual             л               PowerOn             л               Reset             л                                -3%]  @    џџџџ           NOP_FB           SetStep        
             
   FB_SetStep    о 	                 Me           о            
   BackEnable           о                            -3%]  @    џџџџ           STRINGTOFAILPARTINFO           CON_MAX    d       п 
              CON_MIN            п            
   CON_MAXROW    
       п               CON_MAXPARTINFO           п               CON_SPLITINFO          #     п               CON_SPLITVALUE          :     п               i           п               j           п               TempFailedPartInfo                           StructFailedPartInfo    п            
   TempString   	  
       3       3             п               TempTitleString   	  
                           п               TempValueString   	  
       3       3             п               TempPartTitleString   	                             п               TempPartValueString   	         3       3             п            	   Index7001                            User           п        W   Compatiable as before, you cas see User as CarrierNr if your line is an auotmation one    Station           п               Str               п            
   ManualLine            п                  StringtoFailPartInfo                           StructFailedPartInfo                             -3%]  @    џџџџ        	   SYSTEM_FB           iX            ё               Timer                    TON    ё            
   GetSYSTime                   
   NT_GetTime    ё               OverviewInfo                  FB_ManageOverviewInfo    ё               TempHour    Q       Q     ё            
   TempMinute    Q       Q     ё            
   TempSecond    Q       Q     ё                      MaxTimeTask   	                        ё               ActualTimeTask   	                        ё               MaxTimeTaskInMilliSecond   	                         ё               ActualTimeTaskInMilliSecond   	                         ё 	              wHour           ё 
              wMinute           ё               wSecond           ё               sTime    Q       Q    ё                        -3%]  @    џџџџ           TEXTFILE           local_Number            ѓ            	   localText    Q       Q     ѓ 	              LANGUAGE_OFFSET    ш     ѓ                  Number           ѓ               Language           ѓ                  TextFile    Q       Q                              -3%]  @    џџџџ           UPDATEDESTINATIONSTATION               User           є            	   StationID           є            
   TestResult            є                  UpdateDestinationStation                                      -3%]  @    џџџџ            
  3 X  i  h  f  d  a  "  %     Ѓ   Ђ   Є   Ё                  ^   №   я   U   W      х   ]   е     \   j  l  e  `  m  э   c  ^  _  g  Z  [  \  ]  b  (  )  +  &  #  ю   и   ( %     K   3    K   A    K   O    K   d                q        +     КЛlocalhost кXv   Јћ     H `.ё@з Hз 4й lи саw6ўџџџч/дw.дwЈћ           Јћ       `к Twэ №№   0.ёи ш,дw8.ёF  4й 4й UKћ џџџџ    pеЕи             |и Јћ          Јћ       `к Twэ `к 4й Twэ л_џџџџ@й ]"э     ,   ,                                                        K        @   -3%]F /*BECKCONFI3*/
        !
Дo9 @   @           3               
   Standard            	45^     tEor====           VAR_GLOBAL
END_VAR
                                                                                  "   ,                Standard
         MAINџџџџ               -3%]                 $ћџџџ  rrTir[(i                                  Standard ЇU	ЇU                                       	-3%]     04
	ro=T           VAR_CONFIG
END_VAR
                                                                                   '              , њ њ wё           ADS_Variables -3%]	-3%]         H'mm        ]  VAR_GLOBAL CONSTANT
	CON_REAL_STATIONS			: INT:=3;(*Real Assemble or EOL station*)
END_VAR


VAR_GLOBAL
	(*ADS*)
	PLC_stuEndTestRequest1					: StructRequestAction;
	ADS_stuEndTestResponse1				: StructResponseAction;

	PLC_stuEndTestRequest2					: StructRequestAction;
	ADS_stuEndTestResponse2				: StructResponseAction;

	PLC_stuEndTestRequest3					: StructRequestAction;
	ADS_stuEndTestResponse3				: StructResponseAction;

	ADS_stuScannerSt01						: StructDeviceInteraction;
	ADS_stuPrinterSt01						: StructDeviceInteraction;

	(*Free Test*)
 	PC_PLC_GreenButton                                                   : BOOL;(*Free Test*)
         PC_PLC_RedButton                                                     	 : BOOL;
         PC_PLC_WhiteButton                                                    : BOOL;
         PLC_PC_GreenButton                                                   : BOOL;
         PLC_PC_RedButton                                                       : BOOL;
         PLC_PC_WhiteButton                                                    : BOOL;
	HMI_OKReset 							 : BOOL;


	Station01						: FB_Station_01;(*firstly put the material into the machine*)
	Station02						: FB_Station_02;(*dynamic test *)
	Station03						: FB_Station_03;(*static test*)
	BigTable					: FB_Table;(*control the big table*)
END_VAR
                                                                                               '           и   , с с ^и           LAS_Variables -3%]	45^и     arblison        Ѕ  (*** Read Me:  Abbreviation Instructions:   int--> INT;  arr--> ARRAY;  bul-->BOOL; byt-->BYTE; stu--> STRUCT;***)
(*** Read Me:  Abbreviation Instructions:   uin--> UINT;  str--> STRING; din--> DINT; pt--> POINTER ***)
(*** Read Me:  Kostal Abbreviatios:  LAS-->Line Administration System; LC-->Linecontroller;  ASSM-->Assembly; WT--> Carrier***)
(*** Read Me:  Any reset to a variable is considered as value assignment , e.g. strValue=''; is regarded as a writting operation***)


(***============================================================== ***)
(***============================================================== ***)
(***======LAS PLC INTERFACE INTERNAL VERSION1.22 2017.4.6======***)
(***============================================================== ***)
(***============================================================== ***)


(*==============Standard Kostal ADS Variables, for each AUTOMATION LINE OF KOSTAL ASIA==============*)
VAR_GLOBAL

(* The Header PC_xxxs  mean they are just for LAS program to write. Except reading, any others to write them is prohibited!!!*)
	PC_arrScheduleList						: ARRAY[1..CON_MAXIMUM_SCHEDULES] OF StructScheduleMode; 		(*ADS Interface between PC and PLC, PC write it once when initializing*)
	PC_stuCurrentVariantInfo					: StructVariantInfo;													(*ADS Interface between PC and PLC, PC write variant infromation to PLC before each carrier starts to flow in line.  *)
	PC_bytCurrentScheduleNr					: BYTE;															(*ADS Interface between PC and PLC, PC write it into PLC before each carrier starts to flow.  *)
	PC_bulNewPartAvailable					: BOOL;					(* used as a couple with PLC_bulGetNewPart*)
	PC_bulScanPartRequest					: BOOL;					(*used to make PLC to indicate infomation for operator*)
	PC_bulScannedPartResult					: BOOL;					(*used to check whether scanned part is what we want or not*)


(* The Header ADS_xxxs  mean they are both for PC and PLC program to write. Any others to use them is also permitted!*)
	ADS_stuAssmDataResponse				: StructResponseAction;	(* used  as a couple with PLC_stuAssmDataRequest, for responsing result of ASSM LC*)
	ADS_stuFinishedPartResponse				: StructResponseAction;	(* used as a couple  with PLC_stuFinishedPartRequest, for responsing result of Data Process after receiving request*)
	ADS_stuReferencePartResponse			: StructResponseAction;	(* used as a couple  with PLC_stuFinishedPartRequest, for responsing result of Data Process after receiving request*)


(* The Header PLC_xxxs  mean they are just for PLC program to write.Except reading, any others to write them is prohibited!!!*)
	PLC_stuFailedPartInfo						: StructFailedPartInfo;												(*ADS Interface between PC and PLC, PLC wil pass the info to PC while getting failed part off the line*)
	PLC_bulGetNewPart						: BOOL;					(* used as a couple with PC_bulNewPartAvailable*)
	PLC_stuAssmDataRequest					: StructRequestAction;		(* used as a couple with ADS_stuAssmDataResponse, for requesting ASSM LC*)
	PLC_stuFinishedPartRequest				: StructRequestAction;		(* used as a couple with ADS_stuFinishedPartResponse, for requesting Data Process after finished part-test*)
	PLC_stuReferencePartRequest				: StructRequestAction;		(* used as a couple with ADS_stuFinishedPartResponse, for requesting Data Process after finished part-test*)

END_VAR

(* Retaining data when power loss!!!*)
VAR_GLOBAL PERSISTENT
(* The Header PLC_xxxs  mean they are just for PLC program to write.Except reading, any others to write them is prohibited!!!*)
	PLC_arrCarrierInfo							: ARRAY[1..CON_MAXIMUM_TOTAL_CARRIERS] OF StructCarrierInfo;	(*ADS Interface between PC and PLC, PC can read or inspect any elment of it*)
END_VAR

(*==============Standard PLC&PC Constants, For each AUTOMATION LINE OF KOSTAL ASIA==============*)
(* These constants could be changed depending actual situations.
 like if your line has a total of 24 real stations, you could set CON_MAXIMUM_REAL_STATIONS to 24*)
VAR_GLOBAL CONSTANT

	CON_MAXIMUM_SCHEDULES				: UINT:=50;			(*DO NOT change me!!!*)
	CON_MAXIMUM_TOTAL_CARRIERS		: UINT:=30;			(*DO NOT change me!!!*)
	CON_MAXIMUM_REAL_CARRIERS			: UINT:=50;			(*DO NOT change me!!!*)
	CON_MAXIMUM_TOTAL_STATIONS		: UINT:=100;			(*DO NOT change me!!!*)
	CON_MAXIMUM_REAL_STATIONS			: UINT:=20;			(*you can change it's value due to actual situations!*)
	CON_MAXIMUM_ASSEMBLY_STATIONS	: UINT:=20;			(*you can change it's value due to actual situations!*)
	CON_MAXIMUM_Destination				: UINT:=10;			(*DO NOT change me!!!*)
	KostalBasicSchedules						: StructCommonSchedule;
	LasRequestTypes						: structLasRequestType;
	LasMethType								:StructLasMethType;

END_VAR


(*==============NON-Standard Kostal ADS Variables, For Mlb evo SCM project==============*)
(* The Header EOL_xxx/VIS_xxxs/HAP_xxxs  mean they are just for tester program to write. Except reading, any others to write them is prohibited!!!*)
(* The Header PLC_EOL_xxx/PLC_VIS_xxxs/PLC_HAP_xxxs  mean they are just for PLC to write. Except reading, any others to write them is prohibited!!!
	and PLC_EOL/PLC_VIS/PLC_HAP mean who will be using them, in other words they are transmitted by directions*)
VAR_GLOBAL

	PLC_arrOverviewInfo						: ARRAY[1..CON_MAXIMUM_REAL_STATIONS] OF structStationOverviewInfo;
	PLC_stuErrorMessage						: structErrorMessageSet;

	PC_bulResetError							: BOOL;(*LAS Reset Error*)
	PLC_Reference_Sensor					: BOOL;(*Reference Parts Position*)
	LAS_Reference_Fail						: BOOL;(*Reference Parts Position*)
	LAS_Retest_Fail							: BOOL;(*Reference Parts Position*)
	ADS_stuPrinterSt02			:StructDeviceInteraction;

	HMI_Debug					: StructDebugPage;
	HMI_ShortcutButton			: ARRAY[1..10] OF BOOL;
	HMI_DebugMode				: BOOL;
	ADS_Step					: INT;
END_VAR                                                                                               '           ђ   , Ш Ш EП           System_Variables -3%]	-3%]ђ      V s a         У  
VAR_GLOBAL CONSTANT
	Text						: structTextData;
	MAXARTICLEARRAY		: INT:= 10;
	TimeDelay				: TIME:=t#5000ms;
END_VAR

VAR_GLOBAL
	(*======================================================================*)
	(* Device *)
	(*======================================================================*)
	ProjectData				: structProjectData;
	System					: System_FB;
	KeyBoard				: FB_KeyBoard;
	Lamps					: FB_Lamps;
	HMIMessage				:StructHMIMessage;
	HMI						: SmallKeyBoard_FB;

	(*======================================================================*)
	(*Init*)
	(*======================================================================*)
	InitUser					: FB_InitUser;
	InitPulse					: BOOL;

	(*======================================================================*)
	(*System Moduls*)
	(*======================================================================*)
	MainControl				: FB_MainControl;
	ReportHandler			: FB_ReportHandler;

	(*======================================================================*)
	(* Library *)
	(*======================================================================*)
	CheckDoubleSense		: FB_CheckDoubleSense;
	SignControl				: FB_SignControl;


	StationEnable			:INT:=0;


	Language						: SINT;

(*====EtherCAT watchdog=========================================================================================*)
	CfgSlaveCount							AT %I*		: UINT;
	SlaveCount								AT %I*		: UINT;

	(*======================================================================*)
	(* Hardware *)
	(*======================================================================*)


	EL_DI				AT %I*	: ARRAY[1..4,0..0] OF BYTE;			(*  Number, Byte *)
	EL_DO				AT %Q*   : ARRAY[1..4,0..0] OF BYTE;		(*  Number, Byte *)

	EP_DI				AT %I*	: ARRAY[1..10,0..0] OF BYTE;		(* Number, Byte *)

	FESTO_DO			AT %Q*   : ARRAY[1..4,0..5] OF BYTE;		(* Number, Byte *)

	BK_AI				AT %I*	: ARRAY[1..10,1..4] OF UINT;		(* User, UINT *)

END_VAR

VAR_GLOBAL

	(*======================================================================*)
	(* Stations *)
	(*======================================================================*)
	Stations						: structStationData;
END_VAR







                                                                                               '           	   ,              Variable_Configuration -3%]	-3%]	     d6џџ              VAR_CONFIG
END_VAR
                                                                                                    |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ssѓџџџ                               4     џ   џџџ  Ь3 џџџ   џ џџџ     
    @џ  џџџ     @      DEFAULT             System         |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ssѓџџџ                      )   HH':'mm':'ss @                             dd'-'MM'-'yyyy @       '   "   ,              StructCarrierInfo -3%]	-3%]      ,	','0,	        \  
(*=========================*)
(*========= V2.0.0.0  ========*)
(*========2018-07-24========*)
(*=========================*)
TYPE StructCarrierInfo :
STRUCT
		bytDestinationStation 					: BYTE :=0;			(*to indicate destination station number which carrier should go by*)
		bytScheduleModeNr					: BYTE :=0;			(*to save  test mode number that carrier has got from LAS *)
		bytCycleNr							: BYTE :=0;			(*to save the test circle number which DUT is running in *)
		bytAssemblyCounter					: BYTE :=0;			(*to count how many assembly actions have been executed successfully*)
		bytScrewPassCounter					: BYTE :=0;			(*to indicate how many times of Screwing have been carried out *)
		bytTestRepeatCounter					: BYTE :=0;			(*to indicate how many test times have been carried out *)
		bulReferencePart						: BOOL :=FALSE;		(*to indicate that current DUT is a reference part or not, True seen as yes *)
		bulTestResult							: BOOL :=TRUE;		(*to indicate that current DUT is test passed or not, True seen as passed *)
		strScheduleName						: STRING(20):='ClearMode';
		stuVariantInfoSet						: StructVariantInfo;		(*to save variant data that carrier has got from LAS *)
		stuFailedPartInfo						: StructFailedPartInfo;	(*to save failed infomation while DUT test failed *)
		(*arrAdditionalMessage				: ARRAY[1..10] OF STRING(50) :='';*)
END_STRUCT
END_TYPE             р   ,              StructCommonSchedule -3%]	-3%]                      0  (*=========================*)
(*========= V1.0.0.1  ========*)
(*=========================*)
TYPE StructCommonSchedule :
STRUCT
	ProductionMode						: STRING(20):='ProductionMode';
	RetestMode							: STRING(20):='RetestMode';
	MasterPartTest						: STRING(20):='MasterPartTest';
	SelfResistanceTest					: STRING(20):='SelfResistanceTest';
	ClearMode							: STRING(20):='ClearMode';
	AssemblyOnly						: STRING(20):='AssemblyOnly';
	StandAlone							: STRING(20):='StandAlone';
	UserDefined							: STRING(20):='UserDefined';
END_STRUCT
END_TYPE             с                        StructConfirmSignal -3%]	-3%]                      Ї   TYPE StructConfirmSignal :
STRUCT
	bulActionIsPass				: BOOL:=FALSE;
	bulActionIsFail				: BOOL:=FALSE;
	bulActionIsContinue			: BOOL:=FALSE;
END_STRUCT
END_TYPE             т                        StructDebugCylinder -3%]	-3%]                      r   TYPE StructDebugCylinder :
STRUCT
	bulDOA						: BOOL:=FALSE;
	bulDOB						: BOOL:=FALSE;
END_STRUCT
END_TYPE             у                        StructDebugPage -3%]	-3%]                        TYPE StructDebugPage :
STRUCT
		arrDI_EL1008				: ARRAY[1..20,0..7] OF BOOL;
		arrDO_EL2008			: ARRAY[1..20,0..7] OF BOOL;
		arrDI_EP1008				: ARRAY[1..20,0..7] OF BOOL;
		arrCylinder				: ARRAY[1..10,0..7] OF StructDebugCylinder;
END_STRUCT
END_TYPE             ф   , } } њt           StructDestinationStation -3%]	-3%]                         TYPE StructDestinationStation :
STRUCT
		arrDestinationStationData				: ARRAY[1..CON_MAXIMUM_Destination] OF BYTE:=0;
END_STRUCT
END_TYPE                , Ш Ш З           StructDeviceInteraction -3%]	-3%]      X H§@И%          TYPE StructDeviceInteraction :
STRUCT
	stuPlcArticleSet				: StructVariantInfo;
	bulPlcDoAction				: BOOL:=FALSE;
	bulAdsActionIsPass			: BOOL:=FALSE;
	bulAdsActionIsFail				: BOOL:=FALSE;
	strAdsActionValue				: STRING(255) :='';
END_STRUCT
END_TYPE             х   , } } њt           structErrorMessageSet -3%]	-3%]                      Е  
(*=========================*)
(*========= V1.0.0.0  ========*)
(*=========================*)
TYPE structErrorMessageSet :
STRUCT
	iKeyUser						: INT:=0;
	iErrorCode					: INT:=0;
	strErrorType					: STRING(20):='';
	strErrorSource				: STRING(20):='';
	strErrorValue					: STRING(20):='';
	strRaisedTime				: STRING(20):='';
	strErrorTitle					: STRING(50):='';
	strErrorMessage				: STRING(255):='';
END_STRUCT
END_TYPE                , Џ Џ ,І           StructFailedPartInfo -3%]	-3%]            S        h  TYPE StructFailedPartInfo :
STRUCT
		strFailKostalNr							: STRING(20);		(* Kostal number without Index, e.g. 10110201*)
		strFailSerialNr							: STRING(50);		(* Kostal serial number. e.g. 13 digitals *)
		strFailScheduleName						: STRING(20);		(* Schedule test name,  e.g. Normal_test*)
		strFailTestStatus							: STRING(20);		(* Test status or mode,  e.g. Assemably, PRE, EOL*)
		strFailCarrierNr							: STRING(20);		(* What carrier's number,  e.g. WT10*)
		strFailStationNr							: STRING(20);		(* Station number of failed part,  e.g. ST10*)
		strFailTestStep							: STRING(20);		(* Failed test step number , e.g. 1000.101*)
		strFailCode								: STRING(20);		(* Error code of failed part, e.g. A0F0*)
		strFailText								: STRING(255);	(* Description of failed step, e.g. Spring doesn't exist!*)
		strFailValue								: STRING(20);		(* Failed Value, e.g. 13.95*)
		strFailLowerLimit							: STRING(20);		(* Lower test limit, e.g. 12.50 *)
		strFailUpperLimit							: STRING(20);		(* Upper test limit, e.g. 13.60 *)
		strFailUnit								: STRING(20);		(* Value Unit, e.g. Voltage*)
END_STRUCT
END_TYPE             ц     
SUC
	uP           StructHMIMessage -3%]	-3%]      FAE;	sAd        p   TYPE StructHMIMessage :
STRUCT
	strErrorTitle					:STRING;
	strErrorMessage				:STRING;
END_STRUCT
END_TYPE             ч   ,              StructLasMethType -3%]	-3%]                      |  
TYPE StructLasMethType :
STRUCT
	StepFail						:INT:=1;
	Reset						:INT:=2;
	MainReset					:INT:=3;
	TableReset					:INT:=4;
	Off							:INT:=5;
	OffMainStart					:INT:=6;
	OffStart						:INT:=7;
	End							:INT:=8;
	CalibrateEnd					:INT:=9;
	SoftwareHome				:INT:=10;
	CheckTableDone				:INT:=11;
	CheckStationDestination		:INT:=12;
	ProcessRelease				:INT:=13;
	ProcessPass					:INT:=14;
	ProcessFail					:INT:=15;
	CheckResult					:INT:=16;
	ST1CheckStationDestination	:INT:=17;
	CheckClearMode				:INT:=18;
	CheckScanResult				:INT:=19;
	ST1ProcessRelease			:INT:=20;
	FinalCheckResult				:INT:=21;
	FinalProcessPass				:INT:=22;
	FinalProcessFail				:INT:=23;
	FinalProcessRelease			:INT:=24;
	GotoTest					:INT:=25;
	FinalCheckPassFail			:INT:=26;
	UpdateRefrencePart			:INT:=27;
	UnLoad						:INT:=28;
	UnLoadRelease				:INT:=29;
END_STRUCT
END_TYPE             ш   , Џ Џ ,І           structLasRequestType -3%]	-3%]      	:T:;Ca        h  
(*=========================*)
(*========= V1.0.0.0  ========*)
(*=========================*)
TYPE structLasRequestType :
STRUCT
	Default						: STRING(20):='';
	DoCompare1					: STRING(20):='DoCompare1';
	DoCompare2					: STRING(20):='DoCompare2';
	DoQuery1					: STRING(20):='DoQuery1';
	DoQuery2					: STRING(20):='DoQuery2';
END_STRUCT
END_TYPE             щ     RU
eflt           structMultiCylinder -3%]	-3%]      er';ENST        y   TYPE structMultiCylinder :
	STRUCT
		pCylinder	: POINTER TO Cylinder_V2;
		Direction		: BOOL;
	END_STRUCT
END_TYPE
             ъ     T:1;	Fal           structProjectData -3%]	-3%]      T:6;	Uat        Њ   TYPE structProjectData :
	STRUCT
		Project_ID					: STRING;
		Trace_ID					: STRING(5);
		Drawing_ID					: STRING;
		PLC_Version					: STRING;
	END_STRUCT
END_TYPE             %   ,   й           StructRequestAction -3%]	-3%]      cрП	 В        	  TYPE StructRequestAction :
STRUCT
		bulRunning						: BOOL:=FALSE;
		bulDoPositiveAction				: BOOL:=FALSE;
		bulDoNegativeAction				: BOOL:=FALSE;
		strActionScheduleName			: STRING(20):='';
		stuActionArticleSet					: StructVariantInfo;
END_STRUCT
END_TYPE             &   , 2 2 !ђ           StructResponseAction -3%]	-3%]      C ly
tr        e  TYPE StructResponseAction :
STRUCT
		bulPartReceived				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		bulActionIsPass				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		bulActionIsFail				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		strActionResultText			: STRING(255) :='';				(*written by PLC or LAS or Eol*)
END_STRUCT
END_TYPE             #   , 2 2 Џ)           StructScheduleMode -3%]	-3%]      Y,RRER E        {  
(*=========================*)
(*========= V2.0.0.0  ========*)
(*========2018-07-24========*)
(*=========================*)
TYPE StructScheduleMode:
STRUCT
		bytScheduleNr				: BYTE:=0;						(* Number of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
		strScheduleName				: STRING(20):='';					(* Name of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
		strScheduleDescription			: STRING(50):='';					(* Description about the schedule mode   *)
		bulReferenceSchedule			: BOOL :=FALSE;					(* to indicate the schedule is used for reference verification or not *)
		intSecurityChecksum			: INT :=0;							(* Checksum of arrScheduleData[x] *)
		arrScheduleData				: ARRAY[1..CON_MAXIMUM_TOTAL_STATIONS,0..1] OF StructDestinationStation;		(*  Array of destination of carriers, 1...50 ==>Station Number, 0...1==> Test Result*)
END_STRUCT
END_TYPE

             ы   , Џ Џ ,І           structStation -3%]	-3%]      				STNG        б  (*=========================*)
(*========= V4.0.0.0  ========*)
(*=========================*)
TYPE structStation :
	STRUCT
		(*Station ID*)
		iUser					: INT;		(*Internal user id*)
		bStationID				: BYTE;		(*used for ScheduleList*)
		StationName				: STRING(20);

		(*Step Status*)
		xOn						: BOOL;
		xCalibrate				: BOOL;
		xStop					: BOOL;
		xRun					: BOOL;
		xLastCycle				: BOOL;
		xStart					: BOOL;
		OffPulse					: BOOL;
		SoftSwitchedOn			: BOOL;

		(*Stationstatus*)
		xSoftwareHome			: BOOL;
		xHardwareHome			: BOOL;

		xAutomatic				: BOOL;
		xManual					: BOOL;

		xMasterError				: BOOL;
		xError					: BOOL;
		xMasterMessage			: BOOL;
		xMessage				: BOOL;
		xTips					: BOOL;

		xLightCurtainRelay			: BOOL;

		(*StepControl*)
		iRequestedStepNumber	: INT;
		iActualStepNumber		: INT;
		xToggle					: BOOL;		(*Stepnumber changed*)
		iFailRequestedStepNumber: INT;

		iAddressSoftwareHome	: INT;
		iAddressProcess			: INT;
		iAddressFinal				: INT;
		iAddressFinalPass		: INT;
		iAddressFinalFail			: INT;
		iAddressFinalPost			: INT;

		iAddressRedoTest		: INT;
		iAddressFinalRedoTest	: INT;
		iAddressPass			: INT;
		iAddressFail				: INT;
		iAddressAbort			: INT;
		iAddressRelease			: INT;
		iAddressPost				: INT;
		iCurrentWT				 :INT:=1;
(*
		iAddressDummy			: INT;
		iAddressCycle			: INT;
		iAddressReady			: INT;
		iAddressVariantCheck		: INT;
*)
		(*Messages*)
		TextLine0				: STRING(40);
		iText					: INT;
		sValue01				: STRING(40);

		StepLog					: STRING(255):='';
		Step					: FB_StepControl;
		TimeOut					: FB_TimeOut;
		WaitTime				: FB_WaitTime;

		xDone					: BOOL:=TRUE;
		xStationDisable			: BOOL:=FALSE;
		tProcessTime			: TIME;

		xCycle					: BOOL:=FALSE;

		Signalto_PartitionControl	:BOOL;


		xStationToTable			:BOOL;
		xTableToStation			:BOOL;

		xSafePosition				:BOOL;

		iCarrierNr				: INT;

		pWT					: POINTER TO StructCarrierInfo;
	END_STRUCT
END_TYPE
             ь   ,              StructStationCfg -3%]	-3%]      ENFUTI
        К   TYPE StructStationCfg :
STRUCT
	iDebugStepNumber		: INT;
	Data					:  POINTER TO  structStation;
	iCarrierNr				: INT;
	WT						: POINTER TO StructCarrierInfo;
END_STRUCT
END_TYPE             э   , } } њt           structStationData -3%]	-3%]      o;
ETN           TYPE structStationData :
	STRUCT
		Data				: ARRAY [1..45] OF structStation;
		StartStations			: INT;
		MaxUser				: INT;
		System				: INT;	(*Fixed User - never change this entry*)
		Main				: INT;	(*Fixed User - never change this entry*)
		SelectedStation		: INT;

(*Stations*)
		iStation			: ARRAY [1..50] OF INT;
(*Tables*)
		iPartitionControl		: INT;
		iBigTable			: INT;

	END_STRUCT
END_TYPE             ю   , d d с[           structStationOverviewInfo OFt^	OFt^      IO
NIO
_        Г  (*=========================*)
(*========= V1.0.0.2  ========*)
(*=========================*)
TYPE structStationOverviewInfo :
STRUCT
	iKeyUser						: INT:=0;
	iCarrierNumber				: INT:=-1;
	iDestinationStation				: INT:=0;
	iActualStepNumber			: INT:=-1;
	iRequestedStepNumber		: INT:=-1;
	iTestmanPassCounter			: INT:=0;
	iTestmanFailCounter			: INT:=0;
	xTestResult					: BOOL;
	xPromptError					: BOOL;
	xPromptMessage				: BOOL;
	PLC_bulArticleDisable			: BOOL;
	strStationName				: STRING(20):='INVALID STATION';
	strStationStatus				: STRING(20):='';
	strAutoManual				: STRING(20):='';
	strTestmanStatus				: STRING(20):='';
	strTestmanPercent			: STRING(20):='';
	strTestmanDppm				: STRING(20):='';
	strScheduleName				: STRING(20):='';
	strArticleNumber				: STRING(20):='';
	strProcessTime				: STRING(20):='';
	strSerialNumber				: STRING(50) :='';
	strStepLog					: STRING(255) :='';


END_STRUCT
END_TYPE             я   , 2 2 Џ)        
   structText -3%]	-3%]      D_NCONfo        А  TYPE structText :
	STRUCT

		PLeaseWait					: INT:= 8;
		OnSwitchManualAndOn_F5		: INT:= 9;

		EmptyString					: INT:= 80;
		Clock						: INT:= 88;
		Day							: INT:= 89;
		Month						: INT:= 90;
		Year						: INT:= 91;
		Hour						: INT:= 92;
		Minute						: INT:= 93;
		Second						: INT:= 94;
		Step						: INT:= 95;
		NewValue					: INT:= 96;
		Yes							: INT:= 97;
		No							: INT:= 98;
		ProgramError					: INT:= 99;

		Counter						: INT:= 102;
		Language					: INT:= 103;
		TimeOut						: INT:= 104;

		Fail							: INT:= 200;
(*		SensorFail					: INT:= 201;*)
		CylinderFail					: INT:= 202;
		FuseFail						: INT:= 203;
		MotorFail					: INT:= 204;
(*		EmergencyStop				: INT:= 205;
		SystemAirPressure			: INT:= 206;
		FailProtectionDoor			: INT:= 207;*)
		IFailHeatcircuit				: INT:= 208;
		IFailPreHeatcircuit				: INT:= 209;
		CurveFail					: INT:= 210;
(*		ProtectionCircuitFail			: INT:= 211;
		NotRemoved					: INT:= 212;
*)
		FailEsp						: INT:= 230;
		IndexingCurveSensorOff		: INT:= 231;
		IndexingCurveFailSensor		: INT:= 232;

		PartFail						: INT:= 234;
		DummyFail					: INT:= 235;

		RemoveOKPart				: INT:= 250;
		RemoveNio					: INT:= 251;
		RemoveSlider				: INT:= 252;
		QuittNokParts					: INT:= 253;
		FailTool						: INT:= 254;
		RemoveCurve				: INT:= 255;
		CurveDummy					: INT:= 256;
		NoPartInSelector				: INT:= 257;
		NoStickOnBottom				: INT:= 258;
		BoxFull						: INT:= 259;
		NoBox						: INT:= 260;
		RemoveParts					: INT:= 261;
(*		StackEmpty					: INT:= 262;*)
		RemovePartAndMoveSliderBack: INT:= 263;
		FirstGrease					: INT:= 264;
		FirstAssembleFoil				: INT:= 265;
		CloseProtectionDoor			: INT:= 266;
		OpenProtectionDoor			: INT:= 267;
		TurnScrews					: INT:= 268;
		ScrewDriverReset				: INT:= 269;
		DummyRequest				: INT:= 270;
		DummyRemove				: INT:= 271;

		NoResponseFromPrinter		: INT:= 300;
		SendMaskfile					: INT:= 301;
		SendPrintfile					: INT:= 302;
		CheckFont					: INT:= 303;
		FontFail						: INT:= 304;
		CheckMaskFile				: INT:= 305;
		MaskFileFail					: INT:= 306;
		TestLabelStatus				: INT:= 307;
		RemoveLabel				: INT:= 308;
		WaitForPrinter				: INT:= 309;
		CarrierNumber				: INT:= 310;
		OK							: INT:= 311;
		NotOK						: INT:= 312;
		FailNumber					: INT:= 313;
		KeyCarrierCode				: INT:= 314;
		DoSwitchOn					: INT:= 315;
		DoSwitchOff					: INT:= 316;
		SelectValid					: INT:= 317;
		CarrierENTER				: INT:= 318;
		Stat							: INT:= 319;

		WaitForScanner				: INT:= 400;
		NoScannerResult				: INT:= 401;
		ScanFail						: INT:= 402;

		ContiniousGreaseRun			: INT:= 500;

(*MINE*)
		InvalidSchedule				: INT:= 600;
		EmergencyStop				: INT:= 601;
		SystemAirPressure			: INT:= 602;
		FailProtectionDoor			: INT:= 603;
		SensorFail					: INT:= 604;
		DowelNotinPosition			: INT:= 605;
		StackEmpty					: INT:= 606;
		StackNotEmpty				: INT:= 607;
		MaterialLose					: INT:= 608;
		FeedMotorFail				: INT:= 609;
		NotRemoved					: INT:= 610;
		ProtectionCircuitFail			: INT:= 611;
		TableNotinPosition			: INT:= 612;
		LackOfMaterial				: INT:= 613;
		WrongVariant					: INT:= 614;
		ChangCutter					: INT:= 615;
		NGContinue					: INT:= 616;
		TableAlarm					: INT:= 617;
		VariantCheck					: INT:= 618;
		VisionNotReady				: INT:= 619;
		ShortSampleTest				: INT:= 620;
		MasterSampleTest			: INT:= 621;

		SafePosition					: INT:= 622;
		RequestResponseInteraction	: INT:= 623;
		DeviceInteraction				: INT:= 624;
		WaitForLas					: INT:= 625;
		GetNewPart					: INT:= 626;


		CheckJigExisted				: INT:= 636;
		Adapter_Presence_On			: INT:= 637;
		LightCurtain_On				: INT:= 638;


		PleaseInsertAdapter			: INT:= 641;
		PleaseOpenRedBox			: INT:= 642;
		PleasePutPartToRedBox		: INT:= 643;
		PleaseCloseRedBox			: INT:= 644;
		PleasePressGreenButton		: INT:= 645;
		PleasePressWhiteButton		: INT:= 646;
		PleasePressRedButton		: INT:= 647;
		PleasePressYellowButton		: INT:= 648;
		PleaseInsertAirbagCable		: INT:= 649;
		PleaseRemovePart			: INT:= 650;
		PleaseRemoveRefPart		: INT:= 651;

		GreenButtonToOkPart			: INT:= 660;
		RedButtonToNgPart			: INT:= 661;
		WhiteButtonToClearMode		: INT:= 662;
		ScheduleIsRunningAt			: INT:= 663;
		KeyAutoToAutomaticMode		: INT:= 664;

		PleaseInsertPart				: INT:= 666;
		InvalidFamilyName			: INT:= 670;

		MasterPartTest				: INT:= 680;
		SelfResistanceTest			: INT:= 681;

		Empty						: INT:= 999;
	END_STRUCT
END_TYPE             №   , K K ШB           structTextData -3%]	-3%]      rtfo
RE        ж  TYPE structTextData :
	STRUCT
		Name							: structText;

		StartOfMainControlText				: INT:=		0;
		StartUserDefinition					: INT:=		20;
		StartLanguageNames				: SINT:=		40;
		StartDataNames					: INT:=		100;

		StartSchedulNames				: INT:=		600;
		StartFailNames					: INT:=		700;
		StartStationNames				: INT:=		800;	(* Sequence from enumStationNames *)
		StartInfoPage						: INT:=		900;

		MAX_LANGUAGE					: SINT:= 		2;
	END_STRUCT
END_TYPE



             $   , K K ШB           StructVariantInfo -3%]	-3%]      AR_IEG;        9  TYPE StructVariantInfo:
STRUCT
		strKostalNr							: STRING(20) :='';		(* Kostal article number without Index, like 10110201*)
		strKostalArticleName					: STRING(20) :='';		(* Kostal article name, like B9_SCM*)
		strCustomerNr						: STRING(20) :='';		(* Customer number  like WD.504.105.J *)
		strProductFamily						: STRING(20) :='';		(* Product family name  *)
		strSerialNr							: STRING(50) :='';		(* Kostal serial number. like 13 digitals *)
		strUserDefined						: STRING(50) :='';		(* Additional infomation defined by engineers *)
END_STRUCT
END_TYPE             + '   , Ш Ш Eй           Cylinder_V2 -3%]	-3%]                        FUNCTION_BLOCK Cylinder_V2
VAR_INPUT
	User			: INT;

	(* static variables *)

	D_Enable		: BOOL;
	D_Text			: INT;
	D_Value			: STRING(40);

	T_Enable		: BOOL;
	T_Text			: INT;
	T_ValueToA		: STRING(40);
	T_ValueToB		: STRING(40);

	TextLine0		: STRING(40);

	Direction			: BOOL;
	ConditionToA		: BOOL;
	ConditionToB		: BOOL;


	ConditionTime	: TIME;
	TimeOut			: TIME:=T#3s;

	BackEnable		: BOOL:= TRUE;

	(* dynamic variables *)
	xInit				: BOOL:= TRUE;
	xSet				: BOOL:= FALSE;
	xPressureless	: BOOL:= FALSE;
	StepEnable		: BOOL:= TRUE;
	Freeze			: BOOL:= FALSE;
	Fill				: BOOL:= FALSE;
	JumpAddress	: INT:= 0;
	MultiProg		: BOOL;
END_VAR

VAR_OUTPUT
	ChannelA		: BOOL;
	ChannelB		: BOOL;

	ConditionAck		: BOOL;
END_VAR

VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetStep					 :FB_SetStep;
	ConditionTimer			: TON;
	ConditionTimerIsExpired	: BOOL;
	ConditionTimerRequest	: BOOL;
END_VAR
Я  (*
#############################################################################################################
Cylinder control and Step modul
#############################################################################################################

Interface			:

VAR_INPUT
	xSet				: Only Variable set without any reactions
	sInit				: Set all output signs to zero
	xPressureless	: Set Channel signs to zero

	Direction			: true  > power to channel 'B'
					  false > power to channel 'A'
	Freeze			: Default FALSE - no reaction to channels (cylinder)
	MultiProg		: Multi cylinder modul use this cylindermodul

	ConditionToA		: if True then Stepmodul go forward when direction is 'false'
	ConditionToB		: if True then Stepmodul go forward when direction is 'true'
	ConditionTime	: Is a delay f ConditionTime is <> T#0ms then then Cylinder set ConditionAck after this time

	StepEnable		: Default TRUE - allows modul to use StepControlmodul from User
	BackEnable		: Default FALSE - Allow the stepmodul to step back one position
	JumpAddress	: possible to set the next step in a position not equal '0'

	D_Enable		: Enable (True) or disable (false) the Doublesensor control
	D_Text			: Textnumber of errortext
	D_Value			: Textvariable of errortext

	T_Enable		: Enable (True) or disable (false) the TimeOut control
	T_Text			: Textnumber of errortext
	T_ValueToA		: Textvariable if direction is 'false'
	T_ValueToB		: Textvariable if direction is 'true'
END_VAR

VAR_OUTPUT
	ChannelA		: OUTPUT variable of type 'BOOL' for cylinderdirection to Szxa
	ChannelB		: OUTPUT variable of type 'BOOL' for cylinderdirection to Szxb

	ConditionAck	: Activ if condition for cylinder is TRUE
END_VAR

#############################################################################################################

Version				: 2.0.0
Build				: 2006_08_31
Changes				: Function "Pessureless" and Property "ConditionTime" integrated

Version				: 2.0.1
Build				: 2006_10_23
Changes				: Depend of "Stations.Data[User].TimeOut.Text:= T_Text;" changed

#############################################################################################################
*)
IF Stations.Data[User].xToggle THEN
	ConditionTimerIsExpired:= FALSE;
END_IF;

(* Set Outputs to zero *)
IF xInit OR InitPulse THEN
	ChannelA:= FALSE;
	ChannelB:= FALSE;
	ConditionTimer.IN:= FALSE;
	ConditionTimerRequest:= FALSE;
	ConditionTimerIsExpired:= FALSE;
	ConditionAck:= FALSE;

(* Only variable set *)
ELSIF xSet THEN
	ConditionAck:= FALSE;
	ConditionTimer.IN:= ConditionTimerRequest AND NOT Stations.Data[User].xToggle AND  Stations.Data[User].xAutomatic  ;
	ConditionTimerRequest:= FALSE;
ELSIF Fill THEN
	ConditionTimerRequest:= TRUE;

	ChannelA:= TRUE;
	ChannelB:= TRUE;

	ConditionAck:= ConditionTimerIsExpired;

ELSIF xPressureless THEN
	ConditionTimerRequest:= TRUE;

	ChannelA:= FALSE;
	ChannelB:= FALSE;

	ConditionAck:= ConditionTimerIsExpired;

ELSE


	IF D_Enable THEN
		CheckDoubleSense(SxA:= ConditionToA, SxB:= ConditionToB, User:= User, TextLine0:= TextLine0, Text:= D_Text, sValue01:= D_Value );
	END_IF;

	IF T_Enable THEN
		IF NOT Direction AND NOT ConditionToA THEN
			Stations.Data[User].TextLine0:= TextLine0;
			Stations.Data[User].TimeOut.Text:= T_Text;
			Stations.Data[User].TimeOut.sValue01:= T_ValueToA;
			Stations.Data[User].TimeOut.xEnable:= NOT MultiProg;
			Stations.Data[User].TimeOut.ExternalTimeOut:=TimeOut;
		ELSIF Direction AND NOT ConditionToB THEN
			Stations.Data[User].TextLine0:= TextLine0;
			Stations.Data[User].TimeOut.Text:= T_Text;
			Stations.Data[User].TimeOut.sValue01:= T_ValueToB;
			Stations.Data[User].TimeOut.xEnable:= NOT MultiProg;
			Stations.Data[User].TimeOut.ExternalTimeOut:=TimeOut;
		END_IF;
		ConditionTimerRequest:=  (NOT Direction AND ConditionToA OR Direction AND ConditionToB) ;
		ConditionAck:= (NOT Direction AND ConditionToA OR Direction AND ConditionToB) AND ConditionTimerIsExpired;
	ELSE
		ConditionTimerRequest:=  TRUE;
		ConditionAck:=ConditionTimerIsExpired;
	END_IF;


	IF StepEnable THEN
		SetStep (	User:= User,
					ExternalSign:= ConditionAck,
					JumpAddress:= JumpAddress,
					BackStep:= 	BackEnable
					 );
	END_IF;

	IF NOT Freeze THEN
		ChannelB:= Direction XOR Stations.Data[User].Step.BackStepAck;

		ChannelA:=NOT ChannelB;
	END_IF;

END_IF;

IF Stations.Data[User].xToggle OR (ConditionTime = T#0ms) THEN
	ConditionTimer.IN:= FALSE;
END_IF;

ConditionTimer(IN:= , PT:= ConditionTime, Q=> , ET=> );
ConditionTimerIsExpired:= (ConditionTime = T#0ms) OR ConditionTimer.Q;

xInit:= FALSE;
xSet:= FALSE;
xPressureless:= FALSE;
StepEnable:= TRUE;
Freeze:= FALSE;
Fill:= FALSE;
JumpAddress:= 0;
MultiProg:= FALSE;               (   , њ њ wё           FB_AdsDeviceInteraction -3%]	-3%]                      x  FUNCTION_BLOCK FB_AdsDeviceInteraction
VAR_INPUT
	User						: INT;
	StartTask					: BOOL;

	RequestType					: STRING(20);

	pAgent						: POINTER TO StructDeviceInteraction;


END_VAR

VAR_OUTPUT

	Done				: BOOL;
	Abort				: BOOL;

END_VAR

VAR

	Step						: INT:=0;

	MessageTimer				: TON;

	EmptyDeviceInteraction		: StructDeviceInteraction;
	SetError						: FB_SetError;
	SetMasterError				: FB_SetMasterError;
	SetMasterMessage			: FB_SetMasterMessage;
	SetMessage					: FB_SetMessage;
	SetTips						: FB_SetTips;
	SetStep						 :FB_SetStep;

	TimeOut						: TIME:=t#300s;
	Me: BOOL;
END_VAR!  (*FB_AdsDeviceInteraction*)
(*
	Version:  1.0.0.3
	Time:       2018-07-09
	Author:     Wang Yumin
	Description:  Suits for any device interactions betweein PLC and LAS; Like Scanner , Printer, Laser...etc.
*)

	IF pAgent=0 OR User=0 THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Error' );

		RETURN;
	END_IF

	IF NOT StartTask THEN

		Step:=0;
		RequestType:='';
		pAgent^:=EmptyDeviceInteraction;

		Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1;
		RETURN;
	END_IF

	CASE Step OF
		0:
			MessageTimer(in:=FALSE);

			IF StartTask THEN
				Done := FALSE;
				Abort := FALSE;

				Step:=Step+1;
			END_IF

		1:
			IF StartTask THEN
				Step:=Step+1;
			END_IF

		2:
			IF PC_arrScheduleList[PC_bytCurrentScheduleNr].bulReferenceSchedule THEN
				Step:=6;
				RETURN;
			END_IF
			pAgent^.stuPlcArticleSet:=Stations.Data[User].pWT^.stuVariantInfoSet ;
			IF RequestType = LasRequestTypes.DoQuery1 OR RequestType = LasRequestTypes.DoQuery2 THEN
				pAgent^.stuPlcArticleSet.strUserDefined:=RequestType;
			ELSIF RequestType = LasRequestTypes.DoCompare1 THEN
				pAgent^.stuPlcArticleSet.strSerialNr:=LEFT(Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr,13);
				pAgent^.stuPlcArticleSet.strUserDefined:=RequestType;
			ELSIF RequestType = LasRequestTypes.DoCompare2 THEN
				pAgent^.stuPlcArticleSet.strSerialNr:=RIGHT(Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr,13);
				pAgent^.stuPlcArticleSet.strUserDefined:=RequestType;
			END_IF
			Step:=Step+1;


		3:
			pAgent^.bulPlcDoAction:= TRUE;
			Step:=Step+1;

		4:	(*=================!!!Wait for Tester Result=================================*)
			MessageTimer(in:=TRUE,pt:=TimeOut);

			IF pAgent^.bulAdsActionIsPass AND NOT pAgent^.bulAdsActionIsFail  THEN
				MessageTimer(in:=FALSE);
				Step:=Step+1;

			ELSIF pAgent^.bulAdsActionIsFail AND NOT pAgent^.bulAdsActionIsPass  THEN
				MessageTimer(in:=FALSE);
				Stations.Data[User].pWT^.bulTestResult:= FALSE;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailCarrierNr 		:= CONCAT( 'WT',INT_TO_STRING(Stations.Data[User].iCarrierNr));
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailKostalNr		:= Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailSerialNr 		:= Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailCode 			:= CONCAT('EC',  RIGHT(CONCAT('000', INT_TO_STRING(Text.Name.ScanFail)),3));
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailStationNr 		:= Stations.Data[User].StationName;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailScheduleName := Stations.Data[User].pWT^.strScheduleName;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailText			 := pAgent^.strAdsActionValue;

				IF RequestType = LasRequestTypes.DoQuery1 OR RequestType = LasRequestTypes.DoQuery2  THEN
					Stations.Data[User].pWT^.bytScheduleModeNr:=PC_bytCurrentScheduleNr;
					Stations.Data[User].pWT^.stuVariantInfoSet:=PC_stuCurrentVariantInfo;
					IF Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr='' THEN Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr :='InvalidSN' ; END_IF
					Stations.Data[User].pWT^.bulReferencePart:= PC_arrScheduleList[PC_bytCurrentScheduleNr].bulReferenceSchedule;
					Stations.Data[User].pWT^.strScheduleName:=PC_arrScheduleList[PC_bytCurrentScheduleNr].strScheduleName;
				END_IF
				Step:=Step+1;
			ELSIF pAgent^.bulAdsActionIsFail AND pAgent^.bulAdsActionIsPass  THEN
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Error' );
			ELSIF MessageTimer.Q THEN
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Timeout' );
			ELSE
				SetTips(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Message' );
			END_IF

		5:
			pAgent^:=EmptyDeviceInteraction;
			Step:=Step+1;
		6:
			Done := TRUE;

		ELSE
			Abort :=TRUE;
			SetMasterError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='MasterError' );

	END_CASE


	SetStep (	User:= User,ExternalSign:= Done OR Abort,JumpAddress:= 0,BackStep:= FALSE );
	IF Stations.Data[User].Step.Ack THEN
		StartTask:=FALSE;
		RequestType:='';
		Step:=0;
	END_IF               )   ,     }ї           FB_CheckDoubleSense  -3%]	-3%]                        FUNCTION_BLOCK FB_CheckDoubleSense
VAR_INPUT
	SxA					: BOOL;
	SxB					: BOOL;
	User				: INT;

	TextLine0			: STRING(40);
	Text					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetStep					 :FB_SetStep;
END_VARЮ   (*
Built			: 16.12.2002
Version		: 1.1

Description	: 

*)



IF SxA AND SxB AND Stations.Data[User].xOn THEN
	SetError(User:= User, Title:= TextLine0, Key:= Text, sValue01:= sValue01 );
END_IF;               [  ,              FB_EPSON_ROBOT з.]	-3%]      Xf.Д ПC          FUNCTION_BLOCK FB_EPSON_ROBOT
VAR_INPUT
	User										: INT;
	Run											: BOOL:=FALSE;
	Pos											: BYTE:=0;
	Start										: BOOL;
	Continue									: BOOL;
	Reset										: BOOL;
	SelMode										: BOOL;
	SelSpeed									: BOOL;
	R_Ready										: BOOL;
	R_Running									: BOOL;
	R_Paused									: BOOL;
	R_Error										: BOOL;
	R_Estop										: BOOL;
	R_SafeguardOn								: BOOL;
	R_SError									: BOOL;
	R_Warning									: BOOL;
	R_Busy										: BOOL;
	R_Done										: BOOL;
	R_NC2										: BOOL;
	R_InitialFinish								: BOOL;
END_VAR

VAR_OUTPUT
	Done										: BOOL;
	Ready										: BOOL;
	Running										: BOOL;
	R_Start										: BOOL;
	R_SelSpeed									: BOOL;
	R_Run										: BOOL;
	R_SelMode									: BOOL;
	R_Stop										: BOOL;
	R_Pause										: BOOL;
	R_Continue									: BOOL;
	R_Reset										: BOOL;
	R_Pos										: BYTE;
END_VAR

VAR
	Step						: INT:=0;
	Timer						: TON;
	SafeguardOn_Timer			: TON;
	PusleRobotDone				: R_TRIG;
END_VAR  (****************************************Initialize robot*********************************************)
(**)
R_SelSpeed	:=SelSpeed;
R_Reset		:=Reset;
Ready		:=R_Ready;

IF 	NOT Run
	OR  Stations.Data[User].xMasterError THEN
	R_Start		:=FALSE;
	R_Run		:=FALSE;
	R_SelSpeed	:=FALSE;
	R_SelMode	:=FALSE;
	R_Stop		:=TRUE;
	R_Pause		:=FALSE;
	R_Continue	:=FALSE;
(*	R_Reset:=FALSE;*)
	SelMode		:=FALSE;
	Done		:=FALSE;
	Start		:=FALSE;
	Timer(in:=FALSE);
	Pos			:=0;
	Step		:=0;
  	RETURN;
END_IF

R_Stop			:=FALSE;
Running			:=R_Running;
PusleRobotDone(CLK:=Continue AND R_Done,Q=>);

CASE Step OF
	0:
		R_Start		:=FALSE;
		R_Run		:=FALSE;
		IF R_Ready OR R_Running THEN
			Step:=Step+1;
		END_IF

	1:
		Timer(in:=TRUE,pt:=t#500ms);
		IF Timer.Q THEN
			R_Start:=TRUE;
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF

	2:
		Timer(in:=TRUE,pt:=t#1s);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF

	3:
		IF R_Running THEN
			R_Start:=FALSE;
			Step:=Step+1;
		ELSE
			Step:=0;
		END_IF

	4:
		R_Run:=FALSE;
		Timer(in:=TRUE,pt:=t#200ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=10;
		END_IF


	10:
		Done:=FALSE;
		IF Start  THEN
 			Step:=Step+1;
		END_IF

	11:
		R_Pos:=Pos;
		Done:=FALSE;
		Timer(in:=TRUE,pt:=t#100ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
   			Step:=Step+1;
		END_IF

	12:
		R_Run:=Continue;
		Done:=FALSE;
		IF R_Busy THEN
   			Step:=Step+1;
		END_IF

	13:
		IF PusleRobotDone.Q THEN
			Done:=TRUE;
			R_Run:=FALSE;
    		Step:=Step+1;
		END_IF

	14:
		IF  NOT Start THEN
			Done:=FALSE;
			R_Run:=FALSE;
     			Step:=10;
		END_IF

END_CASE

(*SaftyDoor*)

SafeguardOn_Timer(in:=R_Paused  AND NOT R_SafeguardOn AND NOT R_Pause ,pt :=t#500ms );
IF SafeguardOn_Timer.Q  THEN
	R_Continue:=TRUE;
ELSE
	R_Continue:=FALSE;
END_IF

(* Pause *)
R_Pause:=NOT Continue;

R_SelMode:=SelMode;               n  , њ њ wё           FB_GetNewPartInfo -3%]	-3%]        r u           p  FUNCTION_BLOCK FB_GetNewPartInfo
VAR_INPUT
	User						: INT;
	WT							: INT:=1;
	AbortAddress					: INT;
	StartTask					: BOOL;
	S_DUT_Presence				:BOOL:=FALSE;
	UpdateStep					:BOOL:=TRUE;

END_VAR

VAR_OUTPUT

	Done						: BOOL;
	Abort						: BOOL;

END_VAR

VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetTips						: FB_SetTips;
	SetStep					 :FB_SetStep;
	Step						: INT:=0;

	JumpAddress				: INT;
	pWT						: POINTER TO StructCarrierInfo;

	MessageTimer				: TON;
	rTrig_Abort					: R_TRIG;

	EmptyRequestAction			: StructRequestAction;
	EmptyResponseAction			: StructResponseAction;

	TimeOut						: TIME:=t#300s;
	EmptyCarrierInfo				: StructCarrierInfo;
END_VAR


VAR CONSTANT

	FB_NAME				: STRING:= 'New Part';

END_VARЫ  (*FB_GetNewPartInfo*)
(*
	Version:  1.0.2.0
	Time:       2018-07-24
	Author:    Wang Yumin
	Description:  Suits for Getting Information of Article and Schedule via ads interaction betweein PLC and LAS
*)


	IF NOT StartTask THEN
		PLC_Reference_Sensor:=FALSE;
		Step:=0;
		IF UpdateStep THEN Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1; END_IF
		RETURN;
	END_IF

	IF  User=0 THEN
		PLC_Reference_Sensor:=FALSE;
		SetError(User:= User, Key:= Text.Name.GetNewPart, sValue01:='Error' );
		RETURN;
	END_IF

	CASE Step OF
		0:
			JumpAddress :=0;

			MessageTimer(in:=FALSE);

			IF StartTask THEN
				pWT:=Stations.Data[User].pWT ;
				PLC_Reference_Sensor:=FALSE;
				Done := FALSE;
				Abort := FALSE;
				PLC_bulGetNewPart:=FALSE;
				Step:=Step+1;
			END_IF

		1:
			MessageTimer(in:=Stations.Data[User].xAutomatic,pt:=t#20ms);

			IF MessageTimer.Q THEN

				MessageTimer(in:=FALSE);
				Step:=Step+1;

			END_IF

		2:
			PLC_Reference_Sensor:=S_DUT_Presence;
			IF PC_arrScheduleList[PC_bytCurrentScheduleNr].strScheduleName=KostalBasicSchedules.ClearMode THEN
				pWT^:=EmptyCarrierInfo;
				Step:=6;
				RETURN;
			END_IF
			IF PC_bulScanPartRequest AND NOT PC_bulScannedPartResult THEN
				(*JumpAddress:=AbortAddress;
				MessageTimer(in:= Stations.Data[User].xAutomatic, pt:=t#20ms);
				IF MessageTimer.Q THEN
					MessageTimer(in:= FALSE);
					Abort :=TRUE;
					SetError(User:= User, Text:= Text.Name.GetNewPart, sValue01:=' Error' );
				END_IF*)
				;
			ELSE
				JumpAddress :=0;
				Step:=Step+1;
			END_IF


		3:
			SetTips(User:= User, Key:= Text.Name.WaitForLas, sValue01:= FB_NAME);
	                   IF NOT  PC_bulNewPartAvailable THEN
			      PLC_bulGetNewPart:=TRUE;
		  	      Step:=Step+1;
	                   END_IF

		4:	(*=================!!!Wait for Tester Result=================================*)
			IF PC_arrScheduleList[PC_bytCurrentScheduleNr].strScheduleName=KostalBasicSchedules.ClearMode THEN
				pWT^:=EmptyCarrierInfo;
				Step:=6;
				RETURN;
			END_IF

			SetTips(User:= User, Key:= Text.Name.WaitForLas, sValue01:=FB_NAME);
		      	IF PC_bulNewPartAvailable   THEN
				IF wt=1 THEN
					pWT^:=EmptyCarrierInfo;
					pWT^.bytDestinationStation:=Stations.Data[User].bStationID ;
					pWT^.bytScheduleModeNr:=PC_bytCurrentScheduleNr;
					pWT^.stuVariantInfoSet:=PC_stuCurrentVariantInfo;
					pWT^.bulReferencePart:= PC_arrScheduleList[PC_bytCurrentScheduleNr].bulReferenceSchedule;
					pWT^.strScheduleName:=PC_arrScheduleList[PC_bytCurrentScheduleNr].strScheduleName;
				ELSE
					pWT^.stuVariantInfoSet.strSerialNr:=CONCAT(CONCAT(pWT^.stuVariantInfoSet.strSerialNr, ';'), PC_stuCurrentVariantInfo.strSerialNr);
				END_IF
				PLC_bulGetNewPart:=FALSE;
	  			Step:=Step+1;
		      	 END_IF
		5:
			Step:=Step+1;

		6:
			Done := TRUE;
		ELSE
			Abort :=TRUE;
			SetMasterError(User:= User, Key:= Text.Name.GetNewPart, sValue01:='MasterError' );
	END_CASE


					SetStep(	User:= User,
							ExternalSign:= Done OR Abort,
							JumpAddress:=JumpAddress,
							BackStep:= FALSE,
							 );

	IF Stations.Data[User].Step.Ack THEN
		StartTask:=FALSE;
		Step:=0;

	END_IF               +   , Џ Џ ,І        
   FB_IAIPCON -3%]	-3%]                      %
  FUNCTION_BLOCK FB_IAIPCON
VAR_INPUT
	Run									: BOOL:=FALSE;
	Pos									: BYTE:=0;
	Start									: BOOL;
	Reset								: BOOL;
	HomeStart							: BOOL;
	HomePosition						: BOOL;
	SafePosition							: BOOL;
	Continue								: BOOL;

	APosbit0								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit1								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit2								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit3								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit4								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit5								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit6								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)
	APosbit7								: BOOL;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)

	PZONE								: BOOL;(* *)
	RMDS								: BOOL;(* AUTO ФЃЪНзДЬЌЯТаХКХOFFЃЌMANU ФЃЪНзДЬЌЯТаХКХON *)
	PEND								: BOOL;(* вЦЖЏжСФПБъЮЛжУЃЌНјШыОЭЮЛЧјМфКѓаХКХON *)
	HEND								: BOOL;(* НгЭЈЕчдДЪБаХКХOFFЃЌд­ЕуИДЮЛЭъГЩКѓаХКХON *)
	Alarm								: BOOL;(* е§ГЃЪБаХКХOFFЃЌЗЂЩњБЈОЏЪБаХКХON *)
	SV									: BOOL;(* ЫХЗўONЃЌПЩдЫзЊЕФзДЬЌЯТЪфГі *)
	EMGS								: BOOL;(* аХКХON ЪБЮЊМБЭЃзДЬЌ *)
	CLK                                                                            : BOOL;(* ИљОнЙтеЄON/OFFВњЩњИіТіГхаХКХ *)
	STOP								: BOOL;

	IJogUp								: BOOL; (* Using for Teach MODE ONLY as  PIO 1*)
	IJogDown							: BOOL; (* Using for Teach MODE ONLY as  PIO 1*)

END_VAR
VAR_OUTPUT

	Done								: BOOL;
	Running								: BOOL;
	HomeError							: BOOL:=FALSE;
	PCPosBit0							: BOOL;(* *)
	PCPosBit1							: BOOL;(* *)
	PCPosBit2							: BOOL;(* *)
	PCPosBit3							: BOOL;(* *)
	PCPosBit4							: BOOL;(* *)
	PCPosBit5							: BOOL;(* *)
	PCPosBit6							: BOOL;(* *)
	PCPosBit7							: BOOL;(* *)

	OJogUp								: BOOL;  (* Using for Teach MODE ONLY as  PIO 1*)
	OJogDown							: BOOL;  (* Using for Teach MODE ONLY as  PIO 1*)

	BLRL								: BOOL;(* ЧПжЦНтГ§ДјЩВГЕЕФЧ§ЖЏжсЕФЩВГЕ *)
	RMOD								: BOOL;(* AUTO/MANU ЕФЧаЛЛ *)
	CSTR								: BOOL;(* дкаХКХON ЫВМфПЊЪМвЦЖЏ *)
	HOME								: BOOL;(* дкаХКХON ЫВМфПЊЪМд­ЕуИДЮЛЖЏзїЁЃ *)
	STP									: BOOL;(* ON ЃКПЩвЦЖЏЃЌOFF ЃКМѕЫйЭЃжЙ *)
	RES								: BOOL;(* дкаХКХON ЫВМфБЈОЏЧхСу *)
	SON								: BOOL;(* НгЭЈзДЬЌЯТЫХЗўONЃЌЖЯПЊзДЬЌЯТЫХЗўOFF *)
	MON								: BOOL;(*ЭЈаХЖЫПкСЌНгДђПЊ*)
END_VAR
VAR
	HomeFinished						: BOOL;
	Ready								: BOOL;
	Homing								: BOOL;
	Step								: INT;
	timer								: TON;
	PusleStart							: R_TRIG;

	APos								: BYTE;(*  ЖЈЮЛЭъГЩКѓЪфГівбжДааЖЏзїЕФЮЛжУБрКХ *)

END_VARХ	  (*====================PIO ФЃЪН2============================*)
(****************************************Initialize IAI*********************************************)
(****************************************V1.0.0.0  2018.06.01********************************************)


	MON:=TRUE;

	RES:=Reset;
	STP	:=SafePosition;

	OJogUp := IJogUp;
	OJogDown:= IJogDown;

	IF Reset THEN
		HomeError:=FALSE;
	END_IF

	IF NOT Run THEN
		Pos:=0;
		BLRL:=FALSE;
		RMOD:=FALSE;
		HOME:=FALSE;
		STP:=FALSE;
		CSTR:=FALSE;
		RES:=FALSE;
		SON:=FALSE;
		Done:=FALSE;
		Running:=FALSE;
		Start:=FALSE;
		Ready:=FALSE;
		Step:=0;
	  	RETURN;
	END_IF

	PusleStart(CLK:=Start);

	(**ЕуЮЛ**)
	PCPosBit0:=Pos.0;
	PCPosBit1:=Pos.1;
	PCPosBit2:=Pos.2;
	PCPosBit3:=Pos.3;
	PCPosBit4:=Pos.4;
	PCPosBit5:=Pos.5;

	Apos:=BOOL_TO_BYTE(Aposbit0)+BOOL_TO_BYTE(Aposbit1)*2+BOOL_TO_BYTE(Aposbit2)*4+BOOL_TO_BYTE(Aposbit3)*8+BOOL_TO_BYTE(Aposbit4)*16+BOOL_TO_BYTE(Aposbit5)*32;

CASE Step OF
	(*Initilization*)
	0:
		Ready:=FALSE;
		IF  RUN AND NOT HomeError THEN
			Step:=Step+1;
		END_IF
	1:
		SON:=TRUE;
		IF SV THEN
			Running:=TRUE;
			Step:=Step+1;
		ELSE
			RES:=TRUE;
			Step:=0;
		END_IF
	2:
		Timer(in:=HEND,pt:=t#50ms);
		IF HomeStart  OR Homing  THEN
			HomeFinished:=FALSE;
			Homing:=FALSE;
			Timer(in:=FALSE);
			Step:=Step+1;
		ELSIF Timer.Q THEN
			HomeFinished:=TRUE;
			HOME:=FALSE;
			Timer(in:=FALSE);
			Step:=10;
		END_IF

	3:
		HOME:=SV AND NOT EMGS;
		Timer(in:=HOME,pt:=t#50ms);
		IF Timer.Q  THEN
(*			HOME:=FALSE;*)
			Timer(in:=FALSE);
     			Step:=Step+1;
		END_IF

	4:
		Timer(in:=HEND,pt:=t#5ms);
		IF Timer.Q  AND HomePosition THEN
			HomeFinished:=TRUE;
			HOME:=FALSE;
			Timer(in:=FALSE);
			Step:=10;
		END_IF


	(* Start to action *)
	10:
		Ready:=TRUE;
		IF PusleStart.Q  THEN
			Ready:=FALSE;
			Done:=FALSE;
			Step:=Step+1;
		ELSIF NOT HEND   THEN
			Step:=0;
		END_IF
	11:
		Timer(in:=TRUE,pt:=t#20ms);
		IF Timer.Q  THEN
			Timer(in:=FALSE);
     			Step:=Step+1;
		END_IF
	12:
		CSTR:=SV AND NOT EMGS;
		Timer(in:=CSTR,pt:=t#50ms);
		IF Timer.Q  THEN
			CSTR:=FALSE;
			Timer(in:=FALSE);
     			Step:=Step+1;
		END_IF
	13:
		Done:=PEND ;
		IF NOT SV  OR  EMGS THEN (*wkk*)
			Step:=Step-1;
		END_IF
		IF Done THEN
			Step:=Step+1;
		END_IF
	14:
		IF NOT Start THEN
			Done:=FALSE;
			CSTR:=FALSE;
			Timer(in:=FALSE);
     			Step:=10;
		END_IF
END_CASE               ,   , њ њ wё           FB_IAIPositionServer -3%]	-3%]                      '  FUNCTION_BLOCK FB_IAIPositionServer
VAR_INPUT
	User						: INT;

	Position						: BYTE;

	StartTask					: BOOL;

	StartHome					: BOOL;

	pAgent						: POINTER TO FB_IAIPCON;

END_VAR
VAR_OUTPUT

	Done				: BOOL;
	Abort				: BOOL;

END_VAR
VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetStep					 :FB_SetStep;
	Step						: INT:=0;

	MessageTimer				: TON;

	Timer						: TON;

	TimeOut						: TIME:=t#300s;

END_VAR	  (*FB_IAIPositionServer*)
(*
	Version:  1.0.0.1
	Time:       2018-09-11
	Author:     Wang Yumin
	Description:  Suits for Controlling IAI Robot Cylinders.
*)

	IF pAgent=0 OR User=0 THEN
		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Error0' );
		RETURN;
	END_IF

	IF NOT StartTask AND  NOT StartHome THEN

		Step:=0;

		Done := FALSE;

		Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1;

		RETURN;

	ELSIF StartTask AND   StartHome THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Error1' );

		RETURN;

	END_IF

	IF StartHome THEN

		Done := FALSE;
		pAgent^.Start:=FALSE;
		pAgent^.Run:=TRUE;
		pAgent^.HomeStart :=TRUE;

		IF  pAgent^.HEND AND  pAgent^.Ready   THEN
			Done := TRUE;
		END_IF

	END_IF

	IF pAgent^.EMGS   THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='EMGS' );

	ELSIF pAgent^.Alarm   THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Alarm' );

	ELSIF  NOT pAgent^.SV  THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='SV' );

	END_IF


	CASE Step OF
		0:
			MessageTimer(in:=FALSE);

			IF StartTask  THEN
				Done := FALSE;
				Abort := FALSE;

				Step:=Step+1;
			END_IF

		1: 	IF NOT pAgent^.Running OR NOT pAgent^.Ready   THEN
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Error' );

			ELSIF StartTask THEN
				Step:=Step+1;
			END_IF

		2:
			pAgent^.Pos := Position;
			Timer(in:=TRUE,pt:=t#30ms);
			IF Timer.Q THEN
				Timer(in:=FALSE);
				Step:=Step+1;
			END_IF

		3:
			MessageTimer(in:=TRUE,pt:=TimeOut);

			pAgent^.Start  := TRUE;
			IF pAgent^.Done THEN
				Step:=Step+1;
			ELSIF MessageTimer.Q THEN
				MessageTimer(in:=FALSE);
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Timeout' );
			END_IF

		4:
			Done := TRUE;
			pAgent^.Start  := FALSE;

		ELSE

			Abort :=TRUE;
			pAgent^.Start  := FALSE;
			SetMasterError(User:= User, key:= Text.Name.ProgramError, sValue01:='MasterError' );

	END_CASE


	Stations.Data[User].Step (	User:= User,
							ExternalSign:= Done ,
							JumpAddress:= 0,
							BackStep:= 	FALSE
							 );

	IF Stations.Data[User].Step.Ack THEN

		Step:=0;
		pAgent^.Start  := FALSE;

	END_IF               -   , K K ШB           FB_InitUser -3%]	-3%]                      ~   FUNCTION_BLOCK FB_InitUser
VAR
	iUser		: INT;
	iCnt			: INT;
	i: BOOL;
END_VAR
VAR_OUTPUT
	InitDone		: BOOL;
END_VAR
И  (*
Init User

Build		: 2007_03_13
Version		: 1.2

Description	: 
*)
(*Set Language as English*)
Language:=1;

(* 1. User >> System - Do not change *)
iUser:= 1;
Stations.Data[iUser].iUser:= iUser;
Stations.Data[iUser].StationName :='System';
Stations.System:= iUser;

(**************User define the partitons you need*******************************)

(* 2. User >> Main - Do not change *)
iUser:= 2;
Stations.Data[iUser].iUser:= iUser;
Stations.Data[iUser].StationName :='MainControl';
Stations.Main:= iUser;
Stations.StartStations:= iUser;

(*3.User >> BigTableControl - Do not change*)
iUser:=3;
Stations.Data[iUser].iUser:= iUser;
Stations.Data[iUser].bStationID :=0;
Stations.Data[iUser].StationName :='Table';
Stations.iBigTable:= iUser;

(* 4. User *)
FOR iCnt:=1 TO CON_REAL_STATIONS DO
	iUser:= 3+iCnt;
	Stations.Data[iUser].iUser:= iUser;
	Stations.Data[iUser].bStationID := INT_TO_BYTE(iCnt);
	Stations.Data[iUser].StationName :=CONCAT('Station0', INT_TO_STRING(iCnt));
	Stations.iStation [iCnt]:=iUser;
END_FOR

(*	===========================
	Do not change these entries
	===========================
*)

Stations.MaxUser:= iUser;

InitDone:= TRUE;               .   , њ њ wё           FB_KeyBoard  -3%]	-3%]                      a  FUNCTION_BLOCK FB_KeyBoard
VAR_INPUT
	InKeys					: structKeyBoardData;
	xKeyAutomatik			: BOOL;
	xKeyManual				: BOOL;
	xKeyRepeat				: BOOL;
	xKeyClear				: BOOL;

	xPressButton_Green		: BOOL;
	xPressButton_White		: BOOL;
	xPressButton_Red			: BOOL;
	xPressButton_Yellow		: BOOL;

	xButtonLight_StartIndicator	: BOOL;
	xButtonLight_RedIndicator	: BOOL;
	xButtonLight_GreenIndicator	: BOOL;
END_VAR


VAR_OUTPUT

	xButtonLight_Green			: BOOL;
	xButtonLight_White			: BOOL;
	xButtonLight_Red				: BOOL;
	xButtonLight_Yellow			: BOOL;
	Key							: structKeyBoardData;

END_VAR

VAR
	Trigger_xAutomatic	: R_TRIG;
	Trigger_xManual		: R_TRIG;
	Trigger_xData		: R_TRIG;
	Trigger_xClear		: R_TRIG;
	Trigger_xOn			: R_TRIG;
	Trigger_xEsc			: R_TRIG;

	RepeatTimer			: TON;
	PC_PLC_GreenButton: BOOL;
END_VAR

VAR_OUTPUT

END_VAR
  (*
Keyboard driver

Build		: 05.02.2018
Version		: 1.3

Description	: 
*)


Key:=HMI.Keys;

(*======================================================	*)
(*Sign definition						*)
(*======================================================	*)
Key.Sign.xAutomatic:= xKeyAutomatik;
Key.Sign.xManual:= xKeyManual;
Key.Sign.xData:= NOT Key.Sign.xAutomatic AND NOT Key.Sign.xManual;
Key.Sign.xOn := xPressButton_White;
Key.Sign.xEsc := xPressButton_Yellow;(*Reset*)
Key.Sign.xStopOnly :=xPressButton_Red;


(*====== ================================================	*)
(*Trigger definition						*)
(*======================================================	*)
Trigger_xAutomatic(CLK:=Key.Sign.xAutomatic);
Trigger_xManual(CLK:=Key.Sign.xManual);
Trigger_xData(CLK:=Key.Sign.xData);
Trigger_xOn(CLK:=Key.Sign.xOn );
Trigger_xEsc(CLK:=Key.Sign.xEsc);

(*
======================================================
Pulse definition
======================================================
*)
Key.Pulse.xAutomatic:= Trigger_xAutomatic.Q;
Key.Pulse.xManual:= Trigger_xManual.Q;
Key.Pulse.xData:= Trigger_xData.Q;
Key.Pulse.xOn := Trigger_xOn.Q ;
Key.Pulse.xEsc:= Trigger_xEsc.Q;

xButtonLight_Green:=PC_PLC_GreenButton OR xButtonLight_GreenIndicator  ;
xButtonLight_White :=PC_PLC_WhiteButton OR xButtonLight_StartIndicator  ;
xButtonLight_Red:=PC_PLC_RedButton OR xButtonLight_RedIndicator  ;
xButtonLight_Yellow:=Lamps.H5_TopLightRed;

PLC_PC_GreenButton:=xPressButton_Green ;
PLC_PC_RedButton:= xPressButton_Red ;
PLC_PC_WhiteButton:=xPressButton_White ;


IF xKeyRepeat THEN
	IF NOT RepeatTimer.Q AND Key.BufferEmpty THEN
		RepeatTimer(IN:= TRUE, PT:= T#10ms);
	ELSE
		RepeatTimer(IN:= FALSE);
		Key.Pulse:= Key.LastKey;
	END_IF;
ELSE
	RepeatTimer(IN:= FALSE);
END_IF;               /   , с с ^и        
   FB_KEYENCE -3%]	-3%]                      #  FUNCTION_BLOCK FB_KEYENCE
VAR_INPUT
	Run					: BOOL:=FALSE;
	Start					: BOOL;
	Variant				: INT;
	Keyence_OR                   		: BOOL;
	Keyence_TrigConfirm 		: BOOL;
	Keyence_ST0			: BOOL;
	Keyence_RUN			: BOOL;
END_VAR
VAR_OUTPUT
	Ready				: BOOL;
	Pass				: BOOL;
	Fail					: BOOL;
	Keyence_IN0          			: BOOL;
	Keyence_IN1                    	: BOOL;
	Keyence_IN2                         	: BOOL;
	Keyence_IN3                             	: BOOL;
	Keyence_IN4                     	: BOOL;
	Keyence_IN5                           	: BOOL;
	Keyence_IN6                         	: BOOL;
	Keyence_IN7                         	: BOOL;
	Keyence_IN8                           	: BOOL;
	Keyence_IN9                          	: BOOL;
	Keyence_IN10                        	: BOOL;
	Keyence_IN11                         	: BOOL;
	Keyence_CST                    	: BOOL;
	Keyence_TRG                           : BOOL;
END_VAR
VAR
	Timer				: TON;
	Start_Trig			: R_TRIG;
	KeyenceConfirm_Trig	: F_TRIG;
	Step				: INT:=0;
END_VARЭ  Keyence_IN0:=Variant.0;
Keyence_IN1:=Variant.1;
Keyence_IN2:=Variant.2;
Keyence_IN3:=Variant.3;
Keyence_IN4:=Variant.4;
Keyence_IN5:=Variant.5;
Keyence_IN6:=Variant.6;
Keyence_IN7:=Variant.7;
IF NOT Run THEN
	Ready:=FALSE;
	Pass:=FALSE;
	Fail:=FALSE;
	Step:=0;
  	RETURN;
END_IF

Start_Trig(clk:=Start);
KeyenceConfirm_Trig(clk:=Keyence_ST0);

CASE Step OF
	0:
	        	Keyence_IN8:=FALSE;
        		Keyence_IN9:=TRUE;
        		Keyence_IN10:=FALSE;
        		Keyence_IN11:=FALSE;
            	Keyence_CST:=FALSE;
            	Keyence_TRG:=FALSE;
		Timer(in:=Keyence_ST0,pt:=t#100ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF
	1:
            	Keyence_CST:=TRUE;
		Timer(in:=TRUE,pt:=t#200ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF
	2:
		Keyence_CST:=FALSE;
		Timer(in:=TRUE,pt:=t#200ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Ready:=TRUE;
			Step:=10;
		END_IF
	10:
		IF Start_Trig.Q  THEN
			Pass:=FALSE;
			Fail:=FALSE;
			Step:=Step+1;
		END_IF
	11:
		Keyence_TRG:=TRUE;
		Timer(in:=TRUE,pt:=t#1s);
		IF KeyenceConfirm_Trig.Q THEN
			Keyence_TRG:=FALSE;
			Timer(in:=FALSE);
			Step:=Step+1;
		ELSIF Timer.Q THEN
			Pass:=FALSE;
			Fail:=TRUE;
			Keyence_TRG:=FALSE;
			Timer(in:=FALSE);
			Step:=Step+2;
		END_IF
	12:
		Timer(in:=TRUE,pt:=t#1000ms);
		IF Timer.Q AND Keyence_OR THEN
			Pass:=TRUE;
			Fail:=FALSE;
			Step:=Step+1;
			Timer(in:=FALSE);
		ELSIF  NOT Keyence_OR THEN
			Pass:=FALSE;
			Fail:=TRUE;
			Step:=Step+1;
			Timer(in:=FALSE);
		END_IF
	13:
		Timer(in:=TRUE,pt:=t#1s);
		IF Timer.Q THEN
			Pass:=FALSE;
			Fail:=FALSE;
			Timer(in:=FALSE);
			Step:=10;
		END_IF
END_CASE               0   , с с ^и           FB_Lamps  -3%]	-3%]                      S  FUNCTION_BLOCK FB_Lamps

VAR_OUTPUT
	H0T_On					: BOOL;
	H1T_Plus				: BOOL;
	H2T_Minus				: BOOL;
	H3T_Esc				: BOOL;
	H4T_Clear				: BOOL;
	H5_TopLightRed			: BOOL;
	H6_TopLightOrange		: BOOL;
	H7_TopLightGreen		: BOOL;

	xFlash					: BOOL;		(*Blinker*)
END_VAR

VAR
	TimeOn					: TON;
	xTrigger					: BOOL;
END_VAR

е  (*
Keyboard-Lights

Built			: 6.12.2018
Version		: 1.2
Changed		: 

Description	: 
*)

TimeOn(IN:=NOT xTrigger,PT:= T#500ms);
xTrigger:= TimeOn.Q;

IF xTrigger THEN
	xFlash:= SEL(xFlash,TRUE,FALSE);
END_IF;

SignControl();

H0T_On:= Stations.Data[Stations.Main].xOn;
H1T_Plus:= SignControl.xSoftwareHome AND ( SignControl.xHardwareHome OR xFlash );
H3T_Esc:= SignControl.xMasterError OR SignControl.xError AND xFlash;
H4T_Clear:=Stations.Data[Stations.Main].xLastCycle;

HMI.Display.LED:= H3T_Esc;


H5_TopLightRed:= SignControl.xMasterError OR SignControl.xError; (* AND xFlash;*)
H6_TopLightOrange:= NOT H5_TopLightRed AND NOT H7_TopLightGreen;  (*SignControl.xMasterMessage OR SignControl.xMessage AND xFlash;*)
H7_TopLightGreen:= NOT (SignControl.xMasterError OR SignControl.xError ) AND Stations.Data[Stations.Main].xRun AND Stations.Data[Stations.Main].xAutomatic;
(*
H5_TopLightRed:= SignControl.xMasterError OR SignControl.xError AND xFlash;
H6_TopLightOrange:= SignControl.xMasterMessage OR SignControl.xMessage AND xFlash;
H7_TopLightGreen:= NOT (SignControl.xMasterError OR SignControl.xError  OR SignControl.xMasterMessage OR SignControl.xMessage) AND Stations.Data[Stations.Main].xRun;
*)
               1   ,   *           FB_LasControl -3%]	-3%]                        FUNCTION_BLOCK FB_LasControl
VAR_INPUT

END_VAR

VAR_OUTPUT
END_VAR

VAR
	SetError						: FB_SetError;
	SetMasterError				: FB_SetMasterError;
	SetMasterMessage			: FB_SetMasterMessage;
	SetMessage					: FB_SetMessage;
	SetTips						: FB_SetTips;
	SetStep						 :FB_SetStep;
	WaitTime					: FB_WaitTime;
	TimeOut						: FB_TimeOut;
	NOP						: NOP_FB;
	LasMethControl				: FB_LasMethControl;
	EOLTest						: FB_RequestResponseInteraction;
	ADS_LasFinishedPart			: FB_RequestResponseInteraction;
	ADS_ReferencePart			: FB_RequestResponseInteraction;
	ADS_LasGetNewPart			: FB_GetNewPartInfo;
	ADS_LasScannerSr752		: FB_AdsDeviceInteraction;
END_VAR  LasMethControl.pEOL:=ADR(EOLTest);
LasMethControl.pADS_LasFinishedPart:=ADR(ADS_LasFinishedPart);
LasMethControl.pADS_LasGetNewPart:=ADR(ADS_LasGetNewPart);
LasMethControl.pADS_LasScannerSr752:=ADR(ADS_LasScannerSr752);
LasMethControl.pADS_ReferencePart:=ADR(ADS_ReferencePart);               Z  , 2 2 wм            FB_LasMethControl РА.]	-3%]      Xf.Д ПC        ж  FUNCTION_BLOCK FB_LasMethControl
VAR_INPUT
	User				: INT;
	LasType				: INT;
	WT					: INT:=1;
	S_DUT_Presence		:BOOL:=FALSE;
	LightCurtainControl          :BOOL:=FALSE;
	pAgent				: POINTER TO StructStationCfg;
	pEOL				: POINTER TO FB_RequestResponseInteraction;
	pADS_LasFinishedPart: POINTER TO FB_RequestResponseInteraction;
	pADS_ReferencePart: 	 POINTER TO FB_RequestResponseInteraction;
	pADS_LasGetNewPart	: POINTER TO FB_GetNewPartInfo;
	pADS_LasScannerSr752: POINTER TO FB_AdsDeviceInteraction;
END_VAR
VAR_OUTPUT
	Done				:BOOL;
	CleanMode			:BOOL;
END_VAR
VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetTips					: FB_SetTips;
	SetStep					 :FB_SetStep;
	Step					:INT;
	UpdateTarget			:BOOL;
	TriggerClearMode			: R_TRIG;
	StampRoundNumber		: DINT;
	CurrentSchedule			: STRING(20):='';
	EmptyCarrierInfo			: StructCarrierInfo;
END_VARдM  Step:=0;
IF pAgent<>0 AND User<>0 THEN
	pAgent^.Data:=ADR(Stations.Data[User]);
	pAgent^.iCarrierNr:=Stations.Data[User].iCarrierNr ;
	pAgent^.WT:=Stations.Data[User].pWT ;
	IF pAgent^.iDebugStepNumber<>0 THEN
		pAgent^.Data^.iRequestedStepNumber :=pAgent^.iDebugStepNumber;
		pAgent^.iDebugStepNumber:=0;
	END_IF
	Step:=9999;
END_IF
CASE LasType OF
	LasMethType.StepFail:
			Stations.Data[User].iFailRequestedStepNumber:= Stations.Data[User].iActualStepNumber;
			SetError(User:= User, Key:= Text.Name.ProgramError,sValue01:=CONCAT(Stations.Data[User].StationName,' Step Fail'));
			Step:=LasType;
	LasMethType.Reset:
			IF User =0 THEN
				Done:=FALSE;
				RETURN;
			END_IF
			Stations.Data[User].xStart :=FALSE;
			Stations.Data[User].xStart :=Stations.Data[User].xCalibrate;

			IF KeyBoard.Key.Pulse.xEsc AND KeyBoard.Key.Sign.xManual THEN
				Stations.Data[User].xMasterError:= FALSE;
				Stations.Data[User].xError:= FALSE;
			END_IF
			Stations.Data[User].xMessage:= FALSE;
			Stations.Data[User].xMasterMessage:= FALSE;
			Stations.Data[User].xTips:= FALSE;

			(*Auto/Manual*)
			IF LightCurtainControl THEN
				Stations.Data[User].xAutomatic:= Stations.Data[Stations.Main].xAutomatic
										AND NOT Stations.Data[User].xError
										AND NOT Stations.Data[User].xMasterError
										AND NOT maincontrol.LightCurtainSafetyRelay
										AND KeyBoard.Key.Sign.xAutomatic;
			ELSE
					Stations.Data[User].xAutomatic:= Stations.Data[Stations.Main].xAutomatic
										AND NOT Stations.Data[User].xError
										AND NOT Stations.Data[User].xMasterError
										AND KeyBoard.Key.Sign.xAutomatic;
			END_IF

			Stations.Data[User].xManual:= KeyBoard.Key.Sign.xManual AND NOT Stations.Data[User].xAutomatic;
			Stations.Data[User].xHardwareHome:= Stations.Data[User].xSoftwareHome AND Stations.Data[User].xSafePosition ;

(*======================================================	*)
			(*Station Off*)
			(*======================================================	*)
			Stations.Data[User].OffPulse:=FALSE;
			IF Stations.Data[User].iActualStepNumber <> 0 AND ( NOT Stations.Data[ Stations.Main].xOn OR Stations.Data[User].xMasterError ) THEN
				Stations.Data[User].iRequestedStepNumber:= 0;
				Stations.Data[User].OffPulse:=TRUE;
			END_IF;

			(*======================================================	*)
			(*Steps*)
			(*======================================================	*)
			Stations.Data[User].xToggle:= Stations.Data[User].iActualStepNumber <> Stations.Data[User].iRequestedStepNumber;
			
			Stations.Data[User].iActualStepNumber:= Stations.Data[User].iRequestedStepNumber;
			
			Stations.Data[User].Step(Reset:= TRUE);

			(*======================================================	*)
			(*StepLog added on 2018-06-23*)
			(*======================================================	*)
			IF Stations.Data[User].xToggle THEN
				IF   Stations.Data[User].iActualStepNumber=0
					OR LEN(Stations.Data[User].StepLog)>=250 THEN
					Stations.Data[User].StepLog:='';
				END_IF
				Stations.Data[User].StepLog := CONCAT( CONCAT( Stations.Data[User].StepLog , '->'),INT_TO_STRING(Stations.Data[User].iActualStepNumber));
			END_IF

			(*Table*)
			IF  BigTable.MoveTable THEN
				Stations.Data[User].Signalto_PartitionControl:=FALSE;
			END_IF

			Done:=TRUE;
			Step:=LasType;
	LasMethType.MainReset:
			IF User =0 THEN
				Done:=FALSE;
				RETURN;
			END_IF

			IF KeyBoard.Key.Pulse.xEsc AND KeyBoard.Key.Sign.xManual THEN
				Stations.Data[User].xMasterError:= FALSE;
				Stations.Data[User].xError:= FALSE;
			END_IF
			Stations.Data[User].xMessage:= FALSE;
			Stations.Data[User].xTips:= FALSE;
			Stations.Data[User].xMasterMessage:= FALSE;
			(* StepHeader *)
			(*======================================================	*)
			(*Station Off*)
			(*======================================================	*)
			IF  Stations.Data[User].iActualStepNumber <> 0 AND SignControl.xMasterError THEN

				Stations.Data[User].iRequestedStepNumber:= 0;
			END_IF;

			(*======================================================	*)
			(*Steps*)
			(*======================================================	*)
			Stations.Data[User].xToggle:= Stations.Data[User].iActualStepNumber <> Stations.Data[User].iRequestedStepNumber;
			Stations.Data[User].iActualStepNumber:= Stations.Data[User].iRequestedStepNumber;
			Stations.Data[User].Step(Reset:= TRUE);

			(*======================================================	*)
			(*StepLog added on 2018-06-23*)
			(*======================================================	*)
			IF Stations.Data[User].xToggle THEN
				IF   Stations.Data[User].iActualStepNumber=0
					OR LEN(Stations.Data[User].StepLog)>=250 THEN
					Stations.Data[User].StepLog:='';
				END_IF
				Stations.Data[User].StepLog := CONCAT( CONCAT( Stations.Data[User].StepLog , '->'),INT_TO_STRING(Stations.Data[User].iActualStepNumber));
			END_IF
			Done:=TRUE;
			Step:=LasType;

	LasMethType.TableReset :
			IF User =0 THEN
				Done:=FALSE;
				RETURN;
			END_IF
			Stations.Data[User].xStart :=FALSE;
			Stations.Data[User].xStart :=Stations.Data[User].xCalibrate;

			IF KeyBoard.Key.Pulse.xEsc AND KeyBoard.Key.Sign.xManual THEN
				Stations.Data[User].xMasterError:= FALSE;
				Stations.Data[User].xError:= FALSE;
			END_IF
			Stations.Data[User].xMessage:= FALSE;
			Stations.Data[User].xMasterMessage:= FALSE;
			Stations.Data[User].xTips:= FALSE;

(*======================================================	*)
			(*Station Off*)
			(*======================================================	*)
			Stations.Data[User].OffPulse:=FALSE;
			IF Stations.Data[User].iActualStepNumber <> 0 AND ( NOT Stations.Data[ Stations.Main].xOn OR Stations.Data[User].xMasterError ) THEN
				Stations.Data[User].iRequestedStepNumber:= 0;
				Stations.Data[User].OffPulse:=TRUE;
			END_IF;

			(*======================================================	*)
			(*Steps*)
			(*======================================================	*)
			Stations.Data[User].xToggle:= Stations.Data[User].iActualStepNumber <> Stations.Data[User].iRequestedStepNumber;
			
			Stations.Data[User].iActualStepNumber:= Stations.Data[User].iRequestedStepNumber;
			
			Stations.Data[User].Step(Reset:= TRUE);

			(*======================================================	*)
			(*StepLog added on 2018-06-23*)
			(*======================================================	*)
			IF Stations.Data[User].xToggle THEN
				IF   Stations.Data[User].iActualStepNumber=0
					OR LEN(Stations.Data[User].StepLog)>=250 THEN
					Stations.Data[User].StepLog:='';
				END_IF
				Stations.Data[User].StepLog := CONCAT( CONCAT( Stations.Data[User].StepLog , '->'),INT_TO_STRING(Stations.Data[User].iActualStepNumber));
			END_IF

			Stations.Data[User].xAutomatic:= 	Stations.Data[Stations.Main].xAutomatic
											AND NOT Stations.Data[User].xError
											AND NOT Stations.Data[User].xMasterError
											AND KeyBoard.Key.Sign.xAutomatic;
			Stations.Data[User].xManual:= KeyBoard.Key.Sign.xManual AND NOT Stations.Data[User].xAutomatic;
			Stations.Data[User].xHardwareHome:= Stations.Data[User].xSoftwareHome AND Stations.Data[User].xSafePosition ;
			Done:=TRUE;
			Step:=LasType;


	LasMethType.Off :
			(*Off*)
			Stations.Data[User].xOn:= FALSE;
			Stations.Data[User].xCalibrate:= FALSE;
			Stations.Data[User].xStop:= FALSE;
			Stations.Data[User].xRun:= FALSE;
			Stations.Data[User].xLastCycle:= FALSE;
			Stations.Data[User].xStart:= FALSE;
			Stations.Data[User].xStationToTable:= FALSE;
			KeyBoard.xButtonLight_RedIndicator:=FALSE;
			KeyBoard.xButtonLight_GreenIndicator:=FALSE;
			pEOL^(StartTask:=FALSE,);
			pADS_LasFinishedPart^(StartTask:=FALSE,);
			pADS_LasGetNewPart^(StartTask:=FALSE,UpdateStep:=FALSE);
			pADS_LasScannerSr752^(StartTask:=FALSE,);
			 PC_PLC_GreenButton:= FALSE;
        			 PC_PLC_RedButton:= FALSE;
      			 PC_PLC_WhiteButton:= FALSE;
			Step:=LasType;

	LasMethType.OffMainStart:
			Step:=LasType;
			IF  KeyBoard.Key.Sign.xManual
			AND NOT SignControl.xError
			AND NOT SignControl.xMasterError
			AND SignControl.xOff
			AND NOT Stations.Data[User].OffPulse THEN
					Step:=51;
					KeyBoard.xButtonLight_StartIndicator:=TRUE;
					IF  Stations.Data[User].SoftSwitchedOn THEN
							Step:=52;
							KeyBoard.xButtonLight_StartIndicator:=FALSE;
							Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iRequestedStepNumber+1;
					END_IF;
			END_IF;

	LasMethType.OffStart:
				Step:=LasType;
				IF Stations.Data[Stations.Main].xOn AND
				Stations.Data[User].xAutomatic  AND
				NOT Stations.Data[User].xError AND
				NOT Stations.Data[User].xMasterError AND
				NOT Stations.Data[User].OffPulse THEN
					Stations.Data[User].xOn:= TRUE;
					Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1;
				END_IF
	LasMethType.End:
				Step:=LasType;
				Stations.Data[User].WaitTime(UpDate:= TRUE, User:= User);
				Stations.Data[User].WaitTime.WaitTimeValue:= T#500ms;
				Stations.Data[User].TimeOut(User:= User);
				Stations.Data[User].xSoftwareHome:= Stations.Data[User].iActualStepNumber = Stations.Data[User].iAddressSoftwareHome
					AND Stations.Data[User].iRequestedStepNumber = Stations.Data[User].iAddressSoftwareHome;
	LasMethType.CalibrateEnd:
				Step:=LasType;
				(*Calibrate ready - Jump Software Home*)

				Stations.Data[User].xCalibrate:= TRUE;
				
				(*Stations.Data[me].xStationToTable:=TRUE;*)

				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:= Stations.Data[User].iAddressSoftwareHome, BackStep:= FALSE);

	LasMethType.SoftwareHome:
				Step:=LasType;
				SetStep(User:= User,
					ExternalSign:= Stations.Data[stations.Main].xRun
											AND Stations.Data[User].xHardwareHome
											AND Stations.Data[User].xSafePosition
											AND Stations.Data[User].xStart,
								JumpAddress:= 0,
								BackStep:= FALSE,
								Ack=> Stations.Data[User].xRun);
	LasMethType.CheckTableDone:
				Step:=LasType;
				SetStep(User:= User,
								ExternalSign:= Stations.Data[User].xTableToStation,
								JumpAddress:= 0,
								BackStep:= FALSE,
								Ack=> );

				IF Stations.Data[User].Step.Ack THEN
					Stations.Data[User].xTableToStation:=FALSE;
				END_IF

	LasMethType.CheckStationDestination:
				Step:=LasType;
				IF  Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID THEN
					Stations.Data[User].xStart:= TRUE;
					Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressProcess;
				ELSE
					Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressRelease ;
				END_IF

	LasMethType.CheckResult :
				Step:=LasType;
				IF NOT  Stations.Data[User].pWT^.bulTestResult   THEN
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFail, BackStep:= FALSE);
				ELSE
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressPass, BackStep:= FALSE);
				END_IF

	LasMethType.ProcessPass :
				Step:=LasType;
				UpdateTarget:=UpdateDestinationStation(User:=User,StationID:=Stations.Data[User].bStationID,TestResult:=TRUE);
				Stations.Data[User].xStart:= FALSE;
				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressRelease, BackStep:= FALSE);

	LasMethType.ProcessFail :
				Step:=LasType;
				UpdateTarget:=UpdateDestinationStation(User:=User,StationID:=Stations.Data[User].bStationID,TestResult:=FALSE );
				Stations.Data[User].xStart:= FALSE;
				IF Stations.Data[User].pWT^.bulReferencePart THEN
					SetMasterError(User:= User, sValue01:= CONCAT(Stations.Data[User].pWT^.strScheduleName, ' Fail!'));
				END_IF
				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressRelease, BackStep:= FALSE);

	LasMethType.ProcessRelease :
				Step:=LasType;
				IF (Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID )   THEN
					IF Stations.Data[User].pWT^.bulReferencePart THEN
						Stations.Data[User].pWT^.bulTestResult:=TRUE;
					END_IF
					SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressProcess, BackStep:= FALSE);
					RETURN ;
				ELSE
                                              IF Stations.Data[User].xToggle  THEN    Stations.Data[User].xStationToTable:=TRUE; END_IF
					Stations.Data[User].xStart:= FALSE;
					SetStep(User:= User,ExternalSign:= NOT Stations.Data[User].xStationToTable,JumpAddress:= Stations.Data[User].iAddressSoftwareHome,BackStep:= FALSE);
				END_IF


	LasMethType.ST1ProcessRelease :
				Step:=LasType;

				IF Stations.Data[User].pWT^.bulReferencePart AND NOT  Stations.Data[User].pWT^.bulTestResult THEN
					SetMasterError(User:= User, sValue01:= CONCAT(Stations.Data[User].pWT^.strScheduleName, ' Fail!'));
				END_IF

				IF (Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID )   THEN
					IF Stations.Data[User].pWT^.bulReferencePart THEN
						Stations.Data[User].pWT^.bulTestResult:=TRUE;
					END_IF
					IF Stations.Data[User].pWT^.bulTestResult=FALSE THEN
						SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalFail, BackStep:= FALSE);
					ELSE
						SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressRedoTest, BackStep:= FALSE);
					END_IF
					RETURN ;
				ELSE
                                              IF Stations.Data[User].xToggle  THEN    Stations.Data[User].xStationToTable:=TRUE; END_IF
					Stations.Data[User].xStart:= FALSE;
					SetStep(User:= User,ExternalSign:= NOT Stations.Data[User].xStationToTable,JumpAddress:= Stations.Data[User].iAddressSoftwareHome,BackStep:= FALSE);
				END_IF

	LasMethType.ST1CheckStationDestination :
			Step:=LasType;
			IF  (Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID)  AND Stations.Data[User].pWT^.bulTestResult  THEN
				SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinal, BackStep:= FALSE);
                            ELSIF Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr=''  AND NOT S_DUT_Presence THEN
				SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressProcess, BackStep:= FALSE);
			ELSE
				Stations.Data[User].pWT^ := EmptyCarrierInfo;
				SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalFail, BackStep:= FALSE);
			END_IF;

	LasMethType.CheckClearMode :
			Step:=LasType;
			IF PC_bytCurrentScheduleNr>0 THEN
				CurrentSchedule:=PC_arrScheduleList[PC_bytCurrentScheduleNr].strScheduleName;
			END_IF

			IF CurrentSchedule = KostalBasicSchedules.ClearMode THEN
				CleanMode:=TRUE;
				KeyBoard.xButtonLight_StartIndicator :=NOT Stations.Data[User].xLastCycle;
				IF Stations.Data[User].xLastCycle THEN
					IF BigTable.RoundCounter-StampRoundNumber>=CON_REAL_STATIONS THEN
						Stations.Data[User].xLastCycle :=FALSE;
					END_IF
					SetTips(User:= User, Key:= Text.Name.ScheduleIsRunningAt, sValue01:=CurrentSchedule );
				ELSE
					SetMasterMessage(User:= User, Key:= Text.Name.WhiteButtonToClearMode, sValue01:=CurrentSchedule );
					TriggerClearMode(CLK:=KeyBoard.Key.Pulse.xOn);
					IF TriggerClearMode.Q THEN
						StampRoundNumber := BigTable.RoundCounter;
						Stations.Data[User].xLastCycle :=TRUE;
						KeyBoard.xButtonLight_GreenIndicator :=FALSE;
					END_IF
				END_IF

				IF NOT S_DUT_Presence AND NOT Stations.Data[stations.Main].xLightCurtainRelay AND Stations.Data[User].xLastCycle THEN
					Stations.Data[User].pWT^ := EmptyCarrierInfo;
					SetStep(User:= User, ExternalSign:= Stations.Data[User].xLastCycle, JumpAddress:=Stations.Data[User].iAddressRelease, BackStep:= FALSE);
				END_IF
			ELSE
				CleanMode:=FALSE;
				Stations.Data[User].xLastCycle :=FALSE;
			END_IF

	LasMethType.CheckScanResult  :
			Step:=LasType;
			IF Stations.Data[User].pWT^.bulTestResult THEN
				SetStep (	User:= User,ExternalSign:=TRUE,JumpAddress:= 0,BackStep:= FALSE );
			ELSE
				SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:= Stations.Data[User].iAddressFail, BackStep:= FALSE);
			END_IF


	LasMethType.FinalCheckResult  :
				Step:=LasType;
				IF  Stations.Data[User].pWT^.bulTestResult THEN
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalPass, BackStep:= FALSE);
				ELSE
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalFail, BackStep:= FALSE);
				END_IF

	LasMethType.FinalCheckPassFail  :
				Step:=LasType;
				IF  Stations.Data[User].pWT^.bulTestResult THEN
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=0, BackStep:= FALSE);
				ELSE
					SetStep(User:= User, ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalFail, BackStep:= FALSE);
				END_IF

	LasMethType.FinalProcessPass :
				Step:=LasType;
				Stations.Data[User].xStart:= FALSE;
				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalPost, BackStep:= FALSE);

	LasMethType.FinalProcessFail :
				Step:=LasType;
				Stations.Data[User].xStart:= FALSE;
				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressFinalPost, BackStep:= FALSE);

	LasMethType.FinalProcessRelease :
				Stations.Data[User].pWT^ := EmptyCarrierInfo;
				Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iAddressProcess;

	LasMethType.GotoTest :
				Step:=LasType;
				SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=Stations.Data[User].iAddressRedoTest , BackStep:= FALSE);

	LasMethType.UpdateRefrencePart  :
				Step:=LasType;
				IF Stations.Data[User].pWT^.bulReferencePart  THEN
					IF  (Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID)  AND Stations.Data[User].pWT^.bulTestResult  THEN
						pADS_LasFinishedPart ^(User:=user,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=FALSE, pRequest:=ADR(ADS_stuReferencePartResponse),pResponse:=ADR(PLC_stuReferencePartRequest));
					ELSE
						pADS_LasFinishedPart ^(User:=user,StartTask:=TRUE,PositiveAction:=FALSE,UpdateTarget:=FALSE, pRequest:=ADR(ADS_stuReferencePartResponse),pResponse:=ADR(PLC_stuReferencePartRequest));
					END_IF

				ELSE
					SetStep(User:= User,  ExternalSign:= TRUE, JumpAddress:=0 , BackStep:= FALSE);;
				END_IF

	LasMethType.UnLoad :
				Step:=LasType;
				IF  Stations.Data[User].pWT^.bytDestinationStation =Stations.Data[User].bStationID AND Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr <>'' THEN
					IF Stations.Data[User].pWT^.bulTestResult THEN
						Stations.Data[User].xStart:= TRUE;
						Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressPass;
					ELSE
						Stations.Data[User].xStart:= TRUE;
						Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressFail;
					END_IF
				ELSIF Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr <>'' THEN
					Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressFail;
				ELSE
					Stations.Data[User].iRequestedStepNumber:=Stations.Data[User].iAddressRelease ;
				END_IF

	LasMethType.UnLoadRelease  :
				Step:=LasType;
               			 IF Stations.Data[User].xToggle  THEN    Stations.Data[User].xStationToTable:=TRUE; END_IF
				Stations.Data[User].xStart:= FALSE;
				SetStep(User:= User,ExternalSign:= NOT Stations.Data[User].xStationToTable,JumpAddress:= Stations.Data[User].iAddressSoftwareHome,BackStep:= FALSE);


	ELSE
			;
END_CASE;


S_DUT_Presence		:=FALSE;
LightCurtainControl:=FALSE;               "  , Ш Ш EП           FB_MainControl  -3%]	-3%]      s e fat           FUNCTION_BLOCK FB_MainControl

VAR_INPUT
	Me						: INT;
	EmergencyStop			: BOOL;
	VirtualEmergencyStop		: BOOL;
	S2P_SystemAirPower		: BOOL;
	S1P_SystemAirPower		: BOOL;
	VirtualSystemAirPower		: BOOL;
	ProtectionDoorKey1		: BOOL;
	ProtectionDoorKey2		: BOOL;
	ProtectionDoorKey3		: BOOL;
	ProtectionDoorKey4		: BOOL;
	ProtectionDoorKey5		: BOOL;
	ProtectionDoorKey6		: BOOL;
	ProtectionDoorKey7		: BOOL;
	ProtectionDoorKey8		: BOOL;
	ProtectionDoorKey9		: BOOL;
	ProtectionDoorKey10		: BOOL;
	VirtualProtectionDoor		: BOOL;
	LightCurtainSafetyRelay	: BOOL;
	EStopSafetyRelay			: BOOL;
	SafetyDoorSafetyRelay	: BOOL;
END_VAR

VAR_OUTPUT
	K0_MainValve			: BOOL;
	K1_MainValve			: BOOL;
	K2_MainValve			: BOOL;
END_VAR

VAR



	SystemPressure			: TON;
	BranchK1AirPressure		: TON;
	BranchK2AirPressure		: TON;
	EtherCATErrorTimer		: TON;

	Timer_PowerOn			: TON;
	rTrig_PowerOn			: R_TRIG;

	LasControl				: FB_LasControl;
	StationCfg				:StructStationCfg;
	McCfgSlaveCount: BOOL;
END_VAR

VAR CONSTANT
	ADR_OFF					: INT:= 0;
	ADR_STOP					: INT:= 3;
	ADR_RUN					: INT:= 4;
	ADR_LASTCYCLE			: INT:= 5;
END_VAR
#  (*
MainControl

Build		: 14.11.2005
Version		: 1.2

Description	: 
*)
LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.MainReset,pAgent:=ADR(StationCfg));(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;

GlobalError();
GlobalMessages();
AutomaticManual();
SoftSwitchControl();
SafePositionDefine();
CASE StationCfg.Data^.iActualStepNumber OF
	0:
            LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   a_Off ();
	   LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffMainStart );(*Don't Change*)
	1: a_PowerOn();
	2: a_Calibration();
	3: a_Stop();
	4: a_Run();
	5: a_LastCycle();
ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE; #  , Џ Џ ,І           a_Calibration -3%])  (* Calibtration *)

StationCfg.Data^.xCalibrate:= TRUE;

SignControl();
(*SetMasterMessage(User:= Me, Text:= Text.Name.KeyAutoToAutomaticMode, sValue01:= '');*)

IF SignControl.xCalibrate THEN
	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= FALSE);
END_IF;$  , Ш Ш EП           a_LastCycle -3%]  (* Step_LastCycle *)
SignControl();
StationCfg.Data^.xLastCycle:= TRUE;
IF (*SignControl.xSoftwareHome AND SignControl.xStartOff *)StationEnable=0  THEN
	StationCfg.Data^.xLastCycle:= FALSE;
	StationCfg.Data^.xRun:= FALSE;
	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= ADR_OFF, BackStep:= FALSE);
ELSIF KeyBoard.Key.Pulse.xEnter AND KeyBoard.Key.Sign.xAutomatic THEN
	StationCfg.Data^.xLastCycle:= FALSE;
	LasControl.SetStep(User:= Me,  JumpAddress:= ADR_RUN, BackStep:= FALSE);
END_IF;%  ,              a_Off -3%]P   SignControl();
K0_MainValve:=FALSE;
K1_MainValve:=FALSE;
K2_MainValve:=FALSE;&  ,           	   a_PowerOn -3%]  (* Airpower *)
StationCfg.Data^.xOn:= TRUE;
K0_MainValve:=TRUE;
K1_MainValve:=TRUE;
K2_MainValve:=TRUE;
IF S1P_SystemAirPower OR VirtualSystemAirPower THEN
	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= FALSE);
END_IF;'  , Џ Џ ,І           a_Run -3%]=  (* Run *)
StationCfg.Data^.xStop:= FALSE;
StationCfg.Data^.xRun:= TRUE;


SignControl();

IF  KeyBoard.Key.Pulse.xEnter AND KeyBoard.Key.Pulse.xOn  AND SignControl.xSoftwareHome THEN
	LasControl.SetStep(User:= Me,  ExternalSign:= TRUE, JumpAddress:= ADR_OFF, BackStep:= FALSE);
ELSIF (( KeyBoard.Key.Sign.xManual AND KeyBoard.xKeyClear))
	AND NOT BigTable.MoveTable AND stations.Data[Stations.iBigTable].xDone THEN
	StationCfg.Data^.xLastCycle:= TRUE;
	LasControl.SetStep(User:= Me,  ExternalSign:= TRUE, JumpAddress:= ADR_LASTCYCLE, BackStep:= FALSE);
END_IF;(  , K K ШB           a_Stop -3%]Ы  (* Stop *)

StationCfg.Data^.xCalibrate:= FALSE;
StationCfg.Data^.xStop:= TRUE;
StationCfg.Data^.xRun:= FALSE;


IF KeyBoard.Key.Pulse.xOn  THEN
	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= ADR_OFF, BackStep:= FALSE);
ELSIF  KeyBoard.Key.Sign.xAutomatic  THEN
	StationCfg.Data^.xStop:= FALSE;
	StationCfg.Data^.xRun:= TRUE;
	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= ADR_RUN, BackStep:= FALSE);
END_IF;)  , d d с[           AutomaticManual -3%]J  StationCfg.Data^.xAutomatic:=    KeyBoard.Key.Sign.xAutomatic AND
							NOT SafetyDoorSafetyRelay AND
							NOT EmergencyStop 	AND
							NOT ProtectionDoorKey1	 AND
							NOT ProtectionDoorKey2	 AND
							NOT ProtectionDoorKey3	 AND
							NOT ProtectionDoorKey4	 AND
							NOT ProtectionDoorKey5	 AND
							NOT ProtectionDoorKey6	 AND
							NOT ProtectionDoorKey7	 AND
							NOT ProtectionDoorKey8	 AND
							NOT ProtectionDoorKey9	 AND
							NOT ProtectionDoorKey10	 OR
							VirtualProtectionDoor;
StationCfg.Data^.xManual:= NOT StationCfg.Data^.xAutomatic;*  , с с ^и           GlobalError -3%]  (* Global Error *)


(*======================================================*)
(*Emergency Stop*)
(*======================================================*)
IF EmergencyStop AND NOT VirtualEmergencyStop THEN
	LasControl.SetMasterError(User:= Me, Key:= Text.Name.EmergencyStop,sValue01:= 'EStop');
END_IF;



(*======================================================*)
(*System AirPressure K1*)
(*======================================================*)

BranchK1AirPressure	(
				IN:= 		K1_MainValve AND NOT S1P_SystemAirPower
						AND NOT StationCfg.Data^.xMasterError
						AND NOT VirtualSystemAirPower
						AND NOT SafetyDoorSafetyRelay ,
				PT:= T#5s
				);

IF BranchK1AirPressure.Q THEN
	LasControl.SetMasterError(User:= Me, Key:= Text.Name.SystemAirPressure,sValue01:= 'S1P');
	BranchK1AirPressure(IN:=FALSE);
END_IF;


(*======================================================*)
(*System AirPressure K2*)
(*======================================================*)

BranchK2AirPressure	(
				IN:= 		K2_MainValve AND NOT S2P_SystemAirPower
						AND NOT StationCfg.Data^.xMasterError
						AND NOT VirtualSystemAirPower
						AND NOT LightCurtainSafetyRelay ,
				PT:= T#5s
				);

IF BranchK2AirPressure.Q THEN
	LasControl.SetMasterError(User:= Me, Key:= Text.Name.SystemAirPressure,sValue01:= 'S2P');
	BranchK2AirPressure(IN:=FALSE);
END_IF;


EtherCATErrorTimer(in:=CfgSlaveCount<>SlaveCount,pt:=t#200ms);
IF EtherCATErrorTimer.Q THEN
	LasControl.SetError (User:= Me, Key:= 1000 , sValue01:=CONCAT(CONCAT(CONCAT( 'EtherCATError,CfgSlaveCount:' , UINT_TO_STRING(CfgSlaveCount)),'SlaveCount:'),UINT_TO_STRING(SlaveCount)) );
END_IF;+  , } } њt           GlobalMessages -3%]S  (* Global Messages *)

IF  ProtectionDoorKey1 AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '1#');
ELSIF  ProtectionDoorKey2  AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '2#');
ELSIF  ProtectionDoorKey3 AND NOT  VirtualProtectionDoor  THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '3#');
ELSIF  ProtectionDoorKey4  AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '4#');
ELSIF  ProtectionDoorKey5 AND NOT  VirtualProtectionDoor  THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '5#');
ELSIF  ProtectionDoorKey6 AND NOT  VirtualProtectionDoor  THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '6#');
ELSIF  ProtectionDoorKey7  AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '7#');
ELSIF  ProtectionDoorKey8  AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '8#');
ELSIF  ProtectionDoorKey9 AND NOT  VirtualProtectionDoor  THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '9#');
ELSIF  ProtectionDoorKey10  AND NOT  VirtualProtectionDoor THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.FailProtectionDoor, sValue01:= '10#');
END_IF,                       SafePositionDefine -3%]Ч   StationCfg.Data^.xSafePosition:=	NOT EStopSafetyRelay AND  (* NOT LightCurtainSafetyRelay AND*)
								NOT SafetyDoorSafetyRelay ;

StationCfg.Data^.xLightCurtainRelay := LightCurtainSafetyRelay;-     J :             SoftSwitchControl -3%]/  IF EStopSafetyRelay THEN
	StationCfg.Data^.xAutomatic:=FALSE;
	StationCfg.Data^.xManual:=FALSE;
	StationCfg.Data^.SoftSwitchedOn:=FALSE;
END_IF

rTrig_PowerOn(CLK:= KeyBoard.Key.Pulse.xOn);
IF   rTrig_PowerOn.Q   AND KeyBoard.Key.Sign.xManual THEN
	StationCfg.Data^.SoftSwitchedOn:=TRUE;
END_IF             J   , 2 2 Џ)           FB_ManageOverviewInfo -3%]	-3%]                      ф   FUNCTION_BLOCK FB_ManageOverviewInfo
VAR_INPUT
END_VAR
VAR_OUTPUT
END_VAR
VAR
	iCounter				: INT;
END_VAR


VAR CONSTANT
	AUTO				: STRING:= 'Auto';
	MANUAL			: STRING:= 'Manual';
	NONE				: STRING:= '---';
END_VAR  (*FB_ManageOverviewInfo*)
(*
	Version:  1.0.0.1
	Time:       2018-04-13
	Author:     Wang Yumin
	Description:  Suits for providing overview infromation of stations fo LAS
*)

FOR iCounter:= 1 TO Stations.MaxUser DO

	PLC_arrOverviewInfo[iCounter].iKeyUser := Stations.Data[iCounter].iUser;
	PLC_arrOverviewInfo[iCounter].iCarrierNumber := Stations.Data[iCounter].iCarrierNr;
	PLC_arrOverviewInfo[iCounter].iTestmanPassCounter :=0;
	PLC_arrOverviewInfo[iCounter].iTestmanFailCounter :=0;
	PLC_arrOverviewInfo[iCounter].iActualStepNumber :=Stations.Data[iCounter].iActualStepNumber;
	PLC_arrOverviewInfo[iCounter].iRequestedStepNumber :=Stations.Data[iCounter].iFailRequestedStepNumber;

	PLC_arrOverviewInfo[iCounter].strStationName :=Stations.Data[iCounter].StationName;
	PLC_arrOverviewInfo[iCounter].strProcessTime :=CONCAT(TIME_TO_STRING(Stations.Data[iCounter].tProcessTime),'');
	PLC_arrOverviewInfo[iCounter].strStepLog :=Stations.Data[iCounter].StepLog;

	PLC_arrOverviewInfo[iCounter].strTestmanStatus :='';
	PLC_arrOverviewInfo[iCounter].strTestmanDppm :='';
	PLC_arrOverviewInfo[iCounter].strTestmanPercent :='';

	PLC_arrOverviewInfo[iCounter].xPromptError :=Stations.Data[iCounter].xMasterError OR  Stations.Data[iCounter].xError;
	PLC_arrOverviewInfo[iCounter].xPromptMessage :=Stations.Data[iCounter].xMasterMessage OR Stations.Data[iCounter].xMessage OR Stations.Data[iCounter].xTips;

	IF Stations.Data[iCounter].xAutomatic THEN
		PLC_arrOverviewInfo[iCounter].strAutoManual :=AUTO;
	ELSIF  Stations.Data[iCounter].xManual THEN
		PLC_arrOverviewInfo[iCounter].strAutoManual :=MANUAL;
	ELSE
		PLC_arrOverviewInfo[iCounter].strAutoManual :=NONE;
	END_IF

	IF Stations.Data[iCounter].pWT=0 THEN
		PLC_arrOverviewInfo[iCounter].strArticleNumber:= NONE;
		PLC_arrOverviewInfo[iCounter].strSerialNumber :=NONE;
		PLC_arrOverviewInfo[iCounter].strScheduleName :=NONE;
		PLC_arrOverviewInfo[iCounter].xTestResult:=TRUE;
		PLC_arrOverviewInfo[iCounter].iDestinationStation:=0;
	ELSE
		PLC_arrOverviewInfo[iCounter].strArticleNumber:= Stations.Data[iCounter].pWT^.stuVariantInfoSet.strKostalNr;
		PLC_arrOverviewInfo[iCounter].strSerialNumber :=Stations.Data[iCounter].pWT^.stuVariantInfoSet.strSerialNr;
		PLC_arrOverviewInfo[iCounter].strScheduleName :=Stations.Data[iCounter].pWT^.strScheduleName;
		PLC_arrOverviewInfo[iCounter].xTestResult:=Stations.Data[iCounter].pWT^.bulTestResult;
		PLC_arrOverviewInfo[iCounter].iDestinationStation:=BYTE_TO_INT(Stations.Data[iCounter].pWT^.bytDestinationStation);
	END_IF

	IF Stations.Data[iCounter].xLastCycle THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='LastCycle';
	ELSIF Stations.Data[iCounter].xStationToTable AND iCounter <> stations.iBigTable THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Ready';
	ELSIF Stations.Data[iCounter].xTableToStation AND iCounter= stations.iBigTable THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Ready';
	ELSIF Stations.Data[iCounter].xRun THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Run';
	ELSIF Stations.Data[iCounter].xStart THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Start';
	ELSIF Stations.Data[iCounter].xStop THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Stop';
	ELSIF Stations.Data[iCounter].xCalibrate THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Calibrated';
	ELSIF Stations.Data[iCounter].xOn THEN
		PLC_arrOverviewInfo[iCounter].strStationStatus :='On';
	ELSE
		PLC_arrOverviewInfo[iCounter].strStationStatus :='Off';
	END_IF

END_FOR;

IF KeyBoard.Key.Sign.xAutomatic THEN
	PLC_arrOverviewInfo[Stations.system].strAutoManual:=AUTO;

ELSIF KeyBoard.Key.Sign.xManual THEN
	PLC_arrOverviewInfo[Stations.system].strAutoManual:=MANUAL;

ELSE
	PLC_arrOverviewInfo[Stations.system].strAutoManual:=NONE;

END_IF

PLC_arrOverviewInfo[Stations.system].strStationStatus:=PLC_arrOverviewInfo[Stations.Main].strStationStatus;               K   , K K ШB           FB_MultiCylinder -3%]	-3%]                      х  FUNCTION_BLOCK FB_MultiCylinder
VAR_INPUT
	Me					: INT;
	JumpAddress		: INT;
	UsedCylinder			: INT;

	Cylinders			: ARRAY[1..50] OF structMultiCylinder;

END_VAR

VAR_OUTPUT CONSTANT
	MAX_CYLINDER		: INT:= 50;
END_VAR
VAR
	iX					: INT;
	xAllCylinderAck		: BOOL;
	i					: INT;
	SetError						: FB_SetError;
	SetMasterError				: FB_SetMasterError;
	SetMasterMessage			: FB_SetMasterMessage;
	SetMessage					: FB_SetMessage;
	SetStep						 :FB_SetStep;
END_VAR

џ  (*
Switch x Ccylindermoduls Parallel

Version 1.0
Build	12.05.2006

*)

UsedCylinder:= LIMIT(1,UsedCylinder,MAX_CYLINDER);

FOR i:= 1 TO UsedCylinder DO
	Cylinders[i].pCylinder^ (Direction:= Cylinders[i].Direction, StepEnable:= FALSE, MultiProg:= TRUE );
END_FOR;

xAllCylinderAck:= TRUE;
FOR i:= 1 TO UsedCylinder DO
	IF NOT Cylinders[i].pCylinder^.ConditionAck THEN
		xAllCylinderAck:= FALSE;
		EXIT;
	END_IF;
END_FOR;

Stations.Data[Me].TimeOut.xEnable:= NOT xAllCylinderAck;

SetStep(User:= Me,  ExternalSign:= xAllCylinderAck, JumpAddress:= JumpAddress, BackStep:= TRUE );

IF Stations.Data[Me].Step.BackStepAck AND JumpAddress = 0 THEN
	FOR i:= 1 TO UsedCylinder DO
		Cylinders[i].pCylinder^(StepEnable:= FALSE );
	END_FOR;
END_IF;               N   , Ш Ш EП           FB_Omron -3%]	-3%]                      Y  FUNCTION_BLOCK FB_Omron
VAR_INPUT
	Run					: BOOL:=FALSE;
	Start					: BOOL;
	Variant				: INT;
	Omron_OR			: BOOL;
	Omron_Error			: BOOL;
	Omron_Enable		: BOOL;
END_VAR
VAR_OUTPUT
	Ready				: BOOL;
	Pass				: BOOL;
	Fail					: BOOL;
	Omron_Reset			: BOOL;
	Omron_Trig			: BOOL;
	Omron_Bank0		: BOOL;
	Omron_Bank1		: BOOL;
	Omron_Bank2 		: BOOL;
	Omron_Bank3		: BOOL;
	Omron_Bank4		: BOOL;
	Omron_DI5			: BOOL;
	Omron_DI6			: BOOL;
	Omron_DI7			: BOOL;
	Omron_DI8			: BOOL;
	Omron_DSA			: BOOL;
END_VAR
VAR
	Timer			: TON;
	Start_Trig		: R_TRIG;
	Step			: INT:=0;
END_VARH  IF NOT Run THEN
	Ready:=FALSE;
	Pass:=FALSE;
	Fail:=FALSE;
	Step:=0;
  	RETURN;
END_IF

Start_Trig(clk:=Start);

CASE Step OF
	0:
		SetGroup();
		Timer(in:=Omron_Enable,pt:=t#200ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF
	1:
		Omron_DI8:=TRUE;
		IF NOT Omron_Enable THEN
			Step:=Step+1;
		END_IF
	2:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=FALSE;
		Omron_DI7:=FALSE;
		Omron_DI8:=FALSE;
		IF Omron_Enable THEN
			Step:=Step+1;
		END_IF
	3:
		SetBank();
		Timer(in:=TRUE,pt:=t#200ms);
		IF Timer.Q THEN
			Timer(in:=FALSE);
			Step:=Step+1;
		END_IF
	4:
		Omron_DI8:=TRUE;
		IF NOT Omron_Enable THEN
			Step:=Step+1;
		END_IF
	5:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=FALSE;
		Omron_DI7:=FALSE;
		Omron_DI8:=FALSE;
		IF Omron_Enable THEN
			Ready:=TRUE;
			Step:=10;
		END_IF
	10:
		IF Start_Trig.Q  THEN
			Pass:=FALSE;
			Fail:=FALSE;
			Step:=Step+1;
		END_IF
	11:
		Omron_Trig:=TRUE;
		Timer(in:=TRUE,pt:=t#1s);
		IF Omron_OR THEN
			Pass:=TRUE;
			Fail:=FALSE;
			Omron_Trig:=FALSE;
			Step:=Step+1;
			Timer(in:=FALSE);
		ELSIF Timer.Q THEN
			Pass:=FALSE;
			Fail:=TRUE;
			Omron_Trig:=FALSE;
			Step:=Step+1;
			Timer(in:=FALSE);
		END_IF
	12:
		Timer(in:=TRUE,pt:=t#1s);
		IF Timer.Q THEN
			Pass:=FALSE;
			Fail:=FALSE;
			Timer(in:=FALSE);
			Step:=10;
		END_IF
END_CASE O   , њ њ wё           SetBank -3%]Љ  CASE( Variant  MOD 32) OF
	0:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	1:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	2:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	3:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	4:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	5:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	6:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	7:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	8:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	9:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	10:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	11:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	12:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	13:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	14:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	15:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=FALSE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	16:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	17:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	18:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	19:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	20:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	21:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	22:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	23:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=FALSE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	24:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	25:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	26:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	27:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=FALSE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	28:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	29:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=FALSE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	30:
		Omron_Bank0:=FALSE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
	31:
		Omron_Bank0:=TRUE;
		Omron_Bank1:=TRUE;
		Omron_Bank2:=TRUE;
		Omron_Bank3:=TRUE;
		Omron_Bank4:=TRUE;
		Omron_DI5:=FALSE;
		Omron_DI6:=TRUE;
		Omron_DI7:=FALSE;
END_CASEP   ,     }ї           SetGroup -3%]Ё  IF Variant<=32 THEN
	Omron_Bank0:=FALSE;
	Omron_Bank1:=FALSE;
	Omron_Bank2:=FALSE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=33 AND Variant<=64 THEN
	Omron_Bank0:=TRUE;
	Omron_Bank1:=FALSE;
	Omron_Bank2:=FALSE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=65 AND Variant<=96 THEN
	Omron_Bank0:=FALSE;
	Omron_Bank1:=TRUE;
	Omron_Bank2:=FALSE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=97 AND Variant<=128 THEN
	Omron_Bank0:=TRUE;
	Omron_Bank1:=TRUE;
	Omron_Bank2:=FALSE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=129 AND Variant<=160 THEN
	Omron_Bank0:=FALSE;
	Omron_Bank1:=FALSE;
	Omron_Bank2:=TRUE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=161 AND Variant<=192 THEN
	Omron_Bank0:=TRUE;
	Omron_Bank1:=FALSE;
	Omron_Bank2:=TRUE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
ELSIF Variant>=224 AND Variant<=256 THEN
	Omron_Bank0:=FALSE;
	Omron_Bank1:=TRUE;
	Omron_Bank2:=TRUE;
	Omron_Bank3:=FALSE;
	Omron_Bank4:=FALSE;
	Omron_DI5:=FALSE;
	Omron_DI6:=TRUE;
	Omron_DI7:=TRUE;
END_IF             U   ,              FB_ReportHandler LЦT]	-3%]                      р  FUNCTION_BLOCK FB_ReportHandler
VAR_INPUT
END_VAR
VAR_OUTPUT
	SelectedUser				: INT;

	ErrorMessageSet			: structErrorMessageSet;

END_VAR
VAR
	LastUser_MasterError		: INT;
	LastUser_Error			: INT;
	LastUser_MasterMessage	: INT;
	LastUser_Message		: INT;
	LastUser_Tips			: INT;
	ErrorID					:INT;
	iX						: INT;
	i						: INT;

	InfoTimer				: TON;
	InfoRun					: BOOL;
	InfoPointer				: INT;
	sTemp1					: STRING;
	LastManualSelectedUser	: INT:=2;

	LastErrorCode			: INT:=  0;

END_VAR


VAR CONSTANT
	MasterError				: STRING:= 'MasterError';
	Error					: STRING:= 'Error';
	MasterMessage			: STRING:= 'MasterMessage';
	Message				: STRING:= 'Message';
	Tips						: STRING:= 'Tips';
END_VAR


Ћ   (*

Version		: 3.0.0
Built		: 24.08.2006

Description	: 
*)

ReportControl();
SelectedUserControl();

(*loop Errors or Messages for LAS*)
ReportControlToLas(); V   ,              InfoPage -3%]  (* =================================================================================== *)
(* InfoPages *)
(* =================================================================================== *)

InfoTimer (	IN:= InfoRun
				AND NOT KeyBoard.Key.Pulse.xArrowDown
				AND NOT KeyBoard.Key.Pulse.xArrowUp,
			PT:= T#1m);

IF KeyBoard.Key.Pulse.Info THEN
	InfoRun:= NOT InfoRun;
ELSIF InfoTimer.Q THEN
	InfoRun:= FALSE;
END_IF;

IF InfoRun THEN
	HMImessage.strErrorTitle:= TextFile( Text.StartInfoPage + InfoPointer, Language );

	IF KeyBoard.Key.Pulse.xArrowUp THEN
		InfoPointer:= InfoPointer + 1;
	ELSIF KeyBoard.Key.Pulse.xArrowDown THEN
		InfoPointer:= InfoPointer - 1;
	END_IF;

	CASE InfoPointer OF
		0:	hmimessage.strErrorMessage:= ProjectData.Project_ID;
		1:	hmimessage.strErrorMessage:= ProjectData.Trace_ID;
		2:	hmimessage.strErrorMessage:= ProjectData.Drawing_ID;
		3:	hmimessage.strErrorMessage:= ProjectData.PLC_Version;
	ELSE
		InfoPointer:= 0;
	END_CASE;

ELSE
	InfoPointer:= 0;
END_IF;W   ,              ReportControl :ЦT]E$  (* SelectedUser:= 0; *)

InfoPage();
IF InfoRun THEN RETURN; END_IF;

(* =================================================================================== *)
(* =================================================================================== *)
(*Priority 1 => MasterError*)
IF LastUser_MasterError <> 0 THEN
	IF Stations.Data[LastUser_MasterError].xMasterError THEN
		IF Stations.Data[LastUser_MasterError].TextLine0 = '' THEN
			hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + LastUser_MasterError, Language );
		ELSE
			hmiMessage.strErrorTitle:= Stations.Data[LastUser_MasterError].TextLine0;
		END_IF;
		sTemp1:= TextFile( Stations.Data[LastUser_MasterError].iText, Language );
		hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastUser_MasterError].sValue01 );
		SelectedUser:= LastUser_MasterError;
		RETURN;
	ELSE
		LastUser_MasterError:= 0;
	END_IF;

END_IF;

FOR iX:= 1 TO Stations.MaxUser DO
	IF Stations.Data[iX].xMasterError THEN
		IF Stations.Data[iX].TextLine0 = '' THEN
			hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + iX, Language );
		ELSE
			hmiMessage.strErrorTitle:= Stations.Data[iX].TextLine0;
		END_IF;
		sTemp1:= TextFile( Stations.Data[iX].iText, Language );
		hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[iX].sValue01 );
		LastUser_MasterError:= iX;
		SelectedUser:= LastUser_MasterError;
		RETURN;
	END_IF;
END_FOR;

(*Automatik view*)
IF KeyBoard.Key.Sign.xAutomatic THEN

	(*Priority 2 => Error*)
	IF LastUser_Error <> 0 THEN
		IF Stations.Data[LastUser_Error].xError THEN
			IF Stations.Data[LastUser_Error].TextLine0 = '' THEN
				hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + LastUser_Error, Language );
			ELSE
				hmiMessage.strErrorTitle:= Stations.Data[LastUser_Error].TextLine0;
			END_IF;
			sTemp1:= TextFile( Stations.Data[LastUser_Error].iText, Language );
			hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastUser_Error].sValue01 );
			SelectedUser:= LastUser_Error;
			RETURN;
		ELSE
			LastUser_Error:= 0;
		END_IF;
	END_IF;

	FOR iX:= 1 TO Stations.MaxUser DO
	IF Stations.Data[iX].xError THEN
	IF Stations.Data[iX].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + iX, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[iX].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[iX].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[iX].sValue01 );
	LastUser_Error:= iX;
	SelectedUser:= LastUser_Error;
	RETURN;
	END_IF;
	END_FOR;

	(* Priority 3 => Station off - No Message will send *)
	IF NOT Stations.Data[Stations.Main].xOn THEN
	IF Stations.Data[Stations.Main].TextLine0 = '' THEN
	hmiMessage.strErrorTitle:= TextFile( Text.StartOfMainControlText + Stations.Data[Stations.Main].iActualStepNumber + 1, Language );
	ELSE
	hmiMessage.strErrorTitle:= Stations.Data[Stations.Main].TextLine0;
	END_IF;
	CASE Stations.Data[Stations.Main].iActualStepNumber OF
	0:	hmiMessage.strErrorMessage:= TextFile( Text.Name.OnSwitchManualAndOn_F5, Language );
	1:	hmiMessage.strErrorMessage:= TextFile( Text.Name.PLeaseWait, Language );
	END_CASE;

	SelectedUser:= Stations.Main;
	RETURN;
	END_IF;


	(*Priority 4 => MasterMessage*)
	IF LastUser_MasterMessage <> 0 THEN
	IF Stations.Data[LastUser_MasterMessage].xMasterMessage THEN
	IF Stations.Data[LastUser_MasterMessage].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + LastUser_MasterMessage, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[LastUser_MasterMessage].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[LastUser_MasterMessage].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastUser_MasterMessage].sValue01 );
	SelectedUser:= LastUser_MasterMessage;
	RETURN;
	ELSE
	LastUser_MasterMessage:= 0;
	END_IF;
	END_IF;

	FOR iX:= 1 TO Stations.MaxUser DO
	IF Stations.Data[iX].xMasterMessage THEN
	IF Stations.Data[iX].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + iX, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[iX].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[iX].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[iX].sValue01 );
	LastUser_MasterMessage:= iX;
	SelectedUser:= LastUser_MasterMessage;
	RETURN;
	END_IF;
	END_FOR;

	(*Priority 5 => Message*)
	IF LastUser_Message <> 0 THEN
	IF Stations.Data[LastUser_Message].xMessage THEN
	IF Stations.Data[LastUser_Message].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + LastUser_Message, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[LastUser_Message].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[LastUser_Message].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastUser_Message].sValue01 );
	SelectedUser:= LastUser_Message;
	RETURN;
	ELSE
	LastUser_Message:= 0;
	END_IF;
	END_IF;

	FOR iX:= 1 TO Stations.MaxUser DO
	IF Stations.Data[iX].xMessage THEN
	IF Stations.Data[iX].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + iX, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[iX].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[iX].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[iX].sValue01 );
	LastUser_Message:= iX;
	SelectedUser:= LastUser_Message;
	RETURN;
	END_IF;
	END_FOR;

	(*Priority 6 => Tips*)
	IF LastUser_Tips <> 0 THEN
	IF Stations.Data[LastUser_Tips].xTips THEN
	IF Stations.Data[LastUser_Tips].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + LastUser_Tips, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[LastUser_Tips].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[LastUser_Tips].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastUser_Tips].sValue01 );
	SelectedUser:= LastUser_Tips;
	RETURN;
	ELSE
	LastUser_Tips:= 0;
	END_IF;
	END_IF;

	FOR iX:= 1 TO Stations.MaxUser DO
	IF Stations.Data[iX].xTips  THEN
	IF Stations.Data[iX].TextLine0 = '' THEN
		hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + iX, Language );
	ELSE
		hmiMessage.strErrorTitle:= Stations.Data[iX].TextLine0;
	END_IF;
	sTemp1:= TextFile( Stations.Data[iX].iText, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[iX].sValue01 );
	LastUser_Tips:= iX;
	SelectedUser:= LastUser_Tips;
	RETURN;
	END_IF;
	END_FOR;


	(*Priority 7 => MainControlText*)
	hmiMessage.strErrorTitle:= TextFile( Text.StartOfMainControlText + Stations.Data[Stations.Main].iActualStepNumber + 1, Language );
	hmiMessage.strErrorMessage:= ProjectData.Project_ID;
	SelectedUser:= Stations.Main;

	(*Manual view*)
	ELSIF KeyBoard.Key.Sign.xManual THEN

	(* SelectedUser:= 0; *)

	hmiMessage.strErrorTitle:= TextFile( Text.StartUserDefinition + Stations.SelectedStation, Language );

	IF Stations.Data[SelectedUser].xMasterError
	OR Stations.Data[SelectedUser].xError THEN
	LastManualSelectedUser:= SelectedUser;
	sTemp1:= TextFile( Stations.Data[LastManualSelectedUser].iText, Language );
	IF Stations.Data[LastManualSelectedUser].TextLine0 <> '' THEN
	hmiMessage.strErrorTitle:= Stations.Data[LastManualSelectedUser].TextLine0;
	END_IF;
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastManualSelectedUser].sValue01 );

	ELSIF NOT Stations.Data[Stations.Main].xOn THEN
	IF Stations.Data[Stations.Main].TextLine0 = '' THEN
	hmiMessage.strErrorTitle:= TextFile( Text.StartOfMainControlText + Stations.Data[Stations.Main].iActualStepNumber + 1, Language );
	ELSE
	hmiMessage.strErrorTitle:= Stations.Data[Stations.Main].TextLine0;
	END_IF;
	CASE Stations.Data[Stations.Main].iActualStepNumber OF
	0:	hmiMessage.strErrorMessage:= TextFile( Text.Name.OnSwitchManualAndOn_F5, Language );
	1:	hmiMessage.strErrorMessage:= TextFile( Text.Name.PLeaseWait, Language );
	END_CASE;

	SelectedUser:= Stations.Main;
	RETURN;

	ELSIF Stations.Data[SelectedUser].xMasterMessage THEN
	LastManualSelectedUser:= SelectedUser;
	sTemp1:= TextFile( Stations.Data[LastManualSelectedUser].iText, Language );
	IF Stations.Data[LastManualSelectedUser].TextLine0 <> '' THEN
	hmiMessage.strErrorTitle:= Stations.Data[LastManualSelectedUser].TextLine0;
	END_IF;
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, Stations.Data[LastManualSelectedUser].sValue01 );

	ELSE

	IF KeyBoard.Key.Pulse.xArrowUp THEN
	LastManualSelectedUser:= LastManualSelectedUser + 1;
	ELSIF KeyBoard.Key.Pulse.xArrowDown THEN
	LastManualSelectedUser:= LastManualSelectedUser - 1;
	END_IF;

	sTemp1:= TextFile( Text.Name.Step, Language );
	hmiMessage.strErrorMessage:= CONCAT ( sTemp1, INT_TO_STRING( Stations.Data[LastManualSelectedUser].iActualStepNumber) );
	END_IF;

	(*LastManualSelectedUser:= LIMIT ( Stations.StartStations, LastManualSelectedUser, Stations.MaxUser); *)
	LastManualSelectedUser:= MIN ( Stations.StartStations,  Stations.MaxUser);
	SelectedUser:= LastManualSelectedUser;
	
	END_IF;X   , Ш Ш EП           ReportControlToLas -3%]ў  (*************************************************Error  ***************************************************)
IF KeyBoard.Key.Sign.xManual THEN
	ErrorMessageSet.iKeyUser			:= Stations.Data[SelectedUser].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[SelectedUser].iText;
	IF Stations.Data[SelectedUser].xMasterError OR Stations.Data[SelectedUser].xError THEN
		ErrorMessageSet.strErrorType		:= MasterError;
	ELSIF Stations.Data[SelectedUser].xMasterMessage  THEN
		ErrorMessageSet.strErrorType		:= MasterMessage;
	ELSIF Stations.Data[SelectedUser].xMessage   THEN
		ErrorMessageSet.strErrorType		:= Message;
	ELSE
		ErrorMessageSet.strErrorType		:= Tips;
	END_IF
	ErrorMessageSet.strErrorSource		:= Stations.Data[SelectedUser].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[SelectedUser].sValue01;
	ErrorMessageSet.strErrorTitle		:= hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSIF LastUser_MasterError <> 0 THEN
	ErrorMessageSet.iKeyUser			:= Stations.Data[LastUser_MasterError].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[LastUser_MasterError].iText;
	ErrorMessageSet.strErrorType		:= MasterError;
	ErrorMessageSet.strErrorSource		:= Stations.Data[LastUser_MasterError].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[LastUser_MasterError].sValue01;
	ErrorMessageSet.strErrorTitle		:= hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSIF  LastUser_Error <> 0 THEN
	ErrorMessageSet.iKeyUser			:= Stations.Data[LastUser_Error].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[LastUser_Error].iText;
	ErrorMessageSet.strErrorType		:= Error;
	ErrorMessageSet.strErrorSource		:= Stations.Data[LastUser_Error].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[LastUser_Error].sValue01;
	ErrorMessageSet.strErrorTitle		:=hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSIF LastUser_MasterMessage <> 0 THEN
	ErrorMessageSet.iKeyUser			:= Stations.Data[LastUser_MasterMessage].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[LastUser_MasterMessage].iText;
	ErrorMessageSet.strErrorType		:= MasterMessage;
	ErrorMessageSet.strErrorSource		:= Stations.Data[LastUser_MasterMessage].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[LastUser_MasterMessage].sValue01;
	ErrorMessageSet.strErrorTitle		:= hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSIF LastUser_Message <> 0 THEN

	ErrorMessageSet.iKeyUser			:= Stations.Data[LastUser_Message].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[LastUser_Message].iText;
	ErrorMessageSet.strErrorType		:= Message;
	ErrorMessageSet.strErrorSource		:= Stations.Data[LastUser_Message].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[LastUser_Message].sValue01;
	ErrorMessageSet.strErrorTitle		:= hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSIF LastUser_Tips <> 0 THEN

	ErrorMessageSet.iKeyUser			:= Stations.Data[LastUser_Tips].iUser;
	ErrorMessageSet.iErrorCode		:= Stations.Data[LastUser_Tips].iText;
	ErrorMessageSet.strErrorType		:= Tips;
	ErrorMessageSet.strErrorSource		:= Stations.Data[LastUser_Tips].StationName;
	ErrorMessageSet.strErrorValue		:= Stations.Data[LastUser_Tips].sValue01;
	ErrorMessageSet.strErrorTitle		:= hmiMessage.strErrorTitle;
	ErrorMessageSet.strErrorMessage	:= hmiMessage.strErrorMessage;

	IF ErrorMessageSet.iErrorCode<> LastErrorCode THEN
		ErrorMessageSet.strRaisedTime:= SYSTEM.sTime ;
	END_IF

ELSE

	ErrorMessageSet.iKeyUser			:= 0;
	ErrorMessageSet.iErrorCode		:= 0;
	ErrorMessageSet.strErrorType		:= '';
	ErrorMessageSet.strErrorSource		:= '';
	ErrorMessageSet.strErrorValue		:= '';
	ErrorMessageSet.strRaisedTime	:= '' ;
	ErrorMessageSet.strErrorTitle		:= '';
	ErrorMessageSet.strErrorMessage	:= '';

END_IF

LastErrorCode :=ErrorMessageSet.iErrorCode;Y   , Џ Џ ,І           SelectedUserControl -3%]  (*======================================================	*)
(*User definition						*)
(*======================================================	*)
(*
IF SelectedUser = 0 THEN
	IF KeyBoard.Key.Sign.xManual AND KeyBoard.Key.Pulse.xArrowUp THEN
		Stations.SelectedStation:= Stations.SelectedStation + 1;
	ELSIF KeyBoard.Key.Sign.xManual AND KeyBoard.Key.Pulse.xArrowDown THEN
		Stations.SelectedStation:= Stations.SelectedStation - 1;
	END_IF;
ELSE
	Stations.SelectedStation:= SelectedUser;
END_IF;
*)


Stations.SelectedStation:= SelectedUser;
Stations.SelectedStation:= LIMIT ( Stations.StartStations, Stations.SelectedStation, Stations.MaxUser);             Z   ,     }ї           FB_RequestResponseInteraction -3%]	-3%]                        FUNCTION_BLOCK FB_RequestResponseInteraction
VAR_INPUT
	User						: INT;
	StartTask					: BOOL;
	PositiveAction				: BOOL;
	UpdateTarget				: BOOL;(*Update Destination*)
	Complete					: BOOL:=FALSE;(*Final LineControl *)

	pRequest					: POINTER TO StructRequestAction;
	pResponse					: POINTER TO StructResponseAction;


END_VAR

VAR_OUTPUT

	Done				: BOOL;
	Abort				: BOOL;

END_VAR

VAR
	SetError						: FB_SetError;
	SetMasterError				: FB_SetMasterError;
	SetMasterMessage			: FB_SetMasterMessage;
	SetMessage					: FB_SetMessage;
	SetTips						: FB_SetTips;
	SetStep						 :FB_SetStep;
	Step						: INT:=0;

	SkipTask					: BOOL:=FALSE;(*Skip EOL Test*)
	MessageTimer				: TON;

	EmptyRequestAction			: StructRequestAction;
	EmptyResponseAction			: StructResponseAction;

	EmptyFailedPartInfo			:StructFailedPartInfo;

	TimeOut						: TIME:=t#500s;
END_VARQ  (*FB_RequestResponseInteraction*)
(*
	Version:  1.0.1.0
	Time:       2018-07-24
	Author:    Wang Yumin
	Description:  Suits for LAS interaction betweein PLC and Tester
*)

	IF pRequest=0 OR pResponse=0 OR User=0 THEN
		SetError(User:= User, Key:= Text.Name.RequestResponseInteraction, sValue01:='Error' );
		RETURN;
	END_IF

	IF NOT StartTask THEN
		Step:=0;
		Complete:=FALSE;
		pRequest^:=EmptyRequestAction;
		pResponse^:=EmptyResponseAction;

		Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1;
		RETURN;
	END_IF

	CASE Step OF
		0:
			MessageTimer(in:=FALSE);

			IF StartTask THEN
				Done := FALSE;
				Abort := FALSE;
				Step:=Step+1;
			END_IF

		1:
			IF StartTask THEN
				Step:=Step+1;
			END_IF

		2:
			pRequest^.stuActionArticleSet:=Stations.Data[User].pWT^.stuVariantInfoSet ;
			pRequest^.strActionScheduleName:=Stations.Data[User].pWT^.strScheduleName;
			Step:=Step+1;

		3:
			IF Complete THEN PLC_stuFailedPartInfo:=Stations.Data[User].pWT^.stuFailedPartInfo; END_IF
			pRequest^.bulDoPositiveAction:= PositiveAction;
			pRequest^.bulDoNegativeAction:=NOT PositiveAction;

			Step:=Step+1;

		4:	(*=================!!!Wait for Tester Result=================================*)
			MessageTimer(in:=TRUE,pt:=TimeOut);

			IF pResponse^.bulActionIsPass AND NOT pResponse^.bulActionIsFail  THEN
				MessageTimer(in:=FALSE);
				IF  UpdateTarget THEN
					UpdateDestinationStation(User, Stations.Data[User].bStationID,TRUE);
				END_IF
				Step:=Step+1;

			ELSIF pResponse^.bulActionIsFail AND NOT pResponse^.bulActionIsPass  THEN

				MessageTimer(in:=FALSE);
				IF  UpdateTarget THEN
					UpdateDestinationStation(User, Stations.Data[User].bStationID,FALSE);
				END_IF
				Stations.Data[User].pWT^.bulTestResult:= FALSE;
				Stations.Data[User].pWT^.stuFailedPartInfo:=StringtoFailPartInfo(User,Stations.Data[User].bStationID,pResponse^.strActionResultText ,TRUE);
				Step:=Step+1;

			ELSIF pResponse^.bulActionIsFail AND pResponse^.bulActionIsPass  THEN

				MessageTimer(in:=FALSE);
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.RequestResponseInteraction, sValue01:='Logic Error' );

			ELSIF MessageTimer.Q THEN

				MessageTimer(in:=FALSE);

				SetError(User:= User, Key:= Text.Name.RequestResponseInteraction, sValue01:='Timeout' );

				(*Abort :=TRUE;
				Stations.Data[User].pWT^.bulTestResult:= FALSE;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailCarrierNr 		:= CONCAT( 'WT',INT_TO_STRING(Stations.Data[User].iCarrierNr));
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailKostalNr		:= Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailSerialNr 		:= Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailCode 			:= CONCAT('EC',  RIGHT(CONCAT('000', INT_TO_STRING(Text.Name.ScannFail)),3));
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailStationNr 		:= Stations.Data[User].StationName;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailScheduleName := Stations.Data[User].pWT^.strScheduleName;
				Stations.Data[User].pWT^.stuFailedPartInfo.strFailText			 := 'Waiting for Tester Timeout';
				*)
(*
			ELSIF KeyBoard.Key.Sign.xStopOnly THEN

				MessageTimer(in:=FALSE);
				Abort :=TRUE;
*)
			ELSE

				SetTips(User:= User, Key:= Text.Name.RequestResponseInteraction, sValue01:=' Message' );

			END_IF

		5:
			pRequest^:=EmptyRequestAction;
			pResponse^:=EmptyResponseAction;

			Step:=Step+1;

		6:
			Done := TRUE;
		ELSE
			Abort :=TRUE;
			SetMasterError(User:= User, Key:= Text.Name.RequestResponseInteraction, sValue01:='MasterError' );

	END_CASE
	SetStep(	User:= User,
					ExternalSign:= Done OR Abort,
					JumpAddress:= 0,
					BackStep:= 	FALSE
			 );
	IF Stations.Data[User].Step.Ack THEN
		Complete:=FALSE;
		StartTask:=FALSE;
		IF Abort AND UpdateTarget  AND NOT SkipTask THEN
			UpdateDestinationStation(User, Stations.Data[User].bStationID,FALSE);
		END_IF
		Step:=0;
		MessageTimer(in:=FALSE);
	END_IF               \  , Џ Џ ,І           FB_RobotPositionServer -3%]	-3%]      ;ТРЈ<          U  FUNCTION_BLOCK FB_RobotPositionServer
VAR_INPUT
	User						: INT;
	Run							: BOOL:=FALSE;(**)
	Position					: BYTE;
	StartTask					: BOOL:=FALSE;
	StartHome					: BOOL:=FALSE;
	pAgent						: POINTER TO FB_EPSON_ROBOT;
END_VAR


VAR_OUTPUT
	Done						: BOOL;
	Abort						: BOOL;
END_VAR


VAR
	SetError					: FB_SetError;
	SetMasterError				: FB_SetMasterError;
	SetMasterMessage			: FB_SetMasterMessage;
	SetMessage					: FB_SetMessage;
	SetStep						: FB_SetStep;
	Step						: INT:=0;

	MessageTimer				: TON;
	Timer						: TON;
	TimeOut						: TIME:=t#300s;
END_VAR1  	IF pAgent=0 OR User=0 THEN
		RETURN;
	END_IF

	IF NOT Run THEN

		Step:=0;
		StartTask:= FALSE;
		StartHome:= FALSE;
		Done := FALSE;

		(*Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iActualStepNumber + 1*);

		RETURN;

	ELSIF StartTask AND   StartHome THEN

		SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Robot Error' );

		RETURN;

	END_IF

	IF StartHome THEN
		StartHome:=FALSE;
		Done := FALSE;
		pAgent^.Run:=TRUE;
		IF  pAgent^.R_InitialFinish   THEN
			Done := TRUE;
		END_IF

	END_IF

	CASE Step OF
		0:
			MessageTimer(in:=FALSE);

			IF StartTask  THEN
				Done := FALSE;
				Abort := FALSE;
				Step:=Step+1;
			END_IF

		1: 	IF  NOT pAgent^.Ready AND NOT  pAgent^.Running   THEN
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Robot not Ready' );

			ELSIF StartTask THEN
				Step:=Step+1;
			END_IF

		2:
			pAgent^.Pos := Position;
			Timer(in:=TRUE,pt:=t#30ms);
			IF Timer.Q THEN
				Timer(in:=FALSE);
				Step:=Step+1;
			END_IF

		3:
			MessageTimer(in:=TRUE,pt:=TimeOut);

			pAgent^.Start  := TRUE;
			IF pAgent^.Done THEN
				Step:=Step+1;
			ELSIF MessageTimer.Q THEN
				MessageTimer(in:=FALSE);
				Abort :=TRUE;
				SetError(User:= User, Key:= Text.Name.DeviceInteraction, sValue01:='Timeout' );
			END_IF

		4:
			Done := TRUE;
			pAgent^.Start  := FALSE;

		ELSE

			Abort :=TRUE;
			pAgent^.Start  := FALSE;
			SetMasterError(User:= User, key:= Text.Name.ProgramError, sValue01:='MasterError' );

	END_CASE


	Stations.Data[User].Step (	User:= User,
							ExternalSign:= Done OR Abort,
							JumpAddress:= 0,
							BackStep:= 	FALSE
							 );

	IF Stations.Data[User].Step.Ack THEN

		Step:=0;
		pAgent^.Start  := FALSE;

	END_IF               [   , Џ Џ ,І           FB_SetError  -3%]	-3%]                      Ј   FUNCTION_BLOCK FB_SetError
VAR_INPUT
	User				: INT;
	Title					: STRING(40);
	Key					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VARЯ  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)


IF NOT Stations.Data[User].xMasterError AND NOT Stations.Data[User].xError THEN
	Stations.Data[User].xAutomatic:= FALSE;
	Stations.Data[User].xMessage:= FALSE;
	Stations.Data[User].xTips:= FALSE;
	Stations.Data[User].xError:= TRUE;
	Stations.Data[User].TextLine0:= Title;
	Stations.Data[User].iText:= Key;
	Stations.Data[User].sValue01:= sValue01;
END_IF

sValue01:= '';
Title:= '';               \   , d d с[           FB_SetMasterError  -3%]	-3%]                      Ў   FUNCTION_BLOCK FB_SetMasterError
VAR_INPUT
	User				: INT;
	Title					: STRING(40);
	Key					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VARж  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

IF NOT Stations.Data[User].xMasterError THEN
	Stations.Data[User].xAutomatic:= FALSE;
	Stations.Data[User].xError:= FALSE;
	Stations.Data[User].xMessage:= FALSE;
	Stations.Data[User].xMasterError:= TRUE;
	Stations.Data[User].xTips:= FALSE;
	Stations.Data[User].TextLine0:= Title;
	Stations.Data[User].iText:= Key;
	Stations.Data[User].sValue01:= sValue01;
END_IF

sValue01:= '';
Title:= '';               ]   , с с ^и           FB_SetMasterMessage -3%]	-3%]                      А   FUNCTION_BLOCK FB_SetMasterMessage
VAR_INPUT
	User				: INT;
	Title					: STRING(40);
	Key					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VAR  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

IF NOT Stations.Data[User].xMasterError AND NOT Stations.Data[User].xError AND NOT Stations.Data[User].xMasterMessage THEN
	Stations.Data[User].xMasterMessage:= TRUE;
	Stations.Data[User].TextLine0:= Title;
	Stations.Data[User].iText:= Key;
	Stations.Data[User].sValue01:= sValue01;
END_IF;

sValue01:= '';
Title:= '';               ^   ,     }ї           FB_SetMessage  -3%]	-3%]      D_TI
ND        Љ   FUNCTION_BLOCK FB_SetMessage
VAR_INPUT
	User				: INT;
	Title				: STRING(40);
	Key					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VARЌ  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

IF NOT Stations.Data[User].xMasterError
AND NOT Stations.Data[User].xError
AND NOT Stations.Data[User].xMasterMessage
AND NOT Stations.Data[User].xMessage THEN
	Stations.Data[User].xMessage:= TRUE;
	Stations.Data[User].TextLine0:= Title;
	Stations.Data[User].iText:= Key;
	Stations.Data[User].sValue01:= sValue01;
END_IF;

sValue01:= '';
Title:= '';               _   , с с ^и        
   FB_SetStep -3%]	-3%]      ENACON
        <  FUNCTION_BLOCK FB_SetStep
VAR_INPUT
	User			: INT;
	ExternalSign		: BOOL;
	JumpAddress	: INT;
	BackStep		: BOOL:=FALSE;
	Reset			: BOOL;
END_VAR

VAR_OUTPUT
	Ack				: BOOL;
	BackStepAck		: BOOL;
	Step								: INT:=0;
END_VAR

VAR
	iRequestedStepNumber	: INT;
	iActualStepNumber		: INT;

END_VAR
:  iRequestedStepNumber:=Stations.Data[User].iRequestedStepNumber;
iActualStepNumber:=Stations.Data[User].iActualStepNumber;

Stations.Data[User].Step(User:= User,
			ExternalSign:=  ExternalSign,
			JumpAddress:= JumpAddress,
			BackStep:= Reset,
			Ack=>Ack ,
			Step=>Step ,
			BackStepAck=>BackStepAck );               `   ,   Ї        
   FB_SetText  -3%]	-3%]      ACON
_I        Њ   FUNCTION_BLOCK FB_SetText
VAR_INPUT
	User				: INT;
	TextLine0			: STRING(40);
	Text					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VAR  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

IF NOT Stations.Data[User].xMasterError
AND NOT Stations.Data[User].xError
AND NOT Stations.Data[User].xMasterMessage
AND NOT Stations.Data[User].xMessage THEN
	Stations.Data[User].TextLine0:= TextLine0;
	Stations.Data[User].iText:= Text;
	Stations.Data[User].sValue01:= sValue01;
END_IF;

sValue01:= '';
TextLine0:= '';                 , Ш Ш EП        
   FB_SetTips -3%]	-3%]      STX_ofer        І   FUNCTION_BLOCK FB_SetTips
VAR_INPUT
	User				: INT;
	Title				: STRING(40);
	Key					: INT;
	sValue01			: STRING(40);
END_VAR
VAR_OUTPUT
END_VAR
VAR
END_VARЬ  (*

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

IF NOT Stations.Data[User].xMasterError
AND NOT Stations.Data[User].xError
AND NOT Stations.Data[User].xMasterMessage
AND NOT Stations.Data[User].xMessage
AND NOT Stations.Data[User].xTips THEN
	Stations.Data[User].xTips:= TRUE;
	Stations.Data[User].TextLine0:= Title;
	Stations.Data[User].iText:= Key;
	Stations.Data[User].sValue01:= sValue01;
END_IF;

sValue01:= '';
Title:= '';               a   , Џ Џ ,Р           FB_SignControl  -3%]	-3%]      NDCTN

        @  FUNCTION_BLOCK FB_SignControl
VAR_INPUT
END_VAR
VAR_OUTPUT
	xHardwareHome	: BOOL;
	xSoftwareHome	: BOOL;
	xError			: BOOL;
	xMasterError		: BOOL;
	xMessage		: BOOL;
	xTips			: BOOL;
	xMasterMessage	: BOOL;
	xCalibrate		: BOOL;
	xOff				: BOOL;
	xStartOff			: BOOL;
END_VAR
VAR
	iCounter			: INT;
END_VARR  (*
SignControl

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

xError:= FALSE;
xMasterError:= FALSE;
xMessage:= FALSE;
xMasterMessage:= FALSE;

xSoftwareHome:= TRUE;
xHardwareHome:= TRUE;
xCalibrate:= TRUE;
xOff:= TRUE;
xStartOff:= TRUE;

FOR iCounter:= 1 TO Stations.MaxUser DO

	IF iCounter >= Stations.StartStations THEN
		IF NOT Stations.Data[iCounter].xSoftwareHome THEN xSoftwareHome:= FALSE; END_IF;
		IF NOT Stations.Data[iCounter].xHardwareHome THEN xHardwareHome:= FALSE; END_IF;
		IF Stations.Data[iCounter].xOn THEN xOff:= FALSE; END_IF;
		IF NOT Stations.Data[iCounter].xCalibrate THEN xCalibrate:= FALSE; END_IF;
		IF Stations.Data[iCounter].xStart THEN xStartOff:= FALSE; END_IF;
	END_IF;

	IF Stations.Data[iCounter].xError THEN xError:= TRUE; END_IF;
	IF Stations.Data[iCounter].xMasterError THEN xMasterError:= TRUE; END_IF;

	IF Stations.Data[iCounter].xMessage THEN xMessage:= TRUE; END_IF;
	IF Stations.Data[iCounter].xMasterMessage THEN xMasterMessage:= TRUE; END_IF;
	IF Stations.Data[iCounter].xTips THEN xTips:= TRUE; END_IF;

END_FOR;               r   ,     }ї           FB_Station_01 !хT]	-3%]      

D_
          FUNCTION_BLOCK FB_Station_01
VAR_INPUT
	Me							: INT;
	Master						: INT;

	xPressButton_PrintLabel			: BOOL;
	Sz13a_S1_Air_Supply_Connector	: BOOL;
	Sz13b_S1_Air_Supply_Connector	: BOOL;
	Sz18a_S1_Box_Illumination			: BOOL;
	Sz18b_S1_Box_Illumination			: BOOL;
	Sz16b_S1_Red_Box_Front_Lock	: BOOL;
	Sz16b_S1_Red_Box_Product_in 	: BOOL;
	Sz17a_S1_Red_Box_Big_Lock		: BOOL;
	Sz17b_S1_Red_Box_Big_Lock		: BOOL;
	S_S1_Fixture_Presence			: BOOL;
	S_S1_DUT_Presence				: BOOL;
	S_S1_Adapter_Presence			: BOOL;
	S_S1_Airbag_Cable_Presence		: BOOL;
	S_S1_Red_Box_Close				: BOOL;


	pRequest					: POINTER TO StructRequestAction;
	pResponse					: POINTER TO StructResponseAction;

	pRequestFinished				: POINTER TO StructRequestAction;
	pResponseFinished			: POINTER TO StructResponseAction;

	pScanner					: POINTER TO StructDeviceInteraction;
END_VAR

VAR_OUTPUT

	z13a_Cylinder_S1_Air_Supply_Connector		: BOOL;
	z13b_Cylinder_S1_Air_Supply_Connector		: BOOL;
	z14b_Cylinder_S1_Fix						: BOOL;
	z15b_Cylinder_S1_Mark					: BOOL;
	z16b_Cylinder_S1_Red_Box_Front_Lock		: BOOL;
	z17b_Cylinder_S1_Red_Box_Big_Lock		: BOOL;
	z18a_Cylinder_S1_Box_Illumination			: BOOL;
	z18b_Cylinder_S1_Box_Illumination			: BOOL;

	xLineLight								: BOOL;
	xCircleLight								: BOOL;
	xBoxLight								: BOOL;
END_VAR

VAR
	WaitTime					: FB_WaitTime;
	MessageTime				: TON;
	RedboxTime					: TON;

	TriggerRedBox				: R_TRIG;
	Trigger_xPrintLabel			: R_TRIG;


	mZ13_Cylinder_S1_Air_Supply_Connector	:Cylinder_V2;
	mZ18_Cylinder_S1_Box_Illumination			:Cylinder_V2;
	mZ09_Cylinder_S1_Red_Box_Big_Lock		:Cylinder_V2;(*backup*)

	MultiCylinder					: FB_MultiCylinder;
	Status_S1_Red_Box_Big_Lock	: BOOL;

	EmptyDeviceInteraction	: StructDeviceInteraction;
	LasControl				: FB_LasControl;
	StationCfg				:StructStationCfg;

	Scanner					: StructDeviceInteraction;
END_VAR

VAR CONSTANT

END_VAR

У"  (*
Build		: 04.03.2005
Version		: 2.0

Description	:

*)
(* Las *)

LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.Reset,pAgent:=ADR(StationCfg),LightCurtainControl:=TRUE);(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;

(*Local*)
AddressSettings();
DeviceDefine();
SafePositionDefine();

(*Message*)
GlobalMasterError();
GlobalError();
GlobalMessage();

CASE  StationCfg.Data^.iActualStepNumber OF

	(*	===========================================================================	*)
	(*	Initialization and Calibration	*)
	(*	===========================================================================	*)
	0:
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   	a_Off ();
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffStart );(*Don't Change*)
	1:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	2:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	3: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	4:	LasControl.NOP(Me:= Me, BackEnable:= FALSE,);
	5:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	6: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	7: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	8:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CalibrateEnd );(*Don't Change*)

	(*	===========================================================================	*)
	(*	Software Home 	*)
	(*	===========================================================================	*)
	30:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.SoftwareHome  );(*Don't Change*)
	31: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckTableDone);(*Don't Change*)
		(*Empyt iAddressProcess(Step:=100)  Equal  iAddressFinal(Step:=500) Fail iAddressFinalFail(Step:=700)*)
	32: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ST1CheckStationDestination,S_DUT_Presence:=S_S1_DUT_Presence);

	(*	===========================================================================	*)
	(*	Main Process 	*)
	(*	===========================================================================	*)
  	100:
		LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckClearMode,S_DUT_Presence:=S_S1_DUT_Presence );(*Don't Change*)
		IF NOT StationCfg.Data^.xLastCycle THEN   a_CheckStartCondition(); END_IF
	101: LasControl.ADS_LasScannerSr752(User:=Me, StartTask:=TRUE, RequestType := LasRequestTypes.DoQuery1,pAgent:=pScanner);
		(*Success Step=Step+1  Fail iAddressFinalFail(Step:=700)*)
	102: LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckScanResult);(*Don't Change*)
	103: 	LasControl.ADS_LasGetNewPart(User:=Me, WT:=1, StartTask:=TRUE, S_DUT_Presence:=S_S1_DUT_Presence);
	104:	LasControl.ADS_LasGetNewPart(User:=Me, WT:=1, StartTask:=FALSE, UpdateStep:=FALSE);
		LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckClearMode,S_DUT_Presence:=S_S1_DUT_Presence );(*Don't Change*)
		IF NOT StationCfg.Data^.xLastCycle THEN   a_CheckStartCondition(); END_IF
	105: LasControl.ADS_LasScannerSr752(User:=Me, StartTask:=TRUE, RequestType := LasRequestTypes.DoQuery2,pAgent:=pScanner);
		(*Success Step=Step+1  Fail iAddressFinalFail(Step:=700)*)
	106: LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckScanResult );(*Don't Change*)
	107: 	LasControl.ADS_LasGetNewPart(User:=Me, WT:=2, StartTask:=TRUE, S_DUT_Presence:=S_S1_DUT_Presence);
	108:	LasControl.ADS_LasGetNewPart(User:=Me, WT:=2, StartTask:=FALSE, UpdateStep:=FALSE);
		LasControl.LasMethControl(User:=me,LasType:=LasMethType.GotoTest );(*Don't Change*)


	(*	===========================================================================	*)
	(*	EOL Test *)
	(*	===========================================================================	*)
	150: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	151:  ADS_MechanicInteraction();(*Left Carrier*)
		LasControl.EOLTest(User:=me,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=TRUE,pRequest:=pRequest,pResponse:=pResponse);
    		(*Pass  iAddressPass(Step:=200) Fail iAddressFail(Step:=300)  *)
	152: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	153: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	154:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	155: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckResult);

	(*	===========================================================================	*)
	(*	Process Pass	*)
	(*	===========================================================================	*)
	200:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	201:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	202:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	203: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessPass);

	(*	===========================================================================	*)
	(*	Process Fail	*)
	(*	===========================================================================	*)
	300:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	301:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	302:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	303: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessFail);

	(*	===========================================================================	*)
	(*	Process Release	*)
	(*	===========================================================================	*)
	400: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	401: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	402:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (*Equal  Station and Result=True  iAddressProcess(Step:=100) Result=False iAddressFinalFail(Step:=700)   not Equal  iAddressSoftwareHome(Step:=30)*)
	403:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ST1ProcessRelease);

	(*	===========================================================================	*)
	(*	Final Process 	*)
	(*	===========================================================================	*)

	500: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	501: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	502: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	503:  ADS_MechanicInteraction();
		LasControl.EOLTest(User:=me,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=FALSE,pRequest:=pRequest,pResponse:=pResponse);
	504:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	505:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	506:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	507: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalCheckResult );


	(*	===========================================================================	*)
	(*	Final Process Pass	*)
	(*	===========================================================================	*)
	600: LasControl.ADS_LasScannerSr752(User:=Me, StartTask:=TRUE, RequestType := LasRequestTypes.DoCompare1,pAgent:=pScanner);
	601: LasControl.ADS_LasScannerSr752(User:=Me, StartTask:=TRUE, RequestType := LasRequestTypes.DoCompare2,pAgent:=pScanner);
	602: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalCheckPassFail);
	603:	LasControl.ADS_LasFinishedPart(User:=me,StartTask:=TRUE,Complete:=TRUE,PositiveAction:=StationCfg.Data^.pWT^.bulTestResult, UpdateTarget:=FALSE,pRequest:=pRequestFinished,pResponse:=pResponseFinished);
	604: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalCheckPassFail );
	605:	a_MarkPoint();
	606:	a_RemoveOKPart();
	607: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalProcessPass);

	(*	===========================================================================	*)
	(*	Final Process Fail 	*)
	(*	===========================================================================	*)
	700:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	701:	LasControl.ADS_LasFinishedPart(User:=me,StartTask:=TRUE,Complete:=TRUE,PositiveAction:=FALSE,UpdateTarget:=FALSE,Complete:=TRUE,pRequest:=pRequestFinished,pResponse:=pResponseFinished);
	702: a_RedBoxPressRedButton();
	703: 	a_RedBoxRemoveNGPart();
	704:	a_RedBoxOpen();
	705:	a_RedBoxPutPart();
	706:	a_RedBoxClose();
	707: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalProcessFail);

	(*	===========================================================================	*)
	(*	Final Process Post*)
	(*	===========================================================================	*)
	800:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	801: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	802:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.UpdateRefrencePart);
	803: LasControl.LasMethControl(User:=me,LasType:=LasMethType.FinalProcessRelease);

ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE;

LasControl.LasMethControl(User:=me,LasType:=LasMethType.End);(*Don't Change*) s   , њ њ w           a_CheckStartCondition -3%]Ь  IF  S_S1_DUT_Presence AND S_S1_Adapter_Presence  AND NOT Stations.Data[Master].xLightCurtainRelay THEN
	LasControl.SetStep(User:= Me,  ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= FALSE);
ELSIF NOT S_S1_DUT_Presence THEN
	z14b_Cylinder_S1_Fix := TRUE;
	KeyBoard.xButtonLight_StartIndicator :=FALSE;
	LasControl.SetMessage(User:= Me, Key:= Text.Name.PleaseInsertPart, sValue01:= '');
ELSIF NOT S_S1_Adapter_Presence THEN
	KeyBoard.xButtonLight_StartIndicator :=FALSE;
	LasControl.SetMessage(User:= Me, Key:= Text.Name.PleaseInsertAdapter, sValue01:= '');
ELSIF Stations.Data[Master].xLightCurtainRelay THEN
	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.LightCurtain_On, sValue01:= '');
END_IFt   , d d сu           a_MarkPoint -3%]Ь   z15b_Cylinder_S1_Mark:=TRUE;

WaitTime(User:= Me, Start:= TRUE, WaitTimeValue:= T#500ms, JumpAddress:= 0, BackStep:= TRUE);

IF Stations.Data[Me].Step.Ack THEN
	z15b_Cylinder_S1_Mark:=FALSE;
END_IF;u   , K K Ш\           a_Off -3%]J  (* Step - Off *)


z14b_Cylinder_S1_Fix := FALSE;
z15b_Cylinder_S1_Mark := FALSE;
z16b_Cylinder_S1_Red_Box_Front_Lock := TRUE;
z17b_Cylinder_S1_Red_Box_Big_Lock := FALSE;


mZ13_Cylinder_S1_Air_Supply_Connector	( xInit:= TRUE, BackEnable:= TRUE );
mZ18_Cylinder_S1_Box_Illumination			( xInit:= TRUE, BackEnable:= TRUE );v   ,   *           a_RedBoxClose -3%]@  LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleaseCloseRedBox, sValue01:= '');

LasControl.SetStep(User:= Me,
					ExternalSign:= Sz16b_S1_Red_Box_Front_Lock,
					JumpAddress:= 0,
					BackStep:= FALSE,
					);

IF Stations.Data[Me].Step.Ack THEN
	z16b_Cylinder_S1_Red_Box_Front_Lock:=TRUE;
END_IF;w   , 2 2 ЏC           a_RedBoxOpen -3%]  z16b_Cylinder_S1_Red_Box_Front_Lock:=FALSE;

LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleaseOpenRedBox, sValue01:= '');

IF NOT Sz16b_S1_Red_Box_Front_Lock THEN

	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= FALSE);



END_IFx   , d d с[           a_RedBoxPressRedButton -3%]o  KeyBoard.xButtonLight_RedIndicator:= TRUE;
LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleasePressRedButton, sValue01:= '');

LasControl.SetStep(User:= Me,
					ExternalSign:= KeyBoard.Key.Sign.xStopOnly,
					JumpAddress:= 0,
					BackStep:= FALSE,
					);

IF StationCfg.Data^.Step.Ack THEN
	KeyBoard.xButtonLight_RedIndicator:= FALSE;
END_IF;y   ,     }           a_RedBoxPutPart -3%]ч   LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleasePutPartToRedBox, sValue01:= '');
IF Sz16b_S1_Red_Box_Product_in THEN

	LasControl.SetStep(User:= Me, ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= FALSE);

END_IFz   , с с ^и           a_RedBoxRemoveNGPart -3%]s  KeyBoard.xButtonLight_RedIndicator:= FALSE;
z14b_Cylinder_S1_Fix := TRUE;

LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.RemoveNio, sValue01:= '');

LasControl.SetStep(User:= Me,
					ExternalSign:= NOT S_S1_DUT_Presence,
					JumpAddress:= 0,
					BackStep:= FALSE,
					);

IF StationCfg.Data^.Step.Ack THEN
	;(*xButtonLight_Red:= FALSE;*)
END_IF;{   , Џ Џ ,І           a_RemoveOKPart -3%]1  LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.RemoveOKPart, sValue01:= '');

z14b_Cylinder_S1_Fix := TRUE;

LasControl.SetStep(User:= Me, ExternalSign:= NOT S_S1_DUT_Presence, JumpAddress:= 0, BackStep:= FALSE);


IF StationCfg.Data^.Step.Ack THEN
	;(*xButtonLight_Red:= FALSE;*)
END_IF;|   ,     }           AddressSettings РтT]Ч  (* Settings of Work addresses *)

StationCfg.Data^.iAddressSoftwareHome:= 30;

StationCfg.Data^.iAddressProcess := 100;
StationCfg.Data^.iAddressRedoTest := 150;
StationCfg.Data^.iAddressPass := 200;
StationCfg.Data^.iAddressFail:= 300;
StationCfg.Data^.iAddressRelease:= 400;

StationCfg.Data^.iAddressFinal:= 500;
StationCfg.Data^.iAddressFinalPass:= 600;
StationCfg.Data^.iAddressFinalFail:= 700;
StationCfg.Data^.iAddressFinalPost:=800;
}   , с с ^и           ADS_MechanicInteraction -3%]   ;~   , с с ^ђ           DeviceDefine -3%]	  (* Define Device and actualize In/Out Parameter *)

mZ13_Cylinder_S1_Air_Supply_Connector	( User:= Me, xSet:= TRUE, xInit:= FALSE,
			D_Enable:= TRUE, D_Text:= Text.Name.SensorFail, D_Value:= ' SZ13-13a/b',
			T_Enable:= TRUE, T_Text:= Text.Name.SensorFail, T_ValueToA:= ' SZ13-13a', T_ValueToB:= ' SZ13-13b',
			ConditionToA:= Sz13a_S1_Air_Supply_Connector,
			ConditionToB:= Sz13b_S1_Air_Supply_Connector,
			ChannelA=> z13a_Cylinder_S1_Air_Supply_Connector,
			ChannelB=> z13b_Cylinder_S1_Air_Supply_Connector  );

mZ18_Cylinder_S1_Box_Illumination	( User:= Me, xSet:= TRUE, xInit:= FALSE,
			D_Enable:= FALSE, D_Text:= Text.Name.SensorFail, D_Value:= ' SZ18-18a/b',
			T_Enable:= FALSE, T_Text:= Text.Name.SensorFail, T_ValueToA:= ' SZ18-18a', T_ValueToB:= ' SZ18-18b',
			ConditionTime:=T#1s,
			ChannelA=> z18a_Cylinder_S1_Box_Illumination,
			ChannelB=>   );



(*
EOL_S1_Light_Circle:=						EL_DO[3,0].0;
EOL_S1_Light_Line:=						EL_DO[3,0].1;
EOL_S1_Light_Box:=						EL_DO[3,0].2;

*)

Trigger_xPrintLabel(CLK:=xPressButton_PrintLabel);

IF ADS_stuPrinterSt01.stuPlcArticleSet.strKostalNr  = PC_stuCurrentVariantInfo.strKostalNr THEN
;
ELSE
	ADS_stuPrinterSt01.stuPlcArticleSet :=PC_stuCurrentVariantInfo;
END_IF

IF Trigger_xPrintLabel.Q AND NOT  ADS_stuPrinterSt01.bulAdsActionIsPass AND NOT  ADS_stuPrinterSt01.bulAdsActionIsFail  THEN
	ADS_stuPrinterSt01.bulPlcDoAction:=TRUE;
END_IF
IF ADS_stuPrinterSt01.bulAdsActionIsPass OR ADS_stuPrinterSt01.bulAdsActionIsFail THEN
	ADS_stuPrinterSt01:=EmptyDeviceInteraction;
	ADS_stuPrinterSt01.stuPlcArticleSet :=PC_stuCurrentVariantInfo;
END_IF

(*
ADS_stuPrinterSt01.bulPlcDoAction :=Trigger_xPrintLabel.Q;;
*)



IF StationCfg.Data^.pWT =0 THEN RETURN; END_IF;
(*ADS_LasFinishedPart.PositiveAction :=StationCfg.Data^.pWT^.bulTestResult;*)


RedboxTime(In:=S_S1_Red_Box_Close,pt:=t#100ms);


IF KeyBoard.Key.Sign.xManual THEN
	z17b_Cylinder_S1_Red_Box_Big_Lock:= NOT HMI_ShortcutButton[1];
	IF NOT S_S1_Red_Box_Close THEN
		LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleaseCloseRedBox, sValue01:= '');
	END_IF

ELSIF KeyBoard.Key.Sign.xAutomatic AND NOT HMI_ShortcutButton[1] THEN
	IF RedboxTime.Q   THEN
		RedboxTime.IN :=FALSE;
		z17b_Cylinder_S1_Red_Box_Big_Lock:= NOT S_S1_Red_Box_Close;
	ELSE
		LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.PleaseCloseRedBox, sValue01:= '');
	END_IF

END_IF   , K K ШB           GlobalError -3%]    (* Define of Global Errors *)
;   , d d с[           GlobalMasterError -3%])   (* Define of Global Master Errors *)

;   , } } њt           GlobalMessage -3%]$   (* Define of Global Messages *)

;   , Ш Ш EП           SafePositionDefine -3%]   StationCfg.Data^.xSafePosition:=	Stations.Data[Master].xSafePosition AND
								Sz13a_S1_Air_Supply_Connector AND
								Sz18a_S1_Box_Illumination;                , њ њ wё           FB_Station_02 -3%]	-3%]      
AIO
F        s  FUNCTION_BLOCK FB_Station_02
VAR_INPUT
	Me							: INT;
	Master						: INT;

	Sz01a_S2_Stator_Right_Connector	:BOOL;
	Sz01b_S2_Stator_Right_Connector	:BOOL;

	pRequest					: POINTER TO StructRequestAction;
	pResponse					: POINTER TO StructResponseAction;

END_VAR

VAR_OUTPUT
	
	z01a_Cylinder_S2_Stator_Right_Connector	:BOOL;
	z01b_Cylinder_S2_Stator_Right_Connector	:BOOL;


END_VAR

VAR
	MultiCylinder								: FB_MultiCylinder;
	mZ01_Cylinder_S3_Stator_Right_Connector	:Cylinder_V2;

	LasControl								: FB_LasControl;
	StationCfg								:StructStationCfg;
END_VAR

VAR CONSTANT

END_VAR

w  (*
Build		: 04.03.2005
Version		: 2.0

Description	:

*)

(* Las *)
LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.Reset,pAgent:=ADR(StationCfg));(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;
AddressSettings();

(*Local*)
DeviceDefine();
SafePositionDefine();

(*Message*)
GlobalMasterError();
GlobalError();
GlobalMessage();

CASE  StationCfg.Data^.iActualStepNumber OF
	(*	===========================================================================	*)
	(*	Initialization and Calibration	*)
	(*	===========================================================================	*)
	0:
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   	a_Off ();
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffStart );(*Don't Change*)
	1:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	2:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	3:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	4: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	5: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	6: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	7: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	8:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	9:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CalibrateEnd );(*Don't Change*)

	(*	===========================================================================	*)
	(*	Software Home 	*)
	(*	===========================================================================	*)
	30:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.SoftwareHome  );(*Don't Change*)
	31: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckTableDone);(*Don't Change*)
		(*Equal  iAddressProcess(Step:=100) not Equal  iAddressRelease(Step:=400)*)
	32: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckStationDestination);(*Don't Change*)

	(*	===========================================================================	*)
	(*	Main Process 	*)
	(*	===========================================================================	*)
	100: LasControl.NOP(Me:= Me, BackEnable:= FALSE);(*mZ06_Cylinder_S3_Rotor_Connector( Direction:= TRUE );*)
 	101:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);(*mZ04_Cylinder_S3_Rotor_Unlock( Direction:= TRUE );*)
	102:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	103: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	104:  ADS_MechanicInteraction();(*Left Carrier*)
		LasControl.EOLTest(User:=me,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=TRUE,pRequest:=pRequest,pResponse:=pResponse);
	105:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	106:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	107: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	108: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	109: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	110: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	    (*Pass  iAddressPass(Step:=200) Fail iAddressFail(Step:=300)  *)
	111: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckResult);


	(*	===========================================================================	*)
	(*	Process Pass	*)
	(*	===========================================================================	*)
	200:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	201:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	202:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	203: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessPass);


	(*	===========================================================================	*)
	(*	Process Fail	*)
	(*	===========================================================================	*)
	300:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	301:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	302:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	303: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessFail);

	(*	===========================================================================	*)
	(*	Process Release	*)
	(*	===========================================================================	*)
	400: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	401: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	402:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (*Equal  Station iAddressProcess(Step:=100) not Equal  iAddressSoftwareHome(Step:=30)*)
	403:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessRelease);

ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE;

LasControl.LasMethControl(User:=me,LasType:=LasMethType.End);(*Don't Change*)    , } } њt           a_Off -3%]   (* Step - Off *)
;   , 2 2 Џ)           AddressSettings -3%]э   (* Settings of Work addresses *)
StationCfg.Data^.iAddressSoftwareHome:= 30;
StationCfg.Data^.iAddressProcess := 100;
StationCfg.Data^.iAddressPass := 200;
StationCfg.Data^.iAddressFail:= 300;
StationCfg.Data^.iAddressRelease:= 400;   , K K ШB           ADS_MechanicInteraction -3%]   ;   , Ш Ш EП           DeviceDefine -3%]9   (* Define Device and actualize In/Out Parameter *)


;     3,.2
)           GlobalError -3%]    (* Define of Global Errors *)
;     rKtar EN           GlobalMasterError -3%])   (* Define of Global Master Errors *)

;     1.lAAcon           GlobalMessage -3%]$   (* Define of Global Messages *)

;     dstiIsss           SafePositionDefine -3%]r   StationCfg.Data^.xSafePosition:=	Stations.Data[Master].xSafePosition AND
								Sz01a_S2_Stator_Right_Connector;                , с с ^и           FB_Station_03 -3%]	-3%]      ON
ENIF          FUNCTION_BLOCK FB_Station_03
VAR_INPUT
	Me								: INT;
	Master							: INT;

	Sz01a_S3_Stator_Right_Connector	:BOOL;
	Sz01b_S3_Stator_Right_Connector	:BOOL;
	pRequest					         : POINTER TO StructRequestAction;
	pResponse					         : POINTER TO StructResponseAction;

END_VAR

VAR_OUTPUT

	z01a_Cylinder_S3_Stator_Right_Connector	:BOOL;
	z01b_Cylinder_S3_Stator_Right_Connector	:BOOL;

END_VAR

VAR
	MultiCylinder								: FB_MultiCylinder;
	mZ01_Cylinder_S3_Stator_Right_Connector	:Cylinder_V2;
	LasControl								: FB_LasControl;
	StationCfg								:StructStationCfg;
END_VAR

VAR CONSTANT

END_VAR

f  (*
Build		: 04.03.2005
Version		: 2.0

Description	:

*)

(* Las *)
LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.Reset,pAgent:=ADR(StationCfg));(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;
AddressSettings();


(*Local*)
DeviceDefine();
SafePositionDefine();

(*Message*)
GlobalMasterError();
GlobalError();
GlobalMessage();


CASE  StationCfg.Data^.iActualStepNumber OF

	(*	===========================================================================	*)
	(*	Initialization and Calibration	*)
	(*	===========================================================================	*)
	0:
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   	a_Off ();
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffStart );(*Don't Change*)
	1:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	2:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	3:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	4: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	5: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	6: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	7: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	8:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	9:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CalibrateEnd );(*Don't Change*)

	(*	===========================================================================	*)
	(*	Software Home 	*)
	(*	===========================================================================	*)
	30:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.SoftwareHome  );(*Don't Change*)
	31: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckTableDone);(*Don't Change*)
		(*Equal  iAddressProcess(Step:=100) not Equal  iAddressRelease(Step:=400)*)
	32: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckStationDestination);(*Don't Change*)

	(*	===========================================================================	*)
	(*	Main Process 	*)
	(*	===========================================================================	*)
	100: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
 	101:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);(*mZ04_Cylinder_S3_Rotor_Unlock( Direction:= TRUE );*)
	102:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	103: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	104:  ADS_MechanicInteraction();
		LasControl.EOLTest(User:=me,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=TRUE,pRequest:=pRequest,pResponse:=pResponse);
	105: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	106:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	107:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	108: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	109: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	110: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	111: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	    (*Pass  iAddressPass(Step:=200) Fail iAddressFail(Step:=300)  *)
	112: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckResult);

	(*	===========================================================================	*)
	(*	Process Pass	*)
	(*	===========================================================================	*)
	200:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	201:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	202:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	203: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessPass);


	(*	===========================================================================	*)
	(*	Process Fail	*)
	(*	===========================================================================	*)
	300:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	301:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	302:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	303: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessFail);

	(*	===========================================================================	*)
	(*	Process Release	*)
	(*	===========================================================================	*)
	400: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	401: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	402:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (*Equal  Station iAddressProcess(Step:=100) not Equal  iAddressSoftwareHome(Step:=30)*)
	403:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessRelease);

ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE;

LasControl.LasMethControl(User:=me,LasType:=LasMethType.End);(*Don't Change*)    , K K ШB           a_Off -3%]   (* Step - Off *)
;   , } } њ           AddressSettings -3%]э   (* Settings of Work addresses *)
StationCfg.Data^.iAddressSoftwareHome:= 30;
StationCfg.Data^.iAddressProcess := 100;
StationCfg.Data^.iAddressPass := 200;
StationCfg.Data^.iAddressFail:= 300;
StationCfg.Data^.iAddressRelease:= 400;   , Џ Џ ,І           ADS_MechanicInteraction -3%]   ;    ,   Ї           DeviceDefine -3%]9   (* Define Device and actualize In/Out Parameter *)


;Ё   , } } њt           GlobalError -3%]    (* Define of Global Errors *)
;Ђ   , K K ШB           GlobalMasterError -3%])   (* Define of Global Master Errors *)

;Ѓ   , 2 2 Џ)           GlobalMessage -3%]$   (* Define of Global Messages *)

;Є   , Џ Џ ,Р           SafePositionDefine -3%]s   StationCfg.Data^.xSafePosition:=	Stations.Data[Master].xSafePosition AND
								Sz01a_S3_Stator_Right_Connector ;               , 2 2 ЏC           FB_Station_04 -3%]	-3%]        r q           o  FUNCTION_BLOCK FB_Station_04
VAR_INPUT
	Me							: INT;
	Master						: INT;

	Sz01a_S3_Stator_Right_Connector	:BOOL;
	Sz01b_S3_Stator_Right_Connector	:BOOL;
	pRequest					: POINTER TO StructRequestAction;
	pResponse					: POINTER TO StructResponseAction;

END_VAR

VAR_OUTPUT
	
	z01a_Cylinder_S3_Stator_Right_Connector	:BOOL;
	z01b_Cylinder_S3_Stator_Right_Connector	:BOOL;

END_VAR

VAR
	MultiCylinder								: FB_MultiCylinder;
	mZ01_Cylinder_S3_Stator_Right_Connector	:Cylinder_V2;
	LasControl								: FB_LasControl;
	StationCfg								:StructStationCfg;

END_VAR

VAR CONSTANT

END_VAR

  (*
Build		: 04.03.2005
Version		: 2.0

Description	:

*)

(* Las *)
LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.Reset,pAgent:=ADR(StationCfg));(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;
AddressSettings();


(*Local*)
DeviceDefine();
SafePositionDefine();

(*Message*)
GlobalMasterError();
GlobalError();
GlobalMessage();


CASE  StationCfg.Data^.iActualStepNumber OF

	(*	===========================================================================	*)
	(*	Initialization and Calibration	*)
	(*	===========================================================================	*)
	0:
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   	a_Off ();
	   	LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffStart );(*Don't Change*)
	1:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	2:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	3:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	4: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	5: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	6: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	7: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	8:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	9:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CalibrateEnd );(*Don't Change*)

	(*	===========================================================================	*)
	(*	Software Home 	*)
	(*	===========================================================================	*)
	30:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.SoftwareHome  );(*Don't Change*)
	31: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckTableDone);(*Don't Change*)
		(*Equal  iAddressProcess(Step:=100) not Equal  iAddressRelease(Step:=400)*)
	32: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckStationDestination);(*Don't Change*)

	(*	===========================================================================	*)
	(*	Main Process 	*)
	(*	===========================================================================	*)
	100: LasControl.NOP(Me:= Me, BackEnable:= FALSE);(*mZ06_Cylinder_S3_Rotor_Connector( Direction:= TRUE );*)
 	101:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);(*mZ04_Cylinder_S3_Rotor_Unlock( Direction:= TRUE );*)
	102:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	103: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	104:  ADS_MechanicInteraction();
		LasControl.EOLTest(User:=me,StartTask:=TRUE,PositiveAction:=TRUE,UpdateTarget:=TRUE,pRequest:=pRequest,pResponse:=pResponse);
	105: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	106:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	107:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	108: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	109: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	110: LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	111: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	    (*Pass  iAddressPass(Step:=200) Fail iAddressFail(Step:=300)  *)
	112: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.CheckResult);

	(*	===========================================================================	*)
	(*	Process Pass	*)
	(*	===========================================================================	*)
	200:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	201:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	202:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	203: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessPass);


	(*	===========================================================================	*)
	(*	Process Fail	*)
	(*	===========================================================================	*)
	300:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	301:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	302:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (* iAddressRelease(Step:=900)*)
	303: 	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessFail);

	(*	===========================================================================	*)
	(*	Process Release	*)
	(*	===========================================================================	*)
	400: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	401: 	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	402:	LasControl.NOP(Me:= Me, BackEnable:= FALSE);
	         (*Equal  Station iAddressProcess(Step:=100) not Equal  iAddressSoftwareHome(Step:=30)*)
	403:	LasControl.LasMethControl(User:=me,LasType:=LasMethType.ProcessRelease);

ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE;

LasControl.LasMethControl(User:=me,LasType:=LasMethType.End);(*Don't Change*)     =mLayp=L           a_Off -3%]   (* Step - Off *)
;    =mLayp=L           AddressSettings -3%]э   (* Settings of Work addresses *)
StationCfg.Data^.iAddressSoftwareHome:= 30;
StationCfg.Data^.iAddressProcess := 100;
StationCfg.Data^.iAddressPass := 200;
StationCfg.Data^.iAddressFail:= 300;
StationCfg.Data^.iAddressRelease:= 400;      Z )              ADS_MechanicInteraction -3%]   ;     §џ             DeviceDefine -3%]9   (* Define Device and actualize In/Out Parameter *)


;    (юџџ              GlobalError -3%]    (* Define of Global Errors *)
;    (юџџ              GlobalMasterError -3%])   (* Define of Global Master Errors *)

;    (юџџ              GlobalMessage -3%]$   (* Define of Global Messages *)

;    (юџџ              SafePositionDefine -3%]s   StationCfg.Data^.xSafePosition:=	Stations.Data[Master].xSafePosition AND
								Sz01a_S3_Stator_Right_Connector ;             Ѕ   , Ш Ш Eй           FB_StepControl -3%]	-3%]      FAE;ENFU        H  FUNCTION_BLOCK FB_StepControl
VAR_INPUT
	User			: INT;
	ExternalSign		: BOOL;
	ExternalNoContinue	: BOOL;
	JumpAddress	: INT;
	BackStep		: BOOL;	(* If False (default) Backstep is denied *)
	Reset			: BOOL;
END_VAR

VAR_OUTPUT
	Ack				: BOOL;
	BackStepAck		: BOOL;
	Step								: INT:=0;
END_VAR

VAR

END_VAR
  Ack:= FALSE;
BackStepAck:= FALSE;
Step:=0;
IF Reset THEN
	ExternalNoContinue:=FALSE;
	Reset:= FALSE;
	Step:=1;
	RETURN;
END_IF;


IF  ExternalSign THEN
	IF Stations.Data[User].xAutomatic OR (KeyBoard.Key.Pulse.xPlus AND Stations.Data[User].xManual AND SignControl.xError )THEN
		Step:=2;
		IF JumpAddress = 0 THEN
			Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iRequestedStepNumber + 1;
			Step:=3;
			Ack:= TRUE;
		ELSE
			Step:=4;
			Stations.Data[User].iRequestedStepNumber:= JumpAddress;
			Ack:= TRUE;
		END_IF;
	ELSIF BackStep AND KeyBoard.Key.Pulse.xMinus AND Stations.Data[User].xManual AND SignControl.xError THEN
			Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iRequestedStepNumber - 1;
			(* Ack:= TRUE; *)
			Step:=5;
			BackStepAck:= TRUE;
	END_IF;
END_IF;


IF ExternalNoContinue THEN
	Stations.Data[User].iRequestedStepNumber:= Stations.Data[User].iRequestedStepNumber + 1;
	Step:=6;
END_IF


ExternalNoContinue:=FALSE;
JumpAddress:= 0;
BackStep:= FALSE;               X  ,     }ї           FB_Table -3%]	-3%]      	CErr_
          FUNCTION_BLOCK FB_Table
VAR_INPUT
	Me						: INT;
	Master					: INT;

	FromUser				: INT;
	ToUser					: INT;

	S_0bit					: BOOL;
	S_1bit					: BOOL;
	S_2bit					: BOOL;
	S_3bit					: BOOL;
	S_4bit					: BOOL;

	TableinPosi				: BOOL;
	ReadytoStart				: BOOL;
	Automatic				: BOOL;
	Alarm_TimeOut			: BOOL;
	Alarm_Overrun			: BOOL;
	Alarm					: BOOL;
	ClockwiseDirection		: BOOL;

END_VAR

VAR_OUTPUT
	Direction					: BOOL;
	Start_level				: BOOL;
	Start_edge				: BOOL;
	SoftwareEnable			: BOOL;
	SpecialMode				: BOOL;
	ParamSet3				: BOOL;
	ParamSet2				: BOOL;
	AlarmReset				: BOOL;
	Relay					 :BOOL;
	RoundCounter			: DINT;
END_VAR

VAR
	WaitTime					: FB_WaitTime;
	StartTime					: TON;
	MoveTime					: TON;
	RunOverTime				: TON;
	TablePosTimer				: TON;
	ResetAlarm					: TON;
	StartPulse					: R_TRIG;
	QuittPulse					: R_TRIG;
	OffPulse						: BOOL;

	PhysicalPosition				: BYTE;
	TheoreticalPosition			: BYTE;

	ActualStepNumber			: INT;
	ReturnAddress				: INT;

	MoveTable					: BOOL;

	MeetConditions				: BOOL;
	SafePositions					: BOOL;

	iCounter						: INT;
	iCarrierNr					: INT;
	MaxUserCount				: INT;
	LasControl				: FB_LasControl;
	StationCfg				:StructStationCfg;
END_VAR

VAR CONSTANT

END_VAR

  (*
Build		: 2018-08-07
Version		: 1.0.1.0
Author 		: Wang Yumin
Description	: Build for rotating multi-station table, suits for any numbers of station

*)

(* Settings *)
LasControl();
LasControl.LasMethControl(User:=me,LasType:=LasMethType.TableReset, pAgent:=ADR(StationCfg));(*Don't Change*)
IF NOT  LasControl.LasMethControl.Done THEN RETURN; END_IF;

AddressSettings();


DeviceDefine();
GlobalMasterError();
GlobalError();
GlobalMessage();

StartDefine();
CASE   StationCfg.Data^.iActualStepNumber  OF

	(*	===========================================================================	*)
	(*	Initialization and Calibration	*)
	(*	===========================================================================	*)
	0:
	   LasControl.LasMethControl(User:=me,LasType:=LasMethType.Off );(*Don't Change*)
	   a_Off ();
	   LasControl.LasMethControl(User:=me,LasType:=LasMethType.OffStart );(*Don't Change*)
	1:  a_On();
	2:  a_BigtableCalibrate();
	3:  a_BigtableCalibrateStop();
	4:  a_ShiftDataAddress();
	5:  a_CalibrateEnd();


	(*	===========================================================================	*)
	(*	Software Home 	*)
	(*	===========================================================================	*)
	30: a_SoftwareHome();
	31: a_SignalMeet();
	32: a_MoveTable();
	33: a_StopTable();
	34: a_CheckPosition();
	35: a_ShiftDataAddress();
	36: a_TableReady();

ELSE
	LasControl.LasMethControl(User:=me,LasType:=LasMethType.StepFail);(*Don't Change*)
END_CASE;

SoftwareHomeDefine();

TimeDefine(); Y  , с с ^и           a_BigtableCalibrate -3%]N  MeetConditions:=StationCfg.Data^.xAutomatic;
SafePositions:=StationCfg.Data^.xAutomatic;
FOR iCounter:= FromUser TO ToUser DO

	IF NOT Stations.Data[iCounter].xCalibrate  THEN
		MeetConditions:=FALSE;
	END_IF

	IF  Stations.Data[iCounter].xCalibrate AND NOT  Stations.Data[iCounter].xSafePosition THEN

		SafePositions:=FALSE;
		LasControl.SetError(User:= iCounter, Key:= Text.Name.ProtectionCircuitFail, sValue01:=CONCAT('Station',BYTE_TO_STRING(Stations.Data[iCounter].bStationID)) );

	END_IF

END_FOR


Start_level:=MeetConditions AND SafePositions AND NOT Stations.Data[Master].xLightCurtainRelay ;
Direction:=TRUE;
(*MoveTime(in:=Start_level,pt:=t#500ms);*)

LasControl.SetStep(User:= Me,
			ExternalSign:=NOT TableinPosi,(* MoveTime.Q   AND  TablePosTimer.Q,*)
			JumpAddress:= 0,
			BackStep:= FALSE,
			Ack=> );Z  , њ њ wё           a_BigtableCalibrateStop -3%]  MeetConditions:=StationCfg.Data^.xAutomatic;
SafePositions:=StationCfg.Data^.xAutomatic;
FOR iCounter:= FromUser TO ToUser DO

	IF NOT Stations.Data[iCounter].xCalibrate  THEN
		MeetConditions:=FALSE;
	END_IF

	IF  Stations.Data[iCounter].xCalibrate AND NOT  Stations.Data[iCounter].xSafePosition THEN

		SafePositions:=FALSE;
		LasControl.SetError(User:= iCounter, Key:= Text.Name.ProtectionCircuitFail, sValue01:=CONCAT('Station',BYTE_TO_STRING(Stations.Data[iCounter].bStationID)) );

	END_IF

END_FOR


Start_level:=MeetConditions AND SafePositions AND NOT Stations.Data[Master].xLightCurtainRelay ;

(*MoveTime(in:=Start_level,pt:=t#500ms);*)


IF Stations.Data[Master].xLightCurtainRelay THEN

	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.LightCurtain_On, sValue01:= '');

END_IF

(***************************************************step control*****************************************************)
LasControl.SetStep(User:= Me,
			ExternalSign:=   TableinPosi AND ReadytoStart,
			JumpAddress:= 0,
			BackStep:= FALSE,
			Ack=> );


IF StationCfg.Data^.Step.Ack THEN

	Start_level:=FALSE;
	MoveTable:=FALSE;
	MoveTime(in:=FALSE);
	TablePosTimer(in:=FALSE);


	TheoreticalPosition:= (TheoreticalPosition MOD INT_TO_BYTE(MaxUserCount))+1;

END_IF

(*
IF MoveTime.Q   AND TablePosTimer.Q THEN

	Start_level:=FALSE;
	MoveTable:=FALSE;
	MoveTime(in:=FALSE);
	TablePosTimer(in:=FALSE);

	StationCfg.Data^.iRequestedStepNumber:= StationCfg.Data^.iActualStepNumber +1;

END_IF
*)[  , њ њ wё           a_CalibrateEnd -3%]  (*Calibrate ready - Jump Software Home*)


FOR iCounter:= FromUser TO ToUser DO

	Stations.Data[iCounter].xTableToStation  :=TRUE;

END_FOR

TheoreticalPosition:=PhysicalPosition;

StationCfg.Data^.xCalibrate:= TRUE;

		LasControl.SetStep(User:= Me,
					ExternalSign:=StationCfg.Data^.xCalibrate,
					JumpAddress:= StationCfg.Data^.iAddressSoftwareHome,
					BackStep:= FALSE);\  , 2 2 Џ)           a_CheckPosition -3%]!  FOR iCounter:= FromUser TO ToUser DO

	Stations.Data[iCounter].xStationToTable :=FALSE;

END_FOR

PhysicalPosition:= TheoreticalPosition;(*Please delete this line,just for debug.*)

IF TheoreticalPosition<>PhysicalPosition THEN

	LasControl.SetError(User:= Me, Key:=2001, sValue01:= 'TheoreticalPosition<>PhysicalPosition BigTable');

ELSIF TheoreticalPosition=PhysicalPosition THEN

				LasControl.SetStep(User:= Me,
							ExternalSign:=TRUE,
							JumpAddress:= 0,
							BackStep:= TRUE,
							Ack =>
							);

END_IF]  , K K ШB           a_MoveTable -3%]@  (*Rotate big table*)

MeetConditions:=StationCfg.Data^.xAutomatic;
SafePositions:=StationCfg.Data^.xAutomatic;
FOR iCounter:= FromUser TO ToUser DO

	IF NOT Stations.Data[iCounter].xStationToTable  THEN
		MeetConditions:=FALSE;
	END_IF

	IF  Stations.Data[iCounter].xStationToTable AND NOT  Stations.Data[iCounter].xSafePosition THEN

		SafePositions:=FALSE;

		LasControl.SetError(User:= iCounter, Key:= Text.Name.SafePosition, sValue01:=CONCAT('Table',BYTE_TO_STRING(Stations.Data[iCounter].bStationID)) );

	END_IF

END_FOR


(***************************************************don't lose it*****************************************************)
Start_level:=MeetConditions AND SafePositions AND NOT Stations.Data[Master].xLightCurtainRelay;
Direction:=TRUE;
(*MoveTime(in:=Start_level,pt:=t#500ms);*)


(***************************************************step control*****************************************************)
LasControl.SetStep(User:= Me,
			ExternalSign:=NOT TableinPosi,(* MoveTime.Q   AND  TablePosTimer.Q,*)
			JumpAddress:= 0,
			BackStep:= FALSE,
			Ack=> );

(*
IF StationCfg.Data^.Step.Ack THEN

	Start_level:=FALSE;
	MoveTable:=FALSE;
	MoveTime(in:=FALSE);
	TablePosTimer(in:=FALSE);

	TheoreticalPosition:= (TheoreticalPosition MOD INT_TO_BYTE(MaxUserCount))+1;

END_IF
*)^  ,              a_Off -3%]v   (* Step - Off *)

Start_level:=FALSE;
MoveTime(in:=FALSE);
SoftwareEnable:=FALSE;
Relay:=FALSE;
RoundCounter:=0;_  ,              a_On -3%]  (*On/Calibrate*)

Relay:=TRUE;

SoftwareEnable:=TRUE;

StationCfg.Data^.xOn:= TRUE;
LasControl.SetMessage(User:= Me, Key:=2000, sValue01:= 'Waiting Table ReadytoStart');
LasControl.SetStep(User:= Me,  ExternalSign:= ReadytoStart, JumpAddress:= 0, BackStep:= FALSE);`  ,              a_ShiftDataAddress -3%]  (*Shift Data Address by switching WT Carrier pointers*)
(*Very IMPORTANT logic!!!  Like (3+x-y)mod3 +1  for Gem SRC 3-STs and Direction is unti-clockwise*)



IF PhysicalPosition>CON_REAL_STATIONS OR PhysicalPosition=0 THEN
	LasControl.SetError(User:= Me, Key:= 2000, sValue01:='BigTable Sensor Fail' );
ELSE
		FOR iCounter:= FromUser TO ToUser DO

			iCarrierNr :=(PhysicalPosition +MaxUserCount-(iCounter-FromUser) ) MOD MaxUserCount;

			IF iCarrierNr=0 THEN iCarrierNr:=MaxUserCount; END_IF

			IF iCarrierNr<1 OR iCarrierNr > MaxUserCount THEN
				Stations.Data[iCounter].pWT :=0;
				LasControl.SetMasterError(User:= Me, Key:= Text.Name.ProgramError,sValue01:= 'Caculation');
			ELSE
				Stations.Data[iCounter].iCarrierNr := iCarrierNr;
				Stations.Data[iCounter].pWT :=ADR(PLC_arrCarrierInfo[Stations.Data[iCounter].iCarrierNr ]);
				Stations.Data[iCounter].xTableToStation :=TRUE;
			END_IF
		END_FOR
		LasControl.SetStep(User:= Me,
						ExternalSign:=  Stations.Data[ToUser].xTableToStation,
						JumpAddress:=0,
						BackStep:= FALSE);

END_IF
IF StationCfg.Data^.Step.Ack THEN
	RoundCounter:=RoundCounter+1;
END_IFa  ,              a_SignalMeet -3%]`  (*to Check all stations are ready!*)

MoveTable:=TRUE;

FOR iCounter:= FromUser TO ToUser DO

	IF NOT Stations.Data[iCounter].xStationToTable  THEN
		MoveTable:=FALSE;
	END_IF

END_FOR


(***********step************)

LasControl.SetStep(User:= Me,
				ExternalSign:= MoveTable,
				JumpAddress:= 0,
				BackStep:= FALSE,
				Ack=> );b  , 2 2 Џ)           a_SoftwareHome -3%]>  (*Software Homeposition*)


	LasControl.SetStep(User:= Me,
				ExternalSign:= Stations.Data[Master].xRun
							AND StationCfg.Data^.xHardwareHome
							AND StationCfg.Data^.xStart,
				JumpAddress:= 0,
				BackStep:= FALSE,
				Ack=> StationCfg.Data^.xRun);

IF StationCfg.Data^.Step.Ack THEN
	;
END_IF;c  , d d с[           a_StopTable -3%]Љ  (*Stop big table*)

MeetConditions:=StationCfg.Data^.xAutomatic;
SafePositions:=StationCfg.Data^.xAutomatic;
FOR iCounter:= FromUser TO ToUser DO

	IF NOT Stations.Data[iCounter].xStationToTable  THEN
		MeetConditions:=FALSE;
	END_IF

	IF  Stations.Data[iCounter].xStationToTable AND NOT  Stations.Data[iCounter].xSafePosition THEN

		SafePositions:=FALSE;

		LasControl.SetError(User:= iCounter, Key:= Text.Name.SafePosition, sValue01:=CONCAT('Table',BYTE_TO_STRING(Stations.Data[iCounter].bStationID)) );

	END_IF

END_FOR


(***************************************************don't lose it*****************************************************)
Start_level:=MeetConditions AND SafePositions AND NOT Stations.Data[Master].xLightCurtainRelay;

(*MoveTime(in:=Start_level,pt:=t#500ms);*)

IF Stations.Data[Master].xLightCurtainRelay THEN

	LasControl.SetMasterMessage(User:= Me, Key:= Text.Name.LightCurtain_On, sValue01:= '');

END_IF

(***************************************************step control*****************************************************)
LasControl.SetStep(User:= Me,
			ExternalSign:=   TableinPosi AND ReadytoStart,
			JumpAddress:= 0,
			BackStep:= FALSE,
			Ack=> );


IF StationCfg.Data^.Step.Ack THEN

	Start_level:=FALSE;
	MoveTable:=FALSE;
	MoveTime(in:=FALSE);
	TablePosTimer(in:=FALSE);

	TheoreticalPosition:= (TheoreticalPosition MOD INT_TO_BYTE(MaxUserCount))+1;

END_IFd  , с с ^и           a_TableReady -3%]  (*ready*)


StationCfg.Data^.xStart:= FALSE;
StationCfg.Data^.xDone:=TRUE;


MeetConditions:=TRUE;
FOR iCounter:= FromUser TO ToUser DO

	IF  Stations.Data[iCounter].xTableToStation  THEN
		MeetConditions:=FALSE;
	END_IF

END_FOR

		LasControl.SetStep(User:= Me,
					ExternalSign:=MeetConditions,
					JumpAddress:= StationCfg.Data^.iAddressSoftwareHome,
					BackStep:= FALSE);e  , Ш Ш EП           AddressSettings -3%]  (* Settings of Work addresses *)


StationCfg.Data^.iAddressSoftwareHome:= 30;
StationCfg.Data^.iAddressProcess := 100;
StationCfg.Data^.iAddressPass := 200;
StationCfg.Data^.iAddressFail:= 300;
StationCfg.Data^.iAddressAbort:= 400;
StationCfg.Data^.iAddressRelease:= 900;f  , с с ^и           DeviceDefine -3%]Ё  (*******************************PhysicalPosition*************************************************************)

TablePosTimer(in:=TableinPosi,pt:=t#20ms);


IF TableinPosi  THEN

	PhysicalPosition.0:=S_0bit;
	PhysicalPosition.1:=S_1bit;
	PhysicalPosition.2:=S_2bit;
	PhysicalPosition.3:=S_3bit;
	PhysicalPosition.4:=S_4bit;

END_IF

StationCfg.Data^.xSafePosition:=Stations.Data[Master].xSafePosition ;g  , } } њt           GlobalError -3%]н  (* Define of Global Errors *)

IF Alarm THEN
	LasControl.SetError(User:= Me, Key:= Text.Name.TableAlarm, sValue01:='BigTable' );
END_IF


IF NOT Alarm  AND RunOverTime.Q THEN
	LasControl.SetError(User:= Me, Key:= Text.Name.TableAlarm, sValue01:='BigTable Sensor' );
END_IF

IF KeyBoard.Key.Pulse.xEsc AND KeyBoard.Key.Sign.xManual  AND Alarm THEN
		AlarmReset:=TRUE;
END_IF

ResetAlarm(in:=AlarmReset,pt:=t#2s);
IF ResetAlarm.Q THEN
	AlarmReset:=FALSE;
END_IFh  , Ш Ш EП           GlobalMasterError -3%])   (* Define of Global Master Errors *)

;i  , Џ Џ ,І           GlobalMessage -3%]$   (* Define of Global Messages *)

;j  , њ њ wё           SafePositionDefine -3%]   ;k  ,              SoftwareHomeDefine -3%]л   (* Software Home *)

StationCfg.Data^.xSoftwareHome:= StationCfg.Data^.iActualStepNumber = StationCfg.Data^.iAddressSoftwareHome
					AND StationCfg.Data^.iRequestedStepNumber = StationCfg.Data^.iAddressSoftwareHome;l  , d d с[           StartDefine -3%]ѓ   (* Define Start requirements *)

StationCfg.Data^.xStart :=FALSE;

IF FromUser=0 OR ToUser=0 OR FromUser > ToUser  THEN

 	RETURN;

END_IF

MaxUserCount :=ToUser - FromUser+1;

StationCfg.Data^.xStart :=StationCfg.Data^.xCalibrate;m  ,           
   TimeDefine -3%]х   (* Time define *)


(* Wait Time *)
WaitTime(UpDate:= TRUE, User:= Me);
WaitTime.WaitTimeValue:= T#500ms;

(* TimeOut *)
StationCfg.Data^.TimeOut(User:= Me);

RunOverTime(in:=Start_level AND NOT TableinPosi,pt:=t#10s );             е   ,           
   FB_TimeOut  -3%]	-3%]                      Ѓ  FUNCTION_BLOCK FB_TimeOut

VAR_INPUT
	User				: INT;
	TextLine0			: STRING(40);
	Text					: INT;
	sValue01			: STRING(40);

	xEnable				: BOOL:= FALSE;
	ExternalTimeOut		: TIME:= T#0ms;
END_VAR

VAR
	TimeOutTime				: TON;
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetStep					 :FB_SetStep;
END_VAR
   (*
TimeOut control

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

TimeOutTime.IN:= xEnable AND Stations.Data[User].xAutomatic AND NOT Stations.Data[User].xToggle;

IF ExternalTimeOut <> T#0ms THEN
	TimeOutTime.PT:= ExternalTimeOut;

ELSE
	TimeOutTime.PT:= (*TimeOut*)T#3000ms;
END_IF;

TimeOutTime();

IF TimeOutTime.Q THEN
 	SetError(User:= User,Title:= TextLine0, Key:= Text, sValue01:= sValue01 );
	TimeOutTime.IN:= FALSE;
 END_IF

xEnable:= FALSE;
ExternalTimeOut:= T#0ms;               ж   , њ њ wё           FB_WaitTime -3%]	-3%]                      (  FUNCTION_BLOCK FB_WaitTime
VAR_INPUT
	User				: INT;
	Start					: BOOL;
	WaitTimeValue		: TIME;
	JumpAddress		: INT;
	UpDate				: BOOL;
	BackStep			: BOOL;
END_VAR
VAR_OUTPUT
	Ack					: BOOL;
	BackStepAck			: BOOL;
END_VAR
VAR
	WaitTimer			: TON;
	SetStep				 :FB_SetStep;
END_VARг  (*
Wait time modul with stepfunction

Built			: 16.12.2002
Version		: 1.1

Description	: 
*)

WaitTimer(IN:= Start AND NOT Stations.Data[User].xToggle, PT:= WaitTimeValue );

IF UpDate THEN
	Start:= FALSE;
	JumpAddress:= 0;
	WaitTimeValue:= T#500ms;
	UpDate:= FALSE;
ELSE
	SetStep(User:=User,ExternalSign:= WaitTimer.Q, JumpAddress:= JumpAddress, User:= User, Ack=> Ack, BackStep:= BackStep, BackStepAck=> BackStepAck );
END_IF;

BackStep:= FALSE;               з   , 2 2 Џ)           GetStationName -3%]	-3%]      ck		 BL;        Ў   FUNCTION GetStationName : STRING
VAR_INPUT
	Station							: BYTE;
END_VAR
VAR
	CON_MAX					  	:INT:=100;
	CON_MIN						:INT:=0;
	TempString						: STRING(50);
END_VAR`  (*
	Version:  1.0.0.0
	Time:       2016-11-22
	Author:     Wu,Jinghui
*)
	TempString:='';

	IF Station<=CON_MIN OR Station>CON_MAX THEN
		GetStationName:=TempString;
		RETURN;
	END_IF

	CASE Station OF
			1:	TempString:='Station1:NewPart';

	ELSE
				TempString:='Invalid Station';

	END_CASE

	GetStationName:=TempString;
	RETURN;               л   , њ њ ^\           Main  -3%]	-3%]                        PROGRAM Main
VAR
	SetError					: FB_SetError;
	SetMasterError			: FB_SetMasterError;
	SetMasterMessage		: FB_SetMasterMessage;
	SetMessage				: FB_SetMessage;
	SetStep					 :FB_SetStep;
	InitFlag			: BOOL;
	Alarm			: BOOL;
	ix_BI	:INT;
	ix_PI	:INT;
	ix_CO	:INT;
	ix_FE	:INT;
	ix_X		:INT;
	ix_Y		:INT;
	Manual    : BOOL;
	PowerOn   : BOOL;
	Reset   : BOOL;
END_VAR

9  (*
==========================================================================================

==========================================================================================
*)

System();(*
SaveData(StartWrite:= TRUE , Frequency:=t#3s );*)

(*InitPulse*)
IF NOT InitFlag THEN
	InitFlag:= TRUE;
	InitPulse:= TRUE;
ELSE
	InitPulse:= FALSE;
END_IF;
(*Init Station Data*)
IF InitPulse OR NOT InitUser.InitDone OR KeyBoard.Key.Sign.xData THEN
	InitUser;
END_IF;


(*KeyBoard*)
KeyBoard(
	xKeyManual:= 					Manual,
	xKeyAutomatik:= 					NOT Manual,(*Manual or Auto*)
	xKeyClear:=						,
	xKeyRepeat:= 					,
	xPressButton_Green:=				EL_DI[1,0].0,(*Free Test*)
	xPressButton_White:=				PowerOn,(*ПЊЛњ*)
	xPressButton_Red:=				EL_DI[1,0].2,(*Free Test*)
	xPressButton_Yellow:=				Reset,(*ИДЮЛ*)

	xButtonLight_Green=>				EL_DO[1,0].0,
	xButtonLight_White=>				EL_DO[1,0].1,
	xButtonLight_Red=>				EL_DO[1,0].2,
	xButtonLight_Yellow=>				EL_DO[1,0].3,
	Key=>
	);

	(*Lampcontrol*)
	Lamps(
		H0T_On=> 						,
		H1T_Plus=> 						,
		H2T_Minus=> 					,
		H3T_Esc=> 						,
		H4T_Clear=> 						,
		H5_TopLightRed=> 				EL_DO[1,0].6,
		H6_TopLightOrange=> 				EL_DO[1,0].5,
		H7_TopLightGreen=> 				EL_DO[1,0].4,
		 );

IF  HMI_DebugMode AND KeyBoard.Key.Sign.xManual THEN
	HMI_Debug.arrDO_EL2008[2,5]:=TRUE;(*Open Main Air*)
	HMI_Debug.arrDO_EL2008[2,6]:=TRUE;(*Open Main Air*)
	HMI_Debug.arrDO_EL2008[2,7]:=TRUE;(*Open Main Air*)
	DebugRefleshDI();
	DebugRefleshDO();
ELSE
	IF  HMI_DebugMode THEN
		DebugRefleshDI();
	END_IF

	(*MainControl*)
	MainControl(
		Me:= 							Stations.Main,
		VirtualEmergencyStop:= 			FALSE,
		VirtualSystemAirPower:= 			FALSE,
		VirtualProtectionDoor:= 			FALSE,
		EmergencyStop:= 					EL_DI[1,0].4,
		EStopSafetyRelay:=				EL_DI[1,0].6,
		LightCurtainSafetyRelay:=			EL_DI[1,0].5,
		SafetyDoorSafetyRelay:=			EL_DI[1,0].7,
		ProtectionDoorKey1:=  				EL_DI[2,0].4,
		ProtectionDoorKey2:=  				EL_DI[2,0].0,
		ProtectionDoorKey3:=  				EL_DI[2,0].1,
		ProtectionDoorKey4:=  				EL_DI[2,0].3,
		ProtectionDoorKey5:=  				EL_DI[2,0].2,
		ProtectionDoorKey6:=  				,
		ProtectionDoorKey7:=  				,
		ProtectionDoorKey8:=  				,
		ProtectionDoorKey9:=  				,
		ProtectionDoorKey10:=  			,
		S2P_SystemAirPower:= 			TRUE,
		S1P_SystemAirPower:= 			TRUE,
		K0_MainValve=> 					EL_DO[2,0].5,
		K1_MainValve=> 					EL_DO[2,0].6,
		K2_MainValve=> 					EL_DO[2,0].7
		);

	(*Stations*)

	BigTable(
			Me:= 							Stations.iBigTable ,
			Master:= 							Stations.Main,
			FromUser:= 						Stations.iStation[1],  (*!!!DO NOT CHANGE!!!, OR IT WILL MAKE MECHANIC DAMAGE!*)
			ToUser:= 						Stations.iStation[CON_REAL_STATIONS],  (*!!!DO NOT CHANGE!!!, OR IT WILL MAKE MECHANIC DAMAGE!*)

			TableinPosi:=						EL_DI[3,0].0,
			ReadytoStart:=					EL_DI[3,0].1,
	(*		Automatic:=						BK_In[0,2].2,
			Alarm_TimeOut:=					BK_In[0,2].3,
			Alarm_Overrun:=					BK_In[0,2].4,
	*)		Alarm:=							EL_DI[3,0].2,

			S_0bit:=							NOT EP_DI[1,0].6,
			S_1bit:=							NOT EP_DI[1,0].5,
	(*		S_2bit:=							Festo_In[3,2,0].2,
			S_3bit:=							Festo_In[3,2,0].3,
			S_4bit:=							Festo_In[3,2,0].4,
	*)
			Direction=>						EL_DO[2,0].0,
			Start_level=>						EL_DO[2,0].1,
	(*		Start_edge=>						BK_Out[0,1].2,*)
			SoftwareEnable=>					EL_DO[2,0].2,
	(*		SpecialMode=>					BK_Out[0,1].4,
			ParamSet3=>						BK_Out[0,1].5,
			ParamSet2=>						BK_Out[0,1].6,
	*)		AlarmReset=>					EL_DO[2,0].3,
			Relay=>							EL_DO[1,0].7);


	(*Stations*)

	Station01(
		Me:= Stations.iStation[1] ,
		Master:= Stations.Main,
	
		Sz13a_S1_Air_Supply_Connector:=			EP_DI[1,0].3,
		Sz13b_S1_Air_Supply_Connector:=			EP_DI[1,0].2,
		Sz18a_S1_Box_Illumination:=				EP_DI[1,0].0,
		Sz18b_S1_Box_Illumination:=				EP_DI[1,0].1,
		Sz16b_S1_Red_Box_Front_Lock:=			EP_DI[7,0].7,
		Sz16b_S1_Red_Box_Product_in:= 			EP_DI[7,0].6,
		Sz17a_S1_Red_Box_Big_Lock:=			EP_DI[5,0].6,
		Sz17b_S1_Red_Box_Big_Lock:=			EP_DI[5,0].7,
		S_S1_Fixture_Presence:=					EP_DI[1,0].7,
		S_S1_DUT_Presence:=					EP_DI[4,0].6,
		S_S1_Red_Box_Close:=					EP_DI[4,0].7,
		S_S1_Adapter_Presence:=					EP_DI[1,0].4,
		(*S_S1_Airbag_Cable_Presence:=			EP_DI[2,0].7,*)
	
		xPressButton_PrintLabel:=					EL_DI[4,0].0,
		pRequest :=								ADR(PLC_stuEndTestRequest1),
		pResponse :=							ADR(ADS_stuEndTestResponse1),
		pRequestFinished :=						ADR(PLC_stuFinishedPartRequest),
		pResponseFinished :=						ADR(ADS_stuFinishedPartResponse),
		pScanner:=								ADR(ADS_stuScannerSt01),
	
		xLineLight=>								EL_DO[3,0].0,
		xCircleLight=>								EL_DO[3,0].1,
		xBoxLight=>								EL_DO[3,0].2,
		z13a_Cylinder_S1_Air_Supply_Connector=>	FESTO_DO[1,3].1,
		z13b_Cylinder_S1_Air_Supply_Connector=>	FESTO_DO[1,3].0,
		z14b_Cylinder_S1_Fix=>					FESTO_DO[1,3].2,
		z15b_Cylinder_S1_Mark=>					FESTO_DO[1,3].3,
		z16b_Cylinder_S1_Red_Box_Front_Lock=>	FESTO_DO[1,3].4,
		z17b_Cylinder_S1_Red_Box_Big_Lock=>		FESTO_DO[1,3].5,
		z18a_Cylinder_S1_Box_Illumination=>		FESTO_DO[1,4].3,
		z18b_Cylinder_S1_Box_Illumination=>		FESTO_DO[1,4].2,
		);
	

	Station02(
		Me:= Stations.iStation [2] ,
		Master:= Stations.Main,
	
		Sz01a_S2_Stator_Right_Connector:=			EP_DI[7,0].0,
		Sz01b_S2_Stator_Right_Connector:=			EP_DI[7,0].1,
		pRequest :=								ADR(PLC_stuEndTestRequest2),
		pResponse :=							ADR(ADS_stuEndTestResponse2),

		z01a_Cylinder_S2_Stator_Right_Connector=>	FESTO_DO[1,1].5,
		z01b_Cylinder_S2_Stator_Right_Connector=>	FESTO_DO[1,1].4,
		);
	
	Station03(
		Me:= Stations.iStation [3] ,
		Master:= Stations.Main,

		Sz01a_S3_Stator_Right_Connector:=			EP_DI[4,0].0,
		Sz01b_S3_Stator_Right_Connector:=			EP_DI[4,0].1,
		pRequest :=								ADR(PLC_stuEndTestRequest3),
		pResponse :=							ADR(ADS_stuEndTestResponse3),
	
		z01a_Cylinder_S3_Stator_Right_Connector=>	FESTO_DO[1,0].1,
		z01b_Cylinder_S3_Stator_Right_Connector=>	FESTO_DO[1,0].0,
		);

END_IF

(**********************************************************************************************************************)
(*Reporthandler*)
ReportHandler(ErrorMessageSet=>				PLC_stuErrorMessage); м   , d d с[           DebugRefleshDI -3%]  FOR ix_BI:=1 TO 20 DO
		HMI_Debug.arrDI_EL1008[ix_BI,0]:=EL_DI[ix_BI,0].0;
		HMI_Debug.arrDI_EL1008[ix_BI,1]:=EL_DI[ix_BI,0].1;
		HMI_Debug.arrDI_EL1008[ix_BI,2]:=EL_DI[ix_BI,0].2;
		HMI_Debug.arrDI_EL1008[ix_BI,3]:=EL_DI[ix_BI,0].3;
		HMI_Debug.arrDI_EL1008[ix_BI,4]:=EL_DI[ix_BI,0].4;
		HMI_Debug.arrDI_EL1008[ix_BI,5]:=EL_DI[ix_BI,0].5;
		HMI_Debug.arrDI_EL1008[ix_BI,6]:=EL_DI[ix_BI,0].6;
		HMI_Debug.arrDI_EL1008[ix_BI,7]:=EL_DI[ix_BI,0].7;

		HMI_Debug.arrDI_EP1008[ix_BI,0]:=EP_DI[ix_BI,0].0;
		HMI_Debug.arrDI_EP1008[ix_BI,1]:=EP_DI[ix_BI,0].1;
		HMI_Debug.arrDI_EP1008[ix_BI,2]:=EP_DI[ix_BI,0].2;
		HMI_Debug.arrDI_EP1008[ix_BI,3]:=EP_DI[ix_BI,0].3;
		HMI_Debug.arrDI_EP1008[ix_BI,4]:=EP_DI[ix_BI,0].4;
		HMI_Debug.arrDI_EP1008[ix_BI,5]:=EP_DI[ix_BI,0].5;
		HMI_Debug.arrDI_EP1008[ix_BI,6]:=EP_DI[ix_BI,0].6;
		HMI_Debug.arrDI_EP1008[ix_BI,7]:=EP_DI[ix_BI,0].7;

END_FORн   , } } њt           DebugRefleshDO -3%]O
  	FOR ix_BI:=1 TO 20 DO

		(*
		HMI_Debug.arrDI_EL1008[ix_BI,0]:=EL_DI[ix_BI,0].0;
		HMI_Debug.arrDI_EL1008[ix_BI,1]:=EL_DI[ix_BI,0].1;
		HMI_Debug.arrDI_EL1008[ix_BI,2]:=EL_DI[ix_BI,0].2;
		HMI_Debug.arrDI_EL1008[ix_BI,3]:=EL_DI[ix_BI,0].3;
		HMI_Debug.arrDI_EL1008[ix_BI,4]:=EL_DI[ix_BI,0].4;
		HMI_Debug.arrDI_EL1008[ix_BI,5]:=EL_DI[ix_BI,0].5;
		HMI_Debug.arrDI_EL1008[ix_BI,6]:=EL_DI[ix_BI,0].6;
		HMI_Debug.arrDI_EL1008[ix_BI,7]:=EL_DI[ix_BI,0].7;

		HMI_Debug.arrDI_EP1008[ix_BI,0]:=EP_DI[ix_BI,0].0;
		HMI_Debug.arrDI_EP1008[ix_BI,1]:=EP_DI[ix_BI,0].1;
		HMI_Debug.arrDI_EP1008[ix_BI,2]:=EP_DI[ix_BI,0].2;
		HMI_Debug.arrDI_EP1008[ix_BI,3]:=EP_DI[ix_BI,0].3;
		HMI_Debug.arrDI_EP1008[ix_BI,4]:=EP_DI[ix_BI,0].4;
		HMI_Debug.arrDI_EP1008[ix_BI,5]:=EP_DI[ix_BI,0].5;
		HMI_Debug.arrDI_EP1008[ix_BI,6]:=EP_DI[ix_BI,0].6;
		HMI_Debug.arrDI_EP1008[ix_BI,7]:=EP_DI[ix_BI,0].7;
		*)

		EL_DO[ix_BI,0].0:=HMI_Debug.arrDO_EL2008[ix_BI,0];
		EL_DO[ix_BI,0].1:=HMI_Debug.arrDO_EL2008[ix_BI,1];
		EL_DO[ix_BI,0].2:=HMI_Debug.arrDO_EL2008[ix_BI,2];
		EL_DO[ix_BI,0].3:=HMI_Debug.arrDO_EL2008[ix_BI,3];
		EL_DO[ix_BI,0].4:=HMI_Debug.arrDO_EL2008[ix_BI,4];
		EL_DO[ix_BI,0].5:=HMI_Debug.arrDO_EL2008[ix_BI,5];
		EL_DO[ix_BI,0].6:=HMI_Debug.arrDO_EL2008[ix_BI,6];
		EL_DO[ix_BI,0].7:=HMI_Debug.arrDO_EL2008[ix_BI,7];

	END_FOR


	ix_X:=1;
	ix_Y:=0;
	FOR ix_CO:=1 TO 10  DO
		IF ix_Y>5 THEN
			ix_X:=ix_X+1;
			ix_Y:=0;
                  END_IF
		FESTO_DO[ix_X,ix_Y].0:=HMI_Debug.arrCylinder[ix_CO,0].bulDOA;
		FESTO_DO[ix_X,ix_Y].1:=HMI_Debug.arrCylinder[ix_CO,0].bulDOB;
		FESTO_DO[ix_X,ix_Y].2:=HMI_Debug.arrCylinder[ix_CO,1].bulDOA;
		FESTO_DO[ix_X,ix_Y].3:=HMI_Debug.arrCylinder[ix_CO,1].bulDOB;
		FESTO_DO[ix_X,ix_Y].4:=HMI_Debug.arrCylinder[ix_CO,2].bulDOA;
		FESTO_DO[ix_X,ix_Y].5:=HMI_Debug.arrCylinder[ix_CO,2].bulDOB;
		FESTO_DO[ix_X,ix_Y].6:=HMI_Debug.arrCylinder[ix_CO,3].bulDOA;
		FESTO_DO[ix_X,ix_Y].7:=HMI_Debug.arrCylinder[ix_CO,3].bulDOB;

		ix_Y:=ix_Y+1;
		IF ix_Y>5 THEN
			ix_X:=ix_X+1;
			ix_Y:=0;
                  END_IF

		FESTO_DO[ix_X,ix_Y].0:=HMI_Debug.arrCylinder[ix_CO,4].bulDOA;
		FESTO_DO[ix_X,ix_Y].1:=HMI_Debug.arrCylinder[ix_CO,4].bulDOB;
		FESTO_DO[ix_X,ix_Y].2:=HMI_Debug.arrCylinder[ix_CO,5].bulDOA;
		FESTO_DO[ix_X,ix_Y].3:=HMI_Debug.arrCylinder[ix_CO,5].bulDOB;
		FESTO_DO[ix_X,ix_Y].4:=HMI_Debug.arrCylinder[ix_CO,6].bulDOA;
		FESTO_DO[ix_X,ix_Y].5:=HMI_Debug.arrCylinder[ix_CO,6].bulDOB;
		FESTO_DO[ix_X,ix_Y].6:=HMI_Debug.arrCylinder[ix_CO,7].bulDOA;
		FESTO_DO[ix_X,ix_Y].7:=HMI_Debug.arrCylinder[ix_CO,7].bulDOB;

		ix_Y:=ix_Y+1;
	END_FOR             о   , } } њ           NOP_FB -3%]	-3%]                         FUNCTION_BLOCK NOP_FB
VAR_INPUT
	Me				: INT;
	BackEnable		: BOOL:= TRUE;
END_VAR
VAR_OUTPUT
END_VAR
VAR
	SetStep						 :FB_SetStep;
END_VARЃ   (*
No OPeration

Version : 1.0
Built	: 08.02.2004
*)
SetStep( User:= Me, ExternalSign:= TRUE, JumpAddress:= 0, BackStep:= BackEnable );

BackEnable:= TRUE;               п   , K K ШB           StringtoFailPartInfo -3%]	-3%]                      ]  FUNCTION StringtoFailPartInfo : StructFailedPartInfo
VAR_INPUT
  	(*CarrierNr						: UINT;*)
	User							: INT;		(*Compatiable as before, you cas see User as CarrierNr if your line is an auotmation one *)
         Station							: BYTE;
	Str								: STRING(255);
	ManualLine						: BOOL;
END_VAR
VAR
	CON_MAX					  	:INT:=100;
	CON_MIN						:INT:=0;
         CON_MAXROW					:INT:=10;
	CON_MAXPARTINFO				:INT:=13;
	CON_SPLITINFO					:STRING(1):='#';
	CON_SPLITVALUE				:STRING(1):=':';
	i								:INT:=1;
	j								:INT:=1;
	TempFailedPartInfo				:StructFailedPartInfo;
	TempString						: ARRAY[1..10] OF STRING(50);
	TempTitleString					: ARRAY[1..10] OF STRING(20);
	TempValueString					: ARRAY[1..10] OF STRING(50);
	TempPartTitleString				: ARRAY[1..13] OF STRING(20);
	TempPartValueString				: ARRAY[1..13] OF STRING(50);
END_VAR

k  (*
	Version:  1.0.0.1
	Time:       2018-03-14
	Author:     Wu,Jinghui
*)
	TempPartTitleString[1]:='strFailKostalNr';
	TempPartTitleString[2]:='strFailSerialNr';
	TempPartTitleString[3]:='strFailScheduleName';
	TempPartTitleString[4]:='strFailTestStatus';
	TempPartTitleString[5]:='strFailCarrierNr';
	TempPartTitleString[6]:='strFailStationNr';
	TempPartTitleString[7]:='strFailTestStep';
	TempPartTitleString[8]:='strFailCode';
	TempPartTitleString[9]:='strFailText';
	TempPartTitleString[10]:='strFailValue';
	TempPartTitleString[11]:='strFailLowerLimit';
	TempPartTitleString[12]:='strFailUpperLimit';
	TempPartTitleString[13]:='strFailUnit';

	(*Clean Data*)
	TempFailedPartInfo.strFailKostalNr:='';
	TempFailedPartInfo.strFailSerialNr:='';
	TempFailedPartInfo.strFailScheduleName:='';
	TempFailedPartInfo.strFailTestStatus:='';
	TempFailedPartInfo.strFailCarrierNr:='';
	TempFailedPartInfo.strFailStationNr:='';
	TempFailedPartInfo.strFailTestStep:='';
	TempFailedPartInfo.strFailCode:='';
	TempFailedPartInfo.strFailText:='';
	TempFailedPartInfo.strFailValue:='';
	TempFailedPartInfo.strFailLowerLimit:='';
	TempFailedPartInfo.strFailUpperLimit:='';
	TempFailedPartInfo.strFailUnit:='';

	IF User<=CON_MIN OR User>CON_MAX THEN
		StringtoFailPartInfo:=TempFailedPartInfo;
		RETURN;
	END_IF

	IF Station<=CON_MIN OR Station>CON_MAX THEN
		StringtoFailPartInfo:=TempFailedPartInfo;
		RETURN;
	END_IF

	FOR i:=1 TO CON_MAXROW DO
		TempString[i]:='';
		TempTitleString[i]:='';
		TempValueString[i]:='';
	END_FOR

	FOR i:=1 TO CON_MAXPARTINFO DO
		TempPartValueString[i]:='';
	END_FOR

	(*Split string*)
	i:=1;
	 WHILE LEN(str)>0 DO
		IF FIND(Str,CON_SPLITINFO)>0 THEN
			TempString[i]:=LEFT(Str,FIND(Str,CON_SPLITINFO)-1);
			Str:=MID(Str,LEN(Str)-FIND(Str,CON_SPLITINFO),FIND(Str,CON_SPLITINFO)+1);
		ELSE
			TempString[i]:=Str;
			Str:='';
		END_IF
		i:=i+1;
	END_WHILE

	(*Split Article and Value*)
	FOR i:=1 TO CON_MAXROW DO
		IF LEN(TempString[i])>0 THEN
			IF FIND(TempString[i],CON_SPLITVALUE)>0 THEN
				TempTitleString[i]:=LEFT(TempString[i],FIND(TempString[i],CON_SPLITVALUE)-1);
				TempValueString[i]:=MID(TempString[i],LEN(TempString[i])-FIND(TempString[i],CON_SPLITVALUE),FIND(TempString[i],CON_SPLITVALUE)+1);
			END_IF
		ELSE
			TempTitleString[i]:='';
			TempValueString[i]:='';
		END_IF
	END_FOR

	(*Get FailPartValue*)
	FOR i:=1 TO 13 DO
		FOR j:=1 TO CON_MAXROW DO
			IF TempPartTitleString[i]=TempTitleString[j] THEN
				TempPartValueString[i]:=TempValueString[j];
                            END_IF
		END_FOR
	END_FOR

	TempFailedPartInfo.strFailTestStep:=TempPartValueString[7];
	TempFailedPartInfo.strFailCode:=TempPartValueString[8];
	TempFailedPartInfo.strFailText:=TempPartValueString[9];
	TempFailedPartInfo.strFailValue:=TempPartValueString[10];
	TempFailedPartInfo.strFailLowerLimit:=TempPartValueString[11];
	TempFailedPartInfo.strFailUpperLimit:=TempPartValueString[12];
	TempFailedPartInfo.strFailUnit:=TempPartValueString[13];

	IF ManualLine THEN
		TempFailedPartInfo.strFailKostalNr		:=Stations.Data[User].pWT^.stuVariantInfoSet.strKostalNr;
		TempFailedPartInfo.strFailSerialNr		:=Stations.Data[User].pWT^.stuVariantInfoSet.strSerialNr;
		TempFailedPartInfo.strFailScheduleName:=Stations.Data[User].pWT^.strScheduleName;
		TempFailedPartInfo.strFailTestStatus		:=Stations.Data[User].StationName;
		TempFailedPartInfo.strFailStationNr		:=BYTE_TO_STRING(Stations.Data[User].bStationID);
		TempFailedPartInfo.strFailCarrierNr		:=INT_TO_STRING(Stations.Data[User].iCarrierNr);
	ELSE
		TempFailedPartInfo.strFailKostalNr		:=PLC_arrCarrierInfo[User].stuVariantInfoSet.strKostalNr;
		TempFailedPartInfo.strFailSerialNr		:=PLC_arrCarrierInfo[User].stuVariantInfoSet.strSerialNr;
		TempFailedPartInfo.strFailScheduleName:=PC_arrScheduleList[PLC_arrCarrierInfo[User].bytScheduleModeNr].strScheduleName;
		TempFailedPartInfo.strFailTestStatus		:=GetStationName(Station);
		TempFailedPartInfo.strFailStationNr		:=UINT_TO_STRING(Station);
		TempFailedPartInfo.strFailCarrierNr		:=INT_TO_STRING(User);
	END_IF

	StringtoFailPartInfo:=TempFailedPartInfo;

	RETURN ;               ё   , K K Ш\        	   System_FB -3%]	-3%]                      6  FUNCTION_BLOCK System_FB
VAR_INPUT
END_VAR
VAR_OUTPUT
	MaxTimeTask					: ARRAY[1..4] OF UDINT;
	ActualTimeTask					: ARRAY[1..4] OF UDINT;

	MaxTimeTaskInMilliSecond			: ARRAY[1..4] OF REAL;
	ActualTimeTaskInMilliSecond		: ARRAY[1..4] OF REAL;
	wHour							: WORD;
	wMinute							: WORD;
	wSecond						: WORD;

	sTime							: STRING;
END_VAR
VAR
	iX: SINT;
	Timer							: TON;
	GetSYSTime						: NT_GetTime;
	OverviewInfo						: FB_ManageOverviewInfo;

	TempHour 						: STRING;
	TempMinute						: STRING;
	TempSecond						: STRING;

END_VARР  OverviewInfo();

Timer(in:=NOT GetSYSTime.BUSY,pt:=t#2s);

GetSYSTime(
	NETID:= '',
	START:= Timer.Q,
	TMOUT:= T#3s,
	BUSY=> ,
	ERR=> ,
	ERRID=> ,
	TIMESTR=> );
wHour:=GetSYSTime.TIMESTR.wHour;
wMinute:=GetSYSTime.TIMESTR.wMinute;
wSecond:=GetSYSTime.TIMESTR.wSecond;

TempHour	 := CONCAT(RIGHT(CONCAT ('0',WORD_TO_STRING(wHour)),2),':');
TempMinute	 := CONCAT(RIGHT(CONCAT ('0',WORD_TO_STRING(wMinute)),2),':');
TempSecond := RIGHT(CONCAT ('0',WORD_TO_STRING(wSecond)),2);
sTime 		:= CONCAT(CONCAT(Temphour, TempMinute),TempSecond);

(* lastExecTime in 1/100 ns *)
FOR iX:= 1 TO 4 DO
	IF MaxTimeTask[iX] < SYSTEMTASKINFOARR[iX].lastExecTime THEN
		MaxTimeTask[iX]:= SYSTEMTASKINFOARR[iX].lastExecTime;
		MaxTimeTaskInMilliSecond[iX]:= UDINT_TO_REAL(MaxTimeTask[iX]) / 10000;
	END_IF;

	ActualTimeTask[iX]:= SYSTEMTASKINFOARR[iX].lastExecTime;
	ActualTimeTaskInMilliSecond[iX]:= UDINT_TO_REAL(ActualTimeTask[iX]) / 10000;
END_FOR;               ѓ   , Ш Ш EП           TextFile -3%]	-3%]                      и   FUNCTION TextFile : STRING
VAR_INPUT
	Number					: INT;
	Language				: SINT;
END_VAR

VAR
	local_Number			: INT;
	localText					: STRING;
END_VAR

VAR CONSTANT
	LANGUAGE_OFFSET		: INT:= 1000;
END_VAR

§C  (*
Text of display

Author	: Frank Dќmpelmann
Built	: 26.08.2003

Use structText to access Textvariables

*)


Language:= LIMIT(1,Language,Text.MAX_LANGUAGE);

(* define language *)
local_Number:= Number + LANGUAGE_OFFSET * ( Language - 1 );

(* get Text *)
CASE local_Number OF

	(*
		===========================================================
		Chinese 
		===========================================================
	*)

		(* MAIN CONTROL *)

				  (* 1234567890123456789012345678901234567890 *)
	0:	localText:= 'Start Main Texts';
	1:	localText:= 'Machine off';
	2:	localText:= 'Machine Airpower';
	3:	localText:= 'Machine calib.';
	4:	localText:= 'Machine STOP';
	5:	localText:= 'Machine START';
	6:	localText:= 'Machine Cycle Stop';
	7:	localText:= 'Machine move to empty';
	8:	localText:= 'Please wait';
	9:	localText:= 'On=> [Manual+Start]';

		(* USER DEFINE *)

				  (* 1234567890123456789012345678901234567890 *)
	20:	localText:= 'Start Userdefinition';
	21:	localText:= 'System';
	22:	localText:= 'Main';
	23:	localText:= 'Table';
	24:	localText:= 'Station01';
	25:	localText:= 'Station02';
	26:	localText:= 'Station03';

		(* LANGUAGES *)

				  (* 1234567890123456789012345678901234567890 *)
	40:	localText:= 'Start Languages';
	41:	localText:= 'Chinese';
	42:	localText:= 'English';


		(* SYSTEM *)

				  (* 1234567890123456789012345678901234567890 *)
	80:	localText:= '';

	88:	localText:= 'Uhr';
	89:	localText:= 'Tag';
	90:	localText:= 'Monat';
	91:	localText:= 'Jahr';
	92:	localText:= 'H';
	93:	localText:= 'Min';
	94:	localText:= 'Sec';
	95:	localText:= 'Step : ';
	96:	localText:= 'New : ';
	97:	localText:= 'Yes';
	98:	localText:= 'No';
	99:	localText:= 'Software ';


		(* DATA *)

					  (* 1234567890123456789012345678901234567890 *)
	100:	localText:= 'Daten (+/-/ENT/ESC)';
	101:	localText:= 'SchedulNo: ';
	102:	localText:= 'Counter: ';
	103:	localText:= 'Language: ';
	104:	localText:= 'TimeOut(s): ';

	105:	localText:= 'PreTestTime:';
	106:	localText:= Center20('PrintedBarcode: ');
	107:	localText:= Center20('ScannedBarcode: ');
	108:	localText:= Center20('CommonCounter');
	109:	localText:= 'DummyReq.:';

					  (* 1234567890123456789012345678901234567890 *)
	(* AQx Area *)

	110:	localText:= 'Index HW-Year:';
	111:	localText:= 'Index HW_Week:';
	112:	localText:= 'Index SW-Year:';
	113:	localText:= 'Index SW-Week:';
	114:	localText:= 'Index SW-Index:';
	115:	localText:= 'Index E:';
	116:	localText:= 'Index ZGS:';
	117:	localText:= 'Date:';
	118:	localText:= 'Date:';
	119:	localText:= 'Date:';
	120:	localText:= 'Time:';
	121:	localText:= 'Time:';


		(* ERROR/MESSAGE *)

					  (* 1234567890123456789012345678901234567890 *)
	200:	localText:= 'FAIL ';
	201:	localText:= 'FAIL Sensor ';
	202:	localText:= 'FAIL Cyl. ';
	203:	localText:= 'FAIL Fuse ';
	204:	localText:= 'FAIL Motor ';
	205:	localText:= 'FAIL Emergency Stop ';
	206:	localText:= 'FAIL Air pressure ';
	207:	localText:= 'FAIL Protection Door ';
	208:	localText:= 'I-Fail.Heatcircuit ';
	209:	localText:= 'I-Fail.PreHeatcircuit';
	210:	localText:= 'Articlefail:';
	211:	localText:= 'ProtectionCircuitFail:';

					  (* 1234567890123456789012345678901234567890 *)
	230:	localText:= 'Fail ESP';
	231:	localText:= 'IC Sensor off:';	(* IC = Indeixing curve *)
	232:	localText:= 'Fail IC Sensor:';	(* IC = Indeixing curve *)
	234:	localText:= 'PartFail:';
	235:	localText:= 'DummyFail:';

					  (* 1234567890123456789012345678901234567890 *)
	250:	localText:= 'ЧыШЁГіКЯИёВњЦЗЃЁ';
	251:	localText:= 'ЧыШЁГіВЛСМВњЦЗЃЁ';
	252:	localText:= 'Please Remove Slider!:';
	253:	localText:= 'Quitt NOK Parts';
	254:	localText:= 'Fail Tool:';
	255:	localText:= 'Remove curve:';
	256:	localText:= 'Req.Curve Dummy: ';
	257:	localText:= 'NoPartInSelector:';
	258:	localText:= 'NoStickOnBottom:';
	259:	localText:= 'Box full: ';
	260:	localText:= 'No Box: ';
	261:	localText:= 'Remove parts';
	262:	localText:= 'Stack empty: ';
	263:	localText:= 'Remove part and move fixture back ';
	264:	localText:= 'First Grease !';
	265:	localText:= 'First Assemble Foil';
	266:	localText:= 'Protectiondoor close:';
	267:	localText:= 'Protectiondoor open:';
	268:	localText:= 'TurnScrews';
	269:	localText:= 'ScrewDriverReset';
	270:	localText:= 'Set Dummy';
	271:	localText:= 'Remove Dummy';

					  (* 1234567890123456789012345678901234567890 *)
	300:	localText:= 'No PrinterResponse';
	301:	localText:= 'Send MaskFile';
	302:	localText:= 'Send PrintFile';
	303:	localText:= 'Check Font';
	304:	localText:= 'Font failed';
	305:	localText:= 'Check MaskFile';
	306:	localText:= 'MaskFile failed';
	307:	localText:= 'Check LabelState';
	308:	localText:= 'Remove Label';
	309:	localText:= 'Wait for printer';
	310:	localText:= 'WT Number:';
	311:	localText:= 'OK';
	312:	localText:= 'BAD';
	313:	localText:= 'FailNo:';
	314:	localText:= 'Key Carr.Code';
	315:	localText:= 'do switch on ';
	316:	localText:= 'do switch off ';
	317:	localText:= 'Select valid';
	318:	localText:= 'Carrier(<>ENT):';
	319:	localText:= 'St.';
		  		      (* 1234567890123456789012345678901234567890 *)

	400:	localText:= 'ЕШД§ЩЈУш';
	401:	localText:= 'ЮДевЕНЩЈУшНсЙћ';
	402:	localText:= 'ЩЈУшЪЇАмЃЁ';

	500:	localText:= 'ContiniousGreaseRun>Channel:';

		(* ============================================================== *)
		(* Schedule Names *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	600:	localText:= 'SCHEDULE-INVALID';
	601:	localText:= 'All Stations Stopped by ';
	602:	localText:= 'System Air Power Lost:';
	603:	localText:= 'Safety door open at ';
	604:	localText:= 'Cylinder Sensor Failed';
	612:	localText:= 'Table not in Position!';

	622:	localText:= 'Station is not at Safe Position';
	623:	localText:= 'Waiting for ADS';
	624:	localText:= 'Waiting for Device';
	625:	localText:= 'Waiting for LAS';
	626:	localText:= 'Get New Part ';

	636:	localText:= 'ЧыЗХжУМаОпЃЁ';
	637:	localText:= 'ЧыШЁЯТЪЪХфЦїЃЁ';
	638:	localText:= 'ЧыРыПЊЙтеЄЃЁ';


	641:	localText:= 'ЧыЗХШыЪЪХфЦїЃЁ';
	642:	localText:= 'ЧыДђПЊКьКазгЃЁ';
	643:	localText:= 'ЧыНЋВњЦЗЗХШыКьКазгЃЁ';
	644:	localText:= 'ЧыЙиБеКьКазгЃЁ';
	645:	localText:= 'ЧыАДТЬЩЋАДХЅЃЁ';
	646:	localText:= 'ЧыАДАзЩЋАДХЅЃЁ';
	647:	localText:= 'ЧыАДКьЩЋАДХЅЃЁ';
	648:	localText:= 'ЧыАДЛЦЩЋАДХЅЃЁ';
	649:	localText:= 'ЧыВхШыЦјФвЯпРТЃЁ';
	650:	localText:= 'ЧыШЁГіВњЦЗЃЁ';
	651:	localText:= 'ЧыШЁГібљМўЃЁ';

	660:	localText:= 'Press GreenButton to Confirm OK!';
	661:	localText:= 'Press RedButton to confirm NG!';
	662:	localText:= 'Press Start to Run ';
	663:	localText:= 'It is Running at ';
	664:	localText:= 'Turn Key to Auto';
   	666:	localText:= 'ЧыЗХШыВњЦЗЃЁ';
	670:	localText:= 'Invalid Product Family Name!';
	680:	localText:= 'ЙІФмВтЪдбљМў';
	681:	localText:= 'здаЃбщбљМў';
    

		(* ============================================================== *)
		(* Fail Names *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	700:	localText:= 'FailNo.invalid';
	701:	localText:= 'HV Fail';

		(* ============================================================== *)
		(* Stations Name *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	800:	localText:= 'No Station';
	801:	localText:= 'St1.New Part';
	802:	localText:= 'St1b.Press Grid';
	803:	localText:= 'St2.Press Cover';
	804:	localText:= 'St3.Cut Free';
	805:	localText:= 'St4.HV Test';
	806:	localText:= 'St5a.ManuallWork';
	807:	localText:= 'St5b.ManuallWork';
	808:	localText:= 'St8a.ClipsMajor';
	809:	localText:= 'St8b.ClipsMinor';
	810:	localText:= 'St9.ManuallWork';
	811:	localText:= 'TSW1.TrackSwitch';
	812:	localText:= 'St11a.Welding';
	813:	localText:= 'TSW2.TrackSwitch';
	814:	localText:= 'St11b.Welding';
	815:	localText:= 'TSW3.TrackSwitch';
	816:	localText:= 'St11c.Welding';
	817:	localText:= 'St12.Press';
	818:	localText:= 'St13.Vision';
	819:	localText:= 'St13b.ManuallWork';
	820:	localText:= 'St13c.SpillTest';
	821:	localText:= 'St14a.EndOfLine';
	822:	localText:= 'St15a.Remove';
	823:	localText:= 'St14b.EndOfLine';
	824:	localText:= 'St15b.Remove';

					  		   (* 1234567890123456789012345678901234567890 *)
	900:	localText:= Center20('Projectnumber');
	901:	localText:= Center20('Trace ID');
	902:	localText:= Center20('Drawingnumber');
	903:	localText:= Center20('PLC-Program');

	999:	localText:= '';

	(*
		===========================================================
		English
		===========================================================
	*)

		(* MAIN CONTROL *)

					  (* 1234567890123456789012345678901234567890 *)
	1000:	localText:= 'Start Main Texts';
	1001:	localText:= 'Machine off';
	1002:	localText:= 'Machine Airpower';
	1003:	localText:= 'Machine calib.';
	1004:	localText:= 'Machine STOP';
	1005:	localText:= 'Machine START';
	1006:	localText:= 'Machine Cycle Stop';
	1007:	localText:= 'Machine move to empty';
	1008:	localText:= 'Please wait';
	1009:	localText:= 'On => [Manual+Start]';

		(* USER DEFINE *)

					  (* 1234567890123456789012345678901234567890 *)
	1020:	localText:= 'Start Userdefinition';
	1021:	localText:= 'System';
	1022:	localText:= 'Main';
	1023:	localText:= 'TestScrewTool';

		(* LANGUAGES *)

					  (* 1234567890123456789012345678901234567890 *)
	1040:	localText:= 'Start Languages';
	1041:	localText:= 'German';
	1042:	localText:= 'English';


		(* SYSTEM *)

					  (* 1234567890123456789012345678901234567890 *)
	1091:	localText:= '';
	1092:	localText:= 'H';
	1093:	localText:= 'Min';
	1094:	localText:= 'Sec';
	1095:	localText:= 'Step : ';
	1096:	localText:= 'New : ';
	1097:	localText:= 'Yes';
	1098:	localText:= 'No';
	1099:	localText:= 'Software ';

		(* DATA *)


					  (* 1234567890123456789012345678901234567890 *)
	1100:	localText:= 'Data (+/-/ENT/ESC)';
	1101:	localText:= 'SchedulNo: ';
	1102:	localText:= 'Counter: ';
	1103:	localText:= 'Language: ';
	1104:	localText:= 'TimeOut(s): ';

	1105:	localText:= 'PreTestTime:';
	1106:	localText:= 'PrintedBarcode: ';
	1107:	localText:= 'ScannedBarcode: ';
	1108:	localText:= Center20('CommonCounter');
	1109:	localText:= 'DummyReq.:';

					  (* 1234567890123456789012345678901234567890 *)
	(* AQx Area *)
	1110:	localText:= 'Index HW-Year:';
	1111:	localText:= 'Index HW_Week:';
	1112:	localText:= 'Index SW-Year:';
	1113:	localText:= 'Index SW-Week:';
	1114:	localText:= 'Index SW-Index:';
	1115:	localText:= 'Index E:';
	1116:	localText:= 'Index ZGS:';
	1117:	localText:= 'Date:';
	1118:	localText:= 'Date:';
	1119:	localText:= 'Date:';
	1120:	localText:= 'Time:';
	1121:	localText:= 'Time:';


		(* ERROR/MESSAGE *)

					  (* 1234567890123456789012345678901234567890 *)
	1200:	localText:= 'FAIL ';
	1201:	localText:= 'FAIL Sensor ';
	1202:	localText:= 'FAIL Cyl. ';
	1203:	localText:= 'FAIL Fuse ';
	1204:	localText:= 'FAIL Motor ';
	1205:	localText:= 'FAIL Emergency Stop ';
	1206:	localText:= 'FAIL Air pressure ';
	1207:	localText:= 'FAIL Protection Door ';
	1208:	localText:= 'I-Fail.Heatcircuit ';
	1209:	localText:= 'I-Fail.PreHeatcircuit';
	1210:	localText:= 'Articlefail:';
	1211:	localText:= 'ProtectionCircuitFail:';


					  (* 1234567890123456789012345678901234567890 *)
	1230:	localText:= 'Fail ESP';
	1231:	localText:= 'IC Sensor off:';	(* IC = Indeixing curve *)
	1232:	localText:= 'Fail IC Sensor:';	(* IC = Indeixing curve *)
	1234:	localText:= 'PartFail:';
	1235:	localText:= 'DummyFail:';

					  (* 1234567890123456789012345678901234567890 *)
	1250:	localText:= 'Please Remove OK Part!';
	1251:	localText:= 'Please Remove NG Part!';
	1252:	localText:= 'Please Remove Slider!:';
	1253:	localText:= 'Quitt NOK Parts';
	1254:	localText:= 'Fail Tool:';
	1255:	localText:= 'Remove curve:';
	1256:	localText:= 'Req.Curve Dummy: ';
	1257:	localText:= 'NoPartInSelector:';
	1258:	localText:= 'NoStickOnBottom:';
	1259:	localText:= 'Box full: ';
	1260:	localText:= 'No Box: ';
	1261:	localText:= 'Remove parts';
	1262:	localText:= 'Stack empty: ';
	1263:	localText:= 'Remove part and move fixture back ';
	1264:	localText:= 'First Grease !';
	1265:	localText:= 'First Assemble Foil';
	1266:	localText:= 'Protectiondoor close:';
	1267:	localText:= 'Protectiondoor open:';
	1268:	localText:= 'TurnScrews';
	1269:	localText:= 'ScrewDriverReset';
	1270:	localText:= 'Set Dummy';
	1271:	localText:= 'Remove Dummy';

					  (* 1234567890123456789012345678901234567890 *)
	1300:	localText:= 'No PrinterResponse';
	1301:	localText:= 'Send MaskFile';
	1302:	localText:= 'Send PrintFile';
	1303:	localText:= 'Check Font';
	1304:	localText:= 'Font failed';
	1305:	localText:= 'Check MaskFile';
	1306:	localText:= 'MaskFile failed';
	1307:	localText:= 'Check LabelState';
	1308:	localText:= 'Remove Label';
	1309:	localText:= 'Wait for printer';
	1310:	localText:= 'WT Number:';
	1311:	localText:= 'OK';
	1312:	localText:= 'BAD';
	1313:	localText:= 'FailNo:';
	1314:	localText:= 'Key Carr.Code';
	1315:	localText:= 'do switch on ';
	1316:	localText:= 'do switch off ';
	1317:	localText:= 'Select valid';
	1318:	localText:= 'Carrier(<>ENT):';
	1319:	localText:= 'St.';

		  		      (* 1234567890123456789012345678901234567890 *)
	1400:	localText:= 'Wait for Scanner';
	1401:	localText:= 'No ScannResult';
	1402:	localText:= 'Scann Fail';

	1500:	localText:= 'ContiniousGreaseRun>Channel:';

		(* ============================================================== *)
		(* Schedule Names *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	1600:	localText:= 'SCHEDULE-INVALID';
	1601:	localText:= 'All Stations Stopped by ';
	1602:	localText:= 'System Air Power Lost:';
	1603:	localText:= 'Safety door open at ';
	1604:	localText:= 'Cylinder Sensor Failed';
	1612:	localText:= 'Table not in Position!';

	1622:	localText:= 'Station is not at Safe Position';
	1623:	localText:= 'Waiting for ADS';
	1624:	localText:= 'Waiting for Device';
	1625:	localText:= 'Waiting for LAS';
	1626:	localText:= 'Get New Part ';

	1636:	localText:= 'Please Set Fixture!';
	1637:	localText:= 'Please Remove Adapter!';
	1638:	localText:= 'Please Leave the Light Curtain!';

	
	1641:	localText:= 'Please Insert Adapter!';
	1642:	localText:= 'Please Open the Red Box!';
	1643:	localText:= 'Please Put Part to Red Box!';
	1644:	localText:= 'Please Close the Red Box!';
	1645:	localText:= 'Please Press the Green Button!';
	1646:	localText:= 'Please Press the White Button!';
	1647:	localText:= 'Please Press the Red Button!';
	1648:	localText:= 'Please Press the Yellow Button!';
	1649:	localText:= 'Please Insert Airbag Cable!';
	1650:	localText:= 'Please Remove Part!';
	1651:	localText:= 'Please Remove REF Part!';

	1660:	localText:= 'Press GreenButton to Confirm OK!';
	1661:	localText:= 'Press RedButton to confirm NG!';
	1662:	localText:= 'Press Start to Run ';
	1663:	localText:= 'It is Running at ';
	1664:	localText:= 'Turn Key to Auto';
   	1666:	localText:= 'Please Insert Part!';
	1670:	localText:= 'Invalid Product Family Name!';
	1680:	localText:= 'MasterPartTest';
	1681:	localText:= 'SelfResistanceTest';

		(* ============================================================== *)
		(* Fail Names *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	1700:	localText:= 'FailNo.invalid';
	1701:	localText:= 'HV Fail';

		(* ============================================================== *)
		(* Stations Name *)
		(* ============================================================== *)
		  		      (* 1234567890123456789012345678901234567890 *)
	1800:	localText:= 'No Station';
	1801:	localText:= 'St1.New Part';
	1802:	localText:= 'St1b.Press Grid';
	1803:	localText:= 'St2.Press Cover';
	1804:	localText:= 'St3.Cut Free';
	1805:	localText:= 'St4.HV Test';
	1806:	localText:= 'St5a.ManuallWork';
	1807:	localText:= 'St5b.ManuallWork';
	1808:	localText:= 'St8a.ClipsMajor';
	1809:	localText:= 'St8b.ClipsMinor';
	1810:	localText:= 'St9.ManuallWork';
	1811:	localText:= 'TSW1.TrackSwitch';
	1812:	localText:= 'St11a.Welding';
	1813:	localText:= 'TSW2.TrackSwitch';
	1814:	localText:= 'St11b.Welding';
	1815:	localText:= 'TSW3.TrackSwitch';
	1816:	localText:= 'St11c.Welding';
	1817:	localText:= 'St12.Press';
	1818:	localText:= 'St13.Vision';
	1819:	localText:= 'St13b.ManuallWork';
	1820:	localText:= 'St13c.SpillTest';
	1821:	localText:= 'St14a.EndOfLine';
	1822:	localText:= 'St15a.Remove';
	1823:	localText:= 'St14b.EndOfLine';
	1824:	localText:= 'St15b.Remove';

					  		   (* 1234567890123456789012345678901234567890 *)
	1900:	localText:= Center20('Projectnumber');
	1901:	localText:= Center20('Trace ID');
	1902:	localText:= Center20('Drawingnumber');
	1903:	localText:= Center20('PLC-Program');

	1999:	localText:= '';

ELSE
	localText:= '';
END_CASE

TextFile:= LEFT( localText,40);               є   ,              UpdateDestinationStation -3%]	-3%]                         FUNCTION UpdateDestinationStation : BOOL
VAR_INPUT
	User						: INT;
	StationID						: INT;
	TestResult					: BOOL;
END_VAR
VAR

END_VARВ  (*FUN:UpdateStationDestination*)
(*
	Version:  1.0.0.1
	Time:       2018-03-13
	Author:     Wang Yumin
	Description:  Update Station Destination for WTs
*)

(*Pointer as 0 or prohibited updating destination by other startions*)

		IF Stations.Data[User].pWT=0 THEN
		
			UpdateDestinationStation:=FALSE;
		
			RETURN;
		
		END_IF



(*Update Destination Station*)
IF TestResult THEN
	IF Stations.Data[User].pWT^.bytScheduleModeNr>0 THEN
	            Stations.Data[User].pWT^.bytDestinationStation:=PC_arrScheduleList[Stations.Data[User].pWT^.bytScheduleModeNr].arrScheduleData[StationID,1].arrDestinationStationData[1];
	  END_IF

ELSE
	IF Stations.Data[User].pWT^.bytScheduleModeNr>0 THEN
			Stations.Data[User].pWT^.bytDestinationStation:=PC_arrScheduleList[Stations.Data[User].pWT^.bytScheduleModeNr].arrScheduleData[StationID,0].arrDestinationStationData[1];
	  END_IF
END_IF


UpdateDestinationStation:=TRUE;                 §џџџ,            #   COMlibV2.lib 26.2.19 20:20:29 @/u\   HMI.lib 27.6.18 13:21:15 @Ы3[$   TcSystemCX.lib 7.9.16 11:51:26 @ОЯW!   TcBase.lib 14.5.09 11:14:08 @J"   TcSystem.lib 7.6.16 10:01:22 @ђ*VW%   TcUtilities.lib 3.2.16 15:08:58 @ЇБV"   STANDARD.LIB 5.6.98 11:03:02 @ц_w5   e  _ReceiveString @      ComADSSerialConfig_1       ComADSServerCmd    	   ComBuffer       ComDebugBuffer       ComDTRCtrl_t    
   ComError_t       ComHandshake_t       ComParity_t       ComRegisterData_t       ComRegisterList_t       ComRTSCtrl_t       ComSerialConfig       ComSerialLineMode_t       ComStopBits_t       EL6inData22B       EL6outData22B       IE6inData11B       IE6outData11B    	   KL6inData       KL6inData22B       KL6inData5B    
   KL6outData       KL6outData22B       KL6outData5B       M8000inData       M8000outData       PcComInData       PcComOutData                  _SendString @          _strncpy @       	   ASC @       	   CHR @          ClearComBuffer @          ComReset @          Get_ComLib_Version @          KL6configuration @          KL6ReadRegisters @          KL6WriteRegisters @          M8000configuration @          ReceiveByte @          ReceiveData @          ReceiveString @          ReceiveString255 @          SendByte @          SendData @          SendString @          SendString255 @          SerialLineControl @          SerialLineControlADS @          SerialLineControlM8000 @             Global_Constants @       $   Global_Constants_ComLibVersion @          _   Center20 @      structDisplayData       structKeyBoard       structKeyBoardData                  CX_Display_Intern @       !   CX_Display_Intern.PrintPage @          CX_Display_Intern.ReadKey @          ESA_VT50_Seriell_FB @       &   ESA_VT50_Seriell_FB.ClearDisplay @       "   ESA_VT50_Seriell_FB.ClearRow @       #   ESA_VT50_Seriell_FB.CursorOff @       #   ESA_VT50_Seriell_FB.PrintPage @       !   ESA_VT50_Seriell_FB.ReadKey @          ESA_VT50_Seriell_FB.Row_1 @          ESA_VT50_Seriell_FB.Row_2 @          LastKeysOff @          PILZ_PX30_Seriell_FB @       '   PILZ_PX30_Seriell_FB.ClearDisplay @       #   PILZ_PX30_Seriell_FB.ClearRow @       "   PILZ_PX30_Seriell_FB.FlashOn @       &   PILZ_PX30_Seriell_FB.MonitorMode @       $   PILZ_PX30_Seriell_FB.PrintPage @       "   PILZ_PX30_Seriell_FB.ReadKey @           PILZ_PX30_Seriell_FB.Row_1 @           PILZ_PX30_Seriell_FB.Row_2 @          SerialLogger_FB @       $   SerialLogger_FB.GetDateAndTime @          SmallKeyBoard_FB @          TextTicker16_FB @          TextTicker20_FB @             Globale_Variablen @              F_CXNaviSwitch @	      E_CX1100_DisplayModes       E_CX1100_NaviSwitch       E_CX2100_DisplayModesRd       E_CX2100_DisplayModesWr       E_CX2100_NaviSwitch       E_UPS_STATE       ST_CX_ProfilerStruct       ST_CxDeviceIdentification       ST_CxDeviceIdentificationEx                  F_CXNaviSwitchUSB @          F_CXSubTimeStamp @          F_GetVersionTcSystemCX @       "   FB_CxGetDeviceIdentification @       $   FB_CxGetDeviceIdentificationEx @          FB_CXGetTextDisplayUSB @          FB_CXProfiler @          FB_CXSetTextDisplay @          FB_CXSetTextDisplayUSB @          FB_CXSimpleUps @             Global_Constants @          z   FW_AdsClearEvents @      FW_NoOfByte       FW_SystemInfoType       FW_SystemTaskInfoType    
   FW_TcEvent                   FW_AdsLogDINT @           FW_AdsLogEvent @           FW_AdsLogLREAL @           FW_AdsLogSTR @           FW_AdsRdWrt @           FW_AdsRdWrtInd @           FW_AdsRdWrtRes @           FW_AdsRead @           FW_AdsReadDeviceInfo @           FW_AdsReadInd @           FW_AdsReadRes @           FW_AdsReadState @           FW_AdsWrite @           FW_AdsWriteControl @           FW_AdsWriteInd @           FW_AdsWriteRes @           FW_DRand @           FW_GetCpuAccount @           FW_GetCpuCounter @           FW_GetCurTaskIndex @           FW_GetSystemTime @           FW_GetVersionTcBase @           FW_LptSignal @           FW_MemCmp @           FW_MemCpy @           FW_MemMove @           FW_MemSet @           FW_PortRead @          FW_PortWrite @           T   ^  ADSCLEAREVENTS @%      E_IOAccessSize    
   E_OpenPath       E_SeekOrigin       E_TcEventClass       E_TcEventClearModes       E_TcEventPriority       E_TcEventStreamType       E_UsrLED_Color       E_WATCHDOG_TIME_CONFIG       ExpressionResult       PVOID       SFCActionType       SFCStepType       ST_AdsBaDevApiHead       ST_AdsBaDevApiIoCtlModifier       ST_AdsBaDevApiReq       ST_AdsCallGenericFbExReq       ST_AdsRdWrtListHead       ST_AdsRdWrtListPara       ST_AdsReadWriteListEntry    
   ST_AmsAddr       ST_StructMemberAlignmentProbe       ST_WD_GPIO_Info       ST_WD_GPIO_InfoEx       SYSTEMINFOTYPE       SYSTEMTASKINFOTYPE    
   T_AmsNetId       T_AmsNetIdArr    	   T_AmsPort    
   T_IPv4Addr       T_IPv4AddrArr       T_MaxString       T_U64KAFFINITY       TcEvent       UXINT       XINT       XWORD                   ADSLOGDINT @           ADSLOGEVENT @           ADSLOGLREAL @           ADSLOGSTR @           ADSRDDEVINFO @           ADSRDSTATE @           ADSRDWRT @           ADSRDWRTEX @           ADSRDWRTIND @           ADSRDWRTRES @           ADSREAD @           ADSREADEX @           ADSREADIND @           ADSREADRES @           ADSWRITE @           ADSWRITEIND @           ADSWRITERES @           ADSWRTCTL @           AnalyzeExpression @          AnalyzeExpressionCombined @          AnalyzeExpressionTable @          AppendErrorString @          BAVERSION_TO_DWORD @          CLEARBIT32 @           CSETBIT32 @           DRAND @           F_CompareFwVersion @          F_CreateAmsNetId @           F_CreateIPv4Addr @           F_GetStructMemberAlignment @          F_GetVersionTcSystem @           F_IOPortRead @          F_IOPortWrite @          F_ScanAmsNetIds @          F_ScanIPv4AddrIds @          F_SplitPathName @          F_ToASC @          F_ToCHR @          FB_AdsReadWriteList @          FB_BaDeviceIoControl @          FB_BaGenGetVersion @          FB_CreateDir @          FB_EOF @           FB_FileClose @           FB_FileDelete @           FB_FileGets @           FB_FileOpen @           FB_FilePuts @           FB_FileRead @           FB_FileRename @           FB_FileSeek @           FB_FileTell @           FB_FileWrite @           FB_PcWatchdog @          FB_PcWatchdog_BAPI @          FB_RemoveDir @          FB_SetLedColor_BAPI @          FB_SimpleAdsLogEvent @          FILECLOSE @           FILEOPEN @           FILEREAD @           FILESEEK @           FILEWRITE @           FW_CallGenericFb @          FW_CallGenericFbEx @          FW_CallGenericFun @          GETBIT32 @           GETCPUACCOUNT @           GETCPUCOUNTER @           GETCURTASKINDEX @           GETSYSTEMTIME @           GETTASKTIME @          LPTSIGNAL @           MEMCMP @           MEMCPY @           MEMMOVE @           MEMSET @           ROL32 @           ROR32 @           SETBIT32 @           SFCActionControl @           SHL32 @           SHR32 @              Global_Variables @        J    ARG_TO_CSVFIELD @@      ADSDATATYPEID       E_AmsLoggerMode    	   E_ArgType       E_DbgContext       E_DbgDirection       E_EnumCmdType       E_FileRBufferCmd       E_HashPrefixTypes       E_MIB_IF_Type       E_NumGroupTypes       E_PersistentMode       E_PrefixFlagParam       E_RegValueType       E_RouteTransportType    
   E_SBCSType       E_ScopeServerState       E_TimeZoneID       E_TypeFieldParam       E_UTILITIES_ERRORCODES       GUID       OTSTRUCT       PROFILERSTRUCT       REMOTEPC       REMOTEPCINFOSTRUCT       ST_AmsFindFileSystemEntry       ST_AmsGetTimeZoneInformation       ST_AmsLoggerReq       ST_AmsRouteEntry       ST_AmsRouteEntryHead       ST_AmsRouterInfoEntry       ST_AmsRouteSystemEntry       ST_AmsStartProcessReq       ST_AmsSymbolInfoEntry       ST_DeviceIdentification       ST_DeviceIdentificationEx       ST_FileAttributes       ST_FileRBufferHead       ST_FindFileEntry       ST_FormatParameters       ST_HKeySrvRead       ST_HKeySrvWrite       ST_IP_ADAPTER_INFO       ST_IP_ADDR_STRING       ST_IPAdapterHwAddr       ST_IPAdapterInfo       ST_SBCSTable    #   ST_ScopeServerRecordModeDescription       ST_TcRouterStatusInfo       ST_TimeZoneInformation       SYMINFO_BUFFER       SYMINFOSTRUCT       T_Arg    
   T_FILETIME       T_FIX16    
   T_FloatRec       T_HashTableEntry       T_HHASHTABLE       T_HLINKEDLIST       T_HUGE_INTEGER       T_LARGE_INTEGER       T_LinkedListEntry       T_UHUGE_INTEGER       T_ULARGE_INTEGER    
   TIMESTRUCT                  BCD_TO_DEC @           BE128_TO_HOST @          BE16_TO_HOST @          BE32_TO_HOST @          BE64_TO_HOST @          BYTE_TO_BINSTR @          BYTE_TO_DECSTR @          BYTE_TO_HEXSTR @          BYTE_TO_LREALEX @          BYTE_TO_OCTSTR @          BYTEARR_TO_MAXSTRING @          CSVFIELD_TO_ARG @          CSVFIELD_TO_STRING @          DATA_TO_HEXSTR @          DCF77_TIME @          DCF77_TIME_EX @          DEC_TO_BCD @           DEG_TO_RAD @           DINT_TO_DECSTR @          DT_TO_FILETIME @          DT_TO_SYSTEMTIME @           DWORD_TO_BINSTR @          DWORD_TO_DECSTR @          DWORD_TO_HEXSTR @          DWORD_TO_LREALEX @          DWORD_TO_OCTSTR @          F_ARGCMP @          F_ARGCPY @          F_ARGIsZero @          F_BIGTYPE @          F_BOOL @          F_BYTE @           F_BYTE_TO_CRC16_CCITT @          F_CheckSum16 @           F_CRC16_CCITT @           F_CreateHashTableHnd @          F_CreateLinkedListHnd @          F_DATA_TO_CRC16_CCITT @          F_DINT @           F_DWORD @           F_FormatArgToStr @          F_GetDayOfMonthEx @          F_GetDayOfWeek @          F_GetDOYOfYearMonthDay @          F_GetFloatRec @          F_GetMaxMonthDays @          F_GetMonthOfDOY @          F_GetVersionTcUtilities @           F_GetWeekOfTheYear @          F_HUGE @          F_INT @           F_LARGE @          F_LREAL @           F_LTrim @          F_PVOID @          F_REAL @           F_RTrim @          F_SINT @           F_STRING @           F_SwapReal @           F_SwapRealEx @          F_ToLCase @          F_ToUCase @          F_TranslateFileTimeBias @          F_UDINT @           F_UHUGE @          F_UINT @           F_ULARGE @          F_USINT @           F_WORD @           F_YearIsLeapYear @          FB_AddRouteEntry @          FB_AmsLogger @          FB_BasicPID @           FB_BufferedTextFileWriter @       '   FB_BufferedTextFileWriter.A_Reset @          FB_ConnectScopeServer @          FB_CSVMemBufferReader @          FB_CSVMemBufferWriter @          FB_DbgOutputCtrl @          FB_DbgOutputCtrl.A_Log @          FB_DbgOutputCtrl.A_LogHex @          FB_DbgOutputCtrl.A_Reset @          FB_DisconnectScopeServer @          FB_EnumFindFileEntry @          FB_EnumFindFileList @          FB_EnumRouteEntry @          FB_EnumStringNumbers @          FB_FileRingBuffer @       !   FB_FileRingBuffer.A_AddTail @          FB_FileRingBuffer.A_Close @           FB_FileRingBuffer.A_Create @       !   FB_FileRingBuffer.A_GetHead @          FB_FileRingBuffer.A_Open @       $   FB_FileRingBuffer.A_RemoveHead @          FB_FileRingBuffer.A_Reset @       &   FB_FileTimeToTzSpecificLocalTime @       .   FB_FileTimeToTzSpecificLocalTime.A_Reset @          FB_FormatString @           FB_GetAdaptersInfo @           FB_GetDeviceIdentification @       "   FB_GetDeviceIdentificationEx @          FB_GetHostAddrByName @          FB_GetHostName @          FB_GetLocalAmsNetId @          FB_GetRouterStatusInfo @          FB_GetTimeZoneInformation @          FB_HashTableCtrl @          FB_HashTableCtrl.A_Add @       !   FB_HashTableCtrl.A_GetFirst @       )   FB_HashTableCtrl.A_GetIndexAtPosPtr @           FB_HashTableCtrl.A_GetNext @          FB_HashTableCtrl.A_Lookup @          FB_HashTableCtrl.A_Remove @       "   FB_HashTableCtrl.A_RemoveAll @       $   FB_HashTableCtrl.A_RemoveFirst @          FB_HashTableCtrl.A_Reset @          FB_LinkedListCtrl @       &   FB_LinkedListCtrl.A_AddHeadValue @       &   FB_LinkedListCtrl.A_AddTailValue @       "   FB_LinkedListCtrl.A_FindNext @       "   FB_LinkedListCtrl.A_FindPrev @       !   FB_LinkedListCtrl.A_GetHead @       *   FB_LinkedListCtrl.A_GetIndexAtPosPtr @       !   FB_LinkedListCtrl.A_GetNext @       !   FB_LinkedListCtrl.A_GetPrev @       !   FB_LinkedListCtrl.A_GetTail @       )   FB_LinkedListCtrl.A_RemoveHeadValue @       )   FB_LinkedListCtrl.A_RemoveTailValue @       -   FB_LinkedListCtrl.A_RemoveValueAtPosPtr @          FB_LinkedListCtrl.A_Reset @       *   FB_LinkedListCtrl.A_SetValueAtPosPtr @          FB_LocalSystemTime @          FB_MemBufferMerge @          FB_MemBufferSplit @          FB_MemRingBuffer @           FB_MemRingBuffer.A_AddTail @           FB_MemRingBuffer.A_GetHead @       #   FB_MemRingBuffer.A_RemoveHead @          FB_MemRingBuffer.A_Reset @          FB_MemRingBufferEx @       "   FB_MemRingBufferEx.A_AddTail @       #   FB_MemRingBufferEx.A_FreeHead @       &   FB_MemRingBufferEx.A_GetFreeSize @       "   FB_MemRingBufferEx.A_GetHead @           FB_MemRingBufferEx.A_Reset @          FB_MemStackBuffer @          FB_MemStackBuffer.A_Pop @          FB_MemStackBuffer.A_Push @          FB_MemStackBuffer.A_Reset @          FB_MemStackBuffer.A_Top @          FB_RegQueryValue @           FB_RegSetValue @           FB_RemoveRouteEntry @           FB_ResetScopeServerControl @          FB_SaveScopeServerData @          FB_ScopeServerControl @          FB_SetTimeZoneInformation @          FB_StartScopeServer @          FB_StopScopeServer @          FB_StringRingBuffer @       #   FB_StringRingBuffer.A_AddTail @       #   FB_StringRingBuffer.A_GetHead @       &   FB_StringRingBuffer.A_RemoveHead @       !   FB_StringRingBuffer.A_Reset @       (   FB_SystemTimeToTzSpecificLocalTime @       0   FB_SystemTimeToTzSpecificLocalTime.A_Reset @          FB_TextFileRingBuffer @       %   FB_TextFileRingBuffer.A_AddTail @       #   FB_TextFileRingBuffer.A_Close @       "   FB_TextFileRingBuffer.A_Open @       #   FB_TextFileRingBuffer.A_Reset @       (   FB_TranslateLocalTimeToUtcByZoneID @       0   FB_TranslateLocalTimeToUtcByZoneID.A_Reset @       (   FB_TranslateUtcToLocalTimeByZoneID @       0   FB_TranslateUtcToLocalTimeByZoneID.A_Reset @       &   FB_TzSpecificLocalTimeToFileTime @       .   FB_TzSpecificLocalTimeToFileTime.A_Reset @       (   FB_TzSpecificLocalTimeToSystemTime @       0   FB_TzSpecificLocalTimeToSystemTime.A_Reset @          FB_WritePersistentData @          FILETIME_TO_DT @          FILETIME_TO_SYSTEMTIME @          FIX16_TO_LREAL @          FIX16_TO_WORD @          FIX16Add @          FIX16Align @          FIX16Div @          FIX16Mul @          FIX16Sub @          GetRemotePCInfo @           GUID_TO_REGSTRING @          GUID_TO_STRING @          GuidsEqualByVal @          HEXASCNIBBLE_TO_BYTE @          HEXCHRNIBBLE_TO_BYTE @          HEXSTR_TO_DATA @          HOST_TO_BE128 @          HOST_TO_BE16 @          HOST_TO_BE32 @          HOST_TO_BE64 @          INT64_TO_LREAL @          Int64Add64 @          Int64Add64Ex @          Int64Cmp64 @          Int64Div64Ex @          Int64IsZero @          Int64Negate @          Int64Not @          Int64Sub64 @          IsFinite @          LARGE_INTEGER @          LARGE_TO_ULARGE @          LREAL_TO_FIX16 @          LREAL_TO_FMTSTR @          LREAL_TO_INT64 @          LREAL_TO_UINT64 @          MAXSTRING_TO_BYTEARR @          NT_AbortShutdown @           NT_GetTime @           NT_Reboot @           NT_SetLocalTime @          NT_SetTimeToRTCTime @           NT_Shutdown @           NT_StartProcess @           OTSTRUCT_TO_TIME @           PBOOL_TO_BOOL @          PBYTE_TO_BYTE @          PDATE_TO_DATE @          PDINT_TO_DINT @          PDT_TO_DT @          PDWORD_TO_DWORD @          PHUGE_TO_HUGE @          PINT_TO_INT @          PLARGE_TO_LARGE @          PLC_ReadSymInfo @           PLC_ReadSymInfoByName @           PLC_ReadSymInfoByNameEx @           PLC_Reset @           PLC_Start @           PLC_Stop @           PLREAL_TO_LREAL @          PMAXSTRING_TO_MAXSTRING @          PREAL_TO_REAL @          Profiler @           PSINT_TO_SINT @          PSTRING_TO_STRING @          PTIME_TO_TIME @          PTOD_TO_TOD @          PUDINT_TO_UDINT @          PUHUGE_TO_UHUGE @          PUINT64_TO_UINT64 @          PUINT_TO_UINT @          PULARGE_TO_ULARGE @          PUSINT_TO_USINT @          PVOID_TO_BINSTR @          PVOID_TO_DECSTR @          PVOID_TO_HEXSTR @          PVOID_TO_OCTSTR @          PVOID_TO_STRING @          PWORD_TO_WORD @          RAD_TO_DEG @           REGSTRING_TO_GUID @          ROUTETRANSPORT_TO_STRING @       	   RTC @          RTC_EX @          RTC_EX2 @          ScopeASCIIExport @           ScopeExit @          ScopeGetRecordLen @           ScopeGetState @           ScopeLoadFile @           ScopeManualTrigger @           ScopeSaveAs @          ScopeSetOffline @           ScopeSetOnline @           ScopeSetRecordLen @           ScopeViewExport @           STRING_TO_CSVFIELD @          STRING_TO_GUID @          STRING_TO_PVOID @          STRING_TO_SYSTEMTIME @          STRING_TO_UINT64 @          SYSTEMTIME_TO_DT @           SYSTEMTIME_TO_FILETIME @          SYSTEMTIME_TO_STRING @          TC_Config @          TC_CpuUsage @           TC_Restart @           TC_Stop @           TC_SysLatency @           TIME_TO_OTSTRUCT @           UDINT_TO_LREALEX @          UInt32x32To64 @          UINT64_TO_LREAL @          UINT64_TO_STRING @          UInt64Add64 @          UInt64Add64Ex @          UInt64And @          UInt64Cmp64 @          UInt64Div16Ex @          UInt64Div64 @          UInt64Div64Ex @          UInt64isZero @          UInt64Limit @          UInt64Max @          UInt64Min @          UInt64Mod64 @          UInt64Mul64 @          UInt64Mul64Ex @          UInt64Not @          UInt64Or @          UInt64Rol @          UInt64Ror @          UInt64Shl @          UInt64Shr @          UInt64Sub64 @          UInt64Xor @          UINT_TO_LREALEX @          ULARGE_INTEGER @          ULARGE_TO_LARGE @          USINT_TO_LREALEX @          WORD_TO_BINSTR @          WORD_TO_DECSTR @          WORD_TO_FIX16 @          WORD_TO_HEXSTR @          WORD_TO_LREALEX @          WORD_TO_OCTSTR @          WritePersistentData @              Global_Variables @              CONCAT @                	   CTD @        	   CTU @        
   CTUD @           DELETE @           F_TRIG @        
   FIND @           INSERT @        
   LEFT @        	   LEN @        	   MID @           R_TRIG @           REPLACE @           RIGHT @           RS @        
   SEMA @           SR @        	   TOF @        	   TON @           TP @              Global Variables 0 @                          UE
E_I
E           2                џџџџџџџџџџџџџџџџ  
             њџџџ  anie sar        јџџџ, K K ШB                      POUs               Device               Device               Cylinder                 Cylinder_V2  '                   FB_MultiCylinder  K   џџџџ              EPSON_Robot                 FB_EPSON_ROBOT  [                  FB_RobotPositionServer  \  џџџџ              HMI                 FB_KeyBoard  .                   FB_Lamps  0   џџџџ              IAI              
   FB_IAIPCON  +                   FB_IAIPositionServer  ,   џџџџ              Vision              
   FB_KEYENCE  /                  FB_Omron                SetBank  O                  SetGroup  P   N   џџџџџџџџ                FB_InitUser  -                   TextFile  ѓ   џџџџ              LAS                 FB_AdsDeviceInteraction  (                   FB_GetNewPartInfo  n                  FB_LasControl  1                   FB_LasMethControl  Z                  FB_RequestResponseInteraction  Z                   GetStationName  з                   StringtoFailPartInfo  п                   UpdateDestinationStation  є   џџџџ              SourceCodeLibrary               StructureDepend                 FB_CheckDoubleSense  )                   FB_ManageOverviewInfo  J                  FB_ReportHandler                InfoPage  V                  ReportControl  W                  ReportControlToLas  X                  SelectedUserControl  Y   U                   FB_SetError  [                   FB_SetMasterError  \                   FB_SetMasterMessage  ]                   FB_SetMessage  ^                
   FB_SetStep  _                
   FB_SetText  `                
   FB_SetTips                    FB_SignControl  a                   FB_StepControl  Ѕ                
   FB_TimeOut  е                   FB_WaitTime  ж                   NOP_FB  о   џџџџџџџџ              StationModuls                FB_Station_01                a_CheckStartCondition  s                  a_MarkPoint  t                  a_Off  u                  a_RedBoxClose  v                  a_RedBoxOpen  w                  a_RedBoxPressRedButton  x                  a_RedBoxPutPart  y                  a_RedBoxRemoveNGPart  z                  a_RemoveOKPart  {                  AddressSettings  |                  ADS_MechanicInteraction  }                  DeviceDefine  ~                  GlobalError                    GlobalMasterError                    GlobalMessage                    SafePositionDefine     r                   FB_Station_02                a_Off                    AddressSettings                    ADS_MechanicInteraction                    DeviceDefine                    GlobalError                    GlobalMasterError                    GlobalMessage                    SafePositionDefine                       FB_Station_03                a_Off                    AddressSettings                    ADS_MechanicInteraction                    DeviceDefine                     GlobalError  Ё                  GlobalMasterError  Ђ                  GlobalMessage  Ѓ                  SafePositionDefine  Є                      FB_Station_04                a_Off                   AddressSettings                   ADS_MechanicInteraction                   DeviceDefine                   GlobalError                   GlobalMasterError                   GlobalMessage                   SafePositionDefine      џџџџ              TableModuls                FB_Table                a_BigtableCalibrate  Y                 a_BigtableCalibrateStop  Z                 a_CalibrateEnd  [                 a_CheckPosition  \                 a_MoveTable  ]                 a_Off  ^                 a_On  _                 a_ShiftDataAddress  `                 a_SignalMeet  a                 a_SoftwareHome  b                 a_StopTable  c                 a_TableReady  d                 AddressSettings  e                 DeviceDefine  f                 GlobalError  g                 GlobalMasterError  h                 GlobalMessage  i                 SafePositionDefine  j                 SoftwareHomeDefine  k                 StartDefine  l              
   TimeDefine  m  X  џџџџ              FB_MainControl                a_Calibration  #                 a_LastCycle  $                 a_Off  %              	   a_PowerOn  &                 a_Run  '                 a_Stop  (                 AutomaticManual  )                 GlobalError  *                 GlobalMessages  +                 SafePositionDefine  ,                 SoftSwitchControl  -  "                 Main                DebugRefleshDI  м                  DebugRefleshDO  н   л                	   System_FB  ё   џџџџ           
   Data types               LAS                 StructCommonSchedule  р                   StructConfirmSignal  с                   StructDestinationStation  ф                   StructLasMethType  ч   џџџџ                StructCarrierInfo  "                   StructDebugCylinder  т                   StructDebugPage  у                   StructDeviceInteraction                     structErrorMessageSet  х                   StructFailedPartInfo                     StructHMIMessage  ц                   structLasRequestType  ш                   structMultiCylinder  щ                   structProjectData  ъ                   StructRequestAction  %                   StructResponseAction  &                   StructScheduleMode  #                   structStation  ы                   StructStationCfg  ь                   structStationData  э                  structStationOverviewInfo  ю                
   structText  я                   structTextData  №                   StructVariantInfo  $   џџџџ             Visualizations  џџџџ              Global Variables                 ADS_Variables                    LAS_Variables  и                   System_Variables  ђ                   Variable_Configuration  	   џџџџ                                                            ЇU                         	   localhost            P      	   localhost            P      	   localhost            P           F О