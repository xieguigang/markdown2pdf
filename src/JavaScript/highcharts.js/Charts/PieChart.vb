#Region "Microsoft.VisualBasic::8c5f0c71ed8f6807d92c70ec250b62b5, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Charts/PieChart.vb"

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

    '   Total Lines: 48
    '    Code Lines: 36
    ' Comment Lines: 0
    '   Blank Lines: 12
    '     File Size: 1.42 KB


    '     Class PieChart
    ' 
    '         Function: ToString
    ' 
    '     Class PieChart3D
    ' 
    ' 
    ' 
    '     Class pieOptions
    ' 
    '         Properties: allowPointSelect, cursor, depth, showInLegend
    ' 
    '     Class pieData
    ' 
    '         Properties: name, selected, sliced, y
    ' 
    '     Class VariablePieSerial
    ' 
    '         Properties: innerSize, minPointSize, zMin
    ' 
    '     Class VariablePieSerialData
    ' 
    '         Properties: name, y, z
    ' 
    '     Class VariablePieChart
    ' 
    '         Function: ChartType
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace PieChart

    Public Class PieChart : Inherits Highcharts(Of serial)

        Public Overrides Function ToString() As String
            Return title.ToString
        End Function
    End Class

    Public Class PieChart3D : Inherits Highcharts3D(Of serial)

    End Class

    Public Class pieOptions : Inherits seriesoptions
        Public Property allowPointSelect As Boolean
        Public Property cursor As String
        Public Property depth As String
        Public Property showInLegend As Boolean
    End Class

    Public Class pieData
        Public Property name As String
        Public Property y As Double
        Public Property sliced As Boolean
        Public Property selected As Boolean
    End Class

    Public Class VariablePieSerial : Inherits AbstractSerial(Of VariablePieSerialData)

        Public Property minPointSize As Double?
        Public Property innerSize As String
        Public Property zMin As Double?

    End Class

    Public Class VariablePieSerialData
        Public Property name As String
        Public Property y As Double?
        Public Property z As Double?
    End Class

    Public Class VariablePieChart : Inherits Highcharts(Of VariablePieSerial)

        Public Shared Function ChartType() As chart
            Return New chart With {.type = "variablepie"}
        End Function
    End Class
End Namespace
