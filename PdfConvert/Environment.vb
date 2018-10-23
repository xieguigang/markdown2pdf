Imports System.Configuration
Imports System.IO
Imports Microsoft.VisualBasic.Language

Module InternalEnvironment

    Public ReadOnly Property Environment As PdfConvertEnvironment

    Public Const wkhtmltopdf$ = "wkhtmltopdf.exe"

    Sub New()
        Environment = New PdfConvertEnvironment With {
            .TempFolderPath = Path.GetTempPath(),
            .WkHtmlToPdfPath = GetWkhtmlToPdfExeLocation(),
            .Timeout = 60000
        }
    End Sub

    Private Function GetWkhtmlToPdfExeLocation() As String
        Dim customPath As String = ConfigurationManager.AppSettings("wkhtmltopdf:path")
        Dim filePath As New Value(Of String)

        If customPath Is Nothing Then
            customPath = App.HOME
        End If

        If (filePath = Path.Combine(customPath, "wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        If (filePath = Path.Combine(App.HOME, "wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Dim programFilesPath As String = System.Environment.GetEnvironmentVariable("ProgramFiles")

        If (filePath = Path.Combine(programFilesPath, "wkhtmltopdf\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Dim programFilesx86Path As String = System.Environment.GetEnvironmentVariable("ProgramFiles(x86)")

        If (filePath = Path.Combine(programFilesx86Path, "wkhtmltopdf\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        If (filePath = Path.Combine(programFilesPath, "wkhtmltopdf\bin\wkhtmltopdf.exe")).FileExists Then
            Return filePath
        End If

        Return Path.Combine(programFilesx86Path, "wkhtmltopdf\bin\wkhtmltopdf.exe")
    End Function
End Module
