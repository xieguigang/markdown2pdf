#Region "Microsoft.VisualBasic::def1b9729f5e2c4ebd78cd9afcfd93ea, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Common/AbstractSerial.vb"

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

    '   Total Lines: 46
    '    Code Lines: 19
    ' Comment Lines: 20
    '   Blank Lines: 7
    '     File Size: 1.30 KB


    ' Class AbstractSerial
    ' 
    '     Properties: data, name, type
    ' 
    '     Function: ToString
    ' 
    ' Class GenericDataSerial
    ' 
    '     Properties: pointPlacement
    ' 
    ' Class serial
    ' 
    '     Properties: colorByPoint, data, dataLabels, tooltip
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart

''' <summary>
''' With basic ``type``, ``name`` and ``data``.
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class AbstractSerial(Of T)

    ''' <summary>
    ''' 数据序列的展示类型。
    ''' </summary>
    ''' <returns></returns>
    Public Property type As String
    ''' <summary>
    ''' 数据序列的名称。
    ''' </summary>
    ''' <returns></returns>
    Public Property name As String

    Public Overridable Property data As T()

    Public Overrides Function ToString() As String
        Return $"Dim {name} As {type} = {data.GetJson}"
    End Function
End Class

Public Class GenericDataSerial : Inherits AbstractSerial(Of Double)
    Public Property pointPlacement As String
End Class

''' <summary>
''' Object array
''' </summary>
Public Class serial : Inherits AbstractSerial(Of Object)

    ''' <summary>
    ''' + <see cref="Double"/>
    ''' + <see cref="pieData"/>
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Property data As Object()
    Public Property dataLabels As dataLabels
    Public Property tooltip As tooltip
    Public Property colorByPoint As Boolean?
End Class
