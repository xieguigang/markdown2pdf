Imports System.Text
Imports Microsoft.VisualBasic.MIME.Markup
Imports Microsoft.VisualBasic.CommandLine
Imports WkHtmlToPdf

Module Program

    ''' <summary>
    ''' HTML模板
    ''' </summary>
    ReadOnly HTML_template As XElement =
        <html>
            <head>
                <style>{0}</style>
            </head>
            <body class="markdown haroopad">
                {1}
            </body>
        </html>

    Public Function Main() As Integer
        Dim args As CommandLine = App.CommandLine
        Dim in$ = If(args Is Nothing OrElse args.Tokens.IsNullOrEmpty, "", args.Tokens(Scan0))
        Dim css$ = If(args Is Nothing OrElse args.Tokens.IsNullOrEmpty, "", args.Tokens.Get(1, ""))

        If Not [in].FileExists Then
            Call Console.WriteLine("markdown2PDF <input.md> [custom.css]")
            Call Console.WriteLine()

            Call Program.HelloWorld()
        Else
            Dim md As String = [in].ReadAllText
            Dim html As String = New MarkDown.MarkdownHTML().Transform(md)
            Dim url$ = [in].TrimSuffix & "-markdown2PDF.html"

            If css.FileExists Then
                css = css.ReadAllText
            Else
                css = My.Resources.haroopad
            End If

            html = String.Format(HTML_template.ToString, css, html)
            html.SaveTo(url, Encoding.UTF8)

            PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
               .Url = url
            }, New PdfOutput With {
                .OutputFilePath = url.TrimSuffix & ".pdf"
            })
        End If

        Return 0
    End Function

    Public Sub HelloWorld()
        Dim html As New HTMLDocument With {
            .HTML =
<html>
    <head>
        <title>Hello World!</title>
    </head>
    <body>
        <h1>Hello World!!!</h1>
        <hr/>
        <h2>Example code</h2>
        <code>
            <pre>
Public Function Main() As Integer
    Call println("Hello world!")
    Return 0
End Function
            </pre>
        </code>
        <h4>Another header</h4>
        <table>
            <thead>
                <tr>
                    <th>1</th>
                    <th>2</th>
                    <th>3</th>
                </tr>
            </thead>
            <tr>
                <td>a</td>
                <td>b</td>
                <td>c</td>
            </tr>
        </table>
        <footer style="position:fixed; font-size:.8em; text-align:right; bottom:0px; margin-left:-25px; height:20px; width:100%;">
            Here is the PDF document footer.
        </footer>
    </body>
</html>
        }
        Call println(html.GetDocument)
        Call PdfConvert.ConvertHtmlToPdf(
            html,
            App.HOME & "/hello-world.pdf")
    End Sub
End Module
