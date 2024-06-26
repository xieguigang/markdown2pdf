﻿#Region "Microsoft.VisualBasic::f651476e2df3f2db538d61c2b1ae81a5, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Common/Components.vb"

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

    '   Total Lines: 150
    '    Code Lines: 111
    ' Comment Lines: 20
    '   Blank Lines: 19
    '     File Size: 4.47 KB


    ' Class Axis
    ' 
    '     Properties: allowDecimals, categories, className, crosshair, dateTimeLabelFormats
    '                 enabled, endOnTick, gridLineInterpolation, gridLineWidth, labels
    '                 lineWidth, max, min, opposite, plotBands
    '                 showFirstLabel, showLastLabel, startOnTick, tickInterval, tickmarkPlacement
    '                 title, type
    ' 
    ' Class dateTimeLabelFormats
    ' 
    '     Properties: month, year
    ' 
    ' Class Band
    ' 
    '     Properties: [to], color, from
    ' 
    ' Class legendOptions
    ' 
    '     Properties: align, backgroundColor, borderWidth, enabled, floating
    '                 layout, reversed, shadow, verticalAlign, x
    '                 y
    ' 
    ' Class title
    ' 
    '     Properties: align, enable, skew3d, text
    ' 
    '     Function: ToString
    ' 
    ' Class tooltip
    ' 
    '     Properties: [shared], crosshairs, footerFormat, headerFormat, pointFormat
    '                 useHTML, valueSuffix
    ' 
    ' Class labelOptions
    ' 
    '     Properties: autoRotation, connectorAllowed, enabled, formatter, overflow
    '                 rotation, skew3d, style
    ' 
    ' Class styleOptions
    ' 
    '     Properties: color, fontFamily, fontSize
    ' 
    ' Class dataLabels
    ' 
    '     Properties: align, color, enabled, format, rotation
    '                 style, y
    ' 
    ' Class responsiveOptions
    ' 
    '     Properties: rules
    ' 
    ' Class rule
    ' 
    '     Properties: chartOptions, condition
    ' 
    ' Class ruleConditions
    ' 
    '     Properties: maxWidth
    ' 
    ' Class chartOptions
    ' 
    '     Properties: legend
    ' 
    ' Class credits
    ' 
    '     Properties: enabled, href, text
    ' 
    ' /********************************************************************************/

#End Region

Imports Newtonsoft.Json

Public Class Axis

    Public Property type As String
    Public Property allowDecimals As Boolean?
    Public Property className As String
    Public Property opposite As Boolean?
    Public Property title As title
    Public Property min As Double?
    Public Property max As Double?
    Public Property labels As labelOptions
    Public Property categories As String()
    Public Property startOnTick As Boolean?
    Public Property endOnTick As Boolean?
    Public Property showLastLabel As Boolean?
    Public Property gridLineWidth As Boolean?
    Public Property gridLineInterpolation As String
    Public Property showFirstLabel As Boolean?
    Public Property crosshair As Boolean?
    Public Property enabled As Boolean?
    Public Property tickmarkPlacement As String
    Public Property lineWidth As Integer

    ''' <summary>
    ''' 逻辑值或者一个实数
    ''' </summary>
    ''' <returns></returns>
    Public Property tickInterval As Object
    Public Property plotBands As Band()
    Public Property dateTimeLabelFormats As dateTimeLabelFormats
End Class

Public Class dateTimeLabelFormats
    Public Property month As String
    Public Property year As String
End Class

Public Class Band
    Public Property from As Double?
    Public Property [to] As Double?
    Public Property color As String
End Class

Public Class legendOptions

    ''' <summary>
    ''' 是否允许图注。
    ''' </summary>
    ''' <returns></returns>
    Public Property enabled As Boolean?
    Public Property layout As String
    Public Property align As String
    Public Property verticalAlign As String
    Public Property x As Double?
    Public Property y As Double?
    Public Property floating As Boolean?
    Public Property borderWidth As Double?
    Public Property backgroundColor As String
    Public Property shadow As Boolean?
    Public Property reversed As Boolean?
End Class

Public Class title
    Public Property text As String
    Public Property align As String
    Public Property enable As Boolean?
    Public Property skew3d As Boolean?

    Public Overrides Function ToString() As String
        Return text
    End Function

    Public Shared Widening Operator CType(title As String) As title
        Return New title With {.text = title}
    End Operator
End Class

Public Class tooltip
    Public Property headerFormat As String
    Public Property pointFormat As String
    Public Property valueSuffix As String
    Public Property footerFormat As String
    Public Property [shared] As Boolean?
    Public Property useHTML As Boolean?
    Public Property crosshairs As Boolean?
End Class

<JsonConverter(GetType(LambdaWriter))>
Public Class labelOptions
    Public Property connectorAllowed As Boolean?
    Public Property overflow As String
    Public Property skew3d As Boolean?
    Public Property style As styleOptions
    Public Property formatter As Lambda
    Public Property rotation As Double?
    Public Property autoRotation As Double()
    Public Property enabled As Boolean?
End Class

Public Class styleOptions
    Public Property fontSize As String
    Public Property fontFamily As String
    Public Property color As String
End Class

Public Class dataLabels
    Public Property enabled As Boolean?
    Public Property format As String
    Public Property style As styleOptions
    Public Property rotation As Double?
    Public Property color As String
    Public Property align As String
    Public Property y As Double?
End Class

Public Class responsiveOptions
    Public Property rules As rule()
End Class

Public Class rule
    Public Property condition As ruleConditions
    Public Property chartOptions As chartOptions
End Class

Public Class ruleConditions
    Public Property maxWidth As Double?
End Class

Public Class chartOptions
    Public Property legend As legendOptions
End Class

Public Class credits
    ''' <summary>
    ''' 是否允许显示版权信息。
    ''' </summary>
    ''' <returns></returns>
    Public Property enabled As Boolean?
    ''' <summary>
    ''' 版权所有的链接。
    ''' </summary>
    ''' <returns></returns>
    Public Property href As String
    ''' <summary>
    ''' 版权信息显示文字。
    ''' </summary>
    ''' <returns></returns>
    Public Property text As String
End Class
