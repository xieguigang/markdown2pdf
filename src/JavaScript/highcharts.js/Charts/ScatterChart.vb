#Region "Microsoft.VisualBasic::e8e1993c8f1dcb32570955344edef96e, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Charts/ScatterChart.vb"

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

    '   Total Lines: 37
    '    Code Lines: 31
    ' Comment Lines: 0
    '   Blank Lines: 6
    '     File Size: 1.06 KB


    '     Class scatterOptions
    ' 
    '         Properties: marker, states, tooltip
    ' 
    '     Class marker
    ' 
    '         Properties: radius, states
    ' 
    '     Class states
    ' 
    '         Properties: hover
    ' 
    '     Class effect
    ' 
    '         Properties: enabled, lineColor, marker
    ' 
    '     Class markerOptions
    ' 
    '         Properties: enabled, fillColor, lineColor, lineWidth
    ' 
    '     Class ScatterSerial
    ' 
    '         Properties: color, colorByPoint, data, name
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace ScatterChart

    Public Class scatterOptions
        Public Property marker As marker
        Public Property states As states
        Public Property tooltip As tooltip
    End Class

    Public Class marker
        Public Property radius As Double
        Public Property states As states
    End Class

    Public Class states
        Public Property hover As effect
    End Class

    Public Class effect
        Public Property enabled As Boolean
        Public Property lineColor As String
        Public Property marker As markerOptions
    End Class

    Public Class markerOptions
        Public Property enabled As Boolean?
        Public Property fillColor As String
        Public Property lineWidth As Double?
        Public Property lineColor As String
    End Class

    Public Class ScatterSerial
        Public Property name As String
        Public Property color As String
        Public Property colorByPoint As Boolean
        Public Property data As Double()()
    End Class
End Namespace
