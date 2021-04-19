CoDeSys+'   �                   @        @   2.3.9.31    @/    @                             3��\ +    @       w��"             �U        �   @   q   C:\TWINCAT\PLC\LIB\STANDARD.LIB @                                                                                          CONCAT               STR1               ��              STR2               ��                 CONCAT                                         d�66  �   ����           CTD           M             ��           Variable for CD Edge Detection      CD            ��           Count Down on rising edge    LOAD            ��           Load Start Value    PV           ��           Start Value       Q            ��           Counter reached 0    CV           ��           Current Counter Value             d�66  �   ����           CTU           M             ��            Variable for CU Edge Detection       CU            ��       
    Count Up    RESET            ��           Reset Counter to 0    PV           ��           Counter Limit       Q            ��           Counter reached the Limit    CV           ��           Current Counter Value             d�66  �   ����           CTUD           MU             ��            Variable for CU Edge Detection    MD             ��            Variable for CD Edge Detection       CU            ��	       
    Count Up    CD            ��
           Count Down    RESET            ��           Reset Counter to Null    LOAD            ��           Load Start Value    PV           ��           Start Value / Counter Limit       QU            ��           Counter reached Limit    QD            ��           Counter reached Null    CV           ��           Current Counter Value             d�66  �   ����           DELETE               STR               ��              LEN           ��              POS           ��                 DELETE                                         d�66  �   ����           F_TRIG           M             ��
                 CLK            ��           Signal to detect       Q            ��           Edge detected             d�66  �   ����           FIND               STR1               ��              STR2               ��                 FIND                                     d�66  �   ����           INSERT               STR1               ��              STR2               ��              POS           ��                 INSERT                                         d�66  �   ����           LEFT               STR               ��              SIZE           ��                 LEFT                                         d�66  �   ����           LEN               STR               ��                 LEN                                     d�66  �   ����           MID               STR               ��              LEN           ��              POS           ��                 MID                                         d�66  �   ����           R_TRIG           M             ��
                 CLK            ��           Signal to detect       Q            ��           Edge detected             d�66  �   ����           REPLACE               STR1               ��              STR2               ��              L           ��              P           ��                 REPLACE                                         d�66  �   ����           RIGHT               STR               ��              SIZE           ��                 RIGHT                                         d�66  �   ����           RS               SET            ��              RESET1            ��                 Q1            ��
                       d�66  �   ����           SEMA           X             ��                 CLAIM            ��	              RELEASE            ��
                 BUSY            ��                       d�66  �   ����           SR               SET1            ��              RESET            ��                 Q1            ��	                       d�66  �   ����           TOF           M             ��           internal variable 	   StartTime            ��           internal variable       IN            ��       ?    starts timer with falling edge, resets timer with rising edge    PT           ��           time to pass, before Q is set       Q            ��	       2    is FALSE, PT seconds after IN had a falling edge    ET           ��
           elapsed time             d�66  �   ����           TON           M             ��           internal variable 	   StartTime            ��           internal variable       IN            ��       ?    starts timer with rising edge, resets timer with falling edge    PT           ��           time to pass, before Q is set       Q            ��	       0    is TRUE, PT seconds after IN had a rising edge    ET           ��
           elapsed time             d�66  �   ����           TP        	   StartTime            ��           internal variable       IN            ��       !    Trigger for Start of the Signal    PT           ��       '    The length of the High-Signal in 10ms       Q            ��	           The pulse    ET           ��
       &    The current phase of the High-Signal             d�66  �   ����    R    @                                                                                          MAIN           Step            !            	   stationNr            !               result            !            	   carrierNr            !            
   scheduleNr            !               emptyVariantInfo                    StructVariantInfo    !               STATION_INDEX          !               Station          !                                '�S\  @    ����            
 �     	   $   %   !      &       ( �      K   �     K   �     K   �     K                             +     ��localhost �ژXv   ��     ��H �`��.�@��� H� 4� l� ��w�6�����/�w�.�w��           ��       `� Tw� ���   0.�� �,�w8.�F  4� 4� UK� ����    pյ��             |� ��          ��       `� Tw� `� 4� Tw� ��_����@� ]"�     ,   ,                                                        K        @   Y�gV�  /*BECKCONFI3*/
        !       @   @   �   �     3               
   Standard            	3��\     tEor====           VAR_GLOBAL
