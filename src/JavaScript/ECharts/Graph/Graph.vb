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
            .Select(Function(v) v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE)) _
            .Distinct _
            .Indexing
        Dim nodes As New Dictionary(Of String, Node)
        Dim links As New List(Of Link)

        For Each node In g.vertex
            nodes(node.label) = New Node With {
                .category = typeIndex.IndexOf(node.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE)),
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