#Region "Microsoft.VisualBasic::6ab1e352e28905a404be71184ea7895b, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/highcharts.js//Javascript.vb"

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

    '   Total Lines: 136
    '    Code Lines: 94
    ' Comment Lines: 26
    '   Blank Lines: 16
    '     File Size: 5.05 KB


    ' Module Javascript
    ' 
    '     Function: CreateDataSequence, FixDate, GetHtmlViewer, GetHtmlViews, NewtonsoftJsonWriter
    '               RemovesEmptyLine, RemoveTrailingComma, WriteJavascript
    ' 
    '     Sub: WriteHighchartsHTML
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text.Xml
Imports Newtonsoft.Json
Imports SMRUCC.WebCloud.JavaScript.highcharts.PieChart
Imports r = System.Text.RegularExpressions.Regex

''' <summary>
''' The highcharts.js helper
''' </summary>
Public Module Javascript

    ''' <summary>
    ''' 在这里输出的日期格式都被统一为``\/Date(1198908717056)\/``.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function NewtonsoftJsonWriter(Of T)(obj As T) As String
        Return JsonConvert.SerializeObject(
            obj,
            Formatting.Indented,
            settings:=New JsonSerializerSettings With {
                .DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            }
        )
    End Function

    ''' <summary>
    ''' Generates the javascript call for highcharts.js charting from a given data model. 
    ''' </summary>
    ''' <typeparam name="S"></typeparam>
    ''' <param name="container$">The ``id`` attribute of a ``&lt;div>`` html tag.</param>
    ''' <param name="chart">highcharts.js data model.</param>
    ''' <returns></returns>
    <Extension>
    Public Function WriteJavascript(Of S)(container$, chart As Highcharts(Of S), Optional UTCdate As Boolean = True) As String
        Dim knownTypes As Type() = {
            GetType(String),
            GetType(Double),
            GetType(pieData),
            GetType(Date)
        }
        'Dim json$ = chart _
        '    .GetType _
        '    .GetObjectJson(chart, indent:=True, knownTypes:=knownTypes) _
        '    .RemoveJsonNullItems _
        '    .FixDate
        Dim JSON$ = Microsoft.VisualBasic.MIME.application.json.JSONSerializer.GetJson(chart) _
            .FixDate(UTCdate) _
            .RemoveJsonNullItems _
            .RemoveTrailingComma _
            .RemovesEmptyLine
        Dim javascript$ = $"Highcharts.chart('{container}', {LambdaWriter.StripLambda(JSON)});"
        Return javascript
    End Function

    Const MicrosoftDatePattern$ = "[""]\\/Date\(\d+(.\d+)?\)\\/[""]"

    <Extension>
    Public Function FixDate(json$, UTCdate As Boolean) As String
        If Not UTCdate Then
            Return json
        Else
            Dim dates$() = r.Matches(json, MicrosoftDatePattern, RegexICSng).ToArray
            Dim sb As New StringBuilder(json)

            For Each d As String In dates
                Dim [date] As Date = d.LoadJSON(Of Date)
                Dim UTC$ = $"Date.UTC({[date].Year}, {[date].Month}, {[date].Day})"

                Call sb.Replace(d, UTC)
            Next

            Return sb.ToString
        End If
    End Function

    <Extension>
    Public Function RemovesEmptyLine(str As String) As String
        Return r.Replace(str, "(((\r)|(\n)){2,}\s*)+", vbCrLf, RegexICMul)
    End Function

    <Extension>
    Public Function RemoveTrailingComma(json As String) As String
        Dim trim As New StringBuilder(json)

        For Each match As String In json.Matches(",\s*\]", RegexICSng)
            Call trim.Replace(match, vbCrLf & "]")
        Next
        For Each match As String In json.Matches(",\s*}", RegexICSng)
            Call trim.Replace(match, vbCrLf & "}")
        Next

        Return trim.ToString
    End Function

    ''' <summary>
    ''' [key => value] to [[key, value]]
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function CreateDataSequence(obj As Dictionary(Of String, Object)) As Object()
        Return obj _
            .Select(Function(item) {CObj(item.Key), item.Value}) _
            .ToArray
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Sub WriteHighchartsHTML(template As StringBuilder, name$, javascript$, div$, Optional style$ = "height: 450px;")
        Call template.Replace(name, javascript.GetHtmlViewer(div, style))
    End Sub

    <Extension>
    Public Function GetHtmlViewer(javascript$, div$, Optional style$ = "width:100%; height: 450px;", Optional class$ = "") As String
        Return sprintf(
            <p>
                <div id=<%= div %> class=<%= [class] %> style=<%= style %>></div>
                <script type="text/javascript">
                    %s
                </script>
            </p>, javascript)
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function GetHtmlViews(Of T)(chart As Highcharts(Of T), div$, Optional style$ = "width:100%; height: 450px;", Optional class$ = "") As String
        Return div.WriteJavascript(chart).GetHtmlViewer(div, style, [class])
    End Function
End Module
