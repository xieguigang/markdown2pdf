#Region "Microsoft.VisualBasic::463d98ab7f2398a00ae44c54a3d25c78, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//HTML/TemplateHandler.vb"

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

    '   Total Lines: 76
    '    Code Lines: 41
    ' Comment Lines: 26
    '   Blank Lines: 9
    '     File Size: 2.72 KB


    '     Class TemplateHandler
    ' 
    '         Properties: builder, html, lines, path
    ' 
    '         Constructor: (+1 Overloads) Sub New
    ' 
    '         Function: ToString
    ' 
    '         Sub: Commit, Flush
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html.Document
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder
Imports Microsoft.VisualBasic.Text

Namespace HTML

    ''' <summary>
    ''' A html template file handler
    ''' </summary>
    Public Class TemplateHandler : Implements IVisualStudioPreviews

        ''' <summary>
        ''' 模板文件的文件全路径
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property path As String
        ''' <summary>
        ''' 模板文本字符串的缓存
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property builder As ScriptBuilder

        Public ReadOnly Property html As String Implements IVisualStudioPreviews.Previews
            Get
                Return builder.ToString
            End Get
        End Property

        Public ReadOnly Property lines As IEnumerable(Of String)
            Get
                Return builder.ToString.LineTokens
            End Get
        End Property

        ''' <summary>
        ''' create a report template handler for 
        ''' a single html template file.
        ''' </summary>
        ''' <param name="file"></param>
        Sub New(file As String)
            ' 可能在将报告写入硬盘文件之前，文件系统的上下文已经变了
            ' 所以需要在这里获取得到全路径
            path = file.GetFullPath
            builder = New ScriptBuilder(Interpolation.Interpolate(path))
        End Sub

        Public Sub Commit(newLines As IEnumerable(Of String))
            _builder = New ScriptBuilder(newLines)
        End Sub

        ''' <summary>
        ''' Interpolated html report file save
        ''' </summary>
        ''' <param name="path">
        ''' the file path of the target ``*.html`` text file, 
        ''' the default <see cref="path"/> will be used if 
        ''' the value of this parameter is nothing.
        ''' </param>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub Flush(Optional minify As Boolean = True, Optional path As String = Nothing)
            Call If(minify,
                builder.ToString.DoCall(AddressOf HtmlCompress.Minify),
                builder.ToString
            ) _
            .SaveTo(path Or Me.path.AsDefault, TextEncodings.UTF8WithoutBOM)
        End Sub

        Public Overrides Function ToString() As String
            Return path
        End Function
    End Class
End Namespace
