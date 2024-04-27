#Region "Microsoft.VisualBasic::3a061ff9287158149d036c49866d5944, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/ECharts//Graph/Graph.vb"

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

    '   Total Lines: 77
    '    Code Lines: 57
    ' Comment Lines: 5
    '   Blank Lines: 15
    '     File Size: 2.38 KB


    ' Class Graph
    ' 
    '     Properties: categories, links, nodes, type
    ' 
    '     Function: FromGraph
    ' 
    ' Class Link
    ' 
    '     Properties: source, target
    ' 
    ' Class Node
    ' 
    '     Properties: category, index, name, value
    ' 
    ' Class Category
    ' 
    '     Properties: name
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph

Public Class Graph

    Public Property type As String
    Public Property categories As Category()
    Public Property nodes As Node()
    Public Property links As Link()

    ''' <summary>
    ''' convert the sciBASIC graph object as the echart graph object
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns></returns>
    Public Shared Function FromGraph(g As NetworkGraph) As Graph
        Dim typeIndex As Index(Of String) = g.vertex _
            .Select(Function(v)
                        Return If(v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE), "NO_CLASS")
                    End Function) _
            .Distinct _
            .Where(Function(si) Not si Is Nothing) _
            .Indexing
        Dim nodes As New Dictionary(Of String, Node)
        Dim links As New List(Of Link)
        Dim tag As String

        For Each node In g.vertex
            tag = If(node.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE), "NO_CLASS")
            nodes(node.label) = New Node With {
                .category = typeIndex.IndexOf(tag),
                .index = nodes.Count,
                .name = node.label,
                .value = node.data.weights.ElementAtOrDefault(0)
            }
        Next
        For Each edge As Edge In g.graphEdges
            Call links.Add(New Link With {
                .source = nodes(edge.U.label).index,
                .target = nodes(edge.V.label).index
            })
        Next

        Return New Graph With {
            .nodes = nodes.Values.ToArray,
            .categories = typeIndex.Objects _
                .Select(Function(t) New Category With {.name = t}) _
                .ToArray,
            .links = links.ToArray,
            .type = "force"
        }
    End Function

End Class

Public Class Link

    Public Property source As Integer
    Public Property target As Integer

End Class

Public Class Node

    Public Property name As String
    Public Property value As Double
    Public Property category As Integer
    Public Property index As Integer

End Class

Public Class Category

    Public Property name As String

End Class
