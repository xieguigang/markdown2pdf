#Region "Microsoft.VisualBasic::010f16cbe3be0d5cda485287073bff1e, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Arguments/Options/Outline.vb"

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

    '   Total Lines: 35
    '    Code Lines: 13
    ' Comment Lines: 16
    '   Blank Lines: 6
    '     File Size: 1.07 KB


    '     Class Outline
    ' 
    '         Properties: dumpdefaulttocxsl, dumpoutline, nooutline, outlinedepth
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.CommandLine.Reflection

Namespace Arguments

    Public Class Outline

        ''' <summary>
        ''' Dump the default TOC xsl style sheet to stdout
        ''' </summary>
        ''' <returns></returns>
        <Argv("--dump-default-toc-xsl", CLITypes.Boolean)>
        Public Property dumpdefaulttocxsl As Boolean

        ''' <summary>
        ''' Dump the outline to a file
        ''' </summary>
        ''' <returns></returns>
        <Argv("--dump-outline", CLITypes.File)>
        Public Property dumpoutline As String

        ''' <summary>
        ''' Do not put an outline into the pdf
        ''' </summary>
        ''' <returns></returns>
        <Argv("--no-outline", CLITypes.Boolean)>
        Public Property nooutline As Boolean

        ''' <summary>
        ''' Set the depth of the outline (default 4)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--outline-depth", CLITypes.Integer)>
        Public Property outlinedepth As Integer?
    End Class
End Namespace
