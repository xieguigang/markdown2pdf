Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.WebCloud.JavaScript.highcharts
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart
Imports chartProfiles = SMRUCC.WebCloud.JavaScript.highcharts.chart

<Package("highcharts.js")>
Module highcharts

    <ExportAPI("to_javascript")>
    Public Function getJavascript(chart As Object, Optional divId As Object = "") As String
        If chart Is Nothing Then
            Return ""
        End If

        If TypeOf chart Is PieChart Then

        End If
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

End Module
