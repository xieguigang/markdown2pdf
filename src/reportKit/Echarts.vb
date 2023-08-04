
Imports ECharts
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Scripting.MetaData

''' <summary>
''' Echarts plot data helper, generate echart plot data object from .net clr object
''' </summary>
<Package("Echarts")>
Module Echarts

    ''' <summary>
    ''' Convert the sciBASIC.NET network graph object as the e-charts graph object
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns></returns>
    <ExportAPI("json_graph")>
    Public Function JsonGraph(g As NetworkGraph) As Graph
        Return Graph.FromGraph(g)
    End Function


End Module
