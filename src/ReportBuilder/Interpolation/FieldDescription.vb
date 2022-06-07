Imports Microsoft.VisualBasic.My.JavaScript
Imports any = Microsoft.VisualBasic.Scripting

Public Class FieldDescription

    ''' <summary>
    ''' the field reference name in the csv table file
    ''' </summary>
    ''' <returns></returns>
    Public Property referenceName As String
    Public Property format As String
    Public Property ordinal As Integer
    ''' <summary>
    ''' the display title name
    ''' </summary>
    ''' <returns></returns>
    Public Property [alias] As String

    Public Overloads Function ToString(isHeader As Boolean, str As String) As String
        If isHeader OrElse format.StringEmpty Then
            Return str
        Else
            Return Val(str).ToString(format)
        End If
    End Function

    Friend Shared Iterator Function parseFieldOrdinals(fieldNames As Object(), names As String()) As IEnumerable(Of FieldDescription)
        For Each name As Object In fieldNames
            If TypeOf name Is String Then
                Yield ParseDescription(DirectCast(name, String), names)
            ElseIf TypeOf name Is JavaScriptObject Then
                Yield ParseDescription(DirectCast(name, JavaScriptObject), names)
            Else
                Throw New NotImplementedException(name.GetType.FullName)
            End If
        Next
    End Function

    Public Shared Function ParseDescription(field As JavaScriptObject, names As String()) As FieldDescription
        Dim refStr As String = field.GetNames.First
        Dim opt As JavaScriptObject = field(refStr)
        Dim aliasName As String = any.ToString(opt("alias"))
        Dim format As String = any.ToString(opt("format"))

        Return New FieldDescription With {
            .referenceName = refStr,
            .ordinal = names.IndexOf(.referenceName),
            .[alias] = aliasName,
            .format = format
        }
    End Function

    Public Shared Function ParseDescription(name As String, names As String()) As FieldDescription
        Dim i As Integer = names.IndexOf(name)

        If i = -1 Then
            Dim format As String = name.Match("[:][GF]\d+", RegexICSng)

            If format.StringEmpty Then
                Return New FieldDescription With {
                    .referenceName = name,
                    .ordinal = -1
                }
            Else
                Return New FieldDescription With {
                    .referenceName = name.Replace(format, ""),
                    .ordinal = names.IndexOf(.referenceName),
                    .format = format.Trim(":"c)
                }
            End If
        Else
            Return New FieldDescription With {
                .referenceName = name,
                .ordinal = i
            }
        End If
    End Function

    Public Shared Narrowing Operator CType(field As FieldDescription) As Integer
        Return field.ordinal
    End Operator

End Class
