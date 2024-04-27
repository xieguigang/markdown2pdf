#Region "Microsoft.VisualBasic::a83042c91f66cfd4aec36c518fd5dc7a, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Arguments/Options/TOC.vb"

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

    '   Total Lines: 53
    '    Code Lines: 17
    ' Comment Lines: 27
    '   Blank Lines: 9
    '     File Size: 1.77 KB


    '     Class TOC
    ' 
    '         Properties: disabledottedlines, disabletoclinks, tocheadertext, toclevelindentation, toctextsizeshrink
    '                     xslstylesheet
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.CommandLine.Reflection

Namespace Arguments

    ''' <summary>
    ''' 目录设置参数
    ''' </summary>
    Public Class TOC

        ''' <summary>
        ''' The header text of the toc (default Table of Contents)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--toc-header-text", CLITypes.String)>
        Public Property tocheadertext As String

        ''' <summary>
        ''' For each level of headings in the toc indent by this length (Default 1em)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--toc-level-indentation", CLITypes.String)>
        Public Property toclevelindentation As String

        ''' <summary>
        ''' Do not use dotted lines in the toc
        ''' </summary>
        ''' <returns></returns>
        <Argv("--disable-dotted-lines", CLITypes.Boolean)>
        Public Property disabledottedlines As Boolean

        ''' <summary>
        ''' Do not link from toc to sections
        ''' </summary>
        ''' <returns></returns>
        <Argv("--disable-toc-links", CLITypes.Boolean)>
        Public Property disabletoclinks As Boolean

        ''' <summary>
        ''' For each level of headings in the toc the font Is scaled by this factor (default 0.8)
        ''' </summary>
        ''' <returns></returns>
        <Argv("--toc-text-size-shrink", CLITypes.Double)>
        Public Property toctextsizeshrink As Double?

        ''' <summary>
        ''' Use the supplied xsl style sheet for printing the table Of content
        ''' </summary>
        ''' <returns></returns>
        <Argv("--xsl-style-sheet", CLITypes.File)>
        Public Property xslstylesheet As String

    End Class
End Namespace
