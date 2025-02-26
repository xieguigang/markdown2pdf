#Region "Microsoft.VisualBasic::5af9e90421c8fca6dd127709d431694e, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//HTML/HTMLExtensions.vb"

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

    '   Total Lines: 77
    '    Code Lines: 56
    ' Comment Lines: 4
    '   Blank Lines: 17
    '     File Size: 3.28 KB


    ' Module HTMLExtensions
    ' 
    '     Properties: BackPreviousPage, Error404
    ' 
    '     Function: GetHTML, (+3 Overloads) SaveAsHTML, ToHTML
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.Framework.DATA
Imports Microsoft.VisualBasic.Scripting.MetaData

''' <summary>
''' 可能会出现中文字符，所以html文档必须要以Utf8编码进行保存
''' </summary>
''' 
<Package("GCModeller.HTML.ReportBuiulder", Publisher:="amethyst.asuka@gcmodeller.org")>
Public Module HTMLExtensions

    Public Const BR As String = "<br />"

    Const CSS As String = "<!-- <style>{CSS}</style> -->"

    <Extension>
    Public Function ToHTML(Of T As Class)(source As IEnumerable(Of T), Optional title As String = "", Optional describ As String = "") As String
        Dim table As String = HTMLWriter.ToHTMLTable(source)
        Dim html As New StringBuilder(My.Resources.index)

        If String.IsNullOrEmpty(title) Then title = GetType(T).Name

        Call html.Replace(CSS, $"<style>{My.Resources.foundation}</style>")
        Call html.Replace("{Title}", title)

        Dim innerDoc As New StringBuilder($"<p>{describ}</p>")
        Call innerDoc.AppendLine(table)
        Call html.Replace("{doc}", innerDoc.ToString)

        Return html.ToString
    End Function

    <Extension>
    Public Function SaveAsHTML(Of T As Class)(source As IEnumerable(Of T), saveHTML As String, Optional title As String = "", Optional describ As String = "") As Boolean
        Dim table As String = HTMLWriter.ToHTMLTable(source)
        Dim innerDoc As New StringBuilder($"<p>{describ}</p>")

        If String.IsNullOrEmpty(title) Then title = GetType(T).Name

        Call innerDoc.AppendLine(table)
        Return innerDoc.ToString.SaveAsHTML(saveHTML, title, describ)
    End Function

    <Extension, ExportAPI("Save.HTML")>
    Public Function SaveAsHTML(innerDoc As String, saveHTML As String, Optional title As String = "", Optional describ As String = "") As Boolean
        Dim outDIR As String = FileIO.FileSystem.GetParentPath(saveHTML)
        Call My.Resources.foundation.SaveTo($"{outDIR}/foundation.css")
        Return GetHTML(innerDoc, title).SaveTo(saveHTML, System.Text.Encoding.UTF8)
    End Function

    Public Function GetHTML(doc As String, Optional title As String = "") As String
        Dim html As New StringBuilder(My.Resources.index)

        Call html.Replace("{Title}", title)
        Call html.Replace("{doc}", doc)

        Return html.ToString
    End Function

    <Extension, ExportAPI("Save.HTML")>
    Public Function SaveAsHTML(innerDoc As StringBuilder, saveHTML As String, Optional title As String = "", Optional describ As String = "") As Boolean
        Return innerDoc.ToString.SaveAsHTML(saveHTML, title, describ)
    End Function

    Public ReadOnly Property BackPreviousPage As String = "<script type=""text/javascript"">
              function goBack()
  {
  window.history.back()
  }
        </script><p>
<h6><a href=""javascript:goBack()"">Back to previous page.</a></h6>
</p>"
    Public ReadOnly Property Error404 As String = GetHTML("<p>Oops! Something bad just happened......  <font size=""4""><strong>:-(</strong></font><br />
 </p><p>%EXCEPTION%</p>" & BackPreviousPage, "GCModeller Server ERROR")
End Module
