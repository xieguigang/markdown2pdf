#Region "Microsoft.VisualBasic::a74858991de8ffb5d73d622a36df0bf3, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//3D/options3d.vb"

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

    '   Total Lines: 33
    '    Code Lines: 27
    ' Comment Lines: 0
    '   Blank Lines: 6
    '     File Size: 950 B


    '     Class options3d
    ' 
    '         Properties: alpha, beta, depth, enabled, fitToPlot
    '                     frame, viewDistance
    ' 
    '         Function: ToString
    ' 
    '     Class frame3DOptions
    ' 
    '         Properties: back, bottom, side
    ' 
    '     Class frameOptions
    ' 
    '         Properties: color, size
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace viz3D

    Public Class options3d

        Public Property enabled As Boolean?
        Public Property alpha As Double?
        Public Property beta As Double?
        Public Property depth As Double?
        Public Property viewDistance As Double?
        Public Property fitToPlot As Boolean?
        Public Property frame As frame3DOptions

        Public Overrides Function ToString() As String
            If enabled Then
                Return NameOf(enabled)
            Else
                Return $"Not {NameOf(enabled)}"
            End If
        End Function
    End Class

    Public Class frame3DOptions
        Public Property bottom As frameOptions
        Public Property back As frameOptions
        Public Property side As frameOptions
    End Class

    Public Class frameOptions
        Public Property size As Double?
        Public Property color As String
    End Class

End Namespace
