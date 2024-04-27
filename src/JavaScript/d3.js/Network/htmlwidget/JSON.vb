#Region "Microsoft.VisualBasic::fea3898a643ed32d13ff097efde347fc, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//Network/htmlwidget/JSON.vb"

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
    '    Code Lines: 41
    ' Comment Lines: 0
    '   Blank Lines: 5
    '     File Size: 1.54 KB


    '     Class JSON
    ' 
    '         Properties: evals, jsHooks, x
    ' 
    '     Class NetGraph
    ' 
    '         Properties: links, nodes, options
    ' 
    '     Class Options
    ' 
    '         Properties: arrows, bounded, charge, clickAction, clickTextSize
    '                     colourScale, fontFamily, fontSize, Group, legend
    '                     linkDistance, linkWidth, NodeID, nodesize, opacity
    '                     opacityNoHover, radiusCalculation, zoom
    ' 
    '     Class Links
    ' 
    '         Properties: colour, source, target
    ' 
    '     Class Nodes
    ' 
    '         Properties: group, name
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Network.htmlwidget

    Public Class JSON
        Public Property x As NetGraph
        Public Property evals As String()
        Public Property jsHooks As String()
    End Class

    Public Class NetGraph
        Public Property links As Links
        Public Property nodes As Nodes
        Public Property options As Options
    End Class

    Public Class Options
        Public Property NodeID As String
        Public Property Group As String
        Public Property colourScale As String
        Public Property fontSize As Integer
        Public Property fontFamily As String
        Public Property clickTextSize As Integer
        Public Property linkDistance As Integer
        Public Property linkWidth As String
        Public Property charge As Integer
        Public Property opacity As Double
        Public Property zoom As Boolean
        Public Property legend As Boolean
        Public Property arrows As Boolean
        Public Property nodesize As Boolean
        Public Property radiusCalculation As String
        Public Property bounded As Boolean
        Public Property opacityNoHover As Double
        Public Property clickAction As String
    End Class

    Public Class Links
        Public Property source As Integer()
        Public Property target As Integer()
        Public Property colour As String()
    End Class

    Public Class Nodes
        Public Property name As String()
        Public Property group As String()
    End Class
End Namespace
