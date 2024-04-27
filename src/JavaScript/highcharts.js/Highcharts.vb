#Region "Microsoft.VisualBasic::726cee9988b933958dab14481c01298d, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Highcharts.vb"

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

    '   Total Lines: 93
    '    Code Lines: 56
    ' Comment Lines: 23
    '   Blank Lines: 14
    '     File Size: 3.32 KB


    ' Class Highcharts
    ' 
    '     Properties: chart, colors, credits, legend, plotOptions
    '                 Preview, responsiveOptions, series, subtitle, title
    '                 tooltip, xAxis, yAxis
    ' 
    '     Function: setBackground
    ' 
    ' Class Highcharts3D
    ' 
    '     Properties: zAxis
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.ApplicationServices.Debugging
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Text.Xml

''' <summary>
''' The abstract ``highcharts.js`` data model
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class Highcharts(Of T)
    Implements IVisualStudioPreviews

    Protected Friend reference As New List(Of String) From {
        "https://code.highcharts.com/highcharts.js"
    }

    ''' <summary>
    ''' The charting options
    ''' </summary>
    ''' <returns></returns>
    Public Property chart As chart
    ''' <summary>
    ''' 可以通过这个属性来自定义每一个系列的颜色
    ''' </summary>
    ''' <returns></returns>
    Public Property colors As String()
    Public Property title As title
    Public Property subtitle As title
    Public Property yAxis As Axis
    Public Property xAxis As Axis
    Public Property tooltip As tooltip
    Public Property plotOptions As plotOptions
    Public Property legend As legendOptions
    Public Property series As T()
    Public Property responsiveOptions As responsiveOptions
    Public Property credits As credits

    Const container$ = NameOf(container)

    ''' <summary>
    ''' HTML Debug view in VisualStudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' This preview feature required Windows 10
    ''' </remarks>
    Protected ReadOnly Property Preview As String Implements IVisualStudioPreviews.Previews
        Get
            Dim refs$ = reference _
                .Select(Function(url)
                            Return (<script type="text/javascript" src=<%= url %>></script>).ToString
                        End Function) _
                .JoinBy(vbCrLf)

            Return sprintf(<html>
                               <head>
                                   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                   <meta name="viewport" content="width=device-width, initial-scale=1"/>

                                   <title>Highcharts VisualStudio Previews</title>

                                   <style type="text/css">
                                   </style>

                                   %s
                               </head>
                               <body>
                                   <div id=<%= container %> style="min-width: 310px; max-width: 400px; height: 400px; margin: 0 auto">
                                   </div>
                                   <script type="text/javascript">
                                       %s
                                   </script>
                               </body>
                           </html>, refs, Javascript.WriteJavascript(container, Me))
        End Get
    End Property

    Public Function setBackground(color As Color) As Highcharts(Of T)
        chart.backgroundColor = color.ToHtmlColor
        Return Me
    End Function

End Class

Public MustInherit Class Highcharts3D(Of T) : Inherits Highcharts(Of T)

    ''' <summary>
    ''' The Z- axis in a 3D zone space.
    ''' </summary>
    ''' <returns></returns>
    Public Property zAxis As Axis

End Class
