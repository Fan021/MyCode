Imports System.Windows.Forms
Imports Kostal.Las.ArticleProvider

Public Class Schedule
    Inherits ArticleBase
    Public Event IDManualChange(ByVal mID As String)
    Public Const Name As String = "_mAppShedule"

    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language, Optional ByVal ComboBox As ComboBox = Nothing)
        MyBase.New(MyStation, _AppSettings, MyLanguage, New ScheduleReader(MyStation, _AppSettings, MyLanguage), ComboBox)
        _ClassName = "Schedule"
    End Sub

    Public Overloads Function Init() As Boolean
        If Not _AritcleReader.Init() Then
            Return False
        End If
        _ArticleListElements = _AritcleReader.ArticleListElements
        _ArticleElements = _AritcleReader.ArticleElements

        ReadArticleLanguage()

        If Not IsNothing(_ComboBox) Then
            _ComboBox.Items.Clear()
            If _ArticleListElements.Count <> 0 Then
                For Each _Element In _ArticleListElements
                    _ComboBox.Items.Add(_Element.Value.IndicatedNativeName + "(" + _Element.Value.IndicatedName + ")")
                Next '
            End If
        End If

        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCHEDULE_INIT, "Successful"), "Schedule.Init")
        Return True
    End Function


    Public Overloads Function ReadArticleLanguage() As Boolean
        Dim mElement As KeyValuePair(Of String, ArticleElement)
        Dim Result As String = ""

        For Each mElement In _ArticleElements
            _ArticleElements.Item(mElement.Key).Name = _Language.Read(_AritcleReader.mSelection, mElement.Key)   '_FileHandler.ReadLanguageFile(AppSettings.LngFolder, _Language.SelectedLanguageFileName, CON_SECTION_ARTICLE, mElement.Key)
            If _ArticleElements.Item(mElement.Key).Name.ToUpper.Contains("ERROR") Then
                _ArticleElements.Item(mElement.Key).Name = mElement.Key
            End If
        Next


        For Each mElementList In _ArticleListElements
            Result = _Language.Read(_AritcleReader.mSelection, mElementList.Value.IndicatedName)
            If Result <> FileHandler.s_DEFAULT Then mElementList.Value.IndicatedNativeName = Result
            If Result = FileHandler.s_DEFAULT Then mElementList.Value.IndicatedNativeName = mElementList.Value.IndicatedName
        Next

        For Each _Element In ArticleListElement
            GetArticle_FromID(_Element.Value.ID)
            _Element.Value.UserVerification = GetUserVerificationByString(_ArticleElements(KostalScheduleKeys.KEY_USER_VERIFICATION).Data)
            _Element.Value.SchedulePriority = GetSchedulePriority(_Element.Value.UserVerification, _Element.Value.IndicatedName)
        Next
        Return True
    End Function

    '获取密码
    Protected Function GetUserVerificationByString(ByVal strUserVerification As String) As StructUserVerification

        Dim strText() As String = strUserVerification.Split("="c)
        Dim stuUserVeri As StructUserVerification
        Dim strFirstRes As String = strText(0).Trim.ToUpper

        If strText.Length = 1 And strFirstRes.Contains("ARTICLE") Then
            stuUserVeri.VerificationType = enumUserVerificationType.ARTICLE_OCCUPIED
            stuUserVeri.Password = ""
        ElseIf strText.Length = 1 And strFirstRes.Contains("PLC") Then
            stuUserVeri.VerificationType = enumUserVerificationType.PLC_OCCUPIED
            stuUserVeri.Password = ""
        ElseIf strText.Length = 1 And strFirstRes.Contains("PASSWORD") Then
            stuUserVeri.VerificationType = enumUserVerificationType.PASSWORD_APPLICATION
            stuUserVeri.Password = _xmlHande.GetSectionInformation(AppSettings.ApplicationFolder, AppSettings.RootIniName, "Password", "UserPassWord")
        ElseIf strText.Length > 1 And strFirstRes.Contains("PASSWORD") Then
            stuUserVeri.VerificationType = enumUserVerificationType.PASSWORD_USERDEFINED
            stuUserVeri.Password = strText(1).Trim
        Else
            stuUserVeri.VerificationType = enumUserVerificationType.NULL_VERIFICATION
            stuUserVeri.Password = ""
        End If

        Return stuUserVeri
    End Function


    Protected Function GetSchedulePriority(ByVal strUserVerification As StructUserVerification, ByVal strIndicatedName As String) As enumSchedulePriority
        Dim stuSchedulePriority As enumSchedulePriority
        If strUserVerification.VerificationType = enumUserVerificationType.PLC_OCCUPIED Then
            stuSchedulePriority = enumSchedulePriority.PLC
        End If

        If strUserVerification.VerificationType = enumUserVerificationType.ARTICLE_OCCUPIED Then
            stuSchedulePriority = enumSchedulePriority.Article
        End If

        If strUserVerification.VerificationType = enumUserVerificationType.PASSWORD_USERDEFINED Then
            stuSchedulePriority = enumSchedulePriority.Manual
        End If

        If strUserVerification.VerificationType = enumUserVerificationType.PASSWORD_APPLICATION Then
            stuSchedulePriority = enumSchedulePriority.Manual
        End If
        If strUserVerification.VerificationType = enumUserVerificationType.NULL_VERIFICATION Then
            stuSchedulePriority = enumSchedulePriority.Manual
        End If

        If strIndicatedName.IndexOf(ProductionMode.SelfResistance.ToString) >= 0 Or strIndicatedName.IndexOf(ProductionMode.MasterPart.ToString) >= 0 Or _ArticleElements(KostalScheduleKeys.KEY_REFERENCE_SCHEDULE).Data.ToUpper = "TRUE" Then
            stuSchedulePriority = enumSchedulePriority.REF
        End If

        Return stuSchedulePriority
    End Function

    Public Function ManualChange(ByVal strID As String) As Boolean
        RaiseEvent IDManualChange(strID)
        Return True
    End Function

End Class

