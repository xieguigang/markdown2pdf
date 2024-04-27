#Region "Microsoft.VisualBasic::ee92ff5188fefc1e6dc35526fd9aae95, G:/GCModeller/src/runtime/markdown2pdf/src/reportKit//Utils.vb"

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

    '   Total Lines: 75
    '    Code Lines: 59
    ' Comment Lines: 2
    '   Blank Lines: 14
    '     File Size: 2.84 KB


    ' Module Utils
    ' 
    '     Function: GetContentHtml
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Net.Http
Imports Microsoft.VisualBasic.Text.Xml
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML
Imports MarkdownHTML = Microsoft.VisualBasic.MIME.text.markdown.MarkdownRender

Module Utils

    <Extension>
    Friend Iterator Function GetContentHtml(files As IEnumerable(Of String),
                                            wwwroot$,
                                            style$,
                                            resolvedAsDataUri As Boolean,
                                            [strict] As Boolean) As IEnumerable(Of String)
        Dim render As New MarkdownHTML
        Dim dir As String = App.CurrentDirectory

        wwwroot = wwwroot.GetDirectoryFullPath

        If Not style.StringEmpty Then
            If style.FileExists Then
                style = style.GetFullPath
            ElseIf $"{wwwroot}/{style}".FileExists Then
                style = $"{wwwroot}/{style}".GetFullPath
            Else
                Dim tmp As String = App.SysTemp & $"/pdf_styles_{App.PID.ToHexString}-{Now.ToString.MD5.ToLower}.css"

                style.SaveTo(tmp)
                style = tmp
            End If

            If resolvedAsDataUri Then
                style = New DataURI(style).ToString
            End If
        End If

        For Each file As String In files.SafeQuery
            If Not file.FileExists Then

                If strict Then
                    Throw New EntryPointNotFoundException($"missing source file: {file}!")
                Else
                    Call $"missing source file: {file}!".Warning
                End If

            ElseIf file.ExtensionSuffix("html") Then
                ' Yield RelativePath(dir, file.GetFullPath)
                Yield file.GetFullPath
            Else
                Dim htmlfile As String = file.GetFullPath.ChangeSuffix("html")
                Dim html As String = file _
                    .ReadAllText _
                    .DoCall(AddressOf render.Transform) _
                    .ResolveLocalFileLinks(relativeTo:=wwwroot, asDataUri:=True)

                If Not style.StringEmpty Then
                    html = sprintf(<html>
                                       <head>

                                           <link rel="stylesheet" href=<%= style %>/>

                                       </head>
                                       <body>%s</body>
                                   </html>, html)
                End If

                Call html.SaveTo(htmlfile)

                ' Yield RelativePath(dir, htmlfile)
                Yield htmlfile
            End If
        Next
    End Function
End Module

