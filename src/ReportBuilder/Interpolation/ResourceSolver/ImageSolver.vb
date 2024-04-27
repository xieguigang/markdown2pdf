#Region "Microsoft.VisualBasic::07f3f8239c066bf21467363278bab0ed, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/ResourceSolver/ImageSolver.vb"

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

    '   Total Lines: 62
    '    Code Lines: 40
    ' Comment Lines: 14
    '   Blank Lines: 8
    '     File Size: 2.19 KB


    ' Class ImageSolver
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: encodeLocalURL, getfile, GetHtml
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Net.Http
Imports any = Microsoft.VisualBasic.Scripting

Public Class ImageSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim filepath As String = getfile(workdir)
        Dim html As Boolean = resource.options.TryGetValue("html", [default]:=False)
        Dim isDataUri As Boolean = False

        If filepath.FileExists Then
            ' 20220425
            ' try to convert the image data to base64 data URI
            ' to fix the file path error in wkhtmltopdf
            filepath = New DataURI(filepath).ToString
            isDataUri = True
        End If

        If html Then
            If isDataUri OrElse filepath.FileExists Then
                Return $"<img style='{resource.styles("*")?.CSSValue}' src='{filepath}' />"
            Else
                Return any.ToString(resource.options.TryGetValue("missing", [default]:=""))
            End If
        Else
            Return filepath
        End If
    End Function

    ''' <summary>
    ''' there is a bug in wkhtmltopdf application when generates 
    ''' html page contains image file with non-ascii chars, the 
    ''' file path of the image will not be recognized in wkhtmltopdf 
    ''' application.
    ''' 
    ''' try to fix this error with the url encode of the non-ascii
    ''' chars.
    ''' </summary>
    ''' <param name="filepath"></param>
    ''' <returns></returns>
    Public Shared Function encodeLocalURL(filepath As String) As String
        Dim tokens As String() = Strings.Trim(filepath) _
            .StringSplit("[/\\]") _
            .Select(Function(str) str.UrlEncode) _
            .ToArray
        Dim newPath As String = tokens.JoinBy("/").StringReplace("[/]{2,}", "/")

        Return newPath
    End Function

    Private Function getfile(workdir As String) As String
        If resource.image.FileExists Then
            Return resource.image
        Else
            Return $"{workdir}/{resource.image}"
        End If
    End Function
End Class
