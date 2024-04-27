#Region "Microsoft.VisualBasic::b1583f5f9bf056f5f0af68bec96c7ba8, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Charts/PolarChart.vb"

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

    '   Total Lines: 22
    '    Code Lines: 14
    ' Comment Lines: 3
    '   Blank Lines: 5
    '     File Size: 567 B


    '     Class paneOptions
    ' 
    '         Properties: endAngle, size, startAngle
    ' 
    '     Class PolarChart
    ' 
    '         Properties: pane
    ' 
    '         Constructor: (+1 Overloads) Sub New
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace PolarChart

    Public Class paneOptions
        Public Property startAngle As Double?
        Public Property endAngle As Double?
        Public Property size As String
    End Class

    ''' <summary>
    ''' 雷达图
    ''' </summary>
    Public Class PolarChart : Inherits Highcharts(Of GenericDataSerial)

        Public Property pane As paneOptions

        Sub New()
            Call MyBase.New
            Call MyBase.reference.Add("https://code.highcharts.com/highcharts-more.js")
        End Sub

    End Class
End Namespace
