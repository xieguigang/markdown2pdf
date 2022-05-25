Imports System.Runtime.CompilerServices
Imports SMRUCC.genomics.GCModeller.Workbench.ReportBuilder.HTML

Public Module PageElements

    ''' <summary>
    ''' assign the page numbers to the html templates
    ''' </summary>
    ''' <param name="report"></param>
    ''' <param name="orders">
    ''' the file basename of the html files
    ''' </param>
    ''' <returns></returns>
    <Extension>
    Public Function pageNumbers(report As HTMLReport, orders As String(),
                                Optional pageStart As Integer = 1,
                                Optional ByRef warnings As String() = Nothing) As HTMLReport

        Dim pageNumber As Integer = pageStart
        Dim page As TemplateHandler
        Dim msg As New List(Of String)

        For Each name As String In orders
            page = report.GetPageByName(name)

            If page Is Nothing Then
                Call msg.Add($"missing page '{name}' in the template!")
            End If

            If InStr(page.html, "[#page]") > 1 Then
                page.builder.Replace("[#page]", pageNumber)
                pageNumber += 1
            End If
        Next

        warnings = msg.ToArray

        Return report
    End Function
End Module
