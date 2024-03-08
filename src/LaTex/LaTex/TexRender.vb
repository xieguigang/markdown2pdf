Imports Microsoft.VisualBasic.MIME.text.markdown

Public Class TexRender : Inherits Render

    Public ReadOnly Property Size As String = "a4paper"
    Public ReadOnly Property BaseFontSizePt As Single = 12

    Sub New()
    End Sub

    Public Overrides Function Paragraph(text As String, CreateParagraphs As Boolean) As String
        ' replace the leading whitespace as the tex tag 
        Dim p As String = _leadingWhitespace.Replace(text, If(CreateParagraphs, "\paragraph{}" & vbCrLf & vbCrLf, ""))
        Dim html As String = p & vbCrLf & vbCrLf

        Return html
    End Function

    Public Overrides Function Document(text As String) As String
        Return $"\documentclass[{Size},{BaseFontSizePt}pt]{{article}}

\usepackage{{listings}}
\usepackage{{ctex}}
\usepackage{{xcolor}}
\usepackage{{graphicx}}
\usepackage{{epstopdf}} %%package to overcome problem with eps in pdf files

% language syntax highlight definition

\lstdefinestyle{{numbers}} {{numbers=left, stepnumber=1, numberstyle=\tiny, numbersep=10pt,frame=lines,backgroundcolor={{}}}}

\lstdefinestyle{{lang_r}} {{language=r,style=numbers}}
\lstdefinestyle{{lang_xml}} {{language=xml,style=numbers}}
\lstdefinestyle{{lang_vbnet}} {{language=vbnet,style=numbers}}

\lstset{{language=r,frame=lines}}
\lstset{{language=xml,frame=lines}}
\lstset{{language=vbnet,frame=lines}}

\begin{{document}}

{text}

\end{{document}}
"
    End Function

    Public Overrides Function CodeSpan(text As String) As String
        Return $"\code{{{text}}}"
    End Function

    Public Overrides Function Header(text As String, level As Integer) As String
        Select Case level
            Case 1 : Return $"\section{{{text}}}" & vbCrLf & vbCrLf
            Case 2 : Return $"\subsection{{{text}}}" & vbCrLf & vbCrLf
            Case 3 : Return $"\subsubsection{{{text}}}" & vbCrLf & vbCrLf
            Case 4 : Return $"\subsubsubsection{{{text}}}" & vbCrLf & vbCrLf

            Case Else
                Throw New NotImplementedException($"header title at level {level}: {text}")
        End Select
    End Function

    Public Overrides Function HorizontalLine() As String
        Return "\hrule height h depth d width w \relax"
    End Function

    Public Overrides Function NewLine() As String
        Return "\newline"
    End Function

    Public Overrides Function CodeBlock(code As String, lang As String) As String
        Return $"

\begin{{lstlisting}}[style=lang_{lang}]
{code}
\end{{lstlisting}}

"
    End Function

    Public Overrides Function Image(url As String, altText As String, title As String) As String
        Return $"

\begin{{figure}}
\centering
        \includegraphics[totalheight=8cm]{{{url}}}
    \caption{{{title}}}
    \label{{fig:verticalcell}}
\end{{figure}}

"
    End Function

    Public Overrides Function Bold(text As String) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function Italic(text As String) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function BlockQuote(text As String) As String
        Throw New NotImplementedException()
    End Function
End Class
