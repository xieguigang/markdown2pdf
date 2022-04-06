Imports System.Text

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

    Public Function FillMetadata(meta As Dictionary(Of String, String)) As ResourceDescription
        Dim text As New StringBuilder(If(Me.text, ""))
        Dim image As New StringBuilder(If(Me.image, ""))
        Dim table As New StringBuilder(If(Me.table, ""))

        For Each key As String In meta.Keys
            Call text.Replace($"${{{key}}}", meta(key))
            Call image.Replace($"${{{key}}}", meta(key))
            Call table.Replace($"${{{key}}}", meta(key))
        Next

        Return New ResourceDescription With {
            .text = text.ToString,
            .image = image.ToString,
            .table = table.ToString,
            .styles = styles
        }
    End Function

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