Public Class SafeDictionary(Of TKey, TValue)
    Implements IDictionary(Of TKey, TValue)
    Private ReadOnly syncRoot As New Object()

    Private ReadOnly d As Dictionary(Of TKey, TValue) = New Dictionary(Of TKey, TValue)

    Public Sub Add(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Add
        SyncLock syncRoot
            CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).Add(item)
        End SyncLock
    End Sub

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Clear

        SyncLock syncRoot
            CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).Clear()
        End SyncLock

    End Sub

    Public Function Contains(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Contains
        SyncLock syncRoot
            Return CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).Contains(item)
        End SyncLock
    End Function

    Public Sub CopyTo(ByVal array() As System.Collections.Generic.KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).CopyTo
        SyncLock syncRoot
            CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).CopyTo(array, arrayIndex)
        End SyncLock
    End Sub

    Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Count
        Get
            SyncLock syncRoot
                Return CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).Count
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).IsReadOnly
        Get
            SyncLock syncRoot
                Return CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).IsReadOnly
            End SyncLock
        End Get
    End Property

    Public Function Remove(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Remove
        SyncLock syncRoot
            Return CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).Remove(item)
        End SyncLock
    End Function

    Public Sub Add(ByVal key As TKey, ByVal value As TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Add
        SyncLock syncRoot
            d.Add(key, value)
        End SyncLock
    End Sub

    Public Function ContainsKey(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).ContainsKey
        SyncLock syncRoot
            Return d.ContainsKey(key)
        End SyncLock
    End Function

    Default Public Property Item(ByVal key As TKey) As TValue Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Item
        Get
            SyncLock syncRoot
                Return d(key)
            End SyncLock
        End Get
        Set(ByVal value As TValue)
            SyncLock syncRoot
                d(key) = value
            End SyncLock
        End Set
    End Property

    

    Public ReadOnly Property Keys As System.Collections.Generic.ICollection(Of TKey) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Keys
        Get
            SyncLock syncRoot
                Return d.Keys
            End SyncLock
        End Get
    End Property

    Public Function Remove(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Remove
        SyncLock syncRoot
            Return d.Remove(key)
        End SyncLock
    End Function

    Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).TryGetValue
        SyncLock syncRoot
            Return d.TryGetValue(key, value)
        End SyncLock
    End Function

    Public ReadOnly Property Values As System.Collections.Generic.ICollection(Of TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Values
        Get
            SyncLock syncRoot
                SyncLock syncRoot
                    Return d.Values
                End SyncLock
            End SyncLock
        End Get
    End Property


    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        SyncLock syncRoot
            Return d.GetEnumerator
        End SyncLock
    End Function

    Public Function GetEnumerator1() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).GetEnumerator
        SyncLock syncRoot
            Return CType(d, System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue))).GetEnumerator
        End SyncLock
    End Function
End Class
