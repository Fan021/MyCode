Imports System.Runtime.Serialization

''' <summary>
''' Simple container to keep count of passed and failed testruns.
''' Used for serialization
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public Class CounterEntity
  Implements ISerializable

  Const Delimiter As Char = ","c

  Private _pass As Integer
  Private _fail As Integer



  ''' <summary>
  ''' Initializes a new instance of the <see cref="CounterEntity" /> class.
  ''' Pass and Fail properties both have a value of zero.
  ''' </summary>
  Public Sub New()
  End Sub

  ''' <summary>
  ''' Initializes a new instance of CounterEntity with serialized data.
  ''' </summary>
  ''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
  ''' <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/>" that contains contextual information about the source or destination.</param>
  ''' <exception cref="ArgumentNullException">The info parameter is null.</exception>
  ''' <exception cref="SerializationException">The class name is null or <see cref="System.Exception.HResult"/>" is zero (0).</exception>
  ''' <remarks></remarks>
  Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
    _Pass = info.GetInt32("Pass")
    _Fail = info.GetInt32("Fail")
  End Sub


  ''' <summary>
  ''' Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
  ''' </summary>
  ''' <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
  ''' <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
  ''' <exception cref="T:System.Security.SecurityException">
  ''' The caller does not have the required permission.
  '''   </exception>
  Public Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
    info.AddValue("Fail", Me.Fail)
    info.AddValue("Pass", Me.Pass)
  End Sub

  ''' <summary>
  ''' Gets or sets the value indicating the count of successful tests.
  ''' </summary>
  ''' <value>count of successful tests</value>
  ''' <returns>new count of successful tests.</returns>
  ''' <remarks></remarks>
  Public Property Pass() As Integer
    Get
      Return _Pass
    End Get
    Set(ByVal value As Integer)
      _Pass = value
    End Set
  End Property

  ''' <summary>
  ''' Gets or sets the value indicating the count of failed tests.
  ''' </summary>
  ''' <value>count of failed tests</value>
  ''' <returns>new count of failed tests.</returns>
  ''' <remarks></remarks>
  Public Property Fail() As Integer
    Get
      Return _Fail
    End Get
    Set(ByVal value As Integer)
      _Fail = value
    End Set
  End Property


  ''' <summary>
  ''' Returns a <see cref="System.String" /> that represents this instance.
  ''' </summary><returns>
  ''' A <see cref="System.String" /> that represents this instance.
  ''' </returns>
  Public Overrides Function ToString() As String

    Dim sb As New System.Text.StringBuilder

    sb.Append("Pass:")
    sb.Append(Pass.ToString)
    sb.Append(Delimiter)
    sb.Append("Fail:")
    sb.Append(Fail.ToString)

    Return sb.ToString

  End Function


End Class


