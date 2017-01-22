Imports System.IO
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class PdfOutput

    Public Property OutputFilePath As String
    Public Property OutputStream As Stream
    Public Property OutputCallback As Action(Of PdfDocument, Byte())

End Class

Public Class PdfDocument

    Public Property Url As String
    Public Property Html As String
    Public Property HeaderUrl As String
    Public Property FooterUrl As String
    Public Property HeaderLeft As String
    Public Property HeaderCenter As String
    Public Property HeaderRight As String
    Public Property FooterLeft As String
    Public Property FooterCenter As String
    Public Property FooterRight As String
    Public Property State As Object
    Public Property Cookies As Dictionary(Of String, String)
    Public Property ExtraParams As Dictionary(Of String, String)

    Public Overrides Function ToString() As String
        Return Me.GetJson
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