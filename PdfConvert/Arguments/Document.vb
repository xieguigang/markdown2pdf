Imports System.Xml.Linq
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace Arguments

    Public Class PdfDocument : Inherits PDFContent
        Implements IPDFDocument(Of String)

        Public Property Url As String
        Public Property Html As String Implements IPDFDocument(Of String).HTML

        Public Overrides Function GetDocument() As String Implements IPDFDocument(Of String).GetDocument
            Return Html
        End Function
    End Class

    Public Class HTMLDocument : Inherits PDFContent
        Implements IPDFDocument(Of XElement)

        Public Property HTML As XElement Implements IPDFDocument(Of XElement).HTML

        Public Overrides Function GetDocument() As String Implements IPDFDocument(Of XElement).GetDocument
            Return HTML.ToString
        End Function
    End Class

    Public MustInherit Class PDFContent

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

        Public MustOverride Function GetDocument() As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class

    Public Interface IPDFDocument(Of T)
        Property HTML As T
        Function GetDocument() As String
    End Interface
End Namespace