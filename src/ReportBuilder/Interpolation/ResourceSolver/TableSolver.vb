Imports System.Net
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

        Dim table As EntityObject() = EntityObject.LoadDataSet(tablefile).ToArray
        Dim tbody As New StringBuilder
        Dim css As CSSFile = resource.styles
        Dim names As String() = table(Scan0).Properties.Keys.ToArray
        Dim thead As String = BuildRowHtml(names, css, isHeader:=True)

        For Each row As EntityObject In table
            tbody.AppendLine($"<tr style='{any.ToString(css("tr")?.CSSValue)}'>{BuildRowHtml(row(names), css, isHeader:=False)}</tr>")
        Next

        Return $"<table style='{any.ToString(css("table")?.CSSValue)}'>

<thead style='{any.ToString(css("thead")?.CSSValue)}'>
<tr style='{any.ToString(css("th")?.CSSValue)}'>
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
                            Return $"<th>{s}</th>"
                        Else
                            Return $"<td>{s}</td>"
                        End If
                    End Function) _
            .JoinBy(vbCrLf)
    End Function
End Class