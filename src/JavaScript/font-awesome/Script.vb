#Region "Microsoft.VisualBasic::5ec7333b00b9ddb69ff13dbbff30f352, G:/GCModeller/src/runtime/markdown2pdf/src/JavaScript/font-awesome//Script.vb"

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

    '   Total Lines: 46
    '    Code Lines: 4
    ' Comment Lines: 32
    '   Blank Lines: 10
    '     File Size: 1.83 KB


    ' Module VBScript
    ' 
    '     Function: FromCSS
    ' 
    ' /********************************************************************************/

#End Region

'Imports System.ComponentModel
'Imports System.Text
'Imports Microsoft.VisualBasic.ApplicationServices.Emit.CodeDOM_VBC
'Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Module VBScript

    Public Function FromCSS(fontawesome As String) As String
        'Dim css As CSSFile = CssParser.GetTagWithCSS(fontawesome.SolveStream, selectorFilter:="\.fa[-]")
        'Dim icons = css.Selectors.Values.Where(Function(s) s.HasProperty("content")).ToArray
        'Dim members = icons _
        '    .Select(Function(icon As Selector)
        '                Dim member = icon.Selector _
        '                                 .Replace(".fa-", "") _
        '                                 .Replace(":before", "")
        '                member = CodeHelper.EnumMember(member, True, newLine:=True)
        '                member = $"<Content({icon!content})>" & vbCrLf & member

        '                ' 因为在CSS文件里面，content的值里面已经存在双引号了
        '                ' 所以在生成自定义属性的时候不需要再添加双引号了
        '                ' 否则会出错

        '                ' .fa-audible: before {
        '                '     content: "\f373"
        '                ' }

        '                Return member
        '            End Function) _
        '    .ToArray

        'With New StringBuilder

        '    Call .AppendLine($"Imports {GetType(DescriptionAttribute).Namespace}")
        '    Call .AppendLine()
        '    Call .AppendLine($"Public Enum {NameOf(icons)}")

        '    For Each member In members
        '        Call .AppendLine(member)
        '    Next

        '    Call .AppendLine("End Enum")

        '    Return .ToString
        'End With
    End Function
End Module
