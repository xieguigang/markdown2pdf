#Region "Microsoft.VisualBasic::55c26a994a28ecd5d22c9b3af492119d, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Data/SynchronizedLines.vb"

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

    '   Total Lines: 17
    '    Code Lines: 12
    ' Comment Lines: 0
    '   Blank Lines: 5
    '     File Size: 402 B


    ' Class SynchronizedLines
    ' 
    '     Properties: datasets, xData
    ' 
    ' Class LineDataSet
    ' 
    '     Properties: data, max, name, type, unit
    '                 valueDecimals
    ' 
    ' /********************************************************************************/

#End Region

Public Class SynchronizedLines

    Public Property xData As Double()
    Public Property datasets As LineDataSet()

End Class

Public Class LineDataSet

    Public Property name As String
    Public Property type As String
    Public Property unit As String
    Public Property valueDecimals As Integer
    Public Property data As Double()
    Public Property max As Double?

End Class
