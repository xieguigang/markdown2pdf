#Region "Microsoft.VisualBasic::b54abc960d96dd71a030fda1a4f091e5, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Charts/BarChart.vb"

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

    '   Total Lines: 38
    '    Code Lines: 29
    ' Comment Lines: 0
    '   Blank Lines: 9
    '     File Size: 1.07 KB


    '     Class barOptions
    ' 
    '         Properties: dataLabels
    ' 
    '     Class columnrangeSerial
    ' 
    '         Properties: data, name
    ' 
    '     Class columnrangeOptions
    ' 
    '         Properties: dataLabels
    ' 
    '     Class columnOptions
    ' 
    '         Properties: borderRadius, borderWidth, depth, groupPadding, pointPadding
    '                     stacking
    ' 
    '     Class BarChart
    ' 
    '         Function: ToString
    ' 
    '     Class VariWideBarChart
    ' 
    ' 
    ' 
    '     Class ColumnChart
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace BarChart

    Public Class barOptions
        Public Property dataLabels As dataLabels
    End Class

    Public Class columnrangeSerial
        Public Property name As String
        Public Property data As Double()()
    End Class

    Public Class columnrangeOptions
        Public Property dataLabels As dataLabels
    End Class

    Public Class columnOptions
        Public Property borderRadius As Double?
        Public Property depth As Integer?
        Public Property stacking As String
        Public Property pointPadding As Double?
        Public Property groupPadding As Double?
        Public Property borderWidth As Double?
    End Class

    Public Class BarChart : Inherits Highcharts(Of GenericDataSerial)

        Public Overrides Function ToString() As String
            Return title.ToString
        End Function
    End Class

    Public Class VariWideBarChart : Inherits Highcharts(Of serial)
    End Class

    Public Class ColumnChart : Inherits Highcharts(Of serial)

    End Class
End Namespace
