Public Class ResourceDescription

    Public Property image As String
    Public Property table As String
    Public Property styles As String
    Public Property text As String

    Public ReadOnly Property type As ResourceTypes
        Get
            If Not text.StringEmpty Then
                Return ResourceTypes.text
            ElseIf Not table.StringEmpty Then
                Return ResourceTypes.table
            Else
                Return ResourceTypes.image
            End If
        End Get
    End Property

    Public Function getResourceValue() As String
        Select Case type
            Case ResourceTypes.image : Return image
            Case ResourceTypes.table : Return table
            Case ResourceTypes.text : Return text
            Case Else
                Throw New NotImplementedException(type.ToString)
        End Select
    End Function

    Public Overrides Function ToString() As String
        Return $"[{type.Description}] {getResourceValue()}"
    End Function

End Class

Public Enum ResourceTypes
    text
    image
    table
End Enum