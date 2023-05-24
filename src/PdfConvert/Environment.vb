#Region "Microsoft.VisualBasic::1405d12ad29a17f0a11ce67f71420f07, markdown2pdf\PdfConvert\Environment.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xie (genetics@smrucc.org)
'       xieguigang (xie.guigang@live.com)
' 
' Copyright (c) 2018 GPL3 Licensed
' 
' 
' GNU GENERAL PUBLIC LICENSE (GPL3)
' 
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.



' /********************************************************************************/

' Summaries:

' Module InternalEnvironment
' 
'     Properties: Environment
' 
'     Constructor: (+1 Overloads) Sub New
'     Function: GetWkhtmlToPdfExeLocation, lazyGetEnvironment
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
        Dim search As New ProgramFiles() With {
            .CustomDirectories = {
                customPath, App.HOME
            }
        }
        Dim file As String = search _
            .FindProgram("wkhtmltopdf", includeDll:=False) _
            .FirstOrDefault

        If Not file.StringEmpty Then
            Return file
        End If

        ' search inside the C:\Program Files at first
        For Each dir As String In ProgramFiles.SearchDirectory("wkhtmltopdf")
            For Each exeFile As String In ProgramFiles.SearchProgram(dir, "wkhtmltopdf", includeDll:=False)
                Return exeFile
            Next
        Next

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
