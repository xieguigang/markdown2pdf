#Region "Microsoft.VisualBasic::c09479b1d7f0245575e702e0a01dca85, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//RadarChart.vb"

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

    '   Total Lines: 23
    '    Code Lines: 18
    ' Comment Lines: 0
    '   Blank Lines: 5
    '     File Size: 632 B


    '     Class AxisValue
    ' 
    '         Properties: axis, value
    ' 
    '         Function: ToString
    ' 
    '     Class RadarData
    ' 
    '         Properties: colors, layers, names
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON

Namespace RadarChart

    Public Class AxisValue
        Public Property axis As String
        Public Property value As Double

        Public Overrides Function ToString() As String
            Return $"Dim {axis} = {value}"
        End Function
    End Class

    Public Class RadarData
        Public Property colors As String()
        Public Property names As String()
        Public Property layers As AxisValue()()

        Public Overrides Function ToString() As String
            Return names.GetJson
        End Function
    End Class
End Namespace
