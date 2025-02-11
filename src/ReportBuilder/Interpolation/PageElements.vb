#Region "Microsoft.VisualBasic::440adce6f25247c2b610715211abc6b7, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/PageElements.vb"

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

    '   Total Lines: 163
    '    Code Lines: 98
    ' Comment Lines: 37
    '   Blank Lines: 28
    '     File Size: 5.56 KB


    ' Module PageElements
    ' 
    '     Function: elementCounter, pageHeaders, pageNumbers
    ' 
    ' /********************************************************************************/

#End Region

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
    ''' <remarks>
    ''' the placeholder of the page number inside the template document text should be:
    ''' 
    ''' 1. [#page] for the page number of current page
    ''' 2. [#total_pages] for the total page numbers which is count from the template files.
    ''' 
    ''' </remarks>
    <Extension>
    Public Function pageNumbers(report As HTMLReport, orders As String(),
                                Optional pageStart As Integer = 1,
                                Optional ByRef warnings As String() = Nothing,
                                Optional total_overrides As Integer = -1,
                                Optional println As Action(Of Object) = Nothing) As HTMLReport

        Dim pageNumber As Integer = pageStart
        Dim page As TemplateHandler
        Dim msg As New List(Of String)
        Dim total As Integer = If(total_overrides > 0, total_overrides, report.pages)

        For Each name As String In orders
            page = report.GetPageByName(name)

            If Not println Is Nothing Then
                Call println($"{name} -> {pageNumber}")
            End If

            If page Is Nothing Then
                Call msg.Add($"missing page '{name}' in the template!")
            Else
                If InStr(page.html, "[#page]") > 1 Then
                    page.builder.Replace("[#page]", pageNumber)
                    pageNumber += 1
                End If
                If InStr(page.html, "[#total_pages]") > 1 Then
                    page.builder.Replace("[#total_pages]", total)
                End If
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

