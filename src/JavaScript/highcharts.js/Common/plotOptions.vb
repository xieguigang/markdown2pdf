#Region "Microsoft.VisualBasic::0858c73daa87e92ea8c4536b4b2937fd, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Common/plotOptions.vb"

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

    '   Total Lines: 39
    '    Code Lines: 35
    ' Comment Lines: 0
    '   Blank Lines: 4
    '     File Size: 1.37 KB


    ' Class plotOptions
    ' 
    '     Properties: areaspline, bar, column, columnrange, pie
    '                 scatter, series
    ' 
    '     Function: ToString
    ' 
    ' Class seriesOptions
    ' 
    '     Properties: borderWidth, dataLabels, label, pointStart, type
    ' 
    ' /********************************************************************************/

#End Region

Imports SMRUCC.WebCloud.JavaScript.highcharts.AreaChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.BarChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.ScatterChart

Public Class plotOptions

    Public Property pie As pieOptions
    Public Property series As seriesOptions
    Public Property scatter As scatterOptions
    Public Property columnrange As columnrangeOptions
    Public Property column As columnOptions
    Public Property areaspline As areasplineOptions
    Public Property bar As barOptions

    Public Overrides Function ToString() As String
        If Not pie Is Nothing Then
            Return pie.ToString
        ElseIf Not series Is Nothing Then
            Return series.ToString
        ElseIf Not scatter Is Nothing Then
            Return scatter.ToString
        ElseIf Not columnrange Is Nothing Then
            Return columnrange.ToString
        ElseIf Not column Is Nothing Then
            Return column.ToString
        Else
            Return "null"
        End If
    End Function
End Class

Public Class seriesOptions
    Public Property type As String
    Public Property borderWidth As Double?
    Public Property dataLabels As dataLabels
    Public Property label As labelOptions
    Public Property pointStart As String
End Class
