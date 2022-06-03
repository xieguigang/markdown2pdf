Public Class HeaderCounter

    Public Property h1 As Integer = 1
    Public Property h2 As Integer = 1
    Public Property h3 As Integer = 1
    Public Property h4 As Integer = 1

    Public Function Count(tag As String) As HeaderCounter
        Select Case Strings.LCase(tag)
            Case "h1"
                h1 += 1
                h2 = 1
                h3 = 1
                h4 = 1
            Case "h2"
                h2 += 1
                h3 = 1
                h4 = 1
            Case "h3"
                h3 += 1
                h4 = 1
            Case Else
                h4 += 1
        End Select

        Return Me
    End Function

    Public Shared Function Parse(h0 As String) As HeaderCounter
        Dim n As Integer() = Strings.Trim(h0) _
            .Split(" "c) _
            .Select(AddressOf Integer.Parse) _
            .ToArray

        If n.Length = 1 Then
            Return New HeaderCounter With {.h1 = n(Scan0)}
        ElseIf n.Length = 2 Then
            Return New HeaderCounter With {.h1 = n(0), .h2 = n(1)}
        ElseIf n.Length = 3 Then
            Return New HeaderCounter With {.h1 = n(0), .h2 = n(1), .h3 = n(2)}
        Else
            Return New HeaderCounter With {.h1 = n(0), .h2 = n(1), .h3 = n(2), .h4 = n(3)}
        End If
    End Function

    Public Overloads Function ToString(level As Integer) As String
        If level = 1 Then
            Return h1
        ElseIf level = 2 Then
            Return $"{h1}.{h2}"
        ElseIf level = 3 Then
            Return $"{h1}.{h2}.{h3}"
        Else
            Return $"{h1}.{h2}.{h3}.{h4}"
        End If
    End Function

    Public Overrides Function ToString() As String
        Return $"{h1}.{h2}.{h3}.{h4}"
    End Function
End Class
