﻿Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.Language
Imports WkHtmlToPdf.Arguments

''' <summary>
''' wkhtmltopdf is able to put several objects into the output file, an object is
''' either a single webpage, a cover webpage or a table of contents.  The objects
''' are put into the output document in the order they are specified on the
''' command line, options can be specified on a per object basis or in the global
''' options area. Options from the Global Options section can only be placed in
''' the global options area
''' </summary>
Public Module PdfConvert

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
    Public Sub ConvertHtmlToPdf(document As PDFContent, out$)
        Call ConvertHtmlToPdf(document, New PdfOutput With {
            .OutputFilePath = out
        })
    End Sub

    Const noHTML$ = "You must supply a HTML string, if you have enterd the url: '-'"

    Public Sub ConvertHtmlToPdf(document As PDFContent, woutput As PdfOutput, Optional environment As PdfConvertEnvironment = Nothing)
        Dim html$ = document.GetDocument
        Dim url$ = Nothing

        If TypeOf document Is PdfDocument Then
            url = DirectCast(document, PdfDocument).Url
        End If

        If url.StringEmpty Then
            If Not html.StringEmpty Then
                With App.GetAppSysTempFile(, App.PID)
                    Call html.SaveTo(.ByRef)
                    Call url.SetValue(.ByRef)
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
            outputPdfFilePath = App.GetAppSysTempFile(".pdf", App.PID)
            delete = True
        End If

        If Not File.Exists(environment.WkHtmlToPdfPath) Then
            Throw New PdfConvertException($"File '{environment.WkHtmlToPdfPath}' not found. Check if wkhtmltopdf application is installed.")
        Else
            argument = document.BuildArguments(url, outputPdfFilePath)
        End If

        Try
            Call environment.RunProcess(
                args:=argument,
                url:=url,
                document:=document,
                outputPdfFilePath:=outputPdfFilePath,
                woutput:=woutput
            )
        Finally
            If delete AndAlso File.Exists(outputPdfFilePath) Then
                File.Delete(outputPdfFilePath)
            End If
        End Try
    End Sub

    <Extension>
    Public Function BuildArguments(document As PDFContent, url$, pdfOut$) As String
        Dim paramsBuilder As New StringBuilder()
        paramsBuilder.Append("--page-size A4 ")

        If Not String.IsNullOrEmpty(document.HeaderUrl) Then
            paramsBuilder.AppendFormat("--header-html {0} ", document.HeaderUrl)
            paramsBuilder.Append("--margin-top 25 ")
            paramsBuilder.Append("--header-spacing 5 ")
        End If
        If Not String.IsNullOrEmpty(document.FooterUrl) Then
            paramsBuilder.AppendFormat("--footer-html {0} ", document.FooterUrl)
            paramsBuilder.Append("--margin-bottom 25 ")
            paramsBuilder.Append("--footer-spacing 5 ")
        End If

        If Not String.IsNullOrEmpty(document.HeaderLeft) Then
            paramsBuilder.AppendFormat("--header-left ""{0}"" ", document.HeaderLeft)
        End If

        If Not String.IsNullOrEmpty(document.HeaderCenter) Then
            paramsBuilder.AppendFormat("--header-center ""{0}"" ", document.HeaderCenter)
        End If

        If Not String.IsNullOrEmpty(document.HeaderRight) Then
            paramsBuilder.AppendFormat("--header-right ""{0}"" ", document.HeaderRight)
        End If

        If Not String.IsNullOrEmpty(document.FooterLeft) Then
            paramsBuilder.AppendFormat("--footer-left ""{0}"" ", document.FooterLeft)
        End If

        If Not String.IsNullOrEmpty(document.FooterCenter) Then
            paramsBuilder.AppendFormat("--footer-center ""{0}"" ", document.FooterCenter)
        End If

        If Not String.IsNullOrEmpty(document.FooterRight) Then
            paramsBuilder.AppendFormat("--footer-right ""{0}"" ", document.FooterRight)
        End If

        If document.ExtraParams IsNot Nothing Then
            For Each extraParam In document.ExtraParams
                paramsBuilder.AppendFormat("--{0} {1} ", extraParam.Key, extraParam.Value)
            Next
        End If

        If document.Cookies IsNot Nothing Then
            For Each cookie In document.Cookies
                paramsBuilder.AppendFormat("--cookie {0} {1} ", cookie.Key, cookie.Value)
            Next
        End If

        paramsBuilder.AppendFormat("""{0}"" ""{1}""", url, pdfOut)

        Return paramsBuilder.ToString
    End Function

    <Extension>
    Private Sub RunProcess(environment As PdfConvertEnvironment, args$, url$, outputPdfFilePath$, document As PDFContent, woutput As PdfOutput)
        Using process As New IORedirect(environment.WkHtmlToPdfPath, args)
            Call process.Start(False)

            If process.WaitForExit(environment.Timeout) AndAlso
               process.WaitOutput(environment.Timeout) AndAlso
               process.WaitError(environment.Timeout) Then

                If process.ExitCode <> 0 AndAlso Not File.Exists(outputPdfFilePath) Then
                    Call process.GetError.PdfConvertFailure(url)
                End If
            Else
                If Not process.HasExited Then
                    process.Kill()
                End If

                Throw New PdfConvertTimeoutException()
            End If
        End Using

        If woutput.OutputStream IsNot Nothing Then
            Using fs As Stream = New FileStream(outputPdfFilePath, FileMode.Open)
                Dim buffer As Byte() = New Byte(32 * 1024 - 1) {}
                Dim read As New Value(Of Integer)

                While (read = fs.Read(buffer, 0, buffer.Length)) > 0
                    woutput.OutputStream.Write(buffer, 0, read)
                End While
            End Using
        End If

        If woutput.OutputCallback IsNot Nothing Then
            Dim pdfFileBytes As Byte() = File.ReadAllBytes(outputPdfFilePath)
            woutput.OutputCallback()(document, pdfFileBytes)
        End If
    End Sub

    <Extension> Private Sub PdfConvertFailure(error$, url$)
        Dim msg$ = $"Html to PDF conversion of '{url}' failed. 
Wkhtmltopdf output:

{[error]}"
        Throw New PdfConvertException(msg)
    End Sub

    Public Sub ConvertHtmlToPdf(url As String, outputFilePath As String)
        Dim [in] As New PdfDocument With {
            .Url = url
        }
        Dim out As New PdfOutput With {
            .OutputFilePath = outputFilePath
        }

        Call ConvertHtmlToPdf([in], out)
    End Sub
End Module
