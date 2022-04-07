Imports System.Net
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports any = Microsoft.VisualBasic.Scripting

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