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
        Dim names As String() = table.Headers.ToArray
        Dim thead As String = BuildRowHtml(names, css, isHeader:=True)
        Dim maxRows As Integer = resource.options.TryGetValue("nrows", [default]:=-1)

        For Each row As RowObject In If(maxRows > 0, table.Rows.Take(maxRows), table.Rows)
            tbody.AppendLine($"<tr style='{any.ToString(css("tr")?.CSSValue)}'>{BuildRowHtml(row.AsEnumerable, css, isHeader:=False)}</tr>")
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

    Private Function BuildRowHtml(cells As IEnumerable(Of String), css As CSSFile, isHeader As Boolean) As String
        Return cells _
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