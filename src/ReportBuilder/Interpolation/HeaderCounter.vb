Public Class HeaderCounter

    Public Property h1 As Integer = 1
    Public Property h2 As Integer = 1
    Public Property h3 As Integer = 1
    Public Property h4 As Integer = 1

    Public Overrides Function ToString() As String
        Return $"{h1}.{h2}.{h3}.{h4}"
    End Function
End Class
