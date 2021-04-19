﻿Public Module INI_CONSTANTS
    Public Const CON_SECTION_SCHEDULE As String = "Schedule_Csv"
    Public Const CON_SECTION_ARTICLE As String = "Article_Csv"
    Public Const CON_SECTION_DEVICE As String = "Device"
    Public Const CON_KEYWORD_SELVARIANT As String = "SelVariant"
    Public Const CON_KEYWORD_SELSCHEDULE As String = "SelSchedule"
    Public Const CON_KEYWORD_SELLANGUAGE As String = "SelectedLanguage"
End Module

Public Module LOG_SYMBOL
    Private Const _DELIMITER As String = ";"
End Module

Public Enum enumLK_CSV_STATUS
    LK_CSV_WINDOWS_ERROR = -99
    LK_CSV_UNDEFINED_ERROR = -98
    LK_CSV_NOT_INITIALIZED = -97
    LK_CSV_ERROR = -9

    LK_CSV_ALIAS_FILE_NOT_AVAILABLE = -12
    LK_CSV_FILE_NOT_AVAILABLE = -11
    LK_CSV_MAPPER_NOT_AVAILABLE = -10
    LK_CSV_READY = 0
End Enum

Public Enum enumLK_FAILCODE
    LK_FAILCODE_NONE = 0
    LK_FAILCODE_BARCODE_INVALID = 1
    LK_FAILCODE_LINECONTROLLER_RESULT_FAIL = 2
    LK_FAILCODE_ARTICLE = 3
    LK_FAILCODE_BARCODEAppSettings = 4
End Enum

Public Enum enumLK_COLOR
    LK_COLOR_GREY = &HCDCDC8
    LK_COLOR_GREEN = &H32CDB4
    LK_COLOR_RED = &H9B
    LK_COLOR_ORANGE = &H37C3FA
    LK_COLOR_YELLOW = &HFFFF&
    LK_COLOR_BLUE = &H7D461E
    LK_COLOR_WHITE = &H80000005
    LK_COLOR_LIGHTGREY = &HEDEDEB
    LK_COLOR_LIGHTBLUE = &HEBE3D2
    LK_COLOR_BLACK = &H0
    LK_COLOR_NOMALBLUE = &HDC0000
    LK_COLOR_LIGHTRED = &HFF
End Enum

