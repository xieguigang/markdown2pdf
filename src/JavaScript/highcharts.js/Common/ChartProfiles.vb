#Region "Microsoft.VisualBasic::4edfd2202970baf22ff909a3f5f760ba, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Common/ChartProfiles.vb"

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

    '   Total Lines: 59
    '    Code Lines: 51
    ' Comment Lines: 0
    '   Blank Lines: 8
    '     File Size: 1.77 KB


    ' Module ChartProfiles
    ' 
    '     Function: AreaSpline, BarChart3D, ColumnChart, PieChart3D, PolarChart
    '               profileBase, VariWide
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports SMRUCC.WebCloud.JavaScript.highcharts.viz3D

Friend Module ChartProfiles

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Function profileBase(type As ChartTypes) As chart
        Return New chart With {
            .type = type.Description
        }
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function PieChart3D() As chart
        Return New chart With {
            .type = "pie",
            .options3d = New options3d With {
                .enabled = True,
                .alpha = 45,
                .beta = 0
            }
        }
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function BarChart3D() As chart
        Return New chart With {
            .type = "column",
            .options3d = New options3d With {
                .enabled = True,
                .alpha = 5,
                .beta = 20,
                .depth = 70
            }
        }
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function PolarChart() As chart
        Return New chart With {
            .polar = True
        }
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function VariWide() As chart
        Return profileBase(ChartTypes.variwide)
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function AreaSpline() As chart
        Return profileBase(ChartTypes.areaspline)
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function ColumnChart() As chart
        Return profileBase(ChartTypes.column)
    End Function
End Module
