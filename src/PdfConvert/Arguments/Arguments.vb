#Region "Microsoft.VisualBasic::7f9ca24700480170c079656187bb385a, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Arguments/Arguments.vb"

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

    '   Total Lines: 30
    '    Code Lines: 22
    ' Comment Lines: 0
    '   Blank Lines: 8
    '     File Size: 862 B


    '     Class PdfOutput
    ' 
    '         Properties: OutputCallback, OutputFilePath, OutputStream
    ' 
    '         Function: ToString
    ' 
    '     Class PdfConvertEnvironment
    ' 
    '         Properties: Debug, PopulateSlaveProgressMessage, TempFolderPath, Timeout, WkHtmlToPdfPath
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Arguments

    Public Class PdfOutput

        Public Property OutputFilePath As String
        Public Property OutputStream As Stream
        Public Property OutputCallback As Action(Of PDFContent, Byte())

        Public Overrides Function ToString() As String
            Return OutputFilePath
        End Function

    End Class

    Public Class PdfConvertEnvironment

        Public Property TempFolderPath As String
        Public Property WkHtmlToPdfPath As String
        Public Property Timeout As Integer
        Public Property Debug As Boolean
        Public Property PopulateSlaveProgressMessage As Boolean

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace
