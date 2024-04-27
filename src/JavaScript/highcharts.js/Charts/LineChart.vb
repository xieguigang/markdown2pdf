#Region "Microsoft.VisualBasic::9ff71f649f12a785091cd6d1a1edda06, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Charts/LineChart.vb"

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

    '   Total Lines: 44
    '    Code Lines: 25
    ' Comment Lines: 8
    '   Blank Lines: 11
    '     File Size: 1.48 KB


    '     Class lineOptions
    ' 
    '         Properties: pointInterval, stacking
    ' 
    '     Class LineChart
    ' 
    ' 
    ' 
    '     Class DateTimeLineChart
    ' 
    ' 
    ' 
    '     Class LineWithRangeChart
    ' 
    '         Constructor: (+1 Overloads) Sub New
    ' 
    '     Class LineRangeSerial
    ' 
    '         Properties: color, fillOpacity, lineWidth, linkedTo, marker
    '                     zIndex
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports SMRUCC.WebCloud.JavaScript.highcharts.ScatterChart

Namespace LineChart

    Public Class lineOptions : Inherits seriesOptions

        Public Property stacking As String
        ''' <summary>
        ''' 这个属性可能是逻辑值或者数值，所以在这里使用字符串来兼容
        ''' </summary>
        ''' <returns></returns>
        Public Property pointInterval As Object

    End Class

    Public Class LineChart : Inherits Highcharts(Of GenericDataSerial)

    End Class

    Public Class DateTimeLineChart : Inherits Highcharts(Of serial)

    End Class

    Public Class LineWithRangeChart : Inherits Highcharts(Of LineRangeSerial)

        Sub New()
            Call MyBase.New
            Call MyBase.reference.Add("https://code.highcharts.com/highcharts-more.js")
        End Sub
    End Class

    ''' <summary>
    ''' <see cref="LineRangeSerial.data"/> 如果是datetime类型的话，则应该为``{unix_time_stamp, value}``的集合
    ''' 对于range类型，则应该是``{unix_time_stamp, min, max}``的集合
    ''' </summary>
    Public Class LineRangeSerial : Inherits AbstractSerial(Of Object())
        Public Property zIndex As Integer?
        Public Property marker As markerOptions
        Public Property lineWidth As Double?
        Public Property linkedTo As String
        Public Property color As String
        Public Property fillOpacity As Double?
    End Class
End Namespace
