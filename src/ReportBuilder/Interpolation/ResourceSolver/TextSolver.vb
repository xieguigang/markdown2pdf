#Region "Microsoft.VisualBasic::d01687c7e8be19c07c7e3799fbce3ab1, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/ResourceSolver/TextSolver.vb"

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

    '   Total Lines: 29
    '    Code Lines: 22
    ' Comment Lines: 0
    '   Blank Lines: 7
    '     File Size: 838 B


    ' Class TextSolver
    ' 
    '     Properties: isHtml
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: GetHtml
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Net
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Class TextSolver : Inherits ResourceSolver

    Public ReadOnly Property isHtml As Boolean = False

    Public Sub New(res As ResourceDescription, Optional isHtml As Boolean = False)
        MyBase.New(res)

        Me.isHtml = isHtml
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim text As String = If(isHtml, resource.html, resource.text)

        If Not isHtml Then
            text = WebUtility.HtmlEncode(text)
        End If

        If resource.styles.IsNullOrEmpty Then
            Return text
        Else
            Return $"<span style='{resource.styles("*")?.CSSValue}'>
                        {text}
                     </span>"
        End If
    End Function

    Public Overrides Function GetResourceFile(workdir As String) As String
        Return Nothing
    End Function
End Class
