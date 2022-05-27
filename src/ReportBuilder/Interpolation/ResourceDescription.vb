Imports System.Text
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Class ResourceDescription

    Public Property image As String
    Public Property table As String
    Public Property styles As CSSFile
    Public Property text As String
    Public Property html As String
    Public Property options As Dictionary(Of String, Object)

    Public ReadOnly Property type As ResourceTypes
        Get
            If Not text.StringEmpty Then
                Return ResourceTypes.text
            ElseIf Not html.StringEmpty Then
                Return ResourceTypes.html
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
        Dim html As New StringBuilder(If(Me.html, ""))
        Dim opts As New Dictionary(Of String, Object)
        Dim contentText As String

        For Each key As String In meta.Keys
            contentText = meta(key)
            key = $"${{{key}}}"

            Call text.Replace(key, contentText)
            Call image.Replace(key, contentText)
            Call table.Replace(key, contentText)
            Call html.Replace(key, contentText)
        Next

        For Each key As String In options.Keys
            Dim value As Object = options(key)

            If TypeOf value Is String Then
                Dim str As New StringBuilder(DirectCast(value, String))

                For Each name As String In meta.Keys
                    Call str.Replace($"${{{name}}}", meta(name))
                Next

                value = str
            End If

            Call opts.Add(key, value)
        Next

        Return New ResourceDescription With {
            .text = text.ToString,
            .image = image.ToString,
            .table = table.ToString,
            .html = html.ToString,
            .styles = styles,
            .options = opts
        }
    End Function

    Public Function getResourceValue() As String
        Select Case type
            Case ResourceTypes.image : Return image
            Case ResourceTypes.table : Return table
            Case ResourceTypes.text : Return text
            Case ResourceTypes.html : Return html
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
    html
End Enum