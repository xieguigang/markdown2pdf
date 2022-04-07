Imports System.Net
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv.IO
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports any = Microsoft.VisualBasic.Scripting

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