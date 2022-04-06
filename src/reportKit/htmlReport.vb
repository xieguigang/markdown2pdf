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

Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.VisualBasic.MIME.text.markdown
Imports Microsoft.VisualBasic.Scripting.MetaData
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
        Dim singleVal As String
        Dim strs As String()
        Dim engine As RInterpreter = env.globalEnvironment.Rscript
        Dim value As Object

        For Each key As String In metadata.getNames
            value = metadata.getByName(key)
getStringValue:
            If TypeOf value Is Message Then
                Return value
            End If

            strs = REnv.asVector(Of String)(value)
            singleVal = If(strs.IsNullOrEmpty, "", strs(Scan0))

            If singleVal.StartsWith("~") Then
                value = engine.Evaluate(singleVal.Trim("~"c))
                GoTo getStringValue
            End If

            template(key) = singleVal
        Next

        Return template
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
            Dim tmpdir As String = TempFileSystem.GetAppSysTempFile
            Call New Directory(template).CopyTo(tmpdir).ToArray
            template = tmpdir
        End If

        Return New HTMLReport(template)
    End Function
End Module
