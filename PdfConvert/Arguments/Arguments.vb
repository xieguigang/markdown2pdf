Imports System.IO
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel
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

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    Public Class WkHtmlToPdfArguments : Inherits XmlDataModel

        <Prefix("--header")>
        Public Property header As Decoration
        <Prefix("--footer")>
        Public Property footer As Decoration
        Public Property globalOptions As GlobalOptions
        Public Property outline As Outline
        Public Property page As Page
        Public Property pagesize As PageSize
        Public Property TOC As TOC

    End Class
End Namespace