Public Enum enumLK_TEXT

    LK_TEXT_INIT = 10
    LK_TEXT_PLC_SERIAL = 12
    LK_TEXT_PLC_ARTICLE = 11
    LK_TEXT_PLC_CUSTOM = 13
    LK_TEXT_PLC_CHECK = 14
    LK_TEXT_HOME = 15
    LK_TEXT_DISPOSE = 16
    LK_TEXT_REFERENCE = 17
    LK_TEXT_CHANGEARTICLE = 18
    LK_TEXT_FAIL_IN_COUNTER_FILE = 19
    LK_TEXT_STEPHOME = 20
    LK_TEXT_STEPHOME_CMD = 21
    LK_TEXT_PLC_SIGNAL = 22


    LK_TEXT_ARTICLE_INIT = 100
    LK_TEXT_ARTICLE_GETARTICLE_FROMID = 101
    LK_TEXT_ARTICLE_GETARTICLE_FROMID_FAIL = 102
    LK_TEXT_ARTICLE_INDICATE = 103
    LK_TEXT_ARTICLE_CHECKELMENT = 104

    LK_TEXT_READBASE_ADD = 110
    LK_TEXT_READBASE_GETELEMENT = 111
    LK_TEXT_READBASE_GETINDICATE = 112
    LK_TEXT_READBASE_SOURCE = 113

    LK_TEXT_ARTICLEREAD_INIT = 120
    LK_TEXT_ARTICLEREAD_ELEMENT_CHECK = 121
    LK_TEXT_BASE_ELEMENT_CHECK = 122

    LK_TEXT_SCHEDULE_INIT = 130

    LK_TEXT_SCHEDULEREAD_INIT = 140

    LK_TEXT_LANGUAGE_FILE = 200
    LK_TEXT_LANGUAGE_SELECTED = 201
    LK_TEXT_LANGUAGE_SELECTED_INVALID = 202
    LK_TEXT_LANGUAGE_INIT = 203

    LK_TEXT_REFERENCE_INIT = 210
    LK_TEXT_REFERENCE_FAMILY = 211
    LK_TEXT_REFERENCE_REMOVE = 212
    LK_TEXT_REFERENCE_ADD = 213
    LK_TEXT_REFERENCE_REMOVE_SCHEDULE = 214

    LK_TEXT_RETEST_INIT = 220
    LK_TEXT_RETEST_ADD = 221
    LK_TEXT_RETEST_REMOVE = 222

    LK_TEXT_ShirtCOUNTER_INIT = 230
    LK_TEXT_ShirtCOUNTER_WRITE = 231
    LK_TEXT_ShirtCOUNTER_ADD = 232
    LK_TEXT_ShirtCOUNTER_CHANGE = 233

    LK_TEXT_SURFACECOUNTER_INIT = 240
    LK_TEXT_SURFACECOUNTER_INIT_PASS = 241
    LK_TEXT_SURFACECOUNTER_ADD = 242
    LK_TEXT_SURFACECOUNTER_RESET = 243
    LK_TEXT_SURFACECOUNTER_CHANGE = 244

    LK_TEXT_WT_RESET = 250

    LK_TEXT_SHOWPIC_INIT = 260
    LK_TEXT_SHOWPIC_COUNT = 261
    LK_TEXT_SHOWPIC_ADD = 262

    LK_TEXT_CAQ_INIT_PASS = 910
    LK_TEXT_CAQ_INIT_FAIL = 911
    LK_TEXT_CAQ_START = 912
    LK_TEXT_CAQ_RUN = 913
    LK_TEXT_CAQ_ENDPASS = 914
    LK_TEXT_CAQ_ENDFAIL = 915

    LK_TEXT_STARTCOUNTER = 1000
    LK_TEXT_COUNTERADDPASS = 1001
    LK_TEXT_COUNTERADDFAIL = 1002
    LK_TEXT_COUNTERENDPASS = 1003
    LK_TEXT_COUNTERENDFAIL = 1004

    LK_TEXT_FAILPRINTER_INIT_PASS = 1010
    LK_TEXT_FAILPRINTER_INIT_FAIL = 1011
    LK_TEXT_FAILPRINTER_START = 1012
    LK_TEXT_FAILPRINTER_RUN = 1013
    LK_TEXT_FAILPRINTENDPASS = 1014
    LK_TEXT_FAILPRINTENDFAIL = 1015

    LK_TEXT_LASER_INIT_PASS = 1020
    LK_TEXT_LASER_INIT_FAIL = 1021
    LK_TEXT_LASER_START = 1022
    LK_TEXT_LASER_RESETRESPONSE = 1023
    LK_TEXT_LASER_GETSTATUS = 1024
    LK_TEXT_LASER_GETSTATUSREADY = 1025
    LK_TEXT_LASER_SET_TEMPLATE = 1026
    LK_TEXT_LASER_SET_TEMPLATEREADY = 1027
    LK_TEXT_LASER_SET_COMMAND = 1028
    LK_TEXT_LASER_SET_COMMANDREADY = 1029
    LK_TEXT_LASERENDPASS = 1030
    LK_TEXT_LASERENDFAIL = 1031
    LK_TEXT_LASER_DEFINE = 1032
    LK_TEXT_LASER_START_GETSTATUS = 1033
    LK_TEXT_LASER_START_TEMPLATE = 1034
    LK_TEXT_LASER_START_CMD = 1035
    LK_TEXT_LASER_START_GETVAR = 1036
    LK_TEXT_LASER_SET_GETVARREADY = 1037
    LK_TEXT_LASER_START_MARK = 1038
    LK_TEXT_LASER_SET_START_MARKREADY = 1039

    LK_TEXT_LINECONTROL_INIT_PASS = 1040
    LK_TEXT_LINECONTROL_INIT_FAIL = 1041
    LK_TEXT_LINECONTROL_START = 1042
    LK_TEXT_LINECONTROL_COUNTER = 1043
    LK_TEXT_LINECONTROL_COUNTER_END = 1044
    LK_TEXT_LINECONTROL_PRINT = 1045
    LK_TEXT_LINECONTROL_PRINT_END = 1046
    LK_TEXT_LINECONTROL_READ = 1047
    LK_TEXT_LINECONTROL_READRESULTPASS = 1048
    LK_TEXT_LINECONTROL_READRESULTFAIL = 1049
    LK_TEXT_LINECONTROL_WRITE = 1050
    LK_TEXT_LINECONTROL_WRITERESULTPASS = 1051
    LK_TEXT_LINECONTROL_WRITERESULTFAIL = 1052
    LK_TEXT_LINECONTROLENDPASS = 1053
    LK_TEXT_LINECONTROLENDFAIL = 1054
    LK_TEXT_LINECONTROL_CHILD = 1055
    LK_TEXT_LINECONTROL_DEFINE = 1056
    LK_TEXT_LINECONTROL_CAQ = 1057
    LK_TEXT_LINECONTROL_CAQ_END = 1058
    LK_TEXT_LINECONTROL_SaveStation = 1059


    LK_TEXT_MANUAL_START = 1060
    LK_TEXT_MANUAL_SCAN = 1061
    LK_TEXT_MANUAL_SCANRESULT = 1062
    LK_TEXT_MANUAL_DEFINE = 1063
    LK_TEXT_MANUAL_LINECONTROL = 1064
    LK_TEXT_MANUAL_LINECONTROLEND = 1065
    LK_TEXT_MANUALENDPASS = 1066
    LK_TEXT_MANUALENDFAIL = 1067
    LK_TEXT_MANUAL_SCANMSGSN = 1068
    LK_TEXT_MANUAL_SCANMSGRESULT = 1069

    LK_TEXT_CHECKSN_INIT_PASS = 1070
    LK_TEXT_CHECKSN_INIT_FAIL = 1071
    LK_TEXT_NEWPART_MODE = 1072
    LK_TEXT_NEWPART_ARTICLE = 1073
    LK_TEXT_SN_EXIST = 1074
    LK_TEXT_SN_CREATE = 1075
    LK_TEXT_NEWPART_WAIT = 1076
    LK_TEXT_NEWPART_START = 1077
    LK_TEXT_NEWPART_PRINT = 1078
    LK_TEXT_NEWPART_PRINT_END = 1079
    LK_TEXT_NEWPART_LINECONTROL = 1080
    LK_TEXT_NEWPART_LINECONTROL_END = 1081
    LK_TEXT_NEWPART_REFERENCE_SCAN = 1082
    LK_TEXT_NEWPART_REFERENCE_LK = 1083
    LK_TEXT_NEWPART_RETEST_SCAN = 1084
    LK_TEXT_NEWPARTENDPASS = 1085
    LK_TEXT_NEWPARTENDFAIL = 1086
    LK_TEXT_NEWPARTVARIANT = 1087
    LK_TEXT_SN_CREAT_NEW = 1088
    LK_TEXT_SN_CHECK = 1089
    LK_TEXT_SN_ENDCHECK = 1090
    LK_TEXT_SN_SAVE = 1091
    LK_TEXT_SN_ENDSAVE = 1092

    LK_TEXT_NEWPART_MSG1 = 1300
    LK_TEXT_NEWPART_MSG2 = 1301
    LK_TEXT_NEWPART_MSG3 = 1302
    LK_TEXT_NEWPART_MSG4 = 1303
    LK_TEXT_NEWPART_MSG5 = 1304
    LK_TEXT_NEWPART_MSG6 = 1305
    LK_TEXT_NEWPART_MSG7 = 1306
    LK_TEXT_NEWPART_MSG8 = 1307
    LK_TEXT_NEWPART_MSG9 = 1308
    LK_TEXT_NEWPART_MSG10 = 1309
    LK_TEXT_NEWPART_MSG11 = 1310
    LK_TEXT_NEWPART_MSG12 = 1311
    LK_TEXT_NEWPART_MSG13 = 1312
    LK_TEXT_NEWPART_MSG14 = 1313
    LK_TEXT_NEWPART_MSG15 = 1314
    LK_TEXT_NEWPART_MSG16 = 1315
    LK_TEXT_NEWPART_MSG17 = 1316
    LK_TEXT_NEWPART_MSG18 = 1317
    LK_TEXT_NEWPART_MSG19 = 1318
    LK_TEXT_NEWPART_MSG20 = 1319

    LK_TEXT_PRINT_INIT_PASS = 1100
    LK_TEXT_PRINT_INIT_FAIL = 1101
    LK_TEXT_PRINT_START = 1102
    LK_TEXT_PRINT_DEFINE = 1103
    LK_TEXT_PRINT_DEFINE_VALUE = 1104
    LK_TEXT_PRINT_SENDDATA = 1105
    LK_TEXT_PRINT_END = 1106
    LK_TEXT_PRINTENDPASS = 1107
    LK_TEXT_PRINTENDFAIL = 1108
    LK_TEXT_PRINT_MASK = 1109
    LK_TEXT_PRINT_CALIBRATION_START = 1110
    LK_TEXT_PRINT_CALIBRATION_WAIT = 1111


    LK_TEXT_REFERENCE_INIT_PASS = 1120
    LK_TEXT_REFERENCE_INIT_FAIL = 1121
    LK_TEXT_REFERENCE_START = 1122
    LK_TEXT_REFERENCE_SN = 1123
    LK_TEXT_REFERENCE_ERROR = 1124
    LK_TEXT_REFERENCE_PLEASE_INSERT = 1125
    LK_TEXT_REFERENCE_PLEASE_INSERT2 = 1126
    LK_TEXT_REFERENCE_SCAN = 1127
    LK_TEXT_REFERENCE_SCAN_RESULT = 1128
    LK_TEXT_REFERENCE_SCAN_DEFINE = 1129
    LK_TEXT_REFERENCE_SCAN_PASS = 1130
    LK_TEXT_REFERENCE_SCAN_FAIL = 1131
    LK_TEXT_REFERENCEENDPASS = 1132
    LK_TEXT_REFERENCEENDFAIL = 1133
    LK_TEXT_REFERENCEENDCHANGEFAIL = 1134
    LK_TEXT_REFERENCEENDCHANGEFAILENABLE = 1135
    LK_TEXT_REFERENCEENDCHANGESTEPFAIL = 1136

    LK_TEXT_RETEST_START = 1140
    LK_TEXT_RETEST_SCAN = 1141
    LK_TEXT_RETEST_SCAN_RESULT = 1142
    LK_TEXT_RETEST_SCAN_DEFINE = 1143
    LK_TEXT_RETEST_LINECONTROL = 1144
    LK_TEXT_RETESTENDPASS = 1145
    LK_TEXT_RETESTENDFAIL = 1146
    LK_TEXT_RETEST_NEWPART = 1147
    LK_TEXT_RETEST_RESULT = 1148
    LK_TEXT_RETEST_RETEST_DEFINE = 1149

    LK_SCANNER_WRONG_TYPE = 1160
    LK_SCANNER_INIT_FAIL = 1161
    LK_SCANNER_INIT_PASS = 1162
    LK_TEXT_SCAN_START = 1163
    LK_TEXT_MANUALSCAN_START = 1164
    LK_TEXT_SCAN_TRIGON = 1165
    LK_TEXT_SCAN_TRIGOFF = 1166
    LK_TEXT_SCAN_RESULT = 1167
    LK_TEXT_SCAN_DEFINE = 1168
    LK_TEXT_SCAN_LINECONTROL = 1169
    LK_TEXT_SCANENDPASS = 1170
    LK_TEXT_SCANENDFAIL = 1171
    LK_TEXT_BARCODERECEIVE = 1172
    LK_TEXT_BARCODERECEIVEFALSE = 1173
    LK_TEXT_SCAN_TIMEOUT = 1174

    LK_TEXT_SHOWPIC_START = 1180
    LK_TEXT_SHOWPIC = 1181
    LK_TEXT_SHOWPICENDPASS = 1182
    LK_TEXT_SHOWPICENDFAIL = 1183
    LK_TEXT_SHOWPIC_FAIL = 1184
    LK_TEXT_SHOWPIC_DEFINE = 1185
    LK_TEXT_SHOWPIC_FAIL_ADD = 1186

    LK_TEXT_STARTARTICLE = 1190
    LK_TEXT_ENDARTICLE = 1191

    LK_TEXT_STARTCREATSN = 1200

    LK_TEXT_Flash_INIT_PASS = 1210
    LK_TEXT_Flash_INIT_FAIL = 1211
    LK_TEXT_Flash_START = 1212
    LK_TEXT_Flash_DEFINE = 1213
    LK_TEXT_Flash_SET_COMMANDREADY1 = 1214
    LK_TEXT_Flash_SET_COMMANDREADY2 = 1215
    LK_TEXT_Flash_START_CMD = 1216
    LK_TEXT_Flash_LINECONTROL = 1217
    LK_TEXT_Flash_Response = 1218


    LK_TEXT_REF_MSG1 = 1340
    LK_TEXT_REF_MSG2 = 1341
    LK_TEXT_REF_MSG3 = 1342
    LK_TEXT_REF_MSG4 = 1343
    LK_TEXT_REF_MSG5 = 1344
    LK_TEXT_REF_MSG6 = 1345
    LK_TEXT_REF_MSG7 = 1346
    LK_TEXT_REF_MSG8 = 1347

    LK_TEXT_REF_MSG9 = 1348
    LK_TEXT_REF_MSG10 = 1349
    LK_TEXT_REF_MSG11 = 1350
    LK_TEXT_REF_MSG12 = 1351
    LK_TEXT_REF_MSG13 = 1352
    LK_TEXT_REF_MSG14 = 1353


    LK_TEXT_REF_MSG15 = 1354
    LK_TEXT_REF_MSG16 = 1355
    LK_TEXT_REF_MSG17 = 1356
    LK_TEXT_REF_MSG18 = 1357
    LK_TEXT_REF_MSG19 = 1358
    LK_TEXT_REF_MSG20 = 1359
    LK_TEXT_REF_MSG21 = 1360
    LK_TEXT_REF_MSG22 = 1361
    LK_TEXT_REF_MSG23 = 1362

    LK_TEXT_REF_MSG24 = 1363
    LK_TEXT_REF_MSG25 = 1364
    LK_TEXT_REF_MSG26 = 1365
    LK_TEXT_REF_MSG27 = 1366
    LK_TEXT_REF_MSG28 = 1367
    LK_TEXT_REF_MSG29 = 1368

    LK_TEXT_SHOWPIC_MSG3 = 1370
    LK_TEXT_SHOWPIC_MSG4 = 1371

    LK_TEXT_Alrm_MSG1 = 1400
    LK_TEXT_Alrm_MSG2 = 1401

    LK_LC_INIT1 = 1500
    LK_LC_INIT2 = 1501
    LK_LC_INIT3 = 1502
    LK_LC_INIT4 = 1503
    LK_LC_INIT5 = 1504
    LK_LC_INIT6 = 1505
    LK_LC_INIT7 = 1506
    LK_LC_INIT8 = 1507
    LK_LC_INIT9 = 1508
    LK_LC_INIT10 = 1509
    LK_LC_INIT11 = 1510
    LK_LC_INIT12 = 1511
    LK_LC_INIT13 = 1512
    LK_LC_INIT14 = 1513
    LK_LC_INIT15 = 1514
    LK_LC_INIT16 = 1515
    LK_LC_INIT17 = 1516
    LK_LC_INIT18 = 1517
    LK_LC_INIT19 = 1518
    LK_LC_INIT20 = 1519

    LK_LC_CURRENT1 = 1530
    LK_LC_CURRENT2 = 1531
    LK_LC_CURRENT3 = 1532
    LK_LC_CURRENT4 = 1533
    LK_LC_CURRENT5 = 1534
    LK_LC_CURRENT6 = 1535
    LK_LC_CURRENT7 = 1536
    LK_LC_CURRENT8 = 1537
    LK_LC_CURRENT9 = 1538
    LK_LC_CURRENT10 = 1539

    LK_LC_PREVIOUS1 = 1540
    LK_LC_PREVIOUS2 = 1541
    LK_LC_PREVIOUS3 = 1542
    LK_LC_PREVIOUS4 = 1543
    LK_LC_PREVIOUS5 = 1544
    LK_LC_PREVIOUS6 = 1545
    LK_LC_PREVIOUS7 = 1546
    LK_LC_PREVIOUS8 = 1547
    LK_LC_PREVIOUS9 = 1548
    LK_LC_PREVIOUS10 = 1549
    LK_LC_PREVIOUS11 = 1550
    LK_LC_LAN = 1551
    LK_LC_Button = 1552


    LK_LC_WRITE1 = 1560
    LK_LC_WRITE2 = 1561
    LK_LC_WRITE3 = 1562
    LK_LC_WRITE4 = 1563
    LK_LC_WRITE5 = 1564
    LK_LC_WRITE6 = 1565
    LK_LC_WRITE7 = 1566
    LK_LC_WRITE8 = 1567
    LK_LC_WRITE9 = 1568
    LK_LC_WRITE10 = 1569

    LK_LASER_ERROR1 = 1580
    LK_LASER_ERROR2 = 1581
    LK_LASER_ERROR3 = 1582
    LK_LASER_ERROR4 = 1583
    LK_LASER_ERROR5 = 1584
    LK_LASER_ERROR6 = 1585
    LK_LASER_ERROR7 = 1586
    LK_LASER_ERROR8 = 1587
    LK_LASER_ERROR9 = 1588
    LK_LASER_ERROR10 = 1589

    LK_DEVICE_ERROR1 = 1600
    LK_DEVICE_ERROR2 = 1601
    LK_DEVICE_ERROR3 = 1602
    LK_DEVICE_ERROR4 = 1603
    LK_DEVICE_ERROR5 = 1604
    LK_DEVICE_ERROR6 = 1605
    LK_DEVICE_ERROR7 = 1606
    LK_DEVICE_ERROR8 = 1607
    LK_DEVICE_ERROR9 = 1608
    LK_DEVICE_ERROR10 = 1609

    LK_EPSON_EEROR = 1610

    LK_ZEBRA_ERROR1 = 1620
    LK_ZEBRA_ERROR2 = 1621
    LK_ZEBRA_ERROR3 = 1622
    LK_ZEBRA_ERROR4 = 1623
    LK_ZEBRA_ERROR5 = 1624
    LK_ZEBRA_ERROR6 = 1625
    LK_ZEBRA_ERROR7 = 1626
    LK_ZEBRA_ERROR8 = 1627
    LK_ZEBRA_ERROR9 = 1628
    LK_ZEBRA_ERROR10 = 1629

    LK_TWINCAT_ERROR1 = 1640
    LK_TWINCAT_ERROR2 = 1641
    LK_TWINCAT_ERROR3 = 1642
    LK_TWINCAT_ERROR4 = 1643
    LK_TWINCAT_ERROR5 = 1644
    LK_TWINCAT_ERROR6 = 1645
    LK_TWINCAT_ERROR7 = 1646
    LK_TWINCAT_ERROR8 = 1647
    LK_TWINCAT_ERROR9 = 1648
    LK_TWINCAT_ERROR10 = 1649
    LK_TWINCAT_ERROR11 = 1650
    LK_TWINCAT_ERROR12 = 1651
    LK_TWINCAT_ERROR13 = 1652
End Enum
