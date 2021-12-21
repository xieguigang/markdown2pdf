Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.Rsharp.Runtime.Interop
Imports SMRUCC.WebCloud.JavaScript.highcharts
Imports SMRUCC.WebCloud.JavaScript.highcharts.BarChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.Javascript
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.viz3D
Imports any = Microsoft.VisualBasic.Scripting
Imports chartProfiles = SMRUCC.WebCloud.JavaScript.highcharts.chart

<Package("highcharts.js")>
Module highcharts

    <ExportAPI("to_javascript")>
    Public Function getJavascript(chart As Object, <RDefaultExpression> Optional divId As Object = "~random_str(8)") As String
        If chart Is Nothing Then
            Return ""
        End If

        If TypeOf chart Is PieChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, PieChart))
        ElseIf TypeOf chart Is BarChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, BarChart))
        ElseIf TypeOf chart Is VariWideBarChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, VariWideBarChart))
        Else
            Throw New NotImplementedException
        End If
    End Function

    <ExportAPI("to_html")>
    Public Function getHtml(Javascript As String, divId As String, Optional style$ = "height: 450px;") As String
        Return Javascript.GetHtmlViewer(divId, style)
    End Function

    <ExportAPI("piechart")>
    Public Function piechart(data As list,
                             Optional title As String = "title",
                             Optional subtitle As String = "subtitle",
                             Optional serialName As String = "percentage") As PieChart

        Dim pieData As Object() = data.slots.CreateDataSequence
        Dim chart As New PieChart With {
            .chart = chartProfiles.PieChart3D,
            .title = New title With {.text = title},
            .subtitle = New title With {.text = subtitle},
            .tooltip = New tooltip With {
                .pointFormat = "{series.name}: <b>{point.percentage:.1f}%</b>"
            },
            .plotOptions = New plotOptions With {
                .pie = New pieOptions With {
                    .allowPointSelect = True,
                    .cursor = "pointer",
                    .depth = 35,
                    .dataLabels = New dataLabels With {
                        .enabled = True,
                        .format = "{point.name}"
                    }
                }
            },
            .series = {
                New serial With {
                    .type = "pie",
                    .name = serialName,
                    .data = pieData
                }
            }
        }

        Return chart
    End Function

    <ExportAPI("barchart")>
    Public Function BarChart(data As list,
                             Optional title As String = "title",
                             Optional subtitle As String = "subtitle",
                             Optional ylab As String = "Y",
                             Optional serialName As String = "data",
                             Optional env As Environment = Nothing) As BarChart

        Dim values As Dictionary(Of String, Double) = data.AsGeneric(Of Double)(env)
        Dim x As New Axis With {
            .categories = values.Keys.ToArray,
            .labels = New labelOptions With {
                .skew3d = True,
                .style = New styleOptions With {
                    .fontSize = "16px"
                }
            }
        }
        Dim chart As New BarChart With {
            .chart = New chart With {
                .type = "column",
                .options3d = New options3d With {
                    .enabled = True,
                    .alpha = 15,
                    .beta = 15,
                    .viewDistance = 25,
                    .depth = 40
                }
            },
            .title = New title With {.text = title},
            .subtitle = New title With {.text = subtitle},
            .xAxis = x,
            .yAxis = New Axis With {
                .min = 0,
                .title = New title With {
                    .text = ylab,
                    .skew3d = True
                },
                .allowDecimals = False
            },
            .legend = New legendOptions With {
                .reversed = False
            },
            .tooltip = New tooltip With {
                .headerFormat = "<b>{point.key}</b><br>" ', .pointFormat = "<span style=""color:{series.color}"">{series.name}: {point.y:.2f}</span>"
            },
            .plotOptions = New plotOptions With {
                .column = New columnOptions With {
                    .stacking = "normal",
                    .depth = 40
                }
            },
            .series = {
                New GenericDataSerial With {
                    .name = serialName,
                    .data = x.categories _
                        .Select(Function(d) values(d)) _
                        .ToArray
                }
            }
        }

        Return chart
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data">
    ''' data mapping of ``[name -> x, y]``
    ''' </param>
    ''' <param name="title"></param>
    ''' <param name="subtitle"></param>
    ''' <param name="xlab"></param>
    ''' <param name="ylab"></param>
    ''' <param name="serialName"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("varywide_barChart")>
    Public Function VariWideBarChart(data As list,
                                     Optional title As String = "title",
                                     Optional subtitle As String = "subtitle",
                                     Optional xlab As String = "X",
                                     Optional ylab As String = "Y",
                                     Optional serialName As String = "data",
                                     Optional env As Environment = Nothing) As VariWideBarChart

        Dim sdata = data.slots.Keys _
            .Select(Function(name)
                        Dim value As Object() = data.getValue(name, env, New Object() {0.0, 0.0})
                        Dim items As Object() = New Object() {name} _
                            .Join(value) _
                            .ToArray

                        Return items
                    End Function) _
            .ToArray
        Dim chart As New VariWideBarChart With {
            .chart = chartProfiles.VariWide,
            .title = New title With {.text = title},
            .subtitle = New title With {.text = subtitle},
            .yAxis = New Axis With {.title = New title With {.text = ylab}},
            .xAxis = New Axis With {
                .type = "category",
                .title = New title With {.text = xlab}},
                .legend = New legendOptions With {.enabled = False},
                .series = {
                    New serial With {
                        .colorByPoint = True,
                        .tooltip = New tooltip With {
                            .headerFormat = "",
                            .pointFormat = $"<span style=""color:{{point.color}}""><b> {{point.name}}</b></span> <br/><br />" &
                                           $"{ylab}: <b>{{point.y:.2f}}</b><br>" &
                                           $"{xlab}: <b>{{point.z:.2f}}</b><br>"
                        },
                        .name = serialName,
                        .data = sdata
                    }
                }
        }

        Return chart
    End Function
End Module
