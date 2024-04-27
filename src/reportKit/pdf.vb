#Region "Microsoft.VisualBasic::51a11a2a7c618c2e5612db29efc772ca, G:/GCModeller/src/runtime/markdown2pdf/src/reportKit//pdf.vb"

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

    '   Total Lines: 203
    '    Code Lines: 140
    ' Comment Lines: 40
    '   Blank Lines: 23
    '     File Size: 8.42 KB


    ' Module pdf
    ' 
    '     Function: logoHtml, makePDF, pdfDecoration, pdfGlobalOptions, pdfPageOptions
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
'Imports iText.IO.Image
'Imports iText.Kernel.Pdf
'Imports iText.Layout
'Imports iText.Layout.Element
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Net.Http
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text.Xml
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Interop
Imports SMRUCC.Rsharp.Runtime.Vectorization
Imports WkHtmlToPdf
Imports WkHtmlToPdf.Arguments
Imports REnv = SMRUCC.Rsharp.Runtime

<Package("pdf", Category:=APICategories.UtilityTools)>
Module pdf

    '''' <summary>
    '''' convert wmf image to pdf file
    '''' </summary>
    '''' <param name="wmf"></param>
    '''' <param name="pdfsave"></param>
    '<ExportAPI("convertWmf")>
    'Public Sub convertWmf(wmf As String, pdfsave As String)
    '    Dim imageData = ImageDataFactory.Create(wmf)
    '    Dim PdfDocument As New iText.Kernel.Pdf.PdfDocument(New PdfWriter(pdfsave))
    '    Dim document As New Document(PdfDocument)

    '    Dim image = New Image(imageData)
    '    image.SetWidth(PdfDocument.GetDefaultPageSize().GetWidth() - 50)
    '    image.SetAutoScaleHeight(True)

    '    document.Add(image)
    '    PdfDocument.Close()
    'End Sub

    <ExportAPI("pdfPage_options")>
    Public Function pdfPageOptions(Optional javascriptdelay# = 3000,
                                   Optional loaderrorhandling As handlers = handlers.abort,
                                   Optional debugjavascript As Boolean = True,
                                   Optional enableforms As Boolean = True) As Page

        Return New Page With {
            .javascriptdelay = javascriptdelay,
            .loaderrorhandling = loaderrorhandling,
            .debugjavascript = debugjavascript,
            .enableforms = enableforms
        }
    End Function

    <ExportAPI("pdfGlobal_options")>
    Public Function pdfGlobalOptions(Optional margintop% = 0,
                                     Optional marginleft% = 0,
                                     Optional marginright% = 0,
                                     Optional marginbottom% = 0,
                                     Optional imagequality% = 100,
                                     Optional title$ = "") As GlobalOptions

        Return New GlobalOptions With {
            .margintop = margintop,
            .marginleft = marginleft,
            .marginright = marginright,
            .marginbottom = marginbottom,
            .imagequality = imagequality,
            .title = title
        }
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="spacing"></param>
    ''' <param name="center"></param>
    ''' <param name="fontsize"></param>
    ''' <param name="fontname"></param>
    ''' <param name="html">Adds a html footer/header</param>
    ''' <returns></returns>
    <ExportAPI("pdfDecoration")>
    Public Function pdfDecoration(Optional spacing# = 8,
                                  Optional center$ = "-- " & Decoration.page & " --",
                                  Optional fontsize! = 14,
                                  Optional fontname$ = FontFace.MicrosoftYaHei,
                                  Optional html As String = Nothing) As Decoration

        Return New Decoration With {
            .center = center,
            .fontname = fontname,
            .fontsize = fontsize,
            .spacing = spacing,
            .html = html
        }
    End Function

    <ExportAPI("logo_html")>
    Public Function logoHtml(logo As String) As String
        If (Not logo.ExtensionSuffix("html", "htm")) Then
            Dim tmp As String = App.SysTemp & $"/{App.PID.ToHexString}_logo{App.GetNextUniqueName("image_")}.html"
            Dim html As String = sprintf(
                <div>
                    <img style="height 100%;" src=<%= New DataURI(logo).ToString %>/>
                </div>)

            html.SaveTo(tmp)
            logo = tmp.GetFullPath
        End If

        Return logo
    End Function

    ''' <summary>
    ''' convert the local html documents to pdf document.
    ''' </summary>
    ''' <param name="files">
    ''' markdown files or html files
    ''' </param>
    ''' <param name="pdfout"></param>
    ''' <remarks>
    ''' the executable file path of the wkhtmltopdf should be
    ''' configed via ``options(wkhtmltopdf = ...)``.
    ''' </remarks>
    <ExportAPI("makePDF")>
    <RApiReturn(GetType(String))>
    Public Function makePDF(<RRawVectorArgument> files As Object,
                            Optional pdfout As String = "out.pdf",
                            Optional wwwroot As String = "/",
                            Optional style As String = Nothing,
                            Optional resolvedAsDataUri As Boolean = False,
                            Optional footer As Decoration = Nothing,
                            Optional header As Decoration = Nothing,
                            Optional opts As GlobalOptions = Nothing,
                            Optional pageOpts As Page = Nothing,
                            Optional pdf_size As QPrinter = QPrinter.A4,
                            Optional env As Environment = Nothing) As Object

        Dim [strict] As Boolean = env.globalEnvironment.options.strict
        Dim filelist As Array = CLRVector.asObject(files)
        Dim contentUrls As String()

        If filelist.Length = 1 AndAlso TypeOf filelist(Scan0) Is HTMLReport Then
            contentUrls = DirectCast(filelist(Scan0), HTMLReport) _
                .Save() _
                .HtmlFiles
        Else
            contentUrls = CLRVector.asCharacter(filelist) _
                .GetContentHtml(
                    wwwroot:=wwwroot,
                    style:=style,
                    resolvedAsDataUri:=resolvedAsDataUri,
                    strict:=strict
                ) _
                .ToArray
        End If

        If contentUrls.IsNullOrEmpty Then
            Return Internal.debug.stop("no pdf content files was found!", env)
        End If

        Dim content As New WkHtmlToPdf.Arguments.PdfDocument With {
            .Url = contentUrls,
            .footer = If(footer, New Decoration With {.right = "[page] / [toPage]"}),
            .header = header,
            .globalOptions = If(opts, New GlobalOptions With {.imagequality = 100, .enablelocalfileaccess = True}),
            .page = If(pageOpts, New Page With {.javascriptdelay = 3000, .loaderrorhandling = handlers.ignore, .enableforms = True}),
            .pagesize = New PageSize With {.pagesize = pdf_size},
            .LocalConfigMode = False
        }
        Dim workdir As String = TempFileSystem.GetAppSysTempFile("__pdf", App.PID.ToHexString, "wkhtmltopdf")
        Dim output As New PdfOutput With {.OutputFilePath = pdfout}
        Dim wkhtmltopdf As New PdfConvertEnvironment With {
            .TempFolderPath = workdir,
            .Debug = env.globalEnvironment.Rscript.debug,
            .Timeout = 60000,
            .WkHtmlToPdfPath = env.globalEnvironment.options.getOption("wkhtmltopdf")
        }

        If wkhtmltopdf.WkHtmlToPdfPath.StringEmpty Then
            Return Internal.debug.stop("please config wkhtmltopdf program at first!", env)
        ElseIf Not wkhtmltopdf.WkHtmlToPdfPath.FileExists Then
            Return Internal.debug.stop($"wkhtmltopdf program Is Not exists at the given location '{wkhtmltopdf.WkHtmlToPdfPath}'...", env)
        End If

        If wkhtmltopdf.Debug Then
            Call Console.WriteLine("wkhtmltopdf config:")
            Call Console.WriteLine(wkhtmltopdf.GetJson)
        End If

        If Not content.CheckContentSource Then
            Return Internal.debug.stop("part of the content is missing... break pdf conversion progress...", env)
        End If

        Call pdfout.ParentPath.MakeDir
        Call PdfConvert.ConvertHtmlToPdf(content, output, environment:=wkhtmltopdf)

        Return pdfout.GetFullPath
    End Function
End Module
