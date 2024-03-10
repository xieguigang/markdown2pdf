Imports Microsoft.VisualBasic.MIME.text.markdown
Imports WkHtmlToPdf.LaTex

Module Program
    Sub Main(args As String())
        convertTest()
        Console.WriteLine("Hello World!")
    End Sub

    Sub convertTest()
        Dim md As String = "E:\GCModeller\src\workbench\markdown2pdf\src\LaTex\test\test1.md".ReadAllText
        Dim render As New Markdownrender(New TexRender)
        Dim tex As String = render.Transform(md)

        Call Console.WriteLine(tex)

        Pause()
    End Sub
End Module
