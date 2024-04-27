#Region "Microsoft.VisualBasic::8e72d3901b51d1f6175332a6c6ff248b, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/d3.js//test/test/Module1.vb"

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

    '   Total Lines: 81
    '    Code Lines: 58
    ' Comment Lines: 8
    '   Blank Lines: 15
    '     File Size: 3.38 KB


    ' Module Module1
    ' 
    '     Function: Bezie
    ' 
    '     Sub: Main
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Math2D.ConvexHull
Imports names = Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic.NameOf

Module Module1

    Sub Main()
        Dim json = SMRUCC.WebCloud.d3js.Network.htmlwidget.BuildData.BuildGraph("../../..\viewer.html")
        Dim colors As SolidBrush() = Designer.GetColors("d3.scale.category10()") _
            .Select(Function(c) New SolidBrush(c)) _
            .ToArray ' using d3.js colors
        Dim graph = json.CreateGraph(Function(n) colors(CInt(n.NodeType)))
        Dim nodePoints As Dictionary(Of Graph.Node, Point) = Nothing

        NetworkVisualizer.DefaultEdgeColor = Color.DimGray

        Call graph.doRandomLayout
        Call graph.doForceLayout(showProgress:=True, Repulsion:=2000, Stiffness:=40, Damping:=0.5, iterations:=1500)
        Call graph.Tabular.Save("./test")

        Dim canvas As Image = graph _
            .DrawImage(
                canvasSize:="2700,3000",
                scale:=8.5,
                fontSizeFactor:=20,
                radiusScale:=5,
                labelColorAsNodeColor:=True,
                nodePoints:=nodePoints) _
            .AsGDIImage

        Dim groups = graph.nodes _
            .GroupBy(Function(x)
                         Return x.Data.Properties(names.REFLECTION_ID_MAPPING_NODETYPE)
                     End Function) _
            .ToArray

        Using g As Graphics2D = canvas.CreateCanvas2D(directAccess:=True)
            For Each group In groups
                Dim polygon As Point() = group _
                    .Select(Function(node) nodePoints(node)) _
                    .ToArray

                ' 只有两个点或者一个点是无法计算凸包的，则跳过这些点
                If polygon.Length <= 3 Then
                    Continue For
                End If

                ' 凸包算法计算出边界
                polygon = ConvexHull.JarvisMatch(polygon).Enlarge(1.5).Bezie

                '' 凸包计算出来的多边形进行矢量放大1.5倍，并进行二次插值处理
                'With polygon.Enlarge(scale:=2).AsList
                '    Call .Add(.First)

                '    polygon = .BSpline(2, RESOLUTION:=3).ToPoints
                'End With

                ' 描绘出边界
                Dim gcolor As SolidBrush = colors(CInt(group.Key))
                Dim transparent = Color.FromArgb(30, gcolor.Color)

                Call g.DrawPolygon(New Pen(gcolor, 3), polygon)
                Call g.FillPolygon(New SolidBrush(transparent), polygon)
            Next
        End Using

        Call canvas.SaveAs("../../..\/viewer.png")
    End Sub

    <Extension> Public Function Bezie(polygon As IEnumerable(Of Point)) As Point()
        Dim newPolygon As New List(Of Point)
        newPolygon = Microsoft.VisualBasic.Math.Interpolation.BezierCurve.BezierSmoothInterpolation
    End Function
End Module
