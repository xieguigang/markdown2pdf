#Region "Microsoft.VisualBasic::045bb44473ecffa6fcf70bf12e428b29, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Common/chart.vb"

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

    '   Total Lines: 98
    '    Code Lines: 58
    ' Comment Lines: 32
    '   Blank Lines: 8
    '     File Size: 3.14 KB


    ' Class chart
    ' 
    '     Properties: backgroundColor, BarChart3D, ColumnChart, height, inverted
    '                 margin, options3d, PieChart3D, plotBackgroundColor, plotBorderWidth
    '                 plotShadow, polar, PolarChart, reflow, renderTo
    '                 showAxes, type, VariWide, width, zoomType
    ' 
    '     Function: ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports SMRUCC.WebCloud.JavaScript.highcharts.viz3D

Public Class chart

    ''' <summary>
    ''' 图表的类型，默认为line，还有bar/column/pie……
    ''' </summary>
    ''' <returns></returns>
    Public Property type As String
    Public Property options3d As options3d
    Public Property backgroundColor As String = "#ffffff"
    ''' <summary>
    ''' 图表中数据报表的放大类型，可以以X轴放大，或是以Y轴放大，还可以以XY轴同时放大。
    ''' </summary>
    ''' <returns></returns>
    Public Property zoomType As String
    ''' <summary>
    ''' 图表中的x，y轴对换。
    ''' </summary>
    ''' <returns></returns>
    Public Property inverted As Boolean?
    Public Property renderTo As String
    Public Property margin As Double?
    ''' <summary>
    ''' 是否为极性图表。
    ''' </summary>
    ''' <returns></returns>
    Public Property polar As Boolean?
    Public Property plotBackgroundColor As String
    Public Property plotBorderWidth As String
    Public Property plotShadow As Boolean?
    ''' <summary>
    ''' 在空白图表中，是否显示坐标轴。
    ''' </summary>
    ''' <returns></returns>
    Public Property showAxes As Boolean?
    ''' <summary>
    ''' 所绘制图表的高度值。
    ''' </summary>
    ''' <returns></returns>
    Public Property height As Double?
    ''' <summary>
    ''' 图表绘图区的宽度，默认为自适应。
    ''' </summary>
    ''' <returns></returns>
    Public Property width As Double?
    ''' <summary>
    ''' 当窗口大小改变时，图表宽度自适应窗口大小改变。
    ''' </summary>
    ''' <returns></returns>
    Public Property reflow As Boolean?

#Region "Chart Profiles"
    Public Shared ReadOnly Property PieChart3D As chart
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ChartProfiles.PieChart3D
        End Get
    End Property

    Public Shared ReadOnly Property BarChart3D As chart
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ChartProfiles.BarChart3D
        End Get
    End Property

    Public Shared ReadOnly Property PolarChart As chart
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ChartProfiles.PolarChart
        End Get
    End Property

    Public Shared ReadOnly Property VariWide As chart
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ChartProfiles.VariWide
        End Get
    End Property

    Public Shared ReadOnly Property ColumnChart As chart
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Get
            Return ChartProfiles.ColumnChart
        End Get
    End Property
#End Region

    Public Overrides Function ToString() As String
        If options3d Is Nothing OrElse Not options3d.enabled Then
            Return type
        Else
            Return $"[3D] {type}"
        End If
    End Function
End Class
