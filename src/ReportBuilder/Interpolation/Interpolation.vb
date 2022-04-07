#Region "Microsoft.VisualBasic::ddddfa5186ae52117c86c3a0c97a9521, markdown2pdf\ReportBuilder\Interpolation\Interpolation.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xie (genetics@smrucc.org)
'       xieguigang (xie.guigang@live.com)
' 
' Copyright (c) 2018 GPL3 Licensed
' 
' 
' GNU GENERAL PUBLIC LICENSE (GPL3)
' 
' 
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with this program. If not, see <http://www.gnu.org/licenses/>.



' /********************************************************************************/

' Summaries:

' Module Interpolation
' 
'     Function: Interpolate
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS

Public Module Interpolation

    ''' <summary>
    ''' 在模板之中可能还会存在html碎片的插值
    ''' 在这里进行模板的html碎片的加载
    ''' 
    ''' 模板的插值格式为${relpath}
    ''' </summary>
    ''' <param name="templateUrl">the file path of the template</param>
    ''' <returns></returns>
    Public Function Interpolate(templateUrl As String) As String
        Dim dir As String = templateUrl.ParentPath
        Dim template As New StringBuilder(templateUrl.ReadAllText)
        Dim fragmentRefs As String() = template.ToString.Matches("[$]\{.+?\}", RegexICSng).ToArray
        Dim url As String
        Dim part As String

        For Each ref As String In fragmentRefs
            url = dir & "/" & ref.GetStackValue("{", "}")
            part = Interpolate(templateUrl:=url)
            template.Replace(ref, part)
        Next

        Return template.ToString
    End Function

    <Extension>
    Public Function ParseResourceList(json As JsonObject, meta As Dictionary(Of String, String)) As Dictionary(Of String, ResourceDescription)
        Dim resources As New Dictionary(Of String, ResourceDescription)

        For Each name As String In json.ObjectKeys
            resources(name) = json(name).ParseResourceFile(meta)
        Next

        Return resources
    End Function

    <Extension>
    Public Function CreateResourceHtml(res As ResourceDescription, workdir As String) As String
        Select Case res.type
            Case ResourceTypes.image : Return New ImageSolver(res).GetHtml(workdir)
            Case ResourceTypes.table : Return New TableSolver(res).GetHtml(workdir)
            Case ResourceTypes.text : Return New TextSolver(res).GetHtml(workdir)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function ParseResourceFile(value As JsonElement, meta As Dictionary(Of String, String)) As ResourceDescription
        If TypeOf value Is JsonValue Then
            ' text
            Return New ResourceDescription With {.text = DirectCast(value, JsonValue).GetStripString}
        ElseIf TypeOf value Is JsonObject Then
            Return DirectCast(value, JsonObject).ParseResourceFile(meta)
        Else
            ' json array for ul list in html
            Throw New NotImplementedException
        End If
    End Function

    Private Function parseOpts(opts As JsonObject) As Dictionary(Of String, Object)
        Dim options As New Dictionary(Of String, Object)
        Dim value As JsonValue

        If opts.HasObjectKey("options") Then
            opts = opts("options")

            For Each key As String In opts.ObjectKeys
                value = DirectCast(opts(key), JsonValue)

                If value.UnderlyingType Is GetType(String) Then
                    options(key) = value.GetStripString
                Else
                    options(key) = value.value
                End If
            Next
        End If

        Return options
    End Function

    <Extension>
    Private Function getCssString(meta As Dictionary(Of String, String), str As String) As String
        Dim css As New StringBuilder(str)

        For Each key As String In meta.Keys
            Call css.Replace($"${{{key}}}", meta(key))
        Next

        Return css.ToString
    End Function

    <Extension>
    Private Function parseCss(styles As JsonElement, meta As Dictionary(Of String, String)) As CSSFile
        If TypeOf styles Is JsonValue Then
            Return New CSSFile With {
                .Selectors = New Dictionary(Of Selector) From {
                    {"*", CssParser.ParseStyle(meta.getCssString(DirectCast(styles, JsonValue).GetStripString))}
                }
            }
        Else
            Dim list As JsonObject = DirectCast(styles, JsonObject)
            Dim css As New CSSFile With {.Selectors = New Dictionary(Of Selector)}

            For Each key As String In list.ObjectKeys
                Dim value As JsonValue = DirectCast(list(key), JsonValue)
                Dim cssStr As String = meta.getCssString(value.GetStripString)
                Dim style As Selector = CssParser.ParseStyle(cssStr)

                Call css.Selectors.Add(key, style)
            Next

            Return css
        End If
    End Function

    <Extension>
    Public Function ParseResourceFile(res As JsonObject, meta As Dictionary(Of String, String)) As ResourceDescription
        Dim names As Index(Of String) = res.ObjectKeys.Indexing
        Dim styles As CSSFile = Nothing
        Dim options As Dictionary(Of String, Object) = parseOpts(res)

        If "styles" Like names Then
            styles = res("styles").parseCss(meta)
        Else
            styles = New CSSFile With {.Selectors = New Dictionary(Of Selector)}
        End If

        If "text" Like names Then
            Return New ResourceDescription With {
                .text = DirectCast(res("text"), JsonValue).GetStripString,
                .styles = styles,
                .options = options
            }
        ElseIf "image" Like names Then
            Return New ResourceDescription With {
                .image = DirectCast(res("image"), JsonValue).GetStripString,
                .styles = styles,
                .options = options
            }
        ElseIf "table" Like names Then
            Return New ResourceDescription With {
                .table = DirectCast(res("table"), JsonValue).GetStripString,
                .styles = styles,
                .options = options
            }
        Else
            Throw New NotImplementedException
        End If
    End Function
End Module
