MarkdownToPdf
------------------

![](./dist/Adobe_Acrobat_Pro_PDF.png)

Convert markdown document to html/pdf using VisualBasic in a super easy way!

Translate from https://github.com/codaxy/wkhtmltopdf.

This is a small VB.NET wrapper utility around ``wkhtmltopdf`` console tool. You can use it to easily convert Markdown/HTML reports to PDF.

## Usage

```vbnet
Console.InputEncoding = Encoding.UTF8

PdfConvert.Environment.Debug = False
PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
    .Url = "input.html"
}, New PdfOutput With {
    .OutputFilePath = "output.pdf"
})

PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
    .Url = "input.html",
    .HeaderLeft = "[title]",
    .HeaderRight = "[date] [time]",
    .FooterCenter = "Page [page] of [topage]"
}, New PdfOutput With {
    .OutputFilePath = "output.pdf"
})
PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
    .Url = "-",
    .Html = "<html><h1>test</h1></html>"
}, New PdfOutput With {
    .OutputFilePath = "inline.pdf"
})
PdfConvert.ConvertHtmlToPdf(New PdfDocument With {
    .Url = "-",
    .Html = "<html><h1>測試</h1></html>"
}, New PdfOutput With {
    .OutputFilePath = "inline_cht.pdf"
})
```

## CLI Usage

```bash
# markdown2pdf
markdown2pdf ./input.html
```

## Licence

This project is available under MIT Licence.
