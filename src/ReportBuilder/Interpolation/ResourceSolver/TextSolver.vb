Imports System.Net
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Class TextSolver : Inherits ResourceSolver

    Public ReadOnly Property isHtml As Boolean = False

    Public Sub New(res As ResourceDescription, Optional isHtml As Boolean = False)
        MyBase.New(res)

        Me.isHtml = isHtml
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim text As String = resource.text

        If Not isHtml Then
            text = WebUtility.HtmlEncode(text)
        End If

        If resource.styles.IsNullOrEmpty Then
            Return text
        Else
            Return $"<span style='{resource.styles("*")?.CSSValue}'>
                        {text}
                     </span>"
        End If
    End Function
End Class