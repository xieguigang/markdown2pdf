Public Class HeaderCounter

    Public ReadOnly Property h1 As Integer = 1
    Public ReadOnly Property h2 As Integer = 1
    Public ReadOnly Property h3 As Integer = 1
    Public ReadOnly Property h4 As Integer = 1

    Private Sub New(h1 As Integer,
                    Optional h2 As Integer = 1,
                    Optional h3 As Integer = 1,
                    Optional h4 As Integer = 1)

        Me.h1 = h1 - 1
        Me.h2 = If(h2 > 0, h2 - 1, 0)
        Me.h3 = If(h3 > 0, h3 - 1, 0)
        Me.h4 = If(h4 > 0, h4 - 1, 0)
    End Sub

    Public Function Count(tag As String) As HeaderCounter
        Select Case Strings.LCase(tag).Match("[Hh]\d+")
            Case "h1"
                _h1 += 1
                _h2 = 0
                _h3 = 0
                _h4 = 0
            Case "h2"
                _h2 += 1
                _h3 = 0
                _h4 = 0
            Case "h3"
                _h3 += 1
                _h4 = 0
            Case Else
                _h4 += 1
        End Select

        Return Me
    End Function

    Public Shared Function Parse(h0 As String) As HeaderCounter
        Dim n As Integer() = Strings.Trim(h0) _
            .Split(" "c) _
            .Select(AddressOf Integer.Parse) _
            .ToArray

        If n.Length = 1 Then
            Return New HeaderCounter(h1:=n(Scan0))
        ElseIf n.Length = 2 Then
            Return New HeaderCounter(h1:=n(Scan0), h2:=n(1))
        ElseIf n.Length = 3 Then
            Return New HeaderCounter(h1:=n(Scan0), h2:=n(1), h3:=n(2))
        Else
            Return New HeaderCounter(h1:=n(Scan0), h2:=n(1), h3:=n(2), h4:=n(3))
        End If
    End Function

    Public Overloads Function ToString(level As String) As String
        Return ToString(Integer.Parse(level.Match("\d+")))
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
