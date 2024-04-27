#Region "Microsoft.VisualBasic::5b1a923d4597b7ea624280fea9f2feb4, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//Network/htmlwidget/BuildData.vb"

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

    '   Total Lines: 70
    '    Code Lines: 49
    ' Comment Lines: 8
    '   Blank Lines: 13
    '     File Size: 2.40 KB


    '     Module BuildData
    ' 
    '         Function: BuildGraph, ParseHTML
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.JSON
Imports NetGraphData = Microsoft.VisualBasic.Data.visualize.Network.FileStream.NetworkTables
Imports r = System.Text.RegularExpressions.Regex

Namespace Network.htmlwidget

    ''' <summary>
    ''' 将``htmlwidget``之中的D3.js网络模型解析为scibasic的标准网络模型
    ''' </summary>
    Public Module BuildData

        Const JSON$ = "<script type[=]""application/json"".+?</script>"

        ''' <summary>
        ''' 参数为html文本或者url路径
        ''' </summary>
        ''' <param name="html$"></param>
        ''' <returns></returns>
        Public Function ParseHTML(html$) As String
            If html.FileExists Then
                html = html.GET
            End If

            html = r.Match(html, BuildData.JSON, RegexICSng).Value
            html = html.GetStackValue(">", "<")

            Return html
        End Function

        Public Function BuildGraph(html$) As NetGraphData
            Dim json$ = BuildData.ParseHTML(html)
            Dim data As htmlwidget.NetGraph = json.LoadJSON(Of htmlwidget.JSON).x
            Dim nodes As New List(Of Node)
            Dim edges As New List(Of NetworkEdge)

            For i As Integer = 0 To data.nodes.name.Length - 1
                Dim name$ = data.nodes.name(i)
                Dim type$ = data.nodes.group(i)

                nodes += New Node With {
                    .ID = name,
                    .NodeType = type
                }
            Next

            Dim nodesVector As Node() = nodes.ToArray

            For i As Integer = 0 To data.links.source.Length - 1
                Dim src = nodesVector(data.links.source(i)).ID
                Dim tar = nodesVector(data.links.target(i)).ID
                Dim type = data.links.colour(i)

                edges += New NetworkEdge With {
                    .FromNode = src, 
                    .ToNode = tar, 
                    .value = 1, 
                    .Interaction = type
                }
            Next

            Dim net As New NetGraphData With {
                .Nodes = nodes,
                .Edges = edges
            }
            Return net
        End Function
    End Module
End Namespace
