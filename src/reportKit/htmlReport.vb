#Region "Microsoft.VisualBasic::b5852cafc5c217b4ddeb79ec319753b3, G:/GCModeller/src/runtime/markdown2pdf/src/reportKit//htmlReport.vb"

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

'   Total Lines: 447
'    Code Lines: 302
' Comment Lines: 76
'   Blank Lines: 69
'     File Size: 17.07 KB


' Module htmlReportEngine
' 
'     Function: countFigures, countTables, encodeLocalURL, evalString, exportJSON
'               fillContent, getMetaData, htmlRender, loadResource, markdownToHtml
'               markdownToLaTex, markdownToText, pageBreak, pageHeaders, pageNumbers
'               reportTemplate, saveReport, template
' 
' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Bootstrap5
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Emit.Delegates
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.MIME.text.markdown
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML
Imports SMRUCC.Rsharp.Development
Imports SMRUCC.Rsharp.Interpreter
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Components
Imports SMRUCC.Rsharp.Runtime.Components.[Interface]
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.Rsharp.Runtime.Interop
Imports SMRUCC.Rsharp.Runtime.Vectorization
Imports WkHtmlToPdf
Imports WkHtmlToPdf.LaTex
Imports any = Microsoft.VisualBasic.Scripting
Imports Directory = Microsoft.VisualBasic.FileIO.Directory
Imports MarkdownHTML = Microsoft.VisualBasic.MIME.text.markdown.MarkdownRender
Imports RInternal = SMRUCC.Rsharp.Runtime.Internal

