Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Language
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML

Public Module PageElements

    ''' <summary>
    ''' assign the page numbers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders">
    ''' the file basename of the html files
    ''' </param>
    ''' <returns></returns>
    <Extension>
    Public Function pageNumbers(report As HTMLReport, orders As String(),
                                Optional pageStart As Integer = 1,
                                Optional ByRef warnings As String() = Nothing) As HTMLReport

        Dim pageNumber As Integer = pageStart
        Dim page As TemplateHandler
        Dim msg As New List(Of String)
        Dim total As Integer = report.pages

        For Each name As String In orders
            page = report.GetPageByName(name)

            If page Is Nothing Then
                Call msg.Add($"missing page '{name}' in the template!")
            End If

            If InStr(page.html, "[#page]") > 1 Then
                page.builder.Replace("[#page]", pageNumber)
                pageNumber += 1
            End If
            If InStr(page.html, "[#total_pages]") > 1 Then
                page.builder.Replace("[#total_pages]", total)
            End If
        Next

        warnings = msg.ToArray

        Return report
    End Function

    ''' <summary>
    ''' fill h1, h2, h3, h4 header titles
    ''' </summary>
    ''' <param name="report">
    ''' place holder for the title could be:
    ''' 
    ''' ``[#h1]``, ``[#h2]``, ``[#h3]`` and ``[#h4]``.
    ''' </param>
    ''' <param name="orders"></param>
    ''' <param name="headerStart"></param>
    ''' <param name="warnings"></param>
    ''' <returns></returns>
    <Extension>
    Public Function pageHeaders(report As HTMLReport, orders As String(),
                                Optional headerStart As String = "1",
                                Optional ByRef warnings As String() = Nothing) As HTMLReport

        Dim page As TemplateHandler
        Dim msg As New List(Of String)
        Dim header As HeaderCounter = HeaderCounter.Parse(headerStart)
        Dim tag As Match
        Dim buffer As New List(Of String)
        Dim headStr As String

        Static placeholder As New Regex("\[#[hH][1234]\]", RegexICSng)

        For Each name As String In orders
            page = report.GetPageByName(name)

            If page Is Nothing Then
                Call msg.Add($"missing page '{name}' in the template!")
            End If

            For Each line As String In page.lines
                tag = placeholder.Match(line)

                If tag.Success Then
                    headStr = header _
                        .Count(tag.Value) _
                        .ToString(tag.Value)
                    line = line.Replace(tag.Value, headStr)
                End If

                buffer.Add(line)
            Next

            Call page.Commit(buffer)
            Call buffer.Clear()
        Next

        warnings = msg.ToArray

        Return report
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders"></param>
    ''' <param name="elementStart"></param>
    ''' <param name="prefix"></param>
    ''' <param name="pattern">
    ''' + ``[#fig]`` for count figures
    ''' + ``[#tab]`` for count tables
    ''' </param>
    ''' <param name="format">
    ''' + p for prefix
    ''' + # for counts
    ''' </param>
    ''' <param name="warnings"></param>
    ''' <returns></returns>
    <Extension>
    Public Function elementCounter(report As HTMLReport, orders As String(),
                                   Optional elementStart As Integer = 1,
                                   Optional prefix As String = "fig",
                                   Optional pattern As String = "[#fig]",
                                   Optional format As String = "p #. ",
                                   Optional ByRef warnings As String() = Nothing) As HTMLReport

        Dim page As TemplateHandler
        Dim msg As New List(Of String)
        Dim i As i32 = elementStart
        Dim buffer As New List(Of String)
        Dim tag As String

        For Each name As String In orders
            page = report.GetPageByName(name)

            If page Is Nothing Then
                Call msg.Add($"missing page '{name}' in the template!")
            End If

            For Each line As String In page.lines
                If line.Contains(pattern) Then
                    tag = format.Replace("#", ++i).Replace("p", prefix)
                    line = line.Replace(pattern, tag)
                End If

                Call buffer.Add(line)
            Next

            Call page.Commit(buffer)
            Call buffer.Clear()
        Next

        warnings = msg.ToArray

        Return report
    End Function
End Module
