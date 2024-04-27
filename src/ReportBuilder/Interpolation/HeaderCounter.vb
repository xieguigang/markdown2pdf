#Region "Microsoft.VisualBasic::f21b2eb611a73b06eb9e428a9a86c9c2, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/HeaderCounter.vb"

    ' Author:
    ' 
    '       xieguigang (I@xieguigang.me)
    ' 
    ' Copyright (c) 2021 R# language
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:


    ' Code Statistics:

    '   Total Lines: 76
    '    Code Lines: 66
    ' Comment Lines: 0
    '   Blank Lines: 10
    '     File Size: 2.33 KB


    ' Class HeaderCounter
    ' 
    '     Properties: h1, h2, h3, h4
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: Count, Parse, (+3 Overloads) ToString
    ' 
    ' /********************************************************************************/

#End Region

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

