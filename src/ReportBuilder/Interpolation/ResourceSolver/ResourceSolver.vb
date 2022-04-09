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


