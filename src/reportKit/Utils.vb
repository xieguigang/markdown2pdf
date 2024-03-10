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