END_VAR
                                                                                  "   , � � �             Standard
         MAIN����               Y�gV                 $����  rrTir[(i                                  Standard �U	�U                                       	��S\     04
	ro=T           VAR_CONFIG
END_VAR
                                                                                   '               , d d �[           Global_Variables_ADS Y�gV	3��\      d6��           l  (*** Read Me:  Abbreviation Instructions:   int--> INT;  arr--> ARRAY;  bul-->BOOL; byt-->BYTE; stu--> STRUCT;***)
(*** Read Me:  Abbreviation Instructions:   uin--> UINT;  str--> STRING; din--> DINT;***)
(*** Read Me:  Kostal Abbreviatios:  LAS-->Line Administration System; LC-->Linecontroller;  ASSM-->Assembly; WT--> Carrier***)
(*** Read Me:  Any reset to a variable is considered as value assignment , e.g. strValue=''; is regarded as a writting operation***)


(***============================================================ ***)
(***======LAS PLC INTERFACE RELEASE VERSION1.00 2015.12.07=====***)
(***============================================================ ***)

(*==============Standard Kostal ADS Variables, for each Bosch project==============*)
VAR_GLOBAL

(* The Header PC_xxxs  mean they are just for PC program to write. Except reading, any others to write them is prohibited!!!*)
	PC_stuCurrentVariantInfo					: StructVariantInfo;													(*ADS Interface between PC and PLC, PC write variant infromation to PLC before each carrier starts to flow in line.  *)
END_VAR


(*==============Standard PLC&PC Constants, For each bosch project==============*)
(* Those constants could be changed depending actual situations.
 like if your line has a total of 24 real stations, you could set CON_MAXIMUM_REAL_STATIONS to 24*)
VAR_GLOBAL CONSTANT

	CON_MAXIMUM_SCHEDULES				: UINT:=50;			(*DO NOT change me!!!*)
	CON_MAXIMUM_TOTAL_CARRIERS		: UINT:=100;			(*DO NOT change me!!!*)
	CON_MAXIMUM_REAL_CARRIERS			: UINT:=50;			(*DO NOT change me!!!*)
	CON_MAXIMUM_TOTAL_STATIONS		: UINT:=100;			(*DO NOT change me!!!*)
	CON_MAXIMUM_REAL_STATIONS			: UINT:=20;			(*you can change it's value due to actual situations!*)
	CON_MAXIMUM_ASSEMBLY_STATIONS	: UINT:=20;			(*you can change it's value due to actual situations!*)

END_VAR


(*==============NON-Standard Kostal ADS Variables, For Mlb evo SCM project==============*)
VAR_GLOBAL


	ADS_stuScannerSt08						: StructDeviceInteraction;
	ADS_stuScannerSt11						: StructDeviceInteraction;
	ADS_stuScannerSt12						: StructDeviceInteraction;
	ADS_stuScannerSt01						: StructDeviceInteraction;
	ADS_stuPrinterSt19						: StructDeviceInteraction;
	ADS_stuPrinterSt01						: StructDeviceInteraction;


	PLC_stuEndTestRequest1					: StructRequestAction;
	ADS_stuEndTestResponse1				: StructResponseAction;

	PLC_stuEndTestRequest2					: StructRequestAction;
	ADS_stuEndTestResponse2				: StructResponseAction;

	PLC_stuEndTestRequest3					: StructRequestAction;
	ADS_stuEndTestResponse3				: StructResponseAction;

	PLC_stuAssmDataRequest					: StructRequestAction;
	ADS_stuAssmDataResponse				: StructResponseAction;

	ADS_stuScannerSt61L						: StructDeviceInteraction;
	ADS_stuScannerSt61R						: StructDeviceInteraction;
	ADS_stuScannerSt62L						: StructDeviceInteraction;
	ADS_stuScannerSt62R						: StructDeviceInteraction;

	ADS_stuLaserSt61						: StructDeviceInteraction;

	ADS_stuStoreData61L						: StructDeviceInteraction;
	ADS_stuStoreData61R						: StructDeviceInteraction;
	ADS_stuStoreData_SN62L					: StructDeviceInteraction;
	ADS_stuStoreData_SN62R					: StructDeviceInteraction;

	PLC_RequestAction62L					: StructRequestAction;
	ADS_ResponseAction62L					: StructResponseAction;
	PLC_RequestAction62R					: StructRequestAction;
	ADS_ResponseAction62R					: StructResponseAction;
END_VAR                                                                                               '           	   , � � �           Variable_Configuration Y�gV	Y�gV	     d6��              VAR_CONFIG
END_VAR
                                                                                                 �   |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ss�����                               4     �   ���  �3 ���   � ���     
    @��  ���     @      DEFAULT             System      �   |0|0 @|    @Z   MS Sans Serif @       HH':'mm':'ss @      dd'-'MM'-'yyyy   dd'-'MM'-'yyyy HH':'mm':'ss�����                      )   HH':'mm':'ss @                             dd'-'MM'-'yyyy @       '      , � � ��           StructDeviceInteraction ��S\	Y�gV      X H�@�%          TYPE StructDeviceInteraction :
STRUCT
	stuPlcArticleSet				: StructVariantInfo;
	bulPlcDoAction				: BOOL:=FALSE;
	bulAdsActionIsPass			: BOOL:=FALSE;
	bulAdsActionIsFail				: BOOL:=FALSE;
	strAdsActionValue				: STRING(255) :='';
END_STRUCT
END_TYPE             %   ,   �           StructRequestAction ��S\	Y�gV      c�	 ��        	  TYPE StructRequestAction :
STRUCT
		bulRunning						: BOOL:=FALSE;
		bulDoPositiveAction				: BOOL:=FALSE;
		bulDoNegativeAction				: BOOL:=FALSE;
		strActionScheduleName			: STRING(20):='';
		stuActionArticleSet					: StructVariantInfo;
END_STRUCT
END_TYPE             &   , 2 2 !�           StructResponseAction ��S\	Y�gV      C ly
tr        e  TYPE StructResponseAction :
STRUCT
		bulPartReceived				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		bulActionIsPass				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		bulActionIsFail				: BOOL:=FALSE;				(*written by PLC or LAS or Eol*)
		strActionResultText			: STRING(255) :='';				(*written by PLC or LAS or Eol*)
END_STRUCT
END_TYPE             $   , K K �B           StructVariantInfo ��S\	��S\      AR_IEG;        9  TYPE StructVariantInfo:
STRUCT
		strKostalNr							: STRING(20) :='';		(* Kostal article number without Index, like 10110201*)
		strKostalArticleName					: STRING(20) :='';		(* Kostal article name, like B9_SCM*)
		strCustomerNr						: STRING(20) :='';		(* Customer number  like WD.504.105.J *)
		strProductFamily						: STRING(20) :='';		(* Product family name  *)
		strSerialNr							: STRING(50) :='';		(* Kostal serial number. like 13 digitals *)
		strUserDefined						: STRING(50) :='';		(* Additional infomation defined by engineers *)
END_STRUCT
END_TYPE              !   , } } �t           MAIN  a\	'�S\      
               PROGRAM MAIN
VAR
	Step					: UINT	:=0;
	stationNr					: UINT	:=0;
	result					: BYTE	:=0;
	carrierNr					: BYTE	:=0;
	scheduleNr				: BYTE	:=0;
	emptyVariantInfo			: StructVariantInfo;
END_VAR
VAR CONSTANT
	STATION_INDEX		: UINT	:=1;
	Station				: UINT	:=1;
END_VAR?  
(**=====================================================**)
(** For Example to use StructDeviceInteraction---Example1**)
(**=====================================================**)
	(*Step0   Initialize ONCE*)
		emptyVariantInfo.strCustomerNr :='';
		emptyVariantInfo.strKostalArticleName :='';
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
		(*ADS_stuScannerSt08.stuPlcArticleSet  :=PLC_arrCarrierInfo[CarrierNr].stuVariantInfoSet;*)

	(*Step3   set DoAction as True to tell Las do some actions,like scaning barcode or printing labels or do lasing etc.*)
		ADS_stuScannerSt08.bulPlcDoAction :=TRUE;

	(*Step4   wait reslut from PC*)
		IF ADS_stuScannerSt08.bulAdsActionIsPass THEN
			(*do something here*)
			(*goto Passed Steps*)
			;
		ELSIF ADS_stuScannerSt08.bulAdsActionIsFail THEN
			(*do something here*)
			;
		END_IF

	(*Step5   clear values again*)
		ADS_stuScannerSt08.stuPlcArticleSet :=emptyVariantInfo;
		ADS_stuScannerSt08.strAdsActionValue :='';
		ADS_stuScannerSt08.bulAdsActionIsPass:=FALSE;
		ADS_stuScannerSt08.bulAdsActionIsFail:=FALSE;
		ADS_stuScannerSt08.bulPlcDoAction:=FALSE;


(**===========================================================================**)
(** For Example to use PLC_stuAssmDataRequest & ADS_stuAssmDataResponse---Example3**)
(**===========================================================================**)
	(*Step1   clear PLC_stuAssmDataRequest & ADS_stuAssmDataResponse , e.g. clear values, set ArticleSet etc.*)
		ADS_stuAssmDataResponse.bulPartReceived :=FALSE;
		ADS_stuAssmDataResponse.bulActionIsFail :=FALSE;
		ADS_stuAssmDataResponse.bulActionIsPass :=FALSE;
		ADS_stuAssmDataResponse.strActionResultText :='';

		PLC_stuAssmDataRequest.bulRunning :=FALSE;
		PLC_stuAssmDataRequest.bulDoPositiveAction :=FALSE;
		PLC_stuAssmDataRequest.bulDoNegativeAction :=FALSE;
		PLC_stuAssmDataRequest.strActionScheduleName :='';
		PLC_stuAssmDataRequest.stuActionArticleSet := emptyVariantInfo;

	(*Step2 set ScheduleModeName and ArticleSet*)


	(*Step3 judge whether it is neccessary to do ASSM-Linecontrol/CAQ or not*)
		PLC_stuAssmDataRequest.bulRunning :=TRUE;
		PLC_stuAssmDataRequest.bulDoPositiveAction := TRUE;
		PLC_stuAssmDataRequest.bulDoNegativeAction := FALSE;
		(*Step+1 here*);

	(*Step4 wait for  result from LAS*)
		IF ADS_stuAssmDataResponse.bulActionIsPass OR  ADS_stuAssmDataResponse.bulActionIsFail THEN
			PLC_stuAssmDataRequest.bulRunning :=FALSE;
			PLC_stuAssmDataRequest.bulDoPositiveAction := FALSE;
			PLC_stuAssmDataRequest.bulDoNegativeAction := FALSE;
			(*Step+1 here*);
		END_IF

	(*Step5   clear PLC_stuAssmDataRequest & ADS_stuAssmDataResponse again*)
		ADS_stuAssmDataResponse.bulPartReceived :=FALSE;
		ADS_stuAssmDataResponse.bulActionIsFail :=FALSE;
		ADS_stuAssmDataResponse.bulActionIsPass :=FALSE;
		ADS_stuAssmDataResponse.strActionResultText :='';

		PLC_stuAssmDataRequest.bulRunning :=FALSE;
		PLC_stuAssmDataRequest.bulDoPositiveAction :=FALSE;
		PLC_stuAssmDataRequest.bulDoNegativeAction :=FALSE;
		PLC_stuAssmDataRequest.strActionScheduleName :='';
		(*!!!!!The End: please goto Step Home Position !!!!!!!*);



(**===========================================================================**)
(** For Example to use PLC_stuEndTestRequest1 & ADS_stuEndTestResponse1---Example4**)
(**===========================================================================**)
	(*Step1   clear PLC_stuEndTestRequest1 & ADS_stuEndTestResponse1 , e.g. clear values, set ArticleSet etc.*)
		ADS_stuEndTestResponse1.bulPartReceived :=FALSE;
		ADS_stuEndTestResponse1.bulActionIsFail :=FALSE;
		ADS_stuEndTestResponse1.bulActionIsPass :=FALSE;
		ADS_stuEndTestResponse1.strActionResultText :='';

		PLC_stuEndTestRequest1.bulRunning :=FALSE;
		PLC_stuEndTestRequest1.bulDoPositiveAction :=FALSE;
		PLC_stuEndTestRequest1.bulDoNegativeAction :=FALSE;
		PLC_stuEndTestRequest1.strActionScheduleName :='';
		PLC_stuEndTestRequest1.stuActionArticleSet := emptyVariantInfo;

	(*Step2 set ScheduleModeName and ArticleSet*)
		(*PLC_stuEndTestRequest1.stuActionArticleSet := PLC_arrCarrierInfo[CarrierNr].stuVariantInfoSet ;*)


	(*Step3  judge ask EOL-PLC grab DUT and then do some actions, e.g. doing End Test*)
		PLC_stuEndTestRequest1.bulRunning :=TRUE;
		PLC_stuEndTestRequest1.bulDoPositiveAction :=TRUE ;
		(*Step+1 here*);

	(*Step4 wait for part beening taken away by robot controlled by EOL-PLC*)
	IF ADS_stuEndTestResponse1.bulPartReceived  THEN
		(*do some actions e.g. Let current carrier down and then go away  *);
	END_IF

	(*Ste5 wait for  result from  EOL-PLC*)
		IF ADS_stuEndTestResponse1.bulActionIsPass OR  ADS_stuEndTestResponse1.bulActionIsFail THEN
			PLC_stuEndTestRequest1.bulRunning :=FALSE;
			PLC_stuEndTestRequest1.bulDoPositiveAction := FALSE;
			PLC_stuEndTestRequest1.bulDoNegativeAction := FALSE;
			(*Step+1 here*);
		END_IF

	(*Step6   clear PLC_stuEndTestRequest1 & ADS_stuEndTestResponse1 again*)
		ADS_stuEndTestResponse1.bulPartReceived :=FALSE;
		ADS_stuEndTestResponse1.bulActionIsFail :=FALSE;
		ADS_stuEndTestResponse1.bulActionIsPass :=FALSE;
		ADS_stuEndTestResponse1.strActionResultText :='';

		PLC_stuEndTestRequest1.bulRunning :=FALSE;
		PLC_stuEndTestRequest1.bulDoPositiveAction :=FALSE;
		PLC_stuEndTestRequest1.bulDoNegativeAction :=FALSE;
		PLC_stuEndTestRequest1.strActionScheduleName :='';
		PLC_stuEndTestRequest1.stuActionArticleSet := emptyVariantInfo;
		(*!!!!!The End: please goto Step Home Position !!!!!!!*);

                 ����  ENIF
ro         "   STANDARD.LIB 5.6.98 11:03:02 @�_w5      CONCAT @                	   CTD @        	   CTU @        
   CTUD @           DELETE @           F_TRIG @        
   FIND @           INSERT @        
   LEFT @        	   LEN @        	   MID @           R_TRIG @           REPLACE @           RIGHT @           RS @        
   SEMA @           SR @        	   TOF @        	   TON @           TP @              Global Variables 0 @                          UE
E_I
E           2                ����������������  
             ����  anie sar        ����  ionoinia                      POUs                MAIN  !   ����           
   Data types                 StructDeviceInteraction                     StructRequestAction  %                  StructResponseAction  &                   StructVariantInfo  $   ����             Visualizations  ����              Global Variables                Global_Variables_ADS                      Variable_Configuration  	   ����                                                            �U                         	   localhost            P      	   localhost            P      	   localhost            P            ���