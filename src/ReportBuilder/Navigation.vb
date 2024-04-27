#Region "Microsoft.VisualBasic::46acb5d4b6470e2f504811ddbf8a75db, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Navigation.vb"

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

    '   Total Lines: 37
    '    Code Lines: 24
    ' Comment Lines: 5
    '   Blank Lines: 8
    '     File Size: 1.45 KB


    ' Module Navigation
    ' 
    '     Properties: MEMESWTomQuery, MEMETomQuery
    ' 
    '     Function: BreadcrumbNavigation
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text

Public Module Navigation

    ''' <summary>
    ''' {display, url}
    ''' </summary>
    ''' <param name="nag">{display, url}</param>
    ''' <returns></returns>
    Public Function BreadcrumbNavigation(current As String, nag As Dictionary(Of String, String)) As String
        Dim nbr As StringBuilder = New StringBuilder(1024)

        Call nbr.AppendLine("<p>")
        Call nbr.AppendLine("<a href=""http://gcmodeller.org"">HOME</a> > ")

        For Each lv In nag
            Call nbr.AppendLine($"<a href=""{lv.Value}"">{lv.Key}</a> / ")
        Next

        Call nbr.AppendLine($"<strong>{current}</strong>")
        Call nbr.AppendLine("</p>")

        Return nbr.ToString
    End Function

    Public ReadOnly Property MEMESWTomQuery As String =
        BreadcrumbNavigation("Show Result", New Dictionary(Of String, String) From {
            {"Services", "http://services.gcmodeller.org"},
            {"MEME", "http://services.gcmodeller.org/meme/"},
            {"TomQuery", "http://services.gcmodeller.org/meme/tom-query.sw/"}})

    Public ReadOnly Property MEMETomQuery As String =
        BreadcrumbNavigation("Show Result", New Dictionary(Of String, String) From {
            {"Services", "http://services.gcmodeller.org"},
            {"MEME", "http://services.gcmodeller.org/meme/"},
            {"TomQuery", "http://services.gcmodeller.org/meme/tom-query/"}})
End Module
