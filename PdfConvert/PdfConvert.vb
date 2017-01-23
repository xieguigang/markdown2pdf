Imports System.Configuration
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.Language

Public Module PdfConvert

    Public ReadOnly Property Environment As PdfConvertEnvironment

    Sub New()
        PdfConvert.Environment = New PdfConvertEnvironment With {
            .TempFolderPath = Path.GetTempPath(),
            .WkHtmlToPdfPath = GetWkhtmlToPdfExeLocation(),
            .Timeout = 60000
        }
    End Sub

    Private Function GetWkhtmlToPdfExeLocation() As String
        Dim customPath As String = ConfigurationManager.AppSettings("wkhtmltopdf:path")
        Dim filePath As New Value(Of String)

        If customPath Is Nothing Then
            customPath = App.HOME
        End If

        If (filePath = Path.Combine(customPath, "wkhtmltopdf.exe")).FileExists  Then
            Return filePath
        End If

        If (filePath = Path.Combine(App.HOME, "wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Dim programFilesPath As String = System.Environment.GetEnvironmentVariable("ProgramFiles")

        If (filePath = Path.Combine(programFilesPath, "wkhtmltopdf\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Dim programFilesx86Path As String = System.Environment.GetEnvironmentVariable("ProgramFiles(x86)")

        If (filePath = Path.Combine(programFilesx86Path, "wkhtmltopdf\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        If (filePath = Path.Combine(programFilesPath, "wkhtmltopdf\bin\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Return Path.Combine(programFilesx86Path, "wkhtmltopdf\bin\wkhtmltopdf.exe")
    End Function

    Public Sub ConvertHtmlToPdf(document As PDFContent, output As PdfOutput)
        ConvertHtmlToPdf(document, Nothing, output)
    End Sub

    Const noHTML$ = "You must supply a HTML string, if you have enterd the url: '-'"

    Public Sub ConvertHtmlToPdf(document As PDFContent, environment As PdfConvertEnvironment, woutput As PdfOutput)
        Dim html$ = document.GetDocument
        Dim url$

        If TypeOf document Is PdfDocument Then
            url = DirectCast(document, PdfDocument).Url
        Else
            url = "-"
        End If

        If (url.IsNullOrEmpty OrElse url = "-") AndAlso html.IsNullOrEmpty Then
            Throw New PdfConvertException(noHTML)
        End If

        If environment Is Nothing Then
            environment = PdfConvert.Environment
        End If

        Dim outputPdfFilePath As String
        Dim delete As Boolean

        If woutput.OutputFilePath IsNot Nothing Then
            outputPdfFilePath = woutput.OutputFilePath
            delete = False
        Else
            outputPdfFilePath = Path.Combine(environment.TempFolderPath, String.Format("{0}.pdf", Guid.NewGuid()))
            delete = True
        End If

        If Not File.Exists(environment.WkHtmlToPdfPath) Then
            Throw New PdfConvertException($"File '{environment.WkHtmlToPdfPath}' not found. Check if wkhtmltopdf application is installed.")
        End If

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

        paramsBuilder.AppendFormat("""{0}"" ""{1}""", url, outputPdfFilePath)

        Try
            Dim output As New StringBuilder(), [error] As New StringBuilder()

            Using process As New Process()
                process.StartInfo.FileName = environment.WkHtmlToPdfPath
                process.StartInfo.Arguments = paramsBuilder.ToString()
                process.StartInfo.UseShellExecute = False
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.RedirectStandardError = True
                process.StartInfo.RedirectStandardInput = True

                Using outputWaitHandle As New AutoResetEvent(False), errorWaitHandle As New AutoResetEvent(False)

                    Dim outputHandler As DataReceivedEventHandler =
                        Sub(sender, e)
                            If e.Data Is Nothing Then
                                outputWaitHandle.[Set]()
                            Else
                                output.AppendLine(e.Data)
                            End If
                        End Sub
                    Dim errorHandler As DataReceivedEventHandler =
                        Sub(sender, e)
                            If e.Data Is Nothing Then
                                errorWaitHandle.[Set]()
                            Else
                                [error].AppendLine(e.Data)
                            End If
                        End Sub

                    AddHandler process.OutputDataReceived, outputHandler
                    AddHandler process.ErrorDataReceived, errorHandler

                    Try
                        process.Start()

                        process.BeginOutputReadLine()
                        process.BeginErrorReadLine()

                        If Not html.IsNullOrEmpty Then
                            Using stream = process.StandardInput
                                Dim buffer As Byte() = Encoding.UTF8.GetBytes(html)
                                stream.BaseStream.Write(buffer, 0, buffer.Length)
                                stream.WriteLine()
                            End Using
                        End If

                        If process.WaitForExit(environment.Timeout) AndAlso
                            outputWaitHandle.WaitOne(environment.Timeout) AndAlso
                            errorWaitHandle.WaitOne(environment.Timeout) Then

                            If process.ExitCode <> 0 AndAlso Not File.Exists(outputPdfFilePath) Then
                                Dim msg$ = $"Html to PDF conversion of '{url}' failed. Wkhtmltopdf output:
{[error]}"
                                Throw New PdfConvertException(msg)
                            End If
                        Else
                            If Not process.HasExited Then
                                process.Kill()
                            End If

                            Throw New PdfConvertTimeoutException()
                        End If
                    Finally
                        RemoveHandler process.OutputDataReceived, outputHandler
                        RemoveHandler process.ErrorDataReceived, errorHandler
                    End Try
                End Using
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
        Finally
            If delete AndAlso File.Exists(outputPdfFilePath) Then
                File.Delete(outputPdfFilePath)
            End If
        End Try
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
