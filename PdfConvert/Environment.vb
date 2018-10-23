Imports System.Configuration
Imports System.IO
Imports Microsoft.VisualBasic.Language
Imports ENV = System.Environment
Imports ProgramFiles = Microsoft.VisualBasic.FileIO.Path.ProgramPathSearchTool

Module InternalEnvironment

    Public ReadOnly Property Environment As PdfConvertEnvironment

    Public Const wkhtmltopdf$ = "wkhtmltopdf.exe"
    Public Const wkhtmltopdfInstall$ = "wkhtmltopdf\wkhtmltopdf.exe"

    Sub New()
        Environment = New PdfConvertEnvironment With {
            .TempFolderPath = Path.GetTempPath(),
            .WkHtmlToPdfPath = GetWkhtmlToPdfExeLocation(),
            .Timeout = 60000
        }
    End Sub

    Private Function GetWkhtmlToPdfExeLocation() As String
        Dim customPath As String = ConfigurationManager.AppSettings("wkhtmltopdf:path")
        Dim file As New Value(Of String)

        If customPath Is Nothing Then
            customPath = App.HOME
        End If

        If (file = Path.Combine(customPath, wkhtmltopdf)).FileExists Then
            Return file
        End If

        If (file = Path.Combine(App.HOME, wkhtmltopdf)).FileExists Then
            Return file
        End If

        For Each folder As String In {
            ENV.GetEnvironmentVariable("ProgramFiles"),
            ENV.GetEnvironmentVariable("ProgramFiles(x86)")
        }
            If (file = Path.Combine(folder, wkhtmltopdfInstall)).FileExists Then
                Return file
            End If
        Next

        For Each dir As String In ProgramFiles.SearchDirectory("wkhtmltopdf")
            For Each exeFile As String In ProgramFiles.SearchProgram(dir, "wkhtmltopdf", includeDll:=False)
                Return exeFile
            Next
        Next

        Throw New FileNotFoundException("Progream ""wkhtmltopdf"" is not installed!")
    End Function
End Module
