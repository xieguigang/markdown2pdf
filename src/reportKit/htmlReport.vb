#Region "Microsoft.VisualBasic::aa2037c010be3151c703068db7b5b27a, markdown2pdf\reportKit\html.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xie (genetics@smrucc.org)
'       xieguigang (xie.guigang@live.com)
' 
' Copyright (c) 2018 GPL3 Licensed
' 
' 
' GNU GENERAL PUBLIC LICENSE (GPL3)
' 
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.



' /********************************************************************************/

' Summaries:

' Module html
' 
'     Function: markdownToHtml, template
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.MIME.text.markdown
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML
Imports SMRUCC.Rsharp.Interpreter
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Components
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.Rsharp.Runtime.Interop
Imports REnv = SMRUCC.Rsharp.Runtime

''' <summary>
''' html templat handler
''' </summary>
<Package("htmlReport", Category:=APICategories.UtilityTools)>
Public Module htmlReportEngine

    ''' <summary>
    ''' Create a html template model from the given template file
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    <ExportAPI("htmlTemplate")>
    Public Function template(url As String) As TemplateHandler
        Return New TemplateHandler(file:=url)
    End Function

    ''' <summary>
    ''' assign the page numbers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders">
    ''' the file basename of the html files
    ''' </param>
    ''' <returns></returns>
    <ExportAPI("pageNumbers")>
    Public Function pageNumbers(report As HTMLReport, orders As String(),
                                Optional pageStart As Integer = 1,
                                Optional env As Environment = Nothing) As HTMLReport

        Dim warnings As String() = Nothing
        Dim output As HTMLReport = report.pageNumbers(orders, pageStart, warnings)

        For Each msg As String In warnings
            Call env.AddMessage(msg, MSG_TYPES.WRN)
        Next

        Return output
    End Function

    <ExportAPI("encodeLocalURL")>
    Public Function encodeLocalURL(filepath As String) As String
        Return ImageSolver.encodeLocalURL(filepath)
    End Function

    ''' <summary>
    ''' Render markdown to html text
    ''' </summary>
    ''' <param name="markdown"></param>
    ''' <returns></returns>
    <ExportAPI("markdown.html")>
    Public Function markdownToHtml(markdown As String) As String
        Static render As New MarkdownHTML
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

        Dim strs As String() = REnv.asVector(Of String)(value)
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
                Return Internal.debug.stop({$"error while handling resource file: {file}!", $"file: {file}"}, env)
            End Try

            contents(file) = html
        Next

        Return New list With {.slots = contents}
    End Function

    <ExportAPI("flush")>
    Public Function saveReport(template As HTMLReport) As Boolean
        Call template.Save()
        Return True
    End Function

    ''' <summary>
    ''' Create a html template model from a 
    ''' given report template directory.
    ''' </summary>
    ''' <param name="template"></param>
    ''' <returns></returns>
    <ExportAPI("reportTemplate")>
    Public Function reportTemplate(template As String, Optional copyToTemp As Boolean = True) As HTMLReport
        If template.ExtensionSuffix("zip") Then
            Dim tempdir As String = TempFileSystem.TempDir
            Call Zip.ImprovedExtractToDirectory(template, tempdir, Overwrite.Always)
            template = tempdir
            copyToTemp = False
        End If

        If copyToTemp Then
            Dim tmpdir As String = TempFileSystem.GetAppSysTempFile(ext:="__pdf_template", sessionID:="", prefix:="wkhtmltopdf")
            Call New Directory(template).CopyTo(tmpdir).ToArray
            template = tmpdir
        End If

        Return New HTMLReport(template)
    End Function
End Module
