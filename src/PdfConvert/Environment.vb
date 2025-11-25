#Region "Microsoft.VisualBasic::29c4d402407d9ac7a425c74cc0c65881, G:/GCModeller/src/runtime/markdown2pdf/src/PdfConvert//Environment.vb"

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

    '   Total Lines: 95
    '    Code Lines: 77
    ' Comment Lines: 3
    '   Blank Lines: 15
    '     File Size: 3.55 KB


    ' Module InternalEnvironment
    ' 
    '     Properties: Environment
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: FromDefault, GetWkhtmlToPdfExeLocation, lazyGetEnvironment
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Configuration
Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Language.Default
Imports WkHtmlToPdf.Arguments
Imports ProgramFiles = Microsoft.VisualBasic.ApplicationServices.ProgramPathSearchTool

Module InternalEnvironment

    Public ReadOnly Property Environment As [Default](Of PdfConvertEnvironment)

    Public Const wkhtmltopdf$ = "wkhtmltopdf.exe"
    Public Const wkhtmltopdfInstall$ = "wkhtmltopdf\wkhtmltopdf.exe"

#If DEBUG Then
    Const isDebugMode As Boolean = True
#Else
    Const isDebugMode As Boolean = False
#End If

    Sub New()
        Environment = New [Default](Of PdfConvertEnvironment) With {
            .lazy = New Lazy(Of PdfConvertEnvironment)(AddressOf lazyGetEnvironment)
        }
    End Sub

    Public Function FromDefault(Optional debug As Boolean = False,
                                Optional sendMessage As Boolean = False,
                                Optional timeout As Integer = 60000,
                                Optional tempdir As String = Nothing,
                                Optional bin As String = Nothing) As PdfConvertEnvironment

        If tempdir.StringEmpty Then
            tempdir = TempFileSystem.GetAppSysTempFile(".txt", sessionID:="wkhtmltopdf___" & App.PID.ToHexString).ParentPath
        End If
        If bin.StringEmpty Then
            bin = GetWkhtmlToPdfExeLocation()
        End If

        Return New PdfConvertEnvironment With {
            .Debug = debug,
            .PopulateSlaveProgressMessage = sendMessage,
            .TempFolderPath = tempdir,
            .Timeout = timeout,
            .WkHtmlToPdfPath = bin
        }
    End Function

    Private Function lazyGetEnvironment() As PdfConvertEnvironment
        Return New PdfConvertEnvironment With {
            .TempFolderPath = Path.GetTempPath(),
            .WkHtmlToPdfPath = GetWkhtmlToPdfExeLocation(),
            .Timeout = 60000,
            .Debug = isDebugMode
        }
    End Function

    Private Function GetWkhtmlToPdfExeLocation() As String
        Dim customPath As String = ConfigurationManager.AppSettings("wkhtmltopdf:path")
        Dim file As String = ProgramFiles.Which("wkhtmltopdf", {customPath, App.HOME})

        If Not file.StringEmpty Then
            Return file
        End If

        ' and then try to get executable path from environment variables
        If App.GetVariable("wkhtmltopdf").FileExists Then
            Return App.GetVariable("wkhtmltopdf")
        End If
        If System.Environment.GetEnvironmentVariable("wkhtmltopdf").FileExists Then
            Return System.Environment.GetEnvironmentVariable("wkhtmltopdf")
        End If

        ' deal with the linux env
        If "/usr/local/bin/wkhtmltopdf".FileExists Then
            Return "/usr/local/bin/wkhtmltopdf"
        End If

        Throw New FileNotFoundException("Progream ""wkhtmltopdf"" is not installed!")
    End Function
End Module
