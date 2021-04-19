Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Schema
Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv

    Public Class XmlConfigurationBuilder

        Private Const [Namespace] As String = "http://Kostal.com/Las/CsvProviderParametrisation"

        Public Const CsvProviderSchemaResourceName As String = "Kostal.Las.ArticleProvider.LasCsvProvider.xsd"
        Public Const IniFilePropertyName As String = "IniFile"

        Public Enum AttributeName
            ' ReSharper disable InconsistentNaming
            parameterName
            columnName
            onEmptyColumn
            show
            value
            type
            name
            allowEmpty
            articleProperty
            scheduleName         'added by wang65
            ' ReSharper restore InconsistentNaming
        End Enum

        Public Enum NodeName
            Mappings
            AdditionalGlobalParameter
            Assignment
            ConditionalParameter
            ConditionalIniFile
            ConditionalArticleParameter
            ConditionalCustomParameter
            DefaultAssignment
            Condition
            Conditions
            ColumnName
            MatchValue
            RegexMatchValue
            ArticlePropertyMapping
            CustomPropertyMapping
            IniFileMapping
            SimpleMapping
            ArticleIndexMapping
            ArticleNumberMapping
            DateMapping
            BooleanColumnMapping
            TrueValueDescription
            FalseValueDescription
            KeyColumnMapping
            UseFirstColumnAsKey
            UsingSpecificKeyColumn
            StringModification
            StringComparison
            AcceptsEmptyField
            MultipleAssignment
            Value
            Values
            ArticleScheduleMapping          'added by wang65
            UsingSpecificScheduleMode        'added by wang65
        End Enum

        Public Enum ConditionType
            [And]
            [Or]
        End Enum

        Private _currentConfig As ReaderConfiguration

        Public Sub New(ByVal csvFile As String, ByVal xmlDescriptionFile As String)

            'Check csv existence
            If Not File.Exists(csvFile) Then
                Throw New FileNotFoundException("The referenced csv file is not existing.", csvFile)
            End If

            'check config xml exists
            If Not File.Exists(xmlDescriptionFile) Then
                Throw New FileNotFoundException("The referenced xml description file is not existing.", xmlDescriptionFile)
            End If

            'load and verify xml document
            Dim descriptionFile As XDocument = XDocument.Load(xmlDescriptionFile)

            ValidateDescriptionFile(descriptionFile)

            'Create config root
            Dim cfg = ConfigurationFactory.ConfigureUsingFile(csvFile)

            'Add content of mappings node
            Dim incompleteCfg = CreateKeyMapping(cfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            ' Set the OnEmptyColumnOption
            SetGlobalEmptyColumnOption(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'Add content of article mappings
            CreateArticlePropertyMappings(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'add content of article schedule mapping
            CreateScheduleMapping(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'Add content of custom mappings
            CreateCustomPropertyMappings(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'Add content of IniFile mappings
            '  CreateIniFileMappings(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'Add content of AdditionalGlobalParameter
            CreateAdditionalGlobalParameters(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.AdditionalGlobalParameter.ToString, [Namespace])))

            'Add content of ConditionalParameter
            CreateConditionalParameters(incompleteCfg, descriptionFile.Root.Element(XName.Get(NodeName.ConditionalParameter.ToString, [Namespace])))

            _currentConfig = incompleteCfg.UseAdditionalColumnsAsCustomEntries()

        End Sub

        Public Function GetConfig() As ReaderConfiguration
            Return _currentConfig
        End Function

        Private Sub CreateConditionalParameters(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal conditionalParamNode As XElement)

            If conditionalParamNode Is Nothing Then
                Return
            End If

            ' for each column
            For Each param As XElement In conditionalParamNode.Elements()

                Dim name As String = param.Attribute(XName.Get(AttributeName.parameterName.ToString))
                If String.CompareOrdinal(param.Name.LocalName, NodeName.ConditionalIniFile.ToString) = 0 Then
                    name = IniFilePropertyName
                End If

                ' check all Assignment nodes
                For Each assignment In param.Elements(XName.Get(NodeName.Assignment.ToString, [Namespace]))
                    Dim m As Mapping.Mapping = CreateConditionalParameter(assignment)

                    ' Consider 'show' attribute
                    Dim showAttr = param.Attribute(XName.Get(AttributeName.show.ToString))
                    If Not showAttr Is Nothing Then
                        m.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                    End If

                    AddMappingToArticleAttribute(name, m, incompleteConfiguration)
                Next assignment

                ' check all MultipleAssignment nodes
                For Each assignment In param.Elements(XName.Get(NodeName.MultipleAssignment.ToString, [Namespace]))
                    Dim m As IEnumerable(Of Mapping.Mapping) = CreateConditionalMultipleParameter(assignment)

                    ' Consider 'show' attribute
                    Dim showAttr = param.Attribute(XName.Get(AttributeName.show.ToString))
                    For Each map In m.ToArray
                        If Not showAttr Is Nothing Then
                            map.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                        End If
                        AddMappingToArticleAttribute(name, map, incompleteConfiguration)
                    Next

                Next assignment

                'Default column
                Dim defaultAssignment = param.Element(XName.Get(NodeName.DefaultAssignment.ToString, [Namespace]))
                If Not defaultAssignment Is Nothing Then
                    Throw New NotImplementedException("The 'DefaultAssgignment tag is not yet supported.")
                End If
            Next param

        End Sub

        Private Function CreateConditionalParameter(ByVal singleAssignment As XElement) As Mapping.Mapping

            '  Assignment
            Dim assignedValue As String = singleAssignment.Attribute(XName.Get(AttributeName.value.ToString)).Value

            Dim conditionItems As New List(Of Condition)
            Dim type As String = ConditionType.And.ToString

            Dim condition = singleAssignment.Element(XName.Get(NodeName.Condition.ToString, [Namespace]))
            If Not condition Is Nothing Then
                conditionItems.Add(ParseSingleCondition(condition))
            End If

            Dim conditions = singleAssignment.Element(XName.Get(NodeName.Conditions.ToString, [Namespace]))
            If Not conditions Is Nothing Then
                type = conditions.Attribute(XName.Get(AttributeName.type.ToString))

                For Each condition In conditions.Elements(XName.Get(NodeName.Condition.ToString, [Namespace]))
                    conditionItems.Add(ParseSingleCondition(condition))
                Next

            End If

            If String.Compare(type, ConditionType.And.ToString, StringComparison.InvariantCultureIgnoreCase) = 0 Then
                Return New ConditionalMappingAnd(assignedValue, conditionItems.ToArray())
            Else
                Return New ConditionalMappingOr(assignedValue, conditionItems.ToArray())
            End If

        End Function

        Private Function CreateConditionalMultipleParameter(ByVal singleAssignment As XElement) As IEnumerable(Of Mapping.Mapping)

            Dim mappings As New List(Of Mapping.Mapping)
            Dim conditionItems As New List(Of Condition)
            Dim type As String = ConditionType.And.ToString

            Dim condition = singleAssignment.Element(XName.Get(NodeName.Condition.ToString, [Namespace]))
            If Not condition Is Nothing Then
                conditionItems.Add(ParseSingleCondition(condition))
            End If

            Dim conditions = singleAssignment.Element(XName.Get(NodeName.Conditions.ToString, [Namespace]))
            If Not conditions Is Nothing Then
                type = conditions.Attribute(XName.Get(AttributeName.type.ToString))

                For Each condition In conditions.Elements(XName.Get(NodeName.Condition.ToString, [Namespace]))
                    conditionItems.Add(ParseSingleCondition(condition))
                Next
            End If

            mappings.Clear()
            Dim valuesNode = singleAssignment.Element(XName.Get(NodeName.Values.ToString, [Namespace]))
            For Each valueNode In valuesNode.Elements(XName.Get(NodeName.Value.ToString, [Namespace]))
                If String.Compare(type, ConditionType.And.ToString, StringComparison.InvariantCultureIgnoreCase) = 0 Then
                    mappings.Add(New ConditionalMappingAnd(valueNode.Value, conditionItems.ToArray()))
                Else
                    mappings.Add(New ConditionalMappingOr(valueNode.Value, conditionItems.ToArray()))
                End If
            Next

            Return mappings

        End Function

        Private Function ParseSingleCondition(ByVal conditionNode As XElement) As Condition
            Dim columnName As String = conditionNode.Element(XName.Get(NodeName.ColumnName.ToString, [Namespace])).Value

            If conditionNode.Element(XName.Get(NodeName.MatchValue.ToString, [Namespace])) IsNot Nothing Then
                Dim matchValue As String = conditionNode.Element(XName.Get(NodeName.MatchValue.ToString, [Namespace])).Value

                Return New Condition(New CsvColumnMapping(columnName), matchValue)
            Else
                Dim regex As New Text.RegularExpressions.Regex(conditionNode.Element(XName.Get(NodeName.RegexMatchValue.ToString, [Namespace])).Value)

                Return New Condition(New CsvColumnMapping(columnName), regex)
            End If

        End Function

        Private Sub CreateAdditionalGlobalParameters(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal globalParameterNode As XElement)

            ' check whether global parameters are defined
            If globalParameterNode Is Nothing Then
                Return
            End If

            For Each param As XElement In globalParameterNode.Elements()

                If String.CompareOrdinal(param.Name.LocalName, IniFilePropertyName) = 0 Then
                    AddMappingToArticleAttribute(IniFilePropertyName, New ConstantValueMapping(param.Value), incompleteConfiguration)
                Else

                    Dim m As ConstantValueMapping = New ConstantValueMapping(param.Value)

                    ' Consider 'show' attribute
                    Dim showAttr = param.Attribute(XName.Get(AttributeName.show.ToString))
                    If Not showAttr Is Nothing Then
                        m.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                    End If

                    Dim name = param.Attribute(XName.Get(AttributeName.name.ToString))

                    AddMappingToArticleAttribute(name, m, incompleteConfiguration)
                End If
            Next param

        End Sub

        Private Sub CreateArticlePropertyMappings(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            ' testman article properties
            For Each articleMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.ArticlePropertyMapping.ToString, [Namespace]))
                CreateArticlePropertyMapping(incompleteConfiguration, articleMapping)
            Next

        End Sub

        Private Sub CreateCustomPropertyMappings(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            For Each customMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.CustomPropertyMapping.ToString, [Namespace]))
                CreateCustomPropertyMapping(incompleteConfiguration, customMapping)
            Next

        End Sub

        Private Sub CreateIniFileMappings(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            ' testman article properties
            For Each iniFileMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.IniFileMapping.ToString, [Namespace]))
                CreateIniFileMapping(incompleteConfiguration, iniFileMapping)
            Next

        End Sub

        Private Sub CreateCustomPropertyMapping(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)

            Dim columnNameAttr = singleNode.Attribute(XName.Get(AttributeName.columnName.ToString))
            Dim m As Mapping.Mapping

            m = New CsvColumnMapping(columnNameAttr.Value)

            ' Consider OnEmptyColumnOption
            Dim onEmptyColumnAttr = singleNode.Attribute(XName.Get(AttributeName.onEmptyColumn.ToString))
            If onEmptyColumnAttr Is Nothing Then
                m.EmptyColumnOption = incompleteConfiguration.EmptyColumnOption ' use global option
            Else
                m.EmptyColumnOption = ParseOnEmptyColumnOption(onEmptyColumnAttr.Value) ' use property specific option
            End If

            ' Consider 'show' attribute
            Dim showAttr = singleNode.Attribute(XName.Get(AttributeName.show.ToString))
            If Not showAttr Is Nothing Then
                m.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
            End If

            AddMappingToArticleAttribute(columnNameAttr.Value, m, incompleteConfiguration)
        End Sub

        Private Sub CreateScheduleMapping(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            Dim node = mappingsNode.Element(XName.Get(NodeName.ArticleScheduleMapping.ToString, [Namespace]))
            If IsNothing(node) Then Return

            Dim articleProperty = node.Attribute(XName.Get(AttributeName.articleProperty.ToString)).Value

            Dim mElement As XElement = node.Element(XName.Get(NodeName.UsingSpecificScheduleMode.ToString, [Namespace]))

            If Not mElement Is Nothing Then

                Dim modificationStrategy = ParseStringModificationStrategy(mElement)
                If modificationStrategy.HasValue Then
                    'Modification strategy present -> use it
                Else
                    'No Modification strategy present -> take the first colum
                End If

                Dim attrib = mElement.Attribute(XName.Get(AttributeName.scheduleName.ToString))
                Dim m As ConstantValueMapping = New ConstantValueMapping(attrib.Value)

                AddMappingToArticleAttribute(articleProperty, m, incompleteConfiguration)
            Else

                Dim m As Mapping.CsvColumnMapping = CreateSimplePropertyMapping(node.Element(XName.Get(NodeName.SimpleMapping.ToString, [Namespace])))

                AddMappingToArticleAttribute(articleProperty, m, incompleteConfiguration)
            End If

        End Sub
        Private Sub CreateArticlePropertyMapping(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)

            Dim m As Mapping.Mapping

            ' Consider mapping type
            Dim articleProperty = singleNode.Attribute(XName.Get(AttributeName.articleProperty.ToString)).Value

            If singleNode.Element(XName.Get(NodeName.SimpleMapping.ToString, [Namespace])) IsNot Nothing Then
                m = CreateSimplePropertyMapping(singleNode.Element(XName.Get(NodeName.SimpleMapping.ToString, [Namespace])))
            ElseIf singleNode.Element(XName.Get(NodeName.ArticleIndexMapping.ToString, [Namespace])) IsNot Nothing Then
                m = CreateArticleIndexPropertyMapping(singleNode.Element(XName.Get(NodeName.ArticleIndexMapping.ToString, [Namespace])))
            ElseIf singleNode.Element(XName.Get(NodeName.ArticleNumberMapping.ToString, [Namespace])) IsNot Nothing Then
                m = CreateArticleNoPropertyMapping(singleNode.Element(XName.Get(NodeName.ArticleNumberMapping.ToString, [Namespace])))
            ElseIf singleNode.Element(XName.Get(NodeName.DateMapping.ToString, [Namespace])) IsNot Nothing Then
                m = CreateDatePropertyMapping(singleNode.Element(XName.Get(NodeName.DateMapping.ToString, [Namespace])))
            Else
                m = CreateBooleanPropertyMapping(singleNode.Element(XName.Get(NodeName.BooleanColumnMapping.ToString, [Namespace])))
            End If

            ' Consider attributes
            Dim onEmptyColumnAttr = singleNode.Attribute(XName.Get(AttributeName.onEmptyColumn.ToString))
            If onEmptyColumnAttr Is Nothing Then
                ' use global option
                m.EmptyColumnOption = incompleteConfiguration.EmptyColumnOption
            Else
                ' use property specific option
                m.EmptyColumnOption = ParseOnEmptyColumnOption(onEmptyColumnAttr.Value)
            End If

            AddMappingToArticleAttribute(articleProperty, m, incompleteConfiguration)
        End Sub

        Private Sub CreateIniFileMapping(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)

            Dim allowEmptyAttr = singleNode.Attribute(XName.Get(AttributeName.allowEmpty.ToString))
            Dim columnNameAttr = singleNode.Attribute(XName.Get(AttributeName.columnName.ToString))
            Dim m As Mapping.Mapping

            m = New CsvColumnMapping(columnNameAttr.Value)

            ' consider allowEmpty flag
            If allowEmptyAttr Is Nothing Then
                m.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.NullEntry  ' take also NullEntries --> will cause an exception in case of empty field
            Else
                If XmlConvert.ToBoolean(allowEmptyAttr.Value) Then
                    m.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.SkipField ' ignore this IniFile mapping --> no loading of any IniFile
                Else
                    m.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.NullEntry ' take also NullEntries --> will cause an exception in case of empty field
                End If
            End If

            AddMappingToArticleAttribute(IniFilePropertyName, m, incompleteConfiguration)
        End Sub

        Private Function CreateBooleanPropertyMapping(ByVal boolNode As XElement) As Mapping.Mapping
            Dim columnName = boolNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value

            Dim trueValue = boolNode.Element(XName.Get(NodeName.TrueValueDescription.ToString, [Namespace])).Value
            Dim trueMapping = New BooleanValueDescription(trueValue)

            Dim falseValue = boolNode.Element(XName.Get(NodeName.FalseValueDescription.ToString, [Namespace])).Value
            Dim falseMapping = New BooleanValueDescription(falseValue)

            Dim modificationStrategy = ParseStringModificationStrategy(boolNode)
            If modificationStrategy.HasValue Then
                trueMapping.Strategy = modificationStrategy.Value
                falseMapping.Strategy = modificationStrategy.Value
            End If

            Dim stringComparison = ParseStringStringComparisonOptions(boolNode)
            If stringComparison.HasValue Then
                trueMapping.ComparisonOption = stringComparison.Value
                falseMapping.ComparisonOption = stringComparison.Value
            End If

            Dim allowEmpty = ParseAcceptsEmptyFieldOptions(boolNode)
            If allowEmpty.HasValue Then
                trueMapping.AcceptsEmptyString = allowEmpty.Value
                falseMapping.AcceptsEmptyString = allowEmpty.Value
            End If


            Return New CsvColumnBooleanMapping(columnName, trueMapping, falseMapping)

        End Function

        Private Function CreateDatePropertyMapping(ByVal dateNode As XElement) As Mapping.Mapping
            Dim columnName = dateNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy = ParseStringModificationStrategy(dateNode)

            If modificationStrategy.HasValue Then
                Return New CsvColumnDateMapping(columnName, modificationStrategy.Value)
            Else
                Return New CsvColumnDateMapping(columnName)
            End If
        End Function

        Private Function CreateArticleNoPropertyMapping(ByVal articleNoNode As XElement) As Mapping.Mapping
            Dim columnName = articleNoNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy = ParseStringModificationStrategy(articleNoNode)

            If modificationStrategy.HasValue Then
                Return New CsvColumnArticleNumberMapping(columnName, modificationStrategy.Value)
            Else
                Return New CsvColumnArticleNumberMapping(columnName)
            End If
        End Function

        Private Function CreateArticleIndexPropertyMapping(ByVal articleIndexNode As XElement) As Mapping.Mapping
            Dim columnName = articleIndexNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy = ParseStringModificationStrategy(articleIndexNode)

            If modificationStrategy.HasValue Then
                Return New CsvColumnArticleIndexMapping(columnName, modificationStrategy.Value)
            Else
                Return New CsvColumnArticleIndexMapping(columnName)
            End If
        End Function

        Private Function CreateSimplePropertyMapping(ByVal simpeNode As XElement) As Mapping.Mapping
            Dim columnName = simpeNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy = ParseStringModificationStrategy(simpeNode)

            If modificationStrategy.HasValue Then
                Return New CsvColumnMapping(columnName, modificationStrategy.Value)
            Else
                Return New CsvColumnMapping(columnName)
            End If
        End Function

        Private Function CreateKeyMapping(ByVal csvFileConfiguration As CsvFileConfiguration, ByVal mappingsNode As XElement) As IncompleteConfiguration

            Dim node = mappingsNode.Element(XName.Get(NodeName.KeyColumnMapping.ToString, [Namespace]))

            Dim mapping As XElement = node.Element(XName.Get(NodeName.UseFirstColumnAsKey.ToString, [Namespace]))

            'First column is key
            If Not mapping Is Nothing Then
                Dim modificationStrategy = ParseStringModificationStrategy(mapping)
                If modificationStrategy.HasValue Then
                    'Modification strategy present -> use it
                    Return csvFileConfiguration.UsingFirstColumnAsKey(modificationStrategy.Value)
                Else
                    'No Modification strategy present -> take the first column
                    Return csvFileConfiguration.UsingFirstColumnAsKey()
                End If
            Else
                'specific column as key
                mapping = node.Element(XName.Get(NodeName.UsingSpecificKeyColumn.ToString, [Namespace]))
                'column name
                Dim csvColumn = mapping.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
                'modification strategy
                Dim modificationStrategy = ParseStringModificationStrategy(mapping)
                If modificationStrategy.HasValue Then
                    'Modification strategy present -> use it
                    Return csvFileConfiguration.UsingSpecificKeyColumn(New CsvColumnMapping(csvColumn, modificationStrategy))
                Else
                    'No Modification strategy present -> take the first column
                    Return csvFileConfiguration.UsingSpecificKeyColumn(New CsvColumnMapping(csvColumn))
                End If
            End If

        End Function

        Private Function ParseStringModificationStrategy(ByVal parentNode As XElement) As StringModification?

            Dim node As XElement = parentNode.Element(XName.Get(NodeName.StringModification.ToString, [Namespace]))
            If Not node Is Nothing Then
                Return CType([Enum].Parse(GetType(StringModification), node.Value), StringModification)
            Else
                Return Nothing
            End If

        End Function

        Private Function ParseStringStringComparisonOptions(ByVal parentNode As XElement) As StringComparison?

            Dim node As XElement = parentNode.Element(XName.Get(NodeName.StringComparison.ToString, [Namespace]))
            If Not node Is Nothing Then
                Return CType([Enum].Parse(GetType(StringComparison), node.Value), StringComparison)
            Else
                Return Nothing
            End If

        End Function

        Private Function ParseAcceptsEmptyFieldOptions(ByVal parentNode As XElement) As Boolean?

            Dim node As XElement = parentNode.Element(XName.Get(NodeName.AcceptsEmptyField.ToString, [Namespace]))
            If Not node Is Nothing Then
                Return XmlConvert.ToBoolean(node.Value)
            Else
                Return Nothing
            End If

        End Function

        Private Sub AddMappingToArticleAttribute(ByVal attributeName As String, ByVal mapping As Mapping.Mapping, ByVal config As IncompleteConfiguration)

            If [Enum].GetNames(GetType(ArticleAttribute)).Contains(attributeName) Then
                config.SetArticleProperty([Enum].Parse(GetType(ArticleAttribute), attributeName), mapping)
                '  ElseIf [Enum].GetNames(GetType(ArticleAttribute)).Contains(attributeName) Then
                '    config.SetKostalArticleProperty([Enum].Parse(GetType(ArticleAttribute), attributeName), mapping)
            ElseIf attributeName.Equals(IncompleteConfiguration.TestmanFlowFilePropertyName) Then
                config.SetTestmanFlowFile(mapping)
            Else
                If TypeOf mapping Is ConstantValueMapping Then
                    config.SetCustomField(attributeName, CType(mapping, ConstantValueMapping))
                ElseIf TypeOf mapping Is CsvColumnMapping Then
                    config.SetCustomField(attributeName, CType(mapping, CsvColumnMapping))
                ElseIf TypeOf mapping Is ConditionalMappingAnd Then
                    config.SetCustomField(attributeName, CType(mapping, ConditionalMappingAnd))
                ElseIf TypeOf mapping Is ConditionalMappingOr Then
                    config.SetCustomField(attributeName, CType(mapping, ConditionalMappingOr))
                Else
                    Throw New ArgumentException("Invalid mapping type!")
                End If
            End If
        End Sub

        Private Sub SetGlobalEmptyColumnOption(ByVal csvFileConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            Dim node = mappingsNode.Attribute(XName.Get(AttributeName.onEmptyColumn.ToString))

            If (Not node Is Nothing) AndAlso (ParseOnEmptyColumnOption(node.Value) = Mapping.Mapping.OnEmptyColumnOption.SkipField) Then
                csvFileConfiguration.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.SkipField
            Else
                csvFileConfiguration.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.NullEntry
            End If

        End Sub

        Shared Function ParseOnEmptyColumnOption(ByVal optionString As String) As Mapping.Mapping.OnEmptyColumnOption

            ' find matching option
            For Each v In [Enum].GetValues(GetType(Mapping.Mapping.OnEmptyColumnOption))
                If String.CompareOrdinal(optionString, v.ToString) = 0 Then
                    Return v
                End If
            Next v

            ' matching option not found --> use "Unknown"
            Return Mapping.Mapping.OnEmptyColumnOption.Unknown

        End Function

        Private Sub ValidateDescriptionFile(ByVal configuration As XDocument)

            Dim resourceStream As Stream = Assembly.GetExecutingAssembly.GetManifestResourceStream(CsvProviderSchemaResourceName)
            If resourceStream Is Nothing Then
                Throw New XmlSchemaValidationException(String.Format("Cannot open the resource '{0}' in the assembly '{1}'.", CsvProviderSchemaResourceName, Assembly.GetExecutingAssembly.FullName))
            End If

            Dim schema As XmlSchema
            Try
                Dim reader As XmlReader = XmlTextReader.Create(resourceStream)
                schema = XmlSchema.Read(reader, Nothing)
            Catch ex As Exception
                Throw New XmlSchemaValidationException(String.Format("Cannot find or access the specified resource '{0}'!", CsvProviderSchemaResourceName), ex)
            End Try

            Dim schemas As New XmlSchemaSet()
            schemas.Add(schema)

            configuration.Validate(schemas, AddressOf XsdErrors)

        End Sub

        Private Sub XsdErrors(ByVal o As Object, ByVal e As ValidationEventArgs)
            Throw New XmlSchemaValidationException(String.Format("Error validating description file. {0}", e.Message))
        End Sub



    End Class
End Namespace