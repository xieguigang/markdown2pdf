Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
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

        For Each row As RowObject In If(maxRows > 0, table.Rows.Take(maxRows), table.Rows)
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