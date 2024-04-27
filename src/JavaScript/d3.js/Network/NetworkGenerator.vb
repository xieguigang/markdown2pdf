#Region "Microsoft.VisualBasic::435724a8a1e7faa8ea44cbe0712fa79e, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//Network/NetworkGenerator.vb"

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

    '   Total Lines: 124
    '    Code Lines: 94
    ' Comment Lines: 19
    '   Blank Lines: 11
    '     File Size: 5.37 KB


    '     Module NetworkGenerator
    ' 
    '         Function: FromNetwork, (+2 Overloads) FromRegulations, LoadJson
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.csv
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.WebCloud.JavaScript.d3js.Network.JSON
Imports SMRUCC.WebCloud.JavaScript.d3js.Network.JSON.v3
Imports NetGraph = Microsoft.VisualBasic.Data.visualize.Network.FileStream.NetworkTables
Imports NetworkEdge = Microsoft.VisualBasic.Data.visualize.Network.FileStream.NetworkEdge
Imports NetworkIO = Microsoft.VisualBasic.Data.visualize.Network.NetworkFileIO

Namespace Network

    ''' <summary>
    ''' Network visualization model json data generator.
    ''' </summary>
    Public Module NetworkGenerator

        ''' <summary>
        ''' Creates network data from network model
        ''' </summary>
        ''' <param name="net"></param>
        ''' <param name="indent">默认值False是为了网络传输所优化的无换行的格式</param>
        ''' <returns></returns>
        <Extension> Public Function FromNetwork(net As NetGraph, Optional indent As Boolean = False) As String
            Dim types$() = net.nodes _
                .Select(Function(x) x.NodeType) _
                .Distinct _
                .ToArray
            Dim nodes As node() = LinqAPI.Exec(Of node) <=
 _
                From x As FileStream.Node
                In net.nodes
                Let color As String = x("color")
                Select New node With {
                    .name = x.ID,
                    .group = Array.IndexOf(types, x.NodeType),
                    .type = x.NodeType,
                    .size = net.Links(x.ID),
                    .color = color
                }

            Dim nodeTable As Dictionary(Of node) = nodes _
                .WriteAddress _
                .ToDictionary
            Dim links As link(Of Integer)() = LinqAPI.Exec(Of link(Of Integer)) <=
 _
                From edge As NetworkEdge
                In net.edges
                Select New link(Of Integer) With {
                    .source = nodeTable(edge.fromNode).ID,
                    .target = nodeTable(edge.toNode).ID,
                    .value = edge.value
                }

            Dim JSON$ = New out With {
                .nodes = nodes,
                .links = links
            }.GetJson(indent:=indent)
            Return JSON
        End Function

        ''' <summary>
        ''' Build network json data from the bacterial transcription regulation network
        ''' </summary>
        ''' <param name="regs"></param>
        ''' <returns></returns>
        <Extension>
        Public Function FromRegulations(regs As IEnumerable(Of Regulation)) As String
            Dim nodes As String() =
                LinqAPI.Exec(Of String) <= From x As Regulation
                                           In regs
                                           Select {x.ORF_ID, x.Regulator}
            Dim net As New NetGraph
            Dim nodesTable = (From x As Regulation
                              In regs
                              Select x
                              Group x By x.ORF_ID Into Group) _
                                  .ToDictionary(Function(x) x.ORF_ID,
                                                Function(x) (From g As Regulation
                                                             In x.Group
                                                             Select g
                                                             Group g By g.MotifFamily Into Count
                                                             Order By Count Descending).First.MotifFamily)

            For Each tf As String In regs.Select(Function(x) x.Regulator).Distinct
                If nodesTable.ContainsKey(tf) Then
                    Call nodesTable.Remove(tf)
                End If
                Call nodesTable.Add(tf, NameOf(tf))
            Next

            net += nodes.Distinct.Select(Function(x) New FileStream.Node(x, nodesTable(x)))
            net += From o In (From x As Regulation
                              In regs
                              Select x
                              Group x By x.GetJson Into Group)
                   Let edge As Regulation = o.Group.First
                   Let n As Integer = o.Group.Count
                   Select New NetworkEdge(edge.Regulator, edge.ORF_ID, n)

            Return net.FromNetwork
        End Function

        ''' <summary>
        ''' Build network json data from the bacterial transcription regulation network
        ''' </summary>
        ''' <param name="regs">The raw csv document file path.</param>
        ''' <returns></returns>
        Public Function FromRegulations(regs As String) As String
            Dim json As String =
            regs.LoadCsv(Of Regulation).FromRegulations()
            Return json
        End Function

        Public Function LoadJson(netDIR As String) As String
            Dim net As NetGraph = NetworkIO.Load(netDIR)
            Dim json As String = FromNetwork(net)
            Return json
        End Function
    End Module
End Namespace
