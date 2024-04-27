#Region "Microsoft.VisualBasic::afb499698576063d84fd1cb87b878bc3, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//HTML/BuilderHelpers.vb"

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

    '   Total Lines: 66
    '    Code Lines: 41
    ' Comment Lines: 13
    '   Blank Lines: 12
    '     File Size: 2.36 KB


    '     Module BuilderHelpers
    ' 
    '         Function: Hide, ResolveLocalFileLinks, Show
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Net.Http

Namespace HTML

    Public Module BuilderHelpers

        <Extension>
        Public Function Show(report As HTMLReport, sectionBegin$, sectionEnd$) As HTMLReport
            Call report.Replace(sectionBegin, "")
            Call report.Replace(sectionEnd, "")

            Return report
        End Function

        ''' <summary>
        ''' 将目标区域注释掉，注意如果这个区域内还存在其他的html注释标签，则当前的html注释操作将会失败
        ''' </summary>
        ''' <param name="report"></param>
        ''' <param name="sectionBegin$"></param>
        ''' <param name="sectionEnd$"></param>
        ''' <returns></returns>
        <Extension>
        Public Function Hide(report As HTMLReport, sectionBegin$, sectionEnd$) As HTMLReport
            Call report.Replace(sectionBegin, "<!--")
            Call report.Replace(sectionEnd, "-->")

            Return report
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="html"></param>
        ''' <param name="relativeTo">working as wwwroot</param>
        ''' <returns></returns>
        <Extension>
        Public Function ResolveLocalFileLinks(html As String, relativeTo As String, Optional asDataUri As Boolean = False) As String
            Dim links As String() = html _
                .Matches("\s((src)|(href))\s*[=]\s*[""'].+?[""']") _
                .Distinct _
                .ToArray
            Dim sb As New StringBuilder(html)
            Dim target As NamedValue(Of String)
            Dim resolved As String

            For Each link As String In links
                target = link.GetTagValue("=", trim:=" ""'" & vbTab)

                If target.Value.FirstOrDefault = "/"c Then
                    resolved = relativeTo & target.Value

                    If asDataUri Then
                        resolved = New DataURI(resolved).ToString
                    End If

                    sb.Replace(link, $" {target.Name}=""{resolved}""")
                End If
            Next

            Return sb.ToString
        End Function
    End Module
End Namespace
