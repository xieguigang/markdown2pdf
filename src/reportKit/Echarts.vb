
Imports ECharts
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Scripting.MetaData

<Package("Echarts")>
Module Echarts

    <ExportAPI("json_graph")>
    Public Function JsonGraph(g As NetworkGraph) As Graph
        Return Graph.FromGraph(g)
    End Function
End Module
