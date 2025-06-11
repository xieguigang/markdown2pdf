#Region "Microsoft.VisualBasic::b2fa7e66c614dece5588a6ced5fcddf9, G:/GCModeller/src/runtime/markdown2pdf/src/ReportBuilder//Interpolation/Interpolation.vb"

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

'   Total Lines: 195
'    Code Lines: 157
' Comment Lines: 10
'   Blank Lines: 28
'     File Size: 7.74 KB


' Module Interpolation
' 
'     Function: CreateResourceHtml, getCssString, Interpolate, parseCss, parseOpts
'               (+2 Overloads) ParseResourceFile, ParseResourceList, parseValue
' 
' /********************************************************************************/

#End Region

Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.MIME.Html.Language.CSS
Imports Microsoft.VisualBasic.My.JavaScript

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

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function CreateResourceHtml(res As ResourceDescription, workdir As String) As String
        Return res.GetResource.GetHtml(workdir)
    End Function

    <Extension>
    Public Function GetResource(res As ResourceDescription) As ResourceSolver
        Select Case res.type
            Case ResourceTypes.image : Return New ImageSolver(res)
            Case ResourceTypes.table : Return New TableSolver(res)
            Case ResourceTypes.text : Return New TextSolver(res)
            Case ResourceTypes.html : Return New TextSolver(res, isHtml:=True)
            Case Else
                Throw New NotImplementedException("unknown resource type: " & res.type.ToString)
        End Select
    End Function

    <Extension>
    Public Function ParseResourceFile(value As JsonElement, meta As Dictionary(Of String, String)) As ResourceDescription
        If TypeOf value Is JsonValue Then
            ' text
            Return New ResourceDescription With {
                .text = DirectCast(value, JsonValue).GetStripString(decodeMetachar:=True)
            }
        ElseIf TypeOf value Is JsonObject Then
            Return DirectCast(value, JsonObject).ParseResourceFile(meta)
        Else
            ' json array for ul list in html
            Throw New NotImplementedException
        End If
    End Function

    Private Function parseOpts(opts As JsonObject) As Dictionary(Of String, Object)
        Dim options As New Dictionary(Of String, Object)

        If opts.HasObjectKey("options") Then
            opts = opts("options")

            For Each key As String In opts.ObjectKeys
                options(key) = parseValue(opts(key))
            Next
        End If

        Return options
    End Function

    Private Function parseValue(val As JsonElement) As Object
        Select Case val.GetType
            Case GetType(JsonValue)
                Dim value = DirectCast(val, JsonValue)

                If value.UnderlyingType Is GetType(String) Then
                    Return value.GetStripString(decodeMetachar:=True)
                Else
                    Return value.value
                End If
            Case GetType(JsonArray)
                Dim array As Object() = DirectCast(val, JsonArray) _
                    .Select(Function(d)
                                If TypeOf d Is JsonValue Then
                                    Return DirectCast(d, JsonValue).GetStripString(decodeMetachar:=True)
                                Else
                                    Return parseValue(d)
                                End If
                            End Function) _
                    .ToArray

                Return array
            Case Else
                Dim obj As New JavaScriptObject
                Dim source As JsonObject = DirectCast(val, JsonObject)

                For Each keyName As String In source.ObjectKeys
                    obj(keyName) = parseValue(source(keyName))
                Next

                Return obj
        End Select
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
                    {"*", CssParser.ParseStyle(meta.getCssString(DirectCast(styles, JsonValue).GetStripString(decodeMetachar:=True)))}
                }
            }
        Else
            Dim list As JsonObject = DirectCast(styles, JsonObject)
            Dim css As New CSSFile With {.Selectors = New Dictionary(Of Selector)}

            For Each key As String In list.ObjectKeys
                Dim value As JsonValue = DirectCast(list(key), JsonValue)
                Dim cssStr As String = meta.getCssString(value.GetStripString(decodeMetachar:=True))
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
                .text = DirectCast(res("text"), JsonValue).GetStripString(decodeMetachar:=True),
                .styles = styles,
                .options = options
            }
        ElseIf "html" Like names Then
            Return New ResourceDescription With {
                .html = DirectCast(res("html"), JsonValue).GetStripString(decodeMetachar:=True),
                .styles = styles,
                .options = options
            }
        ElseIf "image" Like names Then
            Return New ResourceDescription With {
                .image = DirectCast(res("image"), JsonValue).GetStripString(decodeMetachar:=True),
                .styles = styles,
                .options = options
            }
        ElseIf "table" Like names Then
            Return New ResourceDescription With {
                .table = DirectCast(res("table"), JsonValue).GetStripString(decodeMetachar:=True),
                .styles = styles,
                .options = options
            }
        Else
            Throw New NotImplementedException
        End If
    End Function
End Module
