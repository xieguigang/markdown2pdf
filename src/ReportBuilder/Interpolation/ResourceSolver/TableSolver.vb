﻿Imports System.Net
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
        Dim css As CSSFile

        If resource.styles.StringEmpty Then
            css = New CSSFile With {.Selectors = New Dictionary(Of Selector)}
        Else
            css = CssParser.GetTagWithCSS(CSS:=resource.styles)
        End If

        Dim names As String() = table(Scan0).Properties.Keys.ToArray
        Dim thead As String = BuildRowHtml(names, css)

        For Each row As EntityObject In table
            tbody.AppendLine($"<tr style='{css("tr").ToString}'>{BuildRowHtml(row(names), css)}</tr>")
        Next

        Return $"<table>

<thead style='{css("thead").ToString}'>
<th style='{css("th").ToString}'>
{thead}
</th>
</thead>
<tbody>
{tbody.ToString}
</tbody>

</table>"
    End Function

    Private Function BuildRowHtml(cells As IEnumerable(Of String), css As CSSFile) As String
        Return cells.Select(Function(s) $"<td>{s}</td>").JoinBy(vbCrLf)
    End Function
End Class