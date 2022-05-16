﻿Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports Microsoft.VisualBasic.My.JavaScript
Imports any = Microsoft.VisualBasic.Scripting

Public Class TableSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim tablefile As String = If(resource.table.FileExists, resource.table, $"{workdir}/{resource.table}")

        If Not tablefile.FileExists Then
            Return Nothing
        End If

        Dim table As DataFrame = DataFrame.Load(tablefile)
        Dim tbody As New StringBuilder
        Dim css As CSSFile = resource.styles
        Dim names As String() = table.Headers.Select(Function(str) str.Trim(""""c)).ToArray
        Dim maxRows As Integer = resource.options.TryGetValue("nrows", [default]:=-1)
        Dim orderBy As Object = resource.options.TryGetValue("order_by", [default]:=Nothing)
        Dim fieldNames As String() = resource.options.TryGetValue("fields", [default]:=Nothing)
        Dim ordinals As Integer() = If(fieldNames Is Nothing, Nothing, fieldNames.Select(Function(d) names.IndexOf(d)).ToArray)
        Dim thead As String

        If Not ordinals Is Nothing AndAlso ordinals.Any(Function(i) i = -1) Then
            If fieldNames Is Nothing Then
                Return resource.options.TryGetValue("no_content", [default]:="<span style='color: red;'>No table content data.</span>")
            Else
                thead = fieldNames _
                    .Select(Function(s)
                                Return $"<th style='{any.ToString(css("th")?.CSSValue)}'>{s.Trim(""""c)}</th>"
                            End Function) _
                    .JoinBy(vbCrLf)
            End If
        Else
            thead = BuildRowHtml(names, ordinals, css, isHeader:=True)
        End If

        For Each row As RowObject In RowSelector(table, maxRows, orderBy)
            tbody.AppendLine($"<tr style='{any.ToString(css("tr")?.CSSValue)}'>{BuildRowHtml(row.AsEnumerable, ordinals, css, isHeader:=False)}</tr>")
        Next

        Return $"<table style='{any.ToString(css("table")?.CSSValue)}'>

<thead style='{any.ToString(css("thead")?.CSSValue)}'>
<tr>
{thead}
</tr>
</thead>
<tbody>
{tbody.ToString}
</tbody>

</table>"
    End Function

    ''' <summary>
    ''' orderBy -> take
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="maxRows"></param>
    ''' <param name="orderBy"></param>
    ''' <returns></returns>
    Private Iterator Function RowSelector(table As DataFrame, maxRows As Integer, orderBy As Object) As IEnumerable(Of RowObject)
        Dim orders As Integer()

        If Not orderBy Is Nothing Then
            Dim opts As JavaScriptObject

            If TypeOf orderBy Is String Then
                ' order by field by default .NET comparer function
                opts = New JavaScriptObject
                opts("field") = orderBy
                opts("desc") = False
            ElseIf TypeOf orderBy Is JavaScriptObject Then
                opts = orderBy
            Else
                Throw New NotImplementedException
            End If

            Dim eval As String = opts("eval")
            Dim evalFunc As Func(Of String, Object)

            If eval Is Nothing Then
                evalFunc = Function(str) str
            Else
                Select Case eval
                    Case "nchars" : evalFunc = Function(str) Strings.Len(str)
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If

            Dim desc As Boolean = opts("desc")
            Dim field As String = opts("field")
            Dim fieldOrdinal As Integer = table.GetOrdinal(field)
            Dim vec As String() = table.Column(fieldOrdinal).ToArray
            Dim vecVal = vec.Select(evalFunc).Select(Function(any, i) (any, i)).ToArray

            If desc Then
                orders = vecVal _
                    .OrderByDescending(Function(x) x.any) _
                    .Select(Function(x) x.i) _
                    .ToArray
            Else
                orders = vecVal _
                    .OrderBy(Function(x) x.any) _
                    .Select(Function(x) x.i) _
                    .ToArray
            End If
        Else
            ' no orders
            orders = table.RowNumbers.Sequence.ToArray
        End If

        maxRows = If(maxRows > 0, maxRows, table.RowNumbers)

        For i As Integer = 0 To maxRows - 1
            Yield table(orders(i))
        Next
    End Function

    Private Function BuildRowHtml(cells As IEnumerable(Of String), ordinals As Integer(), css As CSSFile, isHeader As Boolean) As String
        Dim allStrs As String() = cells.ToArray
        Dim partStrs As String()

        If ordinals Is Nothing Then
            partStrs = allStrs
        Else
            partStrs = (From i As Integer
                        In ordinals
                        Select allStrs(i)).ToArray
        End If

        Return partStrs _
            .Select(Function(s)
                        If isHeader Then
                            Return $"<th style='{any.ToString(css("th")?.CSSValue)}'>{s.Trim(""""c)}</th>"
                        Else
                            Return $"<td style='{any.ToString(css("td")?.CSSValue)}'>{s.Trim(""""c)}</td>"
                        End If
                    End Function) _
            .JoinBy(vbCrLf)
    End Function
End Class