Imports System.Net
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports any = Microsoft.VisualBasic.Scripting

Public MustInherit Class ResourceSolver

    Protected resource As ResourceDescription

    Sub New(res As ResourceDescription)
        resource = res
    End Sub

    Public MustOverride Function GetHtml(workdir As String) As String

    Public Overrides Function ToString() As String
        Return resource.ToString
    End Function

End Class

Public Class ImageSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim filepath As String = getfile(workdir)
        Dim html As Boolean = resource.options.TryGetValue("html", [default]:=False)

        If html Then
            If filepath.FileExists Then
                Return $"<img style='{resource.styles}' src='{filepath}' />"
            Else
                Return any.ToString(resource.options.TryGetValue("missing", [default]:=""))
            End If
        Else
            Return filepath
        End If
    End Function

    Private Function getfile(workdir As String) As String
        If resource.image.FileExists Then
            Return resource.image
        Else
            Return $"{workdir}/{resource.image}"
        End If
    End Function
End Class

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

Public Class TextSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        If resource.styles.StringEmpty Then
            Return WebUtility.HtmlEncode(resource.text)
        Else
            Return $"<span style='{resource.styles}'>{WebUtility.HtmlEncode(resource.text)}</span>"
        End If
    End Function
End Class