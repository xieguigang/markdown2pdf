Imports Microsoft.VisualBasic.MIME.text.markdown

Public Class TexRender : Inherits Render

    Public ReadOnly Property Size As String = "a4paper"
    Public ReadOnly Property BaseFontSizePt As Single = 12

    Sub New()
    End Sub

    Public Overrides Function Paragraph(text As String, CreateParagraphs As Boolean) As String
        ' replace the leading whitespace as the tex tag 
        Dim p As String = _leadingWhitespace.Replace(text, If(CreateParagraphs, "\begin{document}" & vbCrLf & vbCrLf, ""))
        Dim html As String = p & (If(CreateParagraphs, vbCrLf & vbCrLf & "\end{document}", ""))

        Return html
    End Function

    Public Overrides Function Document(text As String) As String
        Return $"\documentclass[{Size},{BaseFontSizePt}pt]{{article}}" & vbCrLf & vbCrLf & text
    End Function
End Class
