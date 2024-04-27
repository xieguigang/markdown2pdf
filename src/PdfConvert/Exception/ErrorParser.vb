#Region "Microsoft.VisualBasic::56d9ae0fd43254f3a9b22fb225ce0fc8, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Exception/ErrorParser.vb"

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

    '   Total Lines: 26
    '    Code Lines: 20
    ' Comment Lines: 0
    '   Blank Lines: 6
    '     File Size: 856 B


    ' Module ErrorParser
    ' 
    '     Function: YieldErrors
    ' 
    ' Enum Errors
    ' 
    '     FailedLoadingPage, HostNotFound
    ' 
    '  
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.ComponentModel
Imports r = System.Text.RegularExpressions.Regex

Module ErrorParser

    ReadOnly errorTags As Dictionary(Of String, Errors) = Enums(Of Errors).ToDictionary(Function(e) e.Description)

    Public Iterator Function YieldErrors(output As String) As IEnumerable(Of (err As Errors, message As String))
        Dim errors = r.Matches(output, "((Error[:])|(Exit with)).+?$", RegexICMul).ToArray

        For Each err As String In errors
            Dim type As Errors = errorTags _
                .First(Function(t) InStr(err, t.Key) > 0) _
                .Value

            Yield (type, err)
        Next
    End Function
End Module

Public Enum Errors
    <Description("Error: Failed loading page")>
    FailedLoadingPage
    <Description("network error: HostNotFoundError")>
    HostNotFound
End Enum
