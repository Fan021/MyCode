CoDeSys+%   �                   @        @   2.3.9.31    @/    @                             �|�U +    @       w��"             �U        �   @   q   C:\TWINCAT\PLC\LIB\STANDARD.LIB @                                                                                          CONCAT               STR1               ��              STR2               ��                 CONCAT                                         �|�U  �   ����           CTD           M             ��           Variable for CD Edge Detection      CD            ��           Count Down on rising edge    LOAD            ��           Load Start Value    PV           ��           Start Value       Q            ��           Counter reached 0    CV           ��           Current Counter Value             �|�U  �   ����           CTU           M             ��            Variable for CU Edge Detection       CU            ��       
    Count Up    RESET            ��           Reset Counter to 0    PV           ��           Counter Limit       Q            ��           Counter reached the Limit    CV           ��           Current Counter Value             �|�U  �   ����           CTUD           MU             ��            Variable for CU Edge Detection    MD             ��            Variable for CD Edge Detection       CU            ��	       
    Count Up    CD            ��
           Count Down    RESET            ��           Reset Counter to Null    LOAD            ��           Load Start Value    PV           ��           Start Value / Counter Limit       QU            ��           Counter reached Limit    QD            ��           Counter reached Null    CV           ��           Current Counter Value             �|�U  �   ����           DELETE               STR               ��              LEN           ��              POS           ��                 DELETE                                         �|�U  �   ����           F_TRIG           M             ��
                 CLK            ��           Signal to detect       Q            ��           Edge detected             �|�U  �   ����           FIND               STR1               ��              STR2               ��                 FIND                                     �|�U  �   ����           INSERT               STR1               ��              STR2               ��              POS           ��                 INSERT                                         �|�U  �   ����           LEFT               STR               ��              SIZE           ��                 LEFT                                         �|�U  �   ����           LEN               STR               ��                 LEN                                     �|�U  �   ����           MID               STR               ��              LEN           ��              POS           ��                 MID                                         �|�U  �   ����           R_TRIG           M             ��
                 CLK            ��           Signal to detect       Q            ��           Edge detected             �|�U  �   ����           REPLACE               STR1               ��              STR2               ��              L           ��              P           ��                 REPLACE                                         �|�U  �   ����           RIGHT               STR               ��              SIZE           ��                 RIGHT                                         �|�U  �   ����           RS               SET            ��              RESET1            ��                 Q1            ��
                       �|�U  �   ����           SEMA           X             ��                 CLAIM            ��	              RELEASE            ��
                 BUSY            ��                       �|�U  �   ����           SR               SET1            ��              RESET            ��                 Q1            ��	                       �|�U  �   ����           TOF           M             ��           internal variable 	   StartTime            ��           internal variable       IN            ��       ?    starts timer with falling edge, resets timer with rising edge    PT           ��           time to pass, before Q is set       Q            ��	       2    is FALSE, PT seconds after IN had a falling edge    ET           ��
           elapsed time             �|�U  �   ����           TON           M             ��           internal variable 	   StartTime            ��           internal variable       IN            ��       ?    starts timer with rising edge, resets timer with falling edge    PT           ��           time to pass, before Q is set       Q            ��	       0    is TRUE, PT seconds after IN had a rising edge    ET           ��
           elapsed time             �|�U  �   ����           TP        	   StartTime            ��           internal variable       IN            ��       !    Trigger for Start of the Signal    PT           ��       '    The length of the High-Signal in 10ms       Q            ��	           The pulse    ET           ��
       &    The current phase of the High-Signal             �|�U  �   ����    R    @                                                                                          MAIN        	   stationNr            !               result            !            	   carrierNr            !            
   scheduleNr            !               emptyVariantInfo                   StructVariantInfo   !               STATION_INDEX          ! 
                               �|�U  @   ����            
 � 	 #   $      	   "         !       ( �      K   �     K   �     K   �     K   �                 �         +     ��localhost �ژXv   ��     ��H �`��.�@��� H� 4� l� ��w�6�����/�w�.�w��           ��       `� Tw� ���   0.�� �,�w8.�F  4� 4� UK� ����    pյ��             |� ��          ��       `� Tw� `� 4� Tw� ��_����@� ]"�     ,   ,                                                        K        @   �|�U�  /*BECKCONFI3*/
        !       @   @   �   �     3               
   Standard            	�|�U     tEor====           VAR_GLOBAL