''' <summary>
''' html templat handler
''' </summary>
<Package("htmlReport", Category:=APICategories.UtilityTools)>
Public Module htmlReportEngine

    ''' <summary>
    ''' Create a html template model from the given template file
    ''' </summary>
    ''' <param name="url">should be a local file its file path, andalso this function also supports
    ''' the http url for http get and load the template document text.</param>
    ''' <returns></returns>
    <ExportAPI("htmlTemplate")>
    Public Function template(url As String) As TemplateHandler
        Return New TemplateHandler(file:=url)
    End Function

    ''' <summary>
    ''' insert a pdf pagebreak div into the current html template string
    ''' </summary>
    ''' <returns></returns>
    <ExportAPI("pageBreak")>
    Public Function pageBreak() As String
        Return PdfConvert.PdfPageBreak
    End Function

    ''' <summary>
    ''' assign the page numbers to the html templates
    ''' </summary>
    ''' <param name="report">the html report template model</param>
    ''' <param name="orders">
    ''' the file basename of the html files to sort of the pages inside the report model object.
    ''' </param>
    ''' <param name="pageStart">
    ''' page number count start, default is count start from 1
    ''' </param>
    ''' <returns></returns>
    ''' <remarks>
    ''' the placeholder of the page number inside the template document text should be:
    ''' 
    ''' 1. [#page] for the page number of current page
    ''' 2. [#total_pages] for the total page numbers which is count from the template files.
    ''' 
    ''' </remarks>
    <ExportAPI("pageNumbers")>
    Public Function pageNumbers(report As HTMLReport, orders As String(),
                                Optional pageStart As Integer = 1,
                                Optional env As Environment = Nothing) As HTMLReport

        Dim warnings As String() = Nothing
        Dim output As HTMLReport = report.pageNumbers(orders, pageStart, warnings)
        Dim println = env.WriteLineHandler

        Call println("get pdf page orders:")
        Call println(orders)

        For Each msg As String In warnings.SafeQuery
            Call env.AddMessage(msg, MSG_TYPES.WRN)
        Next

        Return output
    End Function

    ''' <summary>
    ''' assign the page headers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders"></param>
    ''' <param name="headerStart"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' place holder for the title could be:
    ''' 
    ''' ``[#h1]``, ``[#h2]``, ``[#h3]`` and ``[#h4]``.
    ''' </remarks>
    <ExportAPI("pageHeaders")>
    Public Function pageHeaders(report As Object,
                                Optional orders As String() = Nothing,
                                Optional headerStart As String = "1",
                                Optional env As Environment = Nothing) As Object

        If report Is Nothing Then
            Return RInternal.debug.stop("the required report object can not be nothing!", env)
        End If
        If TypeOf report Is TemplateHandler Then
            report = New HTMLReport(DirectCast(report, TemplateHandler))
        End If
        If Not TypeOf report Is HTMLReport Then
            Return Message.InCompatibleType(GetType(HTMLReport), report.GetType, env)
        End If

        Dim template As HTMLReport = DirectCast(report, HTMLReport)
        Dim warnings As String() = Nothing

        If orders.IsNullOrEmpty Then
            If template.pages = 1 Then
                orders = {template.templates.First.Key}
            Else
                Return RInternal.debug.stop("the page orders must be specificed when the report template contains multuple pages!", env)
            End If
        End If

        Call template.pageHeaders(orders, headerStart, warnings)

        For Each line As String In warnings
            Call env.AddMessage(line, MSG_TYPES.WRN)
        Next

        Return template
    End Function

    ''' <summary>
    ''' assign the figure numbers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders"></param>
    ''' <param name="figStart"></param>
    ''' <param name="prefix"></param>
    ''' <param name="format"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' the placeholder of the figure number in your template document text should be ``[#fig]``.
    ''' </remarks>
    <ExportAPI("countFigures")>
    Public Function countFigures(report As Object,
                                 Optional orders As String() = Nothing,
                                 Optional figStart As Integer = 1,
                                 Optional prefix As String = "fig",
                                 Optional format As String = "p #. ",
                                 Optional env As Environment = Nothing) As Object

        If report Is Nothing Then
            Return RInternal.debug.stop("the required report object can not be nothing!", env)
        End If
        If TypeOf report Is TemplateHandler Then
            report = New HTMLReport(DirectCast(report, TemplateHandler))
        End If
        If Not TypeOf report Is HTMLReport Then
            Return Message.InCompatibleType(GetType(HTMLReport), report.GetType, env)
        End If

        Dim template As HTMLReport = DirectCast(report, HTMLReport)
        Dim warnings As String() = Nothing

        If orders.IsNullOrEmpty Then
            If template.pages = 1 Then
                orders = {template.templates.First.Key}
            Else
                Return RInternal.debug.stop("the page orders must be specificed when the report template contains multuple pages!", env)
            End If
        End If

        Call template.elementCounter(orders, figStart, prefix,, format, warnings)

        For Each line As String In warnings
            Call env.AddMessage(line, MSG_TYPES.WRN)
        Next

        Return template
    End Function

    ''' <summary>
    ''' assign the table numbers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders"></param>
    ''' <param name="tableStart"></param>
    ''' <param name="prefix"></param>
    ''' <param name="format"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' the placeholder of the table number in your template text should be ``[#tab]``.
    ''' </remarks>
    <ExportAPI("countTables")>
    Public Function countTables(report As Object,
                                Optional orders As String() = Nothing,
                                Optional tableStart As Integer = 1,
                                Optional prefix As String = "table",
                                Optional format As String = "p #. ",
                                Optional env As Environment = Nothing) As Object
        If report Is Nothing Then
            Return RInternal.debug.stop("the required report object can not be nothing!", env)
        End If
        If TypeOf report Is TemplateHandler Then
            report = New HTMLReport(DirectCast(report, TemplateHandler))
        End If
        If Not TypeOf report Is HTMLReport Then
            Return Message.InCompatibleType(GetType(HTMLReport), report.GetType, env)
        End If

        Dim template As HTMLReport = DirectCast(report, HTMLReport)
        Dim warnings As String() = Nothing

        If orders.IsNullOrEmpty Then
            If template.pages = 1 Then
                orders = {template.templates.First.Key}
            Else
                Return RInternal.debug.stop("the page orders must be specificed when the report template contains multuple pages!", env)
            End If
        End If

        Call template.elementCounter(orders, tableStart, prefix, "[#tab]", format, warnings)

        For Each line As String In warnings
            Call env.AddMessage(line, MSG_TYPES.WRN)
        Next

        Return template
    End Function

    ''' <summary>
    ''' make url encoded
    ''' </summary>
    ''' <param name="filepath"></param>
    ''' <returns></returns>
    <ExportAPI("encodeLocalURL")>
    Public Function encodeLocalURL(filepath As String) As String
        Return ImageSolver.encodeLocalURL(filepath)
    End Function

    <ExportAPI("defaultSyntaxHighlight")>
    Public Function defaultcodeSyntaxHighlight() As HtmlRender.CodeHtmlSyntaxHighlight
        Return Function(code, lang)
                   Select Case Strings.Trim(lang).ToLower
                       Case "r", "json", "javascript", "js", "ts"
                           Dim html As New StringBuilder
                           Dim console As New StringWriter(html)

                           Call ConsoleSyntaxHighlightPrinter.PrintCode(code, console, OutputEnvironments.Html)
                           Call console.Flush()

                           Return html.ToString
                       Case Else
                           Return If(code, "").Replace("<", "&lt;")
                   End Select
               End Function
    End Function

    ''' <summary>
    ''' create a markdown to html render
    ''' </summary>
    ''' <param name="image_url">
    ''' apply for the document template rendering
    ''' </param>
    <ExportAPI("html_render")>
    <RApiReturn(GetType(HtmlRender), GetType(BootstrapRender))>
    Public Function htmlRender(<RRawVectorArgument>
                               Optional image_class As Object = Nothing,
                               Optional image_url As Object = Nothing,
                               Optional framework As String = "bootstrap",
                               Optional syntax_highlight As HtmlRender.CodeHtmlSyntaxHighlight = Nothing,
                               Optional env As Environment = Nothing) As Object

        Dim render As HtmlRender

        Select Case Strings.Trim(framework).ToLower
            Case "bootstrap" : render = New BootstrapRender

            Case Else
                render = New HtmlRender
        End Select

        render.CodeSyntaxHighlight = syntax_highlight
        render.image_class = CLRVector.asCharacter(image_class).JoinBy(" ")

        If image_url IsNot Nothing Then
            If TypeOf image_url Is MethodInfo Then
                Dim del As MethodInfo = image_url

                render.SetImageUrlRouter(
                    Function(url) As String
                        Return any.ToString(del.Invoke(Nothing, New Object() {url}))
                    End Function)
            ElseIf image_url.GetType.ImplementInterface(Of RFunction) Then
                Dim fun As RFunction = image_url

                render.SetImageUrlRouter(
                    Function(url) As String
                        Dim out As Object = fun.Invoke(env, InvokeParameter.CreateLiterals(url))

                        If Program.isException(out) Then
                            Throw DirectCast(out, Message).ToCLRException
                        Else
                            Return CLRVector _
                                .asCharacter(out) _
                                .DefaultFirst
                        End If
                    End Function)
            Else
                Return Message.InCompatibleType(GetType(RFunction), image_url.GetType, env)
            End If
        End If

        Return render
    End Function

    ''' <summary>
    ''' Render markdown to html text
    ''' </summary>
    ''' <param name="markdown"></param>
    ''' <returns></returns>
    <ExportAPI("markdown.html")>
    <RApiReturn(TypeCodes.string)>
    Public Function markdownToHtml(markdown As String, Optional htmlRender As HtmlRender = Nothing) As Object
        Return New MarkdownHTML(If(htmlRender, New HtmlRender)).Transform(markdown)
    End Function

    <ExportAPI("markdown.text")>
    <RApiReturn(TypeCodes.string)>
    Public Function markdownToText(markdown As String) As String
        Return New MarkdownHTML(New TextRender).Transform(markdown)
    End Function

    <ExportAPI("markdown.latex")>
    Public Function markdownToLaTex(markdown As String) As String
        Static render As New MarkdownHTML(render:=New TexRender)
        Return render.Transform(markdown)
    End Function

    ''' <summary>
    ''' do report data interpolation.
    ''' </summary>
    ''' <param name="template"></param>
    ''' <param name="metadata"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("interpolate")>
    <ROperator("+")>
    <RApiReturn(GetType(HTMLReport))>
    Public Function fillContent(template As HTMLReport, metadata As list, Optional env As Environment = Nothing) As Object
        Dim singleVal As [Variant](Of String, Message)
        Dim engine As RInterpreter = env.globalEnvironment.Rscript
        Dim value As Object

        For Each key As String In metadata.getNames
            value = metadata.getByName(key)
            singleVal = engine.evalString(value)

            If singleVal Like GetType(Message) Then
                Return singleVal.TryCast(Of Message)
            Else
                template(key) = singleVal.TryCast(Of String)
            End If
        Next

        Return template
    End Function

    <Extension>
    Public Function evalString(engine As RInterpreter, value As Object) As [Variant](Of String, Message)
getStringValue:

        If TypeOf value Is Message Then
            Return DirectCast(value, Message)
        End If

        Dim strs As String() = CLRVector.asCharacter(value)
        Dim singleVal = If(strs.IsNullOrEmpty, "", strs(Scan0))

        If singleVal.StartsWith("~") Then
            value = engine.Evaluate(singleVal.Trim("~"c))
            GoTo getStringValue
        End If

        Return singleVal
    End Function

    Private Function getMetaData(meta As list, engine As RInterpreter) As Object
        Dim metadata As New Dictionary(Of String, String)
        Dim singleVal As [Variant](Of String, Message)

        If meta Is Nothing Then
            meta = New list
        End If

        For Each line In meta.slots
            singleVal = engine.evalString(line.Value)

            If singleVal Like GetType(Message) Then
                Return singleVal.TryCast(Of Message)
            Else
                metadata(line.Key) = singleVal.TryCast(Of String)
            End If
        Next

        Return metadata
    End Function

    ''' <summary>
    ''' Load resource files for build html report
    ''' 
    ''' load the resource files based on the description list data, 
    ''' and then generates the html segments for the resource files.
    ''' </summary>
    ''' <param name="description">
    ''' the resource file contents in this description data 
    ''' supports file types:
    ''' 
    ''' 1. ``*.txt`` for plant text file
    ''' 2. ``*.csv`` for data table file
    ''' 3. ``*.png/jpg/bmp`` for raster image file
    ''' 4. ``*.svg`` for vector image file
    ''' 5. ``*.html`` for html text file
    ''' 6. ``*.md`` for makrdown text file, the markdown text file 
    '''      will be rendering as the html document text at first
    '''      and then returns a html document to the rendering 
    '''      engine.
    '''      
    ''' </param>
    ''' <param name="workdir"></param>
    ''' <param name="meta"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("loadResource")>
    <RApiReturn(GetType(list))>
    Public Function loadResource(description As JsonObject,
                                 <RDefaultExpression>
                                 Optional workdir As Object = "~getwd();",
                                 Optional meta As list = Nothing,
                                 Optional env As Environment = Nothing) As Object

        Dim engine As RInterpreter = env.globalEnvironment.Rscript
        Dim metadata As Object = getMetaData(meta, engine)

        If TypeOf metadata Is Message Then
            Return metadata
        End If

        Dim contents As New Dictionary(Of String, Object)
        Dim res As Dictionary(Of String, ResourceDescription) = Interpolation.ParseResourceList(description, metadata)

        For Each file As String In res.Keys
            Dim resVal As ResourceDescription = res(file)
            Dim html As String

            Try
                html = resVal _
                    .FillMetadata(metadata) _
                    .CreateResourceHtml(workdir)
            Catch ex As Exception
                Return RInternal.debug.stop({$"error while handling resource file: {file}!", $"file: {file}"}, env)
            End Try

            contents(file) = html
        Next

        Return New list With {.slots = contents}
    End Function

    ''' <summary>
    ''' save the modified interpolated html
    ''' template data onto the disk file.
    ''' </summary>
    ''' <param name="template"></param>
    ''' <param name="outputdir">
    ''' export of the page files into this new output 
    ''' directory instead of export to the source 
    ''' folder where this report object is loaded via 
    ''' the ``reportTemplate`` function.
    ''' </param>
    ''' <returns></returns>
    <ExportAPI("flush")>
    Public Function saveReport(template As HTMLReport, Optional outputdir As String = Nothing) As Boolean
        Call template.Save(outputdir)
        Return True
    End Function

    <ExportAPI("exportJSON")>
    Public Function exportJSON(report As HTMLReport) As Dictionary(Of String, String)
        Return report.ExportJSON
    End Function

    ''' <summary>
    ''' Create a html template model from a 
    ''' given report template directory.
    ''' </summary>
    ''' <param name="template">
    ''' the zip file package or the template html resource directory path
    ''' </param>
    ''' <param name="copyToTemp">
    ''' copy the template source to a temp directory and then load the template files?
    ''' </param>
    ''' <param name="tmpdir">
    ''' config this temp dir parameter for run debug
    ''' </param>
    ''' <returns></returns>
    <ExportAPI("reportTemplate")>
    Public Function reportTemplate(template As String,
                                   Optional copyToTemp As Boolean = True,
                                   Optional tmpdir As String = Nothing) As HTMLReport

        If template.ExtensionSuffix("zip") Then
            Dim tempdir As String = TempFileSystem.TempDir
            Call Zip.ImprovedExtractToDirectory(template, tempdir, Overwrite.Always)
            template = tempdir
            copyToTemp = False
        End If

        If copyToTemp Then
            Dim dir As New Directory(template)

            tmpdir = If(tmpdir, TempFileSystem.GetAppSysTempFile(
                ext:="__pdf_template",
                sessionID:="",
                prefix:="wkhtmltopdf")
            )
            dir.CopyTo(tmpdir).ToArray
            template = tmpdir
        End If

        Return New HTMLReport(template)
    End Function
End Module
