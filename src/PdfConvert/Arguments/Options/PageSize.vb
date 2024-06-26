﻿#Region "Microsoft.VisualBasic::702098be2346ee5856c8ae9df55400df, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Arguments/Options/PageSize.vb"

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

    '   Total Lines: 105
    '    Code Lines: 62
    ' Comment Lines: 34
    '   Blank Lines: 9
    '     File Size: 4.47 KB


    '     Enum QPrinter
    ' 
    ' 
    '  
    ' 
    ' 
    ' 
    '     Class PageSize
    ' 
    '         Properties: pageheight, pagesize, pagewidth
    ' 
    '         Function: ParsePageSize, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.Default
Imports Microsoft.VisualBasic.Language.Values

Namespace Arguments

    ''' <summary>
    ''' The default page size of the rendered document is A4, but using this
    ''' --page-size optionthis can be changed to almost anything else, such as A3,
    ''' Letter And Legal.  For a full list of supported pages sizes please see
    ''' &lt;http: //qt-project.org/doc/qt-4.8/qprinter.html#PaperSize-enum>.
    '''
    ''' For a more fine grained control over the page size the --page-height And
    ''' --page-width options may be used
    ''' 
    ''' This enum type specifies what paper size QPrinter should use. QPrinter does 
    ''' not check that the paper size is available; it just uses this information, 
    ''' together with QPrinter::Orientation and QPrinter::setFullPage(), to determine 
    ''' the printable area.
    '''
    ''' The defined sizes (With setFullPage(True)) are
    ''' </summary>
    Public Enum QPrinter As Byte
        A0 = 5        ' 841 x 1189 mm
        A1 = 6        ' 594 x 841 mm
        A2 = 7        ' 420 x 594 mm
        A3 = 8        ' 297 x 420 mm
        A4 = 0        ' 210 x 297 mm, 8.26 x 11.69 inches
        A5 = 9        ' 148 x 210 mm
        A6 = 10       ' 105 x 148 mm
        A7 = 11       ' 74 x 105 mm
        A8 = 12       ' 52 x 74 mm
        A9 = 13       ' 37 x 52 mm
        B0 = 14       ' 1000 x 1414 mm
        B1 = 15       ' 707 x 1000 mm
        B2 = 17       ' 500 x 707 mm
        B3 = 18       ' 353 x 500 mm
        B4 = 19       ' 250 x 353 mm
        B5 = 1        ' 176 x 250 mm, 6.93 x 9.84 inches
        B6 = 20       ' 125 x 176 mm
        B7 = 21       ' 88 x 125 mm
        B8 = 22       ' 62 x 88 mm
        B9 = 23       ' 33 x 62 mm
        B10 = 16      ' 31 x 44 mm
        C5E = 24      ' 163 x 229 mm
        Comm10E = 25  ' 105 x 241 mm, U.S. Common 10 Envelope
        DLE = 26      ' 110 x 220 mm
        Executive = 4 ' 7.5 x 10 inches, 190.5 x 254 mm
        Folio = 27    ' 210 x 330 mm
        Ledger = 28   ' 431.8 x 279.4 mm
        Legal = 3     ' 8.5 x 14 inches, 215.9 x 355.6 mm
        Letter = 2    ' 8.5 x 11 inches, 215.9 x 279.4 mm
        Tabloid = 29  ' 279.4 x 431.8 mm
        Custom = 30   ' Unknown, Or a user defined size.
    End Enum

    ''' <summary>
    ''' The default page size of the rendered document is A4, but using this
    ''' --page-size optionthis can be changed to almost anything else, such as A3,
    ''' Letter And Legal.  For a full list of supported pages sizes please see
    ''' &lt;http: //qt-project.org/doc/qt-4.8/qprinter.html#PaperSize-enum>.
    '''
    ''' For a more fine grained control over the page size the --page-height And
    ''' --page-width options may be used
    ''' </summary>
    Public Class PageSize

        ''' <summary>
        ''' 如果这个参数为<see cref="QPrinter.Custom"/>，则还需要指定width和height
        ''' </summary>
        ''' <returns></returns>
        <Argv("--page-size", CLITypes.String)>
        Public Property pagesize As QPrinter = QPrinter.A4

        <Argv("--page-width", CLITypes.Double)>
        Public Property pagewidth As Double

        <Argv("--page-height", CLITypes.Double)>
        Public Property pageheight As Double

        ''' <summary>
        ''' Config page size from commandline
        ''' </summary>
        ''' <param name="pdf_size"></param>
        ''' <returns></returns>
        Public Shared Function ParsePageSize(pdf_size As Value(Of String), Optional defaultSize As QPrinter = QPrinter.A4) As QPrinter
            Static toEnums As Dictionary(Of String, QPrinter) = Enums(Of QPrinter).ToDictionary(Function(size) size.ToString.ToLower)

            If toEnums.ContainsKey(pdf_size = pdf_size.ToLower) Then
                Return toEnums(pdf_size)
            Else
                Return defaultSize
            End If
        End Function

        Public Overrides Function ToString() As String
            If pagesize = QPrinter.Custom Then
                Return $"--page-size ""{pagesize.ToString}"" --page-width {pagewidth} --page-height {pageheight}"
            Else
                Return $"--page-size ""{pagesize.ToString}"""
            End If
        End Function
    End Class
End Namespace
