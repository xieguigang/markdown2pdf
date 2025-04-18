﻿#Region "Microsoft.VisualBasic::2cda220e9e8751b55a7c02ceaff12d6d, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//PdfConvert.vb"

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

    '   Total Lines: 337
    '    Code Lines: 242
    ' Comment Lines: 40
    '   Blank Lines: 55
    '     File Size: 12.40 KB


    ' Module PdfConvert
    ' 
    '     Function: BuildArguments, CheckContentSource, createPageArguments, (+2 Overloads) getRepeatParameters, localFileExists
    ' 
    '     Sub: (+4 Overloads) ConvertHtmlToPdf, PdfConvertFailure, RunProcess
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.InteropService
Imports Microsoft.VisualBasic.CommandLine.InteropService.Pipeline
Imports Microsoft.VisualBasic.Language
Imports WkHtmlToPdf.Arguments
Imports ASCII = Microsoft.VisualBasic.Text.ASCII

''' <summary>
''' wkhtmltopdf is able to put several objects into the output file, an object is
''' either a single webpage, a cover webpage or a table of contents.  The objects
''' are put into the output document in the order they are specified on the
''' command line, options can be specified on a per object basis or in the global
''' options area. Options from the Global Options section can only be placed in
''' the global options area
''' </summary>
Public Module PdfConvert

    Public Const PdfPageBreak$ = "<div style='page-break-before:always;'></div>"

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Sub ConvertHtmlToPdf(document As PDFContent, output As PdfOutput)
        ConvertHtmlToPdf(document, output, Nothing)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="document"></param>
    ''' <param name="out$">PDF的保存的文件路径</param>
    <Extension>
    Public Sub ConvertHtmlToPdf(document As PDFContent, out$,
                                Optional debug As Boolean = False,
                                Optional sendMessage As Boolean = False,
                                Optional bin As String = Nothing)
        Call ConvertHtmlToPdf(
            document:=document,
            woutput:=New PdfOutput With {
                .OutputFilePath = out
            },
            environment:=InternalEnvironment.FromDefault(
                debug:=debug,
                sendMessage:=sendMessage,
                bin:=bin
            )
        )
    End Sub

    Const noHTML$ = "You must supply a HTML string, if you have enterd the url: '-'"

    Public Sub ConvertHtmlToPdf(document As PDFContent, woutput As PdfOutput, Optional environment As PdfConvertEnvironment = Nothing)
        Dim html$ = document.GetDocument
        Dim url$() = Nothing

        If TypeOf document Is PdfDocument Then
            url = DirectCast(document, PdfDocument).Url
        End If

        If url.IsNullOrEmpty Then
            If Not html.StringEmpty Then
                With TempFileSystem.GetAppSysTempFile(, App.PID)
                    html.SaveTo(.ByRef)
                    url = { .ByRef}
                End With
            Else
                Throw New PdfConvertException(noHTML)
            End If
        End If

        Dim outputPdfFilePath As String
        Dim delete As Boolean
        Dim argument$

        environment = environment Or InternalEnvironment.Environment

        If woutput.OutputFilePath IsNot Nothing Then
            outputPdfFilePath = woutput.OutputFilePath
            delete = False
        Else
            outputPdfFilePath = TempFileSystem.GetAppSysTempFile(".pdf", App.PID)
            delete = True
        End If

        Dim tmpfile As String = TempFileSystem.GetAppSysTempFile(
            ext:=".pdf",
            sessionID:="wkhtmltopdf_tempWork",
            prefix:="output_contents"
        )

        woutput.OutputFilePath = tmpfile

        If Not File.Exists(environment.WkHtmlToPdfPath) Then
            Throw New PdfConvertException($"File '{environment.WkHtmlToPdfPath}' not found. Check if wkhtmltopdf application is installed.")
        Else
            ' generates the commandline arguments
            argument = document.BuildArguments(url, tmpfile)
        End If

        Try
            Call tmpfile.ParentPath.MakeDir
            Call outputPdfFilePath.ParentPath.MakeDir
            Call environment.RunProcess(
                args:=argument,
                url:=url.JoinBy(ASCII.LF),
                document:=document,
                outputPdfFilePath:=tmpfile,
                woutput:=woutput
            )
            Call tmpfile.FileCopy(outputPdfFilePath)
            Call VBDebugger.EchoLine($"  --> {outputPdfFilePath.GetFullPath}")
        Finally
            If delete AndAlso File.Exists(outputPdfFilePath) Then
                File.Delete(outputPdfFilePath)
            End If
        End Try

        If environment.PopulateSlaveProgressMessage Then
            Call RunSlavePipeline.SendMessage($"""{environment.WkHtmlToPdfPath}"" {argument.TrimNewLine}")
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="document"></param>
    ''' <param name="urls$"></param>
    ''' <param name="pdfOut$"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 这些命令部分之间是具有顺序的
    ''' </remarks>
    <Extension>
    Public Function BuildArguments(document As PDFContent, urls$(), pdfOut$) As String
        Dim paramsBuilder As New StringBuilder

        If Not document.globalOptions Is Nothing Then
            Call paramsBuilder.AppendLine(document.globalOptions.GetCLI)
        End If
        If Not document.pagesize Is Nothing Then
            Call paramsBuilder.AppendLine(document.pagesize.ToString)
        End If

        If Not document.TOC Is Nothing Then
            Call paramsBuilder.AppendLine("toc")
            Call paramsBuilder.AppendLine(document.TOC.GetCLI)
        End If
        If Not document.outline Is Nothing Then
            Call paramsBuilder.AppendLine(document.outline.GetCLI)
        End If

        If Not document.header Is Nothing Then
            Call paramsBuilder.AppendLine(document.header.GetCLI("--header"))
        End If
        If Not document.footer Is Nothing Then
            Call paramsBuilder.AppendLine(document.footer.GetCLI("--footer"))
        End If

        If Not document.page Is Nothing Then
            Dim pageArgument = document.createPageArguments

            If TypeOf document Is PdfDocument AndAlso TryCast(document, PdfDocument).LocalConfigMode Then
                For Each url As String In urls
                    Call paramsBuilder.AppendLine(pageArgument)
                    Call paramsBuilder.AppendLine(url.CLIPath)
                Next
            Else
                Call paramsBuilder.AppendLine(pageArgument)
                Call paramsBuilder.AppendLine($"""{urls.JoinBy(""" """)}""")
            End If
        Else
            Call paramsBuilder.AppendLine($"""{urls.JoinBy(""" """)}""")
        End If

        Call paramsBuilder.AppendLine(pdfOut.CLIPath)

        Return paramsBuilder.ToString
    End Function

    <Extension>
    Private Function createPageArguments(document As PDFContent) As String
        Dim paramsBuilder As New StringBuilder

        ' 2018-10-25 添加page标记会出bug
        ' Call paramsBuilder.AppendLine("page")
        Call paramsBuilder.AppendLine(document.page.GetCLI)

        If Not document.page.cookies.IsNullOrEmpty Then
            Call paramsBuilder.AppendLine(document.page.cookies.getRepeatParameters("--cookie"))
        End If
        If Not document.page.customheader.IsNullOrEmpty Then
            Call paramsBuilder.AppendLine(document.page.customheader.getRepeatParameters("--custom-header"))
        End If
        If Not document.page.runscript.IsNullOrEmpty Then
            Call paramsBuilder.AppendLine(document.page.runscript.getRepeatParameters("--run-script"))
        End If

        Return paramsBuilder.ToString
    End Function

    <Extension>
    Private Function getRepeatParameters(data$(), argName$) As String
        Dim sb As New StringBuilder

        For Each item In data
            Call sb.AppendLine($"{argName} {item.CLIToken}")
        Next

        Return sb.ToString
    End Function

    <Extension>
    Private Function getRepeatParameters(data As Dictionary(Of String, String), argName$) As String
        Dim sb As New StringBuilder

        For Each item In data
            Call sb.AppendLine($"{argName} {item.Key.CLIToken} {item.Value.CLIToken}")
        Next

        Return sb.ToString
    End Function

    ''' <summary>
    ''' run the wkhtmltopdf convert task
    ''' </summary>
    ''' <param name="environment">The environment and configs for invoke the wkhtmltopdf</param>
    ''' <param name="args">the commandline arguments</param>
    ''' <param name="url$"></param>
    ''' <param name="outputPdfFilePath">the file path of the generated pdf file</param>
    ''' <param name="document"></param>
    ''' <param name="woutput"></param>
    <Extension>
    Private Sub RunProcess(environment As PdfConvertEnvironment,
                           args$,
                           url$, outputPdfFilePath$,
                           document As PDFContent,
                           woutput As PdfOutput)

        Using process As New IORedirect(environment.WkHtmlToPdfPath, args, IOredirect:=True)
            If environment.Debug Then
                Call $"Process running in debug mode...".__DEBUG_ECHO
                Call $"Current workspace: {App.CurrentDirectory}.".__DEBUG_ECHO
            End If

            Call process.Start(False)

            If process.WaitForExit(environment.Timeout) AndAlso
               process.WaitOutput(environment.Timeout) AndAlso
               process.WaitError(environment.Timeout) Then

                If process.ExitCode <> 0 AndAlso Not File.Exists(outputPdfFilePath) Then
                    Call process.GetError.PdfConvertFailure(url)
                End If
            Else
                If Not process.HasExited Then
                    Call process.Kill()
                End If

                Throw New PdfConvertTimeoutException()
            End If

            If environment.Debug Then
                Call VBDebugger.EchoLine(process.StandardOutput)
            End If
        End Using

        If woutput.OutputStream IsNot Nothing Then
            Using fs As Stream = New FileStream(outputPdfFilePath, FileMode.Open)
                Dim buffer As Byte() = New Byte(32 * 1024 - 1) {}
                Dim read As i32 = 0

                While (read = fs.Read(buffer, 0, buffer.Length)) > 0
                    Call woutput.OutputStream.Write(buffer, 0, read)
                End While
            End Using
        End If

        If woutput.OutputCallback IsNot Nothing Then
            Dim pdfFileBytes As Byte() = File.ReadAllBytes(outputPdfFilePath)
            Call woutput.OutputCallback()(document, pdfFileBytes)
        End If
    End Sub

    <Extension> Private Sub PdfConvertFailure(error$, url$)
        Dim msg$ = $"Html to PDF conversion of '{url}' failed. 
Wkhtmltopdf output:

{[error]}"
        Throw New PdfConvertException(msg)
    End Sub

    Public Sub ConvertHtmlToPdf(url As String, outputFilePath As String, Optional environment As PdfConvertEnvironment = Nothing)
        Dim [in] As New PdfDocument With {.Url = {url}}
        Dim out As New PdfOutput With {.OutputFilePath = outputFilePath}

        Call ConvertHtmlToPdf([in], out, environment)
    End Sub

    <Extension>
    Private Function localFileExists(file As String) As Boolean
        If file.isURL Then
            Return True
        Else
            Return file.FileExists
        End If
    End Function

    ''' <summary>
    ''' check pdf content source
    ''' </summary>
    ''' <param name="content"></param>
    ''' <returns></returns>
    <Extension>
    Public Function CheckContentSource(content As PdfDocument) As Boolean
        Dim check As Boolean = True
        Dim valid As Boolean
        Dim message As String

        Call VBDebugger.EchoLine($"check for {content.Url.Length} content source urls...")

        For Each file As String In content.Url
            valid = file.localFileExists
            message = $"{file.GetFullPath} ... [{valid.ToString.ToLower}]"

            Call VBDebugger.EchoLine(message)

            If Not valid Then
                check = False
            End If
        Next

        Return check
    End Function
End Module
