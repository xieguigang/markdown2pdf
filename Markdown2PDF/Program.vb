Imports System.Text
Imports Microsoft.VisualBasic.MIME.Markup
Imports WkHtmlToPdf

Module Program

    Public Function Main() As Integer
        Dim [in] = App.Command

        If Not [in].FileExists Then
            Call Console.WriteLine("markdown2PDF <input.md>")
        Else
            Dim md As String = [in].ReadAllText
            Dim html As String = New MarkDown.MarkdownHTML().Transform(md)
            Dim url$ = [in].TrimSuffix & "-markdown2PDF.html"

            Call html.SaveTo(url, Encoding.UTF8)

            PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
               .Url = url
            }, New PdfOutput With {
                .OutputFilePath = url.TrimSuffix & ".pdf"
            })
        End If

        Return 0
    End Function
End Module
