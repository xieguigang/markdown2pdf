#Region "Microsoft.VisualBasic::eb6c657528a49aa47818dec08bbd5d2b, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/ResourceSolver/TableSolver.vb"

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

'   Total Lines: 178
'    Code Lines: 144
' Comment Lines: 9
'   Blank Lines: 25
'     File Size: 6.78 KB


' Class TableSolver
' 
'     Constructor: (+1 Overloads) Sub New
'     Function: BuildRowHtml, GetHtml, RowSelector
' 
' /********************************************************************************/

#End Region

Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.Framework.IO
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports Microsoft.VisualBasic.My.JavaScript
Imports any = Microsoft.VisualBasic.Scripting
Imports std = System.Math

Public Class TableSolver : Inherits ResourceSolver

    Public Sub New(res As ResourceDescription)
        MyBase.New(res)
    End Sub

    Public Overrides Function GetHtml(workdir As String) As String
        Dim tablefile As String = If(resource.table.FileExists, resource.table, $"{workdir}/{resource.table}")

        If Not tablefile.FileExists Then
            Return Nothing
        End If

        Dim table As DataFrameResolver = DataFrameResolver.Load(tablefile)
        Dim tbody As New StringBuilder
        Dim css As CSSFile = resource.styles
        Dim names As String() = table.Headers.Select(Function(str) str.Trim(""""c)).ToArray
        Dim maxRows As Integer = resource.options.TryGetValue("nrows", [default]:=-1)
        Dim maxWidth As Integer = resource.options.TryGetValue("max_width", [default]:=-1)
        Dim orderBy As Object = resource.options.TryGetValue("order_by", [default]:=Nothing)
        Dim fieldNames As Object() = resource.options.TryGetValue("fields", [default]:=Nothing)
        Dim ordinals As FieldDescription() = If(fieldNames Is Nothing, Nothing, FieldDescription.parseFieldOrdinals(fieldNames, names).ToArray)
        Dim thead As String
        Dim rowCells As String

        If Not ordinals Is Nothing AndAlso ordinals.Any(Function(i) i.ordinal = -1) Then
            If fieldNames Is Nothing Then
                Return resource.options.TryGetValue("no_content", [default]:="<span style='color: red;'>No table content data.</span>")
            Else
                thead = fieldNames _
                    .Select(Function(s)
                                Return $"<th style='{any.ToString(css("th")?.CSSValue)}'>{s.Trim(""""c)}</th>"
                            End Function) _
                    .JoinBy(vbCrLf)
            End If
        Else
            thead = BuildRowHtml(names, ordinals, css, isHeader:=True, -1)
        End If

        For Each row As RowObject In RowSelector(table, maxRows, orderBy)
            rowCells = BuildRowHtml(
                cells:=row.AsEnumerable,
                ordinals:=ordinals,
                css:=css,
                isHeader:=False,
                maxWidth:=maxWidth
            )
            tbody.AppendLine($"<tr style='{any.ToString(css("tr")?.CSSValue)}'>{rowCells}</tr>")
        Next

        Return $"<table style='{any.ToString(css("table")?.CSSValue)}'>

<thead style='{any.ToString(css("thead")?.CSSValue)}'>
<tr>
{thead}
</tr>
</thead>
<tbody>
{tbody.ToString}
</tbody>

</table>"
    End Function

    ''' <summary>
    ''' orderBy -> take
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="maxRows"></param>
    ''' <param name="orderBy"></param>
    ''' <returns></returns>
    Private Iterator Function RowSelector(table As DataFrameResolver, maxRows As Integer, orderBy As Object) As IEnumerable(Of RowObject)
        Dim orders As Integer()

        If Not orderBy Is Nothing Then
            Dim opts As JavaScriptObject

            If TypeOf orderBy Is String Then
                ' order by field by default .NET comparer function
                opts = New JavaScriptObject
                opts("field") = orderBy
                opts("desc") = False
            ElseIf TypeOf orderBy Is JavaScriptObject Then
                opts = orderBy
            Else
                Throw New NotImplementedException
            End If

            Dim eval As String = opts("eval")
            Dim evalFunc As Func(Of String, Object)

            If eval Is Nothing Then
                evalFunc = Function(str) str
            Else
                Select Case eval
                    Case "nchars" : evalFunc = Function(str) Strings.Len(str)
                    Case "as.numeric" : evalFunc = Function(str) str.ParseDouble
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If

            Dim desc As Boolean = opts("desc")
            Dim field As String = opts("field")
            Dim fieldOrdinal As Integer = table.GetOrdinal(field)
            Dim vec As String() = table.Column(fieldOrdinal).ToArray
            Dim vecVal = vec _
                .Select(evalFunc) _
                .Select(Function(any, i) (any, i)) _
                .ToArray

            If desc Then
                orders = vecVal _
                    .OrderByDescending(Function(x) x.any) _
                    .Select(Function(x) x.i) _
                    .ToArray
            Else
                orders = vecVal _
                    .OrderBy(Function(x) x.any) _
                    .Select(Function(x) x.i) _
                    .ToArray
            End If
        Else
            ' no orders
            orders = table.Nrows.Sequence.ToArray
        End If

        maxRows = If(maxRows > 0, std.Min(maxRows, table.Nrows), table.Nrows)

        For i As Integer = 0 To maxRows - 1
            Yield table.GetRow(orders(i))
        Next
    End Function

    Private Function BuildRowHtml(cells As IEnumerable(Of String),
                                  ordinals As FieldDescription(),
                                  css As CSSFile,
                                  isHeader As Boolean,
                                  maxWidth As Integer) As String

        Dim allStrs As String() = cells.ToArray
        Dim partStrs As IEnumerable(Of String)

        If ordinals Is Nothing Then
            partStrs = allStrs
        Else
            partStrs = From i As FieldDescription
                       In ordinals
                       Let str As String = allStrs(i)
                       Let val As String = i.ToString(isHeader, str)
                       Select val
        End If

        Return partStrs _
            .Select(Function(s)
                        s = s.Trim(""""c)

                        If maxWidth > 0 AndAlso s.Length > maxWidth Then
                            s = Mid(s, 1, maxWidth) & "..."
                        End If

                        If isHeader Then
                            Return $"<th style='{any.ToString(css("th")?.CSSValue)}'>{s}</th>"
                        Else
                            Return $"<td style='{any.ToString(css("td")?.CSSValue)}'>{s}</td>"
                        End If
                    End Function) _
            .JoinBy(vbCrLf)
    End Function
End Class
