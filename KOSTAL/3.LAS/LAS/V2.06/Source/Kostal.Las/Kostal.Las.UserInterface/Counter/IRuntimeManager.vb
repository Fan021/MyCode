'Imports Kostal.Testman.Plugin.Base

''' <summary>
''' The runtime manager to fetch runtime instances of classes
''' </summary>
Public Interface IRuntimeManager

    'ReadOnly Property RuntimeParameters As RuntimeParameters

    ''' <summary>
    ''' Register and Resolves a certain class or interface from or to runtime container.
    ''' </summary>
    ''' <typeparam name="T"></typeparam><returns></returns>
    Function Resolve(Of T)() As T
  Function Resolve(ByVal type As Type) As Object

    'Sub RegisterType(Of TFrom, TTo As TFrom)(ByVal lifetime As LifetimeMethod)
    Sub RegisterInstance(Of T)(ByVal instanceObject As T)
    'Sub RegisterPlugin(ByVal plugin As Global.Kostal.Testman.Plugin.Base.PluginBase, ByVal name As String)

    Function IsRegistered(Of T)() As Boolean

  Function IsRegistered(Of T)(id As String) As Boolean

End Interface