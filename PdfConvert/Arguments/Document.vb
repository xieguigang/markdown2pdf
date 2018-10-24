Imports System.Xml.Linq
Imports Microsoft.VisualBasic.CommandLine.Reflection
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

        Public Property state As Object

        <Prefix("--header")>
        Public Property header As Decoration
        <Prefix("--footer")>
        Public Property footer As Decoration
        Public Property globalOptions As GlobalOptions
        Public Property outline As Outline
        Public Property page As Page
        Public Property pagesize As PageSize
        Public Property TOC As TOC

#Region "Public methods"
        Public MustOverride Function GetDocument() As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
#End Region

    End Class

    Public Interface IPDFDocument(Of T)
        Property HTML As T
        Function GetDocument() As String
    End Interface
End Namespace