Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.Rsharp.Runtime.Interop
Imports SMRUCC.Rsharp.Runtime.Vectorization
Imports SMRUCC.WebCloud.JavaScript.highcharts
Imports SMRUCC.WebCloud.JavaScript.highcharts.BarChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.Javascript
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart
Imports SMRUCC.WebCloud.JavaScript.highcharts.viz3D
Imports any = Microsoft.VisualBasic.Scripting
Imports chartProfiles = SMRUCC.WebCloud.JavaScript.highcharts.chart
Imports REnv = SMRUCC.Rsharp.Runtime

<Package("highcharts.js")>
Module highcharts

    <ExportAPI("to_javascript")>
    Public Function getJavascript(chart As Object,
                                  <RDefaultExpression>
                                  Optional divId As Object = "~random_str(8)",
                                  Optional env As Environment = Nothing) As String
        If chart Is Nothing Then
            Return ""
        End If

        If TypeOf chart Is PieChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, PieChart))
        ElseIf TypeOf chart Is BarChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, BarChart))
        ElseIf TypeOf chart Is VariWideBarChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, VariWideBarChart))
        ElseIf TypeOf chart Is VariablePieChart Then
            Return any.ToString(divId).WriteJavascript(DirectCast(chart, VariablePieChart))
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
                             Optional serialName As String = "percentage",
                             <RRawVectorArgument>
                             Optional backgroundColor As Object = "#ffffff",
                             Optional env As Environment = Nothing) As PieChart

        Dim bg As String = RColorPalette.getColor(backgroundColor, [default]:="#ffffff")
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

        Return chart.setBackground(bg.TranslateColor)
    End Function

    ''' <summary>
    ''' It is a column char actually
    ''' </summary>
    ''' <param name="data">
    ''' a dictionary of [name => value] tuple paired list
    ''' </param>
    ''' <param name="title"></param>
    ''' <param name="subtitle"></param>
    ''' <param name="ylab"></param>
    ''' <param name="serialName"></param>
    ''' <param name="backgroundColor"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("barchart")>
    Public Function BarChart(data As list,
                             Optional title As String = "title",
                             Optional subtitle As String = "subtitle",
                             Optional ylab As String = "Y",
                             Optional serialName As String = "data",
                             <RRawVectorArgument>
                             Optional backgroundColor As Object = "#ffffff",
                             Optional display3D As Boolean = True,
                             Optional env As Environment = Nothing) As BarChart

        Dim values As Dictionary(Of String, Double) = data.AsGeneric(Of Double)(env)
        Dim x As New Axis With {
            .categories = values.Keys.ToArray,
            .labels = New labelOptions With {
                .skew3d = display3D,
                .style = New styleOptions With {
                    .fontSize = "16px"
                }
            },
            .title = New title With {
                .skew3d = display3D,
                .text = serialName
            }
        }
        Dim bg As String = RColorPalette.getColor(backgroundColor, [default]:="#ffffff")
        Dim chart As New BarChart With {
            .chart = New chart With {
                .type = "column",
                .options3d = New options3d With {
                    .enabled = display3D,
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
                    .skew3d = display3D
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

        Return chart.setBackground(bg.TranslateColor)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="innerSize">radius size in percentage.</param>
    ''' <param name="title$"></param>
    ''' <param name="subtitle$"></param>
    ''' <param name="pointerName$"></param>
    ''' <param name="serialName$"></param>
    ''' <returns></returns>
    <ExportAPI("varywide_pieChart")>
    Public Function VariablePieChart(data As dataframe,
                                     Optional innerSize$ = "20",
                                     Optional title$ = "pie chart",
                                     Optional subtitle$ = "pie chart",
                                     Optional pointerName$ = "item",
                                     Optional serialName$ = "serial name",
                                     <RRawVectorArgument>
                                     Optional backgroundColor As Object = "#ffffff") As VariablePieChart

        Dim names As String() = data.rownames
        Dim y As Double() = CLRVector.asNumeric(data.getColumnVector("y"))
        Dim z As Double() = CLRVector.asNumeric(data.getColumnVector("z"))
        Dim dataset As VariablePieSerialData() = names _
            .Select(Function(name, i)
                        Return New VariablePieSerialData With {
                            .name = name,
                            .y = y(i),
                            .z = z(i)
                        }
                    End Function) _
            .GroupBy(Function(v) v.name) _
            .Select(Function(v)
                        Dim my As Double = v.Select(Function(i) i.y).Average
                        Dim mz As Double = v.Select(Function(i) i.z).Average

                        Return New VariablePieSerialData With {
                            .name = v.Key,
                            .y = my,
                            .z = mz
                        }
                    End Function) _
            .ToArray
        Dim bg As String = RColorPalette.getColor(backgroundColor, [default]:="#ffffff")
        Dim serial As New VariablePieSerial With {
            .minPointSize = 10,
            .innerSize = $"{innerSize}%",
            .zMin = 0,
            .name = serialName,
            .data = dataset
        }

        Dim chart As New VariablePieChart With {
            .chart = VariablePieChart.ChartType,
            .title = New title With {.text = title},
            .subtitle = New title With {.text = subtitle},
            .tooltip = New tooltip With {
                .headerFormat = "",
                .pointFormat = $"<span style=""color:{{point.color}}""><b> {{point.name}}</b></span> <br/>" &
                               $"{pointerName}: <b>{{point.y}}</b><br/>"
            },
            .series = {serial}
        }

        Return chart.setBackground(bg.TranslateColor)
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
                                     <RRawVectorArgument>
                                     Optional backgroundColor As Object = "#ffffff",
                                     Optional env As Environment = Nothing) As VariWideBarChart

        Dim bg As String = RColorPalette.getColor(backgroundColor, [default]:="#ffffff")
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
                .labels = New labelOptions With {
                    .rotation = -45,
                    .autoRotation = {-45},
                    .enabled = True
                },
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

        Return chart.setBackground(bg.TranslateColor)
    End Function
End Module
