﻿#Region "Microsoft.VisualBasic::db6327f34226580300dc90f7fa093ac5, G:/GCModeller/src/runtime/markdown2pdf/src/LaTex/LaTex//TexRender.vb"

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

    '   Total Lines: 160
    '    Code Lines: 123
    ' Comment Lines: 1
    '   Blank Lines: 36
    '     File Size: 4.55 KB


    ' Class TexRender
    ' 
    '     Properties: BaseFontSizePt, Size
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: AnchorLink, BlockQuote, Bold, CodeBlock, CodeSpan
    '               Document, Header, HorizontalLine, Image, Italic
    '               List, NewLine, Paragraph, Table, Underline
    ' 
    ' /********************************************************************************/

#End Region

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
\usepackage{{float}}
\usepackage{{booktabs}}

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
        Return $"\textbf{{{text}}}"
    End Function

    Public Overrides Function Italic(text As String) As String
        Return $"\textit{{{text}}}"
    End Function

    Public Overrides Function Underline(text As String) As String
        Return $"\underline{{{text}}}"
    End Function

    Public Overrides Function BlockQuote(text As String) As String
        Return $"
\begin{{quotation}}
{text}
\end{{quatation}}
"
    End Function

    Public Overrides Function AnchorLink(url As String, text As String, title As String) As String
        Return $"\href{{{url}}}{{{text}}}"
    End Function

    Public Overrides Function List(items As IEnumerable(Of String), orderList As Boolean) As String
        items = items _
            .Select(Function(i) $"\item {i}") _
            .ToArray

        If orderList Then
            Return $"
\begin{{enumerate}}
{items.JoinBy(vbLf)}
\end{{enumerate}}
"
        Else
            Return $"
\begin{{itemize}}
{items.JoinBy(vbLf)}
\end{{itemize}}
"
        End If
    End Function

    Public Overrides Function Table(head() As String, rows As IEnumerable(Of String())) As String
        Dim row_text As String() = rows.Select(Function(r) r.JoinBy("&")).ToArray

        Return $"
\begin{{table}}[H]
\caption{{\textbf{{table caption title}}}}
\centering
\begin{{tabular}}{{{New String("c"c, head.Length)}}}
\toprule
{head.JoinBy("&")} \\
\midrule
{row_text.JoinBy(" \\" & vbLf) }
\bottomrule
\end{{tabular}}
\end{{table}}

"
    End Function

End Class

