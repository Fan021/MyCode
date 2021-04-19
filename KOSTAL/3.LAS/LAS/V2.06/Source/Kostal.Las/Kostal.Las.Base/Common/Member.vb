Imports System
Imports System.Linq.Expressions
Imports System.Reflection

''' <summary>
''' Helper class for converting expressions to members.
''' </summary>
Public Module ExpressionMember
    ''' <summary>
    ''' Convert <see cref="MethodCallExpression"/> to <see cref="Member"/>
    ''' </summary>
    ''' <returns>An instance of <see cref="Member"/></returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function AsMethod(member As LambdaExpression) As Member
        Dim methodExpr As MethodCallExpression = TryCast(member.Body, MethodCallExpression)

        Return If(methodExpr IsNot Nothing, New Member(methodExpr.Method), Nothing)
    End Function

    ''' <summary>
    ''' Convert <see cref="MemberExpression"/> to <see cref="Member"/>
    ''' </summary>
    ''' <returns>An instance of <see cref="Member"/></returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function AsMember(member As LambdaExpression) As Member
        Dim memberExpr As MemberExpression = TryCast(member.Body, MemberExpression)

        Return If(memberExpr IsNot Nothing, New Member(memberExpr), Nothing)
    End Function

    ''' <summary>
    ''' Convert the member or method from <see cref="LambdaExpression"/> to <see cref="Member"/>
    ''' </summary>
    ''' <returns>An instance of <see cref="Member"/></returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function AsMemberOrMethod(member As LambdaExpression) As Member
        Return If(member.AsMember(), If(member.AsMethod(), member.AsUnaryWrappedMember()))
    End Function

    ''' <summary>
    ''' Convert the member or method from <see cref="System.Linq.Expressions.Expression"/> to <see cref="Member"/>
    ''' </summary>
    ''' <returns>An instance of <see cref="Member"/></returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function AsMemberOrMethod(member As Expression) As Member
        Dim memberExpr As MemberExpression = TryCast(member, MemberExpression)

        If memberExpr IsNot Nothing Then
            Return memberExpr.Member
        End If

        Dim methodExpr As MethodCallExpression = TryCast(member, MethodCallExpression)

        If methodExpr IsNot Nothing Then
            Return methodExpr.Method
        End If

        Dim unaryExpr As UnaryExpression = TryCast(member, UnaryExpression)

        If unaryExpr IsNot Nothing Then
            Return unaryExpr.Operand.AsMemberOrMethod()
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Convert the UnaryExpression from <see cref="LambdaExpression"/> to <see cref="Member"/>
    ''' </summary>
    ''' <returns>An instance of <see cref="Member"/></returns>
    <System.Runtime.CompilerServices.Extension> _
    Public Function AsUnaryWrappedMember(member As LambdaExpression) As Member
        Dim unaryExpr As UnaryExpression = TryCast(member.Body, UnaryExpression)

        If unaryExpr IsNot Nothing Then
            Return unaryExpr.Operand.AsMemberOrMethod()
        End If

        Return Nothing
    End Function
End Module

''' <summary>
''' Class for easily getting information about a type member,
''' and referencing the member by name, or getting it as a
''' <see cref="MemberInfo"/>, <see cref="PropertyInfo"/>,
''' <see cref="FieldInfo"/>, or <see cref="MethodInfo"/>.
''' </summary>
Public Class Member
    ''' <summary>
    ''' Initializes a new <see cref="Member"/> using the specified
    ''' <see cref="MemberExpression"/>.
    ''' </summary>
    ''' <param name="expression">The expression that references the desired member.</param>
    Public Sub New(expression As MemberExpression)
        Me.Info = expression.Member
    End Sub

    ''' <summary>
    ''' Initializes a new <see cref="Member"/> using the specified
    ''' <see cref="MemberInfo"/>.
    ''' </summary>
    ''' <param name="info">The member info that references the desired member.</param>
    Public Sub New(info As MemberInfo)
        Me.Info = info
    End Sub

    ''' <summary>
    ''' The <see cref="MemberInfo"/> discovered for the member specified.
    ''' </summary>
    Public Property Info() As MemberInfo
        Get
            Return m_Info
        End Get
        Private Set(value As MemberInfo)
            m_Info = value
        End Set
    End Property
    Private m_Info As MemberInfo

    ''' <summary>
    ''' Implicitly cast a <see cref="Member"/> to a <see cref="String"/>, using the
    ''' <see cref="ToString"/> method.
    ''' </summary>
    ''' <param name="member">The <see cref="Member"/> to cast to a string.</param>
    Public Shared Widening Operator CType(member As Member) As String
        Return member.ToString()
    End Operator

    ''' <summary>
    ''' Implicitly cast a <see cref="Member"/> to a <see cref="MemberInfo"/>, by
    ''' returning the <see cref="Info"/> property value.
    ''' </summary>
    ''' <param name="member">The <see cref="Member"/> to cast to a <see cref="MemberInfo"/>.</param>
    ''' <returns>The <see cref="Info"/> of the specified member, or <c>null</c>.</returns>
    Public Shared Widening Operator CType(member As Member) As MemberInfo
        Return If(member IsNot Nothing, member.Info, Nothing)
    End Operator

    ''' <summary>
    ''' Implicitly cast a <see cref="Member"/> to a <see cref="MethodInfo"/>, by
    ''' returning the <see cref="AsMethod"/> result.
    ''' </summary>
    ''' <param name="member">
    ''' The <see cref="Member"/> to cast to a <see cref="MethodInfo"/>.
    ''' </param>
    ''' <returns>A <see cref="MethodInfo"/> for the specified member.</returns>
    ''' <exception cref="InvalidCastException">
    ''' When the current member cannot be cast to a <see cref="MethodInfo"/>.
    ''' </exception>
    Public Shared Widening Operator CType(member As Member) As MethodInfo
        Return If(member IsNot Nothing, member.AsMethod(), Nothing)
    End Operator

    ''' <summary>
    ''' Implicitly cast a <see cref="MemberInfo"/> to a <see cref="Member"/>.
    ''' </summary>
    ''' <param name="memberInfo">The member info to use as a member.</param>
    ''' <returns>
    ''' A <see cref="MemberInfo"/> representing the specified <paramref name="memberInfo"/>.
    ''' </returns>
    Public Shared Widening Operator CType(memberInfo As MemberInfo) As Member
        Return New Member(memberInfo)
    End Operator

    ''' <summary>
    ''' Create a <see cref="Member"/> using a method call expression.
    ''' </summary>
    ''' <remarks>
    ''' This is used for <c>void</c> methods.
    ''' </remarks>
    ''' <param name="methodCall">The method call expression to use as a member.</param>
    ''' <returns>A <see cref="Member"/> for the specified method call expression.</returns>
    Public Shared Function [Of](methodCall As Expression(Of Action)) As Member
        Return methodCall.AsMethod()
    End Function

    ''' <summary>
    ''' Create a <see cref="Member"/> using a method call expression
    ''' for the specified type.
    ''' </summary>
    ''' <typeparam name="T">The type of the Action parameter</typeparam>
    ''' <param name="methodCall"></param>
    ''' <returns>A <see cref="Member"/> for the specified method call expression.</returns>
    Public Shared Function [Of](Of T)(methodCall As Expression(Of Action(Of T))) As Member
        Return methodCall.AsMethod()
    End Function

    ''' <summary>
    ''' Create a <see cref="Member"/> using a property, field, or non-void
    ''' method call expression.
    ''' </summary>
    ''' <param name="member">The member expression to use as a member.</param>
    ''' <returns>A <see cref="Member"/> for the specified member expression.</returns>
    Public Shared Function [Of](member As Expression(Of Func(Of Object))) As Member
        Return member.AsMemberOrMethod()
    End Function

    ''' <summary>
    ''' Create a <see cref="Member"/> using a property, field, or non-void
    ''' method call expression for the specified type.
    ''' </summary>
    ''' <typeparam name="T">The type containing the member specified.</typeparam>
    ''' <param name="member">The member expression to use as a member.</param>
    ''' <returns>A <see cref="Member"/> for the specified member expression.</returns>
    Public Shared Function [Of](Of T)(member As Expression(Of Func(Of T, Object))) As Member
        Return member.AsMemberOrMethod()
    End Function

    ''' <summary>
    ''' Gets the current member as a <see cref="PropertyInfo"/> instance.
    ''' </summary>
    ''' <returns>A <see cref="PropertyInfo"/> instance for the current member <see cref="Info"/>.</returns>
    ''' <exception cref="InvalidCastException">
    ''' When the current member cannot be cast to a <see cref="PropertyInfo"/>.
    ''' </exception>
    Public Function AsProperty() As PropertyInfo
        Return DirectCast(Me.Info, PropertyInfo)
    End Function

    ''' <summary>
    ''' Gets the current member as a <see cref="FieldInfo"/> instance.
    ''' </summary>
    ''' <returns>A <see cref="FieldInfo"/> instance for the current member <see cref="Info"/>.</returns>
    ''' <exception cref="InvalidCastException">
    ''' When the current member cannot be cast to a <see cref="FieldInfo"/>.
    ''' </exception>
    Public Function AsField() As FieldInfo
        Return DirectCast(Me.Info, FieldInfo)
    End Function

    ''' <summary>
    ''' Gets the current member as a <see cref="MethodInfo"/> instance.
    ''' </summary>
    ''' <returns>A <see cref="MethodInfo"/> instance for the current member <see cref="Info"/>.</returns>
    ''' <exception cref="InvalidCastException">
    ''' When the current member cannot be cast to a <see cref="MethodInfo"/>.
    ''' </exception>
    Public Function AsMethod() As MethodInfo
        Return DirectCast(Me.Info, MethodInfo)
    End Function

    ''' <summary>
    ''' Returns the current member name as the string representation of a <see cref="Member"/>.
    ''' </summary>
    ''' <returns>The name of the current member <see cref="Info"/>.</returns>
    Public Overrides Function ToString() As String
        Return Me.Info.Name
    End Function
End Class