END_VAR
                                                                                  "   , � � �             Standard
         MAIN����               �|�U                 $����  rrTir[(i                                  Standard �U	�U                                       	�|�U     04
	ro=T           VAR_CONFIG
END_VAR
                                                                                   '               , d d �[           Global_Variables_ADS �|�U	�|�U      d6��           M  (*** Read Me:  Abbreviation Instructions:   int--> INT;  arr--> ARRAY;  b-->BOOL; byt-->BYTE; stu--> STRUCT;***)
(*** Read Me:  Abbreviation Instructions:   uin--> UINT;  str--> STRING;***)
(*** Read Me:  Any reset to a variable is considered as value assignment , e.g. strValue=''; is regarded as write operation***)

(***============================================================ ***)
(***=====LAS PLC INTERFACE INTERNAL VERSION0.04 2015.07.10=====***)
(***============================================================ ***)

(*==============Standard Kostal ADS Variables, for each Bosch project==============*)
VAR_GLOBAL

(* The Header PC_xxxs  mean they are just for PC program to write. Except reading, any others to write them is prohibited!!!*)
	PC_arrScheduleList						: ARRAY[1..CON_MAXIMUM_SCHEDULES] OF StructScheduleMode; 		(*ADS Interface between PC and PLC, PC write it once when initializing*)
	PC_stuCurrentVariantInfo					: StructVariantInfo;													(*ADS Interface between PC and PLC, PC write variant infromation to PLC before each carrier starts to flow.  *)
	PC_bytCurrentScheduleNr					: BYTE;															(*ADS Interface between PC and PLC, PC write it into PLC before each carrier starts to flow.  *)
	PC_bulNewPartAvailable					: BOOL;
	PC_bulWriteResultIsPass					: BOOL;
	PC_bulWriteResultIsFail					: BOOL;
	PC_bulWriteCarrierInfoRequest				: BOOL;
	PC_bulWriteCarrierInfoFinished				: BOOL;
	PC_uinTargetCarrierNr						: UINT;
	PC_uinTargetCarrierStNr					: UINT;

(* The Header ADS_xxxs  mean they are both for PC and PLC program to write. Any others to use them is also permitted!*)
	ADS_stuOperateCarrierInfo					: StructCarrierInfo;													(*ADS Interface between PC and PLC, PC write it into PLC while article beening changed.*)

(* The Header PLC_xxxs  mean they are just for PLC program to write.Except reading, any others to write them is prohibited!!!*)
	PLC_arrCarrierInfo							: ARRAY[1..CON_MAXIMUM_TOTAL_CARRIERS] OF StructCarrierInfo;	(*ADS Interface between PC and PLC, PC can read or inspect any elment of it*)
	PLC_stuFailedPartInfo						: StructFailedPartInfo;												(*ADS Interface between PC and PLC, PLC wil pass the info to PC while getting failed part off the line*)
	PLC_bulGetNewPart						: BOOL;
	PLC_bulTestPass							: BOOL;
	PLC_bulTestFail							: BOOL;
	PLC_bulAssemblyPass					: BOOL;
	PLC_bulAssemblyFail						: BOOL;
	PLC_bulSetCarrierInfoReady				: BOOL;

END_VAR


(*==============Standard PLC&PC Constants, For each bosch project==============*)
(* Those constants could be changed depending actual situations.
 like if your line has a total of 24 real stations, you could set CON_MAXIMUM_REAL_STATIONS to 24*)
VAR_GLOBAL CONSTANT
	CON_MAXIMUM_SCHEDULES				: UINT:=50;
	CON_MAXIMUM_TOTAL_CARRIERS		: UINT:=100;
	CON_MAXIMUM_REAL_CARRIERS			: UINT:=50;
	CON_MAXIMUM_TOTAL_STATIONS		: UINT:=100;
	CON_MAXIMUM_REAL_STATIONS			: UINT:=20;
	CON_MAXIMUM_ASSEMBLY_STATIONS	: UINT:=20;
END_VAR


(*==============NON-Standard Kostal ADS Variables, For Mlb evo SCM project==============*)
VAR_GLOBAL

	ADS_stuScannerSt08						: StructDeviceInteraction;
	ADS_stuScannerSt11						: StructDeviceInteraction;
	ADS_stuScannerSt12						: StructDeviceInteraction;
	ADS_stuScannerSt20						: StructDeviceInteraction;
	ADS_stuPrinterSt19						: StructDeviceInteraction;

END_VAR                                                                                               '           	   , � � �           Variable_Configuration �|�U	�|�U	     d6��              VAR_CONFIG
END_VAR
                                                                                                 �   |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ss�����                               4     �   ���  �3 ���   � ���     
    @��  ���     @      DEFAULT             System      �   |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ss�����                      )   HH':'mm':'ss @                             dd'-'MM'-'yyyy @       '   "   ,   �           StructCarrierInfo �|�U	�|�U      ,	','0,	        �  TYPE StructCarrierInfo :
STRUCT
		bytDestinationStation 					: BYTE :=0;			(*to indicate destination station number which carrier should go by*)
		bytScheduleModeNr					: BYTE :=0;			(*to save  test mode number that carrier has got from LAS *)
		bytCycleNr							: BYTE :=0;			(*to save the test circle number which DUT is running in *)
		bytAssemblyCounter					: BYTE :=0;			(*to count how many assembly actions have been executed successfully*)
		(*bulFullyAssembled					: BOOL :=FALSE;	*)	(*to indicate that current DUT has been fully assemabled or not, True seen as passed *)
		bulTestResult							: BOOL :=FALSE;		(*to indicate that current DUT is test passed or not, True seen as passed *)
		stuVariantInfoSet						: StructVariantInfo;		(*to save variant data that carrier has got from LAS *)
		stuFailedPartInfo						: StructFailedPartInfo;	(*to save failed infomation while DUT test failed *)
END_STRUCT
END_TYPE                , � � ��           StructDeviceInteraction �|�U	�|�U      X H�@�%          TYPE StructDeviceInteraction :
STRUCT
	stuPlcArticleSet				: StructVariantInfo;
	bulPlcDoAction				: BOOL:=FALSE;
	bulAdsActionIsPass			: BOOL:=FALSE;
	bulAdsActionIsFail				: BOOL:=FALSE;
	strAdsActionValue				: STRING(255) :='';
END_STRUCT
END_TYPE                , � � ,�           StructFailedPartInfo �|�U	�|�U            S        h  TYPE StructFailedPartInfo :
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
END_TYPE             #   , 2 2 �)           StructScheduleMode �|�U	�|�U      Y,RRER E        "  TYPE StructScheduleMode:
STRUCT
		bytScheduleNr				: BYTE:=0;						(* Number of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
		strScheduleName				: STRING(20):='';					(* Name of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
		strScheduleDescription			: STRING(50):='';					(* Description about the schedule mode   *)
		arrScheduleData				: ARRAY[1..CON_MAXIMUM_TOTAL_STATIONS,0..1] OF BYTE:=0;		(*  Array of destination of carriers, 1...50 ==>Station Number, 0...1==> Test Result*)
END_STRUCT
END_TYPE

             $   , K K �B           StructVariantInfo �|�U	�|�U      AR_IEG;        �  TYPE StructVariantInfo:
STRUCT
		strKostalNr							: STRING(20) :='';		(* Kostal article number without Index, like 10110201*)
		strKostalArticleName					: STRING(20) :='';		(* Kostal article name, like B9_SCM*)
		strCustomerNr						: STRING(20) :='';		(* Customer number  like WD.504.105.J *)
		strProductFamily						: STRING(20) :='';		(* Product family name  *)
		strSerialNr							: STRING(50) :='';		(* Kostal serial number. like 13 digitals *)
END_STRUCT
END_TYPE              !   , } } �t           MAIN �|�U	�|�U      
             �   PROGRAM MAIN
VAR
	stationNr					: UINT	:=0;
	result					: BYTE	:=0;
	carrierNr					: BYTE	:=0;
	scheduleNr				: BYTE	:=0;
	emptyVariantInfo			: StructVariantInfo;
END_VAR
VAR CONSTANT
	STATION_INDEX		: UINT	:=1;
END_VAR�  
(**=====================================================**)
(** For Example to use StructDeviceInteraction**)
(**=====================================================**)
	(*Step0   Initialize ONCE*)
		emptyVariantInfo.strCustomerNr :='';
		emptyVariantInfo.strKostalNr :='';
		emptyVariantInfo.strProductFamily :='';
		emptyVariantInfo.strSerialNr :='';

	(*Step1   clear values*)
		ADS_stuScannerSt08.stuPlcArticleSet :=emptyVariantInfo;
		ADS_stuScannerSt08.strAdsActionValue :='';
		ADS_stuScannerSt08.bulAdsActionIsPass:=FALSE;
		ADS_stuScannerSt08.bulAdsActionIsFail:=FALSE;
		ADS_stuScannerSt08.bulPlcDoAction:=FALSE;

	(*Step2   set ArticleSet*)
		ADS_stuScannerSt08.stuPlcArticleSet  :=PLC_arrCarrierInfo[CarrierNr].stuVariantInfoSet;

	(*Step3   set DoAction as True to tell Las do some actions,like scaning barcode or printing labels or do lasing etc.*)
		ADS_stuScannerSt08.bulPlcDoAction :=TRUE;

	(*Step4   wait reslut from PC*)
		IF ADS_stuScannerSt08.bulAdsActionIsPass THEN
			(*do something here*)
			(*goto Passed Steps*)
			;
		ELSIF ADS_stuScannerSt08.bulAdsActionIsFail THEN
			(*do something here*)
			PLC_stuFailedPartInfo.strFailKostalNr :=PLC_arrCarrierInfo[CarrierNr].stuVariantInfoSet.strKostalNr ;
			(*set other  PLC_stuFailedPartInfo items*)
			PLC_stuFailedPartInfo.strFailText  := ADS_stuScannerSt08.strAdsActionValue;
			(*goto Failed Steps*)
		END_IF

	(*Step5   clear values again*)
		ADS_stuScannerSt08.stuPlcArticleSet :=emptyVariantInfo;
		ADS_stuScannerSt08.strAdsActionValue :='';
		ADS_stuScannerSt08.bulAdsActionIsPass:=FALSE;
		ADS_stuScannerSt08.bulAdsActionIsFail:=FALSE;
		ADS_stuScannerSt08.bulPlcDoAction:=FALSE;

(**=====================================================**)
(** For Example to use PLC_arrCarrierInfo & PC_arrScheduleList**)
(**=====================================================**)
	IF PLC_arrCarrierInfo[CarrierNr].bytDestinationStation=STATION_INDEX THEN
		(**First Cycle  do something here**)
		stationNr:=STATION_INDEX;
		IF TRUE THEN		(**Test Passed**)
			result:=1;
		ELSE 				(**Test Failed**)
			result:=0;
		END_IF
		IF carrierNr >0 AND carrierNr<=CON_MAXIMUM_TOTAL_CARRIERS AND stationNr >0 AND stationNr <=CON_MAXIMUM_TOTAL_STATIONS AND result>=0 AND result<=1 THEN
			scheduleNr :=PLC_arrCarrierInfo[carrierNr].bytScheduleModeNr;
			PLC_arrCarrierInfo[carrierNr].bytDestinationStation  := PC_arrScheduleList[scheduleNr].arrScheduleData[stationNr,result];
		ELSE
			;(**PLC will be stay here or set error message to LAS, like PLC_bulError :=False;**)
		END_IF

	ELSIF  PLC_arrCarrierInfo[CarrierNr].bytDestinationStation=STATION_INDEX+CON_MAXIMUM_REAL_STATIONS THEN
		stationNr:=STATION_INDEX+CON_MAXIMUM_REAL_STATIONS;
		;(**Second Cycle, do the same as the above one**)
	ELSE
		;(**Do something else***)
	END_IF



(**===========================================================**)
(** For Example to use PLC_bulAssemblyPass & PLC_bulAssemblyFail**)
(**===========================================================**)
	(*Step0   other actions to ADS_stuScannerSt20, e.g. clear values, set ArticleSet etc.*)

	(*Step1 do scan*)
		ADS_stuScannerSt20.bulPlcDoAction :=TRUE;

	(*Step2 wait for scanning result*)
		IF ADS_stuScannerSt20.bulAdsActionIsPass THEN
			(*do something here*)
			PLC_arrCarrierInfo[CarrierNr].bytAssemblyCounter := PLC_arrCarrierInfo[CarrierNr].bytAssemblyCounter+1;
			(*Step+1 here*)
			;
		ELSIF ADS_stuScannerSt20.bulAdsActionIsFail THEN
			(*do something here*)
			(*set other  PLC_stuFailedPartInfo items.....*)
			PLC_stuFailedPartInfo.strFailKostalNr :=PLC_arrCarrierInfo[CarrierNr].stuVariantInfoSet.strKostalNr ;
			PLC_stuFailedPartInfo.strFailText  := ADS_stuScannerSt20.strAdsActionValue;
			PLC_arrCarrierInfo[CarrierNr].bulTestResult :=FALSE;
			(*goto Failed Steps*)
		END_IF

	(*Step3 judge whether do ASSM-Linecontrol or not*)
		IF PLC_arrCarrierInfo[CarrierNr].bytAssemblyCounter =CON_MAXIMUM_ASSEMBLY_STATIONS THEN
			PLC_bulAssemblyPass :=TRUE;
			PLC_bulAssemblyFail :=FALSE;
			(*Step+1 here*);
		ELSIF PLC_arrCarrierInfo[CarrierNr].bytAssemblyCounter>0 THEN
			PLC_bulAssemblyPass :=FALSE;
			PLC_bulAssemblyFail :=TRUE;
			(*Step+1 here*);
		ELSIF PLC_arrCarrierInfo[CarrierNr].bytAssemblyCounter=0 THEN
			(*Step+1 here*);
		END_IF


	(*Step4 wait for wriet result and then reset values of PLC_bulAssemblyPass & PLC_bulAssemblyFail *)
		IF PC_bulWriteResultIsPass OR PC_bulWriteResultIsFail THEN
			PLC_bulAssemblyPass :=FALSE;
			PLC_bulAssemblyFail :=TRUE;
			(*Step+1 here*);
		END_IF

	(*Step5 ask eol-plc to take part away and do eol tests*)
		IF PLC_arrCarrierInfo[CarrierNr].bulTestResult THEN
			(*ask EOL-PLC to take part away and test it*);
		ELSE
			(*goto Failed Steps*);
		END_IF
                 ����  ENIF
ro         "   STANDARD.LIB 5.6.98 11:03:02 @�_w5      CONCAT @                	   CTD @        	   CTU @        
   CTUD @           DELETE @           F_TRIG @        
   FIND @           INSERT @        
   LEFT @        	   LEN @        	   MID @           R_TRIG @           REPLACE @           RIGHT @           RS @        
   SEMA @           SR @        	   TOF @        	   TON @           TP @              Global Variables 0 @                          UE
E_I
E         ��2                ����������������  
             ����  anie sar        ����  ionoinia                      POUs                MAIN  !   ����           
   Data types                StructCarrierInfo  "                   StructDeviceInteraction                     StructFailedPartInfo                     StructScheduleMode  #                   StructVariantInfo  $   ����             Visualizations  ����              Global Variables                Global_Variables_ADS                      Variable_Configuration  	   ����                                                            �U                         	   localhost            P      	   localhost            P      	   localhost            P            9��x