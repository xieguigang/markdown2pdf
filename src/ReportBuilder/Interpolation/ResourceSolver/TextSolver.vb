Imports System.Net
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Class TextSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        If resource.styles.IsNullOrEmpty Then
            Return WebUtility.HtmlEncode(resource.text)
        Else
            Return $"<span style='{resource.styles("*")?.CSSValue}'>{WebUtility.HtmlEncode(resource.text)}</span>"
        End If
    End Function
End Class