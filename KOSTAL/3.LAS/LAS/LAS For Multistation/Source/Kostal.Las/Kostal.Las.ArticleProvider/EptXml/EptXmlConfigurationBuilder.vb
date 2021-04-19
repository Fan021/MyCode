Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Schema
Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Ept.Mapping
Imports Kostal.Las.ArticleProvider.Ept.Fluent

Namespace Ept
    Public Class EptXmlConfigurationBuilder

        Private Const [Namespace] As String = "http://Kostal.com/Las/CsvProviderParametrisation"

        Public Const EptXmlProviderSchemaResourceName As String = "Kostal.Las.ArticleProvider.LasCsvProvider.xsd"
        Public Const EptXmlOutputSchemaResourceName As String = "Kostal.Las.ArticleProvider.EptXmlOutput.xsd"

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
            delimiter
            index
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
            ArticleRowElementMapping
        End Enum

        Public Enum ConditionType
            [And]
            [Or]
        End Enum

        Private _currentConfig As ReaderConfiguration


        Public Sub New(ByVal xmlFile As String, ByVal xmlMappingFile As String)

            ' check article xml
            If Not File.Exists(xmlFile) Then
                Throw New FileNotFoundException("Referenced EPT xml file not found", xmlFile)
            End If

            ' check mapping xml
            If Not File.Exists(xmlMappingFile) Then
                Throw New FileNotFoundException("Referenced xml mapping file not found", xmlMappingFile)
            End If

            ' load and verify xml-formatted EPT output document
            Dim eptXml As XDocument = XDocument.Load(xmlFile)
            ValidateEptXmlOutputFile(eptXml)

            ' load and verify mapping document
            Dim mappingXml As XDocument = XDocument.Load(xmlMappingFile)
            ValidateMappingFile(mappingXml)

            ' create config root
            Dim cfg As IncompleteConfiguration = ConfigurationFactory.ConfigureUsingFile(xmlFile)

            ' Set the OnEmptyEptPropertyOption
            SetGlobalOnEptEmptyPropertyOption(cfg, mappingXml.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))
            ' Add content of row element mappings
            CreateTestmanRowElementMappings(cfg, mappingXml.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            ' Add content of Testman property mappings
            CreateTestmanPropertyMappings(cfg, mappingXml.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'add content of article schedule mapping
            CreateScheduleMapping(cfg, mappingXml.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            ' Add content of global ini files
            '  CreateTestmanIniFileMappings(cfg, mappingXml.Root.Element(XName.Get(NodeName.Mappings.ToString, [Namespace])))

            'Add content of AdditionalGlobalParameter
            CreateGlobalProperties(cfg, mappingXml.Root.Element(XName.Get(NodeName.AdditionalGlobalParameter.ToString, [Namespace])))

            'Add content of ConditionalParameter
            CreateConditionalProperties(cfg, mappingXml.Root.Element(XName.Get(NodeName.ConditionalParameter.ToString, [Namespace])))

            _currentConfig = cfg.UseAdditionalColumnsAsCustomEntries()

        End Sub

        Public Function GetConfig() As ReaderConfiguration
            Return _currentConfig
        End Function

        Private Sub CreateConditionalProperties(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal conditionalParamNode As XElement)

            If conditionalParamNode Is Nothing Then
                Return
            End If

            ' for each property
            For Each param As XElement In conditionalParamNode.Elements()
                Dim name As String

                Select Case param.Name.LocalName
                    Case Is = NodeName.ConditionalIniFile.ToString
                        name = IncompleteConfiguration.TestmanFlowFilePropertyName
                    Case Is = NodeName.ConditionalArticleParameter.ToString
                        name = param.Attribute(XName.Get(AttributeName.parameterName.ToString)).Value
                    Case Is = NodeName.ConditionalCustomParameter.ToString
                        name = param.Attribute(XName.Get(AttributeName.parameterName.ToString)).Value
                    Case Else
                        name = String.Empty
                End Select

                ' check all Assignment nodes
                For Each assignment As XElement In param.Elements(XName.Get(NodeName.Assignment.ToString, [Namespace]))
                    Dim m As Mapping.Mapping = CreateConditionalParameter(assignment)

                    ' Consider 'show' attribute
                    Dim showAttr As XAttribute = param.Attribute(XName.Get(AttributeName.show.ToString))
                    If Not showAttr Is Nothing Then
                        m.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                    End If

                    AddMappingToArticleAttribute(name, m, eptXmlConfiguration)
                Next assignment

                ' check all MultipleAssignment nodes
                For Each assignment As XElement In param.Elements(XName.Get(NodeName.MultipleAssignment.ToString, [Namespace]))
                    Dim m As IEnumerable(Of Mapping.Mapping) = CreateConditionalMultipleParameter(assignment)

                    ' Consider 'show' attribute
                    Dim showAttr As XAttribute = param.Attribute(XName.Get(AttributeName.show.ToString))
                    For Each map As Mapping.Mapping In m.ToArray
                        If Not showAttr Is Nothing Then
                            map.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                        End If
                        AddMappingToArticleAttribute(name, map, eptXmlConfiguration)
                    Next map

                    ' Default column
                    Dim defaultAssignment As XElement = param.Element(XName.Get(NodeName.DefaultAssignment.ToString, [Namespace]))
                    If Not defaultAssignment Is Nothing Then
                        Throw New NotImplementedException("The 'DefaultAssignment' tag is not yet supported.")
                    End If
                Next assignment
            Next param

        End Sub

        Private Function CreateConditionalMultipleParameter(ByVal singleAssignment As XElement) As IEnumerable(Of Mapping.Mapping)

            Dim mappings As New List(Of Mapping.Mapping)
            Dim conditionItems As New List(Of Condition)
            Dim type As String = ConditionType.And.ToString

            Dim condition As XElement = singleAssignment.Element(XName.Get(NodeName.Condition.ToString, [Namespace]))
            If Not condition Is Nothing Then
                conditionItems.Add(ParseSingleCondition(condition))
            End If

            Dim conditions As XElement = singleAssignment.Element(XName.Get(NodeName.Conditions.ToString, [Namespace]))
            If Not conditions Is Nothing Then
                type = conditions.Attribute(XName.Get(AttributeName.type.ToString)).Value

                For Each condition In conditions.Elements(XName.Get(NodeName.Condition.ToString, [Namespace]))
                    conditionItems.Add(ParseSingleCondition(condition))
                Next
            End If

            mappings.Clear()
            Dim valuesNode As XElement = singleAssignment.Element(XName.Get(NodeName.Values.ToString, [Namespace]))
            For Each valueNode As XElement In valuesNode.Elements(XName.Get(NodeName.Value.ToString, [Namespace]))
                If String.Compare(type, ConditionType.And.ToString, StringComparison.InvariantCultureIgnoreCase) = 0 Then
                    mappings.Add(New ConditionalMappingAnd(valueNode.Value, conditionItems.ToArray()))
                Else
                    mappings.Add(New ConditionalMappingOr(valueNode.Value, conditionItems.ToArray()))
                End If
            Next

            Return mappings

        End Function

        Private Function CreateConditionalParameter(ByVal singleAssignment As XElement) As Mapping.Mapping

            '  Assignment
            Dim assignedValue As String = singleAssignment.Attribute(XName.Get(AttributeName.value.ToString)).Value
            Dim conditionItems As New List(Of Condition)
            Dim type As String = ConditionType.And.ToString

            Dim condition As XElement = singleAssignment.Element(XName.Get(NodeName.Condition.ToString, [Namespace]))
            If Not condition Is Nothing Then
                conditionItems.Add(ParseSingleCondition(condition))
            End If

            Dim conditions As XElement = singleAssignment.Element(XName.Get(NodeName.Conditions.ToString, [Namespace]))
            If Not conditions Is Nothing Then
                type = conditions.Attribute(XName.Get(AttributeName.type.ToString)).Value

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

        Private Function ParseSingleCondition(ByVal conditionNode As XElement) As Condition
            Dim propertyName As String = conditionNode.Element(XName.Get(NodeName.ColumnName.ToString, [Namespace])).Value

            If conditionNode.Element(XName.Get(NodeName.MatchValue.ToString, [Namespace])) IsNot Nothing Then
                Dim matchValue As String = conditionNode.Element(XName.Get(NodeName.MatchValue.ToString, [Namespace])).Value

                Return New Condition(New XmlNodeMapping(propertyName), matchValue)
            Else
                Dim regex As New Text.RegularExpressions.Regex(conditionNode.Element(XName.Get(NodeName.RegexMatchValue.ToString, [Namespace])).Value)

                Return New Condition(New XmlNodeMapping(propertyName), regex)
            End If

        End Function

        Private Sub CreateGlobalProperties(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal globalParameterNode As XElement)

            ' check whether global parameters are defined
            If globalParameterNode Is Nothing Then
                Return
            End If

            For Each param As XElement In globalParameterNode.Elements()

                If String.CompareOrdinal(param.Name.LocalName, NodeName.IniFileMapping.ToString) = 0 Then
                    AddMappingToArticleAttribute(IncompleteConfiguration.TestmanFlowFilePropertyName, New ConstantValueMapping(param.Value), eptXmlConfiguration)
                Else

                    Dim m As ConstantValueMapping = New ConstantValueMapping(param.Value)

                    ' Consider 'show' attribute
                    Dim showAttr As XAttribute = param.Attribute(XName.Get(AttributeName.show.ToString))
                    If Not showAttr Is Nothing Then
                        m.ShowInArticleSelector = XmlConvert.ToBoolean(showAttr.Value)
                    End If

                    Dim name As XAttribute = param.Attribute(XName.Get(AttributeName.name.ToString))

                    AddMappingToArticleAttribute(name.Value, m, eptXmlConfiguration)
                End If
            Next param

        End Sub

        Private Sub CreateTestmanPropertyMappings(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            For Each articleMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.ArticlePropertyMapping.ToString, [Namespace]))
                CreateTestmanPropertyMapping(eptXmlConfiguration, articleMapping)
            Next articleMapping

        End Sub

        Private Sub CreateTestmanIniFileMappings(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            For Each iniFileMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.IniFileMapping.ToString, [Namespace]))
                CreateTestmanIniFileMapping(eptXmlConfiguration, iniFileMapping)
            Next

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

                Dim m As Mapping.Mapping = CreateSimplePropertyMapping(node.Element(XName.Get(NodeName.SimpleMapping.ToString, [Namespace])))

                AddMappingToArticleAttribute(articleProperty, m, incompleteConfiguration)
            End If

        End Sub

        Private Sub CreateTestmanPropertyMapping(ByVal incompleteConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)

            Dim m As Mapping.Mapping

            ' consider mapping type
            Dim articleProperty As String = singleNode.Attribute(XName.Get(AttributeName.articleProperty.ToString)).Value

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

            ' consider attributes
            Dim onEmptyColumnAttr As XAttribute = singleNode.Attribute(XName.Get(AttributeName.onEmptyColumn.ToString))
            If onEmptyColumnAttr Is Nothing Then
                ' use global option
                m.EmptyEptPropertyOption = incompleteConfiguration.EmptyEptPropertyOption
            Else
                ' use property specific option
                m.EmptyEptPropertyOption = ParseOnEmptyEptPropertyOption(onEmptyColumnAttr.Value)
            End If

            AddMappingToArticleAttribute(articleProperty, m, incompleteConfiguration)
        End Sub

        Private Sub CreateTestmanIniFileMapping(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)

            Dim allowEmptyAttr As XAttribute = singleNode.Attribute(XName.Get(AttributeName.allowEmpty.ToString))
            Dim propertyNameAttr As XAttribute = singleNode.Attribute(XName.Get(AttributeName.articleProperty.ToString))
            Dim m As Mapping.Mapping

            m = New XmlNodeMapping(propertyNameAttr.Value)

            ' consider allowEmpty flag
            If allowEmptyAttr Is Nothing Then
                m.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.NullEntry  ' take also NullEntries --> will cause an exception in case of empty field
            Else
                If XmlConvert.ToBoolean(allowEmptyAttr.Value) Then
                    m.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.Skip ' ignore this IniFile mapping --> no loading of any IniFile
                Else
                    m.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.NullEntry ' take also NullEntries --> will cause an exception in case of empty field
                End If
            End If

            AddMappingToArticleAttribute(IncompleteConfiguration.TestmanFlowFilePropertyName, m, eptXmlConfiguration)
        End Sub

        Private Function CreateBooleanPropertyMapping(ByVal boolNode As XElement) As Mapping.Mapping
            Dim propertyName As String = boolNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value

            Dim trueValue As String = boolNode.Element(XName.Get(NodeName.TrueValueDescription.ToString, [Namespace])).Value
            Dim trueMapping As New BooleanValueDescription(trueValue)

            Dim falseValue As String = boolNode.Element(XName.Get(NodeName.FalseValueDescription.ToString, [Namespace])).Value
            Dim falseMapping As New BooleanValueDescription(falseValue)

            Dim modificationStrategy As Nullable(Of StringModification) = ParseStringModificationStrategy(boolNode)
            If modificationStrategy.HasValue Then
                trueMapping.Strategy = modificationStrategy.Value
                falseMapping.Strategy = modificationStrategy.Value
            End If

            Dim stringComparison As Nullable(Of StringComparison) = ParseStringStringComparisonOptions(boolNode)
            If stringComparison.HasValue Then
                trueMapping.ComparisonOption = stringComparison.Value
                falseMapping.ComparisonOption = stringComparison.Value
            End If

            Dim allowEmpty As Nullable(Of Boolean) = ParseAcceptsEmptyFieldOptions(boolNode)
            If allowEmpty.HasValue Then
                trueMapping.AcceptsEmptyString = allowEmpty.Value
                falseMapping.AcceptsEmptyString = allowEmpty.Value
            End If

            Return New XmlNodeBooleanMapping(propertyName, trueMapping, falseMapping)

        End Function

        Private Function CreateDatePropertyMapping(ByVal dateNode As XElement) As Mapping.Mapping
            Dim propertyName As String = dateNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy As Nullable(Of StringModification) = ParseStringModificationStrategy(dateNode)

            If modificationStrategy.HasValue Then
                Return New XmlNodeDateMapping(propertyName, modificationStrategy.Value)
            Else
                Return New XmlNodeDateMapping(propertyName)
            End If
        End Function

        Private Function CreateArticleNoPropertyMapping(ByVal articleNoNode As XElement) As Mapping.Mapping
            Dim propertyName As String = articleNoNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy As Nullable(Of StringModification) = ParseStringModificationStrategy(articleNoNode)

            If modificationStrategy.HasValue Then
                Return New XmlNodeMaterialMapping(propertyName, modificationStrategy.Value)
            Else
                Return New XmlNodeMaterialMapping(propertyName)
            End If
        End Function

        Private Function CreateArticleIndexPropertyMapping(ByVal articleIndexNode As XElement) As Mapping.Mapping
            Dim propertyName As String = articleIndexNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy As Nullable(Of StringModification) = ParseStringModificationStrategy(articleIndexNode)

            If modificationStrategy.HasValue Then
                Return New XmlNodeMaterialRevisionMapping(propertyName, modificationStrategy.Value)
            Else
                Return New XmlNodeMaterialRevisionMapping(propertyName)
            End If
        End Function

        Private Sub CreateTestmanRowElementMappings(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)
            For Each rowElementMapping As XElement In mappingsNode.Elements(XName.Get(NodeName.ArticleRowElementMapping.ToString, [Namespace]))
                CreateTestmanRowElementMapping(eptXmlConfiguration, rowElementMapping)
            Next

        End Sub
        Private Sub CreateTestmanRowElementMapping(eptXmlConfiguration As IncompleteConfiguration, ByVal singleNode As XElement)
            Dim articleProperty As String = singleNode.Attribute(XName.Get(AttributeName.articleProperty.ToString)).Value
            Dim delimiter As String = singleNode.Attribute(XName.Get(AttributeName.delimiter.ToString)).Value
            Dim indexString As String = singleNode.Attribute(XName.Get(AttributeName.index.ToString)).Value
            Dim index As Integer = Integer.Parse(indexString)
            Dim m As Mapping.Mapping

            m = New XmlNodeVariantRowMapping(articleProperty, delimiter, index)
            AddMappingToArticleAttribute(IncompleteConfiguration.TestmanRowElementPropertyName, m, eptXmlConfiguration)
        End Sub



        Private Function CreateSimplePropertyMapping(ByVal simpeNode As XElement) As Mapping.Mapping
            Dim propertyName As String = simpeNode.Attribute(XName.Get(AttributeName.columnName.ToString)).Value
            Dim modificationStrategy As Nullable(Of StringModification) = ParseStringModificationStrategy(simpeNode)

            If modificationStrategy.HasValue Then
                Return New XmlNodeMapping(propertyName, modificationStrategy.Value)
            Else
                Return New XmlNodeMapping(propertyName)
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
                config.SetCustomProperty(CType([Enum].Parse(GetType(ArticleAttribute), attributeName), ArticleAttribute), mapping)
                'ElseIf [Enum].GetNames(GetType(ArticleAttribute)).Contains(attributeName) Then
                '    config.SetTestmanProperty(CType([Enum].Parse(GetType(ArticleAttribute), attributeName), ArticleAttribute), mapping)
            ElseIf attributeName.Equals(IncompleteConfiguration.TestmanFlowFilePropertyName) Then
                config.SetTestmanFlowFile(mapping)
            ElseIf attributeName.Equals(IncompleteConfiguration.TestmanRowElementPropertyName) Then
                config.SetTestmanRowElement(mapping)
            Else
                If TypeOf mapping Is ConstantValueMapping Then
                    config.SetCustomField(attributeName, CType(mapping, ConstantValueMapping))
                ElseIf TypeOf mapping Is XmlNodeMapping Then
                    config.SetCustomField(attributeName, CType(mapping, XmlNodeMapping))
                ElseIf TypeOf mapping Is ConditionalMappingAnd Then
                    config.SetCustomField(attributeName, CType(mapping, ConditionalMappingAnd))
                ElseIf TypeOf mapping Is ConditionalMappingOr Then
                    config.SetCustomField(attributeName, CType(mapping, ConditionalMappingOr))
                Else
                    Throw New ArgumentException("Invalid mapping type!")
                End If
            End If
        End Sub

        Private Sub SetGlobalOnEptEmptyPropertyOption(ByVal eptXmlConfiguration As IncompleteConfiguration, ByVal mappingsNode As XElement)

            Dim node As XAttribute = mappingsNode.Attribute(XName.Get(AttributeName.onEmptyColumn.ToString))

            If (Not node Is Nothing) AndAlso (ParseOnEmptyEptPropertyOption(node.Value) = Mapping.Mapping.OnEmptyEptPropertyOption.Skip) Then
                eptXmlConfiguration.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.Skip
            Else
                eptXmlConfiguration.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.NullEntry
            End If

        End Sub

        Shared Function ParseOnEmptyEptPropertyOption(ByVal optionString As String) As Mapping.Mapping.OnEmptyEptPropertyOption

            ' find matching option
            For Each v As Mapping.Mapping.OnEmptyEptPropertyOption In [Enum].GetValues(GetType(Mapping.Mapping.OnEmptyEptPropertyOption))
                If String.CompareOrdinal(optionString, v.ToString) = 0 Then
                    Return v
                End If
            Next v

            ' matching option not found --> use "Unknown"
            Return Mapping.Mapping.OnEmptyEptPropertyOption.Unknown

        End Function

        Private Sub ValidateMappingFile(ByVal configuration As XDocument)

            Dim resourceStream As Stream = Assembly.GetExecutingAssembly.GetManifestResourceStream(EptXmlProviderSchemaResourceName)
            If resourceStream Is Nothing Then
                Throw New XmlSchemaValidationException(String.Format("Cannot open the resource '{0}' in the assembly '{1}'.", EptXmlProviderSchemaResourceName, Assembly.GetExecutingAssembly.FullName))
            End If

            Dim schema As XmlSchema
            Try
                Dim reader As XmlReader = XmlTextReader.Create(resourceStream)
                schema = XmlSchema.Read(reader, Nothing)
            Catch ex As Exception
                Throw New XmlSchemaValidationException(String.Format("Cannot find or access the specified resource '{0}'!", EptXmlProviderSchemaResourceName), ex)
            End Try

            Dim schemas As New XmlSchemaSet()
            schemas.Add(schema)

            configuration.Validate(schemas, AddressOf XsdErrors)

        End Sub

        Private Sub ValidateEptXmlOutputFile(ByVal eptXmlOutput As XDocument)

            Dim resourceStream As Stream = Assembly.GetExecutingAssembly.GetManifestResourceStream(EptXmlOutputSchemaResourceName)
            If resourceStream Is Nothing Then
                Throw New XmlSchemaValidationException(String.Format("Cannot open the resource '{0}' in the assembly '{1}'.", EptXmlOutputSchemaResourceName, Assembly.GetExecutingAssembly.FullName))
            End If

            Dim schema As XmlSchema
            Try
                Dim reader As XmlReader = XmlTextReader.Create(resourceStream)
                schema = XmlSchema.Read(reader, Nothing)
            Catch ex As Exception
                Throw New XmlSchemaValidationException(String.Format("Cannot find or access the specified resource '{0}'!", EptXmlOutputSchemaResourceName), ex)
            End Try

            Dim schemas As New XmlSchemaSet()
            schemas.Add(schema)

            eptXmlOutput.Validate(schemas, AddressOf XsdErrors)

        End Sub

        Private Sub XsdErrors(ByVal o As Object, ByVal e As ValidationEventArgs)
            Throw New XmlSchemaValidationException(String.Format("Error validating xml. {0}", e.Message))
        End Sub


    End Class
End Namespace