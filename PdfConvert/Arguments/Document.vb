Imports System.Runtime.CompilerServices
Imports System.Xml.Linq
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Language
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

        Public Shared Function DefaultPDFStyle(data$, Optional isUrl As Boolean = True) As PdfDocument
            Return DefaultStyleDocument(Of PdfDocument)(
                Sub(doc)
                    If isUrl Then
                        doc.Url = data
                    Else
                        doc.Html = data
                    End If
                End Sub)
        End Function

        Public Shared Function DefaultPDFStyle(html As XElement) As HTMLDocument
            Return DefaultStyleDocument(Of HTMLDocument)(Sub(pdf) pdf.HTML = html)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Shared Function DefaultStyleDocument(Of T As {New, PDFContent})(write As Action(Of T)) As T
            With Activator.CreateInstance(Of T)
                Call write(.ByRef)

                ' set default style
                .footer = New Decoration With {
                    .footerspacing = "10",
                    .footerfontname = FontFace.MicrosoftYaHei,
                    .footerfontsize = 11,
                    .footerline = True,
                    .footerright = $"{Decoration.page}/{Decoration.topage}"
                }
                .pagesize = New PageSize With {
                    .pagesize = QPrinter.A4
                }
                .TOC = New TOC With {
                    .disabledottedlines = True,
                    .tocheadertext = "[TOC]",
                    .toctextsizeshrink = 0.9
                }
                .header = New Decoration With {
                    .footercenter = Decoration.title,
                    .footerspacing = 10,
                    .footerline = True
                }
                .globalOptions = New GlobalOptions With {
                    .marginbottom = 20,
                    .orientation = Orientations.Portrait,
                    .imagequality = 100,
                    .marginleft = 10,
                    .marginright = 30,
                    .nopdfcompression = True,
                    .grayscale = True
                }

                Return .ByRef
            End With
        End Function
    End Class

    Public Interface IPDFDocument(Of T)
        Property HTML As T
        Function GetDocument() As String
    End Interface
End Namespace