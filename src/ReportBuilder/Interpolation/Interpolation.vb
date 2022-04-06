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

Module Interpolation

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
    Public Function ParseResourceList(json As JsonObject) As Dictionary(Of String, ResourceDescription)
        Dim resources As New Dictionary(Of String, ResourceDescription)

        For Each name As String In json.ObjectKeys
            resources(name) = json(name).ParseResourceFile
        Next

        Return resources
    End Function

    <Extension>
    Public Function ParseResourceFile(value As JsonElement) As ResourceDescription
        If TypeOf value Is JsonValue Then
            ' text
            Return New ResourceDescription With {.text = DirectCast(value, JsonValue).GetStripString}
        ElseIf TypeOf value Is JsonObject Then
            Dim res As JsonObject = value
            Dim names As Index(Of String) = res.ObjectKeys.Indexing
            Dim styles As String = Nothing

            If "styles" Like names Then
                styles = DirectCast(res("styles"), JsonValue).GetStripString
            End If

            If "text" Like names Then
                Return New ResourceDescription With {
                    .text = DirectCast(res("text"), JsonValue).GetStripString,
                    .styles = styles
                }
            ElseIf "image" Like names Then
                Return New ResourceDescription With {
                    .image = DirectCast(res("image"), JsonValue).GetStripString,
                    .styles = styles
                }
            ElseIf "table" Like names Then
                Return New ResourceDescription With {
                    .table = DirectCast(res("table"), JsonValue).GetStripString,
                    .styles = styles
                }
            Else
                Throw New NotImplementedException
            End If
        Else
            ' json array for ul list in html
            Throw New NotImplementedException
        End If
    End Function
End Module
